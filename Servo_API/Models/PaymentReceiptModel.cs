using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servo_API.Models
{
    public class PaymentReceiptModel
    {
        public string LedgerID { get; set; }
        public bool PanReceiptNo { get; set; }
        public string InvoiceNo { get; set; }
        public string ReceiptNo { get; set; }
        public string Receipt { get; set; }

        public string CustomerName { get; set; }
        public string City { get; set; }
        public string ActualAmount { get; set; }
        public string CustomerID { get; set; }
        public string SubReceiptNo { get; set; }
        public string CustomerID2 { get; set; }

        public string Discount1 { get; set; }
        public string Discount2 { get; set; }

        public string DiscountType1 { get; set; }
        public string DiscountType2 { get; set; }

        public string DiscountID1 { get; set; }
        public string DiscountID2 { get; set; }

        public double Amount { get; set; }
        public string ReceivedAmount { get; set; }
        public string TotalRec { get; set; }
        public string AccountType { get; set; }
        public string BankName { get; set; }
        public string ChequeDate { get; set; }
        public string ChequeNumber { get; set; }
        public string Mode { get; set; }
        public string Narration { get; set; }
        public string CustBankName { get; set; }
        public string RecDate { get; set; }

        public string Cust_ID { get; set; }
        public string OldCust_ID { get; set; }
        public string ReceiptFromDate { get; set; }
        public string ReceiptToDate { get; set; }
        public string Invoice_Date { get; set; }
        public string LedgerName { get; set; }
        public string DiscLedgerID1 { get; set; }
        public string DiscLedgerID2 { get; set; }
    }
}