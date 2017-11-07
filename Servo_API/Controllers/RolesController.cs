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
    public class RolesController : ApiController
    {
        static string FromDate = "", ToDate = "";
        App_Start.DbOperations_LATEST.DBUtil dbobj = new App_Start.DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);

        [HttpGet]
        [Route("api/Roles/GetNextRoleID")]
        public IHttpActionResult GetNextRoleID()
        {
            string roleID = string.Empty;
            try
            {

                #region Fetch Next Role ID
                string sql = "select max(Role_ID)+1 from Roles";
                SqlDataReader SqlDtr;

                EmployeeClass obj = new EmployeeClass();

                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    roleID = SqlDtr.GetSqlValue(0).ToString();
                }
                SqlDtr.Close();
                #endregion
                if (roleID == null )
                    return Content(HttpStatusCode.NotFound, "Failed to get Next Role ID.");

                return Ok(roleID);                
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to get Next Role ID.");
            }

        }

        [HttpGet]
        [Route("api/Roles/GetCheckRoleExists")]
        public IHttpActionResult GetCheckRoleExists(string txtRoleName)
        {
            int count = 0;
            string roleID = string.Empty;
            try
            {
                dbobj.ExecuteScalar("select count(*) from Roles where Role_Name='" + txtRoleName.Trim() + "'", ref count);

                return Ok(count);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to check existence of Role.");
            }
        }

        [HttpGet]
        [Route("api/Roles/GetCheckRoleExistsUser_master")]
        public IHttpActionResult GetCheckRoleExistsUser_master(string txtRoleName)
        {
            int count = 0;
            string roleID = string.Empty;
            try
            {
                dbobj.ExecuteScalar("select count(*) from User_master where Role_ID='" + txtRoleName + "'", ref count);

                return Ok(count);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to check existence of Role in User_master.");
            }
        }

        [HttpPost]
        [Route("api/Roles/DeleteRole")]
        public IHttpActionResult DeleteRole(string txtRoleName)
        {
            int count = 0;
            string roleID = string.Empty;
            try
            {
                dbobj.Insert_or_Update("delete from roles where Role_Id='" + txtRoleName + "'", ref count);

                return Ok(count);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to delete Role.");
            }
        }

        [HttpPost]
        [Route("api/Roles/InsertRole")]
        public IHttpActionResult InsertRole(EmployeeClass obj)
        {
            SqlCommand cmd;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            //EmployeeClass obj1 = new EmployeeClass();
            try
            {
                obj.InsertRoles();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to insert Role.");
            }
        }

        [HttpPost]
        [Route("api/Roles/UpdateRole")]
        public IHttpActionResult UpdateRole(EmployeeClass obj)
        {
            SqlCommand cmd;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            //EmployeeClass obj1 = new EmployeeClass();
            try
            {
                obj.UpdateRoles();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to update Role.");
            }
        }

        [HttpGet]
        [Route("api/Roles/FillDropRoleID")]
        public IHttpActionResult FillDropRoleID()
        {            
            List<string> dropRoleID = new List<string>();
            try
            {                
                SqlDataReader SqlDtr = null;

                dbobj.SelectQuery("select Role_ID from Roles", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    dropRoleID.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();
                return Ok(dropRoleID);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to get Role IDs.");
            }
        }

        [HttpGet]
        [Route("api/Roles/GetSelectedRoleIDData")]
        public IHttpActionResult GetSelectedRoleIDData(string RoleID)
        {
            RolesModel role = new RolesModel();

            List<string> dropRoleID = new List<string>();
            try
            {               
                SqlDataReader SqlDtr = null;

                dbobj.SelectQuery("select * from roles where Role_Id='" + RoleID + "'", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    role.Role_Name = SqlDtr.GetValue(1).ToString();
                    role.Description = SqlDtr.GetValue(2).ToString();
                }
                if (role == null)
                    return Content(HttpStatusCode.NotFound, "Failed to get Role ID related data.");

                return Ok(role);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Failed to get Role ID related data.");
            }
        }
    }
}
