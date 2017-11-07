using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servo_API.Models
{
    public class StockAdjustmentModel
    {
        public string Route_ID { get; set; }

        public string Route_Name { get; set; }

        public string product_info { get; set; }

        public string product_info1 { get; set; }

        public string product_info2 { get; set; }

        public string Prod { get; set; }

        public string PackType { get; set; }

        public string Qty { get; set; }

        public string Prod1 { get; set; }

        public string PackType1 { get; set; }

        public string Qty1 { get; set; }

        public bool SAV_ID_Visible { get; set; }

        public string Date { get; set; }

        public string SAV_ID { get; set; }
    }
}