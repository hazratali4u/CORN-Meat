using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;
using System.Web;
/// <summary>
/// Form For Voucher View Report
/// </summary>
public partial class Forms_frmVoucherView : System.Web.UI.Page
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
            this.VoucherType();
            btnView.Attributes.Add("onclick", "return ValidateForm();");
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            DrpVoucherType_SelectedIndexChanged(null, null);

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));

        clsWebFormUtil.FillDxComboBoxList(drpDistributor, dt, 0, 2);

        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Loads Voucher Types To VoucherType Combo
    /// </summary>
    private void VoucherType()
    {
        LedgerController LController = new LedgerController();
        DataTable dt = LController.SelectVoucherType(int.Parse(this.Session["UserId"].ToString()));
        clsWebFormUtil.FillDxComboBoxList(this.DrpVoucherType, dt, 0, 1, true);
        if (dt.Rows.Count > 0)
        {
            DrpVoucherType.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Loads Vouchers To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnView_Click(object sender, EventArgs e)
    {
        LedgerController mController = new LedgerController();

        int VoucherType = Constants.IntNullValue;
        int VoucherType2 = Constants.IntNullValue;

        if (DrpVoucherType.SelectedItem.Value.ToString() == "16")
        {
            VoucherType = Convert.ToInt32(DrpVoucherType.SelectedItem.Value);
            VoucherType2 = Convert.ToInt32(DrpVoucherType.SelectedItem.Value);
        }
        else if (DrpVoucherType.SelectedItem.Value.ToString() == "14" || DrpVoucherType.SelectedItem.Value.ToString() == "15")
        {
            if (DrpVoucherSubType.SelectedItem.Value.ToString() == Constants.IntNullValue.ToString())
            {
                VoucherType2 = Convert.ToInt32(DrpVoucherType.SelectedItem.Value);
            }
            else
            {
                VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedItem.Value);
            }
        }

        int mUserID = Constants.IntNullValue;
        if (this.Session["RoleID"].ToString() == "3")
        {
            mUserID = Convert.ToInt32(this.Session["UserID"]);
        }
        DataTable dt = mController.SelectUnPostVoucherNo(VoucherType, int.Parse(drpDistributor.SelectedItem.Value.ToString()), mUserID, false, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), VoucherType2);
        GrdLedger.DataSource = dt;
        GrdLedger.DataBind();
    }

    /// <summary>
    /// Shows Voucher in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdLedger_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        crpVoucherView CrpReport = new crpVoucherView();

        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedItem.Value.ToString()), GrdLedger.Rows[e.NewEditIndex].Cells[0].Text, Convert.ToInt32(GrdLedger.Rows[e.NewEditIndex].Cells[5].Text));
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        if (DrpVoucherType.SelectedItem.Value.ToString() == "14" || DrpVoucherType.SelectedItem.Value.ToString() == "15")
        {
            CrpReport.SetParameterValue("VoucherType", DrpVoucherType.SelectedItem.Text);
            CrpReport.SetParameterValue("VoucherSubType", DrpVoucherSubType.SelectedItem.Text);
        }
        else
        {
            CrpReport.SetParameterValue("VoucherType", "Journal Voucher");

            CrpReport.SetParameterValue("VoucherSubType", "Journal Voucher");
        }
        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    protected void DrpVoucherType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DrpVoucherSubType.Items.Clear();
        DrpVoucherSubType.Value = "";

        if (DrpVoucherType.SelectedItem.Value.ToString() == "14")
        {
            DrpVoucherSubType.Items.Add(new DevExpress.Web.ListEditItem("Cash Receipt Voucher", "14"));
            DrpVoucherSubType.Items.Add(new DevExpress.Web.ListEditItem("Cash Payment Voucher", "24"));

            DrpVoucherSubType.Enabled = true;

            DrpVoucherSubType.SelectedIndex = 0;
        }

        else if (DrpVoucherType.SelectedItem.Value.ToString() == "15")
        {

            DrpVoucherSubType.Items.Add(new DevExpress.Web.ListEditItem("Bank Receipt Voucher", "15"));
            DrpVoucherSubType.Items.Add(new DevExpress.Web.ListEditItem("Bank Payment Voucher", "17"));

            DrpVoucherSubType.Enabled = true;

            DrpVoucherSubType.SelectedIndex = 0;
        }
        else
        {
            DrpVoucherSubType.Enabled = false;
        }
    }
}