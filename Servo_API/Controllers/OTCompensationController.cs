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
    public class OTCompensationController : ApiController
    {
        InventoryClass obj = new InventoryClass();
        DbOperations_LATEST.DBUtil dbobj = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);

        [HttpGet]
        [Route("api/OTCompensation/GetEmployeeData")]
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
        [Route("api/OTCompensation/CountOTRegister")]

        public IHttpActionResult CountOTRegister(string str1,string str2,string str3)
        {
            try
            {
                SqlDataReader SqlDtr;
                string sql;
                int count = 0;
                sql = "select count(*) from OverTime_Register where Emp_ID='" + str1 + "' and cast(floor(cast(cast(ot_from as datetime) as float)) as datetime) <='" + str2 + "' and cast(floor(cast(cast(ot_to as datetime) as float)) as datetime)>='" + str3 + "'";
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
        [Route("api/OTCompensation/GetOTRegisterData")]

        public IHttpActionResult GetOTRegisterData(string str1, string str2, string str3)
        {
            try
            {
                SqlDataReader SqlDtr;
                string sql;
                int count = 0;
                sql = "select * from OverTime_Register where Emp_ID='" + str1 + "' and cast(floor(cast(cast(ot_from as datetime) as float)) as datetime) <='" + str2 + "' and cast(floor(cast(cast(ot_to as datetime) as float)) as datetime)>='" + str3 + "'";
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
        [Route("api/OTCompensation/DeleteOTRegisterData")]

        public IHttpActionResult DeleteOTRegisterData(string str1, string str2, string str3)
        {            
            int x=0;
            dbobj.Insert_or_Update("delete from OverTime_Register where Emp_ID='" + str1 + "' and cast(floor(cast(cast(ot_from as datetime) as float)) as datetime) <='" + str2 + "' and cast(floor(cast(cast(ot_to as datetime) as float)) as datetime)>='" + str3 + "'", ref x);
            return Ok();
        }

        [HttpGet]
        [Route("api/OTCompensation/InsertOTRegisterData")]

        public IHttpActionResult InsertOTRegisterData(string LeaveID, string Todate, string Emp_id,string datefrom,string dateto)
        {            
            int x = 0;
            dbobj.Insert_or_Update("Insert into OverTime_Register (OT_ID,OT_Date,Emp_Id,OT_From,Ot_To) values(" + LeaveID + ",'" + Todate + "'," + Emp_id + ",'" + datefrom + "','" + dateto + "')", ref x);            
            return Ok();
        }

        [HttpGet]
        [Route("api/OTCompensation/GetNextOTID")]

        public IHttpActionResult GetNextOTID()
        {
            try
            {
                SqlDataReader SqlDtr;
                string sql, nextid = "";
                sql = "select max(OT_ID) from OverTime_Register";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    if (nextid == null || nextid == "")
                    {
                        nextid = SqlDtr.GetValue(0).ToString();
                    }
                    else
                        nextid = "1";
                }
                SqlDtr.Close();
                return Ok(nextid);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
            
        }
    }
}