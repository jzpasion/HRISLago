﻿using System;
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
    }
}
