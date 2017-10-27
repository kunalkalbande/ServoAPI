using Servo_API.App_Start;
using Servo_API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servo_API.Controllers
{
    public class PurchaseController : ApiController
    {
        public SqlConnection SqlCon { get; private set; }
        int ProductCount = 0;
        string[] mainarr = null;
        DbOperations_LATEST.DBUtil dbobj = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
        DbOperations_LATEST.DBUtil dbobj1 = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);


        //public List<ProductModels> Post(List<ProductModels> products)
        //{
        //    List<ProductModels> prducts = new List<ProductModels>();
        //    int focDisc = 0;
        //    double birdPerProd = 0;
        //    var schdistot_Add = 0;
        //    double tot_fixdisc = 0;
        //    string mainGSt = null;
        //    int IgstRate = 0;
        //    int CgstRate = 0;
        //    int SgstRate = 0;
        //    int Hsn = 0;
        //    double totalValue = 0;
        //    string ProdUnit = null;
        //    string Scheme = null;

        //    mainGSt = PriceUpdation();
        //    Scheme = getscheme(products.Select(p => p.InvoiceDate).FirstOrDefault());
        //    var stockScheme = GetStockistScheme(products.Select(p => p.InvoiceDate).FirstOrDefault());
        //    var schemeAddprimary = get_Addscheme(products.Select(p => p.InvoiceDate).FirstOrDefault());
        //    var fixedDiscount = GetFixedDiscount(products.Select(p => p.InvoiceDate).FirstOrDefault());
        //    ProdUnit = GetProductsUnit();
        //    var schemearr = Scheme.Split(',');
        //    var stockSchemearr = stockScheme.Split(',');
        //    var schemeAddprimaryarr = schemeAddprimary.Split(',');
        //    var fixedDiscountarr = fixedDiscount.Split(',');

        //    var prodUnitarr = ProdUnit.Split(',');

        //    mainarr = mainGSt.Split('~');

        //    foreach (var product in products)
        //    {
        //        var selproduct = product.ProductName.Split(':');
        //        //getschemeprimary
        //        for (var i = 0; i < Scheme.Length - 1; i++)
        //        {
        //            var taxarr = schemearr[i].Split(':');
        //            if (taxarr[1] == selproduct[0])
        //            {
        //                product.SchDis = taxarr[4] + ":" + taxarr[5];
        //            }
        //        }
        //        //GetStockistScheme
        //        for (var i = 0; i < stockSchemearr.Length - 1; i++)
        //        {
        //            var taxarr = stockSchemearr[i].Split(':');
        //            if (taxarr[1] == selproduct[0])
        //            {
        //                product.StockDiscount = taxarr[4] + ":" + taxarr[5];
        //            }
        //        }

        //        //getschemeAddprimary
        //        for (var i = 0; i < schemeAddprimaryarr.Length - 1; i++)
        //        {
        //            var taxarr = schemeAddprimaryarr[i].Split(':');
        //            if (taxarr[1] == selproduct[0])
        //            {
        //                product.SchAdditionalDiscount = taxarr[4] + ":" + taxarr[5];
        //            }
        //        }

        //        //GetFixedDiscount
        //        for (var i = 0; i < fixedDiscountarr.Length - 1; i++)
        //        {
        //            var taxarr = fixedDiscountarr[i].Split(':');
        //            if (taxarr[1] == selproduct[0])
        //            {
        //                product.FixedDisc = taxarr[4] + ":" + taxarr[5];
        //            }
        //        }

        //        //GetProductsUnit
        //        for (var i = 0; i < prodUnitarr.Length - 1; i++)
        //        {
        //            var taxarr = prodUnitarr[i].Split(':');
        //            if (taxarr[1] == selproduct[0])
        //            {
        //                product.Unit = taxarr[4];
        //            }
        //        }
        //        if (product.DropFOCType == "Per")
        //            product.FOC = product.Amount * focDisc / 100;

        //        if (ProductCount != 0)
        //        {
        //            product.TradeLess = product.TradeLess / ProductCount;
        //        }
        //        if (product.EarlyDisType != "%")
        //        {
        //            birdPerProd = (product.QuantityPack * product.Quantity) * product.EBird;
        //        }
        //        else
        //        {
        //            var early = product.Amount * double.Parse(product.EBird.ToString());
        //            birdPerProd = early / 100;
        //        }

        //        if (ProductCount != 0)
        //        {
        //            product.BirdLess = product.BirdLess / ProductCount;
        //        }

        //        var ltrs = selproduct[2].Split('X');
        //        var calcLtrs = Convert.ToInt32(ltrs[0]) * Convert.ToInt32(ltrs[1]);
        //        if (product.DropDiscType == "Per")
        //        {
        //            var Dt = product.Amount;
        //            product.Discount = Convert.ToString(Dt * Convert.ToInt32(product.Discount) / 100);
        //        }
        //        else
        //        {
        //            product.Discount = Convert.ToString((calcLtrs * product.Quantity) * double.Parse(product.Discount.ToString()));
        //        }
        //        if (!string.IsNullOrEmpty(product.SchAdditionalDiscount))
        //        {
        //            var dis = product.SchAdditionalDiscount.ToString();
        //            var schdis_Add = dis.Split(':');
        //            if (schdis_Add[1] == "%")
        //            {
        //                schdistot_Add = Convert.ToInt32(product.Amount) * Convert.ToInt32(schdis_Add[0]) / 100;
        //            }
        //            else
        //            {
        //                schdistot_Add = schdistot_Add + product.QuantityPack * product.Quantity * Convert.ToInt32(schdis_Add[0]);
        //            }
        //        }

        //        var fixedDisc_Add = 0;
        //        fixedDisc_Add = schdistot_Add;
        //        var Sch_Disc = 0;

        //        if (!string.IsNullOrEmpty(product.SchDiscount))
        //            Sch_Disc = Convert.ToInt32(product.SchDiscount);

        //        string[] stktdis = null;
        //        var stktdistot = 0;
        //        if (product.Unit == "Barrel" || product.Unit == "Drum")
        //        {
        //            if (!string.IsNullOrEmpty(product.StockDiscount))
        //            {
        //                var stkt = product.StockDiscount.ToString();
        //                stktdis = stkt.Split(':');
        //                if (!product.CheckFOC)
        //                {
        //                    if (stktdis[1] == "%")
        //                    {
        //                        var stk = (double.Parse(product.Amount.ToString()) * double.Parse(stktdis[0].ToString())) / 100;
        //                        stktdistot = Convert.ToInt32(stk);
        //                    }
        //                    else
        //                    {
        //                        stktdistot = stktdistot + product.QuantityPack * product.Quantity * Convert.ToInt32(stktdis[0]);
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            var cgst = "";
        //            var sgst = "";
        //            for (var i = 0; i < mainarr.Length - 1; i++)
        //            {
        //                var taxarr = mainarr[i].Split('|');
        //                if (taxarr[0] == selproduct[0])
        //                {
        //                    cgst = taxarr[4];
        //                    sgst = taxarr[5];
        //                }
        //            }

        //            if (!string.IsNullOrEmpty(product.StockDiscount))
        //            {
        //                var stkt = product.StockDiscount.ToString();
        //                stktdis = stkt.Split(':');
        //                if (!product.CheckFOC)
        //                {
        //                    if (stktdis[1] == "%")
        //                    {
        //                        var stckDisc = ((double.Parse(product.Amount.ToString()) * (double.Parse(cgst) + double.Parse(sgst)))) / 100;
        //                        var stk = ((double.Parse(stckDisc.ToString()) + double.Parse(product.Amount.ToString())) * double.Parse(stktdis[0].ToString())) / 100;
        //                        stktdistot = Convert.ToInt32(stk);
        //                    }
        //                    else
        //                    {
        //                        stktdistot = stktdistot + product.QuantityPack * product.Quantity * Convert.ToInt32(stktdis[0]);
        //                    }
        //                }
        //            }
        //        }

        //        if (!string.IsNullOrEmpty(product.FixedDisc))
        //        {
        //            var stkt = product.FixedDisc.ToString();
        //            stktdis = stkt.Split(':');
        //            if (stktdis[1] == "%")
        //            {

        //            }
        //            else
        //            {
        //                tot_fixdisc = double.Parse(tot_fixdisc.ToString()) + double.Parse(product.QuantityPack.ToString()) * double.Parse(product.Quantity.ToString()) * double.Parse(stktdis[0]);
        //            }

        //        }
        //        var schdistot = 0;
        //        if (!string.IsNullOrEmpty(product.SchDis))
        //        {
        //            var dis = product.SchDis.ToString();
        //            var schdis = dis.Split(':');
        //            if (schdis[1] == "%")
        //            {
        //                schdistot = Convert.ToInt32(product.Amount) * Convert.ToInt32(schdis[0]) / 100;
        //            }
        //            else
        //            {
        //                schdistot = schdistot + product.QuantityPack * product.Quantity * Convert.ToInt32(schdis[0]);
        //            }
        //        }
        //        var tradeDisc = stktdistot;
        //        product.StockDiscountAmount = stktdistot;
        //        if (product.DropCashDiscType == "Per")
        //        {
        //            var GT = double.Parse(product.Amount.ToString()) - tot_fixdisc - fixedDisc_Add - ((tradeDisc) - (product.TradeLess) + double.Parse(product.FOC.ToString()) + double.Parse(product.Discount.ToString()) + (schdistot) + ((birdPerProd) - (product.BirdLess) + (Sch_Disc)));
        //            var cashdiscount = (double.Parse(GT.ToString()) * double.Parse(product.CashDiscount)) / 100;
        //            product.CashDiscount = cashdiscount.ToString();
        //        }
        //        else
        //        {
        //            var cashdiscount = Convert.ToInt32(product.CashDiscount) * (calcLtrs * product.Quantity);
        //            product.CashDiscount = cashdiscount.ToString();
        //        }

        //        for (var i = 0; i < mainarr.Length - 1; i++)
        //        {
        //            var taxarr = mainarr[i].Split('|');
        //            if (taxarr[0] == selproduct[0])
        //            {
        //                IgstRate = Convert.ToInt32(taxarr[3]);
        //                CgstRate = Convert.ToInt32(taxarr[4]);
        //                SgstRate = Convert.ToInt32(taxarr[5]);
        //                var earbird = birdPerProd - product.BirdLess;
        //                var trade = (tradeDisc - product.TradeLess);
        //                totalValue = product.Amount - (trade + double.Parse(product.FOC.ToString()) + (earbird) + double.Parse(product.CashDiscount) + double.Parse(product.Discount) + fixedDisc_Add + tot_fixdisc + Sch_Disc);

        //                product.IGST = (totalValue * IgstRate) / 100;
        //                product.CGST = (totalValue * CgstRate) / 100;
        //                product.SGST = (totalValue * SgstRate) / 100;
        //            }
        //        }

        //        prducts.Add(product);
        //    }
        //    return prducts;
        //}

        [HttpGet]
        [Route("api/purchase/PriceUpdation")]
        public string PriceUpdation()
        {
            using (SqlCon = new SqlConnection())
            {
                string mainGSt = null;
                SqlCon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ServoSMS"]);
                SqlCon.Open();
                var SqlCmd = new SqlCommand("ProPriceUpdation", SqlCon);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                var da = new SqlDataAdapter(SqlCmd);
                var dsPriceUpdation = new DataSet();
                da.Fill(dsPriceUpdation);
                var dtTable = dsPriceUpdation.Tables[0];
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    mainGSt = mainGSt + dtTable.Rows[i][0].ToString();//ProductCode
                    mainGSt = mainGSt + "|" + dtTable.Rows[i][1];//ProductName 
                    mainGSt = mainGSt + "|" + dtTable.Rows[i][2];//ProductId
                    mainGSt = mainGSt + "|" + dtTable.Rows[i][3];//IGST
                    mainGSt = mainGSt + "|" + dtTable.Rows[i][4];//cGST
                    mainGSt = mainGSt + "|" + dtTable.Rows[i][5];//sGST
                    mainGSt = mainGSt + "|" + dtTable.Rows[i][6];//HSN
                    mainGSt = mainGSt + "~";
                }
                mainGSt = mainGSt.Substring(0, mainGSt.LastIndexOf("~"));
                return mainGSt;
            }
        }

        [HttpGet]
        [Route("api/purchase/GetProductsUnit")]
        public string GetProductsUnit()
        {
            using (SqlCon = new SqlConnection())
            {
                string str = null;
                SqlCon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ServoSMS"]);
                SqlCon.Open();
                string sql = "select p.prod_code cat,p.prod_name pname,p.pack_type ptype,p.Unit unit from products p";
                var SqlCmd = new SqlCommand(sql, SqlCon);
                var SqlDtr = SqlCmd.ExecuteReader();
                while (SqlDtr.Read())
                {
                    str = str + ":" + SqlDtr["cat"] + ":" + SqlDtr["pname"] + ":" + SqlDtr["ptype"] + ":" + SqlDtr["unit"] + ",";
                }
                return str;
            }
        }
        [HttpGet]
        [Route("api/purchase/getscheme")]
        public string getscheme(string invoiceDate)
        {
            string str = "";
            using (SqlCon = new SqlConnection())
            {
                SqlCon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ServoSMS"]);
                SqlCon.Open();
                SqlDataReader SqlDtr;
                string sql;
                sql = "select p.prod_code cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,o.discounttype distype  from products p,per_discount o where p.prod_id=o.prodid and o.schname in ('Secondry(LTR Scheme)','Primary(LTR&% Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '" + invoiceDate + "' and cast(floor(cast(o.dateto as float)) as datetime) >= '" + invoiceDate + "'";
                var SqlCmd = new SqlCommand(sql, SqlCon);
                SqlDtr = SqlCmd.ExecuteReader();
                while (SqlDtr.Read())
                {
                    str = str + ":" + SqlDtr["cat"] + ":" + SqlDtr["pname"] + ":" + SqlDtr["ptype"] + ":" + SqlDtr["dis"] + ":" + SqlDtr["distype"] + ",";
                }
                SqlDtr.Close();
            }
            return str;
        }
        [HttpGet]
        [Route("api/purchase/GetStockistScheme")]
        public string GetStockistScheme(string invoiceDate)
        {
            string strDiscount = GetStockistDiscount();
            SqlDataReader SqlDtr;
            string sql;
            string str = "";
            using (SqlCon = new SqlConnection())
            {
                SqlCon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ServoSMS"]);
                SqlCon.Open();
                sql = "select p.prod_code cat,p.prod_name pname,p.pack_type ptype,o.discount dis,o.discounttype distype  from products p,StktSchDiscount o where p.prod_id=o.prodid and o.schtype in ('Secondry(LTR Scheme)','Primary(LTR&% Scheme)','Secondry SP(LTRSP Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '" + invoiceDate + "' and cast(floor(cast(o.dateto as float)) as datetime) >= '" + invoiceDate + "'";
                var SqlCmd = new SqlCommand(sql, SqlCon);
                SqlDtr = SqlCmd.ExecuteReader();
                while (SqlDtr.Read())
                {
                    str = str + ":" + SqlDtr["cat"] + ":" + SqlDtr["pname"] + ":" + SqlDtr["ptype"] + ":" + strDiscount + ":" + "%" + ",";
                }
                SqlDtr.Close();
            }
            return str;
        }
        [HttpGet]
        [Route("api/purchase/GetStockistDiscount")]
        public string GetStockistDiscount()
        {
            string strDiscount = "";
            SqlDataReader SqlDtr;
            string sql;
            using (SqlCon = new SqlConnection())
            {
                SqlCon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ServoSMS"]);
                SqlCon.Open();
                sql = "select Servostk from SetDis";
                var SqlCmd = new SqlCommand(sql, SqlCon);
                SqlDtr = SqlCmd.ExecuteReader();
                while (SqlDtr.Read())
                {
                    strDiscount = SqlDtr["Servostk"].ToString();
                }
                SqlDtr.Close();
            }
            return strDiscount;
        }
        [HttpGet]
        [Route("api/purchase/get_Addscheme")]
        public string get_Addscheme(string invoiceDate)
        {
            SqlDataReader SqlDtr;
            string sql;
            string str = "";
            using (SqlCon = new SqlConnection())
            {
                SqlCon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ServoSMS"]);
                SqlCon.Open();
                sql = "select p.prod_code cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,o.discounttype distype  from products p,per_discount o where p.prod_id=o.prodid and o.schname in ('Primary(LTR&% Addl Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '" + invoiceDate + "' and cast(floor(cast(o.dateto as float)) as datetime) >= '" + invoiceDate + "'";
                var SqlCmd = new SqlCommand(sql, SqlCon);
                SqlDtr = SqlCmd.ExecuteReader();
                while (SqlDtr.Read())
                {
                    str = str + ":" + SqlDtr["cat"] + ":" + SqlDtr["pname"] + ":" + SqlDtr["ptype"] + ":" + SqlDtr["dis"] + ":" + SqlDtr["distype"] + ",";
                }
                SqlDtr.Close();
            }
            return str;
        }

        [HttpGet]
        [Route("api/purchase/GetFixedDiscount")]
        public string GetFixedDiscount(string invoiceDate)
        {
            SqlDataReader SqlDtr;
            string sql;
            string str = "";
            using (SqlCon = new SqlConnection())
            {
                SqlCon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ServoSMS"]);
                SqlCon.Open();
                sql = "select p.prod_code cat,p.prod_name pname,p.pack_type ptype,o.discount dis,o.discounttype distype  from products p,StktSchDiscount o where p.prod_id=o.prodid and o.schtype in ('Fixd Discount') and cast(floor(cast(o.datefrom as float)) as datetime) <= '" + invoiceDate + "' and cast(floor(cast(o.dateto as float)) as datetime) >= '" + invoiceDate + "'";
                var SqlCmd = new SqlCommand(sql, SqlCon);
                SqlDtr = SqlCmd.ExecuteReader();
                if (SqlDtr.HasRows)
                {
                    while (SqlDtr.Read())
                    {
                        str = str + ":" + SqlDtr["cat"] + ":" + SqlDtr["pname"] + ":" + SqlDtr["ptype"] + ":" + SqlDtr["dis"] + ":" + SqlDtr["distype"] + ",";
                    }
                    SqlDtr.Close();
                }
                else
                {
                    str = str + "0:0:0:0,";
                }
            }
            return str;
        }

        [HttpGet]
        [Route("api/purchase/GetDiscount")]
        public DiscountModels GetDiscount()
        {
            SqlDataReader SqlDtr;
            string sql;
            using (SqlCon = new SqlConnection())
            {
                SqlCon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ServoSMS"]);
                SqlCon.Open();
                sql = "select * from SetDis";
                var SqlCmd = new SqlCommand(sql, SqlCon);
                SqlDtr = SqlCmd.ExecuteReader();
                DiscountModels discount = new DiscountModels();
                if (SqlDtr.Read())
                {
                    discount.FixedStatus = SqlDtr["FixedStatus"].ToString();
                    discount.FixedDis = SqlDtr["FixedDis"].ToString();
                    discount.ServoStatus = SqlDtr["ServoStatus"].ToString();
                    discount.ServoStk = SqlDtr["ServoStk"].ToString();
                    discount.CashDisPurchaseStatus = SqlDtr["CashDisPurchaseStatus"].ToString();
                    discount.CashDisPurchase = SqlDtr["CashDisPurchase"].ToString();
                    discount.CashDisLtrPurchase = SqlDtr["CashDisLtrPurchase"].ToString();
                    discount.DiscountPurchaseStatus = SqlDtr["DiscountPurchaseStatus"].ToString();
                    discount.DiscountPurchase = SqlDtr["DiscountPurchase"].ToString();
                    discount.DisLtrPurchase = SqlDtr["DisLtrPurchase"].ToString();
                    discount.CashDisSalesStatus = SqlDtr["CashDisSalesStatus"].ToString();
                    discount.CashDisSales = SqlDtr["CashDisSales"].ToString();
                    discount.CashDisLtrSales = SqlDtr["CashDisLtrSales"].ToString();
                    discount.DiscountSalesStatus = SqlDtr["DiscountSalesStatus"].ToString();
                    discount.DiscountSales = SqlDtr["DiscountSales"].ToString();
                    discount.DisLtrSales = SqlDtr["DisLtrSales"].ToString();
                    discount.EarlyStatus = SqlDtr["EarlyStatus"].ToString();
                    discount.EarlyBird = SqlDtr["EarlyBird"].ToString();
                    discount.EarlyDisLtrPurchase = SqlDtr["EarlyDisLtrPurchase"].ToString();
                    discount.EarlyBird_Period = SqlDtr["EarlyBird_Period"].ToString();
                }
                return discount;
            }
        }
        [HttpGet]
        [Route("api/purchase/GetNextInvoiceNo")]
        public string GetNextInvoiceNo()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            string NextInvoiceNo = null;

            sql = "select max(Invoice_No)+1 from Purchase_Master";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                NextInvoiceNo = SqlDtr.GetValue(0).ToString();
                if (NextInvoiceNo == "")
                    NextInvoiceNo = "1001";
            }
            SqlDtr.Close();

            return NextInvoiceNo;
        }

        [HttpGet]
        [Route("api/purchase/GetProductsType")]
        public string GetProductsType()
        {
            InventoryClass obj = new InventoryClass();
            string prodType = null;
            string sql = "select distinct Prod_Code,Prod_name,Pack_Type from Products p,price_updation pu where Category!='Fuel' and p.prod_id=pu.prod_id";
            var SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.HasRows)
            {
                prodType = "Type,";
                while (SqlDtr.Read())
                {
                    prodType += SqlDtr.GetValue(0).ToString() + ":" + SqlDtr.GetValue(1).ToString() + ":" + SqlDtr.GetValue(2).ToString() + ",";
                }
            }
            SqlDtr.Close();
            return prodType;
        }
        [HttpGet]
        [Route("api/purchase/GetSupplierName")]
        public List<string> GetSupplierName()
        {
            InventoryClass obj = new InventoryClass();
            List<string> supplierName = new List<string>();
            string sql = "select Supp_Name from Supplier order by Supp_Name";
            var SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                supplierName.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            return supplierName;
        }
        [HttpGet]
        [Route("api/purchase/GetProducts")]
        public List<string> GetProducts()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            SqlDataReader rdr = null;
            int count = 0;
            int count1 = 0;
            List<string> result = new List<string>();
            dbobj.ExecuteScalar("Select Count(Prod_id) from  products where Category != 'Fuel'", ref count);
            dbobj.ExecuteScalar("select count(distinct p.Prod_ID) from products p,price_updation pu where Category!='Fuel' and p.prod_id =pu.prod_id", ref count1);

            if (count != count1)
            {
                result.Add("Price updation not available for some products");
            }
            string str = "";
            sql = "select distinct p.Prod_ID,Category,Prod_Name,Pack_Type,Prod_Code,Unit from products p,price_updation pu where Category!='Fuel' and p.prod_id =pu.prod_id order by Category,Prod_Name";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                str = str + SqlDtr["Category"] + ":" + SqlDtr["Prod_Name"] + ":" + SqlDtr["Pack_Type"];
                sql = "select top 1 Pur_Rate from Price_Updation where Prod_ID=" + SqlDtr["Prod_ID"] + " and Pur_Rate<>0 order by eff_date desc";
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str = str + ":" + rdr["Pur_Rate"] + ":";
                    str = str + SqlDtr["Prod_Code"] + ",";
                }
                rdr.Close();
            }
            result.Add(str);
            SqlDtr.Close();
            return result;
        }

        [HttpGet]
        [Route("api/purchase/FetchInvoiceNo")]
        public string FetchInvoiceNo()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader rdr;
            string str = "";
            string strstr = "select Vndr_Invoice_No from Purchase_Master";
            rdr = obj.GetRecordSet(strstr);
            while (rdr.Read())
            {
                str += rdr["Vndr_Invoice_No"].ToString() + "~";
            }
            rdr.Close();
            return str;

        }
        [HttpGet]
        [Route("api/purchase/FetchCity")]
        public string FetchCity()
        {
            var supplier = GetSupplierName();
            string city = "";
            string str1 = "";
            IEnumerator enum1 = supplier.GetEnumerator();
            enum1.MoveNext();
            while (enum1.MoveNext())
            {
                string s = enum1.Current.ToString();
                dbobj.SelectQuery("Select City from Supplier where Supp_Name='" + s + "'", "City", ref city);
                str1 = str1 + s + "~" + city + "#";
            }
            return str1;

        }
        [HttpGet]
        [Route("api/purchase/FillInvoiceNo")]//fillID method in app.
        public List<string> FillInvoiceNo()
        {
            List<string> invoiceNo = new List<string>();
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            sql = "select distinct Invoice_No from Fuel_Purchase_Details";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                invoiceNo.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            return invoiceNo;
        }
        [HttpGet]
        [Route("api/purchase/FetchAllInvoice")]//edit button click
        public List<string> FetchAllInvoice()
        {
            List<string> invoice = new List<string>();
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            sql = "select cast(Invoice_No as varchar)+':'+cast(Vndr_Invoice_No as varchar) InvoiceNo from Purchase_Master";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                invoice.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            return invoice;
        }

        [HttpGet]
        [Route("api/purchase/GetInvoiceCount")]
        public int GetInvoiceCount(string invoiceNo)
        {
            int count = 0;
            dbobj.ExecuteScalar("Select count(Invoice_No) from Purchase_Master where Invoice_No = " + invoiceNo, ref count);
            return count;
        }
        //[HttpPost]
        //[Route("api/purchase/SavePurchaseDetails")]
        public void Post(ProductModels product)
        {
            InventoryClass obj = new InventoryClass();

            obj.InsertPurchaseDetail(product);

        }
        [HttpPost]
        [Route("api/purchase/PostPurchaseMaster")]
        public void PostPurchaseMaster(PurchaseModels purchase)
        {
            InventoryClass obj = new InventoryClass();

            obj.InsertPurchaseMaster(purchase);

        }
        [HttpGet]
        [Route("api/purchase/GetSNo")]
        public int GetSNo()
        {
            InventoryClass objprod = new InventoryClass();
            int SNo = 0;
            SqlDataReader rdr = null;
            rdr = objprod.GetRecordSet("select max(sno)+1 from Batch_Transaction");
            if (rdr.Read())
            {
                if (rdr.GetValue(0).ToString() != null && rdr.GetValue(0).ToString() != "")
                    SNo = int.Parse(rdr.GetValue(0).ToString());
                else
                    SNo = 1;
            }
            else
                SNo = 1;
            rdr.Close();
            return SNo;
        }
        [HttpGet]
        [Route("api/purchase/GetBatchID")]
        public int GetBatchID()
        {
            InventoryClass objprod = new InventoryClass();
            int batch_id = 0;
            SqlDataReader rdr = null;
            rdr = objprod.GetRecordSet("select max(Batch_ID)+1 from BatchNo");
            if (rdr.Read())
            {
                if (rdr.GetValue(0).ToString() != null && rdr.GetValue(0).ToString() != "")
                    batch_id = int.Parse(rdr.GetValue(0).ToString());
                else
                    batch_id = 1;
            }
            else
                batch_id = 1;
            rdr.Close();
            return batch_id;
        }

        [HttpPost]
        [Route("api/purchase/PostBatchTransaction")]
        public void PostBatchTransaction(List<ProductModels> products)
        {
            InventoryClass objprod = new InventoryClass();
            InventoryClass objprod1 = new InventoryClass();
            SqlDataReader rdr = null;
            SqlDataReader rdr1 = null;
            int x = 0, batch_id = 0, SNo = 0;

            rdr = objprod.GetRecordSet("select max(sno)+1 from Batch_Transaction");
            if (rdr.Read())
            {
                if (rdr.GetValue(0).ToString() != null && rdr.GetValue(0).ToString() != "")
                    SNo = int.Parse(rdr.GetValue(0).ToString());
                else
                    SNo = 1;
            }
            else
                SNo = 1;
            rdr.Close();

            rdr = objprod.GetRecordSet("select max(Batch_ID)+1 from BatchNo");
            if (rdr.Read())
            {
                if (rdr.GetValue(0).ToString() != null && rdr.GetValue(0).ToString() != "")
                    batch_id = int.Parse(rdr.GetValue(0).ToString());
                else
                    batch_id = 1;
            }
            else
                batch_id = 1;
            rdr.Close();
            int tot_bat_qty = 0;
            foreach (var product in products)
            {
                //for (int i = 0, j = 1; i < 11; i++, j++)
                {

                    if (product.Batch != "")
                    {
                        string prodid = "";
                        string[] arrName = product.ProductName.Split(new char[] { ':' }, product.ProductName.Length);
                        rdr = objprod.GetRecordSet("select prod_id from products where prod_name='" + arrName[1].ToString() + "' and pack_type='" + arrName[2].ToString() + "'");
                        if (rdr.Read())
                        {
                            prodid = rdr["prod_id"].ToString();
                        }
                        rdr.Close();

                        var dt = product.InvoiceDate;
                        string[] arr = product.Batch.Split(new char[] { ',' }, product.Batch.Length);
                        for (int n = 0; n < arr.Length; n += 3)
                        {
                            if (!arr[n].ToString().Equals("''"))
                            {
                                //********Start * Coment by vikas 16.06.09 *******************************************
                                string BNo = arr[n].ToString();
                                string BQty = arr[n + 2].ToString();

                                rdr = objprod.GetRecordSet("select batch_id from batchno where batch_no=" + arr[n].ToString() + " and prod_id='" + prodid.ToString() + "'");
                                if (rdr.Read())
                                {

                                    rdr1 = objprod1.GetRecordSet("select * from StockMaster_batch where batch_id=" + rdr.GetValue(0).ToString() + " and productid='" + prodid.ToString() + "'");
                                    if (rdr1.Read())
                                    {
                                        double op_stk = Convert.ToDouble(rdr1.GetValue(3));
                                        double receipt = Convert.ToDouble(rdr1.GetValue(4));
                                        string qty1 = arr[n + 2].ToString();
                                        qty1 = qty1.Substring(1, (qty1.Length) - 2); ;

                                        receipt = receipt + Convert.ToDouble(qty1.ToString());
                                        double Sales = Convert.ToDouble(rdr1.GetValue(5));
                                        double Cl_stk = Convert.ToDouble(rdr1.GetValue(6));
                                        Cl_stk = (op_stk + receipt) - Sales;

                                        dbobj.Insert_or_Update("update StockMaster_Batch set stock_date='" + dt + "',opening_stock=" + op_stk + ",receipt=" + receipt.ToString() + ", sales=" + Sales + ",closing_stock=" + Math.Round(Cl_stk) + " where productid=" + prodid + " and batch_id=" + rdr.GetValue(0).ToString(), ref x);

                                        //coment by vikas 17.06.09 dbobj.Insert_or_Update("insert into Batch_Transaction values("+(SNo++)+",'"+lblInvoiceNo.Text+"','Purchase Invoice','"+dt+"','"+prodid+"',"+rdr.GetValue(0).ToString()+","+arr[n+2].ToString()+","+arr[n+2].ToString()+")",ref x);//Maintain the closing stock by Prod_ID on every Batch No
                                        dbobj.Insert_or_Update("insert into Batch_Transaction values(" + (SNo++) + ",'" + product.Invoice_No + "','Purchase Invoice','" + dt + "','" + prodid + "'," + rdr.GetValue(0).ToString() + "," + arr[n + 2].ToString() + "," + Cl_stk + ")", ref x);
                                        dbobj.Insert_or_Update("update BatchNo set qty=" + Cl_stk + " where prod_id=" + prodid + " and batch_id=" + rdr.GetValue(0).ToString(), ref x);

                                        BQty = BQty.Substring(1, (BQty.Length) - 2);
                                        tot_bat_qty = +Convert.ToInt32(BQty);

                                    }
                                    rdr1.Close();
                                }
                                else
                                {
                                    dbobj.Insert_or_Update("insert into BatchNo values(" + batch_id + "," + arr[n].ToString() + ",'" + prodid + "','" + dt + "'," + arr[n + 2].ToString() + "," + prodid + ")", ref x);
                                    dbobj.Insert_or_Update("insert into Batch_Transaction values(" + (SNo++) + ",'" + product.Invoice_No + "','Purchase Invoice','" + dt + "','" + prodid + "'," + batch_id + "," + arr[n + 2].ToString() + "," + arr[n + 2].ToString() + ")", ref x);//Maintain the closing stock by Prod_ID on every Batch No
                                    dbobj.Insert_or_Update("insert into StockMaster_Batch values(" + prodid + ",'" + batch_id + "','" + dt + "',0," + arr[n + 2].ToString() + ",0," + arr[n + 2].ToString() + ",0,0)", ref x);

                                    BQty = BQty.Substring(1, (BQty.Length) - 2);
                                    tot_bat_qty = +Convert.ToInt32(BQty);


                                    batch_id++;
                                }
                                rdr.Close();
                            }
                            else
                            {
                                if (!arr[n + 2].ToString().Equals("''"))
                                {
                                    dbobj.Insert_or_Update("insert into Batch_Transaction values(" + (SNo++) + ",'" + product.Invoice_No + "','Purchase Invoice','" + dt + "','" + prodid + "',''," + arr[n + 2].ToString() + "," + arr[n + 2].ToString() + ")", ref x);
                                    dbobj.Insert_or_Update("insert into StockMaster_Batch values(" + prodid + ",'','" + dt + "',0," + arr[n + 2].ToString() + ",0," + arr[n + 2].ToString() + ",0,0)", ref x);
                                }
                                break;
                            }
                        }
                    }
                }
            }

        }
        [HttpPost]
        [Route("api/purchase/UpdateProductQty")]
        public void UpdateProductQty(List<ProductModels> products)
        {
            foreach (var product in products)
            {
                InventoryClass obj = new InventoryClass();
                InventoryClass obj1 = new InventoryClass();
                SqlDataReader rdr;
                SqlCommand cmd;
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                string str = "select Prod_ID from Products where Category='" + product.ProductType + "' and Prod_Name='" + product.ProductName + "' and Pack_Type='" + product.ProductPack + "'";
                rdr = obj.GetRecordSet(str);
                if (rdr.Read())
                {
                    Con.Open();
                    string invdt = product.InvoiceDate.ToString("MM/dd/yyyy HH:mm:ss");
                    cmd = new SqlCommand("update Stock_Master set receipt=receipt-" + double.Parse(product.Quantity) + ", Closing_Stock=Closing_Stock-" + double.Parse(product.Quantity) + " where ProductID='" + rdr["Prod_ID"].ToString() + "' and cast(floor(cast(cast(Stock_Date as datetime) as float)) as datetime)=convert(datetime,'" + invdt + "')", Con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Con.Close();
                }
                rdr.Close();
            }

        }

        [HttpPost]
        [Route("api/purchase/UpdateMasterPurchase")]
        public void UpdateMasterPurchase(PurchaseModels purchase)
        {
            InventoryClass obj = new InventoryClass();
            int VendorID = 0;
            string Vendor_ID = null;
            string sql = "select * from Purchase_Master where Invoice_No='" + purchase.InvoiceNo + "'";
            var SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                Vendor_ID = SqlDtr["Vendor_ID"].ToString();
            }
            SqlDtr.Close();
            dbobj.ExecuteScalar("Select Supp_ID from  Supplier where Supp_Name='" + purchase.VendorID + "'", ref VendorID);
            if (Vendor_ID != VendorID.ToString())
            {
                int xx = 0;
                dbobj.Insert_or_Update("delete from Purchase_Master where Invoice_No='" + purchase.InvoiceNo + "'", ref xx);
                dbobj.Insert_or_Update("delete from Accountsledgertable where Particulars='Purchase Invoice (" + purchase.InvoiceNo + ")'", ref xx);
                dbobj.Insert_or_Update("delete from Vendorledgertable where Particular='Purchase Invoice (" + purchase.InvoiceNo + ")'", ref xx);
                obj.InsertPurchaseMaster(purchase);
            }
            else
                obj.updateMasterPurchase(purchase);

        }
        [HttpPost]
        [Route("api/purchase/UpdateProductDetails")]
        public void UpdateProductDetails(ProductModels product)
        {
            InventoryClass obj = new InventoryClass();

            obj.UpdatePurchaseDetail(product);
        }
        [HttpPost]
        [Route("api/purchase/InsertBatchNo")]
        public void InsertBatchNo(ProductModels product)
        {
            if (product.Batch.ToString() == "")
            {
                return;
            }
            InventoryClass objprod = new InventoryClass();
            InventoryClass objprod1 = new InventoryClass();
            //InventoryClass obj = new InventoryClass();
            SqlDataReader rdr = null;
            int x = 0, SNo = 0, BatID = 0;
            string Inv_No = "";
            //string sql = "select p.Category,p.Prod_Name,p.Pack_Type,	pd.qty,pd.rate,pd.amount,pd.foc,p.Prod_Code,pd.Prod_ID,Invoice_Date,pd.Discount" +
            //           " from Products p, Purchase_Details pd, Purchase_Master pm" +
            //           //" where pm.Invoice_No=pd.Invoice_No and p.Prod_ID=pd.prod_id and pd.invoice_no=(select Invoice_No from Purchase_Master where Vndr_Invoice_No='"+ DropInvoiceNo.SelectedItem.Value +"')";
            //           " where pm.Invoice_No=pd.Invoice_No and p.Prod_ID=pd.prod_id and pm.invoice_no='" + product.Invoice_No + "' order by sno";

            //var SqlDtr = obj.GetRecordSet(sql);
            //while (SqlDtr.Read())
            //{
            //    product.ProductName = SqlDtr.GetValue(1).ToString();
            //    product.ProductPack = SqlDtr.GetValue(2).ToString();
            //}
            SqlDataReader rdr1 = objprod.GetRecordSet("select max(sno)+1 from Batch_Transaction");
            if (rdr1.Read())
            {
                if (rdr1.GetValue(0).ToString() != null && rdr1.GetValue(0).ToString() != "")
                    SNo = int.Parse(rdr1.GetValue(0).ToString());
                else
                    SNo = 1;
            }
            else
                SNo = 1;
            rdr1.Close();
            rdr1 = objprod.GetRecordSet("select max(Batch_ID) from BatchNo");
            if (rdr1.Read())
            {
                if (rdr1.GetValue(0).ToString() != null && rdr1.GetValue(0).ToString() != "")
                    BatID = int.Parse(rdr1.GetValue(0).ToString());
                else
                    BatID = 0;
            }
            else
                BatID = 0;
            rdr1.Close();
            string prodid = "", oldProdID = "";
            rdr1 = objprod.GetRecordSet("select prod_id from products where prod_name='" + product.ProductName + "' and pack_type='" + product.ProductPack + "'");
            if (rdr1.Read())
            {
                prodid = rdr1.GetValue(0).ToString();
            }
            rdr1.Close();

            rdr1 = objprod.GetRecordSet("select prod_id from products where prod_name='" + product.ProductName + "' and pack_type='" + product.ProductPack + "'");
            if (rdr1.Read())
            {
                oldProdID = rdr1.GetValue(0).ToString();
            }
            rdr1.Close();

            //UpdateBatchNo1();

            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            InventoryClass obj = new InventoryClass();
            SqlCommand cmd;

            rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='" + product.Invoice_No + "' and trans_type='Purchase invoice' and batch_id=0");
            while (rdr.Read())
            {
                Con.Open();
                cmd = new SqlCommand("update StockMaster_Batch set Receipt=Receipt-" + rdr["Qty"].ToString() + ",Closing_Stock=Closing_Stock-" + rdr["Qty"].ToString() + " where ProductID='" + rdr["Prod_ID"].ToString() + "' and Batch_ID='0'", Con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Con.Close();
            }
            rdr.Close();

            string[] arr = product.Batch.Split(new char[] { ',' }, product.Batch.Length);
            if (arr[1] != "''")
            {
                dbobj.Insert_or_Update("delete from batch_transaction where trans_ID='" + Inv_No + "' and trans_type='Purchase Invoice' and prod_id='" + prodid + "'", ref x);
            }

            var dt = System.Convert.ToDateTime(DateTime.Now.ToString()).ToShortDateString();

            rdr1 = objprod.GetRecordSet("select max(sno)+1 from Batch_Transaction");
            if (rdr1.Read())
            {
                if (rdr1.GetValue(0).ToString() != null && rdr1.GetValue(0).ToString() != "")
                    SNo = int.Parse(rdr1.GetValue(0).ToString());
                else
                    SNo = 1;
            }
            else
                SNo = 1;
            rdr1.Close();
            int tot_bat_qty = 0;

            for (int n = 0; n < arr.Length; n += 3)
            {
                if (arr[n].ToString() != "''")
                {
                    rdr1 = objprod.GetRecordSet("select * from BatchNo where batch_id=" + arr[n + 1].ToString() + " and prod_id='" + oldProdID + "'");
                    if (rdr1.Read())
                    {
                        string BQty = arr[n + 2].ToString();
                        rdr = objprod1.GetRecordSet("select * from StockMaster_batch where batch_id=" + rdr1.GetValue(0).ToString() + " and productid='" + oldProdID.ToString() + "'");
                        if (rdr.Read())
                        {
                            double op_stk = Convert.ToDouble(rdr.GetValue(3).ToString());
                            double receipt = Convert.ToDouble(rdr.GetValue(4).ToString());
                            string qty1 = arr[n + 2].ToString();
                            qty1 = qty1.Substring(1, (qty1.Length) - 2); ;

                            receipt = Convert.ToDouble(qty1.ToString());
                            double Sales = Convert.ToDouble(rdr.GetValue(5).ToString());
                            double Cl_stk = Convert.ToDouble(rdr.GetValue(6).ToString());
                            Cl_stk = (op_stk + receipt) - Sales;

                            dbobj.Insert_or_Update("update StockMaster_Batch set stock_date='" + dt + "',opening_stock=" + op_stk + ",receipt=" + receipt.ToString() + ", sales=" + Sales + ",closing_stock=" + Math.Round(Cl_stk) + " where productid=" + prodid + " and batch_id=" + rdr1.GetValue(0).ToString(), ref x);

                            dbobj.Insert_or_Update("insert into Batch_Transaction values(" + (SNo++) + ",'" + Inv_No + "','Purchase Invoice','" + dt + "','" + prodid + "'," + rdr1.GetValue(0).ToString() + "," + arr[n + 2].ToString() + "," + Cl_stk + ")", ref x);
                            dbobj.Insert_or_Update("update BatchNo set qty=" + Cl_stk + " where prod_id=" + prodid + " and batch_id=" + rdr1.GetValue(0).ToString(), ref x);

                            BQty = BQty.Substring(1, (BQty.Length) - 2);
                            tot_bat_qty += Convert.ToInt32(BQty);
                        }
                        rdr.Close();
                    }
                    else
                    {
                        string BQty = arr[n + 2].ToString();
                        dbobj.Insert_or_Update("insert into BatchNo values(" + (++BatID) + "," + arr[n].ToString() + ",'" + prodid + "','" + dt + "'," + arr[n + 2].ToString() + "," + prodid + ")", ref x);
                        dbobj.Insert_or_Update("insert into StockMaster_Batch values(" + prodid + ",'" + BatID + "','" + dt + "',0," + arr[n + 2].ToString() + ",0," + arr[n + 2].ToString() + ",0,0)", ref x);

                        if (arr[n + 1].ToString() != "''" && arr[n + 1].ToString() != "'0'")
                            dbobj.Insert_or_Update("insert into Batch_Transaction values(" + (SNo++) + ",'" + Inv_No + "','Purchase Invoice','" + dt + "','" + prodid + "'," + arr[n + 1].ToString() + "," + arr[n + 2].ToString() + "," + arr[n + 2].ToString() + ")", ref x);//Maintain the closing stock by prod_id on every batch no
                        else
                            dbobj.Insert_or_Update("insert into Batch_Transaction values(" + (SNo++) + ",'" + Inv_No + "','Purchase Invoice','" + dt + "','" + prodid + "'," + BatID + "," + arr[n + 2].ToString() + "," + arr[n + 2].ToString() + ")", ref x);//Maintain the closing stock by prod_id on every batch no

                        BQty = BQty.Substring(1, (BQty.Length) - 2);
                        tot_bat_qty += Convert.ToInt32(BQty);
                    }
                    rdr1.Close();
                }


                else
                {
                    if (arr[n + 2].ToString() != "''")
                    {
                        dbobj.Insert_or_Update("insert into Batch_Transaction values(" + (SNo++) + ",'" + Inv_No + "','Purchase Invoice','" + dt + "','" + prodid + "',0," + arr[n + 2].ToString() + "," + arr[n + 2].ToString() + ")", ref x);

                        dbobj.SelectQuery("select * from StockMaster_Batch where productid='" + prodid + "' and Batch_id=0", ref rdr);
                        if (rdr.HasRows)
                        {
                            dbobj.Insert_or_Update("update StockMaster_Batch set stock_date='" + dt + "',Receipt=" + arr[n + 2].ToString() + ",closing_stock=" + arr[n + 2].ToString() + " where productid='" + prodid + "' and batch_id=0", ref x);
                        }
                        else
                        {
                            dbobj.Insert_or_Update("insert into StockMaster_Batch values(" + prodid + ",'0','" + dt + "',0," + arr[n + 2].ToString() + ",0," + arr[n + 2].ToString() + ",0,0)", ref x);
                        }

                        if ((arr[n + 1].ToString() != "''"))
                        {
                            dbobj.Insert_or_Update("delete from BatchNo where Prod_id='" + prodid + "' and Batch_ID=" + arr[n + 1].ToString() + "", ref x);

                            rdr1 = objprod.GetRecordSet("select * from Batchno where prod_id='" + oldProdID + "' and Batch_ID=" + arr[n + 1].ToString());
                            if (!rdr1.Read())
                            {
                                dbobj.Insert_or_Update("delete from StockMaster_Batch where productid=" + prodid + " and Batch_ID=" + arr[n + 1].ToString(), ref x);
                            }
                            rdr1.Close();
                        }
                    }
                    else
                    {
                        rdr1 = objprod.GetRecordSet("select * from StockMaster_Batch where productid=" + prodid + " and Batch_ID=0 and closing_stock!=0");
                        if (!rdr1.Read())
                        {
                            dbobj.Insert_or_Update("delete from StockMaster_Batch where productid=" + prodid + " and Batch_ID=0", ref x);
                        }
                        rdr1.Close();
                    }
                    break;
                }
            }
        }
        [HttpPost]
        [Route("api/purchase/InsertSeqStockMaster")]
        public void InsertSeqStockMaster(ProductModels product)
        {
            InventoryClass obj = new InventoryClass();
            InventoryClass obj1 = new InventoryClass();
            SqlCommand cmd;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            SqlDataReader rdr1 = null, rdr = null;

            if (product.ProductType != "" || product.ProductName != "" || product.Quantity != "")
            {
                string str = "select Prod_ID from Products where Category='" + product.ProductType + "' and Prod_Name='" + product.ProductName + "' and Pack_Type='" + product.ProductPack + "'";
                rdr = obj.GetRecordSet(str);
                if (rdr.Read())
                {
                    string str1 = "select * from Stock_Master where Productid='" + rdr["Prod_ID"].ToString() + "' order by Stock_date";
                    rdr1 = obj1.GetRecordSet(str1);
                    double OS = 0, CS = 0, k = 0;
                    while (rdr1.Read())
                    {
                        if (k == 0)
                        {
                            OS = double.Parse(rdr1["opening_stock"].ToString());
                            k++;
                        }
                        else
                            OS = CS;
                        CS = OS + double.Parse(rdr1["receipt"].ToString()) - (double.Parse(rdr1["sales"].ToString()) + double.Parse(rdr1["salesfoc"].ToString()));
                        Con.Open();
                        var dt = DateTime.Parse(rdr1["stock_date"].ToString()).Date.ToString("yyyy-MM-dd HH:mm:ss");
                        cmd = new SqlCommand("update Stock_Master set opening_stock='" + OS.ToString() + "', Closing_Stock='" + CS.ToString() + "' where ProductID='" + rdr1["Productid"].ToString() + "' and Stock_Date=convert(datetime,'" + dt + "',101)", Con);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        Con.Close();
                    }
                    rdr1.Close();
                }
                rdr.Close();
            }

        }
        [HttpPost]
        [Route("api/purchase/CustomerUpdate")]
        public void CustomerUpdate(ProductModels product)
        {
            string Vendor_ID = null;
            SqlDataReader rdr = null;
            SqlCommand cmd;
            InventoryClass obj = new InventoryClass();
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            double Bal = 0;
            string BalType = "", str = "";
            int i = 0;
            //*************************
            string sql = "select * from Purchase_Master where Invoice_No='" + product.Invoice_No + "'";
            var SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                Vendor_ID = SqlDtr["Vendor_ID"].ToString();
            }
            SqlDtr.Close();
            //for(int k=0;k<LedgerID.Count;k++)
            //{
            var Invoice_Date = product.InvoiceDate.Date.ToString("yyyy-MM-dd HH:mm:ss");
            rdr = obj.GetRecordSet("select top 1 Entry_Date from AccountsLedgerTable where Ledger_ID=(select Ledger_ID from Ledger_Master l,supplier s where Supp_Name=Ledger_Name and Supp_ID='" + Vendor_ID + "') and Entry_Date<=convert(datetime,'" + Invoice_Date + "',101) order by entry_date desc");
            if (rdr.Read())
            {
                var dt = DateTime.Parse(rdr.GetValue(0).ToString()).Date.ToString("yyyy-MM-dd HH:mm:ss"); 
                str = "select * from AccountsLedgerTable where Ledger_ID=(select Ledger_ID from Ledger_Master l,supplier s where Supp_Name=Ledger_Name and Supp_ID='" + Vendor_ID + "') and Entry_Date>=convert(datetime,'" + dt + "',101) order by entry_date";
            }
            else
                str = "select * from AccountsLedgerTable where Ledger_ID=(select Ledger_ID from Ledger_Master l,supplier s where Supp_Name=Ledger_Name and Supp_ID='" + Vendor_ID + "') order by entry_date";
            rdr.Close();
            //*************************
            //string str="select * from AccountsLedgerTable where Ledger_ID='"+LedgerID+"' order by entry_date";
            rdr = obj.GetRecordSet(str);
            Bal = 0;
            BalType = "";
            i = 0;
            while (rdr.Read())
            {
                if (i == 0)
                {
                    BalType = rdr["Bal_Type"].ToString();
                    Bal = double.Parse(rdr["Balance"].ToString());
                    i++;
                }
                else
                {
                    if (double.Parse(rdr["Credit_Amount"].ToString()) != 0)
                    {
                        if (BalType == "Cr")
                        {
                            string ss = rdr["Credit_Amount"].ToString();
                            Bal += double.Parse(rdr["Credit_Amount"].ToString());
                            BalType = "Cr";
                        }
                        else
                        {
                            string ss = rdr["Credit_Amount"].ToString();
                            Bal -= double.Parse(rdr["Credit_Amount"].ToString());
                            if (Bal < 0)
                            {
                                Bal = double.Parse(Bal.ToString().Substring(1));
                                BalType = "Cr";
                            }
                            else
                                BalType = "Dr";
                        }
                    }
                    else if (double.Parse(rdr["Debit_Amount"].ToString()) != 0)
                    {
                        if (BalType == "Dr")
                        {
                            string ss = rdr["Debit_Amount"].ToString();
                            Bal += double.Parse(rdr["Debit_Amount"].ToString());
                        }
                        else
                        {
                            string ss = rdr["Debit_Amount"].ToString();
                            Bal -= double.Parse(rdr["Debit_Amount"].ToString());
                            if (Bal < 0)
                            {
                                Bal = double.Parse(Bal.ToString().Substring(1));
                                BalType = "Dr";
                            }
                            else
                                BalType = "Cr";
                        }
                    }
                    Con.Open();
                    string str11 = "update AccountsLedgerTable set Balance='" + Bal.ToString() + "',Bal_Type='" + BalType + "' where Ledger_ID='" + rdr["Ledger_ID"].ToString() + "' and Particulars='" + rdr["Particulars"].ToString() + "'";
                    cmd = new SqlCommand("update AccountsLedgerTable set Balance='" + Bal.ToString() + "',Bal_Type='" + BalType + "' where Ledger_ID='" + rdr["Ledger_ID"].ToString() + "' and Particulars='" + rdr["Particulars"].ToString() + "'", Con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Con.Close();
                }
            }
            rdr.Close();
            //*************************
            rdr = obj.GetRecordSet("select top 1 EntryDate from VendorLedgerTable where VendorID='" + Vendor_ID.ToString() + "' and EntryDate<=convert(datetime,'" + Invoice_Date + "') order by entrydate desc");
            if (rdr.Read())
            {
                var date = DateTime.Parse(rdr.GetValue(0).ToString()).Date.ToString("yyyy-MM-dd HH:mm:ss"); ;
                str = "select * from VendorLedgerTable where VendorID='" + Vendor_ID + "' and EntryDate>=convert(datetime,'" + date + "') order by entrydate";
            }
            else
                str = "select * from VendorLedgerTable where VendorID='" + Vendor_ID + "' order by entrydate";
            rdr.Close();
            //*************************
            //string str1="select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer c,Ledger_Master l where Ledger_Name=Cust_Name and Ledger_ID='"+LedgerID+"') order by entrydate";
            rdr = obj.GetRecordSet(str);
            Bal = 0;
            i = 0;
            BalType = "";
            while (rdr.Read())
            {
                if (i == 0)
                {
                    BalType = rdr["BalanceType"].ToString();
                    Bal = double.Parse(rdr["Balance"].ToString());
                    i++;
                }
                else
                {
                    if (double.Parse(rdr["CreditAmount"].ToString()) != 0)
                    {
                        if (BalType == "Cr.")
                        {
                            Bal += double.Parse(rdr["CreditAmount"].ToString());
                            BalType = "Cr.";
                        }
                        else
                        {
                            Bal -= double.Parse(rdr["CreditAmount"].ToString());
                            if (Bal < 0)
                            {
                                Bal = double.Parse(Bal.ToString().Substring(1));
                                BalType = "Cr.";
                            }
                            else
                                BalType = "Dr.";
                        }
                    }
                    else if (double.Parse(rdr["DebitAmount"].ToString()) != 0)
                    {
                        if (BalType == "Dr.")
                            Bal += double.Parse(rdr["DebitAmount"].ToString());
                        else
                        {
                            Bal -= double.Parse(rdr["DebitAmount"].ToString());
                            if (Bal < 0)
                            {
                                Bal = double.Parse(Bal.ToString().Substring(1));
                                BalType = "Dr.";
                            }
                            else
                                BalType = "Cr.";
                        }
                    }
                    Con.Open();
                    cmd = new SqlCommand("update VendorLedgerTable set Balance='" + Bal.ToString() + "',BalanceType='" + BalType + "' where VendorID='" + rdr["VendorID"].ToString() + "' and Particular='" + rdr["Particular"].ToString() + "'", Con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Con.Close();
                }
            }
            rdr.Close();
        }
        [HttpGet]
        [Route("api/purchase/GetPurchaseInvoice")]
        public PurchaseModels GetPurchaseInvoice(string invoiceNo)
        {
            InventoryClass obj = new InventoryClass();
            InventoryClass obj1 = new InventoryClass();
            PurchaseModels purchase = new PurchaseModels();
            SqlDataReader SqlDtr, rdr;
            string sql;
            string strDate, strDate1;
            int i = 0;
            sql = "select * from Purchase_Master where Invoice_No='" + invoiceNo + "'";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                purchase.InvoiceDate = DateTime.Parse(SqlDtr.GetValue(1).ToString());
                strDate = SqlDtr.GetValue(1).ToString().Trim();
                ////txtVInnvoiceNo.Enabled=false;
                int pos = strDate.IndexOf(" ");
                if (pos != -1)
                {
                    strDate = strDate.Substring(0, pos);
                }
                else
                {
                    strDate = "";
                }
                strDate1 = SqlDtr.GetValue(6).ToString().Trim();
                pos = strDate1.IndexOf(" ");
                if (pos != -1)
                {
                    strDate1 = strDate1.Substring(0, pos);
                }
                else
                {
                    strDate1 = "";
                }
                //purchase.InvoiceDate = GenUtil.str2DDMMYYYY(strDate);

                purchase.ModeofPayment = SqlDtr.GetValue(2).ToString();
                purchase.VehicleNo = SqlDtr.GetValue(4).ToString();
                purchase.VendorInvoiceNo = SqlDtr.GetValue(5).ToString();
                //tempVndrInvoiceNo.Value = SqlDtr.GetValue(5).ToString();
                //Coment By vikas 7.3.2013 Fore Early Birld Tax txtVInvoiceDate.Text=GenUtil.str2DDMMYYYY(strDate1);
                purchase.VendorInvoiceDate = strDate1;
                purchase.GrandTotal = SqlDtr.GetValue(7).ToString();


                //double ETFOC = double.Parse(SqlDtr["FOC_Discount"].ToString()) * 2 / 100;
                //coment by vikas 23.11.2012 txtDisc.Text=GenUtil.strNumericFormat(SqlDtr.GetValue(8).ToString()); 
                //txtDisc.Text=GenUtil.strNumericFormat(SqlDtr.GetValue(8).ToString());
                purchase.DiscountType = SqlDtr.GetValue(9).ToString();

                /* Comment by vikas 23.11.2012 if(DropDiscType.SelectedIndex==1)
                    txtTotalDisc.Text=GenUtil.strNumericFormat(System.Convert.ToString(double.Parse(SqlDtr.GetValue(7).ToString())*double.Parse(SqlDtr.GetValue(8).ToString())/100));*/



                purchase.NetAmount = SqlDtr.GetValue(10).ToString();
                //txtNetAmount.Text = GenUtil.strNumericFormat(txtNetAmount.Text.ToString());

                purchase.PromoScheme = SqlDtr.GetValue(11).ToString();
                purchase.Remark = SqlDtr.GetValue(12).ToString();
                purchase.EntryBy = SqlDtr.GetValue(13).ToString();
                purchase.EntryTime = DateTime.Parse(SqlDtr.GetValue(14).ToString());
                purchase.CashDiscount = SqlDtr.GetValue(15).ToString();
                //txtCashDisc.Text = GenUtil.strNumericFormat(Request.Form["txtCashDisc"].ToString()); 

                purchase.CashDiscType = SqlDtr.GetValue(16).ToString();
                purchase.VATAmount = SqlDtr.GetValue(17).ToString();
                purchase.CGSTAmount = SqlDtr["CGST_Amount"].ToString();
                purchase.SGSTAmount = SqlDtr["SGST_Amount"].ToString();
                purchase.Birdless = SqlDtr["BirdDiscount_Less"].ToString();
                purchase.Tradeless = SqlDtr["TradeDiscount_Less"].ToString();

                //03.07.09 double TotalCashDiscount=double.Parse(SqlDtr["Grand_Total"].ToString())+double.Parse(SqlDtr["Entry_Tax1"].ToString())-(double.Parse(SqlDtr["Trade_Discount"].ToString())+double.Parse(SqlDtr["FOC_Discount"].ToString())+double.Parse(SqlDtr["Discount"].ToString())+double.Parse(SqlDtr["Fixed_Discount_Type"].ToString())+double.Parse(SqlDtr["Ebird_Discount"].ToString())+ETFOC);

                /*******Temp***********
                double G_Tot=double.Parse(SqlDtr["Grand_Total"].ToString());
                double E_Tax=double.Parse(SqlDtr["Entry_Tax1"].ToString());
                double Trade_Disc=double.Parse(SqlDtr["Trade_Discount"].ToString());
                double Foc_Disc=double.Parse(SqlDtr["FOC_Discount"].ToString());
                double Disc=double.Parse(SqlDtr["Discount"].ToString());
                double Fixd_Disc_Type=double.Parse(SqlDtr["Fixed_Discount_Type"].ToString());
                double Fixd_Disc=double.Parse(SqlDtr["Fixed_Discount"].ToString());
                double EB_Disc=double.Parse(SqlDtr["Ebird_Discount"].ToString());
                ******************/

                /*******Add by vikas 6.12.2012****************************/
                double Fixed_Disc_Amount = 0;
                if (SqlDtr["Fixed_Disc_Amount"].ToString() != null && SqlDtr["Fixed_Disc_Amount"].ToString() != "")
                    purchase.FixedDiscount = SqlDtr["Fixed_Disc_Amount"].ToString();
                else
                    purchase.FixedDiscount = null;
                /*******End by vikas 6.12.2012****************************/
                double TotalDiscount = 0;//double.Parse(SqlDtr["Grand_Total"].ToString()) + double.Parse(SqlDtr["Entry_Tax1"].ToString()) - (double.Parse(SqlDtr["Trade_Discount"].ToString()) + double.Parse(SqlDtr["FOC_Discount"].ToString()) + double.Parse(SqlDtr["Fixed_Discount"].ToString()) + double.Parse(SqlDtr["Ebird_Discount"].ToString()) + ETFOC - TotalCashDiscount + Fixed_Disc_Amount);
                purchase.Discount = SqlDtr.GetValue(8).ToString();
                purchase.Totalqtyltr = SqlDtr["totalqtyltr"].ToString();

                //if (DropDiscType.SelectedIndex == 0)
                //{
                //    txtDisc.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(8).ToString());
                //    TotalDiscount = double.Parse(GenUtil.strNumericFormat(SqlDtr.GetValue(8).ToString())) * double.Parse(GenUtil.strNumericFormat(SqlDtr["totalqtyltr"].ToString()));
                //    txtTotalDisc.Text = Convert.ToString(Math.Round(TotalDiscount));
                //}
                //else
                //{
                //    txtDisc.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(8).ToString());
                //    if (SqlDtr["Discount_Type"].ToString() == "Per")
                //        TotalDiscount = double.Parse(SqlDtr["Grand_Total"].ToString()) * double.Parse(SqlDtr["Discount"].ToString()) / 100;
                //    txtTotalDisc.Text = GenUtil.strNumericFormat(TotalDiscount.ToString());
                //}
                //Coment by vikas 2.1.2013 double TotalCashDiscount=double.Parse(SqlDtr["Grand_Total"].ToString())+double.Parse(SqlDtr["Entry_Tax1"].ToString())-(double.Parse(SqlDtr["Trade_Discount"].ToString())+double.Parse(SqlDtr["FOC_Discount"].ToString())+double.Parse(SqlDtr["Discount"].ToString())+double.Parse(SqlDtr["Fixed_Discount_Type"].ToString())+double.Parse(SqlDtr["Fixed_Discount"].ToString())+double.Parse(SqlDtr["Ebird_Discount"].ToString())+ETFOC);
                purchase.TradeDiscount = SqlDtr["Trade_Discount"].ToString();
                purchase.FocDiscount = SqlDtr["FOC_Discount"].ToString();
                purchase.FixedDiscount = SqlDtr["Fixed_Discount"].ToString();
                purchase.EbirdDiscount = SqlDtr["Ebird_Discount"].ToString();
                //double TotalCashDiscount = double.Parse(SqlDtr["Grand_Total"].ToString()) - tradeDisc - double.Parse(SqlDtr["FOC_Discount"].ToString()) - TotalDiscount + double.Parse(SqlDtr["Fixed_Discount"].ToString()) + double.Parse(SqlDtr["Ebird_Discount"].ToString()) + Fixed_Disc_Amount;

                //if (SqlDtr["Cash_Disc_Type"].ToString() == "Per")
                //    TotalCashDiscount = TotalCashDiscount * double.Parse(SqlDtr["Cash_Discount"].ToString()) / 100;
                //else
                //    TotalCashDiscount = double.Parse(GenUtil.strNumericFormat(SqlDtr.GetValue(15).ToString())) * double.Parse(GenUtil.strNumericFormat(SqlDtr["totalqtyltr"].ToString()));

                //txtTotalCashDisc.Text = GenUtil.strNumericFormat(TotalCashDiscount.ToString());



                /******Add by vikas 23.11.2012****************/
                //coment by vikas 6.12.2012 double TotalDiscount=double.Parse(SqlDtr["Grand_Total"].ToString())+double.Parse(SqlDtr["Entry_Tax1"].ToString())-(double.Parse(SqlDtr["Trade_Discount"].ToString())+double.Parse(SqlDtr["FOC_Discount"].ToString())+double.Parse(SqlDtr["Fixed_Discount"].ToString())+double.Parse(SqlDtr["Ebird_Discount"].ToString())+ETFOC-TotalCashDiscount+double.Parse(SqlDtr["Fixed_Disc_Amount"].ToString()));





                /******end****************/
                //****************
                //txttradedis.Text=GenUtil.strNumericFormat(SqlDtr.GetValue(18).ToString()); 
                purchase.TradedisAmount = SqlDtr.GetValue(19).ToString();
                purchase.Ebird = SqlDtr.GetValue(20).ToString();
                purchase.EbirdAmount = SqlDtr.GetValue(21).ToString();
                purchase.FocDiscount = SqlDtr["Foc_Discount"].ToString();
                purchase.FocDiscountType = SqlDtr.GetValue(25).ToString();
                //txtentry.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(22).ToString()); 
                //dropentry.SelectedIndex= dropentry.Items.IndexOf((dropentry.Items.FindByValue(SqlDtr.GetValue(23).ToString())));
                purchase.Fixed = SqlDtr.GetValue(26).ToString();
                //dropfixed.SelectedIndex= dropfixed.Items.IndexOf((dropfixed.Items.FindByValue(SqlDtr.GetValue(27).ToString())));
                purchase.FixedAmount = SqlDtr.GetValue(27).ToString();
                //txttotalqtyltr.Text=GenUtil.strNumericFormat(SqlDtr.GetValue(28).ToString());
                purchase.Totalqtyltr1 = SqlDtr["totalqtyltr"].ToString();
                //***************************

                /*********Add by vikas 5.11.2012 **********************/
                if (SqlDtr["fixed_Disc_New"] != null && SqlDtr["fixed_Disc_New"].ToString() != "")
                    purchase.NewFixeddisc = SqlDtr["fixed_Disc_New"].ToString().Trim();
                else
                    purchase.NewFixeddisc = "0";

                if (SqlDtr["fixed_Disc_Amount"] != null && SqlDtr["fixed_Disc_Amount"].ToString() != "")
                    purchase.NewFixeddiscAmount = SqlDtr["fixed_Disc_Amount"].ToString().Trim();
                else
                    purchase.NewFixeddiscAmount = "0";
                /*********End**********************/

                //if (txtVAT.Text.Trim() == "0")
                //{
                //    Yes.Checked = false;
                //    No.Checked = true;
                //}
                //else
                //{
                //    No.Checked = false;
                //    Yes.Checked = true;
                //}
                //Vendor_ID = SqlDtr["Vendor_ID"].ToString();
                //CheckMode=SqlDtr["Mode_of_Payment"].ToString();

                //txtAddDis.Text = SqlDtr.GetValue(26).ToString();   //Add by vikas 30.06.09

                /*************************
                double Net_Amount=double.Parse(txtGrandTotal.Text.ToString())+ETFOC+Fixed_Disc_Amount+double.Parse(txtebirdamt.Text)+double.Parse(txttradedisamt.Text)+double.Parse(txtfixdiscamount.Text)+double.Parse(txtVAT.Text);
                txtNetAmount.Text=	Net_Amount.ToString();							//Add by Vikas7.3.2013
                /******************************/

            }
            SqlDtr.Close();
            //sql="select s.Supp_Name,s.City from Supplier as s, Purchase_Master as p where p.Invoice_No='"+DropInvoiceNo.SelectedValue +"' and S.Supp_ID = P.Vendor_ID ";
            //sql="select s.Supp_Name,s.City from Supplier as s, Purchase_Master as p where p.Vndr_Invoice_No='"+DropInvoiceNo.SelectedValue +"' and S.Supp_ID = P.Vendor_ID ";
            sql = "select s.Supp_Name,s.City from Supplier as s, Purchase_Master as p where p.Invoice_No='" + invoiceNo + "' and S.Supp_ID = P.Vendor_ID ";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                purchase.VendorID = SqlDtr.GetValue(0).ToString();
                purchase.Place = SqlDtr.GetValue(1).ToString();
            }
            SqlDtr.Close();
            return purchase;
        }

        [HttpPost]
        [Route("api/purchase/GetProductDetails")]
        public List<ProductModels> GetProductDetails(ProductModels pro)
        {
            InventoryClass obj = new InventoryClass();
            InventoryClass obj1 = new InventoryClass();
            List<ProductModels> products = new List<ProductModels>();
            #region Get Data from Purchase Details Table regarding Invoice No.
            int TotalQty = 0;
            int i = 0;
            /*
            sql="select p.Category,p.Prod_Name,p.Pack_Type,	pd.qty,pd.rate,pd.amount,pd.foc"+
                " from Products p, Purchase_Details pd"+
                " where p.Prod_ID=pd.prod_id and pd.invoice_no='"+ DropInvoiceNo.SelectedItem.Value +"'" ;
            */
            string sql = "select p.Category,p.Prod_Name,p.Pack_Type,	pd.qty,pd.rate,pd.amount,pd.foc,p.Prod_Code,pd.Prod_ID,Invoice_Date,pd.Discount" +
                " from Products p, Purchase_Details pd, Purchase_Master pm" +
                //" where pm.Invoice_No=pd.Invoice_No and p.Prod_ID=pd.prod_id and pd.invoice_no=(select Invoice_No from Purchase_Master where Vndr_Invoice_No='"+ DropInvoiceNo.SelectedItem.Value +"')";
                " where pm.Invoice_No=pd.Invoice_No and p.Prod_ID=pd.prod_id and pm.invoice_no='" + pro.Invoice_No + "' order by sno";

            var SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                ProductModels product = new ProductModels();
                //Rate[i].Enabled = true;
                //Qty[i].Enabled = true;
                //Amount[i].Enabled = true;
                //chkfoc[i].Enabled = true;
                product.ProductType = SqlDtr.GetValue(7).ToString() + ":" + SqlDtr.GetValue(1).ToString() + ":" + SqlDtr.GetValue(2).ToString();
                product.Quantity = SqlDtr.GetValue(3).ToString();
                if (product.Quantity != "")
                    TotalQty += System.Convert.ToInt32(product.Quantity);

                //Quantity[i].Text = Request.Form[Qty[i].ID.ToString()].ToString();

                product.Rate = SqlDtr.GetValue(4).ToString();
                product.Amount = SqlDtr.GetValue(5).ToString();
                product.Category = SqlDtr.GetValue(0).ToString();
                product.ProductName = SqlDtr.GetValue(1).ToString();
                product.ProductPack = SqlDtr.GetValue(2).ToString();
                product.Quantity = SqlDtr.GetValue(3).ToString();
                product.FOC = SqlDtr.GetValue(6).ToString();
                string invdt = pro.InvoiceDate.ToString("MM/dd/yyyy HH:mm:ss");

                string strstr = "select Discount,DiscountType from stktSchDiscount where prodid='" + SqlDtr["Prod_ID"].ToString() + "' and cast(floor(cast(cast(Datefrom as datetime) as float)) as datetime)<='" + invdt + "' and cast(floor(cast(cast(Dateto as datetime) as float)) as datetime)>='" + invdt + "'";
                var rdr = obj1.GetRecordSet(strstr);
                if (rdr.Read())
                {
                    product.TempStktSchDis = rdr["Discount"].ToString() + ":" + rdr["DiscountType"].ToString();
                }
                else
                    product.TempStktSchDis = "";
                rdr.Close();

                strstr = "select Discount,DiscountType from Per_Discount where prodid='" + SqlDtr["Prod_ID"].ToString() + "' and schname='Primary(LTR&% Scheme)' and cast(floor(cast(cast(Datefrom as datetime) as float)) as datetime)<='" + invdt + "' and cast(floor(cast(cast(Dateto as datetime) as float)) as datetime)>='" + invdt + "'";
                rdr = obj1.GetRecordSet(strstr);
                if (rdr.Read())
                {
                    product.TempSchDis = rdr["Discount"].ToString() + ":" + rdr["DiscountType"].ToString();
                }
                else
                    product.TempSchDis = "";
                rdr.Close();

                /*****03.07.09**Add by vikas *******************/
                strstr = "select Discount,DiscountType from Per_Discount where prodid='" + SqlDtr["Prod_ID"].ToString() + "' and schname='Primary(LTR&% Addl Scheme)' and cast(floor(cast(cast(Datefrom as datetime) as float)) as datetime)<='" + invdt + "' and cast(floor(cast(cast(Dateto as datetime) as float)) as datetime)>='" + invdt + "'";
                rdr = obj1.GetRecordSet(strstr);
                if (rdr.Read())
                {
                    product.TempSchAddDis = rdr["Discount"].ToString() + ":" + rdr["DiscountType"].ToString();
                }
                else
                    product.TempSchAddDis = "";
                rdr.Close();
                /**************************/

                product.Discount = SqlDtr["Discount"].ToString();

                product.TempDiscount = SqlDtr["Discount"].ToString();
                rdr.Close();
                products.Add(product);
                //***
                i++;

            }
            //txttotalqty.Text = System.Convert.ToString(TotalQty);

            /*****Add by vikas 10.12.2012********************/
            //Earlybird_dis();
            /******end*******************/
            /*Hide By Mahesh 16.08.007
            while(i<12)
            {
                ProdType[i].SelectedIndex=0;
                ProdType[i].Enabled = false; 
                ProdName[i].SelectedIndex=0;
                ProdName[i].Enabled = false; 
                PackType[i].Items.Clear();
                PackType[i].Items.Add("Select");
                PackType[i].SelectedIndex=(PackType[i].Items.IndexOf((PackType[i].Items.FindByValue ("Select"))));
                PackType[i].Enabled = false; 
                Qty[i].Text="";
                Qty[i].Enabled = false;
                Quantity[i].Text = "";
                Quantity[i].Enabled = false; 
                Rate[i].Text="";
                Rate[i].Enabled = false;
                Amount[i].Text="";
                Amount[i].Enabled = false;
                chkfoc[i].Enabled=false;//*bhal*
                //***********
                ProductType[i]="";
                ProductName[i]="";
                ProductPack[i]="";
                ProductQty[i]="";
                //***********
                i++;
            }
            */
            SqlDtr.Close();
            return products;
            #endregion
        }
        [HttpPost]
        [Route("api/purchase/DeletePurchaseInvoice")]
        public void DeletePurchaseInvoice(PurchaseModels purchase)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader rdr;
            SqlCommand cmd;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            //string st="select Invoice_No from Purchase_Master where Vndr_Invoice_No='"+DropInvoiceNo.SelectedItem.Text+"'";
            string st = "select Invoice_No from Purchase_Master where Invoice_No='" + purchase.InvoiceNo + "'";
            rdr = obj.GetRecordSet(st);
            if (rdr.Read())
            {
                Con.Open();
                cmd = new SqlCommand("delete from Vendorledgertable where Particular='Purchase Invoice (" + rdr["Invoice_No"].ToString() + ")'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();
                Con.Open();
                cmd = new SqlCommand("delete from Accountsledgertable where Particulars='Purchase Invoice (" + rdr["Invoice_No"].ToString() + ")'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();
            }
            rdr.Close();
            string str1 = "select * from VendorLedgerTable where VendorID=(select Supp_ID from Supplier where Supp_Name='" + purchase.VendorID + "') order by entrydate";
            rdr = obj.GetRecordSet(str1);
            double Bal = 0;
            while (rdr.Read())
            {
                if (rdr["BalanceType"].ToString().Equals("Dr."))
                    Bal += double.Parse(rdr["DebitAmount"].ToString()) - double.Parse(rdr["CreditAmount"].ToString());
                else
                    Bal += double.Parse(rdr["CreditAmount"].ToString()) - double.Parse(rdr["DebitAmount"].ToString());
                if (Bal.ToString().StartsWith("-"))
                    Bal = double.Parse(Bal.ToString().Substring(1));
                Con.Open();
                cmd = new SqlCommand("update VendorLedgerTable set Balance='" + Bal.ToString() + "' where VendorID='" + rdr["VendorID"].ToString() + "' and Particular='" + rdr["Particular"].ToString() + "'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();
            }
            rdr.Close();
            Con.Open();
            //cmd = new SqlCommand("delete from Purchase_Master where Vndr_Invoice_No='"+DropInvoiceNo.SelectedItem.Text+"'",Con);
            cmd = new SqlCommand("delete from Purchase_Master where Invoice_No='" + purchase.InvoiceNo + "'", Con);
            cmd.ExecuteNonQuery();
            Con.Close();
            cmd.Dispose();

            string str = "select * from AccountsLedgerTable where Ledger_ID=(select Ledger_ID from Ledger_Master where Ledger_Name='" + purchase.VendorID + "') order by entry_date";
            rdr = obj.GetRecordSet(str);
            Bal = 0;
            while (rdr.Read())
            {
                if (rdr["Bal_Type"].ToString().Equals("Dr"))
                    Bal += double.Parse(rdr["Debit_Amount"].ToString()) - double.Parse(rdr["Credit_Amount"].ToString());
                else
                    Bal += double.Parse(rdr["Credit_Amount"].ToString()) - double.Parse(rdr["Debit_Amount"].ToString());
                if (Bal.ToString().StartsWith("-"))
                    Bal = double.Parse(Bal.ToString().Substring(1));
                Con.Open();
                cmd = new SqlCommand("update AccountsLedgerTable set Balance='" + Bal.ToString() + "' where Ledger_ID='" + rdr["Ledger_ID"].ToString() + "' and Particulars='" + rdr["Particulars"].ToString() + "'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();
            }
            rdr.Close();

        }


        [HttpPost]
        [Route("api/purchase/DeleteProductDetails")]
        public void DeleteProductDetails(List<ProductModels> products)
        {
            foreach (var product in products)
            {
                InventoryClass obj = new InventoryClass();
                SqlCommand cmd;
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);

                Con.Open();
                string invdt = product.InvoiceDate.ToString("MM/dd/yyyy HH:mm:ss");
                cmd = new SqlCommand("update Stock_Master set receipt=receipt-'" + double.Parse(product.Quantity) + "',closing_stock=closing_stock-'" + double.Parse(product.Quantity) + "' where ProductID=(select Prod_ID from Products where Category='" + product.ProductType + "' and Prod_Name='" + product.ProductName + "' and Pack_Type='" + product.ProductPack + "') and cast(floor(cast(cast(Stock_Date as datetime) as float)) as datetime)='" + invdt + "'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();
            }


        }

        [HttpGet]
        [Route("api/purchase/UpdateBatchNo")]
        public void UpdateBatchNo(string invoiceNo)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            InventoryClass obj = new InventoryClass();
            SqlDataReader rdr;
            SqlCommand cmd;
            rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='" + invoiceNo + "' and trans_Type='Purchase Invoice'");
            while (rdr.Read())
            {
                //******************************
                Con.Open();
                cmd = new SqlCommand("update StockMaster_Batch set Sales=Sales-" + rdr["Qty"].ToString() + ",Closing_Stock=Closing_Stock+" + rdr["Qty"].ToString() + " where ProductID='" + rdr["Prod_ID"].ToString() + "' and Batch_ID='" + rdr["Batch_ID"].ToString() + "'", Con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Con.Close();
                //*****************************
                Con.Open();
                cmd = new SqlCommand("update BatchNo set Qty=Qty+" + rdr["Qty"].ToString() + " where Prod_ID='" + rdr["Prod_ID"].ToString() + "' and Batch_ID='" + rdr["Batch_ID"].ToString() + "'", Con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Con.Close();
            }
            rdr.Close();
            Con.Open();
            //cmd = new SqlCommand("delete Batch_Transaction where Trans_ID='"+DropInvoiceNo.SelectedItem.Text+"' and Trans_Type='Purchase Invoice'",Con);
            cmd = new SqlCommand("delete Batch_Transaction where Trans_ID='" + invoiceNo + "' and Trans_Type='Purchase Invoice'", Con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            Con.Close();
        }

        [HttpPost]
        [Route("api/purchase/PrePrintReport")]
        public List<ProductModels> PrePrintReport(List<ProductModels> products)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr = null;
            List<ProductModels> prodcts = new List<ProductModels>();

            string[] arrProdType = new string[3];

            foreach (var product in products)
            {
                if (product.Category != null)
                {
                    ProductModels prodct = new ProductModels();
                    string str = "select invoice_date from purchase_master where invoice_no=" + product.Invoice_No + "";
                    SqlDtr = obj.GetRecordSet(str);
                    if (SqlDtr.Read())
                        prodct.InvoiceDate = DateTime.Parse(SqlDtr.GetValue(0).ToString());
                    SqlDtr.Close();
                    if (product.Category.IndexOf(":") > 0)
                        arrProdType = product.Category.Split(new char[] { ':' }, product.Category.Length);
                    else
                    {
                        arrProdType[0] = "";
                        arrProdType[1] = "";
                        arrProdType[2] = "";
                    }
                    str = "select Prod_Code,Total_Qty from Products where Prod_Name='" + arrProdType[1].ToString() + "' and Pack_Type='" + arrProdType[2].ToString() + "'";
                    SqlDtr = obj.GetRecordSet(str);
                    if (SqlDtr.Read())
                    {
                        prodct.ProductCode = SqlDtr.GetValue(0).ToString();
                        prodct.Package_Type = SqlDtr.GetValue(1).ToString();
                        prodct.ProductName = arrProdType[1] + ":" + arrProdType[2];
                    }
                    else
                    {
                        prodct.ProductCode = "";
                        prodct.Package_Type = "";
                        prodct.ProductName = "";
                    }
                    SqlDtr.Close();
                    prodcts.Add(prodct);
                }
            }
            return prodcts;
        }

        [HttpPost]
        [Route("api/purchase/PrePrintProductDetails")]
        public ProductModels PrePrintProductDetails(ProductModels product)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr = null;
            List<ProductModels> prodcts = new List<ProductModels>();
            string str = "select b.batch_no,bt.qty from batch_transaction bt,batchno b where b.prod_id=bt.prod_id and b.prod_id=(select prod_id from products where Prod_Code='" + product.ProductCode + "' and Prod_Name='" + product.ProductName + "' and Pack_Type='" + product.Package_Type + "') and b.batch_id=bt.batch_id and bt.trans_id='" + product.Invoice_No + "' and trans_type='Purchase Invoice'";
            SqlDtr = obj.GetRecordSet(str);
            ProductModels prodct = new ProductModels();
            if (SqlDtr.HasRows)
            {
                while (SqlDtr.Read())
                {
                    prodct.Quantity = SqlDtr.GetValue(1).ToString();
                }
            }
            SqlDtr.Close();

            
            return prodct;
        }

        [HttpGet]
        [Route("api/purchase/GetSupplierDetails")]
        public PurchaseModels GetSupplierDetails(string vendorID)
        {
            SqlDataReader SqlDtr = null;
            PurchaseModels purchase = new PurchaseModels();
            dbobj.SelectQuery("select * from supplier where supp_name='" + vendorID + "'", ref SqlDtr);
            if (SqlDtr.Read())
            {
                purchase.City = SqlDtr["City"].ToString();
                //ssc=SqlDtr["sadbhavnacd"].ToString();
                purchase.Tin_No = SqlDtr["Tin_No"].ToString();
            }
            return purchase;
        }

        [HttpGet]
        [Route("api/purchase/GetInvoiceNumber")]
        public string GetInvoiceNumber(string invoiceNo)
        {
            SqlDataReader SqlDtr = null;
            string invoiceNumber = null;
            dbobj.SelectQuery("select invoice_no from purchase_master where invoice_no='" + invoiceNo + "'", ref SqlDtr);
            if (SqlDtr.Read())
            {
                invoiceNumber = SqlDtr["Invoice_No"].ToString();
                //ssc=SqlDtr["sadbhavnacd"].ToString();
            }
            return invoiceNumber;
        }
    }
}




