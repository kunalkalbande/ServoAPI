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
        public IHttpActionResult FetchCustomerNames()
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
            if (CustName == null || CustName.Count == 0)
            {
                return Content(HttpStatusCode.NotFound, "Customer Names Not Found");
            }
            return Ok(CustName);
        }
        [HttpGet]
        [Route("api/ReceiptController/FetchDiscount")]
        public IHttpActionResult FetchDiscount()
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
            if (discount == null || discount.Count == 0)
            {
                return Content(HttpStatusCode.NotFound, "Discount Not Found");
            }
            return Ok(discount);
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
            if (SqlDtr.Read())
            {
                ledgerDetails.BillNo = SqlDtr["invoice_no"].ToString();
                ledgerDetails.BillDate = SqlDtr["invoice_date"].ToString();
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

        [HttpPost]
        [Route("api/ReceiptController/GetCustID")]
        public string GetCustID(PaymentReceiptModel payment)
        {
            int OldCustID = 0;
            if (payment.PanReceiptNo == true)
            {
                dbobj.ExecuteScalar("select cust_id from customer where cust_name=(select ledger_name from ledger_master where ledger_id = '" + payment.CustomerID + "')", ref OldCustID);
                var customerID = OldCustID.ToString();
            }

            /****************Add by vikas 12.09.09 ********************************/
            else
            {
                dbobj.ExecuteScalar("select cust_id from customer where cust_name=(select ledger_name from ledger_master where ledger_name= '" + payment.CustomerID + "')", ref OldCustID);
                var customerID = OldCustID.ToString();
            }

            return OldCustID.ToString();
        }

        [HttpPost]
        [Route("api/ReceiptController/InsertPayment")]
        public void InsertPayment(PaymentReceiptModel payment)
        {
            object op = null;
            if (payment.PanReceiptNo == true)
            {
                //************* Add This code by Mahesh on 05.07.008 ******************
                int x = 0;
                dbobj1.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Receipt (" + payment.ReceiptNo + ")'", ref x);
                dbobj1.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Receipt_" + payment.Discount1 + " (" + payment.ReceiptNo + ")'", ref x);
                dbobj1.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Receipt_" + payment.Discount2 + " (" + payment.ReceiptNo + ")'", ref x);
                dbobj1.Insert_or_Update("delete from CustomerLedgerTable where Particular = 'Payment Received(" + payment.ReceiptNo + ")' and CustID='" + payment.CustomerID + "'", ref x);
                dbobj1.Insert_or_Update("delete from Payment_Receipt where Receipt_No='" + payment.ReceiptNo + "'", ref x);
                //*********************************************************************
                //dbobj.ExecProc(OprType.Insert,"InsertPayment",ref op,"@Ledger_ID",Ledger_ID,"@amount",Amount,"@Acc_Type",Acc_Type,"@BankName",Acc_Type,"@ChNo",txtChequeno.Text,"@ChDate",txtDate.Text,"@Mode",DropMode.SelectedItem.Text,"@Narration",txtNar.Text,"@CustBankName",txtCustBankName.Text);
                if (payment.Mode == "Cash")
                {
                    dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "InsertPayment", ref op, "@Ledger_ID", payment.Cust_ID, "@amount", payment.Amount, "@Acc_Type", payment.AccountType, "@BankName", "", "@ChNo", "", "@ChDate", "", "@Mode", payment.Mode, "@Narration", "", "@CustBankName", "", "@RecDate", payment.RecDate, "@Cust_ID", payment.Cust_ID, "@Receipt", "Save", "@Receipt_No", payment.ReceiptNo);

                }
                else
                {
                    dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "InsertPayment", ref op, "@Ledger_ID", payment.Cust_ID, "@amount", payment.Amount, "@Acc_Type", payment.AccountType, "@BankName", payment.AccountType, "@ChNo", payment.ChequeNumber, "@ChDate", payment.ChequeDate, "@Mode", payment.Mode, "@Narration", payment.Narration, "@CustBankName", payment.CustBankName, "@RecDate", payment.RecDate, "@Cust_ID", payment.Cust_ID, "@Receipt", "Save", "@Receipt_No", payment.ReceiptNo);
                }
            }
            else
            {
                var date = Convert.ToDateTime(payment.RecDate);
                if (payment.Mode == "Cash")
                    dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "InsertPayment", ref op, "@Ledger_ID", payment.Cust_ID, "@amount", payment.Amount, "@Acc_Type", payment.AccountType, "@BankName", "", "@ChNo", "", "@ChDate", "", "@Mode", payment.Mode, "@Narration", "", "@CustBankName", "", "@RecDate", date, "@Cust_ID", payment.Cust_ID, "@Receipt", "Save", "@Receipt_No", payment.ReceiptNo);
                else
                    dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "InsertPayment", ref op, "@Ledger_ID", payment.Cust_ID, "@amount", payment.Amount, "@Acc_Type", payment.AccountType, "@BankName", payment.AccountType, "@ChNo", payment.ChequeNumber, "@ChDate", payment.ChequeDate, "@Mode", payment.Mode, "@Narration", payment.Narration, "@CustBankName", payment.CustBankName, "@RecDate", payment.RecDate, "@Cust_ID", payment.Cust_ID, "@Receipt", "Save", "@Receipt_No", payment.ReceiptNo);
            }
        }
        [HttpPost]
        [Route("api/ReceiptController/UpdatePayment")]
        public void UpdatePayment(PaymentReceiptModel payment)
        {
            object op = null;
            InventoryClass obj = new InventoryClass();
            if (payment.PanReceiptNo == true)
            {
                int x = 0;
                dbobj1.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Receipt (" + payment.ReceiptNo + ")'", ref x);
                dbobj1.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Receipt_" + payment.Discount1 + " (" + payment.ReceiptNo + ")'", ref x);
                dbobj1.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Receipt_" + payment.Discount2 + " (" + payment.ReceiptNo + ")'", ref x);
                dbobj1.Insert_or_Update("delete from CustomerLedgerTable where Particular = 'Payment Received(" + payment.ReceiptNo + ")'", ref x);
                dbobj1.Insert_or_Update("delete from Payment_Receipt where Receipt_No='" + payment.ReceiptNo + "'", ref x);

                int Curr_Credit = 0;
                int Credit_Limit = 0;
                dbobj.ExecuteScalar("Select Cr_Limit from customer where Cust_ID = '" + payment.CustomerID + "'", ref Credit_Limit);
                dbobj.ExecuteScalar("Select Curr_Credit from customer where Cust_ID = '" + payment.CustomerID + "'", ref Curr_Credit);

                if (Curr_Credit <= Credit_Limit)
                {
                    Curr_Credit = Curr_Credit + int.Parse(payment.ReceivedAmount);

                    Curr_Credit = Curr_Credit - int.Parse(payment.TotalRec);

                    if (@Curr_Credit >= @Credit_Limit)
                        dbobj1.Insert_or_Update("update customer set Curr_Credit = '" + Credit_Limit + "' where Cust_ID  = '" + payment.CustomerID + "'", ref x);
                    else
                        dbobj1.Insert_or_Update("update customer set Curr_Credit = '" + Curr_Credit + "' where Cust_ID  = '" + payment.CustomerID + "'", ref x);
                }
                else
                {
                    Curr_Credit = Curr_Credit + int.Parse(payment.ReceivedAmount);
                    Curr_Credit = Curr_Credit - int.Parse(payment.TotalRec);
                    dbobj1.Insert_or_Update("update customer set Cr_Limit = '" + Curr_Credit + "' where Cust_ID  = '" + payment.CustomerID + "'", ref x);
                }

                obj.InsertPaymentReceived(payment);

                dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "ProCustLedgerEntry", ref op, "@Cust_Name", payment.CustomerName, "@City", payment.City, "@Amount", payment.Amount, "@Rec_Acc_Type", payment.AccountType, "@Receipt", payment.Receipt, "@Receipt_No", payment.ReceiptNo, "@ActualAmount", payment.ReceivedAmount, "@RecDate", payment.RecDate);
                if (payment.Discount1 != "" && payment.Discount1 != "0")
                    dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "ProSpacialDiscountEntry", ref op, "@Cust_ID", payment.Cust_ID, "@Receipt", "Save", "@Receipt_No", payment.ReceiptNo, "@Amount", payment.Discount1, "@Ledger_ID", payment.DiscLedgerID1, "@RecDate", payment.RecDate, "@DisType", payment.Discount1);
                if (payment.Discount2 != "" && payment.Discount2 != "0")
                    dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "ProSpacialDiscountEntry", ref op, "@Cust_ID", payment.Cust_ID, "@Receipt", "Save", "@Receipt_No", payment.ReceiptNo, "@Amount", payment.Discount1, "@Ledger_ID", payment.DiscLedgerID2, "@RecDate", payment.RecDate, "@DisType", payment.Discount2);
            }
            else
            {
                obj.InsertPaymentReceived(payment);

                dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "ProCustLedgerEntry", ref op, "@Cust_Name", payment.CustomerName, "@City", payment.City, "@Amount", payment.Amount, "@Rec_Acc_Type", payment.AccountType, "@Receipt", payment.Receipt, "@Receipt_No", payment.ReceiptNo, "@ActualAmount", payment.ReceivedAmount, "@RecDate", payment.RecDate);
                if (payment.Discount1 != "" && payment.Discount1 != "0")
                    dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "ProSpacialDiscountEntry", ref op, "@Cust_ID", payment.Cust_ID, "@Receipt", "Save", "@Receipt_No", payment.ReceiptNo, "@Amount", payment.Discount1, "@Ledger_ID", payment.DiscLedgerID1, "@RecDate", payment.RecDate, "@DisType", payment.DiscountType1);
                if (payment.Discount2 != "" && payment.Discount2 != "0")
                    dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "ProSpacialDiscountEntry", ref op, "@Cust_ID", payment.Cust_ID, "@Receipt", "Save", "@Receipt_No", payment.ReceiptNo, "@Amount", payment.Discount1, "@Ledger_ID", payment.DiscLedgerID2, "@RecDate", payment.RecDate, "@DisType", payment.DiscountType2);

                int x = 0;
                int Curr_Credit = 0;
                int Credit_Limit = 0;
                dbobj.ExecuteScalar("Select Cr_Limit from customer where Cust_ID = '" + payment.OldCust_ID + "'", ref Credit_Limit);
                dbobj.ExecuteScalar("Select Curr_Credit from customer where Cust_ID = '" + payment.OldCust_ID + "'", ref Curr_Credit);
                if (Curr_Credit < Credit_Limit)
                {
                    Curr_Credit = Curr_Credit + int.Parse(payment.ReceivedAmount);
                    if (@Curr_Credit >= @Credit_Limit)
                        dbobj1.Insert_or_Update("update customer set Curr_Credit = '" + Credit_Limit + "' where Cust_ID  = '" + payment.CustomerID + "'", ref x);
                    else
                        dbobj1.Insert_or_Update("update customer set Curr_Credit = '" + Curr_Credit + "' where Cust_ID  = '" + payment.CustomerID + "'", ref x);
                }
                else
                {
                    Curr_Credit = Curr_Credit + int.Parse(payment.ReceivedAmount);          //Add by vikas 12.09.09
                    Curr_Credit = Curr_Credit - int.Parse(payment.TotalRec);         //Add by vikas 12.09.09
                    dbobj1.Insert_or_Update("update customer set Cr_Limit = '" + Curr_Credit + "' where Cust_ID  = '" + payment.CustomerID + "'", ref x);
                }
            }
        }

        [HttpPost]
        [Route("api/ReceiptController/UpdateAccountsLedger")]
        public void UpdateAccountsLedger(PaymentReceiptModel payment)
        {
            SqlDataReader SqlDtr = null;
            object op = null;
            dbobj.ExecProc(DbOperations_LATEST.OprType.Update, "UpdateAccountsLedgerForCustomer", ref op, "@Ledger_ID", payment.LedgerID, "@Invoice_Date", payment.Invoice_Date);
            dbobj.SelectQuery("select cust_id from customer,ledger_master where Cust_Name=Ledger_Name and Ledger_ID='" + payment.LedgerID + "'", ref SqlDtr);
            if (SqlDtr.Read())
            {
                dbobj.ExecProc(DbOperations_LATEST.OprType.Update, "UpdateCustomerLedgerForCustomer", ref op, "@Cust_ID", SqlDtr["Cust_ID"].ToString(), "@Invoice_Date", payment.Invoice_Date);
            }
        }
        [HttpGet]
        [Route("api/ReceiptController/GetLedgerIDs")]
        public List<string> GetLedgerIDs()
        {
            SqlDataReader SqlDtr = null;
            List<string> UpdateLedgerID = new List<string>();
            dbobj.SelectQuery("select Ledger_ID from Ledger_Master lm, Ledger_Master_sub_grp lmsg where lm.sub_grp_id = lmsg.sub_grp_id and  lmsg.sub_grp_name = 'Cash in hand'", ref SqlDtr);
            if (SqlDtr.Read())
            {
                UpdateLedgerID.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            return UpdateLedgerID;
        }

        [HttpPost]
        [Route("api/ReceiptController/GetReceiptNos")]
        public List<string> GetReceiptNos(PaymentReceiptModel payment)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr = null;
            string sql = "select distinct Receipt_No from Payment_Receipt where cast(floor(cast(Receipt_Date as float)) as datetime) >= '" + payment.ReceiptFromDate + "' and cast(floor(cast(Receipt_Date as float)) as datetime) <= '" + payment.ReceiptToDate + "' order by Receipt_No";
            SqlDtr = obj.GetRecordSet(sql);
            List<string> Receipts = new List<string>();
            while (SqlDtr.Read())
            {
                Receipts.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            return Receipts;
        }
        [HttpGet]
        [Route("api/ReceiptController/GetLastID")]
        public string GetLastID()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr = null;
            string Id = null;
            string sql = "select max(Receipt_No)+1 from Payment_Receipt";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                if (SqlDtr.GetValue(0).ToString() != "" && SqlDtr.GetValue(0).ToString() != null)
                    Id = SqlDtr.GetValue(0).ToString();
                else
                    Id = "1";
            }
            SqlDtr.Close();
            return Id;
        }
        [HttpGet]
        [Route("api/ReceiptController/GetSelectedReceipt")]
        public PaymentReceiptModel GetSelectedReceipt(string ReceiptNo)
        {
            PaymentReceiptModel receipt = new PaymentReceiptModel();
            InventoryClass obj = new InventoryClass();
            InventoryClass obj1 = new InventoryClass();
            SqlDataReader SqlDtr;

            string sql;
            sql = "select * from payment_receipt where Receipt_No=" + ReceiptNo;
            SqlDtr = obj.GetRecordSet(sql);
            SqlDataReader rdr = null;
            double totdisc = 0;
            //bool Flag=true;

            //*****while(SqlDtr.Read())
            if (SqlDtr.Read())
            {
                receipt.SubReceiptNo = SqlDtr["SubReceiptNo"].ToString();

                receipt.InvoiceNo = SqlDtr.GetValue(3).ToString();
                receipt.Mode = SqlDtr.GetValue(8).ToString();
                if (SqlDtr.GetValue(5).ToString() != "")
                {
                    dbobj.SelectQuery("select Ledger_Name,Ledger_ID from Ledger_Master where Ledger_ID='" + SqlDtr.GetValue(5).ToString() + "'", ref rdr);
                    if (rdr.Read())
                    {
                        receipt.BankName = rdr["Ledger_Name"].ToString();
                        receipt.LedgerID = rdr["Ledger_ID"].ToString();
                    }
                    rdr.Close();
                }
                else
                {
                    receipt.BankName = "0";
                    receipt.LedgerID = "0";
                }
                //txtBankName.Text=SqlDtr.GetValue(5).ToString();
                receipt.ChequeNumber = SqlDtr.GetValue(6).ToString();
                receipt.ChequeDate = SqlDtr.GetValue(7).ToString();
                receipt.RecDate = SqlDtr["Receipt_Date"].ToString();
                //Invoice_Date=txtReceivedDate.Text.ToString();
                receipt.Invoice_Date = SqlDtr["Receipt_Date"].ToString();
                //********
                receipt.ReceivedAmount = SqlDtr["Received_Amount"].ToString();
                if (SqlDtr["Discount1"].ToString() != "")
                    receipt.Discount1 = SqlDtr["Discount1"].ToString();
                if (SqlDtr["Discount2"].ToString() != "")
                    receipt.Discount2 = SqlDtr["Discount2"].ToString();
                //********
                //str = SqlDtr.GetValue(4).ToString();
                //str=System.Convert.ToString(double.Parse(SqlDtr.GetValue(4).ToString())-totdisc);
                //Cache["RecAmt"] = SqlDtr.GetValue(4).ToString();
                //Cache["RecAmt"]=System.Convert.ToString(double.Parse(SqlDtr.GetValue(4).ToString())-totdisc);
                receipt.Cust_ID = SqlDtr["Cust_ID"].ToString();
                receipt.CustomerID = SqlDtr["Cust_ID"].ToString();
                receipt.Narration = SqlDtr["Narration"].ToString();
                receipt.ReceivedAmount = SqlDtr["Received_Amount"].ToString();
                receipt.CustBankName = SqlDtr["CustBankName"].ToString();
                //Textbox1.Text = totdisc.ToString();

                //Tot_Rec = Convert.ToDouble(totdisc.ToString());                       //Add by vikas 12.09.09

                dbobj.SelectQuery("select Ledger_Name from Ledger_Master where Ledger_ID='" + SqlDtr["DiscLedgerID1"].ToString() + "'", ref rdr);
                if (rdr.Read())
                {
                    receipt.DiscLedgerID1 = rdr["Ledger_Name"].ToString();
                    //DiscLedgerName1 = rdr["Ledger_Name"].ToString();
                }
                else
                {
                    receipt.DiscLedgerID1 = "0";
                    //DiscLedgerName1 = "";
                }
                rdr.Close();
                dbobj.SelectQuery("select Ledger_Name from Ledger_Master where Ledger_ID='" + SqlDtr["DiscLedgerID2"].ToString() + "'", ref rdr);
                if (rdr.Read())
                {
                    receipt.DiscLedgerID2 = rdr["Ledger_Name"].ToString();
                    //DiscLedgerName2 = rdr["Ledger_Name"].ToString();
                }
                else
                {
                    receipt.DiscLedgerID2 = "0";
                    //DiscLedgerName2 = "";
                }
                rdr.Close();
                //if (DropDiscount1.SelectedIndex == 0)
                //    txtDisc1.Enabled = false;
                //else
                //    txtDisc1.Enabled = true;
                //if (DropDiscount2.SelectedIndex == 0)
                //    txtDisc2.Enabled = false;
                //else
                //    txtDisc2.Enabled = true;
                //if (double.Parse(SqlDtr["Discount1"].ToString()) > 0)
                //    TempDiscAmt1 = double.Parse(SqlDtr["Discount1"].ToString());
                //else
                //    TempDiscAmt1 = 0;
                //if (double.Parse(SqlDtr["Discount2"].ToString()) > 0)
                //    TempDiscAmt2 = double.Parse(SqlDtr["Discount2"].ToString());
                //else
                //    TempDiscAmt2 = 0;
                //txtDisc1.Text = SqlDtr["Discount1"].ToString();
                //txtDisc2.Text = SqlDtr["Discount2"].ToString();
                //DropCustName.Disabled = false;

            }
            SqlDtr.Close();
            return receipt;
        }
        [HttpGet]
        [Route("api/ReceiptController/GetCustomerPlaceByName")]
        public CustomerModels GetCustomerPlaceByName(string CustomerID)
        {
            InventoryClass obj = new InventoryClass();
            CustomerModels custmer = new CustomerModels();
            string sql = "";
            sql = "select Cust_Name,City,cust_id from Ledger_Master l,Customer c where l.ledger_name=c.cust_name and Ledger_ID='" + CustomerID + "'";
            var SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
            {
                custmer.CustomerName = SqlDtr.GetValue(0).ToString();
                if (SqlDtr.GetValue(1).ToString().Equals(""))
                {
                    custmer.City = "";
                }
                else
                {
                    custmer.City = SqlDtr.GetValue(1).ToString();
                }
                custmer.CustomerID = SqlDtr.GetValue(2).ToString();
                custmer.Flag = 1;
            }
            SqlDtr.Close();

            return custmer;
        }
        [HttpGet]
        [Route("api/ReceiptController/GetCustmerName")]
        public CustomerModels GetCustmerName(string CustomerID)
        {
            InventoryClass obj = new InventoryClass();
            CustomerModels custmer = new CustomerModels();
            string sql = "";
            sql = "select Ledger_Name,city,Ledger_ID from Ledger_Master l,Employee e where l.ledger_name=e.Emp_name and Ledger_ID='" + CustomerID + "'";
            var SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
            {
                custmer.CustomerName = SqlDtr.GetValue(0).ToString() + ":" + SqlDtr.GetValue(2).ToString();
                if (SqlDtr.GetValue(1).ToString().Equals(""))
                    custmer.City = "";
                else
                    custmer.City = SqlDtr.GetValue(1).ToString();
                custmer.Flag = 1;
            }
            SqlDtr.Close();
            return custmer;
        }
        [HttpGet]
        [Route("api/ReceiptController/GetCustomerName")]
        public string GetCustomerName(string CustomerID)
        {
            InventoryClass obj = new InventoryClass();
            string custmerName = "";
            string sql = "";
            sql = "select Ledger_Name,Ledger_ID from Ledger_Master where Ledger_ID='" + CustomerID + "'";
            var SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
            {
                custmerName = SqlDtr.GetValue(0).ToString() + ":" + SqlDtr.GetValue(1).ToString();
            }
            SqlDtr.Close();
            return custmerName;
        }


        [HttpGet]
        [Route("api/ReceiptController/GetNextReceiptNo")]
        public string GetNextReceiptNo()
        {
            SqlDataReader rdr = null;
            string ReceiptNo = "";
            dbobj.SelectQuery("select max(Receipt_No)+1 from payment_receipt", ref rdr);
            if (rdr.Read())
            {
                if (rdr.GetValue(0).ToString() != null && rdr.GetValue(0).ToString() != "")
                    ReceiptNo = rdr.GetValue(0).ToString();
                else
                    ReceiptNo = "1001";
            }
            return ReceiptNo;
        }
        [HttpGet]
        [Route("api/ReceiptController/GetBank")]
        public List<string> GetBank()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader rdr;
            List<string> Banks = new List<string>();
            string str = "select Ledger_Name from Ledger_Master where sub_grp_id='117' or sub_grp_id='126' or sub_grp_id='127' order by Ledger_Name";
            rdr = obj.GetRecordSet(str);
            while (rdr.Read())
            {
                Banks.Add(rdr.GetValue(0).ToString());
            }
            rdr.Close();
            return Banks;
        }
        [HttpPost]
        [Route("api/ReceiptController/DeleteReceipt")]
        public string DeleteReceipt(PaymentReceiptModel payment)
        {
            int x = 0, Cust_ID = 0;
            object obj = null;
            dbobj.Insert_or_Update("delete from AccountsLedgerTable where particulars = 'Receipt (" + payment.ReceiptNo + ")'", ref x);
            dbobj1.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Receipt_" + payment.DiscountType1 + " (" + payment.ReceiptNo + ")'", ref x);
            dbobj1.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Receipt_" + payment.DiscountType2 + " (" + payment.ReceiptNo + ")'", ref x);
            dbobj.Insert_or_Update("delete from CustomerLedgerTable where particular = 'Payment Received(" + payment.ReceiptNo + ")'", ref x);
            dbobj.Insert_or_Update("delete from Payment_Receipt where Receipt_No='" + payment.ReceiptNo + "'", ref x);
            dbobj.Insert_or_Update("insert into payment_receipt values(" + payment.ReceiptNo + ",'Deleted','" + payment.Invoice_Date + "','','','','','','','','','','','','','')", ref x);

            dbobj.ExecuteScalar("select cust_id from customer where cust_name=(select ledger_name from ledger_master where ledger_id = '" + payment.CustomerID + "')", ref Cust_ID);
            dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "UpdateAccountsLedgerForCustomer", ref obj, "@Ledger_ID", payment.CustomerID, "@Invoice_Date", payment.RecDate);
            dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "UpdateCustomerLedgerForCustomer", ref obj, "@Cust_ID", Cust_ID, "@Invoice_Date", payment.RecDate);
            string message = "Receipt Cancellation Successfully";
            return message;
        }
        [HttpGet]
        [Route("api/ReceiptController/GetInfo")]
        public void GetInfo(string CustomerName)
        {
            string Cust_ID = "";
            InventoryClass obj = new InventoryClass();
            InventoryClass obj1 = new InventoryClass();
            SqlDataReader rdr = obj.GetRecordSet("select Cust_ID from Customer where Cust_Name='" + CustomerName + "'");
            if (rdr.Read())
            {
                Cust_ID = rdr["Cust_ID"].ToString();
            }
            rdr.Close();
            object ob = null;
            if (Cust_ID != "")
                dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "ProInsertLedgerDetails", ref ob, "@Cust_ID", Cust_ID);
        }
    }
}
