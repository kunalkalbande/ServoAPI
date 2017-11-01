/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/

using System;
using System.Data;
using System.Data.SqlClient;

namespace Servosms.Sysitem.Classes
{
    /// <summary>
    /// Summary description for Inventory.
    /// </summary>
    public class SalesSaveDetailsModel
    {
        public string Credit_Limit { get; set; }

        public string schdiscount { get; set; }

        public string foediscount { get; set; }

        public string foediscounttype { get; set; }
        public string foediscountrs { get; set; }

        public string ChallanNo { get; set; }

        public string SecSPDisc { get; set; }

        public string ChallanDate { get; set; }

        public string Cr_Plus { get; set; }
        public string Place { get; set; }

        public string Dr_Plus { get; set; }


        public string Slip_No { get; set; }


        public string Invoice_No { get; set; }

        public string Order_No { get; set; }

        public DateTime Invoice_Date { get; set; }

        public string Sales_Type { get; set; }

        public string Under_SalesMan { get; set; }

        public string Customer_Name { get; set; }

        public string Vehicle_No { get; set; }

        public string Grand_Total { get; set; }

        public string Discount { get; set; }

        public string Discount_Type { get; set; }


        public string Cash_Discount { get; set; }

        public string Cash_Disc_Type { get; set; }

        public string VAT_Amount { get; set; }

        public string SGST_Amount { get; set; }

        public string CGST_Amount { get; set; }

        public string totalqtyltr { get; set; }


        public string Net_Amount { get; set; }

        public string Promo_Scheme { get; set; }

        public string Remerk { get; set; }

        public string Entry_By { get; set; }

        public DateTime Entry_Time { get; set; }
    }
}
