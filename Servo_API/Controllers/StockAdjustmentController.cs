//using DBOperations;
using Servo_API.App_Start;
using Servo_API.Models;
//using Servosms.App_Data;
using Servosms.Sysitem.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Servo_API.Controllers
{
    public class StockAdjustmentController : ApiController
    {
        static string FromDate = "", ToDate = "";
        App_Start.DbOperations_LATEST.DBUtil dbobj = new App_Start.DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);

        [HttpGet]
        [Route("api/RouteMaster/GetNextRouteID")]
        public IHttpActionResult GetNextRouteID()
        {
            string Route_ID = string.Empty;
            try
            {

                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                string sql;

                #region Fetch the Next Route ID
                sql = "select Max(Route_ID)+1 from Route";
                SqlDtr = obj.GetRecordSet(sql);
                if (SqlDtr.Read())
                {
                    Route_ID = SqlDtr.GetValue(0).ToString();
                    if (Route_ID == "")
                    {
                        Route_ID = "1";
                    }
                }
                else
                    Route_ID = "1";
                SqlDtr.Close();
                #endregion
                return Ok(Route_ID);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to get Next Route ID");
            }
        }

        [HttpGet]
        [Route("api/RouteMaster/GetFillRouteNames")]
        public IHttpActionResult GetFillRouteNames()
        {
            List<string> dropRouteNames = new List<string>();
            try
            {
                SqlConnection con;
                SqlCommand cmdselect;
                SqlDataReader dtrdrive;
                con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                con.Open();
                cmdselect = new SqlCommand("Select Route_name  From Route", con);
                dtrdrive = cmdselect.ExecuteReader();

                dropRouteNames.Add("Select");
                while (dtrdrive.Read())
                {
                    dropRouteNames.Add(dtrdrive.GetString(0));
                }
                dtrdrive.Close();
                con.Close();

                if (dropRouteNames == null)
                    return Content(HttpStatusCode.NotFound, "Route Names data Not found");

                return Ok(dropRouteNames);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Route Names data Not found");
            }
        }

        [HttpGet]
        [Route("api/RouteMaster/GetRouteInfo")]
        public IHttpActionResult GetRouteInfo(string selectedRoute)
        {
            //string s = "";
            RouteMasterModel route = new RouteMasterModel();

            try
            {
                SqlConnection con;
                SqlCommand cmdselect;
                SqlDataReader dtrdrive;
                con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                con.Open();
                cmdselect = new SqlCommand("Select Route_name,Route_km  From Route where Route_name=@Route_name", con);
                cmdselect.Parameters.AddWithValue("@Route_name", selectedRoute.ToString().Trim());
                dtrdrive = cmdselect.ExecuteReader();
                while (dtrdrive.Read())
                {                    
                    route.Route_Name= dtrdrive.GetString(0);
                    route.Route_Km = dtrdrive.GetString(1);
                }
                dtrdrive.Close();
                con.Close();

                if (route == null)
                    return Content(HttpStatusCode.NotFound, "Route data Not found.");

                return Ok(route);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Route data Not found.");
            }
        }

        [HttpGet]
        [Route("api/RouteMaster/CheckIfRouteNameExists")]
        public IHttpActionResult CheckIfRouteNameExists(string routeName)
        {
            int iCount = 0;

            try
            {
                SqlConnection con2;
                SqlCommand cmdselect2;
                SqlDataReader dtredit2;
                con2 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                con2.Open();
                cmdselect2 = new SqlCommand("Select Count(Route_name) from Route where Route_name='" + routeName + "'", con2);
                dtredit2 = cmdselect2.ExecuteReader();
                if (dtredit2.Read())
                {
                    iCount = Convert.ToInt32(dtredit2.GetSqlValue(0).ToString());
                }
                dtredit2.Close();
                con2.Close();
                return Ok(iCount);

            }
            catch
            {
                return Content(HttpStatusCode.NotFound, "Could not delete Vehicle Entry Logbook");
            }
        }

        [HttpPost]
        [Route("api/RouteMaster/DeleteRoute")]
        public IHttpActionResult DeleteRoute(string route)
        {
            int count = 0;

            try
            {
                SqlConnection con10;
                SqlCommand cmdselect10;
                SqlDataReader dtredit10;
                string strdelete10;
                con10 = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["Servosms"]);
                con10.Open();

                strdelete10 = "Delete Route where Route_name =@Route_name";
                cmdselect10 = new SqlCommand(strdelete10, con10);
                cmdselect10.Parameters.AddWithValue("@Route_name", route);
                dtredit10 = cmdselect10.ExecuteReader();
                
                return Ok(count);
            }
            catch
            {
                return Content(HttpStatusCode.NotFound, "Could not delete Route");
            }
        }

        [HttpPost]
        [Route("api/RouteMaster/UpdateRoute")]
        public IHttpActionResult UpdateRoute(RouteMasterModel route)
        {

            try
            {
                SqlConnection con2;
                string strUpdate;
                SqlCommand cmdselect2;
                SqlDataReader dtredit2;

                con2 = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["Servosms"]);
                con2.Open();

                strUpdate = "Update Route set Route_name=@Route_name,Route_km=@Route_km where Route_name=@Route2";
                cmdselect2 = new SqlCommand(strUpdate, con2);
                if (route.Route_Name == "")
                    cmdselect2.Parameters.AddWithValue("@Route_name", "");
                else
                    cmdselect2.Parameters.AddWithValue("@Route_name", route.Route_Name.Trim());
                if (route.Route_Km == "")
                    cmdselect2.Parameters.AddWithValue("@Route_km", "");
                else
                    cmdselect2.Parameters.AddWithValue("@Route_km", route.Route_Km.Trim());
                if (route.Index_Route_Name == "0")
                    cmdselect2.Parameters.AddWithValue("@Route2", "");
                else
                    cmdselect2.Parameters.AddWithValue("@Route2", route.Selected_Route_Name.ToString());
                dtredit2 = cmdselect2.ExecuteReader();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Route could not update.");
            }
        }

        [HttpPost]
        [Route("api/RouteMaster/InsertRoute")]
        public IHttpActionResult InsertRoute(RouteMasterModel route)
        {

            try
            {
                SqlConnection con4;
                string strInsert4;
                SqlCommand cmdInsert4;
                con4 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                con4.Open();
                strInsert4 = "Insert Route(Route_name,Route_km)values (@Route_name,@Route_km)";
                cmdInsert4 = new SqlCommand(strInsert4, con4);
                cmdInsert4.Parameters.AddWithValue("@Route_name", route.Route_Name.Trim());
                cmdInsert4.Parameters.AddWithValue("@Route_km", route.Route_Km.Trim());
                cmdInsert4.ExecuteNonQuery();
                con4.Close();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Route could not update.");
            }
        }        
    }
 }
