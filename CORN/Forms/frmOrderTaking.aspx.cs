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


public partial class Forms_frmOrderTaking : System.Web.UI.Page
{
    //DataTable PurchaseSKU;
    DataControl DC = new DataControl();


    private static bool ClosingStockStatus;
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

            DataSet ds = _gCtl.SelectDataForPosLoad(int.Parse(hfUserId.Value),
                int.Parse(Session["DISTRIBUTOR_ID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), Constants.IntNullValue);


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


                Session.Add("MessageSMS", "Testing");

                if (ds.Tables[0].Rows[0]["SHOW_LOGO"].ToString() == "True")
                {
                    imgLogo.Src = "../Pics/" + ds.Tables[0].Rows[0]["PIC"].ToString();
                }
                string url = HttpContext.Current.Request.Url.AbsoluteUri;

            }
            else
            {
                Response.Redirect("Home.aspx");
                // Server.Transfer("Level.aspx?LevelID=27&LevelType=1");
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

            #endregion
            DataTable dtConfig = GetSMSConfiguration();
            if (dtConfig == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Configure Printer setting for this location');", true);
                hfBookingType.Value = "1";
            }
            else
            {
                DataRow[] dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.IsDirectPrinting + "'");
                if (Convert.ToBoolean(Convert.ToInt32(dr[0]["VALUE"])))
                {
                    hfBookingType.Value = "1";
                }
                else
                {
                    hfBookingType.Value = "0";
                }
            }
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
    public DataTable GetSMSConfiguration()
    {
        try
        {
            ConfigurationController _cController = new ConfigurationController();
            DataTable dt = _cController.SelectAppConfiguration(1, (int)Enums.AppSettingMaster.PrinterSettings, null, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
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
    public static string GetJson(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        //    serializer.MaxJsonLength = Int32.MaxValue;

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
    [ScriptMethod]
    public static string LoadSaleForce(string customerType)
    {
        int customerTypeId = Constants.SALES_FORCE_ORDERBOOKER;

        if (customerType == "Delivery")
        {
            customerTypeId = Constants.SALES_FORCE_DELIVERYMAN;
        }
        else
        {
            customerTypeId = Constants.SALES_FORCE_ORDERBOOKER;
        }

        SaleForceController mDController = new SaleForceController();
        DataTable dt = mDController.SelectSaleForceAssignedArea(customerTypeId, int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString())
            , Constants.IntNullValue, int.Parse(HttpContext.Current.Session["companyId"].ToString()), Constants.IntNullValue);

        return GetJson(dt);
    }

    #region Products

    [WebMethod]
    public static string LoadProductCategroy()
    {
        SkuHierarchyController mController = new SkuHierarchyController();
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, 1, null, null, true, 14, Constants.IntNullValue);
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        foreach (DataRow row in dt.Rows)
        {
            Dictionary<string, object> dRow = new Dictionary<string, object>
                {
                    {"CATEGORY_ID", row["SKU_HIE_ID"]},
                    {"CATEGORY_NAME", row["SKU_HIE_NAME"]}
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
        DataTable dtSkus = mSkuController.SelectSkusforOrder(int.Parse(HttpContext.Current.Session["CompanyId"].ToString()),
                Constants.IntNullValue, 1, Constants.IntNullValue,
                int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()),1);

        return GetJson(dtSkus);
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

        //  HttpContext.Current.Session["CustomerType"] = dtSkus.Rows[0]["CUSTOMER_TYPE_ID"];
        HttpContext.Current.Session["TableId"] = dtSkus.Rows[0]["TABLE_ID"];

        return GetJson(dtSkus);
    }

    #endregion


    #region DML

    [WebMethod]
    [ScriptMethod]
    public static void HoldOrder(string orderedProducts, string orderBooker, string coverTable, string customerType, string CustomerName, string maxOrderNo,string remarks)
    {
        try
        {
            int tableId = 0;
            int customerTypeId = 0;
            Int64 customerId = 0;

            if (customerType == "Dine In")
            {
                customerTypeId = 1;

            }
            else if (customerType == "Delivery")
            {
                customerTypeId = 2;
            }
            else
            {
                customerTypeId = 3;

            }
            customerId = Convert.ToInt64(CustomerName);
            //1 for DINE IN for others set table zero

            if (customerTypeId == 1)
            {
                tableId = int.Parse(HttpContext.Current.Session["TableId"].ToString());
            }

            int userId = int.Parse(HttpContext.Current.Session["UserID"].ToString());
            DateTime currentWorkDate = DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString());
            int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());
            DataTable dtValue = new DataTable();

            if (orderedProducts != "")
            {
                dtValue = (DataTable)JsonConvert.DeserializeObject(orderedProducts, (typeof(DataTable)));
            }

            if (dtValue != null && dtValue.Rows.Count > 0 &&
                dtValue.Rows[0]["SALE_INVOICE_MASTER_ID"].ToString().Trim() != "N/A")
            {
                OrderEntryController.HoldOrder(
                    long.Parse(dtValue.Rows[0]["SALE_INVOICE_MASTER_ID"].ToString()), Constants.IntNullValue,
                    customerTypeId, tableId, 0, 0, 0, 0, 0, true, false, userId, currentWorkDate, distributerId, 0, null,
                    dtValue, int.Parse(orderBooker), coverTable, "", false,customerId, remarks);
            }
            else
            {
                OrderEntryController.Add_Invoice(Constants.IntNullValue, customerTypeId, tableId, 0, 0, 0, true,
                   userId, currentWorkDate, distributerId, dtValue, int.Parse(orderBooker), coverTable, customerId, "", "",Constants.IntNullValue, remarks);
            }
        }
        catch
        {

        }
    }

    [WebMethod]
    [ScriptMethod]
    //Run on Print Invoice
    public static void UpdateOrder(string orderedProducts, string amountDue, string discount,
         string Gst, string DiscType, string approvedBy, string gstPerAge, string Service,string remarks)
    {
        try
        {

            if (DiscType == "null")
            {
                DiscType = "0";
            }

            var dc = new DataControl();




            // int tableId = int.Parse(HttpContext.Current.Session["TableId"].ToString());
            //   int customerTypeId = int.Parse(HttpContext.Current.Session["CustomerType"].ToString());

            int userId = int.Parse(HttpContext.Current.Session["UserID"].ToString());
            DateTime currentWorkDate = DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString());
            int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());

            var dtValue = (DataTable)JsonConvert.DeserializeObject(orderedProducts, (typeof(DataTable)));

            if (dtValue != null && dtValue.Rows.Count > 0 &&
                long.Parse(dtValue.Rows[0]["SALE_INVOICE_MASTER_ID"].ToString()) > 0)
            {
                OrderEntryController.Update_Order(
                    long.Parse(dtValue.Rows[0]["SALE_INVOICE_MASTER_ID"].ToString()), int.Parse("0"),
                    Constants.IntNullValue, Constants.IntNullValue, decimal.Parse(amountDue), decimal.Parse(dc.chkNull_0(discount)),
                    decimal.Parse(dc.chkNull_0(Gst)), decimal.Parse(dc.chkNull_0("0")), Convert.ToDecimal(gstPerAge), true, true, userId,
                    currentWorkDate, distributerId, int.Parse(dc.chkNull_0(DiscType)), approvedBy, dtValue, Convert.ToDecimal(dc.chkNull_0(Service)), "",0, null, 0,0,Constants.LongNullValue, remarks);
            }

        }
        catch
        {

        }
    }


    [WebMethod]
    [ScriptMethod]
    public static void InsertInvoice(string orderedProducts, string amountDue, string discount, string paidIn,
        string payType, string Gst, string DiscType, string approvedBy, string gstPerAge, string Service,string remarks)
    {
        try
        {
            if (DiscType == "null")
            {
                DiscType = "0";
            }

            var dc = new DataControl();

            // int tableId = int.Parse(HttpContext.Current.Session["TableId"].ToString());

            int userId = int.Parse(HttpContext.Current.Session["UserID"].ToString());
            DateTime currentWorkDate = DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString());
            int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());

            var dtValue = (DataTable)JsonConvert.DeserializeObject(orderedProducts, (typeof(DataTable)));

            if (dtValue != null && dtValue.Rows.Count > 0 &&
                long.Parse(dtValue.Rows[0]["SALE_INVOICE_MASTER_ID"].ToString()) > 0)
            {
                OrderEntryController.Update_Invoice(
                    long.Parse(dtValue.Rows[0]["SALE_INVOICE_MASTER_ID"].ToString()), int.Parse(payType),
                    Constants.IntNullValue, Constants.IntNullValue, decimal.Parse(amountDue), decimal.Parse(dc.chkNull_0(discount)),
                    decimal.Parse(dc.chkNull_0(Gst)), decimal.Parse(dc.chkNull_0(paidIn)), Convert.ToDecimal(gstPerAge), false, true, userId,
                    currentWorkDate, distributerId, int.Parse(dc.chkNull_0(DiscType)), approvedBy, dtValue, Convert.ToDecimal(dc.chkNull_0(Service)), ""
                    , Constants.ShortNullValue, Constants.IntNullValue, Constants.IntNullValue,null,Constants.LongNullValue,null,Constants.DecimalNullValue, remarks, false, null);
            }

        }
        catch
        {

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


        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        foreach (DataRow row in dtTables.Rows)
        {
            Dictionary<string, object> dRow = new Dictionary<string, object>
                {
                    {"TABLE_ID", row["TABLE_ID"]},
                    {"TABLE_NO", row["TABLE_NO"]}
                };
            rows.Add(dRow);
        }
        System.Web.Script.Serialization.JavaScriptSerializer jSearializer =
            new System.Web.Script.Serialization.JavaScriptSerializer();
        return jSearializer.Serialize(rows);
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



    [WebMethod]
    [ScriptMethod]
    public static string ValidateUser(string UserId, string UserPass, string UserClick)
    {
        var _mController = new UserController();
        var mSkuController = new OrderEntryController();

        int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());

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
        // Server.Transfer("Level.aspx?LevelID=27&LevelType=1");
        UserController UserCtl = new UserController();

        UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
        Session.Clear();
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect("../Login.aspx");
    }

    private bool IsDayClosed()
    {
        DistributorController DistrCtl = new DistributorController();
        try
        {
            DataTable dtDayClose = DistrCtl.MaxDayClose(Convert.ToInt32(Session["DISTRIBUTOR_ID"]), 3);
            if (dtDayClose.Rows.Count > 0)
            {
                if (Convert.ToDateTime(Session["CurrentWorkDate"]) == Convert.ToDateTime(dtDayClose.Rows[0]["DayClose"]))
                {
                    return false;
                }
            }
            return true;
        }
        catch (Exception)
        {

            throw;
        }
    }




}