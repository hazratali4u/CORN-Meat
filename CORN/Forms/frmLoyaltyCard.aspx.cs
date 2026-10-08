using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Collections.Generic;

public partial class Forms_frmLoyaltyCard : System.Web.UI.Page
{
    readonly LoyaltyController lController = new LoyaltyController();
    readonly DataControl dc = new DataControl();
    int CardID;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadGrid();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
    }

    #region Loyalty Card

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

    private void LoadGrid()
    {
        try
        {
            DataTable dt = lController.SelectLoyaltyCard(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 1);
            GrdLoyaltyCard.DataSource = dt;
            GrdLoyaltyCard.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
            throw;
        }
    }

    protected void GrdLoyaltyCard_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ClearAll();

        CardID = Convert.ToInt32(GrdLoyaltyCard.Rows[e.NewEditIndex].Cells[0].Text.ToString());
        try
        {
            ChbDistributorList.SelectedValue = GrdLoyaltyCard.Rows[e.NewEditIndex].Cells[1].Text.ToString();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Releavent location not found.');", true);
        }
        txtDiscount.Text = GrdLoyaltyCard.Rows[e.NewEditIndex].Cells[5].Text.ToString().Replace("&nbsp;", "");
        txtPurchasing.Text = GrdLoyaltyCard.Rows[e.NewEditIndex].Cells[6].Text.ToString().Replace("&nbsp;", "");
        txtPoints.Text = GrdLoyaltyCard.Rows[e.NewEditIndex].Cells[7].Text.ToString().Replace("&nbsp;", "");
        txtAmountLimit.Text = GrdLoyaltyCard.Rows[e.NewEditIndex].Cells[8].Text.ToString().Replace("&nbsp;", "");
        
        //txtCardNo.Text = GrdLoyaltyCard.Rows[e.NewEditIndex].Cells[9].Text.ToString().Replace("&nbsp;", "");

        if (GrdLoyaltyCard.Rows[e.NewEditIndex].Cells[2].Text.ToString() == "1")
        {
            chkPrivilege.Checked = true;
        }
        if (GrdLoyaltyCard.Rows[e.NewEditIndex].Cells[2].Text.ToString() == "2")
        {
            chkReward.Checked = true;
        }
        if (GrdLoyaltyCard.Rows[e.NewEditIndex].Cells[2].Text.ToString() == "3")
        {
            chkDirector.Checked = true;
        }
        btnSave.Text = "Update";
    }

    protected void GrdLoyaltyCard_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lController.UpdatedLoyaltyCard(Convert.ToInt32(GrdLoyaltyCard.Rows[e.RowIndex].Cells[0].Text.ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.DecimalNullValue, true, System.DateTime.Now, Convert.ToInt32(Session["UserID"]));
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Record deleted successfully.');", true);
            LoadGrid();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblErrorMsg.Visible = false;
        lblErrorMsg.Text = "";
        int _items = ChbDistributorList.Items.Count;
        int count = 0;
        foreach (ListItem item in ChbDistributorList.Items)
        {
            if (item.Selected)
            {
                count++;
            }
        }
        if (count == 0)
        {
            lblErrorMsg.Visible = true;
            lblErrorMsg.Text = Utility.ShowAlert(false, "Select Location/s");
            return;
        }
        int _Distributor_id;
        if (btnSave.Text == "Save")
        {
            try
            {
                foreach (ListItem item in ChbDistributorList.Items)
                {
                    if (item.Selected)
                    {
                        _Distributor_id = Convert.ToInt32(item.Value);

                        if (chkPrivilege.Checked)
                        {
                            lController.InsertLoyaltyCard(_Distributor_id, Constants.PrivilegeCard, Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)), Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.DecimalNullValue, System.DateTime.Now, System.DateTime.Now, Convert.ToInt32(Session["UserID"]), Convert.ToDateTime(Session["CurrentWorkDate"]), "");
                        }
                        if (chkReward.Checked)
                        {
                            lController.InsertLoyaltyCard(_Distributor_id, Constants.RewardsCard, Constants.DecimalNullValue, Convert.ToDecimal(dc.chkNull_0(txtPurchasing.Text)), Convert.ToDecimal(dc.chkNull_0(txtPoints.Text)), Constants.DecimalNullValue, System.DateTime.Now, System.DateTime.Now, Convert.ToInt32(Session["UserID"]), Convert.ToDateTime(Session["CurrentWorkDate"]),  "");
                        }
                        if (chkDirector.Checked)
                        {
                            lController.InsertLoyaltyCard(_Distributor_id, Constants.DirectorCard, Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.DecimalNullValue, Convert.ToDecimal(dc.chkNull_0(txtAmountLimit.Text)), System.DateTime.Now, System.DateTime.Now, Convert.ToInt32(Session["UserID"]), Convert.ToDateTime(Session["CurrentWorkDate"]), "");
                        }
                    }
                }
                LoadGrid();
                ClearAll();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Record added successfully.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
            }
        }
        else if (btnSave.Text == "Update")
        {
            try
            {
                foreach (ListItem item in ChbDistributorList.Items)
                {
                    if (item.Selected)
                    {
                        _Distributor_id = Convert.ToInt32(item.Value);

                        if (chkPrivilege.Checked)
                        {
                            lController.InsertLoyaltyCard(_Distributor_id, Constants.PrivilegeCard, Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)), Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.DecimalNullValue, System.DateTime.Now, System.DateTime.Now, Convert.ToInt32(Session["UserID"]), Convert.ToDateTime(Session["CurrentWorkDate"]), "");
                        }
                        if (chkReward.Checked)
                        {
                            lController.InsertLoyaltyCard(_Distributor_id, Constants.RewardsCard, Constants.DecimalNullValue, Convert.ToDecimal(dc.chkNull_0(txtPurchasing.Text)), Convert.ToDecimal(dc.chkNull_0(txtPoints.Text)), Constants.DecimalNullValue, System.DateTime.Now, System.DateTime.Now, Convert.ToInt32(Session["UserID"]), Convert.ToDateTime(Session["CurrentWorkDate"]), "");
                        }
                        if (chkDirector.Checked)
                        {
                            lController.InsertLoyaltyCard(_Distributor_id, Constants.DirectorCard, Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.DecimalNullValue, Convert.ToDecimal(dc.chkNull_0(txtAmountLimit.Text)), System.DateTime.Now, System.DateTime.Now, Convert.ToInt32(Session["UserID"]), Convert.ToDateTime(Session["CurrentWorkDate"]), "");
                        }
                    }
                }
                LoadGrid();
                ClearAll();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Record updated successfully.');", true);
                btnSave.Text = "Save";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    public void ClearAll()
    {
        lblErrorMsg.Text = "";
        lblErrorMsg.Visible = false;
        ChbSelectAll.Checked = false;
        txtAmountLimit.Text = "";
        txtDiscount.Text = "";
        txtPoints.Text = "";
        txtPurchasing.Text = "";
        CardID = 0;
        chkReward.Checked = false;
        chkPrivilege.Checked = false;
        chkDirector.Checked = false;
        //txtCardNo.Text = "";
        foreach (ListItem item in ChbDistributorList.Items)
        {
            item.Selected = false;
        }
    }

    #endregion

    protected void GrdLoyaltyCard_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdLoyaltyCard.PageIndex = e.NewPageIndex;
        LoadGrid();
    }

}
