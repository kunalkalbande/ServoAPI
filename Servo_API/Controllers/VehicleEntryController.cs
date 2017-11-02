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
        static string FromDate = "", ToDate = "";
        App_Start.DbOperations_LATEST.DBUtil dbobj = new App_Start.DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);

        [HttpGet]
        [Route("api/VehicleEntry/FillDropEngineOil")]
        public List<string> FillDropEngineOil()
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
                return dropEngineOil;
            }
            catch (Exception ex)
            {
                return dropEngineOil;
            }
        }

        [HttpGet]
        [Route("api/VehicleEntry/FillDropbreak")]
        public List<string> FillDropbreak()
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
                return dropBreak;
            }
            catch (Exception ex)
            {
                return dropBreak;
            }
        }

        [HttpGet]
        [Route("api/VehicleEntry/FillDropGear")]
        public List<string> FillDropGear()
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
                return dropGear;
            }
            catch (Exception ex)
            {
                return dropGear;
            }
        }

        [HttpGet]
        [Route("api/VehicleEntry/FillDropCoolent")]
        public List<string> FillDropCoolent()
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
                return dropCoolent;
            }
            catch (Exception ex)
            {
                return dropCoolent;
            }
        }

        [HttpGet]
        [Route("api/VehicleEntry/FillDropGrease")]
        public List<string> FillDropGrease()
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
                return dropGrease;
            }
            catch (Exception ex)
            {
                return dropGrease;
            }
        }

        [HttpGet]
        [Route("api/VehicleEntry/FillDropTransmission")]
        public List<string> FillDropTransmission()
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
                return dropTransmission;
            }
            catch (Exception ex)
            {
                return dropTransmission;
            }
        }

        [HttpGet]
        [Route("api/VehicleEntry/GetNextVehicledetailID")]
        public string GetNextVehicledetailID()
        {
            string vehicleDetailID = string.Empty;
            try
            {

                #region Fetch Next Role ID                
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
                return vehicleDetailID;
            }
            catch (Exception ex)
            {
                return vehicleDetailID;
            }

        }

        [HttpPost]
        [Route("api/VehicleEntry/UpdateVehicleEntry")]
        public bool UpdateVehicleEntry(VehicleEntryModel vehEntryModel)
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
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/VehicleEntry/InsertVehicleEntry")]
        public bool InsertVehicleEntry(VehicleEntryModel vehEntryModel)
        {
            //vehEntryModel.Vehicleno = txtVehicleno.Text.Trim();

            //vehEntryModel.Vehiclenm = txtVehiclenm.Text.Trim();

            //vehEntryModel.RTO_Reg_Val_yrs = GenUtil.str2MMDDYYYY(Request.Form["txtrtoregvalidity"].ToString().Trim());
            //vehEntryModel.Model_name = txtmodelnm.Text.Trim();

            //vehEntryModel.RTO_Reg_No = txtrtono.Text.Trim();
            //vehEntryModel.Vehicle_Man_Date = GenUtil.str2MMDDYYYY(Request.Form["txtVehicleyear"].ToString().Trim());
            //vehEntryModel.Insurance_No = txtinsuranceno.Text.Trim();
            //vehEntryModel.Meter_Reading = txtVehiclemreading.Text.Trim();
            //vehEntryModel.Insurance_validity = GenUtil.str2MMDDYYYY(Request.Form["txtvalidityinsurance"].ToString().Trim());
            //vehEntryModel.RouteName = DropDownList1.SelectedItem.Text.Trim();

            //vehEntryModel.Fuel_Used = DropFuelused.SelectedItem.Text.Trim();
            //vehEntryModel.Fuel_Used_Qty = txtfuelinword.Text.Trim();
            //vehEntryModel.Start_Fuel_Qty = txtfuelintank.Text.Trim();

            //vehEntryModel.EngineOil = DropEngineOil.SelectedItem.Text.Trim();
            //vehEntryModel.Engine_Oil_Qty = txtEngineQty.Text.Trim();
            //vehEntryModel.Engine_Oil_Dt = GenUtil.str2MMDDYYYY(Request.Form["txtEngineOilDate"].ToString().Trim());

            //vehEntryModel.Gear_Oil = Dropgear.SelectedItem.Text.Trim();
            //vehEntryModel.Gear_Oil_Qty = txtgearinword.Text.Trim();
            //vehEntryModel.Gear_Oil_Dt = GenUtil.str2MMDDYYYY(Request.Form["txtgeardt"].ToString().Trim());

            //vehEntryModel.Brake_Oil = Dropbreak.SelectedItem.Text.Trim();
            //vehEntryModel.Brake_Oil_Qty = txtbearkinword.Text.Trim();
            //vehEntryModel.Brake_Oil_Dt = GenUtil.str2MMDDYYYY(Request.Form["txtbreakdt"].ToString().Trim());

            //vehEntryModel.Coolent = Dropcoolent.SelectedItem.Text.Trim();
            //vehEntryModel.Coolent_Oil_Qty = txtcoolentinword.Text.Trim();
            //vehEntryModel.Coolent_Oil_Dt = GenUtil.str2MMDDYYYY(Request.Form["txtcoolentdt"].ToString().Trim());

            //vehEntryModel.Grease = Dropgrease.SelectedItem.Text.Trim();
            //vehEntryModel.Grease_Qty = txtgreaseinword.Text.Trim();
            //vehEntryModel.Grease_Dt = GenUtil.str2MMDDYYYY(Request.Form["txtgreasedt"].ToString().Trim());

            //vehEntryModel.Trans_Oil = Droptransmission.SelectedItem.Text.Trim();
            //vehEntryModel.Trans_Oil_Qty = txttransinword.Text.Trim();
            //vehEntryModel.Trans_Oil_Dt = GenUtil.str2MMDDYYYY(Request.Form["txttransmissiondt"].ToString().Trim());
            //vehEntryModel.Trans_Oil_km = txttransmissionkm.Text.Trim();
            //vehEntryModel.Vehicle_Avg = txtvechileavarge.Text.Trim();

            //vehEntryModel.Engine_Oil_km = txtEngineKM.Text.Trim();
            //vehEntryModel.Gear_Oil_km = txtgearkm.Text.Trim();
            //vehEntryModel.Brake_Oil_km = txtbreakkm.Text.Trim();
            //vehEntryModel.Coolent_km = txtcoolentkm.Text.Trim();
            //vehEntryModel.Grease_km = txtgreasekm.Text.Trim();

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
                //Fetch the product id for selected product of type fuel from table products.
                //				dbobj.SelectQuery("Select prod_id from products where prod_name='"+DropFuelused.SelectedItem.Text.Trim()+"' and Category ='Fuel'" ,ref SqlDtr);
                //				if(SqlDtr.Read())
                //				{
                //					prod_id = SqlDtr.GetValue(0).ToString();       
                //				}
                //				SqlDtr.Close();
                //cmdInsert.Parameters .Add ("@Fuel_Used",prod_id );
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
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/VehicleEntry/DeleteVehicleEntry")]
        public int DeleteVehicleEntry(string vehicleID)
        {
            int count = 0;
            string roleID = string.Empty;
            try
            {                                
                dbobj.Insert_or_Update("Delete from vehicleentry where vehicledetail_id = " + vehicleID.Trim(), ref count);
                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        [HttpGet]
        [Route("api/Roles/GetCheckRoleExists")]
        public int GetCheckRoleExists(string txtRoleName)
        {
            int count = 0;
            string roleID = string.Empty;
            try
            {
                dbobj.ExecuteScalar("select count(*) from Roles where Role_Name='" + txtRoleName.Trim() + "'", ref count);

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        [HttpGet]
        [Route("api/Roles/GetCheckRoleExistsUser_master")]
        public int GetCheckRoleExistsUser_master(string txtRoleName)
        {
            int count = 0;
            string roleID = string.Empty;
            try
            {
                dbobj.ExecuteScalar("select count(*) from User_master where Role_ID='" + txtRoleName + "'", ref count);

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        

        

        [HttpPost]
        [Route("api/Roles/UpdateRole")]
        public bool UpdateRole(EmployeeClass obj)
        {
            SqlCommand cmd;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            //EmployeeClass obj1 = new EmployeeClass();
            try
            {
                obj.UpdateRoles();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("api/Roles/FillDropRoleID")]
        public List<string> FillDropRoleID()
        {            
            List<string> dropRoleID = new List<string>();
            try
            {
                //DBOperations.DBUtil obj = new DBOperations.DBUtil();
                SqlDataReader SqlDtr = null;

                dbobj.SelectQuery("select Role_ID from Roles", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    dropRoleID.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();
                return dropRoleID;
            }
            catch (Exception ex)
            {
                return dropRoleID;
            }
        }

        [HttpGet]
        [Route("api/Roles/GetSelectedRoleIDData")]
        public RolesModel GetSelectedRoleIDData(string RoleID)
        {
            RolesModel role = new RolesModel();

            List<string> dropRoleID = new List<string>();
            try
            {
                //DBOperations.DBUtil obj = new DBOperations.DBUtil();
                SqlDataReader SqlDtr = null;

                dbobj.SelectQuery("select * from roles where Role_Id='" + RoleID + "'", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    role.Role_Name = SqlDtr.GetValue(1).ToString();
                    role.Description = SqlDtr.GetValue(2).ToString();
                }
                return role;
            }
            catch (Exception ex)
            {
                return role;
            }
        }
    }
}
