using Servo_API.App_Start;
using Servo_API.Models;
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
    public class ReceiptController : ApiController
    {
        DbOperations_LATEST.DBUtil dbobj = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
        DbOperations_LATEST.DBUtil dbobj1 = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);

        [HttpGet]
        [Route("api/ReceiptController/FetchCustomerNames")]
        public ArrayList FetchCustomerNames()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            ArrayList CustName = new ArrayList();
            sql = "Select Cust_Name,city from Customer";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                CustName.Add(SqlDtr.GetValue(0).ToString() + ";" + SqlDtr.GetValue(1).ToString());
            }
            SqlDtr.Close();
            sql = "Select Ledger_Name+':'+cast(Ledger_ID as varchar) Ledger_Name from Ledger_Master lm,Ledger_master_sub_grp lmsg where lm.sub_grp_id = lmsg.sub_grp_id and lmsg.sub_grp_name not like 'Bank%'  and lmsg.sub_grp_name != 'Cash in hand' and lmsg.sub_grp_name not like 'Discount%' and lmsg.sub_grp_name != 'Sundry Debtors' Order by Ledger_Name";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                CustName.Add(SqlDtr.GetValue(0).ToString() + "; ");
            }
            SqlDtr.Close();
            sql = "Select Ledger_Name+':'+cast(Ledger_ID as varchar) Ledger_Name,city from Ledger_Master, Employee where Emp_Name=Ledger_Name Order by Ledger_Name";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                CustName.Add(SqlDtr.GetValue(0).ToString() + ";" + SqlDtr.GetValue(1).ToString());
            }
            SqlDtr.Close();
            CustName.Sort();
            return CustName;
        }
        [HttpGet]
        [Route("api/ReceiptController/FetchDiscount")]
        public List<string> FetchDiscount()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            List<string> discount = new List<string>();
            sql = "select Ledger_Name from ledger_master lm,ledger_master_sub_grp lms where lms.sub_grp_id=lm.sub_grp_id and lms.sub_grp_name='Discount'";
            SqlDtr = obj.GetRecordSet(sql);
           
            while (SqlDtr.Read())
            {
                if (!SqlDtr.GetValue(0).ToString().Equals(""))
                {
                    discount.Add(SqlDtr.GetValue(0).ToString());
                }
            }
            SqlDtr.Close();
            return discount;
        }
        [HttpGet]
        [Route("api/ReceiptController/GetOrgDate")]
        public string GetOrgDate()
        {
            InventoryClass obj = new InventoryClass();
            string Acc_Date = null;
            var SqlDtr = obj.GetRecordSet("select Acc_Date_from from Organisation");
            if (SqlDtr.Read())
            {
                Acc_Date = SqlDtr["Acc_Date_from"].ToString();
            }
            SqlDtr.Close();
            return Acc_Date;
        }
        [HttpGet]
        [Route("api/ReceiptController/CheckCashAccount")]
        public int CheckCashAccount()
        {
            SqlDataReader SqlDtr = null;
            int f1 = 0;
            dbobj.SelectQuery("select Ledger_ID from Ledger_Master lm, Ledger_Master_sub_grp lmsg where lm.sub_grp_id = lmsg.sub_grp_id and  lmsg.sub_grp_name = 'Cash in hand'", ref SqlDtr);
            if (SqlDtr.HasRows)
            {
                f1 = 1;
            }
            SqlDtr.Close();
            return f1;
        }
        [HttpGet]
        [Route("api/ReceiptController/CheckBankAccount")]
        public int CheckBankAccount()
        {
            SqlDataReader SqlDtr = null;
            int f2 = 0;
            dbobj.SelectQuery("select Ledger_ID from Ledger_Master lm, Ledger_Master_sub_grp lmsg where lm.sub_grp_id = lmsg.sub_grp_id and  lmsg.sub_grp_name = 'Bank'", ref SqlDtr);
            if (SqlDtr.HasRows)
            {
                f2 = 1;
            }
            SqlDtr.Close();
            return f2;
        }
        [HttpGet]
        [Route("api/ReceiptController/FetchCustomerPlace")]
        public string FetchCustomerPlace(string Value)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr, rdr;
            string sql;
            string city = null;
            string str = Value.Substring(0, Value.IndexOf(";"));
            if (str.IndexOf(":") > 0)
            {
                string[] arrstr = str.Split(new char[] { ':' }, str.Length);
                sql = "select City from Employee where Emp_Name='" + arrstr[0].ToString() + "'";
                rdr = obj.GetRecordSet(sql);
                if (rdr.Read())
                {
                    if (rdr.GetValue(0).ToString().Equals(""))
                        city = "";
                    else
                        city = rdr.GetValue(0).ToString();
                }
                rdr.Close();
            }
            else
            {
                sql = "select City from Customer where Cust_Name='" + str.ToString() + "'";
                SqlDtr = obj.GetRecordSet(sql);
                if (SqlDtr.Read())
                {
                    if (SqlDtr.GetValue(0).ToString().Equals(""))
                        city = "";
                    else
                        city = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();
            }
            return city;
        }
        [HttpPost]
        [Route("api/ReceiptController/GetCustomerID")]
        public string GetCustomerID(CustomerModels customer)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr, rdr;
            string sql;
            string Cust_ID = "";
            sql = "select Cust_ID  from Customer where Cust_Name='" + customer.CustomerName + "' and City = '" + customer.City + "'";
            SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
            {
                Cust_ID = SqlDtr.GetValue(0).ToString();
            }
            else
            {
                Cust_ID = "0";
            }
            SqlDtr.Close();
            return Cust_ID;
        }
        [HttpGet]
        [Route("api/ReceiptController/GetLedgerDetailsData")]
        public LedgerDetailsModels GetLedgerDetailsData(string CustomerID)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            LedgerDetailsModels ledgerDetails = new LedgerDetailsModels();
            SqlDtr = obj.GetRecordSet("select bill_no as invoice_no,Bill_date as invoice_date,Amount as balance from LedgerDetails where cust_id = '" + CustomerID + "' and Amount > 0 order by Bill_Date");
            if(SqlDtr.Read())
            {
                ledgerDetails.BillNo = SqlDtr["invoice_no"].ToString();
                ledgerDetails.BillDate= SqlDtr["invoice_date"].ToString();
                ledgerDetails.Amount = SqlDtr["balance"].ToString();
            }
            SqlDtr.Close();
            return ledgerDetails;
        }

        [HttpGet]
        [Route("api/ReceiptController/GetLedgerID")]
        public string GetLedgerID(string LedgerName)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr = null;
            string Cust_ID = "";
            string sql = "select Ledger_ID from Ledger_Master where Ledger_Name='" + LedgerName + "'";
            SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
            {
                Cust_ID = SqlDtr.GetValue(0).ToString();
            }
            else
                Cust_ID = "0";
            SqlDtr.Close();
            return Cust_ID;
        }

        [HttpGet]
        [Route("api/ReceiptController/GetCustomerID")]
        public string GetCustomerID(string LedgerName)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr = null;
            string UpdateCustomerID = "";
            string sql = "select Cust_ID from Customer where Cust_Name='" + LedgerName + "'";
            SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
            {
                UpdateCustomerID = SqlDtr.GetValue(0).ToString();
            }
            SqlDtr.Close();
            return UpdateCustomerID;
        }
        [HttpGet]
        [Route("api/ReceiptController/GetLedgerIDByBank")]
        public string GetLedgerIDByBank(string Bank)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr = null;
            string bankName = "";
            var sql = "Select Ledger_ID from Ledger_Master lm,Ledger_Master_sub_grp lmsg where lmsg.sub_grp_id = lm.sub_grp_id and lmsg.sub_grp_name like 'Bank%' and ledger_Name='" + Bank + "'";
            SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
            {
                bankName = SqlDtr.GetValue(0).ToString();
            }
            else
                bankName = "";
            SqlDtr.Close();
            return bankName;
        }
        [HttpGet]
        [Route("api/ReceiptController/GetLedgerIDByDiscount")]
        public string GetLedgerIDByDiscount(string Discount)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr = null;
            string discount = "";
            SqlDtr = obj.GetRecordSet("Select ledger_id from ledger_master lm,ledger_master_sub_grp lmsg where ledger_name='" + Discount + "' and lm.sub_grp_id=lmsg.sub_grp_id and sub_grp_name='discount'");
            if (SqlDtr.Read())
            {
                discount = SqlDtr.GetValue(0).ToString();
            }
            else
                discount = "";
            SqlDtr.Close();
            return discount;
        }
    }
}
