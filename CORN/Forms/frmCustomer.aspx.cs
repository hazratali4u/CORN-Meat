using System;
using System.Data;
using System.Web.UI.WebControls;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;
using System.Web;
using System.Web.UI;
using System.IO;

public partial class frmCustomer : System.Web.UI.Page
{
    readonly DataControl dc = new DataControl();
    readonly CustomerDataController cController = new CustomerDataController();
    readonly LoyaltyController lController = new LoyaltyController();
    readonly DistributorController mController = new DistributorController();

    readonly UserController _mUController = new UserController();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");
        //=========================================================

        try
        {
            txtDOB.Attributes.Add("readonly", "true");

            if (!IsPostBack)
            {
                btnSave.Attributes.Add("onclick", "return ValidateForm()");
                LoadGrid("");

                LoadDistributor();
                LoadCardType();
                LoadCardInfo();
            
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
   
    #region Load

    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        try
        {
            DataTable dtDistributor = mController.SelectDistributor(Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
          
            clsWebFormUtil.FillDxComboBoxList(ddDistributorId, dtDistributor, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME", true);


            if (dtDistributor.Rows.Count > 0)
            {
                ddDistributorId.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }

    private void LoadGrid(string pType)
    {
        GrdCustomer.DataSource = null;
        GrdCustomer.DataBind();

        DataTable dt = new DataTable();
        dt = cController.SelectAllCustomer(int.Parse(this.Session["DISTRIBUTOR_ID"].ToString()), Constants.IntNullValue, Constants.IntNullValue);

        if (pType == "")
        {
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            {
                dt.DefaultView.RowFilter = "CUSTOMER_NAME LIKE '%" + txtSearch.Text + "%' OR CUSTOMER_CODE LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE1 LIKE '" + txtSearch.Text + "%'";
            }
            GrdCustomer.DataSource = dt;
            GrdCustomer.DataBind();
        }
        else
        {
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            {
                dt.DefaultView.RowFilter = "CUSTOMER_NAME LIKE '%" + txtSearch.Text + "%' OR CUSTOMER_CODE LIKE '%" + txtSearch.Text + "%' OR IS_ACTIVE1 LIKE '" + txtSearch.Text + "%'";
            }
            if (dt.Rows.Count > 0)
            {
                GrdCustomer.PageIndex = 0;
            }
            GrdCustomer.DataSource = dt;
            GrdCustomer.DataBind();
        }
    }

    private void LoadCardType()
    {
        try
        {
            if (ddDistributorId.Items.Count > 0)
            {
                DataTable dt = lController.SelectCardType(Convert.ToInt32(ddDistributorId.SelectedItem.Value));
                dt = dt.AsEnumerable()
                                  .Where(r => r.Field<int>("ID") != 3)
                                  .CopyToDataTable();
                clsWebFormUtil.FillDxComboBoxList(drpCardType, dt, 0, 1, true);

                if (dt.Rows.Count > 0)
                {
                    drpCardType.SelectedIndex = 0;
                }

            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void LoadCardInfo()
    {
        try
        {

            txtDiscount.Text = "";
            txtPurchasing.Text = "";
            txtPoints.Text = "";
            txtAmountLimit.Text = "";

            if (ddDistributorId.Items.Count > 0 && drpCardType.Items.Count>0)
            {
                DataTable dtCard = lController.SelectLoyaltyCard(Constants.IntNullValue, Convert.ToInt32(ddDistributorId.Value)
                , Convert.ToInt32(drpCardType.Value), 2);

                if (dtCard.Rows.Count > 0)
                {
                    txtDiscount.Text = dtCard.Rows[0][0].ToString();
                    txtPurchasing.Text = dtCard.Rows[0][1].ToString();
                    txtPoints.Text = dtCard.Rows[0][2].ToString();
                    txtAmountLimit.Text = dtCard.Rows[0][3].ToString();
                }
                ShowHideCardInfo();
            }
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }

    #endregion
    private void ShowHideCardInfo()
    {
        if (drpCardType.Value.ToString() == "0")
        {
            txtPurchasing.Visible = false;
            txtDiscount.Visible = false;
            txtCardNo.Visible = false;
            ltrlCardNo.Visible = false;
            ltrlPoints.Visible = false;
            txtPoints.Visible = false;
            ltrlCard.Visible = false;
        }
        else if (drpCardType.Value.ToString() == "2")
        {
            txtCardNo.Visible = true;
            ltrlCardNo.Visible = true;
            txtPurchasing.Visible = true;
            txtDiscount.Visible = false;

            ltrlPoints.Visible = true;
            txtPoints.Visible = true;
            ltrlCard.Visible = true;
            ltrlCard.Text = "<label><span class='fa fa-caret-right rgt_cart'></span>Purchasing</label>";
        }
        else if (drpCardType.Value.ToString() == "1")
        {
            txtPurchasing.Visible = false;
            txtCardNo.Visible = true;
            ltrlCardNo.Visible = true;
            txtDiscount.Visible = true;
            ltrlPoints.Visible = false;
            txtPoints.Visible = false;
            ltrlCard.Visible = true;
            ltrlCard.Text = "<label><span class='fa fa-caret-right rgt_cart'></span>Discount</label>";
        }
    }

    private bool ValidateCard(string btn, int recordId)
    {
        try
        {
            if (drpCardType.Value.ToString() != "0")
            {
                if (txtCardNo.Text != "")
                {
                    if (btn == "Save")
                    {
                        if (_mUController.IsExist(txtCardNo.Text, Convert.ToInt32(ddDistributorId.SelectedItem.Value), int.Parse(Session["UserId"].ToString()), 1))
                        {

                            return false;
                        }
                    }
                    else
                    {
                        if (_mUController.IsExist(txtCardNo.Text, Convert.ToInt32(ddDistributorId.SelectedItem.Value), recordId, 3))
                        {

                            return false;
                        }
                    }
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
   
    #region Index/change

    protected void ddDistributorId_SelectedIndexChanged(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        LoadCardType();
        LoadCardInfo();
    }

    protected void drpCardType_SelectedIndexChanged(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        LoadCardInfo();
    }

    #endregion

    #region Click

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        ClearAll();
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            Page.MaintainScrollPositionOnPostBack = true;
            HttpFileCollection uploadedFiles = Request.Files;
            if (FupVendor.HasFile)
            {
                HttpPostedFile userPostedFile = uploadedFiles[0];
                if (userPostedFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(userPostedFile.FileName);
                    var file = Path.Combine(Server.MapPath("~/images/"), fileName);
                    userPostedFile.SaveAs(file);
                    cController.ImportCustomer(0, Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Server.MapPath("~/images/" + fileName));
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Customers imported successfully.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('No file selected.');", true);
            }
            mPopUpLocation.Show();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
            mPopUpLocation.Show();
            throw;
        }
    }
  
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (drpCardType.SelectedItem.Value.ToString() == "1")
                {
                    if (txtCardNo.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Card No is required.');", true);
                        mPopUpLocation.Show();
                        return;
                    }
                    if (txtDiscount.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Discount is required.');", true);
                        mPopUpLocation.Show();
                        return;
                    }
                }
                else if (drpCardType.SelectedItem.Value.ToString() == "2")
                {
                    if (txtCardNo.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Card No is required.');", true);
                        mPopUpLocation.Show();
                        return;
                    }
                    if (txtPurchasing.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Purchase Amount is required.');", true);
                        mPopUpLocation.Show();
                        return;
                    }
                    if (txtPoints.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Points is required.');", true);
                        mPopUpLocation.Show();
                        return;
                    }
                  
                }
                mPopUpLocation.Show();
                lblErrorMsg.Text = "";

                bool flag = true;
                switch (btnSave.Text)
                {

                    case "Save":

                        if (ValidateCard("Save", 0))
                        {

                            CustomerDataController.InsertCustomer(Convert.ToInt32(ddDistributorId.SelectedItem.Value), txtCardNo.Text, txtCNIC.Text, txtDOB.Text
                                       , txtContact.Text, txtEmail.Text, txtName.Text, txtAddress.Text, null, Convert.ToDecimal(dc.chkNull_0(txtOpeningAmount.Text))
                                       , txtNature.Text, txtContact2.Text, Convert.ToInt32(drpCardType.SelectedItem.Value), Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text))
                                       , Convert.ToDecimal(dc.chkNull_0(txtPurchasing.Text)), Convert.ToDecimal(dc.chkNull_0(txtPoints.Text)), Convert.ToDecimal(dc.chkNull_0(txtAmountLimit.Text)));
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');", true);

                            flag = true;
                        }
                        else
                        {
                            flag = false;
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Card No already exist.');", true);
                        }
                        mPopUpLocation.Show();
                        break;
                
                    case "Update":

                        if (ValidateCard("Update", Convert.ToInt32(hfCustomerID.Value)))
                        {
                            bool status = true;
                            if (hfStatus.Value != "Active")
                            {
                                status = false;
                            }

                            cController.UpdateCustomer(Convert.ToInt64(hfCustomerID.Value), status, Constants.IntNullValue, txtContact.Text, txtEmail.Text, txtCardNo.Text
                                , txtName.Text, txtAddress.Text, Convert.ToDateTime(txtDOB.Text), txtCNIC.Text, null, txtNature.Text, Convert.ToDecimal(dc.chkNull_0(txtOpeningAmount.Text))
                                , txtContact2.Text, Convert.ToInt32(drpCardType.SelectedItem.Value), Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)), Convert.ToDecimal(dc.chkNull_0(txtPurchasing.Text))
                                , Convert.ToDecimal(dc.chkNull_0(txtPoints.Text)), Convert.ToDecimal(dc.chkNull_0(txtAmountLimit.Text)));

                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully.');", true);
                            mPopUpLocation.Hide();
                            flag = true;
                        }
                        else {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Card No already exist.');", true);
                            flag = false;
                            mPopUpLocation.Show();

                        }
                        break;
                }
                if (flag)
                {
                    ClearAll();
                    LoadGrid("");
                }
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
    

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid("filter");

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        bool check = false;
        try
        {
            foreach (GridViewRow dr2 in GrdCustomer.Rows)
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
            UserController UserCntrl = new UserController();

            bool flag = false;
            foreach (GridViewRow dr in GrdCustomer.Rows)
            {
                var chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");


                if (chRelized.Checked)
                {
                    if (Convert.ToString(dr.Cells[10].Text) == "Active")
                    {
                        UserCntrl.ActiveInactive(false,Convert.ToInt32(dr.Cells[1].Text), Constants.IntNullValue, 4);

                        flag = true;
                    }
                    else
                    {
                        UserCntrl.ActiveInactive(true, Convert.ToInt32(dr.Cells[1].Text), Constants.IntNullValue, 4);
                        flag = true;
                    }
                }
                if (flag)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record updated successfully');", true);
                }
                this.LoadGrid("");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnSave.Text = "Save";
        myModalLabel.InnerText = "Add New Customer";
        mPopUpLocation.Show();
    }

    #endregion

    private void ClearAll()
    {
        txtCode.Text = "";
        txtName.Text = "";
        txtContact.Text = "";
        txtContact2.Text = "";
        txtAddress.Text = "";
        txtEmail.Text = "";
        txtCNIC.Text = "";
        txtOpeningAmount.Text = "";
        txtNature.Text = "";
        txtDOB.Text = "";
        lblErrorMsg.Text = "";
        btnSave.Text = "Save";
        myModalLabel.InnerText = "Add New Customer";
        txtCardNo.Text = "";
        LoadCardType();
        LoadCardInfo();

        txtCardNo.Enabled = true;
        txtAmountLimit.Enabled = true;
        drpCardType.Enabled = true;
        txtDiscount.Enabled = true;
        txtPurchasing.Enabled = true;
        txtPoints.Enabled = true;
    }
   
    #region Grid Operations

    protected void GrdCustomer_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            hfCustomerID.Value = GrdCustomer.Rows[e.NewEditIndex].Cells[1].Text;
            txtCode.Text = GrdCustomer.Rows[e.NewEditIndex].Cells[2].Text;
            txtName.Text = GrdCustomer.Rows[e.NewEditIndex].Cells[3].Text.Replace("&nbsp;", "");
            txtContact.Text = GrdCustomer.Rows[e.NewEditIndex].Cells[4].Text.Replace("&nbsp;", "");
            txtContact2.Text = GrdCustomer.Rows[e.NewEditIndex].Cells[7].Text.Replace("&nbsp;", "");
            txtEmail.Text = GrdCustomer.Rows[e.NewEditIndex].Cells[5].Text.Replace("&nbsp;", "");
            txtAddress.Text = GrdCustomer.Rows[e.NewEditIndex].Cells[6].Text.Replace("&nbsp;", "");
            txtDOB.Text = GrdCustomer.Rows[e.NewEditIndex].Cells[8].Text.Replace("&nbsp;", "");
            txtOpeningAmount.Text = GrdCustomer.Rows[e.NewEditIndex].Cells[12].Text.Replace("&nbsp;", "");
            hfStatus.Value = GrdCustomer.Rows[e.NewEditIndex].Cells[10].Text;
            txtNature.Text = GrdCustomer.Rows[e.NewEditIndex].Cells[11].Text.Replace("&nbsp;", "");
            txtCNIC.Text = GrdCustomer.Rows[e.NewEditIndex].Cells[13].Text.Replace("&nbsp;", "");
            try
            {
                ddDistributorId.Value = GrdCustomer.Rows[e.NewEditIndex].Cells[20].Text;
                
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('Assigned location is not exist');", true);
            }
            LoadCardType();
            drpCardType.Value = GrdCustomer.Rows[e.NewEditIndex].Cells[19].Text.Replace("&nbsp;","0");
           
            ShowHideCardInfo();

            //if (drpCardType.Value.ToString() == "0")
            //{
            //    txtDiscount.Text = "0";
            //    txtPurchasing.Text = "0";
            //    txtPoints.Text = "0";
            //    txtAmountLimit.Text = "0";
            //    txtCardNo.Text = "";
            //}
            //else
            //{
                txtDiscount.Text = GrdCustomer.Rows[e.NewEditIndex].Cells[14].Text;
                txtPurchasing.Text = GrdCustomer.Rows[e.NewEditIndex].Cells[15].Text;
                txtPoints.Text = GrdCustomer.Rows[e.NewEditIndex].Cells[16].Text;
                txtAmountLimit.Text = GrdCustomer.Rows[e.NewEditIndex].Cells[17].Text;
                txtCardNo.Text = GrdCustomer.Rows[e.NewEditIndex].Cells[18].Text.Replace("&nbsp;", "0");
           // }

            if (_mUController.IsExist(txtCardNo.Text, Convert.ToInt32(ddDistributorId.Value), Constants.IntNullValue, 4))
            {
                txtCardNo.Enabled = false;
                txtAmountLimit.Enabled = false;
                drpCardType.Enabled = false;
                txtDiscount.Enabled = false;
                txtPurchasing.Enabled = false;
                txtPoints.Enabled = false;
            }

            btnSave.Text = "Update";
            myModalLabel.InnerText = "Edit Customer Information";
            mPopUpLocation.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('"+ ex +"');", true);
            
        }
    }

    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdCustomer.PageIndex = e.NewPageIndex;
        LoadGrid("");
    }

    #endregion
}
