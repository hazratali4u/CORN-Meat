using System;
using System.Data;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNBusinessLayer.Reports;
using CORNCommon.Classes;
using System.Web;

public partial class Forms_RptTableWiseSale : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {

            LoadDistributor();
            LoadTables();

            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }
    }


    private void LoadTables()
    {
        if (drpDistributor.Items.Count > 0)
        {
            LstTables.Items.Clear();

            GeoHierarchyController GCtl = new GeoHierarchyController();
            DataTable dt = GCtl.GetTableDefination(Constants.IntNullValue, int.Parse(drpDistributor.SelectedItem.Value.ToString()), false, int.Parse(Session["UserId"].ToString()));//false means all

            clsWebFormUtil.FillListBox(LstTables, dt, 0, 1, false);
        }
    }
    
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));

        clsWebFormUtil.FillDxComboBoxList(drpDistributor, dt, 0, 2);

        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedIndex = 0;
        }

    }

   
    private void ShowReport(int pReportType)
    {
        if (RbReportType.SelectedIndex == 1)
        {
            string Table_IDs = null;
            for (int i = 0; i < LstTables.Items.Count; i++)
            {
                if (LstTables.Items[i].Selected == true)
                {
                    Table_IDs += LstTables.Items[i].Value.ToString() + ",";
                }
            }
            if (Table_IDs == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select Table.');", true);
                return;
            }
            
            var rptCustomerCtl = new RptCustomerController();


            DataSet ds = rptCustomerCtl.SelectInvoiceDetail(Constants.IntNullValue, Table_IDs, int.Parse(drpDistributor.SelectedItem.Value.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 3);


            var crpReport = new CrpBillTableWiseReport();
            crpReport.SetDataSource(ds);
            crpReport.Refresh();


            crpReport.SetParameterValue("FROM_DATE", txtStartDate.Text);
            crpReport.SetParameterValue("TO_DATE", txtEndDate.Text);
            crpReport.SetParameterValue("CompanyName", drpDistributor.SelectedItem.Text);

            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", pReportType);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else
        {
            RptSaleController _rptSaleCtl = new RptSaleController();

            DocumentPrintController _dPrint = new DocumentPrintController();
            DataTable dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));

            DataSet ds = _rptSaleCtl.PrintDailySalesReportDateWise(int.Parse(drpDistributor.SelectedItem.Value.ToString()),
                       DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),
                       int.Parse(Session["UserId"].ToString()), 2);

            var crpReport = new CrpTableDSR();
            crpReport.SetDataSource(ds);
            crpReport.Refresh();

            crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
            crpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            crpReport.SetParameterValue("Address", dt.Rows[0]["ADDRESS1"].ToString());

            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", pReportType);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url +
                            ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }
    
    protected void BtnViewPdf_Click(object sender, EventArgs e)
    {
        ShowReport(0);

    }
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    protected void RbReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RbReportType.SelectedIndex == 0)
        {
            dvTables.Visible = false;
        }
        else
        {
            dvTables.Visible = true;
        }
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTables();
    }

}