using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Fasetto.Word.Core
{
    public class Userlogin
    {

        public UserItem RetrieveUser(string EMP_NO, string EMP_PASSWORD)
        {
            var item = new UserItem();
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GET_USER";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_NO", EMP_NO));
                cmd.Parameters.Add(new SqlParameter("@EMP_PASSWORD", EMP_PASSWORD));

                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                if (reader.Read())
                {
                    item._EMPID = (int)reader["EMP_ID"];
                    item._EMPNO = (string)reader["EMP_NO"];
                    item._FNAME = (string)reader["FIRST_NAME"];
                    item._LNAME = (string)reader["LAST_NAME"];
                    item._EMPPASSWORD = (string)reader["EMP_PASSWORD"];
                    item._EMAIL = (string)reader["EMAIL_ADDRESS"];
                    item._POSITION = (string)reader["POS_NAME"];
                    item._DEPARTMENT = (string)reader["POS_DEPARTMENT"];
                    //item._DATEJOINED = (string)reader["DATE_JOINED"];
                    //item._ENDPROVISION = (string)reader["END_PROVISION"];
                }
                db.Close();
                return item;
            }

        }
    

        
    }
}
