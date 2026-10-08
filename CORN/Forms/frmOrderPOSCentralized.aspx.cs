using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.IO;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using Newtonsoft.Json;
using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using System.Net;
using System.Text;
using System.Xml;

namespace Forms
{
    public partial class frmOrderPOSCentralized : System.Web.UI.Page
    {
        static DataTable dtValue;
        static DataTable dtConfig;
        static DataTable dtConfigDefault;
        
        private static bool OversaleAllowed;
        
        private readonly GeoHierarchyController _gCtl = new GeoHierarchyController();

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            Response.AppendHeader("pragma", "no-cache");
            
            if (!Page.IsPostBack)
            {
                dtConfig = GetAppConfiguration();

                //if (IsDayClosed())
                //{
                //    UserController UserCtl = new UserController();

                //    UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
                //    Session.Clear();
                //    System.Web.Security.FormsAuthentication.SignOut();
                //    Response.Redirect("../Login.aspx");
                //}

                txtCustomerDOB.Attributes.Add("readonly", "readonly");
                hfCompanyName.Value = Session["COMPANY_NAME"].ToString();
                lblUserName.Text = Session["UserName"].ToString();
                DateTime date = DateTime.Parse(Session["CurrentWorkDate"].ToString());
                lblDateTime.Text = date.ToString("dd-MMMM-yyyy");
                hfCurrentWorkDate.Value = date.ToString("dd-MMM-yyyy");
                hfUserId.Value = HttpContext.Current.Session["UserID"].ToString();//for default user add in orderbooker dropdown
                hfIS_CanGiveDiscount.Value = HttpContext.Current.Session["IS_CanGiveDiscount"].ToString();
            }
        }
        
        public static string GetJson(DataTable dt)
        {

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            //  serializer.MaxJsonLength =  Int32.MaxValue;


            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();

                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);

        }
        
        [WebMethod]
        public static string LoadMaxOrderNo(string LocationID)
        {
            DateTime CurrentWorkDate = Constants.DateNullValue;
            DataTable dtLocationInfo = (DataTable)HttpContext.Current.Session["dtLocationInfo"];
            foreach (DataRow dr in dtLocationInfo.Rows)
            {
                if (dr["DISTRIBUTOR_ID"].ToString() == LocationID)
                {
                    if (dr["MaxDayClose"].ToString().Length > 0)
                    {
                        CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                        break;
                    }
                }
            }

            GeoHierarchyController _gCtl = new GeoHierarchyController();
            string Id = _gCtl.SelectMaxOrderNo(int.Parse(LocationID), CurrentWorkDate);
            return Id;
        }

        [WebMethod]
        public static string GetMaxBillNo()
        {
            OrderEntryController _orderCtl = new OrderEntryController();
            string Id = _orderCtl.GetMaxBillNo();
            return Id;
        }

        public static int CustomerType(string pCustomerType)
        {
            if (pCustomerType == "Dine In")
            {
                return 1;
            }
            else if (pCustomerType == "Delivery")
            {
                return 2;
            }
            return 3;
        }
        
        #region Category, Products, Product Stock Status

        [WebMethod]
        public static string LoadProductCategroy(bool ItemType, int ItemId)
        {
            SkuHierarchyController mController = new SkuHierarchyController();
            DataTable dt = mController.SelectSkuHierarchy(Constants.SKUTAG, ItemId, 1, null, null, ItemType, 14, Constants.IntNullValue);
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, object> dRow = new Dictionary<string, object>
                {
                    {"CAT_ID", row["SKU_HIE_ID"]},
                    {"CAT_NAME", row["SKU_HIE_NAME"]}
                };
                rows.Add(dRow);
            }
            System.Web.Script.Serialization.JavaScriptSerializer jSearializer =
                new System.Web.Script.Serialization.JavaScriptSerializer();
            return jSearializer.Serialize(rows);
        }

        [WebMethod]
        public static string LoadAllProducts(string LocationID)
        {
            var mSkuController = new SkuController();
            try
            {
                DataTable dtSkus = mSkuController.SelectSkusforOrder(int.Parse(HttpContext.Current.Session["CompanyId"].ToString()),
                        Constants.IntNullValue, 1, Constants.IntNullValue,int.Parse(LocationID), 1);

                return GetJson(dtSkus);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [WebMethod]
        public static string LoadProductStatus(int id,string LocationID)
        {
            var mSkuController = new SkuController();
            try
            {
                DataTable dtSkus = mSkuController.SelectSkusforOrder(int.Parse(HttpContext.Current.Session["CompanyId"].ToString()),
                        Constants.IntNullValue, 1, id,int.Parse(LocationID), 2);

                return GetJson(dtSkus);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Bills

        [WebMethod]
        [ScriptMethod]
        public static string SelectPendingBills(string customerType,string LocationID)
        {
            DateTime CurrentWorkDate = Constants.DateNullValue;
            DataTable dtLocationInfo = (DataTable)HttpContext.Current.Session["dtLocationInfo"];
            foreach (DataRow dr in dtLocationInfo.Rows)
            {
                if (dr["DISTRIBUTOR_ID"].ToString() == LocationID)
                {
                    if (dr["MaxDayClose"].ToString().Length > 0)
                    {
                        CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                        break;
                    }
                }
            }

            int customerTypeId = -1;

            if (customerType == "Dine In")
            {
                customerTypeId = 1;
            }
            else if (customerType == "Delivery")
            {
                customerTypeId = 2;
            }
            else if (customerType == "Takeaway")
            {
                customerTypeId = 3;
            }

            var mSkuController = new OrderEntryController();
            DataTable dtSkus = mSkuController.SelectPendingBills(Constants.LongNullValue,CurrentWorkDate, int.Parse(LocationID), int.Parse(HttpContext.Current.Session["UserID"].ToString()), customerTypeId, 7);
            return GetJson(dtSkus);
        }

        [WebMethod]
        public static string GetPendingBill(long saleInvoiceMasterId)
        {
            var mSkuController = new SkuController();
            DataTable dtSkus = mSkuController.SpGetPendingBill(saleInvoiceMasterId);

            return GetJson(dtSkus);
        }

        #endregion
        
        #region Customer


        [WebMethod]
        [ScriptMethod]
        public static string LoadAllCustomers(string customerName, string type,string LocationID)
        {
            DateTime CurrentWorkDate = Constants.DateNullValue;
            DataTable dtLocationInfo = (DataTable)HttpContext.Current.Session["dtLocationInfo"];
            foreach (DataRow dr in dtLocationInfo.Rows)
            {
                if (dr["DISTRIBUTOR_ID"].ToString() == LocationID)
                {
                    if (dr["MaxDayClose"].ToString().Length > 0)
                    {
                        CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                        break;
                    }
                }
            }

            var mController = new CustomerDataController();
            try
            {
                DataTable dtCustomers = mController.UspSelectCustomer(int.Parse(LocationID), type, customerName, CurrentWorkDate);

                return GetJson(dtCustomers);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [WebMethod]
        [ScriptMethod]
        public static string LoadLocation()
        {
            var mController = new DistributorController();
            try
            {
                DataTable dtLocations = mController.GetDistributorWithMaxDayClose(Constants.IntNullValue, Convert.ToInt32(HttpContext.Current.Session["UserId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]),1);
                HttpContext.Current.Session.Add("dtLocationInfo", dtLocations);
                return GetJson(dtLocations);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static void InsertCustomer(string cardID, string CNIC, string contactNumer, string contactNumer2, string customerName, string address, string DOB, string OpeningAmount, string Nature, string email,string LocationID)
        {
            try
            {
                int distributerId = int.Parse(LocationID);
                var dc = new DataControl();
                CustomerDataController.InsertCustomer(distributerId, cardID, CNIC, DOB, contactNumer, email, customerName, address
                    , null, Convert.ToDecimal(dc.chkNull_0(OpeningAmount)), Nature, contactNumer2, 0, 0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                throw;
            }
        }

        #endregion

        #region  Order Hold, Update, Invoice

        [WebMethod]
        [ScriptMethod]
        public static void HoldOrder(string orderedProducts, string orderBooker, string coverTable, string customerType, string CustomerName
            , string maxOrderNo, string printType, string tableName, string takeAwayCustomer, string bookerName, string tabId, string LocationID,string remarks)
        {
            //if (IsDayClosed())
            //{
            //    UserController UserCtl = new UserController();

            //    UserCtl.InsertUserLogoutTime(Convert.ToInt32(HttpContext.Current.Session["User_Log_ID"]), Convert.ToInt32(HttpContext.Current.Session["UserID"]));
            //    HttpContext.Current.Session.Clear();
            //    System.Web.Security.FormsAuthentication.SignOut();
            //    HttpContext.Current.Response.Redirect("../Login.aspx");
            //}

            #region Validation

            int tableId = 0;
            long customerId = 0;

            int customerTypeId = CustomerType(customerType);


            if (customerTypeId == 1)
            {
                tableId = int.Parse(tabId);
            }
            customerId = Convert.ToInt64(CustomerName);
            
            #endregion

            try
            {
                DateTime CurrentWorkDate = Constants.DateNullValue;
                DataTable dtLocationInfo = (DataTable)HttpContext.Current.Session["dtLocationInfo"];
                foreach (DataRow dr in dtLocationInfo.Rows)
                {
                    if (dr["DISTRIBUTOR_ID"].ToString() == LocationID)
                    {
                        if (dr["MaxDayClose"].ToString().Length > 0)
                        {
                            CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                            break;
                        }
                    }
                }

                dtValue = new DataTable();

                int userId = int.Parse(HttpContext.Current.Session["UserID"].ToString());
                
                int distributerId = int.Parse(LocationID);

                if (orderedProducts != "")
                {
                    dtValue = (DataTable)JsonConvert.DeserializeObject(orderedProducts, (typeof(DataTable)));
                }

               
                if (dtValue != null && dtValue.Rows.Count > 0 &&
                    dtValue.Rows[0]["INVOICE_ID"].ToString().Trim() != "N/A")
                {
                    OrderEntryController.HoldOrder(long.Parse(dtValue.Rows[0]["INVOICE_ID"].ToString()), Constants.IntNullValue,
                        customerTypeId, tableId, 0, 0, 0, 0, 0, true, false, userId, CurrentWorkDate, distributerId, 0, null,
                        dtValue, int.Parse(orderBooker), coverTable, takeAwayCustomer, OversaleAllowed,customerId, remarks);
                }
                else
                {
                    OrderEntryController.Add_Invoice(Constants.IntNullValue, customerTypeId, tableId, 0, 0, 0, true,
                       userId, CurrentWorkDate, distributerId, dtValue, int.Parse(orderBooker), coverTable, customerId, maxOrderNo, takeAwayCustomer,1, remarks);
                }
            }
            catch (FormatException ex)
            {
                throw new Exception("Please enter price", ex);
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                throw;

            }
        }
        
        #endregion
        
        protected void lnkExit_OnClick(object sender, EventArgs e)
        {

            Response.Redirect("Home.aspx");

        }

        public DataTable GetAppConfiguration()
        {
            try
            {
                ConfigurationController _cController = new ConfigurationController();
                DataTable dt = _cController.SelectAppConfiguration(1, Constants.IntNullValue, null, Constants.IntNullValue);
                dtConfigDefault = _cController.SelectAppConfiguration(1, (int)Enums.AppSettingMaster.WebSettings, null, Constants.IntNullValue);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
                return null;
            }
        }

        #region SMS


        //msgType: 1 for hold, 2 for Ride

        [WebMethod]
        [ScriptMethod]
        public static string SendSMS(string customerNo, string msgType)
        {
            HttpContext.Current.Session.Add("SMSNo", customerNo);
            GsmCommMain comm;

            DataRow[] dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.SMSSendingType + "'");
            string SMSSendingType = dr[0]["VALUE"].ToString();

            if (SMSSendingType == "Web Service")
            {
                string result = readHtmlPage("http://www.sms4connect.com/api/sendsms.php/sendsms/url", msgType);

                XmlDocument xdoc = new XmlDocument();

                // if xml coming via string
                string myXml = result;
                xdoc.LoadXml(myXml);

                string xpath = "corpsms";
                var nodes = xdoc.SelectNodes(xpath);
                foreach (XmlNode childrenNode in nodes)
                {
                    result = childrenNode.SelectSingleNode("code").InnerText;
                    if (result == "100")
                    {
                        result = childrenNode.SelectSingleNode("response").InnerText;
                    }
                    else
                    {
                        result = childrenNode.SelectSingleNode("response").InnerText;
                    }
                }
                return result;
            }
            else
            {
                Int32 comport, baudrate, timeout;
                dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.SMSComPort + "'");
                comport = Convert.ToInt32(dr[0]["VALUE"].ToString());
                dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.SMSbaudrate + "'");
                baudrate = Convert.ToInt32(dr[0]["VALUE"].ToString());
                dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.SMStimeout + "'");
                timeout = Convert.ToInt32(dr[0]["VALUE"].ToString());

                CommSetting cs = new CommSetting();
                SmsSubmitPdu pdu;
                cs.comm = new GsmCommMain(comport, baudrate, timeout);
                comm = cs.comm;

                try
                {
                    if (!comm.IsOpen())
                    {
                        comm.Open();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionPublisher.PublishExceptionforSync(ex, "Exception from SMS");
                    throw;
                }

                try
                {

                    if (HttpContext.Current.Session["SMSNo"].ToString() != "")
                    {
                        if (msgType == "1")
                        {
                            pdu = new SmsSubmitPdu(HttpContext.Current.Session["HOLD_MSG"].ToString(), HttpContext.Current.Session["SMSNo"].ToString());
                            cs.comm.SendMessage(pdu);
                        }
                        else
                        {
                            pdu = new SmsSubmitPdu(HttpContext.Current.Session["RIDE_MSG"].ToString(), HttpContext.Current.Session["SMSNo"].ToString());
                            cs.comm.SendMessage(pdu);

                        }

                    }
                    return "Message Sent To Telecom";
                }
                catch (Exception ex)
                {
                    ExceptionPublisher.PublishException(ex);
                    throw new Exception("Message Not Sent", ex);

                }
            }
        }

        private static String readHtmlPage(string url, string msgType)
        {

            string receipients = HttpContext.Current.Session["SMSNo"].ToString();


            DataRow[] dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.SMSUserID + "'");
            string ID = dr[0]["VALUE"].ToString();
            dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.SMSPassword + "'");
            string PWD = dr[0]["VALUE"].ToString();
            dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.SMSMask + "'");
            string MASK = dr[0]["VALUE"].ToString();

            String result = "";
            string message = "";
            if (msgType == "1")
            {
                message = HttpUtility.UrlEncode(HttpContext.Current.Session["HOLD_MSG"].ToString());
            }
            else
            {
                message = HttpUtility.UrlEncode(HttpContext.Current.Session["RIDE_MSG"].ToString());
            }

            String strPost = "id=" + ID + "&pass=" + PWD + "&msg=" + message + "&to=" + receipients + "&mask=" + MASK + "&type=xml&lang=English";


            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Method = "POST";
            objRequest.ContentLength = Encoding.UTF8.GetByteCount(strPost);
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(strPost);
            }
            catch (Exception e)
            {
                ExceptionPublisher.PublishException(e);
                //throw new Exception("Message Not Sent", e);
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();   // Close and clean up the StreamReader   
                sr.Close();
            }

            return result;
        }

        #endregion

        #region SMS GSM 

        public class CommSetting : IDisposable
        {
            private bool disposed = false;
            public static int Comm_Port = 2;
            public static int Comm_BaudRate = 9600;
            public static int Comm_TimeOut = 300;
            public static GsmCommMain comm2;
            public GsmCommMain comm;
            public static int count = 1;

            public CommSetting()
            {

            }

            public static Int32 Add()
            {
                return count++;
            }

            ~CommSetting()
            {
                Dispose(false);
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this.comm);
            }

            private void Dispose(bool disposing)
            {
                if (this.comm.IsOpen())
                {
                    this.comm.Close();
                }

                disposed = true;

            }
        }

        #endregion
    }
}