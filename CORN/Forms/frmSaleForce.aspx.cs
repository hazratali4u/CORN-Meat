using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;
using System.Linq;
using System.Web.Services;

/// <summary>
/// From to Add, Edit Employee
/// </summary>
public partial class Forms_frmSaleForce : System.Web.UI.Page
{

    readonly Distributor_UserController UController = new Distributor_UserController();
    readonly DistributorController mController = new DistributorController();

    readonly UserController _mUController = new UserController();
    readonly LoyaltyController lController = new LoyaltyController();
    readonly DataControl dc = new DataControl();

    static int UserId;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        try
        {
            if (!Page.IsPostBack)
            {
                LoadDistributor();
                LoadCardType();
                LoadApprovalBy();
                LoadShift();
                LoadDesignation();
                LoadDepartment();
                LoadGrid("");
                btnSave.Attributes.Add("onclick", "return ValidateForm()");
                LoadCardInfo();
                Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
                txtJoinDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
                txtJoinDate.Attributes.Add("readonly", "readonly");
            }
        }
        catch (Exception)
        {

            throw;
        }

        Response.Expires = 0;
        Response.Cache.SetNoStore();
    }
    
    #region Load

    private void LoadCardType()
    {
        try
        {
            if (ddDistributorId.Items.Count > 0)
            {
                DataTable dt = lController.SelectCardType(Convert.ToInt32(ddDistributorId.SelectedItem.Value));
                dt = dt.AsEnumerable()
                                  .Where(r => r.Field<int>("ID") == 3 || r.Field<int>("ID") == 0)
                                  .CopyToDataTable();

                clsWebFormUtil.FillDxComboBoxList(drpCardType, dt, 0, 1, true);

                drpCardType.Value = "0";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    
    private void LoadApprovalBy()
    {
        try
        {
            drpApprovalAuthority.Items.Clear();

            if (ddDistributorId.Items.Count > 0)
            {
                Distributor_UserController du = new Distributor_UserController();
                DataTable dt = du.SelectDistributorUser(5, int.Parse(ddDistributorId.SelectedItem.Value.ToString()), (hfUsedId.Value == "0") ? Constants.IntNullValue : Convert.ToInt32(hfUsedId.Value.ToString()));
                drpApprovalAuthority.Items.Add(new DevExpress.Web.ListEditItem("---Select---", "0"));

                clsWebFormUtil.FillDxComboBoxList(drpApprovalAuthority, dt, 0, 1);
                if (dt.Rows.Count > 0)
                {
                    drpApprovalAuthority.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
    private void LoadShift()
    {
        try
        {
            drpShift.Items.Clear();
            if (ddDistributorId.Items.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = UController.SelectShift(Convert.ToInt32(ddDistributorId.SelectedItem.Value.ToString()), 0);
                drpShift.Items.Add(new DevExpress.Web.ListEditItem("---Select---", "0"));

                clsWebFormUtil.FillDxComboBoxList(drpShift, dt, "SHIFT_ID", "Shift_desc");


                drpShift.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {
        }
    }

    private void LoadDistributor()
    {

        try
        {
            DataTable dt = mController.SelectDistributor(Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
           
            clsWebFormUtil.FillDxComboBoxList(ddDistributorId, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");
            if (dt.Rows.Count > 0)
            {
                ddDistributorId.SelectedIndex = 0;
            }
            LoadCardInfo();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
            throw;
        }
    }

    private void LoadDesignation()
    {
        SLASHCodesController mController = new SLASHCodesController();
        try
        {
            DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.SaleForce, null, Constants.IntNullValue, true);
           

            clsWebFormUtil.FillDxComboBoxList(ddDesignation, dt, "REF_ID", "SLASH_DESC");
            if (dt.Rows.Count > 0)
            {
                ddDesignation.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void LoadDepartment()
    {
        SLASHCodesController mController = new SLASHCodesController();
        try
        {
            DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.Employee_Depoartment_Id, null, Constants.IntNullValue, true);
           

            clsWebFormUtil.FillDxComboBoxList(drpDepartment, dt, "REF_ID", "SLASH_DESC");
            if (dt.Rows.Count > 0)
            {
                drpDepartment.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void LoadGrid(string pType)
    {
        if (ddDistributorId.Items.Count > 0)
        {
            DataTable dt = new DataTable();
            dt = UController.SelectDistributorUser(Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
            if (pType == "")
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
                {
                    dt.DefaultView.RowFilter = "USER_CODE LIKE '%" + txtSearch.Text + "%' OR USER_NAME LIKE '%" + txtSearch.Text + "%'  OR NIC_NO LIKE '%" + txtSearch.Text + "%'  OR PHONE LIKE '%" + txtSearch.Text + "%'  OR MOBILE LIKE '%" + txtSearch.Text + "%'  OR EMAIL LIKE '%" + txtSearch.Text + "%' OR ADDRESS1 LIKE '%" + txtSearch.Text + "%'  OR DEPARTMENT_NAME LIKE '%" + txtSearch.Text + "%'  OR SLASH_DESC LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '" + txtSearch.Text + "%'";
                }
                Grid_users.DataSource = dt;
                Grid_users.DataBind();
            }
            else
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
                {
                    dt.DefaultView.RowFilter = "USER_CODE LIKE '%" + txtSearch.Text + "%' OR USER_NAME LIKE '%" + txtSearch.Text + "%'  OR NIC_NO LIKE '%" + txtSearch.Text + "%'  OR PHONE LIKE '%" + txtSearch.Text + "%'  OR MOBILE LIKE '%" + txtSearch.Text + "%'  OR EMAIL LIKE '%" + txtSearch.Text + "%' OR ADDRESS1 LIKE '%" + txtSearch.Text + "%'  OR DEPARTMENT_NAME LIKE '%" + txtSearch.Text + "%'  OR SLASH_DESC LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '" + txtSearch.Text + "%'";
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
    public void LoadCardInfo()
    {
        try
        {
            txtAmountLimit.Text = "";
            if (ddDistributorId.Items.Count > 0 && drpCardType.Items.Count > 0)
            {
                DataTable dtCardThree = lController.SelectLoyaltyCard(Constants.IntNullValue, Convert.ToInt32(ddDistributorId.SelectedItem.Value)
                    , int.Parse(drpCardType.SelectedItem.Value.ToString()), 2);
                if (dtCardThree.Rows.Count > 0)
                {
                    txtAmountLimit.Text = dtCardThree.Rows[0][3].ToString().Replace("&nbsp;", "0");
                    hfLoyalCard.Value = dtCardThree.Rows[0][3].ToString().Replace("&nbsp;", "0");
                }
                else
                {
                    txtAmountLimit.Text = "0";
                    hfLoyalCard.Value = "";
                }
                ShowHideCardInfo();
            }
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }

    #endregion

    private void ShowHideCardInfo()
    {
        if (drpCardType.SelectedItem.Value.ToString() == "0")
        {
            txtCardNo.Visible = false;
            ltrlCardNo.Visible = false;
            ltrlAmountLimit.Visible = false;
            txtAmountLimit.Visible = false;
        }
        else if (drpCardType.SelectedItem.Value.ToString() == "3")
        {
            txtCardNo.Visible = true;
            ltrlCardNo.Visible = true;
            ltrlAmountLimit.Visible = true;
            txtAmountLimit.Visible = true;

        }

    }
    private bool ValidateCard(string btn, int recordId)
    {
        try
        {
            if (drpCardType.SelectedItem.Value.ToString() != "0")
            {
                if (txtCardNo.Text != "")
                {
                    if (btn == "Save")
                    {
                        if (_mUController.IsExist(txtCardNo.Text, Convert.ToInt32(ddDistributorId.SelectedItem.Value), int.Parse(Session["UserId"].ToString()), 1))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (_mUController.IsExist(txtCardNo.Text, Convert.ToInt32(ddDistributorId.SelectedItem.Value), recordId, 2))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
            throw;
        }
    }

    #region Index Change
    protected void ddDistributorId_SelectedIndexChanged(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        LoadShift();
        LoadCardType();
        LoadCardInfo();
        LoadApprovalBy();
        //LoadGrid();
    }
    protected void drpCardType_SelectedIndexChanged(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        LoadCardInfo();
    }
    #endregion

    #region Click 

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (drpCardType.SelectedItem.Value.ToString() == "3")
                {
                    if (txtCardNo.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Card No is required.');", true);
                        mPopUpLocation.Show();
                        return;
                    }
                    if (txtAmountLimit.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Amount limit is required.');", true);
                        mPopUpLocation.Show();
                        return;
                    }
                }
                bool flag = true;
                if (btnSave.Text == "Save")
                {
                    if (ValidateCard("Save", 0))
                    {
                        SETTINGS_TABLE_Controller mAutoCode = new SETTINGS_TABLE_Controller();

                        string strcode = "";
                        UserId = int.Parse(mAutoCode.GetAutoCustomerCode("EM", 0, Constants.LongNullValue).ToString());

                        if (UserId.ToString().Length == 1)
                        {
                            strcode = "EM" + "000" + UserId.ToString();
                        }
                        else if (UserId.ToString().Length == 2)
                        {
                            strcode = "EM" + "00" + UserId.ToString();
                        }
                        else if (UserId.ToString().Length == 3)
                        {
                            strcode = "EM" + "0" + UserId.ToString();
                        }
                        else
                        {
                            strcode = "EM" + UserId.ToString();
                        }


                        string SaleForceId = UController.InsertDistributor_User(int.Parse(Session["CompanyId"].ToString()), txtNICNo.Text, true, Convert.ToDateTime(txtJoinDate.Text), System.DateTime.Now, int.Parse(ddDesignation.SelectedItem.Value.ToString()),
                        int.Parse(ddDistributorId.SelectedItem.Value.ToString()), Constants.IntNullValue, txtEmail.Text, txtAddress1.Text, txtAddress2.Text, txtLoginId.Text, txtpassword.Text
                        , txtMobileNo.Text, strcode, txtUserName.Text, txtPhoneNo.Text, int.Parse(drpDepartment.SelectedItem.Value.ToString()), int.Parse(drpShift.SelectedItem.Value.ToString())
                        , Convert.ToDecimal(dc.chkNull_0(txtAmountLimit.Text)), Convert.ToDecimal(dc.chkNull_0(txtEMCDiscount.Text)), Convert.ToInt32(drpApprovalAuthority.SelectedItem.Value.ToString())
                        , txtCardNo.Text, int.Parse(drpCardType.SelectedItem.Value.ToString()));

                        mAutoCode.GetAutoCustomerCode("EM", 1, long.Parse(UserId.ToString()));

                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');", true);
                        flag = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Card No already exist.');", true);
                        flag = false;
                    }
                    mPopUpLocation.Show();
                }
                else if (btnSave.Text == "Update")
                {
                    if (ValidateCard("Update", UserId))
                    {
                        bool status = true;
                        if (hfStatus.Value != "Active")
                        {
                            status = false;
                        }
                        UController.UpdateDistributor_User(UserId, int.Parse(Session["CompanyId"].ToString()), txtNICNo.Text, status, Convert.ToDateTime(txtJoinDate.Text), System.DateTime.Now, int.Parse(ddDesignation.SelectedItem.Value.ToString()),
                             int.Parse(ddDistributorId.SelectedItem.Value.ToString()), Constants.IntNullValue, txtEmail.Text, txtAddress1.Text, txtAddress2.Text, txtLoginId.Text, txtpassword.Text
                             , txtMobileNo.Text, null, txtUserName.Text, txtPhoneNo.Text, int.Parse(drpDepartment.SelectedItem.Value.ToString()), int.Parse(drpShift.SelectedItem.Value.ToString()), Convert.ToDecimal(dc.chkNull_0(txtAmountLimit.Text))
                             , Convert.ToDecimal(dc.chkNull_0(txtEMCDiscount.Text)), Convert.ToInt32(drpApprovalAuthority.SelectedItem.Value), txtCardNo.Text, int.Parse(drpCardType.SelectedItem.Value.ToString()));

                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully.');", true);
                        flag = true;
                        mPopUpLocation.Hide();
                    }
                    else {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Card No already exist.');", true);
                        flag = false;
                        mPopUpLocation.Show();

                    }

                }
                if (flag)
                {
                    LoadGrid("");
                    ClearControls();
                    btnSave.Text = "Save";
                }

            }
            else
            {
                mPopUpLocation.Show();
                LoadApprovalBy();
            }
        }

        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
            mPopUpLocation.Show();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        btnSave.Text = "Save";
        ddDistributorId.SelectedIndex = 0;
        ddDesignation.SelectedIndex = 0;
        drpDepartment.SelectedIndex = 0;
        LoadShift();
        LoadCardInfo();
        ClearControls();
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.LoadGrid("filter");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        ClearControls();
        LoadApprovalBy();
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
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('No Record Selected.');", true);
                return;
            }

            UserController UserCntrl = new UserController();

            bool flag = false;
            foreach (GridViewRow dr in Grid_users.Rows)
            {
                var chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");

                if (chRelized.Checked)
                {
                    if (Convert.ToString(dr.Cells[17].Text) == "Active")
                    {
                        UserCntrl.ActiveInactive(false, Constants.IntNullValue, Convert.ToInt32(dr.Cells[1].Text), 3);


                        flag = true;
                    }
                    else
                    {
                        UserCntrl.ActiveInactive(true, Constants.IntNullValue, Convert.ToInt32(dr.Cells[1].Text), 3);


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

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearControls();
        btnSave.Text = "Save";
        mPopUpLocation.Hide();

    }

    #endregion 

    protected void ClearControls()
    {
        try
        {
            txtUserName.Text = null;
            txtPhoneNo.Text = null;
            txtMobileNo.Text = null;
            txtAddress1.Text = null;
            txtAddress2.Text = null;
            txtEmail.Text = null;
            txtLoginId.Text = null;
            txtpassword.Text = null;
            txtNICNo.Text = null;
            lblErrorMsg.Visible = false;
            lblErrorMsg.Text = null;
            txtEMCDiscount.Text = "";
            
            hfUsedId.Value = "0";
            txtCardNo.Text = "";
            txtJoinDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtCardNo.Enabled = true;
            txtAmountLimit.Enabled = true;
            drpCardType.Enabled = true;

            LoadCardInfo();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    #region Grid Operations

    protected void Grid_users_RowEditing(object sender, GridViewEditEventArgs e)
    {

        mPopUpLocation.Show();
        try
        {
            GridViewRow gvr = Grid_users.Rows[e.NewEditIndex];
            UserId = int.Parse(gvr.Cells[1].Text);
            hfUsedId.Value = UserId.ToString();
            txtUserName.Text = gvr.Cells[4].Text.Replace("&nbsp;", "");
            txtNICNo.Text = gvr.Cells[5].Text.Replace("&nbsp;", "");
            txtPhoneNo.Text = gvr.Cells[6].Text.Replace("&nbsp;", "");
            txtMobileNo.Text = gvr.Cells[7].Text.Replace("&nbsp;", "");
            txtEmail.Text = gvr.Cells[8].Text.Replace("&nbsp;", "");
            txtAddress1.Text = gvr.Cells[9].Text.Replace("&nbsp;", "");
            txtAddress2.Text = gvr.Cells[10].Text.Replace("&nbsp;", "");
            hfStatus.Value = gvr.Cells[18].Text;
            ddDesignation.Value = gvr.Cells[12].Text;
            txtJoinDate.Text = gvr.Cells[14].Text.Replace("&nbsp;", "");
            if (gvr.Cells[16].Text != 0.ToString())
            {
                drpDepartment.Value = gvr.Cells[16].Text;
            }
            try
            {
                ddDistributorId.Value = gvr.Cells[20].Text;
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Base location is inactive');", true);
            }
            txtEMCDiscount.Text = gvr.Cells[17].Text.Replace("&nbsp;", "");
            ddDistributorId_SelectedIndexChanged(null, null);
            try
            {
                drpShift.Value = gvr.Cells[19].Text;
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Assigned shift is inactive');", true);
            }
            try
            {
                if (gvr.Cells[21].Text == null || gvr.Cells[21].Text == "" || gvr.Cells[21].Text == "&nbsp;")
                {
                    drpApprovalAuthority.SelectedIndex = 0;
                }
                else
                {
                    drpApprovalAuthority.Value = gvr.Cells[22].Text;
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Assigned manager is inactive');", true);
            }

            txtAmountLimit.Text = gvr.Cells[21].Text.Replace("&nbsp;", ""); 
            drpCardType.Value = gvr.Cells[23].Text;
            ShowHideCardInfo();
            txtCardNo.Text = gvr.Cells[24].Text.Replace("&nbsp;", ""); 

            if (_mUController.IsExist(txtCardNo.Text, Convert.ToInt32(ddDistributorId.Value), Constants.IntNullValue, 4))
            {
                txtCardNo.Enabled = false;
                txtAmountLimit.Enabled = false;
                drpCardType.Enabled = false;
            }
            btnSave.Text = "Update";
        }
        catch (Exception ex)
        {
            btnSave.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred');", true);
            ex.Message.ToString();
        }

    }
    protected void Grid_users_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_users.PageIndex = e.NewPageIndex;
        LoadGrid("");
    }

    #endregion
}