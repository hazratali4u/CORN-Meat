using System;
using System.Data;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;

/// <summary>
/// From To Add, Edit, Delete SKU
/// used PACK_SIZE for UOM
/// used BRAND_ID for CAEGORY_TYPE
/// used UNITS_IN_CASE=1 for ACTIVE, IN_ACTIVE FUNCTION 
/// used DIVISION_ID FOR PRODUCT_TYPE
/// </summary>

public partial class Forms_frmSKUData : System.Web.UI.Page
{
    readonly SkuController _mSkuController = new SkuController();
    readonly UserController _mUController = new UserController();

    readonly DataControl dc = new DataControl();
    readonly SkuHierarchyController _mHerController = new SkuHierarchyController();
    readonly GeoHierarchyController DptTpe = new GeoHierarchyController();
    readonly ConfigurationController _cController = new ConfigurationController();

    public static bool _OversaleAllowed;
    public static bool _ExpiryAllowed;

    #region Load

    List<string> Operators = new List<string>() { "+", "-", "/", "*", "%" };

    private void LoadOperators()
    {
        drpSaletoPurchaseOperator.Items.Clear();
        drpPurchaseToSaleOperator.Items.Clear();
        drpSaleToStockOperator.Items.Clear();
        drpPurchaseToStockOperator.Items.Clear();
        drpStockToSaleOperator.Items.Clear();
        drpStockToPurchaseOperator.Items.Clear();

        drpSaletoPurchaseOperator.DataSource = Operators;
        drpSaletoPurchaseOperator.DataBind();

        drpPurchaseToSaleOperator.DataSource = Operators;
        drpPurchaseToSaleOperator.DataBind();

        drpSaleToStockOperator.DataSource = Operators;
        drpSaleToStockOperator.DataBind();

        drpPurchaseToStockOperator.DataSource = Operators;
        drpPurchaseToStockOperator.DataBind();

        drpStockToSaleOperator.DataSource = Operators;
        drpStockToSaleOperator.DataBind();

        drpStockToPurchaseOperator.DataSource = Operators;
        drpStockToPurchaseOperator.DataBind();

        drpSaletoPurchaseOperator.SelectedIndex = 3;
        drpPurchaseToSaleOperator.SelectedIndex = 3;
        drpSaleToStockOperator.SelectedIndex = 3;
        drpPurchaseToStockOperator.SelectedIndex = 3;
        drpStockToSaleOperator.SelectedIndex = 3;
        drpStockToPurchaseOperator.SelectedIndex = 3;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!IsPostBack)
        {
            Session.Remove("OldSKUCode");
            GetConfiguration();
            LoadCategoryType();
            LoadCategory();
            LoadGrid("");
            LoadUOM();
            LoadSection();

            if (Session["Division"] != null)
            {
                if (Session["Division"].ToString() == "0")
                {
                    trItemType.Visible = true;
                }
            }
            LoadOperators();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
            //ddlType_SelectedIndexChanged(null, null);
        }

    }
    private void LoadCategoryType()
    {
        DataTable dt = _mSkuController.SelectCategoryType();
        clsWebFormUtil.FillDxComboBoxList(ddlType, dt, 0, 1, true);
        if (dt.Rows.Count > 0)
        {
            ddlType.SelectedIndex = 0;
        }

    }
    private void GetConfiguration()
    {
        try
        {
            DataTable dt;
            dt = _cController.SelectAppConfiguration(1, (int)Enums.AppSettingMaster.InventorySetting, null, Convert.ToInt32(Session["DISTRIBUTOR_ID"].ToString()));
            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("CODE = '" + (int)Enums.AppSetting.NegativeAllowed + "'");
                _OversaleAllowed = Convert.ToBoolean(Convert.ToInt32(dr[0]["VALUE"].ToString()));
                dr = dt.Select("CODE = '" + (int)Enums.AppSetting.ExpiryAllowed + "'");
                _ExpiryAllowed = Convert.ToBoolean(Convert.ToInt32(dr[0]["VALUE"].ToString()));
            }
            if (_OversaleAllowed)
            {
                chkIsOverSaleAllowed.Enabled = true;
            }
            else
            {
                chkIsOverSaleAllowed.Enabled = false;
            }
            if (_ExpiryAllowed)
            {
                chkIsExpiryAllowed.Enabled = true;
            }
            else
            {
                chkIsExpiryAllowed.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
            throw;
        }
    }
    private void LoadSection()
    {
        DataTable dt = _mSkuController.SelectProductSection(0, null, null);
        clsWebFormUtil.FillDxComboBoxList(ddlSection, dt, "SECTION_ID", "SECTION_NAME", true);
        if (dt.Rows.Count > 0)
        {
            ddlSection.SelectedIndex = 0;
        }
    }
    private void LoadCategory()
    {
        DataTable _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Convert.ToInt32(ddlType.SelectedItem.Value), null, null, true, 15, Constants.IntNullValue);
        clsWebFormUtil.FillDxComboBoxList(ddlCategory, _mDt, 0, 3, true);
        if (_mDt.Rows.Count > 0)
        {
            ddlCategory.SelectedIndex = 0;
        }
    }

    private void LoadAllItems()
    {

        DataTable _mSkuDt = _mSkuController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()), null);

        DataView dv = _mSkuDt.DefaultView;
        dv.Sort = "Category asc";
        DataTable sortedDt = dv.ToTable();

        grdSKUData.DataSource = sortedDt;
        grdSKUData.DataBind();
    }

    private void LoadGrid(string pType)
    {

        grdSKUData.DataSource = null;
        grdSKUData.DataBind();

        DataTable dt = new DataTable();
        dt = _mSkuController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()), null);

        if (pType == "")
        {
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)//In case after  Filter
            {
                dt.DefaultView.RowFilter = "Category LIKE '%" + txtSearch.Text + "%' OR SKU_NAME LIKE '%" + txtSearch.Text + "%' OR DESCRIPTION LIKE '%" + txtSearch.Text + "%' OR PACKSIZE LIKE '%" + txtSearch.Text + "%' OR ISACTIVE LIKE '" + txtSearch.Text + "%'";
            }
            grdSKUData.DataSource = dt;
            grdSKUData.DataBind();
        }
        else
        {
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            {
                dt.DefaultView.RowFilter = "Category LIKE '%" + txtSearch.Text + "%' OR SKU_NAME LIKE '%" + txtSearch.Text + "%' OR DESCRIPTION LIKE '%" + txtSearch.Text + "%' OR PACKSIZE LIKE '%" + txtSearch.Text + "%' OR ISACTIVE LIKE '" + txtSearch.Text + "%'";
            }
            if (dt.Rows.Count > 0)
            {
                grdSKUData.PageIndex = 0;
            }
            grdSKUData.DataSource = dt;
            grdSKUData.DataBind();
        }
    }
    private void LoadUOM()
    {
        DataTable dt = DptTpe.GetUOM(0, Convert.ToInt32(ddlType.SelectedItem.Value), Constants.IntNullValue);

        clsWebFormUtil.FillDxComboBoxList(ddlUOM, dt, 0, 1, true);
        clsWebFormUtil.FillDxComboBoxList(drpSaleUnit, dt, 0, 1, true);
        clsWebFormUtil.FillDxComboBoxList(drpPurchaseUnit, dt, 0, 1, true);
        clsWebFormUtil.FillDxComboBoxList(drpStockUnit, dt, 0, 1, true);
        clsWebFormUtil.FillDxComboBoxList(drpUnitLifeCode, dt, 0, 1, true);

        if (dt.Rows.Count > 0)
        {
            ddlUOM.SelectedIndex = 0;
            drpSaleUnit.SelectedIndex = 0;
            drpPurchaseUnit.SelectedIndex = 0;
            drpStockUnit.SelectedIndex = 0;
            drpUnitLifeCode.SelectedIndex = 0;
        }
    }

    #endregion

    #region Grid

    protected void grdSKUData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        TabContainer1.ActiveTabIndex = 0;
        mPopUpLocation.Show();
        btnSave.Text = "Update";

        try
        {
            ddlType.Value = grdSKUData.Rows[e.NewEditIndex].Cells[2].Text;
            ddlType_SelectedIndexChanged(null, null);
            hfSkuId.Value = grdSKUData.Rows[e.NewEditIndex].Cells[1].Text;
            try
            {
                if (int.Parse(grdSKUData.Rows[e.NewEditIndex].Cells[3].Text) > 0)
                {
                    ddlSection.Value = grdSKUData.Rows[e.NewEditIndex].Cells[3].Text;
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('Assigned Section is inactive');", true);
            }

            try
            {
                ddlCategory.Value = grdSKUData.Rows[e.NewEditIndex].Cells[4].Text;
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('Assigned category is inactive');", true);
            }

            txtskucode.Text = grdSKUData.Rows[e.NewEditIndex].Cells[6].Text.Replace("&nbsp;", "");
            txtskuname.Text = grdSKUData.Rows[e.NewEditIndex].Cells[7].Text.Replace("&nbsp;", "");
            Session.Add("OldSKUCode", txtskucode.Text);
            txtRatioFactor.Text = grdSKUData.Rows[e.NewEditIndex].Cells[8].Text.Replace("&nbsp;", "");
            txtWeightConsumption.Text = grdSKUData.Rows[e.NewEditIndex].Cells[15].Text.Replace("&nbsp;", "");
            chkIsInventory.Checked = (grdSKUData.Rows[e.NewEditIndex].Cells[9].Text == "&nbsp;") ? false : Convert.ToBoolean(grdSKUData.Rows[e.NewEditIndex].Cells[9].Text);
            chkIsOverSaleAllowed.Checked = (grdSKUData.Rows[e.NewEditIndex].Cells[10].Text == "&nbsp;") ? false : Convert.ToBoolean(grdSKUData.Rows[e.NewEditIndex].Cells[10].Text);
            txtDefaultQty.Text = grdSKUData.Rows[e.NewEditIndex].Cells[11].Text.Replace("&nbsp;", "");
            hfStatus.Value = grdSKUData.Rows[e.NewEditIndex].Cells[12].Text.Replace("&nbsp;", "");
            try
            {
                ddlUOM.Value = grdSKUData.Rows[e.NewEditIndex].Cells[13].Text;
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('Assigned UOM is inactive');", true);
            }
            txtDescription2.Text = grdSKUData.Rows[e.NewEditIndex].Cells[14].Text.Replace("&nbsp;", "");
            cbIs_WastageItem.Checked = (grdSKUData.Rows[e.NewEditIndex].Cells[16].Text == "&nbsp;") ? false : Convert.ToBoolean(grdSKUData.Rows[e.NewEditIndex].Cells[16].Text);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
    #endregion
    public bool Validation()
    {
        if (hfStockSale.Value == "1" && txtStockToSaleFactor.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg1", "Message('Stock to sale factor is required');", true);
            return false;
        }
        if (hfStockPurchase.Value == "1" && txtStockToPurchaseFactor.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg2", "Message('Stock to purchase factor is required');", true);
            return false;
        }
        if (hfSalePurchase.Value == "1" && txtSaleToPurchaseFactor.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "Message('Sale to purchase factor is required');", true);
            return false;
        }
        if (hfSaleStock.Value == "1" && txtSaleToStockFactor.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg4", "Message('Sale to stock factor is required');", true);
            return false;
        }
        if (hfPurchaseSale.Value == "1" && txtPurchaseToSaleFactor.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg5", "Message('Purchase to sale factor is required');", true);
            return false;
        }
        if (hfPurchaseStock.Value == "1" && txtPurchaseToStockFactor.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg6", "Message('Purchase to stock factor is required');", true);
            return false;
        }
        if (chkIsSerialized.Checked == true && txtSerialCode.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg7", "Message('Serial code is required');", true);
            return false;
        }
        if (chkIsWHTItem.Checked == true && txtWHTPercentage.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg8", "Message('WHT percentage is required');", true);
            return false;
        }
        if (chkIsFEDItem.Checked == true && txtFEDPercentage.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg9", "Message('FED percentage is required');", true);
            return false;
        }
        return true;
    }

    #region Click 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        if (!Validation())
        {
            return;
        }
        int sectionId = 0;

        try
        {
            if (ddlSection.Items.Count > 0)
            {
                sectionId = int.Parse(ddlSection.SelectedItem.Value.ToString());
            }
            if (btnSave.Text == "Save")
            {
                if (IsCodeDuplicate())
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('This Item Code already exist.');", true);
                    return;
                }
                _mSkuController.InsertSKUS(chkIsDesc.Checked, chkIsInventory.Checked, true, char.Parse("E"), 1, Convert.ToInt32(drpProductType.SelectedItem.Value), Convert.ToInt32(ddlCategory.SelectedItem.Value),
                  Convert.ToInt32(ddlType.SelectedItem.Value), 1, 0, 0, short.Parse(dc.chkNull_0(txtunitincase.Text)),
                  txtskucode.Text.Trim().ToUpper(), txtskuname.Text.Trim(), null, ddlUOM.SelectedItem.Value.ToString(), int.Parse(Session["UserId"].ToString())
                  , int.Parse(Session["CompanyId"].ToString()), txtDescription.Text.Trim(), sectionId, chkIsDeal.Checked, chkIsModifier.Checked
                  , decimal.Parse(dc.chkNull_0(txtMinLevel.Text)), decimal.Parse(dc.chkNull_0(txtReorderLevel.Text)), chkIsRecipe.Checked,
                   chkIsHasModifier.Checked, Convert.ToInt32(drpSaleUnit.SelectedItem.Value), Convert.ToInt32(drpPurchaseUnit.SelectedItem.Value), Convert.ToInt32(drpStockUnit.SelectedItem.Value),
                   float.Parse(dc.chkNull_0(txtFEDPercentage.Text)), float.Parse(dc.chkNull_0(txtWHTPercentage.Text)), float.Parse(dc.chkNull_0(txtAgeInDays.Text)), chkIsMarketItem.Checked,
                   chkIsReplaceable.Checked, chkIsFEDItem.Checked, chkIsWHTItem.Checked, chkIsSerialized.Checked,
                   chkIsHazardous.Checked, chkIsBatchItem.Checked, chkIsOverSaleAllowed.Checked, chkIsExpiryAllowed.Checked, chkIsWarehouseItem.Checked,
                   decimal.Parse(dc.chkNull_0(txtMaxLevel.Text)), decimal.Parse(dc.chkNull_0(txtSaleToPurchaseFactor.Text)), decimal.Parse(dc.chkNull_0(txtPurchaseToSaleFactor.Text)), decimal.Parse(dc.chkNull_0(txtSaleToStockFactor.Text)),
                   decimal.Parse(dc.chkNull_0(txtPurchaseToStockFactor.Text)), decimal.Parse(dc.chkNull_0(txtDefaultQty.Text)), decimal.Parse(dc.chkNull_0(txtStockToSaleFactor.Text)), decimal.Parse(dc.chkNull_0(txtStockToPurchaseFactor.Text)),
                   drpSaletoPurchaseOperator.SelectedItem.Text, drpPurchaseToSaleOperator.SelectedItem.Text, drpSaleToStockOperator.SelectedItem.Text,
                   drpPurchaseToStockOperator.SelectedItem.Text, drpStockToSaleOperator.SelectedItem.Text,
                   drpStockToPurchaseOperator.SelectedItem.Text, txtDescription2.Text, txtSerialCode.Text, drpStatus.SelectedItem.Text, txtERPCode.Text, float.Parse(dc.chkNull_0(txtShelfAgeInDays.Text))
                   , Convert.ToInt32(drpUnitLifeCode.SelectedItem.Value), Convert.ToDecimal(dc.chkNull_0(txtRatioFactor.Text)), Convert.ToDecimal(dc.chkNull_0(txtWeightConsumption.Text)),cbIs_WastageItem.Checked);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');", true);
                mPopUpLocation.Show();
            }
            else if (btnSave.Text == "Update")
            {
                if (txtskucode.Text.Trim() != Session["OldSKUCode"].ToString())
                {
                    if (IsCodeDuplicate())
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('This Item Code already exist.');", true);
                        return;
                    }
                }
                bool status = true;
                if (hfStatus.Value != "Active")
                {
                    status = false;
                }

                _mSkuController.UpdateSKUS(chkIsDesc.Checked, chkIsInventory.Checked, status, char.Parse("E"), 1, Convert.ToInt32(drpProductType.SelectedItem.Value),
                int.Parse(ddlCategory.SelectedItem.Value.ToString()), Convert.ToInt32(ddlType.SelectedItem.Value), 1, 0, 0, short.Parse(dc.chkNull_0(txtunitincase.Text))
                , Convert.ToInt32(hfSkuId.Value), txtskucode.Text.Trim().ToUpper(), txtskuname.Text.Trim(), null, ddlUOM.SelectedItem.Value.ToString()
                , int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), txtDescription.Text.Trim(), sectionId
                , chkIsDeal.Checked, chkIsModifier.Checked
                , DataControl.chkDecimalNull(txtMinLevel.Text), DataControl.chkDecimalNull(txtReorderLevel.Text), chkIsRecipe.Checked,
                 chkIsHasModifier.Checked, Convert.ToInt32(drpSaleUnit.SelectedItem.Value), Convert.ToInt32(drpPurchaseUnit.SelectedItem.Value), Convert.ToInt32(drpStockUnit.SelectedItem.Value),
                 float.Parse(dc.chkNull_0(txtFEDPercentage.Text)), float.Parse(dc.chkNull_0(txtWHTPercentage.Text)), float.Parse(dc.chkNull_0(txtAgeInDays.Text)), chkIsMarketItem.Checked,
                 chkIsReplaceable.Checked, chkIsFEDItem.Checked, chkIsWHTItem.Checked, chkIsSerialized.Checked,
                 chkIsHazardous.Checked, chkIsBatchItem.Checked, chkIsOverSaleAllowed.Checked, chkIsExpiryAllowed.Checked, chkIsWarehouseItem.Checked,
                 decimal.Parse(dc.chkNull_0(txtMaxLevel.Text)), decimal.Parse(dc.chkNull_0(txtSaleToPurchaseFactor.Text)), decimal.Parse(dc.chkNull_0(txtPurchaseToSaleFactor.Text)), decimal.Parse(dc.chkNull_0(txtSaleToStockFactor.Text)),
                 decimal.Parse(dc.chkNull_0(txtPurchaseToStockFactor.Text)), decimal.Parse(dc.chkNull_0(txtDefaultQty.Text)), decimal.Parse(dc.chkNull_0(txtStockToSaleFactor.Text)), decimal.Parse(dc.chkNull_0(txtStockToPurchaseFactor.Text)),
                 drpSaletoPurchaseOperator.SelectedItem.Text, drpPurchaseToSaleOperator.SelectedItem.Text, drpSaleToStockOperator.SelectedItem.Text,
                 drpPurchaseToStockOperator.SelectedItem.Text, drpStockToSaleOperator.SelectedItem.Text,
                 drpStockToPurchaseOperator.SelectedItem.Text, txtDescription2.Text, txtSerialCode.Text, drpStatus.SelectedItem.Text, txtERPCode.Text, float.Parse(dc.chkNull_0(txtShelfAgeInDays.Text))
                 , Convert.ToInt32(drpUnitLifeCode.SelectedItem.Value), Convert.ToDecimal(dc.chkNull_0(txtRatioFactor.Text)), Convert.ToDecimal(dc.chkNull_0(txtWeightConsumption.Text)),cbIs_WastageItem.Checked);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully.');", true);
                mPopUpLocation.Hide();
            }
            CLearAll();
            LoadGrid("");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Some error occured');", true);
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid("filter");
    }
    protected void btnViewAll_Click(object sender, EventArgs e)
    {
        LoadAllItems();

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnSave.Text = "Save";
        try
        {
            txtskuname.Text = "";
            txtDescription.Text = "";
            ddlType.SelectedIndex = 0;
            ddlType_SelectedIndexChanged(null, null);
            ddlSection.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            ddlUOM.SelectedIndex = 0;
            ToggleActivationControles(true);
            chkIsRecipe.Enabled = true;
        }
        catch (Exception)
        {

        }
        mPopUpLocation.Show();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            mPopUpLocation.Show();
            CLearAll();
            ddlSection.SelectedIndex = 0;
            ddlType.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            LoadCategory();
            LoadUOM();
        }
        catch (Exception)
        {
        }
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        bool check = false;
        try
        {
            foreach (GridViewRow dr2 in grdSKUData.Rows)
            {
                var chRelized2 = (CheckBox)dr2.Cells[0].FindControl("chkRow");

                if (chRelized2.Checked)
                {
                    check = true;
                    break;
                }

            }
            if (!check)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('No Record Selected');", true);
                return;
            }
            bool flag = false;
            foreach (GridViewRow dr in grdSKUData.Rows)
            {
                var chRelized = (CheckBox)dr.Cells[0].FindControl("chkRow");

                if (chRelized.Checked)
                {
                    if (Convert.ToString(dr.Cells[12].Text) == "Active")
                    {
                        _mUController.ActiveInactive(false, Convert.ToInt32(dr.Cells[1].Text), int.Parse(Session["UserId"].ToString()), 1);

                        flag = true;
                    }
                    else
                    {
                        _mUController.ActiveInactive(true, Convert.ToInt32(dr.Cells[1].Text), int.Parse(Session["UserId"].ToString()), 1);

                        flag = true;
                    }

                }
            }
            if (flag)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record updated successfully');", true);
            }
            LoadGrid("");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        mPopUpLocation.Hide();
        CLearAll();
        ddlSection.SelectedIndex = 0;
        ddlType.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        LoadCategory();
        LoadUOM();
    }
    #endregion

    #region Index Change

    protected void grdSKUData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSKUData.PageIndex = e.NewPageIndex;
        LoadGrid("");
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        mPopUpLocation.Show();

        if (ddlType.SelectedItem.Value.ToString() == "2")
        {
            chkIsDesc.Checked = false;
            chkIsDesc.Visible = false;

            chkIsModifier.Checked = false;
            chkIsModifier.Visible = false;

            chkIsDeal.Checked = false;
            chkIsDeal.Visible = false;

            txtMinLevel.Text = "";
            txtMinLevel.Visible = true;
            lblMin.Visible = true;

            txtMaxLevel.Text = "";
            txtMaxLevel.Visible = true;
            lblMax.Visible = true;

            txtReorderLevel.Text = "";
            txtReorderLevel.Visible = true;
            lblReorder.Visible = true;

            chkIsOverSaleAllowed.Checked = false;
            chkIsOverSaleAllowed.Visible = false;

            chkIsHasModifier.Checked = false;
            chkIsHasModifier.Visible = false;

            chkIsRecipe.Checked = false;
            //chkIsRecipe.Enabled = true;

        }
        else if (ddlType.SelectedItem.Value.ToString() == "3")
        {
            chkIsDesc.Checked = false;
            chkIsDesc.Visible = false;

            chkIsModifier.Checked = false;
            chkIsModifier.Visible = false;

            chkIsDeal.Checked = false;
            chkIsDeal.Visible = false;

            txtMinLevel.Text = "";
            txtMinLevel.Visible = false;
            lblMin.Visible = false;

            txtMaxLevel.Text = "";
            txtMaxLevel.Visible = false;
            lblMax.Visible = false;

            txtReorderLevel.Text = "";
            txtReorderLevel.Visible = false;
            lblReorder.Visible = false;

            chkIsOverSaleAllowed.Checked = false;
            chkIsOverSaleAllowed.Visible = false;

            chkIsHasModifier.Checked = false;
            chkIsHasModifier.Visible = false;

            chkIsRecipe.Visible = false;
            chkIsRecipe.Checked = false;

        }

        else
        {
            chkIsDesc.Checked = false;
            chkIsDesc.Visible = true;

            chkIsModifier.Checked = false;
            //chkIsModifier.Visible = true;

            chkIsDeal.Checked = false;
            //chkIsDeal.Visible = true;

            txtMinLevel.Text = "";
            txtMinLevel.Visible = false;
            lblMin.Visible = false;

            txtMaxLevel.Text = "";
            txtMaxLevel.Visible = false;
            lblMax.Visible = false;

            txtReorderLevel.Text = "";
            txtReorderLevel.Visible = false;
            lblReorder.Visible = false;

            chkIsOverSaleAllowed.Checked = true;
            chkIsOverSaleAllowed.Visible = true;

            chkIsHasModifier.Checked = true;
            //chkIsHasModifier.Visible = true;

            chkIsRecipe.Checked = true;
            // chkIsRecipe.Enabled = false;
        }

        LoadCategory();
        LoadUOM();

    }

    #endregion

    /// <summary>
    /// Clears Form Controls on Save and Update
    /// </summary>
    private void CLearAll()
    {
        try
        {
            Session.Remove("OldSKUCode");
            LoadGrid("");
            txtskucode.Text = "";
            txtunitincase.Text = "";
            txtskuname.Text = "";
            txtDescription.Text = "";
            btnSave.Text = "Save";
            txtMinLevel.Text = "";
            txtReorderLevel.Text = "";
            txtDefaultQty.Text = "1";
            txtDescription2.Text = "";
            txtMaxLevel.Text = "";
            txtPurchaseToStockFactor.Text = "1";
            txtPurchaseToSaleFactor.Text = "1";
            txtSaleToStockFactor.Text = "1";
            txtSaleToPurchaseFactor.Text = "1";
            txtStockToPurchaseFactor.Text = "1";
            txtStockToSaleFactor.Text = "1";
            txtSerialCode.Text = "";
            txtFEDPercentage.Text = "";
            txtWHTPercentage.Text = "";
            txtERPCode.Text = "";
            txtAgeInDays.Text = "";
            txtShelfAgeInDays.Text = "";
            chkIsInventory.Checked = true;
            chkIsHasModifier.Checked = true;
            chkIsOverSaleAllowed.Checked = true;
            chkIsRecipe.Checked = true;
            chkIsDeal.Checked = false;
            chkIsModifier.Checked = false;
            chkIsRecipe.Enabled = true;
            chkIsSerialized.Checked = false;
            chkIsFEDItem.Checked = false;
            chkIsWHTItem.Checked = false;
            chkIsHazardous.Checked = false;
            chkIsBatchItem.Checked = false;
            chkIsExpiryAllowed.Checked = false;
            chkIsWarehouseItem.Checked = false;
            chkIsMarketItem.Checked = false;
            chkIsReplaceable.Checked = false;
            ToggleActivationControles(true);
            drpStockToSaleOperator.SelectedIndex = 3;
            drpStockToPurchaseOperator.SelectedIndex = 3;
            drpSaletoPurchaseOperator.SelectedIndex = 3;
            drpSaleToStockOperator.SelectedIndex = 3;
            drpPurchaseToSaleOperator.SelectedIndex = 3;
            drpPurchaseToStockOperator.SelectedIndex = 3;
            // ddlType.SelectedIndex = 0;
            // ddlSection.SelectedIndex = 0;
            // ddlCategory.SelectedIndex = 0;
            //  ddlUOM.SelectedIndex = 0;
            TabContainer1.ActiveTabIndex = 0;
            drpStatus.SelectedIndex = 0;
            txtRatioFactor.Text = string.Empty;
            txtWeightConsumption.Text = string.Empty;
        }
        catch (Exception)
        {
        }
    }

    private void ToggleActivationControles(bool TrueFalse)
    {
        drpStockUnit.Enabled = TrueFalse;
        drpStockToSaleOperator.Enabled = TrueFalse;
        txtStockToSaleFactor.Enabled = TrueFalse;
        drpStockToPurchaseOperator.Enabled = TrueFalse;
        txtStockToPurchaseFactor.Enabled = TrueFalse;

        drpSaleUnit.Enabled = TrueFalse;
        drpSaletoPurchaseOperator.Enabled = TrueFalse;
        txtSaleToPurchaseFactor.Enabled = TrueFalse;
        drpSaleToStockOperator.Enabled = TrueFalse;
        txtSaleToStockFactor.Enabled = TrueFalse;

        drpPurchaseUnit.Enabled = TrueFalse;
        drpPurchaseToSaleOperator.Enabled = TrueFalse;
        txtPurchaseToSaleFactor.Enabled = TrueFalse;
        drpPurchaseToStockOperator.Enabled = TrueFalse;
        txtPurchaseToStockFactor.Enabled = TrueFalse;

        chkIsInventory.Enabled = TrueFalse;
    }

    /// <summary>
    /// Working on Cancel and Close Click
    /// </summary>

    protected void drpStockUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpSaleUnit.SelectedIndex = drpStockUnit.SelectedIndex;
        drpPurchaseUnit.SelectedIndex = drpStockUnit.SelectedIndex;
        mPopUpLocation.Show();
        hfStockSale.Value = "0";
        hfStockPurchase.Value = "0";
        hfSalePurchase.Value = "0";
        hfSaleStock.Value = "0";
        hfPurchaseSale.Value = "0";
        hfPurchaseStock.Value = "0";

        WhiteColor(txtStockToSaleFactor);
        WhiteColor(txtStockToPurchaseFactor);
        WhiteColor(txtSaleToPurchaseFactor);
        WhiteColor(txtSaleToStockFactor);
        WhiteColor(txtPurchaseToSaleFactor);
        WhiteColor(txtPurchaseToStockFactor);
    }

    protected void drpSaleUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        // If Sale unit is differ from stock
        if (drpSaleUnit.SelectedIndex != drpStockUnit.SelectedIndex)
        {
            hfStockSale.Value = "1";
            hfSaleStock.Value = "1";

            RedColor(txtStockToSaleFactor);
            RedColor(txtSaleToStockFactor);
        }
        else
        {
            hfStockSale.Value = "0";
            hfSaleStock.Value = "0";

            WhiteColor(txtStockToSaleFactor);
            WhiteColor(txtSaleToStockFactor);
        }
        // If Sale unit is differ from Purchase
        if (drpSaleUnit.SelectedIndex != drpPurchaseUnit.SelectedIndex)
        {
            hfSalePurchase.Value = "1";
            hfPurchaseSale.Value = "1";

            RedColor(txtSaleToPurchaseFactor);
            RedColor(txtPurchaseToSaleFactor);
        }
        else
        {
            hfSalePurchase.Value = "0";
            hfPurchaseSale.Value = "0";

            WhiteColor(txtSaleToPurchaseFactor);
            WhiteColor(txtPurchaseToSaleFactor);
        }
    }

    protected void drpPurchaseUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        // If Purchase unit is differ from stock
        if (drpPurchaseUnit.SelectedIndex != drpStockUnit.SelectedIndex)
        {
            hfStockPurchase.Value = "1";
            hfPurchaseStock.Value = "1";

            RedColor(txtStockToPurchaseFactor);
            RedColor(txtPurchaseToStockFactor);
        }
        else
        {
            hfStockPurchase.Value = "0";
            hfPurchaseStock.Value = "0";

            WhiteColor(txtStockToPurchaseFactor);
            WhiteColor(txtPurchaseToStockFactor);
        }
        // If Sale unit is differ from Purchase
        if (drpSaleUnit.SelectedIndex != drpPurchaseUnit.SelectedIndex)
        {
            hfSalePurchase.Value = "1";
            hfPurchaseSale.Value = "1";

            RedColor(txtSaleToPurchaseFactor);
            RedColor(txtPurchaseToSaleFactor);
        }
        else
        {
            hfSalePurchase.Value = "0";
            hfPurchaseSale.Value = "0";

            WhiteColor(txtSaleToPurchaseFactor);
            WhiteColor(txtPurchaseToSaleFactor);
        }
    }
    public void RedColor(TextBox txtbox)
    {
        //txtbox.BackColor = System.Drawing.Color.Red;
    }
    public void WhiteColor(TextBox txtbox)
    {
        //txtbox.BackColor = System.Drawing.Color.White;
    }

    public bool IsSKUEditable(int SKUID)
    {
        bool flag = true;
        DataTable dtSKU = _mSkuController.CheckSKU(SKUID);
        if(dtSKU != null)
        {
            if(dtSKU.Rows.Count > 0)
            {
                flag = false;
            }
        }
        return flag;
    }

    public bool IsCodeDuplicate()
    {
        bool flag = false;
        DataTable dtSKUName = _mSkuController.GetSKUByName(txtskucode.Text.Trim(), Convert.ToInt32(ddlCategory.Value), Convert.ToInt32(ddlType.Value));
        if(dtSKUName != null)
        {
            if(dtSKUName.Rows.Count > 0)
            {
                flag = true;
            }
        }
        return flag;
    }
}