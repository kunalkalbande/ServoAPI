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
    public class UserController : ApiController
    {
        private SqlConnection SqlCon;

        public IEnumerable<string> Get()
        {
            EmployeeModels obj = new EmployeeModels();
            List<string> users = new List<string>();
            using (SqlCon = new SqlConnection())
            {
                SqlCon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ServoSMS"]);
                SqlCon.Open();
                string sql = "select LoginName from User_Master";
                var SqlCmd = new SqlCommand(sql, SqlCon);
                var SqlDtr = SqlCmd.ExecuteReader();

                while (SqlDtr.Read())
                {
                    users.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();
                return users;
            }
        }
        // GET api/user/{id}
        public UserModels Get(string id)
        {
            UserModels user = new UserModels();
            string sql = "";
            using (SqlCon = new SqlConnection())
            {
                SqlCon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ServoSMS"]);
                SqlCon.Open();
                sql = "select UserID,UserName from User_Master where LoginName='" + id + "'";
                SqlCommand SqlCmd = new SqlCommand(sql, SqlCon);
                SqlDataReader SqlDtr = SqlCmd.ExecuteReader();
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
}
