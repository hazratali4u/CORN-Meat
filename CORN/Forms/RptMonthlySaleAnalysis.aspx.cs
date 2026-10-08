using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNBusinessLayer.Reports;
using CORNCommon.Classes;
using System.Globalization;
using System.Web;

/// <summary>
/// Form For Monthly Sale Report
/// </summary>
public partial class Forms_RptMonthlySaleAnalysis : System.Web.UI.Page
{
    readonly DocumentPrintController mDocumentPrntControl = new DocumentPrintController();
    readonly RptSaleController RptSaleCtl = new RptSaleController();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");
        if (!Page.IsPostBack)
        {
            DistributorType();
            LoadAssingned();



            SetDivs();
            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];

            txtStartYear.Text = Configuration.SystemCurrentDateTime.ToString("yyyy");
            txtEndYear.Text = Configuration.SystemCurrentDateTime.ToString("yyyy");
            txtMonth.Text = Configuration.SystemCurrentDateTime.ToString("MMM");

            txtFromMonth.Text = Configuration.SystemCurrentDateTime.ToString("MMM-yyyy");
            txtToMonth.Text = Configuration.SystemCurrentDateTime.ToString("MMM-yyyy");

            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtStartYear.Attributes.Add("readonly", "readonly");
            txtEndYear.Attributes.Add("readonly", "readonly");
            txtFromMonth.Attributes.Add("readonly", "readonly");
            txtToMonth.Attributes.Add("readonly", "readonly");
            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }
    }
    static DateTime FirstDateOfWeek(int year, int weekOfYear)
    {
    
        DateTime jan1 = new DateTime(year, 1, 1);

        int daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;// -(int)jan1.DayOfWeek;

        DateTime firstMonday = jan1.AddDays(daysOffset);

        int firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

        if (firstWeek <= 1)
        {
            weekOfYear -= 1;
        }

        return firstMonday.AddDays(weekOfYear * 7);
    }
    static DateTime LastDateOfWeek(int year, int weekOfYear)
    {

        DateTime jan1 = new DateTime(year, 1, 1);

        int daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;

        DateTime firstMonday = jan1.AddDays(daysOffset);

        int firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

        if (firstWeek <= 1)
        {
            weekOfYear -= 1;
        }

        return firstMonday.AddDays((weekOfYear) * 7 );
    }
    /// <summary>
    /// Loads LocationTypes To LocationType Combo
    /// </summary>

    #region Load

    private void DistributorType()
    {
        DistributorController dController = new DistributorController();
        DataTable dt = dController.SelectDistributorTypeInfo(Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(ddDistributorType, dt, 0, 2);
    }
    private void LoadAssingned()
    {
        if (ddDistributorType.Items.Count > 0)
        {
            drpDistributor.Items.Clear();
            UserController mUserController = new UserController();

            DataTable dt = mUserController.SelectUserAssignment(int.Parse(Session["UserId"].ToString()), int.Parse(ddDistributorType.SelectedValue.ToString()), 1, int.Parse(Session["CompanyId"].ToString()));
            drpDistributor.Items.Add(new DevExpress.Web.ListEditItem("All", Constants.IntNullValue.ToString(CultureInfo.InvariantCulture)));

            clsWebFormUtil.FillDxComboBoxList(drpDistributor, dt, 0, 1);

            if (dt.Rows.Count > 0)
            {
                drpDistributor.SelectedIndex = 0;
            }
        }
    }
   
    //private void LoadYear()
    //{
    //    RptSaleController RptSaleCtl = new RptSaleController();
    //    DataTable m_dt = RptSaleCtl.GetWeakofYear(0, null);

    //    clsWebFormUtil.FillDropDownList(DrpYear, m_dt, 0, 0, true);
    //}
    //private void LoadWeakFrom()
    //{
    //    RptSaleController RptSaleCtl = new RptSaleController();
    //    DataTable m_dt = RptSaleCtl.GetWeakofYear(1, DrpYear.SelectedItem.Text);
    //    clsWebFormUtil.FillDropDownList(DrpWeekFrom, m_dt, 1, 0, true);
    //}
    //private void LoadWeakTo()
    //{
    //    RptSaleController RptSaleCtl = new RptSaleController();
    //    DataTable m_dt = RptSaleCtl.GetWeakofYear(1, DrpYear.SelectedItem.Text);
    //    clsWebFormUtil.FillDropDownList(DrpWeekTo, m_dt, 2, 0, true);
    //}

    #endregion

    /// <summary>
    /// Loads Assigned Locations
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddDistributorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAssingned();
    }
    protected void DrpYear_SelectedIndexChanged(object sender, EventArgs e)
    {
     
      //  txtStartDate.Text = FirstDateOfWeek(int.Parse(DrpYear.SelectedValue), int.Parse(DrpWeekFrom.SelectedValue)).ToString("dd-MMM-yyyy");
     //   txtEndDate.Text = FirstDateOfWeek(int.Parse(DrpYear.SelectedValue), int.Parse(DrpWeekTo.SelectedValue)).ToString("dd-MMM-yyyy");

    }

    /// <summary>
    /// Shows Report in Excel Or PDF
    /// </summary>
    /// <param name="pReportType">ReportType</param>
    private void ShowReport(int pReportType)
    {
        DateTime dtFrom;
        DateTime dtTo;
        int dtMonth = 0;

        if (DrpUnitType.SelectedItem.Value.ToString() == "0")
        {
            dtFrom = new DateTime(Convert.ToInt32(txtStartYear.Text), 1, 1);
            dtTo = new DateTime(Convert.ToInt32(txtEndYear.Text), 12, 31);

            if (txtMonth.Text == "Jan")
            {
                dtMonth = 1;
            }
            else if (txtMonth.Text == "Feb")
            {
                dtMonth = 2;
            }
            else if (txtMonth.Text == "Mar")
            {
                dtMonth = 3;
            }
            else if (txtMonth.Text == "Apr")
            {
                dtMonth = 4;

            }
            else if (txtMonth.Text == "May")
            {
                dtMonth = 5;
            }
            else if (txtMonth.Text == "Jun")
            {
                dtMonth = 6;
            }
            else if (txtMonth.Text == "Jul")
            {
                dtMonth = 7;
            }
            else if (txtMonth.Text == "Aug")
            {
                dtMonth = 8;
            }
            else if (txtMonth.Text == "Sep")
            {
                dtMonth = 9;
            }
            else if (txtMonth.Text == "Oct")
            {
                dtMonth = 10;
            }
            else if (txtMonth.Text == "Nov")
            {
                dtMonth = 11;
            }
            else
            {
                dtMonth = 12;
            }


        }
        else if (DrpUnitType.SelectedItem.Value.ToString() == "1")
        {
            DateTime dtFromMonth = DateTime.Parse(txtFromMonth.Text);
            dtFrom = new DateTime(dtFromMonth.Year, dtFromMonth.Month, 1);

            DateTime dtToMonth = DateTime.Parse(txtToMonth.Text);
            dtTo = new DateTime(dtToMonth.Year, dtToMonth.Month, 1);
            dtTo = dtTo.AddMonths(1).AddDays(-1);

        }
        else
        {

            dtFrom = Convert.ToDateTime(txtStartDate.Text);
            dtTo = Convert.ToDateTime(txtEndDate.Text);
        }
        if (DrpUnitType.SelectedItem.Value.ToString() == "0")
        {
           

            DataSet ds = RptSaleCtl.GetDistributorReconcilation2(byte.Parse(DrpUnitType.SelectedIndex.ToString()), Constants.IntNullValue, int.Parse(drpDistributor.SelectedItem.Value.ToString()), dtFrom, dtTo, int.Parse(Session["UserId"].ToString()), byte.Parse(DrpReportType.SelectedIndex.ToString()), byte.Parse(RadioButtonList1.SelectedIndex.ToString()), dtMonth);

            DataTable dt = mDocumentPrntControl.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));




            var crpReport = new CrpMonthSaleValume3();

            crpReport.SetDataSource(ds);
            crpReport.Refresh();

            crpReport.SetParameterValue("FromDate", txtStartYear.Text);
            crpReport.SetParameterValue("ToDate", txtEndYear.Text);
            crpReport.SetParameterValue("Principal", "");
            crpReport.SetParameterValue("CompanyName", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("ReportType", DrpReportType.SelectedItem.Text);
            crpReport.SetParameterValue("ParameterType", DrpUnitType.SelectedItem.Text);
            crpReport.SetParameterValue("Price", RadioButtonList1.SelectedItem.Text);

            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", pReportType);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else if (DrpUnitType.SelectedItem.Value.ToString() == "1")
        {
            DataSet ds = RptSaleCtl.GetDistributorReconcilation(byte.Parse(DrpUnitType.SelectedIndex.ToString()), Constants.IntNullValue, int.Parse(drpDistributor.SelectedItem.Value.ToString()), dtFrom, dtTo, int.Parse(Session["UserId"].ToString()), byte.Parse(DrpReportType.SelectedIndex.ToString()), byte.Parse(RadioButtonList1.SelectedIndex.ToString()));

            DataTable dt = mDocumentPrntControl.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));


            var crpReport = new CrpMonthSaleValume();

            crpReport.SetDataSource(ds);
            crpReport.Refresh();

            crpReport.SetParameterValue("FromDate", txtFromMonth.Text);
            crpReport.SetParameterValue("ToDate", txtToMonth.Text);
            crpReport.SetParameterValue("Principal", "");
            crpReport.SetParameterValue("CompanyName", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("ReportType", DrpReportType.SelectedItem.Text);
            crpReport.SetParameterValue("ParameterType", DrpUnitType.SelectedItem.Text);
            crpReport.SetParameterValue("Price", RadioButtonList1.SelectedItem.Text);

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

           

            DataSet ds = RptSaleCtl.GetDistributorReconcilation2(byte.Parse(DrpUnitType.SelectedIndex.ToString()), Constants.IntNullValue, int.Parse(drpDistributor.SelectedItem.Value.ToString()), dtFrom, dtTo, int.Parse(Session["UserId"].ToString()), byte.Parse(DrpReportType.SelectedIndex.ToString()), byte.Parse(RadioButtonList1.SelectedIndex.ToString()), Constants.IntNullValue);

            DataTable dt = mDocumentPrntControl.SelectReportTitle(int.Parse(drpDistributor.SelectedItem.Value.ToString()));


            var crpReport = new CrpMonthSaleValume2();

            crpReport.SetDataSource(ds);
            crpReport.Refresh();

            crpReport.SetParameterValue("FromDate", dtFrom);
            crpReport.SetParameterValue("ToDate", dtTo);
            crpReport.SetParameterValue("Principal", "");
            crpReport.SetParameterValue("CompanyName", drpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("ReportType", DrpReportType.SelectedItem.Text);
            crpReport.SetParameterValue("ParameterType", DrpUnitType.SelectedItem.Text);
            crpReport.SetParameterValue("Price", RadioButtonList1.SelectedItem.Text);

            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", pReportType);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }

    /// <summary>
    /// Shows Monthly Sale Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Monthly Sale Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    /// <summary>
    /// Sets Date, Month And Year Divisions Visibility
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpUnitType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetDivs();
    }

    /// <summary>
    /// Sets Date, Month And Year Divisions Visibility As Per Report Type
    /// </summary>
    private void SetDivs()
    {
        if (DrpUnitType.SelectedItem.Value.ToString() == "0")
        {
            divYear.Visible = true;
            divMonth.Visible = false;
            divDate.Visible = false;
           
        }
        
        else if (DrpUnitType.SelectedItem.Value.ToString() == "1")
        {
            divYear.Visible = false;
            divMonth.Visible = true;
            divDate.Visible = false;
           
        
        }
        else if (DrpUnitType.SelectedItem.Value.ToString() == "2")
        {
            divYear.Visible = false;
            divMonth.Visible = false;
            divDate.Visible = true;
           
        }
    }
}
   
