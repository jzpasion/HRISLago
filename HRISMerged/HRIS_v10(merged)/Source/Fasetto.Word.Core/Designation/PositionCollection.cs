using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    public class PositionCollection
    {
        public void RetreiveAllPositions()
        {
            StaticPositionCollection.staticPositionList.Clear();
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GetPositions";
                var cmd = new SqlCommand(sql, db);
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;
                }

                while (reader.Read())
                {
                    var item = new DesignationItem();

                    item._POS_ID = (int)reader["POS_ID"];
                    item._POS_NAME = (string)reader["POS_NAME"];
                    item._POS_DEPARTMENT = (string)reader["POS_DEPARTMENT"];

                    StaticPositionCollection.staticPositionList.Add(item);
                }

                db.Close();
            }
        }
    }
}
