using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    public class EducationCollection
    {
        public List<EducationItem> RetreiveEmpEducation(string employeeID)
        {
            var itemList = new List<EducationItem>();
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GetSpecificEducation";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", employeeID));
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                while(reader.Read())
                {
                    var item = new EducationItem();
                    item._EDU_LEVEL = (string)reader["EDU_LEVEL"];
                    item._EMP_ID = (string)reader["EMP_ID"];
                    item._EDU_SCHOOL_NAME = (string)reader["EDU_SCHOOL_NAME"];
                    item._EDU_SCHOOL_ADDRESS = (string)reader["EDU_SCHOOL_ADDRESS"];
                    item._EDU_DATE_GRADUATED = (string)reader["EDU_DATE_GRADUATED"];
                    item._EDU_DEGREE_EARNED = (string)reader["EDU_DEGREE_EARNED"];

                    itemList.Add(item);
                }

                db.Close();

                return itemList;
            }
        }
    }
}
