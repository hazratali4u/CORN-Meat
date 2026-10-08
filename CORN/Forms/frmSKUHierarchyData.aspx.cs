using System;
using System.Data;
using System.Web.UI.WebControls;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;
using System.Web;
using System.Web.UI;

/// <summary>
/// From To Add, Edit, Delete SKU Hierarchy
/// </summary>
public partial class frmSKUHierarchyData : System.Web.UI.Page
{
    readonly SkuHierarchyController _mController = new SkuHierarchyController();

    readonly UserController _UserCtrl = new UserController();


    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!IsPostBack)
        {
            btnSavePrincipal.Attributes.Add("onclick", "return ValidateForm()");
            LoadGrid("");
        }
    }
    private void LoadGrid(string pType)
    {
        GrdPrincipal.DataSource = null;
        GrdPrincipal.DataBind();

        DataTable dt = new DataTable();
        dt = _mController.SelectPrincipal(Constants.SKUPrincipal, int.Parse(this.Session["CompanyId"].ToString()));

        if (pType == "")
        {
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            {
                dt.DefaultView.RowFilter = "SKU_HIE_NAME LIKE '%" + txtSearch.Text + "%' OR CONTACT_PERSON LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '" + txtSearch.Text + "%'";
            }
            GrdPrincipal.DataSource = dt;
            GrdPrincipal.DataBind();
        }
        else
        {
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            {
                dt.DefaultView.RowFilter = "SKU_HIE_NAME LIKE '%" + txtSearch.Text + "%' OR CONTACT_PERSON LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '" + txtSearch.Text + "%'";
            }
            if (dt.Rows.Count > 0)
            {
                GrdPrincipal.PageIndex = 0;
            }
            GrdPrincipal.DataSource = dt;
            GrdPrincipal.DataBind();
        }
    }

    protected void GrdPrincipal_RowEditing(object sender, GridViewEditEventArgs e)
    {
        hfPrincipalId.Value = GrdPrincipal.Rows[e.NewEditIndex].Cells[1].Text;
        txtPrincipalName.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[3].Text;


        txtAddress.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[5].Text.Replace("&nbsp;", "");
        txtContactPerson.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[6].Text.Replace("&nbsp;", "");
        txtEmail.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[7].Text.Replace("&nbsp;", "");
        txtPhoneNumber.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[8].Text.Replace("&nbsp;", "");
        txtFaxNumber.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[9].Text.Replace("&nbsp;", "");
        hfStatus.Value = GrdPrincipal.Rows[e.NewEditIndex].Cells[10].Text;

        btnSavePrincipal.Text = "Update";
        myModalLabel.InnerText = "Edit Supplier Information";
        mPopUpLocation.Show();
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        ClearAll();
    }
    protected void btnSavePrincipal_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                mPopUpLocation.Show();
                lblErrorMsg.Text = "";
                switch (btnSavePrincipal.Text)
                {

                    case "Save":

                        _mController.InsertPrincipal(Constants.SKUPrincipal, Constants.IntNullValue, "", txtPrincipalName.Text,
                            null, true, int.Parse(this.Session["CompanyId"].ToString()), true,
                            txtAddress.Text, txtContactPerson.Text, txtEmail.Text, txtPhoneNumber.Text, txtFaxNumber.Text);

                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');", true);


                        mPopUpLocation.Show();
                        break;

                    case "Update":

                        bool status = true;
                        if (hfStatus.Value != "Active")
                        {
                            status = false;
                        }
                        _mController.UpdatePrincipal(Constants.SKUPrincipal, int.Parse(hfPrincipalId.Value), Constants.IntNullValue, "",
                            txtPrincipalName.Text, null, status, int.Parse(this.Session["CompanyId"].ToString()),
                            true, txtAddress.Text, txtContactPerson.Text, txtEmail.Text, txtPhoneNumber.Text, txtFaxNumber.Text);

                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully.');", true);
                        mPopUpLocation.Hide();
                        break;
                }
                ClearAll();
                LoadGrid("");
            }
            else
            {
                mPopUpLocation.Show();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
            mPopUpLocation.Show();
        }
    }
    private void ClearAll()
    {
        txtPrincipalName.Text = "";
        txtAddress.Text = "";
        txtContactPerson.Text = "";
        txtEmail.Text = "";
        txtPhoneNumber.Text = "";
        txtFaxNumber.Text = "";
        lblErrorMsg.Text = "";
        btnSavePrincipal.Text = "Save";
        myModalLabel.InnerText = "Add New Supplier";
        hfPrincipalId.Value = "";

    }



    /// <summary>
    /// Gets Code For New Principal, Division, Category And Brand
    /// </summary>
    /// <param name="preeFix">Prefix</param>
    /// <param name="codeType">Type</param>
    /// <returns>Code As String</returns>
    private string GetAutoCode(string preeFix, int codeType)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        return AutoCode.GetAutoCode(preeFix, codeType, Constants.LongNullValue);
    }

    /// <summary>
    /// Sets Code For Principal, Division, Category And Brand
    /// </summary>
    /// <param name="preeFix">Prefix</param>
    /// <param name="cValue">Value</param>
    private void SetAutoCode(string preeFix, long cValue)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        string result = AutoCode.GetAutoCode(preeFix, 1, cValue);
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid("filter");

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        bool check = false;
        try
        {
            foreach (GridViewRow dr2 in GrdPrincipal.Rows)
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

            foreach (GridViewRow dr in GrdPrincipal.Rows)
            {
                var chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");


                if (chRelized.Checked)
                {

                    if (Convert.ToString(dr.Cells[10].Text) == "Active")
                    {
                        _UserCtrl.ActiveInactive(false, Convert.ToInt32(dr.Cells[1].Text), Constants.IntNullValue, 5);
                        flag = true;
                    }
                    else
                    {
                        _UserCtrl.ActiveInactive(true, Convert.ToInt32(dr.Cells[1].Text), Constants.IntNullValue, 5);
                        flag = true;
                    }
                }
                if (flag)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record updated successfully');", true);
                }
                this.LoadGrid("");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnSavePrincipal.Text = "Save";
        myModalLabel.InnerText = "Add New Supplier";
        mPopUpLocation.Show();
    }
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdPrincipal.PageIndex = e.NewPageIndex;
        LoadGrid("");
    }
}
