using Servo_API.Models;
using Servosms.Sysitem.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Servo_API.App_Start
{
    public class InventoryClass
    {
        SqlConnection SqlCon;
        SqlConnection SqlCon1;
        SqlCommand SqlCmd;
        SqlDataReader SqlDtr;
        SqlDataAdapter da;
        DataSet ds;

        #region Vars & Prop
        string _scheme;
        string _sch;
        string _schdiscount;
        string _balancetype;
        string _balance;
        string _custType;
        string _custname;
        string _Birdless;
        string _Tradeless;
        string _Tradeval;
        string _Foc_Discount_Type;
        string _Foc_Discount;
        string _Entry_Tax_Type;
        string _Entry_Tax1;
        string _Ebird_Discount;
        string _Ebird;
        string _Trade_Discount;
        string _Invoice_No;
        string _Order_No;
        string _Prod_Name;
        DateTime _Invoice_Date;
        DateTime _Order_Date;
        string _Sales_Type;
        string _Under_SalesMan;
        string _Cust_Name;
        string _Place;
        string _Vehicle_No;
        string _Grand_Total;
        string _Discount;
        string _CustBankName;
        string _Disc_Type;
        string _Cash_Discount;
        string _Cash_Disc_Type;
        string _VAT_Amount;
        string _SGST_Amount;
        string _CGST_Amount;
        string _Net_Amount;
        string _Promo_Scheme;
        string _Remark;
        string _Entry_By;
        DateTime _Entry_Time;
        string _EntryTime;
        string _Product_Name;
        string _Qty;
        string _Rate;
        int _sno;
        string _SchPerDiscType;
        string _SchPerDisc;
        string _SchStktDiscType;
        string _SchStktDisc;
        string _Amount;
        string _Mode_of_Payment;
        string _Vendor_ID;
        string _Vendor_Invoice_No;
        string _Vendor_Invoice_Date;
        string _Prod_ID;
        string _Category;
        string _Pack_Type;
        string _Total_Qty;
        string _Opening_Stock;
        string _Unit;
        string _Store_In;
        string _Eff_Date;
        string _Pur_Rate;
        string _Sal_Rate;
        string _HSN;
        string _IGST;
        string _CGST;
        string _SGST;
        string _Slip_No;
        string _Pack_Unit;
        string _Rec_No;
        string _SubRec_No;
        string _rec_Date;
        string _Rec_Amount;
        string _Density_in_Physical;
        string _Temp_in_Physical;
        string _Conv_Density_Phy;
        string _Density_in_Invoice_conv;
        string _Density_Variation;
        string _Den_After_Dec;
        string _Temp_After_Dec;
        string _Conv_Den_After_Dec;
        string _Total_Tax;
        string _Vendor_Name;
        string _City;
        string _Cr_Plus;
        string _Dr_Plus;
        string _Compartment;
        string _Reduction;
        string _discount;
        string _discountid1;
        string _discountid2;
        string _schemetype;
        string _discounttype;
        string _Entry_Tax;
        string _RPG_Charge;
        string _RPG_Surcharge;
        string _LTC;
        string _Trans_Charge;
        string _OLV;
        string _LST;
        string _LST_Surcharge;
        string _LFR;
        string _DOFOBC_Charge;

        string _InvoiceNo;
        string _ToDate;
        string _CustomerName;
        string _Vndr_Invoice_No;
        string _Vndr_Invoice_Date;
        string _QtyTemp;
        string _Inv_date;
        string _Pre_Amount;
        string _Credit_Limit;
        string _Prod_Code;

        //		string _Place;
        string _DueDate;
        string _CurrentBalance;
        string _VehicleNo;

        string _InvoiceDate;
        string _VendorName;
        string _vendorInvoiceNo;
        string _vendorInvoiceDate;

        string _Prod1;
        string _Prod2;
        string _Prod3;
        string _Prod4;
        string _Prod5;
        string _Prod6;
        string _Prod7;
        string _Prod8;
        string _Qty1;
        string _Qty2;
        string _Qty3;
        string _Qty4;
        string _Qty5;
        string _Qty6;
        string _Qty7;
        string _Qty8;
        string _Rate1;
        string _Rate2;
        string _Rate3;
        string _Rate4;
        string _Rate5;
        string _Rate6;
        string _Rate7;
        string _Rate8;
        string _Amt1;
        string _Amt2;
        string _Amt3;
        string _Amt4;
        string _Amt5;
        string _Amt6;
        string _Amt7;
        string _Amt8;
        string _Total;
        string _Promo;
        string _Remarks;

        string _VendorInvoiceNo;
        string _VendorInvoiceDate;

        string _ProdName1;
        string _ProdName2;
        string _ProdName3;
        string _ProdName4;
        string _QtyInLtr1;
        string _QtyInLtr2;
        string _QtyInLtr3;
        string _QtyInLtr4;
        string _ReducOther1;
        string _ReducOther2;
        string _ReducOther3;

        string _ReducOther4;
        string _EntryTax1;
        string _EntryTax2;
        string _EntryTax3;
        string _EntryTax4;
        string _RpgCharge1;
        string _RpgCharge2;
        string _RpgCharge3;
        string _RpgCharge4;
        string _Ltc1;
        string _Ltc2;
        string _Ltc3;
        string _Ltc4;
        string _TranCharge1;
        string _TranCharge2;
        string _TranCharge3;
        string _TranCharge4;
        string _Olv1;
        string _Olv2;
        string _Olv3;
        string _Olv4;
        string _Lst1;
        string _Lst2;
        string _Lst3;
        string _Lst4;

        string _Lfr1;
        string _Lfr2;
        string _Lfr3;
        string _Lfr4;
        string _ChallanNo;
        string _SecSPDisc;
        string _ChallanDate;
        string _DfbCharge1;
        string _DfbCharge2;
        string _DfbCharge3;
        string _DfbCharge4;

        string _DenPhy1;
        string _DenPhy2;
        string _DenPhy3;
        string _DenPhy4;

        string _TemPhy1;
        string _TemPhy2;
        string _TemPhy3;
        string _TemPhy4;

        string _ConvDenPhy1;
        string _ConvDenPhy2;
        string _ConvDenPhy3;
        string _ConvDenPhy4;

        string _DenInv1;

        string _DenInv2;
        string _DenInv3;
        string _DenInv4;

        string _TemInv1;
        string _TemInv2;
        string _TemInv3;
        string _TemInv4;



        string _ConvDenInv1;
        string _ConvDenInv2;
        string _ConvDenInv3;
        string _ConvDenInv4;

        string _DensVaria1;
        string _DensVaria2;
        string _DensVaria3;
        string _DensVaria4;


        string _DenAftDec1;
        string _DenAftDec2;
        string _DenAftDec3;
        string _DenAftDec4;

        string _TempAftDec1;
        string _TempAftDec2;
        string _TempAftDec3;
        string _TempAftDec4;
        string _ConvDenAft1;
        string _ConvDenAft2;
        string _ConvDenAft3;
        string _ConvDenAft4;

        string _TotalAmount;
        string _TotalAmount1;
        string _TotalAmount2;
        string _TotalAmount3;
        string _NetAmount;
        string _DenP1;
        string _DenP2;
        string _DenP3;
        string _DenP4;
        string _TemP1;
        string _TemP2;
        string _TemP3;
        string _TemP4;
        string _BankName;
        string _ChequeDate;
        string _ChequeNo;
        string _Mode;
        string _Receipt;
        /// <summary>
        string _CompanyID;
        string _DealerName;
        string _DealerShip;
        string _Address;
        //string _City;
        string _State;
        string _Country;
        string _PhoneNo;
        string _FaxNo;
        string _Email;
        string _Website;
        string _TinNo;
        string _ExplosiveNo;
        string _FoodLicNO;
        string _WM;
        string _Drive;
        string _Logo;
        string _tempQty;
        string _Actual_Amount;
        string _sub0;
        string _sub1;
        string _sub2;
        string _sub3;
        string _sub4;
        string _sub5;
        string _sub6;
        string _sub7;
        string _sub8;
        string _sub9;
        string _sub10;
        string _sub11;
        string _sub12;
        string _sub13;
        string _sub14;
        string _sub15;
        string _sub16;
        string _sub17;
        string _sub18;
        string _sub19;
        string _sub20;
        string _sub21;
        string _Month;
        string _MinLabel;
        string _MaxLabel;
        string _ReOrderLabel;
        string _BatchNo;
        string _MRP;
        string _New_fixeddiscAmount; //Add by vikas 5.11.2012
        string _New_fixeddisc;       //Add by vikas 5.11.2012
        string _Group_Name;          //Add by vikas 23.10.2012
        string _SPack_Type;          //Add by vikas 7.11.2012
        string _OVD_No;          //Add by vikas 12.11.2012

        /*************Add by vikas 12.11.2012********/
        public string OVD_No
        {
            get
            {
                return _OVD_No;
            }
            set
            {
                _OVD_No = value;
            }
        }
        /*************Add by vikas 7.11.2012********/
        public string SPack_Type
        {
            get
            {
                return _SPack_Type;
            }
            set
            {
                _SPack_Type = value;
            }
        }
        /*************Add by vikas 23.10.2012********/
        public string Group_Name
        {
            get
            {
                return _Group_Name;
            }
            set
            {
                _Group_Name = value;
            }
        }

        /************Add by vikas 5.11.2012***************/
        public string New_fixeddisc
        {
            get
            {
                return _New_fixeddisc;
            }
            set
            {
                _New_fixeddisc = value;
            }
        }
        public string New_fixeddiscAmount
        {
            get
            {
                return _New_fixeddiscAmount;
            }
            set
            {
                _New_fixeddiscAmount = value;
            }
        }
        /********************End****************************/

        public string ReOrderLabel
        {
            get
            {
                return _ReOrderLabel;
            }
            set
            {
                _ReOrderLabel = value;
            }
        }
        public string BatchNo
        {
            get
            {
                return _BatchNo;
            }
            set
            {
                _BatchNo = value;
            }
        }
        public string MRP
        {
            get
            {
                return _MRP;
            }
            set
            {
                _MRP = value;
            }
        }
        public string MaxLabel
        {
            get
            {
                return _MaxLabel;
            }
            set
            {
                _MaxLabel = value;
            }
        }
        public string MinLabel
        {
            get
            {
                return _MinLabel;
            }
            set
            {
                _MinLabel = value;
            }
        }
        public string sub0
        {
            get
            {
                return _sub0;
            }
            set
            {
                _sub0 = value;
            }
        }
        public string sub1
        {
            get
            {
                return _sub1;
            }
            set
            {
                _sub1 = value;
            }
        }
        public string sub2
        {
            get
            {
                return _sub2;
            }
            set
            {
                _sub2 = value;
            }
        }
        public string sub3
        {
            get
            {
                return _sub3;
            }
            set
            {
                _sub3 = value;
            }
        }
        public string sub4
        {
            get
            {
                return _sub4;
            }
            set
            {
                _sub4 = value;
            }
        }
        public string sub5
        {
            get
            {
                return _sub5;
            }
            set
            {
                _sub5 = value;
            }
        }
        public string sub6
        {
            get
            {
                return _sub6;
            }
            set
            {
                _sub6 = value;
            }
        }
        public string sub7
        {
            get
            {
                return _sub7;
            }
            set
            {
                _sub7 = value;
            }
        }
        public string sub8
        {
            get
            {
                return _sub8;
            }
            set
            {
                _sub8 = value;
            }
        }
        public string sub9
        {
            get
            {
                return _sub9;
            }
            set
            {
                _sub9 = value;
            }
        }
        public string sub10
        {
            get
            {
                return _sub10;
            }
            set
            {
                _sub10 = value;
            }
        }
        public string sub11
        {
            get
            {
                return _sub11;
            }
            set
            {
                _sub11 = value;
            }
        }
        public string sub12
        {
            get
            {
                return _sub12;
            }
            set
            {
                _sub12 = value;
            }
        }
        public string sub13
        {
            get
            {
                return _sub13;
            }
            set
            {
                _sub13 = value;
            }
        }
        public string sub14
        {
            get
            {
                return _sub14;
            }
            set
            {
                _sub14 = value;
            }
        }
        public string sub15
        {
            get
            {
                return _sub15;
            }
            set
            {
                _sub15 = value;
            }
        }
        public string sub16
        {
            get
            {
                return _sub16;
            }
            set
            {
                _sub16 = value;
            }
        }
        public string sub17
        {
            get
            {
                return _sub17;
            }
            set
            {
                _sub17 = value;
            }
        }
        public string sub18
        {
            get
            {
                return _sub18;
            }
            set
            {
                _sub18 = value;
            }
        }
        public string sub19
        {
            get
            {
                return _sub19;
            }
            set
            {
                _sub19 = value;
            }
        }
        public string sub20
        {
            get
            {
                return _sub20;
            }
            set
            {
                _sub20 = value;
            }
        }
        public string sub21
        {
            get
            {
                return _sub21;
            }
            set
            {
                _sub21 = value;
            }
        }
        public string Month
        {
            get
            {
                return _Month;
            }
            set
            {
                _Month = value;
            }
        }
        public string CompanyID
        {
            get
            {
                return _CompanyID;
            }
            set
            {
                _CompanyID = value;
            }
        }

        public string DealerName

        {
            get
            {
                return _DealerName;
            }
            set
            {
                _DealerName = value;
            }
        }

        public string DealerShip

        {
            get
            {
                return _DealerShip;
            }
            set
            {
                _DealerShip = value;
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

        //		public string City
        //
        //		{
        //			get
        //			{
        //				return _City;
        //			}
        //			set
        //			{
        //				_City=value;
        //			}
        //		}	

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

        public string PhoneNo

        {
            get
            {
                return _PhoneNo;
            }
            set
            {
                _PhoneNo = value;
            }
        }


        public string FaxNo

        {
            get
            {
                return _FaxNo;
            }
            set
            {
                _FaxNo = value;
            }
        }


        public string Email

        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
            }
        }


        public string Website

        {
            get
            {
                return _Website;
            }
            set
            {
                _Website = value;
            }
        }


        public string TinNo

        {
            get
            {
                return _TinNo;
            }
            set
            {
                _TinNo = value;
            }
        }

        public string ExplosiveNo

        {
            get
            {
                return _ExplosiveNo;
            }
            set
            {
                _ExplosiveNo = value;
            }
        }
        public string Receipt
        {
            get
            {
                return _Receipt;
            }
            set
            {
                _Receipt = value;
            }
        }
        public string BankName
        {
            get
            {
                return _BankName;
            }
            set
            {
                _BankName = value;
            }
        }
        public string ChequeNo
        {
            get
            {
                return _ChequeNo;
            }
            set
            {
                _ChequeNo = value;
            }
        }
        public string Mode
        {
            get
            {
                return _Mode;
            }
            set
            {
                _Mode = value;
            }
        }
        public string ChequeDate
        {
            get
            {
                return _ChequeDate;
            }
            set
            {
                _ChequeDate = value;
            }
        }
        public string FoodLicNO
        {
            get
            {
                return _FoodLicNO;
            }
            set
            {
                _FoodLicNO = value;
            }
        }

        public string WM

        {
            get
            {
                return _WM;
            }
            set
            {
                _WM = value;
            }
        }

        public string Drive

        {
            get
            {
                return _Drive;
            }
            set
            {
                _Drive = value;
            }
        }

        public string Logo

        {
            get
            {
                return _Logo;
            }
            set
            {
                _Logo = value;
            }
        }

        /// </summary>

        public string Prod_Name

        {
            get
            {
                return _Prod_Name;
            }
            set
            {
                _Prod_Name = value;
            }
        }
        public string Vndr_Invoice_No
        {
            get
            {
                return _Vndr_Invoice_No;
            }
            set
            {
                _Vndr_Invoice_No = value;
            }
        }
        public string Vndr_Invoice_Date
        {
            get
            {
                return _Vndr_Invoice_Date;
            }
            set
            {
                _Vndr_Invoice_Date = value;
            }
        }

        public string DenP1
        {
            get
            {
                return _DenP1;
            }
            set
            {
                _DenP1 = value;
            }
        }

        public string DenP2
        {
            get
            {
                return _DenP2;
            }
            set
            {
                _DenP2 = value;
            }
        }

        public string DenP3
        {
            get
            {
                return _DenP3;
            }
            set
            {
                _DenP3 = value;
            }
        }

        public string DenP4
        {
            get
            {
                return _DenP4;
            }
            set
            {
                _DenP4 = value;
            }
        }

        public string TemP1
        {
            get
            {
                return _TemP1;
            }
            set
            {
                _TemP1 = value;
            }
        }

        public string TemP2
        {
            get
            {
                return _TemP2;
            }
            set
            {
                _TemP2 = value;
            }
        }

        public string TemP3
        {
            get
            {
                return _TemP3;
            }
            set
            {
                _TemP3 = value;
            }
        }

        public string TemP4
        {
            get
            {
                return _TemP4;
            }
            set
            {
                _TemP4 = value;
            }
        }

        public string TotalAmount
        {
            get
            {
                return _TotalAmount;
            }
            set
            {
                _TotalAmount = value;
            }
        }

        public string Credit_Limit
        {
            get
            {
                return _Credit_Limit;
            }
            set
            {
                _Credit_Limit = value;
            }
        }
        public string schdiscount
        {
            get
            {
                return _schdiscount;
            }
            set
            {
                _schdiscount = value;
            }
        }
        string _foediscount = "";
        public string foediscount
        {
            get
            {
                return _foediscount;
            }
            set
            {
                _foediscount = value;
            }
        }
        string _foediscounttype = "";
        public string foediscounttype
        {
            get
            {
                return _foediscounttype;
            }
            set
            {
                _foediscounttype = value;
            }
        }
        string _SecSPDiscType = "";
        public string SecSPDiscType
        {
            get
            {
                return _SecSPDiscType;
            }
            set
            {
                _SecSPDiscType = value;
            }
        }
        string _foediscountrs = "";
        public string foediscountrs
        {
            get
            {
                return _foediscountrs;
            }
            set
            {
                _foediscountrs = value;
            }
        }
        public string TotalAmount1
        {
            get
            {
                return _TotalAmount1;
            }
            set
            {
                _TotalAmount1 = value;
            }
        }

        public string TotalAmount2
        {
            get
            {
                return _TotalAmount2;
            }
            set
            {
                _TotalAmount2 = value;
            }
        }

        public string TotalAmount3
        {
            get
            {
                return _TotalAmount3;
            }
            set
            {
                _TotalAmount3 = value;
            }
        }


        public string NetAmount
        {
            get
            {
                return _NetAmount;
            }
            set
            {
                _NetAmount = value;
            }
        }

        public string VendorName
        {
            get
            {
                return _VendorName;
            }
            set
            {
                _VendorName = value;
            }
        }
        public string VendorInvoiceNo
        {
            get
            {
                return _VendorInvoiceNo;
            }
            set
            {
                _VendorInvoiceNo = value;
            }
        }
        public string VendorInvoiceDate
        {
            get
            {
                return _VendorInvoiceDate;
            }
            set
            {
                _VendorInvoiceDate = value;
            }
        }
        public string VehicleNo
        {
            get
            {
                return _VehicleNo;
            }
            set
            {
                _VehicleNo = value;
            }
        }
        public string ProdName1
        {
            get
            {
                return _ProdName1;
            }
            set
            {
                _ProdName1 = value;
            }
        }
        public string ProdName2
        {
            get
            {
                return _ProdName2;
            }
            set
            {
                _ProdName2 = value;
            }
        }
        public string ProdName3
        {
            get
            {
                return _ProdName3;
            }
            set
            {
                _ProdName3 = value;
            }
        }

        public string ProdName4
        {
            get
            {
                return _ProdName4;
            }
            set
            {
                _ProdName4 = value;
            }
        }
        public string QtyInLtr1
        {
            get
            {
                return _QtyInLtr1;
            }
            set
            {
                _QtyInLtr1 = value;
            }
        }
        public string QtyInLtr2
        {
            get
            {
                return _QtyInLtr2;
            }
            set
            {
                _QtyInLtr2 = value;
            }
        }
        public string QtyInLtr3
        {
            get
            {
                return _QtyInLtr3;
            }
            set
            {
                _QtyInLtr3 = value;
            }
        }
        public string QtyInLtr4
        {
            get
            {
                return _QtyInLtr4;
            }
            set
            {
                _QtyInLtr4 = value;
            }
        }
        public string ReducOther1
        {
            get
            {
                return _ReducOther1;
            }
            set
            {
                _ReducOther1 = value;
            }
        }
        public string ReducOther2
        {
            get
            {
                return _ReducOther2;
            }
            set
            {
                _ReducOther2 = value;
            }
        }
        public string ReducOther3
        {
            get
            {
                return _ReducOther3;
            }
            set
            {
                _ReducOther3 = value;
            }
        }
        public string ReducOther4
        {
            get
            {
                return _ReducOther4;
            }
            set
            {
                _ReducOther4 = value;
            }
        }
        public string EntryTax1
        {
            get
            {
                return _EntryTax1;
            }
            set
            {
                _EntryTax1 = value;
            }
        }
        public string EntryTax2
        {
            get
            {
                return _EntryTax2;
            }
            set
            {
                _EntryTax2 = value;
            }
        }
        public string EntryTax3
        {
            get
            {
                return _EntryTax3;
            }
            set
            {
                _EntryTax3 = value;
            }
        }
        public string EntryTax4
        {
            get
            {
                return _EntryTax4;
            }
            set
            {
                _EntryTax4 = value;
            }
        }
        public string RpgCharge1
        {
            get
            {
                return _RpgCharge1;
            }
            set
            {
                _RpgCharge1 = value;
            }
        }
        public string RpgCharge2
        {
            get
            {
                return _RpgCharge2;
            }
            set
            {
                _RpgCharge2 = value;
            }
        }
        public string RpgCharge3
        {
            get
            {
                return _RpgCharge3;
            }
            set
            {
                _RpgCharge3 = value;
            }
        }
        public string RpgCharge4
        {
            get
            {
                return _RpgCharge4;
            }
            set
            {
                _RpgCharge4 = value;
            }
        }

        public string Ltc1
        {
            get
            {
                return _Ltc1;
            }
            set
            {
                _Ltc1 = value;
            }
        }
        public string Ltc2
        {
            get
            {
                return _Ltc2;
            }
            set
            {
                _Ltc2 = value;
            }
        }
        public string Ltc3
        {
            get
            {
                return _Ltc3;
            }
            set
            {
                _Ltc3 = value;
            }
        }
        public string Ltc4
        {
            get
            {
                return _Ltc4;
            }
            set
            {
                _Ltc4 = value;
            }
        }
        public string TranCharge1
        {
            get
            {
                return _TranCharge1;
            }
            set
            {
                _TranCharge1 = value;
            }
        }
        public string TranCharge2
        {
            get
            {
                return _TranCharge2;
            }
            set
            {
                _TranCharge2 = value;
            }
        }

        public string TranCharge3
        {
            get
            {
                return _TranCharge3;
            }
            set
            {
                _TranCharge3 = value;
            }
        }
        public string TranCharge4
        {
            get
            {
                return _TranCharge4;
            }
            set
            {
                _TranCharge4 = value;
            }
        }
        public string Olv1
        {
            get
            {
                return _Olv1;
            }
            set
            {
                _Olv1 = value;
            }
        }


        public string Olv2
        {
            get
            {
                return _Olv2;
            }
            set
            {
                _Olv2 = value;
            }
        }

        public string QtyTemp
        {
            get
            {
                return _QtyTemp;
            }
            set
            {
                _QtyTemp = value;
            }
        }
        public string Olv3
        {
            get
            {
                return _Olv3;
            }
            set
            {
                _Olv3 = value;
            }
        }
        public string Olv4
        {
            get
            {
                return _Olv4;
            }
            set
            {
                _Olv4 = value;
            }
        }


        public string Lst1
        {
            get
            {
                return _Lst1;
            }
            set
            {
                _Lst1 = value;
            }
        }
        public string Lst2
        {
            get
            {
                return _Lst2;

            }
            set
            {
                _Lst2 = value;
            }
        }
        public string Lst4
        {
            get
            {
                return _Lst4;
            }
            set
            {
                _Lst4 = value;
            }
        }

        public string Lst3
        {
            get
            {
                return _Lst3;
            }
            set
            {
                _Lst3 = value;
            }
        }
        public string Lfr1
        {
            get
            {
                return _Lfr1;
            }
            set
            {
                _Lfr1 = value;
            }
        }
        public string Lfr2
        {
            get
            {
                return _Lfr2;
            }
            set
            {
                _Lfr2 = value;
            }
        }


        public string Lfr3
        {
            get
            {
                return _Lfr3;
            }
            set
            {
                _Lfr3 = value;
            }
        }
        public string Lfr4
        {
            get
            {
                return _Lfr4;
            }
            set
            {
                _Lfr4 = value;
            }
        }


        public string DfbCharge1
        {
            get
            {
                return _DfbCharge1;
            }
            set
            {
                _DfbCharge1 = value;
            }
        }
        public string ChallanNo
        {
            get
            {
                return _ChallanNo;
            }
            set
            {
                _ChallanNo = value;
            }
        }
        public string SecSPDisc
        {
            get
            {
                return _SecSPDisc;
            }
            set
            {
                _SecSPDisc = value;
            }
        }
        public string ChallanDate
        {
            get
            {
                return _ChallanDate;
            }
            set
            {
                _ChallanDate = value;
            }
        }

        public string DfbCharge2
        {
            get
            {
                return _DfbCharge2;
            }
            set
            {
                _DfbCharge2 = value;
            }
        }
        public string DfbCharge3
        {
            get
            {
                return _DfbCharge3;
            }
            set
            {
                _DfbCharge3 = value;
            }
        }
        public string DfbCharge4
        {
            get
            {
                return _DfbCharge4;
            }
            set
            {
                _DfbCharge4 = value;
            }
        }


        public string DenPhy1
        {
            get
            {
                return _DenPhy1;
            }
            set
            {
                _DenPhy1 = value;
            }
        }
        public string DenPhy2
        {
            get
            {
                return _DenPhy2;
            }
            set
            {
                _DenPhy2 = value;
            }
        }
        public string DenPhy3
        {
            get
            {
                return _DenPhy3;
            }
            set
            {
                _DenPhy3 = value;
            }
        }


        public string DenPhy4
        {
            get
            {
                return _DenPhy4;
            }
            set
            {
                _DenPhy4 = value;
            }
        }



        public string TemPhy1
        {
            get
            {
                return _TemPhy1;
            }
            set
            {
                _TemPhy1 = value;
            }
        }
        public string TemPhy2
        {
            get
            {
                return _TemPhy2;
            }
            set
            {
                _TemPhy2 = value;
            }
        }


        public string TemPhy3
        {
            get
            {
                return _TemPhy3;
            }
            set
            {
                _TemPhy3 = value;
            }
        }
        public string TemPhy4
        {
            get
            {
                return _TemPhy4;
            }
            set
            {
                _TemPhy4 = value;
            }
        }


        public string ConvDenPhy1
        {
            get
            {
                return _ConvDenPhy1;
            }
            set
            {
                _ConvDenPhy1 = value;
            }
        }


        public string ConvDenPhy2
        {
            get
            {
                return _ConvDenPhy2;
            }
            set
            {
                _ConvDenPhy2 = value;
            }
        }
        public string ConvDenPhy3
        {
            get
            {
                return _ConvDenPhy3;
            }
            set
            {
                _ConvDenPhy3 = value;
            }
        }
        public string ConvDenPhy4
        {
            get
            {
                return _ConvDenPhy4;
            }
            set
            {
                _ConvDenPhy4 = value;
            }
        }


        public string DenInv1
        {
            get
            {
                return _DenInv1;
            }
            set
            {
                _DenInv1 = value;
            }
        }
        public string DenInv2
        {
            get
            {
                return _DenInv2;
            }
            set
            {
                _DenInv2 = value;
            }
        }
        public string DenInv3
        {
            get
            {
                return _DenInv3;
            }
            set
            {
                _DenInv3 = value;
            }
        }


        public string DenInv4
        {
            get
            {
                return _DenInv4;
            }
            set
            {
                _DenInv4 = value;
            }
        }


        public string TemInv1
        {
            get
            {
                return _TemInv1;
            }
            set
            {
                _TemInv1 = value;
            }
        }
        public string TemInv2
        {
            get
            {
                return _TemInv2;
            }
            set
            {
                _TemInv2 = value;
            }
        }

        public string TemInv3
        {
            get
            {
                return _TemInv3;
            }
            set
            {
                _TemInv3 = value;
            }
        }
        public string TemInv4
        {
            get
            {
                return _TemInv4;
            }
            set
            {
                _TemInv4 = value;
            }
        }
        public string ConvDenInv1
        {
            get
            {
                return _ConvDenInv1;
            }
            set
            {
                _ConvDenInv1 = value;
            }
        }

        public string ConvDenInv2
        {
            get
            {
                return _ConvDenInv2;
            }
            set
            {
                _ConvDenInv2 = value;
            }
        }
        public string ConvDenInv3
        {
            get
            {
                return _ConvDenInv3;
            }
            set
            {
                _ConvDenInv3 = value;
            }
        }
        public string ConvDenInv4
        {
            get
            {
                return _ConvDenInv4;
            }
            set
            {
                _ConvDenInv4 = value;
            }
        }


        public string DensVaria1
        {
            get
            {
                return _DensVaria1;
            }
            set
            {
                _DensVaria1 = value;
            }
        }
        public string DensVaria2
        {
            get
            {
                return _DensVaria2;
            }
            set
            {
                _DensVaria2 = value;
            }
        }
        public string DensVaria3
        {
            get
            {
                return _DensVaria3;
            }
            set
            {
                _DensVaria3 = value;
            }
        }


        public string DensVaria4
        {
            get
            {
                return _DensVaria4;
            }
            set
            {
                _DensVaria4 = value;
            }
        }


        public string DenAftDec1
        {
            get
            {
                return _DenAftDec1;
            }
            set
            {
                _DenAftDec1 = value;
            }
        }
        public string DenAftDec2
        {
            get
            {
                return _DenAftDec2;
            }
            set
            {
                _DenAftDec2 = value;
            }
        }


        public string DenAftDec3
        {
            get
            {
                return _DenAftDec3;
            }
            set
            {
                _DenAftDec3 = value;
            }
        }
        public string DenAftDec4
        {
            get
            {
                return _DenAftDec4;
            }
            set
            {
                _DenAftDec4 = value;
            }
        }

        public string TempAftDec1
        {
            get
            {
                return _TempAftDec1;
            }
            set
            {
                _TempAftDec1 = value;
            }
        }


        public string TempAftDec2
        {
            get
            {
                return _TempAftDec2;
            }
            set
            {
                _TempAftDec2 = value;
            }
        }
        public string TempAftDec3
        {
            get
            {
                return _TempAftDec3;
            }
            set
            {
                _TempAftDec3 = value;
            }
        }
        public string TempAftDec4
        {
            get
            {
                return _TempAftDec4;
            }
            set
            {
                _TempAftDec4 = value;
            }
        }



        public string ConvDenAft1
        {
            get
            {
                return _ConvDenAft1;
            }
            set
            {
                _ConvDenAft1 = value;
            }
        }
        public string ConvDenAft2
        {
            get
            {
                return _ConvDenAft2;
            }
            set
            {
                _ConvDenAft2 = value;
            }
        }
        public string ConvDenAft3
        {
            get
            {
                return _ConvDenAft3;
            }
            set
            {
                _ConvDenAft3 = value;
            }
        }
        public string ConvDenAft4
        {
            get
            {
                return _ConvDenAft4;
            }
            set
            {
                _ConvDenAft4 = value;
            }
        }


        //dfdfdf

        public string InvoiceDate
        {
            get
            {
                return _InvoiceDate;
            }
            set
            {
                _InvoiceDate = value;
            }
        }



        public string vendorInvoiceNo
        {
            get
            {
                return _vendorInvoiceNo;
            }
            set
            {
                _vendorInvoiceNo = value;
            }
        }

        public string vendorInvoiceDate
        {
            get
            {
                return _vendorInvoiceDate;
            }
            set
            {
                _vendorInvoiceDate = value;
            }
        }





        public string Rate1
        {
            get
            {
                return _Rate1;
            }
            set
            {
                _Rate1 = value;
            }
        }
        public string Total
        {
            get
            {
                return _Total;
            }
            set
            {
                _Total = value;
            }
        }
        public string Promo
        {
            get
            {
                return _Promo;
            }
            set
            {
                _Promo = value;
            }
        }
        public string Remarks
        {
            get
            {
                return _Remarks;
            }
            set
            {
                _Remarks = value;
            }
        }


        public string Rate2
        {
            get
            {
                return _Rate2;
            }
            set
            {
                _Rate2 = value;
            }
        }
        public string Rate3
        {
            get
            {
                return _Rate3;
            }
            set
            {
                _Rate3 = value;
            }
        }
        public string Rate4
        {
            get
            {
                return _Rate4;
            }
            set
            {
                _Rate4 = value;
            }
        }
        public string Rate5
        {
            get
            {
                return _Rate5;
            }
            set
            {
                _Rate5 = value;
            }
        }
        public string Rate6
        {
            get
            {
                return _Rate6;
            }
            set
            {
                _Rate6 = value;
            }
        }
        public string Rate7
        {
            get
            {
                return _Rate7;
            }
            set
            {
                _Rate7 = value;
            }
        }
        public string Rate8
        {
            get
            {
                return _Rate8;
            }
            set
            {
                _Rate8 = value;
            }
        }
        public string Amt1
        {
            get
            {
                return _Amt1;
            }
            set
            {
                _Amt1 = value;
            }
        }
        public string Amt2
        {
            get
            {
                return _Amt2;
            }
            set
            {
                _Amt2 = value;
            }
        }
        public string Amt3
        {
            get
            {
                return _Amt3;
            }
            set
            {
                _Amt3 = value;
            }
        }
        public string Amt4
        {
            get
            {
                return _Amt4;
            }
            set
            {
                _Amt4 = value;
            }
        }
        public string Amt5
        {
            get
            {
                return _Amt5;
            }
            set
            {
                _Amt5 = value;
            }
        }
        public string Amt6
        {
            get
            {
                return _Amt6;
            }
            set
            {
                _Amt6 = value;
            }
        }
        public string Amt7
        {
            get
            {
                return _Amt7;
            }
            set
            {
                _Amt7 = value;
            }
        }
        public string Amt8
        {
            get
            {
                return _Amt8;
            }
            set
            {
                _Amt8 = value;
            }
        }





        public string Prod1
        {
            get
            {
                return _Prod1;
            }
            set
            {
                _Prod1 = value;
            }
        }




        public string Prod2
        {
            get
            {
                return _Prod2;
            }
            set
            {
                _Prod2 = value;
            }
        }


        public string Prod3
        {
            get
            {
                return _Prod3;
            }
            set
            {
                _Prod3 = value;
            }
        }

        public string Prod4
        {
            get
            {
                return _Prod4;
            }
            set
            {
                _Prod4 = value;
            }
        }
        public string Prod5
        {
            get
            {
                return _Prod5;
            }
            set
            {
                _Prod5 = value;
            }
        }
        public string Prod6
        {
            get
            {
                return _Prod6;
            }
            set
            {
                _Prod6 = value;
            }
        }
        public string Prod7
        {
            get
            {
                return _Prod7;
            }
            set
            {
                _Prod7 = value;
            }
        }
        public string Prod8
        {
            get
            {
                return _Prod8;
            }
            set
            {
                _Prod8 = value;
            }
        }
        public string Qty1
        {
            get
            {
                return _Qty1;
            }
            set
            {
                _Qty1 = value;
            }
        }
        public string Qty2
        {
            get
            {
                return _Qty2;
            }
            set
            {
                _Qty2 = value;
            }
        }
        public string Qty3
        {
            get
            {
                return _Qty3;
            }
            set
            {
                _Qty3 = value;
            }

        }
        public string Qty4
        {
            get
            {
                return _Qty4;
            }
            set
            {
                _Qty4 = value;
            }
        }
        public string Qty5
        {
            get
            {
                return _Qty5;
            }
            set
            {
                _Qty5 = value;
            }
        }
        public string Qty6
        {
            get
            {
                return _Qty6;
            }
            set
            {
                _Qty6 = value;
            }
        }
        public string Qty7
        {
            get
            {
                return _Qty7;
            }
            set
            {
                _Qty7 = value;
            }
        }
        public string Qty8
        {
            get
            {
                return _Qty8;
            }
            set
            {
                _Qty8 = value;
            }
        }



        public string InvoiceNo
        {
            get
            {
                return _InvoiceNo;
            }
            set
            {
                _InvoiceNo = value;
            }
        }

        public string ToDate
        {
            get
            {
                return _ToDate;
            }
            set
            {
                _ToDate = value;
            }
        }

        public string CustomerName
        {
            get
            {
                return _CustomerName;
            }
            set
            {
                _CustomerName = value;
            }
        }

        public string Place
        {
            get
            {
                return _Place;
            }
            set
            {
                _Place = value;
            }
        }

        public string DueDate
        {
            get
            {
                return _DueDate;
            }
            set
            {
                _DueDate = value;
            }
        }

        public string CurrentBalance
        {
            get
            {
                return _CurrentBalance;
            }
            set
            {
                _CurrentBalance = value;
            }

        }





        public string Compartment
        {
            get
            {
                return _Compartment;
            }
            set
            {
                _Compartment = value;
            }
        }
        public string Reduction
        {
            get
            {
                return _Reduction;
            }
            set
            {
                _Reduction = value;
            }
        }

        public string Entry_Tax
        {
            get
            {
                return _Entry_Tax;
            }
            set
            {
                _Entry_Tax = value;
            }
        }

        public string RPG_Charge
        {
            get
            {
                return _RPG_Charge;
            }
            set
            {
                _RPG_Charge = value;
            }
        }

        public string RPG_Surcharge
        {
            get
            {
                return _RPG_Surcharge;
            }
            set
            {
                _RPG_Surcharge = value;
            }
        }

        public string LTC
        {
            get
            {
                return _LTC;
            }
            set
            {
                _LTC = value;
            }
        }


        public string Trans_Charge
        {
            get
            {
                return _Trans_Charge;
            }
            set
            {
                _Trans_Charge = value;
            }
        }

        public string OLV
        {
            get
            {
                return _OLV;
            }
            set
            {
                _OLV = value;
            }
        }

        public string LST
        {
            get
            {
                return _LST;
            }
            set
            {
                _LST = value;
            }
        }

        public string LST_Sucharge
        {
            get
            {
                return _LST_Surcharge;
            }
            set
            {
                _LST_Surcharge = value;
            }
        }


        public string LFR
        {
            get
            {
                return _LFR;
            }
            set
            {
                _LFR = value;
            }
        }

        public string DOFOBC_Charge
        {
            get
            {
                return _DOFOBC_Charge;
            }
            set
            {
                _DOFOBC_Charge = value;
            }
        }









        public string Cr_Plus
        {
            get
            {
                return _Cr_Plus;
            }
            set
            {
                _Cr_Plus = value;
            }
        }
        public string Pre_Amount
        {
            get
            {
                return _Pre_Amount;
            }
            set
            {
                _Pre_Amount = value;
            }
        }
        public string Dr_Plus
        {
            get
            {
                return _Dr_Plus;
            }
            set
            {
                _Dr_Plus = value;
            }
        }
        public string Vendor_Name
        {
            get
            {
                return _Vendor_Name;
            }
            set
            {
                _Vendor_Name = value;
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
        public string Total_Tax
        {
            get
            {
                return _Total_Tax;
            }
            set
            {
                _Total_Tax = value;
            }
        }
        public string Conv_Den_After_Dec
        {
            get
            {
                return _Conv_Den_After_Dec;
            }
            set
            {
                _Conv_Den_After_Dec = value;
            }
        }
        public string Temp_After_Dec
        {
            get
            {
                return _Temp_After_Dec;
            }
            set
            {
                _Temp_After_Dec = value;
            }
        }
        public string Density_After_Dec
        {
            get
            {
                return _Den_After_Dec;
            }
            set
            {
                _Den_After_Dec = value;
            }
        }
        public string Density_Variation
        {
            get
            {
                return _Density_Variation;
            }
            set
            {
                _Density_Variation = value;
            }
        }
        public string Density_in_Invoice_Conv
        {
            get
            {
                return _Density_in_Invoice_conv;
            }
            set
            {
                _Density_in_Invoice_conv = value;
            }
        }
        public string Converted_Density_Phy
        {
            get
            {
                return _Conv_Density_Phy;
            }
            set
            {
                _Conv_Density_Phy = value;
            }
        }

        public string Temp_in_Physical
        {
            get
            {
                return _Temp_in_Physical;
            }
            set
            {
                _Temp_in_Physical = value;
            }
        }

        public string Density_in_Physical
        {
            get
            {
                return _Density_in_Physical;
            }
            set
            {
                _Density_in_Physical = value;
            }
        }

        public string Received_Amount
        {
            get
            {
                return _Rec_Amount;
            }
            set
            {
                _Rec_Amount = value;
            }
        }
        public string Received_Date
        {
            get
            {
                return _rec_Date;
            }
            set
            {
                _rec_Date = value;
            }
        }
        public string Received_No
        {
            get
            {
                return _Rec_No;
            }
            set
            {
                _Rec_No = value;
            }
        }
        public string SubReceived_No
        {
            get
            {
                return _SubRec_No;
            }
            set
            {
                _SubRec_No = value;
            }
        }
        public string Slip_No
        {
            get
            {
                return _Slip_No;
            }
            set
            {
                _Slip_No = value;
            }
        }
        public string schemetype
        {
            get
            {
                return _schemetype;
            }
            set
            {
                _schemetype = value;
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
        public string discountid1
        {
            get
            {
                return _discountid1;
            }
            set
            {
                _discountid1 = value;
            }
        }
        public string discountid2
        {
            get
            {
                return _discountid2;
            }
            set
            {
                _discountid2 = value;
            }
        }
        public string discounttype
        {
            get
            {
                return _discounttype;
            }
            set
            {
                _discounttype = value;
            }
        }
        public string Eff_Date
        {
            get
            {
                return _Eff_Date;
            }
            set
            {
                _Eff_Date = value;
            }
        }
        public string Pur_Rate
        {
            get
            {
                return _Pur_Rate;
            }
            set
            {
                _Pur_Rate = value;
            }
        }
        public string Sal_Rate
        {
            get
            {
                return _Sal_Rate;
            }
            set
            {
                _Sal_Rate = value;
            }
        }
        public string HSN
        {
            get
            {
                return _HSN;
            }
            set
            {
                _HSN = value;
            }
        }
        public string IGST
        {
            get
            {
                return _IGST;
            }
            set
            {
                _IGST = value;
            }
        }
        public string CGST
        {
            get
            {
                return _CGST;
            }
            set
            {
                _CGST = value;
            }
        }
        public string SGST
        {
            get
            {
                return _SGST;
            }
            set
            {
                _SGST = value;
            }
        }
        public string Invoice_No
        {
            get
            {
                return _Invoice_No;
            }
            set
            {
                _Invoice_No = value;
            }
        }
        public string Order_No
        {
            get
            {
                return _Order_No;
            }
            set
            {
                _Order_No = value;
            }
        }
        public DateTime Invoice_Date
        {
            get
            {
                return _Invoice_Date;
            }
            set
            {
                _Invoice_Date = value;
            }
        }
        public DateTime Order_Date
        {
            get
            {
                return _Order_Date;
            }
            set
            {
                _Order_Date = value;
            }
        }
        public string Sales_Type
        {
            get
            {
                return _Sales_Type;
            }
            set
            {
                _Sales_Type = value;
            }
        }
        public string Under_SalesMan
        {
            get
            {
                return _Under_SalesMan;
            }
            set
            {
                _Under_SalesMan = value;
            }
        }
        public string Customer_Name
        {
            get
            {
                return _Cust_Name;
            }
            set
            {
                _Cust_Name = value;
            }
        }

        public string Vehicle_No
        {
            get
            {
                return _Vehicle_No;
            }
            set
            {
                _Vehicle_No = value;
            }
        }
        public string Grand_Total
        {
            get
            {
                return _Grand_Total;
            }
            set
            {
                _Grand_Total = value;
            }
        }
        public string Discount
        {
            get
            {
                return _Discount;
            }
            set
            {
                _Discount = value;
            }
        }
        public string CustBankName
        {
            get
            {
                return _CustBankName;
            }
            set
            {
                _CustBankName = value;
            }
        }
        public string Discount_Type
        {
            get
            {
                return _Disc_Type;
            }
            set
            {
                _Disc_Type = value;
            }
        }


        public string Cash_Discount
        {
            get
            {
                return _Cash_Discount;
            }
            set
            {
                _Cash_Discount = value;
            }
        }

        public string Cash_Disc_Type
        {
            get
            {
                return _Cash_Disc_Type;
            }
            set
            {
                _Cash_Disc_Type = value;
            }
        }
        //1111111111111111111111111111111
        public string VAT_Amount
        {
            get
            {
                return _VAT_Amount;
            }
            set
            {
                _VAT_Amount = value;
            }
        }

        public string SGST_Amount
        {
            get
            {
                return _SGST_Amount;
            }
            set
            {
                _SGST_Amount = value;
            }
        }
        public string CGST_Amount
        {
            get
            {
                return _CGST_Amount;
            }
            set
            {
                _CGST_Amount = value;
            }
        }
        public string Tradeval
        {
            get
            {
                return _Tradeval;
            }
            set
            {
                _Tradeval = value;
            }
        }

        public string Trade_Discount
        {
            get
            {
                return _Trade_Discount;
            }
            set
            {
                _Trade_Discount = value;
            }
        }
        string _totalqtyltr;
        public string totalqtyltr
        {
            get
            {
                return _totalqtyltr;
            }
            set
            {
                _totalqtyltr = value;
            }
        }
        public string Ebird
        {
            get
            {
                return _Ebird;
            }
            set
            {
                _Ebird = value;
            }
        }
        public string Tradeless
        {
            get
            {
                return _Tradeless;
            }
            set
            {
                _Tradeless = value;
            }
        }

        public string Birdless
        {
            get
            {
                return _Birdless;
            }
            set
            {
                _Birdless = value;
            }
        }
        public string Ebird_Discount
        {
            get
            {
                return _Ebird_Discount;
            }
            set
            {
                _Ebird_Discount = value;
            }
        }

        public string Entry_Tax1
        {
            get
            {
                return _Entry_Tax1;
            }
            set
            {
                _Entry_Tax1 = value;
            }
        }

        public string Entry_Tax_Type
        {
            get
            {
                return _Entry_Tax_Type;
            }
            set
            {
                _Entry_Tax_Type = value;
            }
        }

        public string Foc_Discount
        {
            get
            {
                return _Foc_Discount;
            }
            set
            {
                _Foc_Discount = value;
            }
        }
        string _fixed_Discount;
        public string fixed_Discount
        {
            get
            {
                return _fixed_Discount;
            }
            set
            {
                _fixed_Discount = value;
            }
        }
        string _fixed_Discount_Type;
        public string fixed_Discount_Type
        {
            get
            {
                return _fixed_Discount_Type;
            }
            set
            {
                _fixed_Discount_Type = value;
            }
        }
        public string Foc_Discount_Type
        {
            get
            {
                return _Foc_Discount_Type;
            }
            set
            {
                _Foc_Discount_Type = value;
            }
        }
        public string Net_Amount
        {
            get
            {
                return _Net_Amount;
            }
            set
            {
                _Net_Amount = value;
            }
        }
        public string Promo_Scheme
        {
            get
            {
                return _Promo_Scheme;
            }
            set
            {
                _Promo_Scheme = value;
            }
        }
        public string Remerk
        {
            get
            {
                return _Remark;
            }
            set
            {
                _Remark = value;
            }
        }
        public string Entry_By
        {
            get
            {
                return _Entry_By;
            }
            set
            {
                _Entry_By = value;
            }
        }

        public DateTime Entry_Time
        {
            get
            {
                return _Entry_Time;
            }
            set
            {
                _Entry_Time = value;
            }
        }
        public string EntryTime
        {
            get
            {
                return _EntryTime;
            }
            set
            {
                _EntryTime = value;
            }
        }
        public string Product_Name
        {
            get
            {
                return _Product_Name;
            }
            set
            {
                _Product_Name = value;
            }
        }
        public string sch
        {
            get
            {
                return _sch;
            }
            set
            {
                _sch = value;
            }
        }
        string _foe;
        public string foe
        {
            get
            {
                return _foe;
            }
            set
            {
                _foe = value;
            }
        }
        string _foc;
        public string foc
        {
            get
            {
                return _foc;
            }
            set
            {
                _foc = value;
            }
        }
        public string Qty
        {
            get
            {
                return _Qty;
            }
            set
            {
                _Qty = value;
            }
        }
        public string scheme1
        {
            get
            {
                return _scheme;
            }
            set
            {
                _scheme = value;
            }
        }

        public string Rate
        {
            get
            {
                return _Rate;
            }
            set
            {
                _Rate = value;
            }
        }
        public int sno
        {
            get
            {
                return _sno;
            }
            set
            {
                _sno = value;
            }
        }
        public string Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                _Amount = value;
            }
        }
        public string SchPerDisc
        {
            get
            {
                return _SchPerDisc;
            }
            set
            {
                _SchPerDisc = value;
            }
        }
        public string SchPerDiscType
        {
            get
            {
                return _SchPerDiscType;
            }
            set
            {
                _SchPerDiscType = value;
            }
        }
        public string SchStktDisc
        {
            get
            {
                return _SchStktDisc;
            }
            set
            {
                _SchStktDisc = value;
            }
        }
        public string SchStktDiscType
        {
            get
            {
                return _SchStktDiscType;
            }
            set
            {
                _SchStktDiscType = value;
            }
        }
        public string Mode_of_Payment
        {
            get
            {
                return _Mode_of_Payment;
            }
            set
            {
                _Mode_of_Payment = value;
            }
        }
        public string Vendor_ID
        {
            get
            {
                return _Vendor_ID;
            }
            set
            {
                _Vendor_ID = value;
            }
        }
        public string Vendor_Invoice_No
        {
            get
            {
                return _Vendor_Invoice_No;
            }
            set
            {
                _Vendor_Invoice_No = value;
            }
        }
        public string Vendor_Invoice_Date
        {
            get
            {
                return _Vendor_Invoice_Date;
            }
            set
            {
                _Vendor_Invoice_Date = value;
            }
        }
        public string Prod_ID
        {
            get
            {
                return _Prod_ID;
            }
            set
            {
                _Prod_ID = value;
            }
        }
        public string Category
        {
            get
            {
                return _Category;
            }
            set
            {
                _Category = value;
            }
        }
        public string Package_Type
        {
            get
            {
                return _Pack_Type;
            }
            set
            {
                _Pack_Type = value;
            }
        }
        public string Package_Unit
        {
            get
            {
                return _Pack_Unit;
            }
            set
            {
                _Pack_Unit = value;
            }
        }
        public string Total_Qty
        {
            get
            {
                return _Total_Qty;
            }
            set
            {
                _Total_Qty = value;
            }
        }
        public string Opening_Stock
        {
            get
            {
                return _Opening_Stock;
            }
            set
            {
                _Opening_Stock = value;
            }
        }
        public string Unit
        {
            get
            {
                return _Unit;
            }
            set
            {
                _Unit = value;
            }
        }
        public string Store_In
        {
            get
            {
                return _Store_In;
            }
            set
            {
                _Store_In = value;
            }
        }
        public string Inv_date
        {
            get
            {
                return _Inv_date;
            }
            set
            {
                _Inv_date = value;
            }
        }
        public string Prod_Code
        {
            get
            {
                return _Prod_Code;
            }
            set
            {
                _Prod_Code = value;
            }
        }
        public string tempQty
        {
            get
            {
                return _tempQty;
            }
            set
            {
                _tempQty = value;
            }
        }

        public string Actual_Amount
        {
            get
            {
                return _Actual_Amount;
            }
            set
            {
                _Actual_Amount = value;
            }
        }

        //************************
        string _foid = "";
        public string foid
        {
            get
            {
                return _foid;
            }
            set
            {
                _foid = value;
            }
        }
        string _discription = "";
        public string discription
        {
            get
            {
                return _discription;
            }
            set
            {
                _discription = value;
            }
        }

        string _custtype = "";
        public string custtype
        {
            get
            {
                return _custtype;
            }
            set
            {
                _custtype = value;
            }
        }
        string _cust_id = "";
        public string cust_id
        {
            get
            {
                return _cust_id;
            }
            set
            {
                _cust_id = value;
            }
        }
        string _Narration;
        public string Narration
        {
            get
            {
                return _Narration;
            }
            set
            {
                _Narration = value;
            }
        }
        string _prod_id = "";
        public string prod_id
        {
            get
            {
                return _prod_id;
            }
            set
            {
                _prod_id = value;
            }
        }

        //*************************************
        #endregion

        #region Constructor : Opens the connection with Database Server.
        public InventoryClass()
        {
            SqlCon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            SqlCon.Open();
            SqlCon1 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            SqlCon1.Open();

        }
        #endregion

        /// <summary>
        /// Retruns the SqlDataReader Object containg the Record set of the query passed as a parameter to this function. 
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public SqlDataReader GetRecordSet(string Sql)
        {
            SqlCmd = new SqlCommand(Sql, SqlCon);
            SqlDtr = SqlCmd.ExecuteReader();
            return SqlDtr;
        }

        /// <summary>
        /// Calls the Procedure GetInvoiceBalance1 by passing Customer Name and city as a parameter & returns 
        /// the SqlDataReader object. 
        /// </summary>
        /// <param name="CustName"></param>
        /// <param name="City"></param>
        /// <returns></returns>
        public SqlDataReader GetInvoiceBalance(string CustName, string City)
        {
            SqlCmd = new SqlCommand("GetInvoiceBalance1", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@CustName", CustName);
            SqlCmd.Parameters.Add("@City", City);
            SqlDtr = SqlCmd.ExecuteReader();
            return SqlDtr;
        }

        /// <summary>
        /// Calls the Procedure Test by passing Customer ID as a parameter & returns the SqlDataReader object. 
        /// </summary>
        /// <param name="Cust_ID"></param>
        /// <returns></returns>
        public SqlDataReader GetInvoiceBal(string Cust_ID)
        {
            SqlCmd = new SqlCommand("Test", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Cust_ID", Cust_ID);
            SqlDtr = SqlCmd.ExecuteReader();
            return SqlDtr;
        }

        /// <summary>
        /// Returns the SqlDataReader object containing the max. Invoice No. from Sales_Master table.
        /// </summary>
        /// <returns></returns>
        public SqlDataReader GetNextSalesInvoiceNo()
        {
            SqlCmd = new SqlCommand("select max(Invoice_No)+1 from Sales_Master", SqlCon);
            SqlDtr = SqlCmd.ExecuteReader();
            return SqlDtr;
        }

        /// <summary>
        /// Returns the DataTime Object for the passing date as a string  and boolean.
        /// </summary>
        /// <param name="dat"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private DateTime getdate(string dat, bool to)
        {
            string[] dt = dat.Split(new char[] { '/' }, dat.Length);
            if (to)
                return new DateTime(Int32.Parse(dt[2]), Int32.Parse(dt[1]), Int32.Parse(dt[0]));
            else
                return new DateTime(Int32.Parse(dt[2]), Int32.Parse(dt[1]), Int32.Parse(dt[0]));
        }

        /// <summary>
        /// Calls the Procedure PuchaseINV to insert the Purchase  Invoice Details
        /// This method is not used.
        /// </summary>
        public void InsertPurchaseInvoiceDuplicate()
        {
            SqlCmd = new SqlCommand("PuchaseINV", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@InvoiceNo", InvoiceNo);
            SqlCmd.Parameters.Add("@InvoiceDate", InvoiceDate);
            SqlCmd.Parameters.Add("@VendorName", VendorName);
            SqlCmd.Parameters.Add("@Place", Place);
            SqlCmd.Parameters.Add("@vendorInvoiceNo", vendorInvoiceNo);
            SqlCmd.Parameters.Add("@vendorInvoiceDate", vendorInvoiceDate);
            SqlCmd.Parameters.Add("@Prod1", Prod1);
            SqlCmd.Parameters.Add("@prod2", Prod2);
            SqlCmd.Parameters.Add("@prod3", Prod3);
            SqlCmd.Parameters.Add("@prod4", Prod4);
            SqlCmd.Parameters.Add("@prod5", Prod5);
            SqlCmd.Parameters.Add("@prod6", Prod6);
            SqlCmd.Parameters.Add("@prod7", Prod7);
            SqlCmd.Parameters.Add("@prod8", Prod8);
            SqlCmd.Parameters.Add("@Qty1", Qty1);
            SqlCmd.Parameters.Add("@Qty2", Qty2);
            SqlCmd.Parameters.Add("@Qty3", Qty3);
            SqlCmd.Parameters.Add("@Qty4", Qty4);
            SqlCmd.Parameters.Add("@Qty5", Qty5);
            SqlCmd.Parameters.Add("@Qty6", Qty6);
            SqlCmd.Parameters.Add("@Qty7", Qty7);
            SqlCmd.Parameters.Add("@Qty8", Qty8);
            SqlCmd.Parameters.Add("@Rate1", Rate1);
            SqlCmd.Parameters.Add("@Rate2", Rate2);
            SqlCmd.Parameters.Add("@Rate3", Rate3);
            SqlCmd.Parameters.Add("@Rate4", Rate4);
            SqlCmd.Parameters.Add("@Rate5", Rate5);
            SqlCmd.Parameters.Add("@Rate6", Rate6);
            SqlCmd.Parameters.Add("@Rate7", Rate7);
            SqlCmd.Parameters.Add("@Rate8", Rate8);
            SqlCmd.Parameters.Add("@Amt1", Amt1);
            SqlCmd.Parameters.Add("@Amt2", Amt2);
            SqlCmd.Parameters.Add("@Amt3", Amt3);
            SqlCmd.Parameters.Add("@Amt4", Amt4);
            SqlCmd.Parameters.Add("@Amt5", Amt5);
            SqlCmd.Parameters.Add("@Amt6", Amt6);
            SqlCmd.Parameters.Add("@Amt7", Amt7);
            SqlCmd.Parameters.Add("@Amt8", Amt8);
            SqlCmd.Parameters.Add("@Total", Total);
            SqlCmd.Parameters.Add("@Promo", Promo);
            SqlCmd.Parameters.Add("@Remarks", Remarks);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure Salesinv3 to insert the Sales  Invoice Details
        /// This method is not used.
        /// </summary>
        public void InsertSalesInvoiceDuplicate()
        {
            SqlCmd = new SqlCommand("Salesinv3", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@InvoiceNo", InvoiceNo);
            SqlCmd.Parameters.Add("@ToDate", ToDate);
            SqlCmd.Parameters.Add("@CustomerName", CustomerName);
            SqlCmd.Parameters.Add("@Place", Place);
            SqlCmd.Parameters.Add("@DueDate", DueDate);
            SqlCmd.Parameters.Add("@CurrentBalance", CurrentBalance);
            SqlCmd.Parameters.Add("@VehicleNo", VehicleNo);
            SqlCmd.Parameters.Add("@Prod1", Prod1);
            SqlCmd.Parameters.Add("@prod2", Prod2);
            SqlCmd.Parameters.Add("@prod3", Prod3);
            SqlCmd.Parameters.Add("@prod4", Prod4);
            SqlCmd.Parameters.Add("@prod5", Prod5);
            SqlCmd.Parameters.Add("@prod6", Prod6);
            SqlCmd.Parameters.Add("@prod7", Prod7);
            SqlCmd.Parameters.Add("@prod8", Prod8);
            SqlCmd.Parameters.Add("@Qty1", Qty1);
            SqlCmd.Parameters.Add("@Qty2", Qty2);
            SqlCmd.Parameters.Add("@Qty3", Qty3);
            SqlCmd.Parameters.Add("@Qty4", Qty4);
            SqlCmd.Parameters.Add("@Qty5", Qty5);
            SqlCmd.Parameters.Add("@Qty6", Qty6);
            SqlCmd.Parameters.Add("@Qty7", Qty7);
            SqlCmd.Parameters.Add("@Qty8", Qty8);
            SqlCmd.Parameters.Add("@Rate1", Rate1);
            SqlCmd.Parameters.Add("@Rate2", Rate2);
            SqlCmd.Parameters.Add("@Rate3", Rate3);
            SqlCmd.Parameters.Add("@Rate4", Rate4);
            SqlCmd.Parameters.Add("@Rate5", Rate5);
            SqlCmd.Parameters.Add("@Rate6", Rate6);
            SqlCmd.Parameters.Add("@Rate7", Rate7);
            SqlCmd.Parameters.Add("@Rate8", Rate8);
            SqlCmd.Parameters.Add("@Amt1", Amt1);
            SqlCmd.Parameters.Add("@Amt2", Amt2);
            SqlCmd.Parameters.Add("@Amt3", Amt3);
            SqlCmd.Parameters.Add("@Amt4", Amt4);
            SqlCmd.Parameters.Add("@Amt5", Amt5);
            SqlCmd.Parameters.Add("@Amt6", Amt6);
            SqlCmd.Parameters.Add("@Amt7", Amt7);
            SqlCmd.Parameters.Add("@Amt8", Amt8);
            SqlCmd.Parameters.Add("@Total", Total);
            SqlCmd.Parameters.Add("@Promo", Promo);
            SqlCmd.Parameters.Add("@Remarks", Remarks);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProSalesMasterEntry to insert the Sales Invoice Details in Sales_Master.
        /// after that update the customer credit limit, update the status of Order_Col_Master table 
        /// and also insert the value in CustomerLedgerTable and AccountsLedgerTable with the help of
        /// ProInsertAccountsLedger procedure.
        /// </summary>
        public void InsertSalesMaster(SalesSaveDetailsModel obj)
        {
            SqlCmd = new SqlCommand("ProSalesMasterEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", Int32.Parse(obj.Invoice_No));
            SqlCmd.Parameters.Add("@Invoice_Date", obj.Invoice_Date);
            SqlCmd.Parameters.Add("@Sales_Type", obj.Sales_Type);
            SqlCmd.Parameters.Add("@Under_SalesMan", obj.Under_SalesMan);
            SqlCmd.Parameters.Add("@Cust_Name", obj.Customer_Name);
            SqlCmd.Parameters.Add("@Place", obj.Place);
            SqlCmd.Parameters.Add("@Vehicle_No", obj.Vehicle_No);
            SqlCmd.Parameters.Add("@Grand_Total", obj.Grand_Total);
            //SqlCmd.Parameters .Add("@Discount",float.Parse(Discount));
            SqlCmd.Parameters.Add("@Discount", obj.Discount);
            SqlCmd.Parameters.Add("@Disc_Type", obj.Discount_Type);
            //SqlCmd.Parameters .Add("@Net_Amount",float.Parse(Net_Amount));
            SqlCmd.Parameters.Add("@Net_Amount", obj.Net_Amount);
            SqlCmd.Parameters.Add("@Promo_Scheme", obj.Promo_Scheme);
            SqlCmd.Parameters.Add("@Remark", obj.Remerk);
            SqlCmd.Parameters.Add("@Entry_By", obj.Entry_By);
            SqlCmd.Parameters.Add("@Entry_Time", obj.Entry_Time);
            SqlCmd.Parameters.Add("@Slip_No", obj.Slip_No);
            SqlCmd.Parameters.Add("@Cash_Discount", obj.Cash_Discount);
            SqlCmd.Parameters.Add("@Cash_Disc_Type", obj.Cash_Disc_Type);
            SqlCmd.Parameters.Add("@IGST_Amount", obj.VAT_Amount);
            SqlCmd.Parameters.Add("@ChallanNo", obj.ChallanNo);
            SqlCmd.Parameters.Add("@ChallanDate", System.Convert.ToDateTime(obj.ChallanDate));
            SqlCmd.Parameters.Add("@Credit_Limit", obj.Credit_Limit);
            /*bhal*/
            SqlCmd.Parameters.Add("@schdiscount", obj.schdiscount);
            //SqlCmd.Parameters .Add("@foediscount",float.Parse(foediscount));
            SqlCmd.Parameters.Add("@foediscount", obj.foediscount);
            SqlCmd.Parameters.Add("@foediscounttype", obj.foediscounttype);
            //SqlCmd.Parameters .Add("@foediscountrs",float.Parse(foediscountrs));
            SqlCmd.Parameters.Add("@foediscountrs", obj.foediscountrs);
            //SqlCmd.Parameters .Add("@totalqtyltr",float.Parse(totalqtyltr));
            SqlCmd.Parameters.Add("@totalqtyltr", obj.totalqtyltr);
            SqlCmd.Parameters.Add("@Order_No", obj.Order_No);
            SqlCmd.Parameters.Add("@SecSPDisc", obj.SecSPDisc);
            SqlCmd.Parameters.Add("@SGST_Amount", obj.SGST_Amount);
            SqlCmd.Parameters.Add("@CGST_Amount", obj.CGST_Amount);
            SqlCmd.ExecuteNonQuery();
        }


        /// <summary>
        /// Calls the Procedure ProOrderMasterEntry to insert the Order Details in Order_Col_Master.
        /// </summary>
        public void InsertOrderMaster()
        {
            SqlCmd = new SqlCommand("ProOrderMasterEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Order_No", Int32.Parse(Order_No));
            SqlCmd.Parameters.Add("@Order_Date", Order_Date);
            SqlCmd.Parameters.Add("@Sales_Type", Sales_Type);
            SqlCmd.Parameters.Add("@Under_SalesMan", Under_SalesMan);
            SqlCmd.Parameters.Add("@Cust_Name", Customer_Name);
            SqlCmd.Parameters.Add("@Place", Place);
            SqlCmd.Parameters.Add("@Vehicle_No", Vehicle_No);
            SqlCmd.Parameters.Add("@Grand_Total", Grand_Total);
            SqlCmd.Parameters.Add("@Discount", Discount);
            SqlCmd.Parameters.Add("@Disc_Type", Discount_Type);
            SqlCmd.Parameters.Add("@Net_Amount", Net_Amount);
            SqlCmd.Parameters.Add("@Promo_Scheme", Promo_Scheme);
            SqlCmd.Parameters.Add("@Remark", Remerk);
            SqlCmd.Parameters.Add("@Entry_By", Entry_By);
            SqlCmd.Parameters.Add("@Entry_Time", EntryTime);
            SqlCmd.Parameters.Add("@Slip_No", Slip_No);
            SqlCmd.Parameters.Add("@Cash_Discount", Cash_Discount);
            SqlCmd.Parameters.Add("@Cash_Disc_Type", Cash_Disc_Type);
            SqlCmd.Parameters.Add("@VAT_Amount", VAT_Amount);
            SqlCmd.Parameters.Add("@CGST_Amount", CGST_Amount);
            SqlCmd.Parameters.Add("@SGST_Amount", SGST_Amount);
            SqlCmd.Parameters.Add("@ChallanNo", ChallanNo);
            SqlCmd.Parameters.Add("@ChallanDate", System.Convert.ToDateTime(ChallanDate));
            SqlCmd.Parameters.Add("@Credit_Limit", Credit_Limit);
            SqlCmd.Parameters.Add("@schdiscount", schdiscount);
            SqlCmd.Parameters.Add("@foediscount", foediscount);
            SqlCmd.Parameters.Add("@foediscounttype", foediscounttype);
            SqlCmd.Parameters.Add("@foediscountrs", foediscountrs);
            SqlCmd.Parameters.Add("@totalqtyltr", totalqtyltr);
            SqlCmd.Parameters.Add("@SecSPDisc", SecSPDisc);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProSalesReturnMasterEntry to insert the Sales Return Invoice Details in 
        /// Sales_Return_Master table. after that update the customer credit limit,
        /// and also insert the value in CustomerLedgerTable and AccountsLedgerTable.
        /// </summary>
        public void InsertSalesReturnMaster()
        {
            SqlCmd = new SqlCommand("ProSalesReturnMasterEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", Int32.Parse(Invoice_No));
            SqlCmd.Parameters.Add("@Grand_Total", Grand_Total);
            SqlCmd.Parameters.Add("@Cash_Discount", Cash_Discount);
            SqlCmd.Parameters.Add("@Cash_Disc_Type", Cash_Disc_Type);
            SqlCmd.Parameters.Add("@VAT_Amount", VAT_Amount);
            SqlCmd.Parameters.Add("@CGST_Amount", CGST_Amount);
            SqlCmd.Parameters.Add("@SGST_Amount", SGST_Amount);
            SqlCmd.Parameters.Add("@Discount", Discount);
            SqlCmd.Parameters.Add("@Disc_Type", Discount_Type);
            SqlCmd.Parameters.Add("@Net_Amount", Net_Amount);
            SqlCmd.Parameters.Add("@Entry_By", Entry_By);
            SqlCmd.Parameters.Add("@Entry_Time", Entry_Time);
            SqlCmd.Parameters.Add("@Pre_Amount", Pre_Amount);
            SqlCmd.Parameters.Add("@schdiscount", schdiscount);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProPurchaseReturnMasterEntry to insert the Purchase Return Invoice Details in 
        /// Purchase_Return_Master table. after that update the vendor ledger with the help of UpdateVendorLedger
        /// procedure and also insert the value in AccountsLedgerTable.
        /// </summary>
        public void InsertPurchaseReturnMaster()
        {
            SqlCmd = new SqlCommand("ProPurchaseReturnMasterEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", Int32.Parse(Invoice_No));
            SqlCmd.Parameters.Add("@Grand_Total", Grand_Total);
            SqlCmd.Parameters.Add("@Cash_Discount", Cash_Discount);
            SqlCmd.Parameters.Add("@Cash_Disc_Type", Cash_Disc_Type);
            SqlCmd.Parameters.Add("@VAT_Amount", VAT_Amount);
            //SqlCmd.Parameters .Add("@Discount",float.Parse(Discount));
            SqlCmd.Parameters.Add("@Discount", Discount);
            SqlCmd.Parameters.Add("@Disc_Type", Discount_Type);
            //SqlCmd.Parameters .Add("@Net_Amount",float.Parse(Net_Amount));
            SqlCmd.Parameters.Add("@Net_Amount", Net_Amount);
            SqlCmd.Parameters.Add("@Entry_By", Entry_By);
            SqlCmd.Parameters.Add("@Entry_Time", Entry_Time);
            SqlCmd.Parameters.Add("@Pre_Amount", Pre_Amount);
            //***********bhal add*****************
            SqlCmd.Parameters.Add("@Foc_Discount", Foc_Discount);
            SqlCmd.Parameters.Add("@Foc_Discount_Type", Foc_Discount_Type);
            SqlCmd.Parameters.Add("@Entry_Tax1", Entry_Tax1);
            SqlCmd.Parameters.Add("@Entry_Tax_Type", Entry_Tax_Type);
            SqlCmd.Parameters.Add("@Ebird", Ebird);
            SqlCmd.Parameters.Add("@Ebird_Discount", Ebird_Discount);
            SqlCmd.Parameters.Add("@Tradeval", Tradeval);
            SqlCmd.Parameters.Add("@Trade_Discount", Trade_Discount);
            //*************bhal end*************************	
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure FuelReportDuplicate1 to insert the Fuel Purchase Invoice Details 
        /// This method is not used.
        /// </summary>
        public void InsertFuelDDupli()
        {
            SqlCmd = new SqlCommand("FuelReportDuplicate1", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@InvoiceNo", InvoiceNo);
            SqlCmd.Parameters.Add("@InvoiceDate", InvoiceDate);
            SqlCmd.Parameters.Add("@VendorName", VendorName);
            SqlCmd.Parameters.Add("@VendorInvoiceNo", VendorInvoiceNo);
            SqlCmd.Parameters.Add("@Place", Place);
            SqlCmd.Parameters.Add("@VendorInvoiceDate", VendorInvoiceDate);
            SqlCmd.Parameters.Add("@VehicleNo", VehicleNo);
            SqlCmd.Parameters.Add("@ProdName1", ProdName1);
            SqlCmd.Parameters.Add("@ProdName2", ProdName2);
            SqlCmd.Parameters.Add("@ProdName3", ProdName3);
            SqlCmd.Parameters.Add("@ProdName4", ProdName4);
            SqlCmd.Parameters.Add("@QtyInLtr1", QtyInLtr1);
            SqlCmd.Parameters.Add("@QtyInLtr2", QtyInLtr2);
            SqlCmd.Parameters.Add("@QtyInLtr3", QtyInLtr3);
            SqlCmd.Parameters.Add("@QtyInLtr4", QtyInLtr4);
            SqlCmd.Parameters.Add("@Rate1", Rate1);
            SqlCmd.Parameters.Add("@Rate2", Rate2);
            SqlCmd.Parameters.Add("@Rate3", Rate3);
            SqlCmd.Parameters.Add("@Rate4", Rate4);
            SqlCmd.Parameters.Add("@ReducOther1", ReducOther1);
            SqlCmd.Parameters.Add("@ReducOther2", ReducOther2);
            SqlCmd.Parameters.Add("@ReducOther3", ReducOther3);
            SqlCmd.Parameters.Add("@ReducOther4", ReducOther4);
            SqlCmd.Parameters.Add("@EntryTax1", EntryTax1);
            SqlCmd.Parameters.Add("@EntryTax2", EntryTax2);
            SqlCmd.Parameters.Add("@EntryTax3", EntryTax3);
            SqlCmd.Parameters.Add("@EntryTax4", EntryTax4);
            SqlCmd.Parameters.Add("@RpgCharge1", RpgCharge1);
            SqlCmd.Parameters.Add("@RpgCharge2", RpgCharge2);
            SqlCmd.Parameters.Add("@RpgCharge3", RpgCharge3);
            SqlCmd.Parameters.Add("@RpgCharge4", RpgCharge4);
            SqlCmd.Parameters.Add("@Ltc1", Ltc1);
            SqlCmd.Parameters.Add("@Ltc2", Ltc2);
            SqlCmd.Parameters.Add("@Ltc3", Ltc3);
            SqlCmd.Parameters.Add("@Ltc4", Ltc4);
            SqlCmd.Parameters.Add("@TranCharge1", TranCharge1);
            SqlCmd.Parameters.Add("@TranCharge2", TranCharge2);
            SqlCmd.Parameters.Add("@TranCharge3", TranCharge3);
            SqlCmd.Parameters.Add("@TranCharge4", TranCharge4);
            SqlCmd.Parameters.Add("@Olv1", Olv1);
            SqlCmd.Parameters.Add("@Olv2", Olv2);
            SqlCmd.Parameters.Add("@Olv3", Olv3);
            SqlCmd.Parameters.Add("@Olv4", Olv4);
            SqlCmd.Parameters.Add("@Lst1", Lst1);
            SqlCmd.Parameters.Add("@Lst2", Lst2);
            SqlCmd.Parameters.Add("@Lst3", Lst3);
            SqlCmd.Parameters.Add("@Lst4", Lst4);
            SqlCmd.Parameters.Add("@Lfr1", Lfr1);
            SqlCmd.Parameters.Add("@Lfr2", Lfr2);
            SqlCmd.Parameters.Add("@Lfr3", Lfr3);
            SqlCmd.Parameters.Add("@Lfr4", Lfr4);
            SqlCmd.Parameters.Add("@DfbCharge1", DfbCharge1);
            SqlCmd.Parameters.Add("@DfbCharge2", DfbCharge2);
            SqlCmd.Parameters.Add("@DfbCharge3", DfbCharge3);
            SqlCmd.Parameters.Add("@DfbCharge4", DfbCharge4);
            SqlCmd.Parameters.Add("@DenPhy1", DenPhy1);
            SqlCmd.Parameters.Add("@DenPhy2", DenPhy2);
            SqlCmd.Parameters.Add("@DenPhy3", DenPhy3);
            SqlCmd.Parameters.Add("@DenPhy4", DenPhy4);
            SqlCmd.Parameters.Add("@DenP1", DenP1);
            SqlCmd.Parameters.Add("@DenP2", DenP2);
            SqlCmd.Parameters.Add("@DenP3", DenP3);
            SqlCmd.Parameters.Add("@DenP4", DenP4);
            SqlCmd.Parameters.Add("@TemPhy1", TemPhy1);
            SqlCmd.Parameters.Add("@TemPhy2", TemPhy2);
            SqlCmd.Parameters.Add("@TemPhy3", TemPhy3);
            SqlCmd.Parameters.Add("@TemPhy4", TemPhy4);
            SqlCmd.Parameters.Add("@ConvDenPhy1 ", ConvDenPhy1);
            SqlCmd.Parameters.Add("@ConvDenPhy2 ", ConvDenPhy2);
            SqlCmd.Parameters.Add("@ConvDenPhy3 ", ConvDenPhy3);
            SqlCmd.Parameters.Add("@ConvDenPhy4 ", ConvDenPhy4);
            SqlCmd.Parameters.Add("@DenInv1", DenInv1);
            SqlCmd.Parameters.Add("@DenInv2", DenInv2);
            SqlCmd.Parameters.Add("@DenInv3 ", DenInv3);
            SqlCmd.Parameters.Add("@DenInv4 ", DenInv4);
            SqlCmd.Parameters.Add("@TemInv1 ", TemInv1);
            SqlCmd.Parameters.Add("@TemInv2 ", TemInv2);
            SqlCmd.Parameters.Add("@TemInv3 ", TemInv3);
            SqlCmd.Parameters.Add("@TemInv4 ", TemInv4);
            SqlCmd.Parameters.Add("@ConvDenInv1", ConvDenInv1);
            SqlCmd.Parameters.Add("@ConvDenInv2", ConvDenInv2);
            SqlCmd.Parameters.Add("@ConvDenInv3", ConvDenInv3);
            SqlCmd.Parameters.Add("@ConvDenInv4", ConvDenInv4);
            SqlCmd.Parameters.Add("@DensVaria1 ", DensVaria1);
            SqlCmd.Parameters.Add("@DensVaria2 ", DensVaria2);
            SqlCmd.Parameters.Add("@DensVaria3 ", DensVaria3);
            SqlCmd.Parameters.Add("@DensVaria4 ", DensVaria4);
            SqlCmd.Parameters.Add("@DenAftDec1 ", DenAftDec1);
            SqlCmd.Parameters.Add("@DenAftDec2 ", DenAftDec2);
            SqlCmd.Parameters.Add("@DenAftDec3 ", DenAftDec3);
            SqlCmd.Parameters.Add("@DenAftDec4 ", DenAftDec4);
            SqlCmd.Parameters.Add("@TempAftDec1 ", TempAftDec1);
            SqlCmd.Parameters.Add("@TempAftDec2 ", TempAftDec2);
            SqlCmd.Parameters.Add("@TempAftDec3 ", TempAftDec3);
            SqlCmd.Parameters.Add("@TempAftDec4 ", TempAftDec4);
            SqlCmd.Parameters.Add("@ConvDenAft1 ", ConvDenAft1);
            SqlCmd.Parameters.Add("@ConvDenAft2 ", ConvDenAft2);
            SqlCmd.Parameters.Add("@ConvDenAft3 ", ConvDenAft3);
            SqlCmd.Parameters.Add("@ConvDenAft4 ", ConvDenAft4);
            SqlCmd.Parameters.Add("@Discount ", Discount);
            SqlCmd.Parameters.Add("@TotalAmount ", TotalAmount);
            SqlCmd.Parameters.Add("@TotalAmount1 ", TotalAmount1);
            SqlCmd.Parameters.Add("@TotalAmount2 ", TotalAmount2);
            SqlCmd.Parameters.Add("@TotalAmount3 ", TotalAmount3);
            SqlCmd.Parameters.Add("@NetAmount ", NetAmount);
            SqlCmd.Parameters.Add("@Promo ", Promo);
            SqlCmd.Parameters.Add("@Remarks ", Remarks);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProSalesMasterUpdate to Update the Sales Invoice Details in Sales_Master.
        /// after that delete the invoice no from Sales_Details table and update the credit limit in 
        /// customer table and insert the value in CustomerLedgerTable and AccountLedgerTable with the 
        /// help of ProInsertAccountsLedger procedure.
        /// </summary>
        public void UpdateSalesMaster(SalesSaveDetailsModel obj)
        {
            SqlCmd = new SqlCommand("ProSalesMasterUpdate", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", obj.Invoice_No);
            SqlCmd.Parameters.Add("@Invoice_Date", obj.Invoice_Date);
            SqlCmd.Parameters.Add("@Sales_Type", obj.Sales_Type);
            SqlCmd.Parameters.Add("@Under_SalesMan", obj.Under_SalesMan);
            SqlCmd.Parameters.Add("@Cust_Name", obj.Customer_Name);
            SqlCmd.Parameters.Add("@Place", obj.Place);
            SqlCmd.Parameters.Add("@Vehicle_No", obj.Vehicle_No);
            SqlCmd.Parameters.Add("@Grand_Total", obj.Grand_Total);
            SqlCmd.Parameters.Add("@Discount", obj.Discount);
            SqlCmd.Parameters.Add("@Disc_Type", obj.Discount_Type);
            SqlCmd.Parameters.Add("@Net_Amount", obj.Net_Amount);
            SqlCmd.Parameters.Add("@Promo_Scheme", obj.Promo_Scheme);
            SqlCmd.Parameters.Add("@Remark", obj.Remerk);
            SqlCmd.Parameters.Add("@Entry_By", obj.Entry_By);
            SqlCmd.Parameters.Add("@Entry_Time", obj.Entry_Time);
            SqlCmd.Parameters.Add("@Slip_No", obj.Slip_No);
            SqlCmd.Parameters.Add("@ChallanNo", obj.ChallanNo);
            SqlCmd.Parameters.Add("@ChallanDate", System.Convert.ToDateTime(obj.ChallanDate));
            SqlCmd.Parameters.Add("@Cash_Discount", obj.Cash_Discount);
            SqlCmd.Parameters.Add("@Cash_Disc_Type", obj.Cash_Disc_Type);
            SqlCmd.Parameters.Add("@IGST_Amount", obj.VAT_Amount);
            SqlCmd.Parameters.Add("@Credit_Limit", obj.Credit_Limit);
            /*bhal*/
            SqlCmd.Parameters.Add("@schdiscount", obj.schdiscount);
            //SqlCmd.Parameters .Add("@foediscount",float.Parse(foediscount));
            SqlCmd.Parameters.Add("@foediscount", obj.foediscount);
            SqlCmd.Parameters.Add("@foediscounttype", obj.foediscounttype);
            //SqlCmd.Parameters .Add("@foediscountrs",float.Parse(foediscountrs));
            SqlCmd.Parameters.Add("@foediscountrs", obj.foediscountrs);
            //SqlCmd.Parameters .Add("@totalqtyltr",float.Parse(totalqtyltr));
            SqlCmd.Parameters.Add("@totalqtyltr", obj.totalqtyltr);
            SqlCmd.Parameters.Add("@SecSPDisc", obj.SecSPDisc);
            SqlCmd.Parameters.Add("@SGST_Amount", obj.SGST_Amount);
            SqlCmd.Parameters.Add("@CGST_Amount", obj.CGST_Amount);
            SqlCmd.ExecuteNonQuery();
        }



        /// <summary>
        /// Calls the Procedure ProOrderMasterUpdate to Update the Order Details in Order_Col_Master.
        /// after that delete the order no from order_col_details table.
        /// </summary>
        public void UpdateOrderMaster()
        {
            SqlCmd = new SqlCommand("ProOrderMasterUpdate", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Order_No", Order_No);
            SqlCmd.Parameters.Add("@Order_Date", Order_Date);
            SqlCmd.Parameters.Add("@Sales_Type", Sales_Type);
            SqlCmd.Parameters.Add("@Under_SalesMan", Under_SalesMan);
            SqlCmd.Parameters.Add("@Cust_Name", Customer_Name);
            SqlCmd.Parameters.Add("@Place", Place);
            SqlCmd.Parameters.Add("@Vehicle_No", Vehicle_No);
            SqlCmd.Parameters.Add("@Grand_Total", Grand_Total);
            SqlCmd.Parameters.Add("@Discount", Discount);
            SqlCmd.Parameters.Add("@Disc_Type", Discount_Type);
            SqlCmd.Parameters.Add("@Net_Amount", Net_Amount);
            SqlCmd.Parameters.Add("@Promo_Scheme", Promo_Scheme);
            SqlCmd.Parameters.Add("@Remark", Remerk);
            SqlCmd.Parameters.Add("@Entry_By", Entry_By);
            SqlCmd.Parameters.Add("@Entry_Time", EntryTime);
            SqlCmd.Parameters.Add("@Slip_No", Slip_No);
            SqlCmd.Parameters.Add("@ChallanNo", ChallanNo);
            SqlCmd.Parameters.Add("@ChallanDate", System.Convert.ToDateTime(ChallanDate));
            SqlCmd.Parameters.Add("@Cash_Discount", Cash_Discount);
            SqlCmd.Parameters.Add("@Cash_Disc_Type", Cash_Disc_Type);
            SqlCmd.Parameters.Add("@VAT_Amount", VAT_Amount);
            SqlCmd.Parameters.Add("@CGST_Amount", CGST_Amount);
            SqlCmd.Parameters.Add("@SGST_Amount", SGST_Amount);
            SqlCmd.Parameters.Add("@Credit_Limit", Credit_Limit);
            SqlCmd.Parameters.Add("@schdiscount", schdiscount);
            SqlCmd.Parameters.Add("@foediscount", foediscount);
            SqlCmd.Parameters.Add("@foediscounttype", foediscounttype);
            SqlCmd.Parameters.Add("@foediscountrs", foediscountrs);
            SqlCmd.Parameters.Add("@totalqtyltr", totalqtyltr);
            SqlCmd.Parameters.Add("@SecSPDisc", SecSPDisc);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProUpdateCustomeBalance to Update the Customer Balance.
        /// </summary>
        public void UpdateCustomerBalance()
        {
            SqlCmd = new SqlCommand("ProUpdateCustomeBalance", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Cust_Name", Customer_Name);
            SqlCmd.Parameters.Add("@Place", Place);
            SqlCmd.Parameters.Add("@Cr_Plus", Cr_Plus);
            SqlCmd.Parameters.Add("@Dr_Plus", Dr_Plus);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Call the Procedure ProSalesDetailsEntry to insert the products details in Sales_Details table
        /// and update the stock in every product with the help of ProStockUpdateAfterSales Procedure.
        /// </summary>
        public void InsertSalesDetail()
        {
            SqlCmd = new SqlCommand("ProSalesDetailsEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", Invoice_No);
            SqlCmd.Parameters.Add("@Prod_Name", Product_Name);
            SqlCmd.Parameters.Add("@Pack_Type", Package_Type);
            SqlCmd.Parameters.Add("@Qty", Qty);
            SqlCmd.Parameters.Add("@sno", sno);
            SqlCmd.Parameters.Add("@Rate", Rate);
            SqlCmd.Parameters.Add("@Amount", Amount);
            SqlCmd.Parameters.Add("@Qty1", QtyTemp);
            //SqlCmd.Parameters .Add("@OldQty",tempQty);
            //SqlCmd.Parameters .Add("@ProductID",prod_id);
            //SqlCmd.Parameters.Add("@Invoice_Date",Inv_date); 
            SqlCmd.Parameters.Add("@Invoice_Date", Invoice_Date);
            /*bhal*/
            SqlCmd.Parameters.Add("@scheme1", sch);
            /*bhal*/
            SqlCmd.Parameters.Add("@foe", foe);
            SqlCmd.Parameters.Add("@SchType", schtype); /* add by Mahesh on 16.01.009:5.17 PM */
            SqlCmd.Parameters.Add("@SecSPDisc", SecSPDisc); /* add by Mahesh on 16.01.009:5.17 PM */
            SqlCmd.Parameters.Add("@SecSPDiscType", SecSPDiscType); /* add by Mahesh on 16.01.009:5.17 PM */
            SqlCmd.Parameters.Add("@FoeType", foediscounttype); /* add by Mahesh on 16.01.009:5.17 PM */
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Call the Procedure ProOrderDetailsEntry to insert the Order details in Order_col_Details table
        /// </summary>
        public void InsertOrderDetail()
        {
            SqlCmd = new SqlCommand("ProOrderDetailsEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Order_No", Order_No);
            SqlCmd.Parameters.Add("@Prod_Name", Product_Name);
            SqlCmd.Parameters.Add("@Pack_Type", Package_Type);
            SqlCmd.Parameters.Add("@Qty", Qty);
            SqlCmd.Parameters.Add("@sno", sno);
            SqlCmd.Parameters.Add("@Rate", Rate);
            SqlCmd.Parameters.Add("@Amount", Amount);
            SqlCmd.Parameters.Add("@Qty1", QtyTemp);
            SqlCmd.Parameters.Add("@Order_Date", Order_Date);
            SqlCmd.Parameters.Add("@scheme1", sch);
            SqlCmd.Parameters.Add("@foe", foe);
            SqlCmd.Parameters.Add("@SchType", schtype); /* add by Mahesh on 22.01.009:3.17 PM */
            SqlCmd.Parameters.Add("@SecSPDisc", SecSPDisc); /* add by Mahesh on 22.01.009:3.17 PM */
            SqlCmd.Parameters.Add("@SecSPDiscType", SecSPDiscType); /* add by Mahesh on 22.01.009:3.17 PM */
            SqlCmd.Parameters.Add("@FoeType", foediscounttype); /* add by Mahesh on 22.01.009:3.17 PM */
            SqlCmd.ExecuteNonQuery();
        }

        //*****bhal*********
        /// <summary>
        /// Call the Procedure ProSalesDetailsschemeEntry to insert the products details in Sales_Details table
        /// and update the stock in every product with the help of ProStockUpdateAfterSales Procedure.
        /// </summary>
        public void InsertSaleSchemeDetail()
        {
            SqlCmd = new SqlCommand("ProSalesDetailschemeEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", Invoice_No);
            //SqlCmd.Parameters.Add("@Invoice_Date",Inv_date); 
            SqlCmd.Parameters.Add("@Invoice_Date", Invoice_Date);
            SqlCmd.Parameters.Add("@sno", sno);
            SqlCmd.Parameters.Add("@Prod_Name", Product_Name);
            SqlCmd.Parameters.Add("@Pack_Type", Package_Type);
            SqlCmd.Parameters.Add("@Qty1", tempQty);
            SqlCmd.Parameters.Add("@Qty", Qty);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Call the Procedure ProSalesReturnDetailsSchemeEntry to insert the products details in Sales_Return_Details 
        /// table and update the stock in every product with the help of ProStockUpdateAfterSales Procedure.
        /// </summary>
        public void InsertSaleReturnSchemeDetail()
        {
            SqlCmd = new SqlCommand("ProSalesReturnDetailsSchemeEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", Invoice_No);
            SqlCmd.Parameters.Add("@Invoice_Date", System.Convert.ToDateTime(Inv_date));
            SqlCmd.Parameters.Add("@Prod_Name", Product_Name);
            SqlCmd.Parameters.Add("@Pack_Type", Package_Type);
            //SqlCmd.Parameters .Add("@Qty1",QtyTemp);
            SqlCmd.Parameters.Add("@Qty", Qty);
            SqlCmd.Parameters.Add("@scheme1", scheme1);
            SqlCmd.ExecuteNonQuery();
        }

        //*****bhal*******/
        /// <summary>
        /// Call the Procedure ProSalesReturnDetailsEntry to insert the products details in Sales_Return_Details 
        /// table and update the stock in every product with the help of ProStockUpdateAfterSales Procedure.
        /// </summary>
        public void InsertSalesReturnDetail()
        {
            SqlCmd = new SqlCommand("ProSalesReturnDetailsEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", Invoice_No);
            SqlCmd.Parameters.Add("@Prod_Name", Product_Name);
            SqlCmd.Parameters.Add("@Pack_Type", Package_Type);
            SqlCmd.Parameters.Add("@Qty", Qty);
            SqlCmd.Parameters.Add("@Rate", Rate);
            SqlCmd.Parameters.Add("@Amount", Amount);
            SqlCmd.Parameters.Add("@scheme1", scheme1);
            //	SqlCmd.Parameters .Add("@Qty1",QtyTemp);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Call the Procedure ProPurchaseReturnDetailsEntry to insert the products details in Purchase_Return_Details 
        /// table and update the stock in every product with the help of ProStockUpdateAfterSales Procedure.
        /// </summary>
        public void InsertPurchaseReturnDetail()
        {
            SqlCmd = new SqlCommand("ProPurchaseReturnDetailsEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", Invoice_No);
            SqlCmd.Parameters.Add("@Prod_Name", Product_Name);
            SqlCmd.Parameters.Add("@Pack_Type", Package_Type);
            SqlCmd.Parameters.Add("@Qty", Qty);
            SqlCmd.Parameters.Add("@Rate", Rate);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProSalesDetailsUpdate to Update the Sales Invoice Details.
        /// This method is not used.
        /// </summary>
        public void UpdateSalesDetail()
        {
            SqlCmd = new SqlCommand("ProSalesDetailsUpdate", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", Invoice_No);
            SqlCmd.Parameters.Add("@Prod_Name", Product_Name);
            SqlCmd.Parameters.Add("@Pack_Type", Package_Type);
            SqlCmd.Parameters.Add("@Qty", Qty);
            SqlCmd.Parameters.Add("@Rate", Rate);
            SqlCmd.Parameters.Add("@Amount", Amount);
            //*bhal*/	SqlCmd.Parameters.Add("@scheme1",sch); 
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Returns the SqlDataReader object containing the max. Purchase Invoice ID from Purchase Master
        /// </summary>
        /// <returns></returns>
        public SqlDataReader GetNextPurchaseInvoiceNo()
        {
            SqlCmd = new SqlCommand("select max(Invoice_No)+1 from Purchase_Master", SqlCon);
            SqlDtr = SqlCmd.ExecuteReader();
            return SqlDtr;
        }

        /// <summary>
        /// Calls the Procedure ProPurchaseMasterEntry to insert the Purchase Invoice Details in Purchase_Master.
        /// and insert the value in VendorLedgerTable and also insert the value in AccountsLedgerTable with the 
        /// help of ProInsertAccountsLedger procedure.
        /// </summary>
        public void InsertPurchaseMaster(PurchaseModels purchase)
        {
            SqlCmd = new SqlCommand("ProPurchaseMasterEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", Int32.Parse(purchase.InvoiceNo));
            SqlCmd.Parameters.Add("@Invoice_Date", purchase.InvoiceDate);
            SqlCmd.Parameters.Add("@Mode_of_Payment", purchase.ModeofPayment);
            SqlCmd.Parameters.Add("@Vendor_Name", purchase.VendorName);
            SqlCmd.Parameters.Add("@City", purchase.City);
            SqlCmd.Parameters.Add("@Vehicle_No", purchase.VehicleNo.Length > 0 ? purchase.VehicleNo : "");
            SqlCmd.Parameters.Add("@Vndr_Invoice_No", purchase.VendorInvoiceNo);
            SqlCmd.Parameters.Add("@Vndr_Invoice_Date", purchase.VendorInvoiceDate);
            //SqlCmd.Parameters .Add("@Grand_Total",float.Parse(Grand_Total) );
            SqlCmd.Parameters.Add("@Grand_Total", purchase.GrandTotal);
            //SqlCmd.Parameters .Add("@Discount",float.Parse(Discount));
            SqlCmd.Parameters.Add("@Discount", purchase.Discount);
            SqlCmd.Parameters.Add("@Disc_Type", purchase.DiscountType);
            //SqlCmd.Parameters .Add("@Net_Amount",float.Parse(Net_Amount));
            SqlCmd.Parameters.Add("@Net_Amount", purchase.NetAmount);
            SqlCmd.Parameters.Add("@Promo_Scheme", purchase.PromoScheme);
            SqlCmd.Parameters.Add("@Remark", purchase.Remark);
            SqlCmd.Parameters.Add("@Entry_By", purchase.EntryBy);
            SqlCmd.Parameters.Add("@Entry_Time", purchase.EntryTime);
            SqlCmd.Parameters.Add("@Cash_Discount", purchase.CashDiscount);
            SqlCmd.Parameters.Add("@Cash_Disc_Type", purchase.CashDiscType);
            SqlCmd.Parameters.Add("@IGST_Amount", purchase.VATAmount);
            //****************************
            SqlCmd.Parameters.Add("@Foc_Discount", purchase.FocDiscount);
            SqlCmd.Parameters.Add("@Foc_Discount_Type", purchase.FocDiscountType);
            //SqlCmd.Parameters .Add("@Entry_Tax1",Entry_Tax1);
            //SqlCmd.Parameters .Add("@Entry_Tax_Type",Entry_Tax_Type);
            SqlCmd.Parameters.Add("@Ebird", purchase.Ebird);
            SqlCmd.Parameters.Add("@Ebird_Discount", purchase.EbirdDiscount);
            SqlCmd.Parameters.Add("@Tradeval", purchase.Tradeval);
            SqlCmd.Parameters.Add("@Trade_Discount", purchase.TradeDiscount);
            SqlCmd.Parameters.Add("@fixed_Discount", purchase.FixedDiscount);
            SqlCmd.Parameters.Add("@fixed_Discount_Type", purchase.FixedDiscountType);
            SqlCmd.Parameters.Add("@totalqtyltr", purchase.Totalqtyltr);

            /*******Add by vikas 5.11.2012****************/
            SqlCmd.Parameters.Add("@New_fixdisc", purchase.NewFixeddisc);
            SqlCmd.Parameters.Add("@New_fixdamount", purchase.NewFixeddiscAmount);
            SqlCmd.Parameters.Add("@CGST_Amount", purchase.CGSTAmount);
            SqlCmd.Parameters.Add("@SGST_Amount", purchase.SGSTAmount);
            SqlCmd.Parameters.Add("@Tradeless", purchase.Tradeless);
            SqlCmd.Parameters.Add("@Birdless", purchase.Birdless);

            /********end***************/

            //			SqlCmd.Parameters .Add("@Birdless",Birdless);

            //**************************************
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProPurchaseMasterEntry to update the Purchase Invoice Details in Purchase_Master.
        /// and update the value in VendorLedgerTable and also update the value in AccountsLedgerTable with the 
        /// help of ProUpdateAccountsLedger procedure.
        /// </summary>
        public void UpdatePurchaseMaster()
        {
            SqlCmd = new SqlCommand("ProPurchaseMasterUpdate", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", Int32.Parse(Invoice_No));
            SqlCmd.Parameters.Add("@Invoice_Date", Invoice_Date);
            SqlCmd.Parameters.Add("@Mode_of_Payment", Mode_of_Payment);
            SqlCmd.Parameters.Add("@Vendor_Name", Vendor_Name);
            SqlCmd.Parameters.Add("@City", City);
            SqlCmd.Parameters.Add("@Vehicle_No", Vehicle_No.Length > 0 ? Vehicle_No : "");
            SqlCmd.Parameters.Add("@Vndr_Invoice_No", Vendor_Invoice_No);
            SqlCmd.Parameters.Add("@Vndr_Invoice_Date", getdate(Vendor_Invoice_Date, true));
            //SqlCmd.Parameters .Add("@Grand_Total",float.Parse(Grand_Total) );
            SqlCmd.Parameters.Add("@Grand_Total", Grand_Total);
            SqlCmd.Parameters.Add("@Discount", Discount);
            SqlCmd.Parameters.Add("@Disc_Type", Discount_Type);
            //SqlCmd.Parameters .Add("@Net_Amount",float.Parse(Net_Amount));
            SqlCmd.Parameters.Add("@Net_Amount", Net_Amount);
            SqlCmd.Parameters.Add("@Promo_Scheme", Promo_Scheme);
            SqlCmd.Parameters.Add("@Remark", Remerk);
            SqlCmd.Parameters.Add("@Entry_By", Entry_By);
            SqlCmd.Parameters.Add("@Entry_Time", Entry_Time);
            SqlCmd.Parameters.Add("@CGST_Amount", CGST_Amount);
            SqlCmd.Parameters.Add("@SGST_Amount", SGST_Amount);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure MasterUpdatePurchaseMaster to update the Purchase Invoice Details in Purchase_Master.
        /// after that delete the purchase invoice no from Purchase_Details table
        /// and update the value in VendorLedgerTable and also update the value in AccountsLedgerTable with the 
        /// help of ProUpdateAccountsLedger procedure.
        /// </summary>
        public void updateMasterPurchase(PurchaseModels purchase)
        {
            SqlCmd = new SqlCommand("MasterUpdatePurchaseMaster", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", purchase.InvoiceNo);
            SqlCmd.Parameters.Add("@Invoice_Date", purchase.InvoiceDate);
            SqlCmd.Parameters.Add("@Vehicle_No", purchase.VehicleNo);
            SqlCmd.Parameters.Add("@Vndr_Invoice_No", purchase.VendorInvoiceNo);
            SqlCmd.Parameters.Add("@Vndr_Invoice_Date", purchase.VendorInvoiceDate);
            SqlCmd.Parameters.Add("@Promo_Scheme", purchase.PromoScheme);
            SqlCmd.Parameters.Add("@Remarks", purchase.Remark);
            SqlCmd.Parameters.Add("@Grand_Total", purchase.GrandTotal);
            SqlCmd.Parameters.Add("@Discount", purchase.Discount);
            SqlCmd.Parameters.Add("@Discount_Type", purchase.DiscountType);
            SqlCmd.Parameters.Add("@Net_Amount", purchase.NetAmount);
            SqlCmd.Parameters.Add("@Cash_Discount", purchase.CashDiscount);
            SqlCmd.Parameters.Add("@Cash_Disc_Type", purchase.CashDiscType);
            SqlCmd.Parameters.Add("@IGST_Amount", purchase.VATAmount);
            SqlCmd.Parameters.Add("@Vendor_Name", purchase.VendorName);
            SqlCmd.Parameters.Add("@City", purchase.City);
            //*************************
            SqlCmd.Parameters.Add("@Foc_Discount", purchase.FocDiscount);
            SqlCmd.Parameters.Add("@Foc_Discount_Type", purchase.FocDiscountType);
            //SqlCmd.Parameters .Add("@Entry_Tax1",Entry_Tax1);
            //SqlCmd.Parameters .Add("@Entry_Tax_Type",Entry_Tax_Type);
            SqlCmd.Parameters.Add("@Ebird", purchase.Ebird);
            SqlCmd.Parameters.Add("@Ebird_Discount", purchase.EbirdDiscount);
            SqlCmd.Parameters.Add("@Tradeval", purchase.Tradeval);
            SqlCmd.Parameters.Add("@Trade_Discount", purchase.TradeDiscount);

            //			SqlCmd.Parameters .Add("@Birdless",Birdless);
            SqlCmd.Parameters.Add("@fixed_Discount", purchase.FixedDiscount);
            SqlCmd.Parameters.Add("@fixed_Discount_Type", purchase.FixedDiscountType);
            SqlCmd.Parameters.Add("@totalqtyltr", purchase.Totalqtyltr);
            SqlCmd.Parameters.Add("@Mode_of_Payment", purchase.ModeofPayment);
            /*******Add by vikas 5.11.2012****************/
            SqlCmd.Parameters.Add("@New_fixdisc", purchase.NewFixeddisc);
            SqlCmd.Parameters.Add("@New_fixdamount", purchase.NewFixeddiscAmount);
            SqlCmd.Parameters.Add("@CGST_Amount", purchase.CGSTAmount);
            SqlCmd.Parameters.Add("@SGST_Amount", purchase.SGSTAmount);
            SqlCmd.Parameters.Add("@Tradeless", purchase.Tradeless);
            SqlCmd.Parameters.Add("@Birdless", purchase.Birdless);
            /********end***************/

            //***************************	
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure MasterUpdateProducts to Update the Product Master Details
        /// This method is not used.
        /// </summary>
        public void updateMasterProduct()
        {
            SqlCmd = new SqlCommand("MasterUpdateProducts", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Prod_Name", Prod_Name);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProPurchaseDetailsEntry to insert the Products value in Purchase Details
        /// table and update the stock in every product with the help of ProStockUpdateAfterSales Procedure.
        /// </summary>
        public void InsertPurchaseDetail(ProductModels product)
        {
            SqlCmd = new SqlCommand("ProPurchaseDetailsEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", product.Invoice_No);
            SqlCmd.Parameters.Add("@Prod_Name", product.ProductName);
            SqlCmd.Parameters.Add("@Pack_Type", product.Package_Type);
            SqlCmd.Parameters.Add("@Qty", product.Quantity);
            SqlCmd.Parameters.Add("@Rate", product.Rate);
            SqlCmd.Parameters.Add("@Amount", product.Amount);
            //********
            SqlCmd.Parameters.Add("@foc", product.FOC);
            SqlCmd.Parameters.Add("@Invoice_Date", product.InvoiceDate);
            //********
            SqlCmd.Parameters.Add("@SNo", product.Sno);
            SqlCmd.Parameters.Add("@PerDisc", product.SchPerDisc);
            SqlCmd.Parameters.Add("@PerDiscType", product.SchPerDiscType);
            SqlCmd.Parameters.Add("@StktDisc", product.StockDiscount);
            SqlCmd.Parameters.Add("@StktDiscType", product.SchStktDiscType);
            SqlCmd.Parameters.Add("@Discount", product.Discount);
            //********
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProPurchaseDetailsUpdate to Update the products details in Purchase Details
        /// table and update the stock in every product with the help of ProStockUpdateAfterSales Procedure.
        /// </summary>
        public void UpdatePurchaseDetail(ProductModels product)
        {
            SqlCmd = new SqlCommand("ProPurchaseDetailsupdate", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", product.Invoice_No);
            SqlCmd.Parameters.Add("@Prod_Name", product.ProductName);
            SqlCmd.Parameters.Add("@Pack_Type", product.Package_Type);
            SqlCmd.Parameters.Add("@Qty", product. Quantity);
            SqlCmd.Parameters.Add("@Rate", product.Rate);
            SqlCmd.Parameters.Add("@Amount", product.Amount);
            SqlCmd.Parameters.Add("@Qty1", product.QtyTemp);
            SqlCmd.Parameters.Add("@Inv_Date", product.InvoiceDate);
            /*bhal*/
            SqlCmd.Parameters.Add("@foc", product.FOC);
            //********
            SqlCmd.Parameters.Add("@SNo", product.Sno);
            SqlCmd.Parameters.Add("@PerDisc", product.SchPerDisc);
            SqlCmd.Parameters.Add("@PerDiscType", product.SchPerDiscType);
            SqlCmd.Parameters.Add("@StktDisc", product.StockDiscount);
            SqlCmd.Parameters.Add("@StktDiscType", product.SchStktDiscType);
            SqlCmd.Parameters.Add("@Discount", product.Discount);
            //********
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProFuelPurchaseDetailsEntry to insert the Fuel Purchase Details.
        /// This method is not used.
        /// </summary>
        public void InsertFuelPurchaseDetail()
        {
            SqlCmd = new SqlCommand("ProFuelPurchaseDetailsEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Vendor_Name", Vendor_Name);
            SqlCmd.Parameters.Add("@City", City);
            SqlCmd.Parameters.Add("@Invoice_No", Invoice_No);
            SqlCmd.Parameters.Add("@Compartment", Compartment);
            SqlCmd.Parameters.Add("@Prod_Name", Product_Name);
            SqlCmd.Parameters.Add("@Density_in_Physical", Density_in_Physical);
            SqlCmd.Parameters.Add("@Temp_in_Physical", Temp_in_Physical);
            SqlCmd.Parameters.Add("@conv_Density_Phy", Converted_Density_Phy);
            SqlCmd.Parameters.Add("@Density_in_Invoice_conv", Density_in_Invoice_Conv);
            SqlCmd.Parameters.Add("@Density_Variation", Density_Variation);
            SqlCmd.Parameters.Add("@Den_After_Dec", Density_After_Dec);
            SqlCmd.Parameters.Add("@Temp_After_Dec", Temp_After_Dec);
            SqlCmd.Parameters.Add("@Conv_Den_After_Dec", Conv_Den_After_Dec);
            SqlCmd.Parameters.Add("@Qty", Qty);
            SqlCmd.Parameters.Add("@Rate", Rate);
            SqlCmd.Parameters.Add("@Amount", Amount);
            SqlCmd.Parameters.Add("@Reduction", Reduction);
            SqlCmd.Parameters.Add("@Entry_Tax", Entry_Tax);
            SqlCmd.Parameters.Add("@RPG_Charge", RPG_Charge);
            SqlCmd.Parameters.Add("@RPG_Surcharge", RPG_Surcharge);
            SqlCmd.Parameters.Add("@LTC", LTC);
            SqlCmd.Parameters.Add("@Trans_Charge", Trans_Charge);
            SqlCmd.Parameters.Add("@OLV", OLV);
            SqlCmd.Parameters.Add("@LST", LST);
            SqlCmd.Parameters.Add("@LST_Surcharge", LST_Sucharge);
            SqlCmd.Parameters.Add("@LFR", LFR);
            SqlCmd.Parameters.Add("@DOFOBC_Charge", DOFOBC_Charge);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProOrganisationMasterEntry1 to insert the Organisation Details.
        /// This method is not used.
        /// </summary>
        public void InsertOrganisationDetail()
        {
            SqlCmd = new SqlCommand("ProOrganisationMasterEntry1", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@CompanyID", CompanyID);
            SqlCmd.Parameters.Add("@DealerName", DealerName);
            SqlCmd.Parameters.Add("@DealerShip", DealerShip);
            SqlCmd.Parameters.Add("@Address", Address);
            SqlCmd.Parameters.Add("@City", City);
            SqlCmd.Parameters.Add("@State", State);
            SqlCmd.Parameters.Add("@Country", Country);
            SqlCmd.Parameters.Add("@PhoneNo", PhoneNo);
            SqlCmd.Parameters.Add("@FaxNo", FaxNo);
            SqlCmd.Parameters.Add("@Email", Email);
            SqlCmd.Parameters.Add("@Website", Website);
            SqlCmd.Parameters.Add("@TinNo", TinNo);
            SqlCmd.Parameters.Add("@ExplosiveNo", ExplosiveNo);
            SqlCmd.Parameters.Add("@FoodLicNO", FoodLicNO);
            SqlCmd.Parameters.Add("@WM", WM);
            SqlCmd.Parameters.Add("@Drive", Drive);

            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProFuelPurchaseDetailsupdate2 to update the Fuel Purchase Details
        /// This method is not used.
        /// </summary>
        public void MasterUpdateFuelPurchaseDetail()
        {
            SqlCmd = new SqlCommand("ProFuelPurchaseDetailsupdate2", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", Invoice_No);
            SqlCmd.Parameters.Add("@Compartment", Compartment);
            SqlCmd.Parameters.Add("@Prod_Name", Prod_Name);
            SqlCmd.Parameters.Add("@Density_in_Physical", Density_in_Physical);
            SqlCmd.Parameters.Add("@Temp_in_Physical", Temp_in_Physical);
            SqlCmd.Parameters.Add("@conv_Density_Phy", Converted_Density_Phy);
            SqlCmd.Parameters.Add("@Density_in_Invoice_conv", Density_in_Invoice_Conv);
            SqlCmd.Parameters.Add("@Density_Variation", Density_Variation);
            SqlCmd.Parameters.Add("@Den_After_Dec", Density_After_Dec);
            SqlCmd.Parameters.Add("@Temp_After_Dec", Temp_After_Dec.ToString());
            SqlCmd.Parameters.Add("@Conv_Den_After_Dec", Conv_Den_After_Dec.ToString());
            SqlCmd.Parameters.Add("@Qty", Qty);
            SqlCmd.Parameters.Add("@Rate", Rate);
            SqlCmd.Parameters.Add("@Amount", Amount);
            SqlCmd.Parameters.Add("@Vendor_Name", Vendor_Name);
            SqlCmd.Parameters.Add("@City", City);
            SqlCmd.Parameters.Add("@Qty1", tempQty);
            SqlCmd.Parameters.Add("@Inv_date", Inv_date);
            SqlCmd.Parameters.Add("@Reduction", Reduction);
            SqlCmd.Parameters.Add("@Entry_Tax", Entry_Tax);
            SqlCmd.Parameters.Add("@RPG_Charge", RPG_Charge);
            SqlCmd.Parameters.Add("@RPG_Surcharge", RPG_Surcharge);
            SqlCmd.Parameters.Add("@LTC", LTC);
            SqlCmd.Parameters.Add("@Trans_Charge", Trans_Charge);
            SqlCmd.Parameters.Add("@OLV", OLV);
            SqlCmd.Parameters.Add("@LST", LST);
            SqlCmd.Parameters.Add("@LST_Surcharge", LST_Sucharge);
            SqlCmd.Parameters.Add("@LFR", LFR);
            SqlCmd.Parameters.Add("@DOFOBC_Charge", DOFOBC_Charge);

            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProProductsEntry to insert the Product Details in products table
        /// and also insert the value in Stock Master table.
        /// </summary>
        public void InsertProducts()
        {
            SqlCmd = new SqlCommand("ProProductsEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Prod_ID", Prod_ID);
            SqlCmd.Parameters.Add("@Prod_Name", Product_Name);
            SqlCmd.Parameters.Add("@Category", Category);
            SqlCmd.Parameters.Add("@Pack_Type", Package_Type);
            SqlCmd.Parameters.Add("@Pack_Unit", Package_Unit);
            SqlCmd.Parameters.Add("@Total_Qty", Total_Qty);
            SqlCmd.Parameters.Add("@Opening_Stock", Opening_Stock);
            SqlCmd.Parameters.Add("@Unit", Unit);
            SqlCmd.Parameters.Add("@Store_in", Store_In);
            SqlCmd.Parameters.Add("@Prod_Code", Prod_Code);
            SqlCmd.Parameters.Add("@MinLabel", MinLabel);
            SqlCmd.Parameters.Add("@MaxLabel", MaxLabel);
            SqlCmd.Parameters.Add("@ReOrderLabel", ReOrderLabel);
            SqlCmd.Parameters.Add("@BatchNo", Convert.ToInt32(BatchNo));
            SqlCmd.Parameters.Add("@MRP", MRP);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProProductsUpdate to Update the Products Details in Products table 
        /// and also update the value in Stock Master table.
        /// </summary>
        public void UpdateProducts()
        {
            SqlCmd = new SqlCommand("ProProductsUpdate", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Prod_ID", Prod_ID);
            SqlCmd.Parameters.Add("@Prod_Name", Product_Name);
            SqlCmd.Parameters.Add("@Category", Category);
            SqlCmd.Parameters.Add("@Pack_Type", Package_Type);
            SqlCmd.Parameters.Add("@Pack_Unit", Package_Unit);
            SqlCmd.Parameters.Add("@Total_Qty", Total_Qty);
            SqlCmd.Parameters.Add("@Opening_Stock", Opening_Stock);
            SqlCmd.Parameters.Add("@Unit", Unit);
            SqlCmd.Parameters.Add("@Store_in", Store_In);
            SqlCmd.Parameters.Add("@Prod_Code", Prod_Code);
            SqlCmd.Parameters.Add("@MinLabel", MinLabel);
            SqlCmd.Parameters.Add("@MaxLabel", MaxLabel);
            SqlCmd.Parameters.Add("@ReOrderLabel", ReOrderLabel);
            SqlCmd.Parameters.Add("@BatchNo", Convert.ToInt32(BatchNo));  //add by vikas sharma 08.06.09
            SqlCmd.Parameters.Add("@MRP", MRP);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProPriceUpdateEntry to insert the Price Updation Details in Price_Updation table.
        /// </summary>
        public void InsertPriceUpdation()
        {
            SqlCmd = new SqlCommand("ProPriceUpdateEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Eff_Date", System.Convert.ToDateTime(Eff_Date));
            SqlCmd.Parameters.Add("@Prod_Name", Product_Name);
            SqlCmd.Parameters.Add("@Pack_Type", Package_Type);
            SqlCmd.Parameters.Add("@Pur_Rate", Pur_Rate);
            SqlCmd.Parameters.Add("@Sal_Rate", Sal_Rate);
            SqlCmd.Parameters.Add("@HSN", HSN);
            SqlCmd.Parameters.Add("@IGST", IGST);
            SqlCmd.Parameters.Add("@CGST", CGST);
            SqlCmd.Parameters.Add("@SGST", SGST);
            SqlCmd.ExecuteNonQuery();
        }
        public DataSet ProPriceUpdation()
        {
            SqlCmd = new SqlCommand("ProPriceUpdation", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(SqlCmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;

        }
        //********bhal add****************
        public string custname
        {
            get
            {
                return _custname;
            }
            set
            {
                _custname = value;
            }
        }
        public string custType
        {
            get
            {
                return _custType;
            }
            set
            {
                _custType = value;
            }
        }
        public string balance
        {
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
            }
        }
        public string balancetype
        {
            get
            {
                return _balancetype;
            }
            set
            {
                _balancetype = value;
            }
        }

        /// <summary>
        /// Calls the Procedure ProcustbalanceUpdateEntry to update the Opening balance in Customer table
        /// and update the balance in CustomerLedgerTable and Ledger Master also.
        /// </summary>
        public void InsertcustbalanceUpdation()
        {
            SqlCmd = new SqlCommand("ProcustbalanceUpdateEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@cust_Name", custname);
            SqlCmd.Parameters.Add("@cust_Type", custType);
            SqlCmd.Parameters.Add("@changebal", balance);
            SqlCmd.Parameters.Add("@baltype", balancetype);
            SqlCmd.ExecuteNonQuery();
        }

        //******bhal end******************
        /// <summary>
        /// This method is not used
        /// </summary>
        public void InsertSchemeUpdation()
        {
            SqlCmd = new SqlCommand("ProSchemeUpdateEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Eff_Date", Eff_Date);
            SqlCmd.Parameters.Add("@Prod_Name", Product_Name);
            SqlCmd.Parameters.Add("@Pack_Type", Package_Type);
            SqlCmd.Parameters.Add("@schemetype", schemetype);
            SqlCmd.Parameters.Add("@discount", discount);
            SqlCmd.Parameters.Add("@discounttype", discounttype);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProPaymentReceiptEntry to insert the Payment Receipt Details.
        /// </summary>
        public void InsertPaymentReceived(PaymentReceiptModel payment)
        {
            SqlCmd = new SqlCommand("ProPaymentReceiptEntry", SqlCon1);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Receipt_No", payment.ReceiptNo);
            SqlCmd.Parameters.Add("@Invoice_No", payment.InvoiceNo);
            SqlCmd.Parameters.Add("@Received_Amount", payment.ReceivedAmount);
            SqlCmd.Parameters.Add("@Actual_Amount", payment.ActualAmount);
            SqlCmd.Parameters.Add("@BankName", payment.BankName);
            SqlCmd.Parameters.Add("@ChequeNo", payment.ChequeNumber);
            SqlCmd.Parameters.Add("@ChequeDate", System.Convert.ToDateTime(payment.ChequeDate));
            SqlCmd.Parameters.Add("@Mode", payment.Mode);
            SqlCmd.Parameters.Add("@Receipt", payment.Receipt);
            SqlCmd.Parameters.Add("@Cust_ID", payment.Cust_ID);
            SqlCmd.Parameters.Add("@Narration", payment.Narration);
            SqlCmd.Parameters.Add("@Discount1", payment.Discount1);
            SqlCmd.Parameters.Add("@Discount2", payment.Discount2);
            SqlCmd.Parameters.Add("@DiscountID1", payment.DiscountID1);
            SqlCmd.Parameters.Add("@DiscountID2", payment.DiscountID2);
            SqlCmd.Parameters.Add("@SubReceiptNo", payment.SubReceiptNo);
            SqlCmd.Parameters.Add("@RecDate", payment.Invoice_Date);
            SqlCmd.Parameters.Add("@CustBankName", payment.CustBankName);
            SqlCmd.ExecuteNonQuery();
        }
        //*******
        string _prodid;
        string _schname;
        string _schprodid;
        string _onevery;
        string _freepack;
        //string _discount;
        DateTime _dateto;
        DateTime _datefrom;
        string _Type;
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
        string _schid;
        public string schid
        {
            get
            {
                return _schid;
            }
            set
            {
                _schid = value;
            }
        }
        string _schtype;
        public string schtype
        {
            get
            {
                return _schtype;
            }
            set
            {
                _schtype = value;
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

        public DateTime datefrom
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
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }
        public DateTime dateto
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
        //*****bhal start****
        /* second  */
        /// <summary>
        /// Calls the Procedure ProFOEInsert to insert the FOE Details in FOE table.
        /// </summary>
        public void InsertFOEdiscountEntry()
        {
            SqlCmd = new SqlCommand("proFOEinsert", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@foid", foid);
            SqlCmd.Parameters.Add("@discription", discription);
            SqlCmd.Parameters.Add("@custtype", custtype);
            SqlCmd.Parameters.Add("@discount", discount);
            SqlCmd.Parameters.Add("@discounttype", discounttype);
            SqlCmd.Parameters.Add("@cust_id", cust_id);
            SqlCmd.Parameters.Add("@prod_id", prod_id);
            SqlCmd.Parameters.Add("@datefrom", datefrom);
            SqlCmd.Parameters.Add("@dateto", dateto);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProFOEInsert to insert the FOE Details in FOE table.
        /// </summary>
        public void updateFOEdiscountEntry()
        {
            SqlCmd = new SqlCommand("proFOEUpdate", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            //obj.schid=lblschid.Text;
            SqlCmd.Parameters.Add("@foid", foid);
            SqlCmd.Parameters.Add("@discription", discription);
            SqlCmd.Parameters.Add("@custtype", custtype);
            SqlCmd.Parameters.Add("@discount", discount);
            SqlCmd.Parameters.Add("@discounttype", discounttype);
            SqlCmd.Parameters.Add("@cust_id", cust_id);
            SqlCmd.Parameters.Add("@prod_id", prod_id);
            SqlCmd.Parameters.Add("@datefrom", datefrom);
            SqlCmd.Parameters.Add("@dateto", dateto);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calls the Procedure ProfoecustDelete to delete the FOE Customer from FOE table.
        /// </summary>
        public void Deletefoecust()
        {
            SqlCmd = new SqlCommand("ProfoecustDelete", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@cust_id", cust_id);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// This method is not used.
        /// </summary>
        public void InsertFOEdiscount()
        {
            SqlCmd = new SqlCommand("proFOEdiscountEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            //obj.schid=lblschid.Text;
            SqlCmd.Parameters.Add("@foid", foid);
            SqlCmd.Parameters.Add("@discription", discription);
            SqlCmd.Parameters.Add("@custtype", custtype);
            SqlCmd.Parameters.Add("@discount", discount);
            SqlCmd.Parameters.Add("@discounttype", discounttype);
            SqlCmd.Parameters.Add("@cust_id", cust_id);
            SqlCmd.Parameters.Add("@datefrom", datefrom);
            SqlCmd.Parameters.Add("@dateto", dateto);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// This method is not used.
        /// </summary>
        public void UpdateFOEdiscount()
        {
            SqlCmd = new SqlCommand("proFOEdiscountUpdate", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            //obj.schid=lblschid.Text;
            SqlCmd.Parameters.Add("@foid", foid);
            SqlCmd.Parameters.Add("@discription", discription);
            SqlCmd.Parameters.Add("@custtype", custtype);
            SqlCmd.Parameters.Add("@discount", discount);
            SqlCmd.Parameters.Add("@discounttype", discounttype);
            SqlCmd.Parameters.Add("@cust_id", cust_id);
            SqlCmd.Parameters.Add("@datefrom", datefrom);
            SqlCmd.Parameters.Add("@dateto", dateto);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Call the Procedure ProSchemediscountEntry to insert the scheme in OilScheme and Per_Discount table.
        /// </summary>
        public void InsertSchemediscount()
        {
            SqlCmd = new SqlCommand("proSchemediscountEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            //obj.schid=lblschid.Text;
            SqlCmd.Parameters.Add("@schid", schid);
            SqlCmd.Parameters.Add("@schtype", schtype);
            SqlCmd.Parameters.Add("@prodid", prodid);
            SqlCmd.Parameters.Add("@schname", schname);
            SqlCmd.Parameters.Add("@schprodid", schprodid);
            SqlCmd.Parameters.Add("@onevery", onevery);
            SqlCmd.Parameters.Add("@freepack", freepack);
            SqlCmd.Parameters.Add("@discount", discount);
            SqlCmd.Parameters.Add("@distype", schemetype);
            SqlCmd.Parameters.Add("@datefrom", datefrom);
            SqlCmd.Parameters.Add("@dateto", dateto);
            SqlCmd.Parameters.Add("@Type", Type);
            SqlCmd.Parameters.Add("@Group", Group_Name);        //Add by vikas sharma 23.10.2012
            SqlCmd.Parameters.Add("@Unit", Unit);               //Add by vikas sharma 25.10.2012
            SqlCmd.Parameters.Add("@SP_Type", SPack_Type);     //Add by vikas sharma 7.11.2012
            SqlCmd.ExecuteNonQuery();

        }

        /// <summary>
        /// Call the Procedure ProStockistSchdiscountEntry to insert the scheme in StktSchDiscount table.
        /// </summary>
        public void InsertStockiestSchemediscount()
        {
            SqlCmd = new SqlCommand("ProStockistSchDiscountEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@schid", schid);
            SqlCmd.Parameters.Add("@schtype", schtype);
            SqlCmd.Parameters.Add("@prodid", prodid);
            SqlCmd.Parameters.Add("@schname", schname);
            SqlCmd.Parameters.Add("@discount", discount);
            SqlCmd.Parameters.Add("@distype", discounttype);
            SqlCmd.Parameters.Add("@datefrom", datefrom);
            SqlCmd.Parameters.Add("@dateto", dateto);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Call the Procedure ProStockistSchdiscountEntry to insert the scheme in StktSchDiscount table.
        /// </summary>
        public void InsertPPGE()
        {
            SqlCmd = new SqlCommand("ProPPGE", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@schid", schid);
            SqlCmd.Parameters.Add("@schtype", schtype);
            SqlCmd.Parameters.Add("@prodid", prodid);
            SqlCmd.Parameters.Add("@schname", schname);
            SqlCmd.Parameters.Add("@discount", discount);
            SqlCmd.Parameters.Add("@distype", discounttype);
            SqlCmd.Parameters.Add("@datefrom", datefrom);
            SqlCmd.Parameters.Add("@dateto", dateto);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Call the Procedure ProSchemediscountUpdate to insert the scheme in OilScheme and Per_Discount table.
        /// </summary>
        public void updateSchemediscount()
        {
            SqlCmd = new SqlCommand("proSchemediscountUpdate", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            //obj.schid=lblschid.Text;
            SqlCmd.Parameters.Add("@schid", schid);
            SqlCmd.Parameters.Add("@schtype", schtype);
            SqlCmd.Parameters.Add("@prodid", prodid);
            SqlCmd.Parameters.Add("@schname", schname);
            SqlCmd.Parameters.Add("@schprodid", schprodid);
            SqlCmd.Parameters.Add("@onevery", onevery);
            SqlCmd.Parameters.Add("@freepack", freepack);
            SqlCmd.Parameters.Add("@discount", discount);
            SqlCmd.Parameters.Add("@distype", schemetype);
            SqlCmd.Parameters.Add("@datefrom", datefrom);
            SqlCmd.Parameters.Add("@dateto", dateto);
            SqlCmd.Parameters.Add("@Type", Type);
            SqlCmd.Parameters.Add("@Group", Group_Name);              //Add By Vikas 25.10.2012
            SqlCmd.Parameters.Add("@Unit", Unit);                      //Add By Vikas 25.10.2012
            SqlCmd.Parameters.Add("@SP_Type", SPack_Type);     //Add by vikas sharma 7.11.2012
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Call the Procedure ProStockistSchdiscountUpdate to insert the scheme in StktSchDiscount table.
        /// </summary>
        public void updateStockiestSchemeDiscount()
        {
            SqlCmd = new SqlCommand("ProStockiestSchDiscountUpdate", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            //obj.schid=lblschid.Text;
            SqlCmd.Parameters.Add("@schid", schid);
            SqlCmd.Parameters.Add("@schtype", schtype);
            SqlCmd.Parameters.Add("@prodid", prodid);
            SqlCmd.Parameters.Add("@schname", schname);
            SqlCmd.Parameters.Add("@discount", discount);
            SqlCmd.Parameters.Add("@distype", schemetype);
            SqlCmd.Parameters.Add("@datefrom", datefrom);
            SqlCmd.Parameters.Add("@dateto", dateto);
            SqlCmd.ExecuteNonQuery();

        }
        //********end***********
        /// <summary>
        /// Call the Procedure ProInorUpdateStock to update the products in products table and also update the 
        /// opening stock in stock master table.
        /// </summary>
        public void InsertorUpdate()
        {
            SqlCmd = new SqlCommand("proInorUpdateStock", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            //obj.schid=lblschid.Text;
            SqlCmd.Parameters.Add("@Product_Name", Product_Name);
            SqlCmd.Parameters.Add("@Category", Category);
            SqlCmd.Parameters.Add("@Package_Type", Package_Type);
            SqlCmd.Parameters.Add("@Opening_Stock", Opening_Stock);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Call the Procedure ProInorUpdateStock to update the products in products table and also update the 
        /// opening stock in stock master table.
        /// </summary>
        public void UpdateIndent()
        {
            SqlCmd = new SqlCommand("proInorUpdateStock", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            //obj.schid=lblschid.Text;
            SqlCmd.Parameters.Add("@Product_Name", Product_Name);
            SqlCmd.Parameters.Add("@Category", Category);
            SqlCmd.Parameters.Add("@Package_Type", Package_Type);
            SqlCmd.Parameters.Add("@Opening_Stock", Opening_Stock);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Call the Procedure ProInsertLYPSSALES to insert the value in Ly_Ps_Sales table before delete the 
        /// particular Ly_Ps_Sales ID from Ly_Ps_Sales table.
        /// </summary>
        public void InsertLY_PS_SALES()
        {
            SqlCmd = new SqlCommand("ProInsertLYPSSALES", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@LYPS", foid);
            SqlCmd.Parameters.Add("@Month", Month);
            SqlCmd.Parameters.Add("@sub0", sub0);
            SqlCmd.Parameters.Add("@sub1", sub1);
            SqlCmd.Parameters.Add("@sub2", sub2);
            SqlCmd.Parameters.Add("@sub3", sub3);
            SqlCmd.Parameters.Add("@sub4", sub4);
            SqlCmd.Parameters.Add("@sub5", sub5);
            SqlCmd.Parameters.Add("@sub6", sub6);
            SqlCmd.Parameters.Add("@sub7", sub7);
            SqlCmd.Parameters.Add("@sub8", sub8);
            SqlCmd.Parameters.Add("@sub9", sub9);
            SqlCmd.Parameters.Add("@sub10", sub10);
            SqlCmd.Parameters.Add("@sub11", sub11);
            SqlCmd.Parameters.Add("@sub12", sub12);
            SqlCmd.Parameters.Add("@sub13", sub13);
            SqlCmd.Parameters.Add("@sub14", sub14);
            SqlCmd.Parameters.Add("@sub15", sub15);
            SqlCmd.Parameters.Add("@sub16", sub16);
            SqlCmd.Parameters.Add("@sub17", sub17);
            SqlCmd.Parameters.Add("@sub18", sub18);
            SqlCmd.Parameters.Add("@sub19", sub19);
            SqlCmd.Parameters.Add("@sub20", sub20);
            SqlCmd.Parameters.Add("@sub21", sub21);
            SqlCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// This method is not used.
        /// </summary>
        public void InsertBatch()
        {
            SqlCmd = new SqlCommand("ProInsertBatch", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@Invoice_No", Invoice_No);
            SqlCmd.Parameters.Add("@Invoice_Date", Invoice_Date);
            SqlCmd.Parameters.Add("@Prod_Name", Prod_Name);
            SqlCmd.Parameters.Add("@Qty", Qty);
            SqlCmd.Parameters.Add("@Batch_No", BatchNo);
            SqlCmd.ExecuteNonQuery();
        }

        public void InsertUserMaster(UserModels User)
        {
            SqlCmd = new SqlCommand("ProUserMasterEntry", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@User_ID", User.UserID.Length > 0 ? Int32.Parse(User.UserID) : 0);
            SqlCmd.Parameters.Add("@Login_Name", User.LoginName);
            SqlCmd.Parameters.Add("@Password", User.Password);
            SqlCmd.Parameters.Add("@User_Name", User.UserName);
            SqlCmd.Parameters.Add("@Role_Name", User.RoleName);
            SqlCmd.ExecuteNonQuery();
            SqlCon.Close();
        }

        public void UpdateUserMaster(UserModels User)
        {
            SqlCmd = new SqlCommand("ProUserMasterUpdate", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@User_ID", User.UserID.Length > 0 ? Int32.Parse(User.UserID) : 0);
            SqlCmd.Parameters.Add("@Login_Name", User.LoginName);
            SqlCmd.Parameters.Add("@Password", User.Password);
            SqlCmd.Parameters.Add("@User_Name", User.UserName);
            SqlCmd.Parameters.Add("@Role_Name", User.RoleName);
            SqlCmd.ExecuteNonQuery();
            SqlCon.Close();
        }

        public string checkDate(string str)
        {
            if (!str.Trim().Equals(""))
            {
                if (str.Trim().Equals("1/1/1900"))
                    str = "";
            }
            return str;
        }
        public string trimDate(string strDate)
        {
            int pos = strDate.IndexOf(" ");
            if (pos != -1)
            {
                strDate = strDate.Substring(0, pos);
            }
            else
            {
                strDate = "";
            }
            return strDate;
        }

    }
}