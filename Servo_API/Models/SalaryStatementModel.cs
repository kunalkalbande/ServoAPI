using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servo_API.Models
{
    public class SalaryStatementModel
    {
        public string hour { get; set; }
        public string min { get; set; }

        public string SSRincentiveStatus { get; set; }
        public string SSRincentive { get; set; }
    }
}