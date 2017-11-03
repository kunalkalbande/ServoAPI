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
    public class VehicleDailyLogbookController : ApiController
    {
        static string FromDate = "", ToDate = "";
        App_Start.DbOperations_LATEST.DBUtil dbobj = new App_Start.DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);

        [HttpGet]
        [Route("api/VehicleDailyLogbook/GetNextVehicleLogbookID")]
        public IHttpActionResult GetNextVehicleLogbookID()
        {
            string vehicleDetailID = string.Empty;
            try
            {

                #region Fetch Next Vehicle ID                
                SqlDataReader SqlDtr = null;
                dbobj.SelectQuery("Select max(VDLB_id) from VDLB", ref SqlDtr);
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
                return Ok(vehicleDetailID);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Next Vehicle Logbook ID Not found");
            }
        }

        [HttpGet]
        [Route("api/VehicleDailyLogbook/GetFillVehicleNo")]
        public IHttpActionResult GetFillVehicleNo()
        {
            List<string> dropVehicleNo = new List<string>();
            try
            {
                SqlDataReader SqlDtr = null;

                dbobj.SelectQuery("Select vehicle_no+' VID '+cast(vehicledetail_id as varchar) from vehicleentry", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    dropVehicleNo.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();
                if (dropVehicleNo == null || dropVehicleNo.Count == 0)
                    return Content(HttpStatusCode.NotFound, "Vehicle No data Not found");

                return Ok(dropVehicleNo);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Vehicle No data Not found");
            }
        }

        [HttpGet]
        [Route("api/VehicleDailyLogbook/GetVehicleInfo")]
        public IHttpActionResult GetVehicleInfo()
        {
            string s = "";
            try
            {
                SqlDataReader SqlDtr = null;
                SqlDataReader SqlDtr1 = null;
                string meter_reading = "";
                dbobj.SelectQuery("select ve.vehicle_no+' VID '+cast(vehicledetail_id as varchar),vehicle_name,meter_reading,vehicledetail_id from vehicleentry ve", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    string emp_name = "";
                    dbobj.SelectQuery("Select emp_name from employee where vehicle_id = " + SqlDtr.GetValue(3).ToString().Trim() + " and designation = 'Driver'", ref SqlDtr1);
                    if (SqlDtr1.HasRows)
                    {
                        if (SqlDtr1.Read())
                            emp_name = SqlDtr1.GetValue(0).ToString();

                    }
                    SqlDtr1.Close();

                    meter_reading = SqlDtr.GetValue(2).ToString();
                    dbobj.SelectQuery("Select top 1 meter_reading_cur from VDLB where vehicle_no = " + SqlDtr.GetValue(3).ToString().Trim() + " order by DOE desc", ref SqlDtr1);
                    if (SqlDtr1.HasRows)
                    {
                        if (SqlDtr1.Read())
                            meter_reading = SqlDtr1.GetValue(0).ToString();

                    }
                    SqlDtr1.Close();
                    s = s + SqlDtr.GetValue(0).ToString() + "~" + SqlDtr.GetValue(1).ToString() + "~" + emp_name + "~" + meter_reading + "#";
                }
                SqlDtr.Close();

                if (s == null || s == "")
                    return Content(HttpStatusCode.NotFound, "Vehicle Info data Not found");

                return Ok(s);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Vehicle Info data Not found");
            }
        }

        [HttpGet]
        [Route("api/VehicleEntryLogbook/FillDropEngineOil")]
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
                if (dropEngineOil == null || dropEngineOil.Count == 0)
                    return Content(HttpStatusCode.NotFound, "Engine Oil data Not found");

                return Ok(dropEngineOil);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Engine Oil data Not found");
            }
        }

        [HttpGet]
        [Route("api/VehicleEntryLogbook/FillVehicleEntryLogbookID")]
        public IHttpActionResult FillVehicleEntryLogbookID()
        {
            List<string> dropVehicleEntryLogbookID = new List<string>();
            try
            {
                SqlDataReader SqlDtr = null;

                dbobj.SelectQuery("Select VDLB_id from vdlb order by VDLB_ID", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    dropVehicleEntryLogbookID.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();
                return Ok(dropVehicleEntryLogbookID);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Vehicle Entry Logbook ID data Not found");
            }
        }

        [HttpGet]
        [Route("api/VehicleEntryLogbook/FillDropbreak")]
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
                if (dropBreak == null || dropBreak.Count == 0)
                    return Content(HttpStatusCode.NotFound, "Brake Oil data Not found");

                return Ok(dropBreak);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Brake Oil data Not found");
            }
        }

        [HttpGet]
        [Route("api/VehicleEntryLogbook/FillDropGear")]
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
                if (dropGear == null || dropGear.Count == 0)
                    return Content(HttpStatusCode.NotFound, "Gear data Not found");

                return Ok(dropGear);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Gear data Not found");
            }
        }

        [HttpGet]
        [Route("api/VehicleEntryLogbook/FillDropCoolent")]
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
                if (dropCoolent == null || dropCoolent.Count == 0)
                    return Content(HttpStatusCode.NotFound, "Coolent data Not found");

                return Ok(dropCoolent);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Coolent data Not found");
            }
        }

        [HttpGet]
        [Route("api/VehicleEntryLogbook/FillDropGrease")]
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
                if (dropGrease == null || dropGrease.Count == 0)
                    return Content(HttpStatusCode.NotFound, "Grease data Not found");

                return Ok(dropGrease);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Grease data Not found");
            }
        }

        [HttpGet]
        [Route("api/VehicleEntryLogbook/FillDropTransmission")]
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

                if (dropTransmission == null || dropTransmission.Count == 0)
                    return Content(HttpStatusCode.NotFound, "Trans Oil Not found");

                return Ok(dropTransmission);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Trans Oil Not found");
            }
        }


        [HttpGet]
        [Route("api/VehicleEntryLogbook/FillDropvehicleroute")]
        public IHttpActionResult FillDropvehicleroute()
        {
            try
            {
                List<string> dropvehicleroute = new List<string>();

                SqlConnection con11;
                SqlCommand cmdselect11;
                SqlDataReader dtrdrive11;
                con11 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                con11.Open();
                cmdselect11 = new SqlCommand("Select Route_name  From Route", con11);
                dtrdrive11 = cmdselect11.ExecuteReader();
                dropvehicleroute.Add("Select");
                while (dtrdrive11.Read())
                {
                    dropvehicleroute.Add(dtrdrive11.GetString(0));
                }
                dtrdrive11.Close();
                con11.Close();

                if (dropvehicleroute == null || dropvehicleroute.Count == 0)
                    return Content(HttpStatusCode.NotFound, "vehicle Route Not found");
                return Ok(dropvehicleroute);
            }
            catch
            {
                return Content(HttpStatusCode.NotFound, "vehicle Route Not found");
            }
        }

        [HttpPost]
        [Route("api/VehicleEntryLogbook/InsertVehicleDailyLogbook")]
        public IHttpActionResult InsertVehicleDailyLogbook(VehicleDailyLogbookModel vehDLB)
        {

            try
            {

                object op = null;
                // calls the procedure proVDLBEntry to insert the vehicle log details
                dbobj.ExecProc(Servo_API.App_Start.DbOperations_LATEST.OprType.Insert, "proVDLBEntry", ref op, "@VDLB_ID", vehDLB.VDLB_ID, "@vehicle_no", vehDLB.Vehicle_no, "@DOE", vehDLB.DOE, "@Meter_Reading_Pre", vehDLB.Meter_reading_pre, "@Meter_Reading_Cur", vehDLB.Meter_reading_cur, "@vehicle_route", vehDLB.Vehicle_route, "@Fuel_Used", vehDLB.Fuel_Used, "@Fuel_Used_Qty", vehDLB.Fuel_Used_Qty, "@Engine_Oil", vehDLB.EngineOil, "@Engine_pack", vehDLB.Engine_Oil_Pack, "@Engine_Oil_Qty", vehDLB.Engine_Oil_Qty, "@Gear_Oil", vehDLB.Gear_Oil, "@Gear_pack", vehDLB.Gear_Oil_Pack, "@Gear_Oil_Qty", vehDLB.Gear_Oil_Qty, "@Grease", vehDLB.Grease, "@Grease_pack", vehDLB.Grease_Pack, "@Grease_Qty", vehDLB.Grease_Qty,
                    "@Brake_Oil", vehDLB.Brake_Oil, "@Brake_pack", vehDLB.Brake_Oil_Pack, "@Brake_Oil_Qty", vehDLB.Brake_Oil_Qty, "@Coolent", vehDLB.Coolent, "@Coolent_Pack", vehDLB.Coolent_Oil_Pack, "@Coolent_Qty", vehDLB.Coolent_Oil_Qty, "@Trans_Oil", vehDLB.Trans_Oil, "@Trans_pack", vehDLB.Trans_Oil_Pack, "@Trans_Oil_Qty", vehDLB.Trans_Oil_Qty, "@Toll", vehDLB.Toll, "@Police", vehDLB.Police, "@Food", vehDLB.Food, "@Misc", vehDLB.Misc);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Vehicle Logbbok ID could not insert data");
            }
        }

        [HttpPost]
        [Route("api/VehicleEntry/UpdateVehicleDailyLogbook")]
        public IHttpActionResult UpdateVehicleDailyLogbook(VehicleDailyLogbookModel vehDLB)
        {

            try
            {

                object op = null;
                // calls the procedure proVDLBEntry to insert the vehicle log details
                dbobj.ExecProc(Servo_API.App_Start.DbOperations_LATEST.OprType.Insert, "proVDLBUpdate", ref op, "@VDLB_ID", vehDLB.VDLB_ID, "@vehicle_no", vehDLB.Vehicle_no, "@DOE", vehDLB.DOE, "@Meter_Reading_Pre", vehDLB.Meter_reading_pre, "@Meter_Reading_Cur", vehDLB.Meter_reading_cur, "@vehicle_route", vehDLB.Vehicle_route, "@Fuel_Used", vehDLB.Fuel_Used, "@Fuel_Used_Qty", vehDLB.Fuel_Used_Qty, "@Engine_Oil", vehDLB.EngineOil, "@Engine_pack", vehDLB.Engine_Oil_Pack, "@Engine_Oil_Qty", vehDLB.Engine_Oil_Qty, "@Gear_Oil", vehDLB.Gear_Oil, "@Gear_pack", vehDLB.Gear_Oil_Pack, "@Gear_Oil_Qty", vehDLB.Gear_Oil_Qty, "@Grease", vehDLB.Grease, "@Grease_pack", vehDLB.Grease_Pack, "@Grease_Qty", vehDLB.Grease_Qty,
                    "@Brake_Oil", vehDLB.Brake_Oil, "@Brake_pack", vehDLB.Brake_Oil_Pack, "@Brake_Oil_Qty", vehDLB.Brake_Oil_Qty, "@Coolent", vehDLB.Coolent, "@Coolent_Pack", vehDLB.Coolent_Oil_Pack, "@Coolent_Qty", vehDLB.Coolent_Oil_Qty, "@Trans_Oil", vehDLB.Trans_Oil, "@Trans_pack", vehDLB.Trans_Oil_Pack, "@Trans_Oil_Qty", vehDLB.Trans_Oil_Qty, "@Toll", vehDLB.Toll, "@Police", vehDLB.Police, "@Food", vehDLB.Food, "@Misc", vehDLB.Misc);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Vehicle Logbbok ID could not update data");
            }
        }

        [HttpGet]
        [Route("api/VehicleEntryLogbook/GetDropVehicleID_SelectedData")]
        public IHttpActionResult GetDropVehicleID_SelectedData(string VehicleID)
        {

            VehicleDailyLogbookModel vehDLB = new VehicleDailyLogbookModel();

            try
            {
                SqlDataReader SqlDtr = null;
                SqlDataReader SqlDtr1 = null;
                dbobj.SelectQuery("select v.*,(ve.vehicle_no+' VID '+cast(ve.vehicledetail_id as varchar)) as v_no,ve.vehicle_name,ve.vehicledetail_id from vdlb v,vehicleentry ve where  ve.vehicledetail_id = v.vehicle_no and  vdlb_id =" + VehicleID, ref SqlDtr);
                if (SqlDtr.Read())
                {
                    string emp_name = "";
                    dbobj.SelectQuery("Select emp_name from employee where vehicle_id = " + SqlDtr["vehicledetail_id"].ToString().Trim() + " and designation = 'Driver'", ref SqlDtr1);
                    if (SqlDtr1.HasRows)
                    {
                        if (SqlDtr1.Read())
                            emp_name = SqlDtr1.GetValue(0).ToString().Trim();

                    }
                    SqlDtr1.Close();

                    vehDLB.Vehicle_no = SqlDtr["v_no"].ToString().Trim();
                    // DropVehicleNo.SelectedIndex = DropVehicleNo.Items.IndexOf(DropVehicleNo.Items.FindByText(vehicle_no));
                    vehDLB.Vehicle_Name = SqlDtr["vehicle_name"].ToString().Trim();
                    vehDLB.DOE = SqlDtr["DOE"].ToString().Trim();
                    vehDLB.DriverName = emp_name;
                    vehDLB.Meter_reading_pre = SqlDtr["meter_reading_pre"].ToString().Trim();
                    vehDLB.Meter_reading_cur = SqlDtr["meter_reading_cur"].ToString().Trim();
                    dbobj.SelectQuery("Select route_name From route where route_id =" + SqlDtr["vehicle_route"].ToString().Trim(), ref SqlDtr1);
                    if (SqlDtr1.Read())
                    {
                        vehDLB.Vehicle_route = SqlDtr1.GetValue(0).ToString().Trim();
                    }
                    SqlDtr1.Close();

                    vehDLB.Fuel_Used = SqlDtr["Fuel_Used"].ToString().Trim();
                    vehDLB.Fuel_Used_Qty = SqlDtr["Fuel_Used_Qty"].ToString().Trim();
                    dbobj.SelectQuery("Select prod_name+':'+pack_type from products where prod_id =" + SqlDtr["Engine_Oil"].ToString().Trim() + " and Category like 'Engine Oil%'", ref SqlDtr1);
                    if (SqlDtr1.Read())
                    {
                        vehDLB.EngineOil = SqlDtr1.GetValue(0).ToString().Trim();
                    }
                    SqlDtr1.Close();

                    vehDLB.Engine_Oil_Qty = SqlDtr["Engine_Oil_Qty"].ToString().Trim();

                    dbobj.SelectQuery("Select prod_name+':'+pack_type from products where prod_id =" + SqlDtr["Gear_Oil"].ToString().Trim() + " and Category like 'Gear Oil%'", ref SqlDtr1);
                    if (SqlDtr1.Read())
                    {
                        vehDLB.Gear_Oil = SqlDtr1.GetValue(0).ToString().Trim();
                    }
                    SqlDtr1.Close();

                    vehDLB.Gear_Oil_Qty = SqlDtr["Gear_Oil_Qty"].ToString().Trim();

                    dbobj.SelectQuery("Select prod_name+':'+pack_type from products where prod_id =" + SqlDtr["Grease"].ToString().Trim() + " and Category like 'Grease%'", ref SqlDtr1);
                    if (SqlDtr1.Read())
                    {
                        vehDLB.Grease = SqlDtr1.GetValue(0).ToString().Trim();
                    }
                    SqlDtr1.Close();

                    vehDLB.Grease_Qty = SqlDtr["Grease_Qty"].ToString().Trim();

                    dbobj.SelectQuery("Select prod_name+':'+pack_type from products where prod_id =" + SqlDtr["Brake_Oil"].ToString().Trim() + " and Category like 'Brake Oil%'", ref SqlDtr1);
                    if (SqlDtr1.Read())
                    {
                        vehDLB.Brake_Oil = SqlDtr1.GetValue(0).ToString().Trim();
                    }
                    SqlDtr1.Close();

                    vehDLB.Brake_Oil_Qty = SqlDtr["Brake_Oil_Qty"].ToString().Trim();

                    dbobj.SelectQuery("Select prod_name+':'+pack_type from products where prod_id =" + SqlDtr["Coolent"].ToString().Trim() + " and Category like 'Collents%'", ref SqlDtr1);
                    if (SqlDtr1.Read())
                    {
                        vehDLB.Coolent = SqlDtr1.GetValue(0).ToString().Trim();
                    }
                    SqlDtr1.Close();

                    vehDLB.Coolent_Oil_Qty = SqlDtr["Coolent_Qty"].ToString().Trim();

                    dbobj.SelectQuery("Select prod_name+':'+pack_type from products where prod_id =" + SqlDtr["Trans_Oil"].ToString().Trim() + " and Category like 'Transmission Oil%'", ref SqlDtr1);
                    if (SqlDtr1.Read())
                    {
                        vehDLB.Trans_Oil = SqlDtr1.GetValue(0).ToString().Trim();
                    }
                    SqlDtr1.Close();
                    vehDLB.Trans_Oil_Qty = SqlDtr["Trans_Oil_Qty"].ToString().Trim();

                    vehDLB.Toll = SqlDtr["Toll"].ToString().Trim();
                    vehDLB.Police = SqlDtr["Police"].ToString().Trim();
                    vehDLB.Food = SqlDtr["Food"].ToString().Trim();
                    vehDLB.Misc = SqlDtr["misc"].ToString().Trim();
                }
                SqlDtr.Close();
                if (vehDLB == null)
                    return Content(HttpStatusCode.NotFound, "Vehicle Logbbok ID Data Not found");
                return Ok(vehDLB);
            }
            catch
            {
                return Content(HttpStatusCode.NotFound, "Vehicle Logbbok ID Data Not found");
            }
        }



        [HttpPost]
        [Route("api/VehicleEntryLogbook/DeleteVehicleEntryLogbook")]
        public IHttpActionResult DeleteVehicleEntryLogbook(string vehicleLogbookID)
        {
            int count = 0;

            try
            {
                dbobj.Insert_or_Update("Delete from vdlb where vdlb_id = " + vehicleLogbookID, ref count);
                if (count < 1)
                    return Content(HttpStatusCode.NotFound, "vehicle Route Not found");
                return Ok(count);
            }
            catch
            {
                return Content(HttpStatusCode.NotFound, "Could not delete Vehicle Entry Logbook");
            }
        }
    }
 }
