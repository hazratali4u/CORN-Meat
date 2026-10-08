using System;
using System.Data;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;
using System.Web.UI;


/// <summary>
/// From To Add, Edit, Delete SKU Hierarchy
/// </summary>
public partial class frmSKUCategory : System.Web.UI.Page
{
    readonly SkuHierarchyController mController = new SkuHierarchyController();
    readonly UserController _UserCtrl = new UserController();
    readonly SkuController _mSkuController = new SkuController();

    /// <summary>
    /// Page_Load Function Populates All Combos And Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!IsPostBack)
        {
            LoadCategoryType();
            LoadParentCategories();
            LoadGrid("");
            btnSaveCategory.Attributes.Add("onclick", "return ValidateForm()");
        }
    }
    private void LoadCategoryType()
    {
        try
        {
            DataTable dt = _mSkuController.SelectCategoryType();
            clsWebFormUtil.FillDxComboBoxList(ddlType, dt, 0, 1, true);

            if (dt.Rows.Count > 0)
            {
                ddlType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }

    /// <summary>
    /// Loads Categories To Category Grid On Category Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpCategoryPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadGrid("");
    }

    private void LoadGrid(string pType)
    {
        DataTable dt = new DataTable();
        dt = mController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()), Constants.IntNullValue);
        if (pType == "")
        {
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            {
                dt.DefaultView.RowFilter = "SKU_HIE_NAME LIKE '%" + txtSearch.Text + "%' OR TYPE LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '" + txtSearch.Text + "%'";
            }
            GrdCategory.DataSource = dt;
            GrdCategory.DataBind();
        }
        else
        {
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            {
                dt.DefaultView.RowFilter = "SKU_HIE_NAME LIKE '%" + txtSearch.Text + "%' OR TYPE LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '" + txtSearch.Text + "%'";
            }
            if (dt.Rows.Count > 0)
            {
                GrdCategory.PageIndex = 0;
            }
            GrdCategory.DataSource = dt;
            GrdCategory.DataBind();
        }
    }

    public void CheckCategoryUsed(int CategoryID)
    {
        DataTable dt = mController.SelectSkuHierarchy(Constants.IntNullValue, CategoryID, Constants.IntNullValue, null, null, true, 17, Constants.IntNullValue);
        if (Convert.ToInt32(dt.Rows[0]["USED"].ToString()) == 1)
        {
            ddlType.Enabled = false;
        }
        else
        {
            ddlType.Enabled = true;
        }
    }
    protected void GrdCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            mPopUpCategory.Show();
            btnSaveCategory.Text = "Update";
            //ddCategoryDivision.SelectedValue = GrdCategory.Rows[e.NewEditIndex].Cells[1].Text;
            hfcategoryId.Value = GrdCategory.Rows[e.NewEditIndex].Cells[1].Text;
            txtCategoryCode.Text = GrdCategory.Rows[e.NewEditIndex].Cells[2].Text;
            txtCategoryName.Text = GrdCategory.Rows[e.NewEditIndex].Cells[3].Text;
            hfStatus.Value = GrdCategory.Rows[e.NewEditIndex].Cells[5].Text;
            ddlType.Value = GrdCategory.Rows[e.NewEditIndex].Cells[7].Text;
            try
            {
                drpParentCategory.Value = GrdCategory.Rows[e.NewEditIndex].Cells[8].Text.Replace("&nbsp;", "0");
            }
            catch (Exception)
            {
                drpParentCategory.SelectedIndex = 0;
            }
            txtCategoryName.Enabled = true;
            CheckCategoryUsed(Convert.ToInt32(hfcategoryId.Value));
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }

    /// <summary>
    /// Deletes A Category
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>

    //protected void GrdCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    CategoryId = int.Parse(GrdCategory.Rows[e.RowIndex].Cells[0].Text);
    //    DataTable dt = mController.SelectSkuHierarchy(Constants.SKUBrand, CategoryId);
    //    if (dt.Rows.Count > 0)
    //    {
    //        //lblErrorMsgCategory.Text = "Wrong Command: first delete associated Brand";
    //    }
    //    else
    //    {
    //        mController.UpdateHierarchy(Constants.SKUCategory, CategoryId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
    //        this.LoadGrid();
    //    }
    //}


    protected void GrdCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdCategory.PageIndex = e.NewPageIndex;
        LoadGrid("");
    }

    public bool CheckHierarchyLevel()
    {
        DataTable dt = new DataTable();
        dt = mController.SelectSkuHierarchy(Constants.SKUCategory, Convert.ToInt32(drpParentCategory.SelectedItem.Value), Constants.IntNullValue, null, null, true, 16, Constants.IntNullValue);
        int level = Convert.ToInt32(dt.Rows[0][0].ToString());
        if (level < 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void btnSaveCategory_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CheckHierarchyLevel())
            {
                mPopUpCategory.Show();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "ErrorMessage()", true);
                return;
            }
            if (btnSaveCategory.Text == "Save")
            {
                //txtCategoryCode.Text = GetAutoCode("CA", 0);
                txtCategoryCode.Text = "";
                mController.InsertHierarchy(Constants.SKUCategory, Convert.ToInt32(ddlType.SelectedItem.Value), txtCategoryCode.Text, txtCategoryName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()), Convert.ToInt32(DataControl.chkNull_Zero(drpParentCategory.SelectedItem.Value.ToString())));
                //SetAutoCode("CA", long.Parse(txtCategoryCode.Text.Substring(2)));
                ClearAll();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "Message('Record added successfully')", true);
                mPopUpCategory.Show();
            }
            else if (btnSaveCategory.Text == "Update")
            {
                bool status = true;
                if (hfStatus.Value != "Active")
                {
                    status = false;
                }

                mController.UpdateHierarchy(Constants.SKUCategory, Convert.ToInt32(hfcategoryId.Value), Convert.ToInt32(ddlType.SelectedItem.Value), txtCategoryCode.Text, txtCategoryName.Text, null, status, int.Parse(this.Session["CompanyId"].ToString()), Convert.ToInt32(DataControl.chkNull_Zero(drpParentCategory.SelectedItem.Value.ToString())));
                ClearAll();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "Message('Record updated successfully')", true);
                mPopUpCategory.Hide();
            }
            LoadParentCategories();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
            mPopUpCategory.Show();
        }
    }
    private void ClearAll()
    {
        txtCategoryCode.Text = "";
        txtCategoryName.Text = "";

        LoadGrid("");

        btnSaveCategory.Text = "Save";
        ddlType.Enabled = true;
        //ddlType.SelectedIndex = 0;
    }
    private string GetAutoCode(string PreeFix, int CodeType)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        return AutoCode.GetAutoCode(PreeFix, CodeType, Constants.LongNullValue);
    }

    private void SetAutoCode(string PreeFix, long CValue)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        string result = AutoCode.GetAutoCode(PreeFix, 1, CValue);
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        mPopUpCategory.Show();
        //LoadGrid("");
        LoadParentCategories();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mPopUpCategory.Show();
        ClearAll();
        ddlType.SelectedIndex = 0;
        drpParentCategory.SelectedIndex = 0;
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid("filter");
        //SearchCategroy();
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        bool check = false;
        try
        {
            foreach (GridViewRow dr2 in GrdCategory.Rows)
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

            foreach (GridViewRow dr in GrdCategory.Rows)
            {
                var chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");

                if (chRelized.Checked)
                {
                    if (Convert.ToString(dr.Cells[5].Text) == "Active")
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
                LoadGrid("");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        mPopUpCategory.Show();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearAll();
        mPopUpCategory.Hide();
        ddlType.SelectedIndex = 0;
        drpParentCategory.SelectedIndex = 0;
    }

    public void LoadParentCategories()
    {
        try
        {
            if (ddlType.Items.Count > 0)
            {
                drpParentCategory.Items.Clear();

                DataTable dt = new DataTable();
                dt = mController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Convert.ToInt32(ddlType.SelectedItem.Value), null, null, true, int.Parse(this.Session["CompanyId"].ToString()), Constants.IntNullValue);
                drpParentCategory.Items.Add(new DevExpress.Web.ListEditItem("---Select---", "0"));

                clsWebFormUtil.FillDxComboBoxList(drpParentCategory, dt, 0, 3);

                if (dt.Rows.Count > 0)
                {
                    drpParentCategory.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
}
