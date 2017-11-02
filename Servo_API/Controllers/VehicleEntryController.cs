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
    public class VehicleEntryController : ApiController
    {
        static string FromDate = "", ToDate = "";
        App_Start.DbOperations_LATEST.DBUtil dbobj = new App_Start.DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);

        [HttpGet]
        [Route("api/Roles/GetNextRoleID")]
        public string GetNextRoleID()
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
                return roleID;
            }
            catch (Exception ex)
            {
                return roleID;
            }

        }

        [HttpGet]
        [Route("api/Roles/GetCheckRoleExists")]
        public int GetCheckRoleExists(string txtRoleName)
        {
            int count = 0;
            string roleID = string.Empty;
            try
            {
                dbobj.ExecuteScalar("select count(*) from Roles where Role_Name='" + txtRoleName.Trim() + "'", ref count);

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        [HttpGet]
        [Route("api/Roles/GetCheckRoleExistsUser_master")]
        public int GetCheckRoleExistsUser_master(string txtRoleName)
        {
            int count = 0;
            string roleID = string.Empty;
            try
            {
                dbobj.ExecuteScalar("select count(*) from User_master where Role_ID='" + txtRoleName + "'", ref count);

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        [HttpPost]
        [Route("api/Roles/DeleteRole")]
        public int DeleteRole(string txtRoleName)
        {
            int count = 0;
            string roleID = string.Empty;
            try
            {
                dbobj.Insert_or_Update("delete from roles where Role_Id='" + txtRoleName + "'", ref count);

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        [HttpPost]
        [Route("api/Roles/InsertRole")]
        public bool InsertRole(EmployeeClass obj)
        {
            SqlCommand cmd;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            //EmployeeClass obj1 = new EmployeeClass();
            try
            {
                obj.InsertRoles();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/Roles/UpdateRole")]
        public bool UpdateRole(EmployeeClass obj)
        {
            SqlCommand cmd;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            //EmployeeClass obj1 = new EmployeeClass();
            try
            {
                obj.UpdateRoles();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("api/Roles/FillDropRoleID")]
        public List<string> FillDropRoleID()
        {            
            List<string> dropRoleID = new List<string>();
            try
            {
                //DBOperations.DBUtil obj = new DBOperations.DBUtil();
                SqlDataReader SqlDtr = null;

                dbobj.SelectQuery("select Role_ID from Roles", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    dropRoleID.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();
                return dropRoleID;
            }
            catch (Exception ex)
            {
                return dropRoleID;
            }
        }

        [HttpGet]
        [Route("api/Roles/GetSelectedRoleIDData")]
        public RolesModel GetSelectedRoleIDData(string RoleID)
        {
            RolesModel role = new RolesModel();

            List<string> dropRoleID = new List<string>();
            try
            {
                //DBOperations.DBUtil obj = new DBOperations.DBUtil();
                SqlDataReader SqlDtr = null;

                dbobj.SelectQuery("select * from roles where Role_Id='" + RoleID + "'", ref SqlDtr);
                while (SqlDtr.Read())
                {
                    role.Role_Name = SqlDtr.GetValue(1).ToString();
                    role.Description = SqlDtr.GetValue(2).ToString();
                }
                return role;
            }
            catch (Exception ex)
            {
                return role;
            }
        }
    }
}
