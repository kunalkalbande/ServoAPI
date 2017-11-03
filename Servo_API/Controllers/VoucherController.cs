using Servo_API.App_Start;
using Servo_API.Models;
using Servosms.Sysitem.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servo_API.Controllers
{
    public class VoucherController : ApiController
    {
        App_Start.DbOperations_LATEST.DBUtil dbobj = new App_Start.DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
        App_Start.DbOperations_LATEST.DBUtil dbobj1 = new App_Start.DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);

        [HttpGet]
        [Route("api/VoucherController/GetVoucherTypeInfo")]
        public VoucherModel GetVoucherTypeInfo()
        {
            VoucherModel voucher = new VoucherModel();
            SqlDataReader SqlDtr = null;
            voucher.VoucherID = 0;
            dbobj.SelectQuery("Select max(Voucher_ID) from Voucher_Transaction where Voucher_Type ='Contra'", ref SqlDtr);
            if (SqlDtr.Read())
            {
                voucher.Contra = SqlDtr.GetValue(0).ToString();
            }
            SqlDtr.Close();

            dbobj.SelectQuery("Select max(Voucher_ID) from Voucher_Transaction where Voucher_Type ='Credit Note'", ref SqlDtr);
            if (SqlDtr.Read())
            {
                voucher.Credit = SqlDtr.GetValue(0).ToString();
            }
            SqlDtr.Close();

            dbobj.SelectQuery("Select max(Voucher_ID) from Voucher_Transaction where Voucher_Type ='Debit Note'", ref SqlDtr);
            if (SqlDtr.Read())
            {
                voucher.Debit = SqlDtr.GetValue(0).ToString();
            }
            SqlDtr.Close();

            dbobj.SelectQuery("Select max(Voucher_ID) from Voucher_Transaction where Voucher_Type ='Journal'", ref SqlDtr);
            if (SqlDtr.Read())
            {
                voucher.Journal = SqlDtr.GetValue(0).ToString();
            }
            SqlDtr.Close();
            return voucher;
        }

        [HttpGet]
        [Route("api/VoucherController/GetLedgerName")]
        public VoucherModel GetLedgerName()
        {
            VoucherModel voucher = new VoucherModel();
            voucher.CustomerName = new System.Collections.ArrayList();
            SqlDataReader SqlDtr = null;
            dbobj.SelectQuery("select lm.Ledger_name+':'+cast(lm.Ledger_ID as varchar),lmsg.sub_grp_name from Ledger_Master lm,Ledger_Master_Sub_grp lmsg where lm.Sub_grp_ID = lmsg.Sub_grp_ID order by lm.Ledger_Name", ref SqlDtr);
            
            while (SqlDtr.Read())
            {
                string subgrpname = SqlDtr.GetValue(1).ToString();

                if (subgrpname.Trim().StartsWith("Cash") || subgrpname.Trim().StartsWith("Bank"))
                {
                    voucher.Contra = voucher.Contra + SqlDtr.GetValue(0).ToString() + "~";
                }
            }
            SqlDtr.Close();

            dbobj.SelectQuery("select lm.Ledger_name+':'+cast(lm.Ledger_ID as varchar)+':'+c.City,lmsg.sub_grp_name from Ledger_Master lm,Ledger_Master_Sub_grp lmsg,customer c where lm.Sub_grp_ID = lmsg.Sub_grp_ID and c.cust_name=lm.Ledger_name order by lm.Ledger_Name", ref SqlDtr);
            while (SqlDtr.Read())
            {
                string subgrpname = SqlDtr.GetValue(1).ToString();

                if (subgrpname.Trim().StartsWith("Cash") || subgrpname.Trim().StartsWith("Bank"))
                {
                    voucher.Debit = voucher.Debit + SqlDtr.GetValue(0).ToString() + "~";
                    voucher.Credit = voucher.Credit + SqlDtr.GetValue(0).ToString() + "~";
                }
                else
                {
                    voucher.Journal = voucher.Journal + SqlDtr.GetValue(0).ToString() + "~";
                    string s = SqlDtr.GetValue(0).ToString();
                    voucher.CustomerName.Add(SqlDtr.GetValue(0).ToString());                         
                }
            }
            SqlDtr.Close();

            dbobj.SelectQuery("Select Ledger_Name+':'+cast(Ledger_ID as varchar) Ledger_Name from Ledger_Master lm,Ledger_master_sub_grp lmsg where lm.sub_grp_id = lmsg.sub_grp_id and lmsg.sub_grp_name not like 'Bank%'  and lmsg.sub_grp_name != 'Cash in hand' and lmsg.sub_grp_name not like 'Discount%' and lmsg.sub_grp_name != 'Sundry Debtors' Order by Ledger_Name", ref SqlDtr);
            while (SqlDtr.Read())
            {
                voucher.CustomerName.Add(SqlDtr.GetValue(0).ToString());                       
            }
            SqlDtr.Close();

            dbobj.SelectQuery("Select Ledger_Name+':'+cast(Ledger_ID as varchar) Ledger_Name,city from Ledger_Master, Employee where Emp_Name=Ledger_Name Order by Ledger_Name", ref SqlDtr);
            while (SqlDtr.Read())
            {
                voucher.CustomerName.Add(SqlDtr.GetValue(0).ToString());                         
            }
            SqlDtr.Close();

            dbobj.SelectQuery("Select Ledger_Name+':'+cast(Ledger_ID as varchar) Ledger_Name from Ledger_Master lm,Ledger_master_sub_grp lmsg where lm.sub_grp_id = lmsg.sub_grp_id  and lmsg.sub_grp_name like 'Discount%' Order by Ledger_Name", ref SqlDtr);
            while (SqlDtr.Read())
            {
                voucher.CustomerName.Add(SqlDtr.GetValue(0).ToString());                         
            }
            SqlDtr.Close();

            return voucher;
        }

        [HttpGet]
        [Route("api/VoucherController/GetLedgerNames")]
        public string GetLedgerNames(string VoucherType)
        {
            SqlDataReader SqlDtr = null;
            VoucherModel voucher = new VoucherModel();
            string texthiddenprod = null;
            if (VoucherType.Equals("Contra"))
            {
                dbobj.SelectQuery("select lm.Ledger_name+':'+cast(lm.Ledger_ID as varchar),lmsg.sub_grp_name from Ledger_Master lm,Ledger_Master_Sub_grp lmsg where lm.Sub_grp_ID = lmsg.Sub_grp_ID order by lm.Ledger_Name ", ref SqlDtr);
                texthiddenprod = "Select,";
                while (SqlDtr.Read())
                {
                    string subgrpname = SqlDtr.GetValue(1).ToString();
                    if (VoucherType.Equals("Contra"))
                    {
                        if (subgrpname.Trim().StartsWith("Cash") || subgrpname.Trim().StartsWith("Bank"))
                        {
                            texthiddenprod += SqlDtr.GetValue(0).ToString() + ",";
                        }
                    }
                }
                SqlDtr.Close();
            }
            else
            {

                dbobj.SelectQuery("select lm.Ledger_name+':'+cast(lm.Ledger_ID as varchar)+':'+c.City,lmsg.sub_grp_name from Ledger_Master lm,Ledger_Master_Sub_grp lmsg,customer c where lm.Sub_grp_ID = lmsg.Sub_grp_ID and c.cust_name=lm.Ledger_name order by lm.Ledger_Name", ref SqlDtr);
                texthiddenprod = "Select,";
                while (SqlDtr.Read())
                {
                    string subgrpname = SqlDtr.GetValue(1).ToString();
                    if (VoucherType.Equals("Journal"))
                    {
                        if (!subgrpname.Trim().StartsWith("Cash") && !subgrpname.Trim().StartsWith("Bank"))
                        {
                            texthiddenprod += SqlDtr.GetValue(0).ToString() + ",";
                        }
                    }
                    if (VoucherType.Equals("Credit Note") || VoucherType.Equals("Debit Note"))
                    {
                        texthiddenprod += SqlDtr.GetValue(0).ToString() + ",";
                    }
                }
                SqlDtr.Close();

                dbobj.SelectQuery("select lm.Ledger_name+':'+cast(lm.Ledger_ID as varchar),lmsg.sub_grp_name from Ledger_Master lm,Ledger_Master_Sub_grp lmsg where lm.Sub_grp_ID = lmsg.Sub_grp_ID order by Ledger_name", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    string subgrpname = SqlDtr.GetValue(1).ToString();
                    if (VoucherType.Equals("Journal"))
                    {
                        if (!subgrpname.Trim().StartsWith("Cash") && !subgrpname.Trim().StartsWith("Bank"))
                        {
                            texthiddenprod += SqlDtr.GetValue(0).ToString() + ",";
                        }
                    }
                    if (VoucherType.Equals("Credit Note") || VoucherType.Equals("Debit Note"))
                    {
                        texthiddenprod += SqlDtr.GetValue(0).ToString() + ",";
                    }
                }
                SqlDtr.Close();
            }

            return texthiddenprod;
        }
        [HttpGet]
        [Route("api/VoucherController/GetVoucherId")]
        public IHttpActionResult GetVoucherId(string VoucherName)
        {
            SqlDataReader SqlDtr = null;
            List<string> VoucherIDs = new List<string>();
            dbobj.SelectQuery("select voucher_id from voucher_transaction where voucher_type != 'Payment' and voucher_type='" + VoucherName + "' order by Voucher_ID,Voucher_type", ref SqlDtr);
            while (SqlDtr.Read())
            {
                VoucherIDs.Add(SqlDtr["voucher_id"].ToString());
            }
            SqlDtr.Close();
            if (VoucherIDs == null || VoucherIDs.Count == 0)
                return Content(HttpStatusCode.NotFound, "Voucher Ids Not found");
            return Ok(VoucherIDs);
        }
        [HttpPost]
        [Route("api/VoucherController/InsertVoucher")]
        public IHttpActionResult InsertVoucher(VoucherModel Voucher)
        {
            int c = 0;
            dbobj.Insert_or_Update("Insert into Voucher_Transaction values(" + Voucher.VoucherID + ",'" + Voucher.VoucherType + "',Convert(datetime,'" + Voucher.VoucherDate + "',103)," + Voucher.LedgerIDCr + "," + Voucher.Amount1 + "," + Voucher.LedgerIDDr + "," + Voucher.Amount2 + ",'" + Voucher.Narration + "','" + Voucher.LType + "')", ref c);
            object obj = null;
            dbobj.ExecProc(App_Start.DbOperations_LATEST.OprType.Insert, "ProInsertAccountsLedger", ref obj, "@Ledger_ID", Voucher.LedgerIDDr, "@Particulars", Voucher.VoucherType + " (" + Voucher.VoucherID + ")", "@Debit_Amount", Voucher.Amount2, "@Credit_Amount", "0.0", "@type", "Dr", "@Invoice_Date", Convert.ToDateTime(Voucher.InvoiceDate));
            dbobj.ExecProc(App_Start.DbOperations_LATEST.OprType.Insert, "ProInsertAccountsLedger", ref obj, "@Ledger_ID", Voucher.LedgerIDCr, "@Particulars", Voucher.VoucherType + " (" + Voucher.VoucherID + ")", "@Debit_Amount", "0.0", "@Credit_Amount", Voucher.Amount1, "@type", "Cr", "@Invoice_Date", Convert.ToDateTime(Voucher.InvoiceDate));
            dbobj.ExecProc(App_Start.DbOperations_LATEST.OprType.Insert, "ProCustomerLedgerEntry", ref obj, "@Voucher_ID", Voucher.VoucherID, "@Ledger_ID", Voucher.LedgerIDDr, "@Amount", Voucher.Amount2, "@Type", "Dr.", "@Invoice_Date", Convert.ToDateTime(Voucher.InvoiceDate));
            dbobj.ExecProc(App_Start.DbOperations_LATEST.OprType.Insert, "ProCustomerLedgerEntry", ref obj, "@Voucher_ID", Voucher.VoucherID, "@Ledger_ID", Voucher.LedgerIDDr, "@Amount", Voucher.Amount1, "@Type", "Cr.", "@Invoice_Date", Convert.ToDateTime(Voucher.InvoiceDate));
            return Created(new Uri(Request.RequestUri + ""), "Voucher Created");
        }

        [HttpPost]
        [Route("api/VoucherController/CustomerInsertUpdate")]
        public IHttpActionResult CustomerInsertUpdate(VoucherModel Voucher)
        {
            SqlDataReader rdr = null;
            InventoryClass obj = new InventoryClass();
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            object obj1 = null;
            dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "UpdateAccountsLedgerForCustomer", ref obj1, "@Ledger_ID", Voucher.LedgerID, "@Invoice_Date", System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Voucher.InvoiceDate)));
            dbobj.SelectQuery("select cust_id from customer,ledger_master where ledger_name=cust_name and ledger_id='" + Voucher.LedgerID + "'", ref rdr);
            if (rdr.Read())
            {
                dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "UpdateCustomerLedgerForCustomer", ref obj1, "@Cust_ID", rdr["Cust_ID"].ToString(), "@Invoice_Date", System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Voucher.InvoiceDate)));
            }
            rdr.Close();
            return Created(new Uri(Request.RequestUri + ""), "CustomerInsertUpdate");
        }
        [HttpPost]
        [Route("api/VoucherController/CustomerUpdate")]
        public IHttpActionResult CustomerUpdate(VoucherModel Voucher)
        {
            SqlDataReader rdr = null;
            object obj1 = null;
            dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "UpdateAccountsLedgerForCustomer", ref obj1, "@Ledger_ID", Voucher.LedgerID, "@Invoice_Date", Voucher.InvoiceDate);
            dbobj.SelectQuery("select cust_id from customer,ledger_master where ledger_name=cust_name and ledger_id='" + Voucher.LedgerID + "'", ref rdr);
            if (rdr.Read())
            {
                string strDate = GenUtil.str2DDMMYYYY(Voucher.InvoiceDate).Trim();
                DateTime dtInvoice_Date = System.Convert.ToDateTime(strDate.ToString());
                dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "UpdateCustomerLedgerForCustomer", ref obj1, "@Cust_ID", rdr["Cust_ID"].ToString(), "@Invoice_Date", dtInvoice_Date);
            }
            rdr.Close();
            return Created(new Uri(Request.RequestUri + ""), "CustomerUpdate");
        }
        [HttpGet]
        [Route("api/VoucherController/FetchVoucherByVoucherID")]
        public IHttpActionResult FetchVoucherByVoucherID(int VoucherID)
        {
            SqlDataReader SqlDtr = null;
            SqlDataReader SqlDtr1 = null;
            SqlDataReader SqlDtr2 = null;
            VoucherModel voucher = new VoucherModel();
            voucher.LedgerIDS = new System.Collections.ArrayList();
            dbobj.SelectQuery("Select * from voucher_transaction where Voucher_ID = " + VoucherID, ref SqlDtr);
            if (SqlDtr.Read())
            {
                voucher.VoucherID = VoucherID;
                voucher.VoucherType = SqlDtr["Voucher_Type"].ToString();
                voucher.VoucherDate = SqlDtr["voucher_date"].ToString();
                voucher.InvoiceDate = SqlDtr["Voucher_Date"].ToString();
                voucher.LType = SqlDtr["L_Type"].ToString();
                if (SqlDtr["L_Type"].ToString().Equals("Cr"))
                {
                    voucher.Amount1 = SqlDtr["Amount1"].ToString();
                    voucher.Amount2 = SqlDtr["Amount2"].ToString();
                    voucher.Narration = SqlDtr["Narration"].ToString();
                    dbobj.SelectQuery("Select Ledger_Name+':'+cast(Ledger_ID as varchar) from Ledger_Master where Ledger_ID=" + SqlDtr["Ledg_ID_Cr"].ToString(), ref SqlDtr1);

                    if (SqlDtr1.Read())
                    {
                        voucher.AccName1 = SqlDtr1.GetValue(0).ToString();
                    }
                    SqlDtr1.Close();

                    dbobj.SelectQuery("Select Ledger_Name+':'+cast(Ledger_ID as varchar) from Ledger_Master where Ledger_ID=" + SqlDtr["Ledg_ID_Dr"].ToString(), ref SqlDtr2);

                    if (SqlDtr2.Read())
                    {
                        voucher.AccName5 = SqlDtr2.GetValue(0).ToString();
                    }
                    SqlDtr2.Close();
                    voucher.LedgerIDS.Add(SqlDtr["Ledg_ID_Dr"].ToString());
                    voucher.LedgerIDS.Add(SqlDtr["Ledg_ID_Cr"].ToString());
                }
                else
                {
                    voucher.Amount1 = SqlDtr["Amount2"].ToString();
                    voucher.Amount2 = SqlDtr["Amount1"].ToString();
                    voucher.Narration = SqlDtr["Narration"].ToString();
                    dbobj.SelectQuery("Select Ledger_Name+':'+cast(Ledger_ID as varchar) from Ledger_Master where Ledger_ID=" + SqlDtr["Ledg_ID_Dr"].ToString(), ref SqlDtr1);

                    if (SqlDtr1.Read())
                    {
                        voucher.AccName1 = SqlDtr1.GetValue(0).ToString();
                    }
                    SqlDtr1.Close();

                    dbobj.SelectQuery("Select Ledger_Name+':'+cast(Ledger_ID as varchar) from Ledger_Master where Ledger_ID=" + SqlDtr["Ledg_ID_Cr"].ToString(), ref SqlDtr2);

                    if (SqlDtr2.Read())
                    {
                        voucher.AccName5 = SqlDtr2.GetValue(0).ToString();
                    }
                    SqlDtr2.Close();
                    voucher.LedgerIDS.Add(SqlDtr["Ledg_ID_Dr"].ToString());
                    voucher.LedgerIDS.Add(SqlDtr["Ledg_ID_Cr"].ToString());
                }
            }
            SqlDtr.Close();
            return Ok(voucher);
        }
        [HttpPost]
        [Route("api/VoucherController/UpdateVoucherEntry")]
        public IHttpActionResult UpdateVoucherEntry(VoucherModel voucher)
        {
            int c = 0;
            dbobj.Insert_or_Update("delete from voucher_Transaction where voucher_id =" + voucher.VoucherID, ref c);
            if (voucher.VoucherType.Equals("Contra"))
                dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars ='Contra (" + voucher.VoucherID + ")'", ref c);
            else if (voucher.VoucherType.Equals("Journal"))
                dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars ='Journal (" + voucher.VoucherID + ")'", ref c);
            else if (voucher.VoucherType.Equals("Credit Note"))
                dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars ='Credit Note (" + voucher.VoucherID + ")'", ref c);
            else if (voucher.VoucherType.Equals("Debit Note"))
                dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars ='Debit Note (" + voucher.VoucherID + ")'", ref c);
            dbobj.Insert_or_Update("delete from CustomerLedgerTable where Particular ='Voucher(" + voucher.VoucherID + ")'", ref c);
            return Created(new Uri(Request.RequestUri + ""), "Updated Voucher Entry");
        }
        [HttpPost]
        [Route("api/VoucherController/UpdateVoucher")]
        public IHttpActionResult UpdateVoucher(VoucherModel voucher)
        {
            int c = 0;
            dbobj.Insert_or_Update("Update voucher_transaction set voucher_date =Convert(datetime,'" + voucher.VoucherDate + "',103),Ledg_ID_Cr =" + voucher.LedgerIDCr + ",Amount1=" + voucher.Amount1 + ",Ledg_ID_Dr=" + voucher.LedgerIDDr + ",Amount2=" + voucher.Amount2 + ",Narration='" + voucher.Narration + "',L_Type='" + voucher.LType + "' where Voucher_ID =" + voucher.VoucherID, ref c);
            object obj = null;
            dbobj.ExecProc(DbOperations_LATEST.OprType.Update, "ProUpdateAccountsLedger", ref obj, "@Voucher_ID", voucher.VoucherID, "@Ledger_ID", voucher.LedgerIDDr, "@Amount", voucher.Amount2, "@Type", "Dr", "@Invoice_Date", voucher.VoucherDate);
            dbobj.ExecProc(DbOperations_LATEST.OprType.Update, "ProUpdateAccountsLedger", ref obj, "@Voucher_ID", voucher.VoucherID, "@Ledger_ID", voucher.LedgerIDCr, "@Amount", voucher.Amount1, "@Type", "Cr", "@Invoice_Date", voucher.VoucherDate);
            if (c > 0)
                return Ok(c);
            else
                return Content(HttpStatusCode.NotFound, "Voucher is not updated");
        }
        [HttpPost]
        [Route("api/VoucherController/DeleteVoucher")]
        public IHttpActionResult DeleteVoucher(VoucherModel voucher)
        {
            int c = 0;
            dbobj.Insert_or_Update("delete from voucher_Transaction where voucher_id =" + voucher.VoucherID, ref c);
            if (voucher.VoucherType.Equals("Contra"))
                dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars ='Contra (" + voucher.VoucherID + ")'", ref c);
            else if (voucher.VoucherType.Equals("Journal"))
                dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars ='Journal (" + voucher.VoucherID + ")'", ref c);
            else if (voucher.VoucherType.Equals("Credit Note"))
                dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars ='Credit Note (" + voucher.VoucherID + ")'", ref c);
            else if (voucher.VoucherType.Equals("Debit Note"))
                dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars ='Debit Note (" + voucher.VoucherID + ")'", ref c);
            dbobj.Insert_or_Update("delete from CustomerLedgerTable where Particular ='Voucher(" + voucher.VoucherID + ")'", ref c);
            if (c > 0)
                return Ok(c);
            else
                return Content(HttpStatusCode.NotFound, "Voucher is not deleted");
        }
        [HttpGet]
        [Route("api/VoucherController/Report")]
        public IHttpActionResult Report(string VoucherName)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader rdr = null;
            CustomerModels customer = new CustomerModels();
            string str = "select address,city from customer,ledger_master where ledger_name=cust_name and ledger_id='" + VoucherName + "'";
            rdr = obj.GetRecordSet(str);
            if (rdr.Read())
            {
                customer.Address = rdr.GetValue(0).ToString();
                customer.City = rdr.GetValue(1).ToString();
            }
            rdr.Close();
            return Ok(customer);
        }
    }
}
