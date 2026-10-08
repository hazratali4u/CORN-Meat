using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

public partial class Forms_frmVoucherEditing : System.Web.UI.Page
{
    DataTable dtVoucher;

    private void CreatTable()
    {
        dtVoucher = new DataTable();
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Account_Code", typeof(string));
        dtVoucher.Columns.Add("Account_Name", typeof(string));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));
        dtVoucher.Columns.Add("Principal", typeof(string));
        Session.Add("dtVoucher", dtVoucher);

    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
        if (drpDistributor.Items.Count > 0)
        {
            drpDistributor.SelectedValue = Session["DistributorId"].ToString();
        }
    }
    private void LoadEditVoucher()
    {
        LedgerController LController = new LedgerController();
        DataTable dtVoucher = (DataTable)Session["dtVoucher"];

        DataTable dt = LController.SelectVoucherNo(Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(Session["VoucherTypeId"]), int.Parse(Session["UserId"].ToString()), lblVoucherNo.Text);
        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            if (dt.Rows[0]["VOUCHER_TYPE_ID"].ToString() == "14" || dt.Rows[0]["VOUCHER_TYPE_ID"].ToString() == "24")
            {
                DrpVoucherSubType.Items.Add(new ListItem("Cash Receipt Voucher", "14"));
                DrpVoucherSubType.Items.Add(new ListItem("Cash Payment Voucher", "24"));

                DrpVoucherType.SelectedValue = "14";
                DrpVoucherSubType.SelectedValue = dt.Rows[0]["VOUCHER_TYPE_ID"].ToString();

                spanlblChequeNo.Visible = false;
                spanlblDueDate.Visible = false;
                spanlblSlipNo.Visible = false;

                lblDueDate.Visible = false;
                txtDueDate.Visible = false;
                lblChequeNo.Visible = false;
                txtChequeNo.Visible = false;
                lblSlipNo.Visible = false;
                txtSlipNo.Visible = false;

                spanPayees.Visible = true;
                lblPayees.Visible = true;
                txtpayeesName.Visible = true;

                spanRemarks.Visible = true;
                lblRemarks.Visible = true;
                txtRemarks.Visible = true;
            }
            else if (dt.Rows[0]["VOUCHER_TYPE_ID"].ToString() == "15" || dt.Rows[0]["VOUCHER_TYPE_ID"].ToString() == "17")
            {
                DrpVoucherSubType.Items.Add(new ListItem("Bank Receipt Voucher", "15"));
                DrpVoucherSubType.Items.Add(new ListItem("Bank Payment Voucher", "17"));

                DrpVoucherType.SelectedValue = "15";
                DrpVoucherSubType.SelectedValue = dt.Rows[0]["VOUCHER_TYPE_ID"].ToString();

                spanPayees.Visible = true;
                lblPayees.Visible = true;
                txtpayeesName.Visible = true;

                spanRemarks.Visible = true;
                lblRemarks.Visible = true;
                txtRemarks.Visible = true;
            }
            else if (dt.Rows[0]["VOUCHER_TYPE_ID"].ToString() == "16")
            {
                DrpVoucherType.SelectedValue = "16";

                spanlblChequeNo.Visible = false;
                spanlblDueDate.Visible = false;
                spanlblSlipNo.Visible = false;

                lblDueDate.Visible = false;
                txtDueDate.Visible = false;
                SpanVsubtype.Visible = false;
                lblVouchetSubType.Visible = false;
                DrpVoucherSubType.Visible = false;
                spanPaymentMode.Visible = false;
                lblPyamentMode.Visible = false;
                DrpPaymentMode.Visible = false;
                spanDdlBank.Visible = false;
                lblBank.Visible = false;
                ddlBank.Visible = false;
                lblChequeNo.Visible = false;
                txtChequeNo.Visible = false;
                lblSlipNo.Visible = false;
                txtSlipNo.Visible = false;

                spanPayees.Visible = true;
                lblPayees.Visible = true;
                txtpayeesName.Visible = true;

                spanRemarks.Visible = true;
                lblRemarks.Visible = true;
                txtRemarks.Visible = true;
            }
            else
            {
                DrpVoucherType.SelectedValue = "812";

                spanlblChequeNo.Visible = false;
                spanlblDueDate.Visible = false;
                spanlblSlipNo.Visible = false;

                lblDueDate.Visible = false;
                txtDueDate.Visible = false;
                SpanVsubtype.Visible = false;
                lblVouchetSubType.Visible = false;
                DrpVoucherSubType.Visible = false;
                spanPaymentMode.Visible = false;
                lblPyamentMode.Visible = false;
                DrpPaymentMode.Visible = false;
                spanDdlBank.Visible = false;
                lblBank.Visible = false;
                ddlBank.Visible = false;
                lblChequeNo.Visible = false;
                txtChequeNo.Visible = false;
                lblSlipNo.Visible = false;
                txtSlipNo.Visible = false;

                spanPayees.Visible = false;
                lblPayees.Visible = false;
                txtpayeesName.Visible = false;

                spanRemarks.Visible = false;
                lblRemarks.Visible = false;
                txtRemarks.Visible = false;
            }

            txtVoucherDate.Text = DateTime.Parse(dt.Rows[0]["VOUCHER_DATE"].ToString()).ToString("dd-MMM-yyyy");
            if (dt.Rows[0]["DUE_DATE"].ToString() != "")
            {
                txtDueDate.Text = DateTime.Parse(dt.Rows[0]["DUE_DATE"].ToString()).ToString("dd-MMM-yyyy");
            }
            txtChequeNo.Text = dt.Rows[0]["CHEQUE_NO"].ToString();
            txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
            txtpayeesName.Text = dt.Rows[0]["PAYEES_NAME"].ToString();

            dtVoucher = LController.SelectVoucherDetail(int.Parse(drpDistributor.SelectedValue.ToString()), lblVoucherNo.Text, int.Parse(dt.Rows[0]["VOUCHER_TYPE_ID"].ToString()));

            Session.Add("dtVoucher", dtVoucher);
            GrdOrder.DataSource = dtVoucher;
            GrdOrder.DataBind();

            if (DrpVoucherSubType.SelectedIndex == 0)
            {
                if (DrpVoucherType.SelectedValue == "14")
                {
                    DrpPaymentMode.Items.Add(new ListItem("Cash", "19"));
                    DrpPaymentMode.Items.Add(new ListItem("Online Transfer", "30"));

                }
                else if (DrpVoucherType.SelectedValue == "15")
                {
                    DrpPaymentMode.Items.Add(new ListItem("Cheque", "18"));
                    DrpPaymentMode.Items.Add(new ListItem("Online Transfer", "30"));
                }

                //txtDebitAmount.Enabled = false;
                //txtCreditAmount.Enabled = true;
            }
            else if (DrpVoucherSubType.SelectedIndex == 1)
            {
                if (DrpVoucherType.SelectedValue == "14")
                {
                    DrpPaymentMode.Items.Add(new ListItem("Cash", "19"));
                    DrpPaymentMode.Items.Add(new ListItem("Online Transfer", "30"));
                }
                else if (DrpVoucherType.SelectedValue == "15")
                {
                    DrpPaymentMode.Items.Add(new ListItem("Cheque", "18"));
                    DrpPaymentMode.Items.Add(new ListItem("Pay Order", "21"));
                }

                //txtDebitAmount.Enabled = true;
                //txtCreditAmount.Enabled = false;
            }
            else
            {
                DrpPaymentMode.Items.Add(new ListItem("Cheque", "18"));
            }

            LoadAccountHead();
            DrpPaymentMode.SelectedValue = dt.Rows[0]["PAYMENT_MODE"].ToString();
        }

    }
    /// <summary>
    /// Loads Account Heads To ListBox
    /// </summary>
    private void LoadAccountDetail()
    {
        // Load Account Head 
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue, 1);
        ddlAccountHead.DataSource = dtHead;
        ddlAccountHead.ValueField = "ACCOUNT_HEAD_ID";
        ddlAccountHead.DataBind();
        Session.Add("dtHead", dtHead);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ClearControls();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");
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
            LoadAccountDetail();
            if (ddlAccountHead.Items.Count > 0)
            {
                ddlAccountHead.SelectedIndex = 0;
            }
            LoadDistributor();

            CreatTable();
            lblVoucherNo.Text = Session["VoucherNo"].ToString();
            LoadAccountHead();
            if (int.Parse(DrpVoucherType.SelectedValue.ToString()) == Constants.Journal_Voucher)
            {
                lblChequeNo.Text = "Invoice No";
            }

            LoadEditVoucher();

            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            RowId.Value = "-1";
            Session.Remove("VoucherNo");
            Session.Remove("DistributorId");
            Session.Remove("VoucherTypeId");
            GetTotal();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string SelectedValue = ddlAccountHead.Value.ToString();
        this.LoadAccountDetail();
        ddlAccountHead.Value = SelectedValue;
        DataControl dc = new DataControl();
        dtVoucher = (DataTable)Session["dtVoucher"];

        if (Convert.ToInt32(RowId.Value) >= 0)
        {
            DataRow dr = dtVoucher.Rows[Convert.ToInt32(RowId.Value)];
            dr["ACCOUNT_HEAD_ID"] = ddlAccountHead.Value;
            dr["ACCOUNT_CODE"] = "";
            dr["ACCOUNT_NAME"] = ddlAccountHead.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtDebitAmount.Text));
            dr["CREDIT"] = decimal.Parse(dc.chkNull_0(txtCreditAmount.Text));
            dr["REMARKS"] = txtAccountDes.Text;
            dr["Principal"] = "";
            dr["Principal_id"] = "0";


        }
        else
        {
            DataRow dr = dtVoucher.NewRow();
            dr["ACCOUNT_HEAD_ID"] = ddlAccountHead.Value;
            dr["ACCOUNT_CODE"] = "";
            dr["ACCOUNT_NAME"] = ddlAccountHead.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtDebitAmount.Text));
            dr["CREDIT"] = decimal.Parse(dc.chkNull_0(txtCreditAmount.Text));
            dr["REMARKS"] = txtAccountDes.Text;
            dtVoucher.Rows.Add(dr);
            dr["Principal"] = "";
            dr["Principal_id"] = "0";

        }


        #region Clear Txtbox

        txtDebitAmount.Text = "";
        txtCreditAmount.Text = "";
        txtAccountDes.Text = "";
        RowId.Value = "-1";

        #endregion

        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        Session.Add("dtVoucher", dtVoucher);
        ScriptManager.GetCurrent(Page).SetFocus(ddlAccountHead);
        GetTotal();
    }
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string SelectedValue = ddlAccountHead.Value.ToString();
        this.LoadAccountDetail();
        ddlAccountHead.Value = SelectedValue;
        decimal TotalDebit = 0;
        decimal TotalCredit = 0;
        dtVoucher = (DataTable)Session["dtVoucher"];
        dtVoucher.Rows.RemoveAt(e.RowIndex);

        #region Set Total

        //foreach (DataRow dr in dtVoucher.Rows)
        //{
        //    TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
        //    TotalCredit += decimal.Parse(dr["CREDIT"].ToString());
        //}
        txtTotalDebit.Text = decimal.Round(TotalDebit, 2).ToString();
        txtTotalCredit.Text = decimal.Round(TotalCredit, 2).ToString();


        #endregion

        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        RowId.Value = "-1";
    }
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string SelectedValue = ddlAccountHead.Value.ToString();
        this.LoadAccountDetail();
        ddlAccountHead.Value = SelectedValue;
        RowId.Value = e.NewEditIndex.ToString();
        ddlAccountHead.Value = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;
        txtAccountDes.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text.Replace("&nbsp;", "");
        txtDebitAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[4].Text;
        txtCreditAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[5].Text;
    }
    protected void btnDone_Click(object sender, EventArgs e)
    {
        //if (IsDayClosed())
        //{
        //    UserController UserCtl = new UserController();

        //    UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
        //    Session.Clear();
        //    System.Web.Security.FormsAuthentication.SignOut();
        //    Response.Redirect("../Login.aspx");
        //}

        decimal TotalDebit = 0;
        decimal TotalCredit = 0;
        dtVoucher = (DataTable)Session["dtVoucher"];

        #region Set Total

        foreach (DataRow dr in dtVoucher.Rows)
        {
            TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
            TotalCredit += decimal.Parse(dr["CREDIT"].ToString());
        }

        #endregion

        if (TotalDebit == TotalCredit)
        {
            LedgerController mLController = new LedgerController();
            DataControl dc = new DataControl();
            DataTable dtHead = (DataTable)Session["dtHead"];
            DateTime Voucherdate;
            DateTime DueDate;

            int VoucherType = Constants.IntNullValue;

            if (DrpVoucherType.SelectedValue == "16" || DrpVoucherType.SelectedValue == "812")
            {
                VoucherType = Convert.ToInt32(DrpVoucherType.SelectedValue);
            }
            else
            {
                VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedValue);
            }

            Voucherdate = DateTime.Parse(txtVoucherDate.Text + " 00:00:00");

            if (txtDueDate.Text.Length > 0)
            {
                DueDate = DateTime.Parse(txtDueDate.Text + " 00:00:00");
            }
            else
            {
                DueDate = Constants.DateNullValue;
            }

            #region In Case of Bank & Cash Voucher

            if (ddlBank.SelectedItem.Text != "N/A")
            {
                decimal differnceAmt = TotalDebit - TotalCredit;

                if (differnceAmt > 0)
                {
                    DataRow dr1 = dtVoucher.NewRow();
                    DataRow[] foundSubRows = dtHead.Select("ACCOUNT_HEAD_ID  = " + ddlBank.SelectedValue.ToString());
                    dr1["Ledger_ID"] = Constants.LongNullValue.ToString();
                    dr1["ACCOUNT_HEAD_ID"] = foundSubRows[0]["ACCOUNT_HEAD_ID"];
                    dr1["ACCOUNT_CODE"] = foundSubRows[0]["ACCOUNT_CODE"];
                    dr1["ACCOUNT_NAME"] = foundSubRows[0]["ACCOUNT_NAME"];
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
                    DataRow[] foundSubRows = dtHead.Select("ACCOUNT_HEAD_ID  = " + ddlBank.SelectedValue.ToString());
                    dr1["Ledger_ID"] = Constants.LongNullValue.ToString();
                    dr1["ACCOUNT_HEAD_ID"] = foundSubRows[0]["ACCOUNT_HEAD_ID"];
                    dr1["ACCOUNT_CODE"] = foundSubRows[0]["ACCOUNT_CODE"];
                    dr1["ACCOUNT_NAME"] = foundSubRows[0]["ACCOUNT_NAME"];
                    dr1["REMARKS"] = txtAccountDes.Text;
                    dr1["CREDIT"] = 0;
                    dr1["DEBIT"] = -(differnceAmt);
                    dr1["Principal"] = "";
                    dr1["Principal_id"] = dtVoucher.Rows[0]["Principal_id"].ToString();
                    dtVoucher.Rows.Add(dr1);
                }
            }

            #endregion
            dtVoucher = (DataTable)Session["dtVoucher"];

            string MaxDocumentId = lblVoucherNo.Text;

            bool MResualt = mLController.Edit_Voucher(int.Parse(drpDistributor.SelectedValue.ToString()), 0, MaxDocumentId, VoucherType,
                   Voucherdate, Convert.ToInt32(dc.chkNull_0(DrpPaymentMode.SelectedValue)), txtpayeesName.Text, txtRemarks.Text, DueDate, txtChequeNo.Text, dtVoucher, int.Parse(Session["UserId"].ToString()), null, DueDate);

            if (MResualt == true)
            {
                Response.Write("<script language='javascript'> { window.close();}</script>");
                ClearControls();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occure');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Debit must equal To credit balance');", true);
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
    /// <summary>
    /// Loads Account Heads To Bank Combo
    /// </summary>
    private void LoadAccountHead()
    {
        CORNCommon.Classes.Configuration.GetAccountHead();
        if (int.Parse(DrpVoucherType.SelectedValue.ToString()) == 14 || int.Parse(DrpVoucherType.SelectedValue.ToString()) == 15)
        {
            AccountHeadController mAccountController = new AccountHeadController();
            DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(CORNCommon.Classes.Configuration.CashDefaultType), 1);
            //  DataTable dt = mAccountController.SelectAccountHeadLocation(Constants.AC_AccountHeadId, long.Parse(CORNCommon.Classes.Configuration.CashDefaultType), Convert.ToInt32(drpDistributor.SelectedValue));

            clsWebFormUtil.FillDropDownList(ddlBank, dt, 0, 10, true);
        }
        else if (int.Parse(DrpVoucherType.SelectedValue.ToString()) == 15)
        {
            AccountHeadController mAccountController = new AccountHeadController();
            DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(CORNCommon.Classes.Configuration.BankDefaultType), 1);
            clsWebFormUtil.FillDropDownList(ddlBank, dt, 0, 10, true);
        }
        else
        {
            ddlBank.Items.Clear();
            ddlBank.Items.Add(new ListItem("N/A", Constants.IntNullValue.ToString()));
        }
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

    public void ClearControls()
    {
        txtTotalCredit.Text = "0";
        txtTotalDebit.Text = "0";
    }
    public void GetTotal()
    {
        decimal TotalDebit = 0;
        decimal TotalCredit = 0;
        foreach (GridViewRow dr in GrdOrder.Rows)
        {
            TotalDebit += decimal.Parse(dr.Cells[4].Text.ToString());
            TotalCredit += decimal.Parse(dr.Cells[5].Text.ToString());
        }
        txtTotalDebit.Text = decimal.Round(TotalDebit, 2).ToString();
        txtTotalCredit.Text = decimal.Round(TotalCredit, 2).ToString();

        GrdOrder.FooterRow.Cells[4].Text = txtTotalDebit.Text;
        GrdOrder.FooterRow.Cells[5].Text = txtTotalCredit.Text;
    }
}