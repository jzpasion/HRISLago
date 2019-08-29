using System.Data;
using System.Data.SqlClient;

namespace Fasetto.Word.Core
{
    public class TrainingManager
    {
        public void SaveTrainingData(TrainingItem myItem)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.spInsertTraining";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", myItem._EMP_ID));
                cmd.Parameters.Add(new SqlParameter("@TITLE", myItem._TITLE));
                cmd.Parameters.Add(new SqlParameter("@INSTITUTION", myItem._INSTITUTION));
                cmd.Parameters.Add(new SqlParameter("@TRAINING_DATE", myItem._TRAINING_DATE));
                cmd.Parameters.Add(new SqlParameter("@TRAINING_LOCATION", myItem._TRAINING_LOCATION));

                cmd.ExecuteNonQuery();
                db.Close();
            }
        }

        public void DeleteTrainData(string empId)
        {
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.spDeleteTraining";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMP_ID", empId));

                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
    }
}
