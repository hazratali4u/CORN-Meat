using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;

public partial class Forms_frmByProductAssignment : System.Web.UI.Page
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
                LoadSKUFinished();
                CreateSKUDataTable();
                GetFinishedDetail();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void LoadSKUFinished()
    {
        DataTable Dtsku_Price = SKUCtl.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 5, int.Parse(Session["CompanyId"].ToString()), null);
        clsWebFormUtil.FillDxComboBoxList(this.ddlSKUFinished, Dtsku_Price, 0, 2);
        clsWebFormUtil.FillDxComboBoxList(this.ddlSKU, Dtsku_Price, 0, 2);
        if (Dtsku_Price.Rows.Count > 0)
        {
            ddlSKUFinished.SelectedIndex = 0;
            ddlSKU.SelectedIndex = 0;
        }
    }
    protected void gvSKU_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            RowId = e.NewEditIndex;
            ddlSKU.Value = gvSKU.Rows[e.NewEditIndex].Cells[0].Text;
            btnAdd.Text = "Update";
        }
        catch (Exception)
        {
            throw;
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
            if(ddlSKUFinished.SelectedItem.Value.ToString() == ddlSKU.SelectedItem.Value.ToString())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Item itemself can not be added in By Products')", true);
                return;
            }
            DataTable dtSKU = (DataTable)Session["dtSKU"];
            if (btnAdd.Text == "Add")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = dtSKU.NewRow();
                    dr["SKU_ID"] = Convert.ToInt32(ddlSKU.SelectedItem.Value);
                    dr["SKU_NAME"] = ddlSKU.SelectedItem.Text;
                    dtSKU.Rows.Add(dr);
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
                dr["SKU_NAME"] = ddlSKU.SelectedItem.Text;
                btnAdd.Text = "Add";
                RowId = 0;
            }
            ScriptManager.GetCurrent(Page).SetFocus(ddlSKU);
            LoadGrid();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void CreateSKUDataTable()
    {
        DataTable dtSKU = new DataTable();
        dtSKU.Columns.Add("SKU_ID", typeof(int));
        dtSKU.Columns.Add("SKU_NAME", typeof(string));
        Session.Add("dtSKU", dtSKU);
    }
    private bool CheckDublicateSKU()
    {
        DataTable dtSKU = (DataTable)Session["dtSKU"];
        DataRow[] foundRows = dtSKU.Select("SKU_ID  = '" + ddlSKU.SelectedItem.Value + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        return false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtSKU = (DataTable)Session["dtSKU"];
            if (SKUCtl.InsertByProduct(Convert.ToInt32(ddlSKUFinished.SelectedItem.Value), Convert.ToDateTime(System.DateTime.Now.ToShortDateString()), Convert.ToInt32(Session["UserID"]), dtSKU))
            {
                CreateSKUDataTable();
                GetFinishedDetail();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record added succesfully.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured')", true);
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
        GetFinishedDetail();
    }

    private void LoadGrid()
    {
        DataTable dtSKU = (DataTable)Session["dtSKU"];
        gvSKU.DataSource = dtSKU;
        gvSKU.DataBind();
    }
    private void GetFinishedDetail()
    {
        try
        {
            gvSKU.DataSource = null;
            gvSKU.DataBind();

            if (ddlSKUFinished.Items.Count > 0)
            {
                DataTable dtSKU = SKUCtl.GetByProduct(Convert.ToInt32(ddlSKUFinished.SelectedItem.Value), 1);
                if (dtSKU.Rows.Count > 0)
                {
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
            btnAdd.Text = "Add";
            CreateSKUDataTable();
            GetFinishedDetail();

        }
        catch (Exception)
        {

            throw;
        }
    }

}