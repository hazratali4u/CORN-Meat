using System;
using System.Data;
using System.Web.UI;
using System.Web;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;


public partial class Forms_RptCovertTable : System.Web.UI.Page
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

            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }
    }

    
    private void LoadDistributor()
    {
        DataTable dt = _dController.SelectDistributorInfo(Constants.IntNullValue,
            int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        // drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString(CultureInfo.InvariantCulture)));

        clsWebFormUtil.FillDxComboBoxList(drpDistributor, dt, 0, 2, true);

        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedIndex = 0;
        }
    }
   
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

   
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    private void ShowReport(int pReprotType)
    {

        DataTable dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        DataSet ds;
        if (rdbReportType.SelectedValue == "0")
        {
            ds = _rptSaleCtl.PrintDailySalesReportDateWise(int.Parse(drpDistributor.SelectedItem.Value.ToString()),
                   DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),
                   Constants.IntNullValue, 3);

            if (ds.Tables["spPrintDailySalesReportDateWise"].Rows.Count > 0)
            {

                CrpCoverTable CrpReport = new CrpCoverTable();
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
                CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
                CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("Address", dt.Rows[0]["ADDRESS1"].ToString());

                Session.Add("CrpReport", CrpReport);

                Session.Add("ReportType", pReprotType);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
                lblErrorMsg.Visible = false;
            }
            else
            {
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = "Data not exist against these parameters. Please Try again.";
            }

        }
        else
        {

            ds = _rptSaleCtl.PrintDailySalesReportDateWise(int.Parse(drpDistributor.SelectedItem.Value.ToString()),
                   DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),
                   Constants.IntNullValue, 4);

            if (ds.Tables["spPrintDailySalesReportDateWise"].Rows.Count > 0)
            {

                CrpCoverTableDateWise CrpReport = new CrpCoverTableDateWise();
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
                CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
                CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("Address", dt.Rows[0]["ADDRESS1"].ToString());

                Session.Add("CrpReport", CrpReport);

                Session.Add("ReportType", pReprotType);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
                lblErrorMsg.Visible = false;
            }
            else
            {
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = "Data not exist against these parameters. Please Try again.";
            }
        }
        
    }

}