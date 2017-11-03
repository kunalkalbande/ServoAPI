using Servo_API.App_Start;
using Servo_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servo_API.Controllers
{
    public class BeatMasterEntryController : ApiController
    {
        [HttpGet]
        [Route("api/BeatMasterEntryController/GetID")]
        public IHttpActionResult GetID()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string ID = "";
            SqlDtr = obj.GetRecordSet("select max(Beat_No)+1 from Beat_Master");
            while (SqlDtr.Read())
            {
                ID = SqlDtr.GetValue(0).ToString();
                if (ID == "")
                    ID = "1001";
            }
            return Ok(ID);

        }
        [HttpGet]
        [Route("api/BeatMasterEntryController/GetAllBeatIDs")]
        public IHttpActionResult GetAllBeatIDs()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            List<string> BeatIds = new List<string>();
            SqlDtr = obj.GetRecordSet("select Beat_No,city from Beat_Master order by city");
            BeatIds.Add("Select");
            while (SqlDtr.Read())
            {
                BeatIds.Add(SqlDtr.GetValue(0).ToString() + ':' + SqlDtr.GetValue(1).ToString());
            }
            SqlDtr.Close();
            return Ok(BeatIds);
        }
        [HttpGet]
        [Route("api/BeatMasterEntryController/FetchCity")]
        public IHttpActionResult FetchCity(string City)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            int flag = 0;
            sql = "select City  from Beat_Master where City='" + City + "'";
            SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.Read())
            {
                flag = 1;
            }
            return Ok(flag);
        }
        [HttpPost]
        [Route("api/BeatMasterEntryController/InsertBeatMaster")]
        public IHttpActionResult InsertBeatMaster(BeatMasterModel BeatMaster)
        {
            InventoryClass obj = new InventoryClass();
            obj.InsertBeatMaster(BeatMaster);
            return Created(new Uri(Request.RequestUri + ""), "Beat Master entry created");
        }
        [HttpGet]
        [Route("api/BeatMasterEntryController/GetSelectedBeat")]
        public IHttpActionResult GetSelectedBeat(string BeatNo)
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            BeatMasterModel beat = new BeatMasterModel();
            sql = "Select * from Beat_Master where Beat_No='" + BeatNo + "'";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                beat.City = SqlDtr.GetValue(1).ToString();
                beat.State = SqlDtr.GetValue(2).ToString();
                beat.Country = SqlDtr.GetValue(3).ToString();
            }
            SqlDtr.Close();
            return Ok(beat);
        }
        [HttpGet]
        [Route("api/BeatMasterEntryController/DeleteBeat")]
        public IHttpActionResult DeleteBeat(string BeatNo)
        {
            InventoryClass obj = new InventoryClass();
            obj.DeleteBeatMaster(BeatNo);
            return Created(new Uri(Request.RequestUri + ""), "Beat Master Deleted");
        }
        [HttpPost]
        [Route("api/BeatMasterEntryController/UpdateBeat")]
        public IHttpActionResult UpdateBeat(BeatMasterModel beat)
        {
            InventoryClass obj = new InventoryClass();
            obj.UpdateBeatMaster(beat);
            return Created(new Uri(Request.RequestUri + ""), "Beat Master Updated");
        }
    }
}
    
