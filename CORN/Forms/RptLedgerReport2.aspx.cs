using System;
using System.Data;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;
using CrystalDecisions.CrystalReports.Engine;

/// <summary>
/// Form For General Ledger Report
/// </summary>
public partial class Forms_RptLedgerReport2 : System.Web.UI.Page
{

    static string opType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrincipal();
            LoadDistributor();

            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }
    
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDxComboBoxList(this.DrpPrincipal, m_dt, 0, 1);


        if (m_dt.Rows.Count > 0)
        {
            DrpPrincipal.SelectedIndex = 0;
        }

    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();

        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));

        clsWebFormUtil.FillDxComboBoxList(drpDistributor, dt, 0, 2, true);

        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedIndex = 0;
        }
    }
    private decimal LoadVendoerOpBalance()
    {
        if (drpDistributor.Items.Count > 0 && DrpPrincipal.Items.Count > 0)
        {
            VenderEntryController mController = new VenderEntryController();
            DataTable dt = mController.GetVendorOpening(int.Parse(DrpPrincipal.SelectedItem.Value.ToString()), int.Parse(drpDistributor.SelectedItem.Value.ToString()),
                      DateTime.Parse(txtStartDate.Text + " 00:00:00"));

            if (decimal.Parse(dt.Rows[0][0].ToString()) > 0)
            {
                opType = "DR";

            }
            else
            {
                opType = "CR";
            }


            return decimal.Parse(dt.Rows[0][0].ToString());
        }
        return 0;
    }

    private void showReport(int reportType)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        VenderEntryController RptCustCtl = new VenderEntryController();

        DataSet ds = null;

        {
            ds = RptCustCtl.GetVendorLedger(int.Parse(DrpPrincipal.SelectedItem.Value.ToString()), int.Parse(drpDistributor.SelectedItem.Value.ToString()),
                      DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

            DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));

            CrpVendorLedger CrpReport = new CrpVendorLedger();
            ReportDocument subReport = CrpReport.OpenSubreport("SubReport");

            CrpReport.SetDataSource(ds);
            subReport.SetDataSource(ds);

            CrpReport.Refresh();

            CrpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
            CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
            CrpReport.SetParameterValue("Op_Balance", LoadVendoerOpBalance());
            CrpReport.SetParameterValue("opType", opType);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", reportType);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
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