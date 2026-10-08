using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;

public partial class Forms_frmChickenSKUPrice : System.Web.UI.Page
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
                LoadDistributor();
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
        DataTable Dtsku_Price = SKUCtl.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 11, int.Parse(Session["CompanyId"].ToString()), null);
        clsWebFormUtil.FillDxComboBoxList(this.ddlSKUFinished, Dtsku_Price, 0, 2);
        if (Dtsku_Price.Rows.Count > 0)
        {
            ddlSKUFinished.SelectedIndex = 0;
        }
        Session.Add("Dtsku_Price", Dtsku_Price);
    }
    private void CreateSKUDataTable()
    {
        DataTable dtSKU = new DataTable();
        dtSKU.Columns.Add("SKU_ID", typeof(int));
        dtSKU.Columns.Add("SKU_NAME", typeof(string));
        Session.Add("dtSKU", dtSKU);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SKUPriceDetailController mPriceController = new SKUPriceDetailController();
            DataControl dc = new DataControl();
            int count = 0;

            //Insert Item Trade Price
            DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
            for (int i = 0; i < ChbDistributorList.Items.Count; i++)
            {
                if (ChbDistributorList.Items[i].Selected == true)
                {
                    foreach (DataRow dr in Dtsku_Price.Rows)
                    {
                        if (dr["SKU_ID"].ToString() == ddlSKUFinished.Value.ToString())
                        {
                            mPriceController.InsertSKU_PRICESByProduct(int.Parse(ChbDistributorList.Items[i].Value), Convert.ToInt32(ddlSKUFinished.Value), 1, 0, 0, Convert.ToDecimal(dc.chkNull_0(txtTradePrice.Text)), Convert.ToDecimal(dc.chkNull_0(txtTradePrice.Text)) * Convert.ToDecimal(dc.chkNull_0(dr["Ratio_Factor"].ToString())), 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()), Convert.ToDecimal(dc.chkNull_0(dr["Ratio_Factor"].ToString())));
                        }
                    }
                }
            }

            //Insert Byproducts Trade Price
            for (int i = 0; i < ChbDistributorList.Items.Count; i++)
            {
                if (ChbDistributorList.Items[i].Selected == true)
                {
                    count++;
                    foreach (GridViewRow gvr in gvSKU.Rows)
                    {
                        if (Convert.ToDecimal(dc.chkNull_0(gvr.Cells[6].Text)) > 0)
                        {
                            mPriceController.InsertSKU_PRICESByProduct(int.Parse(ChbDistributorList.Items[i].Value), Convert.ToInt32(gvr.Cells[0].Text), 1, 0, 0, 0, Convert.ToDecimal(dc.chkNull_0(gvr.Cells[6].Text)), 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()), Convert.ToDecimal(dc.chkNull_0(gvr.Cells[4].Text)));
                        }
                    }
                }
            }
            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please select location');", true);
                return;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Price saved successfully');", true);
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
                DataTable dtSKU = SKUCtl.GetByProduct(Convert.ToInt32(ddlSKUFinished.SelectedItem.Value), 2);
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
            CreateSKUDataTable();
            GetFinishedDetail();

        }
        catch (Exception)
        {

            throw;
        }
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(-1, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(ChbDistributorList, dt, 0, 2);
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        if (Convert.ToDecimal(dc.chkNull_0(txtTradePrice.Text)) > 0)
        {
            foreach (GridViewRow gvr in gvSKU.Rows)
            {
                gvr.Cells[6].Text = (Convert.ToDecimal(dc.chkNull_0(gvr.Cells[4].Text)) * Convert.ToDecimal(dc.chkNull_0(txtTradePrice.Text))).ToString();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please enter today's price');", true);
        }
    }
}