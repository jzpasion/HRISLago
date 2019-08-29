using System.Data;
using System.Data.SqlClient;

namespace Fasetto.Word.Core
{
    public class HolidayManager
    {
        public void SaveHoliday(HolidayItem myItem)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.spInsertHoliday";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@HOLIDAY_NAME", myItem._HOLIDAY_NAME));
                cmd.Parameters.Add(new SqlParameter("@HOLIDAY_DATE", myItem._HOLIDAY_DATE));
                cmd.Parameters.Add(new SqlParameter("@HOLIDAY_TYPE", myItem._HOLIDAY_TYPE));
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }

        public void UpdateHoliday(HolidayItem myItem)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.spUpdateHoliday";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@HOLIDAY_ID", myItem._HOLIDAY_ID));
                cmd.Parameters.Add(new SqlParameter("@HOLIDAY_NAME", myItem._HOLIDAY_NAME));
                cmd.Parameters.Add(new SqlParameter("@HOLIDAY_DATE", myItem._HOLIDAY_DATE));
                cmd.Parameters.Add(new SqlParameter("@HOLIDAY_TYPE", myItem._HOLIDAY_TYPE));
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }

        public void DeleteHoliday(int holidayId)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.spDeleteHoliday";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@HOLIDAY_ID", holidayId));
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
    }
}
