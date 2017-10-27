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
    public class PriviligesController : ApiController
    {
        public SqlConnection SqlCon { get; private set; }
        [HttpGet]
        [Route("api/PriviligesController/GetPriviliges")]
        public IEnumerable<PriviligesModel> GetPriviliges(string id)
        {
            PriviligesModel privilige = new PriviligesModel();
            List<PriviligesModel> priviliges = new List<PriviligesModel>();
            using (SqlCon = new SqlConnection())
            {
                SqlCon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ServoSMS"]);
                SqlCon.Open();
                string sql = "select * from privileges where User_ID='" + id + "'";
                SqlCommand SqlCmd1 = new SqlCommand(sql, SqlCon);
                SqlDataReader SqlDtr1 = SqlCmd1.ExecuteReader();
                while (SqlDtr1.Read())
                {
                    privilige.User_ID = SqlDtr1.GetValue(0).ToString();
                    privilige.Module_ID = SqlDtr1.GetValue(1).ToString();
                    privilige.SubModule_ID = SqlDtr1.GetValue(2).ToString();
                    privilige.ViewFlag = SqlDtr1.GetValue(3).ToString();
                    privilige.Add_Flag = SqlDtr1.GetValue(4).ToString();
                    privilige.Edit_Flag = SqlDtr1.GetValue(5).ToString();
                    privilige.Del_Flag = SqlDtr1.GetValue(6).ToString();
                    priviliges.Add(privilige);
                    privilige = new PriviligesModel();
                }
                SqlDtr1.Close();
                return priviliges;
            }

        }
        [HttpPost]
        [Route("api/PriviligesController/InsertPriviliges")]
        public void InsertPriviliges(List<PriviligesModel> priviliges)
        {
            foreach (var privilige in priviliges)
            {
                using (SqlCon = new SqlConnection())
                {
                    SqlCon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ServoSMS"]);
                    SqlCon.Open();
                    SqlCommand SqlCmd;
                    SqlCmd = new SqlCommand("ProPrivilegesEntry", SqlCon);
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlCmd.Parameters.Add("@Login_Name", privilige.Login_Name);
                    SqlCmd.Parameters.Add("@Module_ID", privilige.Module_ID.Length > 0 ? Int32.Parse(privilige.Module_ID) : 0);
                    SqlCmd.Parameters.Add("@SubModule_ID", privilige.SubModule_ID.Length > 0 ? Int32.Parse(privilige.SubModule_ID) : 0);
                    SqlCmd.Parameters.Add("@View_Flag", privilige.ViewFlag);
                    SqlCmd.Parameters.Add("@Add_Flag", privilige.Add_Flag);
                    SqlCmd.Parameters.Add("@Edit_Flag", privilige.Edit_Flag);
                    SqlCmd.Parameters.Add("@Del_Flag", privilige.Del_Flag);
                    SqlCmd.ExecuteNonQuery();
                }
            }
        }
        [HttpGet]
        [Route("api/PriviligesController/GetAllUsers")]
        public IEnumerable<string> GetAllUsers()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            List<string> Users = new List<string>();
            #region Fetch All Users Information
            sql = "select LoginName from User_Master";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                Users.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            #endregion
            return Users;
        }

        [HttpGet]
        [Route("api/PriviligesController/GetSelectedUser")]
        public UserModels GetSelectedUser(string UserID)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            UserModels user = new UserModels();
            sql = "select UserID,UserName from User_Master where LoginName='" + UserID + "'";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                user.UserID = SqlDtr.GetValue(0).ToString();
                user.UserName = SqlDtr.GetValue(1).ToString();
            }
            SqlDtr.Close();
            return user;
        }
    }
}
