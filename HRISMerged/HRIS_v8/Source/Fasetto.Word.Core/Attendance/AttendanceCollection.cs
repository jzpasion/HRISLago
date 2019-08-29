using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    public class AttendanceCollection
    {
        public List<AttendanceItem> GetAttendance()
        {
            List<AttendanceItem> itemList = new List<AttendanceItem>();
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GET_ATTENDANCE";
                var cmd = new SqlCommand(sql, db);
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                while (reader.Read())
                {
                    var item = new AttendanceItem();

                    item._LOG_ID = (int)reader["LOG_ID"];
                    item._EMP_ID = (int)reader["EMP_ID"];
                    item._TIME_IN = (string)reader["TIME_IN"];
                    item._TIME_OUT = (string)reader["TIME_OUT"];
                    item._DATE = (string)reader["DATE"];


                    itemList.Add(item);
                }

                db.Close();
                return itemList;
            }
        }
    }
}
