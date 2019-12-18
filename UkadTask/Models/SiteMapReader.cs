using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace UkadTask.Models
{
    public class SiteMapReader
    {
        private Uri _inputUri;
        private List<string> _urlList;
        private List<URLInfo> _urlInfoList;
        private Stopwatch _stopwatch;

        public SiteMapReader(string url)
        {
            UriBuilder uriBuilder = new UriBuilder(url);
            _inputUri = new Uri($"{uriBuilder.Scheme}://{uriBuilder.Host}");

            _urlList = new List<string>();
            _urlInfoList = new List<URLInfo>();

            _stopwatch = new Stopwatch();
        }

        public void GetUrlsFromSiteMap(string _url = null)
        {
            URLInfo urlInfo = new URLInfo();

            bool urlChanged = false;

            string sitemapUrl = null;

            if (string.IsNullOrEmpty(_url))
            {
                sitemapUrl = _inputUri + "sitemap.xml";
                urlChanged = true;
            }
            else
            {
                sitemapUrl = _url;
            }
            try
            {
                _stopwatch.Reset();
                _stopwatch.Start();
                XDocument xDoc = XDocument.Load(sitemapUrl);
                _stopwatch.Stop();

                urlInfo.Url = sitemapUrl;
                urlInfo.ElapsedTime = _stopwatch.ElapsedMilliseconds;
                _urlInfoList.Add(urlInfo);

                XElement urlset = xDoc.Element(XName.Get("urlset", xDoc.Root.Name.NamespaceName));
                if (urlset != null)
                {
                    _urlList = urlset.Elements(XName.Get("url", xDoc.Root.Name.NamespaceName))
                        .Select(x => x.Element(XName.Get("loc", xDoc.Root.Name.NamespaceName)).Value).ToList();
                }
                else
                {
                    XElement sitemapindex = xDoc.Element(XName.Get("sitemapindex", xDoc.Root.Name.NamespaceName));
                    if (sitemapindex != null)
                    {
                        List<string> sitemaps = sitemapindex.Elements(XName.Get("sitemap", xDoc.Root.Name.NamespaceName))
                            .Select(x => x.Element((XName.Get("loc", xDoc.Root.Name.NamespaceName))).Value).ToList();

                        List<Task> tasks = new List<Task>();

                        foreach (var sitemap in sitemaps)
                        {
                            string current = sitemap;
                            var task = Task.Run(() =>
                           {
                               GetUrlsFromSiteMap(current);
                           });
                            tasks.Add(task);
                        }

                        Task.WaitAll(tasks.ToArray());
                    }
                }
            }
            catch (WebException e)
            {
                if (!urlChanged)
                {
                    urlInfo.ElapsedTime = 0;
                    urlInfo.Url = sitemapUrl;
                }
            }
        }

        public void GetUrlsFromHTML()
        {
            URLInfo urlInfo = new URLInfo();

            try
            {
                HtmlWeb hw = new HtmlWeb();
                _stopwatch.Reset();
                _stopwatch.Start();
                HtmlDocument doc = hw.Load(_inputUri.ToString());
                _stopwatch.Stop();
                urlInfo.ElapsedTime = _stopwatch.ElapsedMilliseconds;
                urlInfo.Url = _inputUri.AbsoluteUri;
                _urlInfoList.Add(urlInfo);

                List<HtmlNode> nodesLinks = doc.DocumentNode.SelectNodes("//a[@href]")?.ToList();

                if (nodesLinks == null)
                {
                    return;
                }
                foreach (HtmlNode nodeLink in nodesLinks)
                {
                    string link = nodeLink.Attributes["href"].Value;

                    if (!Uri.IsWellFormedUriString(link, UriKind.Absolute))
                    {
                        UriBuilder builder = new UriBuilder();
                        builder.Scheme = _inputUri.Scheme;
                        builder.Host = _inputUri.Host;
                        builder.Path = link;
                        link = builder.Uri.AbsoluteUri;
                    }
                    if (Uri.IsWellFormedUriString(link, UriKind.Absolute))
                    {
                        _urlList.Add(link);
                    }
                }
            }
            catch (WebException e)
            {
                urlInfo.ElapsedTime = 0;
                urlInfo.Url = _inputUri.AbsoluteUri;
                _urlInfoList.Add(urlInfo);
            }
        }

        public List<URLInfo> MeasureResponseTime()
        {
            List<Task> tasks = new List<Task>();
            foreach (var url in _urlList)
            {
                var current = url;
                Task task = Task.Run(() =>
                {
                    var urlInfo = new URLInfo();
                    try
                    {
                        if (!Uri.IsWellFormedUriString(current, UriKind.Absolute))
                        {
                            return;
                        }

                        var stopwatch = new Stopwatch();

                        stopwatch.Start();
                        var request = (HttpWebRequest)WebRequest.Create(current);
                        WebResponse responce = request.GetResponse();
                        stopwatch.Stop();

                        urlInfo.ElapsedTime = stopwatch.ElapsedMilliseconds;
                        urlInfo.Url = current;
                        _urlInfoList.Add(urlInfo);
                    }
                    catch (WebException e)
                    {
                        urlInfo.ElapsedTime = 0;
                        urlInfo.Url = current;
                        _urlInfoList.Add(urlInfo);
                    }
                    catch (NotSupportedException) { }
                });
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
            return _urlInfoList;
        }
    }
}