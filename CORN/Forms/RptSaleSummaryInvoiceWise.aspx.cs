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

            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

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

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        string strInvoiceNo2 = null;
        string[] strInvoiceNo = null;
        if (txtInvoiceNo.Text.Trim().Length > 0)
        {
            if (txtInvoiceNo.Text.Contains(","))
            {
                strInvoiceNo = txtInvoiceNo.Text.Split(',');
            }
            else
            {
                strInvoiceNo[0] = txtInvoiceNo.Text;
            }

            foreach(string s in strInvoiceNo)
            {
                strInvoiceNo2 += s;
                strInvoiceNo2 += ",";
            }
        }
        CORNBusinessLayer.Reports.CrpInvoiceWiseSaleSummary CrpReport = new CORNBusinessLayer.Reports.CrpInvoiceWiseSaleSummary();
        DataSet ds = new DataSet();
        ds = SKUCtl.SelectServiceWiseSales(2, Convert.ToInt32(drpDistributor.SelectedItem.Value), Constants.IntNullValue, Constants.IntNullValue, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), strInvoiceNo2);

        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
        CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
        CrpReport.SetParameterValue("ReportType", "Sale Summary Invoice Wise ");
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);


    }
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        string strInvoiceNo2 = null;
        string[] strInvoiceNo = null;
        if (txtInvoiceNo.Text.Trim().Length > 0)
        {
            if (txtInvoiceNo.Text.Contains(","))
            {
                strInvoiceNo = txtInvoiceNo.Text.Split(',');
            }
            else
            {
                strInvoiceNo[0] = txtInvoiceNo.Text;
            }

            foreach (string s in strInvoiceNo)
            {
                strInvoiceNo2 += s;
                strInvoiceNo2 += ",";
            }
        }
        CORNBusinessLayer.Reports.CrpInvoiceWiseSaleSummary CrpReport = new CORNBusinessLayer.Reports.CrpInvoiceWiseSaleSummary();
        DataSet ds = new DataSet();
        ds = SKUCtl.SelectServiceWiseSales(2, Convert.ToInt32(drpDistributor.SelectedItem.Value), Constants.IntNullValue, Constants.IntNullValue, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), strInvoiceNo2);

        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
        CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
        CrpReport.SetParameterValue("ReportType", "Sale Summary Invoice Wise ");

        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 1);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);

    }
}