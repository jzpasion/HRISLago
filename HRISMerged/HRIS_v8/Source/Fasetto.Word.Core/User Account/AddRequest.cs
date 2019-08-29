using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Fasetto.Word.Core
{
    public class AddRequest
    {
        public void Addleave(RequestItem leaveitem)
        {
            using (var db = DBConnection.CreateConnection()) 
            {
                db.Open();

                var sql = "dbo.ADD_LEAVE";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", leaveitem.EMP_ID));
                cmd.Parameters.Add(new SqlParameter("@LEAVE_DATE", leaveitem.DATE));
                cmd.Parameters.Add(new SqlParameter("@TYPE", leaveitem.TYPE));
                cmd.Parameters.Add(new SqlParameter("@REASON", leaveitem.REASON));
                cmd.Parameters.Add(new SqlParameter("@STATUS", leaveitem.STATUS));
                cmd.Parameters.Add(new SqlParameter("@LEAVE_START", leaveitem.LEAVE_START));
                cmd.Parameters.Add(new SqlParameter("@LEAVE_END", leaveitem.LEAVE_END));
                cmd.ExecuteNonQuery();
                db.Close();

            }
        }

        public void AddOt(RequestItem requestitem)
        {
            using(var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.ADD_OVERTIME";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", requestitem.EMP_ID));
                cmd.Parameters.Add(new SqlParameter("@OT_DATE", requestitem.DATE));
                cmd.Parameters.Add(new SqlParameter("@PROJECT", requestitem.PROJECT));
                cmd.Parameters.Add(new SqlParameter("@REASON", requestitem.REASON));
                cmd.Parameters.Add(new SqlParameter("@STATUS", requestitem.STATUS));
                cmd.Parameters.Add(new SqlParameter("@TIME_FROM", requestitem.TIME_FROM));
                cmd.Parameters.Add(new SqlParameter("@TIME_TO", requestitem.TIME_TO));
                cmd.ExecuteNonQuery();
                db.Close();

            }
        }
    }
}
