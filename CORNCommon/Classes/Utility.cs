using System;
using System.Data;

namespace  CORNCommon.Classes
{
    public static class Utility
    {
        public static string ShowAlert(bool IsSuccess,string Message)
        {
            string AlertMessage = "";

            if (IsSuccess)
            {
                AlertMessage = "<div style='padding:10px; margin:10px; background-color:#ecfaf0;border: Solid 1px #04551d; color:#04551d;	font-size:12px; font-family:Verdana;'>" + Message + "</div>";
            }
            else
            {
                AlertMessage = "<div style='padding:10px; margin:10px; background-color:#fbefef;	border: Solid 1px #bb0000; color:#bb0000;	font-size:12px; font-family:Verdana;'>" + Message + "</div>";
            }

            return AlertMessage;
        }
    }
}