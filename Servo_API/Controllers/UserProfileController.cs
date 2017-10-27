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
    public class UserProfileController : ApiController
    {
        DbOperations_LATEST.DBUtil dbobj = new DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
        [HttpGet]
        [Route("api/userprofile/GetRoles")]
        public List<string> GetRoles()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr = null;
            string sql;
            List<string> roles = new List<string>();
            #region Fetch Roles from Database and Add in the ComboBox
            sql = "select Role_Name from Roles";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                roles.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            #endregion
            return roles;
        }
        [HttpGet]
        [Route("api/userprofile/GetNextUserID")]
        public string GetNextUserID()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            string userID = null;
            #region Fetch Next User ID
            sql = "select max(UserID)+1 from User_Master";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                userID = SqlDtr.GetSqlValue(0).ToString();
                if (userID == null)
                    userID = "1001";
            }
            SqlDtr.Close();
            #endregion
            return userID;
        }
        [HttpGet]
        [Route("api/userprofile/GetUsers")]
        public List<string> GetUsers()
        {
            DbOperations_LATEST.DBUtil obj = new DbOperations_LATEST.DBUtil();
            SqlDataReader SqlDtr = null;
            List<string> Users = new List<string>();
            obj.SelectQuery("select UserID from User_Master", ref SqlDtr);
            while (SqlDtr.Read())
            {
                Users.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            return Users;
        }
        [HttpGet]
        [Route("api/userprofile/GetSelectedUser")]
        public UserModels GetSelectedUser(string userID)
        {
            DbOperations_LATEST.DBUtil obj = new DbOperations_LATEST.DBUtil();
            SqlDataReader SqlDtr = null;
            UserModels user = new UserModels();
            obj.SelectQuery("select loginname, password, username,role_name from user_master um, roles r where um.role_id=r.role_id and UserId='" + userID + "'", ref SqlDtr);
            while (SqlDtr.Read())
            {
                user.LoginName = SqlDtr.GetValue(0).ToString();
                user.Password = SqlDtr.GetValue(1).ToString();
                user.UserName = SqlDtr.GetValue(2).ToString();
                user.RoleName = SqlDtr.GetValue(3).ToString();
            }
            SqlDtr.Close();
            return user;
        }
        [HttpGet]
        [Route("api/userprofile/CheckUser")]
        public int CheckUser(string loginName)
        {
            InventoryClass obj = new InventoryClass();
            int x = 0;
            dbobj.ExecuteScalar("select count(*) from user_master where loginname='" + loginName + "'", ref x);
            return x;
        }
        [HttpGet]
        [Route("api/userprofile/GetUserID")]
        public string GetUserID(string loginName)
        {
            SqlDataReader SqlDtr = null;
            string UserId = "";
            dbobj.SelectQuery("Select UserID from user_master where loginname='" + loginName + "'", ref SqlDtr);
            if (SqlDtr.Read())
            {
                UserId = SqlDtr.GetValue(0).ToString().Trim();
            }
            SqlDtr.Close();
            return UserId;
        }

        [HttpPost]
        [Route("api/userprofile/UpdateUser")]
        public void UpdateUser(UserModels User)
        {
            InventoryClass obj = new InventoryClass();
            obj.UpdateUserMaster(User);
        }

        [HttpPost]
        [Route("api/userprofile/InsertUser")]
        public void InsertUser(UserModels User)
        {
            InventoryClass obj = new InventoryClass();
            obj.InsertUserMaster(User);
        }

        [HttpGet]
        [Route("api/userprofile/DeleteUser")]
        public void DeleteUser(string UserID)
        {
            int output = 0;
            DbOperations_LATEST.DBUtil obj = new DbOperations_LATEST.DBUtil();
            obj.Insert_or_Update("delete from User_Master where UserID='" + UserID + "'", ref output);
            obj.Insert_or_Update("delete from privileges where User_ID='" + UserID + "'", ref output);

        }
    }
}
