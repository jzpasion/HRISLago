using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    public class GetAttendance
    {
        public void GetUserAttendance(int empid)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GET_ATTENDANCE";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", empid));
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;
                }

                while (reader.Read())
                {
                    var item = new AttendanceItem();
                    item.LOG_ID = (int)reader["LOG_ID"];
                    item.FNAME = (string)reader["FIRST_NAME"];
                    item.TIMEIN = (string)reader["TIME_IN"];
                    item.TIMEOUT = (string)reader["TIME_OUT"];
                    item.DATE = (string)reader["LOG_DATE"];

                    StaticAttendanceList.staticAttendanceList.Add(item);

                }
                db.Close();
            }
        }
    }
}
