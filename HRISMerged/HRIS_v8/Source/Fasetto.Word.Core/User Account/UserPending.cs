using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Fasetto.Word.Core
{
    public class UserPending
    {
        public void AddPending(PendingItem item,string reason)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.ADD_PENDING";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", item.EMPID));
                cmd.Parameters.Add(new SqlParameter("@PENDING_TYPE", item.PENDING_TYPE));
                cmd.Parameters.Add(new SqlParameter("@PENDING_REASON", reason));
                cmd.Parameters.Add(new SqlParameter("@PENDING_STATUS", item.PENDING_STATUS));
                cmd.Parameters.Add(new SqlParameter("@PENDING_DATE", item.PENDING_DATE));
                cmd.Parameters.Add(new SqlParameter("@PENDING_POSITION", item.PENDING_POSITION));
                cmd.Parameters.Add(new SqlParameter("@PENDING_TIME", item.PENDING_TIME));
                cmd.Parameters.Add(new SqlParameter("@PENDING_LEAVE_FROM", item.PENDING_LEAVE_FROM));
                cmd.Parameters.Add(new SqlParameter("@PENDING_LEAVE_TO", item.PENDING_LEAVE_TO));
                cmd.Parameters.Add(new SqlParameter("@SEND_TO", item.SEND_TO));
                cmd.Parameters.Add(new SqlParameter("@APPROVED_BY", item.APPROVED_BY));
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }

        public void AddPendingOT(PendingItem item,string reason)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.ADD_PENDING_OT";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", item.EMPID));
                cmd.Parameters.Add(new SqlParameter("@PENDING_TYPE", item.PENDING_TYPE));
                cmd.Parameters.Add(new SqlParameter("@PENDING_REASON", reason));
                cmd.Parameters.Add(new SqlParameter("@PENDING_STATUS", item.PENDING_STATUS));
                cmd.Parameters.Add(new SqlParameter("@PENDING_DATE", item.PENDING_DATE));
                cmd.Parameters.Add(new SqlParameter("@PENDING_POSITION", item.PENDING_POSITION));
                cmd.Parameters.Add(new SqlParameter("@PENDING_TIME", item.PENDING_TIME));
                cmd.Parameters.Add(new SqlParameter("@PENDING_OT_FROM", item.PENDING_OT_FROM));
                cmd.Parameters.Add(new SqlParameter("@PENDING_OT_TO", item.PENDING_OT_TO));
                cmd.Parameters.Add(new SqlParameter("@SEND_TO", item.SEND_TO));
                cmd.Parameters.Add(new SqlParameter("@APPROVED_BY", item.APPROVED_BY));
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }

        public void GetPending(int empid)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GET_PENDING";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("EMP_ID", empid));
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;
                }
                while (reader.Read())
                {
                    var item = new PendingItem();
                    item.PENDING_ID = (int)reader["PENDING_ID"];
                    item.PENDING_NAME = (string)reader["FIRST_NAME"];
                    item.PENDING_TYPE = (string)reader["PENDING_TYPE"];
                    item.PENDING_REASON = (string)reader["PENDING_REASON"];
                    item.PENDING_STATUS = (string)reader["PENDING_STATUS"];
                    item.PENDING_DATE = (string)reader["PENDING_DATE"];
                    item.PENDING_POSITION = (string)reader["PENDING_POSITION"];
                    item.APPROVED_BY = (string)reader["APPROVED_BY"];

                    StaticPendingList.staticPendingList.Add(item);
                }
                db.Close();
            }
        }

        public void RetrievePending(int id , string status , string sendto)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GET_HEAD_APPROVAL";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", id));
                cmd.Parameters.Add(new SqlParameter("@PENDING_STATUS", status));
                cmd.Parameters.Add(new SqlParameter("@SEND_TO", sendto));
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;
                }
                while (reader.Read())
                {
                    var item = new PendingItem();
                    item.PENDING_ID = (int)reader["PENDING_ID"];
                    item.EMPID = (int)reader["EMP_ID"];
                    item.PENDING_NAME = (string)reader["FIRST_NAME"];
                    item.PENDING_TYPE = (string)reader["PENDING_TYPE"];
                    item.PENDING_REASON = (string)reader["PENDING_REASON"];
                    item.PENDING_STATUS = (string)reader["PENDING_STATUS"];
                    item.PENDING_DATE = (string)reader["PENDING_DATE"];
                    item.PENDING_POSITION = (string)reader["PENDING_POSITION"];

                    StaticApprovalList.staticApprovalList.Add(item);

                }
                db.Close();
            }
        }
        public void RetrieveDeclined(int id, string sendto)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GET_HEAD_DECLINED";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", id));
                cmd.Parameters.Add(new SqlParameter("@SEND_TO", sendto));
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;
                }
                while (reader.Read())
                {
                    var item = new PendingItem();
                    item.PENDING_ID = (int)reader["PENDING_ID"];
                    item.EMPID = (int)reader["EMP_ID"];
                    item.PENDING_NAME = (string)reader["FIRST_NAME"];
                    item.PENDING_TYPE = (string)reader["PENDING_TYPE"];
                    item.PENDING_REASON = (string)reader["PENDING_REASON"];
                    item.PENDING_STATUS = (string)reader["PENDING_STATUS"];
                    item.PENDING_DATE = (string)reader["PENDING_DATE"];
                    item.PENDING_POSITION = (string)reader["PENDING_POSITION"];

                    StaticApprovalList.staticApprovalList.Add(item);

                }
                db.Close();
            }
        }
        public void RetrieveApproved(int id, string sendto)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GET_HEAD_APPROVED";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", id));
                cmd.Parameters.Add(new SqlParameter("@SEND_TO", sendto));
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;
                }
                while (reader.Read())
                {
                    var item = new PendingItem();
                    item.PENDING_ID = (int)reader["PENDING_ID"];
                    item.EMPID = (int)reader["EMP_ID"];
                    item.PENDING_NAME = (string)reader["FIRST_NAME"];
                    item.PENDING_TYPE = (string)reader["PENDING_TYPE"];
                    item.PENDING_REASON = (string)reader["PENDING_REASON"];
                    item.PENDING_STATUS = (string)reader["PENDING_STATUS"];
                    item.PENDING_DATE = (string)reader["PENDING_DATE"];
                    item.PENDING_POSITION = (string)reader["PENDING_POSITION"];

                    StaticApprovalList.staticApprovalList.Add(item);

                }
                db.Close();
            }
        }
        public void SpecificPendingLeave(int id, string type)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();
                var sql = "dbo.GET_SPECIFIC_PENDING";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PENDING_ID", id));
                cmd.Parameters.Add(new SqlParameter("@PENDING_TYPE", type));
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;
                }
                while (reader.Read())
                {
                    var item = new PendingItem();
                    item.PENDING_ID = (int)reader["PENDING_ID"];
                    item.PENDING_NAME = (string)reader["FIRST_NAME"];
                    item.PENDING_TYPE = (string)reader["PENDING_TYPE"];
                    item.PENDING_STATUS = (string)reader["PENDING_STATUS"];
                    item.PENDING_DATE = (string)reader["PENDING_DATE"];
                    item.PENDING_POSITION = (string)reader["PENDING_POSITION"];
                    item.PENDING_LEAVE_FROM = (string)reader["PENDING_LEAVE_FROM"];
                    item.PENDING_LEAVE_TO = (string)reader["PENDING_LEAVE_TO"];
                    item.PENDING_LEAVE_REASON = (string)reader["PENDING_REASON"];

                    StaticApprovalItem.staticApprovalModalItem = item;
                }
                db.Close();
            }
        }
        public void SpecificPendingOT(int id, string type)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();
                var sql = "dbo.GET_SPECIFIC_PENDING";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PENDING_ID", id));
                cmd.Parameters.Add(new SqlParameter("@PENDING_TYPE", type));
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;
                }
                while (reader.Read())
                {
                    var item = new PendingItem();
                    item.PENDING_ID = (int)reader["PENDING_ID"];
                    item.PENDING_NAME = (string)reader["FIRST_NAME"];
                    item.PENDING_TYPE = (string)reader["PENDING_TYPE"];
                    item.PENDING_STATUS = (string)reader["PENDING_STATUS"];
                    item.PENDING_DATE = (string)reader["PENDING_DATE"];
                    item.PENDING_POSITION = (string)reader["PENDING_POSITION"];
                    item.PENDING_OT_FROM = (string)reader["PENDING_OT_FROM"];
                    item.PENDING_OT_TO = (string)reader["PENDING_OT_TO"];
                    item.PENDING_OT_REASON = (string)reader["PENDING_REASON"];
                    StaticApprovalItem.staticApprovalModalItem = item;
                }
                db.Close();
            }
        }
        public void Approve(string appby , int id, string status) 
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.UPDATE_PENDING";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PENDING_STATUS", status));
                cmd.Parameters.Add(new SqlParameter("@APPROVED_BY", appby));
                cmd.Parameters.Add(new SqlParameter("@PENDING_ID", id));
                cmd.ExecuteNonQuery();
                db.Close();

            }
        }
        public void DeleteSpecific(int id)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.DELETE_SPECIFIC_PENDING";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PENDING_ID", id));
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
        public void DeleteAll(int id)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.DELETE_ALL_PENDINGS";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", id));
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
        public void BetweenDate(int id , string fromdate , string todate)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GET_BETWEEN_DATE";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", id));
                cmd.Parameters.Add(new SqlParameter("@FROM_DATE", fromdate));
                cmd.Parameters.Add(new SqlParameter("@TO_DATE", todate));
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;
                }
                while (reader.Read())
                {
                    var item = new AttendanceItem();
                    item.FNAME = (string)reader["FIRST_NAME"];
                    item.TIMEIN = (string)reader["TIME_IN"];
                    item.TIMEOUT = (string)reader["TIME_OUT"];
                    item.DATE = (string)reader["LOG_DATE"];

                    StaticSortedListAttendance.staticSortedList.Add(item);
                }
                db.Close();
            }
        }
        public void ComboItems()
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GET_DEPARTMENT_HEADS";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;
                }
                while (reader.Read())
                {
                    var item = new ComboBoxItem();
                    item.POS_NAME = (string)reader["POS_NAME"];
                    item.POS_ID = (int)reader["POS_ID"];
                    Comboboxitem.ComboItem.Add(item);
                }
                db.Close();
            }
        }
    }
}
