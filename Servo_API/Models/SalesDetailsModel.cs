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

    ///// <summary>
    ///// Call the Procedure ProSalesDetailsEntry to insert the products details in Sales_Details table
    ///// and update the stock in every product with the help of ProStockUpdateAfterSales Procedure.
    ///// </summary>
    //public void InsertSalesDetail()
    //{
    //    SqlCmd = new SqlCommand("ProSalesDetailsEntry", SqlCon);
    //    SqlCmd.CommandType = CommandType.StoredProcedure;
    //    SqlCmd.Parameters.Add("@Invoice_No", Invoice_No);
    //    SqlCmd.Parameters.Add("@Prod_Name", Product_Name);
    //    SqlCmd.Parameters.Add("@Pack_Type", Package_Type);
    //    SqlCmd.Parameters.Add("@Qty", Qty);
    //    SqlCmd.Parameters.Add("@sno", sno);
    //    SqlCmd.Parameters.Add("@Rate", Rate);
    //    SqlCmd.Parameters.Add("@Amount", Amount);
    //    SqlCmd.Parameters.Add("@Qty1", QtyTemp);
    //    //SqlCmd.Parameters .Add("@OldQty",tempQty);
    //    //SqlCmd.Parameters .Add("@ProductID",prod_id);
    //    //SqlCmd.Parameters.Add("@Invoice_Date",Inv_date); 
    //    SqlCmd.Parameters.Add("@Invoice_Date", Invoice_Date);
    //    /*bhal*/
    //    SqlCmd.Parameters.Add("@scheme1", sch);
    //    /*bhal*/
    //    SqlCmd.Parameters.Add("@foe", foe);
    //    SqlCmd.Parameters.Add("@SchType", schtype); /* add by Mahesh on 16.01.009:5.17 PM */
    //    SqlCmd.Parameters.Add("@SecSPDisc", SecSPDisc); /* add by Mahesh on 16.01.009:5.17 PM */
    //    SqlCmd.Parameters.Add("@SecSPDiscType", SecSPDiscType); /* add by Mahesh on 16.01.009:5.17 PM */
    //    SqlCmd.Parameters.Add("@FoeType", foediscounttype); /* add by Mahesh on 16.01.009:5.17 PM */
    //    SqlCmd.ExecuteNonQuery();
    //}
}