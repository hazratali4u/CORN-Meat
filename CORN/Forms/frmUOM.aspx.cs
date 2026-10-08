using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;

public partial class Forms_frmUOM2 : System.Web.UI.Page
{
    readonly GeoHierarchyController DptTpe = new GeoHierarchyController();
    readonly UserController _UserCtrl = new UserController();
    DistributorController DistCtl = new DistributorController();
    DataTable dt = new DataTable();

    int count;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {

            LoadGrid("");
            maxid();
            txtDescription.Focus();
            ScriptManager.GetCurrent(Page).SetFocus(txtDescription);

            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
    }
    public void maxid()
    {
        dt = DptTpe.GetMaxUOM_ID();
        string maxid = dt.Rows[0][0].ToString();

        txtCodeNo.Text = maxid;
    }
    private bool ValidateDuplicate(string btn, int recordId)
    {
        try
        {

            if (btn == "Save")
            {
                if (_UserCtrl.IsExist(txtDescription.Text, Convert.ToInt32(ddlType.SelectedItem.Value), int.Parse(Session["UserId"].ToString()), 5))
                {
                    return false;
                }
            }
            else
            {
                if (_UserCtrl.IsExist(txtDescription.Text, Convert.ToInt32(ddlType.SelectedItem.Value), recordId, 6))
                {
                    return false;
                }
            }
            return true;
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
            throw;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Session["UserID"]);
        DateTime docDate = Convert.ToDateTime(Session["CurrentWorkDate"]);

        try
        {
            bool flag = true;

            if (btnSave.Text == "Save")
            {
                if (ValidateDuplicate("Save", 0))
                {
                    DptTpe.InsertUOM(txtCodeNo.Text, txtDescription.Text.Trim(), txtRemarks.Text.Trim(), int.Parse(ddlType.SelectedItem.Value.ToString()), DateTime.Now, Convert.ToInt32(Session["USER_ID"]), true);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');", true);
                    //  Response.Redirect(Request.Url.AbsoluteUri);
                    flag = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Already exist.');", true);
                    flag = false;
                }
                mPopUpSection.Show();

            }
            else if (btnSave.Text == "Update")
            {
                if (ValidateDuplicate("Update", Convert.ToInt32(hf_RefID.Value)))
                {
                    DptTpe.UpdateUOM(Convert.ToInt32(hf_RefID.Value), txtDescription.Text.Trim(), txtRemarks.Text.Trim(), int.Parse(ddlType.SelectedItem.Value.ToString()), DateTime.Now, Convert.ToInt32(Session["USER_ID"]), true);

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully.');", true);
                    mPopUpSection.Hide();
                    flag = true;

                }
                else {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Already exist.');", true);
                    flag = false;
                    mPopUpSection.Show();
                }
            }
            if (flag)
            {
                Cleartxt();
                LoadGrid("");

                txtDescription.Focus();
                ScriptManager.GetCurrent(Page).SetFocus(txtDescription);

                maxid();
            }
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
            mPopUpSection.Show();
        }
    }
    private void Cleartxt()
    {
        btnSave.Text = "Save";
        txtCodeNo.Text = "";
        txtDescription.Text = "";
        txtRemarks.Text = "";
        ddlType.Enabled = true;
        ddlType.SelectedIndex = 0;
        ddlType.Enabled = true;
    }
    private void LoadGrid(string pType)
    {
        DataTable dt = new DataTable();

        dt = DptTpe.GetUOM(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
        if (pType == "")
        {
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            {
                dt.DefaultView.RowFilter = "UOM_DESC LIKE '%" + txtSearch.Text + "%' OR UOM_REMARKS LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '" + txtSearch.Text + "%'";
            }
            GrdUOM.DataSource = dt;
            GrdUOM.DataBind();
        }
        else
        {
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            {
                dt.DefaultView.RowFilter = "UOM_DESC LIKE '%" + txtSearch.Text + "%' OR UOM_REMARKS LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '" + txtSearch.Text + "%'";
            }
            if (dt.Rows.Count > 0)
            {
                GrdUOM.PageIndex = 0;
            }
            GrdUOM.DataSource = dt;
            GrdUOM.DataBind();
        }

    }
    public void CheckUOMUsed(int uomID)
    {
        DataTable dt = DptTpe.GetUOM(uomID, Constants.IntNullValue, 1);
        if (Convert.ToInt32(dt.Rows[0]["USED"].ToString()) == 1)
        {
            ddlType.Enabled = false;
        }
        else
        {
            ddlType.Enabled = true;
        }
    }
    protected void GrdUOM_RowEditing(object source, GridViewEditEventArgs e)
    {
        btnSave.Text = "Update";
        mPopUpSection.Show();
        hf_RefID.Value = GrdUOM.Rows[e.NewEditIndex].Cells[1].Text;
        txtDescription.Text = GrdUOM.Rows[e.NewEditIndex].Cells[3].Text.Replace("&nbsp;", "");
        txtRemarks.Text = GrdUOM.Rows[e.NewEditIndex].Cells[4].Text.Replace("&nbsp;", "");
        ddlType.Value = GrdUOM.Rows[e.NewEditIndex].Cells[7].Text;
        CheckUOMUsed(Convert.ToInt32(hf_RefID.Value));
    }
    protected void GrdUOM_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdUOM.PageIndex = e.NewPageIndex;
        LoadGrid("");
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        bool check = false;
        try
        {
            foreach (GridViewRow dr2 in GrdUOM.Rows)
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
            foreach (GridViewRow dr in GrdUOM.Rows)
            {
                var chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
               
                if (chRelized.Checked)
                {
                    if (dr.Cells[4].Text == "Active")
                    {
                        DataTable dt = DistCtl.GetReference("UOM", Convert.ToInt64(dr.Cells[1].Text));
                        if(dt.Rows.Count >0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Can not be Inactive as reference found in Item.');", true);
                            return;
                        }
                        _UserCtrl.ActiveInactive(false, Convert.ToInt32(dr.Cells[1].Text), Constants.IntNullValue, 7);
                        flag = true;
                    }
                    else
                    {
                        _UserCtrl.ActiveInactive(true, Convert.ToInt32(dr.Cells[1].Text), Constants.IntNullValue, 7);
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
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ddlType.Enabled = true;
        mPopUpSection.Show();
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.LoadGrid("filter");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mPopUpSection.Show();
        Cleartxt();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Cleartxt();
    }
}