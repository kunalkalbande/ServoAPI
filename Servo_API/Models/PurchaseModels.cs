using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servo_API.Models
{
    public class PurchaseModels
    {
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string ModeofPayment { get; set; }
        public string VendorName { get; set; }
        public string Tin_No { get; set; }
        public string VendorID { get; set; }
        public string City { get; set; }
        public string VehicleNo { get; set; }
        public string VendorInvoiceNo { get; set; }
        public string VendorInvoiceDate { get; set; }
        public string GrandTotal { get; set; }
        public string Discount { get; set; }
        public string DiscountType { get; set; }
        public string NetAmount { get; set; }
        public string PromoScheme { get; set; }
        public string Remark { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryTime { get; set; }
        public string CashDiscount { get; set; }
        public string CashDiscType { get; set; }
        public string VATAmount { get; set; }
        public string SGSTAmount { get; set; }
        public string CGSTAmount { get; set; }
        public string FixedDiscount { get; set; }
        public string FixedDiscountType { get; set; }
        public string FocDiscount { get; set; }
        public string FocDiscountType { get; set; }
        public string Ebird { get; set; }
        public string Tradeless { get; set; }
        public string Birdless { get; set; }
        public string EbirdDiscount { get; set; }
        public string Tradeval { get; set; }
        public string TradeDiscount { get; set; }
        public string Totalqtyltr { get; set; }
        public string Totalqtyltr1 { get; set; }
        public string NewFixeddisc { get; set; }
        public string NewFixeddiscAmount { get; set; }
        public string TradedisAmount { get; set; }
        public string EbirdAmount { get; set; }
        public string Fixed { get; set; }
        public string FixedAmount { get; set; }
        public string Place { get; set; }
    }
}