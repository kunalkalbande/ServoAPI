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

        public string Route_Km { get; set; }

        public string Index_Route_Name { get; set; }

        public string Selected_Route_Name { get; set; }
    }
}