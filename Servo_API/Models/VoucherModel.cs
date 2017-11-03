using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servo_API.Models
{
    public class VoucherModel
    {
        public string Contra { get; set; }
        public string Credit { get; set; }
        public string Debit { get; set; }
        public string Journal { get; set; }
        public int VoucherID { get; set; }
        public string VoucherType { get; set; }
        public string VoucherDate { get; set; }
        public string LedgerIDCr { get; set; }
        public string LedgerIDDr { get; set; }
        public string Amount1 { get; set; }
        public string Amount2 { get; set; }
        public string Narration { get; set; }
        public string LType { get; set; }
        public string InvoiceDate { get; set; }
        public string LedgerID { get; set; }

        public string AccName1 { get; set; }
        public string AccName5 { get; set; }
        public ArrayList CustomerName { get; set; }
        public ArrayList LedgerIDS { get; set; }

    }

    //public enum VoucherType
    //{
    //    Contra,
    //    Credit,
    //    Debit,
    //    Journal
    //}
}