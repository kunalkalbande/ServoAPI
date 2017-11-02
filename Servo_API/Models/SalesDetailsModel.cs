using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Servo_API.Models
{

    public class SalesDetailsModel
    {
        SqlCommand SqlCmd;
        public string Invoice_No { get; set; }
        public string Product_Name { get; set; }
        public string Package_Type { get; set; }
        public string Qty { get; set; }
        public int sno { get; set; }

        public string Rate { get; set; }
        public string Amount { get; set; }
        public string QtyTemp { get; set; }

        public DateTime Invoice_Date { get; set; }

        public string sch { get; set; }
        public string foe { get; set; }
        public string schtype { get; set; }
        public string SecSPDisc { get; set; }
        public string SecSPDiscType { get; set; }
        public string foediscounttype { get; set; }
    }
}