using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Data;


public partial class Forms_frmEmployeeSalary : System.Web.UI.Page
{
    Distributor_UserController UController = new Distributor_UserController();
    EmployeController ccc = new EmployeController();

    private static int AllowanceID;
    private static int DeductionID;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
         
            LoadDistributor();
            LoadEmployee();
            LoadAllowances();
            LoadDeductions();
            CreateAllowances();
            CreateDeductions();
            LoadDesignation();
            GetEmployeeSalery();
          
            }
    }

    #region DataTable Creation

    public void CreateAllowances()
    {
        DataTable dtAllowances = new DataTable();

        dtAllowances.Columns.Add("ALLOWANCE_ID", typeof(int));
        dtAllowances.Columns.Add("ALLOWANCE_DESC", typeof(string));
        dtAllowances.Columns.Add("ALLOWANCE_AMOUNT", typeof(decimal));

        this.Session.Add("dtAllowances", dtAllowances);
    }
    public void CreateDeductions()
    {
        DataTable dtDeductions = new DataTable();

        dtDeductions.Columns.Add("DEDUCTION_ID", typeof(int));
        dtDeductions.Columns.Add("DEDUCTION_DESC", typeof(string));
        dtDeductions.Columns.Add("DEDUCTION_AMOUNT", typeof(decimal));

        this.Session.Add("dtDeductions", dtDeductions);
    }

    #endregion

    #region Load 

    private void LoadAllowances()
    {
        DataTable dt = ccc.SelectAllowances(Constants.IntNullValue, Int32.Parse(Session["CompanyId"].ToString()));
        if (dt.Rows.Count <= 0) return;

        clsWebFormUtil.FillDropDownList(DrpAllowance, dt, 0, 2, true);
      
        DataRow[] foundRows = dt.Select("AllowanceID = '" + DrpAllowance.SelectedValue + "'");
        txtAllowanceAmount.Text = foundRows[0]["AllowanceRatio"].ToString();
     
            
    }
    private void LoadDeductions()
    {
       
        
            DataTable dt = ccc.SelectDeductions(Constants.IntNullValue, Int32.Parse(Session["CompanyId"].ToString()));
            if (dt.Rows.Count <= 0) return;

            clsWebFormUtil.FillDropDownList(DrpDeduction, dt, 0, 2, true);
         
            DataRow[] foundRows = dt.Select("DeductionId = '" + DrpDeduction.SelectedValue + "'");
            txtDeductionAmount .Text = foundRows[0]["DeductionRatio"].ToString();

        
       
    }

    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        DataTable dtDistributor = mController.SelectDistributor(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        if (dtDistributor.Rows.Count <= 0) return;
        DrpDistributor.DataSource = dtDistributor;
        DrpDistributor.DataTextField = "DISTRIBUTOR_NAME";
        DrpDistributor.DataValueField = "DISTRIBUTOR_ID";
        DrpDistributor.DataBind();
    }
    private void LoadEmployee()
    {

        if (DrpDistributor.Items.Count > 0)
        {
            DataTable dtEmployee = UController.SelectDistributorUser(Constants.IntNullValue, int.Parse(DrpDistributor.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            if (dtEmployee.Rows.Count <= 0) return;
            DrpEmployee.DataSource = dtEmployee;
            DrpEmployee.DataTextField = "USER_NAME";
            DrpEmployee.DataValueField = "USER_ID";
            DrpEmployee.DataBind();
        }
    }

    public void LoadGirdAllowances()
    {
        DataTable dtAllowances = (DataTable)this.Session["dtAllowances"];
        Grid_Allowances.DataSource = dtAllowances;
        Grid_Allowances.DataBind();
    }
    public void LoadGirdDeductions()
    {
        DataTable dtDeductions = (DataTable)this.Session["dtDeductions"];
        Grid_Deductions.DataSource = dtDeductions;
        Grid_Deductions.DataBind();
    }
 
    

    #endregion

    #region Sel/Index Change

    protected void DrpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtBasicSalary.Text = "";

        btnSave.Text = "Save";
        Grid_Allowances.DataSource = null;
        Grid_Allowances.DataBind();
        Grid_Deductions.DataSource = null;
        Grid_Deductions.DataBind();
        this.Session.Remove("dtAllowances");
        this.Session.Remove("dtDeductions");
        CreateAllowances();
        CreateDeductions();
        this.LoadEmployee();
        if (DrpEmployee.Items.Count > 0)
        {
              GetEmployeeSalery();
        }
     
    }
    protected void DrpEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSave.Text = "Save";
        LoadDesignation();
        GetEmployeeSalery();
    }
    private void LoadDesignation()
    {
        DataTable dt = UController.SelectDistributorUser(Constants.IntNullValue, int.Parse(DrpDistributor.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        DataRow[] foundRows = dt.Select("USER_ID = '" + DrpEmployee.SelectedValue + "'");

        lbl_Designation.Text = foundRows[0]["SLASH_DESC"].ToString();

        hdnDesignationID.Value = foundRows[0]["USER_TYPE_ID"].ToString(); 
    }

    protected void DrpAllowance_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = ccc.SelectAllowances(Constants.IntNullValue, Int32.Parse(Session["CompanyId"].ToString()));
       
        DataRow[] foundRows = dt.Select("AllowanceID = '" + DrpAllowance.SelectedValue + "'");
        txtAllowanceAmount.Text = foundRows[0]["AllowanceRatio"].ToString();

    }
    protected void DrpDeduction_SelectedIndexChanged(object sender, EventArgs e)
    {
       DataTable dt = ccc.SelectDeductions(Constants.IntNullValue, Int32.Parse(Session["CompanyId"].ToString()));
       
        DataRow[] foundRows = dt.Select("DeductionId = '" + DrpDeduction.SelectedValue + "'");
        txtDeductionAmount.Text = foundRows[0]["DeductionRatio"].ToString();
        
       
    }

    #endregion

    #region Click OPerations
    private bool CheckDublicateAllowance()
    {
       
      DataTable   dtAllowances = (DataTable)this.Session["dtAllowances"];
        DataRow[] foundRows = dtAllowances.Select("ALLOWANCE_ID  = '" + DrpAllowance.SelectedItem.Value  + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CheckDublicatededuction()
    {

        DataTable dtdeuction = (DataTable)this.Session["dtDeductions"];
        DataRow[] foundRows = dtdeuction.Select("DEDUCTION_ID  = '" + DrpDeduction.SelectedItem.Value + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void btnAdd_Allowance_Click(object sender, EventArgs e)
    {
        DataTable dtAllowances = (DataTable)this.Session["dtAllowances"];

        if (btnAdd_Allowance.Text == "Add")
        {
            if (CheckDublicateAllowance())
            {
                DataRow dr = dtAllowances.NewRow();

                dr["ALLOWANCE_DESC"] = DrpAllowance.SelectedItem.Text;
                dr["ALLOWANCE_ID"] = DrpAllowance.SelectedItem.Value;
                dr["ALLOWANCE_AMOUNT"] = txtAllowanceAmount.Text;

                dtAllowances.Rows.Add(dr);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Allowance Already Exists')", true);
            }
        }

        else
        {
            DataRow dr = dtAllowances.Rows[AllowanceID];

            dr["ALLOWANCE_DESC"] = DrpAllowance.SelectedItem.Text;
            dr["ALLOWANCE_ID"] = DrpAllowance.SelectedItem.Value;
            dr["ALLOWANCE_AMOUNT"] = txtAllowanceAmount.Text;


        }

        this.Session.Add("dtAllowances", dtAllowances);

        this.LoadGirdAllowances();
        ClearAllowanceDetail();

        ScriptManager.GetCurrent(Page).SetFocus(DrpAllowance);

    }
    protected void btnAdd_Deduction_Click(object sender, EventArgs e)
    {
        DataTable dtDeductions = (DataTable)this.Session["dtDeductions"];

        if (btnAdd_Deduction.Text == "Add")
        {
            if (CheckDublicatededuction())
            {
                DataRow dr = dtDeductions.NewRow();

                dr["DEDUCTION_DESC"] = DrpDeduction.SelectedItem.Text;
                dr["DEDUCTION_ID"] = DrpDeduction.SelectedItem.Value;
                dr["DEDUCTION_AMOUNT"] = txtDeductionAmount.Text;

                dtDeductions.Rows.Add(dr);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Deduction Already Exists')", true);
            }
        }
     

        else
        {
            DataRow dr = dtDeductions.Rows[DeductionID];

            dr["DEDUCTION_DESC"] = DrpDeduction.SelectedItem.Text;
            dr["DEDUCTION_ID"] = DrpDeduction.SelectedItem.Value;
            dr["DEDUCTION_AMOUNT"] = txtDeductionAmount.Text;
        }

        this.Session.Add("dtDeductions", dtDeductions);

        this.LoadGirdDeductions();

        ClearDeductionDetail();
        ScriptManager.GetCurrent(Page).SetFocus(DrpDeduction);
    }

    #endregion

    #region Grid Operations

    protected void Grid_Allowances_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

            DataTable dtAllowances = (DataTable)this.Session["dtAllowances"];
            AllowanceID = e.NewEditIndex;

            this.DrpAllowance.SelectedValue = Grid_Allowances.Rows[e.NewEditIndex].Cells[0].Text;
            this.txtAllowanceAmount.Text = Grid_Allowances.Rows[e.NewEditIndex].Cells[2].Text;

            btnAdd_Allowance.Text = "Update";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }
    protected void Grid_Deductions_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            DataTable dtDeductions = (DataTable)this.Session["dtDeductions"];
            DeductionID = e.NewEditIndex;

            this.DrpDeduction.SelectedValue = Grid_Deductions.Rows[e.NewEditIndex].Cells[0].Text;
            this.txtDeductionAmount.Text = Grid_Deductions.Rows[e.NewEditIndex].Cells[2].Text;

            btnAdd_Deduction.Text= "Update";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }
    protected void Grid_Allowances_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dtAllowances = (DataTable)this.Session["dtAllowances"];
        dtAllowances.Rows.RemoveAt(e.RowIndex);
        this.Session.Add("dtAllowances", dtAllowances);

        this.LoadGirdAllowances();


    }
    protected void Grid_Deductions_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dtDeductions = (DataTable)this.Session["dtDeductions"];
        dtDeductions.Rows.RemoveAt(e.RowIndex);
        this.Session.Add("dtDeductions", dtDeductions);

        this.LoadGirdDeductions();


    }

    #endregion

    private void ClearAllowanceDetail()
    {
        txtAllowanceAmount.Text = "";
        btnAdd_Allowance.Text = "Add";
        LoadAllowances();
    }
    private void ClearDeductionDetail()
    {
        txtDeductionAmount.Text = "";
        btnAdd_Deduction.Text = "Add";
        LoadDeductions();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        EmployeeSalary_Controller esc=new EmployeeSalary_Controller();
        bool flag = true;
        
        DataTable dtAllowances = (DataTable)this.Session["dtAllowances"];
        DataTable dtDeductions = (DataTable)this.Session["dtDeductions"];
        if (btnSave.Text == "Save")
        {
            flag = esc.Add_Salary(int.Parse(DrpDistributor.SelectedValue), int.Parse(hdnDesignationID.Value), int.Parse(DrpEmployee.SelectedValue)
                , decimal.Parse(txtBasicSalary.Text), DateTime.Parse(Session["CurrentWorkDate"].ToString()), int.Parse(Session["Userid"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), dtAllowances, dtDeductions);

        }
        else if ( btnSave .Text =="Update")
        {

            flag = esc.Update_Salary(int.Parse (hdnSalaryID .Value ),int.Parse(DrpDistributor.SelectedValue), int.Parse(hdnDesignationID.Value), int.Parse(DrpEmployee.SelectedValue)
                , decimal.Parse(txtBasicSalary.Text), DateTime.Parse(Session["CurrentWorkDate"].ToString()), int.Parse(Session["Userid"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), dtAllowances, dtDeductions);

        }

        
        if (flag)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Updated successfully')", true);
            ClearMasterAll();
        }

    }

    private void ClearMasterAll()
    {
        txtBasicSalary.Text = "";
        Grid_Allowances.DataSource = null;
        Grid_Allowances.DataBind();
        Grid_Deductions.DataSource = null;
        Grid_Deductions.DataBind();
        this.Session.Remove("dtAllowances");
        this.Session.Remove("dtDeductions");
        btnSave.Text = "Save";
        this.LoadDistributor();
        this.LoadEmployee();
        LoadAllowances();
        LoadDeductions();
        CreateAllowances();
        CreateDeductions();
        LoadDesignation();
        GetEmployeeSalery();

    }
    private void GetEmployeeSalery()
    {

        txtBasicSalary.Text = "";
        Grid_Allowances.DataSource = null;
        Grid_Allowances.DataBind();
        Grid_Deductions.DataSource = null;
        Grid_Deductions.DataBind();
        Session.Remove("dtAllowances");
        Session.Remove("dtDeductions");
        CreateAllowances();
        CreateDeductions();

        DataTable dt = ccc.SelectEmployeeSalery(int.Parse(DrpDistributor.SelectedValue), int.Parse(DrpEmployee.SelectedValue.ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        Session.Add("dtEmployeeSalary", dt);
        DataRow[] foundRows = dt.Select("EMPLOYEE_ID = '" + DrpEmployee .SelectedValue + "'");

        if (dt.Rows.Count > 0)
        {

            DataTable DtAllowances = ccc.SelectEmployeeAllowances(int.Parse(foundRows[0]["SALARY_ID"].ToString()));
            if (DtAllowances.Rows.Count > 0)
            {
                this.Session.Add("dtAllowances", DtAllowances);
                LoadGirdAllowances();
            }


            DataTable DtDEDUCTION = ccc.SelectEmployeeDeductions(int.Parse(foundRows[0]["SALARY_ID"].ToString()));
            if (DtDEDUCTION.Rows.Count > 0)
            {
                this.Session.Add("dtDeductions", DtDEDUCTION);
                LoadGirdDeductions();
            }
            txtBasicSalary.Text = foundRows[0]["SALARY_AMOUNT"].ToString();
            hdnSalaryID.Value = foundRows[0]["salary_id"].ToString();
            btnSave.Text = "Update";
        }
        else
        {
            txtBasicSalary.Text = "";
            Grid_Allowances.DataSource = null;
            Grid_Allowances.DataBind();
            Grid_Deductions.DataSource = null;
            Grid_Deductions.DataBind();
            this.Session.Remove("dtAllowances");
            this.Session.Remove("dtDeductions");
            CreateAllowances();
            CreateDeductions();

        }

    }
    protected void Grid_Allowances_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
  
   
}