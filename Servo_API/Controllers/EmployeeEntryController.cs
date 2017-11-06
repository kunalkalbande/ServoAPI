using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Servo_API.App_Start;
using Servo_API.Models;
using Servosms.Sysitem.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace Servo_API.Controllers
{
    public class EmployeeEntryController : ApiController
    {
        public SqlConnection SqlCon { get; private set; }

        InventoryClass obj = new InventoryClass();       
        DbOperations_LATEST.DBUtil dbobj = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);

        [HttpGet]
        [Route("api/EmployeeEntry/FetchCity")]
        public IHttpActionResult FetchCity()
        {
            SqlDataReader SqlDtr;
            string sql;
            List<string> DropCity = new List<string>();
            sql = "select distinct City from Beat_Master order by City asc";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                DropCity.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            if (DropCity.Count == 0 || DropCity == null)
            {
                return Content(HttpStatusCode.NotFound, "City Not Found");
            }
            return Ok(DropCity);
            
        }

        [HttpGet]
        [Route("api/EmployeeEntry/FetchState")]
        public IHttpActionResult FetchState()
        {
            SqlDataReader SqlDtr;
            string sql;
            List<string> DropState = new List<string>();
            sql = "select distinct state from Beat_Master order by state asc";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                DropState.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            if (DropState.Count == 0 || DropState == null)
            {
                return Content(HttpStatusCode.NotFound, "State Not Found");
            }
            return Ok(DropState);
        }

        [HttpGet]
        [Route("api/EmployeeEntry/FetchCountry")]
        public IHttpActionResult FetchCountry()
        {
            SqlDataReader SqlDtr;
            string sql;
            List<string> DropCountry = new List<string>();
            sql = "select distinct country from Beat_Master order by country asc";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                DropCountry.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            if (DropCountry.Count == 0 || DropCountry == null)
            {
                return Content(HttpStatusCode.NotFound, "Country Not Found");
            }
            return Ok(DropCountry);
        }

        [HttpGet]
        [Route("api/EmployeeEntry/FetchData")]
        public IHttpActionResult FetchData()
        {
            SqlDataReader SqlDtr;
            string sql,str="";            
            sql = "select city,state,country from beat_master";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                str = str + SqlDtr.GetValue(0).ToString() + ":";
                str = str + SqlDtr.GetValue(1).ToString() + ":";
                str = str + SqlDtr.GetValue(2).ToString() + "#";
            }              
            SqlDtr.Close();
            if (str == "" || str == null)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
            return Ok(str);
        }

        [HttpGet]
        [Route("api/EmployeeEntry/FetchVehicle")]
        public IHttpActionResult FetchVehicle()
        {
            SqlDataReader SqlDtr;
            string sql;
            List<string> Vehicle = new List<string>();
            sql = "Select vehicle_no from vehicleentry";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                Vehicle.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            if (Vehicle.Count == 0 || Vehicle == null)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
            return Ok(Vehicle);
        }
    }
}