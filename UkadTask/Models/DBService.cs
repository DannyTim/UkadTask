using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlServerCe;

namespace UkadTask.Models
{
    public class DBService
    {
        private SqlCeConnection _connection
        {
            get
            {
                var connection = new SqlCeConnection(ConfigurationManager.ConnectionStrings["DBconnection"].ConnectionString);
                connection.Open();
                return connection;
            }
        }

        public void CreateTableForURLs(string tableName)
        {
            string query = $@"CREATE TABLE {tableName} (URL varchar(max) NOT NULL, ElapsedTime nvarchar(max) NOT NULL);";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            using (_connection)
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //TODO: Add action
                }
            }
        }

        public void DropTable(string tableName)
        {
            string query = $@"DROP TABLE {tableName}";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            using (_connection)
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //TODO: Add action
                }
            }
        }

        public void AddURLInfos(List<URLInfo> URLInfos, string tableName)
        {
            string query;
            SqlCeCommand cmd = null;
            using (_connection)
            {
                foreach (var urlInfo in URLInfos)
                {
                    query = $@"INSERT INTO {tableName} VALUES ('{urlInfo.Url}', '{urlInfo.ElapsedTime}')";
                    cmd = new SqlCeCommand(query, _connection);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveAllURLInfos(string tableName)
        {
            string query = $@"DELETE FROM {tableName}";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            using (_connection)
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //TODO: Add action
                }
            }
        }

        public List<URLInfo> GetAllURLInfos(string name)
        {
            List<URLInfo> urlInfos = new List<URLInfo>();
            var query = $@"SELECT * FROM {name}";
            SqlCeCommand cmd = new SqlCeCommand(query, _connection);
            using (_connection)
            {
                SqlCeDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount - 1; i++)
                    {
                        urlInfos.Add(new URLInfo() { Url = reader.GetString(0), ElapsedTime = Convert.ToInt32(reader.GetString(1)) });
                    }
                }
            }
            return urlInfos;
        }
    }
}