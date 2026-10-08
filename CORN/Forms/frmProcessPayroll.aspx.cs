using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Data;


public partial class Forms_frmProcessPayroll : System.Web.UI.Page
{

    EmployeeSalary_Controller esc = new EmployeeSalary_Controller();
    DataControl dc = new DataControl();
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
   
            LoadDistributor();
            LoadDesignation();
            FillRepeater();
        }

    }
    private void LoadDesignation()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.Employee_Depoartment_Id, null, Constants.IntNullValue, true);
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
    protected void rProcessSalary_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {


   
        CheckBox ChbAll = (CheckBox)rProcessSalary.FindControl("chbAll");
        if (ChbAll.Checked == true)
        {
            foreach (RepeaterItem dr in rProcessSalary.Items)
            {
                CheckBox ChbInvoice = (CheckBox)dr.FindControl("chbSelect");
                ChbInvoice.Checked = true;
            }
        }
        else
        {
            foreach (RepeaterItem dr in rProcessSalary.Items)
            {
                CheckBox ChbInvoice = (CheckBox)dr.FindControl("chbSelect");
                ChbInvoice.Checked = false;
            }
        }
    }

    private void FillRepeater()
    {
        rProcessSalary.DataSource = null;
        rProcessSalary.DataBind();
        lblTotalNoOfRecords.Text = "";
        lblCurrentPageNo.Text = "";
        lblTotalNoOfPages.Text = "";
        PagedDataSource PagedResults = new PagedDataSource();
        PagedResults.AllowPaging = true;
        PagedResults.PageSize = 20;

      DataTable dt = esc.Get_Employee_Salary(int.Parse (DrpDistributor .SelectedValue ),int .Parse (ddDesignation .SelectedValue ),DateTime .Parse (txtSalaryMonth .Text ));

       PagedResults.DataSource = dt.DefaultView;
        if (PagedResults.Count > 0)
        {
            PagedResults.CurrentPageIndex = CurrentPage;
            linkbtnnext.Enabled = !PagedResults.IsLastPage;
            linkbtnprev.Enabled = !PagedResults.IsFirstPage;
            rProcessSalary.DataSource = PagedResults;
            rProcessSalary.DataBind();

            linkbtnnext.Visible = true;
            linkbtnprev.Visible = true;
            lblTotalNoOfPages.Visible = true;
            lblCurrentPageNo.Visible = true;
            lblTotalNoOfRecords.Visible = true;
            lblDummy.Visible = true;
            lblOf.Visible = true;
            lblTotalNoOfRecords.Text = dt.Rows.Count.ToString();
            lblCurrentPageNo.Text = (CurrentPage + 1).ToString();
            lblTotalNoOfPages.Text = Convert.ToString(Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / Convert.ToDecimal(20)));

        }
        else if (PagedResults.Count == 0)
        {
            linkbtnnext.Visible = false;
            linkbtnprev.Visible = false;
            lblTotalNoOfPages.Visible = false;
            lblCurrentPageNo.Visible = false;
            lblTotalNoOfRecords.Visible = false;
            lblOf.Visible = false;
            lblDummy.Visible = false;
            lblTotalNoOfRecords.Text = "";
        }
    }

    public int CurrentPage
    {
        get
        {
            object objview = this.ViewState["_CurrentPage"];
            if (objview == null)
                return 0;
            else
                return (int)objview;
        }
        set
        {
            this.ViewState["_CurrentPage"] = value;
        }
    }

    protected void linkbtnprev_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        FillRepeater();
    }

    protected void linkbtnnext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        FillRepeater();
    }
    protected void DrpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillRepeater();
    }
    protected void ddDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillRepeater();
    }
    protected void txtSalaryMonth_TextChanged(object sender, EventArgs e)
    {
        FillRepeater();
    }
    protected void ibtnSalaryMonth_Click(object sender, ImageClickEventArgs e)
    {
        FillRepeater();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool Status = false;
        bool flag = false;
        if (rProcessSalary.Items.Count > 0)
        {
            foreach (RepeaterItem dr in rProcessSalary.Items)
            {
                CheckBox ChbInvoice = (CheckBox)dr.FindControl("chbSelect");
                if (ChbInvoice.Checked == true)
                {
                    Status = true;
                    HiddenField hfSALARY_ID = (HiddenField)dr.FindControl("hfSALARY_ID");
                    HiddenField hdnEmployeeID = (HiddenField)dr.FindControl("hfCUSTOMER_ID");

                    Label lblbasicSalary = (Label)dr.FindControl("lblBasicSalary");
                    Label lblAllowances = (Label)dr.FindControl("lblAllowances");
                    Label lbldeduction = (Label)dr.FindControl("lblDeduction");
                    Label lblNetAmount = (Label)dr.FindControl("lblNetAmount");

                    int Employee_ID = Convert.ToInt32(dc.chkNull_0(hdnEmployeeID.Value));
                    decimal BasicSalary = Convert.ToDecimal(dc.chkNull_0(lblbasicSalary.Text));
                    decimal Allowance = Convert.ToDecimal(dc.chkNull_0(lblAllowances.Text));

                    decimal Deduction = Convert.ToDecimal(dc.chkNull_0(lbldeduction.Text));
                    decimal NetAmount = Convert.ToDecimal(dc.chkNull_0(lblNetAmount.Text));
                    int Salary_ID = Convert.ToInt32(dc.chkNull_0(hfSALARY_ID.Value));
                    try
                    {

                        flag = esc.InsertSalary(DateTime.Parse(txtSalaryMonth.Text), int.Parse(DrpDistributor.SelectedValue), int.Parse(ddDesignation.SelectedValue), Salary_ID, 0, Employee_ID, BasicSalary, Allowance, Deduction, 0, int.Parse(this.Session["UserId"].ToString()), 0, 0, 0);


                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error !')", true);
                    }
                }
            }
            if (flag == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Updated !')", true);
                foreach (RepeaterItem ss in rProcessSalary.Items)
                {
                    CheckBox ChbInvoicess = (CheckBox)ss.FindControl("chbSelect");
                    ChbInvoicess.Checked = false;
                }

            }
            

            if (Status==false )
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No Record selected!')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No Record Found !')", true);
        }
    }
    protected void SetAllChecks(object sender, EventArgs e)
    {
        //foreach (RepeaterItem dr in rProcessSalary.Items)
        //{
        //    CheckBox ChbAll = (CheckBox)dr.FindControl("chbAll");
        //}

        //if (ChbAll.Checked == true)
        //{
        //    foreach (RepeaterItem dr in rProcessSalary.Items)
        //    {
        //        CheckBox ChbInvoice = (CheckBox)dr.FindControl("chbSelect");
        //        ChbInvoice.Checked = true;
        //    }
        //}
        //else
        //{
        //    foreach (RepeaterItem dr in rProcessSalary.Items)
        //    {
        //        CheckBox ChbInvoice = (CheckBox)dr.FindControl("chbSelect");
        //        ChbInvoice.Checked = false;
        //    }
        //}

    }
}