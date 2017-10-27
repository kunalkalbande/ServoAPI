using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servo_API.Models
{
    public class ProductModels
    {
        public string Invoice_No { get; set; }
        public int Sno { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductType { get; set; }
        public int ProductCount { get; set; }
        public string ProductPack { get; set; }

        public string Category { get; set; }
        public string Package_Type { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public string Quantity { get; set; }
        public string QtyTemp { get; set; }
        public string SchPerDisc { get; set; }
        public string SchPerDiscType { get; set; }
        public string StockDiscount { get; set; }
        public string SchStktDiscType { get; set; }
        public double StockDiscountAmount { get; set; }
        public bool CheckFOC { get; set; }
        public bool CheckSchDisc { get; set; }
        public int FOCDisc { get; set; }
        public string FOC { get; set; }
        public string DropFOCType { get; set; }
        public int ETFOC { get; set; }
        public int TradeLess { get; set; }
        public string EarlyDisType { get; set; }
        public int EBird { get; set; }
        public int BirdLess { get; set; }
        public int QuantityPack { get; set; }
        public string DropDiscType { get; set; }
        public string CashDiscount { get; set; }
        public string Discount { get; set; }
        public string SchAdditionalDiscount { get; set; }
        public string SchDiscount { get; set; }
        public string SchDis { get; set; }
        public int TotalFixedDiscount { get; set; }
        public string FixedDisc { get; set; }
        public int TradeDiscount { get; set; }
        public string DropCashDiscType { get; set; }
        public double CGST { get; set; }
        public double SGST { get; set; }
        public double IGST { get; set; }
        public string Unit { get; set; }
        public DateTime InvoiceDate { get; set; }

        public string Batch { get; set; }
        public string TempStktSchDis { get; set; }
        public string TempSchDis { get; set; }
        public string TempSchAddDis { get; set; }
        public string TempDiscount { get; set; }
    }
}