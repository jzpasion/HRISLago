using System;
using System.Data.SqlClient;

namespace Fasetto.Word.Core
{
    public class HolidayCollection
    {
        public void RetreiveAllHolidays()
        {
            StaticHolidayCollection.staticHolidayList.Clear();
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.spGetHolidays";
                var cmd = new SqlCommand(sql, db);
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

                    StaticHolidayCollection.staticHolidayList.Add(item);
                }

                db.Close();
            }
        }
    }
}
