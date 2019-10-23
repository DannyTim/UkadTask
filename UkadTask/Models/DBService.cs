using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlServerCe;

namespace UkadTask.Models
{
    public class DBService
    {
        private SqlCeConnection _connection = new SqlCeConnection(ConfigurationManager.ConnectionStrings["DBconnection"].ConnectionString);

        public DBService()
        {
            _connection.Open();
        }

        public string ParceName(string name)
        {
            name = name.Replace("www.", string.Empty);
            name = name.Replace(".com", string.Empty);
            name = name.Replace(".ua", string.Empty);
            name = name.Replace(".", string.Empty);

            return name;
        }

        public void AddTable(string name)
        {
            name = ParceName(name);

            string query = $@"CREATE TABLE {name}(URL nvarchar NOT NULL, ElapsedTime smallint NOT NULL);";

            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            cmd.ExecuteNonQuery();
        }

        public List<URLInfo> GetURLInfos(string name)
        {
            List<URLInfo> urlInfos = new List<URLInfo>();

            name = ParceName(name);

            var query = $@"SELECT * FROM {name}";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            SqlCeDataReader reader = cmd.ExecuteReader();

            string url;
            int elapsedTime;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    url = reader.GetString(0);
                    elapsedTime = reader.GetInt32(1);
                    urlInfos.Add(new URLInfo(){Url = url, ElapsedTime = elapsedTime});
                }
            }

            return new List<URLInfo>();
        }

        public void AddURLInfos(List<URLInfo> URLInfos, string name)
        {
            name = ParceName(name);

            var query = $@"INSERT INTO {name} VALUES ('hello', '0')"; //{URLInfos[0].Url} {URLInfos[0].ElapsedTime}
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            cmd.ExecuteNonQuery(); 
        }

        public void RemoveURLInfos()
        {

        }

        ~DBService()
        {
            _connection.Close();
        }
    }
}