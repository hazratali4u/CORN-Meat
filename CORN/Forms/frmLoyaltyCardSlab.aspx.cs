using System;
using System.Data;
using System.Web;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class Forms_frmLoyaltyCardSlab : System.Web.UI.Page
{
    UserController _UserCtrl = new UserController();
    COAMappingController _cController = new COAMappingController();
    readonly SkuController mSKUController = new SkuController();
    readonly SkuHierarchyController sController = new SkuHierarchyController();
    LoyaltyController LoyaltyCtl = new LoyaltyController();
    DataControl dc = new DataControl();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadItem();
            LoadLoyaltySalb("");
        }
    }
    
    private void LoadLoyaltySalb(string pType)
    {
        try
        {

            DataTable dt = new DataTable();

            dt = LoyaltyCtl.GetLoyaltyCardSlab(Constants.LongNullValue,Constants.IntNullValue,1);
            if (pType == "")
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
                {
                    dt.DefaultView.RowFilter = "decPointsFrom LIKE '%" + txtSearch.Text + "%'  OR decPointsTo LIKE '%" + txtSearch.Text + "%' OR IsActive LIKE '%" + txtSearch.Text + "%' OR decPointsDeduction LIKE '%" + txtSearch.Text + "%'";
                }
                gvLoyaltySlab.DataSource = dt;
                gvLoyaltySlab.DataBind();
            }
            else
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
                {
                    dt.DefaultView.RowFilter = "decPointsFrom LIKE '%" + txtSearch.Text + "%'  OR decPointsTo LIKE '%" + txtSearch.Text + "%' OR IsActive LIKE '%" + txtSearch.Text + "%' OR decPointsDeduction LIKE '%" + txtSearch.Text + "%'";
                }
                if (dt.Rows.Count > 0)
                {
                    gvLoyaltySlab.PageIndex = 0;
                }
                gvLoyaltySlab.DataSource = dt;
                gvLoyaltySlab.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
        }
    }

    private void LoadItem()
    {
        try
        {
            DataTable dt = mSKUController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue,Constants.IntNullValue, 16,int.Parse(Session["CompanyId"].ToString()), null);
            clsWebFormUtil.FillDxComboBoxList(ddlItem, dt, "SKU_ID", "SKU_NAME", false);
            if (ddlItem.Items.Count > 0)
            {
                ddlItem.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void LoadDistributor()
    {
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(-1, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
            clsWebFormUtil.FillListBox(ChbDistributorList, dt, 0, 2, true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
            throw;
        }
    }

    #region Grid Operations   

    protected void gvLoyaltySlab_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLoyaltySlab.PageIndex = e.NewPageIndex;
        LoadLoyaltySalb("");
    }
    protected void gvLoyaltySlab_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            ClearControls();
            mPopUpLocation.Show();
            GridViewRow gvr = gvLoyaltySlab.Rows[e.NewEditIndex];
            hfSlabID.Value = gvr.Cells[1].Text;
            rblType.SelectedValue = gvr.Cells[2].Text.ToString();
            rblType.Enabled = false;
            hfType.Value = rblType.SelectedValue;
            DataTable dtLocation = LoyaltyCtl.GetLoyaltyCardSlab(Convert.ToInt64(gvr.Cells[1].Text), Constants.IntNullValue, 2);
            foreach(DataRow dr in dtLocation.Rows)
            {
                foreach(ListItem li in ChbDistributorList.Items)
                {
                    if(li.Value == dr["intLocationID"].ToString())
                    {
                        li.Selected = true;
                        break;
                    }
                }
            }

            if (gvr.Cells[2].Text.ToString() == "1")
            {
                ddlItem.Value = Convert.ToDecimal(gvr.Cells[3].Text).ToString();
                txtPointsFrom.Text = String.Format("{0:0.00}", Convert.ToDecimal(gvr.Cells[4].Text));
                txtPointsTo.Text = String.Format("{0:0.00}", Convert.ToDecimal(gvr.Cells[5].Text));
                txtCash.Text = String.Format("{0:0.00}", Convert.ToDecimal(gvr.Cells[6].Text));
                txtDiscount.Text = String.Format("{0:0.00}", Convert.ToDecimal(gvr.Cells[7].Text));
                txtFreeUnit.Text = String.Format("{0:0.00}", Convert.ToDecimal(gvr.Cells[8].Text));
                txtPointDeduction.Text = String.Format("{0:0.00}", Convert.ToDecimal(gvr.Cells[9].Text));
            }
            else
            {
                txtPointsFrom2.Text = String.Format("{0:0.00}", Convert.ToDecimal(gvr.Cells[4].Text));
                txtPointsTo2.Text = String.Format("{0:0.00}", Convert.ToDecimal(gvr.Cells[5].Text));
                txtCash2.Text = String.Format("{0:0.00}", Convert.ToDecimal(gvr.Cells[6].Text));
                txtDiscount2.Text = String.Format("{0:0.00}", Convert.ToDecimal(gvr.Cells[7].Text));
                txtPointDeduction2.Text = String.Format("{0:0.00}", Convert.ToDecimal(gvr.Cells[9].Text));
            }
            btnSave.Text = "Update";
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
        btnSave.Text = "Save";
        ClearControls();
        mPopUpLocation.Show();
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        //btnActive.OnClientClick = string.Format("return confirm('Are you sure you want to delete?');");
        bool check = false;
        try
        {
            foreach (GridViewRow dr2 in gvLoyaltySlab.Rows)
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

            foreach (GridViewRow dr in gvLoyaltySlab.Rows)
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

            LoadLoyaltySalb("");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnSave.Text = "Save";
       
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadLoyaltySalb("filter");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool flag = false;
            DataTable dtLocation = new DataTable();
            dtLocation.Columns.Add("intLocationID", typeof(int));

            foreach(ListItem li in ChbDistributorList.Items)
            {
                if(li.Selected)
                {
                    DataRow dr = dtLocation.NewRow();
                    dr["intLocationID"] = li.Value;
                    dtLocation.Rows.Add(dr);
                }
            }
            if (btnSave.Text == "Save")
            {
                if (rblType.SelectedItem.Value == "1")
                {
                    flag = LoyaltyCtl.InsertLoyaltyCardSalab(Constants.RewardsCard, Convert.ToInt32(rblType.SelectedItem.Value), Convert.ToInt32(ddlItem.SelectedItem.Value), Convert.ToDecimal(dc.chkNull_0(txtPointsFrom.Text)), Convert.ToDecimal(dc.chkNull_0(txtPointsTo.Text)), Convert.ToDecimal(dc.chkNull_0(txtCash.Text)), Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)), Convert.ToDecimal(dc.chkNull_0(txtFreeUnit.Text)), Convert.ToDecimal(dc.chkNull_0(txtPointDeduction.Text)), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(Session["UserID"]), dtLocation);
                }
                else
                {
                    flag = LoyaltyCtl.InsertLoyaltyCardSalab(Constants.RewardsCard, Convert.ToInt32(rblType.SelectedItem.Value), Constants.IntNullValue, Convert.ToDecimal(dc.chkNull_0(txtPointsFrom2.Text)), Convert.ToDecimal(dc.chkNull_0(txtPointsTo2.Text)), Convert.ToDecimal(dc.chkNull_0(txtCash2.Text)), Convert.ToDecimal(dc.chkNull_0(txtDiscount2.Text)), Constants.DecimalNullValue, Convert.ToDecimal(dc.chkNull_0(txtPointDeduction2.Text)), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(Session["UserID"]), dtLocation);
                }
                if (flag)
                {
                    LoadLoyaltySalb("");
                    ClearControls();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record Saved Successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Some error occured!')", true);
                }
                mPopUpLocation.Show();
            }
            else
            {
                if (rblType.SelectedItem.Value == "1")
                {
                    flag = LoyaltyCtl.UpdateLoyaltyCardSalab(Convert.ToInt64(hfSlabID.Value), Constants.RewardsCard, Convert.ToInt32(rblType.SelectedItem.Value), Convert.ToInt32(ddlItem.SelectedItem.Value), Convert.ToDecimal(dc.chkNull_0(txtPointsFrom.Text)), Convert.ToDecimal(dc.chkNull_0(txtPointsTo.Text)), Convert.ToDecimal(dc.chkNull_0(txtCash.Text)), Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)), Convert.ToDecimal(dc.chkNull_0(txtFreeUnit.Text)), Convert.ToDecimal(dc.chkNull_0(txtPointDeduction.Text)),Convert.ToInt32(Session["UserID"]),true,false, dtLocation);
                }
                else
                {
                    flag = LoyaltyCtl.UpdateLoyaltyCardSalab(Convert.ToInt64(hfSlabID.Value), Constants.RewardsCard, Convert.ToInt32(rblType.SelectedItem.Value), Constants.IntNullValue, Convert.ToDecimal(dc.chkNull_0(txtPointsFrom2.Text)), Convert.ToDecimal(dc.chkNull_0(txtPointsTo2.Text)), Convert.ToDecimal(dc.chkNull_0(txtCash2.Text)), Convert.ToDecimal(dc.chkNull_0(txtDiscount2.Text)), Constants.DecimalNullValue, Convert.ToDecimal(dc.chkNull_0(txtPointDeduction2.Text)), Convert.ToInt32(Session["UserID"]),true,false, dtLocation);
                }
                if (flag)
                {
                    LoadLoyaltySalb("");
                    ClearControls();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record Saved Successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Some error occured!')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Error Occured: \n" + ex + "');", true);
        }
    }

    #endregion

    private void ClearControls()
    {
        txtCash.Text = string.Empty;
        txtCash2.Text = string.Empty;
        txtDiscount.Text = string.Empty;
        txtDiscount2.Text = string.Empty;
        txtFreeUnit.Text = string.Empty;
        txtPointDeduction.Text = string.Empty;
        txtPointDeduction2.Text = string.Empty;
        txtPointsFrom.Text = string.Empty;
        txtPointsFrom2.Text = string.Empty;
        txtPointsTo.Text = string.Empty;
        txtPointsTo2.Text = string.Empty;
        foreach(ListItem li in ChbDistributorList.Items)
        {
            li.Selected = false;
        }
    }
}