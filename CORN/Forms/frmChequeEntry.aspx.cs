using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;


public partial class Forms_frmChequeEntry : System.Web.UI.Page
{
    readonly DataControl dc = new DataControl();
    readonly LedgerController LController = new LedgerController();
    readonly BankController _bnkController = new BankController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //if (IsDayClosed())
            //{
            //    UserController UserCtl = new UserController();

            //    UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
            //    Session.Clear();
            //    System.Web.Security.FormsAuthentication.SignOut();
            //    Response.Redirect("../Login.aspx");
            //}
            //DrpStatus.Items.Add(new DevExpress.Web.ListEditItem("Issue", "527"));
            DrpStatus.Items.Add(new DevExpress.Web.ListEditItem("Clear", "529"));
            //DrpStatus.Items.Add(new DevExpress.Web.ListEditItem("Bounce", "530"));
            //DrpStatus.Items.Add(new DevExpress.Web.ListEditItem("Cancel", "560"));

            DrpStatus.SelectedIndex = 0;
            LoadAccountHead();
            LoadDistributor();
            LoadAccountDetail();
            LoadData();
            SelectCreditInvoice();
            LoadReceviedCheque();

            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            DrpStatus.SelectedIndex = 0;
            DrpStatus.Enabled = false;
            DataTable dtConfig = GetCOAConfiguration();
            bool IsFinanceSetting = GetFinanceConfig();
            Session.Add("dtConfig", dtConfig);
            Session.Add("IsFinanceSetting", IsFinanceSetting);
        }
    }
    
    private void toggleControls(string pAccountType)
    {
        if (pAccountType == "21")
        {
            lblChequeNo.Visible = false;
            txtChequeNo.Visible = false;
            lblChequeDate.Visible = false;
            txtStartDate.Visible = false;
            ibtnStartDate.Visible = false;
            DrpStatus.Visible = false;
            lblStatus.Visible = false;
        }
        else if (pAccountType == "33")
        {
            lblChequeNo.Visible = false;
            txtChequeNo.Visible = false;
            lblChequeDate.Visible = true;
            txtStartDate.Visible = true;
            ibtnStartDate.Visible = true;
            DrpStatus.Visible = false;
            lblStatus.Visible = false;
            lblChequeDate.Text = "Transfer Date";
        }
        else
        {

            lblChequeNo.Visible = true;
            txtChequeNo.Visible = true;
            lblChequeDate.Visible = true;
            txtStartDate.Visible = true;
            ibtnStartDate.Visible = true;
            DrpStatus.Visible = true;
            lblStatus.Visible = true;
            lblChequeDate.Text = "Cheque Date";
        }
    }

    #region Load
    private void LoadPaymentRecieved()
    {
        ChequeEntryController CController = new ChequeEntryController();
        LedgerController LController = new LedgerController();

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

        if (drpDistributor.Items.Count > 0)
        {
            DataSet dsReceived = LController.SelectBankCashTransction(int.Parse(drpDistributor.SelectedItem.Value.ToString()), Constants.IntNullValue, 21,
                CurrentWorkDate);

            DataTable dtRealized = dsReceived.Tables[2];

            if (dtRealized.Rows.Count > 0)
            {
                lblAmount.Text = string.Format("{0:0,0.00}", Convert.ToDecimal(dtRealized.Rows[0][0].ToString()));

            }

        }
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.GetDistributorWithMaxDayClose(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()), 1);
        clsWebFormUtil.FillDxComboBoxList(drpDistributor, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");

        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedIndex = 0;
        }
        Session.Add("dtLocationInfo", dt);
    }

    private void LoadData()
    {
        var mController = new CustomerDataController();
        DataTable dtCustomers = mController.UspSelectCustomer(int.Parse(drpDistributor.Value.ToString()), "All2", null, Constants.DateNullValue);
        clsWebFormUtil.FillDxComboBoxList(ddlCustomer, dtCustomers, "CUSTOMER_ID", "CUSTOMER_NAME", true);
        if (dtCustomers.Rows.Count > 0)
        {
            ddlCustomer.SelectedIndex = 0;
        }
    }

    private void LoadReceviedCheque()
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

        ChequeEntryController CController = new ChequeEntryController();
        
        GrdCO.DataSource = null;
        GrdCO.DataBind();

        decimal chqAmount = 0;
        if (drpDistributor.Items.Count > 0)
        {
            DataTable dt = LController.SelectBankCashTransction(int.Parse(drpDistributor.SelectedItem.Value.ToString()), Constants.IntNullValue, int.Parse(DrpAccountType.SelectedItem.Value.ToString()),
                       CurrentWorkDate, CurrentWorkDate);

            Session.Add("dt", dt);

            GrdCO.DataSource = dt;
            GrdCO.DataBind();

            if (dt != null)
            {
                foreach (DataRow gvr in dt.Rows)
                {
                    chqAmount += Convert.ToDecimal(gvr["CHEQUE_AMOUNT"]);
                }
                lblTotalAmount.Text = string.Format("{0:0.00}", chqAmount);
            }
        }
    }

    private void checkDuplication()
    {
        DataTable dt = (DataTable)Session["dt"];

        DataRow[] foundRows = dt.Select("CUSTOMER_ID = '" + ddlCustomer.SelectedItem.Value.ToString() + "' and CHEQUE_NO='" + txtChequeNo.Text + "'");

        if (foundRows.Length > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Cheque No Already exist against this vendor!');", true);
        }
    }

    private void SelectCreditInvoice()
    {
        LedgerController CDC = new LedgerController();
        GrdCredit.DataSource = null;
        GrdCredit.DataBind();
        if (ddlCustomer.Items.Count > 0)
        {
            DataTable dtCredit = CDC.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedItem.Value.ToString()),Constants.IntNullValue, int.Parse(ddlCustomer.SelectedItem.Value.ToString()), 0);
            GrdCredit.DataSource = dtCredit;
            GrdCredit.DataBind();
        }
    }

    private void LoadAccountHead()
    {
        Configuration.GetAccountHead();
        AccountHeadController mAccountController = new AccountHeadController();

        if (DrpAccountType.SelectedItem.Value.ToString() == "21")
        {
            DataTable dt = mAccountController.SelectAccountHeadByMapping(Constants.AC_AccountHeadId, Constants.LongNullValue, 2, Constants.AC_CashInHandAccountHead);
            clsWebFormUtil.FillDxComboBoxList(DrpBankAccount, dt, 0, 4, true);

            if (dt.Rows.Count > 0)
            {
                DrpBankAccount.SelectedIndex = 0;
            }
        }
        else
        {
            DataTable dt = mAccountController.SelectAccountHeadByMapping(Constants.AC_AccountHeadId, Constants.LongNullValue, 2, Constants.AC_BankAccountHead);
            clsWebFormUtil.FillDxComboBoxList(DrpBankAccount, dt, 0, 4, true);

            if (dt.Rows.Count > 0)
            {
                DrpBankAccount.SelectedIndex = 0;
            }
        }

    }

    private void LoadAccountDetail()
    {
        AccountHeadController mAccountController = new AccountHeadController();

        DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue, 1);
        clsWebFormUtil.FillDxComboBoxList(this.ddlAccountHead, dtHead, "ACCOUNT_HEAD_ID", "ACCOUNT_NAME", true);

        if (dtHead.Rows.Count > 0)
        {
            ddlAccountHead.SelectedIndex = 0;
        }
    }

    #endregion

    private void CashAdvance(DateTime CurrentWorkDate)
    {
        string remarks = "Cash " + txtRemarks.Text;
        DateTime chqDate = Constants.DateNullValue;

        int VoucherType = Constants.CashPayment_Voucher;

        if (DrpAccountType.SelectedItem.Value.ToString() == "33")
        {
            chqDate = DateTime.Parse(ConvertDate.British_To_American(txtStartDate.Text)); //Convert.ToDateTime(txtStartDate.Text);
            VoucherType = Constants.Expanse_Voucher;
            remarks = "Online Transfer " + DrpBankAccount.SelectedItem.Text + ", " + txtRemarks.Text;
        }
        string MaxDocumentID = LController.SelectLedgerMaxDocumentId(VoucherType, int.Parse(drpDistributor.SelectedItem.Value.ToString()), 1);

        Session.Add("VoucherNo", MaxDocumentID);

        DataRow[] drConfig = null;
        DataTable dtConfig = (DataTable)Session["dtConfig"];

        drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.AccountPayable + "'");
        int PayableAccount = Convert.ToInt32(drConfig[0]["VALUE"].ToString());
        
        LController.PostingInvoiceAccount(VoucherType, long.Parse(MaxDocumentID), PayableAccount, int.Parse(drpDistributor.SelectedItem.Value.ToString()), 0, decimal.Parse(dc.chkNull_0(txtAmount.Text)),
                       CurrentWorkDate, remarks, DateTime.Now,Constants.IntNullValue, int.Parse(ddlCustomer.SelectedItem.Value.ToString()), Constants.Document_Purchase,
                       null, Constants.Document_Invoice, int.Parse(Session["UserId"].ToString()), int.Parse(DrpAccountType.SelectedItem.Value.ToString()),"",null);

        LController.PostingInvoiceAccount(VoucherType, long.Parse(MaxDocumentID), long.Parse(DrpBankAccount.SelectedItem.Value.ToString()), int.Parse(drpDistributor.SelectedItem.Value.ToString()), decimal.Parse(dc.chkNull_0(txtAmount.Text)), 0,
                       CurrentWorkDate, remarks, DateTime.Now, Constants.IntNullValue, int.Parse(ddlCustomer.SelectedItem.Value.ToString()), Constants.Document_Purchase,
                       null, Constants.Document_Invoice, int.Parse(Session["UserId"].ToString()), int.Parse(DrpAccountType.SelectedItem.Value.ToString()), "",null);
    }
    private void ChequeRealization(DateTime CurrentWorkDate)
    {
        DateTime ChequeDate = Constants.DateNullValue;
        try
        {
            if (DrpAccountType.SelectedItem.Value.ToString() == "18" || DrpAccountType.SelectedItem.Value.ToString() == "33")
            {
                if (txtStartDate.Text.Length == 10)
                {
                    ChequeDate = DateTime.Parse(ConvertDate.British_To_American(txtStartDate.Text));
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Correct Cheque Date Pattern is DD/MM/YYYY.');", true);
            return;
        }

        LedgerController LController = new LedgerController();
        string MaxDocumentId = "";
        int VoucherType = Constants.Expanse_Voucher;
        decimal OfferAmount = decimal.Parse(txtAmount.Text);
        decimal realizeAmount = 0;
        string remarks = "";
        if (DrpAccountType.SelectedItem.Value.ToString() == "18")//Cheque
        {
            MaxDocumentId = LController.SelectLedgerMaxDocumentId(VoucherType, int.Parse(drpDistributor.SelectedItem.Value.ToString()), 1);
            remarks = "Chq# " + txtChequeNo.Text + ", " + DrpBankAccount.SelectedItem.Text + ", " + txtRemarks.Text;
        }
        else if (DrpAccountType.SelectedItem.Value.ToString() == "33")//Online
        {
            MaxDocumentId = LController.SelectLedgerMaxDocumentId(VoucherType, int.Parse(drpDistributor.SelectedItem.Value.ToString()), 1);
            remarks = "Online Transfer " + DrpAccountType.SelectedItem.Text + ", " + txtRemarks.Text;
        }
        else if (DrpAccountType.SelectedItem.Value.ToString() == "21")//Cash
        {
            VoucherType = Constants.Cash_Voucher;
            MaxDocumentId = LController.SelectLedgerMaxDocumentId(VoucherType, int.Parse(drpDistributor.SelectedItem.Value.ToString()), 1);
            remarks = "Cash " + txtRemarks.Text;
        }
        Session.Add("VoucherNo", MaxDocumentId);

        DataRow[] drConfig = null;
        DataTable dtConfig = (DataTable)Session["dtConfig"];
        drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.AccountPayable + "'");
        int PayableAccount = Convert.ToInt32(drConfig[0]["VALUE"].ToString());
        string ChequeNo = null;
        string manualNo = "";
        if (DrpAccountType.SelectedIndex == 0)
        {
            ChequeNo = txtChequeNo.Text;
        }

        foreach (GridViewRow dr in GrdCredit.Rows)
        {
            CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
            if (chRelized.Checked == true)
            {
                manualNo = dr.Cells[5].Text;
                if (Convert.ToString(dr.Cells[5].Text) == "opng")
                {
                    manualNo = "opng";
                }
                if (decimal.Parse(dr.Cells[4].Text) >= OfferAmount)
                {
                    realizeAmount += OfferAmount;
                    LController.PostingInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), PayableAccount, int.Parse(drpDistributor.SelectedItem.Value.ToString()), 0, OfferAmount,
                    CurrentWorkDate, remarks, DateTime.Now, Constants.IntNullValue, int.Parse(ddlCustomer.SelectedItem.Value.ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), manualNo, Constants.Document_Invoice,
                    int.Parse(Session["UserId"].ToString()),  int.Parse(DrpAccountType.SelectedItem.Value.ToString()), "",ChequeNo);

                    LController.PostingInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), Convert.ToInt64(DrpBankAccount.SelectedItem.Value), int.Parse(drpDistributor.SelectedItem.Value.ToString()), OfferAmount, 0,
                    CurrentWorkDate, remarks, DateTime.Now, Constants.IntNullValue, int.Parse(ddlCustomer.SelectedItem.Value.ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), manualNo, Constants.Document_Invoice,
                    int.Parse(Session["UserId"].ToString()), int.Parse(DrpAccountType.SelectedItem.Value.ToString()), "", ChequeNo);

                    OfferAmount = decimal.Parse(dr.Cells[4].Text) - OfferAmount;
                    LController.UpdateSaleInvoice(Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedItem.Value.ToString()), OfferAmount);
                    break;
                }
                else if (decimal.Parse(dr.Cells[4].Text) <= OfferAmount)
                {
                    realizeAmount += decimal.Parse(dr.Cells[4].Text);

                    LController.PostingInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), PayableAccount, int.Parse(drpDistributor.SelectedItem.Value.ToString()), 0, decimal.Parse(dr.Cells[4].Text),
                    CurrentWorkDate, remarks, DateTime.Now, Constants.IntNullValue, int.Parse(ddlCustomer.SelectedItem.Value.ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), manualNo, Constants.Document_Invoice,
                    int.Parse(Session["UserId"].ToString()), int.Parse(DrpAccountType.SelectedItem.Value.ToString()), "", ChequeNo);

                    LController.PostingInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), Convert.ToInt64(DrpBankAccount.SelectedItem.Value), int.Parse(drpDistributor.SelectedItem.Value.ToString()), decimal.Parse(dr.Cells[4].Text), 0,
                    CurrentWorkDate, remarks, DateTime.Now, Constants.IntNullValue, int.Parse(ddlCustomer.SelectedItem.Value.ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), manualNo, Constants.Document_Invoice,
                    int.Parse(Session["UserId"].ToString()), int.Parse(DrpAccountType.SelectedItem.Value.ToString()), "", ChequeNo);
                    
                    OfferAmount = OfferAmount - decimal.Parse(dr.Cells[4].Text);
                    LController.UpdateSaleInvoice(long.Parse(dr.Cells[1].Text), int.Parse(drpDistributor.SelectedItem.Value.ToString()), 0);
                }
            }
        }

        //Advance Entry
        if (realizeAmount < decimal.Parse(txtAmount.Text))
        {

            LController.PostingInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), PayableAccount, int.Parse(drpDistributor.SelectedItem.Value.ToString()), 0, OfferAmount,
                       CurrentWorkDate, remarks + " (Advance)", DateTime.Now, Constants.IntNullValue, int.Parse(ddlCustomer.SelectedItem.Value.ToString()), Constants.Document_Invoice,
                       null, Constants.Document_Invoice, int.Parse(Session["UserId"].ToString()), int.Parse(DrpAccountType.SelectedItem.Value.ToString()), "", null);


            LController.PostingInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedItem.Value.ToString()), int.Parse(drpDistributor.SelectedItem.Value.ToString()), OfferAmount, 0,
                           CurrentWorkDate, remarks + " (Advance)", DateTime.Now, Constants.IntNullValue, int.Parse(ddlCustomer.SelectedItem.Value.ToString()), Constants.Document_Purchase,
                           null, Constants.Document_Invoice, int.Parse(Session["UserId"].ToString()), int.Parse(DrpAccountType.SelectedItem.Value.ToString()), "", null);
        }
    }
    
    #region Sel/Index Change

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectCreditInvoice();
        LoadReceviedCheque();
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadData();
        SelectCreditInvoice();
        LoadReceviedCheque();
    }

    protected void DrpBankAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                LoadReceviedCheque();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    protected void DrpAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountHead();
        toggleControls(DrpAccountType.SelectedItem.Value.ToString());
        SelectCreditInvoice();
        LoadReceviedCheque();
    }


    #endregion

    #region Click

    private bool InvalidDate()
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

        if (DrpAccountType.SelectedItem.Value.ToString() == "18")
        {
            if (DrpStatus.SelectedItem.Value.ToString() == "527")
            {
                if (Convert.ToDateTime(ConvertDate.British_To_American(txtStartDate.Text)) < CurrentWorkDate)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invalid Cheque Date');", true);

                    return false;
                }
            }

        }
        return true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
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

            if (CurrentWorkDate != Constants.DateNullValue)
            {
                if (InvalidDate())
                {
                    ChequeEntryController CController = new ChequeEntryController();
                    DateTime ChequeDate;
                    try
                    {
                        if (txtStartDate.Text.Length == 10)
                        {
                            ChequeDate = DateTime.Parse(ConvertDate.British_To_American(txtStartDate.Text));
                        }
                        else
                        {
                            ChequeDate = DateTime.Now;
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Correct Cheque Date Pattern is DD/MM/YYYY.');", true);
                        return;
                    }
                    int InvoiceCount = Constants.IntNullValue;
                    if (DrpAccountType.SelectedIndex == 0)//on Cash Realization check Invoice Selection
                    {
                        foreach (GridViewRow dr in GrdCredit.Rows)
                        {
                            CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                            if (chRelized.Checked == true)
                            {
                                InvoiceCount++;
                                break;
                            }
                        }
                        if (InvoiceCount == Constants.IntNullValue)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please select an invoice');", true);
                            return;
                        }
                    }
                    if (btnSave.Text == "Save")
                    {
                        if (DrpAccountType.SelectedIndex != 0)//on Cash Realization check Invoice Selection
                        {
                            foreach (GridViewRow dr in GrdCredit.Rows)
                            {
                                CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                                if (chRelized.Checked == true)
                                {
                                    InvoiceCount++;
                                    break;
                                }
                            }
                            if (InvoiceCount == Constants.IntNullValue && DrpAccountType.SelectedIndex != 0)
                            {
                                CashAdvance(CurrentWorkDate);
                                if (Convert.ToBoolean(Session["IsFinanceSetting"]))
                                {
                                    InsertGL2(null, CurrentWorkDate);
                                }
                                Session.Remove("VoucherNo");
                            }
                            else if (InvoiceCount != Constants.IntNullValue && DrpAccountType.SelectedIndex != 0)
                            {
                                ChequeRealization(CurrentWorkDate);// used as All cash, online, cheque
                                if (Convert.ToBoolean(Session["IsFinanceSetting"]))
                                {
                                    string ChequeNo = null;
                                    if (DrpAccountType.SelectedIndex == 0)
                                    {
                                        ChequeNo = txtChequeNo.Text;
                                    }
                                    InsertGL2(ChequeNo, CurrentWorkDate);
                                }
                                Session.Remove("VoucherNo");
                                SelectCreditInvoice();
                            }
                        }
                        else if (DrpAccountType.SelectedIndex == 0)//on Cash Realization check Invoice Selection
                        {
                            checkDuplication();
                            ChequeRealization(CurrentWorkDate);
                            if (Convert.ToBoolean(Session["IsFinanceSetting"]))
                            {
                                string ChequeNo = null;
                                if (DrpAccountType.SelectedIndex == 0)
                                {
                                    ChequeNo = txtChequeNo.Text;
                                }
                                InsertGL(ChequeNo, CurrentWorkDate);
                            }
                            Session.Remove("VoucherNo");
                            SelectCreditInvoice();
                        }
                    }
                    ClearAll();
                    LoadReceviedCheque();
                    LoadPaymentRecieved();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Dayclose not found for selected location!')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        GrdCO.DataSource = null;
        GrdCO.DataBind();
        DataTable dt = (DataTable)Session["dt"];

        switch (ddSearchType.SelectedIndex)
        {
            case 1:
                dt.DefaultView.RowFilter = ddSearchType.SelectedItem.Value.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 2:
                dt.DefaultView.RowFilter = ddSearchType.SelectedItem.Value.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            default:
                dt.DefaultView.RowFilter = "CHEQUE_NO" + " like '%" + "" + "%'";
                break;
        }
        GrdCO.DataSource = dt.DefaultView;
        GrdCO.DataBind();
    }

    #endregion
    
    private void InsertGL(string ChequeNo,DateTime CurrentWorkDate)
    {

        Configuration.GetAccountHead();
        DataTable dtVoucher = new DataTable();

        dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtVoucher.Columns.Add("DEBIT", typeof(decimal));
        dtVoucher.Columns.Add("CREDIT", typeof(decimal));
        dtVoucher.Columns.Add("REMARKS", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));

        DataRow[] drConfig = null;
        DataTable dtConfig = (DataTable)Session["dtConfig"];

        drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.AccountPayable + "'");
        int PayableAccount = Convert.ToInt32(drConfig[0]["VALUE"].ToString());

        DataRow dr = dtVoucher.NewRow();
        dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpBankAccount.SelectedItem.Value.ToString());
        dr["REMARKS"] = DrpBankAccount.SelectedItem.Text + " Paid to " + ddlCustomer.SelectedItem.Text;
        dr["DEBIT"] = 0;
        dr["CREDIT"] = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text));
        dr["Principal_Id"] = 0;
        dtVoucher.Rows.Add(dr);

        //Debit Side Entry

        DataRow dr1 = dtVoucher.NewRow();
        dr1["ACCOUNT_HEAD_ID"] = PayableAccount;
        dr1["DEBIT"] = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text));
        dr1["CREDIT"] = 0;
        dr1["Principal_Id"] = 0;
        dr1["REMARKS"] = DrpBankAccount.SelectedItem.Text + " Paid to " + ddlCustomer.SelectedItem.Text;
        dtVoucher.Rows.Add(dr1);
        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Expanse_Voucher, Convert.ToInt32(drpDistributor.SelectedItem.Value.ToString()), CurrentWorkDate);
        LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedItem.Value.ToString()), 0, MaxDocumentId, Constants.Expanse_Voucher, CurrentWorkDate, Constants.Bank_Deposit, Session["VoucherNo"].ToString(), DrpBankAccount.SelectedItem.Text + " Voucher", Constants.DateNullValue, ChequeNo, dtVoucher, Convert.ToInt32(Session["UserID"]), "-1", Constants.DateNullValue, true);
    }

    private void InsertGL2(string ChequeNo,DateTime CurrentWorkDate)
    {
        DateTime ChequeDate = Constants.DateNullValue;
        try
        {
            if (DrpAccountType.SelectedItem.Value.ToString() == "18" || DrpAccountType.SelectedItem.Value.ToString() == "33")
            {
                if (txtStartDate.Text.Length == 10)
                {
                    ChequeDate = DateTime.Parse(ConvertDate.British_To_American(txtStartDate.Text));

                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Correct Cheque Date Pattern is DD/MM/YYYY.');", true);
            return;
        }

        Configuration.GetAccountHead();
        DataTable dtVoucher = new DataTable();

        dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtVoucher.Columns.Add("DEBIT", typeof(decimal));
        dtVoucher.Columns.Add("CREDIT", typeof(decimal));
        dtVoucher.Columns.Add("REMARKS", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));

        DataRow[] drConfig = null;
        DataTable dtConfig = (DataTable)Session["dtConfig"];

        drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.AccountPayable + "'");
        int PayableAccount = Convert.ToInt32(drConfig[0]["VALUE"].ToString());


        DataRow dr = dtVoucher.NewRow();
        dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpBankAccount.SelectedItem.Value.ToString());
        dr["REMARKS"] = DrpAccountType.SelectedItem.Text + " Paid to " + ddlCustomer.SelectedItem.Text;
        dr["DEBIT"] = 0;
        dr["CREDIT"] = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)) ;
        dr["Principal_Id"] = 0;
        dtVoucher.Rows.Add(dr);


        //Debit Side Entry
        
        DataRow dr1 = dtVoucher.NewRow();
        dr1["ACCOUNT_HEAD_ID"] = PayableAccount;
        dr1["DEBIT"] = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text));
        dr1["CREDIT"] = 0;
        dr1["Principal_Id"] = 0;
        dr1["REMARKS"] = DrpAccountType.SelectedItem.Text + " Paid to " + ddlCustomer.SelectedItem.Text;
        dtVoucher.Rows.Add(dr1);

        string MaxDocumentId = "";

        //using chequeDate as Transfer Date
        if (DrpAccountType.SelectedItem.Value.ToString() == "21")
        {
            MaxDocumentId = LController.SelectMaxVoucherId(Constants.CashPayment_Voucher, Convert.ToInt32(drpDistributor.SelectedItem.Value.ToString()), CurrentWorkDate);
            LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedItem.Value.ToString()), int.Parse(ddlCustomer.SelectedItem.Value.ToString()), MaxDocumentId, Constants.CashPayment_Voucher, CurrentWorkDate, Constants.Cash_Relization, Session["VoucherNo"].ToString(), DrpAccountType.SelectedItem.Text + " Voucher", Constants.DateNullValue, ChequeNo, dtVoucher, Convert.ToInt32(Session["UserID"]), "-1", Constants.DateNullValue, true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Voucher No : " + MaxDocumentId + " saved successfully');", true);
            PrintVoucher(MaxDocumentId);
        }
        else
        {
            MaxDocumentId = LController.SelectMaxVoucherId(Constants.Expanse_Voucher, Convert.ToInt32(drpDistributor.SelectedItem.Value.ToString()), CurrentWorkDate);
            LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedItem.Value.ToString()), int.Parse(ddlCustomer.SelectedItem.Value.ToString()), MaxDocumentId, Constants.Expanse_Voucher, CurrentWorkDate, Constants.Bank_Deposit, Session["VoucherNo"].ToString(), DrpAccountType.SelectedItem.Text + " Voucher", ChequeDate, ChequeNo, dtVoucher, Convert.ToInt32(Session["UserID"]), "-1", Constants.DateNullValue, true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Voucher No : " + MaxDocumentId + " saved successfully');", true);
            PrintVoucher(MaxDocumentId);
        }
    }

    private void PrintVoucher(string VoucherNo)
    {

        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        CORNBusinessLayer.Reports.crpVoucherView CrpReport = new CORNBusinessLayer.Reports.crpVoucherView();
        int VoucherType = Constants.IntNullValue;

        string VoucherType2 = "";
        string pVoucherType = "";

        if (DrpAccountType.SelectedItem.Value.ToString() == "18")
        {
            pVoucherType = "Bank Voucher";
            VoucherType = 17;
            VoucherType2 = "Bank Payment Voucher";
        }
        else if (DrpAccountType.SelectedItem.Value.ToString() == "21")
        {
            pVoucherType = "Cash Voucher";
            VoucherType = 24;
            VoucherType2 = "Cash Payment Voucher";
        }
        else
        {
            pVoucherType = "Bank Voucher";
            VoucherType = 17;
            VoucherType2 = "Bank Payment Voucher";
        }

        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedItem.Value.ToString()), VoucherNo, VoucherType, Constants.IntNullValue);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        CrpReport.SetParameterValue("VoucherType", pVoucherType);
        CrpReport.SetParameterValue("VoucherSubType", VoucherType2);
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);


    }

    private void PrintChequeVoucher(string ChequeProcessId)
    {

        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        CORNBusinessLayer.Reports.crpChequeVoucher CrpReport = new CORNBusinessLayer.Reports.crpChequeVoucher();


        string VoucherType2 = "";
        string pVoucherType = "";

        if (DrpAccountType.SelectedItem.Value.ToString() == "18")
        {
            pVoucherType = "Bank Voucher";
            VoucherType2 = "Bank Payment Voucher";
        }


        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedItem.Value.ToString()), null, Convert.ToInt32(ChequeProcessId), 0);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        CrpReport.SetParameterValue("VoucherType", pVoucherType);
        CrpReport.SetParameterValue("VoucherSubType", VoucherType2);
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    private void ClearAll()
    {
        //txtChequeNo.Text = "";
        txtAmount.Text = "";
        txtBankName.Text = "";
        txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        txtReceivedDate.Text = "";
        txtRemarks.Text = "";
        txtTax.Text = "";
        txtDiscount.Text = "";
        ChkIsDiscount.Checked = false;
        DrpStatus.SelectedIndex = 0;
        DrpStatus.Enabled = false;
    }
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