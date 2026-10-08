using System;
using System.Web;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Data;
using CrystalDecisions.Shared;

public partial class Forms_Default2 : System.Web.UI.Page
{
    readonly SkuController SKUCtl = new SkuController();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if(!Page.IsPostBack)
        {
            LoadHasModifier();
        }
    }
    private void LoadHasModifier()
    {

        DataTable dt = SKUCtl.SelectSkuHasModifier(0);

        ddlHasModifier.Items.Add(new DevExpress.Web.ListEditItem("All", Constants.IntNullValue.ToString()));

        clsWebFormUtil.FillDxComboBoxList(ddlHasModifier, dt, 0, 2);

        if (dt.Rows.Count > 0)
        {
            ddlHasModifier.SelectedIndex = 0;
        }

    }


    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        //DocumentPrintController DPrint = new DocumentPrintController();

        //DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        CORNBusinessLayer.Reports.CrpSkuModifier CrpReport = new CORNBusinessLayer.Reports.CrpSkuModifier();
               
        DataSet ds = new DataSet();
        ds= SKUCtl.SelectModifierRpt( Convert.ToInt32(ddlHasModifier.SelectedItem.Value));

        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        

        //CrpReport.SetParameterValue("Typeid", 1);
        CrpReport.SetParameterValue("SKU_NAME", "");
        //CrpReport.SetParameterValue("Status", drpStatus.SelectedItem.Text);
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    /// <summary>
    /// Shows Route Wise Customer List in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        //DocumentPrintController DPrint = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        //DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        CORNBusinessLayer.Reports.CrpSkuModifier CrpReport = new CORNBusinessLayer.Reports.CrpSkuModifier();
        DataSet ds = new DataSet();
        ds = SKUCtl.SelectModifierRpt(Convert.ToInt32(ddlHasModifier.SelectedItem.Value));

        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("SKU_NAME", ddlHasModifier.SelectedItem.Text);
        string path = Configuration.GetAppInstallationPath() + "\\SkuModifier.xls";

        CrpReport.SetDatabaseLogon("sa", "Laislabonitamac2065");

        CrpReport.ExportToDisk(ExportFormatType.Excel, path);


        System.IO.FileInfo file = new System.IO.FileInfo(path);

        if (file.Exists)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

            Response.AddHeader("Content-Length", file.Length.ToString());

            Response.ContentType = "application/octet-stream";

            Response.WriteFile(file.FullName);

            Response.End();

        }
        else
        {
            Response.Write("This file does not exist.");
        }
    }
}