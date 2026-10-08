using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;
using System.Web;

public partial class Forms_frmInvoiceBooking : System.Web.UI.Page
{
    readonly RoleManagementController mController = new RoleManagementController();
    readonly UserController UController = new UserController();
    readonly PurchaseController _mPurchaseCtrl = new PurchaseController();
   
    readonly LedgerController CDC = new LedgerController();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            LoadDISTRIBUTOR();
            LoadSupplier();
            LoadGrid("");
            btnSave.Attributes.Add("onclick", "return ValidateForm()");

            DoEmptyTextBox();
            txtDiscount.Attributes.Add("autocomplete", "off");
            txtGross.Attributes.Add("autocomplete", "off");
            txtGst.Attributes.Add("autocomplete", "off");
            txtInvDate.Attributes.Add("autocomplete", "off");
            txtNetAmount.Attributes.Add("autocomplete", "off");
            txtNetAmount.Attributes.Add("readonly", "readonly");
            txtInvDate.Attributes.Add("readonly", "readonly");
            txtInvDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    public void DoEmptyTextBox()
    {
        txtDiscount.Attributes.Add("value", "");
        txtGross.Attributes.Add("value", "");
        txtGst.Attributes.Add("value", "");
        txtInvDate.Attributes.Add("value", "");
        txtNetAmount.Attributes.Add("value", "");
    }

    private void LoadDISTRIBUTOR()
    {
        DistributorController mController = new DistributorController();
        DataTable dtDistributor = mController.SelectDistributor(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);

        clsWebFormUtil.FillDxComboBoxList(ddDistributorId, dtDistributor, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");

        if (dtDistributor.Rows.Count > 0)
        {
            ddDistributorId.SelectedIndex = 0;
        }

    }


    private void LoadSupplier()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        

        if (ddDistributorId.Items.Count > 0)
        {
            DataTable dtVendor = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));

            clsWebFormUtil.FillDxComboBoxList(DrpSupplier, dtVendor, 0, 1);

            if (dtVendor.Rows.Count > 0)
            {
                DrpSupplier.SelectedIndex = 0;
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

            dt = _mPurchaseCtrl.SelectPurchaseDocumentNo(2, Constants.IntNullValue, Constants.LongNullValue, int.Parse(Session["UserId"].ToString()), 0);

            if (pType == "")
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
                {
                    dt.DefaultView.RowFilter = "DISTRIBUTOR_NAME LIKE '%" + txtSearch.Text + "%' OR SUPPLIER LIKE '%" + txtSearch.Text + "%'  OR ORDER_NUMBER LIKE '%" + txtSearch.Text + "%'";
                }
                Grid_users.DataSource = dt;
                Grid_users.DataBind();
            }
            else
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
                {
                    dt.DefaultView.RowFilter = "DISTRIBUTOR_NAME LIKE '%" + txtSearch.Text + "%' OR SUPPLIER LIKE '%" + txtSearch.Text + "%'  OR ORDER_NUMBER LIKE '%" + txtSearch.Text + "%'";
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
        LoadSupplier();
    }

    protected void Grid_users_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridViewRow gvr = Grid_users.Rows[e.NewEditIndex];
            hfDocumentID.Value = gvr.Cells[1].Text;
            try
            {
                ddDistributorId.Value = gvr.Cells[2].Text;
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Relavent location is inactive');", true);
                return;
            }
            LoadSupplier();
            try
            {
                DrpSupplier.Value = gvr.Cells[8].Text;
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Relevant supplier is inactive');", true);
                return;
            }
            txtInvNo.Text = gvr.Cells[5].Text;
            txtInvDate.Text = gvr.Cells[6].Text;
            txtGross.Text = gvr.Cells[9].Text;
            txtDiscount.Text = gvr.Cells[12].Text;
            txtGst.Text = gvr.Cells[11].Text;
            txtNetAmount.Text = gvr.Cells[13].Text;
            txtRemarks.Text = gvr.Cells[14].Text.Replace("&nbsp;", "");


            DrpSupplier.Enabled = false;
            ddDistributorId.Enabled = false;

            btnSave.Text = "Update";
            mPopUpLocation.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
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
    private bool GetFinanceConfig()
    {
        try
        {
            ConfigurationController _cController = new ConfigurationController();
            DataTable dt = _cController.SelectAppConfiguration(1, (int)Enums.AppSettingMaster.FinanceSetting, null, Constants.IntNullValue);

            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("CODE = '" + (int)Enums.AppSetting.IsFinanceIntegrate + "'");
                if (dr.Length > 0)
                {
                    return Convert.ToInt32(dr[0][2]) == 1 ? true : false;
                }
            }
            return false;
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Error in Financial Setting!');", true);
            throw;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        if (Page.IsValid)
        {
            try
            {
                DataControl _dc = new DataControl();

                lblErrorMsg.Text = "";
                lblErrorMsg.Visible = false;

                DataTable dtConfig = GetCOAConfiguration();
                bool IsFinanceSetting = GetFinanceConfig();

                if (btnSave.Text == "Save")
                {
                    bool PurchaseID = _mPurchaseCtrl.InsertInvoiceBooking(Convert.ToInt32(ddDistributorId.SelectedItem.Value), txtInvNo.Text, 2
                , Convert.ToDateTime(txtInvDate.Text), Convert.ToInt32(ddDistributorId.SelectedItem.Value), Convert.ToInt32(DrpSupplier.SelectedItem.Value), Convert.ToDecimal(txtGross.Text), false, 0, "", Convert.ToInt32(Session["UserId"].ToString()), Convert.ToInt32(DrpSupplier.SelectedItem.Value)
                , Convert.ToDecimal(_dc.chkNull_0(txtGst.Text)), Convert.ToDecimal(_dc.chkNull_0(txtDiscount.Text)), Convert.ToDecimal(txtNetAmount.Text)
                , txtRemarks.Text, dtConfig, IsFinanceSetting);

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');", true);

                    mPopUpLocation.Show();
                }
                else if (btnSave.Text == "Update")
                {
                    _mPurchaseCtrl.UpdateInvoiceBooking(long.Parse(hfDocumentID.Value), int.Parse(ddDistributorId.SelectedItem.Value.ToString()), txtInvNo.Text, 2
                 , Convert.ToDateTime(txtInvDate.Text), int.Parse(ddDistributorId.SelectedItem.Value.ToString()), int.Parse(DrpSupplier.SelectedItem.Value.ToString()), Convert.ToDecimal(txtGross.Text), false, 0, "", int.Parse(Session["UserId"].ToString()), Convert.ToInt32(DrpSupplier.SelectedItem.Value)
                 , Convert.ToDecimal(_dc.chkNull_0(txtGst.Text)), decimal.Parse(_dc.chkNull_0(txtDiscount.Text)), Convert.ToDecimal(txtNetAmount.Text)
                 , txtRemarks.Text, dtConfig, IsFinanceSetting);
                    mPopUpLocation.Hide();
                }
                LoadGrid("");
                ClearControls();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
                mPopUpLocation.Show();
            }
        }
    }
    
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.LoadGrid("filter");
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        //DoEmptyTextBox();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearControls();
        mPopUpLocation.Hide();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        ddDistributorId.SelectedIndex = 0;
        LoadSupplier();
        ClearControls();
    }

    protected void ClearControls()
    {
        try
        {
            txtInvNo.Text = "";
            txtDiscount.Text = "";
            txtGross.Text = "";
            txtGst.Text = "";
            txtNetAmount.Text = "";
            txtRemarks.Text = "";
            lblErrorMsg.Visible = false;
            lblErrorMsg.Text = "";

            DrpSupplier.Enabled = true;
            ddDistributorId.Enabled = true;
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}
