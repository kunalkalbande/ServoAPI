//using DBOperations;
using Servo_API.App_Start;
using Servo_API.Models;
//using Servosms.App_Data;
using Servosms.Sysitem.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Servo_API.App_Start.DbOperations_LATEST;

namespace Servo_API.Controllers
{
    
    public class StockAdjustmentController : ApiController
    {        
        App_Start.DbOperations_LATEST.DBUtil dbobj = new App_Start.DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
        SqlConnection SqlCon;
        InventoryClass obj = new InventoryClass();

        [HttpGet]
        [Route("api/StockAdjustment/GetFillCombo")]
        public IHttpActionResult GetFillCombo()
        {            
            string texthiddenprod = string.Empty;
            try
            {
                SqlDataReader SqlDtr = null;
                dbobj.SelectQuery("Select case when pack_type != '' then Prod_Name+':'+Pack_Type else Prod_Name  end from products order by Prod_Name", ref SqlDtr);
                if (SqlDtr.HasRows)
                {
                    texthiddenprod = "Select,";
                    while (SqlDtr.Read())
                    {                        
                        texthiddenprod += SqlDtr.GetValue(0).ToString() + ",";
                    }
                }
                SqlDtr.Close();

                if (texthiddenprod == null)
                    return Content(HttpStatusCode.NotFound, "Could not get data to fill Product Name combobox");

                return Ok(texthiddenprod);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Could not get data to fill Product Name combobox");
            }
        }

        [HttpGet]
        [Route("api/StockAdjustment/GetStockAdjustmentIds")]
        public IHttpActionResult GetStockAdjustmentIds()
        {
            List<string> dropStockAdjustmentIds = new List<string>();
            try
            {
                SqlDataReader SqlDtr = null;
                InventoryClass obj = new InventoryClass();
                SqlDataReader rdr = obj.GetRecordSet("select distinct sav_id from stock_adjustment order by sav_id");
                
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        dropStockAdjustmentIds.Add(rdr.GetValue(0).ToString());
                    }
                }
                rdr.Close();

                if (dropStockAdjustmentIds == null)
                    return Content(HttpStatusCode.NotFound, "Could not get data to fill Stock Adjustment combobox");

                return Ok(dropStockAdjustmentIds);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Could not get data to fill Stock Adjustment combobox");
            }
        }

        [HttpGet]
        [Route("api/StockAdjustment/GetStoreIn")]
        public IHttpActionResult GetStoreIn()
        {
            SqlDataReader SqlDtr = null;
            SqlDataReader SqlDtr1 = null;
            SqlDataReader SqlDtr2 = null;
            StockAdjustmentModel stockAdjust = new StockAdjustmentModel();
            try
            {
                dbobj.SelectQuery("Select case when pack_type != '' then Prod_Name+':'+Pack_Type else Prod_Name  end,Category,Store_In,Pack_Type,Prod_ID from products", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    if (SqlDtr.GetValue(1).ToString().Equals("Fuel"))
                    {
                        dbobj.SelectQuery("Select Prod_AbbName from tank where Tank_ID = '" + SqlDtr.GetValue(2).ToString() + "'", ref SqlDtr1);
                        if (SqlDtr1.Read())
                        {
                            stockAdjust.product_info = stockAdjust.product_info + SqlDtr.GetValue(0).ToString().Trim() + "~" + SqlDtr.GetValue(1).ToString().Trim() + "~" + SqlDtr1.GetValue(0).ToString().Trim() + "~" + " " + "#";
                            stockAdjust.product_info1 = stockAdjust.product_info1 + SqlDtr.GetValue(0).ToString().Trim() + "~" + "1X1#";
                        }
                        SqlDtr1.Close();
                    }
                    else
                    {
                        stockAdjust.product_info = stockAdjust.product_info + SqlDtr.GetValue(0).ToString().Trim() + "~" + SqlDtr.GetValue(1).ToString().Trim() + "~" + SqlDtr.GetValue(2).ToString().Trim() + "~" + SqlDtr.GetValue(3).ToString().Trim() + "#";
                        stockAdjust.product_info1 = stockAdjust.product_info1 + SqlDtr.GetValue(0).ToString().Trim() + "~" + SqlDtr.GetValue(3).ToString() + "#";
                    }
                    dbobj.SelectQuery("Select top 1 Closing_Stock from Stock_Master where ProductID = " + SqlDtr.GetValue(4).ToString() + " order by stock_date desc", ref SqlDtr2);
                    if (SqlDtr2.Read())
                    {
                        stockAdjust.product_info2 = stockAdjust.product_info2 + SqlDtr.GetValue(0).ToString().Trim() + "~" + SqlDtr2.GetValue(0).ToString() + "#";
                    }
                    SqlDtr2.Close();
                }
                SqlDtr.Close();

                if (stockAdjust == null)
                    return Content(HttpStatusCode.NotFound, "Failed to get data for GetStoreIn method");

                return Ok(stockAdjust);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to get data for GetStoreIn method");
            }
        }

        [HttpGet]
        [Route("api/StockAdjustment/GetNextStockAdjustID")]
        public IHttpActionResult GetNextStockAdjustID()
        {
            SqlDataReader SqlDtr = null;
            string stockAdjust_ID = string.Empty;
            try
            {

                dbobj.SelectQuery("Select max(SAV_ID)+1 from Stock_Adjustment", ref SqlDtr);
                if (SqlDtr.Read())
                {
                    if (!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
                        stockAdjust_ID = SqlDtr.GetValue(0).ToString();
                    else
                        stockAdjust_ID = "1001";

                    
                }
                return Ok(stockAdjust_ID);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to get Next Stock Adjustment ID");
            }
        }

        [HttpPost]
        [Route("api/StockAdjustment/InsertBatchNo")]
        public IHttpActionResult InsertBatchNo(StockAdjustmentModel stockAdjust)
        {
            
            try
            {
                //StockAdjustmentModel stockAdjust = new StockAdjustmentModel();


                //stockAdjust.Prod = Prod;
                //stockAdjust.PackType = PackType;
                //stockAdjust.Qty = Qty;

                InventoryClass obj = new InventoryClass();
                InventoryClass obj1 = new InventoryClass();
                DbOperations_LATEST.DBUtil dbobj1 = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationManager.AppSettings["Servosms"], true);
                SqlDataReader rdr1 = null;
                int SNo = 0;
                rdr1 = obj1.GetRecordSet("select max(SNo)+1 from Batch_Transaction");
                if (rdr1.Read())
                {
                    if (rdr1.GetValue(0).ToString() != "" && rdr1.GetValue(0).ToString() != null)
                        SNo = int.Parse(rdr1.GetValue(0).ToString());
                    else
                        SNo = 1;
                }
                else
                    SNo = 1;
                rdr1.Close();
                SqlDataReader rdr = obj.GetRecordSet("select * from stockmaster_batch where productid=(select prod_id from products where prod_name='" + stockAdjust.Prod + "' and Pack_Type='" + stockAdjust.PackType + "') order by stock_date");
                int count = 0;
                if (stockAdjust.Qty != "")
                    count = int.Parse(stockAdjust.Qty);
                int x = 0;
                double cl_sk = 0;
                while (rdr.Read())
                {
                    if (double.Parse(rdr["closing_stock"].ToString()) > 0)
                        cl_sk = double.Parse(rdr["closing_stock"].ToString());
                    else
                        continue;
                    if (count > 0)
                    {
                        if (int.Parse(rdr["closing_stock"].ToString()) > 0)
                        {
                            if (count <= int.Parse(rdr["closing_stock"].ToString()))
                            {
                                cl_sk -= count;

                                dbobj1.Insert_or_Update("update stockmaster_batch set sales=sales+" + count + ",closing_stock=closing_stock-" + count + " where productid='" + rdr["productid"].ToString() + "' and batch_id='" + rdr["batch_id"].ToString() + "'", ref x);
                                if (stockAdjust.SAV_ID_Visible == true)
                                    dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + stockAdjust.SAV_ID + "','Stock Adjustment (OUT)','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "','" + rdr["ProductID"].ToString() + "','" + rdr["Batch_ID"].ToString() + "','" + count + "'," + cl_sk.ToString() + ")", ref x);
                                else
                                    //22.06.09 dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+DropSavID.SelectedItem.Text+"','Stock Adjustment (OUT)','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["ProductID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);	
                                    dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + stockAdjust.SAV_ID + "','Stock Adjustment (OUT)','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "','" + rdr["ProductID"].ToString() + "','" + rdr["Batch_ID"].ToString() + "','" + count + "'," + cl_sk.ToString() + ")", ref x);


                                //***********add by vikas 19.06.09 *****************

                                dbobj1.Insert_or_Update("update batchno set qty=qty-" + count + " where prod_id='" + rdr["productid"].ToString() + "' and batch_id='" + rdr["batch_id"].ToString() + "'", ref x);
                                //****************************
                                count = 0;
                                break;
                            }
                            else
                            {
                                cl_sk -= double.Parse(rdr["closing_stock"].ToString());
                                //dbobj1.Insert_or_Update("update batchno set qty=0 where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"' and Batch_No='"+rdr["Batch_No"].ToString()+"' and Date='"+rdr["Date"].ToString()+"'",ref x);
                                dbobj1.Insert_or_Update("update stockmaster_batch set sales=sales+" + double.Parse(rdr["closing_stock"].ToString()) + ",closing_stock=closing_stock-" + double.Parse(rdr["closing_stock"].ToString()) + " where productid='" + rdr["productid"].ToString() + "' and batch_id='" + rdr["batch_id"].ToString() + "'", ref x);
                                if (stockAdjust.SAV_ID_Visible == true)
                                    dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + stockAdjust.SAV_ID + "','Stock Adjustment (OUT)','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "','" + rdr["ProductID"].ToString() + "','" + rdr["Batch_ID"].ToString() + "','" + rdr["closing_stock"].ToString() + "'," + cl_sk.ToString() + ")", ref x);
                                else
                                    dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + stockAdjust.SAV_ID + "','Stock Adjustment (OUT)','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "','" + rdr["ProductID"].ToString() + "','" + rdr["Batch_ID"].ToString() + "','" + rdr["closing_stock"].ToString() + "'," + cl_sk.ToString() + ")", ref x);
                                //count-=int.Parse(rdr["qty"].ToString());

                                //***********add by vikas 19.06.09 *****************
                                dbobj1.Insert_or_Update("update batchno set qty=" + cl_sk + " where prod_id='" + rdr["productid"].ToString() + "' and batch_id='" + rdr["batch_id"].ToString() + "'", ref x);
                                //****************************

                                count -= int.Parse(rdr["closing_stock"].ToString());

                                //*****Add by vikas 10.06.09*********
                                if (stockAdjust.SAV_ID_Visible == true)
                                    dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + stockAdjust.SAV_ID + "','Stock Adjustment (OUT)','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "','" + rdr["ProductID"].ToString() + "','0','" + count.ToString() + "'," + cl_sk.ToString() + ")", ref x);
                                else
                                    dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + stockAdjust.SAV_ID + "','Stock Adjustment (OUT)','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "','" + rdr["ProductID"].ToString() + "','0','" + count.ToString() + "'," + cl_sk.ToString() + ")", ref x);
                                //*****end*********
                            }
                        }
                    }
                }
                rdr.Close();

                return Ok(true);
            }
            catch
            {
                return Content(HttpStatusCode.NotFound, "Could not insert Batch No.");
            }
        }

        [HttpPost]
        [Route("api/StockAdjustment/InsertBatchNoIn")]
        public IHttpActionResult InsertBatchNoIn(StockAdjustmentModel stockAdjust)
        {

            try
            {
                //StockAdjustmentModel stockAdjust = new StockAdjustmentModel();


                //stockAdjust.Prod = Prod;
                //stockAdjust.PackType = PackType;
                //stockAdjust.Qty = Qty;

                InventoryClass obj = new InventoryClass();
                InventoryClass obj1 = new InventoryClass();
                DbOperations_LATEST.DBUtil dbobj1 = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
                SqlDataReader rdr1 = null;
                int SNo = 0, BatID = 0; ;
                rdr1 = obj1.GetRecordSet("select max(SNo)+1 from Batch_Transaction");
                if (rdr1.Read())
                {
                    if (rdr1.GetValue(0).ToString() != "" && rdr1.GetValue(0).ToString() != null)
                        SNo = int.Parse(rdr1.GetValue(0).ToString());
                    else
                        SNo = 1;
                }
                else
                    SNo = 1;
                rdr1.Close();
                rdr1 = obj.GetRecordSet("select max(Batch_ID) from BatchNo");
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

                SqlDataReader rdr = obj.GetRecordSet("select * from stockmaster_batch where productid=(select prod_id from products where prod_name='" + stockAdjust.Prod + "' and Pack_Type='" + stockAdjust.PackType + "') order by stock_date");
                int count = 0;
                if (stockAdjust.Qty != "")
                    count = int.Parse(stockAdjust.Qty);
                int x = 0;
                double cl_sk = 0;
                string batch_name = "";
                while (rdr.Read())
                {
                    if (double.Parse(rdr["closing_stock"].ToString()) > 0)
                    {
                        cl_sk = double.Parse(rdr["closing_stock"].ToString());
                    }
                    else
                    {
                        /*******Add by vikas 24.06.09 ****************************/

                        rdr1 = obj1.GetRecordSet("select * from batchno where prod_id=(select prod_id from products where prod_name='" + stockAdjust.Prod1 + "' and pack_type='" + stockAdjust.PackType1 + "')");
                        if (rdr1.Read())
                        {
                            batch_name = rdr1.GetValue(1).ToString();
                        }
                        rdr1.Close();

                        cl_sk += count;

                        string prod_id = "";
                        rdr1 = obj1.GetRecordSet("select prod_id from products where prod_name='" + stockAdjust.Prod + "' and pack_type='" + stockAdjust.PackType + "'");
                        if (rdr1.Read())
                        {
                            prod_id = rdr1.GetValue(0).ToString();
                        }
                        rdr1.Close();

                        string batch_id = "";
                        rdr1 = obj1.GetRecordSet("select batch_id from batchno where batch_no='" + batch_name + "' and prod_id='" + prod_id + "'");
                        if (rdr1.Read())
                        {
                            batch_id = rdr1.GetValue(0).ToString();
                        }
                        rdr1.Close();
                       

                        dbobj1.Insert_or_Update("update batchno set qty=" + count.ToString() + " where prod_id='" + prod_id.ToString() + "' and batch_id='" + batch_id.ToString() + "'", ref x);
                        dbobj1.Insert_or_Update("update stockmaster_batch set receipt=receipt+" + count + ",closing_stock=closing_stock+" + count + " where productid='" + prod_id.ToString() + "' and batch_id='" + batch_id.ToString() + "'", ref x);


                        if (stockAdjust.SAV_ID_Visible == true)
                            dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + stockAdjust.SAV_ID + "','Stock Adjustment (IN)','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "','" + prod_id.ToString() + "','" + BatID.ToString() + "','" + count + "'," + cl_sk.ToString() + ")", ref x);
                        else
                            dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + stockAdjust.SAV_ID + "','Stock Adjustment (IN)','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "','" + prod_id.ToString() + "','" + batch_id.ToString() + "','" + count + "'," + cl_sk.ToString() + ")", ref x);

                        count = 0;

                        /*******End ****************************/
                        continue;
                    }

                    /*******Add by vikas 23.06.09***********************/
                    rdr1 = obj1.GetRecordSet("select * from batchno where prod_id=" + rdr["productid"].ToString() + " and batch_id=" + rdr["batch_id"].ToString());
                    if (rdr1.Read())
                    {
                        batch_name = rdr1.GetValue(1).ToString();
                    }
                    rdr1.Close();
                    /*******End***********************/

                    if (count > 0)
                    {
                        if (int.Parse(rdr["closing_stock"].ToString()) > 0)
                        {
                            if (count <= int.Parse(rdr["closing_stock"].ToString()))
                            {
                                cl_sk += count;

                                //23.06.09 dbobj1.Insert_or_Update("update stockmaster_batch set receipt=receipt+"+count+",closing_stock=closing_stock+"+count+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);

                                /*******Add by vikas 23.06.09***********************/

                                rdr1 = obj1.GetRecordSet("select * from batchno where prod_id=(select prod_id from products where prod_name='" + stockAdjust.Prod1 + "' and Pack_Type='" + stockAdjust.PackType1 + "') and batch_no='" + batch_name + "'");
                                //23.06.09 rdr1 = obj1.GetRecordSet("select * from batchno where prod_id="+rdr["productid"].ToString()+" and batch_no='"+batch_name+"'");
                                if (rdr1.HasRows)
                                {
                                    dbobj1.Insert_or_Update("update batchno set qty=" + cl_sk + " where prod_id='" + rdr["productid"].ToString() + "' and batch_id='" + rdr["batch_id"].ToString() + "'", ref x);
                                    dbobj1.Insert_or_Update("update stockmaster_batch set receipt=receipt+" + count + ",closing_stock=closing_stock+" + count + " where productid='" + rdr["productid"].ToString() + "' and batch_id='" + rdr["batch_id"].ToString() + "'", ref x);
                                }
                                else
                                {
                                    dbobj.Insert_or_Update("insert into BatchNo values(" + (++BatID) + ",'" + batch_name.ToString() + "','" + rdr["productid"].ToString() + "','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "'," + count.ToString() + ",'" + stockAdjust.SAV_ID+ "')", ref x);
                                    dbobj1.Insert_or_Update("insert into stockmaster_batch values(" + rdr["productid"].ToString() + "," + rdr["batch_id"].ToString() + ",'" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "',0," + count.ToString() + ",0," + count.ToString() + ",0,0", ref x);
                                }
                                rdr1.Close();
                                /*******End***********************/


                                if (stockAdjust.SAV_ID_Visible == true)
                                    dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + stockAdjust.SAV_ID + "','Stock Adjustment (IN)','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "','" + rdr["ProductID"].ToString() + "','" + rdr["Batch_ID"].ToString() + "','" + count + "'," + cl_sk.ToString() + ")", ref x);
                                else
                                    dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + stockAdjust.SAV_ID + "','Stock Adjustment (IN)','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "','" + rdr["ProductID"].ToString() + "','" + rdr["Batch_ID"].ToString() + "','" + count + "'," + cl_sk.ToString() + ")", ref x);

                                count = 0;
                                break;
                            }
                            else
                            {

                                cl_sk += count;                                

                                /*******Add by vikas 23.06.09***********************/

                                rdr1 = obj1.GetRecordSet("select * from batchno where prod_id=(select prod_id from products where prod_name='" + stockAdjust.Prod1 + "' and Pack_Type='" + stockAdjust.PackType1 + "') and batch_no='" + batch_name + "'");
                                //23.06.09 rdr1 = obj1.GetRecordSet("select * from batchno where prod_id="+rdr["productid"].ToString()+" and batch_no='"+batch_name+"'");
                                if (rdr1.HasRows)
                                {
                                    dbobj1.Insert_or_Update("update batchno set qty=" + cl_sk + " where prod_id='" + rdr["productid"].ToString() + "' and batch_id='" + rdr["batch_id"].ToString() + "'", ref x);
                                    dbobj1.Insert_or_Update("update stockmaster_batch set receipt=receipt+" + count + ",closing_stock=closing_stock+" + count + " where productid='" + rdr["productid"].ToString() + "' and batch_id='" + rdr["batch_id"].ToString() + "'", ref x);
                                }
                                else
                                {
                                    dbobj.Insert_or_Update("insert into BatchNo values(" + (++BatID) + ",'" + batch_name.ToString() + "','" + rdr["productid"].ToString() + "','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "'," + count.ToString() + ",'" + stockAdjust.SAV_ID + "')", ref x);
                                    dbobj1.Insert_or_Update("insert into stockmaster_batch values(" + rdr["productid"].ToString() + "," + rdr["batch_id"].ToString() + ",'" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "',0," + count.ToString() + ",0," + count.ToString() + ",0,0", ref x);
                                }
                                rdr1.Close();
                                /*******End***********************/

                                if (stockAdjust.SAV_ID_Visible == true)
                                    dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + stockAdjust.SAV_ID + "','Stock Adjustment (IN)','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "','" + rdr["ProductID"].ToString() + "','" + rdr["Batch_ID"].ToString() + "','" + count + "'," + cl_sk.ToString() + ")", ref x);
                                else
                                    dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + stockAdjust.SAV_ID + "','Stock Adjustment (IN)','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "','" + rdr["ProductID"].ToString() + "','" + rdr["Batch_ID"].ToString() + "','" + count + "'," + cl_sk.ToString() + ")", ref x);
                            }
                        }
                    }
                }
                if (!rdr.HasRows)
                {
                    rdr1 = obj1.GetRecordSet("select * from batchno where prod_id=(select prod_id from products where prod_name='" + stockAdjust.Prod1 + "' and pack_type='" + stockAdjust.PackType1 + "')");
                    if (rdr1.Read())
                    {
                        batch_name = rdr1.GetValue(1).ToString();
                    }
                    rdr1.Close();

                    if (batch_name != "")
                    {
                        cl_sk += count;

                        string prod_id = "";
                        rdr1 = obj1.GetRecordSet("select prod_id from products where prod_name='" + stockAdjust.Prod + "' and pack_type='" + stockAdjust.PackType + "'");
                        if (rdr1.Read())
                        {
                            prod_id = rdr1.GetValue(0).ToString();
                        }
                        rdr1.Close();

                        dbobj.Insert_or_Update("insert into BatchNo values(" + (++BatID) + ",'" + batch_name.ToString() + "','" + prod_id.ToString() + "','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "'," + count.ToString() + ",'" + stockAdjust.SAV_ID + "')", ref x);
                        dbobj1.Insert_or_Update("insert into stockmaster_batch values(" + prod_id.ToString() + "," + BatID.ToString() + ",'" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "',0," + count.ToString() + ",0," + count.ToString() + ",0,0)", ref x);

                        if (stockAdjust.SAV_ID_Visible == true)
                            dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + stockAdjust.SAV_ID + "','Stock Adjustment (IN)','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "','" + prod_id.ToString() + "','" + BatID.ToString() + "','" + count + "'," + cl_sk.ToString() + ")", ref x);
                        else
                            dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + stockAdjust.SAV_ID + "','Stock Adjustment (IN)','" + System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(stockAdjust.Date) + " " + DateTime.Now.TimeOfDay.ToString()) + "','" + prod_id.ToString() + "','" + BatID.ToString() + "','" + count + "'," + cl_sk.ToString() + ")", ref x);

                        count = 0;
                    }
                }
                rdr.Close();

                return Ok(true);
            }
            catch
            {
                return Content(HttpStatusCode.NotFound, "Could not insert Batch No. In");
            }
        }

        [HttpPost]
        [Route("api/StockAdjustment/UpdateBatchNo")]
        public IHttpActionResult UpdateBatchNo(string DropSavID)
        {
            int count = 0;

            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                InventoryClass obj = new InventoryClass();
                SqlDataReader rdr;
                SqlCommand cmd;
                //coment by vikas 18.06.09 rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='"+dropInvoiceNo.SelectedItem.Text+"'");
                rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='" + DropSavID + "' and trans_type='Stock Adjustment (OUT)'");
                while (rdr.Read())
                {
                    //******************************
                    string s = "update StockMaster_Batch set Sales=Sales-" + rdr["Qty"].ToString() + ",Closing_Stock=Closing_Stock+" + rdr["Qty"].ToString() + " where ProductID='" + rdr["Prod_ID"].ToString() + "' and Batch_ID='" + rdr["Batch_ID"].ToString() + "'";
                    Con.Open();
                    cmd = new SqlCommand("update StockMaster_Batch set Sales=Sales-" + rdr["Qty"].ToString() + ",Closing_Stock=Closing_Stock+" + rdr["Qty"].ToString() + " where ProductID='" + rdr["Prod_ID"].ToString() + "' and Batch_ID='" + rdr["Batch_ID"].ToString() + "'", Con);
                    //cmd = new SqlCommand("update StockMaster_Batch set Sales=Sales-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock+"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"' and stock_date='"+GenUtil.str2MMDDYYYY(tempInvoiceDate.Value)+"'",Con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Con.Close();

                    /*******Add by vikas 19.06.09**********************/
                    Con.Open();
                    cmd = new SqlCommand("update BatchNo set Qty=Qty+" + rdr["Qty"].ToString() + " where Prod_ID='" + rdr["Prod_ID"].ToString() + "' and Batch_ID='" + rdr["Batch_ID"].ToString() + "'", Con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Con.Close();
                    /************************************************/
                }
                rdr.Close();
                Con.Open();
                cmd = new SqlCommand("delete Batch_Transaction where Trans_ID='" + DropSavID + "' and Trans_Type='Stock Adjustment (OUT)'", Con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Con.Close();

                return Ok(true);
            }
            catch
            {
                return Content(HttpStatusCode.NotFound, "Could not delete Route");
            }
        }

        [HttpPost]
        [Route("api/StockAdjustment/UpdateBatchNo_In")]
        public IHttpActionResult UpdateBatchNo_In(string DropSavID)
        {
            int count = 0;

            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                InventoryClass obj = new InventoryClass();
                SqlDataReader rdr;
                SqlCommand cmd;
                //coment by vikas 18.06.09 rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='"+dropInvoiceNo.SelectedItem.Text+"'");
                rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='" + DropSavID + "' and trans_type='Stock Adjustment (IN)'");
                while (rdr.Read())
                {
                    //******************************
                    string s = "update StockMaster_Batch set Sales=Sales-" + rdr["Qty"].ToString() + ",Closing_Stock=Closing_Stock+" + rdr["Qty"].ToString() + " where ProductID='" + rdr["Prod_ID"].ToString() + "' and Batch_ID='" + rdr["Batch_ID"].ToString() + "'";
                    Con.Open();
                    cmd = new SqlCommand("update StockMaster_Batch set Receipt=Receipt-" + rdr["Qty"].ToString() + ",Closing_Stock=Closing_Stock-" + rdr["Qty"].ToString() + " where ProductID='" + rdr["Prod_ID"].ToString() + "' and Batch_ID='" + rdr["Batch_ID"].ToString() + "'", Con);
                    //cmd = new SqlCommand("update StockMaster_Batch set Sales=Sales-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock+"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"' and stock_date='"+GenUtil.str2MMDDYYYY(tempInvoiceDate.Value)+"'",Con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Con.Close();

                    /*******Add by vikas 19.06.09**********************/
                    Con.Open();
                    cmd = new SqlCommand("update BatchNo set Qty=Qty-" + rdr["Qty"].ToString() + " where Prod_ID='" + rdr["Prod_ID"].ToString() + "' and Batch_ID='" + rdr["Batch_ID"].ToString() + "'", Con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Con.Close();
                    /************************************************/
                }
                rdr.Close();
                Con.Open();
                cmd = new SqlCommand("delete Batch_Transaction where Trans_ID='" + DropSavID + "' and Trans_Type='Stock Adjustment (IN)'", Con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Con.Close();

                return Ok(count);
            }
            catch
            {
                return Content(HttpStatusCode.NotFound, "Could not delete Route");
            }
        }

        [HttpGet]
        [Route("api/RouteMaster/GetRouteInfo")]
        public IHttpActionResult GetRouteInfo(string selectedRoute)
        {
            //string s = "";
            RouteMasterModel route = new RouteMasterModel();

            try
            {
                SqlConnection con;
                SqlCommand cmdselect;
                SqlDataReader dtrdrive;
                con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                con.Open();
                cmdselect = new SqlCommand("Select Route_name,Route_km  From Route where Route_name=@Route_name", con);
                cmdselect.Parameters.AddWithValue("@Route_name", selectedRoute.ToString().Trim());
                dtrdrive = cmdselect.ExecuteReader();
                while (dtrdrive.Read())
                {                    
                    route.Route_Name= dtrdrive.GetString(0);
                    route.Route_Km = dtrdrive.GetString(1);
                }
                dtrdrive.Close();
                con.Close();

                if (route == null)
                    return Content(HttpStatusCode.NotFound, "Route data Not found.");

                return Ok(route);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Route data Not found.");
            }
        }

        [HttpGet]
        [Route("api/RouteMaster/CheckIfRouteNameExists")]
        public IHttpActionResult CheckIfRouteNameExists(string routeName)
        {
            int iCount = 0;

            try
            {
                SqlConnection con2;
                SqlCommand cmdselect2;
                SqlDataReader dtredit2;
                con2 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                con2.Open();
                cmdselect2 = new SqlCommand("Select Count(Route_name) from Route where Route_name='" + routeName + "'", con2);
                dtredit2 = cmdselect2.ExecuteReader();
                if (dtredit2.Read())
                {
                    iCount = Convert.ToInt32(dtredit2.GetSqlValue(0).ToString());
                }
                dtredit2.Close();
                con2.Close();
                return Ok(iCount);

            }
            catch
            {
                return Content(HttpStatusCode.NotFound, "Could not delete Vehicle Entry Logbook");
            }
        }

        [HttpPost]
        [Route("api/RouteMaster/DeleteRoute")]
        public IHttpActionResult DeleteRoute(string route)
        {
            int count = 0;

            try
            {
                SqlConnection con10;
                SqlCommand cmdselect10;
                SqlDataReader dtredit10;
                string strdelete10;
                con10 = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["Servosms"]);
                con10.Open();

                strdelete10 = "Delete Route where Route_name =@Route_name";
                cmdselect10 = new SqlCommand(strdelete10, con10);
                cmdselect10.Parameters.AddWithValue("@Route_name", route);
                dtredit10 = cmdselect10.ExecuteReader();
                
                return Ok(count);
            }
            catch
            {
                return Content(HttpStatusCode.NotFound, "Could not delete Route");
            }
        }

        [HttpPost]
        [Route("api/RouteMaster/UpdateRoute")]
        public IHttpActionResult UpdateRoute(RouteMasterModel route)
        {

            try
            {
                SqlConnection con2;
                string strUpdate;
                SqlCommand cmdselect2;
                SqlDataReader dtredit2;

                con2 = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["Servosms"]);
                con2.Open();

                strUpdate = "Update Route set Route_name=@Route_name,Route_km=@Route_km where Route_name=@Route2";
                cmdselect2 = new SqlCommand(strUpdate, con2);
                if (route.Route_Name == "")
                    cmdselect2.Parameters.AddWithValue("@Route_name", "");
                else
                    cmdselect2.Parameters.AddWithValue("@Route_name", route.Route_Name.Trim());
                if (route.Route_Km == "")
                    cmdselect2.Parameters.AddWithValue("@Route_km", "");
                else
                    cmdselect2.Parameters.AddWithValue("@Route_km", route.Route_Km.Trim());
                if (route.Index_Route_Name == "0")
                    cmdselect2.Parameters.AddWithValue("@Route2", "");
                else
                    cmdselect2.Parameters.AddWithValue("@Route2", route.Selected_Route_Name.ToString());
                dtredit2 = cmdselect2.ExecuteReader();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Route could not update.");
            }
        }

        [HttpPost]
        [Route("api/RouteMaster/InsertRoute")]
        public IHttpActionResult InsertRoute(RouteMasterModel route)
        {

            try
            {
                SqlConnection con4;
                string strInsert4;
                SqlCommand cmdInsert4;
                con4 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                con4.Open();
                strInsert4 = "Insert Route(Route_name,Route_km)values (@Route_name,@Route_km)";
                cmdInsert4 = new SqlCommand(strInsert4, con4);
                cmdInsert4.Parameters.AddWithValue("@Route_name", route.Route_Name.Trim());
                cmdInsert4.Parameters.AddWithValue("@Route_km", route.Route_Km.Trim());
                cmdInsert4.ExecuteNonQuery();
                con4.Close();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Route could not update.");
            }
        }

        [HttpGet]
        [Route("api/StockAdjustment/DeleteStockAdj")]
        public IHttpActionResult DeleteStockAdj(string sav_id)
        {
            try
            {                
                string sql;
                SqlDataReader SqlDtr;                                
                sql = "delete from stock_adjustment where sav_id = '"+sav_id+"'";
                SqlDtr = obj.GetRecordSet(sql);                
                SqlDtr.Close();
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Route could not update.");
            }
        }

        [HttpGet]
        [Route("api/StockAdjustment/SaveStockAdj")]
        public IHttpActionResult SaveStockAdj(string sav_id, string prod_name1 , string pack1 , string store1 , string type1 , string qty1 , string prod_name2 , string pack2 , string store2 , string type2 , string qty2 , string uid , string str1 , string str2)
        {
            try
            {
                object obj = null;
                dbobj.ExecProc(OprType.Insert, "ProInsertStockAdjustment", ref obj, "@SAV_ID", sav_id, "@Out_Product", prod_name1, "@pack1", pack1, "@Store1", store1, "@Type1", type1, "@Out_Qty", qty1, "@In_Product", prod_name2, "@Pack2", pack2, "@Store2", store2, "@Type2", type2, "@In_Qty", qty2, "@Entry_By", uid, "@Nar", str1, "@Stock_Date", str2);                
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Route could not update.");
            }
        }

        [HttpGet]
        [Route("api/StockAdjustment/UpdateStockAdj")]
        public IHttpActionResult UpdateStockAdj(string str1, string str2, string str3, string str4)
        {
            try
            {
                int x = 0;                
                dbobj.Insert_or_Update("update stock_master set sales=sales-" + str1 + ",closing_stock=closing_stock+" + str1 + " where productid='" + str2 + "' and cast(floor(cast(stock_date as float)) as datetime)=Convert(datetime,'" + str3 + "',103)", ref x);
                dbobj.Insert_or_Update("update stock_master set receipt=receipt-" + str4 + ",closing_stock=closing_stock-" + str1 + " where productid='" + str2 + "' and cast(floor(cast(stock_date as float)) as datetime)=Convert(datetime,'" + str3 + "',103)", ref x);
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Route could not update.");
            }
        }
        List<string> controlopening_stock = new List<string>();
        List<string> controlreceipt = new List<string>();
        List<string> controlsales = new List<string>();
        List<string> controlsalesfoc = new List<string>();
        List<string> controlProductid = new List<string>();
        List<string> controlstock_date = new List<string>();

        [HttpGet]
        [Route("api/StockAdjustment/SeqStockMaster")]
        public IHttpActionResult SeqStockMaster(string str1)
        {
            try
            {
                SqlDataReader SqlDtr;
                StockAdjustmentModel stkadj = new StockAdjustmentModel();
                string sql;
                sql = "select * from Stock_Master where Productid='" + str1 + "' order by Stock_date";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    controlopening_stock.Add(SqlDtr["opening_stock"].ToString());
                    controlreceipt.Add(SqlDtr["receipt"].ToString());
                    controlsales.Add(SqlDtr["sales"].ToString());
                    controlsalesfoc.Add(SqlDtr["salesfoc"].ToString());
                    controlProductid.Add(SqlDtr["Productid"].ToString());
                    controlstock_date.Add(SqlDtr["stock_date"].ToString());
                }
                SqlDtr.Close();

                stkadj.opening_stock = controlopening_stock;
                stkadj.receipt = controlreceipt;
                stkadj.sales = controlsales;
                stkadj.salesfoc = controlsalesfoc;
                stkadj.Productid = controlProductid;
                stkadj.stock_date = controlstock_date;
                return Ok(stkadj);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Route could not update.");
            }
        }

        [HttpGet]
        [Route("api/StockAdjustment/UpdateSeqStockMaster")]
        public IHttpActionResult UpdateSeqStockMaster(string OS,string CS,string str7,string str8)
        {
            try
            {
                SqlDataReader SqlDtr;
                StockAdjustmentModel stkadj = new StockAdjustmentModel();
                string sql;
                sql = "update Stock_Master set opening_stock='" + OS.ToString() + "', Closing_Stock='" + CS.ToString() + "' where ProductID='" + str7 + "' and Stock_Date=Convert(datetime,'" + str8 + "',103)";
                SqlDtr = obj.GetRecordSet(sql);
                SqlDtr.Close();
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Route could not update.");
            }
        }
    }
 }
