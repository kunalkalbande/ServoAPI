using Servo_API.Models;
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
    public class LedgerController : ApiController
    {
        App_Start.DbOperations_LATEST.DBUtil dbobj = new App_Start.DbOperations_LATEST.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
        [HttpGet]
        [Route("api/LedgerController/GetSubGroup")]
        public List<string> GetSubGroup()
        {
            SqlDataReader SqlDtr = null;
            List<string> subGroups = new List<string>();
            dbobj.SelectQuery("select distinct sub_grp_name from Ledger_Master_Sub_Grp", ref SqlDtr);
            while (SqlDtr.Read())
            {
                subGroups.Add(SqlDtr["sub_grp_name"].ToString());
            }
            SqlDtr.Close();
            return subGroups;
        }

        [HttpPost]
        [Route("api/LedgerController/GetGroup")]
        public string GetGroup(List<string> SubGroupName)
        {
            SqlDataReader SqlDtr = null;
            //string str = "";
            string s = "";
            if (SubGroupName != null)
            {
                foreach (var str in SubGroupName)
                {
                    if (!str.Trim().Equals("Other"))
                    {
                        dbobj.SelectQuery("select gr.Grp_Name,lmsg.Nature_Of_Group from Ledger_Master_Sub_Grp lmsg,mgroup gr where lmsg.sub_grp_name = '" + str.Trim() + "' and lmsg.Grp_ID = gr.Grp_ID", ref SqlDtr);
                        while (SqlDtr.Read())
                        {
                            s = s + str.Trim() + "~" + SqlDtr["Grp_Name"].ToString() + "~" + SqlDtr["Nature_Of_Group"].ToString() + "#";
                        }
                    }
                }
            }
            return s;

        }
        [HttpGet]
        [Route("api/LedgerController/GetDistinctGroupName")]
        public string GetDistinctGroupName()
        {
            SqlDataReader SqlDtr = null;
            string s1 = "";
            dbobj.SelectQuery("select distinct gr.Grp_Name from Ledger_Master_Sub_Grp lmsg,mgroup gr where  lmsg.Grp_ID = gr.Grp_ID", ref SqlDtr);
            while (SqlDtr.Read())
            {
                s1 = s1 + SqlDtr["Grp_name"].ToString() + "~";
            }
            SqlDtr.Close();
            return s1;
        }
        [HttpGet]
        [Route("api/LedgerController/GetParties")]
        public object[] GetParties()
        {
            ArrayList al = new ArrayList();
            SqlDataReader SqlDtr = null;
            dbobj.SelectQuery("Select Cust_Name from  Customer order by Cust_Name", ref SqlDtr);
            while (SqlDtr.Read())
            {
                al.Add(SqlDtr["Cust_Name"].ToString());
            }
            SqlDtr.Close();

            dbobj.SelectQuery("Select Supp_Name from  Supplier order by Supp_Name", ref SqlDtr);
            while (SqlDtr.Read())
            {
                al.Add(SqlDtr["Supp_Name"].ToString());
            }
            SqlDtr.Close();
            return al.ToArray();
        }
        [HttpGet]
        [Route("api/LedgerController/FetchGroup")]
        public List<string> FetchGroup(string SubGroupName)
        {
            SqlDataReader SqlDtr = null;
            List<string> Groups = new List<string>();
            if (SubGroupName != "Other")
            {
                dbobj.SelectQuery("select gr.Grp_Name from Ledger_Master_Sub_Grp lmsg,mgroup gr where lmsg.sub_grp_name = '" + SubGroupName + "' and lmsg.Grp_ID = gr.Grp_ID", ref SqlDtr);
            }
            else
            {
                dbobj.SelectQuery("select distinct gr.Grp_Name from Ledger_Master_Sub_Grp lmsg,mgroup gr where  lmsg.Grp_ID = gr.Grp_ID", ref SqlDtr);
            }
            while (SqlDtr.Read())
            {
                Groups.Add(SqlDtr["Grp_Name"].ToString());
            }
            SqlDtr.Close();
            return Groups;
        }
        [HttpGet]
        [Route("api/LedgerController/CheckAcc_Period")]
        public bool CheckAcc_Period()
        {
            SqlDataReader SqlDtr = null;
            int c = 0;
            dbobj.SelectQuery("Select count(Acc_Date_From) from Organisation", ref SqlDtr);
            if (SqlDtr.Read())
            {
                c = System.Convert.ToInt32(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();

            if (c > 0)
                return true;
            else
                return false;
        }
        [HttpGet]
        [Route("api/LedgerController/FetchAllLedgers")]
        public List<string> FetchAllLedgers()
        {
            SqlDataReader SqlDtr = null;
            List<string> ledgers = new List<string>();
            dbobj.SelectQuery("Select Ledger_name+':'+cast(Ledger_ID as varchar) from Ledger_Master order by Ledger_Name", ref SqlDtr);
            while (SqlDtr.Read())
            {
                ledgers.Add(SqlDtr.GetValue(0).ToString());
            }
            SqlDtr.Close();
            return ledgers;
        }

        [HttpGet]
        [Route("api/LedgerController/FetchSelectedLedger")]
        public LedgerModels FetchSelectedLedger(string ledgerName)
        {
            LedgerModels ledger = new LedgerModels();
            SqlDataReader SqlDtr = null;
            string[] strArr = ledgerName.Split(new char[] { ':' }, ledgerName.Length);
            dbobj.SelectQuery("select ln.*,m.grp_name,lmsg.Sub_grp_Name,lmsg.Nature_of_group from Ledger_Master ln,mGroup m,Ledger_Master_Sub_Grp lmsg where Ledger_id = " + strArr[1].Trim() + " and Ledger_Name = '" + strArr[0].Trim() + "' and ln.sub_grp_id = lmsg.sub_grp_id and lmsg.grp_id = m.grp_id", ref SqlDtr);
            if (SqlDtr.Read())
            {
                ledger.LedgerName = SqlDtr["Ledger_name"].ToString();
                ledger.SubGroupName = SqlDtr["Sub_grp_Name"].ToString();
                ledger.GroupName = SqlDtr["grp_Name"].ToString();
                ledger.GroupNature = SqlDtr["Nature_of_group"].ToString();
                ledger.OpeningBalance = SqlDtr["Op_Balance"].ToString();
                ledger.BalanceType = SqlDtr["Bal_Type"].ToString();
            }
            SqlDtr.Read();
            return ledger;
        }
        [HttpGet]
        [Route("api/LedgerController/CheckLedgerExist")]
        public bool CheckLedgerExist(string ledger)
        {
            SqlDataReader SqlDtr = null;
            dbobj.SelectQuery("select * from ledger_master where ledger_name='" + ledger + "'", ref SqlDtr);
            if (SqlDtr.HasRows)
            {
                return true;
            }
            else
                return false;
        }
        [HttpGet]
        [Route("api/LedgerController/GetLedgersCount")]
        public string GetLedgersCount()
        {
            SqlDataReader SqlDtr = null;
            string count = "";
            dbobj.SelectQuery("select count(*) from ledger_master where sub_grp_id=118", ref SqlDtr);
            if (SqlDtr.Read())
            {
                count = SqlDtr.GetValue(0).ToString();
            }
            SqlDtr.Close();
            return count;
        }
        [HttpPost]
        [Route("api/LedgerController/GetSubGroupID")]
        public int GetSubGroupID(LedgerModels ledger)
        {
            SqlDataReader SqlDtr = null;
            int subgrpid = 0;
            string message = "";
            dbobj.SelectQuery("select sub_grp_Id from Ledger_Master_Sub_grp  where sub_grp_name = '" + ledger.SubGroupName + "' and Nature_Of_group ='" + ledger.GroupNature + "' and grp_id = (select top 1 grp_id from mgroup where grp_name = '" + ledger.GroupName + "')", ref SqlDtr);
            if (SqlDtr.Read())
            {
                subgrpid = System.Convert.ToInt32(SqlDtr["sub_grp_id"].ToString());
            }
            SqlDtr.Close();
            int count = 0;

            // check the Ledger Name is already present for the selected sub group
            dbobj.ExecuteScalar("Select count(Ledger_ID) from Ledger_Master where Ledger_Name = '" + ledger.LedgerName + "' and Sub_grp_ID = " + subgrpid, ref count);

            if (count > 0)
            {
                message = "Ledger Name is already exist for selected Sub Group ";
            }
            else
            {
                object op = null;
                dbobj.ExecProc(App_Start.DbOperations_LATEST.OprType.Insert, "ProInsertLedger", ref op, "@Ledger_Name", ledger.LedgerName, "@SubGrp_Name", ledger.SubGroupName, "@Group_Name", ledger.GroupName, "@Grp_Nature", ledger.GroupNature, "@Op_Bal", ledger.OpeningBalance, "@Bal_Type", ledger.BalanceType);
            }

            return count;
        }
        [HttpPost]
        [Route("api/LedgerController/UpdateLedger")]
        public string UpdateLedger(LedgerModels ledger)
        {
            SqlDataReader SqlDtr = null;
            int subgrpid = 0;
            string message = "";string id = null;
            dbobj.SelectQuery("select sub_grp_Id from Ledger_Master_Sub_grp  where sub_grp_name = '" + ledger.SubGroupName + "' and Nature_Of_group ='" + ledger.GroupNature + "' and grp_id = (select top 1 grp_id from mgroup where grp_name = '" + ledger.GroupName + "')", ref SqlDtr);
            if (SqlDtr.Read())
            {
                subgrpid = System.Convert.ToInt32(SqlDtr["sub_grp_id"].ToString());
            }
            SqlDtr.Close();

            int count = 0;
            dbobj.ExecuteScalar("Select count(Ledger_ID) from Ledger_Master where Ledger_Name = '" + ledger.LedgerName + "' and Sub_grp_ID = " + subgrpid, ref count);
            if (count > 0)
            {
                dbobj.SelectQuery("Select Ledger_ID from Ledger_Master where Ledger_Name = '" + ledger.LedgerName + "' and Sub_grp_ID = " + subgrpid, ref SqlDtr);
                if (SqlDtr.Read())
                {
                    id = SqlDtr["Ledger_ID"].ToString();
                }
                SqlDtr.Close();
                //if (!id.Equals(strArr[1]))
                //{
                //    MessageBox.Show("Ledger Name is already exist for selected Sub Group ");
                //    fetchGroup();
                //    DropGroup.SelectedIndex = DropGroup.Items.IndexOf(DropGroup.Items.FindByText(Group));
                //    return;
                //}
            }
            string subgrpid1 = "";
            dbobj.SelectQuery("select sub_grp_Id from Ledger_Master where Ledger_id = " + ledger.LedgerID, ref SqlDtr);
            while (SqlDtr.Read())
            {
                subgrpid1 = SqlDtr["sub_grp_ID"].ToString();
            }
            SqlDtr.Close();
            object op = null;
            // Call Procedure to update the Ledger
            dbobj.ExecProc(App_Start.DbOperations_LATEST.OprType.Update, "ProUpdateLedger", ref op, "@Ledger_ID", ledger.LedgerID, "@Ledger_Name", ledger.LedgerName, "@SubGrp_Name", ledger.SubGroupName, "@Group_Name", ledger.GroupName, "@Grp_Nature", ledger.GroupNature, "@Op_Bal", ledger.OpeningBalance, "@Bal_Type", ledger.BalanceType);
            dbobj.ExecProc(App_Start.DbOperations_LATEST.OprType.Update, "UpdateAccountsLedger", ref op, "@Ledger_ID", ledger.LedgerID, "@Amount", ledger.OpeningBalance, "@Type", ledger.BalanceType);
            // Procedure to update or delete the Unused newly created Group and Sub Groups.
            dbobj.ExecProc(App_Start.DbOperations_LATEST.OprType.Update, "ProUpdatesubgroup", ref op, "@subgrp_id", subgrpid1);

            return id;
        }
        [HttpGet]
        [Route("api/LedgerController/DeleteLedger")]
        public string DeleteLedger(string LedgerID)
        {
            SqlDataReader SqlDtr = null;
            string id1 = "";
            string id2 = "";
            string message = null;
            
            dbobj.SelectQuery("Select Ledg_ID_Cr, Ledg_ID_Dr from Voucher_Transaction where Ledg_ID_Cr = " + LedgerID + " or Ledg_ID_Dr = " + LedgerID, ref SqlDtr);
            while (SqlDtr.Read())
            {
                id1 = SqlDtr["Ledg_ID_Cr"].ToString();
                id2 = SqlDtr["Ledg_ID_Dr"].ToString();
            }
            SqlDtr.Close();
            if (!id1.Trim().Equals("") || !id2.Trim().Equals(""))
            {
                message = "Unable to delete Ledger ";
                return message;
            }
            else
            {
                int c = 0, Count = 0;
                string subgrpid = "";
                dbobj.SelectQuery("select sub_grp_Id from Ledger_Master where Ledger_id = " + LedgerID, ref SqlDtr);
                while (SqlDtr.Read())
                {
                    subgrpid = SqlDtr["sub_grp_ID"].ToString();
                }
                SqlDtr.Close();

                dbobj.ExecuteScalar("select count(*) from AccountsLedgerTable where Ledger_id = '" + LedgerID + "' and particulars<>'Opening balance'", ref Count);
                if (Count > 0)
                {
                    message = "Please Remove The All Transaction Concerning Ledger";
                    return message;
                }

                dbobj.Insert_or_Update("delete from Ledger_master where Ledger_Id = " + LedgerID, ref c);

                object op = null;
                dbobj.ExecProc(App_Start.DbOperations_LATEST.OprType.Update, "ProUpdatesubgroup", ref op, "@subgrp_id", subgrpid);
            }
            return message;
        }

    }
}
