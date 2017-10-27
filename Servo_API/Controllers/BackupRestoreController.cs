using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servo_API.Controllers
{
    public class BackupRestoreController : ApiController
    {
        [HttpGet]
        [Route("api/BackupRestoreController/Backup")]
        public void Backup()
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            con.Open();
            SqlCommand cmd = new SqlCommand("BACKUP DATABASE [Servosms] TO  DISK = N'C:\\ServosmsBackup\\Son\\Servosms.bak' WITH NOFORMAT, INIT,  NAME = N'Servosms-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        [HttpGet]
        [Route("api/BackupRestoreController/BackupDB")]
        public void BackupDB()
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            con.Open();
            SqlCommand cmd = new SqlCommand("BACKUP DATABASE [Servosms] TO  DISK = N'C:\\ServosmsBackup\\Son\\Servosms.bak' WITH NOFORMAT, INIT,  NAME = N'Servosms-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10", con);
            cmd.CommandTimeout = 1000;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        [HttpGet]
        [Route("api/BackupRestoreController/Restore")]
        public void Restore(string FilePath)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Master"]);
            con.Open();
            SqlCommand cmd = new SqlCommand("Alter DATABASE Servosms SET SINGLE_USER WITH ROLLBACK IMMEDIATE", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            con.Open();
            cmd = new SqlCommand("RESTORE DATABASE [Servosms] FROM  DISK = '" + FilePath + "' WITH  FILE = 1,REPLACE", con);
            //cmd = new SqlCommand("RESTORE DATABASE [Servosms] FROM  DISK = 'C:\\ServosmsBackup\\Son\\Servosms.bak' WITH  FILE = 1,REPLACE",con);
            //cmd = new SqlCommand("RESTORE DATABASE Servosms FROM DISK = 'c:\\Servosms.bak' WITH FILE = 1, RECOVERY, REPLACE;",con);
            cmd.CommandTimeout = 1000;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            con.Open();
            cmd = new SqlCommand("Alter DATABASE Servosms SET MULTI_USER", con);
            cmd.ExecuteNonQuery();
        }
    }
}
