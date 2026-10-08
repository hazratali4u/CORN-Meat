using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;
using System.Web;
using System.IO;
using System.Data.OleDb;

public partial class Forms_frmVoucherEntry : System.Web.UI.Page
{
    DataTable dtVoucher;
    private static int RowId = -1;
    readonly BankController _bnkController = new BankController();
    readonly FinancialYearController _yearController = new FinancialYearController();
    readonly AccountHeadController mAccountController = new AccountHeadController();

    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            GetFinancialYear();
            //if (IsDayClosed())
            //{
            //    UserController UserCtl = new UserController();

            //    UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
            //    Session.Clear();
            //    System.Web.Security.FormsAuthentication.SignOut();
            //    Response.Redirect("../Login.aspx");
            //}

            this.LoadDistributor();
            this.LoadAccountDetail();
            this.CreatTable();
            this.LoadAccountHead();
            LoadCheque(2);
            this.HideShowControls();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            btnImport.Attributes.Add("onclick", "jQuery('#" + FileUpload1.ClientID + "').click();return false;");
            txtDueDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            txtTransactionDate.Text = ((DateTime)Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            txtTransactionDate.Attributes.Add("readonly", "readonly");
            RowId = -1;

            txtDebitAmount.Enabled = false;
            txtCreditAmount.Enabled = true;

            DrpPaymentMode.Items.Add(new DevExpress.Web.ListEditItem("Cash", "19"));
            DrpPaymentMode.Items.Add(new DevExpress.Web.ListEditItem("Online Transfer", "30"));
            DrpPaymentMode.SelectedIndex = 0;
        }
    }
    public void GetFinancialYear()
    {
        try
        {
            DataTable dt = _yearController.SelectFinancialYear(null, null, null, null, Constants.ShortNullValue, false, true, 1);
            DateTime StartDate = Convert.ToDateTime(dt.Rows[0]["dtStart"].ToString());
            DateTime EndDate = Convert.ToDateTime(dt.Rows[0]["dtEnd"].ToString());
            CalendarExtender1.StartDate = StartDate;
            CalendarExtender1.EndDate = EndDate;
        }
        catch (IndexOutOfRangeException)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Start a financial year')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }

    private void LoadAccountHead()
    {
        if (int.Parse(DrpVoucherType.SelectedItem.Value.ToString()) == 14)
        {
            DataTable dt = mAccountController.SelectAccountHeadByMapping(Constants.AC_AccountHeadId, Constants.LongNullValue, 2, Constants.AC_CashInHandAccountHead);
            clsWebFormUtil.FillDxComboBoxList(drpBanks, dt, 0, 3, true);

            if (dt.Rows.Count > 0)
            {
                drpBanks.SelectedIndex = 0;
            }
        }
        else if (int.Parse(DrpVoucherType.SelectedItem.Value.ToString()) == 15)
        {
            DataTable dt = mAccountController.SelectAccountHeadByMapping(Constants.AC_AccountHeadId, Constants.LongNullValue, 2, Constants.AC_BankAccountHead);
            clsWebFormUtil.FillDxComboBoxList(drpBanks, dt, 0, 3, true);

            if (dt.Rows.Count > 0)
            {
                drpBanks.SelectedIndex = 0;
            }
            drpBanks_SelectedIndexChanged(null, null);
        }
        else
        {
            drpBanks.Items.Clear();
            drpBanks.Items.Add(new DevExpress.Web.ListEditItem("N/A", Constants.IntNullValue.ToString()));
        }
    }
    protected void drpBanks_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(DrpVoucherType.SelectedItem.Value.ToString()) == 15 && drpBanks.Items.Count > 0)
        {
            LoadCheque(2);
        }
    }
    private void CreatTable()
    {
        dtVoucher = new DataTable();
        dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Account_Code", typeof(string));
        dtVoucher.Columns.Add("Account_Name", typeof(string));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        dtVoucher.Columns.Add("InvoiceNo", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));
        dtVoucher.Columns.Add("Principal", typeof(string));
        this.Session.Add("dtVoucher", dtVoucher);
    }

    public void LoadCheque(int TYPE)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = _bnkController.SelectCheckBook(Constants.IntNullValue, Convert.ToInt32(drpBanks.SelectedItem.Value), null, null, null, true, Constants.DateNullValue, TYPE);
            clsWebFormUtil.FillDxComboBoxList(this.drpChequeNo, dt, 0, 1, true);

            if (dt.Rows.Count > 0)
            {
                drpChequeNo.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDxComboBoxList(this.drpDistributor, dt, 0, 2, true);

        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedIndex = 0;
        }
    }

    private void LoadAccountDetail()
    {
        //Load Voucher Type by User Rights 

        LedgerController mLedgerController = new LedgerController();
        DataTable dt = mLedgerController.SelectVoucherType(int.Parse(this.Session["UserId"].ToString()));
        clsWebFormUtil.FillDxComboBoxList(this.DrpVoucherType, dt, 0, 1, true);
        if (dt.Rows.Count > 0)
        {
            DrpVoucherType.SelectedIndex = 0;
        }

        // Load Account Head 
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue, 1);
        clsWebFormUtil.FillDxComboBoxList(this.ddlAccountHead, dtHead, "ACCOUNT_HEAD_ID", "ACCOUNT_DETAIL", true);

        if (dtHead.Rows.Count > 0)
        {
            ddlAccountHead.SelectedIndex = 0;
        }
        this.Session.Add("dtHead", dtHead);
    }


    private void ClearAll()
    {
        txtTotalCredit.Text = "0";
        txtTotalDebit.Text = "0";
        txtRemarks.Text = "";
        txtDebitAmount.Text = "";
        txtCreditAmount.Text = "";
        txtAccountDes.Text = "";
        GrdOrder.DataSource = null;
        GrdOrder.DataBind();

        Session.Remove("dtVoucher");
        CreatTable();
        txtpayeesName.Text = "";
        drpDistributor.Enabled = true;
        DrpVoucherType.Enabled = true;
        drpDistributor.Enabled = true;
        DrpVoucherSubType.Enabled = true;
        lblError.Text = "";
    }

    private void PrintVoucher(string VoucherNo)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        crpVoucherView CrpReport = new crpVoucherView();
        int VoucherType = Constants.IntNullValue;

        if (DrpVoucherType.SelectedItem.Value.ToString() == "16" || DrpVoucherType.SelectedItem.Value.ToString() == "812")
        {
            VoucherType = Convert.ToInt32(DrpVoucherType.SelectedItem.Value.ToString());
        }
        else
        {
            if (DrpVoucherSubType.Items.Count > 0)
            {
                VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedItem.Value.ToString());
            }
        }

        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedItem.Value.ToString()), VoucherNo, VoucherType);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        CrpReport.SetParameterValue("VoucherType", DrpVoucherType.SelectedItem.Text);
        if (DrpVoucherType.SelectedItem.Value.ToString() == "16")
        {
            CrpReport.SetParameterValue("VoucherSubType", "Journal Voucher");
        }
        else if (DrpVoucherType.SelectedItem.Value.ToString() == "812")
        {
            CrpReport.SetParameterValue("VoucherSubType", "Opening Balance Voucher");
        }
        else
        {
            CrpReport.SetParameterValue("VoucherSubType", DrpVoucherSubType.SelectedItem.Text);
        }
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    protected void DrpVoucherType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ClearAll();
        this.LoadAccountHead();
        this.HideShowControls();
        txtDebitAmount.Enabled = true;
        txtCreditAmount.Enabled = true;
        if (DrpVoucherType.SelectedItem.Value.ToString() != "16" && DrpVoucherType.SelectedItem.Value.ToString() != "812")
        {
            DrpVoucherSubType_SelectedIndexChanged(null, null);
        }
        DrpVoucherType.Focus();

        if (DrpVoucherType.SelectedItem.Value.ToString() == "812")
        {
            divImport.Visible = true;
        }
        else
        {
            divImport.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        decimal TotalDebit = 0;
        decimal TotalCredit = 0;

        DataControl dc = new DataControl();
        DataTable dtHead = (DataTable)this.Session["dtHead"];
        dtVoucher = (DataTable)this.Session["dtVoucher"];
        if (RowId >= 0)
        {
            DataRow dr = dtVoucher.Rows[RowId];
            dr["ACCOUNT_HEAD_ID"] = ddlAccountHead.SelectedItem.Value.ToString();
            dr["ACCOUNT_CODE"] = "";
            dr["ACCOUNT_NAME"] = ddlAccountHead.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtDebitAmount.Text));
            dr["CREDIT"] = decimal.Parse(dc.chkNull_0(txtCreditAmount.Text));
            dr["Principal"] = "0";
            dr["Principal_id"] = "0";
            dr["REMARKS"] = txtAccountDes.Text;

        }
        else
        {
            DataRow dr = dtVoucher.NewRow();
            dr["Ledger_ID"] = Constants.LongNullValue.ToString();
            dr["ACCOUNT_HEAD_ID"] = ddlAccountHead.SelectedItem.Value.ToString();
            dr["ACCOUNT_CODE"] = "";
            dr["ACCOUNT_NAME"] = ddlAccountHead.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtDebitAmount.Text));
            dr["CREDIT"] = decimal.Parse(dc.chkNull_0(txtCreditAmount.Text));
            dr["REMARKS"] = txtAccountDes.Text;
            dr["Principal"] = "0";
            dr["Principal_id"] = "0";
            dtVoucher.Rows.Add(dr);
        }

        #region Set Total

        foreach (DataRow dr in dtVoucher.Rows)
        {
            TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
            TotalCredit += decimal.Parse(dr["CREDIT"].ToString());
        }
        txtTotalDebit.Text = decimal.Round(TotalDebit, 2).ToString();
        txtTotalCredit.Text = decimal.Round(TotalCredit, 2).ToString();

        #endregion

        #region Clear Txtbox

        txtDebitAmount.Text = "";
        txtCreditAmount.Text = "";
        txtAccountDes.Text = "";
        RowId = -1;

        #endregion

        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        if (GrdOrder.Rows.Count > 0)
        {
            DrpVoucherSubType.Enabled = false;
            DrpVoucherType.Enabled = false;
            drpDistributor.Enabled = false;
        }
        this.Session.Add("dtVoucher", dtVoucher);
        ScriptManager.GetCurrent(Page).SetFocus(ddlAccountHead);
    }

    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        decimal TotalDebit = 0;
        decimal TotalCredit = 0;
        dtVoucher = (DataTable)this.Session["dtVoucher"];
        dtVoucher.Rows.RemoveAt(e.RowIndex);

        #region Set Total

        //foreach (DataRow dr in dtVoucher.Rows)
        //{
        //    TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
        //    TotalCredit += decimal.Parse(dr["CREDIT"].ToString());
        //}
        txtTotalDebit.Text = TotalDebit.ToString();
        txtTotalCredit.Text = TotalCredit.ToString();

        #endregion

        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        RowId = -1;
    }

    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowId = e.NewEditIndex;
        ddlAccountHead.Value = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;
        txtAccountDes.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;
        txtDebitAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[4].Text;
        txtCreditAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[5].Text;
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        dtVoucher = (DataTable)this.Session["dtVoucher"];
        if (dtVoucher.Rows.Count > 0)
        {
            //if (IsDayClosed())
            //{
            //    UserController UserCtl = new UserController();

            //    UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
            //    Session.Clear();
            //    System.Web.Security.FormsAuthentication.SignOut();
            //    Response.Redirect("../Login.aspx");
            //}

            LedgerController mLController = new LedgerController();
            DataControl dc = new DataControl();
            dtVoucher = (DataTable)this.Session["dtVoucher"];
            DataTable dtHead = (DataTable)this.Session["dtHead"];

            decimal TotalDebit = 0;
            decimal TotalCredit = 0;

            #region Set Total

            foreach (DataRow dr in dtVoucher.Rows)
            {
                TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
                TotalCredit += decimal.Parse(dr["CREDIT"].ToString());
            }

            #endregion

            #region In Case of Bank & Cash Voucher

            if (DrpVoucherType.Value.ToString() == "14" || DrpVoucherType.Value.ToString() == "15")
            {
                decimal differnceAmt = TotalDebit - TotalCredit;

                if (differnceAmt > 0)
                {
                    DataRow dr1 = dtVoucher.NewRow();
                    dr1["Ledger_ID"] = Constants.LongNullValue.ToString();
                    dr1["ACCOUNT_HEAD_ID"] = drpBanks.SelectedItem.Value.ToString();
                    dr1["ACCOUNT_CODE"] = "";
                    dr1["ACCOUNT_NAME"] = drpBanks.SelectedItem.Text;
                    dr1["REMARKS"] = txtAccountDes.Text;
                    dr1["CREDIT"] = differnceAmt;
                    dr1["DEBIT"] = 0;
                    dr1["Principal"] = "";
                    dr1["Principal_id"] = dtVoucher.Rows[0]["Principal_id"].ToString();
                    dtVoucher.Rows.Add(dr1);

                }
                else if (differnceAmt < 0)
                {
                    DataRow dr1 = dtVoucher.NewRow();
                    dr1["Ledger_ID"] = Constants.LongNullValue.ToString();
                    dr1["ACCOUNT_HEAD_ID"] = drpBanks.SelectedItem.Value.ToString();
                    dr1["ACCOUNT_CODE"] = "";
                    dr1["ACCOUNT_NAME"] = drpBanks.SelectedItem.Text;
                    dr1["REMARKS"] = txtAccountDes.Text;
                    dr1["CREDIT"] = 0;
                    dr1["DEBIT"] = -(differnceAmt);
                    dr1["Principal"] = "";
                    dr1["Principal_id"] = dtVoucher.Rows[0]["Principal_id"].ToString();
                    dtVoucher.Rows.Add(dr1);
                }
            }

            #endregion

            int VoucherType = Constants.IntNullValue;

            if (DrpVoucherType.Items.Count > 0)
            {
                if (DrpVoucherType.SelectedItem.Value.ToString() == "16" || DrpVoucherType.SelectedItem.Value.ToString() == "812")
                {
                    VoucherType = Convert.ToInt32(DrpVoucherType.SelectedItem.Value.ToString());
                }
                else
                {
                    VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedItem.Value.ToString());
                }
            }
            DateTime ChequeDate;
            DateTime Voucherdate;
            DateTime DueDate;
            DateTime TransactionDate;

            ChequeDate = Constants.DateNullValue;

            if (txtDueDate.Text.Length > 0 && txtDueDate.Visible)
            {
                DueDate = DateTime.Parse(txtDueDate.Text + " 00:00:00");
            }
            else
            {
                DueDate = Constants.DateNullValue;
            }

            if (txtTransactionDate.Text.Length > 0 && txtTransactionDate.Visible)
            {
                TransactionDate = DateTime.Parse(txtTransactionDate.Text + " 00:00:00");
            }
            else
            {
                TransactionDate = Constants.DateNullValue;
            }
            string ChequeNo = null;
            if (DrpVoucherType.SelectedItem.Value.ToString() == "15")
            {
                if (DrpVoucherSubType.SelectedIndex == 1)
                {
                    try
                    {
                        ChequeNo = drpChequeNo.SelectedItem.Text;
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select cheque no');", true);
                        return;
                    }
                }
            }
            Voucherdate = TransactionDate;

            string MaxDocumentId = mLController.SelectMaxVoucherId(VoucherType, int.Parse(drpDistributor.SelectedItem.Value.ToString()), Voucherdate);

            if (TotalDebit == TotalCredit || DrpVoucherType.SelectedItem.Value.ToString() != "16")
            {
                try
                {
                    bool IsSaveVoucher = mLController.Edit_Voucher(int.Parse(drpDistributor.SelectedItem.Value.ToString()), 0, MaxDocumentId, VoucherType,
                       TransactionDate, int.Parse(DrpPaymentMode.SelectedItem.Value.ToString()), txtpayeesName.Text, txtRemarks.Text, DueDate, ChequeNo, dtVoucher, int.Parse(this.Session["UserId"].ToString()), txtSlipNo.Text, DueDate);

                    if (IsSaveVoucher == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Voucher No : " + MaxDocumentId + " has been saved');", true);
                        ClearAll();
                        PrintVoucher(MaxDocumentId);
                        LoadCheque(2);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occur wrong entry');", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some of entries are wrong');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Debit must equal to credit balance');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No reord found.!');", true);
        }
    }

    protected void GrdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "Total";
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[4].Text = Convert.ToDecimal(txtTotalDebit.Text).ToString("N");
            e.Row.Cells[5].Text = Convert.ToDecimal(txtTotalCredit.Text).ToString("N");
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[5].Font.Bold = true;
            e.Row.Cells[8].Visible = false;
        }
    }

    private void HideShowControls()
    {
        if (DrpVoucherType.SelectedItem.Value.ToString() == "14")
        {
            spanlblSlipNo.Visible = false;
            spanlblChequeNo.Visible = false;
            spanlblDueDate.Visible = false;

            lblChequeNo.Visible = false;
            drpChequeNo.Visible = false;
            lblSlipNo.Visible = false;
            txtSlipNo.Visible = false;
            lblDueDate.Visible = false;
            txtDueDate.Visible = false;
            ibnEndDate.Visible = false;

            spanlblPaymentMode.Visible = true;
            spanlblBankHead.Visible = true;

            lblPaymentMode.Visible = true;
            DrpPaymentMode.Visible = true;
            lblBankHead.Visible = true;
            drpBanks.Visible = true;

            DrpVoucherSubType.Items.Clear();
            DrpVoucherSubType.Items.Add(new DevExpress.Web.ListEditItem("Cash Receipt Voucher", "14"));
            DrpVoucherSubType.Items.Add(new DevExpress.Web.ListEditItem("Cash Payment Voucher", "24"));

            SubType.Visible = true;

            txtDebitAmount.Enabled = false;
            txtCreditAmount.Enabled = true;

            txtpayeesName.Visible = true;
            lblpayeesName.Visible = true;
            txtRemarks.Visible = true;
            lblRemarks.Visible = true;
        }
        else if (DrpVoucherType.SelectedItem.Value.ToString() == "16")
        {
            spanlblSlipNo.Visible = false;
            spanlblPaymentMode.Visible = false;
            spanlblBankHead.Visible = false;
            spanlblChequeNo.Visible = false;
            spanlblDueDate.Visible = false;

            lblChequeNo.Visible = false;
            drpChequeNo.Visible = false;
            lblSlipNo.Visible = false;
            txtSlipNo.Visible = false;
            lblDueDate.Visible = false;
            txtDueDate.Visible = false;
            ibnEndDate.Visible = false;

            lblPaymentMode.Visible = false;
            DrpPaymentMode.Visible = false;
            lblBankHead.Visible = false;
            drpBanks.Visible = false;

            //lblSubType.Visible = false;
            //spanlblSubType.Visible = false;
            //DrpVoucherSubType.Visible = false;

            SubType.Visible = false;

            txtDebitAmount.Enabled = true;
            txtCreditAmount.Enabled = true;

            txtpayeesName.Visible = true;
            lblpayeesName.Visible = true;
            txtRemarks.Visible = true;
            lblRemarks.Visible = true;
        }
        else if (DrpVoucherType.SelectedItem.Value.ToString() == "812")
        {
            spanlblSlipNo.Visible = false;
            spanlblPaymentMode.Visible = false;
            spanlblBankHead.Visible = false;
            spanlblChequeNo.Visible = false;
            spanlblDueDate.Visible = false;

            lblChequeNo.Visible = false;
            drpChequeNo.Visible = false;
            lblSlipNo.Visible = false;
            txtSlipNo.Visible = false;
            lblDueDate.Visible = false;
            txtDueDate.Visible = false;
            ibnEndDate.Visible = false;
            txtpayeesName.Visible = false;
            lblpayeesName.Visible = false;
            txtRemarks.Visible = false;
            lblRemarks.Visible = false;

            lblPaymentMode.Visible = false;
            DrpPaymentMode.Visible = false;
            lblBankHead.Visible = false;
            drpBanks.Visible = false;

            //lblSubType.Visible = false;
            //spanlblSubType.Visible = false;
            //DrpVoucherSubType.Visible = false;
            SubType.Visible = false;

            txtDebitAmount.Enabled = true;
            txtCreditAmount.Enabled = true;
        }
        else
        {
            spanlblSlipNo.Visible = true;
            spanlblPaymentMode.Visible = true;
            spanlblBankHead.Visible = true;
            spanlblChequeNo.Visible = true;
            spanlblDueDate.Visible = true;

            lblChequeNo.Visible = true;
            drpChequeNo.Visible = true;
            lblSlipNo.Visible = true;
            txtSlipNo.Visible = true;
            lblDueDate.Visible = true;
            txtDueDate.Visible = true;
            ibnEndDate.Visible = true;
            lblPaymentMode.Visible = true;
            DrpPaymentMode.Visible = true;
            lblBankHead.Visible = true;
            drpBanks.Visible = true;

            txtpayeesName.Visible = true;
            lblpayeesName.Visible = true;
            txtRemarks.Visible = true;
            lblRemarks.Visible = true;

            DrpVoucherSubType.Items.Clear();
            DrpVoucherSubType.Items.Add(new DevExpress.Web.ListEditItem("Bank Receipt Voucher", "15"));
            DrpVoucherSubType.Items.Add(new DevExpress.Web.ListEditItem("Bank Payment Voucher", "17"));

            //spanlblSubType.Visible = true;
            //lblSubType.Visible = true;
            //DrpVoucherSubType.Visible = true;
            SubType.Visible = true;

            txtDebitAmount.Enabled = false;
            txtCreditAmount.Enabled = true;
        }

        if (DrpVoucherSubType.Items.Count > 0)
        {
            DrpVoucherSubType.SelectedIndex = 0;
        }
    }

    protected void DrpVoucherSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpChequeNo.Enabled = false;
        DrpPaymentMode.Items.Clear();
        if (DrpVoucherSubType.SelectedIndex == 0)
        {
            if (DrpVoucherType.SelectedItem.Value.ToString() == "14")
            {
                DrpPaymentMode.Items.Add(new DevExpress.Web.ListEditItem("Cash", "19"));
                DrpPaymentMode.Items.Add(new DevExpress.Web.ListEditItem("Online Transfer", "30"));
            }
            else if (DrpVoucherType.SelectedItem.Value.ToString() == "15")
            {
                DrpPaymentMode.Items.Add(new DevExpress.Web.ListEditItem("Cheque", "18"));
                DrpPaymentMode.Items.Add(new DevExpress.Web.ListEditItem("Online Transfer", "30"));
            }

            txtDebitAmount.Enabled = false;
            txtCreditAmount.Enabled = true;
        }
        else if (DrpVoucherSubType.SelectedIndex == 1)
        {
            if (DrpVoucherType.SelectedItem.Value.ToString() == "14")
            {
                DrpPaymentMode.Items.Add(new DevExpress.Web.ListEditItem("Cash", "19"));
                DrpPaymentMode.Items.Add(new DevExpress.Web.ListEditItem("Online Transfer", "30"));
            }
            else if (DrpVoucherType.SelectedItem.Value.ToString() == "15")
            {
                DrpPaymentMode.Items.Add(new DevExpress.Web.ListEditItem("Cheque", "18"));
                DrpPaymentMode.Items.Add(new DevExpress.Web.ListEditItem("Pay Order", "21"));
            }

            txtDebitAmount.Enabled = true;
            txtCreditAmount.Enabled = false;
            drpChequeNo.Enabled = true;
        }
        DrpVoucherSubType.Focus();
        if (DrpPaymentMode.Items.Count > 0)
        {
            DrpPaymentMode.SelectedIndex = 0;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearAll();
        LoadCheque(2);
    }

    //protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    LoadAccountDetail();
    //    LoadAccountHead();
    //}

    private bool IsDayClosed()
    {
        DistributorController DistrCtl = new DistributorController();
        try
        {
            DataTable dtDayClose = DistrCtl.MaxDayClose(Convert.ToInt32(Session["DISTRIBUTOR_ID"]), 3);
            if (dtDayClose.Rows.Count > 0)
            {
                if (Convert.ToDateTime(Session["CurrentWorkDate"]) == Convert.ToDateTime(dtDayClose.Rows[0]["DayClose"]))
                {
                    return false;
                }
            }
            return true;
        }
        catch (Exception)
        {

            throw;
        }
    }

    #region Import Vouchers
    protected void btnImport_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FolderPath = @"../Docs/";
            if (Extension != ".xls" && Extension != ".xlsx")
            {
                lblError.Text = "Only Excel files are allowed.";
                return;
            }
            string FilePath = Server.MapPath(FolderPath + FileName);
            FileUpload1.SaveAs(FilePath);
            Import_To_Grid(FilePath, Extension, "Yes");
        }
    }
    private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    {
        try
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = System.Configuration.ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = System.Configuration.ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            if (dt.Rows.Count > 0)
            {
                lblError.Text = BindWithGrid(dt);
            }
            else
            {
                lblError.Text = "No record found to import";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    public string BindWithGrid(DataTable dtExcel)
    {
        string errorMessage = "";
        try
        {

            decimal TotalDebit = 0;
            decimal TotalCredit = 0;

            decimal TotalDebitSheet = 0;
            decimal TotalCreditSheet = 0;

            DataControl dc = new DataControl();
            dtVoucher = (DataTable)this.Session["dtVoucher"];

            #region Validations
            int RowsCount = dtExcel.Rows.Count;

            for (int i = 0; i < RowsCount; i++)
            {
                decimal DEBIT = Convert.ToDecimal(dc.chkNull_0(dtExcel.Rows[i]["DEBIT"].ToString()));
                decimal CREDIT = Convert.ToDecimal(dc.chkNull_0(dtExcel.Rows[i]["CREDIT"].ToString()));
                if ((dtExcel.Rows[i]["DEBIT"].ToString().Length == 0) && (dtExcel.Rows[i]["CREDIT"].ToString().Length == 0))
                {
                    errorMessage = "At record number '" + (i + 1) + "' Debit or Credit must enter";
                    return errorMessage;
                }
                if ((DEBIT > 0) && (CREDIT > 0))
                {
                    errorMessage = "At record number '" + (i + 1) + "' Only Debit or Credit can enter";
                    return errorMessage;
                }
                if (dtExcel.Rows[i][0].ToString().Length == 0)
                {
                    errorMessage = "At record number '" + (i + 1) + "' Account Head must enter";
                    return errorMessage;
                }
                TotalDebitSheet += Convert.ToDecimal(dc.chkNull_0(dtExcel.Rows[i]["DEBIT"].ToString()));
                TotalCreditSheet += Convert.ToDecimal(dc.chkNull_0(dtExcel.Rows[i]["CREDIT"].ToString()));
            }

            if (TotalDebitSheet != TotalCreditSheet)
            {
                errorMessage = "Total Dabit and Credit should be same";
                return errorMessage;
            }
            #endregion

            #region Load Grid
            DataTable dt;
            string COA_ID;
            string COA_NAME;

            foreach (DataRow dtrow in dtExcel.Rows)
            {
                if (dtExcel.Columns[0].ColumnName.ToString().ToUpper() == "COA CODE DESCRIPTION")
                {
                    dt = mAccountController.GetAccountHeadIDbyNameOrCode(Constants.AC_AccountHeadId, null, null, 3);
                    DataRow[] drow = dt.Select("ACCOUNT_DETAIL = '" + dtrow["COA CODE DESCRIPTION"].ToString() + "' ");
                    if (drow == null || drow.Length == 0)
                    {
                        errorMessage = "Account Head '" + dtrow[0].ToString() + "' is invalid ";
                        return errorMessage;
                    }
                    COA_ID = drow[0]["ACCOUNT_HEAD_ID"].ToString();
                    COA_NAME = drow[0]["ACCOUNT_DETAIL"].ToString();
                }
                else
                {
                    if (dtExcel.Columns[0].ColumnName.ToString().ToUpper() == "COA DESCRIPTION")
                    {
                        dt = mAccountController.GetAccountHeadIDbyNameOrCode(Constants.AC_AccountHeadId, null, dtrow["COA DESCRIPTION"].ToString(), 3);
                    }
                    else
                    {
                        dt = mAccountController.GetAccountHeadIDbyNameOrCode(Constants.AC_AccountHeadId, dtrow["COA CODE"].ToString(), null, 3);
                    }
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        errorMessage = "Account Head '" + dtrow[0].ToString() + "' is invalid ";
                        return errorMessage;
                    }
                    COA_ID = dt.Rows[0]["ACCOUNT_HEAD_ID"].ToString();
                    COA_NAME = dt.Rows[0]["ACCOUNT_DETAIL"].ToString();
                }

                DataRow dr = dtVoucher.NewRow();
                dr["Ledger_ID"] = Constants.LongNullValue.ToString();
                dr["ACCOUNT_HEAD_ID"] = COA_ID;
                dr["ACCOUNT_CODE"] = "";
                dr["ACCOUNT_NAME"] = COA_NAME;
                dr["DEBIT"] = decimal.Parse(dc.chkNull_0(dtrow["DEBIT"].ToString()));
                dr["CREDIT"] = decimal.Parse(dc.chkNull_0(dtrow["CREDIT"].ToString()));
                dr["REMARKS"] = dtrow["REMARKS"].ToString();
                dr["Principal"] = "0";
                dr["Principal_id"] = "0";
                dtVoucher.Rows.Add(dr);
            }
            #endregion

            #region Set Total

            foreach (DataRow drow in dtVoucher.Rows)
            {
                TotalDebit += decimal.Parse(drow["DEBIT"].ToString());
                TotalCredit += decimal.Parse(drow["CREDIT"].ToString());
            }
            txtTotalDebit.Text = decimal.Round(TotalDebit, 2).ToString();
            txtTotalCredit.Text = decimal.Round(TotalCredit, 2).ToString();


            #endregion

            #region Binding
            GrdOrder.DataSource = dtVoucher;
            GrdOrder.DataBind();
            if (GrdOrder.Rows.Count > 0)
            {
                DrpVoucherSubType.Enabled = false;
                DrpVoucherType.Enabled = false;
                drpDistributor.Enabled = false;
            }
            #endregion

            this.Session.Add("dtVoucher", dtVoucher);
            ScriptManager.GetCurrent(Page).SetFocus(ddlAccountHead);
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
        }
        return errorMessage;
    }
    #endregion
}