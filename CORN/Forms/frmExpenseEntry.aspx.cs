using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;
/// <summary>
/// Form To Add, Edit Branch Expense And Salary
/// </summary>
public partial class frmExpenseEntry : System.Web.UI.Page
{
    LedgerController mLController = new LedgerController();
    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
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
            this.LoadDistributor();

            this.LoadAccountHead();
            this.CreatTable();
            this.LoadVoucherNo();
            //LoadClosingBal();
            btnAddNew.Attributes.Add("onclick", "return ValidateForm();");
            ScriptManager.GetCurrent(Page).SetFocus(drpDistributor);
            lblRowId.Text = "-1";
            txtMainCash.Attributes.Add("readonly", "readonly");

            DataTable dtConfig = GetCOAConfiguration();
            bool IsFinanceSetting = GetFinanceConfig();
            Session.Add("dtConfig", dtConfig);
            Session.Add("IsFinanceSetting", IsFinanceSetting);
        }
    }

    private void LoadClosingBal()
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.Value.ToString())
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }
        DataTable dt = mLController.SelectPetyCashSummary(int.Parse(drpDistributor.SelectedItem.Value.ToString()), Convert.ToDateTime("2000-12-12" + " 00:00:00"), CurrentWorkDate, int.Parse(this.Session["UserId"].ToString()));
        if (dt.Rows.Count > 0)
        {
            decimal dm = Convert.ToDecimal(dt.Rows[0]["Opening_Balance"]);
            dm += Convert.ToDecimal(dt.Rows[0]["Received"]);
            decimal ex = Convert.ToDecimal(dt.Rows[0]["Expenese_Out"]);
            txtClosinBal.Text = Convert.ToString(dm - ex);
        }

    }
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.GetDistributorWithMaxDayClose(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()),1);
        clsWebFormUtil.FillDxComboBoxList(drpDistributor, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");

        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedIndex = 0;
        }
        Session.Add("dtLocationInfo", dt);
    }

    /// <summary>
    /// Loads Account Heads To AccountHead Combo
    /// </summary>
    private void LoadAccountHead()
    {

        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.GetAssignAccountHead(Constants.IntNullValue, Constants.IntNullValue);
        clsWebFormUtil.FillDxComboBoxList(DrpAccountHead, dt, 0, 10, true);
        
        if (dt.Rows.Count > 0)
        {
            DrpAccountHead.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Creates Datatable For Expense And Salary
    /// </summary>
    private void CreatTable()
    {
        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Account_Code", typeof(string));
        dtVoucher.Columns.Add("Account_Name", typeof(string));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));
        dtVoucher.Columns.Add("Principal", typeof(string));
        dtVoucher.Columns.Add("ACCOUNT_PARENT_ID", typeof(string));
        this.Session.Add("dtVoucher", dtVoucher);

    }

    /// <summary>
    /// Loads Voucher Nos To Voucher No Combo
    /// </summary>
    private void LoadVoucherNo()
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.Value.ToString())
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }
        DrpVoucherNo.Items.Clear();
        LedgerController LController = new LedgerController();
        DataTable dt = LController.SelectVoucherNo(CurrentWorkDate, int.Parse(drpDistributor.SelectedItem.Value.ToString()), Constants.Journal_Voucher, int.Parse(this.Session["UserId"].ToString()), null,Constants.Document_Petty_Cash);
        DrpVoucherNo.Items.Add(new DevExpress.Web.ListEditItem("New", "0"));
        clsWebFormUtil.FillDxComboBoxList(DrpVoucherNo, dt, 3, 3);
        DrpVoucherNo.SelectedIndex = 0;
    }

    /// <summary>
    /// Deletes Expense And Salary
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dtVoucher = (DataTable)this.Session["dtVoucher"];
        dtVoucher.Rows.RemoveAt(e.RowIndex);
        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        lblRowId.Text = "-1";

        decimal TotalDebit = 0;

        foreach (DataRow dr in dtVoucher.Rows)
        {
            TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
        }
        txtMainCash.Text = TotalDebit.ToString();

    }

    /// <summary>
    /// Sets Expense And Salary Data For Edit. This Function Runs When An Existing Expense And Salary Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblRowId.Text = e.NewEditIndex.ToString();
        this.LoadAccountHead();
        DrpAccountHead.Value = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;
        txtRemarks.Text = GrdOrder.Rows[e.NewEditIndex].Cells[2].Text;
        txtAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;
    }

    /// <summary>
    /// Adds Record To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        DataControl dc = new DataControl();
        DataTable dtVoucher = (DataTable)this.Session["dtVoucher"];
        if (lblRowId.Text == "-1")
        {
            DataRow dr = dtVoucher.NewRow();
            dr["ACCOUNT_HEAD_ID"] = DrpAccountHead.SelectedItem.Value.ToString();
            dr["ACCOUNT_NAME"] = DrpAccountHead.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtAmount.Text));
            dr["Credit"] = "0";
            dr["REMARKS"] = txtRemarks.Text;
            dr["Principal"] = null;
            dr["Principal_id"] = 0;
            dr["ACCOUNT_PARENT_ID"] = 61;//Branch Expenses  (FDM EXP)
            dtVoucher.Rows.Add(dr);

        }
        else
        {
            DataRow dr = dtVoucher.Rows[int.Parse(lblRowId.Text)];
            dr["ACCOUNT_HEAD_ID"] = DrpAccountHead.SelectedItem.Value.ToString();
            dr["ACCOUNT_NAME"] = DrpAccountHead.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtAmount.Text));
            dr["Credit"] = "0";
            dr["REMARKS"] = txtRemarks.Text;
            dr["Principal"] = null;
            dr["Principal_id"] = 0;
            dr["ACCOUNT_PARENT_ID"] = 61; //Branch Expenses  (FDM EXP)
        }
        
        decimal TotalDebit = 0;

        foreach (DataRow dr in dtVoucher.Rows)
        {
            TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
        }
        txtMainCash.Text = TotalDebit.ToString();
        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        txtAmount.Text = "";
        txtRemarks.Text = "";
        lblRowId.Text = "-1";
        ScriptManager.GetCurrent(Page).SetFocus(DrpAccountHead);
    }

    /// <summary>
    /// Save Or Updates Expenses And Salary
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach(DataRow dr in dtLocationInfo.Rows)
        {
            if(dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.Value.ToString())
            {
                if(dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }
        if (CurrentWorkDate != Constants.DateNullValue)
        {
            DataControl dc = new DataControl();
            DataTable dtVoucher = (DataTable)this.Session["dtVoucher"];
            bool IsSaveVoucher;
            DataRow[] drConfig = null;
            DataTable dtConfig = (DataTable)Session["dtConfig"];
            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.PettyExpense + "'");
            int PettyExpense = Convert.ToInt32(drConfig[0]["VALUE"].ToString());
            string MaxDocumentId;

            if (dtVoucher.Rows.Count > 0)
            {
                DataRow dr = dtVoucher.NewRow();
                dr["ACCOUNT_HEAD_ID"] = PettyExpense;
                dr["ACCOUNT_NAME"] = "Petty Cash";
                dr["DEBIT"] = 0;
                dr["Credit"] = decimal.Parse(dc.chkNull_0(txtMainCash.Text));
                dr["REMARKS"] = "Petty Cash Credit";
                dr["Principal"] = null;
                dr["Principal_id"] = 0;
                dtVoucher.Rows.Add(dr);
                if (DrpVoucherNo.SelectedItem.Value.ToString() == "0")
                {
                    MaxDocumentId = mLController.SelectMaxVoucherId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedItem.Value.ToString()), CurrentWorkDate);
                    IsSaveVoucher = mLController.Add_Voucher(int.Parse(drpDistributor.SelectedItem.Value.ToString()), Constants.IntNullValue, MaxDocumentId, Constants.Journal_Voucher,
                           CurrentWorkDate, Constants.Document_Petty_Cash, null, "Petty Voucher", Constants.DateNullValue, null, dtVoucher, int.Parse(this.Session["UserId"].ToString()), txtslipNo.Text);
                }
                else
                {
                    MaxDocumentId = DrpVoucherNo.SelectedItem.Value.ToString();
                    IsSaveVoucher = mLController.Add_Voucher(int.Parse(drpDistributor.SelectedItem.Value.ToString()), Constants.IntNullValue, MaxDocumentId, Constants.Journal_Voucher,
                           CurrentWorkDate, Constants.Document_Petty_Cash, null, "Petty Voucher", Constants.DateNullValue, null, dtVoucher, int.Parse(this.Session["UserId"].ToString()), txtslipNo.Text);
                }
                if (IsSaveVoucher == true)
                {
                    dtVoucher.Rows.Clear();
                    GrdOrder.DataSource = null;
                    GrdOrder.DataBind();
                    txtAmount.Text = "";
                    txtslipNo.Text = "";
                    txtRemarks.Text = "";
                    lblRowId.Text = "-1";
                    ScriptManager.GetCurrent(Page).SetFocus(DrpAccountHead);
                    decimal TotalDebit = 0;
                    foreach (DataRow dr1 in dtVoucher.Rows)
                    {
                        TotalDebit += decimal.Parse(dr1["DEBIT"].ToString());
                    }
                    txtMainCash.Text = TotalDebit.ToString();
                    this.LoadVoucherNo();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error in Voucher')", true);
                    return;
                }
            }
            else if (lblRowId.Text == "-1")
            {
                MaxDocumentId = DrpVoucherNo.SelectedItem.Value.ToString();
                IsSaveVoucher = mLController.Delete_Voucher(int.Parse(drpDistributor.SelectedItem.Value.ToString()), MaxDocumentId, Constants.Journal_Voucher);
                Session.Remove("dtVoucher");
                LoadVoucherNo();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Dayclose not found for selected location!')", true);
        }
    }

    /// <summary>
    /// Loads Expense And Salary Data To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpVoucherNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtVoucher = (DataTable)this.Session["dtVoucher"];

        if (DrpVoucherNo.SelectedItem.Value.ToString() != "0")
        {
            dtVoucher = mLController.SelectVoucherDetail(int.Parse(drpDistributor.SelectedItem.Value.ToString()), DrpVoucherNo.SelectedItem.Value.ToString(), Constants.Journal_Voucher);
            for (int i = 0; i < dtVoucher.Rows.Count; i++)
            {
                if (Decimal.Parse(dtVoucher.Rows[i]["CREDIT"].ToString()) > 0)
                {
                    dtVoucher.Rows.RemoveAt(i);
                }
            }
            this.Session.Add("dtVoucher", dtVoucher);
            GrdOrder.DataSource = dtVoucher;
            GrdOrder.DataBind();
        }
        else
        {
            dtVoucher.Rows.Clear();
            GrdOrder.DataSource = null;
            GrdOrder.DataBind();

        }
        lblRowId.Text = "0";
        this.Session.Add("dtVoucher", dtVoucher);

    }

    /// <summary>
    /// Loads Account Heads
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadAccountHead();

    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {

        //LoadClosingBal();
    }

    protected void GrdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total";
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[3].Text = Convert.ToDecimal(txtMainCash.Text).ToString("N");
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[6].Visible = false;
        }
    }

    private DataTable GetCOAConfiguration()
    {
        try
        {
            COAMappingController _cController = new COAMappingController();
            DataTable dt = _cController.SelectCOAConfiguration(5, Constants.ShortNullValue, Constants.LongNullValue, "Level 4");
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
            return null;
        }
    }

    private bool GetFinanceConfig()
    {
        try
        {
            ConfigurationController _cController = new ConfigurationController();
            DataTable dt = _cController.SelectAppConfiguration(1, (int)Enums.AppSettingMaster.FinanceSetting, null, Constants.IntNullValue);

            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("CODE = '" + (int)Enums.AppSetting.IsFinanceIntegrate + "'");
                if (dr.Length > 0)
                {
                    return Convert.ToInt32(dr[0][2]) == 1 ? true : false;
                }
            }
            return false;
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Error in Financial Setting!');", true);
            throw;
        }
    }
}