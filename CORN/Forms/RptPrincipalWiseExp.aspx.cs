using System;
using System.Data;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;

/// <summary>
/// Form For Petty Expense Summary Report
/// </summary>
public partial class Forms_RptPrincipalWiseExp : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadAccountParent();
            this.LoadAccountHead();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtFromDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtToDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtFromDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
        }
    }



    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DrpLocation.Items.Clear();
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        DrpLocation.Items.Add(new DevExpress.Web.ListEditItem("All", Constants.IntNullValue.ToString()));

        clsWebFormUtil.FillDxComboBoxList(DrpLocation, dt, 0, 2);

        if (dt.Rows.Count > 0)
        {
            DrpLocation.SelectedIndex = 0;
        }

    }

    /// <summary>
    /// Loads Parent Account Heads To Parent Account Combo
    /// </summary>
    private void LoadAccountParent()
    {
        Configuration.GetAccountHead();
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_DetailTypeId, long.Parse(Configuration.ExpensDefaultType), 1);
        clsWebFormUtil.FillDxComboBoxList(DrpMasterHead, dt, 0, 4, true);
        if (dt.Rows.Count > 0)
        {
            DrpMasterHead.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Loads Account Heaeds To Account Combo
    /// </summary>
    private void LoadAccountHead()
    {
        if (DrpMasterHead.Items.Count > 0)
        {
            AccountHeadController mAccountController = new AccountHeadController();
            DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(DrpMasterHead.SelectedItem.Value.ToString()), 1);
            clsWebFormUtil.FillListBox(LstAccountHead, dt, 0, 4, true);
        }
    }

    /// <summary>
    /// Loads Account Heads
    /// </summary>
    protected void DrpMasterHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadAccountHead();
    }

    /// <summary>
    /// Shows Petty Expense Summary in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Petty Expense Summary in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcell_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    /// <summary>
    /// Shows Petty Expense Summary Report Either in Excel or PDF
    /// </summary>
    /// <param name="p_ReportType"></param>
    private void ShowReport(int p_ReportType)
    {
        try
        {
            CORNBusinessLayer.Classes.DocumentPrintController DPrint = new CORNBusinessLayer.Classes.DocumentPrintController();
            RptAccountController RptAccountCtl = new RptAccountController();

            DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
            DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
            string FromDate = parsed_date_fromdate.ToShortDateString();
            string ToDate = parsed_date_todate.ToShortDateString();
            string Catagories_IDs = null;

            CORNBusinessLayer.Reports.CrpPrincipalWiseExp CrpReport = new CORNBusinessLayer.Reports.CrpPrincipalWiseExp();

            DataSet ds = null;

            for (int i = 0; i < this.LstAccountHead.Items.Count; i++)
            {
                if (this.LstAccountHead.Items[i].Selected == true)
                {
                    Catagories_IDs += this.LstAccountHead.Items[i].Value.ToString() + ",";
                }
            }

            ds = RptAccountCtl.PrincipalWiseSale(int.Parse(this.Session["UserId"].ToString()), Convert.ToInt32(this.DrpLocation.SelectedItem.Value.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 23:59:59"), Catagories_IDs, Constants.IntNullValue);
            DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedItem.Value.ToString()));

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Principal", "");
            CrpReport.SetParameterValue("Distributor", this.DrpLocation.SelectedItem.Text.ToString());
            CrpReport.SetParameterValue("From_date", this.txtFromDate.Text);
            CrpReport.SetParameterValue("To_Date", this.txtToDate.Text);
            CrpReport.SetParameterValue("ReportTitle", "Petty Expense Summary");
            CrpReport.SetParameterValue("AccountHead", DrpMasterHead.SelectedItem.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", p_ReportType);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
}
