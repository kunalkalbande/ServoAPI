using Servo_API.App_Start;
using Servo_API.Models;
using Servosms.Sysitem.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
        [Route("api/payment/btnEdit1_Click")]
        public List<string> btnEdit1_Click()
        {
            List<string> LedgerName = new List<string>();
            string sql = "select Ledger_Name+';'+cast(Ledger_ID_Dr as varchar)+':'+cast(voucher_id as varchar) from Payment_transaction pt, Ledger_Master lm where pt.Ledger_ID_Dr = lm.Ledger_ID  order by Voucher_id";
            var SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                LedgerName.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            return LedgerName;
        }

        [HttpGet]
        [Route("api/payment/DropLedgerName1_SelectedIndexChanged")]

        public PaymentModels DropLedgerName1_SelectedIndexChanged(string VoucherId)
        {
            SqlDataReader SqlDtr, SqlDtr1;
            SqlCommand SqlCmd;
            string sql, sql2;
            string str = "";
            //PaymentModels payment = new PaymentModels();
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
            return payment;
        }

        [HttpGet]
        [Route("api/payment/btnDelete_Click")]

        public PaymentModels btnDelete_Click(string CustName, string VoucherId)
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
            return payment;
        }

        [HttpPost]
        [Route("api/payment/btnSave_Click")]

        public PaymentModels btnSave_Click(PaymentModels payment)
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
            return payment;
        }

        [HttpGet]
        [Route("api/payment/btnEdit_Click")]

        public string btnEdit_Click(string Ledger_Name1)
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
            return pay;
        }

        [HttpGet]
        [Route("api/payment/btnEdit_Click2")]

        public PaymentModels btnEdit_Click2(string Ledger_ID1, string OldLedger_ID)
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
            return payment;
        }

        [HttpPost]
        [Route("api/payment/btnEdit_Click3")]

        public PaymentModels btnEdit_Click3(PaymentModels payment)
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
            return payment;
        }

        [HttpPost]
        [Route("api/payment/btnEdit_Click4")]

        public PaymentModels btnEdit_Click4(PaymentModels payment)
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
            return payment;
        }
    }
}