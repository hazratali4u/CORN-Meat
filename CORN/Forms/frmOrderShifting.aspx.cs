using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using Newtonsoft.Json;


namespace Forms

{
    public partial class FormsfrmOrderShifting : System.Web.UI.Page
    {
        static DataTable dtValue;
        

        private readonly GeoHierarchyController _gCtl = new GeoHierarchyController();

        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            Response.AppendHeader("pragma", "no-cache");


            if (!Page.IsPostBack)
            {

                if (IsDayClosed())
                {
                    UserController UserCtl = new UserController();

                    UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
                    Session.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    Response.Redirect("../Login.aspx");
                }

                txtCustomerDOB.Attributes.Add("readonly", "readonly");

                
                lblUserName.Text = Session["UserName"].ToString();
                DateTime date = DateTime.Parse(Session["CurrentWorkDate"].ToString());
                lblDateTime.Text = date.ToString("dd-MMMM-yyyy");
                hfCurrentWorkDate.Value = date.ToString("dd-MMM-yyyy");
                hfUserId.Value = HttpContext.Current.Session["UserID"].ToString();//for default user add in orderbooker dropdown

                #region Location Information

                DataSet ds = _gCtl.SelectDataForPosLoad(Constants.IntNullValue,
                    int.Parse(Session["DISTRIBUTOR_ID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString())
                    , int.Parse(Session["RoleID"].ToString()));

                if (ds.Tables[0].Rows.Count > 0)
                {
                     hfIsCoverTable.Value = ds.Tables[0].Rows[0]["ISCOVERTABLE"].ToString();
                    hfLocationName.Value = ds.Tables[0].Rows[0]["DISTRIBUTOR_NAME"].ToString();
                    hfAddress.Value = ds.Tables[0].Rows[0]["ADDRESS1"].ToString();
                    hfPhoneNo.Value = ds.Tables[0].Rows[0]["CONTACT_NUMBER"].ToString();
                   hfCustomerType.Value = ds.Tables[0].Rows[0]["SERVICE_TYPE"].ToString();
                    
                    if (ds.Tables[0].Rows[0]["SHOW_LOGO"].ToString() == "True")
                    {
                        imgLogo.Src = "../Pics/" + ds.Tables[0].Rows[0]["PIC"].ToString();
                    }

                    string url = HttpContext.Current.Request.Url.AbsoluteUri;
                }
                else
                {
                    // Server.Transfer("Home.aspx");
                    Response.Redirect("Home.aspx");
                }
                
                
                #endregion

               
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
                    , Constants.DecimalNullValue, Constants.DecimalNullValue);
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

        #region  Order Shift

        [WebMethod]
        [ScriptMethod]
        public static void ShiftOrder(long invoiceId, string orderBooker, string coverTable, string customerType, long CustomerID
                                    , string takeAwayCustomer, string bookerName, string tabId)
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

            int customerTypeId = 0;
            int tableId = 0;

            if (customerType == "Dine In")
            {
                customerTypeId = 1;

                if (tabId == "0")
                {
                    try
                    {
                        throw new ArgumentException("Please wait Transaction in progress");
                    }
                    catch (ArgumentException ex)
                    {
                        throw new ArgumentException("Please wait Transaction in progress");
                    }
                }
                else
                {

                    tableId = int.Parse(tabId);
                }
            }
            else if (customerType == "Delivery")
            {
                customerTypeId = 2;

                if (CustomerID == 0)
                {
                    try
                    {
                        throw new ArgumentException("Please wait Transaction in progress");
                    }
                    catch (ArgumentException ex)
                    {
                        throw new ArgumentException("Please wait Transaction in progress");
                    }
                }
            }
            else
            {
                customerTypeId = 3;

            }

            #endregion

            try
            {
                dtValue = new DataTable();

                int userId = int.Parse(HttpContext.Current.Session["UserID"].ToString());
                DateTime currentWorkDate = DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString());
                int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());

                    OrderEntryController.ShiftOrder(
                        invoiceId, customerTypeId, tableId
                         ,userId, currentWorkDate, distributerId 
                         ,int.Parse(orderBooker), coverTable, takeAwayCustomer, CustomerID);
                

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
        

        #region Table

        [WebMethod]
        public static string SelectPendingTables()
        {
            var mTableController = new OrderEntryController();

            DataTable dtTables = mTableController.SelectPendingBills(Constants.LongNullValue,
            DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString()), int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()), int.Parse(HttpContext.Current.Session["UserID"].ToString()), Constants.IntNullValue, 1);

            return GetJson(dtTables);
        }

        #endregion
        
        

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
        
    }
}