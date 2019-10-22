using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace UkadTask.Models
{
    public class DBtest
    {
        SqlCeConnection con = new SqlCeConnection(ConfigurationManager.ConnectionStrings["DBconnection"].ConnectionString);
        string query = @"CREATE TABLE TEST(Field nvarchar Null);";

        public void Connect()
        {
            con.Open();
        }

        public void AddTable()
        {
            SqlCeCommand cmd = new SqlCeCommand(query, con);
            cmd.ExecuteNonQuery();
        }
    }
}