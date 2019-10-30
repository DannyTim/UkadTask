using System.Collections.Generic;

namespace UkadTask.Models
{
    public class HistoryViewModel
    {
        public IEnumerable<URLInfo> URLInfos { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}