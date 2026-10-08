using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

public partial class Forms_frmRollbackInvoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            //if (IsDayClosed())
            //{
            //    UserController UserCtl = new UserController();

            //    UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
            //    Session.Clear();
            //    System.Web.Security.FormsAuthentication.SignOut();
            //    Response.Redirect("../Login.aspx");
            //}
            LoadDistributor();
            LoadLegend();
        }
    }

    #region Load

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.GetDistributorWithMaxDayClose(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()), 1);
        clsWebFormUtil.FillDxComboBoxList(drpDistributor, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");

        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedIndex = 0;
        }
        Session.Add("dtLocationInfo", dt);
    }

    private void LoadLegend()
    {
        OrderEntryController or = new OrderEntryController();
        DataTable m_dt = or.SelectLegend();
        clsWebFormUtil.FillDropDownList(this.DrpLenged, m_dt, 0, 2, true);
    }
    private void LoadOrderBooker()
    {
        if (drpDistributor.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable mDt = mDController.SelectRollBackInvoiceSaleForce(int.Parse(DrpDocumentType.SelectedValue), Constants.IntNullValue, Convert.ToInt32(drpDistributor.SelectedItem.Value));
            clsWebFormUtil.FillDropDownList(DrpOrderBooker, mDt, 0, 1, true);
        }
        else
        {
            DrpOrderBooker.Items.Clear();
        }
    }

    private void LoadRollbackDocument(DateTime CurrentWorkDate)
    {
        var or = new OrderEntryController();
        DataTable dtOrder = or.SelectRollBackDocument(Convert.ToInt32(drpDistributor.SelectedItem.Value), Constants.IntNullValue,
           Constants.IntNullValue, int.Parse(DrpDocumentType.SelectedValue), CurrentWorkDate);
        GrdOrder.DataSource = dtOrder;
        GrdOrder.DataBind();
    }

    private void LoadRollbackCheque()
    {
        ChequeEntryController CController = new ChequeEntryController();
        DataTable dt = CController.SelectChequeEntry(Constants.Cheque_Clear, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), Convert.ToInt32(drpDistributor.SelectedItem.Value), int.Parse(DrpPrincipal.SelectedValue.ToString()), 0);
        GrdCheque.DataSource = dt;
        GrdCheque.DataBind();
    }

    #endregion

    #region Index/Change

    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadOrderBooker();
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadPrincipal();
        //LoadOrderBooker();         
    }

    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadOrderBooker();
    }
    #endregion

    #region Click

    protected void btnPost_Click(object sender, EventArgs e)
    {
        //if (IsDayClosed())
        //{
        //    UserController UserCtl = new UserController();

        //    UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
        //    Session.Clear();
        //    System.Web.Security.FormsAuthentication.SignOut();
        //    Response.Redirect("../Login.aspx");
        //}

        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.Value.ToString())
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }
        if (CurrentWorkDate != Constants.DateNullValue)
        {
            if (DrpDocumentType.SelectedIndex == 3)
            {
                foreach (GridViewRow dr in GrdCheque.Rows)
                {
                    CheckBox ChbInvoice = (CheckBox)dr.FindControl("ChbInvoice");
                    if (ChbInvoice.Checked == true)
                    {
                        ChequeEntryController CController = new ChequeEntryController();
                        DataControl dc = new DataControl();
                        CController.RollbackChequeEntry(int.Parse(DrpPrincipal.SelectedValue.ToString()), Convert.ToInt32(drpDistributor.SelectedItem.Value), dr.Cells[6].Text, long.Parse(dr.Cells[0].Text), Constants.LongNullValue);
                    }
                }
                LoadRollbackCheque();
            }
            else
            {
                foreach (GridViewRow dr in GrdOrder.Rows)
                {
                    var chbInvoice = (CheckBox)dr.FindControl("ChbInvoice");
                    if (chbInvoice.Checked == true)
                    {
                        var ord = new OrderEntryController();

                        ord.UpdateRollBackDocument(Convert.ToInt64(GrdOrder.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(DrpDocumentType.SelectedValue), int.Parse(DrpLenged.SelectedValue));
                    }
                }
                LoadRollbackDocument(CurrentWorkDate);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Dayclose not found for selected location!')", true);
        }
    }
    protected void btnGetOrder_Click(object sender, EventArgs e)
    {
        //if (IsDayClosed())
        //{
        //    UserController UserCtl = new UserController();

        //    UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
        //    Session.Clear();
        //    System.Web.Security.FormsAuthentication.SignOut();
        //    Response.Redirect("../Login.aspx");
        //}

        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.Value.ToString())
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }

        if (CurrentWorkDate != Constants.DateNullValue)
        {
            if (DrpDocumentType.SelectedIndex == 3)
            {
                LoadRollbackCheque();
                GrdOrder.Visible = false;
                GrdCheque.Visible = true;
            }
            else
            {
                GrdOrder.Visible = true;
                GrdCheque.Visible = false;
                LoadRollbackDocument(CurrentWorkDate);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Dayclose not found for selected location!')", true);
        }
    }
    #endregion

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