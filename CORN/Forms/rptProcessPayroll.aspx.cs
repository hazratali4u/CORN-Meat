using System;
using System.Data;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CrystalDecisions.CrystalReports.Engine;
using System.Web;

public partial class Forms_rptProcessPayroll : System.Web.UI.Page
{

    EmployeeSalary_Controller esc = new EmployeeSalary_Controller();
    DataControl dc = new DataControl();
     Distributor_UserController UController = new Distributor_UserController();
     EmployeController ccc = new EmployeController();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtSalaryMonth.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("MMM-yyyy");
            txtSalaryMonth.Attributes.Add("readonly", "readonly");
            LoadDistributor();
            LoadDesignation();
            LoadEmployee();
        }
        
    }
    private void LoadDesignation()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.Employee_Depoartment_Id, null, Constants.IntNullValue, true);

        DataRow dr = m_dt.NewRow();
        dr["SLASH_DESC"] = "All";
        dr["REF_ID"] = Constants .IntNullValue .ToString ();
        m_dt.Rows.InsertAt(dr, 0); 
        
        
        
        ddDesignation.DataSource = m_dt;
        ddDesignation.DataTextField = "SLASH_DESC";
        ddDesignation.DataValueField = "REF_ID";
        ddDesignation.DataBind();
    }
    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        DataTable dtDistributor = mController.SelectDistributor(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        DrpDistributor.DataSource = dtDistributor;
        DrpDistributor.DataTextField = "DISTRIBUTOR_NAME";
        DrpDistributor.DataValueField = "DISTRIBUTOR_ID";
        DrpDistributor.DataBind();
    }

    protected void DrpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadEmployee();
    }
    protected void ddDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEmployee();
    }
    protected void txtSalaryMonth_TextChanged(object sender, EventArgs e)
    {
    }
    protected void ibtnSalaryMonth_Click(object sender, ImageClickEventArgs e)
    {
       
    }
   
   private void LoadEmployee()
    {

        if (DrpDistributor.Items.Count > 0)
        {
            DataTable dtEmployee = UController.SelectDistributorUser(Constants.IntNullValue, int.Parse(DrpDistributor.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            if (dtEmployee.Rows.Count > 0)
            {
                if (int.Parse(ddDesignation.SelectedValue) != 0)
                {

                    DataTable dt4 = dtEmployee.Clone();
                    DataRow[] dr1 = dtEmployee.Select("USER_TYPE_ID = '" + ddDesignation.SelectedValue + "'");

                    if (dr1.Length > 0)
                    {
                        for (int k = 0; k < dr1.Length; k++)
                        {
                            dt4.ImportRow(dr1[k]);
                        }
                    }



                    DataRow dr = dt4.NewRow();
                    dr["USER_NAME"] = "All";
                    dr["USER_ID"] = Constants .IntNullValue .ToString ();

                    dt4.Rows.InsertAt(dr, 0);
                    DrpEmployee.DataSource = dt4;
                    DrpEmployee.DataTextField = "USER_NAME";
                    DrpEmployee.DataValueField = "USER_ID";
                    DrpEmployee.DataBind();
                }
                else
                {
                    DataRow dr = dtEmployee.NewRow();
                    dr["USER_NAME"] = "All";
                    dr["USER_ID"] = Constants.IntNullValue .ToString ();

                    dtEmployee.Rows.InsertAt(dr, 0);
                    DrpEmployee.DataSource = dtEmployee;
                    DrpEmployee.DataTextField = "USER_NAME";
                    DrpEmployee.DataValueField = "USER_ID";
                    DrpEmployee.DataBind();
                }
            }
            else
            {
                DrpEmployee.Items.Clear();
                DrpEmployee.DataSource = null;
                DrpEmployee.DataBind();
            }
        }
    }
   protected void DrpEmployee_SelectedIndexChanged(object sender, EventArgs e)
   {

   }
   protected void btnShow_Click(object sender, EventArgs e)
   {
     
   }
   protected void btnShow_Click1(object sender, EventArgs e)
   {
       DocumentPrintController DPrint = new DocumentPrintController();
       EmployeeSalary_Controller RptsalarySlip = new EmployeeSalary_Controller();
       DataSet ds = null;
       DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpDistributor.SelectedValue.ToString()));

       DataControl dc = new DataControl();
       ds = RptsalarySlip.payslip(int.Parse(DrpDistributor.SelectedValue.ToString()), int.Parse(ddDesignation.SelectedValue.ToString()),
           DateTime.Parse(txtSalaryMonth.Text), int.Parse(DrpEmployee.SelectedValue));

       ReportDocument SubReportAllowaances = new ReportDocument();
      ReportDocument SubReportdeduction = new ReportDocument();
       
       CORNBusinessLayer.Reports.crpPaySlip CrpReport = new CORNBusinessLayer.Reports.crpPaySlip();

       SubReportAllowaances = CrpReport.OpenSubreport("SubReportA");
       SubReportdeduction = CrpReport.OpenSubreport("SubReportB");
       
       CrpReport.SetDataSource(ds);
       SubReportAllowaances.SetDataSource(ds);
       SubReportdeduction.SetDataSource(ds);
       
       CrpReport.Refresh();


       CrpReport.SetParameterValue("Month", DateTime.Parse(txtSalaryMonth .Text));
       //CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
     CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
       //CrpReport.SetParameterValue("DISTRIBUTOR_NAME", DrpDistributor.SelectedItem.Text);

       Session.Add("ReportType", 0);
       Session.Add("CrpReport", CrpReport);
       const string url = "'Default.aspx'";
       const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
       Type cstype = this.GetType();
       ClientScriptManager cs = Page.ClientScript;
       cs.RegisterStartupScript(cstype, "OpenWindow", script);

   }
}