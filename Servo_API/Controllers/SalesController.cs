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

namespace Servo_API.Controllers
{
    public class SalesController : ApiController
    {
        static string FromDate = "", ToDate = "";
        App_Start.DbOperations_LATEST.DBUtil dbobj = new App_Start.DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);

        [HttpPost]
        [Route("api/Sales/InsertSalesMaster")]
        public bool InsertSalesMaster(SalesSaveDetailsModel obj)
        {
            SqlCommand cmd;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            InventoryClass obj1 = new InventoryClass();
            try
            {
                obj1.InsertSalesMaster(obj);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/UpdateCustomerBalance")]
        public bool UpdateCustomerBalance(InventoryClass obj)
        {
            SqlCommand cmd;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);

            try
            {
                obj.UpdateCustomerBalance();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/UpdateStock_Master_SetSales")]
        public bool UpdateStock_Master_SetSales(SalesModels sales)
        {
            SqlCommand cmd;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);

            try
            {
                for (int i = 0; i < 12; i++)
                {
                    //if(DropType[i].SelectedItem.Text.Equals("Type") || ProdName[i].Value=="" || PackType[i].Value=="")
                    //if(DropType[i].SelectedItem.Text.Equals("Type"))
                    if (sales.ProductType[i].ToString().Equals(""))
                        continue;
                    else
                    {
                        Con.Open();
                        cmd = new SqlCommand("update Stock_Master set sales=sales-'" + double.Parse(sales.ProductQty[i].ToString()) + "',closing_stock=closing_stock+'" + double.Parse(sales.ProductQty[i].ToString()) + "' where ProductID=(select Prod_ID from Products where Category='" + sales.ProductType[i].ToString() + "' and Prod_Name='" + sales.ProductName[i].ToString() + "' and Pack_Type='" + sales.ProductPack[i].ToString() + "') and cast(floor(cast(stock_date as float)) as datetime)=Convert(datetime,'" + sales.Invoice_Date + "',103)", Con);

                        cmd.ExecuteNonQuery();
                        Con.Close();
                        cmd.Dispose();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/UpdateBatchNo")]
        public bool UpdateBatchNo(string invoiceNo)
        {
            SqlCommand cmd;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);

            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader rdr;
                //coment by vikas 18.06.09 rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='"+dropInvoiceNo.SelectedItem.Text+"'");
                rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='" + invoiceNo + "' and trans_type='Sales Invoice'");
                while (rdr.Read())
                {
                    //******************************
                    string s = "update StockMaster_Batch set Sales=Sales-" + rdr["Qty"].ToString() + ",Closing_Stock=Closing_Stock+" + rdr["Qty"].ToString() + " where ProductID='" + rdr["Prod_ID"].ToString() + "' and Batch_ID='" + rdr["Batch_ID"].ToString() + "'";
                    Con.Open();
                    cmd = new SqlCommand("update StockMaster_Batch set Sales=Sales-" + rdr["Qty"].ToString() + ",Closing_Stock=Closing_Stock+" + rdr["Qty"].ToString() + " where ProductID='" + rdr["Prod_ID"].ToString() + "' and Batch_ID='" + rdr["Batch_ID"].ToString() + "'", Con);
                    //cmd = new SqlCommand("update StockMaster_Batch set Sales=Sales-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock+"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"' and stock_date=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["tempInvoiceDate"].ToString()) + "',103)",Con);
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
                cmd = new SqlCommand("delete Batch_Transaction where Trans_ID='" + invoiceNo + "' and Trans_Type='Sales Invoice'", Con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/UpdateStock_Master_SetSalesFOC")]
        public bool UpdateStock_Master_SetSalesFOC(SalesModels sales)
        {
            SqlCommand cmd;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);

            try
            {
                for (int i = 0; i < 12; i++)
                {
                    if (sales.ProdType[i].ToString() == "")
                        continue;
                    else
                    {
                        Con.Open();
                        //cmd = new SqlCommand("update Stock_Master set salesfoc=salesfoc-'"+double.Parse(Qty1[i].Text)+"',closing_stock=closing_stock+'"+double.Parse(Qty1[i].Text)+"' where ProductID=(select Prod_ID from Products where Category='"+ProdType[i].Text+"' and Prod_Name='"+ProdName1[i].Text+"' and Pack_Type='"+PackType1[i].Text+"') and cast(stock_date as smalldatetime)='"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text)+"'",Con);
                        cmd = new SqlCommand("update Stock_Master set salesfoc=salesfoc-'" + double.Parse(sales.SchProductQty[i].ToString()) + "',closing_stock=closing_stock+'" + double.Parse(sales.SchProductQty[i].ToString()) + "' where ProductID=(select Prod_ID from Products where Category='" + sales.SchProductType[i].ToString() + "' and Prod_Name='" + sales.SchProductName[i].ToString() + "' and Pack_Type='" + sales.SchProductPack[i].ToString() + "') and cast(floor(cast(stock_date as float)) as datetime)=Convert(datetime,'" + sales.Invoice_Date + "',103)", Con);
                        cmd.ExecuteNonQuery();
                        Con.Close();
                        cmd.Dispose();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/Update_OVD_Sale_Trans_Id")]
        public bool Update_OVD_Sale_Trans_Id(string id)
        {
            try
            {

                SqlCommand cmd;
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                Con.Open();
                cmd = new SqlCommand("Update OVD set Sale_Trans_Id='0' , sale_qty='0' where Sale_Trans_Id='" + id + "'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/DeleteSalesMasterData")]
        public bool DeleteSalesOil(string id)
        {
            try
            {

                SqlCommand cmd;
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                cmd = new SqlCommand("delete from Sales_Oil where invoice_no='" + id + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/DeleteSalesMasterData")]
        public bool DeleteSalesMasterData(string id)
        {
            try
            {

                SqlCommand cmd;
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                Con.Open();
                cmd = new SqlCommand("delete from Sales_Master where Invoice_No='" + id + "'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();

                Con.Open();
                cmd = new SqlCommand("delete from Sales_Oil where Invoice_No='" + id + "'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();

                Con.Open();
                cmd = new SqlCommand("delete from Accountsledgertable where Particulars='Sales Invoice (" + id + ")'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/UpdateAccountsLedgerCustomerLedger")]
        public bool UpdateAccountsLedgerCustomerLedger(SalesModels sales)
        {
            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader rdr;
                SqlCommand cmd;
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                string str = "select * from AccountsLedgerTable where Ledger_ID = (select Ledger_ID from Ledger_Master where Ledger_Name='" + sales.Cust_Name + "') order by entry_date";
                rdr = obj.GetRecordSet(str);
                double Bal = 0;
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
                Con.Open();
                cmd = new SqlCommand("delete from Customerledgertable where Particular='Sales Invoice (" + sales.Invoice_ID + ")'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();
                //string str1="select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer where Cust_Name='"+DropCustName.SelectedItem.Text+"') order by entrydate";

                //Comment by vikas sharma 1.05.09 string str1="select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer where Cust_Name='"+text1.Value+"') order by entrydate";
                string str1 = "select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer where Cust_Name='" + sales.Cust_Name + "') order by entrydate";
                rdr = obj.GetRecordSet(str1);
                Bal = 0;
                while (rdr.Read())
                {
                    if (rdr["BalanceType"].ToString().Equals("Dr."))
                        Bal += double.Parse(rdr["DebitAmount"].ToString()) - double.Parse(rdr["CreditAmount"].ToString());
                    else
                        Bal += double.Parse(rdr["CreditAmount"].ToString()) - double.Parse(rdr["DebitAmount"].ToString());
                    if (Bal.ToString().StartsWith("-"))
                        Bal = double.Parse(Bal.ToString().Substring(1));
                    Con.Open();
                    cmd = new SqlCommand("update CustomerLedgerTable set Balance='" + Bal.ToString() + "' where CustID='" + rdr["CustID"].ToString() + "' and Particular='" + rdr["Particular"].ToString() + "'", Con);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    cmd.Dispose();
                }
                rdr.Close();

                Con.Open();
                //cmd = new SqlCommand("Update Customer_Balance set DR_Amount = DR_Amount-'"+double.Parse(txtNetAmount.Text)+"' where Cust_ID = (select Cust_ID from Customer where Cust_Name='"+DropCustName.SelectedItem.Text+"' and city='"+lblPlace.Value+"')",Con);

                //Comment by vikas sharma 1.05.09 cmd = new SqlCommand("Update Customer_Balance set DR_Amount = DR_Amount-'"+double.Parse(txtNetAmount.Text)+"' where Cust_ID = (select Cust_ID from Customer where Cust_Name='"+text1.Value+"' and city='"+lblPlace.Value+"')",Con);
                cmd = new SqlCommand("Update Customer_Balance set DR_Amount = DR_Amount-'" + double.Parse(sales.Net_Amount.ToString()) + "' where Cust_ID = (select Cust_ID from Customer where Cust_Name='" + sales.Cust_Name + "' and city='" + sales.Place + "')", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/InsertSalesOil")]
        public bool InsertSalesOil(List<string> sales)
        {
            try
            {
                string cat = sales[0].ToString();
                string invoiceNo = sales[1].ToString();
                string prod_id = sales[2].ToString();
                string txtcusttype = sales[3].ToString();
                string prodName = sales[4].ToString();
                string qty = sales[5].ToString();
                string invoiceDate = sales[6].ToString();

                SqlCommand cmd = new SqlCommand();
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                con.Open();
                if (cat == "")
                    //cmd=new SqlCommand("insert into Sales_Oil values("+dropInvoiceNo.SelectedItem.Text.Trim()+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+",0,0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                    //Mahesh11.04.007 cmd=new SqlCommand("insert into Sales_Oil values("+dropInvoiceNo.SelectedItem.Text.Trim()+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                    //cmd=new SqlCommand("insert into Sales_Oil values("+invoice+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
                    //Mahesh 05.11.007 cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);

                    //Comment by vikas 1.05.09 cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                    cmd = new SqlCommand("insert into Sales_Oil values(" + invoiceNo + "," + prod_id + ",'" + txtcusttype + "'," + GenUtil.changeqty(prodName, int.Parse(qty)) + ",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate) + "',103))", con);

                else if (cat.StartsWith("2t") || cat.StartsWith("2T"))
                    //cmd=new SqlCommand("insert into Sales_Oil values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+","+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+",0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                    //Mahesh11.04.007 cmd=new SqlCommand("insert into Sales_Oil values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+","+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                    //cmd=new SqlCommand("insert into Sales_Oil values("+invoice+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+","+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
                    //**cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+","+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);

                    //Comment by vikas 1.05.09cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+","+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                    cmd = new SqlCommand("insert into Sales_Oil values(" + invoiceNo + ",'" + prod_id + "','" + txtcusttype + "'," + GenUtil.changeqty(prodName, int.Parse(qty)) + "," + GenUtil.changeqty(prodName, int.Parse(qty)) + ",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate) + "',103))", con);

                else if (cat.StartsWith("4t") || cat.StartsWith("4T"))
                    // cmd=new SqlCommand("insert into Sales_Oil values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+",0,"+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                    //cmd=new SqlCommand("insert into Sales_Oil values("+invoice+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);

                    // Comment by vikas 1.05.09 cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+",0,"+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                    cmd = new SqlCommand("insert into Sales_Oil values(" + invoiceNo + ",'" + prod_id + "','" + txtcusttype + "'," + GenUtil.changeqty(prodName.ToString(), int.Parse(qty)) + ",0," + GenUtil.changeqty(prodName, int.Parse(qty)) + ",Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate) + "',103))", con);

                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/UpdateSalesOil")]
        public bool UpdateSalesOil(List<string> sales)
        {
            try
            {
                string cat = sales[0].ToString();
                string invoiceNo = sales[1].ToString();
                string prod_id = sales[2].ToString();
                string txtcusttype = sales[3].ToString();
                string prodName = sales[4].ToString();
                string qty = sales[5].ToString();
                string invoiceDate = sales[6].ToString();

                SqlCommand cmd = new SqlCommand();
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                con.Open();
                //cmd=new SqlCommand("insert into monthwise1 values("+lblInvoiceNo.Text+",'"+txtmwid1.Text+"',"+GenUtil.changeqty(txtPack1.Value,bi)+","+GenUtil.changeqty(txtPack1.Value,ri)+","+GenUtil.changeqty(txtPack1.Value,oi)+","+GenUtil.changeqty(txtPack1.Value,fi)+","+GenUtil.changeqty(txtPack1.Value,ii)+","+GenUtil.changeqty(txtPack1.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                if (cat == "")
                    //cmd=new SqlCommand("insert into Sales_Oil values("+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+",0,0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                    //**cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);

                    // Comment by vikas 1.05.09 cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                    cmd = new SqlCommand("insert into Sales_Oil values(" + invoiceNo + "," + prod_id + ",'" + txtcusttype + "'," + GenUtil.changeqty(prodName, int.Parse(qty)) + ",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate) + "',103))", con);

                else if (cat.StartsWith("2t") || cat.StartsWith("2T"))
                    //cmd=new SqlCommand("insert into Sales_Oil values("+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+","+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+",0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                    //**cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+","+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);

                    //Comment by vikas 1.05.09 cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+","+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                    cmd = new SqlCommand("insert into Sales_Oil values(" + invoiceNo + ",'" + prod_id + "','" + txtcusttype + "'," + GenUtil.changeqty(prodName, int.Parse(qty)) + "," + GenUtil.changeqty(prodName, int.Parse(qty)) + ",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate) + "',103))", con);

                else if (cat.StartsWith("4t") || cat.StartsWith("4T"))
                    //cmd=new SqlCommand("insert into Sales_Oil values("+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+",0,"+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                    //**cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);

                    //Comment by vikas 1.05.09cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+",0,"+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                    cmd = new SqlCommand("insert into Sales_Oil values(" + invoiceNo + ",'" + prod_id + "','" + txtcusttype + "'," + GenUtil.changeqty(prodName, int.Parse(qty)) + ",0," + GenUtil.changeqty(prodName, int.Parse(qty)) + ",Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate) + "',103))", con);

                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/InsertSalesOilProdschName")]
        public bool InsertSalesOilProdschName(List<string> sales)
        {
            try
            {
                string cat = sales[0].ToString();
                string invoiceNo = sales[1].ToString();
                string prod_id = sales[2].ToString();
                string txtcusttype = sales[3].ToString();
                string prodschName = sales[4].ToString();
                string qty = sales[5].ToString();
                string invoiceDate = sales[6].ToString();

                SqlCommand cmd = new SqlCommand();
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                con.Open();
                if (cat == "")
                    //cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);

                    //Comment By vikas 1.05.09 cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdschName1[1].ToString(),int.Parse(Qty1[cc].Text))+",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                    cmd = new SqlCommand("insert into Sales_Oil values(" + invoiceNo + "," + prod_id + ",'" + txtcusttype + "'," + GenUtil.changeqty(prodschName, int.Parse(qty)) + ",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate.Trim()) + "',103))", con);

                else if (cat.StartsWith("2t") || cat.StartsWith("2T"))
                    //cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+","+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                    cmd = new SqlCommand("insert into Sales_Oil values(" + invoiceNo + ",'" + prod_id + "','" + txtcusttype + "'," + GenUtil.changeqty(prodschName, int.Parse(qty)) + "," + GenUtil.changeqty(prodschName, int.Parse(qty)) + ",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate.Trim()) + "',103))", con);
                else if (cat.StartsWith("4t") || cat.StartsWith("4T"))
                    //cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+",0,"+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                    cmd = new SqlCommand("insert into Sales_Oil values(" + invoiceNo + ",'" + prod_id + "','" + txtcusttype + "'," + GenUtil.changeqty(prodschName, int.Parse(qty)) + ",0," + GenUtil.changeqty(prodschName, int.Parse(qty)) + ",Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate.Trim()) + "',103))", con);

                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/UpdateSalesOilProdschName")]
        public bool UpdateSalesOilProdschName(List<string> sales)
        {
            try
            {
                string cat = sales[0].ToString();
                string invoiceNo = sales[1].ToString();
                string prod_id = sales[2].ToString();
                string txtcusttype = sales[3].ToString();
                string prodschName = sales[4].ToString();
                string qty = sales[5].ToString();
                string invoiceDate = sales[6].ToString();

                SqlCommand cmd = new SqlCommand();
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                con.Open();
                //cmd=new SqlCommand("insert into monthwise1 values("+lblInvoiceNo.Text+",'"+txtmwid1.Text+"',"+GenUtil.changeqty(txtPack1.Value,bi)+","+GenUtil.changeqty(txtPack1.Value,ri)+","+GenUtil.changeqty(txtPack1.Value,oi)+","+GenUtil.changeqty(txtPack1.Value,fi)+","+GenUtil.changeqty(txtPack1.Value,ii)+","+GenUtil.changeqty(txtPack1.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                if (cat == "")
                    //cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                    cmd = new SqlCommand("insert into Sales_Oil values(" + invoiceNo + "," + prod_id + ",'" + txtcusttype + "'," + GenUtil.changeqty(prodschName, int.Parse(qty)) + ",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate.Trim()) + "',103))", con);
                else if (cat.StartsWith("2t") || cat.StartsWith("2T"))
                    //cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+","+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                    cmd = new SqlCommand("insert into Sales_Oil values(" + invoiceNo + ",'" + prod_id + "','" + txtcusttype + "'," + GenUtil.changeqty(prodschName, int.Parse(qty)) + "," + GenUtil.changeqty(prodschName, int.Parse(qty)) + ",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate.Trim()) + "',103))", con);
                else if (cat.StartsWith("4t") || cat.StartsWith("4T"))
                    //cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+",0,"+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                    cmd = new SqlCommand("insert into Sales_Oil values(" + invoiceNo + ",'" + prod_id + "','" + txtcusttype + "'," + GenUtil.changeqty(prodschName, int.Parse(qty)) + ",0," + GenUtil.changeqty(prodschName, int.Parse(qty)) + ",Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate.Trim()) + "',103))", con);

                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/DeleteSales_OilData")]
        public bool DeleteSales_OilData(string id)
        {
            try
            {
                SqlCommand cmd;
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                Con.Open();
                cmd = new SqlCommand("delete from Sales_Oil where Invoice_No='" + id + "'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/InsUpdateCustLedgerAcctLedgerCustomer")]
        public bool InsUpdateCustLedgerAcctLedgerCustomer(List<string> sales)
        {
            int x = 0;
            string invoiceDate = sales[0].ToString();
            string NetAmount = sales[1].ToString();
            string CustID = sales[2].ToString();
            try
            {
                DbOperations_LATEST.DBUtil dbobj = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
                dbobj.Insert_or_Update("delete from customerledgertable where particular='Sales Invoice (" + invoiceDate + ")'", ref x);
                dbobj.Insert_or_Update("delete from AccountsLedgerTable where particulars='Sales Invoice (" + invoiceDate + ")'", ref x);
                dbobj.Insert_or_Update("update customer set Curr_Credit=Curr_Credit+" + NetAmount + " where Cust_ID='" + CustID + "'", ref x);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/UpdateSalesMaster")]
        public bool UpdateSalesMaster(SalesSaveDetailsModel obj)
        {
            InventoryClass obj1 = new InventoryClass();
            try
            {
                obj1.UpdateSalesMaster(obj);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/ProUpdate_Customer_Balance")]
        public bool ProUpdate_Customer_Balance(InventoryClass obj)
        {
            try
            {
                obj.UpdateCustomerBalance();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/InsertSalesDetail")]
        public bool InsertSalesDetail(SalesDetailsModel salesDetails)
        {
            try
            {
                InventoryClass obj = new InventoryClass();

                obj.Invoice_No = salesDetails.Invoice_No;
                obj.Product_Name = salesDetails.Product_Name;
                obj.Package_Type = salesDetails.Package_Type;
                obj.Qty = salesDetails.Qty;
                obj.sno = salesDetails.sno;
                obj.Rate = salesDetails.Rate;
                obj.Amount = salesDetails.Amount;
                obj.QtyTemp = salesDetails.QtyTemp;
                obj.Invoice_Date = salesDetails.Invoice_Date;
                obj.sch = salesDetails.sch;
                obj.foe = salesDetails.foe;
                obj.schtype = salesDetails.schtype;
                obj.SecSPDisc = salesDetails.SecSPDisc;
                obj.SecSPDiscType = salesDetails.SecSPDiscType;
                obj.foediscounttype = salesDetails.foediscounttype;

                obj.InsertSalesDetail();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/InsertSaleSchemeDetail")]
        public bool InsertSaleSchemeDetail(InventoryClass obj)
        {
            try
            {
                obj.InsertSaleSchemeDetail();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/InsertBatchNo")]
        public bool InsertBatchNo(List<string> sales)
        {
            string Prod = sales[0].ToString();
            string PackType = sales[1].ToString();
            string Qty = sales[2].ToString();
            string lblInvoiceNo = sales[3].ToString();
            string dropInvoiceNo = sales[4].ToString();
            string lblInvoiceDate = sales[5].ToString();

            try
            {
                InventoryClass obj = new InventoryClass();
                InventoryClass obj1 = new InventoryClass();
                DbOperations_LATEST.DBUtil dbobj1 = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
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
                int x = 0;


                //SqlDataReader rdr = obj.GetRecordSet("select * from batchno where prod_id=(select prod_id from products where prod_name='"+Prod+"' and Pack_Type='"+PackType+"') order by Batch_ID");
                //SqlDataReader rdr = obj.GetRecordSet("select * from stockmaster_batch where productid=(select prod_id from products where prod_name='"+Prod+"' and Pack_Type='"+PackType+"') order by Batch_ID");
                SqlDataReader rdr = obj.GetRecordSet("select * from stockmaster_batch where productid=(select prod_id from products where prod_name='" + Prod + "' and Pack_Type='" + PackType + "') order by stock_date");
                int count = 0;
                if (Qty != "")
                    count = int.Parse(Qty);

                double cl_sk = 0;
                double cl_sk_New = 0;
                while (rdr.Read())
                {
                    if (double.Parse(rdr["closing_stock"].ToString()) > 0)
                    {
                        cl_sk = double.Parse(rdr["closing_stock"].ToString());
                        cl_sk_New = double.Parse(rdr["closing_stock"].ToString());
                    }
                    else
                        continue;
                    if (count > 0)
                    {
                        if (int.Parse(rdr["closing_stock"].ToString()) > 0)
                        {
                            if (count <= int.Parse(rdr["closing_stock"].ToString()))
                            {
                                cl_sk -= count;
                                cl_sk_New -= count;
                                /**********Add by vikas 15.09.09*********************************
								if(lblInvoiceNo.Visible!=true)
								{
									cl_sk_New+=count;
									dbobj1.Insert_or_Update("Delete from batch_transaction where trans_id="+dropInvoiceNo.SelectedItem.Text+" and trans_type='Sales Invoice'",ref x);	                                        //Add by vikas 15.09.09
								}
								**********End***************************************************/

                                //07.07.09 dbobj1.Insert_or_Update("update stockmaster_batch set sales=sales+"+count+",closing_stock=closing_stock-"+count+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);

                                dbobj1.Insert_or_Update("update stockmaster_batch set stock_date=Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate) + "',103), sales=sales+" + count + ",closing_stock=closing_stock-" + count + " where productid='" + rdr["productid"].ToString() + "' and batch_id='" + rdr["batch_id"].ToString() + "'", ref x);

                                /***************Add by vikas 15.09.09******************************
								if(lblInvoiceNo.Visible!=true)
								{
									dbobj1.Insert_or_Update("update stockmaster_batch set stock_date=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103), sales=sales+"+count+",closing_stock=closing_stock-"+count+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);  
								}
								else
								{
									dbobj1.Insert_or_Update("update stockmaster_batch set stock_date=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103), sales=sales+"+count+",closing_stock=closing_stock-"+count+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);  
								}
								********************************End*******************************/

                                if (lblInvoiceNo.ToString() != "")
                                    dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + lblInvoiceNo + "','Sales Invoice',Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate) + "',103),'" + rdr["ProductID"].ToString() + "','" + rdr["Batch_ID"].ToString() + "','" + count + "'," + cl_sk.ToString() + ")", ref x);
                                else
                                    dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + dropInvoiceNo + "','Sales Invoice',Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate) + "',103),'" + rdr["ProductID"].ToString() + "','" + rdr["Batch_ID"].ToString() + "','" + count + "'," + cl_sk.ToString() + ")", ref x);

                                /***********add by vikas 19.06.09 *****************/
                                dbobj1.Insert_or_Update("update batchno set qty=qty-" + count + " where prod_id='" + rdr["productid"].ToString() + "' and batch_id='" + rdr["batch_id"].ToString() + "'", ref x);
                                /****************************/
                                count = 0;
                                break;
                            }
                            else
                            {
                                cl_sk -= double.Parse(rdr["closing_stock"].ToString());

                                //dbobj1.Insert_or_Update("update batchno set qty=0 where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"' and Batch_No='"+rdr["Batch_No"].ToString()+"' and Date='"+rdr["Date"].ToString()+"'",ref x);
                                //07.07.09 dbobj1.Insert_or_Update("update stockmaster_batch set sales=sales+"+double.Parse(rdr["closing_stock"].ToString())+",closing_stock=closing_stock-"+double.Parse(rdr["closing_stock"].ToString())+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);

                                /**********Add by vikas 15.09.09*********************************
								if(lblInvoiceNo.Visible!=true)
								{
									cl_sk_New+=double.Parse(rdr["closing_stock"].ToString());
									dbobj1.Insert_or_Update("Delete from batch_transaction where trans_id="+dropInvoiceNo.SelectedItem.Text+" and trans_type='Sales Invoice'",ref x);	                                        //Add by vikas 15.09.09
								}
								**********End***************************************************/

                                dbobj1.Insert_or_Update("update stockmaster_batch set stock_date=Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate) + "',103), sales=sales+" + double.Parse(rdr["closing_stock"].ToString()) + ",closing_stock=closing_stock-" + double.Parse(rdr["closing_stock"].ToString()) + " where productid='" + rdr["productid"].ToString() + "' and batch_id='" + rdr["batch_id"].ToString() + "'", ref x);

                                if (lblInvoiceNo.ToString() != "")
                                    dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + lblInvoiceNo + "','Sales Invoice',Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate) + "',103),'" + rdr["ProductID"].ToString() + "','" + rdr["Batch_ID"].ToString() + "','" + rdr["closing_stock"].ToString() + "'," + cl_sk.ToString() + ")", ref x);
                                else
                                    dbobj1.Insert_or_Update("insert into batch_transaction values(" + (SNo++) + ",'" + lblInvoiceNo + "','Sales Invoice',Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate) + "',103),'" + rdr["ProductID"].ToString() + "','" + rdr["Batch_ID"].ToString() + "','" + rdr["closing_stock"].ToString() + "'," + cl_sk.ToString() + ")", ref x);

                                //dbobj1.Insert_or_Update("update batch_transaction set trans_date=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103),qty='"+rdr["closing_stock"].ToString()+"',closing_stock="+cl_sk.ToString()+" where trans_id='"+dropInvoiceNo.SelectedItem.Text+"' and trans_type='Sales Invoice' and prod_id='"+rdr["ProductID"].ToString()+"'",ref x);	

                                //count-=int.Parse(rdr["qty"].ToString());

                                /***********add by vikas 19.06.09 *****************/
                                dbobj1.Insert_or_Update("update batchno set qty=" + cl_sk + " where prod_id='" + rdr["productid"].ToString() + "' and batch_id='" + rdr["batch_id"].ToString() + "'", ref x);
                                /****************************/

                                count -= int.Parse(rdr["closing_stock"].ToString());

                                /*****Add by vikas 10.06.09*********
								if(lblInvoiceNo.Visible==true)
									dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblInvoiceNo.Text+"','Sales Invoice',Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103),'"+rdr["ProductID"].ToString()+"','0','"+count.ToString()+"',"+cl_sk.ToString()+")",ref x);
								else
									dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+dropInvoiceNo.SelectedItem.Text+"','Sales Invoice',Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103),'"+rdr["ProductID"].ToString()+"','0','"+count.ToString()+"',"+cl_sk.ToString()+")",ref x);
								/*****end*********/
                            }
                        }
                    }
                }
                rdr.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/CustomerUpdate")]
        public bool CustomerUpdate(List<string> sales)
        {
            try
            {
                string CustID = sales[0].ToString();
                string Invoice_Date = sales[1].ToString();

                SqlDataReader rdr = null;
                SqlCommand cmd;
                InventoryClass obj = new InventoryClass();
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                double Bal = 0;
                string BalType = "", str = "";
                int i = 0;
                //************************
                //string[] CheckDate = Invoice_Date.Split(new char[] { ' ' }, Invoice_Date.Length);
                //if (DateTime.Compare(System.Convert.ToDateTime(CheckDate[0].ToString()), System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))) > 0)
                //    Invoice_Date = GenUtil.str2DDMMYYYY(lblInvoiceDate.Text);

                rdr = obj.GetRecordSet("select top 1 Entry_Date from AccountsLedgerTable where Ledger_ID=(select Ledger_ID from Ledger_Master l,Customer c where Cust_Name=Ledger_Name and Cust_ID='" + CustID + "') and Entry_Date<='" + GenUtil.str2MMDDYYYY(Invoice_Date) + "' order by entry_date desc");
                if (rdr.Read())
                    str = "select * from AccountsLedgerTable where Ledger_ID=(select Ledger_ID from Ledger_Master l,Customer c where Cust_Name=Ledger_Name and Cust_ID='" + CustID + "') and Entry_Date>='" + GenUtil.str2MMDDYYYY(rdr.GetValue(0).ToString()) + "' order by entry_date";
                else
                    str = "select * from AccountsLedgerTable where Ledger_ID=(select Ledger_ID from Ledger_Master l,Customer c where Cust_Name=Ledger_Name and Cust_ID='" + CustID + "') order by entry_date";
                rdr.Close();
                //*************************
                //string str="select * from AccountsLedgerTable where Ledger_ID=(select Ledger_ID from Ledger_Master l,Customer c where Cust_Name=Ledger_Name and Cust_ID='"+CustID+"') order by entry_date";
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
                                Bal += double.Parse(rdr["Credit_Amount"].ToString());
                                BalType = "Cr";
                            }
                            else
                            {
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
                                Bal += double.Parse(rdr["Debit_Amount"].ToString());
                            else
                            {
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
                        cmd = new SqlCommand("update AccountsLedgerTable set Balance='" + Bal.ToString() + "',Bal_Type='" + BalType + "' where Ledger_ID='" + rdr["Ledger_ID"].ToString() + "' and Particulars='" + rdr["Particulars"].ToString() + "'", Con);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        Con.Close();
                    }
                }
                rdr.Close();

                //*************************
                rdr = obj.GetRecordSet("select top 1 EntryDate from CustomerLedgerTable where CustID='" + CustID.ToString() + "' and EntryDate<='" + GenUtil.str2MMDDYYYY(Invoice_Date) + "' order by entrydate desc");
                if (rdr.Read())
                    str = "select * from CustomerLedgerTable where CustID='" + CustID + "' and EntryDate>='" + GenUtil.str2MMDDYYYY(rdr.GetValue(0).ToString()) + "' order by entrydate";
                else
                    str = "select * from CustomerLedgerTable where CustID='" + CustID + "' order by entrydate";
                rdr.Close();
                //*************************
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
                        cmd = new SqlCommand("update CustomerLedgerTable set Balance='" + Bal.ToString() + "',BalanceType='" + BalType + "' where CustID='" + rdr["CustID"].ToString() + "' and Particular='" + rdr["Particular"].ToString() + "'", Con);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        Con.Close();
                    }
                }
                rdr.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/Update_Customer_Balance")]
        public bool Update_Customer_Balance(List<string> sales)
        {
            int x = 0;
            string invoiceDate = sales[0].ToString();
            string NetAmount = sales[1].ToString();
            string CustID = sales[2].ToString();
            try
            {
                DbOperations_LATEST.DBUtil dbobj = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
                dbobj.Insert_or_Update("update customer_balance set DR_Amount=DR_Amount-" + NetAmount + " where cust_id='" + CustID + "'", ref x);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/UpdateProductQty")]
        public bool UpdateProductQty(SalesModels sales)
        {
            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader rdr;
                SqlCommand cmd;
                for (int i = 0; i < sales.ProductType.Count; i++)
                {

                    SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                    string str = "";
                    if (sales.ProductType[i] == "" || sales.ProductName[i] == "" || sales.ProductQty[i] == "")
                        continue;
                    else
                    {
                        str = "select Prod_ID from Products where Category='" + sales.ProductType[i].ToString() + "' and Prod_Name='" + sales.ProductName[i].ToString() + "' and Pack_Type='" + sales.ProductPack[i].ToString() + "'";
                        rdr = obj.GetRecordSet(str);
                        if (rdr.Read())
                        {
                            Con.Open();
                            //cmd = new SqlCommand("update Stock_Master set sales=sales-"+double.Parse(ProductQty[i].ToString())+", Closing_Stock=Closing_Stock+"+double.Parse(ProductQty[i].ToString())+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and cast(floor(cast(cast(Stock_Date as datetime) as float)) as datetime)='"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text)+"'",Con);
                            string s = "update Stock_Master set sales=sales-" + double.Parse(sales.ProductQty[i].ToString()) + ", Closing_Stock=Closing_Stock+" + double.Parse(sales.ProductQty[i].ToString()) + " where ProductID='" + rdr["Prod_ID"].ToString() + "' and cast(floor(cast(cast(Stock_Date as datetime) as float)) as datetime)=Convert(datetime,'" + sales.Invoice_Date + "',103)";
                            cmd = new SqlCommand("update Stock_Master set sales=sales-" + double.Parse(sales.ProductQty[i].ToString()) + ", Closing_Stock=Closing_Stock+" + double.Parse(sales.ProductQty[i].ToString()) + " where ProductID='" + rdr["Prod_ID"].ToString() + "' and cast(floor(cast(cast(Stock_Date as datetime) as float)) as datetime)=Convert(datetime,'" + sales.Invoice_Date + "',103)", Con);
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            Con.Close();
                        }
                        rdr.Close();
                        if (sales.SchProductType == null || sales.SchProductName == null || sales.SchProductQty == null || sales.SchProductPack == null)
                            continue;
                        //if (sales.SchProductType[i] == "" || sales.SchProductName[i] == "" || sales.SchProductQty[i] == "")
                        //    continue;
                        else
                        {
                            str = "select Prod_ID from Products where Category='" + sales.SchProductType[i].ToString() + "' and Prod_Name='" + sales.SchProductName[i].ToString() + "' and Pack_Type='" + sales.SchProductPack[i].ToString() + "'";
                            rdr = obj.GetRecordSet(str);
                            if (rdr.Read())
                            {
                                Con.Open();
                                //cmd = new SqlCommand("update Stock_Master set sales=sales-"+double.Parse(SchProductQty[i].ToString())+", Closing_Stock=Closing_Stock+"+double.Parse(SchProductQty[i].ToString())+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and cast(floor(cast(cast(Stock_Date as datetime) as float)) as datetime)='"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text)+"'",Con);
                                string s = "update Stock_Master set sales=sales-" + double.Parse(sales.ProductQty[i].ToString()) + ", Closing_Stock=Closing_Stock+" + double.Parse(sales.ProductQty[i].ToString()) + " where ProductID='" + rdr["Prod_ID"].ToString() + "' and cast(floor(cast(cast(Stock_Date as datetime) as float)) as datetime)=Convert(datetime,'" + sales.Invoice_Date + "',103)";
                                cmd = new SqlCommand("update Stock_Master set salesfoc=salesfoc-" + double.Parse(sales.SchProductQty[i].ToString()) + ", Closing_Stock=Closing_Stock+" + double.Parse(sales.SchProductQty[i].ToString()) + " where ProductID='" + rdr["Prod_ID"].ToString() + "' and cast(floor(cast(cast(Stock_Date as datetime) as float)) as datetime)=Convert(datetime,'" + sales.Invoice_Date + "',103)", Con);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                Con.Close();
                            }
                            rdr.Close();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Sales/DeleteAccountsledgertableData")]
        public bool DeleteAccountsledgertableData(string id)
        {
            try
            {
                SqlCommand cmd;
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                Con.Open();
                cmd = new SqlCommand("delete from Accountsledgertable where Particulars='Sales Invoice (" + id + ")'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //[HttpDelete]
        //[Route("api/Sales/DeleteSalesInvoice")]
        //public int DeleteSalesInvoice(string id, string customerName, string netAmount, string place, string [] ProductType, string[] ProductQty, string invoiceDate, string[] ProductName, string[] ProductPack, string [] SchProductQty, string [] SchProductType, string [] SchProductPack, string [] SchProductName, string [] TypeSch)
        //{
        //    GetFromDateToDate();
        //    string txtval = "";

        //        string [] ProdType = { TypeSch[1], TypeSch[2], TypeSch[3], TypeSch[4], TypeSch[5], TypeSch[6], TypeSch[7], TypeSch[8], TypeSch[9], TypeSch[10], TypeSch[11], TypeSch[12] };
        //        //TextBox[] ProdName1 = { txtProdsch1, txtProdsch2, txtProdsch3, txtProdsch4, txtProdsch5, txtProdsch6, txtProdsch7, txtProdsch8, txtProdsch9, txtProdsch10, txtProdsch11, txtProdsch12 };
        //        //TextBox[] PackType1 = { txtPacksch1, txtPacksch2, txtPacksch3, txtPacksch4, txtPacksch5, txtPacksch6, txtPacksch7, txtPacksch8, txtPacksch9, txtPacksch10, txtPacksch11, txtPacksch12 };
        //        //TextBox[] Qty1 = { txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12 };

        //        InventoryClass obj = new InventoryClass();
        //        SqlDataReader rdr = null;
        //        SqlCommand cmd;
        //        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
        //        Con.Open();
        //        cmd = new SqlCommand("delete from Sales_Master where Invoice_No='" + FromDate + ToDate + id + "'", Con);
        //        cmd.ExecuteNonQuery();
        //        Con.Close();
        //        cmd.Dispose();
        //        //				Con.Open();
        //        //				cmd = new SqlCommand("delete from monthwise1 where Invoice_No='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text+"'",Con);
        //        //				cmd.ExecuteNonQuery();
        //        //				Con.Close();
        //        //				cmd.Dispose();
        //        Con.Open();
        //        cmd = new SqlCommand("delete from Sales_Oil where Invoice_No='" + FromDate + ToDate + id + "'", Con);
        //        cmd.ExecuteNonQuery();
        //        Con.Close();
        //        cmd.Dispose();
        //        Con.Open();
        //        cmd = new SqlCommand("delete from Accountsledgertable where Particulars='Sales Invoice (" + FromDate + ToDate + id + ")'", Con);
        //        cmd.ExecuteNonQuery();
        //        Con.Close();
        //        cmd.Dispose();
        //        //string str="select * from AccountsLedgerTable where Ledger_ID=(select Ledger_ID from Ledger_Master where Ledger_Name='"+DropCustName.SelectedItem.Text+"') order by entry_date";
        //        //txtval = customerName; //Add by vikas sharma 
        //                                                                     //Comment by vikas 1.05.09 string str="select * from AccountsLedgerTable where Ledger_ID = (select Ledger_ID from Ledger_Master where Ledger_Name='"+text1.Value+"') order by entry_date";
        //        string str = "select * from AccountsLedgerTable where Ledger_ID = (select Ledger_ID from Ledger_Master where Ledger_Name='" + customerName + "') order by entry_date";
        //        rdr = obj.GetRecordSet(str);
        //        double Bal = 0;
        //        while (rdr.Read())
        //        {
        //            if (rdr["Bal_Type"].ToString().Equals("Dr"))
        //                Bal += double.Parse(rdr["Debit_Amount"].ToString()) - double.Parse(rdr["Credit_Amount"].ToString());
        //            else
        //                Bal += double.Parse(rdr["Credit_Amount"].ToString()) - double.Parse(rdr["Debit_Amount"].ToString());
        //            if (Bal.ToString().StartsWith("-"))
        //                Bal = double.Parse(Bal.ToString().Substring(1));
        //            Con.Open();
        //            cmd = new SqlCommand("update AccountsLedgerTable set Balance='" + Bal.ToString() + "' where Ledger_ID='" + rdr["Ledger_ID"].ToString() + "' and Particulars='" + rdr["Particulars"].ToString() + "'", Con);
        //            cmd.ExecuteNonQuery();
        //            Con.Close();
        //            cmd.Dispose();
        //        }
        //        rdr.Close();
        //        Con.Open();
        //        cmd = new SqlCommand("delete from Customerledgertable where Particular='Sales Invoice (" + FromDate + ToDate + id + ")'", Con);
        //        cmd.ExecuteNonQuery();
        //        Con.Close();
        //        cmd.Dispose();
        //        //string str1="select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer where Cust_Name='"+DropCustName.SelectedItem.Text+"') order by entrydate";

        //        //Comment by vikas sharma 1.05.09 string str1="select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer where Cust_Name='"+text1.Value+"') order by entrydate";
        //        string str1 = "select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer where Cust_Name='" + customerName + "') order by entrydate";
        //        rdr = obj.GetRecordSet(str1);
        //        Bal = 0;
        //        while (rdr.Read())
        //        {
        //            if (rdr["BalanceType"].ToString().Equals("Dr."))
        //                Bal += double.Parse(rdr["DebitAmount"].ToString()) - double.Parse(rdr["CreditAmount"].ToString());
        //            else
        //                Bal += double.Parse(rdr["CreditAmount"].ToString()) - double.Parse(rdr["DebitAmount"].ToString());
        //            if (Bal.ToString().StartsWith("-"))
        //                Bal = double.Parse(Bal.ToString().Substring(1));
        //            Con.Open();
        //            cmd = new SqlCommand("update CustomerLedgerTable set Balance='" + Bal.ToString() + "' where CustID='" + rdr["CustID"].ToString() + "' and Particular='" + rdr["Particular"].ToString() + "'", Con);
        //            cmd.ExecuteNonQuery();
        //            Con.Close();
        //            cmd.Dispose();
        //        }
        //        rdr.Close();
        //        //				Con.Open();
        //        //				cmd = new SqlCommand("delete from LedgDetails where Bill_No='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text+"'",Con);
        //        //				cmd.ExecuteNonQuery();
        //        //				Con.Close();
        //        //				cmd.Dispose();
        //        //				Con.Open();
        //        //				cmd = new SqlCommand("delete from Invoice_Transaction where Invoice_No='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text+"'",Con);
        //        //				cmd.ExecuteNonQuery();
        //        //				Con.Close();
        //        //				cmd.Dispose();
        //        Con.Open();
        //        //cmd = new SqlCommand("Update Customer_Balance set DR_Amount = DR_Amount-'"+double.Parse(txtNetAmount.Text)+"' where Cust_ID = (select Cust_ID from Customer where Cust_Name='"+DropCustName.SelectedItem.Text+"' and city='"+lblPlace.Value+"')",Con);

        //        //Comment by vikas sharma 1.05.09 cmd = new SqlCommand("Update Customer_Balance set DR_Amount = DR_Amount-'"+double.Parse(txtNetAmount.Text)+"' where Cust_ID = (select Cust_ID from Customer where Cust_Name='"+text1.Value+"' and city='"+lblPlace.Value+"')",Con);
        //        cmd = new SqlCommand("Update Customer_Balance set DR_Amount = DR_Amount-'" + double.Parse(netAmount) + "' where Cust_ID = (select Cust_ID from Customer where Cust_Name='" + customerName + "' and city='" + place + "')", Con);
        //        cmd.ExecuteNonQuery();
        //        Con.Close();
        //        cmd.Dispose();
        //        for (int i = 0; i < 12; i++)
        //        {
        //            //if(DropType[i].SelectedItem.Text.Equals("Type") || ProdName[i].Value=="" || PackType[i].Value=="")
        //            //if(DropType[i].SelectedItem.Text.Equals("Type"))
        //            if (ProductType[i].ToString().Equals(""))
        //                continue;
        //            else
        //            {
        //                Con.Open();
        //                //cmd = new SqlCommand("update Stock_Master set sales=sales-'"+double.Parse(Qty[i].Text)+"',closing_stock=closing_stock+'"+double.Parse(Qty[i].Text)+"' where ProductID=(select Prod_ID from Products where Category='"+DropType[i].SelectedItem.Text+"' and Prod_Name='"+ProdName[i].Value+"' and Pack_Type='"+PackType[i].Value+"') and cast(stock_date as smalldatetime)='"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text)+"'",Con);
        //                cmd = new SqlCommand("update Stock_Master set sales=sales-'" + double.Parse(ProductQty[i].ToString()) + "',closing_stock=closing_stock+'" + double.Parse(ProductQty[i].ToString()) + "' where ProductID=(select Prod_ID from Products where Category='" + ProductType[i].ToString() + "' and Prod_Name='" + ProductName[i].ToString() + "' and Pack_Type='" + ProductPack[i].ToString() + "') and cast(floor(cast(stock_date as float)) as datetime)=Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate) + "',103)", Con);
        //                cmd.ExecuteNonQuery();
        //                Con.Close();
        //                cmd.Dispose();
        //            }
        //            //if(ProdType[i].Text=="" || ProdName1[i].Text=="" || PackType1[i].Text=="")
        //            if (ProdType[i].ToString() == "")
        //                continue;
        //            else
        //            {
        //                Con.Open();
        //                //cmd = new SqlCommand("update Stock_Master set salesfoc=salesfoc-'"+double.Parse(Qty1[i].Text)+"',closing_stock=closing_stock+'"+double.Parse(Qty1[i].Text)+"' where ProductID=(select Prod_ID from Products where Category='"+ProdType[i].Text+"' and Prod_Name='"+ProdName1[i].Text+"' and Pack_Type='"+PackType1[i].Text+"') and cast(stock_date as smalldatetime)='"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text)+"'",Con);
        //                cmd = new SqlCommand("update Stock_Master set salesfoc=salesfoc-'" + double.Parse(SchProductQty[i].ToString()) + "',closing_stock=closing_stock+'" + double.Parse(SchProductQty[i].ToString()) + "' where ProductID=(select Prod_ID from Products where Category='" + SchProductType[i].ToString() + "' and Prod_Name='" + SchProductName[i].ToString() + "' and Pack_Type='" + SchProductPack[i].ToString() + "') and cast(floor(cast(stock_date as float)) as datetime)=Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate) + "',103)", Con);
        //                cmd.ExecuteNonQuery();
        //                Con.Close();
        //                cmd.Dispose();
        //            }
        //        }

        //        /***********Add by vikas 16.11.2012*****************/

        //        Con.Open();
        //        cmd = new SqlCommand("Update OVD set Sale_Trans_Id='0' , sale_qty='0' where Sale_Trans_Id='" + FromDate + ToDate + id + "'", Con);
        //        cmd.ExecuteNonQuery();
        //        Con.Close();
        //        cmd.Dispose();

        //        /***********End*****************/
        //        //SeqStockMaster(ProductType, ProductName, ProductQty, ProductPack, SchProductType, SchProductName, SchProductQty, SchProductPack);
        //        //MessageBox.Show("Sales Transaction Deleted");
        //        //CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:btnDelete_Click - InvoiceNo : " + FromDate + ToDate + id + " Deleted, user : " + uid);
        //        //Clear();
        //        //clear1();
        //        int iNextInvoiceNo = GetNextInvoiceNo();
        //        //GetProducts();
        //        //FetchData();
        //        //getschemefoe();
        //        //getscheme();
        //    //getscheme1();
        //    //lblInvoiceNo.Visible = true;
        //    //dropInvoiceNo.Visible = false;
        //    //btnEdit.Visible = true;        
        //    return iNextInvoiceNo;
        //}

        [HttpGet]
        [Route("api/Sales/GetBatchDetailsForProdCodeSales")]
        public List<string> GetBatchDetailsForProdCodeSales(string ProdCode, string arrProdCat1, string arrProdCat2, string InvoiceNo)
        {
            List<string> btQty = new List<string>();
            try
            {

                string str = "select b.batch_no,bt.qty from batch_transaction bt,batchno b where b.prod_id=bt.prod_id and b.prod_id=(select prod_id from products where Prod_Code='" + ProdCode + "' and Prod_Name='" + arrProdCat1 + "' and Pack_Type='" + arrProdCat2 + "') and b.batch_id=bt.batch_id and bt.trans_id='" + InvoiceNo + "' and trans_type='Sales Invoice'";
                SqlDataReader SqlDtr;
                InventoryClass obj = new InventoryClass();
                SqlDtr = obj.GetRecordSet(str);
                if (SqlDtr.HasRows)
                {
                    while (SqlDtr.Read())
                    {
                        btQty.Add(SqlDtr.GetValue(1).ToString());

                    }
                }
                return btQty;
            }
            catch (Exception ex)
            {
                return btQty;
            }

        }

        //[HttpGet]
        //[Route("api/Sales/GetBatchDetailsDuringUpdateSales")]
        //public List<string> GetBatchDetailsDuringUpdateSales(string invoiceDate)
        //{
        //    List<string> InDate = new List<string>();
        //    try
        //    {

        //        string str = "select invoice_date from sales_master where invoice_no=" + invoiceDate + "";
        //        SqlDataReader SqlDtr;
        //        InventoryClass obj = new InventoryClass();
        //        SqlDtr = obj.GetRecordSet(str);
        //        if (SqlDtr.Read())
        //            InDate = SqlDtr.GetValue(0).ToString();
        //        else
        //            InDate = "";
        //        SqlDtr.Close();
        //        return InDate;
        //    }
        //    catch (Exception ex)
        //    {
        //        return InDate;
        //    }

        //}
        [HttpGet]
        [Route("api/Sales/GetinvoiceDate")]
        public string GetinvoiceDate(string invoiceDate)
        {
            string InDate = string.Empty;
            try
            {
                string str = "select invoice_date from sales_master where invoice_no=" + invoiceDate + "";
                SqlDataReader SqlDtr;
                InventoryClass obj = new InventoryClass();
                SqlDtr = obj.GetRecordSet(str);
                if (SqlDtr.Read())
                    InDate = SqlDtr.GetValue(0).ToString();
                else
                    InDate = "";
                SqlDtr.Close();
                return InDate;
            }
            catch (Exception ex)
            {
                return InDate;
            }
        }

        [HttpGet]
        [Route("api/Sales/GetProdCode_PackType")]
        public List<string> GetProdCode_PackType(string ProdType1, string ProdType2)
        {
            string prodCode = string.Empty;
            string packType = string.Empty;
            List<string> sales = new List<string>();
            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                string str = "select Prod_Code,Total_Qty from Products where Prod_Name='" + ProdType1 + "' and Pack_Type='" + ProdType2 + "'";
                SqlDtr = obj.GetRecordSet(str);
                if (SqlDtr.Read())
                {
                    prodCode = SqlDtr.GetValue(0).ToString();
                    packType = SqlDtr.GetValue(1).ToString();
                }
                else
                {
                    prodCode = "";
                    packType = "";
                }
                sales.Add(prodCode);
                sales.Add(packType);
                SqlDtr.Close();
                return sales;
            }
            catch (Exception ex)
            {
                return sales;
            }
        }

        [HttpGet]
        [Route("api/Sales/GetCustomer")]
        public List<string> GetCustomer(string txtval)
        {
            string addr = string.Empty;
            string ssc = string.Empty;
            string TinNo = string.Empty;

            List<string> sales = new List<string>();
            try
            {
                DbOperations_LATEST.DBUtil dbobj = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr = null;

                dbobj.SelectQuery("select * from customer where cust_name='" + txtval + "'", ref SqlDtr);
                if (SqlDtr.Read())
                {
                    addr = SqlDtr["Address"].ToString();
                    ssc = SqlDtr["sadbhavnacd"].ToString();
                    TinNo = SqlDtr["Tin_No"].ToString();
                }

                sales.Add(addr);
                sales.Add(ssc);
                sales.Add(TinNo);

                SqlDtr.Close();
                return sales;
            }
            catch (Exception ex)
            {
                return sales;
            }
        }

        //[HttpGet]
        //[Route("api/Sales/GetBatchDetails")]
        //public SalesModel GetBatchDetails(string[] ProdCode, string  string arrProdCat1, string arrProdCat2, string invoiceNo)
        //{
        //    SalesModel sales = new SalesModel();
        //    try
        //    {
        //        return sales;
        //    }

        //    catch (Exception ex)
        //    {
        //        return sales;
        //    }
        //}

        [HttpGet]
        [Route("api/Sales/GetschProdCode")]
        public string GetschProdCode(string ProdSchType1, string ProdSchType2)
        {
            string schProdCode = string.Empty;

            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                string str = "select Prod_Code from Products where Prod_Name='" + ProdSchType1 + "' and Pack_Type='" + ProdSchType2 + "'";
                SqlDtr = obj.GetRecordSet(str);
                if (SqlDtr.Read())
                {
                    schProdCode = SqlDtr.GetValue(0).ToString();
                }
                else
                {
                    schProdCode = "";
                }

                SqlDtr.Close();
                return schProdCode;
            }
            catch (Exception ex)
            {
                return schProdCode;
            }
        }

        [HttpGet]
        [Route("api/Sales/GetCountofInvoiceNo")]
        public int GetCountofInvoiceNo(string invoiceNo)
        {
            int count = 0;
            try
            {
                DbOperations_LATEST.DBUtil dbobj = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
                dbobj.ExecuteScalar("Select count(Invoice_No) from Sales_Master where Invoice_No = " + invoiceNo, ref count);
                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        [HttpGet]
        [Route("api/Sales/GetCategory")]
        public string GetCategory(string prodName1, string prodName2)
        {
            string category = string.Empty;
            try
            {
                string sql = "";
                string prod_id = "";

                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;

                // Comment by vikas sharma 22.04.09 sql="select prod_id,category from Products where prod_name='"+arrProdName[0].ToString()+"' and pack_type='"+arrProdName[1].ToString()+"'";
                sql = "select prod_id,category from Products where prod_name='" + prodName1.ToString() + "' and pack_type='" + prodName2.ToString() + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    //txtmwid1.Text=SqlDtr.GetValue(0).ToString();
                    prod_id = SqlDtr.GetValue(0).ToString();
                    if (SqlDtr.GetValue(1).ToString().StartsWith("2T") || SqlDtr.GetValue(1).ToString().StartsWith("4T") || SqlDtr.GetValue(1).ToString().StartsWith("2t") || SqlDtr.GetValue(1).ToString().StartsWith("4t"))
                        category = SqlDtr.GetValue(1).ToString();
                }
                SqlDtr.Close();
                return category;
            }
            catch (Exception ex)
            {
                return category;
            }
        }

        /// <summary>
        /// This method checks the price updation for all the products is available or not?
        /// </summary>
        [HttpGet]
        [Route("api/Sales/GetProducts")]
        public SalesModels GetProducts()
        {
            SalesModels sales = new SalesModels();
            try
            {
                InventoryClass obj = new InventoryClass();
                InventoryClass obj1 = new InventoryClass();
                SqlDataReader SqlDtr;
                string sql;
                SqlDataReader rdr = null;
                int count = 0;
                int count1 = 0;
                dbobj.ExecuteScalar("Select Count(Prod_id) from  products", ref count);
                dbobj.ExecuteScalar("select count(distinct p.Prod_ID ) from products p, Price_Updation pu where p.Prod_id = pu.Prod_id", ref count1);
                //				sql = "select distinct p.Prod_ID,Category,Prod_Name,Pack_Type,Unit from products p, Price_Updation pu where p.Prod_id = pu.Prod_id order by Category,Prod_Name";
                //				SqlDtr = obj.GetRecordSet(sql); 
                //				while(SqlDtr.Read())
                //				{			
                //					count1 = count1+1;
                //				}					
                //				SqlDtr.Close();
                if (count != count1)
                {
                    sales.Message = "Price updation not available for some products";
                }

                #region Fetch the Product Types and fill in the ComboBoxes
                string str = "", MinMax = "";
                //sql="select distinct p.Prod_ID,Category,Prod_Name,Pack_Type,Unit,minlabel,maxlabel,reorderlable from products p, Price_Updation pu where p.Prod_id = pu.Prod_id order by Category,Prod_Name";
                sql = "select distinct p.Prod_ID,Category,Prod_Name,Pack_Type,Unit,minlabel,maxlabel,reorderlable from products p, Price_Updation pu where p.Prod_id = pu.Prod_id order by Category,Prod_Name";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    #region Fetch Sales Rate
                    /********
					sql= "select top 1 Sal_Rate from Price_Updation where Prod_ID="+SqlDtr["Prod_ID"]+" order by eff_date desc";
					//dbobj.SelectQuery(sql,ref rdr); 
					rdr = obj1.GetRecordSet(sql);
					if(rdr.Read())
					{
						if(double.Parse(rdr.GetValue(0).ToString())==0)
						{
							rdr.Close();
							continue;
						}
					}
					rdr.Close();
					***********/
                    //str=str+ SqlDtr["Category"]+":"+SqlDtr["Prod_Name"]+":"+SqlDtr["Pack_Type"];
                    sql = "select top 1 Pur_Rate from Price_Updation where Prod_ID=" + SqlDtr["Prod_ID"] + " and Pur_Rate<>0 order by eff_date desc";
                    //dbobj.SelectQuery(sql,ref rdr); 
                    rdr = obj1.GetRecordSet(sql);
                    if (rdr.Read())
                    {
                        if (double.Parse(rdr["Pur_Rate"].ToString()) != 0)
                        {
                            str = str + SqlDtr["Category"] + ":" + SqlDtr["Prod_Name"] + ":" + SqlDtr["Pack_Type"];
                            str = str + ":" + rdr["Pur_Rate"];
                        }
                        else
                        {
                            rdr.Close();
                            continue;
                        }
                    }
                    else
                        str = str + ":0";
                    rdr.Close();

                    //********
                    MinMax = MinMax + SqlDtr["Prod_Name"] + ":" + SqlDtr["Pack_Type"] + ":" + SqlDtr["minlabel"] + ":" + SqlDtr["maxlabel"] + ":" + SqlDtr["reorderlable"] + "~";
                    //********
                    #endregion

                    #region Fetch Closing Stock
                    sql = "select top 1 Closing_Stock from Stock_Master where productid=" + SqlDtr["Prod_ID"] + " order by stock_date desc";
                    //dbobj.SelectQuery(sql,ref rdr); 
                    rdr = obj1.GetRecordSet(sql);
                    if (rdr.Read())
                        //**str=str+":"+rdr["Closing_Stock"]+":"+SqlDtr["Unit"]+",";
                        str = str + ":" + rdr["Closing_Stock"] + ":" + SqlDtr["Unit"];
                    else
                        //str=str+":0"+":"+SqlDtr["Unit"]+",";
                        str = str + ":0" + ":" + SqlDtr["Unit"];
                    rdr.Close();
                    #endregion
                    //*************
                    #region Fetch Scheme 
                    //sql="select discount from schemeupdation where Prod_ID="+SqlDtr["Prod_ID"]+"";
                    sql = "select discount from oilscheme where ProdID=" + SqlDtr["Prod_ID"] + "";
                    //dbobj.SelectQuery(sql,ref rdr);
                    rdr = obj1.GetRecordSet(sql);
                    if (rdr.Read())
                        str = str + ":" + rdr["discount"] + ",";
                    else
                        str = str + ":0" + ",";
                    rdr.Close();
                    #endregion
                    //*******************
                }
                SqlDtr.Close();
                sales.TempText = str;
                sales.Tempminmax = MinMax;
                return sales;
                #endregion
            }
            catch (Exception ex)
            {
                return sales;
            }
        }

        /// <summary>
        /// This method returns the next Invoice No
        /// </summary>
        [HttpGet]
        [Route("api/Sales/GetNextInvoiceNo")]
        public int GetNextInvoiceNo()
        {
            GetFromDateToDate();

            string nextInvoiceNo = string.Empty;
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr, rdr = null;
            string sql;

            #region Fetch the Next Invoice Number
            sql = "select max(Invoice_No) from Sales_Master";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                string InNo = SqlDtr.GetValue(0).ToString();
                string fdt = "", No = "";
                int n = 0;
                if (InNo != "" && InNo.Length >= 4)
                {
                    if (FromDate.StartsWith("0"))
                    {
                        fdt = FromDate.Substring(1) + ToDate;
                        No = InNo.Substring(0, 3);
                    }
                    else
                    {
                        fdt = FromDate + ToDate;
                        if (fdt.Length == 3)
                            No = InNo.Substring(0, 3);
                        else
                            No = InNo.Substring(0, 4);
                    }
                }
                else
                    fdt = "0";
                if (fdt == No)
                {
                    //lblInvoiceNo.Text =SqlDtr.GetValue(0).ToString ();				
                    if (No.Length == 3)
                        InNo = InNo.Substring(3);
                    else
                        InNo = InNo.Substring(4);
                    n = int.Parse(InNo);
                    nextInvoiceNo = System.Convert.ToString(++n);
                }
                else
                //if(lblInvoiceNo.Text=="")
                {   //lblInvoiceNo.Text ="1001";
                    dbobj.SelectQuery("select * from organisation", ref rdr);
                    if (rdr.Read())
                    {
                        nextInvoiceNo = rdr["StartInvoice"].ToString();
                    }
                    else
                        nextInvoiceNo = "1";
                }

            }
            SqlDtr.Close();
            #endregion
            int iNextInvoiceNo = Int32.Parse(nextInvoiceNo);
            return iNextInvoiceNo;
        }

        [HttpGet]
        [Route("api/Sales/GetCustNameCityData")]
        public List<string> GetCustNameCityData()
        {

            List<string> custNameCity = new List<string>();
            try
            {

                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr = null;
                string sql = "select cust_name,City from customer order by cust_name ";
                //sql="select cust_name from customer where cust_type='Ro-1' order by cust_name ";
                SqlDtr = obj.GetRecordSet(sql);
                int i = 0;
                string val = "";
                string texthidden = string.Empty;
                while (SqlDtr.Read())
                {
                    //texthidden.Value+=SqlDtr.GetValue(0).ToString()+","; //Comment by vikas sharma 27.04.09
                    texthidden += SqlDtr.GetValue(0).ToString() + ":" + SqlDtr.GetValue(1).ToString() + ",";
                    //MessageBox.Show(texthidden.Value);
                    //Request.Params.Set("DropCustName",SqlDtr.GetValue(0).ToString());
                    val += SqlDtr.GetValue(0).ToString() + ",";

                    //MessageBox.Show(val);
                    i++;
                }
                SqlDtr.Close();
                custNameCity.Add(texthidden);
                custNameCity.Add(val);
                return custNameCity;
            }
            catch (Exception ex)
            {
                return custNameCity;
            }
        }

        [HttpGet]
        [Route("api/Sales/PriceUpdation")]
        public string PriceUpdation()
        {
            string txtMainIGST = string.Empty;
            try
            {
                InventoryClass obj = new InventoryClass();
                var dsPriceUpdation = obj.ProPriceUpdation();
                var dtTable = dsPriceUpdation.Tables[0];
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    txtMainIGST = txtMainIGST + dtTable.Rows[i][0].ToString();//ProductCode
                    txtMainIGST = txtMainIGST + "|" + dtTable.Rows[i][1];//ProductName 
                    txtMainIGST = txtMainIGST + "|" + dtTable.Rows[i][2];//ProductId
                    txtMainIGST = txtMainIGST + "|" + dtTable.Rows[i][3];//IGST
                    txtMainIGST = txtMainIGST + "|" + dtTable.Rows[i][4];//cGST
                    txtMainIGST = txtMainIGST + "|" + dtTable.Rows[i][5];//sGST
                    txtMainIGST = txtMainIGST + "|" + dtTable.Rows[i][6];//HSN
                    txtMainIGST = txtMainIGST + "~";


                }
                txtMainIGST = txtMainIGST.Substring(0, txtMainIGST.LastIndexOf("~"));
                return txtMainIGST;
            }
            catch (Exception ex)
            {
                return txtMainIGST;
            }
        }

        [HttpGet]
        [Route("api/Sales/FillInvoceNoDropdown")]
        public List<string> FillInvoceNoDropdown(string invoiceFromDate, string invoiceToDate)
        {
            GetFromDateToDate();
            List<string> dropInvoiceNo = new List<string>();
            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                string sql = "select Invoice_No from Sales_Master where cast(floor(cast(Invoice_Date as float)) as datetime) >= '" + GenUtil.str2MMDDYYYY(invoiceFromDate) + "' and cast(floor(cast(Invoice_Date as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(invoiceToDate) + "' and Invoice_No like '" + FromDate + ToDate + "%' order by Invoice_No";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    if (FromDate.StartsWith("0"))
                        dropInvoiceNo.Add(SqlDtr.GetValue(0).ToString().Substring(2));
                    else
                        dropInvoiceNo.Add(SqlDtr.GetValue(0).ToString().Substring(3));
                }
                SqlDtr.Close();
                return dropInvoiceNo;
            }
            catch (Exception ex)
            {
                return dropInvoiceNo;
            }
        }

        [HttpGet]
        [Route("api/Sales/GetOrderInvoice")]
        public List<string> GetOrderInvoice()
        {
            List<string> orderInvoice = new List<string>();
            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                string sql;
                ArrayList order_no = new ArrayList();

                #region Fetch the Next Invoice Number               
                orderInvoice.Add("Select");
                sql = "select Order_No from Order_col_Master where status=0 order by Order_No";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    orderInvoice.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();

                sql = " select distinct Order_id,bo_1,bo_2,bo_3 from ovd where cast(item_qty as float)>cast(sale_qty as float) ";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    if (SqlDtr["Bo_3"].ToString() == null || SqlDtr["Bo_3"].ToString() == "")
                    {
                        if (SqlDtr["Bo_2"] == null || SqlDtr["Bo_2"].ToString() == "")
                        {
                            if (SqlDtr["Bo_1"].ToString() == null || SqlDtr["Bo_1"].ToString() == "")
                            {
                            }
                            else
                            {
                                if (SqlDtr["Bo_1"].ToString() != null && SqlDtr["Bo_1"].ToString() != "")
                                {
                                    orderInvoice.Add("BO:" + SqlDtr["Bo_1"].ToString());
                                }
                            }
                        }
                        else
                        {
                            if (SqlDtr["Bo_2"].ToString() != null && SqlDtr["Bo_2"].ToString() != "")
                            {
                                orderInvoice.Add("BO:" + SqlDtr["Bo_2"].ToString());
                            }
                        }
                    }
                    else
                    {
                        if (SqlDtr["Bo_3"].ToString() != null && SqlDtr["Bo_3"].ToString() != "")
                        {
                            orderInvoice.Add("BO:" + SqlDtr["Bo_3"].ToString());
                        }
                    }
                }
                SqlDtr.Close();
                /**********End***********************/
                #endregion
                return orderInvoice;
            }
            catch (Exception ex)
            {
                return orderInvoice;
            }

        }

        [HttpGet]
        [Route("api/Sales/GetDiscountData")]
        public List<string> GetDiscountData()
        {
            List<string> discount = new List<string>();
            try
            {
                string txtCashDisc = string.Empty;
                string txtDisc = string.Empty;
                string dropCashDiscType = string.Empty;
                string dropDiscType = string.Empty;

                string sql = "select * from SetDis";
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr = null;
                SqlDtr = obj.GetRecordSet(sql);
                if (SqlDtr.Read())
                {
                    if (SqlDtr["CashDisSalesStatus"].ToString() == "1")
                    {
                        txtCashDisc = SqlDtr["CashDisSales"].ToString();
                        if (SqlDtr["CashDisLtrSales"].ToString() == "Rs.")
                            dropCashDiscType = "0";
                        else
                            dropCashDiscType = "1";
                    }
                    else
                        txtCashDisc = "0";
                    if (SqlDtr["DiscountSalesStatus"].ToString() == "1")
                    {
                        txtDisc = SqlDtr["DiscountSales"].ToString();
                        if (SqlDtr["DisLtrSales"].ToString() == "Rs.")
                            dropDiscType = "0";
                        else
                            dropDiscType = "1";
                    }
                    else
                        txtDisc = "0";
                }
                else
                {
                    txtDisc = "0";
                    txtCashDisc = "0";
                    dropCashDiscType = "0";
                    dropDiscType = "0";
                }

                SqlDtr.Close();
                discount.Add(txtCashDisc);
                discount.Add(dropCashDiscType);
                discount.Add(txtDisc);
                discount.Add(dropDiscType);
                return discount;
            }
            catch (Exception ex)
            {
                return discount;
            }
        }

        [HttpGet]
        [Route("api/Sales/GetSalesManData")]
        public List<string> GetSalesManData()
        {

            List<string> salesMan = new List<string>();
            try
            {
                InventoryClass obj = new InventoryClass();
                string sql = string.Empty;
                SqlDataReader SqlDtr = null;
                sql = "Select Emp_Name from Employee where Designation ='Servo Sales Representative' and status=1 order by Emp_Name";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    salesMan.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();
                return salesMan;
            }
            catch (Exception ex)
            {
                return salesMan;
            }
        }

        [HttpGet]
        [Route("api/Sales/GetFromDateToDateData")]
        public List<string> GetFromDateToDateData()
        {
            List<string> result = new List<string>();
            try
            {
                SqlDataReader rdr = null;
                dbobj.SelectQuery("select * from organisation", ref rdr);
                if (rdr.Read())
                {
                    FromDate = GetYear(GenUtil.trimDate(rdr["Acc_date_from"].ToString()));
                    if (FromDate != "")
                        FromDate = System.Convert.ToString(int.Parse(FromDate));
                    ToDate = GetYear(GenUtil.trimDate(rdr["Acc_date_To"].ToString()));
                }
                else
                {
                    // MessageBox.Show("Please Fill The Organization Form First");
                    // return;
                }
                result.Add(FromDate);
                result.Add(ToDate);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        [HttpGet]
        [Route("api/Sales/GetProductTypes")]
        public string GetProductTypes()
        {
            string sql;
            string texthiddenprod = string.Empty;
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            try
            {
                sql = "select distinct Prod_Code,Prod_name,Pack_Type from Products p,price_updation pu where p.prod_id=pu.prod_id";
                SqlDtr = obj.GetRecordSet(sql);
                if (SqlDtr.HasRows)
                {
                    texthiddenprod = "Type,";
                    while (SqlDtr.Read())
                    {
                        texthiddenprod += SqlDtr.GetValue(0).ToString() + ":" + SqlDtr.GetValue(1).ToString() + ":" + SqlDtr.GetValue(2).ToString() + ",";
                        //21.04.09 coment by vikas  texthiddenprod.Value+=SqlDtr.GetValue(0).ToString()+":"+SqlDtr.GetValue(1).ToString()+",";
                    }
                }
                SqlDtr.Close();
                return texthiddenprod;
            }
            catch (Exception)
            {
                return texthiddenprod;
            }
        }


        [HttpGet]
        [Route("api/Sales/Getschemefoe")]
        public string Getschemefoe(string invoiceDate)
        {
            string temptextfoe = string.Empty;
            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                //SqlDataReader SqlDtr1;
                string sql;
                string sql1;
                string str = "";
                SqlDataReader rdr = null;

                //sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.onevery one,o.freepack free,o.schprodid sch,o.datefrom df,o.dateto dt,o.discount dis  from products p,oilscheme o where p.prod_id=o.prodid and";
                //Mahesh11.04.007 
                sql = "select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.custid cust,o.distype  from products p,foe o where p.prod_id=o.prodid  and cast(floor(cast(o.datefrom as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(invoiceDate.Trim()) + "' and cast(floor(cast(o.dateto as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate.Trim()) + "',103)";
                //sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.custid cust  from products p,foe o where p.prod_id=o.prodid  and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()) +"'";
                SqlDtr = obj.GetRecordSet(sql);
                if (SqlDtr.HasRows)
                {
                    while (SqlDtr.Read())
                    {
                        str = str + ":" + SqlDtr["cat"].ToString().Trim() + ":" + SqlDtr["pname"].ToString().Trim() + ":" + SqlDtr["ptype"].ToString().Trim() + ":" + SqlDtr["dis"].ToString() + ":" + SqlDtr["distype"].ToString();
                        sql1 = "select cust_name from customer where cust_id='" + SqlDtr["cust"] + "'";
                        dbobj.SelectQuery(sql1, ref rdr);
                        //	SqlDtr1=obj.GetRecordSet(sql1);
                        //	while(SqlDtr1.Read())
                        if (rdr.Read())
                        {
                            str = str + ":" + rdr["cust_name"].ToString().Trim() + ",";
                        }
                        rdr.Close();
                    }

                    SqlDtr.Close();
                }

                //Mahesh11.04.007 
                string sql2 = "select p.cust_name cust,o.datefrom df,o.dateto dt,o.discount dis,o.custid cust,o.distype  from customer p,foe o where p.cust_id=o.custid and o.prodid='0'  and cast(floor(cast(o.datefrom as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(invoiceDate.Trim()) + "' and cast(floor(cast(o.dateto as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate.Trim()) + "',103)";
                //string sql2="select p.cust_name cust,o.datefrom df,o.dateto dt,o.discount dis,o.custid cust  from customer p,foe o where p.cust_id=o.custid and o.prodid='0'  and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()) +"'";
                //SqlDtr=obj.GetRecordSet(sql2);
                dbobj.SelectQuery(sql2, ref rdr);
                //while(SqlDtr.Read())
                while (rdr.Read())
                {
                    //*bhal*/					str=str+":"+"0"+":"+"0"+":"+"0"+":"+SqlDtr["dis"]+":"+SqlDtr["cust"].ToString().Trim()+",";
                    str = str + ":" + "0" + ":" + "0" + ":" + "0" + ":" + rdr["dis"] + ":" + rdr["distype"] + ":" + rdr["cust"].ToString().Trim() + ",";
                    //**sql1="select cust_name from customer where cust_id='"+SqlDtr["cust"]+"'";
                    //**dbobj.SelectQuery(sql1,ref rdr); 
                    //	SqlDtr1=obj.GetRecordSet(sql1);
                    //	while(SqlDtr1.Read())
                    //**	if(rdr.Read())
                    //**	{
                    //**		str=str+":"+rdr["cust_name"]+",";
                    //**	}
                    //**	rdr.Close();
                }
                //SqlDtr.Close();
                rdr.Close();
                temptextfoe = str;
                return temptextfoe;
                //MessageBox.Show("foe " +temptextfoe.Value);
            }
            catch (Exception ex)
            {
                return temptextfoe;
            }
        }


        [HttpGet]
        [Route("api/Sales/GetschemeSecSP")]
        public string GetschemeSecSP(string invoiceDate)
        {
            string temptextSecSP = string.Empty;
            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                string sql;
                string str = "";
                //SqlDataReader rdr=null; 

                //sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.onevery one,o.freepack free,o.schprodid sch,o.datefrom df,o.dateto dt,o.discount dis  from products p,oilscheme o where p.prod_id=o.prodid and";
                //sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme  from products p,oilscheme o where p.prod_id=o.prodid and o.schname='Secondry(LTR Scheme)' or o.schname='Primary(LTR Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103)";
                //Mahesh11.04.007 

                //coment by vikas 26.10.2012 sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,o.discounttype distype  from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Secondry SP(LTRSP Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103)";
                sql = "select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,o.discounttype distype,o.group_name gname,o.unit from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Primary(LTR&% Addl Scheme)','Secondry SP(LTRSP Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(invoiceDate.Trim()) + "' and cast(floor(cast(o.dateto as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate.Trim()) + "',103)";       //add by vikas 26.10.2012

                //sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme  from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Secondry(LTR Scheme)','Primary(LTR Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()) +"'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    //coment by vikas 26.10.2012 str=str+":"+SqlDtr["cat"]+":"+SqlDtr["pname"]+":"+SqlDtr["ptype"]+":"+SqlDtr["dis"]+":"+SqlDtr["scheme"]+":"+SqlDtr["distype"]+",";

                    /*************Add by vikas 26.10.2012******************/
                    str = str + ":" + SqlDtr["cat"] + ":" + SqlDtr["pname"] + ":" + SqlDtr["ptype"] + ":" + SqlDtr["dis"] + ":" + SqlDtr["scheme"] + ":" + SqlDtr["distype"];

                    if (SqlDtr["gname"].ToString().Trim() != null && SqlDtr["gname"].ToString().Trim() != "")
                        str = str + ":" + SqlDtr["gname"] + ",";
                    else
                        str = str + ":" + 0 + ",";
                    /**********End*********************/

                }
                SqlDtr.Close();
                temptextSecSP = str;
                return temptextSecSP;
            }
            catch (Exception ex)
            {
                return temptextSecSP;
            }
        }


        [HttpGet]
        [Route("api/Sales/Getscheme1")]
        public string Getscheme1(string invoiceDate)
        {
            string strText12 = string.Empty;
            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                string sql;
                string str = "";
                //SqlDataReader rdr=null; 
                int i = 0;
                //sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.onevery one,o.freepack free,o.schprodid sch,o.datefrom df,o.dateto dt,o.discount dis  from products p,oilscheme o where p.prod_id=o.prodid and";
                //sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme  from products p,oilscheme o where p.prod_id=o.prodid and o.schname='Secondry(LTR Scheme)' or o.schname='Primary(LTR Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103)";
                //Mahesh11.04.007 

                //coment by vikas 25.10.2012 sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,o.discounttype distype  from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Secondry(LTR Scheme)','Primary(LTR&% Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103)";

                sql = "select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,o.discounttype distype,o.group_name gname,o.unit unit from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Secondry(LTR Scheme)','Primary(LTR&% Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(invoiceDate.Trim()) + "' and cast(floor(cast(o.dateto as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate.Trim()) + "',103)";    //Add by vikas 25.10.2012

                //sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme  from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Secondry(LTR Scheme)','Primary(LTR Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()) +"'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    //Coment by vikas 25.10.2012 str=str+":"+SqlDtr["cat"]+":"+SqlDtr["pname"]+":"+SqlDtr["ptype"]+":"+SqlDtr["dis"]+":"+SqlDtr["scheme"]+":"+SqlDtr["distype"]+",";
                    i++;

                    str = str + ":" + SqlDtr["cat"] + ":" + SqlDtr["pname"] + ":" + SqlDtr["ptype"] + ":" + SqlDtr["dis"] + ":" + SqlDtr["scheme"] + ":" + SqlDtr["distype"];     //add by vikas 25.10.2012

                    /*********Add by vikas 25.10.2012************
					if(SqlDtr["gname"]!= null && SqlDtr["unit"]!= null)
					{
						str=str+":"+SqlDtr["gname"]+":"+SqlDtr["unit"]+",";
					}
					else
					{
						str=str+":"+0+":"+0+",";
					}
					**********End***********/
                    /*********add by vikas 25.10.2012************/
                    if (SqlDtr["gname"].ToString().Trim() != null && SqlDtr["gname"].ToString().Trim() != "")
                        str = str + ":" + SqlDtr["gname"];       // 
                    else
                        str = str + ":" + 0;

                    if (SqlDtr["unit"].ToString().Trim() != null && SqlDtr["unit"].ToString().Trim() != "")
                        str = str + ":" + SqlDtr["unit"] + ",";
                    else
                        str = str + ":" + 0 + ",";
                    /*****************************/

                }
                int j = i;
                SqlDtr.Close();
                strText12 = str;
                return strText12;
            }
            catch (Exception ex)
            {
                return strText12;
            }
        }

        [HttpGet]
        [Route("api/Sales/GetFOECust")]
        public string GetFOECust()
        {
            string strText13 = string.Empty;
            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                string str = "";
                string sql = "select cust_Name from customer  where cust_type like'Fleet%' or cust_type like('Oe%')  order by cust_Name";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    str = str + "," + SqlDtr.GetValue(0).ToString().Trim();
                }
                SqlDtr.Close();
                strText13 = str;
                return strText13;
            }
            catch (Exception ex)
            {
                return strText13;
            }
        }


        [HttpGet]
        [Route("api/Sales/Getscheme")]
        public string Getscheme(string invoiceDate)
        {
            string str = "";
            int i = 0;
            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                string sql;

                SqlDataReader rdr = null;

                sql = "select p.category cat,p.prod_name pname,p.pack_type ptype,o.onevery one,o.freepack freep,o.schprodid sch,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,Group_Name GName,o.Unit Unit,o.Pack_Type Packtype,sch_id from products p,oilscheme o where p.prod_id=o.prodid and cast(floor(cast(o.datefrom as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(invoiceDate.Trim()) + "' and cast(floor(cast(o.dateto as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(invoiceDate.Trim()) + "',103) and schname in ('Primary(Free Scheme)','Secondry(Free Scheme)') order by prod_name desc";

                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    str = str + ":" + SqlDtr["cat"] + ":" + SqlDtr["pname"] + ":" + SqlDtr["ptype"];

                    string sql1 = "select p.category cat1,p.prod_name pname1,p.pack_type ptype1,o.onevery one,o.freepack freep,o.datefrom df,o.dateto dt,p.unit unit from products p,oilscheme o where p.prod_id='" + SqlDtr["sch"] + "'";

                    dbobj.SelectQuery(sql1, ref rdr);

                    string unit = "";
                    if (rdr.Read())
                    {
                        str = str + ":" + rdr["cat1"] + ":" + rdr["pname1"] + ":" + rdr["ptype1"] + ":" + SqlDtr["one"] + ":" + SqlDtr["freep"] + ":" + GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["df"].ToString())) + ":" + GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["dt"].ToString()));
                        unit = rdr["unit"].ToString();
                    }
                    else
                    {
                        str = str + ":" + 0 + ":" + 0 + ":" + 0 + ":" + SqlDtr["one"] + ":" + SqlDtr["freep"] + ":" + GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["df"].ToString())) + ":" + GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["dt"].ToString()));
                        unit = "";
                    }
                    rdr.Close();

                    #region Fetch Closing Stock
                    string sql2 = "select top 1 Closing_Stock from Stock_Master where productid=" + SqlDtr["sch"] + " order by stock_date desc";
                    dbobj.SelectQuery(sql2, ref rdr);
                    if (rdr.Read())
                        str = str + ":" + rdr["Closing_Stock"] + ":" + unit + ":" + SqlDtr["dis"];
                    else
                        str = str + ":0" + ":" + unit + ":" + SqlDtr["dis"];
                    rdr.Close();

                    str = str + ":" + SqlDtr["scheme"];
                    #endregion

                    /*********add by vikas 25.10.2012************/
                    if (SqlDtr["GName"].ToString().Trim() != null && SqlDtr["GName"].ToString().Trim() != "")
                        str = str + ":" + SqlDtr["GName"];       // 
                    else
                        str = str + ":" + 0;

                    if (SqlDtr["unit"].ToString().Trim() != null && SqlDtr["unit"].ToString().Trim() != "")
                        str = str + ":" + SqlDtr["unit"];
                    else
                        str = str + ":" + 0;
                    /*****************************/

                    if (SqlDtr["Packtype"].ToString().Trim() != null && SqlDtr["Packtype"].ToString().Trim() != "")
                        //25.72013 str=str+":"+SqlDtr["Packtype"]+",";       
                        str = str + ":" + SqlDtr["Packtype"];
                    else
                        //25.72013 str=str+":"+0+",";
                        str = str + ":" + 0;

                    /******************Add by vikas 25.7.2013************/
                    if (SqlDtr["sch_id"].ToString().Trim() != null && SqlDtr["sch_id"].ToString().Trim() != "")
                        //25.72013 str=str+":"+SqlDtr["Packtype"]+",";       
                        str = str + ":" + SqlDtr["sch_id"] + ",";
                    else
                        //25.72013 str=str+":"+0+",";
                        str = str + ":" + 0 + ",";
                    /******************************/
                    i++;
                }
                SqlDtr.Close();
                return str;
            }
            catch (Exception ex)
            {
                return str;
                //CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : getscheme() EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        /// <summary>
		/// This method is used to update the product stock after sales in edit time.
		/// </summary>
        [HttpPost]
        [Route("api/Sales/SeqStockMaster")]
        public bool SeqStockMaster(SalesModels sales)
        {
            try
            {


                InventoryClass obj = new InventoryClass();
                InventoryClass obj1 = new InventoryClass();
                SqlCommand cmd;
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                SqlDataReader rdr1 = null, rdr = null;
                for (int i = 0; i < sales.ProductType.Count; i++)
                {
                    if (sales.ProductType[i] == "" || sales.ProductName[i] == "" || sales.ProductQty[i] == "")
                        continue;
                    else
                    {
                        //					InventoryClass obj = new InventoryClass();
                        //					InventoryClass obj1 = new InventoryClass();
                        //					SqlCommand cmd;
                        //					SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                        //					SqlDataReader rdr1=null,rdr=null;
                        string str = "select Prod_ID from Products where Category='" + sales.ProductType[i].ToString() + "' and Prod_Name='" + sales.ProductName[i].ToString() + "' and Pack_Type='" + sales.ProductPack[i].ToString() + "'";
                        rdr = obj.GetRecordSet(str);
                        if (rdr.Read())
                        //for(int i=1001;i<=1070;i++)//add
                        {
                            string str1 = "select * from Stock_Master where Productid='" + rdr["Prod_ID"].ToString() + "' order by Stock_date";
                            //string str1="select * from Stock_Master where Productid='"+i.ToString()+"' order by Stock_date";//add
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
                                //CS=OS+double.Parse(rdr1["receipt"].ToString())-double.Parse(rdr1["sales"].ToString());
                                CS = OS + double.Parse(rdr1["receipt"].ToString()) - (double.Parse(rdr1["sales"].ToString()) + double.Parse(rdr1["salesfoc"].ToString()));
                                Con.Open();
                                cmd = new SqlCommand("update Stock_Master set opening_stock='" + OS.ToString() + "', Closing_Stock='" + CS.ToString() + "' where ProductID='" + rdr1["Productid"].ToString() + "' and Stock_Date='" + GenUtil.str2MMDDYYYY(rdr1["stock_date"].ToString()) + "'", Con);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                Con.Close();
                            }
                            rdr1.Close();
                        }
                        rdr.Close();
                    }
                    //*******************
                    if (sales.SchProductName.Count > 0)
                    {
                        if (sales.SchProductType[i] == "" || sales.SchProductName[i] == "" || sales.SchProductQty[i] == "")
                            continue;
                        else
                        {

                            string str = "select Prod_ID from Products where Category='" + sales.SchProductType[i].ToString() + "' and Prod_Name='" + sales.SchProductName[i].ToString() + "' and Pack_Type='" + sales.SchProductPack[i].ToString() + "'";
                            rdr = obj.GetRecordSet(str);
                            if (rdr.Read())
                            //for(int i=1001;i<=1070;i++)//add
                            {
                                string str1 = "select * from Stock_Master where Productid='" + rdr["Prod_ID"].ToString() + "' order by Stock_date";
                                //string str1="select * from Stock_Master where Productid='"+i.ToString()+"' order by Stock_date";//add
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
                                    //CS=OS+double.Parse(rdr1["receipt"].ToString())-double.Parse(rdr1["sales"].ToString());
                                    CS = OS + double.Parse(rdr1["receipt"].ToString()) - (double.Parse(rdr1["sales"].ToString()) + double.Parse(rdr1["salesfoc"].ToString()));
                                    Con.Open();
                                    cmd = new SqlCommand("update Stock_Master set opening_stock='" + OS.ToString() + "', Closing_Stock='" + CS.ToString() + "' where ProductID='" + rdr1["Productid"].ToString() + "' and Stock_Date='" + GenUtil.str2MMDDYYYY(rdr1["stock_date"].ToString()) + "'", Con);
                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    Con.Close();
                                }
                                rdr1.Close();
                            }
                            rdr.Close();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        [HttpGet]
        [Route("api/Sales/GetFetchData")]
        public string GetFetchData()
        {
            string strTxtVen = string.Empty;

            InventoryClass obj = new InventoryClass();
            InventoryClass obj1 = new InventoryClass();
            SqlDataReader rdr1 = null;
            SqlDataReader rdr3 = null;
            string str1 = "";
            DateTime duedate;
            string duedatestr = "";

            //coment by vikas 25.10.2012 rdr3 = obj.GetRecordSet("select c.City,CR_Days,Curr_Credit,Cust_ID,SSR,Cust_Name,Emp_Name  from Customer c,Employee e where e.Emp_ID=c.SSR order by Cust_Name");

            //SalesCustomerModel salesCustomer = new SalesCustomerModel();

            rdr3 = obj.GetRecordSet("select c.City,CR_Days,Curr_Credit,Cust_ID,SSR,Cust_Name,Emp_Name,ct.group_name  from Customer c,Employee e,customertype ct where e.Emp_ID=c.SSR and c.cust_type=ct.customertypename order by Cust_Name");
            while (rdr3.Read())
            {
                //salesCustomer.CR_Days.Add(DateTime.Now.AddDays(System.Convert.ToDouble(rdr3["CR_Days"])).ToString());
                duedate = DateTime.Now.AddDays(System.Convert.ToDouble(rdr3["CR_Days"]));
                duedatestr = duedate.ToShortDateString();
                str1 = str1 + rdr3["Cust_Name"].ToString() + "~" + rdr3["City"].ToString().Trim() + "~" + GenUtil.str2DDMMYYYY(duedatestr.Trim()) + "~" + GenUtil.strNumericFormat(rdr3["Curr_Credit"].ToString().Trim()) + "~";
                rdr1 = obj1.GetRecordSet("select top 1 Balance,BalanceType from customerledgertable where CustID=" + rdr3["Cust_ID"] + " order by EntryDate Desc");
                //dbobj.SelectQuery("select top 1 Balance,BalanceType from customerledgertable where CustID="+rdr3["Cust_ID"]+" order by EntryDate Desc",ref rdr1);
                if (rdr1.Read())
                {
                    string str15 = GenUtil.strNumericFormat(rdr1["Balance"].ToString().Trim()) + "~" + rdr1["BalanceType"].ToString().Trim() + "~";
                    str1 = str1 + GenUtil.strNumericFormat(rdr1["Balance"].ToString().Trim()) + "~" + rdr1["BalanceType"].ToString().Trim() + "~";
                }
                else
                {
                    str1 = str1 + "0" + "~" + " " + "~";
                }
                rdr1.Close();
                //coment by vikas 25.10.2012 str1+=rdr3["Emp_Name"].ToString()+"#";
                str1 += rdr3["Emp_Name"].ToString() + "~" + rdr3["group_name"].ToString() + "#";
            }
            rdr3.Close();
            strTxtVen = str1;
            return strTxtVen;
        }

        [HttpGet]
        [Route("api/Sales/GetDataSelectedOrderInvoice")]
        public SalesModels GetDataSelectedOrderInvoice(string id)
        {
            SalesModels sales = new SalesModels();

            string strTextSelect = id;

            /*********Add by vikas 11.12.12********************************/
            string[] Order_No = strTextSelect.Split(new char[] { ':' });
            int Count = Order_No.Length;
            /***************************End********************************/

            if (strTextSelect != "Select")
            {
                //Clear();
                //HtmlInputText[] ProdType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12 };
                //TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12 };
                //TextBox[] Rate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12 };
                //TextBox[] Amount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12 };
                //TextBox[] AvStock = { txtAvStock1, txtAvStock2, txtAvStock3, txtAvStock4, txtAvStock5, txtAvStock6, txtAvStock7, txtAvStock8, txtAvStock9, txtAvStock10, txtAvStock11, txtAvStock12 };
                //TextBox[] tempQty = { txtTempQty1, txtTempQty2, txtTempQty3, txtTempQty4, txtTempQty5, txtTempQty6, txtTempQty7, txtTempQty8, txtTempQty9, txtTempQty10, txtTempQty11, txtTempQty12 };
                //TextBox[] tempSchQty = { txtTempSchQty1, txtTempSchQty2, txtTempSchQty3, txtTempSchQty4, txtTempSchQty5, txtTempSchQty6, txtTempSchQty7, txtTempSchQty8, txtTempSchQty9, txtTempSchQty10, txtTempSchQty11, txtTempSchQty12 };
                //HtmlInputHidden[] tmpQty = { tmpQty1, tmpQty2, tmpQty3, tmpQty4, tmpQty5, tmpQty6, tmpQty7, tmpQty8, tmpQty9, tmpQty10, tmpQty11, tmpQty12 };
                //HtmlInputHidden[] tmpSchType = { tmpSchType1, tmpSchType2, tmpSchType3, tmpSchType4, tmpSchType5, tmpSchType6, tmpSchType7, tmpSchType8, tmpSchType9, tmpSchType10, tmpSchType11, tmpSchType12 };
                //TextBox[] pid = { txtpname1, txtpname2, txtpname3, txtpname4, txtpname5, txtpname6, txtpname7, txtpname8, txtpname9, txtpname10, txtpname11, txtpname12 };
                //TextBox[] pid1 = { txtmwid1, txtmwid2, txtmwid3, txtmwid4, txtmwid5, txtmwid6, txtmwid7, txtmwid8, txtmwid9, txtmwid10, txtmwid11, txtmwid12 };
                //TextBox[] scheme = { txtsch1, txtsch2, txtsch3, txtsch4, txtsch5, txtsch6, txtsch7, txtsch8, txtsch9, txtsch10, txtsch11, txtsch12 };
                //TextBox[] foe = { txtfoe1, txtfoe2, txtfoe3, txtfoe4, txtfoe5, txtfoe6, txtfoe7, txtfoe8, txtfoe9, txtfoe10, txtfoe11, txtfoe12 };
                //TextBox[] ProdType1 = { txtTypesch1, txtTypesch2, txtTypesch3, txtTypesch4, txtTypesch5, txtTypesch6, txtTypesch7, txtTypesch8, txtTypesch9, txtTypesch10, txtTypesch11, txtTypesch12 };
                //TextBox[] Qty1 = { txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12 };
                //TextBox[] stk1 = { txtstk1, txtstk2, txtstk3, txtstk4, txtstk5, txtstk6, txtstk7, txtstk8, txtstk9, txtstk10, txtstk11, txtstk12 };
                //HtmlInputHidden[] tmpFoeType = { tmpFoeType1, tmpFoeType2, tmpFoeType3, tmpFoeType4, tmpFoeType5, tmpFoeType6, tmpFoeType7, tmpFoeType8, tmpFoeType9, tmpFoeType10, tmpFoeType11, tmpFoeType12 };
                //HtmlInputHidden[] SchSPType = { tmpSecSPType1, tmpSecSPType2, tmpSecSPType3, tmpSecSPType4, tmpSecSPType5, tmpSecSPType6, tmpSecSPType7, tmpSecSPType8, tmpSecSPType9, tmpSecSPType10, tmpSecSPType11, tmpSecSPType12 };
                //HtmlInputHidden[] SchSP = { txtTempSecSP1, txtTempSecSP2, txtTempSecSP3, txtTempSecSP4, txtTempSecSP5, txtTempSecSP6, txtTempSecSP7, txtTempSecSP8, txtTempSecSP9, txtTempSecSP10, txtTempSecSP11, txtTempSecSP12 };


                InventoryClass obj = new InventoryClass();
                InventoryClass obj1 = new InventoryClass();
                SqlDataReader SqlDtr;
                string sql, sql1;
                SqlDataReader rdr = null, rdr1 = null, rdr2 = null, rdr3 = null;
                int i = 0;


                #region Get Data from Order_Col_Master Table regarding Order No.
                //coment by vikas 11.12.12 sql="select * from Order_Col_Master,Employee where Under_SalesMan=Emp_ID and Order_No='"+DropOrderInvoice.SelectedItem.Value +"'" ;

                /***********Add by vikas 11.12.12***************************/
                if (Count == 1)
                    sql = "select * from Order_Col_Master,Employee where Under_SalesMan=Emp_ID and Order_No='" + id + "'";
                else
                    sql = "select * from Order_Col_Master,Employee where Under_SalesMan=Emp_ID and Order_No=(select distinct order_id from ovd o where bo_1=" + Order_No[1].ToString() + " or bo_2=" + Order_No[1].ToString() + " or bo_3=" + Order_No[1].ToString() + ")";
                /************End**************************/

                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    sales.Invoice_Date = SqlDtr.GetValue(1).ToString();
                    string strDate = SqlDtr.GetValue(1).ToString().Trim();
                    int pos = strDate.IndexOf(" ");

                    if (pos != -1)
                    {
                        strDate = strDate.Substring(0, pos);
                    }
                    else
                    {
                        strDate = "";
                    }

                    sales.Invoice_Date = GenUtil.str2DDMMYYYY(strDate);
                    //tempInvoiceDate.Value = GenUtil.str2DDMMYYYY(strDate);
                    sales.Sales_Type = (SqlDtr.GetValue(2).ToString());

                    sales.Under_SalesMan = SqlDtr["Emp_Name"].ToString();
                    sales.Vehicle_No = SqlDtr.GetValue(5).ToString();

                    sales.Grand_Total = GenUtil.strNumericFormat(SqlDtr.GetValue(6).ToString());
                    sales.Discount = float.Parse(SqlDtr.GetValue(7).ToString());
                    string strDisc = GenUtil.strNumericFormat(SqlDtr.GetValue(7).ToString());
                    sales.Discount = float.Parse(strDisc);
                    sales.Discount_Type = (SqlDtr.GetValue(8).ToString());
                    string strNetAmount = GenUtil.strNumericFormat(SqlDtr.GetValue(9).ToString());
                    sales.Net_Amount = float.Parse(strNetAmount);
                    //tempNetAmount.Value = SqlDtr.GetValue(9).ToString();                               //Add by vikas 14.07.09
                    //tempNetAmount.Value = GenUtil.strNumericFormat(tempNetAmount.Value.ToString());     //Add by vikas 14.07.09

                    //NetAmount = GenUtil.strNumericFormat(txtNetAmount.Text.ToString());

                    sales.Promo_Scheme = SqlDtr.GetValue(10).ToString();
                    sales.Remark = SqlDtr.GetValue(11).ToString();
                    sales.Entry_By = SqlDtr.GetValue(12).ToString();
                    sales.Entry_Time = System.Convert.ToDateTime(SqlDtr.GetValue(13).ToString());
                    sales.SecSPDisc = float.Parse(SqlDtr["SecSPDisc"].ToString());

                    sales.Promo_Scheme = SqlDtr.GetValue(10).ToString();
                    sales.Remark = SqlDtr.GetValue(11).ToString();
                    sales.Entry_By = SqlDtr.GetValue(12).ToString();

                    sales.Entry_Time = System.Convert.ToDateTime(SqlDtr.GetValue(13).ToString());
                    sales.SecSPDisc = float.Parse(SqlDtr["SecSPDisc"].ToString());

                    if (SqlDtr["Discount_type"].ToString() == "Per")
                    {
                        string strTotalDisc = System.Convert.ToString((double.Parse(SqlDtr["Grand_Total"].ToString()) - double.Parse(SqlDtr["schdiscount"].ToString())) * double.Parse(SqlDtr["discount"].ToString()) / 100);
                        sales.Total_Discount = float.Parse(System.Convert.ToString(Math.Round(double.Parse(strTotalDisc), 2)));
                    }
                    else
                    {
                        double Discount = double.Parse(GenUtil.strNumericFormat(SqlDtr["Discount"].ToString())) * double.Parse(GenUtil.strNumericFormat(SqlDtr["totalqtyltr"].ToString()));
                        sales.Total_Discount = float.Parse(GenUtil.strNumericFormat(Discount.ToString()));
                    }


                    if (SqlDtr["cash_Disc_type"].ToString() == "Per")
                    {
                        double tot = 0;
                        if (Convert.ToString(sales.Total_Discount) != "")
                            tot = double.Parse(SqlDtr["Grand_Total"].ToString()) - (double.Parse(SqlDtr["schdiscount"].ToString()) + double.Parse(SqlDtr["foediscount"].ToString()) + double.Parse(sales.Total_Discount.ToString()));
                        else
                            tot = double.Parse(SqlDtr["Grand_Total"].ToString()) - (double.Parse(SqlDtr["schdiscount"].ToString()) + double.Parse(SqlDtr["foediscount"].ToString()));
                        string strCashDiscount = System.Convert.ToString(tot * double.Parse(SqlDtr["Cash_Discount"].ToString()) / 100);
                        sales.Total_Discount = float.Parse(System.Convert.ToString(Math.Round(double.Parse(strCashDiscount), 2)));
                    }
                    else
                    {
                        sales.Total_Discount = 0;
                    }


                    string strCashDisc = SqlDtr.GetValue(15).ToString();
                    sales.Cash_Discount = float.Parse(GenUtil.strNumericFormat(strCashDisc));

                    sales.Cash_Disc_Type = (SqlDtr.GetValue(16).ToString());
                    sales.IGST_Amount = float.Parse(SqlDtr.GetValue(17).ToString());
                    sales.Scheme_Discount = float.Parse(SqlDtr.GetValue(18).ToString());
                    sales.FOE_Discount = float.Parse(SqlDtr.GetValue(19).ToString());
                    sales.FOE_Discounttype = (SqlDtr.GetValue(20).ToString());
                    sales.FOE_Discountrs = float.Parse(SqlDtr.GetValue(21).ToString());
                    sales.Total_Qty_Ltr = float.Parse(SqlDtr.GetValue(22).ToString());
                    //sales.CGST_Amount = float.Parse(SqlDtr.GetValue(27).ToString());
                    //sales.SGST_Amount = float.Parse(SqlDtr.GetValue(26).ToString());



                    if (SqlDtr["ChallanNo"].ToString() == "0")
                        sales.ChallanNo = "";
                    else
                        sales.ChallanNo = SqlDtr["ChallanNo"].ToString();


                    if (GenUtil.trimDate(SqlDtr["ChallanDate"].ToString()) == "1/1/1900")
                        sales.ChallanDate = System.Convert.ToDateTime("1/1/1900");
                    else
                        sales.ChallanDate = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["ChallanDate"].ToString())));
                }
                SqlDtr.Close();
                #endregion

                #region Get Customer name and place regarding Customer ID

                //coment by vikas 10.11.2012 sql="select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID from Customer as c, Order_Col_master as s where c.Cust_ID= s.Cust_ID and s.Order_No='"+DropOrderInvoice.SelectedValue +"'";

                //coment by vikas 11.12.2012 sql="select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID,ct.group_name from Customer as c, Order_Col_Master as s,customertype as ct where c.Cust_ID= s.Cust_ID and c.cust_type=ct.customertypename and s.Order_No='"+DropOrderInvoice.SelectedValue +"'";
                /***********Add by vikas 11.12.12***************************/
                if (Count == 1)
                    sql = "select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID,ct.group_name from Customer as c, Order_Col_Master as s,customertype as ct where c.Cust_ID= s.Cust_ID and c.cust_type=ct.customertypename and s.Order_No='" + id + "'";
                else
                    sql = "select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID,ct.group_name from Customer as c, Order_Col_Master as s,customertype as ct where c.Cust_ID= s.Cust_ID and c.cust_type=ct.customertypename and s.Order_No =(select distinct order_id from ovd o where bo_1=" + Order_No[1].ToString() + " or bo_2=" + Order_No[1].ToString() + " or bo_3=" + Order_No[1].ToString() + ")";
                /************End**************************/
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    //coment by vikas 12.11.2012 texthidden1.Value=SqlDtr.GetValue(0).ToString();
                    //coment by vikas 12.11.2012 text1.Value=SqlDtr.GetValue(0).ToString();                        
                    sales.Cust_Name = SqlDtr.GetValue(0).ToString() + ":" + SqlDtr.GetValue(1).ToString();
                    sales.Cust_ID = Int32.Parse(SqlDtr["Cust_ID"].ToString());

                    sales.Place = SqlDtr.GetValue(1).ToString();//System.Convert.ToDateTime(
                    sales.DueDate = DateTime.Now.AddDays(System.Convert.ToDouble(SqlDtr.GetValue(2).ToString()));
                    string duedatestr = (sales.DueDate.ToShortDateString());
                    sales.DueDate = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(duedatestr));
                    sales.Current_Balance = GenUtil.strNumericFormat(SqlDtr.GetValue(3).ToString());
                    //TxtCrLimit.Value = SqlDtr.GetValue(4).ToString();
                    sales.Credit_Limit = float.Parse(SqlDtr.GetValue(4).ToString());
                    //txtcusttype.Text = SqlDtr.GetValue(5).ToString();

                    if (SqlDtr["Group_Name"].ToString() != null && SqlDtr["Group_Name"].ToString() != "")
                        sales.Group_Name = SqlDtr["Group_Name"].ToString();
                }
                SqlDtr.Close();

                //coment by vikas 11.12.2012 sql="select top 1 balance,balancetype  from CustomerLedgerTable as c, Order_Col_master as s where c.CustID= s.Cust_ID and s.Order_No='"+DropOrderInvoice.SelectedValue+"' order by entrydate desc";

                /***********Add by vikas 11.12.12***************************/
                if (Count == 1)
                    sql = "select top 1 balance,balancetype  from CustomerLedgerTable as c, Order_Col_master as s where c.CustID= s.Cust_ID and s.Order_No='" + id + "' order by entrydate desc";
                else
                    sql = "select top 1 balance,balancetype  from CustomerLedgerTable as c, Order_Col_master as s where c.CustID= s.Cust_ID and s.Order_No=(select distinct order_id from ovd o where bo_1=" + Order_No[1].ToString() + " or bo_2=" + Order_No[1].ToString() + " or bo_3=" + Order_No[1].ToString() + ") order by entrydate desc";
                /*****************************End**************************/
                SqlDtr = obj.GetRecordSet(sql);

                while (SqlDtr.Read())
                {
                    sales.Balance = SqlDtr.GetValue(0).ToString();
                    sales.BalanceType = SqlDtr.GetValue(1).ToString();
                    //sales.Current_Balance = GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString()) + " " + SqlDtr.GetValue(1).ToString();
                }
                SqlDtr.Close();
                #endregion

                #region Get Data from Order Details Table regarding Order No.

                /*Coment by vikas 10.11.2012 sql="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.Order_no,sm.Order_date,SchType,FoeType,SPDiscType,SPDisc,cust_id"+
                    " from Products p, Order_Col_Details sd,Order_Col_master sm"+
                    " where p.Prod_ID=sd.prod_id and sd.Order_no=sm.Order_no and sd.Rate >0 and sd.Amount > 0 and sd.Order_no='"+DropOrderInvoice.SelectedItem.Value +"' order by sd.sno" ;*/

                double Avail_Stock = 0, Order_Qty = 0;  //Add by Vikas 12.11.2012

                //coment by vikas 11.12.2012 sql="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.Order_No,sm.Order_Date,SchType,FoeType,SPDiscType,SPDisc,cust_id,p.Prod_Code"+
                //coment by vikas 11.12.2012	" from Products p, Order_Col_Details sd,Order_Col_Master sm"+
                //coment by vikas 11.12.2012	" where p.Prod_ID=sd.prod_id and sd.Order_No=sm.Order_No and sd.Rate >0 and sd.Amount > 0 and sd.Order_No='"+DropOrderInvoice.SelectedItem.Value +"' order by sd.sno" ;

                /***********Add by vikas 11.12.12***************************/
                if (Count == 1)
                {
                    sql = "select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.Order_No,sm.Order_Date,SchType,FoeType,SPDiscType,SPDisc,cust_id,p.Prod_Code" +
                        " from Products p, Order_Col_Details sd,Order_Col_Master sm" +
                        " where p.Prod_ID=sd.prod_id and sd.Order_No=sm.Order_No and sd.Rate >0 and sd.Amount > 0 and sd.Order_No='" + id + "' order by sd.sno";
                }
                else
                {
                    sql = "select p.Category,p.Prod_Name,p.Pack_Type,(cast(ovd.item_qty as int)-cast(ovd.sale_qty as int)) qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.Order_No,sm.Order_Date,SchType,FoeType,SPDiscType,SPDisc,ovd.cust_id,p.Prod_Code" +
                        " from Products p, Order_Col_Details sd,Order_Col_Master sm, ovd" +
                        " where ovd.item_id=sd.prod_id and ovd.order_id=sd.order_no and ovd.item_qty>ovd.sale_qty and p.Prod_ID=sd.prod_id and sd.Order_No=sm.Order_No and sd.Rate >0 and sd.Amount > 0 and sd.Order_No =(select distinct order_id from ovd o where bo_1=" + Order_No[1].ToString() + " or bo_2=" + Order_No[1].ToString() + " or bo_3=" + Order_No[1].ToString() + ") order by sd.sno";
                }
                /*****************************End**************************/

                //select p.Category,p.Prod_Name,p.Pack_Type,sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.Order_No,sm.Order_Date,SchType,FoeType,SPDiscType,SPDisc,ovd.cust_id,p.Prod_Code
                //from Products p, Order_Col_Details sd,Order_Col_Master sm, ovd
                //ovd where ovd.item_id=sd.prod_id and ovd.order_id=sd.order_no and ovd.item_qty=ovd.sale_qty and p.Prod_ID=sd.prod_id and sd.Order_No=sm.Order_No and sd.Rate >0 and sd.Amount > 0 and sd.Order_No =(select distinct order_id from ovd o where bo_1=9 or bo_2=9 or bo_3=9) order by sd.sno
                List<string> controlSalesQty = new List<string>();
                List<string> controlProdType = new List<string>();

                List<string> controlProductType = new List<string>();
                List<string> controlProductName = new List<string>();
                List<string> controlProductPack = new List<string>();
                List<string> controlProductQty = new List<string>();
                List<string> controlPID = new List<string>();
                List<string> controlPID1 = new List<string>();
                List<string> controlscheme = new List<string>();
                List<string> controlDetails_foe = new List<string>();
                List<string> controlAv_Stock = new List<string>();
                List<string> controlSchSPType = new List<string>();
                List<string> controlSchSP = new List<string>();
                List<string> controlTmpFoeType = new List<string>();
                List<string> controlSalesQty1 = new List<string>();
                List<string> controlProdType1 = new List<string>();
                List<string> controlTempSchQty = new List<string>();
                List<string> controlTmpSchType = new List<string>();
                List<string> controlStk1 = new List<string>();
                List<string> controlRate = new List<string>();
                List<string> controlAmount = new List<string>();
                List<string> controlSchProductType = new List<string>();
                List<string> controlSchProductName = new List<string>();
                List<string> controlSchProductPack = new List<string>();
                List<string> controlSchProductQty = new List<string>();

                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    /*****************this Condition Add by Vikas 12.11.2012*becouse Condition shift Above*********************************/
                    sql1 = "select top 1 Closing_Stock from Stock_Master where productid=" + SqlDtr.GetValue(6).ToString() + " order by stock_date desc";
                    dbobj.SelectQuery(sql1, ref rdr);
                    if (rdr.Read())
                    {
                        //Coment by Vikas 12.11.2012 AvStock [i].Text =rdr["Closing_Stock"]+" "+SqlDtr.GetValue(7).ToString();
                        Avail_Stock = Double.Parse(rdr["Closing_Stock"].ToString());
                    }
                    else
                    {
                        //Coment by Vikas 12.11.2012 AvStock [i].Text ="0"+" "+SqlDtr.GetValue(7).ToString();
                        Avail_Stock = 0;
                    }

                    Order_Qty = double.Parse(SqlDtr.GetValue(3).ToString());

                    if (Avail_Stock != 0)
                    {
                        controlAv_Stock.Add(Avail_Stock + " " + SqlDtr.GetValue(7).ToString());

                        controlProdType.Add(SqlDtr["Prod_Code"].ToString() + ":" + SqlDtr.GetValue(1).ToString() + ":" + SqlDtr.GetValue(2).ToString());

                        //Coment by Vikas 10.11.2012 Qty[i].Text=SqlDtr.GetValue(3).ToString();
                        if (Avail_Stock >= Order_Qty)
                        {
                            controlSalesQty.Add(SqlDtr.GetValue(3).ToString());
                        }
                        else
                        {
                            controlSalesQty.Add(Avail_Stock.ToString());
                        }

                        controlProductType.Add(SqlDtr.GetValue(0).ToString());

                        controlProductName.Add(SqlDtr.GetValue(1).ToString());
                        controlProductPack.Add(SqlDtr.GetValue(2).ToString());
                        controlProductQty.Add(SqlDtr.GetValue(3).ToString());
                        //controlTempQty.Add(SqlDtr.GetValue(3).ToString());
                        //controlTmpQty.Add(SqlDtr.GetValue(3).ToString());

                        controlRate.Add(SqlDtr.GetValue(4).ToString());
                        controlAmount.Add(SqlDtr.GetValue(5).ToString());
                        controlPID.Add(SqlDtr.GetValue(6).ToString());
                        controlPID1.Add(SqlDtr.GetValue(6).ToString());
                        controlscheme.Add(SqlDtr.GetValue(8).ToString());
                        controlDetails_foe.Add(SqlDtr.GetValue(9).ToString());

                        if (SqlDtr["SPDiscType"].ToString() == "")
                        {
                            //rdr3 = obj1.GetRecordSet("select o.DiscountType,o.Discount from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and o.schname='Secondry SP(LTRSP Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+SqlDtr["Prod_ID"].ToString()+"'");
                            rdr3 = obj1.GetRecordSet("select o.DiscountType,o.Discount from order_col_details sd,oilscheme o,order_col_master sm where o.prodid=sd.prod_id and sm.order_no=sd.order_no and sd.order_no='" + SqlDtr["Order_No"].ToString() + "' and o.schname='Secondry SP(LTRSP Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.trimDate(SqlDtr["order_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.trimDate(SqlDtr["order_Date"].ToString()) + "' and sd.prod_id='" + SqlDtr["Prod_ID"].ToString() + "'");
                            if (rdr3.HasRows)
                            {
                                if (rdr3.Read())
                                {
                                    controlSchSPType.Add(rdr3.GetValue(0).ToString());
                                    controlSchSP.Add(rdr3.GetValue(1).ToString());
                                }
                            }
                            rdr3.Close();
                        }
                        else
                        {
                            controlSchSPType.Add(SqlDtr["SPDiscType"].ToString());
                            controlSchSP.Add(SqlDtr["SPDisc"].ToString());
                        }

                        if (SqlDtr["FoeType"].ToString() == "")
                        {
                            //rdr3 = obj1.GetRecordSet("select distinct o.distype from sales_details sd,foe o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and custid=cust_id and custid='"+SqlDtr["Cust_ID"].ToString()+"' and sd.prod_id='"+SqlDtr["Prod_ID"].ToString()+"' and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"'");
                            string ss = "select distinct o.distype from order_col_details sd,foe o,order_col_master sm where o.prodid=sd.prod_id and sm.order_no=sd.order_no and custid=cust_id and custid='" + SqlDtr["Cust_ID"].ToString() + "' and sd.prod_id='" + SqlDtr["Prod_ID"].ToString() + "' and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.trimDate(SqlDtr["order_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.trimDate(SqlDtr["Order_Date"].ToString()) + "'";
                            rdr3 = obj1.GetRecordSet("select distinct o.distype from order_col_details sd,foe o,order_col_master sm where o.prodid=sd.prod_id and sm.order_no=sd.order_no and custid=cust_id and custid='" + SqlDtr["Cust_ID"].ToString() + "' and sd.prod_id='" + SqlDtr["Prod_ID"].ToString() + "' and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.trimDate(SqlDtr["order_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.trimDate(SqlDtr["Order_Date"].ToString()) + "'");
                            if (rdr3.HasRows)
                            {
                                if (rdr3.Read())
                                {
                                    controlTmpFoeType.Add(rdr3.GetValue(0).ToString());
                                }
                            }
                            rdr3.Close();
                        }
                        else
                            controlTmpFoeType.Add(SqlDtr["FoeType"].ToString());

                        if (SqlDtr["SchType"].ToString() == "")
                        {
                            //rdr3 = obj1.GetRecordSet("select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and (o.schname='Primary(LTR&% Scheme)' or o.schname='Secondry(LTR Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+SqlDtr["Prod_ID"].ToString()+"'");
                            rdr3 = obj1.GetRecordSet("select o.discounttype from order_col_details sd,oilscheme o,order_col_master sm where o.prodid=sd.prod_id and sm.order_no=sd.order_no and sd.order_no='" + SqlDtr["Order_No"].ToString() + "' and (o.schname='Primary(LTR&% Scheme)' or o.schname='Secondry(LTR Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.trimDate(SqlDtr["order_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.trimDate(SqlDtr["Order_Date"].ToString()) + "' and sd.prod_id='" + SqlDtr["Prod_ID"].ToString() + "'");
                            if (rdr3.HasRows)
                            {
                                if (rdr3.Read())
                                {
                                    controlTmpSchType.Add(rdr3.GetValue(0).ToString());
                                }
                            }
                            rdr3.Close();
                        }
                        else
                            controlTmpSchType.Add(SqlDtr["SchType"].ToString());

                        //Qty[i].ToolTip = "Actual Available Stock = " + Qty[i].Text.ToString() + " + " + AvStock[i].Text.ToString();


                        string sql11 = "select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,p.Prod_ID,p.unit" +
                            " from Products p, Order_Col_Details sd" +
                            " where p.Prod_ID=sd.prod_id and sd.Rate =0 and sd.Amount = 0 and sno=" + i + " and sd.Order_no='" + id + "'";
                        dbobj.SelectQuery(sql11, ref rdr2);
                        if (rdr2.HasRows)
                        {
                            while (rdr2.Read())
                            {
                                controlProdType1.Add(rdr2.GetValue(1).ToString() + ":" + rdr2.GetValue(2).ToString());
                                controlSalesQty1.Add(rdr2.GetValue(3).ToString());
                                controlSchProductType.Add(rdr2.GetValue(0).ToString());
                                controlSchProductName.Add(rdr2.GetValue(1).ToString());
                                controlSchProductPack.Add(rdr2.GetValue(2).ToString());
                                controlSchProductQty.Add(rdr2.GetValue(3).ToString());
                                controlTempSchQty.Add(rdr2.GetValue(3).ToString());
                                string sql12 = "select top 1 Closing_Stock from Stock_Master where productid=" + rdr2.GetValue(4).ToString() + " order by stock_date desc";
                                dbobj.SelectQuery(sql12, ref rdr1);
                                if (rdr1.Read())
                                {
                                    controlStk1.Add(rdr1["Closing_Stock"] + " " + rdr2.GetValue(5).ToString());
                                }
                                else
                                {
                                    controlStk1.Add("0" + " " + rdr2.GetValue(5).ToString());
                                }

                                /*********
                                rdr3 = obj1.GetRecordSet("select o.discounttype from Order_Col_details sd,oilscheme o,Order_Col_master sm where o.prodid=sd.prod_id and sm.Order_no=sd.Order_no and sd.Order_no='"+SqlDtr["Order_No"].ToString()+"' and o.schname='Primary(LTR&% Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)>='"+GenUtil.str2DDMMYYYY(SqlDtr["Order_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)<='"+GenUtil.str2DDMMYYYY(SqlDtr["Order_Date"].ToString())+"' and sd.prod_id='"+rdr2["Prod_ID"].ToString()+"'");
                                if(rdr3.HasRows)
                                {
                                    if(rdr3.Read())
                                    {
                                        tmpSchType[i].Value=rdr3.GetValue(0).ToString();
                                    }
                                }
                                rdr3.Close();
                                **********/
                            }
                            rdr1.Close();
                        }
                        rdr2.Close();
                        rdr.Close();
                        i++;
                    }
                }
                sales.SalesQty = controlSalesQty;
                sales.ProductType = controlProductType;
                sales.ProdType = controlProdType;
                sales.ProductName = controlProductName;
                sales.ProductPack = controlProductPack;
                sales.ProductQty = controlProductQty;
                sales.PID = controlPID;
                sales.PID1 = controlPID1;
                sales.scheme = controlscheme;
                sales.Details_foe = controlDetails_foe;
                sales.Av_Stock = controlAv_Stock;
                sales.SchSPType = controlSchSPType;
                sales.SchSP = controlSchSP;
                sales.tmpFoeType = controlTmpFoeType;
                sales.SalesQty1 = controlSalesQty1;
                sales.ProdType1 = controlProdType1;
                sales.tempSchQty = controlTempSchQty;
                sales.tmpSchType = controlTmpSchType;
                sales.stk1 = controlStk1;

                sales.SchProductType = controlSchProductType;
                sales.SchProductName = controlSchProductName;
                sales.SchProductPack = controlSchProductPack;
                sales.SchProductQty = controlSchProductQty;
                sales.Rate = controlRate;
                sales.Amount = controlAmount;
                SqlDtr.Close();
                #endregion
            }

            return sales;
        }

        [HttpGet]
        [Route("api/Sales/GetDataSelectedSalesInvoice")]
        public SalesModels GetDataSelectedSalesInvoice(string id)
        {
            SalesModels sales = new SalesModels();

            InventoryClass obj = new InventoryClass();
            InventoryClass obj1 = new InventoryClass();
            SqlDataReader SqlDtr;
            #region Fetch the From and To Date From OrganisationDatail table.
            GetFromDateToDate();
            #endregion
            string sql = "select * from Sales_Master sm,employee e where Under_SalesMan=emp_id and Invoice_No='" + int.Parse(FromDate) + ToDate + id + "'";
            string sql1 = "1";

            SqlDataReader rdr = null, rdr1 = null, rdr2 = null, rdr3 = null;
            int i = 0;
            //FlagPrint = false;
            //Button1.CausesValidation = true;



            if (FromDate != "")
            {
                SqlDtr = obj.GetRecordSet(sql);
            }
            else
            {
                SqlDtr = null;
            }
            while (SqlDtr.Read())
            {
                //sales.Invoice_Date = SqlDtr.GetValue(1).ToString();
                string strDate = SqlDtr.GetValue(1).ToString().Trim();
                int pos = strDate.IndexOf(" ");
                if (pos != -1)
                {
                    strDate = strDate.Substring(0, pos);
                }
                else
                {
                    strDate = "";
                }
                sales.Invoice_Date = GenUtil.str2DDMMYYYY(strDate);
                //tempInvoiceDate.Value = GenUtil.str2DDMMYYYY(strDate);
                sales.Sales_Type = (SqlDtr.GetValue(2).ToString());
                sales.Under_SalesMan = SqlDtr.GetValue(4).ToString();
                //DropUnderSalesMan.SelectedIndex=(DropUnderSalesMan.Items.IndexOf((DropUnderSalesMan.Items.FindByValue(SqlDtr.GetValue(4).ToString()))));

                sales.Vehicle_No = SqlDtr.GetValue(5).ToString();

                //txtGrandTotal.Text = SqlDtr.GetValue(6).ToString();
                sales.Grand_Total = GenUtil.strNumericFormat(SqlDtr.GetValue(6).ToString());
                sales.Discount = float.Parse(SqlDtr.GetValue(7).ToString());
                string strDisc = GenUtil.strNumericFormat(SqlDtr.GetValue(7).ToString());
                sales.Discount = float.Parse(strDisc);
                sales.Discount_Type = (SqlDtr.GetValue(8).ToString());
                string strNetAmount = GenUtil.strNumericFormat(SqlDtr.GetValue(9).ToString());
                sales.Net_Amount = float.Parse(strNetAmount);
                //tempNetAmount.Value = SqlDtr.GetValue(9).ToString();                               //Add by vikas 14.07.09
                //tempNetAmount.Value = GenUtil.strNumericFormat(tempNetAmount.Value.ToString());     //Add by vikas 14.07.09

                //NetAmount = GenUtil.strNumericFormat(txtNetAmount.Text.ToString());

                sales.Promo_Scheme = SqlDtr.GetValue(10).ToString();
                sales.Remark = SqlDtr.GetValue(11).ToString();
                sales.Entry_By = SqlDtr.GetValue(12).ToString();
                sales.Entry_Time = System.Convert.ToDateTime(SqlDtr.GetValue(13).ToString());
                sales.SecSPDisc = float.Parse(SqlDtr["SecSPDisc"].ToString());
                //******************
                if (SqlDtr["Discount_type"].ToString() == "Per")
                {
                    string strTotalDisc = System.Convert.ToString((double.Parse(SqlDtr["Grand_Total"].ToString()) - double.Parse(SqlDtr["schdiscount"].ToString())) * double.Parse(SqlDtr["discount"].ToString()) / 100);
                    sales.Total_Discount = float.Parse(System.Convert.ToString(Math.Round(double.Parse(strTotalDisc), 2)));
                }
                else
                {
                    double Discount = double.Parse(GenUtil.strNumericFormat(SqlDtr["Discount"].ToString())) * double.Parse(GenUtil.strNumericFormat(SqlDtr["totalqtyltr"].ToString()));
                    sales.Total_Discount = float.Parse(GenUtil.strNumericFormat(Discount.ToString()));
                }


                if (SqlDtr["cash_Disc_type"].ToString() == "Per")
                {
                    double tot = 0;
                    if (Convert.ToString(sales.Total_Discount) != "")
                        tot = double.Parse(SqlDtr["Grand_Total"].ToString()) - (double.Parse(SqlDtr["schdiscount"].ToString()) + double.Parse(SqlDtr["foediscount"].ToString()) + double.Parse(sales.Total_Discount.ToString()));
                    else
                        tot = double.Parse(SqlDtr["Grand_Total"].ToString()) - (double.Parse(SqlDtr["schdiscount"].ToString()) + double.Parse(SqlDtr["foediscount"].ToString()));
                    string strCashDiscount = System.Convert.ToString(tot * double.Parse(SqlDtr["Cash_Discount"].ToString()) / 100);
                    sales.Total_Discount = float.Parse(System.Convert.ToString(Math.Round(double.Parse(strCashDiscount), 2)));
                }
                else
                {
                    double cashDiscount = double.Parse(GenUtil.strNumericFormat(SqlDtr["Cash_Discount"].ToString())) * double.Parse(GenUtil.strNumericFormat(SqlDtr["totalqtyltr"].ToString()));
                    sales.Total_Discount = float.Parse(GenUtil.strNumericFormat(cashDiscount.ToString()));
                }

                string strCashDisc = SqlDtr.GetValue(15).ToString();
                sales.Cash_Discount = float.Parse(GenUtil.strNumericFormat(strCashDisc));

                sales.Cash_Disc_Type = (SqlDtr.GetValue(16).ToString());
                sales.IGST_Amount = float.Parse(SqlDtr.GetValue(17).ToString());
                sales.Scheme_Discount = float.Parse(SqlDtr.GetValue(18).ToString());
                sales.FOE_Discount = float.Parse(SqlDtr.GetValue(19).ToString());
                sales.FOE_Discounttype = (SqlDtr.GetValue(20).ToString());
                sales.FOE_Discountrs = float.Parse(SqlDtr.GetValue(21).ToString());
                sales.Total_Qty_Ltr = float.Parse(SqlDtr.GetValue(22).ToString());

                if (SqlDtr["ChallanNo"].ToString() == "0")
                    sales.ChallanNo = "";
                else
                    sales.ChallanNo = SqlDtr["ChallanNo"].ToString();

                if (GenUtil.trimDate(SqlDtr["ChallanDate"].ToString()) == "1/1/1900")
                    sales.ChallanDate = System.Convert.ToDateTime("1/1/1900");
                else
                    sales.ChallanDate = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["ChallanDate"].ToString())));

                sales.CGST_Amount = float.Parse(SqlDtr.GetValue(27).ToString());
                sales.SGST_Amount = float.Parse(SqlDtr.GetValue(26).ToString());
            }
            SqlDtr.Close();


            #region Get Customer name and place regarding Customer ID            
            sql = "select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID,ct.group_name from Customer as c, sales_master as s,customertype as ct where c.Cust_ID= s.Cust_ID and c.cust_type=ct.customertypename and s.Invoice_No='" + FromDate + ToDate + id + "'";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                sales.Cust_Name = SqlDtr.GetValue(0).ToString() + ":" + SqlDtr.GetValue(1).ToString(); //Add by vikas sharma 27.04.09

                //Cache["CustName"]=SqlDtr.GetValue(0).ToString();
                sales.Cust_ID = Int32.Parse(SqlDtr["Cust_ID"].ToString());
                //sales.City = SqlDtr["City"].ToString();
                sales.Place = SqlDtr.GetValue(1).ToString();//System.Convert.ToDateTime(
                sales.DueDate = DateTime.Now.AddDays(System.Convert.ToDouble(SqlDtr.GetValue(2).ToString()));
                string duedatestr = (sales.DueDate.ToShortDateString());
                sales.DueDate = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(duedatestr));
                sales.Current_Balance = GenUtil.strNumericFormat(SqlDtr.GetValue(3).ToString());
                //TxtCrLimit.Value = SqlDtr.GetValue(4).ToString();
                sales.Credit_Limit = float.Parse(SqlDtr.GetValue(4).ToString());
                //txtcusttype.Text = SqlDtr.GetValue(5).ToString();

                if (SqlDtr["Group_Name"].ToString() != null && SqlDtr["Group_Name"].ToString() != "")
                    sales.Group_Name = SqlDtr["Group_Name"].ToString();
                /*********************************************/
            }
            SqlDtr.Close();

            //Coment by vikas 06.08.09 sql="select top 1 balance,balancetype  from CustomerLedgerTable as c, sales_master as s where c.CustID= s.Cust_ID and s.Invoice_No='"+FromDate+ToDate+dropInvoiceNo.SelectedValue+"' order by entrydate desc";
            sql = "select top 1 Balance,BalanceType from customerledgertable where CustID=" + sales.Cust_ID + " order by EntryDate Desc";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                sales.Balance = SqlDtr.GetValue(0).ToString();
                sales.BalanceType = SqlDtr.GetValue(1).ToString();
                //sales.Current_Balance = GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString()) + " " + SqlDtr.GetValue(1).ToString();
            }
            SqlDtr.Close();

            #endregion

            #region Get Data from Sales Details Table regarding Invoice No.            
            sql = "select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.invoice_no,sm.invoice_date,sm.cust_id,sd.SchType,sd.FoeType,sd.SPDiscType,sd.SPDisc,p.Prod_Code" +
                " from Products p, sales_Details sd,sales_master sm" +
                " where p.Prod_ID=sd.prod_id and sd.invoice_no=sm.invoice_no and sd.Rate >0 and sd.Amount > 0 and sd.invoice_no='" + FromDate + ToDate + id + "' order by sd.sno";
            /* **********end***************************************/

            List<string> controlSalesQty = new List<string>();

            List<string> controlProdType = new List<string>();

            List<string> controlProductType = new List<string>();
            List<string> controlProductName = new List<string>();
            List<string> controlProductPack = new List<string>();
            List<string> controlProductQty = new List<string>();
            List<string> controlPID = new List<string>();
            List<string> controlPID1 = new List<string>();
            List<string> controlscheme = new List<string>();
            List<string> controlDetails_foe = new List<string>();
            List<string> controlAv_Stock = new List<string>();
            List<string> controlSchSPType = new List<string>();
            List<string> controlSchSP = new List<string>();
            List<string> controlTmpFoeType = new List<string>();
            List<string> controlSalesQty1 = new List<string>();
            List<string> controlProdType1 = new List<string>();
            List<string> controlTempSchQty = new List<string>();
            List<string> controlTmpSchType = new List<string>();
            List<string> controlStk1 = new List<string>();
            List<string> controlRate = new List<string>();
            List<string> controlAmount = new List<string>();
            List<string> controlSchProductType = new List<string>();
            List<string> controlSchProductName = new List<string>();
            List<string> controlSchProductPack = new List<string>();
            List<string> controlSchProductQty = new List<string>();
            //List<string> controlTmpQty = new List<string>();
            //List<string> controlTempQty = new List<string>();

            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                if (SqlDtr.GetValue(3).ToString() != null)
                    controlSalesQty.Add(SqlDtr.GetValue(3).ToString());

                controlProdType.Add(SqlDtr.GetValue(17).ToString() + ":" + SqlDtr.GetValue(1).ToString() + ":" + SqlDtr.GetValue(2).ToString());

                controlProductType.Add(SqlDtr.GetValue(0).ToString());
                controlProductName.Add(SqlDtr.GetValue(1).ToString());
                controlProductPack.Add(SqlDtr.GetValue(2).ToString());
                controlProductQty.Add(SqlDtr.GetValue(3).ToString());

                //controlTempQty.Add(SqlDtr.GetValue(3).ToString());
                //controlTmpQty.Add(SqlDtr.GetValue(3).ToString());
                //tempQty[i].Text = sale.SalesQty[i];
                //tmpQty[i].Value = SqlDtr.GetValue(3).ToString();
                controlRate.Add(SqlDtr.GetValue(4).ToString());
                controlAmount.Add(SqlDtr.GetValue(5).ToString());
                //********
                controlPID.Add(SqlDtr.GetValue(6).ToString());
                controlPID1.Add(SqlDtr.GetValue(6).ToString());
                /*bhal*/
                controlscheme.Add(SqlDtr.GetValue(8).ToString());
                controlDetails_foe.Add(SqlDtr.GetValue(9).ToString());
                //********
                sql1 = "select top 1 Closing_Stock from Stock_Master where productid=" + SqlDtr.GetValue(6).ToString() + " order by stock_date desc";
                dbobj.SelectQuery(sql1, ref rdr);
                if (rdr.Read())
                {
                    controlAv_Stock.Add(rdr["Closing_Stock"] + " " + SqlDtr.GetValue(7).ToString());
                }
                else
                {
                    controlAv_Stock.Add("0" + " " + SqlDtr.GetValue(7).ToString());
                }

                if (SqlDtr["SPDiscType"].ToString() == "")
                {
                    rdr3 = obj1.GetRecordSet("select o.DiscountType,o.Discount from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='" + SqlDtr["invoice_No"].ToString() + "' and o.schname='Secondry SP(LTRSP Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and sd.prod_id='" + SqlDtr["Prod_ID"].ToString() + "'");
                    if (rdr3.HasRows)
                    {
                        if (rdr3.Read())
                        {
                            controlSchSPType.Add(rdr3.GetValue(0).ToString());
                            controlSchSP.Add(rdr3.GetValue(1).ToString());
                        }
                    }
                    rdr3.Close();
                }
                else
                {
                    controlSchSPType.Add(SqlDtr["SPDiscType"].ToString());
                    controlSchSP.Add(SqlDtr["SPDisc"].ToString());
                }
                //strstrste="select distinct o.distype from sales_details sd,foe o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and custid=cust_id and custid='1470' and sd.prod_id='1037' and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"'";
                if (SqlDtr["FoeType"].ToString() == "")
                {
                    rdr3 = obj1.GetRecordSet("select distinct o.distype from sales_details sd,foe o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and custid=cust_id and custid='" + SqlDtr["Cust_ID"].ToString() + "' and sd.prod_id='" + SqlDtr["Prod_ID"].ToString() + "' and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "'");
                    if (rdr3.HasRows)
                    {
                        if (rdr3.Read())
                        {
                            controlTmpFoeType.Add(rdr3.GetValue(0).ToString());
                        }
                    }
                    rdr3.Close();
                }
                else
                    controlTmpFoeType.Add(SqlDtr["FoeType"].ToString());
                //*************
                if (SqlDtr["SchType"].ToString() == "")
                {
                    string ssssss = "select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='" + SqlDtr["invoice_No"].ToString() + "' and (o.schname='Primary(LTR&% Scheme)' or o.schname='Secondry(LTR Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and sd.prod_id='" + SqlDtr["Prod_ID"].ToString() + "'";
                    //rdr3 = obj1.GetRecordSet("select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and o.schname='Primary(LTR&% Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)>='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)<='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+rdr2["Prod_ID"].ToString()+"'");
                    rdr3 = obj1.GetRecordSet("select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='" + SqlDtr["invoice_No"].ToString() + "' and (o.schname='Primary(LTR&% Scheme)' or o.schname='Secondry(LTR Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and sd.prod_id='" + SqlDtr["Prod_ID"].ToString() + "'");
                    if (rdr3.HasRows)
                    {
                        if (rdr3.Read())
                        {
                            controlTmpSchType.Add(rdr3.GetValue(0).ToString());
                        }
                    }
                    rdr3.Close();
                }
                else
                    controlTmpSchType.Add(SqlDtr["SchType"].ToString());
                //*************
                string sql11 = "select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,p.Prod_ID,p.unit" +
                    " from Products p, sales_Details sd" +
                    " where p.Prod_ID=sd.prod_id and sd.Rate =0 and sd.Amount = 0 and sno=" + i + " and sd.invoice_no='" + FromDate + ToDate + id + "'";
                dbobj.SelectQuery(sql11, ref rdr2);

                if (rdr2.HasRows)
                {
                    while (rdr2.Read())
                    {
                        //ProdType1[i].Text=rdr2.GetValue(0).ToString();
                        controlProdType1.Add(rdr2.GetValue(1).ToString() + ":" + rdr2.GetValue(2).ToString());
                        //**ProdName1[i].Text=rdr2.GetValue(1).ToString();
                        //**PackType1[i].Text=rdr2.GetValue(2).ToString();
                        controlSalesQty1.Add(rdr2.GetValue(3).ToString());
                        //*************
                        controlSchProductType.Add(rdr2.GetValue(0).ToString());
                        controlSchProductName.Add(rdr2.GetValue(1).ToString());
                        controlSchProductPack.Add(rdr2.GetValue(2).ToString());
                        controlSchProductQty.Add(rdr2.GetValue(3).ToString());
                        //**************
                        controlTempSchQty.Add(rdr2.GetValue(3).ToString());
                        string sql12 = "select top 1 Closing_Stock from Stock_Master where productid=" + rdr2.GetValue(4).ToString() + " order by stock_date desc";
                        dbobj.SelectQuery(sql12, ref rdr1);
                        if (rdr1.Read())
                        {
                            controlStk1.Add(rdr1["Closing_Stock"] + " " + rdr2.GetValue(5).ToString());
                        }
                        else
                        {
                            controlStk1.Add("0" + " " + rdr2.GetValue(5).ToString());
                        }

                        rdr3 = obj1.GetRecordSet("select o.distype from sales_details sd,foe o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='" + SqlDtr["invoice_No"].ToString() + "' and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString()) + "' and sd.prod_id='" + rdr2["Prod_ID"].ToString() + "'");
                        if (rdr3.HasRows)
                        {
                            if (rdr3.Read())
                            {
                                controlTmpFoeType.Add(rdr3.GetValue(0).ToString());
                            }
                        }
                        rdr3.Close();
                    }
                    rdr1.Close();
                }
                rdr2.Close();
                rdr.Close();

                i++;
            }
            sales.SalesQty = controlSalesQty;
            sales.ProductType = controlProductType;
            sales.ProdType = controlProdType;
            sales.ProductName = controlProductName;
            sales.ProductPack = controlProductPack;
            sales.ProductQty = controlProductQty;
            sales.PID = controlPID;
            sales.PID1 = controlPID1;
            sales.scheme = controlscheme;
            sales.Details_foe = controlDetails_foe;
            sales.Av_Stock = controlAv_Stock;
            sales.SchSPType = controlSchSPType;
            sales.SchSP = controlSchSP;
            sales.tmpFoeType = controlTmpFoeType;
            sales.SalesQty1 = controlSalesQty1;
            sales.ProdType1 = controlProdType1;
            sales.tempSchQty = controlTempSchQty;
            sales.tmpSchType = controlTmpSchType;
            sales.stk1 = controlStk1;

            sales.SchProductType = controlSchProductType;
            sales.SchProductName = controlSchProductName;
            sales.SchProductPack = controlSchProductPack;
            sales.SchProductQty = controlSchProductQty;
            sales.Rate = controlRate;
            sales.Amount = controlAmount;

            SqlDtr.Close();
            #endregion

            return sales;
        }

        public void GetFromDateToDate()
        {
            #region Fetch the From and To Date From OrganisationDatail table.
            SqlDataReader rdr = null;
            dbobj.SelectQuery("select * from organisation", ref rdr);
            if (rdr.Read())
            {
                FromDate = GetYear(GenUtil.trimDate(rdr["Acc_date_from"].ToString()));
                if (FromDate != "")
                    FromDate = System.Convert.ToString(int.Parse(FromDate));

                ToDate = GetYear(GenUtil.trimDate(rdr["Acc_date_To"].ToString()));
            }
            else
            {
                //MessageBox.Show("Please Fill The Organization Form First");                
            }
            #endregion
        }
        /// <summary>
		/// This method fatch the only year according to passing date.
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string GetYear(string dt)
        {
            if (dt != "")
            {
                string[] year = dt.IndexOf("-") > 0 ? dt.Split(new char[] { '-' }, dt.Length) : dt.Split(new char[] { '/' }, dt.Length);
                string yr = year[2].Substring(2);
                return (yr);
            }
            else
                return "";
        }

        [HttpGet]
        [Route("api/Sales/GetCustomerVehicles")]
        public List<string> GetCustomerVehicles(string cust_id)
        {
            List<string> vehNo = new List<string>();
            try
            {

                SqlDataReader SqlDtr = null;
                dbobj.SelectQuery("Select * from Customer_Vehicles where Cust_ID =" + cust_id, ref SqlDtr);
                if (SqlDtr.HasRows)
                {
                    int i = 0;
                    while (SqlDtr.Read())
                    {
                        if (!SqlDtr.GetValue(2).ToString().Trim().Equals(""))
                        {
                            vehNo.Add(SqlDtr.GetValue(2).ToString());
                            i++;
                        }

                        if (!SqlDtr.GetValue(3).ToString().Trim().Equals(""))
                        {
                            vehNo.Add(SqlDtr.GetValue(3).ToString());
                            i++;
                        }

                        if (!SqlDtr.GetValue(4).ToString().Trim().Equals(""))
                        {
                            vehNo.Add(SqlDtr.GetValue(4).ToString());
                            i++;
                        }

                        if (!SqlDtr.GetValue(5).ToString().Trim().Equals(""))
                        {
                            vehNo.Add(SqlDtr.GetValue(5).ToString());
                            i++;
                        }

                        if (!SqlDtr.GetValue(6).ToString().Trim().Equals(""))
                        {
                            vehNo.Add(SqlDtr.GetValue(6).ToString());
                            i++;
                        }

                        if (!SqlDtr.GetValue(7).ToString().Trim().Equals(""))
                        {
                            vehNo.Add(SqlDtr.GetValue(7).ToString());
                            i++;
                        }

                        if (!SqlDtr.GetValue(8).ToString().Trim().Equals(""))
                        {
                            vehNo.Add(SqlDtr.GetValue(8).ToString());
                            i++;
                        }

                        if (!SqlDtr.GetValue(9).ToString().Trim().Equals(""))
                        {
                            vehNo.Add(SqlDtr.GetValue(9).ToString());
                            i++;
                        }

                        if (!SqlDtr.GetValue(10).ToString().Trim().Equals(""))
                        {
                            vehNo.Add(SqlDtr.GetValue(10).ToString());
                            i++;
                        }

                        if (!SqlDtr.GetValue(11).ToString().Trim().Equals(""))
                        {
                            vehNo.Add(SqlDtr.GetValue(11).ToString());
                            i++;
                        }

                    }
                    SqlDtr.Close();
                    //return true;
                }
                return vehNo;
            }
            catch (Exception ex)
            {
                //CreateLogFiles.ErrorLog("Form:Sales Invoice.aspx,Method:getCustomerVehicles().  EXCEPTION  " + ex.Message + "  userid " + "   " + "   " + uid);
            }
            return vehNo;
        }

    }
}
