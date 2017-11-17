using Servo_API.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using Servo_API.Models;
using System.Web.Http;

namespace Servo_API.Controllers
{
    public class SalaryStatementController : ApiController
    {
        InventoryClass obj = new InventoryClass();

        SqlConnection sqlcon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
        DbOperations_LATEST.DBUtil dbobj = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);

        [HttpGet]
        [Route("api/SalaryStatement/BindTheData")]

        public IHttpActionResult BindTheData(string str1, string str2)
        {
            try
            {
                SqlDataReader SqlDtr;
                string sql, str3 = "";
                sql = "select sum(cast(status as integer)) Total_Present from attandance_register where cast(floor(cast(cast(att_date as datetime) as float)) as datetime) >= '" + str1 + "' and cast(floor(cast(cast(att_date as datetime) as float)) as datetime) <= '" + str2 + "' ";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    str3 = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();

                return Ok(str3);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
            
        }

        [HttpGet]
        [Route("api/SalaryStatement/GetData")]
        public IHttpActionResult GetData()
        {
            try
            {
                string sql;
                DataSet ds = new DataSet();
                sql = "select emp_id,emp_name, salary, ot_compensation from employee where status='1'";
                SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                //da.Fill(ds);
                da.Fill(ds, "stkmv");
                return Ok(ds);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
            

        }

        [HttpGet]
        [Route("api/SalaryStatement/FindLeaves")]
        public IHttpActionResult FindLeaves(string str1,string from_date,string emp_id)
        {
            try
            {
                SqlDataReader SqlDtr;
                string sql;
                int leave = 0;
                sql = "select sum(datediff(day,date_from,dateadd(day,1,date_to))) from leave_register where cast(floor(cast(Date_From  as float))as datetime) >= '" + str1 + "' and cast(floor(cast(date_to as float)) as datetime) <= '" + from_date + "' and emp_id = '" + emp_id + "' and isSanction = 1";
                SqlDtr = obj.GetRecordSet(sql);
                if (SqlDtr.HasRows)
                {
                    if (SqlDtr.Read())
                    {
                        if (!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
                            leave = System.Convert.ToInt32(SqlDtr.GetValue(0).ToString());
                    }
                }
                SqlDtr.Close();
                return Ok(leave);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");                
            }
            
        }

        [HttpGet]
        [Route("api/SalaryStatement/FindDays")]
        public IHttpActionResult FindDays(string from_date, string emp_id)
        {
            try
            {
                SqlDataReader SqlDtr;
                string sql;
                int leave = 0;
                sql = "select sum(datediff(day,date_from,dateadd(day,1,'" + from_date + "'))) from leave_register where cast(floor(cast(date_from as float)) as datetime) <= '" + from_date + "' and cast(floor(cast(date_to as float)) as datetime) > '" + from_date + "'and emp_id = '" + emp_id + "' and isSanction = 1 and datepart(month,date_from) = datepart(month,'" + from_date + "')";                
                SqlDtr = obj.GetRecordSet(sql);
                if (SqlDtr.HasRows)
                {
                    if (SqlDtr.Read())
                    {
                        if (!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
                            leave += System.Convert.ToInt32(SqlDtr.GetValue(0).ToString());
                    }
                }
                SqlDtr.Close();
                return Ok(leave);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/SelectCase")]
        public IHttpActionResult SelectCase(string from_date, string emp_id,string str1)
        {
            try
            {
                SqlDataReader SqlDtr;
                string sql;
                int leave = 0;
                sql = "select case when cast(floor(cast(date_to as float)) as datetime) >= '" + from_date + "' then sum(datediff(day,'" + str1 + "',dateadd(day,1,'" + from_date + "'))) else sum(datediff(day,'" + str1 + "',dateadd(day,1,date_to))) end from leave_register where cast(floor(cast(date_from as float)) as datetime) < '" + str1 + "' and cast(floor(cast(date_to as float)) as datetime) >= '" + str1 + "'and emp_id = '" + emp_id + "' and isSanction = 1 group by date_to";               
                SqlDtr = obj.GetRecordSet(sql);
                if (SqlDtr.HasRows)
                {
                    if (SqlDtr.Read())
                    {
                        if (!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
                            leave += System.Convert.ToInt32(SqlDtr.GetValue(0).ToString());
                    }
                }
                SqlDtr.Close();
                return Ok(leave);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/SelectSum")]
        public IHttpActionResult SelectSum(string str2, string str3, string emp_id)
        {
            try
            {
                SqlDataReader SqlDtr;
                string sql;
                string leave="";
                sql = "select sum(cast(status as integer)) Total_Present from attandance_register where cast(floor(cast(cast(att_date as datetime) as float)) as datetime) >= '" + str2 + "' and cast(floor(cast(cast(att_date as datetime) as float)) as datetime) <= '" + str3 + "' and emp_id='" + emp_id + "'";
               
                SqlDtr = obj.GetRecordSet(sql);
                while(SqlDtr.Read())
                {
                    leave = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();
                return Ok(leave);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/SelectSumAndTime")]
        public IHttpActionResult SelectSumAndTime(string str2, string str3, string emp_id)
        {
            try
            {
                SalaryStatementModel salary = new SalaryStatementModel();
                SqlDataReader SqlDtr;
                string sql;                
                sql = "select sum(datepart(hour,Ot_To)-datepart(hour,Ot_From)) OT_Hour,sum(datepart(minute,Ot_To)-datepart(minute,Ot_From)) OT_Minute from OverTime_Register where cast(floor(cast(OT_Date as float)) as datetime) >= '" + str2 + "' and cast(floor(cast(OT_Date as float)) as datetime) <= '" + str3 + "' and emp_id='" + emp_id + "'";

                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    salary.hour = SqlDtr.GetValue(0).ToString();
                    salary.min = SqlDtr.GetValue(1).ToString();
                }
                SqlDtr.Close();
                return Ok(salary);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/SelectLedgerId")]
        public IHttpActionResult SelectLedgerId(string evalue)
        {
            SqlDataReader SqlDtr=null;            
            string Ledger_ID="";
            dbobj.SelectQuery("Select Ledger_ID from Ledger_Master where Ledger_Name ='" + evalue + "'", ref SqlDtr);
            if (SqlDtr.Read())
            {
                Ledger_ID = SqlDtr["Ledger_ID"].ToString();
            }
            SqlDtr.Close();
            if (Ledger_ID == "" || Ledger_ID == null)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
            return Ok(Ledger_ID);
        }

        [HttpGet]
        [Route("api/SalaryStatement/SelectSumAdvance")]
        public IHttpActionResult SelectSumAdvance(string str1,string str2,string Ledger_ID)
        {
            try
            {
                SqlDataReader SqlDtr = null;
                string SumAdvance = "";
                string sql;
                sql = "select sum(cast(Debit_Amount as float)) advance from AccountsLedgerTable where cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '" + str1 + "' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '" + str2 + "' and particulars like ('Payment%') and Ledger_ID='" + Ledger_ID +"'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    SumAdvance = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();
                
                return Ok(SumAdvance);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
            
        }

        [HttpGet]
        [Route("api/SalaryStatement/SelectBalance")]
        public IHttpActionResult SelectBalance(string str3, string str4, string Ledger_ID)
        {
            try
            {
                SqlDataReader SqlDtr = null;
                string Balance = "";
                string sql;
                sql = "select Balance from AccountsLedgerTable where particulars like ('Opening%') and Ledger_ID=" + Ledger_ID + " and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '" + str3 + "' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '" + str4 + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    Balance = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();

                return Ok(Balance);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/SelectCreditAmtOfCustomer")]
        public IHttpActionResult SelectCreditAmtOfCustomer(string str5, string str6, string emp_id)
        {
            try
            {
                SqlDataReader SqlDtr = null;
                double Balance = 0;
                string sql;
                sql = "select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='" + emp_id + "') and particular like 'Payment Received%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='" + str5 + "' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='" + str6 + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    if(SqlDtr.GetValue(0).ToString() != "")
                    Balance = Convert.ToDouble(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();

                return Ok(Balance);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/SelectBounce")]
        public IHttpActionResult SelectBounce(string str5, string str6, string emp_id)
        {
            try
            {
                SqlDataReader SqlDtr = null;
                string Bounce = "";
                string sql;
                sql = "select sum(DebitAmount) from customerledgertable where custid in(select cust_id from customer where ssr='" + emp_id + "') and particular like'voucher(5%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='" + str5 + "' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='" + str6 + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    Bounce = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();

                return Ok(Bounce);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/SelectCreditAmt")]
        public IHttpActionResult SelectCreditAmt(string str5, string str6, string emp_id)
        {
            try
            {
                SqlDataReader SqlDtr = null;
                string creditAmt = "";
                string sql;
                sql = "select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='" + emp_id + "') and particulars like 'Receipt_cd%' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='" + str5 + "' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='" + str6 + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    creditAmt = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();

                return Ok(creditAmt);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
        }

        [HttpGet]
        [Route("api/SalaryStatement/SelectSumOfCreditAmt")]
        public IHttpActionResult SelectSumOfCreditAmt(string str5, string str6, string emp_id)
        {
            try
            {
                SqlDataReader SqlDtr = null;
                string SumOfcreditAmt = "";
                string sql;
                sql = "select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='" + emp_id + "') and particular like 'voucher(3%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='" + str5 + "' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='" + str6 + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    SumOfcreditAmt = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();

                return Ok(SumOfcreditAmt);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
        }

        [HttpGet]
        [Route("api/SalaryStatement/SelectSumOfCreAmt")]
        public IHttpActionResult SelectSumOfCreAmt(string str5, string str6, string emp_id)
        {
            try
            {
                SqlDataReader SqlDtr = null;
                string SumOfcreditAmt = "";
                string sql;
                sql = "select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='" + emp_id + "') and (particulars like 'Receipt_sd%' or particulars like 'Receipt_fd%' or particulars like 'Receipt_dd%') and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='" + str5 + "' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='" + str6 + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    SumOfcreditAmt = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();

                return Ok(SumOfcreditAmt);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
        }

        [HttpGet]
        [Route("api/SalaryStatement/SelectSSRincentive")]
        public IHttpActionResult SelectSSRincentive()
        {
            try
            {
                SqlDataReader SqlDtr = null;
                SalaryStatementModel salary = new SalaryStatementModel();
                string sql;
                sql = "select SSRincentiveStatus,SSRincentive from setDis";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    salary.SSRincentiveStatus = SqlDtr.GetValue(0).ToString();
                    salary.SSRincentive = SqlDtr.GetValue(1).ToString();
                }
                SqlDtr.Close();
                return Ok(salary);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
        }

        [HttpGet]
        [Route("api/SalaryStatement/SelectSumAdv")]
        public IHttpActionResult SelectSumAdv(string str1, string str2, string Ledger_ID)
        {
            try
            {
                SqlDataReader SqlDtr = null;
                string sql, sum = "";
                sql = "select sum(cast(Debit_Amount as float)) advance from AccountsLedgerTable where cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '" + str1 + "' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '" + str2 + "' and particulars like ('Journal%') and Ledger_ID=" + Ledger_ID+"";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    sum = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();
                return Ok(sum);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
        }

        
        List<string> controlempid = new List<string>();
        List<string> controlempname = new List<string>();
        List<string> controlbasicsalary = new List<string>();
        List<string> controlextradays = new List<string>();
        
        [HttpGet]
        [Route("api/SalaryStatement/GetDataFomEmployee")]

        public IHttpActionResult GetDataFomEmployee(string strorderby)
        {
            try
            {
                SalaryStatementModel salary = new SalaryStatementModel();
                SqlDataReader SqlDtr = null;
                string sql;
                sql = "select emp_id,emp_name, salary, ot_compensation from employee where status='1' order by " + strorderby +"";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    controlempid.Add(SqlDtr.GetValue(0).ToString());
                    controlempname.Add(SqlDtr.GetValue(1).ToString());
                    controlbasicsalary.Add(SqlDtr.GetValue(2).ToString());
                    controlextradays.Add(SqlDtr.GetValue(3).ToString());
                }
                SqlDtr.Close();

                salary.empid = controlempid;
                salary.empname = controlempname;
                salary.basicsalary = controlbasicsalary;
                salary.extradays = controlextradays;
                return Ok(salary);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
        }

        [HttpGet]
        [Route("api/SalaryStatement/PrintFindLeaves")]
        public IHttpActionResult PrintFindLeaves(string str1, string from_date, string Emp_id)
        {
            try
            {
                SqlDataReader SqlDtr;
                string sql;
                int leave = 0;
                sql = "select sum(datediff(day,date_from,dateadd(day,1,date_to))) from leave_register where cast(floor(cast(Date_From  as float))as datetime) >= '" + str1 + "' and cast(floor(cast(date_to as float)) as datetime) <= '" + from_date + "' and emp_id = '" + Emp_id + "' and isSanction = 1";
                SqlDtr = obj.GetRecordSet(sql);
                if (SqlDtr.HasRows)
                {
                    if (SqlDtr.Read())
                    {
                        if (!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
                            leave = System.Convert.ToInt32(SqlDtr.GetValue(0).ToString());
                    }
                }
                SqlDtr.Close();
                return Ok(leave);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/PrintFindDays")]
        public IHttpActionResult PrintFindDays(string from_date, string Emp_id)
        {
            try
            {
                SqlDataReader SqlDtr;
                string sql;
                int leave = 0;
                sql = "select sum(datediff(day,date_from,dateadd(day,1,'" + from_date + "'))) from leave_register where cast(floor(cast(date_from as float)) as datetime) <= '" + from_date + "' and cast(floor(cast(date_to as float)) as datetime) > '" + from_date + "'and emp_id = '" + Emp_id + "' and isSanction = 1 and datepart(month,date_from) = datepart(month,'" + from_date + "')";
                SqlDtr = obj.GetRecordSet(sql);
                if (SqlDtr.HasRows)
                {
                    if (SqlDtr.Read())
                    {
                        if (!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
                            leave += System.Convert.ToInt32(SqlDtr.GetValue(0).ToString());
                    }
                }
                SqlDtr.Close();
                return Ok(leave);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/PrintSelectCase")]
        public IHttpActionResult PrintSelectCase(string from_date, string Emp_id, string str1)
        {
            try
            {
                SqlDataReader SqlDtr;
                string sql;
                int leave = 0;
                sql = "select case when cast(floor(cast(date_to as float)) as datetime) >= '" + from_date + "' then sum(datediff(day,'" + str1 + "',dateadd(day,1,'" + from_date + "'))) else sum(datediff(day,'" +str1 + "',dateadd(day,1,date_to))) end from leave_register where cast(floor(cast(date_from as float)) as datetime) < '" + str1 + "' and cast(floor(cast(date_to as float)) as datetime) >= '" + str1 + "'and emp_id = '" + Emp_id + "' and isSanction = 1 group by date_to";                
                SqlDtr = obj.GetRecordSet(sql);
                if (SqlDtr.HasRows)
                {
                    if (SqlDtr.Read())
                    {
                        if (!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
                            leave += System.Convert.ToInt32(SqlDtr.GetValue(0).ToString());
                    }
                }
                SqlDtr.Close();
                return Ok(leave);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/PrintSelectSum")]
        public IHttpActionResult PrintSelectSum(string str1, string str2, string Emp_id)
        {
            try
            {
                SqlDataReader SqlDtr;
                string sql;
                string sum = "";
                sql = "select sum(cast(status as integer)) Total_Present from attandance_register where cast(floor(cast(cast(att_date as datetime) as float)) as datetime) >= '" + str1 + "' and cast(floor(cast(cast(att_date as datetime) as float)) as datetime) <= '" + str2 + "' and emp_id='" + Emp_id + "'";

                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    sum = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();
                return Ok(sum);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/PrintSelectSumAndTime")]
        public IHttpActionResult PrintSelectSumAndTime(string str2, string str3, string Emp_id)
        {
            try
            {
                SalaryStatementModel salary = new SalaryStatementModel();
                SqlDataReader SqlDtr;
                string sql;
                sql = "select sum(datepart(hour,Ot_To)-datepart(hour,Ot_From)) OT_Hour,sum(datepart(minute,Ot_To)-datepart(minute,Ot_From)) OT_Minute from OverTime_Register where cast(floor(cast(OT_Date as float)) as datetime) >= '" + str2 + "' and cast(floor(cast(OT_Date as float)) as datetime) <= '" + str3 + "' and emp_id='" + Emp_id + "'";

                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    salary.hour = SqlDtr.GetValue(0).ToString();
                    salary.min = SqlDtr.GetValue(1).ToString();
                }
                SqlDtr.Close();
                return Ok(salary);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/FindLedgerId")]
        public IHttpActionResult FindLedgerId(string Emp_Name)
        {
            try
            {                
                SqlDataReader SqlDtr;
                string sql,ledgerid="";
                sql = "Select Ledger_ID from Ledger_Master where Ledger_Name ='" + Emp_Name+ "'";

                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    ledgerid = SqlDtr["Ledger_ID"].ToString();
                }
                SqlDtr.Close();
                return Ok(ledgerid);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/PrintSelectSumAdvance")]
        public IHttpActionResult PrintSelectSumAdvance(string str1, string str2, string Ledger_ID)
        {
            try
            {
                SqlDataReader SqlDtr = null;
                string SumAdvance = "";
                string sql;
                sql = "select sum(cast(Debit_Amount as float)) advance from AccountsLedgerTable where cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '" + str1 + "' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '" + str2 + "' and particulars like ('Payment%') and Ledger_ID=" + Ledger_ID+"";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    SumAdvance = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();

                return Ok(SumAdvance);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/PrintSelectBalance")]
        public IHttpActionResult PrintSelectBalance(string str3, string str4, string Ledger_ID)
        {
            try
            {
                SqlDataReader SqlDtr = null;
                string Balance = "";
                string sql;
                sql = "select Balance from AccountsLedgerTable where particulars like ('Opening%') and Ledger_ID=" + Ledger_ID + " and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '" + str3 + "' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '" +str4 + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    Balance = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();

                return Ok(Balance);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/PrintSelectCreditAmtOfCustomer")]
        public IHttpActionResult PrintSelectCreditAmtOfCustomer(string str5, string str6, string Emp_id)
        {
            try
            {
                SqlDataReader SqlDtr = null;
                double Balance = 0;
                string sql;
                sql = "select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='" + Emp_id + "') and particular like 'Payment Received%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='" + str5 + "' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='" + str6 + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    if (SqlDtr.GetValue(0).ToString() != "")
                        Balance = Convert.ToDouble(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();

                return Ok(Balance);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/PrintSelectBounce")]
        public IHttpActionResult PrintSelectBounce(string str5, string str6, string Emp_id)
        {
            try
            {
                SqlDataReader SqlDtr = null;
                string Bounce = "";
                string sql;
                sql = "select sum(DebitAmount) from customerledgertable where custid in(select cust_id from customer where ssr='" + Emp_id + "') and particular like'voucher(5%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='" + str5 + "' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='" + str6 + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    Bounce = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();

                return Ok(Bounce);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }

        }

        [HttpGet]
        [Route("api/SalaryStatement/PrintSelectCreditAmt")]
        public IHttpActionResult PrintSelectCreditAmt(string str5, string str6, string Emp_id)
        {
            try
            {
                SqlDataReader SqlDtr = null;
                string creditAmt = "";
                string sql;
                sql = "select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='" + Emp_id + "') and particulars like 'Receipt_cd%' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='" + str5 + "' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='" + str6 + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    creditAmt = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();

                return Ok(creditAmt);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
        }

        [HttpGet]
        [Route("api/SalaryStatement/PrintSelectSumOfCreditAmt")]
        public IHttpActionResult PrintSelectSumOfCreditAmt(string str5, string str6, string Emp_id)
        {
            try
            {
                SqlDataReader SqlDtr = null;
                string SumOfcreditAmt = "";
                string sql;
                sql = "select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='" + Emp_id + "') and particular like 'voucher(3%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='" + str5 + "' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='" + str6 + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    SumOfcreditAmt = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();

                return Ok(SumOfcreditAmt);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
        }

        [HttpGet]
        [Route("api/SalaryStatement/PrintSelectSumOfCreAmt")]
        public IHttpActionResult PrintSelectSumOfCreAmt(string str5, string str6, string Emp_id)
        {
            try
            {
                SqlDataReader SqlDtr = null;
                string SumOfcreditAmt = "";
                string sql;
                sql = "select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='" + Emp_id + "') and (particulars like 'Receipt_sd%' or particulars like 'Receipt_fd%' or particulars like 'Receipt_dd%') and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='" + str5 + "' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='" + str6 + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    SumOfcreditAmt = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();

                return Ok(SumOfcreditAmt);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
        }

        [HttpGet]
        [Route("api/SalaryStatement/PrintSelectSumAdv")]
        public IHttpActionResult PrintSelectSumAdv(string str1, string str2, string Ledger_ID)
        {
            try
            {
                SqlDataReader SqlDtr = null;
                string sql, sum = "";
                sql = "select sum(cast(Debit_Amount as float)) advance from AccountsLedgerTable where cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '" + str1 + "' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '" + str2 + "' and particulars like ('Journal%') and Ledger_ID=" + Ledger_ID + "";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    sum = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();
                return Ok(sum);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
        }

        [HttpGet]
        [Route("api/SalaryStatement/ExcelGetDataFomEmployee")]

        public IHttpActionResult ExcelGetDataFomEmployee(string strorderby)
        {
            try
            {
                SalaryStatementModel salary = new SalaryStatementModel();
                SqlDataReader SqlDtr = null;
                string sql;
                sql = "select emp_id,emp_name, salary, ot_compensation from employee where status='1' order by " + strorderby + "";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    controlempid.Add(SqlDtr.GetValue(0).ToString());
                    controlempname.Add(SqlDtr.GetValue(1).ToString());
                    controlbasicsalary.Add(SqlDtr.GetValue(2).ToString());
                    controlextradays.Add(SqlDtr.GetValue(3).ToString());
                }
                SqlDtr.Close();
                salary.empid = controlempid;
                salary.empname = controlempname;
                salary.basicsalary = controlbasicsalary;
                salary.extradays = controlextradays;
                return Ok(salary);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, "Data Not Found");
            }
        }
    }
}