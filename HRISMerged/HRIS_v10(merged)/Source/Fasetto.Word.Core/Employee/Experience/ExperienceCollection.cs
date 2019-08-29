using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Fasetto.Word.Core
{
    public class ExperienceCollection
    {
        public List<ExperienceItem> RetreiveEmpExperience(string employeeID)
        {
            var itemList = new List<ExperienceItem>();
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GetSpecificExperience";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_NO", employeeID));
                var reader = cmd.ExecuteReader();

                while(!reader.HasRows)
                {
                    return null;
                }

                while(reader.Read())
                {
                    var item = new ExperienceItem();
                    item._EMP_NO = (string)reader["EMP_NO"];
                    item._DESIGNATION = (string)reader["DESIGNATION"];
                    item._COMPANY = (string)reader["COMPANY"];
                    item._DATE_START = (string)reader["DATE_START"];
                    item._DATE_END = (string)reader["DATE_END"];
                    item._WORK_LOCATION = (string)reader["WORK_LOCATION"];

                    itemList.Add(item);
                }

                db.Close();

                return itemList;
            }
        }
    }
}
