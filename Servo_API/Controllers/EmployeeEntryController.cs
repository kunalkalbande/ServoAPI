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



namespace Servo_API.Controllers
{
    public class EmployeeEntryController : ApiController
    {
        public SqlConnection SqlCon { get; private set; }

        InventoryClass obj = new InventoryClass();       
        DbOperations_LATEST.DBUtil dbobj = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);

        [HttpGet]
        [Route("api/payment/FetchCity")]
        public List<string> FetchCity()
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
            return DropCity;
        }

        [HttpGet]
        [Route("api/payment/FetchState")]
        public List<string> FetchState()
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
            return DropState;
        }

        [HttpGet]
        [Route("api/payment/FetchCountry")]
        public List<string> FetchCountry()
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
            return DropCountry;
        }

        [HttpGet]
        [Route("api/payment/FetchData")]
        public string FetchData()
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
            return str;
        }

        [HttpGet]
        [Route("api/payment/FetchVehicle")]
        public List<string> FetchVehicle()
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
            return Vehicle;
        }
    }
}