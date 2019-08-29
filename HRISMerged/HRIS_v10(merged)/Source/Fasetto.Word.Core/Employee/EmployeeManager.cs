using System.Data;
using System.Data.SqlClient;

namespace Fasetto.Word.Core
{
    public class EmployeeManager
    {
        public void SaveData(EmployeeItem myItem)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.spInsertEmployee";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_NO", myItem._EMP_NO));
                cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", myItem._FIRST_NAME));
                cmd.Parameters.Add(new SqlParameter("@MIDDLE_NAME", myItem._MIDDLE_NAME));
                cmd.Parameters.Add(new SqlParameter("@LAST_NAME", myItem._LAST_NAME));
                cmd.Parameters.Add(new SqlParameter("@EMP_PASSWORD", myItem._EMP_PASSWORD));
                cmd.Parameters.Add(new SqlParameter("@GENDER", myItem._GENDER));
                cmd.Parameters.Add(new SqlParameter("@BIRTHDAY", myItem._BIRTHDAY));
                cmd.Parameters.Add(new SqlParameter("@NATIONALITY", myItem._NATIONALITY));
                cmd.Parameters.Add(new SqlParameter("@PASSPORT", myItem._PASSPORT));
                cmd.Parameters.Add(new SqlParameter("@RELIGION", myItem._RELIGION));
                cmd.Parameters.Add(new SqlParameter("@BIRTH_PLACE", myItem._BIRTH_PLACE));
                cmd.Parameters.Add(new SqlParameter("@EMP_STATUS", myItem._EMP_STATUS));
                cmd.Parameters.Add(new SqlParameter("@EMAIL_ADDRESS", myItem._EMAIL_ADDRESS));
                cmd.Parameters.Add(new SqlParameter("@CONTACT", myItem._CONTACT));
                cmd.Parameters.Add(new SqlParameter("@PRESENT_ADDRESS", myItem._PRESENT_ADDRESS));
                cmd.Parameters.Add(new SqlParameter("@PERMANENT_ADDRESS", myItem._PERMANENT_ADDRESS));
                cmd.Parameters.Add(new SqlParameter("@DATE_JOINED", myItem._DATE_JOINED));
                cmd.Parameters.Add(new SqlParameter("@END_PROVITION", myItem._END_PROVITION));
                cmd.Parameters.Add(new SqlParameter("@POS_ID", myItem._POS_ID));
                cmd.Parameters.Add(new SqlParameter("@IOE_PERSON", myItem._IOE_PERSON));
                cmd.Parameters.Add(new SqlParameter("@IOE_RELATION", myItem._IOE_RELATION));
                cmd.Parameters.Add(new SqlParameter("@IOE_ADDRESS", myItem._IOE_ADDRESS));
                cmd.Parameters.Add(new SqlParameter("@IOE_CONTACT", myItem._IOE_CONTACT));
                cmd.Parameters.Add(new SqlParameter("@HOURLY_RATE", myItem._HOURLY_RATE));
                cmd.Parameters.Add(new SqlParameter("@MONTHLY_SALARY", myItem._MONTHLY_SALARY));
                cmd.Parameters.Add(new SqlParameter("@SSS_NO", myItem._SSS_NO));
                cmd.Parameters.Add(new SqlParameter("@PHIL_HEALTH_NO", myItem._PHIL_HEALTH_NO));
                cmd.Parameters.Add(new SqlParameter("@PAG_IBIG_NO", myItem._PAG_IBIG_NO));
                cmd.Parameters.Add(new SqlParameter("@BIR_NO", myItem._BIR_NO));
                cmd.Parameters.Add(new SqlParameter("@DEDUC_SSS", myItem._DEDUC_SSS));
                cmd.Parameters.Add(new SqlParameter("@DEDUC_PHIL_HEALTH", myItem._DEDUC_PHIL_HEALTH));
                cmd.Parameters.Add(new SqlParameter("@DEDUC_PAG_IBIG", myItem._DEDUC_PAG_IBIG));
                cmd.Parameters.Add(new SqlParameter("@DEDUC_BIR", myItem._DEDUC_BIR));
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }

        public void UpdateData(EmployeeItem myItem)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.spUpdateEmployee";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_NO", myItem._EMP_NO));
                cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", myItem._FIRST_NAME));
                cmd.Parameters.Add(new SqlParameter("@MIDDLE_NAME", myItem._MIDDLE_NAME));
                cmd.Parameters.Add(new SqlParameter("@LAST_NAME", myItem._LAST_NAME));
                cmd.Parameters.Add(new SqlParameter("@GENDER", myItem._GENDER));
                cmd.Parameters.Add(new SqlParameter("@BIRTHDAY", myItem._BIRTHDAY));
                cmd.Parameters.Add(new SqlParameter("@NATIONALITY", myItem._NATIONALITY));
                cmd.Parameters.Add(new SqlParameter("@PASSPORT", myItem._PASSPORT));
                cmd.Parameters.Add(new SqlParameter("@RELIGION", myItem._RELIGION));
                cmd.Parameters.Add(new SqlParameter("@BIRTH_PLACE", myItem._BIRTH_PLACE));
                cmd.Parameters.Add(new SqlParameter("@EMP_STATUS", myItem._EMP_STATUS));
                cmd.Parameters.Add(new SqlParameter("@EMAIL_ADDRESS", myItem._EMAIL_ADDRESS));
                cmd.Parameters.Add(new SqlParameter("@CONTACT", myItem._CONTACT));
                cmd.Parameters.Add(new SqlParameter("@PRESENT_ADDRESS", myItem._PRESENT_ADDRESS));
                cmd.Parameters.Add(new SqlParameter("@PERMANENT_ADDRESS", myItem._PERMANENT_ADDRESS));
                cmd.Parameters.Add(new SqlParameter("@DATE_JOINED", myItem._DATE_JOINED));
                cmd.Parameters.Add(new SqlParameter("@END_PROVITION", myItem._END_PROVITION));
                cmd.Parameters.Add(new SqlParameter("@POS_ID", myItem._POS_ID));
                cmd.Parameters.Add(new SqlParameter("@IOE_PERSON", myItem._IOE_PERSON));
                cmd.Parameters.Add(new SqlParameter("@IOE_RELATION", myItem._IOE_RELATION));
                cmd.Parameters.Add(new SqlParameter("@IOE_ADDRESS", myItem._IOE_ADDRESS));
                cmd.Parameters.Add(new SqlParameter("@IOE_CONTACT", myItem._IOE_CONTACT));
                cmd.Parameters.Add(new SqlParameter("@HOURLY_RATE", myItem._HOURLY_RATE));
                cmd.Parameters.Add(new SqlParameter("@MONTHLY_SALARY", myItem._MONTHLY_SALARY));
                cmd.Parameters.Add(new SqlParameter("@SSS_NO", myItem._SSS_NO));
                cmd.Parameters.Add(new SqlParameter("@PHIL_HEALTH_NO", myItem._PHIL_HEALTH_NO));
                cmd.Parameters.Add(new SqlParameter("@PAG_IBIG_NO", myItem._PAG_IBIG_NO));
                cmd.Parameters.Add(new SqlParameter("@BIR_NO", myItem._BIR_NO));
                cmd.Parameters.Add(new SqlParameter("@DEDUC_SSS", myItem._DEDUC_SSS));
                cmd.Parameters.Add(new SqlParameter("@DEDUC_PHIL_HEALTH", myItem._DEDUC_PHIL_HEALTH));
                cmd.Parameters.Add(new SqlParameter("@DEDUC_PAG_IBIG", myItem._DEDUC_PAG_IBIG));
                cmd.Parameters.Add(new SqlParameter("@DEDUC_BIR", myItem._DEDUC_BIR));
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }

        public void DeleteData(string employeeID)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.spDeleteEmployee";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_NO", employeeID));
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }

        //public void UpdateData(EmployeeItem item)
        //{
        //    using (var db = DBConnection.CreateConnection())
        //    {
        //        db.Open();

        //        //var sql = "dbo.spUpdateEmployee";
        //        //var cmd = new SqlCommand(sql, db);
        //        //cmd.CommandType = CommandType.StoredProcedure;
        //        //cmd.Parameters.Add(new SqlParameter("@Employee_ID", item._employeeId));
        //        //cmd.Parameters.Add(new SqlParameter("@First_Name", item._firstName));
        //        //cmd.Parameters.Add(new SqlParameter("@Middle_Name", item._middleName));
        //        //cmd.Parameters.Add(new SqlParameter("@Last_Name", item._lastName));
        //        //cmd.Parameters.Add(new SqlParameter("@Nationality", item._nationality));
        //        //cmd.Parameters.Add(new SqlParameter("@Religion", item._religion));
        //        //cmd.Parameters.Add(new SqlParameter("@Email", item._eMail));
        //        //cmd.Parameters.Add(new SqlParameter("@Contact_Number", item._contactNum));
        //        //cmd.Parameters.Add(new SqlParameter("@Present_Address", item._presentAddress));
        //        //cmd.Parameters.Add(new SqlParameter("@Permanent_Address", item._permanentAddress));
        //        //cmd.ExecuteNonQuery();
        //        db.Close();
        //    }
        //}
    }
}
