using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;

public partial class Forms_frmChequeBook : System.Web.UI.Page
{
    public static int _CountryID = 0;
    public static int _CityID = 0;
    readonly DistributorController _dController = new DistributorController();
    readonly BankController _bnkController = new BankController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadAccounts();
            LoadGridCountry();
            LoadAccountHead();
            pnlMainDivision.Visible = true;
            btnSaveCountry.Attributes.Add("onclick", "return ValidateForm()");
            RemoveTextboxSuggessions();
            txtIssueDate.Attributes.Add("readonly", "readonly");
            txtIssueDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
        }
    }
    private void LoadAccountHead()
    {
        try
        {
            Configuration.GetAccountHead();
            AccountHeadController mAccountController = new AccountHeadController();

            DataTable dt = mAccountController.SelectAccountHeadByMapping(Constants.AC_AccountHeadId, Constants.LongNullValue, 2, Constants.AC_BankAccountHead);
            clsWebFormUtil.FillDxComboBoxList(drpAccountno, dt, 0, 4, true);

            if (dt.Rows.Count > 0)
            {
                drpAccountno.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    public void RemoveTextboxSuggessions()
    {
        try
        {
            txtCheckfrom.Attributes.Add("autocomplete", "off");
            txtcheckto.Attributes.Add("autocomplete", "off");
            txtremarks.Attributes.Add("autocomplete", "off");
            txtSearch.Attributes.Add("autocomplete", "off");

            txtBranchCode.Attributes.Add("autocomplete", "off");
            txtBranchNo.Attributes.Add("autocomplete", "off");
            txtCPNumber.Attributes.Add("autocomplete", "off");
            txtAccountTitle.Attributes.Add("autocomplete", "off");
            txtIBAN.Attributes.Add("autocomplete", "off");
        }
        catch (Exception)
        {
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        LoadGridCountry();
    }
    public void LoadAccounts()
    {
        try
        {
            //DataTable dt = _bnkController.SelectBankAccount(Constants.IntNullValue, null, null, null, null, null, Constants.DecimalNullValue, null, Constants.IntNullValue, null, null, null, true, Constants.DateNullValue);
            //clsWebFormUtil.FillDropDownList(this.drpAccountno, dt, 0, 15, true);
        }
        catch (Exception)
        {
        }
    }
    protected void btnAddDivision_Click(object sender, EventArgs e)
    {
        try
        {
            clearControls();
            txtIssueDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            mPopupDivision.Show();
            drpAccountno.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    protected void btnSaveCountry_CLICK(object sender, EventArgs e)
    {
        try
        {
            string ChqAlpha = txtPrefix.Text;
            long ChqFromNo = Convert.ToInt64(txtCheckfrom.Text);
            long ChqToNo = Convert.ToInt64(txtcheckto.Text);
            if (ChqFromNo > ChqToNo)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Invalid cheque range');", true);
                mPopupDivision.Show();
                return;
            }
            if (btnSaveCountry.Text == "Save")
            {
                _bnkController.InsertCheckBook(Convert.ToInt32(drpAccountno.SelectedItem.Value), ChqFromNo, ChqToNo, txtremarks.Text, chkIsActive.Checked, DateTime.Parse(txtIssueDate.Text), txtBranchCode.Text, txtBranchNo.Text, txtIBAN.Text, txtAccountTitle.Text, txtCPNumber.Text, ChqAlpha);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record saved successfully');", true);
                clearControls();
                mPopupDivision.Show();
            }
            else if (btnSaveCountry.Text == "Update")
            {
                _bnkController.UpdateCheckBook(Convert.ToInt32(Session["BK_ID"]), Convert.ToInt32(drpAccountno.SelectedItem.Value), ChqFromNo, ChqToNo, txtremarks.Text, chkIsActive.Checked, DateTime.Parse(txtIssueDate.Text), 1, Convert.ToInt32(hfStatus.Value), ChqAlpha, txtBranchCode.Text, txtBranchNo.Text, txtIBAN.Text, txtAccountTitle.Text, txtCPNumber.Text);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully');", true);
                clearControls();
                btnSaveCountry.Text = "Save";
                mPopupDivision.Hide();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
        LoadGridCountry();
    }
    protected void btnActive_CLICK(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow dr in Grid_Country.Rows)
            {
                var chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                int BookId = Convert.ToInt32(dr.Cells[1].Text);
                string _status = dr.Cells[8].Text.ToString();
                if (chRelized.Checked)
                {
                    if (_status == "Active")
                    {
                        if (dr.Cells[14].Text.ToString() != "0")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "abc", "ErrorMessage()", true);
                            return;
                        }
                        _bnkController.UpdateCheckBook(BookId, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, false, Constants.DateNullValue, 2, Constants.IntNullValue, null, null, null, null, null, null);
                    }
                    else
                    {
                        _bnkController.UpdateCheckBook(BookId, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, true, Constants.DateNullValue, 2, Constants.IntNullValue, null, null, null, null, null, null);
                    }
                }
            }
            LoadGridCountry();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    public void clearControls()
    {
        try
        {
            drpAccountno.Focus();
            txtCheckfrom.Text = "";
            txtcheckto.Text = "";
            txtremarks.Text = "";
            txtBranchCode.Text = "";
            txtBranchNo.Text = "";
            txtCPNumber.Text = "";
            txtAccountTitle.Text = "";
            txtIBAN.Text = "";
            btnSaveCountry.Text = "Save";
            chkIsActive.Checked = true;

            txtCheckfrom.ReadOnly = false;
            txtcheckto.ReadOnly = false;
            hfStatus.Value = "0";
        }
        catch (Exception)
        {
        }
    }
    protected void Grid_Country_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            hfStatus.Value = "0";
            txtCheckfrom.ReadOnly = false;
            txtcheckto.ReadOnly = false;
            GridViewRow gvr = Grid_Country.Rows[e.NewEditIndex];
            Session.Add("BK_ID", Convert.ToInt32(gvr.Cells[1].Text));
            drpAccountno.Value = gvr.Cells[2].Text;

            txtBranchCode.Text = gvr.Cells[9].Text.Replace("&nbsp;", "");
            txtBranchNo.Text = gvr.Cells[10].Text.Replace("&nbsp;", "");
            txtCPNumber.Text = gvr.Cells[12].Text.Replace("&nbsp;", "");
            txtAccountTitle.Text = gvr.Cells[13].Text.Replace("&nbsp;", "");
            txtIBAN.Text = gvr.Cells[11].Text.Replace("&nbsp;", "");

            txtCheckfrom.Text = gvr.Cells[4].Text;
            txtcheckto.Text = gvr.Cells[5].Text;
            txtIssueDate.Text = Convert.ToDateTime(gvr.Cells[6].Text).ToString("dd-MMM-yyyy");
            txtremarks.Text = gvr.Cells[7].Text;
            if (gvr.Cells[8].Text == "Active")
            {
                chkIsActive.Checked = true;
            }
            else
            {
                chkIsActive.Checked = false;
            }
            if (gvr.Cells[14].Text != "0")
            {
                txtCheckfrom.ReadOnly = true;
                txtcheckto.ReadOnly = true;
                hfStatus.Value = "1";
            }
            btnSaveCountry.Text = "Update";
            mPopupDivision.Show();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    private void LoadGridCountry()
    {
        try
        {
            Grid_Country.DataSource = null;
            Grid_Country.DataBind();

            DataTable dt = new DataTable();
            dt = _bnkController.SelectCheckBook(Constants.IntNullValue, Constants.IntNullValue, null, null, null, true, Constants.DateNullValue, 1);
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            {
                dt.DefaultView.RowFilter = "ACCOUNT_DETAIL LIKE '%" + txtSearch.Text + "%' OR CHECK_FROM LIKE '%" + txtSearch.Text + "%' OR CHECK_TO LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '%" + txtSearch.Text + "%'";
            }
            Grid_Country.DataSource = dt;
            Grid_Country.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    protected void Grid_Country_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Grid_Country.PageIndex = e.NewPageIndex;
            LoadGridCountry();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    protected void btnCancelCountry_Click(object sender, EventArgs e)
    {
        try
        {
            mPopupDivision.Show();
            clearControls();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
}