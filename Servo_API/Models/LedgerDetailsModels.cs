using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servo_API.Models
{
    public class LedgerDetailsModels
    {
        public string BillNo { get; set; }
        public string BillDate { get; set; }
        public string Amount { get; set; }
        public string CustomerID { get; set; }
    }
}