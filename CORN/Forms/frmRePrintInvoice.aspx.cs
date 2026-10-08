using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

public partial class Forms_frmRePrintInvoice : System.Web.UI.Page
{
    

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

            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtFromDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtToDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtFromDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");

            hfCompanyName.Value = Session["COMPANY_NAME"].ToString();
            lblUserName.Text = Session["UserName"].ToString();
            
            hfUserId.Value = HttpContext.Current.Session["UserID"].ToString();//for default user add in orderbooker dropdown

            DataSet ds = _gCtl.SelectDataForPosLoad(Constants.IntNullValue,
                int.Parse(Session["DISTRIBUTOR_ID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString())
                , int.Parse(Session["RoleID"].ToString()));

            if (ds.Tables[0].Rows.Count > 0)
            {
                hfIsCoverTable.Value = ds.Tables[0].Rows[0]["ISCOVERTABLE"].ToString();
                hfLocationName.Value = ds.Tables[0].Rows[0]["DISTRIBUTOR_NAME"].ToString();
                hfAddress.Value = ds.Tables[0].Rows[0]["ADDRESS1"].ToString();
                hfPhoneNo.Value = ds.Tables[0].Rows[0]["CONTACT_NUMBER"].ToString();
                hfRegNo.Value = ds.Tables[0].Rows[0]["GST_NUMBER"].ToString();
                hfFacebkId.Value = ds.Tables[0].Rows[0]["FACEBOOK"].ToString();
                hfCustomerType.Value = ds.Tables[0].Rows[0]["SERVICE_TYPE"].ToString();

              
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

        }
    }
    public static string GetJson(DataTable dt)
    {

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        
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




    #region Bills

    [WebMethod]
    [ScriptMethod]
    public static string SelectPendingBills(DateTime FromDate, DateTime ToDate)
    {
        

        var mSkuController = new OrderEntryController();
        DataTable dtSkus = mSkuController.SelectPendingBills(FromDate, ToDate, int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()), 6);
        return GetJson(dtSkus);
    }


    [WebMethod]
    public static string GetPendingBill(long saleInvoiceMasterId)
    {
        var mSkuController = new SkuController();
        DataTable dtSkus = mSkuController.SpGetPendingBill(saleInvoiceMasterId,3);

        return GetJson(dtSkus);
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