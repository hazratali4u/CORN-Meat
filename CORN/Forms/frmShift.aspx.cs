using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;

/// <summary>
/// From to Add, Edit Employee
/// </summary>
public partial class Forms_frmShift : System.Web.UI.Page
{
    Distributor_UserController UController = new Distributor_UserController();
    static int UserId;

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

        Response.Expires = 0;
        Response.Cache.SetNoStore();
    }

    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        DataTable dt = mController.SelectDistributor(Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDxComboBoxList(ddDistributorId, dt, 1, 3, true);

        if (dt.Rows.Count > 0)
        {
            ddDistributorId.SelectedIndex = 0;
        }
    }

    private void LoadGrid(string pType)
    {

        if (ddDistributorId.Items.Count > 0)
        {

            DataTable dt = new DataTable();

            dt = UController.SelectShift(Convert.ToInt32(ddDistributorId.SelectedItem.Value), 3);
            if (pType == "")
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
                {
                    dt.DefaultView.RowFilter = "SHIFT_FROM LIKE '%" + txtSearch.Text + "%'  OR SHIFT_TO LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '" + txtSearch.Text + " %' OR DISTRIBUTOR_NAME LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '%" + txtSearch.Text + "%' ";
                }
                GridShift.DataSource = dt;
                GridShift.DataBind();
            }
            else
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
                {
                    dt.DefaultView.RowFilter = "SHIFT_FROM LIKE '%" + txtSearch.Text + "%'  OR SHIFT_TO LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '" + txtSearch.Text + " %'  OR DISTRIBUTOR_NAME LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '%" + txtSearch.Text + "%' ";
                }
                if (dt.Rows.Count > 0)
                {
                    GridShift.PageIndex = 0;
                }
                GridShift.DataSource = dt;
                GridShift.DataBind();
            }
        }
    }

    protected void ddDistributorId_SelectedIndexChanged(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        //LoadGrid("");
    }
    public bool DuplicateChecker()
    {
        DataTable dt = new DataTable();
        dt = UController.SelectShift(Convert.ToInt32(ddDistributorId.SelectedItem.Value), 3);
        DataRow[] dr = dt.Select("SHIFT_FROM = '" + txtTimeFrom.Text + "' AND SHIFT_TO = '" + txtTimeTo.Text + "' AND LOCATION_ID = '" + Convert.ToInt32(ddDistributorId.SelectedItem.Value) + "' ");
        if (dr.Length > 0)
        {
            return true;
        }

        return false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (DuplicateChecker())
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Already exists for selected location.');", true);
                    mPopUpLocation.Show();
                    return;
                }
                if (btnSave.Text == "Save")
                {
                    bool _result = UController.InsertShift(Convert.ToInt32(ddDistributorId.SelectedItem.Value), Convert.ToDateTime(txtTimeFrom.Text), Convert.ToDateTime(txtTimeTo.Text));
                    if (_result)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');", true);
                        mPopUpLocation.Show();
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    bool _result = UController.UpdateShift(Convert.ToInt32(Session["ShiftId"]), Convert.ToInt32(ddDistributorId.SelectedItem.Value), Convert.ToDateTime(txtTimeFrom.Text), Convert.ToDateTime(txtTimeTo.Text), true, 2);
                    if (_result)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully.');", true);
                        mPopUpLocation.Hide();
                    }
                }
                LoadGrid("");
                btnSave.Text = "Save";

            }
            else
            {
                mPopUpLocation.Show();
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
            mPopUpLocation.Show();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        ddDistributorId.SelectedIndex = 0;
        txtTimeFrom.SelectedIndex = 0;
        txtTimeTo.SelectedIndex = 0;
        mPopUpLocation.Show();
    }


    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.LoadGrid("filter");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        bool check = false;
        try
        {
            foreach (GridViewRow dr2 in GridShift.Rows)
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

            foreach (GridViewRow dr in GridShift.Rows)
            {
                var chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");

                if (chRelized.Checked)
                {
                    flag = UController.UpdateShift(Convert.ToInt32(dr.Cells[1].Text), Convert.ToInt32(ddDistributorId.SelectedItem.Value), Convert.ToDateTime(txtTimeFrom.Text), Convert.ToDateTime(txtTimeTo.Text), true, 1);

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
    protected void Grid_users_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridShift.PageIndex = e.NewPageIndex;
        LoadGrid("");
    }

    protected void GridShift_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            mPopUpLocation.Show();
            GridViewRow gvr = GridShift.Rows[e.NewEditIndex];
            Session.Add("ShiftId", Convert.ToInt32(gvr.Cells[1].Text));
            try
            {
                ddDistributorId.Value = gvr.Cells[5].Text.ToString();
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Assigned location is inactive');", true);
            }
          //  txtTimeFrom.ClearSelection();
            txtTimeFrom.Value = gvr.Cells[3].Text.ToString();
           // txtTimeTo.ClearSelection();
            txtTimeTo.Value = gvr.Cells[4].Text.ToString();
            btnSave.Text = "Update";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}
