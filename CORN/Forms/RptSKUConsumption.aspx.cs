using System;
using System.Web;
using System.Web.UI;
using System.Data;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CrystalDecisions.CrystalReports.Engine;

public partial class Forms_RptConsumed : System.Web.UI.Page
{
    readonly SkuController SKUCtl = new SkuController();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");

            LoadDistributor();
            LoadSKUConsumed();
        }

    }
    private void LoadSKUConsumed()
    {
        ddlSKU.Items.Clear();

        int value = 3;
        if (Rblconsumtion.SelectedValue == "1")
        {
            value = 2;
        }
        if (Rblconsumtion.SelectedValue == "3")
        {
            value = 4;
        }
        DataTable dt = SKUCtl.SelectSkuConsumption(value);

        if (dt.Rows.Count > 0)
        {

            ddlSKU.Items.Add(new DevExpress.Web.ListEditItem("All", Constants.IntNullValue.ToString()));

            clsWebFormUtil.FillDxComboBoxList(ddlSKU, dt, 0, 1);

            ddlSKU.SelectedIndex = 0;
           
        }
    }

    private void showReport(int Type)
    {
        try
        {
            DataSet ds = new DataSet();

            ds = SKUCtl.SelectSKUConsumption(Convert.ToInt32(ddlLocation.SelectedItem.Value), Convert.ToInt32(ddlSKU.SelectedItem.Value), Convert.ToInt32(ddlReportType.SelectedValue), Convert.ToInt32(Rblconsumtion.SelectedValue), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text));

            dvMsg.Visible = false;

            if (ds.Tables["spSelectSKU_Consumption"].Rows.Count > 0)
            {
                ReportDocument CrpReport = new ReportDocument();

                if (ddlReportType.SelectedValue == "1")
                {
                     CrpReport = new CORNBusinessLayer.Reports.CrpSKUConsumption();
                }
                else
                {
                     CrpReport = new CORNBusinessLayer.Reports.CrpSKUConsumptionSummary();
                }
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();
                CrpReport.SetParameterValue("Location", ddlLocation.SelectedItem.Text);
                CrpReport.SetParameterValue("SKU_NAME", ddlSKU.SelectedItem.Text);
                CrpReport.SetParameterValue("ReportType", Rblconsumtion.SelectedItem.Text);
                CrpReport.SetParameterValue("FromDate",DateTime.Parse(txtStartDate.Text));
                CrpReport.SetParameterValue("ToDate", DateTime.Parse(txtEndDate.Text));
                Session.Add("CrpReport", CrpReport);
                Session.Add("ReportType", Type);
                const string url = "'Default.aspx'";
                const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else
            {
                dvMsg.Visible =true;
            }
        }
        catch (Exception)
        {

            throw;
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

    protected void Rblconsumtion_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvMsg.Visible = false;
        if (Rblconsumtion.SelectedValue == "1")
        {
            dvReportType.Visible = false;
        }
        else
        {
            dvReportType.Visible = true;
        }
        LoadSKUConsumed();
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));

        ddlLocation.Items.Add(new DevExpress.Web.ListEditItem("All", Constants.IntNullValue.ToString()));

        clsWebFormUtil.FillDxComboBoxList(ddlLocation, dt, 0, 2);

        if (dt.Rows.Count > 0)
        {
            ddlLocation.SelectedIndex = 0;
        }
    }
}