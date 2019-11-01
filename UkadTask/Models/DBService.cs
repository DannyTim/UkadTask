using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlServerCe;

namespace UkadTask.Models
{
    public class DBService
    {
        SqlCeConnection _connection = new SqlCeConnection(ConfigurationManager.ConnectionStrings["DBconnection"].ConnectionString);
        public DBService()
        {
            _connection.Open();
        }

        public void CreateTableForURLs(string tableName = "URLs")
        {
            string query = $@"CREATE TABLE {tableName} (URL nvarchar(1000) NOT NULL, ElapsedTime nvarchar(1000) NOT NULL);";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            cmd.ExecuteNonQuery();
        }

        public void DropTable(string tableName = "URLs")
        {
            string query = $@"DROP TABLE {tableName}";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            cmd.ExecuteNonQuery();
        }

        public void AddURLInfos(List<URLInfo> URLInfos, string tableName = "URLs")
        {
            string query;
            SqlCeCommand cmd = null;

            foreach (var urlInfo in URLInfos)
            {
                query = $@"INSERT INTO {tableName} VALUES ('{urlInfo.Url}', '{urlInfo.ElapsedTime}')";
                cmd = new SqlCeCommand(query, _connection);
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveAllURLInfos(string tableName = "URLs")
        {
            string query = $@"DELETE FROM {tableName}";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            cmd.ExecuteNonQuery();
        }

        public List<URLInfo> GetAllURLInfos(string tableName = "URLs")
        {
            List<URLInfo> urlInfos = new List<URLInfo>();
            var query = $@"SELECT * FROM {tableName}";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            SqlCeDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount - 1; i++)
                {
                    urlInfos.Add(new URLInfo() { Url = reader.GetString(0), ElapsedTime = Convert.ToInt32(reader.GetString(1)) });
                }
            }
            return urlInfos;
        }

        public List<URLInfo> GetByNameURLInfos(string searchWord, string tableName = "URLs")
        {
            List<URLInfo> urlInfos = new List<URLInfo>();
            var query = $@"SELECT * FROM {tableName} WHERE URL LIKE '%{searchWord}%'";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            SqlCeDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount - 1; i++)
                {
                    urlInfos.Add(new URLInfo() { Url = reader.GetString(0), ElapsedTime = Convert.ToInt32(reader.GetString(1)) });
                }
            }
            return urlInfos;
        }
    }
}