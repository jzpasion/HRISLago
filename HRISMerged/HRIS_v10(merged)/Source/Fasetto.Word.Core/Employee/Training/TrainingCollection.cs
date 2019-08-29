using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Fasetto.Word.Core
{
    public class TrainingCollection
    {
        public List<TrainingItem> RetreiveEmpTraining(string employeeID)
        {
            var itemList = new List<TrainingItem>();
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GetSpecificTraining";
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
                    var item = new TrainingItem();
                    item._EMP_ID = (string)reader["EMP_ID"];
                    item._TITLE = (string)reader["TITLE"];
                    item._INSTITUTION = (string)reader["INSTITUTION"];
                    item._TRAINING_DATE = (string)reader["TRAINING_DATE"];
                    item._TRAINING_LOCATION = (string)reader["TRAINING_LOCATION"];

                    itemList.Add(item);
                }

                db.Close();

                return itemList;
            }
        }
    }
}
