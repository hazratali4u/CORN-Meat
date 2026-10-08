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
public partial class Forms_frmCreateUser : System.Web.UI.Page
{
    readonly RoleManagementController mController = new RoleManagementController();
    readonly UserController UController = new UserController();

    static DataTable dtConfig;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            LoadDISTRIBUTOR();
            LoadUser();
            LoadRole();
            LoadGrid("");
            DoEmptyTextBox();
            txtLogId.Attributes.Add("autocomplete", "off");
            txtPswd.Attributes.Add("autocomplete", "off");
            dtConfig = GetAppConfiguration();
        }
    }
    public DataTable GetAppConfiguration()
    {
        try
        {
            ConfigurationController _cController = new ConfigurationController();
            DataTable dt = _cController.SelectAppConfiguration(1, (int)Enums.AppSettingMaster.WebSettings, null, Constants.IntNullValue);
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
    public void DoEmptyTextBox()
    {
        txtLogId.Attributes.Add("value", "");
        txtPswd.Attributes.Add("value", "");
        txtLogId.Text = "";
        txtPswd.Text = "";
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
    
    private void LoadUser()
    {
        DrpUser.Items.Clear();

        if (ddDistributorId.Items.Count > 0)
        {
            Distributor_UserController du = new Distributor_UserController();
            DataTable dt = du.SelectDistributorUser(4, int.Parse(ddDistributorId.SelectedItem.Value.ToString()), int.Parse(Session["CompanyId"].ToString()));

            clsWebFormUtil.FillDxComboBoxList(DrpUser, dt, 0, 6);
            if (dt.Rows.Count > 0)
            {
                DrpUser.SelectedIndex = 0;
            }
        }
    }
    
    protected void LoadRole()
    {
        DataTable dt = mController.SelectRoleMaster(Constants.IntNullValue, null, Constants.DateNullValue, Constants.DateNullValue, true);
      
        clsWebFormUtil.FillDxComboBoxList(ddRole, dt, "ROLE_ID", "ROLE_NAME");

        if (dt.Rows.Count > 0)
        {
            ddRole.SelectedIndex = 0;
        }
    }
    
    protected void LoadGrid(string pType)
    {
        Grid_users.DataSource = null;
        Grid_users.DataBind();

        if (ddDistributorId.Items.Count > 0)
        {
            DataTable dt = new DataTable();

            dt = UController.SelectSlashUser(null, null);
            DataRow[] dr = dt.Select("DISTRIBUTOR_ID = 1234");
            dt.Rows.Remove(dr[0]);
            dt.AcceptChanges();
            if (pType == "")
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
                {
                    dt.DefaultView.RowFilter = "USER_CODE LIKE '%" + txtSearch.Text + "%' OR USER_NAME LIKE '%" + txtSearch.Text + "%'  OR LOGIN_ID LIKE '%" + txtSearch.Text + "%'  OR PASSWORD LIKE '%" + txtSearch.Text + "%'  OR role_name LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '" + txtSearch.Text + "%'";
                }
                Grid_users.DataSource = dt;
                Grid_users.DataBind();
            }
            else
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
                {
                    dt.DefaultView.RowFilter = "USER_CODE LIKE '%" + txtSearch.Text + "%' OR USER_NAME LIKE '%" + txtSearch.Text + "%'  OR LOGIN_ID LIKE '%" + txtSearch.Text + "%'  OR PASSWORD LIKE '%" + txtSearch.Text + "%'  OR role_name LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '" + txtSearch.Text + "%'";
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

    /// <summary>
    /// Sets User Data For Edit. This Function Runs When An Existing User Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void Grid_users_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridViewRow gvr = Grid_users.Rows[e.NewEditIndex];
            try
            {
                ddDistributorId.Value = gvr.Cells[2].Text;
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Relavent location is inactive');", true);
                return;
            }
            LoadUser();
            try
            {
                DrpUser.Value = gvr.Cells[1].Text;
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Relavent employee is inactive');", true);
                return;
            }
            txtLogId.Text = gvr.Cells[5].Text;

            DataRow[] dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.IsEncreptedCredentials + "'");
            bool IsEncrypted = Convert.ToBoolean(Convert.ToInt32(dr[0]["VALUE"]));

            if (IsEncrypted)
            {
                dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.Deployed + "'");
                if (dr[0]["VALUE"].ToString() == Cryptography.Encrypt("Deployed", "b0tin@74"))
                {
                    txtPswd.Attributes.Add("value", Cryptography.Decrypt(gvr.Cells[6].Text, "b0tin@74"));
                }
            }
            else
            {
                txtPswd.Attributes.Add("value", gvr.Cells[6].Text);
            }
            ddRole.Value = gvr.Cells[8].Text;

            chkright.Checked = bool.Parse(gvr.Cells[9].Text);
            chkright2.Checked = bool.Parse(gvr.Cells[10].Text);
            chkCashier.Checked = bool.Parse(gvr.Cells[12].Text);
            chkIS_CanGiveDiscount.Checked = bool.Parse(gvr.Cells[13].Text);
            chkIS_CanReverseDayClose.Checked = bool.Parse(gvr.Cells[14].Text);
            chkCan_DineIn.Checked = bool.Parse(gvr.Cells[15].Text);
            chkCan_Delivery.Checked = bool.Parse(gvr.Cells[16].Text);
            chkCan_TakeAway.Checked = bool.Parse(gvr.Cells[17].Text);
            DrpUser.Enabled = false;
            ddDistributorId.Enabled = false;

            btnSave.Text = "Update";
            mPopUpLocation.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }


    private string CheckDeplyed()
    {
        DataRow[] dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.IsEncreptedCredentials + "'");
        bool IsEncrypted = Convert.ToBoolean(Convert.ToInt32(dr[0]["VALUE"]));

        if (IsEncrypted)
        {
            dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.Deployed + "'");
            if (dr[0]["VALUE"].ToString() == Cryptography.Encrypt("Deployed", "b0tin@74"))
            {
                txtPswd.Attributes["type"] = "text";
            }
            return Cryptography.Encrypt(txtPswd.Text, "b0tin@74");
        }
        else
        {
            return txtPswd.Text;
        }
    }

    /// <summary>
    /// Saves Or Updates A User.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        DataTable dtUsers = UController.SelectSlashUser(txtLogId.Text, txtPswd.Text);
        if (Page.IsValid)
        {
            txtPswd.Text = CheckDeplyed();
            try
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Visible = false;
                if (btnSave.Text == "Save")
                {
                    if (dtUsers.Rows.Count > 0)
                    {
                        lblErrorMsg.Visible = true;
                        lblErrorMsg.Text = Utility.ShowAlert(false, "Login ID already exist");
                        return;
                    }

                    if (UController.InsertSlashUser(int.Parse(DrpUser.SelectedItem.Value.ToString()), int.Parse(Session["CompanyId"].ToString()),
                       int.Parse(ddDistributorId.SelectedItem.Value.ToString()), txtLogId.Text, txtPswd.Text,
                       int.Parse(ddRole.SelectedItem.Value.ToString()), chkright.Checked, chkright2.Checked, chkCashier.Checked, chkIS_CanGiveDiscount.Checked, chkIS_CanReverseDayClose.Checked
                       , chkCan_DineIn.Checked, chkCan_Delivery.Checked, chkCan_TakeAway.Checked) == null)
                    {

                        lblErrorMsg.Visible = true;
                        lblErrorMsg.Text = Utility.ShowAlert(false, "Login ID already exist for this user");
                        return;
                    }

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');", true);

                    mPopUpLocation.Show();

                }
                else if (btnSave.Text == "Update")
                {
                    DataRow[] dr = dtUsers.Select("USER_ID <> '" + int.Parse(DrpUser.SelectedItem.Value.ToString()) + "' ");
                    if (dr.Length == 0)
                    {
                        string result = UController.UpdateUser(int.Parse(DrpUser.SelectedItem.Value.ToString()), int.Parse(Session["CompanyId"].ToString()),
                                        txtLogId.Text, txtPswd.Text, int.Parse(ddRole.SelectedItem.Value.ToString()), true,
                                        int.Parse(ddDistributorId.SelectedItem.Value.ToString()), chkright.Checked, chkright2.Checked, chkCashier.Checked, chkIS_CanGiveDiscount.Checked
                                        , chkIS_CanReverseDayClose.Checked, chkCan_DineIn.Checked, chkCan_Delivery.Checked, chkCan_TakeAway.Checked);

                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully.');", true);
                    }
                    else
                    {
                        lblErrorMsg.Visible = true;
                        lblErrorMsg.Text = Utility.ShowAlert(false, "Login ID already exist for this user");
                        return;
                    }
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
                        UController.ActiveInactive(false,Constants.IntNullValue, Convert.ToInt32(dr.Cells[1].Text),2);

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
        ddRole.SelectedIndex = 0;
        ddDistributorId.SelectedIndex = 0;
        LoadUser();
        ClearControls();
    }

    protected void ClearControls()
    {
        try
        {
            txtLogId.Attributes.Add("value", "");
            txtPswd.Attributes.Add("value", "");
            txtPswd.Attributes["type"] = "password";
            lblErrorMsg.Visible = false;
            lblErrorMsg.Text = "";
            txtPswd.Text = "";
            txtLogId.Text = "";
            btnSave.Text = "Save";

            chkright.Checked = false;
            chkright2.Checked = false;
            chkCashier.Checked = false;
            chkIS_CanGiveDiscount.Checked = false;
            chkIS_CanReverseDayClose.Checked = false;

            DrpUser.Enabled = true;
            ddDistributorId.Enabled = true;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}
