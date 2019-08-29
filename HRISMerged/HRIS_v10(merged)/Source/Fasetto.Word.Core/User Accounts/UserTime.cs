using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    public class UserTime
    {

        public void Time_in(TimeItem timeitem)
        {
            using(var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.ADD_ATTENDANCE";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", timeitem.EMP_ID));
                cmd.Parameters.Add(new SqlParameter("@TIME_IN", timeitem.TIME_IN));
                cmd.Parameters.Add(new SqlParameter("@TIME_OUT", timeitem.TIME_OUT));
                cmd.Parameters.Add(new SqlParameter("@LOG_DATE", timeitem.DATE));
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
        public void leave(int id , string timein , string timeout, string date)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.ADD_ATTENDANCE";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", id));
                cmd.Parameters.Add(new SqlParameter("@TIME_IN", timein));
                cmd.Parameters.Add(new SqlParameter("@TIME_OUT", timeout));
                cmd.Parameters.Add(new SqlParameter("@LOG_DATE", date));
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
        public void getclockin(int id)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GET_CLOCK_IN";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", id));
             var reader =   cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;
                }
                while (reader.Read())
                {
                    var timeItem = new TimeItem();
                    timeItem.TIME_IN = (string)reader["CLOCK_IN"];
                    timeItem.DATE = (string)reader["LOG_DATE"];
                    ClockInItem.Clockin = timeItem;  
                }

                db.Close();
            }
        }
        public TimeItem Checker(int id)
        {
            var item = new TimeItem();
            using (var db= DBConnection.CreateConnection())
            {
                db.Open();
                var sql = "dbo.TIME_OUT_CHECKER";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", id));

                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                if (reader.Read())
                {
                    item.TIME_OUT = (string)reader["CLOCK_OUT"];
                    item.LOG_ID = (int)reader["LOG_ID"];

                    LogItem.staticLogIdItem = item;
                }
                db.Close();
                return item;

            }

        }

        public void GetSpecificId(TimeItem timeitem)
        {
            
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GET_SPECIFIC_LOG_ID";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", timeitem.EMP_ID));
                cmd.Parameters.Add(new SqlParameter("@TIME_IN", timeitem.TIME_IN));
                cmd.Parameters.Add(new SqlParameter("@LOG_DATE", timeitem.DATE));
                cmd.Parameters.Add(new SqlParameter("@TIME_OUT", timeitem.TIME_OUT));
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;
                }
                if (reader.Read())
                   
                {
                    var item = new TimeItem();
                    item.LOG_ID = (int)reader["LOG_ID"];

                    LogItem.staticLogIdItem = item;
                }
                db.Close();
         
            }
        }

        public void Time_out(TimeItem timeitem)
        {
            using(var db = DBConnection.CreateConnection())
            {
                db.Open();
                var sql = "dbo.TIME_OUT";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TIME_OUT", timeitem.TIME_OUT));
                cmd.Parameters.Add(new SqlParameter("@LOG_TIME", timeitem.HOURS));
                cmd.Parameters.Add(new SqlParameter("@LOG_ID", timeitem.LOG_ID));
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", timeitem.EMP_ID));
                cmd.Parameters.Add(new SqlParameter("@LOG_MINUTES", timeitem.MINUTES));
                cmd.Parameters.Add(new SqlParameter("@LOG_PAY_DAY", timeitem.PAY_DAY));
                cmd.Parameters.Add(new SqlParameter("@REMARKS", timeitem.REMARKS));
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }

    }
}
