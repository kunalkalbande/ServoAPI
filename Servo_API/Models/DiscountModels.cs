using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servo_API.Models
{
    public class DiscountModels
    {
        public string FixedStatus { get; set; }
        public string FixedDis { get; set; }
        public string ServoStatus { get; set; }
        public string ServoStk { get; set; }
        public string CashDisPurchaseStatus { get; set; }
        public string CashDisPurchase { get; set; }
        public string CashDisLtrPurchase { get; set; }
        public string DiscountPurchaseStatus { get; set; }
        public string DiscountPurchase { get; set; }
        public string DisLtrPurchase { get; set; }
        public string CashDisSalesStatus { get; set; }
        public string CashDisSales { get; set; }
        public string CashDisLtrSales { get; set; }
        public string DiscountSalesStatus { get; set; }
        public string DiscountSales { get; set; }
        public string DisLtrSales { get; set; }
        public string EarlyStatus { get; set; }
        public string EarlyBird { get; set; }
        public string EarlyDisLtrPurchase { get; set; }
        public string EarlyBird_Period { get; set; }
    }
}