using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Collections;
using System.Web;

/// <summary>
/// Form To Edit Vouchers
/// </summary>
public partial class Forms_frmLedgerViewData : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And Grids On The Page
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
            LoadDistributor();
            LoadPrincipal();
            VoucherType();
            LoadUser();
            btnView.Attributes.Add("onclick", "return ValidateForm();");
            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
            DrpVoucherType_SelectedIndexChanged(null, null);
        }
    }

    /// <summary>
    /// Loads Principals To Supplier Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        DrpPrincipal.Items.Add(new ListItem("GENERAL ENTRY", "0"));
        clsWebFormUtil.FillDropDownList(DrpPrincipal, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Voucher Types To Voucher Type Combo
    /// </summary>
    private void VoucherType()
    {
        LedgerController LController = new LedgerController();
        DataTable dt = LController.SelectVoucherType(int.Parse(Session["UserId"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpVoucherType, dt, 0, 1, true);
    }

    /// <summary>
    /// Loads Users To User Combo
    /// </summary>
    private void LoadUser()
    {
        Distributor_UserController mController = new Distributor_UserController();
        DataTable dt = mController.SelectGLUser();
        DrpUser.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpUser, dt, 0, 1);
    }

    /// <summary>
    /// Loads UnPosted Vouchers To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnView_Click(object sender, EventArgs e)
    {
        int VoucherType = Constants.IntNullValue;
        int VoucherType2 = Constants.IntNullValue;

        if (DrpVoucherType.SelectedValue == "16" || DrpVoucherType.SelectedValue == "812")
        {
            VoucherType = Convert.ToInt32(DrpVoucherType.SelectedValue);
            VoucherType2 = Convert.ToInt32(DrpVoucherType.SelectedValue);
        }
        else if (DrpVoucherType.SelectedValue == "14" || DrpVoucherType.SelectedValue == "15")
        {
            if (DrpVoucherSubType.SelectedValue == Constants.IntNullValue.ToString())
            {
                VoucherType2 = Convert.ToInt32(DrpVoucherType.SelectedValue);
            }
            else
            {
                VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedValue);
            }
        }

        LedgerController mController = new LedgerController();
        DataTable dt = mController.SelectUnPostVoucherNo(VoucherType, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpUser.SelectedValue.ToString()), bool.Parse(RbdList.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), VoucherType2);
        GrdLedger.DataSource = dt;
        GrdLedger.DataBind();
    }

    /// <summary>
    /// Shows Voucher in Crystal Report For Print
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void GrdLedger_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        CORNBusinessLayer.Reports.crpVoucherView CrpReport = new CORNBusinessLayer.Reports.crpVoucherView();

        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue.ToString()), GrdLedger.Rows[e.NewEditIndex].Cells[1].Text, int.Parse(GrdLedger.Rows[e.NewEditIndex].Cells[4].Text));
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        if (DrpVoucherType.SelectedValue == "14" || DrpVoucherType.SelectedValue == "15")
        {
            CrpReport.SetParameterValue("VoucherType", DrpVoucherType.SelectedItem.Text);
            CrpReport.SetParameterValue("VoucherSubType", DrpVoucherSubType.SelectedItem.Text);
        }
        else if (DrpVoucherType.SelectedValue == "16")
        {
            CrpReport.SetParameterValue("VoucherSubType", "Journal Voucher");
            CrpReport.SetParameterValue("VoucherType", "Journal Voucher");
        }
        else
        {
            CrpReport.SetParameterValue("VoucherSubType", "Opening Balance Voucher");
            CrpReport.SetParameterValue("VoucherType", "Opening Balance Voucher");
        }
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=1000,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);


    }

    /// <summary>
    /// Cancels Voucher Posting
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LedgerController lController = new LedgerController();
        foreach (GridViewRow dr in GrdLedger.Rows)
        {
            CheckBox ChbSelect = (CheckBox)dr.Cells[0].FindControl("ChbSelect");
            if (ChbSelect.Checked == true)
            {
                try
                {
                    lController.PostSelectVoucher(int.Parse(drpDistributor.SelectedValue.ToString()), dr.Cells[1].Text, int.Parse(dr.Cells[4].Text), 1, DateTime.Parse(dr.Cells[5].Text));
                    BankController _bController = new BankController();
                    _bController.UpdateChequeStatus(0, dr.Cells[6].Text, "", 1);
                }
                catch (Exception)
                {
                }

            }
        }
        DataTable dt = lController.SelectUnPostVoucherNo(int.Parse(DrpVoucherType.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpUser.SelectedValue), bool.Parse(RbdList.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpVoucherSubType.SelectedValue));
        GrdLedger.DataSource = dt;
        GrdLedger.DataBind();
    }

    /// <summary>
    /// Enables/Disables Cancel Voucher Button
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void RbdList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RbdList.SelectedIndex == 1)
        {
            btnDelete.Enabled = false;
        }
        else
        {
            btnDelete.Enabled = true;
        }
    }
    protected void DrpVoucherType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DrpVoucherSubType.Items.Clear();
        if (DrpVoucherType.SelectedValue == "14")
        {
            DrpVoucherSubType.Items.Add(new ListItem("Cash Receipt Voucher", "14"));
            DrpVoucherSubType.Items.Add(new ListItem("Cash Payment Voucher", "24"));
            DrpVoucherSubType.Enabled = true;
        }
        else if (DrpVoucherType.SelectedValue == "16" || DrpVoucherType.SelectedValue == "812")
        {
            DrpVoucherSubType.Enabled = false;
        }
        else
        {
            DrpVoucherSubType.Items.Add(new ListItem("Bank Receipt Voucher", "15"));
            DrpVoucherSubType.Items.Add(new ListItem("Bank Payment Voucher", "17"));
            DrpVoucherSubType.Enabled = true;
        }
        GrdLedger.DataSource = null;
        GrdLedger.DataBind();
    }
    /// <summary>
    /// Stores Variables To Session And Redirects To Voucher Editing Form
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void GrdLedger_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        Session.Add("VoucherNo", GrdLedger.Rows[e.RowIndex].Cells[1].Text);
        Session.Add("DistributorId", drpDistributor.SelectedValue.ToString());
        Session.Add("VoucherTypeId", GrdLedger.Rows[e.RowIndex].Cells[4].Text);

        const string url = "'frmVoucherEditing.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    /// <summary>
    /// Shows Voucher in Crystal Report For Print
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnPrintVoucher_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        CORNBusinessLayer.Reports.crpVoucherView CrpReport = new CORNBusinessLayer.Reports.crpVoucherView();

        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ArrayList alVoucherNos = new ArrayList();

        foreach (GridViewRow gvr in GrdLedger.Rows)
        {
            CheckBox ChbSelect = (CheckBox)gvr.Cells[0].FindControl("ChbSelect");
            if (ChbSelect.Checked == true)
            {
                alVoucherNos.Add(gvr.Cells[1].Text);
            }
        }
        if (alVoucherNos.Count > 0)
        {

            int VoucherType = Constants.IntNullValue;
            int VoucherType2 = Constants.IntNullValue;

            if (DrpVoucherType.SelectedValue == "16" || DrpVoucherType.SelectedValue == "812")
            {
                VoucherType = Convert.ToInt32(DrpVoucherType.SelectedValue);
            }
            else if (DrpVoucherType.SelectedValue == "14" || DrpVoucherType.SelectedValue == "15")
            {
                if (DrpVoucherSubType.SelectedValue == Constants.IntNullValue.ToString())
                {
                    VoucherType2 = Convert.ToInt32(DrpVoucherType.SelectedValue);
                }
                else
                {
                    VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedValue);
                }
            }

            ds = DPrint.PrintVouchers(VoucherType, int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), RbdList.SelectedIndex, int.Parse(DrpUser.SelectedValue.ToString()), VoucherType2);
            DataTable dtSelectedVoucher = ds.Tables["RptVoucherView"].Clone();

            for (int i = 0; i < alVoucherNos.Count; i++)
            {
                DataRow[] foundRecord = ds.Tables["RptVoucherView"].Select("VOUCHER_NO = '" + alVoucherNos[i].ToString() + "'");
                if (foundRecord.Length > 0)
                {
                    for (int j = 0; j < foundRecord.Length; j++)
                    {
                        DataRow dr = dtSelectedVoucher.NewRow();
                        dtSelectedVoucher.ImportRow(foundRecord[j]);
                    }
                }
            }

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
            CrpReport.SetParameterValue("VoucherType", DrpVoucherType.SelectedItem.Text);
            CrpReport.SetParameterValue("VoucherSubType", DrpVoucherSubType.SelectedItem.Text);

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select at least one Voucher');", true);
        }
    }
}