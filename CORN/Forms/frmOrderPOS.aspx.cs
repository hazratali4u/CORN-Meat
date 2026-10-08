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
using System.Drawing.Printing;
using System.Drawing;
using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using System.Net;
using System.Text;
using System.Xml;

namespace Forms
{
    public partial class FormsfrmOrderPOS : System.Web.UI.Page
    {
        static DataTable dtValue;
        static DataTable dtSection;
        private PrintDocument prnDocument;
        static DataTable dtConfig;
        static DataTable dtCOAConfig = null;
        static DataTable dtConfigDefault;

        private static string _section;
        private static bool ClosingStockStatus;
        private static bool OversaleAllowed;
        private static bool IsFinanceIntegrate = false;

        #region Printing Variables
        // for Report:
        private static int CurrentY;
        private static int CurrentX;
        private static int leftMargin;
        private static int rightMargin;
        private static int topMargin;
        private static int bottomMargin;
        private static int InvoiceWidth;
        private static int InvoiceHeight;

        //private bool ReadInvoice;

        private static int XNoOfuntit;
        private static int YNoOfUnit;

        // for Invoice Head:
        private static string InvTitle;
        private static string InvSubTitle1;
        private static string InvSubTitle2;
        private static string InvSubTitle3;
        private static string InvSubTitle4;

        // Font and Color:------------------
        // Title Font
        private static Font InvTitleFont = new Font("sans-serif", 22, FontStyle.Regular);
        // Title Font height
        private static int InvTitleHeight;
        // SubTitle Font
        private static Font InvSubTitleFont = new Font("sans-serif", 12, FontStyle.Regular);
        private static Font InvOrderTitleFont = new Font("sans-serif", 14, FontStyle.Bold);
        // SubTitle Font height
        private static int InvSubTitleHeight;
        // Invoice Font
        private static Font InvoiceFont = new Font("sans-serif", 11, FontStyle.Regular);

        private static Font InvoiceFont2 = new Font("sans-serif", 9, FontStyle.Regular);



        // Invoice Font height
        private static int InvoiceFontHeight;
        // Blue Color
        private static SolidBrush BlueBrush = new SolidBrush(Color.Blue);

        // Black Color
        private static SolidBrush BlackBrush = new SolidBrush(Color.Black);

        #endregion

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

                if (IsDayClosed())
                {
                    UserController UserCtl = new UserController();

                    UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
                    Session.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    Response.Redirect("../Login.aspx");
                }

                txtCustomerDOB.Attributes.Add("readonly", "readonly");


                hfCompanyName.Value = Session["COMPANY_NAME"].ToString();
                lblUserName.Text = Session["UserName"].ToString();
                DateTime date = DateTime.Parse(Session["CurrentWorkDate"].ToString());
                lblDateTime.Text = date.ToString("dd-MMMM-yyyy");
                hfCurrentWorkDate.Value = date.ToString("dd-MMM-yyyy");
                hfUserId.Value = HttpContext.Current.Session["UserID"].ToString();//for default user add in orderbooker dropdown
                hfIS_CanGiveDiscount.Value = HttpContext.Current.Session["IS_CanGiveDiscount"].ToString();

                #region Location Information

                DataSet ds = _gCtl.SelectDataForPosLoad(int.Parse(hfUserId.Value),
                    int.Parse(Session["DISTRIBUTOR_ID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString())
                    , int.Parse(Session["RoleID"].ToString()));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hfSalesTax.Value = ds.Tables[0].Rows[0]["GST"].ToString();
                    hfIsCoverTable.Value = ds.Tables[0].Rows[0]["ISCOVERTABLE"].ToString();
                    hfServiceCharges.Value = ds.Tables[0].Rows[0]["SERVICE_CHARGES"].ToString();
                    hfLocationName.Value = ds.Tables[0].Rows[0]["DISTRIBUTOR_NAME"].ToString();
                    hfAddress.Value = ds.Tables[0].Rows[0]["ADDRESS1"].ToString();
                    hfPhoneNo.Value = ds.Tables[0].Rows[0]["CONTACT_NUMBER"].ToString();
                    hfRegNo.Value = ds.Tables[0].Rows[0]["GST_NUMBER"].ToString();
                    hfFacebkId.Value = ds.Tables[0].Rows[0]["FACEBOOK"].ToString();
                    hfCustomerType.Value = ds.Tables[0].Rows[0]["SERVICE_TYPE"].ToString();
                    hfIsCard.Value = ds.Tables[0].Rows[0]["CARD_TYPE_ID"].ToString();

                    Session.Add("HOLD_MSG", ds.Tables[0].Rows[0]["HOLD_MSG"].ToString());
                    Session.Add("RIDE_MSG", ds.Tables[0].Rows[0]["RIDE_MSG"].ToString());

                    if (ds.Tables[0].Rows[0]["SHOW_LOGO"].ToString() == "True")
                    {
                        imgLogo.Src = "../Pics/" + ds.Tables[0].Rows[0]["PIC"].ToString();
                        imgLogo2.Src = "../Pics/" + ds.Tables[0].Rows[0]["PIC"].ToString();
                    }

                    string url = HttpContext.Current.Request.Url.AbsoluteUri;
                }
                else
                {
                    // Server.Transfer("Home.aspx");
                    Response.Redirect("Home.aspx");
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    hfCompanyEmail.Value = ds.Tables[1].Rows[0]["EMAIL_ADDRESS"].ToString();
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    hfCan_DineIn.Value = ds.Tables[2].Rows[0]["Can_DineIn"].ToString();
                    hfCan_Delivery.Value = ds.Tables[2].Rows[0]["Can_Delivery"].ToString();
                    hfCan_TakeAway.Value = ds.Tables[2].Rows[0]["Can_TakeAway"].ToString();

                    //if (hfCustomerType.Value == "Dine In")
                    //{
                    //    if (hfCan_DineIn.Value == "False")
                    //    {
                    //        hfCustomerType.Value = "Delivery";
                    //    }
                    //}
                    ////====================================================================

                    //else if (hfCustomerType.Value == "Delivery")
                    //{
                    //    if ((hfCan_Delivery.Value == "False") && (hfCan_DineIn.Value == "False"))
                    //    {
                    //        hfCustomerType.Value = "Takeaway";
                    //    }

                    //}

                    ////====================================================================
                    //else
                    //{
                    //    if ((hfCan_TakeAway.Value == "False") && (hfCan_DineIn.Value == "False"))
                    //    {
                    //        hfCustomerType.Value = "Dine In";
                    //    }
                    //}

                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    hfReport.Value = GetJson(ds.Tables[3]);

                }
                if (ds.Tables[4].Rows.Count > 0)//sLIP notes
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    foreach (DataRow dr in ds.Tables[4].Rows)
                    {
                        sb.Append(dr["SLIP_NOTE"]);
                        sb.Append("<br />");
                    }
                    ltrlSlipNote.Text = sb.ToString();
                }
                #endregion

                #region Stock Validation

                ConfigurationController _cController = new ConfigurationController();
                DataTable dt = _cController.SelectAppConfiguration(1, (int)Enums.AppSettingMaster.InventorySetting, null, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));

                if (dt.Rows.Count > 0)
                {
                    DataRow[] dr = dt.Select("CODE = '" + (int)Enums.AppSetting.ShowClosingStockStatus + "'");
                    ClosingStockStatus = (GetConfigValue((int)Enums.AppSetting.ShowClosingStockStatus, dt, dr) == "1") ? true : false;
                    hfStockStatus.Value = Convert.ToString(ClosingStockStatus);

                    DataRow[] dr2 = dt.Select("CODE = '" + (int)Enums.AppSetting.NegativeAllowed + "'");
                    OversaleAllowed = (GetConfigValue((int)Enums.AppSetting.NegativeAllowed, dt, dr2) == "1") ? true : false;

                }
                DataTable dtFinance = _cController.SelectAppConfiguration(1, (int)Enums.AppSettingMaster.FinanceSetting, null, Constants.IntNullValue);
                if (dtFinance.Rows.Count > 0)
                {
                    DataRow[] drFinance = dtFinance.Select("CODE = '" + (int)Enums.AppSetting.IsFinanceIntegrate + "'");
                    if (drFinance.Length > 0)
                    {
                        IsFinanceIntegrate = Convert.ToInt32(drFinance[0][2]) == 1 ? true : false;
                        dtCOAConfig = GetCOAConfiguration();
                    }
                }
                #endregion
            }
        }

        //=======  Page Load #Stock Validation region==============\\
        public string GetConfigValue(int Code, DataTable dt, DataRow[] dr)
        {
            try
            {
                dr = dt.Select("CODE = '" + Code + "' ");
                return dr[0][2].ToString();
            }
            catch (Exception EX)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + EX + "');", true);
                return null;
            }
        }

        private DataTable GetCOAConfiguration()
        {
            try
            {
                COAMappingController _cController = new COAMappingController();
                DataTable dtCOAConfig = _cController.SelectCOAConfiguration(5, Constants.ShortNullValue, Constants.LongNullValue, "Level 4");

                if (dtCOAConfig.Rows.Count > 0)
                {
                    return dtCOAConfig;
                }

                return null;
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Plz Configure Financial Integration Settings');", true);

                return null;
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

        //==========================================on Page Load
        [WebMethod]
        [ScriptMethod]
        public static string LoadSaleForce(string customerType)
        {
            int customerTypeId = Constants.SALES_FORCE_ORDERBOOKER;

            if (customerType == "Delivery")
            {
                customerTypeId = Constants.SALES_FORCE_DELIVERYMAN;
            }

            SaleForceController mDController = new SaleForceController();
            DataTable dt = mDController.SelectSaleForceAssignedArea(customerTypeId, int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString())
                , Constants.IntNullValue, int.Parse(HttpContext.Current.Session["companyId"].ToString()), Constants.IntNullValue);

            return GetJson(dt);
        }


        //on Page Load, After Payment Save, Cancel
        [WebMethod]
        [ScriptMethod]
        public static string LoadDiscountUser()
        {
            SaleForceController mDController = new SaleForceController();

            DataSet ds = mDController.SelectSaleForceUsers(0, int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()),
                 int.Parse(HttpContext.Current.Session["companyId"].ToString()));

            return ds.GetXml();
            //return GetJson(ds);
        }


        [WebMethod]
        public static string LoadMaxOrderNo()
        {
            GeoHierarchyController _gCtl = new GeoHierarchyController();
            string Id = _gCtl.SelectMaxOrderNo(int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()), DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString()));
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
        public static string LoadAllProducts()
        {
            var mSkuController = new SkuController();
            try
            {
                DataTable dtSkus = mSkuController.SelectSkusforOrder(int.Parse(HttpContext.Current.Session["CompanyId"].ToString()),
                        Constants.IntNullValue, 1, Constants.IntNullValue,
                        int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()), 1);

                return GetJson(dtSkus);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [WebMethod]
        public static string LoadProductStatus(int id)
        {
            var mSkuController = new SkuController();
            try
            {
                DataTable dtSkus = mSkuController.SelectSkusforOrder(int.Parse(HttpContext.Current.Session["CompanyId"].ToString()),
                        Constants.IntNullValue, 1, id,
                        int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()), 2);

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
        public static string SelectPendingBills(string customerType)
        {
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
            DataTable dtSkus = mSkuController.SelectPendingBills(Constants.LongNullValue,
                DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString()), int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()), int.Parse(HttpContext.Current.Session["UserID"].ToString()), customerTypeId, 4);
            return GetJson(dtSkus);
        }

        [WebMethod]
        public static string GetPendingBill(long saleInvoiceMasterId)
        {
            var mSkuController = new SkuController();
            DataTable dtSkus = mSkuController.SpGetPendingBill(saleInvoiceMasterId);

            return GetJson(dtSkus);
        }

        [WebMethod]
        [ScriptMethod]
        public static string GetCentralizedOrders()
        {
            var mSkuController = new OrderEntryController();
            DataTable dtSkus = mSkuController.SelectPendingBills(Constants.LongNullValue,
                DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString()), int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()), int.Parse(HttpContext.Current.Session["UserID"].ToString()), Constants.IntNullValue, 8);
            return GetJson(dtSkus);
        }


        #endregion

        #region Loyalty 

        [WebMethod]
        [ScriptMethod]
        public static string LoadLoyaltyCardDetail(string cardNo)
        {

            var mController = new LoyaltyController();
            try
            {
                DataTable dtCardDetail = mController.GetLoyaltyCardDetail(cardNo, DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString()));

                return GetJson(dtCardDetail);
            }
            catch (Exception)
            {

                throw;
            }

        }


        #endregion

        #region Customer


        [WebMethod]
        [ScriptMethod]
        public static string LoadAllCustomers(string customerName, string type)
        {

            var mController = new CustomerDataController();
            try
            {
                DataTable dtCustomers = mController.UspSelectCustomer(int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString())
                    , type, customerName, DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString()));

                return GetJson(dtCustomers);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [WebMethod]
        [ScriptMethod]
        public static void InsertCustomer(string cardID, string CNIC, string contactNumer, string contactNumer2, string customerName, string address, string DOB, string OpeningAmount, string Nature, string email)
        {
            try
            {

                //  int userId = int.Parse(HttpContext.Current.Session["UserID"].ToString());

                int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());

                var dc = new DataControl();
                //   if (1 > 0)
                //   {
                CustomerDataController.InsertCustomer(distributerId, cardID, CNIC, DOB, contactNumer, email, customerName, address
                    , null, Convert.ToDecimal(dc.chkNull_0(OpeningAmount)), Nature, contactNumer2, 0, 0, 0
                    , 0, 0);
                //  }
                //   else
                //   {

                // }
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
            , string maxOrderNo, string printType, string tableName, string takeAwayCustomer, string bookerName, string tabId,string remarks)
        {
            if (IsDayClosed())
            {
                UserController UserCtl = new UserController();

                UserCtl.InsertUserLogoutTime(Convert.ToInt32(HttpContext.Current.Session["User_Log_ID"]), Convert.ToInt32(HttpContext.Current.Session["UserID"]));
                HttpContext.Current.Session.Clear();
                System.Web.Security.FormsAuthentication.SignOut();
                HttpContext.Current.Response.Redirect("../Login.aspx");
            }

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
                dtValue = new DataTable();

                int userId = int.Parse(HttpContext.Current.Session["UserID"].ToString());
                DateTime currentWorkDate = DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString());
                int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());

                if (orderedProducts != "")
                {
                    dtValue = (DataTable)JsonConvert.DeserializeObject(orderedProducts, (typeof(DataTable)));
                }

                #region Print

                if (printType == "1")
                {

                    HttpContext.Current.Session.Add("maxOrderNo", maxOrderNo);
                    HttpContext.Current.Session.Add("customerType", customerType);
                    HttpContext.Current.Session.Add("tableName", tableName);
                    HttpContext.Current.Session.Add("bookerName", bookerName);

                    HttpContext.Current.Session.Add("takeAwayCustomer", takeAwayCustomer);

                    if (dtValue.Rows.Count > 0)
                    {
                        dtSection = new DataTable();
                        dtSection.Columns.Add("SECTION", typeof(string));

                        foreach (DataRow dr in dtValue.Rows)
                        {
                            if (dr["PRINT"].ToString() == "true")
                            {
                                decimal qty = Convert.ToDecimal(dr["QTY"].ToString()) - Convert.ToInt32(dr["PR_COUNT"].ToString());
                                string IS_VOID = dr["VOID"].ToString();

                                if (IS_VOID == "false")
                                {
                                    if (qty > 0)
                                    {

                                        DataRow drSection = dtSection.NewRow();
                                        drSection["SECTION"] = dr["SECTION"];
                                        dtSection.Rows.Add(drSection);
                                    }
                                }
                            }
                        }
                        DataView view = new DataView(dtSection);
                        dtSection = view.ToTable(true, "SECTION");
                    }


                    if (dtSection.Rows.Count > 0)
                    {
                        _section = null;

                        foreach (DataRow dr in dtSection.Rows)
                        {
                            _section = dr.ItemArray[0].ToString();

                            PrintReport();
                        }

                    }


                }
                #endregion

                if (dtValue != null && dtValue.Rows.Count > 0 &&
                    dtValue.Rows[0]["INVOICE_ID"].ToString().Trim() != "N/A")
                {
                    OrderEntryController.HoldOrder(
                        long.Parse(dtValue.Rows[0]["INVOICE_ID"].ToString()), Constants.IntNullValue,
                        customerTypeId, tableId, 0, 0, 0, 0, 0, true, false, userId, currentWorkDate, distributerId, 0, null,
                        dtValue, int.Parse(orderBooker), coverTable, takeAwayCustomer, OversaleAllowed,customerId, remarks);
                }
                else
                {
                    OrderEntryController.Add_Invoice(Constants.IntNullValue, customerTypeId, tableId, 0, 0, 0, true,
                       userId, currentWorkDate, distributerId, dtValue, int.Parse(orderBooker), coverTable, customerId, maxOrderNo, takeAwayCustomer,Constants.IntNullValue,remarks);
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

        //Run on Print Invoice
        [WebMethod]
        [ScriptMethod]
        public static void UpdateOrder(string orderedProducts, string amountDue, string discount,
             string Gst, string DiscType, string approvedBy, string gstPerAge, string Service, string takeAwayCustomer, int cardType
            , string cardNo, decimal points, decimal purchasing, string customerID,string remarks)
        {
            try
            {
                if (IsDayClosed())
                {
                    UserController UserCtl = new UserController();

                    UserCtl.InsertUserLogoutTime(Convert.ToInt32(HttpContext.Current.Session["User_Log_ID"]), Convert.ToInt32(HttpContext.Current.Session["UserID"]));
                    HttpContext.Current.Session.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    HttpContext.Current.Response.Redirect("../Login.aspx");
                }

                if (DiscType == "null")
                {
                    DiscType = "0";
                }
                long CustomerID = Constants.LongNullValue;

                CustomerID = Convert.ToInt64(customerID);
                var dc = new DataControl();
                int userId = int.Parse(HttpContext.Current.Session["UserID"].ToString());
                DateTime currentWorkDate = DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString());
                int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());

                var dtValue = (DataTable)JsonConvert.DeserializeObject(orderedProducts, (typeof(DataTable)));

                if (dtValue != null && dtValue.Rows.Count > 0 &&
                    long.Parse(dtValue.Rows[0]["INVOICE_ID"].ToString()) > 0)
                {
                    OrderEntryController.Update_Order(
                        long.Parse(dtValue.Rows[0]["INVOICE_ID"].ToString()), int.Parse("0"),
                        Constants.IntNullValue, Constants.IntNullValue, decimal.Parse(amountDue), decimal.Parse(dc.chkNull_0(discount)),
                        decimal.Parse(dc.chkNull_0(Gst)), decimal.Parse(dc.chkNull_0("0")), Convert.ToDecimal(gstPerAge), true, true, userId,
                        currentWorkDate, distributerId, int.Parse(dc.chkNull_0(DiscType)), approvedBy, dtValue, Convert.ToDecimal(dc.chkNull_0(Service))
                        , takeAwayCustomer, cardType, cardNo, points, purchasing,CustomerID,remarks);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Run on Payment

        [WebMethod]
        [ScriptMethod]
        public static void InsertInvoice(string orderedProducts, string Type, string amountDue, string discount, string paidIn,
            string payType, string Gst, string DiscType, string approvedBy, string gstPerAge, string Service, string takeAwayCustomer,
             string empDiscType, string EMC_UserID, string Manager_UserID, string PASSWORD, string customerID, string cardNo,string purchasing,string remarks)
        {
            try
            {
                if (IsDayClosed())
                {
                    UserController UserCtl = new UserController();

                    UserCtl.InsertUserLogoutTime(Convert.ToInt32(HttpContext.Current.Session["User_Log_ID"]), Convert.ToInt32(HttpContext.Current.Session["UserID"]));
                    HttpContext.Current.Session.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    HttpContext.Current.Response.Redirect("../Login.aspx");
                }

                int customerTypeId = CustomerType(Type);
                long CustomerID = Constants.LongNullValue;
                string CardNo = null;
                decimal Purchasing = Constants.DecimalNullValue;
                if (DiscType == "null")
                {
                    DiscType = "0";
                }
                if (short.Parse(empDiscType) == 2)
                {
                    CardNo = cardNo;
                    Purchasing = Convert.ToDecimal(purchasing);
                }
                CustomerID = Convert.ToInt64(customerID);
                var dc = new DataControl();
                try
                {
                    Service = Convert.ToDecimal(Service).ToString();
                }
                catch (Exception ex)
                {
                    Service = "0";
                }     
                
                int userId = int.Parse(HttpContext.Current.Session["UserID"].ToString());
                DateTime currentWorkDate = DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString());
                int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());

                var dtValue = (DataTable)JsonConvert.DeserializeObject(orderedProducts, (typeof(DataTable)));

                if (dtValue != null && dtValue.Rows.Count > 0 &&
                    long.Parse(dtValue.Rows[0]["INVOICE_ID"].ToString()) > 0)
                {
                    OrderEntryController.Update_Invoice(
                        long.Parse(dtValue.Rows[0]["INVOICE_ID"].ToString()), int.Parse(payType),
                        customerTypeId, Constants.IntNullValue, decimal.Parse(amountDue), decimal.Parse(dc.chkNull_0(discount)),
                        decimal.Parse(dc.chkNull_0(Gst)), decimal.Parse(dc.chkNull_0(paidIn)), Convert.ToDecimal(gstPerAge), false, true, userId,
                        currentWorkDate, distributerId, int.Parse(dc.chkNull_0(DiscType)), approvedBy, dtValue, Convert.ToDecimal(dc.chkNull_0(Service)), takeAwayCustomer
                        , short.Parse(empDiscType), int.Parse(EMC_UserID), int.Parse(Manager_UserID), PASSWORD, CustomerID, CardNo,Purchasing, remarks, IsFinanceIntegrate, dtCOAConfig);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Run on Void Payment
        [WebMethod]
        [ScriptMethod]
        public static void VoidOrder(long orderId)
        {
            try
            {
                if (IsDayClosed())
                {
                    UserController UserCtl = new UserController();

                    UserCtl.InsertUserLogoutTime(Convert.ToInt32(HttpContext.Current.Session["User_Log_ID"]), Convert.ToInt32(HttpContext.Current.Session["UserID"]));
                    HttpContext.Current.Session.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    HttpContext.Current.Response.Redirect("../Login.aspx");
                }


                int userId = int.Parse(HttpContext.Current.Session["UserID"].ToString());
                DateTime currentWorkDate = DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString());
                int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());


                string i = OrderEntryController.Void_Order(orderId, userId, currentWorkDate, distributerId, null, 1);
                if (i == "0")
                {
                    throw new Exception("You are not authorized!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static void DirectSale(string orderedProducts, string Type, string amountDue, string discount, string paidIn,
            string payType, string Gst, string DiscType, string approvedBy, string gstPerAge, string Service, string takeAwayCustomer,
             string empDiscType, string EMC_UserID, string Manager_UserID, string PASSWORD, string customerID, string cardNo, string purchasing,string maxOrderNo,string remarks)
        {
            try
            {
                if (IsDayClosed())
                {
                    UserController UserCtl = new UserController();

                    UserCtl.InsertUserLogoutTime(Convert.ToInt32(HttpContext.Current.Session["User_Log_ID"]), Convert.ToInt32(HttpContext.Current.Session["UserID"]));
                    HttpContext.Current.Session.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    HttpContext.Current.Response.Redirect("../Login.aspx");
                }

                int customerTypeId = CustomerType(Type);
                int tableID = 1;//for Direct Sale
                string orderNo = maxOrderNo;
                long CustomerID = Convert.ToInt64(customerID);
                string CardNo = null;
                decimal Purchasing = Constants.DecimalNullValue;
                if (DiscType == "null")
                {
                    DiscType = "0";
                }
                if (short.Parse(empDiscType) == 2)
                {   
                    CardNo = cardNo;
                    Purchasing = Convert.ToDecimal(purchasing);
                }
                var dc = new DataControl();

                int userId = int.Parse(HttpContext.Current.Session["UserID"].ToString());
                DateTime currentWorkDate = DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString());
                int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());

                var dtValue = (DataTable)JsonConvert.DeserializeObject(orderedProducts, (typeof(DataTable)));

                if (dtValue != null && dtValue.Rows.Count > 0)
                {
                    OrderEntryController.Insert_Direct_Invoice(int.Parse(payType),customerTypeId, tableID, decimal.Parse(amountDue), decimal.Parse(dc.chkNull_0(discount)),
                        decimal.Parse(dc.chkNull_0(Gst)), decimal.Parse(dc.chkNull_0(paidIn)), Convert.ToDecimal(gstPerAge), false, true, userId,
                        currentWorkDate, distributerId, int.Parse(dc.chkNull_0(DiscType)), approvedBy, dtValue, Convert.ToDecimal(dc.chkNull_0(Service)), takeAwayCustomer
                        , short.Parse(empDiscType), int.Parse(EMC_UserID), int.Parse(Manager_UserID), PASSWORD, CustomerID, CardNo, Purchasing,orderNo,remarks, IsFinanceIntegrate, dtCOAConfig);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Reports

        [WebMethod]
        [ScriptMethod]
        public static string SelectSalesSummary()
        {
            RptSaleController rptSaleCtl = new RptSaleController();
            DataTable dt =
                rptSaleCtl.SelectSaleReport(int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()),
                    int.Parse(HttpContext.Current.Session["UserId"].ToString()),
                    DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString()), 1);
            return GetJson(dt);
        }

        [WebMethod]
        [ScriptMethod]
        public static string SelectSalesDetail()
        {
            RptSaleController rptSaleCtl = new RptSaleController();
            DataTable dt =
                rptSaleCtl.SelectSaleReport(int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()),
                    int.Parse(HttpContext.Current.Session["UserId"].ToString()),
                    DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString()), 2);
            return GetJson(dt);
        }

        [WebMethod]
        [ScriptMethod]
        public static string SelectItemSalesDetail()
        {
            RptSaleController rptSaleCtl = new RptSaleController();
            DataTable dt =
                rptSaleCtl.SelectSaleReport(int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()),
                    int.Parse(HttpContext.Current.Session["UserId"].ToString()),
                    DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString()), 3);
            return GetJson(dt);
        }

        #endregion

        #region Table

        [WebMethod]
        public static string SelectPendingTables()
        {
            var mTableController = new OrderEntryController();

            DataTable dtTables = mTableController.SelectPendingBills(Constants.LongNullValue,
            DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString()), int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()),Constants.IntNullValue, Constants.IntNullValue, 1);

            return GetJson(dtTables);
        }

        #endregion

        #region Print-INvoice
        public DataTable GetAppConfiguration()
        {
            try
            {
                ConfigurationController _cController = new ConfigurationController();
                DataTable dt = _cController.SelectAppConfiguration(1, Constants.IntNullValue, null, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
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

        private static void PrintReport()
        {
            try
            {
                PrintDocument prnDocument = new PrintDocument();
                prnDocument.PrinterSettings.PrinterName = (System.Configuration.ConfigurationManager.AppSettings["PrinterName"]);
                // The Event of 'PrintPage'

                prnDocument.PrintPage += new PrintPageEventHandler(prnDocument_PrintPage);

                prnDocument.Print();


                //PrintDocument prnDocument = new PrintDocument();
                //DataRow[] dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.PrinterName + "'");
                //prnDocument.PrinterSettings.PrinterName = dr[0]["VALUE"].ToString();
                //// The Event of 'PrintPage'

                //prnDocument.PrintPage += new PrintPageEventHandler(prnDocument_PrintPage);

                //prnDocument.Print();

            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                throw;
            }
        }

        private static void prnDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            leftMargin = (int)e.MarginBounds.Left;//100
            rightMargin = (int)e.MarginBounds.Right;//215
            topMargin = (int)e.MarginBounds.Top;//100
            topMargin = 1;
            bottomMargin = (int)e.MarginBounds.Bottom;//1069
            InvoiceWidth = (int)e.MarginBounds.Width;//115
            InvoiceHeight = (int)e.MarginBounds.Height;//969

            SetInvoiceHead(e.Graphics); // Draw Invoice Head
            SetInvoiceData(e.Graphics, e); // Draw Invoice Data
        }


        private static void SetInvoiceHead(Graphics g)
        {
            CurrentY = topMargin;
            CurrentX = leftMargin;

            InvTitleHeight = (int)(InvTitleFont.GetHeight(g));
            InvSubTitleHeight = (int)(InvSubTitleFont.GetHeight(g));

            // Get Titles Length:

            int lenInvSubTitle3 = (int)g.MeasureString(InvSubTitle3, InvSubTitleFont).Width;
            int lenInvSubTitle4 = (int)g.MeasureString(InvSubTitle4, InvSubTitleFont).Width;
            // Set Titles Left:

            int xInvSubTitle3 = CurrentX + (InvoiceWidth - lenInvSubTitle3) / 2;
            int xInvSubTitle4 = CurrentX + (InvoiceWidth - lenInvSubTitle4) / 2;

            InvSubTitle3 = "SECTION: " + _section;

            if (InvSubTitle3 != "")
            {
                CurrentY = CurrentY + InvSubTitleHeight + 10;
                g.DrawString(InvSubTitle3, InvSubTitleFont, BlueBrush, 10, CurrentY);
            }
            CurrentY = CurrentY + 25;
            g.DrawString("Service Type: " + HttpContext.Current.Session["customerType"], InvoiceFont, BlueBrush, 10, CurrentY);

            if (HttpContext.Current.Session["customerType"].ToString() == "Delivery")
            {
                CurrentY = CurrentY + 20;
                g.DrawString("D-M: " + HttpContext.Current.Session["bookerName"], InvoiceFont, BlueBrush, 10, CurrentY);

                CurrentY = CurrentY + 20;
                g.DrawString("Customer: " + HttpContext.Current.Session["tableName"], InvoiceFont, BlueBrush, 10, CurrentY);
            }

            else if (HttpContext.Current.Session["customerType"].ToString() == "Dine In")
            {
                CurrentY = CurrentY + 20;
                g.DrawString("O-T: " + HttpContext.Current.Session["bookerName"], InvoiceFont, BlueBrush, 10, CurrentY);

                CurrentY = CurrentY + 20;
                g.DrawString("Table No: " + HttpContext.Current.Session["tableName"], InvoiceFont, BlueBrush, 10, CurrentY);
            }
            else
            {
                CurrentY = CurrentY + 20;
                g.DrawString("O-T: " + HttpContext.Current.Session["bookerName"], InvoiceFont, BlueBrush, 10, CurrentY);

                CurrentY = CurrentY + 20;
                g.DrawString("Customer: " + HttpContext.Current.Session["tableName"], InvoiceFont, BlueBrush, 10, CurrentY);
            }

            CurrentY = CurrentY + 20;
            g.DrawString("Date: " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt"), InvoiceFont2, BlueBrush, 10, CurrentY);
            g.DrawString(HttpContext.Current.Session["maxOrderNo"].ToString(), InvOrderTitleFont, BlueBrush, 200, CurrentY - 60);

            XNoOfuntit = (int)g.MeasureString("", InvoiceFont).Width + 180;
            YNoOfUnit = CurrentY;

            CurrentY = CurrentY + 25;
            g.DrawLine(new Pen(Brushes.Black, 2), 10, CurrentY, 300, CurrentY);//
            CurrentY = CurrentY + 5;

        }


        private static void SetInvoiceData(Graphics g, PrintPageEventArgs e)
        {

            // Set Invoice Table:
            string FieldValue = "";
            int CurrentRecord = 0;

            // Set Table Head:
            int xProductID = 10;//leftMargin;

            CurrentY = CurrentY + 4;//InvoiceFontHeight;
            g.DrawString("Item Name", InvoiceFont, BlueBrush, xProductID, CurrentY);

            int xProductName = xProductID + (int)g.MeasureString("Item Name", InvoiceFont).Width + 70;//162 Edit by safdar om 20160806
            g.DrawString("Qty", InvoiceFont, BlueBrush, xProductName, CurrentY);

            CurrentY = CurrentY + 20;
            g.DrawLine(new Pen(Brushes.Black, 2), 10, CurrentY, 300, CurrentY);
            CurrentY = CurrentY + 10;

            DataView dv = dtValue.DefaultView;
            dv.Sort = "C1";
            dtValue = dv.ToTable();

            if (dtValue.Rows.Count > 0)
            {
                foreach (DataRow dr in dtValue.Rows)
                {
                    if (dr["SECTION"].ToString() == _section)
                    {
                        decimal qty = Convert.ToDecimal(dr["QTY"].ToString()) - Convert.ToInt32(dr["PR_COUNT"].ToString());

                        string IS_VOID = dr["VOID"].ToString();

                        if (IS_VOID == "false")
                        {
                            if (qty > 0)
                            {
                                if (dr["IS_DESC"].ToString() == "true")
                                {
                                    FieldValue = Convert.ToString(dr["C1"]) + "-" + Convert.ToString(dr["SKU_NAME"]) + " (" + Convert.ToString(dr["DESC"]) + ")";
                                }
                                else
                                {
                                    FieldValue = Convert.ToString(dr["C1"]) + "-" + Convert.ToString(dr["SKU_NAME"]);// name
                                }
                                if (FieldValue.Length > 30)
                                {
                                    string FieldValue2 = FieldValue.Substring(0, 31);
                                    g.DrawString(FieldValue2, InvoiceFont2, BlackBrush, xProductID, CurrentY);

                                    FieldValue = FieldValue.Substring(31);
                                    g.DrawString(FieldValue, InvoiceFont2, BlackBrush, xProductID, CurrentY + 20);

                                    FieldValue = Convert.ToString(qty);// Qty
                                    g.DrawString(FieldValue, InvoiceFont2, BlackBrush, xProductName, CurrentY);

                                    CurrentY = CurrentY + 40;
                                    g.DrawLine(new Pen(Brushes.Black, 1), 10, CurrentY, 300, CurrentY);

                                }
                                else
                                {
                                    g.DrawString(FieldValue, InvoiceFont2, BlackBrush, xProductID, CurrentY);


                                    FieldValue = Convert.ToString(qty);// Qty
                                    g.DrawString(FieldValue, InvoiceFont2, BlackBrush, xProductName, CurrentY);

                                    CurrentY = CurrentY + 20;
                                    g.DrawLine(new Pen(Brushes.Black, 1), 10, CurrentY, 300, CurrentY);
                                }
                                CurrentY = CurrentY + 10;
                                CurrentRecord++;
                            }
                            if (Convert.ToDecimal(dr["QTY"].ToString()) == 0)
                            {
                                if (qty == 0)
                                {
                                    if (dr["IS_DESC"].ToString() == "true")
                                    {
                                        FieldValue = Convert.ToString(dr["C1"]) + "-" + Convert.ToString(dr["SKU_NAME"]) + " (" + Convert.ToString(dr["DESC"]) + ")";
                                    }
                                    else
                                    {
                                        FieldValue = Convert.ToString(dr["C1"]) + "-" + Convert.ToString(dr["SKU_NAME"]);// name
                                    }
                                    if (FieldValue.Length > 30)
                                    {
                                        string FieldValue2 = FieldValue.Substring(0, 31);
                                        g.DrawString(FieldValue2, InvoiceFont2, BlackBrush, xProductID, CurrentY);

                                        FieldValue = FieldValue.Substring(31);
                                        g.DrawString(FieldValue, InvoiceFont2, BlackBrush, xProductID, CurrentY + 20);

                                        FieldValue = Convert.ToString(qty);// Qty
                                        g.DrawString(FieldValue, InvoiceFont2, BlackBrush, xProductName, CurrentY);

                                        CurrentY = CurrentY + 40;
                                        g.DrawLine(new Pen(Brushes.Black, 1), 10, CurrentY, 300, CurrentY);

                                    }
                                    else
                                    {
                                        g.DrawString(FieldValue, InvoiceFont2, BlackBrush, xProductID, CurrentY);


                                        FieldValue = Convert.ToString(qty);// Qty
                                        g.DrawString(FieldValue, InvoiceFont2, BlackBrush, xProductName, CurrentY);

                                        CurrentY = CurrentY + 20;
                                        g.DrawLine(new Pen(Brushes.Black, 1), 10, CurrentY, 300, CurrentY);
                                    }
                                    CurrentY = CurrentY + 10;
                                    CurrentRecord++;
                                }
                            }
                        }
                    }
                }
            }

            if (CurrentRecord > 0)
            {

                g.Dispose();
            }
        }



        #endregion

        [WebMethod]
        [ScriptMethod]
        public static string ValidateUser(string UserId, string UserPass, string UserClick)
        {
            var _mController = new UserController();
            var mSkuController = new OrderEntryController();

            int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());

            DataRow[] dr = dtConfigDefault.Select("CODE = '" + (int)Enums.AppSetting.IsEncreptedCredentials + "'");
            bool IsEncrypted = Convert.ToBoolean(Convert.ToInt32(dr[0]["VALUE"]));

            if (IsEncrypted)
            {
                dr = dtConfigDefault.Select("CODE = '" + (int)Enums.AppSetting.Deployed + "'");
                if (dr[0]["VALUE"].ToString() == Cryptography.Encrypt("Deployed", "b0tin@74"))
                {
                    UserPass = Cryptography.Encrypt(UserPass, "b0tin@74");
                }
            }

            DataTable dt = _mController.SelectValidateUser(distributerId, UserId, UserPass);
            if (dt != null)
            {
                DataRow[] foundRow = null;
                if (UserClick == "Delete")
                {
                    foundRow = dt.Select("IsDelRight=" + 1);
                }
                else
                {
                    foundRow = dt.Select("IsLessRight=" + 1);
                }
                if (foundRow.Length > 0)
                {
                    return GetJson(dt);
                }
                return GetJson(null);
            }
            return GetJson(null);

        }

        protected void lnkExit_OnClick(object sender, EventArgs e)
        {

            Response.Redirect("Home.aspx");

        }

        private static bool IsDayClosed()
        {
            DistributorController DistrCtl = new DistributorController();
            try
            {
                DataTable dtDayClose = DistrCtl.MaxDayClose(Convert.ToInt32(HttpContext.Current.Session["DISTRIBUTOR_ID"]), 3);
                if (dtDayClose.Rows.Count > 0)
                {
                    if (Convert.ToDateTime(HttpContext.Current.Session["CurrentWorkDate"]) == Convert.ToDateTime(dtDayClose.Rows[0]["DayClose"]))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                throw;

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