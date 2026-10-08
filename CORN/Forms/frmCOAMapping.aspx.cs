using System;
using System.Data;
using System.Web;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class Forms_frmCOAMapping : System.Web.UI.Page
{
    UserController _UserCtrl = new UserController();
    COAMappingController _cController = new COAMappingController();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            LoadConfiguration("");
            LoadAccountHeadMap();
            LoadAccountDetail();
        }
    }

    private void LoadAccountDetail()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = new DataTable();

        drpAccountHead.Items.Clear();

        if (drpAccountLevel.SelectedItem.Text == "Level 3")
        {
            dtHead = mAccountController.SelectAccountHead(Constants.AC_DetailTypeId, Constants.LongNullValue, 1);
        }
        else
        {
            dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue, 1);
        }

        drpAccountHead.DataSource = dtHead;
        // drpAccountHead.ValueType = typeof(long);
        drpAccountHead.ValueField = "ACCOUNT_HEAD_ID";
        drpAccountHead.DataBind();

        if (dtHead.Rows.Count > 0)
        {
            drpAccountHead.SelectedIndex = 0;
        }

    }
    private void LoadAccountHeadMap()
    {
        drpAccountHeadMap.Items.Clear();

        DataTable dt = _cController.SelectCOAConfiguration(1, Constants.ShortNullValue, Constants.LongNullValue, drpAccountLevel.SelectedItem.Value.ToString());

        //  dt = dt.AsEnumerable().Where(p => p.Field<string>("LEVEL") == drpAccountLevel.SelectedItem.Value.ToString()).CopyToDataTable();

        clsWebFormUtil.FillDxComboBoxList(drpAccountHeadMap, dt, 0, 1, true);

        if (dt.Rows.Count > 0)
        {
            drpAccountHeadMap.SelectedIndex = 0;
        }
    }
    private void LoadConfiguration(string pType)
    {
        try
        {

            DataTable dt = new DataTable();

            dt = _cController.SelectCOAConfiguration(2, Constants.ShortNullValue, Constants.LongNullValue, null);
            if (pType == "")
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
                {
                    dt.DefaultView.RowFilter = "KEY LIKE '%" + txtSearch.Text + "%'  OR ACCOUNT_NAME LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '%" + txtSearch.Text + "%' OR LEVEL LIKE '%" + txtSearch.Text + "%'";
                }
                GridCOAMapping.DataSource = dt;
                GridCOAMapping.DataBind();
            }
            else
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
                {
                    dt.DefaultView.RowFilter = "KEY LIKE '%" + txtSearch.Text + "%'  OR ACCOUNT_NAME LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE LIKE '%" + txtSearch.Text + "%' OR LEVEL LIKE '%" + txtSearch.Text + "%'";
                }
                if (dt.Rows.Count > 0)
                {
                    GridCOAMapping.PageIndex = 0;
                }
                GridCOAMapping.DataSource = dt;
                GridCOAMapping.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
        }
    }

    protected void drpAccountLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountHeadMap();
        LoadAccountDetail();
        mPopUpLocation.Show();
    }

    #region Grid Operations   

    protected void GridCOAMapping_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridCOAMapping.PageIndex = e.NewPageIndex;
        LoadConfiguration("");
    }
    protected void GridCOAMapping_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

            GridViewRow gvr = GridCOAMapping.Rows[e.NewEditIndex];

            drpAccountLevel.Value = gvr.Cells[2].Text.ToString();


            DataTable dt = _cController.ChekCOAConfigurationExistance(3, Convert.ToInt64(gvr.Cells[4].Text.ToString()), drpAccountLevel.Value.ToString());


            if (Convert.ToInt32(dt.Rows[0]["IS_EXIST"]) == 0)
            {
                mPopUpLocation.Show();


                LoadAccountHeadMap();
                LoadAccountDetail();

                drpAccountHeadMap.Value = gvr.Cells[1].Text.ToString();
                drpAccountHead.Value = gvr.Cells[4].Text.ToString();

                btnSave.Text = "Update";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Transaction exist against this Account!');", true);
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    #endregion

    #region Click

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        drpAccountLevel.SelectedIndex = 0;
        drpAccountLevel_SelectedIndexChanged(null, null);
        btnSave.Text = "Save";
        mPopUpLocation.Show();
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        //btnActive.OnClientClick = string.Format("return confirm('Are you sure you want to delete?');");
        bool check = false;
        try
        {
            foreach (GridViewRow dr2 in GridCOAMapping.Rows)
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

            foreach (GridViewRow dr in GridCOAMapping.Rows)
            {

                var chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");

                if (chRelized.Checked)
                {

                    if (Convert.ToString(dr.Cells[6].Text) == "Active")
                    {

                        _UserCtrl.ActiveInactive(false, Convert.ToInt32(dr.Cells[1].Text), 1, 9);


                        flag = true;

                    }
                    else
                    {

                        _UserCtrl.ActiveInactive(true, Convert.ToInt32(dr.Cells[1].Text), 1, 9);

                        flag = true;
                    }
                }
            }
            if (flag)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record updated successfully');", true);
            }

            LoadConfiguration("");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnSave.Text = "Save";
        drpAccountLevel.SelectedIndex = 0;
        drpAccountLevel_SelectedIndexChanged(null, null);

    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadConfiguration("filter");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            //DataTable dt = _cController.ChekCOAConfigurationExistance(3, Convert.ToInt64(drpAccountHead.Value), drpAccountLevel.Value.ToString());
            bool flag = false;
            string drpAccountHeadValue = drpAccountHead.Value.ToString();
            //if (Convert.ToInt32(dt.Rows[0]["IS_EXIST"]) == 0)
            //{
            flag = _cController.UpdateAppConfiguration(Convert.ToInt16(drpAccountHeadMap.SelectedItem.Value), Convert.ToInt64(drpAccountHead.Value));
            //  flag = true;

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Transaction exist against this Account!');", true);

            //}

            if (flag)
            {
                LoadConfiguration("");
                LoadAccountDetail();
                drpAccountHead.Value = drpAccountHeadValue;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Mapped Successfully!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Cannnot Configure')", true);

            }
            mPopUpLocation.Show();
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Error Occured: \n" + ex + "');", true);
        }
    }

    #endregion
}