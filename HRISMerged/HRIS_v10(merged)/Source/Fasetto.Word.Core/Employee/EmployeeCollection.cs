using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Fasetto.Word.Core
{
    public class EmployeeCollection
    {
        public void RetreiveAllEmployee()
        {
            StaticEmpoyeeCollection.staticEmployeeList.Clear();
            DBObjConverter objConverter = new DBObjConverter();
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GetEmployees";
                var cmd = new SqlCommand(sql, db);
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return;
                }

                while (reader.Read())
                {
                    var item = new EmployeeItem();

                    item._EMP_NO = objConverter.ConvertFromDBVal<string>(reader["EMP_NO"]);
                    item._FIRST_NAME = objConverter.ConvertFromDBVal<string>(reader["FIRST_NAME"]);
                    item._MIDDLE_NAME = objConverter.ConvertFromDBVal<string>(reader["MIDDLE_NAME"]);
                    item._LAST_NAME = objConverter.ConvertFromDBVal<string>(reader["LAST_NAME"]);
                    item._EMP_PASSWORD = objConverter.ConvertFromDBVal<string>(reader["EMP_PASSWORD"]);
                    item._GENDER = objConverter.ConvertFromDBVal<string>(reader["GENDER"]);
                    item._BIRTHDAY = objConverter.ConvertFromDBVal<string>(reader["BIRTHDAY"]);
                    item._NATIONALITY = objConverter.ConvertFromDBVal<string>(reader["NATIONALITY"]);
                    item._PASSPORT = objConverter.ConvertFromDBVal<string>(reader["PASSPORT"]);
                    item._RELIGION = objConverter.ConvertFromDBVal<string>(reader["RELIGION"]);
                    item._BIRTH_PLACE = objConverter.ConvertFromDBVal<string>(reader["BIRTH_PLACE"]);
                    item._EMP_STATUS = objConverter.ConvertFromDBVal<string>(reader["EMP_STATUS"]);
                    item._EMAIL_ADDRESS = objConverter.ConvertFromDBVal<string>(reader["EMAIL_ADDRESS"]);
                    item._CONTACT = objConverter.ConvertFromDBVal<string>(reader["CONTACT"]);
                    item._PRESENT_ADDRESS = objConverter.ConvertFromDBVal<string>(reader["PRESENT_ADDRESS"]);
                    item._PERMANENT_ADDRESS = objConverter.ConvertFromDBVal<string>(reader["PERMANENT_ADDRESS"]);
                    item._DATE_JOINED = objConverter.ConvertFromDBVal<string>(reader["DATE_JOINED"]);
                    item._END_PROVITION = objConverter.ConvertFromDBVal<string>(reader["END_PROVITION"]);
                    item._POS_ID = objConverter.ConvertFromDBVal<int>(reader["POS_ID"]);
                    item._IOE_PERSON = objConverter.ConvertFromDBVal<string>(reader["IOE_PERSON"]);
                    item._IOE_RELATION = objConverter.ConvertFromDBVal<string>(reader["IOE_RELATION"]);
                    item._IOE_ADDRESS = objConverter.ConvertFromDBVal<string>(reader["IOE_ADDRESS"]);
                    item._IOE_CONTACT = objConverter.ConvertFromDBVal<string>(reader["IOE_CONTACT"]);
                    item._HOURLY_RATE = objConverter.ConvertFromDBVal<double>(reader["HOURLY_RATE"]);
                    item._MONTHLY_SALARY = objConverter.ConvertFromDBVal<double>(reader["MONTHLY_SALARY"]);
                    item._SSS_NO = objConverter.ConvertFromDBVal<string>(reader["SSS_NO"]);
                    item._PHIL_HEALTH_NO = objConverter.ConvertFromDBVal<string>(reader["PHIL_HEALTH_NO"]);
                    item._PAG_IBIG_NO = objConverter.ConvertFromDBVal<string>(reader["PAG_IBIG_NO"]);
                    item._BIR_NO = objConverter.ConvertFromDBVal<string>(reader["BIR_NO"]);
                    item._DEDUC_SSS = objConverter.ConvertFromDBVal<double>(reader["DEDUC_SSS"]);
                    item._DEDUC_PHIL_HEALTH = objConverter.ConvertFromDBVal<double>(reader["DEDUC_PHIL_HEALTH"]);
                    item._DEDUC_PAG_IBIG = objConverter.ConvertFromDBVal<double>(reader["DEDUC_PAG_IBIG"]);
                    item._DEDUC_BIR = objConverter.ConvertFromDBVal<double>(reader["DEDUC_BIR"]);

                    StaticEmpoyeeCollection.staticEmployeeList.Add(item);
                }

                db.Close();
            }
        }

        public EmployeeItem RetreiveSpecificEmployee(string employeeID)
        {
            var item = new EmployeeItem();
            using (var db = DBConnection.CreateConnection())
            {
                db.Open();

                var sql = "dbo.GetSpecificEmployee";
                var cmd = new SqlCommand(sql, db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Employee_ID", employeeID));
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                if (reader.Read())
                {
                    //item._employeeId = (string)reader["Empolyee_ID"];
                    //item._firstName = (string)reader["First_Name"];
                    //item._middleName = (string)reader["Middle_Name"];
                    //item._lastName = (string)reader["Last_Name"];
                    //item._nationality = (string)reader["Nationality"];
                    //item._eMail = (string)reader["Email"];
                    //item._contactNum = (string)reader["Contact_Number"];
                    //item._religion = (string)reader["Religion"];
                    //item._presentAddress = (string)reader["Present_Address"];
                    //item._permanentAddress = (string)reader["Permanent_Address"];
                }

                db.Close();

                return item;
            }
        }
    }
}
