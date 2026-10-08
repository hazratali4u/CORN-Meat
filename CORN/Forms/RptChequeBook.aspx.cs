using System;
using System.Data;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;

/// <summary>
/// Form For General Ledger Report
/// </summary>
public partial class Forms_RptChequeBook : System.Web.UI.Page
{
    readonly BankController _bnkController = new BankController();
    readonly AccountHeadController mAccountController = new AccountHeadController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadAccoutnHead();
            LoadAccountTitle();
        }
    }
    public void LoadAccoutnHead()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = mAccountController.SelectAccountHeadByMapping(Constants.AC_AccountHeadId, Constants.LongNullValue, 2, Constants.AC_BankAccountHead);
            clsWebFormUtil.FillDxComboBoxList(this.drpAccountHead, dt, 0, 4, true);

            if (dt.Rows.Count > 0)
            {
                drpAccountHead.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {
        }
    }
    public void LoadAccountTitle()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = _bnkController.SelectCheckBook(Constants.IntNullValue, Convert.ToInt32(drpAccountHead.SelectedItem.Value), null, null, null, true, Constants.DateNullValue, 7);
            clsWebFormUtil.FillDxComboBoxList(this.drpAccountTitle, dt, 0, 1, true);

            if (dt.Rows.Count > 0)
            {
                drpAccountTitle.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {
        }
    }
    protected void drpAccountHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountTitle();
    }
    private void showReport(int reportType)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        VenderEntryController RptCustCtl = new VenderEntryController();
        DataSet ds;

        ds = _bnkController.SelectRPTCheckBook(Constants.IntNullValue, Convert.ToInt32(drpAccountHead.SelectedItem.Value), null, null, drpAccountTitle.SelectedItem.Text.ToString(), true, Constants.DateNullValue, 8);

        DataTable dt = DPrint.SelectReportTitle(int.Parse(Session["DISTRIBUTOR_ID"].ToString()));

        CrpChequeBookReport CrpReport = new CrpChequeBookReport();

        CrpReport.SetDataSource(ds);

        CrpReport.Refresh();

        CrpReport.SetParameterValue("BankAccount", drpAccountHead.SelectedItem.Text.ToString());
        CrpReport.SetParameterValue("AccountTitle", drpAccountTitle.SelectedItem.Text.ToString());
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", reportType);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        showReport(0);
    }

    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        showReport(1);
    }




}