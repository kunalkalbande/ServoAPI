using Servo_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servo_API.Controllers
{
    public class EmployeeController : ApiController
    {
        private SqlConnection SqlCon;

        public void Post([FromBody]EmployeeModels employee)
        {
            using (SqlCon = new SqlConnection())
            {
                SqlCon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ServoSMS"]);
                SqlCon.Open();
                string str = "";
                string sql;
                EmployeeModels obj1 = new EmployeeModels();
                obj1.Att_Date = employee.Att_Date;

                obj1.Emp_ID = employee.Emp_ID;
                obj1.Status = "1";
                sql = "select Status from Attandance_Register where Att_Date='" + obj1.Att_Date + "' and  Emp_ID=" + obj1.Emp_ID + "";
                var SqlCmd = new SqlCommand(sql, SqlCon);
                var SqlDtr = SqlCmd.ExecuteReader();
                while (SqlDtr.Read())
                {
                    str = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();

                if (str.Equals("0") || str.Equals(""))
                {
                    SqlCmd = new SqlCommand("ProEmpAttadanceEntry", SqlCon);
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlCmd.Parameters.Add("@Att_Date", employee.Att_Date.ToString());
                    SqlCmd.Parameters.Add("@Emp_ID", employee.Emp_ID.ToString());
                    SqlCmd.Parameters.Add("@Status", employee.Status.ToString());
                    SqlCmd.ExecuteNonQuery();
                }
                else
                {
                    obj1.Att_Date = employee.Att_Date.ToString();

                    obj1.Emp_ID = employee.Emp_ID;
                    obj1.Status = "1";
                    SqlCmd = new SqlCommand("ProEmpAttadanceUpdate", SqlCon);
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlCmd.Parameters.Add("@Att_Date", employee.Att_Date.ToString());
                    SqlCmd.Parameters.Add("@Emp_ID", employee.Emp_ID.ToString());
                    SqlCmd.Parameters.Add("@Status", employee.Status.ToString());
                    SqlCmd.ExecuteNonQuery();
                    //sql="insert into Attandance_Register (Att_Date,Emp_Id,Status)  values('"+Att_Date+"','"+Emp_ID+"','"+Status+"')";
                    SqlCmd = new SqlCommand(sql, SqlCon);
                    SqlCmd.ExecuteNonQuery();
                }
            }

        }
    }
}
