using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servo_API.Models
{
    public class VehicleDailyLogbookModel
    {
        public string Vehicle_ID { get; set; }
        public string VehicleType2 { get; set; }

        public string Vehicleno { get; set; }
        public string Vehiclenm { get; set; }

        public string RTO_Reg_Val_yrs { get; set; }

        public string Model_name { get; set; }

        public string RTO_Reg_No { get; set; }
        public string Vehicle_Man_Date { get; set; }

        public string Insurance_No { get; set; }

        public string Meter_Reading { get; set; }

        public string Insurance_validity { get; set; }

        public string RouteName { get; set; }

        public string Insurance_Comp_name { get; set; }

        public string Fuel_Used { get; set; }

        public string Fuel_Used_Qty { get; set; }

        public string Start_Fuel_Qty { get; set; }

        public string EngineOil { get; set; }

        public string Engine_Oil_Qty { get; set; }

        public string Engine_Oil_Dt { get; set; }

        public string Gear_Oil { get; set; }


        public string Gear_Oil_Qty { get; set; }

        public string Gear_Oil_Dt { get; set; }

        public string Brake_Oil { get; set; }

        public string Brake_Oil_Qty { get; set; }

        public string Brake_Oil_Dt { get; set; }

        public string Coolent { get; set; }
        public string Coolent_Oil_Qty { get; set; }
        public string Coolent_Oil_Dt { get; set; }

        public string Grease { get; set; }
        public string Grease_Qty { get; set; }
        public string Grease_Dt { get; set; }

        public string Trans_Oil { get; set; }
        public string Trans_Oil_Qty { get; set; }
        public string Trans_Oil_Dt { get; set; }
        
        public string Vehicle_Avg { get; set; }

        public string Trans_Oil_km { get; set; }
        public string Engine_Oil_km { get; set; }
        public string Gear_Oil_km { get; set; }
        public string Brake_Oil_km { get; set; }
        public string Coolent_km { get; set; }
        public string Grease_km { get; set; }
    }
}