using System;
using System.Collections;
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
        public List<string> empid { get; set; }
        public List<string> empname { get; set; }
        public List<string> basicsalary { get; set; }
        public List<string> extradays { get; set; }
        //public ArrayList empid = new ArrayList();
        //public ArrayList empname = new ArrayList();
        //public ArrayList basicsalary = new ArrayList();
        //public ArrayList extradays = new ArrayList();
    }
}