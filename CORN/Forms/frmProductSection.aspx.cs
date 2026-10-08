using System;
using System.Web.UI.WebControls;
using System.Data;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;
using System.Web.UI;

public partial class Forms_frmProductSection : System.Web.UI.Page
{
    readonly GeoHierarchyController GCtl = new GeoHierarchyController();

    readonly UserController _UserCtrl = new UserController();

    private SkuController _skuController = new SkuController();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            LoadGrid("");

            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
    }
    public void Clear()
    {
        txtCodeNo.Text = string.Empty;
        txtSectionName.Text = string.Empty;
        txtPrinterName.Text = string.Empty;
        txtSectionName.Enabled = true;
        hfSectionId.Value = "";
        chkIsPrint.Checked = true;
        btnSave.Text = "Save";
    }
 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                _skuController.InsertProductSection(txtCodeNo.Text, txtSectionName.Text, txtPrinterName.Text, chkIsPrint.Checked);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');", true);
                mPopUpSection.Show();
            }
            else if (btnSave.Text == "Update")
            {
               

                bool status = true;
                if (hfStatus.Value != "Active")
                {
                    status = false;
                }

                _skuController.UpdateProductSection(Convert.ToInt32(hfSectionId.Value), txtCodeNo.Text, txtSectionName.Text, txtPrinterName.Text, status, chkIsPrint.Checked);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully.');", true);
                mPopUpSection.Hide();
            }
            Clear();
            LoadGrid("");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
 

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        mPopUpSection.Show();
        Clear();
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.LoadGrid("filter");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        mPopUpSection.Show();
    }


    private void LoadGrid(string pType)
    {

        GrdSection.DataSource = null;
        GrdSection.DataBind();


        DataTable dt = new DataTable();

        dt = _skuController.SelectProductSection(Constants.IntNullValue, null, null);
        if (pType == "")
        {
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
        {

            dt.DefaultView.RowFilter = "SECTION_NAME LIKE '%" + txtSearch.Text + "%' OR PRINTER_NAME LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '" + txtSearch.Text + "%'";

        }

        GrdSection.DataSource = dt;
        GrdSection.DataBind();
    }
        else
        {
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            {
                dt.DefaultView.RowFilter = "SECTION_NAME LIKE '%" + txtSearch.Text + "%' OR PRINTER_NAME LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '" + txtSearch.Text + "%'";
            }
            if (dt.Rows.Count > 0)
            {
                GrdSection.PageIndex = 0;
            }
            GrdSection.DataSource = dt;
            GrdSection.DataBind();
        }
    }

    protected void GrdSection_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdSection.PageIndex = e.NewPageIndex;
        LoadGrid("");
    }

    protected void GrdSection_RowEditing(object sender, GridViewEditEventArgs e)
    {
        hfSectionId.Value = GrdSection.Rows[e.NewEditIndex].Cells[1].Text;
        txtSectionName.Text = GrdSection.Rows[e.NewEditIndex].Cells[2].Text;
        txtPrinterName.Text = GrdSection.Rows[e.NewEditIndex].Cells[3].Text;

        chkIsPrint.Checked =Convert.ToBoolean(GrdSection.Rows[e.NewEditIndex].Cells[4].Text);
        hfStatus.Value= GrdSection.Rows[e.NewEditIndex].Cells[5].Text;
        //txtSectionName.Enabled = false;


        mPopUpSection.Show();

        btnSave.Text = "Update";

    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        bool check = false;
        try
        {
            foreach (GridViewRow dr2 in GrdSection.Rows)
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
            foreach (GridViewRow dr in GrdSection.Rows)
            {
                var chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
               
                if (chRelized.Checked)
                {
                    if (Convert.ToString(dr.Cells[5].Text) == "Active")
                    {
                        _UserCtrl.ActiveInactive(false, Convert.ToInt32(dr.Cells[1].Text), Constants.IntNullValue, 6);
                        flag = true;
                    }
                    else
                    {
                        _UserCtrl.ActiveInactive(true,Convert.ToInt32(dr.Cells[1].Text), Constants.IntNullValue, 6);
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
        Clear();
    }
}