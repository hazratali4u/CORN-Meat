using System;
using System.Windows.Forms;
using FtpLib;
using System.Xml;
using System.IO;
using System.Collections;
using System.Data;

namespace CORNCommon.Classes
{
    /// <summary>
    /// Summary description for EditControl.
    /// </summary>
    public class DataControl
    {
        public DataControl()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static string getControlID(string p_ClientID)
        {
            int start = p_ClientID.IndexOf("ctl") + 3;
            int end = p_ClientID.IndexOf("_lBtn");
            string str = p_ClientID.Substring(start, end - start);
            int dec = int.Parse(str);
            dec -= 2;
            return (dec).ToString();
        }
        public static string getTextControlID(string p_ClientID)
        {
            int start = p_ClientID.IndexOf("ctl") + 3;
            int end = p_ClientID.IndexOf("_txt");
            string str = p_ClientID.Substring(start, end - start);
            int dec = int.Parse(str);
            dec -= 2;
            return (dec).ToString();
        }
        public static string getTextControlName(string p_ClientID)
        {
            int start = p_ClientID.IndexOf("txt") + 3;
            int end = p_ClientID.IndexOf("Price");
            string str = p_ClientID.Substring(start, end - start);
            return str;
        }
        public static void refreshData(string p_ClientID) { }


        public static decimal chkDecimalZero(string p_Val)
        {
            if ((p_Val == "") || (p_Val == null))
            {
                return Convert.ToDecimal(0);
            }
            else
            {
                return Convert.ToDecimal(p_Val);
            }
        }

        public string chkNull(string p_Val)
        {
            if ((p_Val == "") || (p_Val == null))
            {
                return p_Val = "-1";
            }
            else
            { return p_Val; }

        }

        public static string chkNull_Zero(string p_Val)
        {
            if ((p_Val == "") || (p_Val == null))
            {
                return p_Val = "0";
            }
            else
            { return p_Val; }

        }

        public string chkNull_0(string p_Val)
        {
            if ((p_Val == "") || (p_Val == null) || (p_Val.Trim() == "-"))
            {
                return p_Val = "0";
            }
            else
            { return p_Val; }

        }
        public static int chkIntNull(string p_Val)
        {
            if ((p_Val == "") || (p_Val == null))
            {
                return Constants.IntNullValue;
            }
            else
            {
                return int.Parse(p_Val);
            }
        }
        public static decimal chkDecimalNull(string p_Val)
        {
            if ((p_Val == "") || (p_Val == null))
            {
                return Constants.DecimalNullValue;
            }
            else
            {
                return decimal.Parse(p_Val);
            }
        }
        
        public static decimal QuantityConversion(decimal DefaultQty, string Operator, decimal Factor, decimal QtyToConvert, decimal RawQty, string Type)
        {
            decimal NEW_QTY = 0;
            if (Type == "Raw")
            {
                switch (Operator)
                {
                    case "+":
                        NEW_QTY = (QtyToConvert * RawQty) * (DefaultQty + Factor);
                        break;
                    case "-":
                        NEW_QTY = (QtyToConvert * RawQty) * (DefaultQty - Factor);
                        break;
                    case "/":
                        NEW_QTY = (QtyToConvert * RawQty) * (DefaultQty / Factor);
                        break;
                    case "%":
                        NEW_QTY = (QtyToConvert * RawQty) * (DefaultQty % Factor);
                        break;
                    default:
                        NEW_QTY = (QtyToConvert * RawQty) * (DefaultQty * Factor);
                        break;
                }
            }
            else
            {
                switch (Operator)
                {
                    case "+":
                        NEW_QTY = QtyToConvert * (DefaultQty + Factor);
                        break;
                    case "-":
                        NEW_QTY = QtyToConvert * (DefaultQty - Factor);
                        break;
                    case "/":
                        NEW_QTY = QtyToConvert * (DefaultQty / Factor);
                        break;
                    case "%":
                        NEW_QTY = QtyToConvert * (DefaultQty % Factor);
                        break;
                    default:
                        NEW_QTY = QtyToConvert * (DefaultQty * Factor);
                        break;
                }
            }
            return NEW_QTY;
        }
    }
}
