using System;
using System.Data;
using System.Data.SqlClient;

namespace Fasetto.Word.Core
{
    public class EducationManager
    {
        public void SaveEduData(EducationItem myItem)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.spInsertEducation";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", myItem._EMP_ID));
                cmd.Parameters.Add(new SqlParameter("@EDU_LEVEL", myItem._EDU_LEVEL));
                cmd.Parameters.Add(new SqlParameter("@EDU_SCHOOL_NAME", myItem._EDU_SCHOOL_NAME));
                cmd.Parameters.Add(new SqlParameter("@EDU_SCHOOL_ADDRESS", myItem._EDU_SCHOOL_ADDRESS));
                cmd.Parameters.Add(new SqlParameter("@EDU_DATE_GRADUATED", myItem._EDU_DATE_GRADUATED));
                cmd.Parameters.Add(new SqlParameter("@EDU_DEGREE_EARNED", (string.IsNullOrEmpty(myItem._EDU_DEGREE_EARNED) ? DBNull.Value.ToString() : myItem._EDU_DEGREE_EARNED)));
             
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }

        public void DeleteEduData(string empId)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.spDeleteEducation";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", empId));

                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
    }
}
