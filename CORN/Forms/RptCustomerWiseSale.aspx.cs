using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Data;

public partial class Forms_RptCustomerWiseSale : System.Web.UI.Page
{
    readonly SkuController SKUCtl = new SkuController();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");
        if (!IsPostBack)
        {
            this.LoadDistributor();
            LoadAllCustomers();

            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
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
    private void LoadAllCustomers()
    {
        drpCustomer.Items.Clear();

        CustomerDataController mController = new CustomerDataController();
        try
        {
            DataTable dtCustomers = mController.SelectCustomerLocationWise(1,int.Parse(drpDistributor.SelectedItem.Value.ToString()));
            
            drpCustomer.Items.Add(new DevExpress.Web.ListEditItem("All", Constants.IntNullValue.ToString()));

            clsWebFormUtil.FillDxComboBoxList(drpCustomer, dtCustomers, 0, 2);
          

            if (dtCustomers.Rows.Count > 0)
            {
                drpCustomer.SelectedIndex = 0;
            }


        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void RblReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RblReportType.SelectedValue == "0")
        {
            divSortBy.Visible = false;
        }
        else if (RblReportType.SelectedValue == "1")
        {
            divSortBy.Visible = true;
        }
    }
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        if (RblReportType.SelectedValue == "0")
        {
            CORNBusinessLayer.Reports.CrpCustomerWiseSaleSummary CrpReport = new CORNBusinessLayer.Reports.CrpCustomerWiseSaleSummary();
            DataSet ds = new DataSet();
            ds = SKUCtl.SelectCustomerWiseSales(int.Parse(RblReportType.SelectedValue), Convert.ToInt32(drpDistributor.SelectedItem.Value), Convert.ToInt32(drpCustomer.SelectedItem.Value), int.Parse(RblSortBy.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
            CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
            CrpReport.SetParameterValue("ReportType", "Customer Wise Sales Report (Summary) ");
            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else if (RblReportType.SelectedValue == "1")
        {
            CORNBusinessLayer.Reports.CrpCustomerWiseSaleDetail CrpReport = new CORNBusinessLayer.Reports.CrpCustomerWiseSaleDetail();
            DataSet ds = new DataSet();
            ds = SKUCtl.SelectCustomerWiseSales(int.Parse(RblReportType.SelectedValue), Convert.ToInt32(drpDistributor.SelectedItem.Value), Convert.ToInt32(drpCustomer.SelectedItem.Value), int.Parse(RblSortBy.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
            CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
            CrpReport.SetParameterValue("ReportType", "Customer Wise Sales Report (Invoice Wise Detail) ");
            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        
    }
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        if (RblReportType.SelectedValue == "0")
        {
            CORNBusinessLayer.Reports.CrpCustomerWiseSaleSummary CrpReport = new CORNBusinessLayer.Reports.CrpCustomerWiseSaleSummary();
            DataSet ds = new DataSet();
            ds = SKUCtl.SelectCustomerWiseSales(int.Parse(RblReportType.SelectedValue), Convert.ToInt32(drpDistributor.SelectedItem.Value), Convert.ToInt32(drpCustomer.SelectedItem.Value), int.Parse(RblSortBy.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
            CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
            CrpReport.SetParameterValue("ReportType", "Customer Wise Sales Report (Summary) ");

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", 1);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else if (RblReportType.SelectedValue == "1")
        {
            CORNBusinessLayer.Reports.CrpCustomerWiseSaleDetail CrpReport = new CORNBusinessLayer.Reports.CrpCustomerWiseSaleDetail();
            DataSet ds = new DataSet();
            ds = SKUCtl.SelectCustomerWiseSales(int.Parse(RblReportType.SelectedValue), Convert.ToInt32(drpDistributor.SelectedItem.Value), Convert.ToInt32(drpCustomer.SelectedItem.Value), int.Parse(RblSortBy.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
            CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
            CrpReport.SetParameterValue("ReportType", "Customer Wise Sales Report (Summary) ");

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", 1);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAllCustomers();
    }
}