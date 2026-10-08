using System;
using System.Web.UI;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;
using System.Data;

public partial class Forms_fmPrincipalOpening : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
            LoadPrincipal();

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
            txtOpeningDate.Text = CurrentWorkDate.ToString("dd-MMM-yyyy");
            LoadOpeningInformation();
            txtOpeningDate.Attributes.Add("readonly", "readonly");
        }
    }
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
    private void LoadPrincipal()
    {
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
        if (drpDistributor.Items.Count > 0)
        {
            var PController = new SKUPriceDetailController();
            DataTable dtVendor = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, CurrentWorkDate);

            clsWebFormUtil.FillDxComboBoxList(drpPrincipal, dtVendor, 0, 1, true);
            if (dtVendor.Rows.Count > 0)
            {
                drpPrincipal.SelectedIndex = 0;
            }
        }
    }

    #region Opening Balance

    private void LoadOpeningInformation()
    {
        PurchaseController mPurchase = new PurchaseController();

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
            txtOpeningDate.Text = CurrentWorkDate.ToString("dd-MMM-yyyy");

            txtOpeningBalance.Text = "0";
            txtOpeningBalanceRemarks.Text = "";
            rblOpening.SelectedValue = "0";
            masterId.Value = "0";
            btnSaveOpeningBalance.Enabled = true;
            btnSaveOpeningBalance.Text = "Save";

            if (drpPrincipal.Items.Count > 0)
            {
                DataTable dtOpening = mPurchase.SelectPrincipalOpening(Convert.ToInt32(drpDistributor.SelectedItem.Value), Convert.ToInt32(drpPrincipal.SelectedItem.Value));
                if (dtOpening.Rows.Count > 0)
                {
                    if (string.Format("{0:0.00}", dtOpening.Rows[0]["TOTAL_AMOUNT"]) == dtOpening.Rows[0]["DEBIT_AMOUNT"].ToString())
                    {
                        txtOpeningDate.Text = Convert.ToDateTime(dtOpening.Rows[0]["DOCUMENT_DATE"]).ToString("dd-MMM-yyyy");

                        txtOpeningBalance.Text = string.Format("{0:0.00}", dtOpening.Rows[0]["TOTAL_AMOUNT"]);
                        txtOpeningBalanceRemarks.Text = dtOpening.Rows[0]["BUILTY_NO"].ToString();
                        rblOpening.SelectedValue = dtOpening.Rows[0]["TYPE_ID"].ToString();
                        masterId.Value = dtOpening.Rows[0]["PURCHASE_MASTER_ID"].ToString();
                        btnSaveOpeningBalance.Enabled = true;
                        btnSaveOpeningBalance.Text = "Update";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Delete Realization for Update.');", true);
                        btnSaveOpeningBalance.Enabled = false;
                    }
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Dayclose not found for selected location!')", true);
        }
    }

    protected void btnOpeningBalance_Click(object sender, EventArgs e)
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
            DataControl dc = new DataControl();
            LedgerController ledgerCtl = new LedgerController();
            DateTime dtOpening = Constants.DateNullValue;

            if (txtOpeningDate.Text.Length > 0)
            {
                dtOpening = Convert.ToDateTime(txtOpeningDate.Text);
            }
            DataTable dtConfig = GetCOAConfiguration();
            long MasterID = Constants.LongNullValue;
            if (btnSaveOpeningBalance.Text == "Save" && masterId.Value == "0")
            {

                if (ledgerCtl.InsertVendorOpening(Convert.ToInt32(drpDistributor.SelectedItem.Value), txtOpeningBalanceRemarks.Text, int.Parse(rblOpening.SelectedValue)
                    , Convert.ToDateTime(txtOpeningDate.Text), Convert.ToInt32(drpPrincipal.SelectedItem.Value), Convert.ToDecimal(dc.chkNull_0(txtOpeningBalance.Text))
                    , int.Parse(this.Session["UserId"].ToString()), ref MasterID, dtConfig))
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Opening Added Successfully.');", true);

                    btnSaveOpeningBalance.Text = "Update";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred.');", true);
                }
            }
            else
            {
                if (ledgerCtl.DeletePrincipalOpening(Convert.ToInt32(drpDistributor.SelectedItem.Value), Convert.ToInt32(drpPrincipal.SelectedItem.Value), Convert.ToInt64(masterId.Value)))
                {
                    if (ledgerCtl.InsertVendorOpening(Convert.ToInt32(drpDistributor.SelectedItem.Value), txtOpeningBalanceRemarks.Text, int.Parse(rblOpening.SelectedValue)
                        , Convert.ToDateTime(txtOpeningDate.Text), Convert.ToInt32(drpPrincipal.SelectedItem.Value), Convert.ToDecimal(dc.chkNull_0(txtOpeningBalance.Text))
                        , int.Parse(this.Session["UserId"].ToString()), ref MasterID, dtConfig))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Opening Updated Successfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred.');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Dayclose not found for selected location!')", true);
        }
    }

    #endregion
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadOpeningInformation();
    }
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadOpeningInformation();
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

    private DataTable GetCOAConfiguration()
    {
        try
        {
            COAMappingController _cController = new COAMappingController();
            DataTable dt = _cController.SelectCOAConfiguration(5, Constants.ShortNullValue, Constants.LongNullValue, "Level 4");
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtOpeningBalance.Text = "0";
        txtOpeningBalanceRemarks.Text = string.Empty;
        btnSaveOpeningBalance.Text = "Save";
    }
}