using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;    
using CORNBusinessLayer.Reports;
using System.Web;

/// <summary>
/// Form For Sale Person DSR Report
/// </summary>
public partial class Forms_frmOrderBookerDSR : Page
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
            LoadOrderBooker();
            Configuration.SystemCurrentDateTime = (DateTime) Session["CurrentWorkDate"];
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
        //  drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString(CultureInfo.InvariantCulture)));

        clsWebFormUtil.FillDxComboBoxList(drpDistributor, dt, 0, 2);

        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedIndex = 0;
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
        if (RbReportType.SelectedItem.Value.ToString() == "1") // Summary wise Report
        {
            ds = _rptSaleCtl.SelectBookerDSR(int.Parse(drpDistributor.SelectedItem.Value.ToString()),DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),
                int.Parse(Session["UserId"].ToString()),Constants.IntNullValue,int.Parse(DrpOrderBooker.SelectedItem.Value.ToString()));

            var crpReport = new CrpBookerDSR();
            crpReport.SetDataSource(ds);
            crpReport.Refresh();
            crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
            crpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            crpReport.SetParameterValue("Address", dt.Rows[0]["ADDRESS1"].ToString());
            crpReport.SetParameterValue("ReportName", "Order Taker Wise Sales Report");
            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", pReprotType);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url +
                            ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else if (RbReportType.SelectedItem.Value.ToString() == "2")
        {

            ds = _rptSaleCtl.SelectBookerInvoiceWiseDSR(int.Parse(drpDistributor.SelectedItem.Value.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),
                int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, int.Parse(DrpOrderBooker.SelectedItem.Value.ToString()));

            var crpReport = new CrpBookerInvoiceWiseDSR();
            crpReport.SetDataSource(ds);
            crpReport.Refresh();
            crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
            crpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            crpReport.SetParameterValue("Address", dt.Rows[0]["ADDRESS1"].ToString());
            crpReport.SetParameterValue("ReportName", "Order Taker Report (Invoice Detail)");
            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", pReprotType);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url +
                            ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else if (RbReportType.SelectedItem.Value.ToString() == "3")
        {

            ds = _rptSaleCtl.SelectBookerSkuWiseDSR(int.Parse(drpDistributor.SelectedItem.Value.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),
                int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, int.Parse(DrpOrderBooker.SelectedItem.Value.ToString()));

            var crpReport = new CrpBookerSkuWiseDSR();
               crpReport.SetDataSource(ds);
            crpReport.Refresh();
            crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
            crpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            crpReport.SetParameterValue("Address", dt.Rows[0]["ADDRESS1"].ToString());
            crpReport.SetParameterValue("ReportName", "Order Taker Report (Item Wise)");
            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", pReprotType);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url +
                            ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }


        //else // For Tables

        //{
        //    ds = _rptSaleCtl.PrintDailySalesReportDateWise(int.Parse(drpDistributor.SelectedValue),
        //           DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),
        //           int.Parse(Session["UserId"].ToString()), 2);

        //    var crpReport = new CrpTableDSR();
        //    crpReport.SetDataSource(ds);
        //    crpReport.Refresh();

        //    crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        //    crpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
        //    crpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
        //    crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        //    crpReport.SetParameterValue("Address", dt.Rows[0]["ADDRESS1"].ToString());

        //    Session.Add("CrpReport", crpReport);
        //    Session.Add("ReportType", pReprotType);
        //    const string url = "'Default.aspx'";
        //    const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url +
        //                    ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        //    Type cstype = GetType();
        //    ClientScriptManager cs = Page.ClientScript;
        //    cs.RegisterStartupScript(cstype, "OpenWindow", script);
        //}

    }



    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadOrderBooker();

    }
    private void LoadOrderBooker()
    {
        DrpOrderBooker.Items.Clear();
        Distributor_UserController UController = new Distributor_UserController();
        if (drpDistributor.Items.Count > 0)
        {
            DataTable dtEmployee = UController.SelectDistributorUser(Constants.SALES_FORCE_ORDERBOOKER, int.Parse(drpDistributor.SelectedItem.Value.ToString().ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            if (dtEmployee.Rows.Count <= 0) return;
              DrpOrderBooker.Items.Add(new DevExpress.Web.ListEditItem("All", Constants.IntNullValue.ToString(CultureInfo.InvariantCulture)));

            clsWebFormUtil.FillDxComboBoxList(DrpOrderBooker, dtEmployee, "USER_ID", "USER_NAME");

            if (dtEmployee.Rows.Count > 0)
            {
                DrpOrderBooker.SelectedIndex = 0;
            }

        }
    }
}
