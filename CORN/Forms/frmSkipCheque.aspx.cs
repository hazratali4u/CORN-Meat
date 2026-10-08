using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;

public partial class Forms_frmSkipCheque : System.Web.UI.Page
{
    public static int _CountryID = 0;
    public static int _CityID = 0;
    readonly DistributorController _dController = new DistributorController();
    readonly BankController _bnkController = new BankController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadChequBooks();
            LoadCheques(2);
            LoadGridCountry();
            pnlMainDivision.Visible = true;
            btnSaveCountry.Attributes.Add("onclick", "return ValidateForm()");
            RemoveTextboxSuggessions();
        }
    }

    public void RemoveTextboxSuggessions()
    {
        try
        {
            txtremarks.Attributes.Add("autocomplete", "off");
            txtSearch.Attributes.Add("autocomplete", "off");
        }
        catch (Exception)
        {
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        LoadGridCountry();
    }

    public void LoadChequBooks()
    {
        try
        {
            DataTable dt = new DataTable();
            AccountHeadController mAccountController = new AccountHeadController();
            dt = mAccountController.SelectAccountHeadByMapping(Constants.AC_AccountHeadId, Constants.LongNullValue, 2, Constants.AC_BankAccountHead);
            clsWebFormUtil.FillDxComboBoxList(drpBankAccount, dt, 0, 4, true);

            if (dt.Rows.Count > 0)
            {
                drpBankAccount.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }

    public void LoadCheques(int Type)
    {
        try
        {
            

            DataTable dt = new DataTable();
            dt = _bnkController.SelectCheckBook(Constants.IntNullValue, Convert.ToInt32(drpBankAccount.SelectedItem.Value), null, null, null, true, Constants.DateNullValue, Type);
            clsWebFormUtil.FillDxComboBoxList(this.drpCheque, dt, 0, 1, true);

            if (dt.Rows.Count > 0)
            {
                drpCheque.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }

    protected void btnAddDivision_Click(object sender, EventArgs e)
    {
        try
        {
            clearControls();
            mPopupDivision.Show();
            drpBankAccount.SelectedIndex = 0;
            LoadCheques(2);
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
            if (btnSaveCountry.Text == "Save")
            {
                _bnkController.UpdateChequeStatus(Constants.IntNullValue, drpCheque.SelectedItem.Text, txtremarks.Text, 2);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record saved successfully');", true);
                clearControls();
                mPopupDivision.Show();
            }
            else if (btnSaveCountry.Text == "Update")
            {
                _bnkController.UpdateChequeStatus(Constants.IntNullValue, drpCheque.SelectedItem.Text, txtremarks.Text, 2);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully');", true);
                clearControls();
                btnSaveCountry.Text = "Save";
                mPopupDivision.Hide();
            }
            LoadCheques(2);
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
                string ChqNo = Convert.ToString(dr.Cells[2].Text);
                if (chRelized.Checked)
                {
                    _bnkController.UpdateChequeStatus(Constants.IntNullValue, ChqNo, "", 3);
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
            drpBankAccount.Focus();
            txtremarks.Text = "";
            drpCheque.Enabled = true;
            drpBankAccount.Enabled = true;
            btnSaveCountry.Text = "Save";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }

    protected void Grid_Country_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            drpCheque.Enabled = true;
            drpBankAccount.Enabled = true;
            GridViewRow gvr = Grid_Country.Rows[e.NewEditIndex];
            drpBankAccount.Value = gvr.Cells[3].Text;
            LoadCheques(3);
            drpCheque.Value = gvr.Cells[1].Text;
            txtremarks.Text = gvr.Cells[4].Text.Replace("&nbsp;", "");

            drpCheque.Enabled = false;
            drpBankAccount.Enabled = false;
            btnSaveCountry.Text = "Update";
            mPopupDivision.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }

    private void LoadGridCountry()
    {
        try
        {
            Grid_Country.DataSource = null;
            Grid_Country.DataBind();

            DataTable dt = new DataTable();
            // dt = _bnkController.SelectCheckCancel(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            dt = _bnkController.SelectCheckBook(Constants.IntNullValue, Constants.IntNullValue, null, null, null, true, Constants.DateNullValue, 6);
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            {
                dt.DefaultView.RowFilter = "CHEQUE_NO LIKE '%" + txtSearch.Text + "%' OR REMARKS LIKE '%" + txtSearch.Text + "%' ";
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
            LoadCheques(2);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }

    protected void drpBankAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadCheques(2);
            mPopupDivision.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
}