using Servo_API.App_Start;
using Servo_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servo_API.Controllers
{
    public class CustomerController : ApiController
    {
        DbOperations_LATEST.DBUtil dbobj = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
        [HttpGet]
        [Route("api/CustomerController/GetCustomerType")]
        public IHttpActionResult GetCustomerType()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            List<string> DropType = new List<string>();
            SqlDtr = obj.GetRecordSet("select * from CustomerType order by CustomerTypeName");
            DropType.Add("Select");
            while (SqlDtr.Read())
            {
                DropType.Add(SqlDtr.GetValue(1).ToString());
            }
            SqlDtr.Close();
            return Ok(DropType);
        }

        [HttpGet]
        [Route("api/CustomerController/FetchSSREmployee")]
        public IHttpActionResult FetchSSREmployee()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            List<string> Employee = new List<string>();
            string sql = "select Emp_Name from Employee where Designation='Servo Sales Representative' order by Emp_Name";
            SqlDtr = obj.GetRecordSet(sql);
            Employee.Add("Select");
            while (SqlDtr.Read())
            {
                Employee.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            return Ok(Employee);
        }

        [HttpGet]
        [Route("api/CustomerController/GetNextID")]
        public IHttpActionResult GetNextID()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string customerID = "";
            #region Fetch Next Customer ID
            string sql = "select max(Cust_ID)+1 from Customer";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                customerID = SqlDtr.GetSqlValue(0).ToString();
                if (customerID == "Null")
                    customerID = "1001";
            }
            SqlDtr.Close();
            #endregion
            return Ok(customerID);
        }

        [HttpGet]
        [Route("api/CustomerController/GetCustID")]
        public IHttpActionResult GetCustID(string custName)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr = null;
            bool CustomerID = false;
            string sql = "select Cust_ID from Customer where Cust_Name='" + custName + "'";
            SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
            {
                CustomerID = true;
            }
            SqlDtr.Close();
            return Ok(CustomerID);
        }

        [HttpGet]
        [Route("api/CustomerController/GetLedger")]
        public IHttpActionResult GetLedger(string custName)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr = null;
            bool LedgerID = false;
            string sql = "select * from Ledger_Master where Ledger_Name='" + custName + "'";
            SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
            {
                LedgerID = true;
            }
            SqlDtr.Close();
            return Ok(LedgerID);
        }

        [HttpGet]
        [Route("api/CustomerController/GetTinNoExists")]
        public IHttpActionResult GetTinNoExists(string TinNo)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr = null;
            string CustomerID = "";
            string sql1 = "Select Tin_No,Cust_ID from customer where Tin_No = '" + TinNo + "' and Tin_No<>'unregister' and Tin_No<>'UNREGISTERED' and Tin_No<>'Un Register'";
            SqlDtr = obj.GetRecordSet(sql1);
            if (SqlDtr.HasRows)
            {
                if (SqlDtr.Read())
                {
                    CustomerID = SqlDtr["Cust_ID"].ToString();
                }

            }
            SqlDtr.Close();
            return Ok(CustomerID);
        }

        [HttpGet]
        [Route("api/CustomerController/GetEmpSSR")]
        public IHttpActionResult GetEmpSSR(string SSR)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr = null;
            string custSSR = "";
            SqlDtr = obj.GetRecordSet("select Emp_ID from Employee where Emp_Name='" + SSR + "'");
            if (SqlDtr.Read())
                custSSR = SqlDtr["Emp_ID"].ToString();
            else
                custSSR = "";
            SqlDtr.Close();
            return Ok(custSSR);
        }

        [HttpPost]
        [Route("api/CustomerController/InsertCustomer")]
        public IHttpActionResult InsertCustomer(CustomerModels customer)
        {
            InventoryClass obj = new InventoryClass();
            obj.InsertCustomer(customer);
            return Created(new Uri(Request.RequestUri + ""), "Customer created");
        }

        [HttpPost]
        [Route("api/CustomerController/GetCustomersData")]
        public IHttpActionResult GetCustomersData(CustomerModels customer)
        {
            InventoryClass obj = new InventoryClass();
            DataSet ds;
            ds = obj.ShowCustomerInfo(customer.CustomerID, customer.CustomerName, customer.Place);
            DataTable dt = ds.Tables[0];
            DataView dv = new DataView(dt);
            return Ok(dv);
        }
        [HttpGet]
        [Route("api/CustomerController/GetCount")]
        public IHttpActionResult GetCount(string Customer)
        {
            SqlDataReader rdr = null;
            int Count = 0;
            dbobj.SelectQuery("select count(*) from AccountsLedgerTable where Ledger_ID='" + Customer + "'", ref rdr);
            if (rdr.Read())
            {
                Count = int.Parse(rdr.GetValue(0).ToString());
            }
            return Ok(Count);
        }
        [HttpGet]
        [Route("api/CustomerController/DeleteCustomer")]
        public IHttpActionResult DeleteCustomer(string Customer)
        {
            SqlConnection sqlConn = new SqlConnection();
            string strCon = System.Configuration.ConfigurationSettings.AppSettings["Servosms"];
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "Delete from Customer Where Cust_ID='" + Customer + "'";
            sqlConn.ConnectionString = strCon;
            sqlConn.Open();
            sqlCmd.Connection = sqlConn;
            sqlCmd.ExecuteNonQuery();
            sqlConn.Close();
            sqlCmd.Dispose();
            return Ok("Customer Deleted");
        }
        [HttpGet]
        [Route("api/CustomerController/DeleteLedger")]
        public IHttpActionResult DeleteLedger(string Customer)
        {
            SqlConnection sqlConn = new SqlConnection();
            string strCon = System.Configuration.ConfigurationSettings.AppSettings["Servosms"];
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "Delete from Ledger_Master Where Ledger_ID='" + Customer + "'";
            sqlConn.ConnectionString = strCon;
            sqlConn.Open();
            sqlCmd.Connection = sqlConn;
            sqlCmd.ExecuteNonQuery();
            sqlConn.Close();
            sqlCmd.Dispose();
            return Ok("Deleted from Ledger_Master");
        }
        [HttpGet]
        [Route("api/CustomerController/FetchEmployeeName")]
        public IHttpActionResult FetchEmployeeName()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            List<string> DropSSRName = new List<string>();
            string sql = "select emp_name from employee where Designation='Servo Sales Representative' order by emp_name";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                DropSSRName.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            return Ok(DropSSRName);
        }
        [HttpGet]
        [Route("api/CustomerController/FetchAllCustomers")]
        public IHttpActionResult FetchAllCustomers()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql = "select Cust_Name from customer order by cust_name";
            SqlDtr = obj.GetRecordSet(sql);
            List<string> ListCustomer = new List<string>();
            while (SqlDtr.Read())
            {
                ListCustomer.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            return Ok(ListCustomer);
        }
        [HttpGet]
        [Route("api/CustomerController/FetchCustomerBySSR")]
        public IHttpActionResult FetchCustomerBySSR(string SSR)
        {
            InventoryClass obj = new InventoryClass();
            List<string> customers = new List<string>();
            SqlDataReader rdr = obj.GetRecordSet("select Cust_Name from Customer where ssr=(select Emp_ID from Employee where Emp_Name='" + SSR + "') order by Cust_Name");
            while (rdr.Read())
            {
                customers.Add(rdr["Cust_Name"].ToString());
            }
            rdr.Close();
            return Ok(customers);
        }
        [HttpPost]
        [Route("api/CustomerController/InsertorUpdateCustomerMapping")]
        public IHttpActionResult InsertorUpdateCustomerMapping(CustomerModels customer)
        {
            int x = 0;
            dbobj.Insert_or_Update("update Customer set ssr='" + customer.CustomerID + "' where Cust_Name='" + customer.CustomerName + "'", ref x);
            return Created(new Uri(Request.RequestUri + ""), "Customer Mapping Updated");
        }
    }
}
