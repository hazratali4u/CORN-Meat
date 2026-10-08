using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;

public partial class Forms_frmTableDefination : System.Web.UI.Page
{
    readonly GeoHierarchyController GCtl = new GeoHierarchyController();
    DataTable dt;
    int count;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadGrid("");

            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        try
        {
            DataTable dt = DController.SelectDistributorInfo(-1, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDxComboBoxList(ddlDistributor, dt, 0, 2, true);

                if (dt.Rows.Count > 0)
                {
                    ddlDistributor.SelectedIndex = 0;
                }

        }
        catch (Exception)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please assign Location');", true);
        }
    }
    public void Clear()
    {
        txtTableNo.Text = string.Empty;
        txtDescription.Text = string.Empty;
        hf_tbldefID.Value = "";
        ddlDistributor.Enabled = true;
        btnSave.Text = "Save";

        txtTableNo.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       
        const string tblCapacity = "N/A";
        const string tblAbbrivation = "N/A";

        try
        {
            if (btnSave.Text == "Save")
            {
                GCtl.InsertTableDefination(int.Parse(ddlDistributor.SelectedItem.Value.ToString()), txtTableNo.Text.Trim(), txtDescription.Text.Trim(), tblCapacity, tblAbbrivation, System.DateTime.Now, int.Parse(this.Session["UserId"].ToString()));

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


                GCtl.UpdateTableDefination(Convert.ToInt32(hf_tbldefID.Value), txtTableNo.Text.Trim(), txtDescription.Text.Trim(), tblCapacity, tblAbbrivation, System.DateTime.Now, int.Parse(this.Session["UserId"].ToString()), status,Convert.ToInt32(ddlDistributor.SelectedItem.Value));

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully.');", true);
                mPopUpSection.Hide();

            }
            Clear();
            LoadGrid("");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
            mPopUpSection.Show();
        }
    }
    private void LoadGrid(string pType)
    {
        GrdTable.DataSource = null;
        GrdTable.DataBind();
        if (ddlDistributor.Items.Count > 0)
        {
            DataTable dt = new DataTable();

            dt = GCtl.GetTableDefination(Constants.IntNullValue, Constants.IntNullValue, false, int.Parse(Session["UserId"].ToString()));//false means all
            if (pType == "")
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
                {
                    dt.DefaultView.RowFilter = "TableDefination_No LIKE '%" + txtSearch.Text + "%' OR TableDefination_Description LIKE '%" + txtSearch.Text + "%' OR Status LIKE '" + txtSearch.Text + "%'";
                }
                GrdTable.DataSource = dt;
                GrdTable.DataBind();
            }
            else
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
                {
                    dt.DefaultView.RowFilter = "TableDefination_No LIKE '%" + txtSearch.Text + "%' OR TableDefination_Description LIKE '%" + txtSearch.Text + "%' OR Status LIKE '" + txtSearch.Text + "%'";
                }
                if (dt.Rows.Count > 0)
                {
                    GrdTable.PageIndex = 0;
                }
                GrdTable.DataSource = dt;
                GrdTable.DataBind();
            }
        }
    }
    protected void GrdTable_RowEditing(object source, GridViewEditEventArgs e)
    {
        mPopUpSection.Show();
        hf_tbldefID.Value = GrdTable.Rows[e.NewEditIndex].Cells[1].Text;
        txtTableNo.Text = GrdTable.Rows[e.NewEditIndex].Cells[3].Text;
        txtDescription.Text = GrdTable.Rows[e.NewEditIndex].Cells[4].Text;
        hfStatus.Value = GrdTable.Rows[e.NewEditIndex].Cells[5].Text;
        ddlDistributor.Value = GrdTable.Rows[e.NewEditIndex].Cells[7].Text;
        btnSave.Text = "Update";
        //ddlDistributor.Enabled = false;
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        bool check = false;
        try
        {
            foreach (GridViewRow dr2 in GrdTable.Rows)
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
            foreach (GridViewRow dr in GrdTable.Rows)
            {
                var chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");

                if (chRelized.Checked)
                {
                    if (Convert.ToString(dr.Cells[5].Text) == "Active")
                    {
                        GCtl.DeleteTableDefination(Convert.ToInt32(dr.Cells[1].Text), false);
                        flag = true;
                    }
                    else
                    {
                        GCtl.DeleteTableDefination(Convert.ToInt32(dr.Cells[1].Text), true);
                        flag = true;

                    }
                }
            }
            if (flag)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record updated successfully');", true);
            }
            this.LoadGrid("");
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        mPopUpSection.Show();
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid("filter");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mPopUpSection.Show();
        Clear();
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void GrdTable_RowEditing(object sender, GridViewPageEventArgs e)
    {
        GrdTable.PageIndex = e.NewPageIndex;
        //LoadGird();
    }
    protected void GrdTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdTable.PageIndex = e.NewPageIndex;
        LoadGrid("");
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Clear();
    }

}