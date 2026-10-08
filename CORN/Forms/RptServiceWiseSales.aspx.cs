using System;
using System.Web;
using System.Web.UI;
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
        string serviceType;
        /// For sericeType null assign to the all selected
        if (drpServiceType.SelectedItem.Value.ToString() == "")
        {
             serviceType = Constants.IntNullValue.ToString();
        }
        else
        {
            serviceType = drpServiceType.SelectedItem.Value.ToString();
        }

        if (RblReportType.SelectedValue == "0")
        {
            CORNBusinessLayer.Reports.CrpServiceTypeWiseSaleSummary CrpReport = new CORNBusinessLayer.Reports.CrpServiceTypeWiseSaleSummary();
            DataSet ds = new DataSet();
            ds = SKUCtl.SelectServiceWiseSales(int.Parse(RblReportType.SelectedValue), Convert.ToInt32(drpDistributor.SelectedItem.Value), Convert.ToInt32(serviceType), int.Parse(RblSortBy.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
            CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
            CrpReport.SetParameterValue("ReportType", "Service Type Sales Report (Summary) ");
            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", 0);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else if (RblReportType.SelectedValue == "1")
        {
            CORNBusinessLayer.Reports.CrpServiceTypeWiseSaleDetail CrpReport = new CORNBusinessLayer.Reports.CrpServiceTypeWiseSaleDetail();
            DataSet ds = new DataSet();
            ds = SKUCtl.SelectServiceWiseSales(int.Parse(RblReportType.SelectedValue), Convert.ToInt32(drpDistributor.SelectedItem.Value), Convert.ToInt32(serviceType), int.Parse(RblSortBy.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
            CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
            CrpReport.SetParameterValue("ReportType", "Service Type Sales Report (Invoice Wise Detail) ");
            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", 0);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }

    }
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        string serviceType;
        /// For sericeType null assign to the all selected
        if (drpServiceType.SelectedItem.Value.ToString() == "")
        {
            serviceType = Constants.IntNullValue.ToString();
        }
        else
        {
            serviceType = drpServiceType.SelectedItem.Value.ToString();
        }

        if (RblReportType.SelectedValue == "0")
        {
            CORNBusinessLayer.Reports.CrpServiceTypeWiseSaleSummary CrpReport = new CORNBusinessLayer.Reports.CrpServiceTypeWiseSaleSummary();
            DataSet ds = new DataSet();
            ds = SKUCtl.SelectServiceWiseSales(int.Parse(RblReportType.SelectedValue), Convert.ToInt32(drpDistributor.SelectedItem.Value), Convert.ToInt32(serviceType), int.Parse(RblSortBy.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
            CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
            CrpReport.SetParameterValue("ReportType", "Service Type Sales Report (Summary) ");

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
            CORNBusinessLayer.Reports.CrpServiceTypeWiseSaleDetail CrpReport = new CORNBusinessLayer.Reports.CrpServiceTypeWiseSaleDetail();
            DataSet ds = new DataSet();
            ds = SKUCtl.SelectServiceWiseSales(int.Parse(RblReportType.SelectedValue), Convert.ToInt32(drpDistributor.SelectedItem.Value), Convert.ToInt32(serviceType), int.Parse(RblSortBy.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
            CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
            CrpReport.SetParameterValue("ReportType", "Service Type Sales Report (Invoice Wise Detail) ");

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", 1);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }


}