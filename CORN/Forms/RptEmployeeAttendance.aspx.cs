using System;
using System.Web;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Data;

public partial class Forms_RptCustomerWiseSale : System.Web.UI.Page
{
    readonly SkuController SKUCtl = new SkuController();
    readonly Distributor_UserController mDUController = new Distributor_UserController();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");
        if (!IsPostBack)
        {
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtFromMonth.Text = Configuration.SystemCurrentDateTime.ToString("MMM-yyyy");

            txtFromMonth.Attributes.Add("readonly", "readonly");

            fillddlLocation();
            LoadDepartment();
            if (ddlLocation.Items.Count > 0 && drpDepartment.Items.Count>0)
            {
                this.fillddlEmployee(Convert.ToInt32(ddlLocation.SelectedItem.Value), Convert.ToInt32(drpDepartment.SelectedItem.Value) );
            }
        }
    }
    private void fillddlLocation()
    {
        DistributorController mController = new DistributorController();
        DataTable dtDistributor = mController.SelectDistributor(Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
       
        clsWebFormUtil.FillDxComboBoxList(ddlLocation, dtDistributor, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");

        if (dtDistributor.Rows.Count > 0)
        {
            ddlLocation.SelectedIndex = 0;
        }

    }
    private void LoadDepartment()
    {
        SLASHCodesController mController = new SLASHCodesController();
        try
        {
            DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.Employee_Depoartment_Id, null, Constants.IntNullValue, true);


            drpDepartment.Items.Add(new DevExpress.Web.ListEditItem("All", Constants.IntNullValue.ToString()));

            clsWebFormUtil.FillDxComboBoxList(drpDepartment, m_dt, "REF_ID", "SLASH_DESC");

            if (m_dt.Rows.Count > 0)
            {
                drpDepartment.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void fillddlEmployee(int LocationID, int DepartmentID)
    {
        Distributor_UserController UserCtl = new Distributor_UserController();

        ddlEmployee.Items.Clear();

        DataTable dt = UserCtl.SelectDistributorUser(8, LocationID, DepartmentID);

        ddlEmployee.Items.Add(new DevExpress.Web.ListEditItem("All", Constants.IntNullValue.ToString()));

        clsWebFormUtil.FillDxComboBoxList(ddlEmployee, dt, "USER_ID", "USER_NAME");

        if (dt.Rows.Count > 0)
        {
            ddlEmployee.SelectedIndex = 0;
        }

    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DepartmentID = int.Parse(drpDepartment.SelectedItem.Value.ToString());
        int LocationID = int.Parse(ddlLocation.SelectedItem.Value.ToString());
        //if (ddlLocation.SelectedIndex != 0)
        //{
        //    LocationID = Int32.Parse(ddlLocation.SelectedValue);
        //}
        fillddlEmployee(LocationID, DepartmentID);
    }

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        CORNBusinessLayer.Reports.CrpEmployeeAttendance CrpReport = new CORNBusinessLayer.Reports.CrpEmployeeAttendance();
        DataSet ds = new DataSet();
        DateTime monthdate = DateTime.Parse(txtFromMonth.Text);
        ds = SKUCtl.SelectMonthalyAttendance(Convert.ToInt32(ddlLocation.SelectedItem.Value), Convert.ToInt32(drpDepartment.SelectedItem.Value), Convert.ToInt32(ddlEmployee.SelectedItem.Value), monthdate);

        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("Location", ddlLocation.SelectedItem.Text);
        CrpReport.SetParameterValue("Department", drpDepartment.SelectedItem.Text);
        CrpReport.SetParameterValue("Date", txtFromMonth.Text);
        CrpReport.SetParameterValue("ReportType", "Employee Attendance Report ");
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);


    }
    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DepartmentID = Convert.ToInt32(drpDepartment.SelectedItem.Value);
        int LocationID = Convert.ToInt32(ddlLocation.SelectedItem.Value);
        fillddlEmployee(LocationID, DepartmentID);
    }
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {

        CORNBusinessLayer.Reports.CrpEmployeeAttendance CrpReport = new CORNBusinessLayer.Reports.CrpEmployeeAttendance();
        DataSet ds = new DataSet();
        DateTime monthdate = DateTime.Parse(txtFromMonth.Text);
        ds = SKUCtl.SelectMonthalyAttendance(Convert.ToInt32(ddlLocation.SelectedItem.Value), Convert.ToInt32(drpDepartment.SelectedItem.Value), Convert.ToInt32(ddlEmployee.SelectedItem.Value), monthdate);

        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Location", ddlLocation.SelectedItem.Text);
        CrpReport.SetParameterValue("Department", drpDepartment.SelectedItem.Text);
        CrpReport.SetParameterValue("Date", txtFromMonth.Text);
        CrpReport.SetParameterValue("ReportType", "Employee Attendance Report ");

        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 1);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);

    }



   
}