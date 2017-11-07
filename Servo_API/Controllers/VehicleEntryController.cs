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
    public class VehicleEntryController : ApiController
    {        
        App_Start.DbOperations_LATEST.DBUtil dbobj = new App_Start.DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);

        [HttpGet]
        [Route("api/VehicleEntry/FillDropEngineOil")]
        public IHttpActionResult FillDropEngineOil()
        {
            List<string> dropEngineOil = new List<string>();
            try
            {                
                SqlDataReader SqlDtr = null;

                dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Engine Oil%'", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    dropEngineOil.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();

                if (dropEngineOil == null)
                    return Content(HttpStatusCode.NotFound, "Failed to get Engine Oil data.");
                return Ok(dropEngineOil);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to get Engine Oil data.");
            }
        }

        [HttpGet]
        [Route("api/VehicleEntry/FillDropbreak")]
        public IHttpActionResult FillDropbreak()
        {
            List<string> dropBreak = new List<string>();
            try
            {
                SqlDataReader SqlDtr = null;

                dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Brake Oil%'", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    dropBreak.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();

                if (dropBreak == null)
                    return Content(HttpStatusCode.NotFound, "Failed to get Brake Oil data.");
                return Ok(dropBreak);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to get Brake Oil data.");
            }
        }

        [HttpGet]
        [Route("api/VehicleEntry/FillDropGear")]
        public IHttpActionResult FillDropGear()
        {
            List<string> dropGear = new List<string>();
            try
            {
                SqlDataReader SqlDtr = null;

                dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Gear Oil%'", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    dropGear.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();

                if (dropGear == null)
                    return Content(HttpStatusCode.NotFound, "Failed to get Gear Oil data.");
                return Ok(dropGear);                
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to get Gear Oil data.");
            }
        }

        [HttpGet]
        [Route("api/VehicleEntry/FillDropCoolent")]
        public IHttpActionResult FillDropCoolent()
        {
            List<string> dropCoolent = new List<string>();
            try
            {
                SqlDataReader SqlDtr = null;

                dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Collent%'", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    dropCoolent.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();

                if (dropCoolent == null)
                    return Content(HttpStatusCode.NotFound, "Failed to get Gear Oil data.");
                return Ok(dropCoolent);                
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to get Gear Oil data.");
            }
        }

        [HttpGet]
        [Route("api/VehicleEntry/FillDropGrease")]
        public IHttpActionResult FillDropGrease()
        {
            List<string> dropGrease = new List<string>();
            try
            {
                SqlDataReader SqlDtr = null;

                dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Grease%'", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    dropGrease.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();

                if (dropGrease == null)
                    return Content(HttpStatusCode.NotFound, "Failed to get Grease data.");
                return Ok(dropGrease);               
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to get Grease data.");
            }
        }

        [HttpGet]
        [Route("api/VehicleEntry/FillDropTransmission")]
        public IHttpActionResult FillDropTransmission()
        {
            List<string> dropTransmission = new List<string>();
            try
            {
                SqlDataReader SqlDtr = null;

                dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Transmission Oil%'", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    dropTransmission.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();

                if (dropTransmission == null)
                    return Content(HttpStatusCode.NotFound, "Failed to get Transmisssion data.");
                return Ok(dropTransmission);                
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to get Transmisssion data.");
            }
        }

        [HttpGet]
        [Route("api/VehicleEntry/GetNextVehicledetailID")]
        public IHttpActionResult GetNextVehicledetailID()
        {
            string vehicleDetailID = string.Empty;
            try
            {

                #region Fetch Next Vehicle ID                
                SqlDataReader SqlDtr = null;
                dbobj.SelectQuery("Select max(vehicledetail_id) from vehicleentry", ref SqlDtr);
                if (SqlDtr.Read())
                {
                    string str = SqlDtr.GetValue(0).ToString();
                    if (!str.Trim().Equals(""))
                    {
                        int id = System.Convert.ToInt32(str.Trim());
                        id = id + 1;
                        str = id.ToString();
                    }
                    else
                    {
                        str = "1001";
                    }
                    vehicleDetailID = str;

                }
                else
                {
                    vehicleDetailID = "1001";
                }
                #endregion

                if (vehicleDetailID == null)
                    return Content(HttpStatusCode.NotFound, "Failed to get Next Vehicle entry ID.");
                return Ok(vehicleDetailID);                
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to get Next Vehicle entry ID.");
            }

        }

        [HttpPost]
        [Route("api/VehicleEntry/UpdateVehicleEntry")]
        public IHttpActionResult UpdateVehicleEntry(VehicleEntryModel vehEntryModel)
        {           
            SqlConnection con;
            string strInsert;
            SqlCommand cmdInsert;
            DateTime dt1 = DateTime.Now;
            con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            try
            {

                con.Open();
                strInsert = "update Vehicleentry set Vehicle_Type = @Vehicle_Type,Vehicle_no = @Vehicle_no,vehicle_name = @vehicle_name,RTO_Reg_Val_yrs = @RTO_Reg_Val_yrs,Model_name = @Model_name,RTO_Reg_No = @RTO_Reg_No,Vehicle_Man_Date =@Vehicle_Man_Date,Insurance_No = @Insurance_No,Meter_Reading=@Meter_Reading,Insurance_validity = @Insurance_validity,Vehicle_Route = @Vehicle_Route,Insurance_Comp_Name = @Insurance_Comp_Name,Fuel_Used=@Fuel_Used,Fuel_Used_Qty = @Fuel_Used_Qty,start_Fuel_Qty = @start_Fuel_Qty,"
                    + "Engine_Oil = @Engine_Oil,Engine_Oil_Qty =@Engine_Oil_Qty,Engine_Oil_Dt = @Engine_Oil_Dt,Engine_Oil_km = @Engine_Oil_km,Gear_Oil = @Gear_Oil,Gear_Oil_Qty = @Gear_Oil_Qty,Gear_Oil_Dt = @Gear_Oil_Dt,Gear_Oil_Km = @Gear_Oil_Km,Brake_oil = @Brake_Oil,Brake_Oil_Qty = @Brake_Oil_Qty,BRake_Oil_dt = @Brake_Oil_Dt,Brake_OIl_Km = @Brake_Oil_Km,Coolent = @Coolent, Coolent_Qty = @Coolent_Qty, Coolent_Dt =@Coolent_Dt, Coolent_km = @Coolent_Km,Grease = @Grease,Grease_Qty =@Grease_Qty,Grease_Dt = @Grease_Dt,Grease_Km = @Grease_Km,"
                    + "Trans_Oil = @Trans_Oil, Trans_Oil_Qty = @Trans_Oil_Qty, Trans_Oil_Dt = @Trans_Oil_Dt, Trans_Oil_Km = @Trans_Oil_km,Vehicle_Avg = @Vehicle_Avg where Vehicledetail_id = " + vehEntryModel.Vehicle_ID.Trim();

                cmdInsert = new SqlCommand(strInsert, con);
                
                cmdInsert.Parameters.AddWithValue("@Vehicle_Type", vehEntryModel.VehicleType2);
                cmdInsert.Parameters.AddWithValue("@Vehicle_no", vehEntryModel.Vehicleno);
                cmdInsert.Parameters.AddWithValue("@Vehicle_name", vehEntryModel.Vehiclenm);
                cmdInsert.Parameters.AddWithValue("@RTO_Reg_Val_yrs", vehEntryModel.RTO_Reg_Val_yrs);

                cmdInsert.Parameters.AddWithValue("@Model_name", vehEntryModel.Model_name);
                cmdInsert.Parameters.AddWithValue("@RTO_Reg_No", vehEntryModel.RTO_Reg_No);
                cmdInsert.Parameters.AddWithValue("@Vehicle_Man_Date", vehEntryModel.Vehicle_Man_Date);

                cmdInsert.Parameters.AddWithValue("@Insurance_No", vehEntryModel.Insurance_No);
                cmdInsert.Parameters.AddWithValue("@Meter_Reading", vehEntryModel.Meter_Reading);
                cmdInsert.Parameters.AddWithValue("@Insurance_validity", vehEntryModel.Insurance_validity.Trim());

                SqlDataReader SqlDtr = null;
                string route_id = "";
                //Fetch the route id for selected route from route table.
                dbobj.SelectQuery("Select route_id from Route where route_name='" + vehEntryModel.RouteName.Trim() + "'", ref SqlDtr);
                if (SqlDtr.Read())
                {
                    route_id = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();
                cmdInsert.Parameters.AddWithValue("@Vehicle_Route", route_id);
                cmdInsert.Parameters.AddWithValue("@Insurance_Comp_name", vehEntryModel.Insurance_Comp_name.Trim());
                string prod_id = "";
                
                cmdInsert.Parameters.AddWithValue("@Fuel_Used", vehEntryModel.Fuel_Used.Trim());
                cmdInsert.Parameters.AddWithValue("@Fuel_Used_Qty", vehEntryModel.Fuel_Used_Qty.Trim());
                cmdInsert.Parameters.AddWithValue("@Start_Fuel_Qty", vehEntryModel.Start_Fuel_Qty.Trim());
                //Fetch the product id for selected product of type Engine Oil from table products.

                if (vehEntryModel.EngineOil.ToString() != "")
                {
                    string[] strArr = vehEntryModel.EngineOil.Split(new char[] { ':' }, vehEntryModel.EngineOil.Length);
                    prod_id = "";
                    dbobj.SelectQuery("Select prod_id from products where prod_name='" + strArr[0].Trim() + "' and pack_type ='" + strArr[1].Trim() + "' and Category like 'Engine Oil%'", ref SqlDtr);
                    if (SqlDtr.Read())
                    {
                        prod_id = SqlDtr.GetValue(0).ToString();
                    }
                    SqlDtr.Close();
                }
                else
                {
                    prod_id = "0";
                }
                cmdInsert.Parameters.AddWithValue("@Engine_Oil", prod_id);
                cmdInsert.Parameters.AddWithValue("@Engine_Oil_Qty", vehEntryModel.Engine_Oil_Qty.Trim());
                cmdInsert.Parameters.AddWithValue("@Engine_Oil_Dt", vehEntryModel.Engine_Oil_Dt.Trim());

                cmdInsert.Parameters.AddWithValue("@Engine_Oil_km", vehEntryModel.Engine_Oil_km);
                //Fetch the product id for selected product of type Gear Oil from table products.
                if (vehEntryModel.Gear_Oil.ToString() != "")
                {
                    string[] strArr = vehEntryModel.Gear_Oil.Split(new char[] { ':' }, vehEntryModel.Gear_Oil.Length);
                    prod_id = "";
                    dbobj.SelectQuery("Select prod_id from products where prod_name='" + strArr[0].Trim() + "' and pack_type ='" + strArr[1].Trim() + "' and Category like 'Gear Oil%'", ref SqlDtr);
                    if (SqlDtr.Read())
                    {
                        prod_id = SqlDtr.GetValue(0).ToString();
                    }
                    SqlDtr.Close();
                }
                else
                {
                    prod_id = "0";
                }
                cmdInsert.Parameters.AddWithValue("@Gear_Oil", prod_id);
                cmdInsert.Parameters.AddWithValue("@Gear_Oil_Qty", vehEntryModel.Gear_Oil_Qty.Trim());
                cmdInsert.Parameters.AddWithValue("@Gear_Oil_Dt", vehEntryModel.Gear_Oil_Dt.Trim());

                cmdInsert.Parameters.AddWithValue("@Gear_Oil_km", vehEntryModel.Gear_Oil_km.Trim());
                //Fetch the product id for selected product of type Brake Oil from table products. 
                if (vehEntryModel.Brake_Oil.ToString() != "")
                {
                    string[] strArr = vehEntryModel.Brake_Oil.Split(new char[] { ':' }, vehEntryModel.Brake_Oil.Length);
                    prod_id = "";
                    dbobj.SelectQuery("Select prod_id from products where prod_name='" + strArr[0].Trim() + "' and pack_type ='" + strArr[1].Trim() + "' and Category like 'Brake Oil%'", ref SqlDtr);
                    if (SqlDtr.Read())
                    {
                        prod_id = SqlDtr.GetValue(0).ToString();
                    }
                    SqlDtr.Close();
                }
                else
                {
                    prod_id = "0";
                }
                cmdInsert.Parameters.AddWithValue("@Brake_Oil", prod_id);
                cmdInsert.Parameters.AddWithValue("@Brake_Oil_Qty", vehEntryModel.Brake_Oil_Qty.Trim());
                cmdInsert.Parameters.AddWithValue("@Brake_Oil_Dt", vehEntryModel.Brake_Oil_Dt.Trim());

                cmdInsert.Parameters.AddWithValue("@Brake_Oil_km", vehEntryModel.Brake_Oil_km.Trim());
                //Fetch the product id for selected product of type Coolent from table products. 
                if (vehEntryModel.Coolent.ToString() != "")
                {
                    string[] strArr = vehEntryModel.Coolent.Split(new char[] { ':' }, vehEntryModel.Coolent.Length);
                    prod_id = "";
                    dbobj.SelectQuery("Select prod_id from products where prod_name='" + strArr[0].Trim() + "' and pack_type ='" + strArr[1].Trim() + "' and Category like 'Collent%'", ref SqlDtr);
                    if (SqlDtr.Read())
                    {
                        prod_id = SqlDtr.GetValue(0).ToString();
                    }
                    SqlDtr.Close();
                }
                else
                {
                    prod_id = "0";
                }
                cmdInsert.Parameters.AddWithValue("@Coolent", prod_id);
                cmdInsert.Parameters.AddWithValue("@Coolent_Qty", vehEntryModel.Coolent_Oil_Qty.Trim());
                cmdInsert.Parameters.AddWithValue("@Coolent_Dt", vehEntryModel.Coolent_Oil_Dt.Trim());

                cmdInsert.Parameters.AddWithValue("@Coolent_km", vehEntryModel.Coolent_km.Trim());

                //Fetch the product id for selected product of type Grease from table products.
                if (vehEntryModel.Grease.ToString() != "")
                {
                    string[] strArr = vehEntryModel.Grease.Split(new char[] { ':' }, vehEntryModel.Grease.Length);
                    prod_id = "";
                    dbobj.SelectQuery("Select prod_id from products where prod_name='" + strArr[0].Trim() + "' and pack_type ='" + strArr[1].Trim() + "' and Category like 'Grease%'", ref SqlDtr);
                    if (SqlDtr.Read())
                    {
                        prod_id = SqlDtr.GetValue(0).ToString();
                    }
                    SqlDtr.Close();
                }
                else
                {
                    prod_id = "0";
                }
                cmdInsert.Parameters.AddWithValue("@Grease", prod_id);
                cmdInsert.Parameters.AddWithValue("@Grease_Qty", vehEntryModel.Grease_Qty.Trim());
                cmdInsert.Parameters.AddWithValue("@Grease_Dt", vehEntryModel.Grease_Dt.Trim());

                cmdInsert.Parameters.AddWithValue("@Grease_km", vehEntryModel.Grease_km.Trim());

                //Fetch the product id for selected product of type Transmission Oil from table products.
                if (vehEntryModel.Trans_Oil.ToString() != "")
                {
                    string[] strArr = vehEntryModel.Trans_Oil.Split(new char[] { ':' }, vehEntryModel.Trans_Oil.Length);
                    prod_id = "";
                    dbobj.SelectQuery("Select prod_id from products where prod_name='" + strArr[0].Trim() + "' and pack_type ='" + strArr[1].Trim() + "' and Category like 'Transmission Oil%'", ref SqlDtr);
                    if (SqlDtr.Read())
                    {
                        prod_id = SqlDtr.GetValue(0).ToString();
                    }
                    SqlDtr.Close();
                }
                else
                {
                    prod_id = "0";
                }
                cmdInsert.Parameters.AddWithValue("@Trans_Oil", prod_id);
                cmdInsert.Parameters.AddWithValue("@Trans_Oil_Qty", vehEntryModel.Trans_Oil_Qty.Trim());
                cmdInsert.Parameters.AddWithValue("@Trans_Oil_Dt", vehEntryModel.Trans_Oil_Dt.Trim());

                cmdInsert.Parameters.AddWithValue("@Trans_Oil_km", vehEntryModel.Trans_Oil_km.Trim());
                cmdInsert.Parameters.AddWithValue("@Vehicle_Avg", vehEntryModel.Vehicle_Avg.Trim());
                cmdInsert.ExecuteNonQuery();
                con.Close();

                return Ok(true);                
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to update Vehicle entry ID.");
            }
        }

        [HttpGet]
        [Route("api/VehicleEntry/GetDropVehicleID_SelectedData")]
        public IHttpActionResult GetDropVehicleID_SelectedData(string VehicleID)
        {

            VehicleEntryModel vehEntryModel = null;
            
            try
            {
                
                SqlDataReader SqlDtr = null;
                SqlDataReader SqlDtr1 = null;
                dbobj.SelectQuery("Select * from vehicleentry where vehicledetail_id = " + VehicleID.Trim(), ref SqlDtr);
                if (SqlDtr.Read())
                {
                    vehEntryModel = new VehicleEntryModel();
                    vehEntryModel.VehicleType2 = SqlDtr["Vehicle_Type"].ToString().Trim();
                    vehEntryModel.Vehicleno = SqlDtr["Vehicle_No"].ToString().Trim();
                    vehEntryModel.Vehiclenm = SqlDtr["Vehicle_Name"].ToString().Trim();
                    vehEntryModel.RTO_Reg_Val_yrs = SqlDtr["RTO_Reg_Val_Yrs"].ToString().Trim();
                    vehEntryModel.Model_name = SqlDtr["Model_Name"].ToString().Trim();
                    vehEntryModel.RTO_Reg_No = SqlDtr["RTO_Reg_No"].ToString().Trim();
                    vehEntryModel.Vehicle_Man_Date = SqlDtr["Vehicle_man_date"].ToString().Trim();
                    vehEntryModel.Insurance_No = SqlDtr["Insurance_No"].ToString().Trim();
                    vehEntryModel.Meter_Reading = SqlDtr["Meter_Reading"].ToString().Trim();
                    vehEntryModel.Insurance_validity = SqlDtr["Insurance_Validity"].ToString().Trim();

                    string route_name = "";
                    dbobj.SelectQuery("Select route_name from route where route_id=" + SqlDtr["Vehicle_Route"].ToString().Trim(), ref SqlDtr1);
                    if (SqlDtr1.Read())
                    {
                        route_name = SqlDtr1.GetValue(0).ToString();
                    }
                    SqlDtr1.Close();

                    vehEntryModel.RouteName = route_name;

                    vehEntryModel.Insurance_Comp_name = SqlDtr["Insurance_Comp_Name"].ToString().Trim();
                    
                    vehEntryModel.Fuel_Used = SqlDtr["Fuel_Used"].ToString().Trim();
                    vehEntryModel.Fuel_Used_Qty = SqlDtr["Fuel_Used_Qty"].ToString().Trim();
                    vehEntryModel.Start_Fuel_Qty = SqlDtr["Start_Fuel_Qty"].ToString().Trim();

                    string engine_oil = "";
                    dbobj.SelectQuery("Select prod_name+':'+pack_type from products where Category like 'Engine Oil%' and  prod_id=" + SqlDtr["Engine_Oil"].ToString().Trim(), ref SqlDtr1);
                    if (SqlDtr1.Read())
                    {
                        engine_oil = SqlDtr1.GetValue(0).ToString();

                    }
                    SqlDtr1.Close();

                    vehEntryModel.EngineOil = engine_oil;
                    vehEntryModel.Engine_Oil_Qty = SqlDtr["Engine_Oil_Qty"].ToString().Trim();
                    vehEntryModel.Engine_Oil_Dt = SqlDtr["Engine_Oil_Dt"].ToString().Trim();
                    vehEntryModel.Engine_Oil_km = SqlDtr["Engine_OIl_Km"].ToString().Trim();

                    string gear_oil = "";
                    dbobj.SelectQuery("Select prod_name+':'+pack_type from products where Category like 'Gear Oil%' and  prod_id=" + SqlDtr["Gear_Oil"].ToString().Trim(), ref SqlDtr1);
                    if (SqlDtr1.Read())
                    {
                        gear_oil = SqlDtr1.GetValue(0).ToString();

                    }
                    SqlDtr1.Close();

                    vehEntryModel.Gear_Oil = gear_oil;
                    vehEntryModel.Gear_Oil_Qty = SqlDtr["Gear_Oil_Qty"].ToString().Trim();
                    vehEntryModel.Gear_Oil_Dt = SqlDtr["Gear_Oil_Dt"].ToString().Trim();
                    vehEntryModel.Gear_Oil_km = SqlDtr["Gear_OIl_Km"].ToString().Trim();

                    string brake_oil = "";
                    dbobj.SelectQuery("Select prod_name+':'+pack_type from products where Category like 'Brake Oil%' and  prod_id=" + SqlDtr["Brake_Oil"].ToString().Trim(), ref SqlDtr1);
                    if (SqlDtr1.Read())
                    {
                        brake_oil = SqlDtr1.GetValue(0).ToString();

                    }
                    SqlDtr1.Close();

                    vehEntryModel.Brake_Oil = brake_oil;
                    vehEntryModel.Brake_Oil_Qty = SqlDtr["Brake_Oil_Qty"].ToString().Trim();
                    vehEntryModel.Brake_Oil_Dt = SqlDtr["Brake_Oil_Dt"].ToString().Trim();
                    vehEntryModel.Brake_Oil_km = SqlDtr["Brake_OIl_Km"].ToString().Trim();

                    string coolent = "";
                    dbobj.SelectQuery("Select prod_name+':'+pack_type from products where Category like 'Collent%' and  prod_id=" + SqlDtr["Coolent"].ToString().Trim(), ref SqlDtr1);
                    if (SqlDtr1.Read())
                    {
                        coolent = SqlDtr1.GetValue(0).ToString();

                    }
                    SqlDtr1.Close();

                    vehEntryModel.Coolent = coolent;
                    vehEntryModel.Coolent_Oil_Qty = SqlDtr["Coolent_Qty"].ToString().Trim();
                    vehEntryModel.Coolent_Oil_Dt = SqlDtr["Coolent_Dt"].ToString().Trim();
                    vehEntryModel.Coolent_km = SqlDtr["Coolent_Km"].ToString().Trim();

                    string grease = "";
                    dbobj.SelectQuery("Select prod_name+':'+pack_type from products where Category like 'Grease%' and  prod_id=" + SqlDtr["Grease"].ToString().Trim(), ref SqlDtr1);
                    if (SqlDtr1.Read())
                    {
                        grease = SqlDtr1.GetValue(0).ToString();

                    }
                    SqlDtr1.Close();
                    vehEntryModel.Grease = grease;
                    vehEntryModel.Grease_Qty = SqlDtr["Grease_Qty"].ToString().Trim();
                    vehEntryModel.Grease_Dt = SqlDtr["Grease_Dt"].ToString().Trim();
                    vehEntryModel.Grease_km = SqlDtr["grease_Km"].ToString().Trim();

                    string trans_oil = "";
                    dbobj.SelectQuery("Select prod_name+':'+pack_type from products where Category like 'Transmission Oil%' and  prod_id=" + SqlDtr["Trans_OIl"].ToString().Trim(), ref SqlDtr1);
                    if (SqlDtr1.Read())
                    {
                        trans_oil = SqlDtr1.GetValue(0).ToString();

                    }
                    SqlDtr1.Close();

                    vehEntryModel.Trans_Oil= trans_oil;
                    vehEntryModel.Trans_Oil_Qty = SqlDtr["Trans_OIl_Qty"].ToString().Trim();
                    vehEntryModel.Trans_Oil_Dt = SqlDtr["Trans_OIl_Dt"].ToString().Trim();
                    vehEntryModel.Trans_Oil_km = SqlDtr["Trans_Oil_Km"].ToString().Trim();

                    vehEntryModel.Vehicle_Avg = SqlDtr["Vehicle_Avg"].ToString();
                    //checkPrevileges();

                }
                SqlDtr.Close();

                if (vehEntryModel == null)
                    return Content(HttpStatusCode.NotFound, "Failed to get data Vehicle entry ID related data.");
                return Ok(vehEntryModel);                
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to get data Vehicle entry ID related data.");
            }
        }

        [HttpPost]
        [Route("api/VehicleEntry/InsertVehicleEntry")]
        public IHttpActionResult InsertVehicleEntry(VehicleEntryModel vehEntryModel)
        {           
            SqlConnection con;
            string strInsert;
            SqlCommand cmdInsert;
            DateTime dt1 = DateTime.Now;
            con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            try
            {
                
                con.Open();
                strInsert = "Insert Vehicleentry(vehicledetail_id,Vehicle_Type,Vehicle_no,vehicle_name,RTO_Reg_Val_yrs,Model_name,RTO_Reg_No,Vehicle_Man_Date,Insurance_No,Meter_Reading,Insurance_validity,Vehicle_Route,Insurance_Comp_Name,Fuel_Used,Fuel_Used_Qty,start_Fuel_Qty,Engine_Oil,Engine_Oil_Qty,Engine_Oil_Dt,Engine_Oil_km,Gear_Oil,Gear_Oil_Qty,Gear_Oil_Dt,Gear_Oil_Km,Brake_Oil,Brake_Oil_Qty,Brake_Oil_Dt,Brake_Oil_km,Coolent,Coolent_Qty,Coolent_Dt,Coolent_Km,Grease,Grease_Qty,Grease_Dt,Grease_Km,Trans_Oil,Trans_Oil_Qty,Trans_Oil_Dt,Trans_Oil_Km,Vehicle_Avg)"
                    + "values(@vehicledetail_id,@Vehicle_Type,@Vehicle_no,@vehicle_name,@RTO_Reg_Val_yrs,@Model_name,@RTO_Reg_No,@Vehicle_Man_Date,@Insurance_No,@Meter_Reading,@Insurance_validity,@Vehicle_Route,@Insurance_Comp_Name,@Fuel_Used,@Fuel_Used_Qty,@start_Fuel_Qty,@Engine_Oil,@Engine_Oil_Qty,@Engine_Oil_Dt,@Engine_Oil_km,@Gear_Oil,@Gear_Oil_Qty,@Gear_Oil_Dt,@Gear_Oil_Km,@Brake_Oil,@Brake_Oil_Qty,@Brake_Oil_Dt,@Brake_Oil_km,@Coolent,@Coolent_Qty,@Coolent_Dt,@Coolent_Km,@Grease,@Grease_Qty,@Grease_Dt,@Grease_Km,@Trans_Oil,@Trans_Oil_Qty,@Trans_Oil_Dt,@Trans_Oil_Km,@Vehicle_Avg)";

                cmdInsert = new SqlCommand(strInsert, con);
                cmdInsert.Parameters.AddWithValue("@vehicledetail_id", vehEntryModel.Vehicle_ID);
                cmdInsert.Parameters.AddWithValue("@Vehicle_Type", vehEntryModel.VehicleType2);
                cmdInsert.Parameters.AddWithValue("@Vehicle_no", vehEntryModel.Vehicleno);
                cmdInsert.Parameters.AddWithValue("@Vehicle_name", vehEntryModel.Vehiclenm);
                cmdInsert.Parameters.AddWithValue("@RTO_Reg_Val_yrs", vehEntryModel.RTO_Reg_Val_yrs);

                cmdInsert.Parameters.AddWithValue("@Model_name", vehEntryModel.Model_name);
                cmdInsert.Parameters.AddWithValue("@RTO_Reg_No", vehEntryModel.RTO_Reg_No);
                cmdInsert.Parameters.AddWithValue("@Vehicle_Man_Date", vehEntryModel.Vehicle_Man_Date);

                cmdInsert.Parameters.AddWithValue("@Insurance_No", vehEntryModel.Insurance_No);
                cmdInsert.Parameters.AddWithValue("@Meter_Reading", vehEntryModel.Meter_Reading);
                cmdInsert.Parameters.AddWithValue("@Insurance_validity", vehEntryModel.Insurance_validity.Trim());

                SqlDataReader SqlDtr = null;
                string route_id = "";
                //Fetch the route id for selected route from route table.
                dbobj.SelectQuery("Select route_id from Route where route_name='" + vehEntryModel.RouteName.Trim() + "'", ref SqlDtr);
                if (SqlDtr.Read())
                {
                    route_id = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();
                cmdInsert.Parameters.AddWithValue("@Vehicle_Route", route_id);
                cmdInsert.Parameters.AddWithValue("@Insurance_Comp_name", vehEntryModel.Insurance_Comp_name.Trim());
                string prod_id = "";
                
                cmdInsert.Parameters.AddWithValue("@Fuel_Used", vehEntryModel.Fuel_Used.Trim());
                cmdInsert.Parameters.AddWithValue("@Fuel_Used_Qty", vehEntryModel.Fuel_Used_Qty.Trim());
                cmdInsert.Parameters.AddWithValue("@Start_Fuel_Qty", vehEntryModel.Start_Fuel_Qty.Trim());

                //Fetch the product id for selected product of type Engine Oil from table products.
                
                if (vehEntryModel.EngineOil.ToString() != "")
                {
                    string[] strArr = vehEntryModel.EngineOil.Split(new char[] { ':' }, vehEntryModel.EngineOil.Length);
                    prod_id = "";
                    dbobj.SelectQuery("Select prod_id from products where prod_name='" + strArr[0].Trim() + "' and pack_type ='" + strArr[1].Trim() + "' and Category like 'Engine Oil%'", ref SqlDtr);
                    if (SqlDtr.Read())
                    {
                        prod_id = SqlDtr.GetValue(0).ToString();
                    }
                    SqlDtr.Close();
                }
                else
                {
                    prod_id = "0";
                }
                cmdInsert.Parameters.AddWithValue("@Engine_Oil", prod_id);
                cmdInsert.Parameters.AddWithValue("@Engine_Oil_Qty", vehEntryModel.Engine_Oil_Qty.Trim());
                cmdInsert.Parameters.AddWithValue("@Engine_Oil_Dt", vehEntryModel.Engine_Oil_Dt.Trim());

                cmdInsert.Parameters.AddWithValue("@Engine_Oil_km", vehEntryModel.Engine_Oil_km);
                //Fetch the product id for selected product of type Gear Oil from table products.
                if (vehEntryModel.Gear_Oil.ToString() != "")
                {
                    string[] strArr = vehEntryModel.Gear_Oil.Split(new char[] { ':' }, vehEntryModel.Gear_Oil.Length);
                    prod_id = "";
                    dbobj.SelectQuery("Select prod_id from products where prod_name='" + strArr[0].Trim() + "' and pack_type ='" + strArr[1].Trim() + "' and Category like 'Gear Oil%'", ref SqlDtr);
                    if (SqlDtr.Read())
                    {
                        prod_id = SqlDtr.GetValue(0).ToString();
                    }
                    SqlDtr.Close();
                }
                else
                {
                    prod_id = "0";
                }
                cmdInsert.Parameters.AddWithValue("@Gear_Oil", prod_id);
                cmdInsert.Parameters.AddWithValue("@Gear_Oil_Qty", vehEntryModel.Gear_Oil_Qty.Trim());
                cmdInsert.Parameters.AddWithValue("@Gear_Oil_Dt", vehEntryModel.Gear_Oil_Dt.Trim());

                cmdInsert.Parameters.AddWithValue("@Gear_Oil_km", vehEntryModel.Gear_Oil_km.Trim());
                //Fetch the product id for selected product of type Brake Oil from table products. 
                if (vehEntryModel.Brake_Oil.ToString() != "")
                {
                    string[] strArr = vehEntryModel.Brake_Oil.Split(new char[] { ':' }, vehEntryModel.Brake_Oil.Length);
                    prod_id = "";
                    dbobj.SelectQuery("Select prod_id from products where prod_name='" + strArr[0].Trim() + "' and pack_type ='" + strArr[1].Trim() + "' and Category like 'Brake Oil%'", ref SqlDtr);
                    if (SqlDtr.Read())
                    {
                        prod_id = SqlDtr.GetValue(0).ToString();
                    }
                    SqlDtr.Close();
                }
                else
                {
                    prod_id = "0";
                }
                cmdInsert.Parameters.AddWithValue("@Brake_Oil", prod_id);
                cmdInsert.Parameters.AddWithValue("@Brake_Oil_Qty", vehEntryModel.Brake_Oil_Qty.Trim());
                cmdInsert.Parameters.AddWithValue("@Brake_Oil_Dt", vehEntryModel.Brake_Oil_Dt.Trim());

                cmdInsert.Parameters.AddWithValue("@Brake_Oil_km", vehEntryModel.Brake_Oil_km.Trim());
                //Fetch the product id for selected product of type Coolent from table products. 
                if (vehEntryModel.Coolent.ToString() != "")
                {
                    string[] strArr = vehEntryModel.Coolent.Split(new char[] { ':' }, vehEntryModel.Coolent.Length);
                    prod_id = "";
                    dbobj.SelectQuery("Select prod_id from products where prod_name='" + strArr[0].Trim() + "' and pack_type ='" + strArr[1].Trim() + "' and Category like 'Collent%'", ref SqlDtr);
                    if (SqlDtr.Read())
                    {
                        prod_id = SqlDtr.GetValue(0).ToString();
                    }
                    SqlDtr.Close();
                }
                else
                {
                    prod_id = "0";
                }
                cmdInsert.Parameters.AddWithValue("@Coolent", prod_id);
                cmdInsert.Parameters.AddWithValue("@Coolent_Qty", vehEntryModel.Coolent_Oil_Qty.Trim());
                cmdInsert.Parameters.AddWithValue("@Coolent_Dt", vehEntryModel.Coolent_Oil_Dt.Trim());

                cmdInsert.Parameters.AddWithValue("@Coolent_km", vehEntryModel.Coolent_km.Trim());

                //Fetch the product id for selected product of type Grease from table products.
                if (vehEntryModel.Grease.ToString() != "")
                {
                    string[] strArr = vehEntryModel.Grease.Split(new char[] { ':' }, vehEntryModel.Grease.Length);
                    prod_id = "";
                    dbobj.SelectQuery("Select prod_id from products where prod_name='" + strArr[0].Trim() + "' and pack_type ='" + strArr[1].Trim() + "' and Category like 'Grease%'", ref SqlDtr);
                    if (SqlDtr.Read())
                    {
                        prod_id = SqlDtr.GetValue(0).ToString();
                    }
                    SqlDtr.Close();
                }
                else
                {
                    prod_id = "0";
                }
                cmdInsert.Parameters.AddWithValue("@Grease", prod_id);
                cmdInsert.Parameters.AddWithValue("@Grease_Qty", vehEntryModel.Grease_Qty.Trim());
                cmdInsert.Parameters.AddWithValue("@Grease_Dt", vehEntryModel.Grease_Dt.Trim());

                cmdInsert.Parameters.AddWithValue("@Grease_km", vehEntryModel.Grease_km.Trim());

                //Fetch the product id for selected product of type Transmission Oil from table products.
                if (vehEntryModel.Trans_Oil.ToString() != "")
                {
                    string[] strArr = vehEntryModel.Trans_Oil.Split(new char[] { ':' }, vehEntryModel.Trans_Oil.Length);
                    prod_id = "";
                    dbobj.SelectQuery("Select prod_id from products where prod_name='" + strArr[0].Trim() + "' and pack_type ='" + strArr[1].Trim() + "' and Category like 'Transmission Oil%'", ref SqlDtr);
                    if (SqlDtr.Read())
                    {
                        prod_id = SqlDtr.GetValue(0).ToString();
                    }
                    SqlDtr.Close();
                }
                else
                {
                    prod_id = "0";
                }
                cmdInsert.Parameters.AddWithValue("@Trans_Oil", prod_id);
                cmdInsert.Parameters.AddWithValue("@Trans_Oil_Qty", vehEntryModel.Trans_Oil_Qty.Trim());
                cmdInsert.Parameters.AddWithValue("@Trans_Oil_Dt", vehEntryModel.Trans_Oil_Dt.Trim());

                cmdInsert.Parameters.AddWithValue("@Trans_Oil_km", vehEntryModel.Trans_Oil_km.Trim());
                cmdInsert.Parameters.AddWithValue("@Vehicle_Avg", vehEntryModel.Vehicle_Avg.Trim());
                cmdInsert.ExecuteNonQuery();
                con.Close();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to insert Vehicle entry ID.");
            }
        }

        [HttpPost]
        [Route("api/VehicleEntry/DeleteVehicleEntry")]
        public IHttpActionResult DeleteVehicleEntry(string vehicleID)
        {
            int count = 0;
            
            try
            {                                
                dbobj.Insert_or_Update("Delete from vehicleentry where vehicledetail_id = " + vehicleID.Trim(), ref count);

                return Ok(count);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to delete Vehicle entry.");
            }
        }

        [HttpGet]
        [Route("api/VehicleEntry/FillDropVehicleID")]
        public IHttpActionResult FillDropVehicleID()
        {
            List<string> dropVehicleID = new List<string>();
            try
            {
                //DBOperations.DBUtil obj = new DBOperations.DBUtil();
                SqlDataReader SqlDtr = null;

                dbobj.SelectQuery("Select vehicledetail_id from vehicleentry ", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    dropVehicleID.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();
                return Ok(dropVehicleID);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to get Vehicle entry IDs.");
            }
        }
    }
}
