using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;
using System.IO;

public partial class Forms_frmFinancialYear : System.Web.UI.Page
{
    readonly FinancialYearController fyController = new FinancialYearController();
    public static int _CountryID = 0;
    public static int _CityID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadFinancialYear();

            btnSaveCountry.Attributes.Add("onclick", "return ValidateForm()");
            RemoveTextboxSuggessions();
            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
            if (txtStartDate.Text == string.Empty && txtEndDate.Text == string.Empty)
            {
                txtStartDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
                txtEndDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            }
        }
    }

    public void RemoveTextboxSuggessions()
    {
        try
        {
            txtYearName.Attributes.Add("autocomplete", "off");
            txtShortName.Attributes.Add("autocomplete", "off");
            txtDescription.Attributes.Add("autocomplete", "off");
            txtSearch.Attributes.Add("autocomplete", "off");
        }
        catch (Exception)
        {
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        LoadFinancialYear();
    }

    protected void btnAddDivision_Click(object sender, EventArgs e)
    {
        try
        {
            clearControls();

            mPopupDivision.Show();
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
            bool IsOpen = false;
            bool IsActive = false;
            DataTable dt = new DataTable();
            dt = fyController.SelectFinancialYear(null, null, null, null, Constants.ShortNullValue, false, false, 3);
            if (rlistOpenClose.SelectedValue == "1")
            {
                IsOpen = true;
            }
            if (rlistActiveInactive.SelectedValue == "1")
            {
                IsActive = true;
            }
            if (btnSaveCountry.Text == "Save")
            {
                if ((IsActive == true) && (dt.Rows.Count > 0))
                {
                    lblError.Text = "Year '" + dt.Rows[0][0].ToString() + "' is already active.";
                    mPopupDivision.Show();
                    return;
                }
                bool result = fyController.InsertFinancialYear(txtYearName.Text, txtShortName.Text, txtDescription.Text, IsOpen, IsActive, Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text));
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record saved successfully');", true);
                    LoadFinancialYear();
                    clearControls();
                    mPopupDivision.Show();
                }
            }
            else if (btnSaveCountry.Text == "Update")
            {
                dt = fyController.SelectFinancialYear(null, null, null, null, Convert.ToInt16(hfCode.Value), false, false, 3);
                if ((IsActive == true) && (dt.Rows.Count > 0))
                {
                    lblError.Text = "Year   '" + dt.Rows[0][0].ToString() + "'   is already active.";
                    mPopupDivision.Show();
                    return;
                }
                bool result = fyController.UpdateFinancialYear(txtYearName.Text, txtShortName.Text, txtDescription.Text, Convert.ToInt16(hfCode.Value), IsOpen, IsActive, Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text));
                mPopupDivision.Show();
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully');", true);
                    btnSaveCountry.Text = "Save";
                    mPopupDivision.Hide();
                    LoadFinancialYear();
                    clearControls();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    protected void btnActive_CLICK(object sender, EventArgs e)
    {

    }
    public void clearControls()
    {
        try
        {
            lblError.Text = "";
            txtDescription.Text = "";
            txtShortName.Text = "";
            txtYearName.Text = "";
            rlistActiveInactive.SelectedValue = "1";
            rlistOpenClose.SelectedValue = "1";
            btnSaveCountry.Text = "Save";
            if (Convert.ToInt32(hfYearsAdded.Value) > 0)
            {
                txtStartDate.Text = hfStartDate.Value;
                txtEndDate.Text = hfEndDate.Value;
                ibnStartDate.Enabled = false;
            }
            else
            {
                ibnStartDate.Enabled = true;
            }
        }
        catch (Exception)
        {
        }
    }
    protected void Grid_Country_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            StringWriter str ;
            clearControls();
            hfCode.Value = "0";
            GridViewRow gvr = Grid_Country.Rows[e.NewEditIndex];
            hfCode.Value = gvr.Cells[0].Text.ToString();
            string YearName = gvr.Cells[1].Text.Replace("&nbsp;", "");
            string shortName = gvr.Cells[2].Text.Replace("&nbsp;", "");
            string desc = gvr.Cells[4].Text.Replace("&nbsp;", "");
            txtStartDate.Text = gvr.Cells[5].Text.Replace("&nbsp;", "");
            txtEndDate.Text = gvr.Cells[6].Text.Replace("&nbsp;", "");

            str = new StringWriter();
            System.Web.HttpUtility.HtmlDecode(YearName, str);
            txtYearName.Text = str.ToString();

            str = new StringWriter();
            System.Web.HttpUtility.HtmlDecode(shortName, str);
            txtShortName.Text = str.ToString();

            str = new StringWriter();
            System.Web.HttpUtility.HtmlDecode(desc, str);
            txtDescription.Text = str.ToString();

            rlistOpenClose.SelectedValue = (gvr.Cells[9].Text == "True") ? "1" : "0";
            rlistActiveInactive.SelectedValue = (gvr.Cells[10].Text == "True") ? "1" : "0";

            if (Convert.ToInt32(hfYearsAdded.Value) == 1)
            {
                ibnStartDate.Enabled = true;
            }

            btnSaveCountry.Text = "Update";
            mPopupDivision.Show();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    private void LoadFinancialYear()
    {
        try
        {
            Grid_Country.DataSource = null;
            Grid_Country.DataBind();

            DataTable dt = fyController.SelectFinancialYear(null, null, null, null, Constants.ShortNullValue, false, true, 2);
            if (dt != null)
            {
                hfYearsAdded.Value = dt.Rows.Count.ToString();
                int AddedYears = Convert.ToInt32(hfYearsAdded.Value);
                if (AddedYears > 0)
                {
                    hfStartDate.Value = (Convert.ToDateTime(dt.Rows[AddedYears - 1]["END DATE"].ToString()).AddDays(1)).ToString("dd-MMM-yyyy");
                    hfEndDate.Value = (Convert.ToDateTime(hfStartDate.Value).AddYears(1).AddDays(-1)).ToString("dd-MMM-yyyy");
                }
            }
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            {
                dt.DefaultView.RowFilter = "[SHORT NAME] LIKE '%" + txtSearch.Text.ToUpper() + "%' OR [YEAR NAME] LIKE '%" + txtSearch.Text.ToUpper() + "%' OR [DESCRIPTION] LIKE '%" + txtSearch.Text.ToUpper() + "%' OR [IS OPEN] LIKE '%" + txtSearch.Text.ToUpper() + "%' OR [IS ACTIVE] LIKE '" + txtSearch.Text.ToUpper() + "%' OR [START DATE] LIKE '%" + txtSearch.Text.ToUpper() + "%' OR [END DATE] LIKE '%" + txtSearch.Text.ToUpper() + "%'  ";
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
            LoadFinancialYear();
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

    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    {
        txtEndDate.Text = (Convert.ToDateTime(txtStartDate.Text).AddYears(1).AddDays(-1)).ToString("dd-MMM-yyyy");
        hfEndDate.Value = txtEndDate.Text;
        mPopupDivision.Show();
    }
}