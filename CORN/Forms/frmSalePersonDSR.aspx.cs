using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;
using System.Web;

/// <summary>
/// Form For Sale Person DSR Report
/// </summary>
public partial class Forms_frmSalePersonDSR : Page
{
    readonly DocumentPrintController _dPrint = new DocumentPrintController();
    readonly RptSaleController _rptSaleCtl = new RptSaleController();
    readonly DistributorController _dController = new DistributorController();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!IsPostBack)
        {
            LoadDistributor();
            LoadCashier();
            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        drpDistributor.Items.Clear();

        DataTable dt = _dController.SelectDistributorInfo(Constants.IntNullValue,
            int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        drpDistributor.Items.Add(new DevExpress.Web.ListEditItem("All", Constants.IntNullValue.ToString()));

        clsWebFormUtil.FillDxComboBoxList(drpDistributor, dt, 0, 2);

        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedIndex = 0;
        }
    }
    protected void RbReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblCashier.Visible = false;
        ddlCashier.Visible = false;
        if(RbReportType.SelectedItem.Value.ToString() == "4")
        {
            lblCashier.Visible = true;
            ddlCashier.Visible = true;
        }
    }
    /// <summary>
    /// Shows Sale Person DSR in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Sale Person DSR in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    private void ShowReport(int pReprotType)
    {

        DataTable dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        DataSet ds;
        if (RbReportType.SelectedIndex == 0 || RbReportType.SelectedIndex == 2)
        {
            ds = _rptSaleCtl.SelectSalePersonDSR(int.Parse(drpDistributor.SelectedItem.Value.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),
                int.Parse(Session["UserId"].ToString()), Convert.ToInt32(RbReportType.SelectedItem.Value));

            var crpReport = new CrpLocationDSR();
            crpReport.SetDataSource(ds);
            crpReport.Refresh();
            crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
            crpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            crpReport.SetParameterValue("Address", RbReportType.SelectedItem.Text);
            crpReport.SetParameterValue("ReportName", RbReportType.SelectedItem.Text + " wise Sales Report");
            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", pReprotType);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url +
                            ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else if (RbReportType.SelectedIndex == 1)
        {
            ds = _rptSaleCtl.PrintDailySalesReportDateWise(int.Parse(drpDistributor.SelectedItem.Value.ToString()),
                DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),
                int.Parse(Session["UserId"].ToString()), 1);
            var crpReport = new CrpDateDSR();
            crpReport.SetDataSource(ds);
            crpReport.Refresh();
            crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
            crpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            crpReport.SetParameterValue("Address", dt.Rows[0]["ADDRESS1"].ToString());

            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", pReprotType);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url +
                            ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }


    }

    private void LoadCashier()
    {
        ddlCashier.Items.Clear();
        Distributor_UserController UController = new Distributor_UserController();
        if (drpDistributor.Items.Count > 0)
        {
            DataTable dtEmployee = UController.SelectDistributorUser(2, int.Parse(drpDistributor.SelectedItem.Value.ToString().ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            if (dtEmployee.Rows.Count > 0)
            {
                ddlCashier.Items.Add(new DevExpress.Web.ListEditItem("All", Constants.IntNullValue.ToString(CultureInfo.InvariantCulture)));
                clsWebFormUtil.FillDxComboBoxList(ddlCashier, dtEmployee, "USER_ID", "USER_NAME");
                ddlCashier.SelectedIndex = 0;
            }
            else
            {
                ddlCashier.Items.Add(new DevExpress.Web.ListEditItem("No Record found.", "-1"));
                ddlCashier.SelectedIndex = 0;
            }
        }
    }


    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCashier();
    }
}
