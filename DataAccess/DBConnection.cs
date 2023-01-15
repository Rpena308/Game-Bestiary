using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;  // the namespace where sqlclient classes are found

namespace DataAccessLayer
{
    internal static class DBConnection
    {
        // this is the one place in the system that any code
        // should get a connection to the database
        public static SqlConnection GetConnection()
        {
            // this is the one place in the system the connnection string
            // should appear
            string connectionString =
                @"Data Source=localhost;Initial Catalog=beast_db_pm;Integrated Security=True";
            // connection string always include:
            //                                  data source (server)
            //                                  initial catalog (database)
            //                                  credentials for connecting


            SqlConnection connection = new SqlConnection(connectionString);

            return connection;
        }
    }
}
