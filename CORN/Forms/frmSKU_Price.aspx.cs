using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;

/// <summary>
/// From To Add, Edit SKU Price
/// </summary>
public partial class SKU_Price : System.Web.UI.Page
{
    readonly SkuHierarchyController _mHerController = new SkuHierarchyController();
    readonly SKUPriceDetailController mSKUController = new SKUPriceDetailController();
    private DataTable _mDt;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            //LoadSKUS();
            LoadDistributor();
            txtFromdate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            btnSave.Attributes.Add("onclick", "return ValidateForm()");

            txtFromdate.Attributes.Add("readonly", "readonly");

            Populate_drpSKUCategory();
            FillddlSkus();
            if (ddlSkus.SelectedItem != null)
            {
                LoadGrid(int.Parse(ddlSkus.SelectedItem.Value.ToString()));
            }
        }
    }

    private void LoadSkus()
    {
        DataTable dtSKU;
        SKUPriceDetailController pController = new SKUPriceDetailController();
        dtSKU = pController.SelectDataPrice2(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["DISTRIBUTOR_ID"].ToString()), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 4, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        hfSKU.Value = GetJson(dtSKU);
    }

    private void LoadAllItems()
    {

        DataTable dtsku = mSKUController.SelectSKuCurrentPrice(Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);

        DataView dv = dtsku.DefaultView;
        dv.Sort = "sku_name asc";
        DataTable sortedDt = dv.ToTable();

        Grid_pricedetails.DataSource = sortedDt;
        Grid_pricedetails.DataBind();
    }

    private void LoadGrid(int skuid)
    {
        DataTable dtsku = mSKUController.SelectSKuCurrentPrice(Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, skuid);
        Grid_pricedetails.DataSource = dtsku.DefaultView;
        Grid_pricedetails.DataBind();
        foreach (DataRow dr in dtsku.Rows)
        {
            if (ChbDistributorList.Items.FindByValue(dr["DISTRIBUTOR_ID"].ToString()) != null)
            {
                ChbDistributorList.Items.FindByValue(dr["DISTRIBUTOR_ID"].ToString()).Selected = true;
            }
        }
    }
    private void FillddlSkus()
    {
        if (ddskucategory.SelectedItem != null && ddlType.SelectedItem != null)
        {
            try
            {
                SkuController mSKUController = new SkuController();
                DataTable dt = mSKUController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue,
                    int.Parse(ddskucategory.SelectedItem.Value.ToString()), Convert.ToInt32(ddlType.SelectedItem.Value),
                    int.Parse(Session["CompanyId"].ToString()), null);
                clsWebFormUtil.FillDxComboBoxList(ddlSkus, dt, "SKU_ID", "SKU_NAME", true);
                if(ddlSkus.Items.Count > 0)
                {
                    ddlSkus.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
            }
        }
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(-1, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(ChbDistributorList, dt, 0, 2, true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SKUPriceDetailController mPriceController = new SKUPriceDetailController();
            DataControl dc = new DataControl();

            int count = 0;

            int SKUID = 0;
            if (ddlSkus.SelectedItem != null)
            {
                SKUID = Convert.ToInt32(dc.chkNull_0(ddlSkus.SelectedItem.Value.ToString()));
            }


            for (int i = 0; i < ChbDistributorList.Items.Count; i++)
            {
                if (ChbDistributorList.Items[i].Selected == true)
                {
                    count++;
                    mPriceController.InsertSKU_PRICES(int.Parse(ChbDistributorList.Items[i].Value), SKUID, 1, 0, 0, 0, decimal.Parse(dc.chkNull_0(txtTradePrice.Text)), 0, DateTime.Parse(txtFromdate.Text), 0);
                }
            }
            LoadGrid(SKUID);
            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please select location');", true);
            }
            txtTradePrice.Text = "";
        }
    }

    public string GetJson(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    protected void btnLoadGrid_Click(object sender, EventArgs e)
    {

        if (hfSKUID.Value != "")
        {
            LoadGrid(Convert.ToInt32(hfSKUID.Value));
        }
        else
        {
            hfSKUID.Value = "";
            Grid_pricedetails.DataSource = null;
            Grid_pricedetails.DataBind();
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate_drpSKUCategory();
        FillddlSkus();
    }
    protected void ddskucategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillddlSkus();
        if (ddlSkus.Items.Count > 0)
        {
            if (ddlSkus.SelectedItem != null)
            {
                LoadGrid(int.Parse(ddlSkus.SelectedItem.Value.ToString()));
            }
        }
        else
        {
            Grid_pricedetails.DataSource = null;
            Grid_pricedetails.DataBind();
        }
    }

    private void Populate_drpSKUCategory()
    {
        if (ddlType.SelectedItem != null)
        {
            _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Convert.ToInt32(ddlType.SelectedItem.Value)
            , null, null, true, int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue);
            clsWebFormUtil.FillDxComboBoxList(ddskucategory, _mDt, 0, 3, true);
            if(ddskucategory.Items.Count > 0)
            {
                ddskucategory.SelectedIndex = 0;
            }
        }
    }
    protected void ddlSkus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSkus.SelectedItem != null)
            LoadGrid(int.Parse(ddlSkus.SelectedItem.Value.ToString()));
    }

    protected void btnViewAll_Click(object sender, EventArgs e)
    {
        LoadAllItems();
    }
}
