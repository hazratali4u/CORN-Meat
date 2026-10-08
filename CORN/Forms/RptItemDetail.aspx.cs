using System;
using System.Data;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;

public partial class Forms_RptItemDetail : System.Web.UI.Page
{
    readonly SkuController _mSkuController = new SkuController();
    readonly UserController _mUController = new UserController();

    readonly DataControl dc = new DataControl();
    readonly SkuHierarchyController _mHerController = new SkuHierarchyController();
    readonly GeoHierarchyController DptTpe = new GeoHierarchyController();
    readonly ConfigurationController _cController = new ConfigurationController();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!IsPostBack)
        {

            LoadGrid("");
        }

    }



    private void LoadGrid(string pType)
    {

        grdSKUData.DataSource = null;
        grdSKUData.DataBind();

        DataTable dt = new DataTable();
        dt = _mSkuController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 15, int.Parse(Session["CompanyId"].ToString()), null);

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



    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid("filter");
    }

    protected void grdSKUData_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        grdSKUData.PageIndex = e.NewPageIndex;
        LoadGrid("");
    }
}
