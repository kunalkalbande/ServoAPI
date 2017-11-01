using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servo_API.Models
{
    public class SalesModels
    {
        public int Invoice_ID { get; set; }
        public string Invoice_Date { get; set; }
        public string Sales_Type { get; set; }
        public int Cust_ID { get; set; }
        public string Under_SalesMan { get; set; }
        public string Vehicle_No { get; set; }
        public string Grand_Total { get; set; }
        public float Discount { get; set; }
        public float Total_Discount { get; set; }
        public float Cash_Discount { get; set; }
        public float Total_Cash_Discount { get; set; }
        public string Discount_Type { get; set; }
        public float Net_Amount { get; set; }
        public string Promo_Scheme { get; set; }
        public string Remark { get; set; }
        public string Entry_By { get; set; }
        public DateTime Entry_Time { get; set; }
        public int Slip_No { get; set; }
        public string Cash_Disc_Type { get; set; }
        public float IGST_Amount { get; set; }
        public float Scheme_Discount { get; set; }
        public float FOE_Discount { get; set; }
        public string FOE_Discounttype { get; set; }
        public float FOE_Discountrs { get; set; }
        public float Total_Qty_Ltr { get; set; }
        public string ChallanNo { get; set; }
        public DateTime ChallanDate { get; set; }
        public float SecSPDisc { get; set; }
        public float SGST_Amount { get; set; }
        public float CGST_Amount { get; set; }

        public int Prod_ID { get; set; }
        public float Qty { get; set; }
        public float scheme1 { get; set; }
        public float foe { get; set; }
        public int sno { get; set; }
        public string SchType { get; set; }
        public string FoeType { get; set; }
        public string SPDiscType { get; set; }
        public float SPDisc { get; set; }

        public string TempText { get; set; }
        public string Tempminmax { get; set; }
        public string Message { get; set; }

        public string Cust_Name { get; set; }
        public string City { get; set; }
        public string Place { get; set; }
        public DateTime DueDate { get; set; }
        public string Current_Balance { get; set; }
        public float Credit_Limit { get; set; }
        public string CustomerType { get; set; }
        public string Group_Name { get; set; }
        public string BalanceType { get; set; }
        public string Balance { get; set; }


        //public List<string> SalesQty { get; set; }
        public List<string> SalesQty { get; set; }
        public List<string> ProdType { get; set; }
        public List<string> ProdCode { get; set; }
        public List<string> PackType { get; set; }
        public List<string> ProductType { get; set; }
        public List<string> ProductName { get; set; }
        public List<string> ProductPack { get; set; }
        public List<string> ProductQty { get; set; }
        public List<string> Rate { get; set; }
        public List<string> Amount { get; set; }
        public List<string> PID { get; set; }
        public List<string> PID1 { get; set; }
        public List<string> scheme { get; set; }
        public List<string> Details_foe { get; set; }
        public List<string> Av_Stock { get; set; }
        public List<string> SchSPType { get; set; }
        public List<string> SchSP { get; set; }
        public List<string> tmpFoeType { get; set; }
        public List<string> SalesQty1 { get; set; }
        public List<string> ProdType1 { get; set; }
        public List<string> tempSchQty { get; set; }
        public List<string> tmpSchType { get; set; }
        public List<string> stk1 { get; set; }
        //public List<string> tmpQty { get; set; }
        //public List<string> tempQty { get; set; }
        public List<string> SchProductType { get; set; }
        public List<string> SchProductName { get; set; }
        public List<string> SchProductPack { get; set; }
        public List<string> SchProductQty { get; set; }

        public ArrayList arrProdCode = new ArrayList();
        public ArrayList arrProdName = new ArrayList();
        public ArrayList arrBatchNo = new ArrayList();
        public ArrayList arrBillQty = new ArrayList();
        public ArrayList arrFreeQty = new ArrayList();
        public ArrayList arrDespQty = new ArrayList();
        public ArrayList arrLtrkg = new ArrayList();
        public ArrayList arrProdRate = new ArrayList();
        public ArrayList arrProdScheme = new ArrayList();
        public ArrayList arrProdAmount = new ArrayList();
        public ArrayList arrSecSP = new ArrayList();
        public ArrayList arrCgst = new ArrayList();
        public ArrayList arrSgst = new ArrayList();
        public ArrayList arrIgst = new ArrayList();
    }
}