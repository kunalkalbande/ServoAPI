using Servo_API.App_Start;
using Servo_API.Models;
using Servosms.Sysitem.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Servo_API.Controllers
{
    public class PaymentController : ApiController
    {
        public SqlConnection SqlCon { get; private set; }

        InventoryClass obj = new InventoryClass();
        PaymentModels payment = new PaymentModels();
        DbOperations_LATEST.DBUtil dbobj = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);


        [HttpGet]
        [Route("api/payment/FillLedgerName")]
        public IHttpActionResult FillLedgerName()
        {
            List<string> LedgerName = new List<string>();
            string sql = "select Ledger_Name+';'+cast(Ledger_ID_Dr as varchar)+':'+cast(voucher_id as varchar) from Payment_transaction pt, Ledger_Master lm where pt.Ledger_ID_Dr = lm.Ledger_ID  order by Voucher_id";
            var SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                LedgerName.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            if (LedgerName.Count == 0 || LedgerName == null)
            {
                return Content(HttpStatusCode.NotFound, "Ledger Name Not Found");
            }
            return Ok(LedgerName);
        }

        [HttpGet]
        [Route("api/payment/LedgerName_SelectedIndexChanged")]

        public IHttpActionResult LedgerName_SelectedIndexChanged(string VoucherId)
        {
            SqlDataReader SqlDtr, SqlDtr1;
            SqlCommand SqlCmd;
            string sql, sql2;                       
            using (SqlCon = new SqlConnection())
            {

                SqlCon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ServoSMS"]);
                SqlCon.Open();
                sql = "Select * from payment_transaction where voucher_Id ='" + VoucherId + "' ";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    payment.tempPaymentID = SqlDtr.GetValue(0).ToString();
                    payment.txtBankname = SqlDtr["Bank_Name"].ToString().Trim();
                    payment.txtCheque = SqlDtr["Cheque_No"].ToString().Trim();
                    payment.txtchkDate = obj.checkDate(GenUtil.str2DDMMYYYY(obj.trimDate(SqlDtr["cheque_date"].ToString().Trim())));
                    payment.txtAmount = SqlDtr["Amount1"].ToString().Trim();
                    payment.txtNarrartion = SqlDtr["Narration"].ToString().Trim();
                    payment.txtDate = GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["Entry_Date"].ToString()));
                    payment.Invoice_Date = SqlDtr["Entry_Date"].ToString();
                    payment.LedgerID = SqlDtr["Ledger_ID_Dr"].ToString();
                    payment.LedgerID1 = SqlDtr["Ledger_ID_Cr"].ToString();
                    sql2 = "Select Ledger_Name from Ledger_Master where Ledger_ID = " + SqlDtr["Ledger_ID_Cr"].ToString().Trim();
                    SqlCmd = new SqlCommand(sql2, SqlCon);
                    SqlDtr1 = SqlCmd.ExecuteReader();
                    if (SqlDtr1.Read())
                    {
                        payment.CheckCashMode = SqlDtr1["Ledger_Name"].ToString();
                    }
                    SqlDtr1.Close();
                }
                SqlDtr.Close();
            }
            if (payment.Equals(0) || payment == null)
            {
                return Content(HttpStatusCode.NotFound, "Ledger Name Not Found");
            }
            return Ok(payment);
        }

        [HttpGet]
        [Route("api/payment/DeletePayment")]

        public IHttpActionResult DeletePayment(string CustName, string VoucherId)
        {
            SqlDataReader SqlDtr;
            string sql = "select Cust_id from Customer where Cust_Name='" + CustName + "' ";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                payment.CustId = SqlDtr.GetValue(0).ToString();
            }
            SqlDtr.Close();
            sql = "delete from payment_transaction where voucher_id = '" + VoucherId + "'";
            SqlDtr = obj.GetRecordSet(sql);
            SqlDtr.Close();
            sql = "delete from AccountsLedgerTable where Particulars = 'Payment (" + VoucherId + ")'";
            SqlDtr = obj.GetRecordSet(sql);
            SqlDtr.Close();
            if (payment.CustId != "")
            {
                sql = "delete from CustomerLedgerTable where Particular = 'Voucher(" + VoucherId + ")'";
            }
            if (payment.Equals(0) || payment == null)
            {
                return Content(HttpStatusCode.NotFound, "Payment Not Deleted");
            }
            return Ok(payment);
        }

        [HttpPost]
        [Route("api/payment/SavePayment")]

        public IHttpActionResult SavePayment(PaymentModels payment)
        {
            SqlDataReader SqlDtr;
            string Vouch_ID;
            string By_ID = "";
            string sql = "Select Ledger_ID from Ledger_Master where Ledger_Name ='" + payment.By_Name + "'";
            SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
            {
                By_ID = SqlDtr.GetValue(0).ToString();
            }
            SqlDtr.Close();

            sql = "Select top 1(voucher_ID + 1)  from Payment_Transaction order by voucher_ID desc";
            SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
            {
                Vouch_ID = SqlDtr.GetValue(0).ToString();
            }
            else
            {
                Vouch_ID = "50001";
            }
            SqlDtr.Close();
            payment.c = 0;
            sql = "insert into payment_transaction values(" + Vouch_ID + ",'Payment'," + payment.Ledger_ID + "," + payment.Amount + "," + By_ID + "," + payment.Amount + ",'" + payment.Bank_name + "','" + payment.Cheque_No + "',CONVERT(datetime,'" + payment.dtDate + "', 103),'" + payment.narration + "','" + payment.uid + "', CONVERT(datetime,'" + payment.Entry_Date + "', 103))";
            SqlDtr = obj.GetRecordSet(sql);
            payment.c = 1;
            object obj1 = null;
            dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "ProInsertAccountsLedger", ref obj1, "@Ledger_ID", payment.Ledger_ID, "@Particulars", "Payment (" + Vouch_ID + ")", "@Debit_Amount", payment.Amount, "@Credit_Amount", "0.0", "@type", "Dr", "@Invoice_Date", System.Convert.ToDateTime(payment.Entry_Date));
            dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "ProInsertAccountsLedger", ref obj1, "@Ledger_ID", By_ID, "@Particulars", "Payment (" + Vouch_ID + ")", "@Debit_Amount", "0.0", "@Credit_Amount", payment.Amount, "@type", "Cr", "@Invoice_Date", System.Convert.ToDateTime(payment.Entry_Date));
            dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "ProCustomerLedgerEntry", ref obj1, "@Voucher_ID", Vouch_ID, "@Ledger_ID", payment.Ledger_ID, "@Amount", payment.Amount, "@Type", "Dr.", "@Invoice_Date", System.Convert.ToDateTime(payment.Entry_Date));
            dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "ProCustomerLedgerEntry", ref obj1, "@Voucher_ID", Vouch_ID, "@Ledger_ID", By_ID, "@Amount", payment.Amount, "@Type", "Cr.", "@Invoice_Date", System.Convert.ToDateTime(payment.Entry_Date));
            if (payment.Equals(0) || payment == null)
            {
                return Content(HttpStatusCode.NotFound, "Payment Not Saved");
            }
            return Ok(payment);
        }

        [HttpGet]
        [Route("api/payment/SelectLedgerId")]

        public IHttpActionResult SelectLedgerId(string Ledger_Name1)
        {
            SqlDataReader SqlDtr;
            string pay = "";
            //PaymentModels payment = new PaymentModels();
            string sql = "Select Ledger_ID from Ledger_Master where Ledger_Name='" + Ledger_Name1 + "'";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                pay = SqlDtr["Ledger_ID"].ToString();
            }
            SqlDtr.Close();
            if (pay == null || pay == "")
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
            return Ok(pay);
        }

        [HttpGet]
        [Route("api/payment/SelectCustId")]

        public IHttpActionResult SelectCustId(string Ledger_ID1, string OldLedger_ID)
        {
            SqlDataReader SqlDtr;
            string sql = "select Cust_ID from Customer,Ledger_Master where Ledger_Name = Cust_Name and Ledger_ID ='" + Ledger_ID1 + "' ";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                payment.Ledger_ID1 = SqlDtr["Cust_ID"].ToString();
            }
            SqlDtr.Close();

            sql = "select Cust_ID from Customer,Ledger_Master where Ledger_Name = Cust_Name and Ledger_ID ='" + OldLedger_ID + "' ";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                payment.OldLedger_ID = SqlDtr["Cust_ID"].ToString();
            }
            SqlDtr.Close();
            if (payment.Equals(0) || payment == null)
            {
                return Content(HttpStatusCode.NotFound, "Customer ID Not Found");
            }
            return Ok(payment);
        }

        [HttpPost]
        [Route("api/payment/UpdatePayment")]

        public IHttpActionResult UpdatePayment(PaymentModels payment)
        {
            SqlDataReader SqlDtr;
            SqlCommand SqlCmd;
            SqlConnection SqlCon1 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);

            payment.c = 0;
            string sql = "Update Payment_transaction set Ledger_ID_Dr = " + payment.Ledger_ID1 + ",Amount1 = " + payment.Amount1 + ",Ledger_ID_Cr = " + payment.By_ID1 + ",Amount2 = " + payment.Amount1 + ",Bank_Name='" + payment.Bank_name1 + "',Cheque_No='" + payment.Cheque_No1 + "',Cheque_date = CONVERT(datetime,'" + payment.dtDate + "', 103),Narration ='" + payment.narration1 + "',Entered_By = '" + payment.uid + "', Entry_Date= CONVERT(datetime,'" + payment.Entry_Date + "', 103) where Voucher_ID = '" + payment.VoucherId + "'";
            SqlCmd = new SqlCommand(sql, SqlCon1);

            SqlCon1.Open();
            SqlDtr = SqlCmd.ExecuteReader();
            SqlDtr.Close();
            payment.c = 1;
            object obj1 = null;

            if (payment.CheckCashMode.Equals(payment.DropBy))
            {
                dbobj.ExecProc(DbOperations_LATEST.OprType.Update, "ProUpdateAccountsLedger", ref obj1, "@Voucher_ID", payment.VoucherId, "@Ledger_ID", payment.Ledger_ID1, "@Amount", payment.Amount1, "@Type", "Dr", "@Invoice_Date", System.Convert.ToDateTime(payment.Entry_Date));
                dbobj.ExecProc(DbOperations_LATEST.OprType.Update, "ProUpdateAccountsLedger", ref obj1, "@Voucher_ID", payment.VoucherId, "@Ledger_ID", payment.By_ID1, "@Amount", payment.Amount1, "@Type", "Cr", "@Invoice_Date", System.Convert.ToDateTime(payment.Entry_Date));

            }
            else
            {
                sql = "delete from CustomerLedgerTable where Particular = 'Voucher(" + payment.VoucherId + ")' and CustID='" + payment.OldCust_ID + "'";
                SqlCmd = new SqlCommand(sql, SqlCon1);
                SqlDtr = SqlCmd.ExecuteReader();
                SqlDtr.Close();

                sql = "delete from AccountsLedgerTable where Particulars = 'Payment (" + payment.VoucherId + ")'";
                SqlCmd = new SqlCommand(sql, SqlCon1);
                SqlDtr = SqlCmd.ExecuteReader();
                SqlDtr.Close();

                dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "ProInsertAccountsLedger", ref obj1, "@Ledger_ID", payment.Ledger_ID1, "@Particulars", "Payment (" + payment.Vouch_ID + ")", "@Debit_Amount", payment.Amount1, "@Credit_Amount", "0.0", "@type", "Dr", "@Invoice_Date", System.Convert.ToDateTime(payment.Entry_Date));
                //dbobj.ExecProc(DBOperations.OprType.Insert, "ProInsertAccountsLedger", ref obj1, "@Ledger_ID", payment.By_ID1, "@Particulars", "Payment (" + payment.Vouch_ID + ")", "@Debit_Amount", "0.0", "@Credit_Amount", payment.Amount1, "@type", "Cr", "@Invoice_Date", System.Convert.ToDateTime(payment.Entry_Date));
                dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "ProCustomerLedgerEntry", ref obj1, "@Voucher_ID", payment.Vouch_ID, "@Ledger_ID", payment.Ledger_ID1, "@Amount", payment.Amount1, "@Type", "Dr.", "@Invoice_Date", System.Convert.ToDateTime(payment.Entry_Date));
                //dbobj.ExecProc(DBOperations.OprType.Insert, "ProCustomerLedgerEntry", ref obj1, "@Voucher_ID", payment.Vouch_ID, "@Ledger_ID", payment.By_ID1, "@Amount", payment.Amount1, "@Type", "Cr.", "@Invoice_Date", System.Convert.ToDateTime(payment.Entry_Date));
            }
            if (payment.Equals(0) || payment == null)
            {
                return Content(HttpStatusCode.NotFound, "Payment Not Updated");
            }
            return Ok(payment);
        }

        [HttpPost]
        [Route("api/payment/DeleteAndUpdatePayment")]

        public IHttpActionResult DeleteAndUpdatePayment(PaymentModels payment)
        {
            SqlDataReader SqlDtr;
            string sql;
            sql = "delete from payment_transaction where voucher_id = " + payment.VoucherId;
            SqlDtr = obj.GetRecordSet(sql);
            SqlDtr.Close();

            sql = "delete from AccountsLedgerTable where Particulars = 'Payment (" + payment.VoucherId + ")'";
            SqlDtr = obj.GetRecordSet(sql);
            SqlDtr.Close();

            if (payment.OldCust_ID != "")
            {
                sql = "delete from CustomerLedgerTable where Particular = 'Voucher(" + payment.VoucherId + ")' and CustID='" + payment.OldCust_ID + "'";
                SqlDtr = obj.GetRecordSet(sql);
                SqlDtr.Close();
            }

            sql = "insert into payment_transaction values(" + payment.Vouch_ID + ",'Payment'," + payment.Ledger_ID1 + "," + payment.Amount1 + "," + payment.By_ID1 + "," + payment.Amount1 + ",'" + payment.Bank_name1 + "','" + payment.Cheque_No1 + "',CONVERT(datetime,'" + payment.dtDate + "', 103),'" + payment.narration1 + "','" + payment.uid + "',CONVERT(datetime,'" + payment.Entry_Date + "', 103))";
            SqlDtr = obj.GetRecordSet(sql);
            SqlDtr.Close();
            object obj1 = null;
            dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "ProInsertAccountsLedger", ref obj1, "@Ledger_ID", payment.Ledger_ID1, "@Particulars", "Payment (" + payment.Vouch_ID + ")", "@Debit_Amount", payment.Amount1, "@Credit_Amount", "0.0", "@type", "Dr", "@Invoice_Date", payment.Entry_Date);
            //dbobj.ExecProc(DBOperations.OprType.Insert, "ProInsertAccountsLedger", ref obj1, "@Ledger_ID", payment.By_ID1, "@Particulars", "Payment (" + payment.Vouch_ID + ")", "@Debit_Amount", "0.0", "@Credit_Amount", payment.Amount1, "@type", "Cr", "@Invoice_Date", payment.Entry_Date);
            dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "ProCustomerLedgerEntry", ref obj1, "@Voucher_ID", payment.Vouch_ID, "@Ledger_ID", payment.Ledger_ID1, "@Amount", payment.Amount1, "@Type", "Dr.", "@Invoice_Date", payment.Entry_Date);
            //dbobj.ExecProc(DBOperations.OprType.Insert, "ProCustomerLedgerEntry", ref obj1, "@Voucher_ID", payment.Vouch_ID, "@Ledger_ID", payment.By_ID1, "@Amount", payment.Amount1, "@Type", "Cr.", "@Invoice_Date", payment.Entry_Date);
            if (payment.Equals(0) || payment == null)
            {
                return Content(HttpStatusCode.NotFound, "Payment Not Updated");
            }
            return Ok(payment);
        }

        [HttpGet]
        [Route("api/payment/page_load")]

        public IHttpActionResult page_load()
        {
            SqlDataReader SqlDtr;
            string sql, str = "";
            sql = "select Acc_Date_from from Organisation";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                str = GenUtil.trimDate(SqlDtr["Acc_Date_from"].ToString());
            }
            SqlDtr.Close();
            if (str == null || str == "")
            {
                return Content(HttpStatusCode.NotFound, "Date Not Found");
            }
            return Ok(str);
        }

        [HttpGet]
        [Route("api/payment/fillCombo")]

        public IHttpActionResult fillCombo()
        {
            SqlDataReader SqlDtr;
            string sql, texthiddenprod = "";
            sql = "Select Ledger_Name,Ledger_ID from Ledger_Master lm,Ledger_master_sub_grp lmsg  where  lm.sub_grp_id = lmsg.sub_grp_id and lmsg.sub_grp_name not like 'Bank%' and lmsg.sub_grp_name <> 'Cash in hand' and lmsg.sub_grp_name <> 'Discount' Order by Ledger_Name";
            SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.HasRows)
            {
                texthiddenprod = "Select,";
                while (SqlDtr.Read())
                {
                    texthiddenprod += SqlDtr["Ledger_Name"].ToString() + ";" + SqlDtr["Ledger_ID"].ToString() + ",";
                }
            }
            SqlDtr.Close();
            if (texthiddenprod == null || texthiddenprod == "")
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
            return Ok(texthiddenprod);            
        }

        [HttpGet]
        [Route("api/payment/fillCombo2")]

        public IHttpActionResult fillCombo2()
        {
            SqlDataReader SqlDtr;
            List<string> DropBy = new List<string>();

            string sql, str, strCash;
            strCash = "";
            sql = "Select Ledger_Name,sub_grp_name from Ledger_Master lm,Ledger_master_sub_grp lmsg  where  lm.sub_grp_id = lmsg.sub_grp_id  and (sub_grp_name='Cash in hand' or sub_grp_name like'Bank%')  Order by Ledger_Name";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                str = SqlDtr["sub_grp_name"].ToString();
                if (str.Equals("Cash in hand") || str.IndexOf("Bank") > -1)
                {
                    DropBy.Add(SqlDtr["Ledger_Name"].ToString());
                    if (str.Equals("Cash in hand"))
                        strCash = SqlDtr["Ledger_Name"].ToString();
                }
            }
            SqlDtr.Close();
            payment.DropBy1 = DropBy;
            payment.strCash = strCash;
            if (payment.Equals(0) || payment == null)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
            return Ok(payment);
        }

        [HttpGet]
        [Route("api/payment/makingReport")]
        public IHttpActionResult makingReport(string str)
        {
            SqlDataReader SqlDtr;
            string sql;
            sql = "select address,city from customer,ledger_master where ledger_name=cust_name and ledger_id='" + str + "'";
            SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
            {
                payment.addr = SqlDtr.GetValue(0).ToString();
                payment.city = SqlDtr.GetValue(1).ToString();
            }
            SqlDtr.Close();
            if (payment.Equals(0) || payment == null)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
            return Ok(payment);
        }

        [HttpGet]
        [Route("api/payment/SeqCashAccount")]

        public IHttpActionResult SeqCashAccount(string Invoice_Date)
        {
            SqlDataReader SqlDtr;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            SqlCommand cmd;
            string sql;
            sql = "select * from AccountsLedgerTable where Ledger_ID=(select Ledger_ID from Ledger_Master where sub_grp_id=118) and Entry_Date>='" + Invoice_Date + "' order by entry_date";
            SqlDtr = obj.GetRecordSet(sql);


            int i = 0;
            while (SqlDtr.Read())
            {
                if (i == 0)
                {
                    payment.BalType = SqlDtr["Bal_Type"].ToString();
                    i++;
                }
                else
                {
                    if (double.Parse(SqlDtr["Credit_Amount"].ToString()) != 0)
                    {
                        if (payment.BalType == "Cr")
                        {
                            payment.Bal += double.Parse(SqlDtr["Credit_Amount"].ToString());
                            payment.BalType = "Cr";
                        }
                        else
                        {
                            payment.Bal -= double.Parse(SqlDtr["Credit_Amount"].ToString());
                            if (payment.Bal < 0)
                            {
                                payment.Bal = double.Parse(payment.Bal.ToString().Substring(1));
                                payment.BalType = "Cr";
                            }
                            else
                                payment.BalType = "Dr";
                        }
                    }
                    else if (double.Parse(SqlDtr["Debit_Amount"].ToString()) != 0)
                    {
                        if (payment.BalType == "Dr")
                            payment.Bal += double.Parse(SqlDtr["Debit_Amount"].ToString());
                        else
                        {
                            payment.Bal -= double.Parse(SqlDtr["Debit_Amount"].ToString());
                            if (payment.Bal < 0)
                            {
                                payment.Bal = double.Parse(payment.Bal.ToString().Substring(1));
                                payment.BalType = "Dr";
                            }
                            else
                                payment.BalType = "Cr";
                        }
                    }
                    Con.Open();
                    cmd = new SqlCommand("update AccountsLedgerTable set Balance='" + payment.Bal.ToString() + "',Bal_Type='" + payment.BalType + "' where Ledger_ID='" + SqlDtr["Ledger_ID"].ToString() + "' and Particulars='" + SqlDtr["Particulars"].ToString() + "' ", Con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Con.Close();
                }
            }
            SqlDtr.Close();
            if (payment.Equals(0) || payment == null)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
            return Ok(payment);
        }

        [HttpGet]
        [Route("api/payment/CustomerUpdate")]

        public IHttpActionResult CustomerUpdate(string str,string Invoice_Date)
        {
            SqlDataReader SqlDtr;
            string sql;
            object obj1 = null;
            dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "UpdateAccountsLedgerForCustomer", ref obj1, "@Ledger_ID", str, "@Invoice_Date", Invoice_Date);
            sql = "select cust_id from customer,ledger_master where ledger_name=cust_name and ledger_id='" + str + "'";
            SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
            {
                dbobj.ExecProc(DbOperations_LATEST.OprType.Insert, "UpdateCustomerLedgerForCustomer", ref obj1, "@Cust_ID", SqlDtr["Cust_ID"].ToString(), "@Invoice_Date", Convert.ToDateTime(Invoice_Date));
            }
            return Ok();
        }

        [HttpGet]
        [Route("api/payment/CustomerInsertUpdate")]

        public IHttpActionResult CustomerInsertUpdate(string Ledger_ID1,string Invoice_Date)
        {
            SqlDataReader SqlDtr=null;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            SqlCommand cmd;
            string sql,str;
            int i = 0;
            sql = "select top 1 Entry_Date from AccountsLedgerTable where Ledger_ID='" + Ledger_ID1 + "' and Entry_Date<=Convert(datetime,'" + Invoice_Date + "',103) order by entry_date desc";
            SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
            {
                var entry_date = GenUtil.str2MMDDYYYY(SqlDtr.GetValue(0).ToString());
                str = "select * from AccountsLedgerTable where Ledger_ID='" + Ledger_ID1 + "' and Entry_Date>='" + entry_date + "' order by entry_date";
            }
            else
                str = "select * from AccountsLedgerTable where Ledger_ID='" + Ledger_ID1 + "' order by entry_date";
            SqlDtr.Close();

            SqlDtr = obj.GetRecordSet(str);
            while (SqlDtr.Read())
            {
                if (i == 0)
                {
                    payment.BalType = SqlDtr["Bal_Type"].ToString();
                    payment.Bal = double.Parse(SqlDtr["Balance"].ToString());
                    i++;
                }
                else
                {
                    if (double.Parse(SqlDtr["Credit_Amount"].ToString()) != 0)
                    {
                        if (payment.BalType == "Cr")
                        {
                            string ss = SqlDtr["Credit_Amount"].ToString();
                            payment.Bal += double.Parse(SqlDtr["Credit_Amount"].ToString());
                            payment.BalType = "Cr";
                        }
                        else
                        {
                            string ss = SqlDtr["Credit_Amount"].ToString();
                            payment.Bal -= double.Parse(SqlDtr["Credit_Amount"].ToString());
                            if (payment.Bal < 0)
                            {
                                payment.Bal = double.Parse(payment.Bal.ToString().Substring(1));
                                payment.BalType = "Cr";
                            }
                            else
                                payment.BalType = "Dr";
                        }
                    }
                    else if (double.Parse(SqlDtr["Debit_Amount"].ToString()) != 0)
                    {
                        if (payment.BalType == "Dr")
                        {
                            string ss = SqlDtr["Debit_Amount"].ToString();
                            payment.Bal += double.Parse(SqlDtr["Debit_Amount"].ToString());
                        }
                        else
                        {
                            string ss = SqlDtr["Debit_Amount"].ToString();
                            payment.Bal -= double.Parse(SqlDtr["Debit_Amount"].ToString());
                            if (payment.Bal < 0)
                            {
                                payment.Bal = double.Parse(payment.Bal.ToString().Substring(1));
                                payment.BalType = "Dr";
                            }
                            else
                                payment.BalType = "Cr";
                        }
                    }
                    Con.Open();
                    string str11 = "update AccountsLedgerTable set Balance='" + payment.Bal.ToString() + "',Bal_Type='" + payment.BalType + "' where Ledger_ID='" + SqlDtr["Ledger_ID"].ToString() + "' and Particulars='" + SqlDtr["Particulars"].ToString() + "'";
                    cmd = new SqlCommand("update AccountsLedgerTable set Balance='" + payment.Bal.ToString() + "',Bal_Type='" + payment.BalType + "' where Ledger_ID='" + SqlDtr["Ledger_ID"].ToString() + "' and Particulars='" + SqlDtr["Particulars"].ToString() + "'", Con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Con.Close();
                }
            }
            SqlDtr.Close();

            sql = "select top 1 EntryDate from CustomerLedgerTable where CustID = (select Cust_ID from Customer,Ledger_Master where Ledger_Name = Cust_Name and Ledger_ID = '" + Ledger_ID1 + "') and EntryDate<= Convert(datetime, '" + Invoice_Date + "', 103) order by entrydate desc";
            SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
                str = "select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer,Ledger_Master where Ledger_Name=Cust_Name and Ledger_ID='" + Ledger_ID1 + "') and  EntryDate>=Convert(datetime,'" + SqlDtr.GetValue(0).ToString() + "',103) order by entrydate";
            else
                str = "select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer c,Ledger_Master l where Ledger_Name=Cust_Name and Ledger_ID='" + Ledger_ID1 + "') order by entrydate";
            SqlDtr.Close();

            SqlDtr = obj.GetRecordSet(str);
            while (SqlDtr.Read())
            {
                if (i == 0)
                {
                    payment.BalType = SqlDtr["BalanceType"].ToString();
                    payment.Bal = double.Parse(SqlDtr["Balance"].ToString());
                    i++;
                }
                else
                {
                    if (double.Parse(SqlDtr["CreditAmount"].ToString()) != 0)
                    {
                        if (payment.BalType == "Cr.")
                        {
                            payment.Bal += double.Parse(SqlDtr["CreditAmount"].ToString());
                            payment.BalType = "Cr.";
                        }
                        else
                        {
                            payment.Bal -= double.Parse(SqlDtr["CreditAmount"].ToString());
                            if (payment.Bal < 0)
                            {
                                payment.Bal = double.Parse(payment.Bal.ToString().Substring(1));
                                payment.BalType = "Cr.";
                            }
                            else
                                payment.BalType = "Dr.";
                        }
                    }
                    else if (double.Parse(SqlDtr["DebitAmount"].ToString()) != 0)
                    {
                        if (payment.BalType == "Dr.")
                            payment.Bal += double.Parse(SqlDtr["DebitAmount"].ToString());
                        else
                        {
                            payment.Bal -= double.Parse(SqlDtr["DebitAmount"].ToString());
                            if (payment.Bal < 0)
                            {
                                payment.Bal = double.Parse(SqlDtr.ToString().Substring(1));
                                payment.BalType = "Dr.";
                            }
                            else
                                payment.BalType = "Cr.";
                        }
                    }
                    Con.Open();
                    cmd = new SqlCommand("update CustomerLedgerTable set Balance='" + payment.Bal.ToString() + "',BalanceType='" + payment.BalType + "' where CustID='" + SqlDtr["CustID"].ToString() + "' and Particular='" + SqlDtr["Particular"].ToString() + "'", Con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Con.Close();
                }
            }
            SqlDtr.Close();
            return Ok();
        }
    }
}