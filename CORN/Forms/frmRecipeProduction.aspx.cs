using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;

public partial class Forms_frmRecipeProduction : System.Web.UI.Page
{

    readonly SKUPriceDetailController PController = new SKUPriceDetailController();
    readonly SkuController SKUCtl = new SkuController();
    readonly DataControl dc = new DataControl();

    private static int RowId;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        try
        {
            if (!IsPostBack)
            {

                txtProductionDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd/MM/yyyy");
                txtDocumentDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd/MM/yyyy");

                LoadDistributor();
                LoadSKUFinished();

                LoadSKU();
                CreateSKUDataTable();
                LoadUOM();
                GetFinishedDetail(2);
                txtProductionDate.Attributes.Add("readonly", "readonly");
                txtRecipeQty.Attributes.Add("readonly", "readonly");
                LoadGridMaster("");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    #region Load

    private void LoadDistributor()
    {
        var dController = new DistributorController();
        DataTable dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDxComboBoxList(drpDistributor, dt, 0, 2, true);
        if(drpDistributor.Items.Count > 0)
        {
            drpDistributor.SelectedIndex = 0;
        }
    }

    private void LoadUOM()
    {

        GeoHierarchyController DptTpe = new GeoHierarchyController();

        drpSkuUnit.Items.Clear();
        DataTable dt = DptTpe.GetUOM(0, Constants.IntNullValue, Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(drpSkuUnit, dt, 0, 1, true);

    }

    private void LoadSKUFinished()
    {
        DataTable Dtsku_Price = SKUCtl.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 9, int.Parse(Session["CompanyId"].ToString()), null);
        clsWebFormUtil.FillDxComboBoxList(this.ddlSKUFinished, Dtsku_Price, 0, 1, true);
        if (ddlSKUFinished.Items.Count > 0)
        {
            ddlSKUFinished.SelectedIndex = 0;
        }
    }

    private void GetFinishedDetail(int TypeID)
    {
        Clear();
        long lngRecipeProductionCode = Constants.LongNullValue;
        if(hfMasterId.Value != null)
        {
            if(hfMasterId.Value != "")
            {
                lngRecipeProductionCode = Convert.ToInt64(hfMasterId.Value);
            }
        }
        if (ddlSKUFinished.Items.Count > 0 && drpDistributor.Items.Count > 0)
        {
            txtClosing.Text = Convert.ToString(CheckStockStatus(Convert.ToInt32(ddlSKUFinished.Value)));
            hfClosing.Value = txtClosing.Text;
            DataTable dtSKU = SKUCtl.SelectRecipeInfo(Convert.ToInt32(ddlSKUFinished.Value), lngRecipeProductionCode, Convert.ToInt32(drpDistributor.Value), Convert.ToDateTime(Session["CurrentWorkDate"]),TypeID);

            if (dtSKU.Rows.Count > 0)
            {
                if (int.Parse(dtSKU.Rows[0]["intRecipeMUnitCode"].ToString()) > 0)
                {
                    drpSkuUnit.SelectedValue = dtSKU.Rows[0]["intRecipeMUnitCode"].ToString();
                }
                else
                {
                    drpSkuUnit.SelectedIndex = 0;
                }
                if (int.Parse(dtSKU.Rows[0]["DISTRIBUTOR_ID"].ToString()) > 0)
                {
                    drpDistributor.Enabled = false;
                    drpDistributor.Value = dtSKU.Rows[0]["DISTRIBUTOR_ID"].ToString();
                }
                else
                {
                    drpDistributor.Enabled = true;
                    drpDistributor.SelectedIndex = 0;
                }

                txtRecipeQty.Text = dtSKU.Rows[0]["Recipe_Qty"].ToString();
                txtActualQty.Text = dtSKU.Rows[0]["ActualProduction_Qty"].ToString();
                hfFinishMasterId.Value = dtSKU.Rows[0]["FINISHED_GOOD_MASTER_ID"].ToString();

                btnSave.Text = dtSKU.Rows[0]["TYPE"].ToString();

                Session.Add("dtSKU", dtSKU);

                LoadGrid();
            }
        }
    }

    private void LoadSKU()
    {
        try
        {
            DataTable Dtsku_Price = SKUCtl.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 6, int.Parse(Session["CompanyId"].ToString()), null);
            clsWebFormUtil.FillDropDownList(this.ddlSKU, Dtsku_Price, 0, 2, true);

        }
        catch (Exception)
        {

            throw;
        }
    }

    private void LoadGrid()
    {
        DataTable dtSKU = (DataTable)Session["dtSKU"];
        gvSKU.DataSource = dtSKU;
        gvSKU.DataBind();
    }

    private void LoadGridMaster(string pType)
    {
        DataTable dt = new DataTable();

        dt = SKUCtl.SelectRecipeInfo(Constants.IntNullValue, Constants.LongNullValue, Constants.IntNullValue,Convert.ToDateTime(Session["CurrentWorkDate"]),1);
        if (pType == "")
        {
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            {
                dt.DefaultView.RowFilter = " SKU_NAME LIKE '%" + txtSearch.Text + "%'";
            }
            GrdRecipe.DataSource = dt;
            GrdRecipe.DataBind();
        }
        else
        {
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            {
                dt.DefaultView.RowFilter = " SKU_NAME LIKE '%" + txtSearch.Text + "%'";
            }
            GrdRecipe.DataSource = dt;
            GrdRecipe.DataBind();
        }
        Session.Add("dtMaster", dt);
    }

    #endregion

    private void CreateSKUDataTable()
    {
        DataTable dtSKU = new DataTable();
        dtSKU.Columns.Add("SKU_ID", typeof(int));
        dtSKU.Columns.Add("QUANTITY", typeof(decimal));
        dtSKU.Columns.Add("SKU_NAME", typeof(string));
        dtSKU.Columns.Add("UOM_ID", typeof(int));
        dtSKU.Columns.Add("UOM_DESC", typeof(string));
        dtSKU.Columns.Add("FINISHED_GOOD_DETAIL_ID", typeof(int));
        dtSKU.Columns.Add("CLOSING_STOCK", typeof(decimal));
        dtSKU.Columns.Add("ORG_QUANTITY", typeof(decimal));

        Session.Add("dtSKU", dtSKU);
    }

    protected void gvSKU_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            mPopUpSection.Show();
            RowId = e.NewEditIndex;
            ddlSKU.SelectedValue = gvSKU.Rows[e.NewEditIndex].Cells[0].Text;
            txtQuantity.Text = gvSKU.Rows[e.NewEditIndex].Cells[3].Text;
            hfClosingDetail.Value = gvSKU.Rows[e.NewEditIndex].Cells[6].Text;
            btnAdd.Text = "Update";
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void ddlSKUFinished_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            mPopUpSection.Show();
            ddlSKU.SelectedIndex = 0;

            txtQuantity.Text = string.Empty;
            btnAdd.Text = "Add";

            CreateSKUDataTable();
            GetFinishedDetail(2);
            lblError.Text = "";
            mPopUpSection.Show();
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private decimal CheckStockStatus(int skuId)
    {

        var mController = new PhaysicalStockController();
        DataTable dt = mController.SelectSKUClosingStock2(int.Parse(drpDistributor.Value.ToString()), skuId, "N/A", DateTime.Parse(Session["CurrentWorkDate"].ToString()), 12);
        if (dt.Rows.Count > 0)
        {
            return decimal.Parse(dt.Rows[0][0].ToString());
        }
        return 0;
    }

    #region Click

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        mPopUpSection.Show();

        try
        {
            if (decimal.Parse(dc.chkNull_0(txtQuantity.Text)) < 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Quantity must be greater than 0.')", true);
            }
        }
        catch (Exception ex)
        {
        }

        lblError.Text = "";
        DataTable dtSKU = (DataTable)Session["dtSKU"];

        if (txtQuantity.Text.Length > 0)
        {
            if (btnAdd.Text == "Add")
            {
                //Do nothing
            }
            else
            {
                DataRow dr = dtSKU.Rows[RowId];
                dr["QUANTITY"] = decimal.Parse(txtQuantity.Text);
                ddlSKU.SelectedIndex = 0;
                hfClosingDetail.Value = "0";
                txtQuantity.Text = string.Empty;
                btnAdd.Text = "Add";
                RowId = 0;
            }

            ScriptManager.GetCurrent(Page).SetFocus(ddlSKU);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Quantity is required');", true);
            return;
        }

        LoadGrid();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        mPopUpSection.Show();
        try
        {
            if(decimal.Parse(dc.chkNull_0(txtActualQty.Text)) < 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Actual Quantity must be greater than 0.')", true);
            }
        }
        catch (Exception ex)
        {
        }
        lblError.Text = "";
        hfClosing.Value = Convert.ToString(CheckStockStatus(Convert.ToInt32(ddlSKUFinished.Value)));
        txtClosing.Text = hfClosing.Value;
        
        DataTable dtSKU = (DataTable)Session["dtSKU"];
        
        try
        {

            if (dtSKU.Rows.Count > 0)
            {
                if (Page.IsValid)
                {
                    if (btnSave.Text == "Save")
                    {
                        SKUCtl.InsertRecipeProduction(int.Parse(hfFinishMasterId.Value), int.Parse(drpDistributor.Value.ToString()), Convert.ToInt32(ddlSKUFinished.Value), decimal.Parse(dc.chkNull_0(txtRecipeQty.Text)), decimal.Parse(dc.chkNull_0(txtActualQty.Text)),
                           Convert.ToDateTime(Session["CurrentWorkDate"].ToString()), int.Parse(drpSkuUnit.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"].ToString()), Convert.ToInt32(Session["UserID"]), dtSKU);

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Succesfully saved.')", true);

                    }
                    else
                    {
                        if (SKUCtl.UpdateRecipeProduction(int.Parse(hfMasterId.Value), int.Parse(drpDistributor.Value.ToString()), decimal.Parse(dc.chkNull_0(txtRecipeQty.Text))
                            , int.Parse(ddlSKUFinished.Value.ToString()), decimal.Parse(dc.chkNull_0(txtActualQty.Text)),
                             Convert.ToDateTime(Session["CurrentWorkDate"].ToString()), int.Parse(drpSkuUnit.SelectedValue)
                            , Convert.ToDateTime(Session["CurrentWorkDate"].ToString()), Convert.ToInt32(Session["UserID"]), dtSKU))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Succesfully updated.')", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured.')", true);
                        }
                    }
                    CreateSKUDataTable();
                    GetFinishedDetail(2);
                    txtSearch.Text = string.Empty;
                    hfMasterId.Value = string.Empty;
                    ddlSKUFinished.Enabled = true;
                    LoadGridMaster("filter");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No Raw Material Item found.')", true);
            }
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured')", true);
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mPopUpSection.Show();
        this.CreateSKUDataTable();
        hfMasterId.Value = string.Empty;
    }

    #endregion

    private void Clear()
    {
        gvSKU.DataSource = null;
        gvSKU.DataBind();
        txtRecipeQty.Text = "0";
        txtActualQty.Text = "0";
        hfFinishMasterId.Value = "";
        btnSave.Text = "Save";
        ddlSKUFinished.Enabled = true;
        drpDistributor.Enabled = true;
        drpDistributor.SelectedIndex = 0;
    }

    public bool IsSKUEditable(int SKUID)
    {
        bool flag = true;
        DataTable dtSKU = SKUCtl.CheckSKU(SKUID);
        if (dtSKU != null)
        {
            if (dtSKU.Rows.Count > 0)
            {
                flag = false;
            }
        }
        return flag;
    }
    
    protected void GrdRecipe_RowEditing(object sender, GridViewEditEventArgs e)
    {   
        btnSave.Text = "Update";
        hfMasterId.Value = GrdRecipe.Rows[e.NewEditIndex].Cells[0].Text;
        ddlSKUFinished.Value = GrdRecipe.Rows[e.NewEditIndex].Cells[3].Text.Replace("&nbsp;", "");
        ddlSKUFinished.Enabled = false;
        CreateSKUDataTable();
        GetFinishedDetail(3);
        mPopUpSection.Show();
    }

    protected void GrdRecipe_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        mPopUpSection.Show();
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        this.LoadGridMaster("filter");
    }

    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        ddlSKUFinished.Enabled = true;
    }

    protected void btnClose_ServerClick(object sender, EventArgs e)
    {
        Clear();
        hfMasterId.Value = string.Empty;
    }

    protected void GrdRecipe_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable dtMaster = (DataTable)this.Session["dtMaster"];
            
            if(dtMaster.Rows.Count > 0)
            {
                DataRow dr = dtMaster.Rows[e.RowIndex];

                if (SKUCtl.DeleteRecipeProduction(long.Parse(dr["lngRecipeProductionCode"].ToString()),Convert.ToInt32(dr["DISTRIBUTOR_ID"]),Convert.ToDateTime(dr["Production_DATE"]),Convert.ToInt32(dr["FINISHED_SKU_ID"]), Convert.ToInt32(Session["UserID"])))
                {
                    LoadGridMaster("filter");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Succesfully deleted.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured.')", true);
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}