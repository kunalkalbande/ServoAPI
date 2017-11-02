/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/

using System;
using System.Data ;
using System.Data.SqlClient ;
using System.Web;
using Servosms.Sysitem.Classes;

namespace Servosms.Sysitem.Classes
{
	/// <summary>
	/// Summary description for Employee.
	/// </summary>
	public class EmployeeClass
	{
		SqlConnection SqlCon;
		SqlCommand SqlCmd;
		SqlDataReader SqlDtRed;
		SqlDataAdapter SqlDtAdp;
		DataSet DS;
//		SqlCommand sqcomm;
//		SqlDataReader sqdr;

		#region Vars & Prop
		string _prodid;
		string _schname;
		string _schprodid;
		string _onevery;
		string _freepack;
		string _discount;
		string _dateto;
		string _datefrom;
		string _Emp_ID;
		int _Emp_ID1;
		string _Emp_Name; 
		string _Desig;
		string _Address;
		string _City;
		string _State;
		string _Country;
		string _Phone;
		string _Mobile;
		string _EMail;
		string _Salary;
		string _OT_Comp;
		string _Shift_ID;
		string _Att_Date;
		string _Status;
		string _OT_Date;
		string _OT_From;
		string _OT_To;
		string _Leave_ID;
		string _Date_From;
		string _Date_To;
		string _Dr_License_No;
	    string _Dr_LIC_No;
		string _Dr_License_validity;
	    string _Dr_LIC_validity;
		string _Vehicle_NO;
		string _OpBalance;
		string _BalType;
		string _Days;
		DateTime _Date_From1;
		DateTime _Date_To1;
		string _TempEmpName;

		public DateTime Date_From1
		{
			get
			{
				return _Date_From1;
			}
			set
			{
				_Date_From1=value;
			}
		}
		public string TempEmpName
		{
			get
			{
				return _TempEmpName;
			}
			set
			{
				_TempEmpName=value;
			}
		}
		public DateTime Date_To1
		{
			get
			{
				return _Date_To1;
			}
			set
			{
				_Date_To1=value;
			}
		}

		public string Days
		{
			get
			{
				return _Days;
			}
			set
			{
				_Days=value;
			}
		}

		string _Reason;
		string _Shift_Date;
		string _Shift_Name;
		string _isSanction;
		string _Role_ID;
		string _Role_Name;
		string _Description;
		string _User_ID;
		string _Login_Name;
		string _Password;
		string _User_Name;
		string _Module_ID;
		string _SubModule_ID;
		string _View_Flag;
		string _Add_Flag;
		string _Edit_Flag;
		string _Del_Flag;

		public int Emp_ID1
		{
			get
			{
				return _Emp_ID1;
			}
			set
			{
				_Emp_ID1=value;
			}
		}
	
		public string Del_Flag
		{
			get
			{
				return _Del_Flag;
			}
			set
			{
				_Del_Flag=value;
			}
		}
		public string Edit_Flag
		{
			get
			{
				return _Edit_Flag;
			}
			set
			{
				_Edit_Flag=value;
			}
		}
		public string Add_Flag
		{
			get
			{
				return _Add_Flag;
			}
			set
			{
				_Add_Flag=value;
			}
		}
		public string View_Flag
		{
			get
			{
				return _View_Flag;
			}
			set
			{
				_View_Flag=value;
			}
		}
		public string SubModule_ID
		{
			get
			{
				return _SubModule_ID;
			}
			set
			{
				_SubModule_ID=value;
			}
		}
		public string Module_ID
		{
			get
			{
				return _Module_ID;
			}
			set
			{
				_Module_ID=value;
			}
		}
		public string User_Name
		{
			get
			{
				return _User_Name;
			}
			set
			{
				_User_Name=value;
			}
		}
		public string Password
		{
			get
			{
				return _Password;
			}
			set
			{
				_Password=value;
			}
		}
		public string Login_Name
		{
			get
			{
				return _Login_Name;
			}
			set
			{
				_Login_Name=value;
			}
		}
		public string User_ID
		{
			get
			{
				return _User_ID;
			}
			set
			{
				_User_ID=value;
			}
		}
		public string Description
		{
			get
			{
				return _Description;
			}
			set
			{
				_Description=value;
			}
		}
		public string Role_Name
		{
			get
			{
				return _Role_Name;
			}
			set
			{
				_Role_Name=value;
			}
		}
		public string Role_ID
		{
			get
			{
				return _Role_ID;
			}
			set
			{
				_Role_ID=value;
			}
		}		
		public string isSanction
		{
			get
			{
				return _isSanction;
			}
			set
			{
				_isSanction=value;
			}
		}
		public string Emp_ID
		{
			get
			{
				return _Emp_ID;
			}
			set
			{
				_Emp_ID=value;
			}
		}
		public string Emp_Name
		{
			get
			{
				return _Emp_Name;
			}
			set
			{
				_Emp_Name=value;
			}
		}
		public string Designation
		{
			get
			{
				return _Desig;
			}
			set
			{
				_Desig=value;
			}
		}
		public string Address
		{
			get
			{
				return _Address;
			}
			set
			{
				_Address=value;
			}
		}
		public string City
		{
			get
			{
				return _City;
			}
			set
			{
				_City=value;
			}
		}
		public string State
		{
			get
			{
				return _State;
			}
			set
			{
				_State=value;
			}
		}
		public string Country
		{
			get
			{
				return _Country;
			}
			set
			{
				_Country=value;
			}
		}
		public string Phone
		{
			get
			{
				return _Phone;
			}
			set
			{
				_Phone=value;
			}
		}
		public string Mobile
		{
			get
			{
				return _Mobile;
			}
			set
			{
				_Mobile=value;
			}
		}
		public string EMail
		{
			get
			{
				return  _EMail;
			}
			set
			{
				 _EMail=value;
			}
		}
		public string Salary
		{
			get
			{
				return  _Salary;
			}
			set
			{
				_Salary=value;
			}
		}
		public string OT_Compensation
		{
			get
			{
				return  _OT_Comp;
			}
			set
			{
				_OT_Comp=value;
			}
		}
		public string Dr_License_No
		{
			get
			{
				return _Dr_License_No;
			}
			set
			{
				_Dr_License_No = value;
			}
		}
		
		public string Dr_LIC_No
		{
			get
			{
				return _Dr_LIC_No;
			}
			set
			{
				_Dr_LIC_No = value;
			}
		}

		public string Dr_License_validity
		{
			get
			{
				return _Dr_License_validity;
			}
			set
			{
				_Dr_License_validity = value;
			}
		}
		public string Dr_LIC_validity
		{
			get
			{
				return _Dr_LIC_validity;
			}
			set
			{
				_Dr_LIC_validity = value;
			}
		}
		public string OpBalance
		{
			get
			{
				return _OpBalance;
			}
			set
			{
				_OpBalance = value;
			}
		}
		public string BalType
		{
			get
			{
				return _BalType;
			}
			set
			{
				_BalType = value;
			}
		}
		public string Vehicle_NO
		{
			get
			{
				return _Vehicle_NO;
			}
			set
			{
				_Vehicle_NO = value;
			}
		}

		
		public string Shift_ID
		{
			get
			{
				return  _Shift_ID;
			}
			set
			{
				_Shift_ID=value;
			}
		}
		public string  Att_Date
		{
			get
			{
				return  _Att_Date;
			}
			set
			{
				_Att_Date=value;
			}
		}
		public string  Status
		{
			get
			{
				return  _Status;
			}
			set
			{
				_Status=value;
			}
		}
		public string OT_Date
		{
			get
			{
				return _OT_Date;
			}
			set
			{
				_OT_Date=value;
			}
		}
		
		public string OT_From
		{
			get
			{
				return _OT_From;
			}
			set
			{
				_OT_From=value;
			}
		}
		
		public string OT_To
		{
			get
			{
				return _OT_To;
			}
			set
			{
				_OT_To=value;
			}
		}
		public string Leave_ID
		{
			get
			{
				return _Leave_ID;
			}
			set
			{
				_Leave_ID=value;
			}
		}
		public string Date_From
		{
			get
			{
				return _Date_From;
			}
			set
			{
				_Date_From=value;
			}
		}
		public string Date_To
		{
			get
			{
				return _Date_To;
			}
			set
			{
				_Date_To=value;
			}
		}
		public string Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason=value;
			}
		}
		public string Shift_Date
		{
			get
			{
				return _Shift_Date ;
			}
			set
			{
				_Shift_Date =value;
			}
		}
		//*******
		public string prodid
		{
			get
			{
				return _prodid;
			}
			set
			{
				_prodid =value;
			}
		}
		public string schname
		{
			get
			{
				return _schname;
			}
			set
			{
				_schname =value;
			}
		}
		public string schprodid
		{
			get
			{
				return _schprodid;
			}
			set
			{
				_schprodid =value;
			}
		}
		public string onevery
		{
			get
			{
				return _onevery;
			}
			set
			{
				_onevery=value;
			}
		}
		public string freepack
		{
			get
			{
				return _freepack;
			}
			set
			{
				_freepack=value;
			}
		}
		public string discount
		{
			get
			{
				return _discount;
			}
			set
			{
				_discount=value;
			}
		}
		public string datefrom
		{
			get
			{
				return _datefrom;
			}
			set
			{
				_datefrom=value;
			}
		}
		public string dateto
		{
			get
			{
				return _dateto;
			}
			set
			{
				_dateto=value;
			}
		}
		//*******
		public string Shift_Name
		{
			get
			{
				return _Shift_Name ;
			}
			set
			{
				_Shift_Name =value;
			}
		}
		#endregion

		#region Constructor & Destructor .. opens the connection to the database server
		public EmployeeClass()
		{
			SqlCon =new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			SqlCon.Open();
		}
		~EmployeeClass()
		{

		}
		#endregion

		/// <summary>
		/// Method returns the SqlDataReader Object  by executing the passing query.
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public SqlDataReader GetRecordSet(string sql)
		{
			SqlCmd=new SqlCommand (sql,SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
			
		}

		/// <summary>
		/// Method execute the insert, update or delete query;
		/// </summary>
		/// <param name="Sql"></param>
		public void ExecRecord(string Sql)
		{
			SqlCmd=new SqlCommand(Sql,SqlCon);
			SqlCmd.ExecuteNonQuery();
		}

        /// <summary>
        /// This method is used to returns the current date.
        /// </summary>
        /// <returns></returns>
		public  string  date()
		{
			String dt = DateTime.Now.ToString();
			int pos = dt.IndexOf(" ");
			String strDate;
			strDate = dt.Substring(0, pos);            

			if (strDate.StartsWith("0"))
			{
				strDate = strDate.Substring(1);
			}
            
			return strDate;

			}

		/// <summary>
		/// This method is used to returns the SqlDataReader for containing next employee ID
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetNextEmployeeID()
		{
			SqlCmd=new SqlCommand ("select max(Emp_ID)+1 from Employee",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
		
		}

		/// <summary>
		/// This method is used to returns the SqlDataReader for containing next shift ID
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetNextShiftID()
		{
			SqlCmd=new SqlCommand ("select max(Shift_ID)+1 from Shift",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
					
		}

		/// <summary>
		/// This method is used to method returns the SqlDataReader for containing  employee ID
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetEmployeeID()
		{
			SqlCmd=new SqlCommand ("select Emp_ID from Employee",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// This method is used to calls procedure prodeleteEmployee to delete the employee
		/// and also delete the ledger from Ledger_Master table.
		/// </summary>
		public void deleteEmployee()
		{
			SqlCmd=new SqlCommand("proDeleteEmployee",SqlCon);
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@Emp_ID",Emp_ID);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// This method is used to calls procedure proEmployeeEntry to insert the employee
		/// and also create ledger in Ledger_Master table.
		/// </summary>
		public void InsertEmployee()
		 { 				
			SqlCmd=new SqlCommand("ProEmployeeEntry",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Emp_ID",Emp_ID );
			SqlCmd.Parameters .Add("@Emp_Name",Emp_Name );
			SqlCmd.Parameters .Add("@Desig",Designation  );
			SqlCmd.Parameters .Add("@Address",Address );
			SqlCmd.Parameters .Add("@City",City);
			SqlCmd.Parameters .Add("@State",State );
			SqlCmd.Parameters .Add("@Country",Country );
			SqlCmd.Parameters .Add("@Phone",Phone);
			SqlCmd.Parameters .Add("@Mobile",Mobile);
			SqlCmd.Parameters .Add("@EMail",EMail);
			SqlCmd.Parameters .Add("@Salary",Salary);
			SqlCmd.Parameters .Add("@OT_Comp",OT_Compensation );
			SqlCmd.Parameters .Add("@driver_lic_no",Dr_License_No  );
			SqlCmd.Parameters .Add("@validity",Dr_License_validity  );
			SqlCmd.Parameters .Add("@lic_policy_no",Dr_LIC_No );
			SqlCmd.Parameters .Add("@lic_validity",Dr_LIC_validity );
			SqlCmd.Parameters .Add("@vehicle_id",Vehicle_NO);
			SqlCmd.Parameters .Add("@OpBalance",OpBalance);
			SqlCmd.Parameters .Add("@BalType",BalType);
			SqlCmd.Parameters .Add("@Status",Status);                       //Add by vikas 27.10.2012
			SqlCmd.ExecuteNonQuery();
		 }

		/// <summary>
		/// This method is used to calls procedure proEmployeeUpdate to Update the employee
		/// and also update the employee ledger in Ledger_Master table.
		/// </summary>
		public void UpdateEmployee()
		{ 				
			SqlCmd=new SqlCommand("ProEmployeeUpdate",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Emp_ID",Emp_ID );
			SqlCmd.Parameters .Add("@Emp_Name",Emp_Name );
			SqlCmd.Parameters .Add("@TempEmpName",TempEmpName);
			SqlCmd.Parameters .Add("@Desig",Designation  );
			SqlCmd.Parameters .Add("@Address",Address );
			SqlCmd.Parameters .Add("@City",City);
			SqlCmd.Parameters .Add("@State",State );
			SqlCmd.Parameters .Add("@Country",Country );
			SqlCmd.Parameters .Add("@Phone",Phone);
			SqlCmd.Parameters .Add("@Mobile",Mobile);
			SqlCmd.Parameters .Add("@EMail",EMail);
			SqlCmd.Parameters .Add("@Salary",Salary);
			SqlCmd.Parameters .Add("@OT_Comp",OT_Compensation );
			SqlCmd.Parameters .Add("@driver_lic_no",Dr_License_No  );
			SqlCmd.Parameters .Add("@validity",Dr_License_validity  );
			SqlCmd.Parameters .Add("@lic_policy_no",Dr_LIC_No );
			SqlCmd.Parameters .Add("@lic_validity",Dr_LIC_validity );
			SqlCmd.Parameters .Add("@vehicle_id",Vehicle_NO);
			SqlCmd.Parameters .Add("@OpBalance",OpBalance);
			SqlCmd.Parameters .Add("@BalType",BalType);
			SqlCmd.Parameters .Add("@Status",Status);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Returns the Employee details for passing id, name and designation
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="name"></param>
		/// <param name="desig"></param>
		/// <returns></returns>
		public SqlDataReader EmployeeList(string ID, string name, string desig)
		{
			#region Query
			string sql;
			int wherestatus=0;
			sql="select * from Employee";

			if(ID!="")
			{
				sql=sql+" where Emp_ID=" + ID;
				wherestatus=1;
			}
			if(name!="")
			{
				if (wherestatus==0)
				{
					sql=sql+" where Emp_Name like('%" + name +"%')";
					wherestatus=1;
				}
				else
				{
					sql=sql+" and Emp_Name like('%" + name +"%')";
				}
			}
			if(desig!="")
			{
				if (wherestatus==0)
				{
					sql=sql+" where Designation='" + desig +"'";
					wherestatus=1;
				}
				else
				{
					sql=sql+" and Designation='" + desig +"'";
				}
			}

			#endregion

			SqlCmd=new SqlCommand (sql,SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;			
		}

		/// <summary>
		/// returns the employee information for passing id , name and designation.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="name"></param>
		/// <param name="desig"></param>
		/// <returns></returns>
		public DataSet ShowEmployeeInfo(string ID, string name, string desig)
		{			
			#region Query
			string sql;
			int wherestatus=0;
			//sql="select * from Employee";
			sql="select * from Employee e,Ledger_Master lm where lm.Ledger_Name=e.Emp_Name";
			if(ID!="")
			{
				//sql=sql+" where Emp_ID=" + ID;
				//sql=sql+" where Emp_ID like('" + ID +"%')";
				sql=sql+" and Emp_ID like('" + ID +"%')";
				wherestatus=1;
			}
			if(name!="" && desig=="")
			{  
				if (wherestatus==0)
				{
					//sql=sql+" where  Emp_Name like('"+name+"%')";
					sql=sql+" and  Emp_Name like('"+name+"%')";
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where Emp_Name like('%" + name +"%')";
					sql=sql+" and Emp_Name like('%" + name +"%')";
					wherestatus=1;
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where Emp_Name like('%" + name +"')";
					sql=sql+" and Emp_Name like('%" + name +"')";
					wherestatus=1;
				}
//				else 
//				{
//					//sql=sql+" and Emp_Name='" + name +"'";
//					sql=sql+" and Emp_Name=('%" + name +"%')";
//					
//				}
				
			}
			if(desig!="" && name=="" )
			{
				if (wherestatus==0)
				{
					//sql=sql+" where Designation like('" + desig +"%')";
					sql=sql+" and Designation like('" + desig +"%')";
					wherestatus=1;
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where Designation like('%" + desig +"')";
					sql=sql+" and Designation like('%" + desig +"')";
					wherestatus=1;
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where Designation like('%" + desig +"%')";
					sql=sql+" and Designation like('%" + desig +"%')";
					wherestatus=1;
				}
//				else
//				{
//					sql=sql+" and Designation=('%" + desig +"%')";
//				}
			}


			if(name!="" && desig!="" )
			 {
				if (wherestatus==0)
				{
					//sql=sql+" where Designation like('" + desig +"%') and  Emp_Name like('" + name +"%')";
					sql=sql+" and Designation like('" + desig +"%') and  Emp_Name like('" + name +"%')";
					wherestatus=1;
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where Designation like('%" + desig +"') and  Emp_Name like('%" + name +"')";
					sql=sql+" and Designation like('%" + desig +"') and  Emp_Name like('%" + name +"')";
					wherestatus=1;
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where Designation like('%" + desig +"%') and  Emp_Name like('%" + name +"%')";
					sql=sql+" and Designation like('%" + desig +"%') and  Emp_Name like('%" + name +"%')";
					wherestatus=1;
				}
//				else
//				{
//					sql=sql+" and Designation=('%" + desig +"%')";
//				}
			}
			#endregion

			SqlDtAdp = new SqlDataAdapter(sql,SqlCon );
			DS = new DataSet();			 
			SqlDtAdp.Fill(DS);
			return DS;		
		}

		/// <summary>
		/// Method calls procedure proEmpAttendanceEntry to insert employee attendance in attendance register.
		/// </summary>
		public void InsertEmployeeAttandance()
		{
			SqlCmd=new SqlCommand("ProEmpAttadanceEntry",SqlCon);
			SqlCmd.CommandType =CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@Att_Date",Att_Date.ToString());
			SqlCmd.Parameters.Add("@Emp_ID",Emp_ID.ToString());
			SqlCmd.Parameters.Add("@Status",Status.ToString() );
			SqlCmd.ExecuteNonQuery();			
		}

		/// <summary>
		/// Method calls procedure proEmpAttendanceEntry to update employee attendance in attendance register
		/// </summary>
		public void UpdateEmployeeAttandance()
		{
			SqlCmd=new SqlCommand("ProEmpAttadanceUpdate",SqlCon);
			SqlCmd.CommandType =CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@Att_Date",Att_Date.ToString());
			SqlCmd.Parameters.Add("@Emp_ID",Emp_ID.ToString());
			SqlCmd.Parameters.Add("@Status",Status.ToString() );
			SqlCmd.ExecuteNonQuery();
		}

		// Method calls procedure proInsertOverTimeRegister to insert the Overtime Details.
		public void InsertOverTimeRegister()
		{ 
			SqlCmd=new SqlCommand("proInsertOverTimeRegister",SqlCon);
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@Emp_ID",Emp_ID);
			SqlCmd.Parameters.Add("@OT_Date",OT_Date );
			SqlCmd.Parameters.Add("@OT_From",OT_From );
			SqlCmd.Parameters.Add("@OT_To",OT_To );
			SqlCmd.ExecuteNonQuery();          
		}

		/// <summary>
		/// Method calls procedure proLeaveEntry to insert the Leave _details
		/// </summary>
		public void InsertLeave()
		{ 				
			SqlCmd=new SqlCommand("ProLeaveEntry",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@Emp_Name",Emp_Name);
			SqlCmd.Parameters.Add("@DateFrom",Date_From );
			SqlCmd.Parameters.Add("@DateTo",Date_To );
			SqlCmd.Parameters.Add("@Days",Days );         //Add by vikas 17.11.2012
			SqlCmd.Parameters.Add("@Reason",Reason);
			SqlCmd.ExecuteNonQuery();
			
		}

		/// <summary>
		/// Method calls procedure ProLeaveUpdate to update the leave details
		/// </summary>
		public void UpdateLeave()
		{ 	
				SqlCmd=new SqlCommand("ProLeaveUpdate",SqlCon );
				SqlCmd.CommandType=CommandType.StoredProcedure;
				SqlCmd.Parameters.Add("@Emp_Name",Emp_Name);
				SqlCmd.Parameters.Add("@DateFrom",Date_From );
				SqlCmd.Parameters.Add("@DateTo",Date_To );
				SqlCmd.Parameters.Add("@Reason",Reason);
				SqlCmd.Parameters.Add("@isSanction",isSanction);
				SqlCmd.ExecuteNonQuery();
		}


		/// <summary>
		/// Method calls procedure proShiftAssignmentEntry to insert the Shift Assignment Entry.
		/// </summary>
		public void InsertShiftAssignment()
		{ 				
			SqlCmd=new SqlCommand("proShiftAssignmentEntry",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Shift_Date", Shift_Date);
			SqlCmd.Parameters .Add("@Shift_ID", Shift_ID);
			SqlCmd.Parameters .Add("@Emp_ID", Emp_ID);
			SqlCmd.ExecuteNonQuery();
		}
		
		//*****bhal start****
		/// <summary>
		/// Method calls procedure proSchemeDiscountEntry to insert the Scheme Discount Entry in database.
		/// </summary>
		public void InsertSchemediscount()
		{ 				
			SqlCmd=new SqlCommand("proSchemediscountEntry",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@prodid", prodid);
			SqlCmd.Parameters .Add("@schname", schname);
			SqlCmd.Parameters .Add("@schprodid", schprodid);
			SqlCmd.Parameters .Add("@onevery", onevery);
			SqlCmd.Parameters .Add("@freepack", freepack);
			SqlCmd.Parameters .Add("@discount", discount);
			SqlCmd.Parameters .Add("@datefrom", datefrom);
			SqlCmd.Parameters .Add("@dateto", dateto);
			SqlCmd.ExecuteNonQuery();
		}
		//********end***********
		
		/// <summary>
		/// Method calls procedure proRolesEntry to insert the Roles details
		/// </summary>
		public void InsertRoles()
		{ 				
			SqlCmd=new SqlCommand("ProRolesEntry",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Role_ID",Role_ID.Length>0?Int32.Parse(Role_ID):0);
			SqlCmd.Parameters .Add("@Role_Name",Role_Name  );
			SqlCmd.Parameters .Add("@Description",Description);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Method calls procedure proRolesUpdate to update the Roles Details
		/// </summary>
		public void UpdateRoles()
		{ 				
			SqlCmd=new SqlCommand("ProRolesUpdate",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Role_ID",Role_ID.Length>0?Int32.Parse(Role_ID):0);
			SqlCmd.Parameters .Add("@Role_Name",Role_Name);
			SqlCmd.Parameters .Add("@Description",Description);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Method calls procedure proPrivilegesEntry to insert the privileges of the user
		/// </summary>
		public void InsertPriveleges()
		{ 				
			SqlCmd=new SqlCommand("ProPrivilegesEntry",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Login_Name",Login_Name);
			SqlCmd.Parameters .Add("@Module_ID",Module_ID.Length>0?Int32.Parse(Module_ID):0);
			SqlCmd.Parameters .Add("@SubModule_ID",SubModule_ID.Length>0?Int32.Parse(SubModule_ID):0);
			SqlCmd.Parameters .Add("@View_Flag",View_Flag);
			SqlCmd.Parameters .Add("@Add_Flag",Add_Flag);
			SqlCmd.Parameters .Add("@Edit_Flag",Edit_Flag);
			SqlCmd.Parameters .Add("@Del_Flag",Del_Flag);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Method calls procedure proUserMasterEntry to insert the User Details
		/// </summary>
		public void InsertUserMaster()
		{ 				
			SqlCmd=new SqlCommand("ProUserMasterEntry",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@User_ID",User_ID.Length>0?Int32.Parse(User_ID):0);
			SqlCmd.Parameters .Add("@Login_Name",Login_Name);
			SqlCmd.Parameters .Add("@Password",Password);
			SqlCmd.Parameters .Add("@User_Name",User_Name);
			SqlCmd.Parameters .Add("@Role_Name",Role_Name);
			SqlCmd.ExecuteNonQuery();
			SqlCon.Close();
		}

		/// <summary>
		/// Method calls procedure proUserMasterUpdate to update the user details
		/// </summary>
		public void UpdateUserMaster()
		{ 				
			SqlCmd=new SqlCommand("ProUserMasterUpdate",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@User_ID",User_ID.Length>0?Int32.Parse(User_ID):0);
			SqlCmd.Parameters .Add("@Login_Name",Login_Name  );
			SqlCmd.Parameters .Add("@Password",Password);
			SqlCmd.Parameters .Add("@User_Name",User_Name);
			SqlCmd.Parameters .Add("@Role_Name",Role_Name);
			SqlCmd.ExecuteNonQuery();
			SqlCon.Close();
		}
	}
}
