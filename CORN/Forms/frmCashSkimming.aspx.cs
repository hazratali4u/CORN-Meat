using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;
using System.Web;

/// <summary>
/// Form to Add, Edit Users
/// </summary>
public partial class Forms_frmCashSkimming : System.Web.UI.Page
{
    readonly RoleManagementController mController = new RoleManagementController();
    readonly DocumentPrintController _mDocumentPrntControl = new DocumentPrintController();
    readonly UserController UController = new UserController();
    readonly SkimmingController sController = new SkimmingController();
    readonly DataControl dc = new DataControl();
    readonly ShiftController _SController = new ShiftController();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadUser();
            LoadGrid("");
            txtAmount.Attributes.Add("autocomplete", "off");
            txtRemarks.Attributes.Add("autocomplete", "off");
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.GetDistributorWithMaxDayClose(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()), 1);
        clsWebFormUtil.FillDxComboBoxList(ddDistributorId, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");

        if (dt.Rows.Count > 0)
        {
            ddDistributorId.SelectedIndex = 0;
        }
        Session.Add("dtLocationInfo", dt);
    }

    private void LoadUser()
    {
        DrpUser.Items.Clear();

        if (ddDistributorId.Items.Count > 0)
        {
            Distributor_UserController du = new Distributor_UserController();
            DataTable dt = du.SelectDistributorUser(3, int.Parse(ddDistributorId.Value.ToString()), int.Parse(Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDxComboBoxList(DrpUser, dt, 0, 1, true);

            if (dt.Rows.Count > 0)
            {
                DrpUser.SelectedIndex = 0;
            }
        }
        LoadCashAmount();
    }

    public void LoadCashAmount()
    {
        lblCashInHand.Text = "0.00";
        if (ddDistributorId.Items.Count > 0 && DrpUser.Items.Count > 0)
        {
            DataTable dt = _SController.SelectSales(Convert.ToInt32(ddDistributorId.SelectedItem.Value), Convert.ToInt32(DrpUser.SelectedItem.Value), Constants.DateNullValue, Constants.DateNullValue, 8, Constants.IntNullValue);
            DataTable dtsKIM = _SController.SelectSales(Convert.ToInt32(ddDistributorId.SelectedItem.Value), Convert.ToInt32(DrpUser.SelectedItem.Value), Constants.DateNullValue, Constants.DateNullValue, 9, Constants.IntNullValue);
            try
            {
                if (dt.Rows[0].ItemArray[0] != DBNull.Value)
                {
                    lblCashInHand.Text = (Math.Round(Convert.ToDecimal(dt.Rows[0].ItemArray[0]), 2) + DataControl.chkDecimalZero(hfOpeningRecieved.Value) + DataControl.chkDecimalZero(hfOpeningCash.Value) - Math.Round(Convert.ToDecimal(dtsKIM.Rows[0].ItemArray[0]), 2)).ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
    protected void LoadGrid(string pType)
    {
        Grid_users.DataSource = null;
        Grid_users.DataBind();

        if (ddDistributorId.Items.Count > 0)
        {
            DataTable dt = new DataTable();
            dt = sController.SelectCashSkimming(Constants.IntNullValue, Constants.IntNullValue, 1);
            if (pType == "")
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
                {
                    dt.DefaultView.RowFilter = "DISTRIBUTOR_NAME LIKE '%" + txtSearch.Text + "%' OR USER_NAME LIKE '%" + txtSearch.Text + "%' ";
                }
                Grid_users.DataSource = dt;
                Grid_users.DataBind();
            }
            else
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
                {
                    dt.DefaultView.RowFilter = "DISTRIBUTOR_NAME LIKE '%" + txtSearch.Text + "%' OR USER_NAME LIKE '%" + txtSearch.Text + "%'  ";
                }
                if (dt.Rows.Count > 0)
                {
                    Grid_users.PageIndex = 0;
                }
                Grid_users.DataSource = dt;
                Grid_users.DataBind();
            }
        }
    }

    protected void ddDistributorId_SelectedIndexChanged(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        LoadUser();
    }

    protected void DrpUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        LoadCashAmount();
    }

    protected void Grid_users_RowEditing(object sender, GridViewEditEventArgs e)
    {
        mPopUpLocation.Show();
        try
        {
            GridViewRow gvr = Grid_users.Rows[e.NewEditIndex];

            txtRemarks.Text = gvr.Cells[6].Text;
            txtAmount.Text = gvr.Cells[5].Text;
            LoadDistributor();
            try
            {
                ddDistributorId.Value = gvr.Cells[1].Text;
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Relevent location not found')", true);
            }
            try
            {
                LoadUser();
                DrpUser.Value = gvr.Cells[2].Text;
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Relevent cashier not found')", true);
            }
            btnSave.Text = "Update";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        if (Page.IsValid)
        {
            DateTime CurrentWorkDate = Constants.DateNullValue;
            DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
            foreach (DataRow dr in dtLocationInfo.Rows)
            {
                if (dr["DISTRIBUTOR_ID"].ToString() == ddDistributorId.Value.ToString())
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
                try
                {
                    if (Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)) > Convert.ToDecimal(dc.chkNull_0(lblCashInHand.Text)))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Cash in hand is less than skimming amount.');", true);
                        mPopUpLocation.Show();
                        return;
                    }
                    lblErrorMsg.Text = "";
                    lblErrorMsg.Visible = false;
                    if (btnSave.Text == "Save")
                    {
                        sController.InsertSkimming(Convert.ToInt32(ddDistributorId.SelectedItem.Value), Convert.ToInt32(DrpUser.SelectedItem.Value), Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)), txtRemarks.Text, CurrentWorkDate);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');", true);
                        ShowReport(CurrentWorkDate);
                    }
                    else if (btnSave.Text == "Update")
                    {
                        sController.UpdateSkimming(1, Convert.ToInt32(ddDistributorId.SelectedItem.Value), Convert.ToInt32(DrpUser.SelectedItem.Value), Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)), txtRemarks.Text);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully.');", true);
                        mPopUpLocation.Hide();
                    }
                    LoadCashAmount();
                    LoadGrid("");
                    ClearControls();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
                    mPopUpLocation.Show();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Dayclose not found for selected location!')", true);
            }
        }
    }
    private void ShowReport(DateTime CurrentWorkDate)
    {
        DataTable dt = _mDocumentPrntControl.SelectReportTitle(Convert.ToInt32(ddDistributorId.SelectedItem.Value));
        CORNBusinessLayer.Reports.crpCashSkimmingInvoice crpReport = new CORNBusinessLayer.Reports.crpCashSkimmingInvoice();
        crpReport.Refresh();
        string date = CurrentWorkDate.ToString("dd-MMM-yyyy");
        crpReport.SetParameterValue("Date", date);
        crpReport.SetParameterValue("USER_NAME", DrpUser.SelectedItem.Text);
        crpReport.SetParameterValue("LOCATION", ddDistributorId.SelectedItem.Text);
        crpReport.SetParameterValue("PHONE_NUMBER", dt.Rows[0]["CONTACT_NUMBER"].ToString());
        crpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());

        crpReport.SetParameterValue("AMOUNT", Convert.ToDecimal(txtAmount.Text));
        crpReport.SetParameterValue("REMARKS", txtRemarks.Text);

        Session.Add("CrpReport", crpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        ddDistributorId.SelectedIndex = 0;
        LoadUser();
        ClearControls();
    }

    protected void ClearControls()
    {
        try
        {
            lblErrorMsg.Visible = false;
            lblErrorMsg.Text = "";
            txtAmount.Text = "";
            txtRemarks.Text = "";
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {

        this.LoadGrid("filter");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        //DoEmptyTextBox();
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        bool check = false;
        try
        {
            foreach (GridViewRow dr2 in Grid_users.Rows)
            {
                var chRelized2 = (CheckBox)dr2.Cells[0].FindControl("ChbIsAssigned");

                if (chRelized2.Checked)
                {
                    check = true;
                    break;
                }
            }
            if (!check)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please select record first');", true);
                return;
            }
            bool flag = false;
            foreach (GridViewRow dr in Grid_users.Rows)
            {
                var chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                if (chRelized.Checked)
                {
                    if (Convert.ToString(dr.Cells[11].Text) == "Active")
                    {
                        UController.ActiveInactive(false, Constants.IntNullValue, Convert.ToInt32(dr.Cells[1].Text), 2);
                        

                        flag = true;
                    }
                    else
                    {
                        UController.ActiveInactive(true, Constants.IntNullValue, Convert.ToInt32(dr.Cells[1].Text), 2);
                        
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record updated successfully');", true);
            }
            this.LoadGrid("");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
    protected void Grid_users_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_users.PageIndex = e.NewPageIndex;
        LoadGrid("");
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearControls();
        mPopUpLocation.Hide();
    }
}
