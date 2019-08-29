using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    public class ExperienceManager
    {
        public void SaveExpData(ExperienceItem myItem)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.spInsertExperience";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_NO", myItem._EMP_NO));
                cmd.Parameters.Add(new SqlParameter("@DESIGNATION", myItem._DESIGNATION));
                cmd.Parameters.Add(new SqlParameter("@COMPANY", myItem._COMPANY));
                cmd.Parameters.Add(new SqlParameter("@DATE_START", myItem._DATE_START));
                cmd.Parameters.Add(new SqlParameter("@DATE_END", myItem._DATE_END));
                cmd.Parameters.Add(new SqlParameter("@WORK_LOCATION", myItem._WORK_LOCATION));

                cmd.ExecuteNonQuery();
                db.Close();
            }
        }

        public void DeleteExpData(string empId)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.spDeleteExperience";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_NO", empId));

                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
    }
}
