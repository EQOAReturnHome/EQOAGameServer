
using System.Configuration;
using MySql.Data.MySqlClient;

namespace ReturnHome.SQL
{
    class SQLBase
    {
        public MySqlConnection con { get; private set; }

        public SQLBase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DevLocal"].ConnectionString;

            //Set connection property from connection string and open connection
            using MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
        }
    }
}
