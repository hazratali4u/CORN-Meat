using System;
using System.Data;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;
using System.Web;

public partial class Forms_RptRawIssuanceReport : Page
{
    readonly DocumentPrintController _dPrint = new DocumentPrintController();
    readonly RptSaleController _rptSaleCtl = new RptSaleController();
    readonly DistributorController _dController = new DistributorController();
    readonly SkuController _skuController = new SkuController();
    readonly RptInventoryController _rptInvenController = new RptInventoryController();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!IsPostBack)
        {
            LoadLocations();
            LoadSection();
            LoadItems();

            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }
    }


    private void LoadSection()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = _skuController.SelectProductSection(Constants.IntNullValue, null, null);
            clsWebFormUtil.FillListBox(chblSection, dt, 0, 2, true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    private void LoadItems()
    {
        try
        {
            string SectionId = null;
            int _value = 0;
            for (int i = 0; i < chblSection.Items.Count; i++)
            {
                if (chblSection.Items[i].Selected == true)
                {
                    _value = Convert.ToInt32(chblSection.Items[i].Value.ToString());
                    SectionId += _value + ",";
                }
            }

            DataTable dt = new DataTable();
            dt = _skuController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 14, Constants.IntNullValue, SectionId);
            clsWebFormUtil.FillListBox(chblItem, dt, 0, 1, true);
            if (chblItem.Items.Count > 0)
            {
                chbAllItems.Checked = true;
                for (int i = 0; i < chblItem.Items.Count; i++)
                {
                    chblItem.Items[i].Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    private void LoadLocations()
    {
        try
        {
            DataTable dt = _dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));

            clsWebFormUtil.FillDxComboBoxList(drpDistributor, dt, 0, 2, true);

            if (dt.Rows.Count > 0)
            {
                drpDistributor.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
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
        string ItemId = null;
        int _value = 0;
        for (int i = 0; i < chblItem.Items.Count; i++)
        {
            if (chblItem.Items[i].Selected == true)
            {
                _value = Convert.ToInt32(chblItem.Items[i].Value.ToString());
                ItemId += _value + ",";
            }
        }

        DataTable dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        DataSet ds;

        ds = _rptInvenController.SelectRawIssuenceReport(int.Parse(drpDistributor.SelectedItem.Value.ToString()),
            DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), ItemId, 1);
        var crpReport = new CrpRawIssuence();
        crpReport.SetDataSource(ds);
        crpReport.Refresh();
        crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        crpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
        crpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
        crpReport.SetParameterValue("ReportName", "Raw Material Issuence Report");
        crpReport.SetParameterValue("COMPANY", dt.Rows[0]["COMPANY_NAME"].ToString());

        Session.Add("CrpReport", crpReport);
        Session.Add("ReportType", pReprotType);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    protected void chblSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadItems();
    }
    protected void chbAllSections_CheckedChanged(object sender, EventArgs e)
    {
        if (chbAllSections.Checked)
        {
            LoadItems();
        }
        else
        {
            chbAllItems.Checked = false;
            //if (chblItem.Items.Count > 0)
            //{
            //    chblItem.DataSource = null;
            //    chblItem.DataBind();
            //}
        }
    }
}
