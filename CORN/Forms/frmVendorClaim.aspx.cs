using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

public partial class Forms_frmVendorClaim : System.Web.UI.Page
{
    DataTable ClaimedSKU;
    readonly LedgerController LController = new LedgerController();

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

            LoadDistributor();
            LoadClaimType();
            LoadAccountHead();

            CreatTableValue();

            LoadVendor();
            LoadGrid();

            btnAddNew.Attributes.Add("onclick", "return ValidateValueForm();");
            ScriptManager.GetCurrent(Page).SetFocus(drpDistributor);
            lblRowId.Text = "-1";
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

    private void LoadClaimType()
    {
        RbdClaimType.Items.Add(new ListItem("Credit", Constants.CreditClaim.ToString()));
        RbdClaimType.Items.Add(new ListItem("Debit", Constants.DebitClaim.ToString()));
        RbdClaimType.SelectedIndex = 0;
    }

    private void LoadAccountHead()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue, 1);
        clsWebFormUtil.FillDxComboBoxList(DrpAccountHead, dt, 0, 10, true);
        if (dt.Rows.Count > 0)
        {
            DrpAccountHead.SelectedIndex = 0;
        }
    }

    private void CreatTableValue()
    {
        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Account_Code", typeof(string));
        dtVoucher.Columns.Add("Account_Name", typeof(string));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        Session.Add("dtVoucher", dtVoucher);
        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();

    }

    private void LoadVendor()
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
        DrpVendor.Items.Clear();
        if (drpDistributor.Items.Count > 0)
        {
            var PController = new SKUPriceDetailController();
            DataTable dtVendor = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, CurrentWorkDate);
            clsWebFormUtil.FillDxComboBoxList(DrpVendor, dtVendor, 0, 1);

            if (dtVendor.Rows.Count > 0)
            {
                DrpVendor.SelectedIndex = 0;
            }
        }
    }

    private void LoadGrid()
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
        if (drpDistributor.Items.Count > 0)
        {
            var lController = new LedgerController();
            var dt = lController.SelectClaimDetail(int.Parse(drpDistributor.SelectedItem.Value.ToString()), int.Parse(RbdClaimType.SelectedValue),
                CurrentWorkDate, CurrentWorkDate, 1);
            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();
            GetTotal();
        }
    }


    private void ClearGrd_Order()
    {
        txtRemarks.Text = "";
        txtAmount.Text = "";
        btnAddNew.Text = "Save";

    }

    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        LedgerController LController = new LedgerController();
        DataControl dc = new DataControl();

        //uPDATE  VENDOR lEDGER,gl MASTER DETAIL

        LController.UpdateVendorLedger(int.Parse(drpDistributor.SelectedItem.Value.ToString()), int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[3].Text)),
        long.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[6].Text)), Convert.ToString(GrdOrder.Rows[e.RowIndex].Cells[9].Text), decimal.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[7].Text)), 2);

        ClearGrd_Order();
        LoadGrid();

    }
    private void EnableDisable(bool flag)
    {
        if (flag == true)
        {
            drpDistributor.Enabled = false;

            DrpVendor.Enabled = false;
        }
        else
        {
            drpDistributor.Enabled = true;
            DrpVendor.Enabled = true;
        }
    }
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        URowId.Value = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;

        LoadVendor();
        DrpVendor.Value = Convert.ToString(GrdOrder.Rows[e.NewEditIndex].Cells[2].Text);
        DrpAccountHead.Value = Convert.ToString(GrdOrder.Rows[e.NewEditIndex].Cells[1].Text);
        txtAmount.Text = Convert.ToString(GrdOrder.Rows[e.NewEditIndex].Cells[7].Text);
        txtRemarks.Text = Convert.ToString(GrdOrder.Rows[e.NewEditIndex].Cells[8].Text.Replace("&nbsp;", ""));

        VoucherNo.Value = GrdOrder.Rows[e.NewEditIndex].Cells[9].Text;
        EnableDisable(true);

        btnAddNew.Text = "Update";
    }

    private void InsertGL(DateTime CurrentWorkDate)
    {
        Configuration.GetAccountHead();

        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
        dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtVoucher.Columns.Add("DEBIT", typeof(decimal));
        dtVoucher.Columns.Add("CREDIT", typeof(decimal));
        dtVoucher.Columns.Add("REMARKS", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));

        if (RbdClaimType.SelectedValue == Constants.CreditClaim.ToString())
        {
            DataRow dr = dtVoucher.NewRow();
            dr["LEDGER_ID"] = Constants.LongNullValue.ToString();
            dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountHead.SelectedItem.Value);
            dr["REMARKS"] = DrpAccountHead.SelectedItem.Text + " Claim to " + DrpVendor.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(txtAmount.Text);
            dr["CREDIT"] = 0;
            dr["Principal_Id"] = 0;
            dtVoucher.Rows.Add(dr);


            DataRow dr1 = dtVoucher.NewRow();
            dr1["LEDGER_ID"] = Constants.LongNullValue.ToString();

            dr1["ACCOUNT_HEAD_ID"] = Configuration.SchemeAccount;

            dr1["REMARKS"] = DrpAccountHead.SelectedItem.Text + " Claim to " + DrpVendor.SelectedItem.Text;

            dr1["DEBIT"] = 0;
            dr1["CREDIT"] = decimal.Parse(txtAmount.Text);
            dr1["Principal_Id"] = 0;
            dtVoucher.Rows.Add(dr1);

        }
        else
        {
            ///Credit Side Entry
            DataRow dr1 = dtVoucher.NewRow();
            dr1["LEDGER_ID"] = Constants.LongNullValue.ToString();
            dr1["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountHead.SelectedItem.Value);
            dr1["DEBIT"] = 0;
            dr1["CREDIT"] = decimal.Parse(txtAmount.Text);
            dr1["Principal_Id"] = 0;
            dr1["REMARKS"] = DrpAccountHead.SelectedItem.Text + " Claim to " + DrpVendor.SelectedItem.Text;
            dtVoucher.Rows.Add(dr1);

            DataRow dr = dtVoucher.NewRow();

            dr["LEDGER_ID"] = Constants.LongNullValue.ToString();

            dr["ACCOUNT_HEAD_ID"] = Configuration.SchemeAccount;

            dr["DEBIT"] = decimal.Parse(txtAmount.Text);
            dr["CREDIT"] = 0;
            dr["Principal_Id"] = 0;
            dr["REMARKS"] = DrpAccountHead.SelectedItem.Text + " Claim to " + DrpVendor.SelectedItem.Text;
            dtVoucher.Rows.Add(dr);

        }

        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, Convert.ToInt32(drpDistributor.SelectedItem.Value.ToString()), CurrentWorkDate);

        LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedItem.Value.ToString()), int.Parse(Session["VoucherNo"].ToString()), MaxDocumentId, Constants.Journal_Voucher, CurrentWorkDate, int.Parse(RbdClaimType.SelectedValue), Session["VoucherNo"].ToString(), "Defualt " + DrpAccountHead.SelectedItem.Text + " Voucher", CurrentWorkDate, null, dtVoucher, Convert.ToInt32(Session["UserID"]), "-1", Constants.DateNullValue, true);
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
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
            {//if (IsDayClosed())
             //{
             //    UserController UserCtl = new UserController();

                //    UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
                //    Session.Clear();
                //    System.Web.Security.FormsAuthentication.SignOut();
                //    Response.Redirect("../Login.aspx");
                //}

                if (btnAddNew.Text == "Save")
                {
                    string MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedItem.Value.ToString().ToString()), 1);
                    Session.Add("VoucherNo", MaxDocumentId);

                    if (RbdClaimType.SelectedValue == Constants.CreditClaim.ToString())
                    {
                        LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(MaxDocumentId), int.Parse(DrpAccountHead.SelectedItem.Value.ToString()), int.Parse(drpDistributor.SelectedItem.Value.ToString()), 0, decimal.Parse(txtAmount.Text),
                                       CurrentWorkDate, txtRemarks.Text, DateTime.Now, int.Parse(DrpVendor.SelectedItem.Value.ToString().ToString()), Constants.IntNullValue,
                                       null, int.Parse(Session["UserId"].ToString()), -1, "0", Constants.CreditClaim, null, null, Constants.DateNullValue);
                        InsertGL(CurrentWorkDate);
                    }
                    else
                    {
                        LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(MaxDocumentId), int.Parse(DrpAccountHead.SelectedItem.Value.ToString()), int.Parse(drpDistributor.SelectedItem.Value.ToString()), decimal.Parse(txtAmount.Text), 0,
                                       CurrentWorkDate, txtRemarks.Text, DateTime.Now, int.Parse(DrpVendor.SelectedItem.Value.ToString()), Constants.IntNullValue,
                                       null, int.Parse(Session["UserId"].ToString()), -1, "0", Constants.DebitClaim, null, null, Constants.DateNullValue);
                        InsertGL(CurrentWorkDate);
                    }
                }
                else
                {
                    DataControl dc = new DataControl();
                    LController.UpdateVendorLedgerClaim(int.Parse(drpDistributor.SelectedItem.Value.ToString()), Convert.ToInt32(URowId.Value), VoucherNo.Value, long.Parse(DrpAccountHead.SelectedItem.Value.ToString()), decimal.Parse(dc.chkNull_0(txtAmount.Text)), txtRemarks.Text, int.Parse(RbdClaimType.SelectedValue));
                    EnableDisable(false);
                }
                ClearGrd_Order();
                LoadGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Dayclose not found for selected location!')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message + "')", true);
        }
    }

    #region Index/Change


    protected void RbdClaimType_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadGrid();

    }

    #endregion
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


    protected void GrdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total";
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[3].Text = Convert.ToDecimal(txtTotalAmount.Text).ToString("N");
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
        }
    }
    public void GetTotal()
    {
        if (GrdOrder.Rows.Count > 0)
        {
            GrdOrder.FooterRow.Cells[3].Text = Convert.ToDecimal(txtTotalAmount.Text).ToString("N");
            decimal TotalAmoUnt = 0;
            foreach (GridViewRow dr in GrdOrder.Rows)
            {
                TotalAmoUnt += decimal.Parse(dr.Cells[7].Text.ToString());
            }
            txtTotalAmount.Text = decimal.Round(TotalAmoUnt, 2).ToString();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnAddNew.Text = "Save";
        ClearGrd_Order();
    }
}