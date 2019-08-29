using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    public class UserHoliday
    {
        public void getUpcoming()
        {
            using(var db = DBConnection.CreateConnection())
            {
                db.Open();
                var sql = "dbo.GET_HOLIDAY";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;
                }
                while (reader.Read())
                {
                    var item = new HolidayItem();
                    item._HOLIDAY_ID = (int)reader["HOLIDAY_ID"];
                    item._HOLIDAY_NAME = (string)reader["HOLIDAY_NAME"];
                    item._HOLIDAY_DATE = (DateTime)reader["HOLIDAY_DATE"];
                    item._HOLIDAY_TYPE = (string)reader["HOLIDAY_TYPE"];
                    holidays.holitem =item;


                }
                db.Close();
            }
        }
    }
}
