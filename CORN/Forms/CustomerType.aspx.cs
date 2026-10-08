using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// Form To Add, Edit, Delete Busines Type
/// </summary>
public partial class Forms_CustomerType : System.Web.UI.Page
{
    private static int RefId;

    EmployeController _empCtrl = new EmployeController();

    /// <summary>
    /// Page_Load Function Populates All Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            LoadChannelType();
            LoadBusinessType();
           // this.LoadVolumClass();
            ResetAllowances();
            ResetDeductions();
        }
    }

    #region Designation Tab

    /// <summary>
    /// Loads Channel Types To Channel Type Grid
    /// </summary>
    private void LoadChannelType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.Employee_Designation_Id, null, Constants.IntNullValue, bool.Parse("True"));
        grdChannelData.DataSource = dt.DefaultView;
        grdChannelData.DataBind();
     }

    

    /// <summary>
    /// Sets Channel Type For Edit. This Function Runs When An Existing Channel Type Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void grdChannelData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RefId = int.Parse(grdChannelData.Rows[e.NewEditIndex].Cells[0].Text);
        txtChannelCode.Text = grdChannelData.Rows[e.NewEditIndex].Cells[1].Text;
        txtChannelName.Text = grdChannelData.Rows[e.NewEditIndex].Cells[2].Text;
        btnSaveChannelType.Text = "Update";
        txtChannelName.Enabled = true;
    }

    /// <summary>
    /// Deletes Channel Type
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void grdChannelData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SLASHCodesController mController = new SLASHCodesController();

        RefId = int.Parse(grdChannelData.Rows[e.RowIndex].Cells[0].Text);
        mController.UpdateSlashCodes(RefId, null, Constants.Employee_Designation_Id, null, 1, false, Constants.DateNullValue);

        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record removed successfully.');", true);

        this.LoadChannelType();

    }
    
    /// <summary>
    /// Save Or Updates a Channel Type
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveChannelType_Click(object sender, EventArgs e)
    {
        SLASHCodesController mController = new SLASHCodesController();
        lblErrorMsg.Visible = false;
        lblErrorMsg.Text = "";
        if (btnSaveChannelType.Text == "New")
        {
            txtChannelCode.Text = this.GetAutoCode("DSG", 0, Constants.LongNullValue);
            txtChannelName.Enabled = true;
            txtChannelName.Focus();
            btnSaveChannelType.Text = "Save";
            ScriptManager.GetCurrent(Page).SetFocus(txtChannelName);
        }
        else if (btnSaveChannelType.Text == "Save")
        {
            if (txtChannelName.Text.Length == 0)
            {
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = Utility.ShowAlert(false, "Designation name is required");
                return;
            }
            mController.InsertSlashCodes(txtChannelCode.Text, Constants.Employee_Designation_Id, txtChannelName.Text, 1, true);
            this.GetAutoCode("DSG", 1, long.Parse(txtChannelCode.Text.Substring(3)));
            this.LoadChannelType();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Record added successfully.');", true);
            txtChannelCode.Text = "";
            txtChannelName.Text = "";
            txtChannelName.Enabled = false;
            btnSaveChannelType.Text = "New";
            
           
        }
        else if (btnSaveChannelType.Text == "Update")
        {
            mController.UpdateSlashCodes(RefId, null, Constants.Employee_Designation_Id, txtChannelName.Text, 1, true, Constants.DateNullValue);
            this.LoadChannelType();
            txtChannelCode.Text = "";
            txtChannelName.Text = "";
            txtChannelName.Enabled = false;
            btnSaveChannelType.Text = "New";

            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record updated successfully.');", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e) 
    {
       
        txtChannelCode.Text = "";
        txtChannelName.Text = "";
        txtChannelName.Enabled = false;
        btnSaveChannelType.Text = "New";
        lblErrorMsg.Text = "";
        lblErrorMsg.Visible = false;
    }

    #endregion

    #region Department Type Tab

    /// <summary>
    /// Loads Business Types To Business Type Grid
    /// </summary>
    private void LoadBusinessType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.Employee_Depoartment_Id, null, Constants.IntNullValue, bool.Parse("True"));
        GrdBusType.DataSource = dt.DefaultView;
        GrdBusType.DataBind();
    }

    /// <summary>
    /// Sets PageIndex Of Business Type Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GrdBusType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdBusType.PageIndex = e.NewPageIndex;
        this.LoadBusinessType();
    }

    /// <summary>
    /// Sets Business Type For Edit. This Function Runs When An Existing Business Type Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdBusType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RefId = int.Parse(GrdBusType.Rows[e.NewEditIndex].Cells[0].Text);
        txtbustypeCode.Text = GrdBusType.Rows[e.NewEditIndex].Cells[1].Text;
        txtbustypeName.Text = GrdBusType.Rows[e.NewEditIndex].Cells[2].Text;
        txtbustypeName.Enabled = true;
        btnSaveBusType.Text = "Update";

    }

    /// <summary>
    /// Deletes Business Type
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdBusType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SLASHCodesController mController = new SLASHCodesController();
        RefId = int.Parse(GrdBusType.Rows[e.RowIndex].Cells[0].Text);
        mController.UpdateSlashCodes(RefId, null, Constants.Employee_Depoartment_Id, null, 1, false, Constants.DateNullValue);

        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record removed successfully.');", true);

        this.LoadBusinessType();
    }

    /// <summary>
    /// Save Or Updates a Business Type
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveBusType_Click(object sender, EventArgs e)
    {
        SLASHCodesController mController = new SLASHCodesController();
        lblErrorMsgDivsion.Visible = false;
        lblErrorMsgDivsion.Text = "";
        if (btnSaveBusType.Text == "New")
        {
            txtbustypeCode.Text = this.GetAutoCode("DPT", 0, Constants.LongNullValue);
            txtbustypeName.Enabled = true;
            txtbustypeName.Focus();
            btnSaveBusType.Text = "Save";
            ScriptManager.GetCurrent(Page).SetFocus(txtbustypeName);
        }
        else if (btnSaveBusType.Text == "Save")
        {
            if (txtbustypeName.Text.Length == 0)
            {
                lblErrorMsgDivsion.Visible = true;
                lblErrorMsgDivsion.Text = Utility.ShowAlert(false, "Department name is required");
                return;
            }
            mController.InsertSlashCodes(txtbustypeCode.Text, Constants.Employee_Depoartment_Id, txtbustypeName.Text, 1, true);
            this.GetAutoCode("DPT", 1, long.Parse(txtbustypeCode.Text.Substring(3)));
            this.LoadBusinessType();

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Record added successfully.');", true);

            txtbustypeCode.Text = "";
            txtbustypeName.Text = "";
            txtbustypeName.Enabled = false;
            btnSaveBusType.Text = "New";
          
        }
        else if (btnSaveBusType.Text == "Update")
        {
            mController.UpdateSlashCodes(RefId, null, Constants.Employee_Depoartment_Id, txtbustypeName.Text, 1, true, Constants.DateNullValue);
            this.LoadBusinessType();
            txtbustypeCode.Text = "";
            txtbustypeName.Text = "";
            txtbustypeName.Enabled = false;
            btnSaveBusType.Text = "New";
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record updated successfully.');", true);
        }
    }

    protected void btncancelDestype_Click(object sender, EventArgs e)
    {
        
        txtbustypeCode.Text = "";
        txtbustypeName.Text="";
        txtbustypeName.Enabled = false;
        btnSaveBusType.Text = "New";
        lblErrorMsg.Text = "";
        lblErrorMsgDivsion.Visible = false;
    }
    
    #endregion


    #region Allowances Tab

    protected void GridAllowances()
    {
        DataTable dt = _empCtrl.SelectAllowances(Constants.IntNullValue, Int32.Parse(Session["CompanyId"].ToString()));
        GridAllowance.DataSource = dt;
        GridAllowance.DataBind();
    }
    protected void InsertUpdateAllowances()
    {
        try
        {
            int ratioType = 2;//for value
            if (rdbPercentage.Checked == true)
            {
                ratioType = 1;// for Percentage
            }
            if (hdnAllowanceID.Value == "" || hdnAllowanceID.Value == null)
            {
                _empCtrl.InsertAllowances(Int32.Parse(Session["CompanyId"].ToString()), txtAllowanceDescription.Text, Decimal.Parse(txtAllowanceRatio.Text), 2, Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            }
            else
            {
                _empCtrl.UpdateAllowances(Int32.Parse(hdnAllowanceID.Value), Int32.Parse(Session["CompanyId"].ToString()), txtAllowanceDescription.Text, Decimal.Parse(txtAllowanceRatio.Text), 2, false, true, Int32.Parse(Session["UserID"].ToString()));
            }
            ResetAllowances();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error');", true);
        }
    }

    protected void GridAllowance_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RefId = int.Parse(GridAllowance.Rows[e.NewEditIndex].Cells[0].Text);
        txtAllowanceDescription.Text = GridAllowance.Rows[e.NewEditIndex].Cells[1].Text;
        txtAllowanceRatio.Text = GridAllowance.Rows[e.NewEditIndex].Cells[2].Text;

        if (GridAllowance.Rows[e.NewEditIndex].Cells[3].Text == "1")
        {
            rdbValue.Checked = true;           
        }
        else
        {
            rdbPercentage.Checked = true;
        }

        hdnAllowanceID.Value = GridAllowance.Rows[e.NewEditIndex].Cells[0].Text;
        btnSaveAllowance.Text = "Update";

        pnlAllowancesContent.Visible = true;
        pnlAllowancesGrid.Visible = false;
    }
    //protected void rptAllowances_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    DataTable dt = _empCtrl.SelectAllowances(Int32.Parse(e.CommandArgument.ToString()), Int32.Parse(Session["CompanyId"].ToString()));
    //    txtAllowanceDescription.Text = dt.Rows[0]["AllowanceDescription"].ToString();
    //    txtAllowanceRatio.Text = dt.Rows[0]["AllowanceRatio"].ToString();
    //    pnlAllowancesContent.Visible = true;
    //    pnlAllowancesGrid.Visible = false;
    //    if (dt.Rows[0]["RatioType"].ToString() == "1")
    //    {
    //        rdbPercentage.Checked = true;
    //    }
    //    else
    //    {
    //        rdbValue.Checked = true;
    //    }
    //    hdnAllowanceID.Value = dt.Rows[0]["AllowanceID"].ToString();
    //    btnSaveAllowance.Text = "Update";
    //}
    protected void ResetAllowances()
    {
        hdnAllowanceID.Value = "";
        txtAllowanceRatio.Text = "";
        txtAllowanceDescription.Text = "";
        rdbPercentage.Checked = false;
        rdbValue.Checked = true;
        pnlAllowancesContent.Visible = false;
        pnlAllowancesGrid.Visible = true;
        GridAllowances();
        btnSaveAllowance.Text = "Save";
    }
    protected void btnSaveAllowance_Click(object sender, EventArgs e)
    {
        try
        {
            InsertUpdateAllowances();
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error');", true);
        }
    }
    protected void btnDiscardAllowance_Click(object sender, EventArgs e)
    {
        try
        {
            ResetAllowances();
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error');", true);
        }
    }
    protected void btnShowAllowancesContent_Click(object sender, EventArgs e)
    {
        pnlAllowancesContent.Visible = true;
        pnlAllowancesGrid.Visible = false;
    }

    #endregion

    #region Deduction Tab

    protected void GridLoadDeductions()
    {
        DataTable dt = _empCtrl.SelectDeductions(Constants.IntNullValue, Int32.Parse(Session["CompanyId"].ToString()));
        GridDeduction.DataSource = dt;
        GridDeduction.DataBind();
    }
    protected void InsertUpdateDeductions()
    {
        try
        {
            int ratioType = 2;//for value
            if (rdbDeductionPercentage.Checked == true)
            {
                ratioType = 1;// for Percentage
            }
            if (hdnDeductionID.Value == "" || hdnDeductionID.Value == null)
            {
                _empCtrl.InsertDeductions(Int32.Parse(Session["CompanyId"].ToString()), txtDeductionDescription.Text, Decimal.Parse(txtDeductionRatio.Text), 2, Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            }
            else
            {
                _empCtrl.UpdateDeductions(Int32.Parse(hdnDeductionID.Value), Int32.Parse(Session["CompanyId"].ToString()), txtDeductionDescription.Text, Decimal.Parse(txtDeductionRatio.Text), 2, false, true, Int32.Parse(Session["UserID"].ToString()));
            }
            ResetDeductions();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error');", true);
        }
    }
    protected void GridDeduction_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RefId = int.Parse(GridAllowance.Rows[e.NewEditIndex].Cells[0].Text);
        txtDeductionDescription.Text = GridAllowance.Rows[e.NewEditIndex].Cells[1].Text;
        txtDeductionRatio.Text = GridAllowance.Rows[e.NewEditIndex].Cells[2].Text;

        if (GridAllowance.Rows[e.NewEditIndex].Cells[3].Text == "1")
        {
            rdbDeductionValue.Checked = false;
            rdbDeductionPercentage.Checked = true;
        }
        else
        {
            rdbDeductionPercentage.Checked = false;
            rdbDeductionValue.Checked = true;
        }
        hdnDeductionID.Value = GridAllowance.Rows[e.NewEditIndex].Cells[0].Text;

        pnlDeductionGrid.Visible = false;
        pnlDeductionContent.Visible = true;
        btnSaveDeduction.Text = "Update";
    }
    //protected void rptDeductions_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    DataTable dt = _empCtrl.SelectDeductions(Int32.Parse(e.CommandArgument.ToString()), Int32.Parse(Session["CompanyId"].ToString()));
    //    txtDeductionDescription.Text = dt.Rows[0]["DeductionDescription"].ToString();
    //    txtDeductionRatio.Text = dt.Rows[0]["DeductionRatio"].ToString();
    //    if (dt.Rows[0]["RatioType"].ToString() == "1")
    //    {
    //        rdbDeductionValue.Checked = false;
    //        rdbDeductionPercentage.Checked = true;
    //    }
    //    else
    //    {
    //        rdbDeductionPercentage.Checked = false;
    //        rdbDeductionValue.Checked = true;
    //    }
    //    hdnDeductionID.Value = dt.Rows[0]["DeductionID"].ToString();
       
    //    pnlDeductionGrid.Visible = false;
    //    pnlDeductionContent.Visible = true;
    //    btnSaveDeduction.Text = "Update";
    //}
    protected void ResetDeductions()
    {
        hdnDeductionID.Value = "";
        txtDeductionRatio.Text = "";
        txtDeductionDescription.Text = "";
        rdbDeductionPercentage.Checked = false;
        rdbDeductionValue.Checked = true;
        pnlDeductionGrid.Visible = true;
        pnlDeductionContent.Visible = false;
        GridLoadDeductions();
        btnSaveDeduction.Text = "Save";
    }
    protected void btnSaveDeduction_Click(object sender, EventArgs e)
    {
        try
        {
            InsertUpdateDeductions();
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error');", true);
        }
    }
    protected void btnDiscardDeduction_Click(object sender, EventArgs e)
    {
        try
        {
            ResetDeductions();
        }
        catch
        {
        }
    }
    protected void btnShowDeductionContent_Click(object sender, EventArgs e)
    {
        pnlDeductionGrid.Visible = false;
        pnlDeductionContent.Visible = true;
    }

    #endregion


    private string GetAutoCode(string PreeFix, int CodeType,long CValue)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        return AutoCode.GetAutoCode(PreeFix,CodeType,CValue);
    }
    protected void grdChannelData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdChannelData.PageIndex = e.NewPageIndex;
        LoadChannelType();
    }


}
