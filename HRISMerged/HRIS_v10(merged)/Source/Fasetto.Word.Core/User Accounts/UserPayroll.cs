using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Fasetto.Word.Core
{
    public class UserPayroll
    {
        public void UPayroll(string id)
        {
            using(var db= DBConnection.CreateConnection())
            {
                db.Open();
                var sql = "dbo.GET_PAYROLL_DETAILS";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", id));
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;
                }
                while (reader.Read())
                {
                    PayrollItem item = new PayrollItem();
                    item.EMP_ID = (int)reader["EMP_ID"];
                    item.EMP_NO = (string)reader["EMP_NO"];
                    item.FIRST_NAME = (string)reader["FIRST_NAME"];
                    item.MIDDLE_NAME = (string)reader["MIDDLE_NAME"];
                    item.LAST_NAME = (string)reader["LAST_NAME"];
                    item.DEPARTMENT = (string)reader["POS_DEPARTMENT"];
                    item.SSS = (string)reader["SSS_NO"];
                    item.PAG_IBIG = (string)reader["PAG_IBIG_NO"];
                    item.PHIL = (string)reader["PHIL_HEALTH_NO"];
                    item.BIR = (string)reader["BIR_NO"];
                    item.DEDUC_SSS = (double)reader["DEDUC_SSS"];
                    item.DEDUC_PAG_IBIG = (double)reader["DEDUC_PAG_IBIG"];
                    item.DEDUC_PHIL = (double)reader["DEDUC_PHIL_HEALTH"];
                    item.DEDUC_BIR = (double)reader["DEDUC_BIR"];

                    PayrollDetails.paydetails = item;

                }
                db.Close();
            }
            
            

        }
        public void Salary(int id)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();
                var sql = "dbo.TIME_OUT";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", id));

                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    return;
                }
                while (reader.Read())
                {
                    PayrollItem item = new PayrollItem();
                    item.SINGLE_SALARY = (double)reader["MONTHLY_SALARY"];
                    item.HOURLY_RATE = (double)reader["HOURLY_RATE"];

                    SingleSalary.salary = item;

                }
                db.Close();
            }
        }

        public void between(int id , string from ,string to)
        {
            using(var db = DBConnection.CreateConnection())
            {
                db.Open();
                var sql = "dbo.GET_PAYROLL_TOTAL";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", id));
                cmd.Parameters.Add(new SqlParameter("@FROM_DATE", from));
                cmd.Parameters.Add(new SqlParameter("@TO_DATE", to));
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;
                }
                while (reader.Read())
                {
                    PayrollTotalItem item = new PayrollTotalItem();
                    item.EMP_ID = (int)reader["EMP_ID"];
                    item.DAYS_COUNT = (string)reader["LOG_DATE"];
                    item.TOTAL_EARNINGS = (double)reader["LOG_PAY_DAY"];
                    item.TOTAL_DEDUCTION = (double)reader["LOG_DEDUCTION"];
                    item.TOTAL_OVERTIME = (double)reader["LOG_OT_TOTAL"];

                    PayrollTotals.Totals.Add(item);
                }
                db.Close();
            }
        }
        public void GetId(string number)
        {
            using(var db = DBConnection.CreateConnection())
            {
                db.Open();
                var sql = "dbo.GET_ID_FROM_EMPNO";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_NO", number));
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;

                }
                while (reader.Read())
                {
                    PayrollItem item = new PayrollItem();
                    item.EMP_ID = (int)reader["EMP_ID"];
                    item.DEPARTMENT = (string)reader["POS_NAME"];

                    PayrollDetails.paydetails = item;
                }
            }
        }
    }
}
