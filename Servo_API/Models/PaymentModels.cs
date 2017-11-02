using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servo_API.Models
{
    public class PaymentModels
    {
        public string tempPaymentID { get; set; }
        public string txtBankname { get; set; }
        public string txtCheque { get; set; }
        public string txtchkDate { get; set; }
        public string txtAmount { get; set; }
        public string txtNarrartion { get; set; }
        public string txtDate { get; set; }
        public string Invoice_Date { get; set; }
        public string LedgerID { get; set; }
        public string LedgerID1 { get; set; }
        public string CheckCashMode { get; set; }
        public string CustId { get; set; }
        public string Entry_Date { get; set; }
        public string Ledger_Name { get; set; }
        public string By_Name { get; set; }
        public string Bank_name { get; set; }
        public string Cheque_No { get; set; }
        public string dtDate { get; set; }
        public string Amount { get; set; }
        public string narration { get; set; }
        public string Ledger_ID { get; set; }
        public string uid { get; set; }

        public int c { get; set; }
        public string VoucherId { get; set; }

        public string Ledger_ID1 { get; set; }
        public string OldLedger_ID { get; set; }
        public string Amount1 { get; set; }
        public string By_ID1 { get; set; }
        public string Bank_name1 { get; set; }
        public string Cheque_No1 { get; set; }
        public string narration1 { get; set; }
        public string DropBy { get; set; }
        public string Vouch_ID { get; set; }
        public string OldCust_ID { get; set; }
        public List<string> DropBy1 { get; set; }
        public string strCash { get; set; }

        public string addr { get; set; }
        public string city { get; set; }
        public string BalType { get; set; }
        public double Bal { get; set; }
        public string Particulars { get; set; }
        public double Bal1 { get; set; }

    }
}