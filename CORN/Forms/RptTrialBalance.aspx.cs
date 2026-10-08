using System;
using System.Data;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// Form For  Trial Balance Report
/// </summary>
public partial class Forms_RptTrialBalance : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadMinMixAccountCode();
            this.LoadDistributor();
            this.LoadAccount();
            this.LoadAccountDetail();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtFromAccount.Attributes.Add("readonly", "readonly");
            txttoAccount.Attributes.Add("readonly", "readonly");

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        drpDistributor.Items.Add(new DevExpress.Web.ListEditItem("All", Constants.IntNullValue.ToString()));

        clsWebFormUtil.FillDxComboBoxList(drpDistributor, dt, 0, 2);

        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedIndex = 0;
        }

    }

    /// <summary>
    /// Loads Main Account Heads To Main Account Combo
    /// </summary>
    private void LoadAccount()
    {
        AccountHeadController mController = new AccountHeadController();
        DataTable dt = mController.SelectAccountHead(Constants.AC_MainTypeId, Constants.LongNullValue, 1);

        DrpMainAccount.Items.Add(new DevExpress.Web.ListEditItem("All", Constants.IntNullValue.ToString()));

        clsWebFormUtil.FillDxComboBoxList(DrpMainAccount, dt, 0, 10);

        if (dt.Rows.Count > 0)
        {
            DrpMainAccount.SelectedIndex = 0;
        }
    }

    private void LoadMinMixAccountCode()
    {
        AccountHeadController mController = new AccountHeadController();
        DataTable dt = mController.GetMinMaxAccountCode(Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            txtFromAccount.Text = dt.Rows[0]["FromAccountCode"].ToString();
            txttoAccount.Text = dt.Rows[0]["ToAccountCode"].ToString();
        }
    }

    /// <summary>
    /// Loads Account Heads To Account ListBox
    /// </summary>
    private void LoadAccountDetail()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue, 1);
        DataView dv = new DataView(dtHead);
        dv.Sort = "ACCOUNT_DETAIL";
        dtHead = dv.ToTable();
        clsWebFormUtil.FillListBox(this.LstAccountHead, dtHead, "ACCOUNT_DETAIL", "ACCOUNT_DETAIL", true);
        this.Session.Add("dtHead", dtHead);
    }

    private void ShowReport(int pReportType)
    {
        DocumentPrintController dPrint = new DocumentPrintController();
        RptAccountController rptAccountCtl = new RptAccountController();

        DataSet ds = rptAccountCtl.TrialBalance(Constants.IntNullValue, int.Parse(drpDistributor.SelectedItem.Value.ToString()), int.Parse(DrpMainAccount.SelectedItem.Value.ToString()),
                  DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpLevel.SelectedItem.Value.ToString()), txtFromAccount.Text, txttoAccount.Text, Constants.IntNullValue);

        DataTable dt = dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));

        CORNBusinessLayer.Reports.CrpTrialBalance CrpReport = new CORNBusinessLayer.Reports.CrpTrialBalance();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("From_Date", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("Principal", "");

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", pReportType);
        const string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    /// <summary>
    /// Shows Trial Balance in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    /// <summary>
    /// Checks All Account Heads In Account Head ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ChbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (ChbSelectAll.Checked == true)
        {
            txtFromAccount.Text = "0000000000";
            txttoAccount.Text = "9999999999";

            txtFromAccount.Attributes.Add("readonly", "readonly");
            txttoAccount.Attributes.Add("readonly", "readonly");
        }
        else
        {
            txtFromAccount.Text = "";
            txttoAccount.Text = "";

            txtFromAccount.Attributes.Remove("readonly");
            txttoAccount.Attributes.Remove("readonly");
        }
    }
}