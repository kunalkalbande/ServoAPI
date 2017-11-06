using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servo_API.Models
{
    public class RouteMasterModel
    {
        public string VDLB_ID { get; set; }

        public string DOE { get; set; }

        public string Vehicle_no { get; set; }

        public string Vehicle_Name{ get; set; }

        public string Meter_reading_pre { get; set; }

        public string Meter_reading_cur { get; set; }

        public string Vehicle_route { get; set; }

        public string Fuel_Used { get; set; }

        public string Fuel_Used_Qty { get; set; }

        public string EngineOil { get; set; }

        public string Engine_Oil_Qty { get; set; }

        public string Engine_Oil_Pack { get; set; }

        public string Gear_Oil { get; set; }

        public string Gear_Oil_Qty { get; set; }

        public string Gear_Oil_Pack { get; set; }

        public string Brake_Oil { get; set; }

        public string Brake_Oil_Qty { get; set; }

        public string Brake_Oil_Pack { get; set; }

        public string Coolent { get; set; }
        public string Coolent_Oil_Qty { get; set; }
        public string Coolent_Oil_Pack { get; set; }

        public string Grease { get; set; }
        public string Grease_Qty { get; set; }
        public string Grease_Pack { get; set; }

        public string Trans_Oil { get; set; }
        public string Trans_Oil_Qty { get; set; }
        public string Trans_Oil_Pack { get; set; }




        public string Toll { get; set; }

        public string Police { get; set; }

        public string Food { get; set; }

        public string Misc { get; set; }

        public string DriverName { get; set; }
      
    }
}