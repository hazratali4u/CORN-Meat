using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;

public partial class Forms_frmPackageMaterial : System.Web.UI.Page
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
                LoadSKUPackaged();

                LoadSKU();
                CreateSKUDataTable();
                LoadUOM();
                GetItemUnitAndQty();
                GetPackageDetail();

                ddlSKU_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void LoadUOM()
    {

        GeoHierarchyController DptTpe = new GeoHierarchyController();

        try
        {
            DataTable dt = DptTpe.GetUOM(0, 1, Constants.IntNullValue);
            clsWebFormUtil.FillDxComboBoxList(drpSkuUnit, dt, 0, 1, true);
            if (dt.Rows.Count > 0)
            {
                drpSkuUnit.SelectedIndex = 0;
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
    private void LoadSKUPackaged()
    {

        try
        {
            DataTable dt = SKUCtl.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 5, int.Parse(Session["CompanyId"].ToString()), null);
            clsWebFormUtil.FillDxComboBoxList(this.ddlSKUFinished, dt, 0, 2, true);
            if (dt.Rows.Count > 0)
            {
                ddlSKUFinished.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void LoadSKU()
    {
        try
        {
        DataTable dt = SKUCtl.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 7, int.Parse(Session["CompanyId"].ToString()), null);
        clsWebFormUtil.FillDxComboBoxList(ddlSKU, dt, 0, 2, true);
            if (dt.Rows.Count > 0)
            {
                ddlSKU.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {

            throw;
    }

    }


    protected void gvSKU_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            RowId = e.NewEditIndex;
            ddlSKU.Value = gvSKU.Rows[e.NewEditIndex].Cells[0].Text;
            txtQuantity.Text = gvSKU.Rows[e.NewEditIndex].Cells[3].Text;
            hfRawUnitName.Value = gvSKU.Rows[e.NewEditIndex].Cells[2].Text;
            hfRawUnit.Value = gvSKU.Rows[e.NewEditIndex].Cells[4].Text;

            chkIsDine.Checked = gvSKU.Rows[e.NewEditIndex].Cells[5].Text == "1" ? true : false;
            chkIsDelivery.Checked = gvSKU.Rows[e.NewEditIndex].Cells[6].Text == "1" ? true : false;
            chkIsTakeaway.Checked = gvSKU.Rows[e.NewEditIndex].Cells[7].Text == "1" ? true : false;

            btnAdd.Text = "Update";
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }


    protected void gvSKU_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
        DataTable dtSKU = (DataTable)this.Session["dtSKU"];
        dtSKU.Rows.RemoveAt(e.RowIndex);

        gvSKU.DataSource = dtSKU;
        gvSKU.DataBind();
    }
        catch (Exception)
        {

            throw;
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
        DataTable dtSKU = (DataTable)Session["dtSKU"];

            if (btnAdd.Text == "Add")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = dtSKU.NewRow();
                    dr["SKU_ID"] = Convert.ToInt32(ddlSKU.SelectedItem.Value);
                    dr["QUANTITY"] = decimal.Parse(txtQuantity.Text);
                    dr["SKU_NAME"] = ddlSKU.SelectedItem.Text;
                    dr["UOM_ID"] = hfRawUnit.Value;
                    dr["UOM_DESC"] = hfRawUnitName.Value;
                    dr["DINE_IN"] = Convert.ToInt32(chkIsDine.Checked);
                    dr["DELIVERY"] = Convert.ToInt32(chkIsDelivery.Checked);
                    dr["TAKEAWAY"] = Convert.ToInt32(chkIsTakeaway.Checked);

                    dtSKU.Rows.Add(dr);
                    ddlSKU.SelectedIndex = 0;
                    txtQuantity.Text = string.Empty;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Item already exists')", true);
                }
            }
            else
            {
                DataRow dr = dtSKU.Rows[RowId];
                dr["SKU_ID"] = Convert.ToInt32(ddlSKU.SelectedItem.Value);
                dr["QUANTITY"] = decimal.Parse(txtQuantity.Text);
                dr["SKU_NAME"] = ddlSKU.SelectedItem.Text;
                dr["UOM_ID"] = hfRawUnit.Value;
                dr["UOM_DESC"] = hfRawUnitName.Value;
                dr["DINE_IN"] = Convert.ToInt32(chkIsDine.Checked);
                dr["DELIVERY"] = Convert.ToInt32(chkIsDelivery.Checked);
                dr["TAKEAWAY"] = Convert.ToInt32(chkIsTakeaway.Checked);

                ddlSKU.SelectedIndex = 0;
                txtQuantity.Text = string.Empty;
                btnAdd.Text = "Add";
                RowId = 0;
            }

            ScriptManager.GetCurrent(Page).SetFocus(ddlSKU);


            LoadGrid();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
        }
    }

    private void CreateSKUDataTable()
    {
        DataTable dtSKU = new DataTable();
        dtSKU.Columns.Add("SKU_ID", typeof(int));
        dtSKU.Columns.Add("QUANTITY", typeof(decimal));
        dtSKU.Columns.Add("SKU_NAME", typeof(string));
        dtSKU.Columns.Add("UOM_ID", typeof(int));
        dtSKU.Columns.Add("UOM_DESC", typeof(string));
        dtSKU.Columns.Add("DINE_IN", typeof(int));
        dtSKU.Columns.Add("DELIVERY", typeof(int));
        dtSKU.Columns.Add("TAKEAWAY", typeof(int));

        Session.Add("dtSKU", dtSKU);
    }
    private bool CheckDublicateSKU()
    {
        try
        {
        DataTable dtSKU = (DataTable)Session["dtSKU"];
        DataRow[] foundRows = dtSKU.Select("SKU_ID  = '" + ddlSKU.SelectedItem.Value + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        return false;
    }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
        DataTable dtSKU = (DataTable)Session["dtSKU"];

        if (dtSKU.Rows.Count > 0)
        {
            if (SKUCtl.InsertPackagedSKU(Convert.ToInt32(ddlSKUFinished.SelectedItem.Value), decimal.Parse(dc.chkNull_0(txtPackageQty.Text)),
               int.Parse(drpSkuUnit.SelectedItem.Value.ToString()), Convert.ToDateTime(Session["CurrentWorkDate"].ToString()), Convert.ToInt32(Session["UserID"])
               , dtSKU))
            {
                    CreateSKUDataTable();
                    
                    GetPackageDetail();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record added succesfully.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured')", true);
            }
        }
        else
        {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No Package Material Item found.')", true);
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
        CreateSKUDataTable();
        GetItemUnitAndQty();
        GetPackageDetail();
    }

    private void LoadGrid()
    {
        DataTable dtSKU = (DataTable)Session["dtSKU"];
        gvSKU.DataSource = dtSKU;
        gvSKU.DataBind();
    }

    public void GetItemUnitAndQty()
    {
        try
        {
            if (ddlSKUFinished.Items.Count > 0)
            {
                DataTable dt = new DataTable();

                dt = SKUCtl.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, DataControl.chkIntNull(ddlSKUFinished.SelectedItem.Value.ToString()), 10, int.Parse(Session["CompanyId"].ToString()), null);
                if (dt.Rows.Count > 0)
                {
                    drpSkuUnit.Value = dt.Rows[0]["UNIT"].ToString();
                    txtPackageQty.Text = dt.Rows[0]["QUANTITY"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
        }
    }
    private void GetPackageDetail()
    {
        try
        {
        gvSKU.DataSource = null;
        gvSKU.DataBind();


        if (ddlSKUFinished.Items.Count > 0)
        {
            DataTable dtSKU = SKUCtl.GetPackageDetail(Convert.ToInt32(ddlSKUFinished.SelectedItem.Value), Constants.IntNullValue);
            if (dtSKU.Rows.Count > 0)
            {
                drpSkuUnit.Value = dtSKU.Rows[0]["intMUnitCode"].ToString();
                txtPackageQty.Text = dtSKU.Rows[0]["Package_Qty"].ToString();

                Session.Add("dtSKU", dtSKU);

                LoadGrid();
            }
        }
    }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
        }
    }

    protected void ddlSKUFinished_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSKU.SelectedIndex = 0;


            txtQuantity.Text = string.Empty;
            btnAdd.Text = "Add";

            CreateSKUDataTable();
            GetItemUnitAndQty();
            GetPackageDetail();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
        }
    }

    protected void ddlSKU_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfRawUnit.Value = "";
        hfRawUnitName.Value = "";
        DataTable dt = new DataTable();
        if (ddlSKU.Items.Count > 0)
        {
            dt = SKUCtl.SelectSkuInfo(int.Parse(ddlSKU.SelectedItem.Value.ToString()), Constants.IntNullValue, Constants.IntNullValue, 8, int.Parse(Session["CompanyId"].ToString()), null);
            if (dt.Rows.Count > 0)
            {
                hfRawUnitName.Value = dt.Rows[0]["UOM_DESC"].ToString();
                hfRawUnit.Value = dt.Rows[0]["UOM_ID"].ToString();
            }
        }
    }
}