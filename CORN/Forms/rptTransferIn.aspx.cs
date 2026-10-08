using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;

/// <summary>
/// Form For Transfer In/Out Report
/// </summary>
public partial class Forms_rptTransferIn : System.Web.UI.Page
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

            LoadLocation();
            this.txtFromDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.txtToDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            txtFromDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>


    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    protected void LoadLocation()
    {
        DrpLocation.Items.Clear();
        if (DrpReportType.SelectedIndex == 2)
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            DrpLocation.Items.Add(new DevExpress.Web.ListEditItem("All", Constants.IntNullValue.ToString()));

            clsWebFormUtil.FillDxComboBoxList(DrpLocation, dt, 0, 2);

            if (dt.Rows.Count > 0)
            {
                DrpLocation.SelectedIndex = 0;
            }
        }
        else
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDxComboBoxList(this.DrpLocation, dt, 0, 2);

            if (dt.Rows.Count > 0)
            {
                DrpLocation.SelectedIndex = 0;
            }
        }

    }

    /// <summary>
    /// Shows Transfer In/Out Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        try
        {
            if (DrpReportType.SelectedIndex == 2)
            {
                CORNBusinessLayer.Classes.DocumentPrintController DPrint = new CORNBusinessLayer.Classes.DocumentPrintController();
                RptInventoryController RptInventoryCtl = new RptInventoryController();

                string FromDate = null;
                string ToDate = null;
                string TransferType = null;
                string TransferTo_FromHeader = null;
                CORNBusinessLayer.Reports.CrpTransferInOutValues CrpReport = new CORNBusinessLayer.Reports.CrpTransferInOutValues();
                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
                DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
                FromDate = parsed_date_fromdate.ToShortDateString();
                ToDate = parsed_date_todate.ToShortDateString();

                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedItem.Value.ToString()));

                DataSet ds = RptInventoryCtl.TransferInOutValue(Constants.IntNullValue, int.Parse(DrpLocation.SelectedItem.Value.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), this.RbTransferType.SelectedItem.Text, int.Parse(this.Session["UserId"].ToString()));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                FromDate = this.txtFromDate.Text;
                ToDate = this.txtToDate.Text;
                if (this.RbTransferType.SelectedItem.Text == "Transfer In")
                {
                    TransferType = "Branch Transfer In Report value wise";
                    TransferTo_FromHeader = "Transfer From";
                }
                else
                {
                    TransferType = "Branch Transfer Out Report value wise";
                    TransferTo_FromHeader = "Transfer To";
                }
                CrpReport.SetParameterValue("FromDate", FromDate);
                CrpReport.SetParameterValue("ToDate", ToDate);
                CrpReport.SetParameterValue("Principal", "");
                CrpReport.SetParameterValue("TransferTo_FromHeader", TransferType);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 0);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else
            {
                CORNBusinessLayer.Classes.DocumentPrintController DPrint = new CORNBusinessLayer.Classes.DocumentPrintController();
                RptInventoryController RptInventoryCtl = new RptInventoryController();

                string FromDate = null;
                string ToDate = null;
                string TransferType = null;
                string TransferTo_FromHeader = null;
                CORNBusinessLayer.Reports.CrpTransferIn CrpReport = new CORNBusinessLayer.Reports.CrpTransferIn();
                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
                DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
                FromDate = parsed_date_fromdate.ToShortDateString();
                ToDate = parsed_date_todate.ToShortDateString();

                DataSet ds = RptInventoryCtl.TransferIn(Constants.IntNullValue, int.Parse(DrpLocation.SelectedItem.Value.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), this.RbTransferType.SelectedItem.Text, DrpReportType.SelectedIndex);
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedItem.Value.ToString()));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("BranchName", this.DrpLocation.SelectedItem.Text.ToString());
                FromDate = this.txtFromDate.Text;
                ToDate = this.txtToDate.Text;
                if (this.RbTransferType.SelectedItem.Text == "Transfer In")
                {
                    TransferType = "Branch Transfer In Report";
                    TransferTo_FromHeader = "Transfer From";
                }
                else
                {
                    TransferType = "Branch Transfer Out Report";
                    TransferTo_FromHeader = "Transfer To";
                }
                CrpReport.SetParameterValue("FromDate", FromDate);
                CrpReport.SetParameterValue("ToDate", ToDate);
                CrpReport.SetParameterValue("TransferType", TransferType);
                CrpReport.SetParameterValue("Principal", "");
                CrpReport.SetParameterValue("TransferTo_FromHeader", TransferTo_FromHeader);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 0);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Shows Transfer In/Out Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (DrpReportType.SelectedIndex == 2)
            {
                CORNBusinessLayer.Classes.DocumentPrintController DPrint = new CORNBusinessLayer.Classes.DocumentPrintController();
                RptInventoryController RptInventoryCtl = new RptInventoryController();

                string FromDate = null;
                string ToDate = null;
                string TransferType = null;
                string TransferTo_FromHeader = null;
                CORNBusinessLayer.Reports.CrpTransferInOutValues CrpReport = new CORNBusinessLayer.Reports.CrpTransferInOutValues();
                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
                DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
                FromDate = parsed_date_fromdate.ToShortDateString();
                ToDate = parsed_date_todate.ToShortDateString();

                DataSet ds = RptInventoryCtl.TransferInOutValue(Constants.IntNullValue, int.Parse(DrpLocation.SelectedItem.Value.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), this.RbTransferType.SelectedItem.Text, int.Parse(this.Session["UserId"].ToString()));
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedItem.Value.ToString()));
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();


                FromDate = this.txtFromDate.Text;
                ToDate = this.txtToDate.Text;
                if (this.RbTransferType.SelectedItem.Text == "Transfer In")
                {
                    TransferType = "Branch Transfer In Report value wise";
                    TransferTo_FromHeader = "Transfer From";
                }
                else
                {
                    TransferType = "Branch Transfer Out Report value wise";
                    TransferTo_FromHeader = "Transfer To";
                }
                CrpReport.SetParameterValue("FromDate", FromDate);
                CrpReport.SetParameterValue("ToDate", ToDate);
                CrpReport.SetParameterValue("Principal", "");
                CrpReport.SetParameterValue("TransferTo_FromHeader", TransferType);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 1);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else
            {
                CORNBusinessLayer.Classes.DocumentPrintController DPrint = new CORNBusinessLayer.Classes.DocumentPrintController();
                RptInventoryController RptInventoryCtl = new RptInventoryController();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedItem.Value.ToString()));
                string FromDate = null;
                string ToDate = null;
                string TransferType = null;
                string TransferTo_FromHeader = null;
                CORNBusinessLayer.Reports.CrpTransferIn CrpReport = new CORNBusinessLayer.Reports.CrpTransferIn();
                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
                DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
                FromDate = parsed_date_fromdate.ToShortDateString();
                ToDate = parsed_date_todate.ToShortDateString();

                DataSet ds = RptInventoryCtl.TransferIn(Constants.IntNullValue, int.Parse(DrpLocation.SelectedItem.Value.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), this.RbTransferType.SelectedItem.Text, DrpReportType.SelectedIndex);

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("BranchName", this.DrpLocation.SelectedItem.Text.ToString());
                FromDate = this.txtFromDate.Text;
                ToDate = this.txtToDate.Text;
                if (this.RbTransferType.SelectedItem.Text == "Transfer In")
                {
                    TransferType = "Branch Transfer In Report";
                    TransferTo_FromHeader = "Transfer From";
                }
                else
                {
                    TransferType = "Branch Transfer Out Report";
                    TransferTo_FromHeader = "Transfer To";
                }
                CrpReport.SetParameterValue("FromDate", FromDate);
                CrpReport.SetParameterValue("ToDate", ToDate);
                CrpReport.SetParameterValue("TransferType", TransferType);
                CrpReport.SetParameterValue("Principal", "");
                CrpReport.SetParameterValue("TransferTo_FromHeader", TransferTo_FromHeader);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 1);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Loads Locations
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadLocation();
    }

}
