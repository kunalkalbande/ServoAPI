using Servo_API.App_Start;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Servo_API.Controllers
{
    public class EmployeeListController : ApiController
    {
        public SqlConnection SqlCon { get; private set; }
        
        InventoryClass obj = new InventoryClass();
        DbOperations_LATEST.DBUtil dbobj = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);

        [HttpGet]
        [Route("api/EmployeeList/FetchData")]

        public IHttpActionResult FetchData(string str)
        {
            SqlDataReader SqlDtr;
            string sql;
            int Count=0;
            sql = "select count(*) from AccountsLedgerTable where Ledger_ID='" + str + "'";
            SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
            {
                Count = int.Parse(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            if (Count== 0)
            {
                return Content(HttpStatusCode.NotFound, "Ledger ID Not Found");
            }
            return Ok(Count);
        }

        [HttpGet]
        [Route("api/EmployeeList/DeleteEmployee")]

        public IHttpActionResult DeleteEmployee(string str)
        {
            SqlDataReader SqlDtr;
            string sql;            
            sql = "Delete from Employee Where Emp_ID='" + str + "'";
            SqlDtr = obj.GetRecordSet(sql);           
            SqlDtr.Close();
            return Ok();
        }

        [HttpGet]
        [Route("api/EmployeeList/DeleteLedgerMaster")]

        public IHttpActionResult DeleteLedgerMaster(string str)
        {
            SqlDataReader SqlDtr;
            string sql;
            sql = "Delete from Ledger_Master Where Ledger_ID='" + str + "'";
            SqlDtr = obj.GetRecordSet(sql);
            SqlDtr.Close();
            return Ok();
        }
    }
}