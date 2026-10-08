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
public partial class Forms_RptCustomerLedger : System.Web.UI.Page
{

    static string opType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {   
            LoadDistributor();
            LoadCustomer();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }
    
    private void LoadCustomer()
    {
        CustomerDataController mController = new CustomerDataController();
        DataTable m_dt = mController.SelectAllCustomer(int.Parse(drpDistributor.Value.ToString()), Constants.IntNullValue, Constants.IntNullValue);
        clsWebFormUtil.FillDxComboBoxList(this.ddlCustomer, m_dt, "CUSTOMER_ID", "CUSTOMER_NAME");
        if (m_dt.Rows.Count > 0)
        {
            ddlCustomer.SelectedIndex = 0;
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
    private decimal LoadCustomerOpBalance()
    {
        if (drpDistributor.Items.Count > 0 && ddlCustomer.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomerOpBalance(int.Parse(ddlCustomer.SelectedItem.Value.ToString()), int.Parse(drpDistributor.SelectedItem.Value.ToString()),
                      DateTime.Parse(txtStartDate.Text + " 00:00:00"),Constants.IntNullValue);

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
        DataControl dc = new DataControl();
        DocumentPrintController DPrint = new DocumentPrintController();
        RptCustomerController RptCustomerCtl = new RptCustomerController();
        DataSet ds;

        ds = RptCustomerCtl.CustomerLedger_View(Constants.IntNullValue, int.Parse(drpDistributor.Value.ToString()), int.Parse(ddlCustomer.Value.ToString()),
                  DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.Value.ToString()));
        CrpCustomerLedger CrpReport = new CrpCustomerLedger();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("Customer", ddlCustomer.SelectedItem.Text);
        CrpReport.SetParameterValue("Op_Balance", LoadCustomerOpBalance());
        CrpReport.SetParameterValue("opType", opType);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", reportType);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
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