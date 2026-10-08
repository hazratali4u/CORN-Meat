using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;


public partial class Forms_frmChequeEntryVendor : System.Web.UI.Page
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
            DrpStatus.Items.Add(new DevExpress.Web.ListEditItem("Issue", "527"));
            DrpStatus.Items.Add(new DevExpress.Web.ListEditItem("Clear", "529"));
            DrpStatus.Items.Add(new DevExpress.Web.ListEditItem("Bounce", "530"));
            DrpStatus.Items.Add(new DevExpress.Web.ListEditItem("Cancel", "560"));

            DrpStatus.SelectedIndex = 0;
            LoadAccountHead();
            LoadDistributor();
            LoadAccountDetail();
            LoadData();
            SelectCreditInvoice();
            LoadReceviedCheque();

            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LoadCheque(2);
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
            drpCheque.Visible = false;
            lblChequeDate.Visible = false;
            txtStartDate.Visible = false;
            ibtnStartDate.Visible = false;
            DrpStatus.Visible = false;
            lblStatus.Visible = false;
            GrdCO.Visible = true;
            GrdCheque.Visible = false;

        }
        else if (pAccountType == "33")
        {
            lblChequeNo.Visible = false;
            drpCheque.Visible = false;
            lblChequeDate.Visible = true;
            txtStartDate.Visible = true;
            ibtnStartDate.Visible = true;
            DrpStatus.Visible = false;
            lblStatus.Visible = false;
            lblChequeDate.Text = "Transfer Date";
            GrdCO.Visible = true;
            GrdCheque.Visible = false;

        }
        else
        {

            lblChequeNo.Visible = true;
            drpCheque.Visible = true;
            lblChequeDate.Visible = true;
            txtStartDate.Visible = true;
            ibtnStartDate.Visible = true;
            DrpStatus.Visible = true;
            lblStatus.Visible = true;
            lblChequeDate.Text = "Cheque Date";
            GrdCO.Visible = false;
            GrdCheque.Visible = true;

        }
    }

    #region Load
    public void LoadCheque(int TYPE)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = _bnkController.SelectCheckBook(Constants.IntNullValue, Convert.ToInt32(DrpBankAccount.SelectedItem.Value.ToString()), null, null, null, true, Constants.DateNullValue, TYPE);
            clsWebFormUtil.FillDxComboBoxList(this.drpCheque, dt, 0, 1, true);

            if (dt.Rows.Count > 0)
            {
                drpCheque.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }

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

        GrdCredit.DataSource = null;
        GrdCredit.DataBind();
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable dtVendor = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, CurrentWorkDate);
        clsWebFormUtil.FillDxComboBoxList(DrpVendor, dtVendor, 0, 1, true);
        if (dtVendor.Rows.Count > 0)
        {
            DrpVendor.SelectedIndex = 0;
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
        GrdCheque.DataSource = null;
        GrdCheque.DataBind();

        GrdCO.DataSource = null;
        GrdCO.DataBind();

        decimal chqAmount = 0;
        if (DrpAccountType.SelectedItem.Value.ToString() == "21" || DrpAccountType.SelectedItem.Value.ToString() == "33")
        {
            if (drpDistributor.Items.Count > 0)
            {
                DataTable dt = LController.VendorBankCashTransction(int.Parse(drpDistributor.SelectedItem.Value.ToString()), Constants.IntNullValue, int.Parse(DrpAccountType.SelectedItem.Value.ToString()),
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

        else
        {
            if (DrpStatus.SelectedItem.Value.ToString() != Constants.Cheque_Clear.ToString())
            {
                DataTable dt = null;
                if (DrpVendor.Items.Count > 0)
                {
                    dt = CController.SelectVendorChequeEntry(int.Parse(DrpStatus.SelectedItem.Value.ToString()), CurrentWorkDate, int.Parse(drpDistributor.SelectedItem.Value.ToString()), int.Parse(DrpVendor.SelectedItem.Value.ToString()), DrpAccountType.SelectedIndex);
                    Session.Add("dt", dt);

                    GrdCheque.DataSource = dt;
                    GrdCheque.DataBind();

                }
                if (dt != null)
                {
                    foreach (DataRow gvr in dt.Rows)
                    {
                        chqAmount += Convert.ToDecimal(gvr["CHEQUE_AMOUNT"]);
                    }
                }
                lblTotalAmount.Text = string.Format("{0:0.00}", chqAmount);

                foreach (GridViewRow gvr in GrdCredit.Rows)
                {
                    CheckBox row = gvr.FindControl("ChbIsAssigned") as CheckBox;
                    if (row.Enabled)
                    {
                        break;
                    }
                    else
                    {
                        row.Enabled = true;
                    }
                }
            }
            else
            {
                ///Load Cash Realization Detail
                if (Convert.ToInt32(DrpStatus.SelectedItem.Value.ToString()) < 530)
                {
                    DataSet ds = CController.SelectVendorChequeEntry(int.Parse(DrpStatus.SelectedItem.Value.ToString()), CurrentWorkDate, int.Parse(drpDistributor.SelectedItem.Value.ToString()), DrpAccountType.SelectedIndex);
                    DataTable dt2 = ds.Tables[1];
                    if (dt2.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dt2.Rows)
                        {
                            chqAmount += Convert.ToDecimal(dr[2].ToString());
                        }
                        lblTotalAmount.Text = string.Format("{0:0.00}", chqAmount);
                    }
                }
                ////////////////////////////////////////////////
                DataTable dt = CController.SelectVendorChequeEntry(int.Parse(DrpStatus.SelectedItem.Value.ToString()), CurrentWorkDate, int.Parse(drpDistributor.SelectedItem.Value.ToString()), Constants.IntNullValue, DrpAccountType.SelectedIndex);
                Session.Add("dt", dt);

                GrdCheque.DataSource = dt;
                GrdCheque.DataBind();

                foreach (GridViewRow gvr in GrdCheque.Rows)
                {
                    LinkButton row = gvr.FindControl("btnEdit") as LinkButton;
                    row.Enabled = false;
                }
                foreach (GridViewRow gvr in GrdCredit.Rows)
                {
                    CheckBox row = gvr.FindControl("ChbIsAssigned") as CheckBox;
                    row.Checked = false;
                    row.Enabled = false;
                }
            }
            if (DrpStatus.SelectedItem.Value.ToString() != Constants.Cheque_Pending.ToString())
            {
                foreach (GridViewRow gvr in GrdCredit.Rows)
                {
                    CheckBox row = gvr.FindControl("ChbIsAssigned") as CheckBox;
                    row.Checked = false;
                    row.Enabled = false;
                }
            }
        }
    }

    private void checkDuplication()
    {
        DataTable dt = (DataTable)Session["dt"];

        DataRow[] foundRows = dt.Select("VENDOR_ID = '" + DrpVendor.SelectedItem.Value.ToString() + "' and CHEQUE_NO='" + drpCheque.SelectedItem.Text + "'");

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

        if (DrpVendor.Items.Count > 0)
        {
            DataTable dtCredit = CDC.SelectCreditPendingInvoice2(int.Parse(drpDistributor.SelectedItem.Value.ToString()), int.Parse(DrpVendor.SelectedItem.Value.ToString()), 0);
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
        
        LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentID), PayableAccount, int.Parse(drpDistributor.SelectedItem.Value.ToString()), 0, decimal.Parse(dc.chkNull_0(txtAmount.Text)),
                       CurrentWorkDate, remarks, DateTime.Now, int.Parse(DrpVendor.SelectedItem.Value.ToString()), Constants.Document_Purchase,
                       null, int.Parse(Session["UserId"].ToString()), 0, "0", int.Parse(DrpAccountType.SelectedItem.Value.ToString()), "", "", chqDate);

        LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentID), long.Parse(DrpBankAccount.SelectedItem.Value.ToString()), int.Parse(drpDistributor.SelectedItem.Value.ToString()), decimal.Parse(dc.chkNull_0(txtAmount.Text)), 0,
                       CurrentWorkDate, remarks, DateTime.Now, int.Parse(DrpVendor.SelectedItem.Value.ToString()), Constants.Document_Purchase,
                       null, int.Parse(Session["UserId"].ToString()), 0, "0", int.Parse(DrpAccountType.SelectedItem.Value.ToString()), "", "", chqDate);
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
            remarks = "Chq# " + drpCheque.SelectedItem.Text + ", " + DrpBankAccount.SelectedItem.Text + ", " + txtRemarks.Text;
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
            ChequeNo = drpCheque.SelectedItem.Text;
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
                    LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), PayableAccount, int.Parse(drpDistributor.SelectedItem.Value.ToString()), 0, OfferAmount,
                    CurrentWorkDate, remarks, DateTime.Now, int.Parse(DrpVendor.SelectedItem.Value.ToString()), Constants.Document_Purchase,
                    ChequeNo, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]), manualNo, int.Parse(DrpAccountType.SelectedItem.Value.ToString()), "", "", ChequeDate);

                    LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedItem.Value.ToString()), int.Parse(drpDistributor.SelectedItem.Value.ToString()), OfferAmount, 0,
                     CurrentWorkDate, remarks, DateTime.Now, int.Parse(DrpVendor.SelectedItem.Value.ToString()), Constants.Document_Purchase,
                     ChequeNo, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]), manualNo, int.Parse(DrpAccountType.SelectedItem.Value.ToString()), "", "", ChequeDate);

                    OfferAmount = decimal.Parse(dr.Cells[4].Text) - OfferAmount;
                    LController.UpdatePurchaseMaster(Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]), int.Parse(drpDistributor.SelectedItem.Value.ToString()), OfferAmount);
                    break;
                }
                else if (decimal.Parse(dr.Cells[4].Text) <= OfferAmount)
                {
                    realizeAmount += decimal.Parse(dr.Cells[4].Text);
                    LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), PayableAccount, int.Parse(drpDistributor.SelectedItem.Value.ToString()), 0, decimal.Parse(dr.Cells[4].Text),
                    CurrentWorkDate, remarks, DateTime.Now, int.Parse(DrpVendor.SelectedItem.Value.ToString()), Constants.Document_Purchase,
                    ChequeNo, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]), manualNo, int.Parse(DrpAccountType.SelectedItem.Value.ToString()), "", "", ChequeDate);

                    LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedItem.Value.ToString()), int.Parse(drpDistributor.SelectedItem.Value.ToString()), decimal.Parse(dr.Cells[4].Text), 0,
                    CurrentWorkDate, remarks, DateTime.Now, int.Parse(DrpVendor.SelectedItem.Value.ToString()), Constants.Document_Purchase,
                    ChequeNo, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]), manualNo, int.Parse(DrpAccountType.SelectedItem.Value.ToString()), "", "", ChequeDate);

                    OfferAmount = OfferAmount - decimal.Parse(dr.Cells[4].Text);
                    LController.UpdatePurchaseMaster(long.Parse(dr.Cells[1].Text), int.Parse(drpDistributor.SelectedItem.Value.ToString()), 0);
                }
            }
        }

        //Advance Entry
        if (realizeAmount < decimal.Parse(txtAmount.Text))
        {

            LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), PayableAccount, int.Parse(drpDistributor.SelectedItem.Value.ToString()), 0, OfferAmount,
                                  CurrentWorkDate, remarks + " (Advance)", DateTime.Now, int.Parse(DrpVendor.SelectedItem.Value.ToString()), Constants.Document_Purchase,
                                  ChequeNo, int.Parse(Session["UserId"].ToString()), 0, "0", int.Parse(DrpAccountType.SelectedItem.Value.ToString()), "", "", ChequeDate);

            LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedItem.Value.ToString()), int.Parse(drpDistributor.SelectedItem.Value.ToString()), OfferAmount, 0,
                                  CurrentWorkDate, remarks + " (Advance)", DateTime.Now, int.Parse(DrpVendor.SelectedItem.Value.ToString()), Constants.Document_Purchase,
                                  ChequeNo, int.Parse(Session["UserId"].ToString()), 0, "0", int.Parse(DrpAccountType.SelectedItem.Value.ToString()), "", "", ChequeDate);
        }
    }

    protected void GrdCheque_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ChequeEntryController CController = new ChequeEntryController();
        CController.DeleteChequeEntry(long.Parse(GrdCheque.Rows[e.RowIndex].Cells[0].Text));
        LoadReceviedCheque();
    }

    protected void GrdCheque_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            hfChequeNo.Value = "";
            ChequeEntryController Ccontroller = new ChequeEntryController();
            HFChqueProcessId.Value = GrdCheque.Rows[e.NewEditIndex].Cells[0].Text;
            ddlAccountHead.Value = GrdCheque.Rows[e.NewEditIndex].Cells[13].Text;
            DrpVendor.Value = GrdCheque.Rows[e.NewEditIndex].Cells[1].Text;
            DrpBankAccount.Value = GrdCheque.Rows[e.NewEditIndex].Cells[9].Text;
            LoadCheque(5);
            drpCheque.SelectedIndex = drpCheque.Items.IndexOf(drpCheque.Items.FindByText(GrdCheque.Rows[e.NewEditIndex].Cells[3].Text.ToString()));
            txtStartDate.Text = GrdCheque.Rows[e.NewEditIndex].Cells[4].Text;
            txtReceivedDate.Text = GrdCheque.Rows[e.NewEditIndex].Cells[5].Text;
            txtAmount.Text = GrdCheque.Rows[e.NewEditIndex].Cells[6].Text;
            txtRemarks.Text = GrdCheque.Rows[e.NewEditIndex].Cells[7].Text.Replace("&nbsp;", "");
            ChkIsDiscount.Checked = Convert.ToBoolean(GrdCheque.Rows[e.NewEditIndex].Cells[11].Text);
            txtDiscount.Text = GrdCheque.Rows[e.NewEditIndex].Cells[10].Text.Replace("&nbsp;", "");
            txtTax.Text = GrdCheque.Rows[e.NewEditIndex].Cells[12].Text.Replace("&nbsp;", "");
            btnSave.Text = "Update";
            SelectCreditInvoice();
            if (DrpAccountType.SelectedItem.Value.ToString() == "18")
            {
                DataTable dt = Ccontroller.SelectChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), 0);
                foreach (GridViewRow dr in GrdCredit.Rows)
                {
                    foreach (DataRow dbr in dt.Rows)
                    {
                        if (Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]) == Convert.ToInt64(dbr["SALE_INVOICE_ID"]))
                        {
                            CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                            chRelized.Checked = true;
                        }
                    }
                }
            }
            hfChequeNo.Value = GrdCheque.Rows[e.NewEditIndex].Cells[3].Text.ToString();
            DrpStatus.Enabled = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Invoice not found for selected cheque');", true);
        }
    }

    #region Sel/Index Change

    protected void DrpVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (Session["dtVendor"] != null)
        //{
        //    DataTable dt = (DataTable)Session["dtVendor"];
        //    DataRow[] foundRows = dt.Select("VENDOR_ID  = '" + DrpVendor.SelectedValue + "'");
        //    if (foundRows.Length > 0)
        //    {
        //        hfVendorType.Value=Convert.ToString(foundRows[0]["vendorType"]);
        //    }
        //}
        SelectCreditInvoice();
        LoadReceviedCheque();
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {

        SelectCreditInvoice();
        LoadReceviedCheque();
    }

    protected void DrpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            LoadReceviedCheque();
        }
    }
    protected void DrpBankAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                LoadReceviedCheque();
            }
            LoadCheque(2);
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
                                        ChequeNo = drpCheque.SelectedItem.Text;
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
                            if (DrpStatus.SelectedIndex == 0)
                            {
                                short chkDiscount = 0;
                                if (ChkIsDiscount.Checked)
                                {
                                    chkDiscount = 1;
                                }
                                HFChqueProcessId.Value = CController.InsertChequeEntry(int.Parse(drpDistributor.SelectedItem.Value.ToString()), int.Parse(DrpVendor.SelectedItem.Value.ToString()), 0, drpCheque.SelectedItem.Text, txtBankName.Text, ChequeDate,
                                    CurrentWorkDate, Constants.DateNullValue, Constants.DateNullValue, Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)), int.Parse(DrpStatus.SelectedItem.Value.ToString()), DateTime.Now, DrpAccountType.SelectedIndex, "", txtRemarks.Text, long.Parse(DrpBankAccount.SelectedItem.Value.ToString()), 1
                                    , Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)), chkDiscount, Convert.ToDecimal(dc.chkNull_0(txtTax.Text)), Convert.ToInt32(ddlAccountHead.SelectedItem.Value.ToString()));
                                foreach (GridViewRow dr in GrdCredit.Rows)
                                {
                                    CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                                    if (chRelized.Checked == true)
                                    {
                                        CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]));
                                    }
                                }
                                PrintChequeVoucher(HFChqueProcessId.Value);
                            }
                        }
                    }
                    else
                    {
                        DataTable dt = _bnkController.SelectCheckBook(Convert.ToInt32(drpCheque.SelectedItem.Value.ToString()), Constants.IntNullValue, null, null, null, true, Constants.DateNullValue, 4);
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dt.Rows[0]["STATUS"].ToString()) == 0 && DrpStatus.SelectedIndex != 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Selected cheque is not issued yet');", true);
                                return;
                            }
                        }
                        if (int.Parse(DrpStatus.SelectedItem.Value.ToString()) == Constants.Cheque_Pending && txtReceivedDate.Text == CurrentWorkDate.ToString("dd/MM/yyyy"))
                        {
                            short chkDiscount = 0;
                            if (ChkIsDiscount.Checked)
                            {
                                chkDiscount = 1;
                            }

                            CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), int.Parse(drpDistributor.SelectedItem.Value.ToString()), int.Parse(DrpVendor.SelectedItem.Value.ToString()), 0, drpCheque.SelectedItem.Text, txtBankName.Text, ChequeDate, CurrentWorkDate, Constants.DateNullValue, Constants.DateNullValue,
                               Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)), int.Parse(DrpStatus.SelectedItem.Value.ToString()), Constants.DateNullValue, "", DrpAccountType.SelectedIndex, txtRemarks.Text, int.Parse(DrpBankAccount.SelectedItem.Value.ToString())
                               , Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)), chkDiscount, Convert.ToDecimal(dc.chkNull_0(txtTax.Text)), Convert.ToInt32(ddlAccountHead.SelectedItem.Value.ToString()), Convert.ToString(hfChequeNo.Value));
                            CController.SelectChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), 1);
                            foreach (GridViewRow dr in GrdCredit.Rows)
                            {
                                CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                                if (chRelized.Checked == true)
                                {
                                    CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]));
                                }
                            }
                        }
                        else if (int.Parse(DrpStatus.SelectedItem.Value.ToString()) == Constants.Cheque_Bons || int.Parse(DrpStatus.SelectedItem.Value.ToString()) == Constants.Cheque_Cancel)
                        {
                            CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, drpCheque.SelectedItem.Text, null, Constants.DateNullValue, Constants.DateNullValue, Constants.DateNullValue, CurrentWorkDate,
                                                             Constants.DecimalNullValue, int.Parse(DrpStatus.SelectedItem.Value.ToString()), Constants.DateNullValue, "", DrpAccountType.SelectedIndex, txtRemarks.Text, long.Parse(DrpBankAccount.SelectedItem.Value.ToString())
                                                             , Constants.DecimalNullValue, Constants.ShortNullValue, Constants.DecimalNullValue, Constants.IntNullValue, Convert.ToString(hfChequeNo.Value));
                        }
                        else if (int.Parse(DrpStatus.SelectedItem.Value.ToString()) == Constants.Cheque_Clear)
                        {
                            CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, drpCheque.SelectedItem.Text, null, Constants.DateNullValue, Constants.DateNullValue, Constants.DateNullValue, CurrentWorkDate,
                                                             Constants.DecimalNullValue, int.Parse(DrpStatus.SelectedItem.Value.ToString()), Constants.DateNullValue, "", DrpAccountType.SelectedIndex, txtRemarks.Text, long.Parse(DrpBankAccount.SelectedItem.Value.ToString())
                                                             , Constants.DecimalNullValue, Constants.ShortNullValue, Constants.DecimalNullValue, Constants.IntNullValue, Convert.ToString(hfChequeNo.Value));
                            CController.SelectChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), 1);
                            foreach (GridViewRow dr in GrdCredit.Rows)
                            {
                                CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                                if (chRelized.Checked == true)
                                {
                                    CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]));
                                }
                            }
                            ChequeRealization(CurrentWorkDate);
                            if (Convert.ToBoolean(Session["IsFinanceSetting"]))
                            {
                                string ChequeNo = null;
                                if (DrpAccountType.SelectedIndex == 0)
                                {
                                    ChequeNo = drpCheque.SelectedItem.Text;
                                }
                                InsertGL(ChequeNo, CurrentWorkDate);
                            }
                            Session.Remove("VoucherNo");
                            SelectCreditInvoice();
                        }
                    }
                    LoadCheque(2);
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
        LoadCheque(2);
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dt"];
        if (DrpAccountType.SelectedItem.Value.ToString() == "18")
        {
            switch (ddSearchType.SelectedIndex)
            {
                case 1:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedItem.Value.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 2:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedItem.Value.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 3:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedItem.Value.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 4:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedItem.Value.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                default:
                    dt.DefaultView.RowFilter = "CHEQUE_NO" + " like '%" + "" + "%'";
                    break;
            }
            GrdCheque.DataSource = dt.DefaultView;
            GrdCheque.DataBind();
        }
        else
        {
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
        dr["REMARKS"] = DrpBankAccount.SelectedItem.Text + " Paid to " + DrpVendor.SelectedItem.Text;
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
        dr1["REMARKS"] = DrpBankAccount.SelectedItem.Text + " Paid to " + DrpVendor.SelectedItem.Text;
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
        dr["REMARKS"] = DrpAccountType.SelectedItem.Text + " Paid to " + DrpVendor.SelectedItem.Text;
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
        dr1["REMARKS"] = DrpAccountType.SelectedItem.Text + " Paid to " + DrpVendor.SelectedItem.Text;
        dtVoucher.Rows.Add(dr1);

        string MaxDocumentId = "";

        //using chequeDate as Transfer Date
        if (DrpAccountType.SelectedItem.Value.ToString() == "21")
        {
            MaxDocumentId = LController.SelectMaxVoucherId(Constants.CashPayment_Voucher, Convert.ToInt32(drpDistributor.SelectedItem.Value.ToString()), CurrentWorkDate);

            LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedItem.Value.ToString()), int.Parse(DrpVendor.SelectedItem.Value.ToString()), MaxDocumentId, Constants.CashPayment_Voucher, CurrentWorkDate, Constants.Cash_Relization, Session["VoucherNo"].ToString(), DrpAccountType.SelectedItem.Text + " Voucher", Constants.DateNullValue, ChequeNo, dtVoucher, Convert.ToInt32(Session["UserID"]), "-1", Constants.DateNullValue, true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Voucher No : " + MaxDocumentId + " saved successfully');", true);

            PrintVoucher(MaxDocumentId);
        }
        else
        {
            MaxDocumentId = LController.SelectMaxVoucherId(Constants.Expanse_Voucher, Convert.ToInt32(drpDistributor.SelectedItem.Value.ToString()), CurrentWorkDate);

            LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedItem.Value.ToString()), int.Parse(DrpVendor.SelectedItem.Value.ToString()), MaxDocumentId, Constants.Expanse_Voucher, CurrentWorkDate, Constants.Bank_Deposit, Session["VoucherNo"].ToString(), DrpAccountType.SelectedItem.Text + " Voucher", ChequeDate, ChequeNo, dtVoucher, Convert.ToInt32(Session["UserID"]), "-1", Constants.DateNullValue, true);

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