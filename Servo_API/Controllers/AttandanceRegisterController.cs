using Servo_API.App_Start;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Servo_API.Controllers
{    
    public class AttandanceRegisterController : ApiController
    {

        public SqlConnection SqlCon { get; private set; }

        InventoryClass obj = new InventoryClass();        
        DbOperations_LATEST.DBUtil dbobj = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);


        [HttpGet]
        [Route("api/AttandanceRegister/SaveAttandance")]

        public string SaveAttandance(string Att_Date, string lblEmpID)
        {
            string str = "";
            SqlDataReader SqlDtr;
            string sql = "select Status from Attandance_Register where Att_Date='" + Att_Date + "' and  Emp_ID=" + lblEmpID + "";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                str = SqlDtr.GetValue(0).ToString();
            }
            SqlDtr.Close();
            return str;
        }

        [HttpGet]
        [Route("api/AttandanceRegister/DeleteAttandance")]

        public void DeleteAttandance(string Emp_ID, string Attan_Date)
        {            
            SqlDataReader SqlDtr;
            string sql = "delete from Attandance_Register where emp_id=" + Emp_ID + " and att_date='" + Attan_Date + "'";
            SqlDtr = obj.GetRecordSet(sql);            
            SqlDtr.Close();            
        }

    }
}