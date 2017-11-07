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
    public class LeaveRegisterController : ApiController
    {
        public SqlConnection SqlCon { get; private set; }

        InventoryClass obj = new InventoryClass();
        

        [HttpGet]
        [Route("api/LeaveRegister/GetEmployeeData")]
        public IHttpActionResult GetEmployeeData()
        {
            SqlDataReader SqlDtr;
            string sql;
            List<string> Employee = new List<string>();
            sql = "select Emp_ID,Emp_Name from Employee where status='1' order by Emp_Name";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                Employee.Add(SqlDtr.GetValue(0).ToString() + ":" + SqlDtr.GetValue(1).ToString());
            }
            SqlDtr.Close();
            if (Employee.Count == 0 || Employee == null)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
            return Ok(Employee);

        }

        [HttpGet]
        [Route("api/LeaveRegister/CountLeaveRegister")]

        public IHttpActionResult CountLeaveRegister(string str1, string str2, string str3)
        {
            try
            {
                SqlDataReader SqlDtr;
                string sql;
                int count = 0;
                sql = "select count(*) from Leave_Register where Emp_ID='" + str1 + "' and cast(floor(cast(cast(date_from as datetime) as float)) as datetime) <='" + str2 + "' and cast(floor(cast(cast(date_to as datetime) as float)) as datetime)>='" + str3 + "' and isSanction=1";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    count = int.Parse(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();               
                return Ok(count);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "OT Register Not Found");
               
            }
            
        }

        [HttpGet]
        [Route("api/LeaveRegister/GetLeaveRegister")]

        public IHttpActionResult GetLeaveRegister(string str1, string str2, string str3)
        {
            try
            {
                SqlDataReader SqlDtr;
                string sql;
                int count = 0;
                sql = "select * from Leave_Register where Emp_ID='" + str1 + "' and cast(floor(cast(cast(date_from as datetime) as float)) as datetime) <='" + str2 + "' and cast(floor(cast(cast(date_to as datetime) as float)) as datetime)>='" + str3 + "' and isSanction=0";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    count = int.Parse(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();                
                return Ok(count);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "OT Register Not Found");                
            }            
        }
    }
}