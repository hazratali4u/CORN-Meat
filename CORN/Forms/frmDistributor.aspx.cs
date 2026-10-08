using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;
using System.IO;

public partial class Forms_frmDistributor : System.Web.UI.Page
{
    readonly DistributorController mController = new DistributorController();
    readonly UserController _UserCtrl = new UserController();

    readonly DataControl dc = new DataControl();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            GetDistributorType();
            LoadGrid("");
            LoadCompany();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
            //  btnSave.Attributes.Add("onclick", "HideLabel()");


            txtsmsDelivery.Attributes.Add("disabled", "disabled");
            txtsmsTakeAway.Attributes.Add("disabled", "disabled");
            txtsmsDayClose.Attributes.Add("disabled", "disabled");

        }
    }

    private void LoadCompany()
    {
        CompanyController mCompany = new CompanyController();
        DataTable dt = mCompany.SelectCompany(Constants.IntNullValue, Constants.IntNullValue);
        clsWebFormUtil.FillDxComboBoxList(DrpCompanyName, dt, 0, 1, true);
        DrpCompanyName.SelectedIndex = 0;
    }

    private void GetDistributorType()
    {
        DataTable dt = mController.SelectDistributorTypeInfo(Constants.IntNullValue);
        clsWebFormUtil.FillDxComboBoxList(ddDistributorType, dt, 0, 2);
        ddDistributorType.SelectedIndex = 0;
    }

    private void LoadGrid(string pType)
    {

        GridDistributor.DataSource = null;
        GridDistributor.DataBind();

        DataTable dt = new DataTable();

        dt = mController.SelectAllDistributors(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);

        if (pType == "")
        {
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)//In case after  Filter
            {
                if (txtSearch.Text != "" || txtSearch.Text != string.Empty)

                    dt.DefaultView.RowFilter = "DISTRIBUTOR_NAME LIKE '%" + txtSearch.Text + "%'  OR TYPENAME LIKE '%" + txtSearch.Text + "%' OR ADDRESS1 LIKE '%" + txtSearch.Text + "%' OR CONTACT_PERSON LIKE '%" + txtSearch.Text + "%' OR CONTACT_NUMBER LIKE '%" + txtSearch.Text + "%' OR GST_NUMBER LIKE '%" + txtSearch.Text + "%' OR ISDELETED LIKE '" + txtSearch.Text + " %'";
            }
            GridDistributor.DataSource = dt;
            GridDistributor.DataBind();
        }
        else
        {
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty)
            {
                dt.DefaultView.RowFilter = "DISTRIBUTOR_NAME LIKE '%" + txtSearch.Text + "%'  OR TYPENAME LIKE '%" + txtSearch.Text + "%' OR ADDRESS1 LIKE '%" + txtSearch.Text + "%' OR CONTACT_PERSON LIKE '%" + txtSearch.Text + "%' OR CONTACT_NUMBER LIKE '%" + txtSearch.Text + "%' OR GST_NUMBER LIKE '%" + txtSearch.Text + "%' OR ISDELETED LIKE '" + txtSearch.Text + " %'";
            }
            if (dt.Rows.Count > 0)
            {
                GridDistributor.PageIndex = 0;
            }
            GridDistributor.DataSource = dt;
            GridDistributor.DataBind();
        }
    }

    protected void chkService_CheckedChanged(object sender, EventArgs e)
    {
        if (chkService.Checked)
        {
            mPopUpLocation.Show();
            txtServiceTax.Enabled = false;
        }
        else
        {
            txtServiceTax.Enabled = true;
            mPopUpLocation.Show();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        mPopUpLocation.Show();

        if (chkService.Checked)
        {
            txtServiceTax.Text = "0";
        }

        int coverTable = 0;

        if (chkIsCoverTable.Checked)
        {
            coverTable = 1;
        }
        string fileName = null;
        if (fuPic.HasFile)
        {
            Session["haspic"] = 1;
            string path = Server.MapPath("~/Pics");
            string fExtension = "";
            FileInfo oFileInfo = new FileInfo(fuPic.PostedFile.FileName);
            fExtension = Path.GetExtension(fuPic.FileName);
            fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "pic" + fExtension;
            string fullFileName = path + "\\" + fileName;
            Session.Add("Pic", fileName);
            hfPic.Value = fileName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fuPic.PostedFile.SaveAs(fullFileName);
        }

        if (Convert.ToString(hfPic.Value) == "" && chkShowLogo.Checked)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Select Logo or Uncheck Show Logo.');", true);
            return;
        }
        if (btnSave.Text == "Save")
        {
            if (mController.InsertDistributor(int.Parse(DrpCompanyName.SelectedItem.Value.ToString()), coverTable, !chkIsActive.Checked, System.DateTime.Now, System.DateTime.Now, Constants.IntNullValue, Constants.IntNullValue
                , Constants.IntNullValue, Constants.IntNullValue, int.Parse(ddDistributorType.SelectedItem.Value.ToString()), txtcontactperson.Text.Trim(), txtPhoneNo.Text.Trim(), txtgstno.Text.Trim(),
               txtpassword.Text.Trim(), txtAddress1.Text.Trim(), txtEmailAddress.Text.Trim(), txtDistributorCode.Text.Trim(), txtDistributorName.Text.Trim(), txtFacebookAddress.Text.Trim(), cbRegistered.Checked, 1, int.Parse(this.Session["UserId"].ToString()), Convert.ToDecimal(dc.chkNull_0(txtServiceTax.Text.Trim())), chkService.Checked, short.Parse(rdbServiceType.SelectedValue), fileName, chkShowLogo.Checked, txtsmsDelivery.Text, txtsmsTakeAway.Text, txtsmsDayClose.Text, null, chksmsDelivery.Checked, chksmsTakeAway.Checked, chksmsDayClose.Checked) == null)
            {
                mPopUpLocation.Show();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Some error occurred.');", true);
                return;
            }


            // ltrlMsg.Text = Utility.ShowAlert(true, "Successfully saved.");

            mPopUpLocation.Show();

            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');", true);
        }
        else
        {
            if (mController.UpdateDistributor(int.Parse(DrpCompanyName.SelectedItem.Value.ToString()), coverTable, !chkIsActive.Checked, Constants.DateNullValue, System.DateTime.Now, Constants.IntNullValue, Constants.IntNullValue
             , Constants.IntNullValue, Constants.IntNullValue, int.Parse(ddDistributorType.SelectedItem.Value.ToString()), txtcontactperson.Text.Trim(), txtPhoneNo.Text.Trim(), txtgstno.Text.Trim(),
             txtpassword.Text.Trim(), txtAddress1.Text.Trim(), txtEmailAddress.Text.Trim(), int.Parse(hfDistributorID.Value), txtDistributorCode.Text.Trim(), txtDistributorName.Text.Trim(), txtFacebookAddress.Text.Trim(), cbRegistered.Checked, 1, int.Parse(this.Session["UserId"].ToString()), Convert.ToDecimal(dc.chkNull_0(txtServiceTax.Text.Trim())), chkService.Checked, short.Parse(rdbServiceType.SelectedValue), fileName, chkShowLogo.Checked, txtsmsDelivery.Text, txtsmsTakeAway.Text, txtsmsDayClose.Text, null, chksmsDelivery.Checked, chksmsTakeAway.Checked, chksmsDayClose.Checked) == null)
            {
                mPopUpLocation.Show();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Some error occurred.');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully.');", true);
            mPopUpLocation.Hide();
        }
        ClearAll();
        LoadGrid("");
        chkService_CheckedChanged(null, null);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mPopUpLocation.Show();

        ClearAll();


    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid("filter");
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ddDistributorType.SelectedIndex = 0;
        DrpCompanyName.SelectedIndex = 0;
        ClearAll();

    }



    protected void GridDistributor_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Session.Add("Pic", "");
        hfPic.Value = "";
        hfDistributorID.Value = GridDistributor.Rows[e.NewEditIndex].Cells[1].Text;
        DrpCompanyName.Value = GridDistributor.Rows[e.NewEditIndex].Cells[12].Text;
        cbRegistered.Checked = bool.Parse(GridDistributor.Rows[e.NewEditIndex].Cells[2].Text);
        ddDistributorType.Value = GridDistributor.Rows[e.NewEditIndex].Cells[3].Text;
        txtEmailAddress.Text = GridDistributor.Rows[e.NewEditIndex].Cells[4].Text.Replace("&nbsp;", "");
        txtDistributorCode.Text = GridDistributor.Rows[e.NewEditIndex].Cells[5].Text.Replace("&nbsp;", "");
        txtDistributorName.Text = GridDistributor.Rows[e.NewEditIndex].Cells[6].Text.Replace("&nbsp;", "");
        txtAddress1.Text = GridDistributor.Rows[e.NewEditIndex].Cells[8].Text.Replace("&nbsp;", "");
        txtcontactperson.Text = GridDistributor.Rows[e.NewEditIndex].Cells[9].Text.Replace("&nbsp;", "");
        txtPhoneNo.Text = GridDistributor.Rows[e.NewEditIndex].Cells[10].Text.Replace("&nbsp;", "");
        if (GridDistributor.Rows[e.NewEditIndex].Cells[13].Text == "Active")
        {
            chkIsActive.Checked = true;
        }
        else
        {
            chkIsActive.Checked = false;
        }
        if (GridDistributor.Rows[e.NewEditIndex].Cells[11].Text == "&nbsp;")
        {
            txtgstno.Text = "";
            cbRegistered.Checked = false;
            txtgstno.Enabled = false;
        }
        else
        {
            cbRegistered.Checked = true;
            txtgstno.Text = GridDistributor.Rows[e.NewEditIndex].Cells[11].Text.Replace("&nbsp;", "");
            txtgstno.Enabled = true;
        }
        txtServiceTax.Text = GridDistributor.Rows[e.NewEditIndex].Cells[15].Text.Replace("&nbsp;", "");

        if (GridDistributor.Rows[e.NewEditIndex].Cells[16].Text == "1")
        {
            chkIsCoverTable.Checked = true;
        }
        else
        {
            chkIsCoverTable.Checked = false;
        }

        chkService.Checked = Convert.ToBoolean(GridDistributor.Rows[e.NewEditIndex].Cells[17].Text);
        txtFacebookAddress.Text = GridDistributor.Rows[e.NewEditIndex].Cells[18].Text.Replace("&nbsp;", "");
        rdbServiceType.SelectedValue = GridDistributor.Rows[e.NewEditIndex].Cells[19].Text;
        hfPic.Value = GridDistributor.Rows[e.NewEditIndex].Cells[20].Text.Replace("&nbsp;", "");
        ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "MyJSFunction", "MyJSFunction2();", true);
        if (Convert.ToString(hfPic.Value) == "")
        {
            Session["haspic"] = 0;
        }
        else
        {
            Session["haspic"] = 1;
        }
        chkShowLogo.Checked = Convert.ToBoolean(GridDistributor.Rows[e.NewEditIndex].Cells[21].Text);
        chksmsDelivery.Checked = Convert.ToBoolean(GridDistributor.Rows[e.NewEditIndex].Cells[22].Text);
        txtsmsDelivery.Text = GridDistributor.Rows[e.NewEditIndex].Cells[23].Text.Replace("&nbsp;", "");
        chksmsTakeAway.Checked = Convert.ToBoolean(GridDistributor.Rows[e.NewEditIndex].Cells[24].Text);
        txtsmsTakeAway.Text = GridDistributor.Rows[e.NewEditIndex].Cells[25].Text.Replace("&nbsp;", "");
        chksmsDayClose.Checked = Convert.ToBoolean(GridDistributor.Rows[e.NewEditIndex].Cells[26].Text);
        txtsmsDayClose.Text = GridDistributor.Rows[e.NewEditIndex].Cells[27].Text.Replace("&nbsp;", "");
        txtSmsContact.Text = GridDistributor.Rows[e.NewEditIndex].Cells[28].Text.Replace("&nbsp;", "");

        txtsmsDelivery.Attributes.Remove("disabled");
        txtsmsTakeAway.Attributes.Remove("disabled");
        txtsmsDayClose.Attributes.Remove("disabled");

        if (!chksmsDelivery.Checked)
        {
            txtsmsDelivery.Attributes.Add("disabled", "disabled");
        }
        if (!chksmsTakeAway.Checked)
        {
            txtsmsTakeAway.Attributes.Add("disabled", "disabled");
        }

        if (!chksmsDayClose.Checked)
        {
            txtsmsDayClose.Attributes.Add("disabled", "disabled");
        }

        mPopUpLocation.Show();
        btnSave.Text = "Update";
    }

    
    private void ClearAll()
    {
        txtDistributorName.Text = "";
        txtDistributorCode.Text = "";
        txtAddress1.Text = "";
        txtEmailAddress.Text = "";
        txtcontactperson.Text = "";
        txtPhoneNo.Text = "";
        txtpassword.Text = "";
        txtgstno.Text = "";
        txtServiceTax.Text = "";
        txtSmsContact.Text = "";
        txtsmsDayClose.Text = "";
        txtsmsDelivery.Text = "";
        txtsmsTakeAway.Text = "";
        btnSave.Text = "Save";
        cbRegistered.Checked = false;
        chksmsDayClose.Checked = false;
        chksmsDelivery.Checked = false;
        chksmsTakeAway.Checked = false;
        txtgstno.Enabled = false;

        txtsmsDelivery.Attributes.Add("disabled", "disabled");
        txtsmsTakeAway.Attributes.Add("disabled", "disabled");
        txtsmsDayClose.Attributes.Add("disabled", "disabled");

        if (cbRegistered.Checked)
        {
            txtgstno.Enabled = true;
        }
        chkIsCoverTable.Checked = true;
        chkService.Checked = false;

        rdbServiceType.SelectedIndex = 0;
        txtFacebookAddress.Text = "";
        hfPic.Value = "";
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        //btnActive.OnClientClick = string.Format("return confirm('Are you sure you want to delete?');");
        bool check = false;
        try
        {
            foreach (GridViewRow dr2 in GridDistributor.Rows)
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

            foreach (GridViewRow dr in GridDistributor.Rows)
            {

                var chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");

                if (chRelized.Checked)
                {

                    if (Convert.ToString(dr.Cells[13].Text) == "Active")
                    {

                        _UserCtrl.ActiveInactive(false, Convert.ToInt32(dr.Cells[1].Text), int.Parse(DrpCompanyName.Value.ToString()), 8);

                        
                        flag = true;

                    }
                    else
                    {

                        _UserCtrl.ActiveInactive(true, Convert.ToInt32(dr.Cells[1].Text), int.Parse(DrpCompanyName.Value.ToString()), 8);

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
        mPopUpLocation.Show();
        ClearAll();
    }

    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridDistributor.PageIndex = e.NewPageIndex;
        LoadGrid("");
    }

}
