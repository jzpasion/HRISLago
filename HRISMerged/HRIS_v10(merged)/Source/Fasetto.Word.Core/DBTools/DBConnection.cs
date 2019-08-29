using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    class DBConnection
    {
        public static SqlConnection CreateConnection()
        {
            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = "192.168.1.133,1433",
                UserID = "sa",
                Password = "password",
                InitialCatalog = "HRIS"

                //DataSource = ".",
                //InitialCatalog = "HRIS",
                //IntegratedSecurity = true
            };
            var db = new SqlConnection(builder.ToString());

            return db;
        }
    }
}
