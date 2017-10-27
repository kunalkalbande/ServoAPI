using Servo_API.App_Start;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servo_API.Controllers
{
    public class OrganizationDetailsController : ApiController
    {
        DbOperations_LATEST.DBUtil dbobj = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
        [HttpGet]
        [Route("api/OrganizationDetailsController/CheckInvoiceEnabled")]
        public bool CheckInvoiceEnabled()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader sqldtr;
            string sql;
            bool enabled = false;
            sql = "select * from Sales_Master";
            sqldtr = obj.GetRecordSet(sql);
            if (sqldtr.Read())
                enabled = true;
            else
                enabled = false;
            sqldtr.Close();
            return enabled;
        }
        [HttpGet]
        [Route("api/OrganizationDetailsController/GetBeat")]
        public string GetBeat()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader sqldtr;
            string sql;
            string str = "";
            sql = "select city,state,country from beat_master";
            sqldtr = obj.GetRecordSet(sql);
            while (sqldtr.Read())
            {
                str = str + sqldtr.GetValue(0).ToString() + ":";
                str = str + sqldtr.GetValue(1).ToString() + ":";
                str = str + sqldtr.GetValue(2).ToString() + "#";
            }
            sqldtr.Close();
            return str;
        }
        [HttpGet]
        [Route("api/OrganizationDetailsController/GetExtraCities")]
        public List<string> GetExtraCities()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            List<string> cities = new List<string>();
            sql = "select distinct City from Beat_Master order by City asc";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                cities.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            return cities;
        }


        [HttpGet]
        [Route("api/OrganizationDetailsController/GetExtraStates")]
        public List<string> GetExtraStates()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            List<string> states = new List<string>();
            sql = "select distinct state from Beat_Master order by state asc";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                states.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            return states;
        }

        [HttpGet]
        [Route("api/OrganizationDetailsController/GetExtraCountry")]
        public List<string> GetExtraCountry()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            List<string> country = new List<string>();
            sql = "select distinct country from Beat_Master order by country asc";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                country.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            return country;
        }
        [HttpGet]
        [Route("api/OrganizationDetailsController/GetNextID")]
        public string GetNextID()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            string ID = null;
            sql = "select max(CompanyID)+1 from Organisation";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                ID = SqlDtr.GetValue(0).ToString();
            }
            SqlDtr.Close();
            return ID;
            
        }
        [HttpPost]
        [Route("api/OrganizationDetailsController/InsertOrganizationDetails")]
        public void InsertOrganizationDetails(OrganizationModels organization)
        {
            SqlConnection conMyData;
            string strInsert;
            SqlCommand cmdInsert;
            conMyData = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            conMyData.Open();
            strInsert = "Insert Into Organisation (CompanyID,DealerName,DealerShip,Address,City,State,Country ,PhoneNo ,FaxNo ,Email,Website,TinNo,Entrytax ,FoodLicNO,WM,Logo,Div_Office,Message,VAT_Rate,Acc_Date_from,Acc_Date_to,startinvoice) " + "Values (@CompanyID,@DealerName,@DealerShip,@Address,@City,@State,@Country ,@PhoneNo,@FaxNo ,@Email,@Website,@TinNo,@Entrytax,@FoodLicNO,@WM,@Logo,@Div_Office,@Message,@VAT_Rate,@Acc_date_from,@Acc_date_To,@StartInvoice)";
            cmdInsert = new SqlCommand(strInsert, conMyData);

            cmdInsert.Parameters.Add("@StartInvoice", organization.InvoiceNo);
            cmdInsert.Parameters.Add("@CompanyID", organization.CompanyID);
            cmdInsert.Parameters.Add("@DealerName", organization.DealerName);
            cmdInsert.Parameters.Add("@DealerShip", organization.DealerShip);
            cmdInsert.Parameters.Add("@Address", organization.Address);
            cmdInsert.Parameters.Add("@City", organization.City);
            cmdInsert.Parameters.Add("@State", organization.State);
            cmdInsert.Parameters.Add("@Country", organization.Country);
            cmdInsert.Parameters.Add("@PhoneNo", organization.PhoneOff);
            cmdInsert.Parameters.Add("@FaxNo", organization.FaxNo);
            cmdInsert.Parameters.Add("@Email", organization.EMail);
            cmdInsert.Parameters.Add("@Website", organization.Website);
            cmdInsert.Parameters.Add("@TinNo", organization.Tinno);
            cmdInsert.Parameters.Add("@Entrytax", organization.Entrytax);
            cmdInsert.Parameters.Add("@FoodLicNO", organization.FoodLicNO);
            cmdInsert.Parameters.Add("@WM", organization.WMlic);
            cmdInsert.Parameters.Add("@Logo", organization.Logo);
            cmdInsert.Parameters.Add("@Div_Office", organization.DivOffice);
            cmdInsert.Parameters.Add("@Message", organization.Message);
            cmdInsert.Parameters.Add("@VAT_Rate", organization.VATRate);
            cmdInsert.Parameters.Add("@Acc_date_from", organization.DateFrom);
            cmdInsert.Parameters.Add("@Acc_date_to", organization.DateTo);
            
            cmdInsert.ExecuteNonQuery();
            object op = null;
            dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "ProInsertLedger", ref op, "@Ledger_Name", "Cash", "@SubGrp_Name", "Cash in hand", "@Group_Name", "Current Assets", "@Grp_Nature", "Assets", "@Op_Bal", "0", "@Bal_Type", "Dr");
            conMyData.Close();
        }

        [HttpPost]
        [Route("api/OrganizationDetailsController/UpdateOrganizationDetails")]
        public void UpdateOrganizationDetails(OrganizationModels organization)
        {
            SqlConnection conMyData;
            string strInsert;
            SqlCommand cmdInsert;
            conMyData = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            conMyData.Open();
            strInsert = "update Organisation set CompanyID=@CompanyID,DealerName=@DealerName,DealerShip=@DealerShip,Address=@Address,City=@City,State=@State,Country=@Country  ,PhoneNo=@PhoneNo ,FaxNo=@FaxNo ,Email=@Email,Website=@Website,TinNo=@TinNo,Entrytax=@Entrytax ,FoodLicNO=@FoodLicNO,WM=@WM ,Logo=@Logo,Div_Office=@Div_Office,Message=@Message,VAT_Rate = @VAT_Rate,Acc_Date_From = @Acc_date_From,Acc_Date_To = @Acc_date_to, StartInvoice=@StartInvoice where CompanyID=@CompanyID";
            cmdInsert = new SqlCommand(strInsert, conMyData);
            cmdInsert.Parameters.Add("@StartInvoice", organization.InvoiceNo);
            cmdInsert.Parameters.Add("@CompanyID", organization.CompanyID);
            cmdInsert.Parameters.Add("@DealerName", organization.DealerName);
            cmdInsert.Parameters.Add("@DealerShip", organization.DealerShip);
            cmdInsert.Parameters.Add("@Address", organization.Address);
            cmdInsert.Parameters.Add("@City", organization.City);
            cmdInsert.Parameters.Add("@State", organization.State);
            cmdInsert.Parameters.Add("@Country", organization.Country);
            cmdInsert.Parameters.Add("@PhoneNo", organization.PhoneOff);
            cmdInsert.Parameters.Add("@FaxNo", organization.FaxNo);
            cmdInsert.Parameters.Add("@Email", organization.EMail);
            cmdInsert.Parameters.Add("@Website", organization.Website);
            cmdInsert.Parameters.Add("@TinNo", organization.Tinno);
            cmdInsert.Parameters.Add("@Entrytax", organization.Entrytax);
            cmdInsert.Parameters.Add("@FoodLicNO", organization.FoodLicNO);
            cmdInsert.Parameters.Add("@WM", organization.WMlic);
            cmdInsert.Parameters.Add("@Logo", organization.Logo);
            cmdInsert.Parameters.Add("@Div_Office", organization.DivOffice);
            cmdInsert.Parameters.Add("@Message", organization.Message);
            cmdInsert.Parameters.Add("@VAT_Rate", organization.VATRate);
            cmdInsert.Parameters.Add("@Acc_date_from", organization.DateFrom);
            cmdInsert.Parameters.Add("@Acc_date_to", organization.DateTo);
            cmdInsert.ExecuteNonQuery();
            conMyData.Close();
        }
        [HttpGet]
        [Route("api/OrganizationDetailsController/GetOrganizations")]
        public List<string> GetOrganizations()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            List<string> orgs = new List<string>();
            sql = "select max(CompanyID) from Organisation";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                orgs.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            return orgs;
        }
        [HttpGet]
        [Route("api/OrganizationDetailsController/GetSelectedOrganization")]
        public OrganizationModels GetSelectedOrganization(string OrgID)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            OrganizationModels organization = new OrganizationModels();
            sql = "select * from Organisation where CompanyID='" + OrgID + "'";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                organization.DealerName = SqlDtr.GetValue(1).ToString();
                organization.DealerShip = SqlDtr.GetValue(2).ToString();
                organization.Address = SqlDtr.GetValue(3).ToString();
                organization.City = SqlDtr.GetValue(4).ToString();
                organization.State = SqlDtr.GetValue(5).ToString();
                organization.Country = SqlDtr.GetValue(6).ToString();
                organization.PhoneOff = SqlDtr.GetValue(7).ToString();
                organization.FaxNo = SqlDtr.GetValue(8).ToString();
                organization.EMail = SqlDtr.GetValue(9).ToString();
                organization.Website = SqlDtr.GetValue(10).ToString();
                organization.Tinno = SqlDtr.GetValue(11).ToString();

                organization.Entrytax = SqlDtr.GetValue(12).ToString();
                organization.FoodLicNO = SqlDtr.GetValue(13).ToString();
                organization.WMlic = SqlDtr.GetValue(14).ToString();
                organization.StateOffice = SqlDtr.GetValue(16).ToString();
                organization.Logo = SqlDtr.GetValue(15).ToString();
                organization.Message = SqlDtr.GetValue(17).ToString();
                organization.VATRate = SqlDtr.GetValue(18).ToString().Trim();
                organization.DateFrom = SqlDtr.GetValue(19).ToString().Trim();
                organization.DateTo = SqlDtr.GetValue(20).ToString().Trim();
                organization.InvoiceNo = SqlDtr.GetValue(21).ToString();
            }
            SqlDtr.Close();
            return organization;
        }
    }
}
