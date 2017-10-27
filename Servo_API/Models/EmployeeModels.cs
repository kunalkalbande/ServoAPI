using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servo_API.Models
{
    public class EmployeeModels
    {
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
                _Date_From1 = value;
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
                _TempEmpName = value;
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
                _Date_To1 = value;
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
                _Days = value;
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
                _Emp_ID1 = value;
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
                _Del_Flag = value;
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
                _Edit_Flag = value;
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
                _Add_Flag = value;
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
                _View_Flag = value;
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
                _SubModule_ID = value;
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
                _Module_ID = value;
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
                _User_Name = value;
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
                _Password = value;
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
                _Login_Name = value;
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
                _User_ID = value;
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
                _Description = value;
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
                _Role_Name = value;
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
                _Role_ID = value;
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
                _isSanction = value;
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
                _Emp_ID = value;
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
                _Emp_Name = value;
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
                _Desig = value;
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
                _Address = value;
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
                _City = value;
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
                _State = value;
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
                _Country = value;
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
                _Phone = value;
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
                _Mobile = value;
            }
        }
        public string EMail
        {
            get
            {
                return _EMail;
            }
            set
            {
                _EMail = value;
            }
        }
        public string Salary
        {
            get
            {
                return _Salary;
            }
            set
            {
                _Salary = value;
            }
        }
        public string OT_Compensation
        {
            get
            {
                return _OT_Comp;
            }
            set
            {
                _OT_Comp = value;
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
                return _Shift_ID;
            }
            set
            {
                _Shift_ID = value;
            }
        }
        public string Att_Date
        {
            get
            {
                return _Att_Date;
            }
            set
            {
                _Att_Date = value;
            }
        }
        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
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
                _OT_Date = value;
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
                _OT_From = value;
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
                _OT_To = value;
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
                _Leave_ID = value;
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
                _Date_From = value;
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
                _Date_To = value;
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
                _Reason = value;
            }
        }
        public string Shift_Date
        {
            get
            {
                return _Shift_Date;
            }
            set
            {
                _Shift_Date = value;
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
                _prodid = value;
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
                _schname = value;
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
                _schprodid = value;
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
                _onevery = value;
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
                _freepack = value;
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
                _discount = value;
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
                _datefrom = value;
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
                _dateto = value;
            }
        }
        //*******
        public string Shift_Name
        {
            get
            {
                return _Shift_Name;
            }
            set
            {
                _Shift_Name = value;
            }
        }
        #endregion
    }
}