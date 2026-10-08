using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
public partial class pr_frmAttendance : System.Web.UI.Page
{
    CompanyController cc = new CompanyController();
    EmployeController Employee = new EmployeController();
    //EmployeeController ec = new EmployeeController();
    //AttendanceController atd = new AttendanceController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            ResetForm();
            rblTime.SelectedValue = "0";
            btnSave.Attributes.Add("onclick", "return ValidateForm()");                        
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlEmployee.SelectedItem.Value.ToString() != Constants.LongNullValue.ToString())
        {
            SaveAttendance();
        }
    }
    protected void btnDisCard_Click(object sender, EventArgs e)
    {
        ResetForm();
    }
    protected void ddlLocation_Change(object sender, EventArgs e)
    {
        int DepartmentID = Constants.IntNullValue;
        int LocationID = Constants.IntNullValue;        
        if (ddlLocation.SelectedIndex != 0)
        {
            LocationID = Convert.ToInt32(ddlLocation.SelectedItem.Value);
        }
        fillddlEmployee(LocationID, DepartmentID);
        fillrptEmployee(LocationID);

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
    private void ResetForm()
    {
        hfAttendanceID.Value = string.Empty;
        txtDate.Attributes.Add("readonly", "readonly");
        fillddlLocation();
        if (ddlLocation.Items.Count > 0)
        {
            this.fillddlEmployee(Convert.ToInt32(ddlLocation.SelectedItem.Value), Constants.IntNullValue);
            this.fillrptEmployee(Constants.IntNullValue);
        }
    }
    private void fillrptEmployee(int LocationID)
    {
        rptEmployee.DataSource = null;
        rptEmployee.DataBind();
        if (ddlEmployee.Items.Count > 0)
        {
            DataTable dt = Employee.GetAttendance(Constants.LongNullValue, Convert.ToInt64(ddlEmployee.SelectedItem.Value), Constants.IntNullValue, LocationID, 0, Convert.ToDateTime(txtDate.Text));
            rptEmployee.DataSource = dt;
            rptEmployee.DataBind();
        }
    }
    protected void SaveAttendance()
    {
        try
        {
            if ((ddlEmployee.SelectedItem.Value.ToString() != Constants.IntNullValue.ToString()))
            {
                string strTime = tsTime.Hour.ToString() + ":" + tsTime.Minute.ToString() + ":" + tsTime.Second.ToString();
                if (hfAttendanceID.Value != "")
                {
                    Employee.UpdateAttendeance(Convert.ToInt64(hfAttendanceID.Value), Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlEmployee.SelectedItem.Value), null, strTime, 1, Convert.ToInt32(rblTime.SelectedItem.Value), false, Convert.ToInt32(Session["UserID"]), true, false);
                }
                else
                {
                    Employee.InsertAttendeance(Convert.ToInt32(ddlEmployee.SelectedItem.Value), 1, Convert.ToInt32(rblTime.SelectedItem.Value), Convert.ToDateTime(txtDate.Text), strTime, null, false, Convert.ToInt32(Session["UserID"]), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                }
                fillrptEmployee(Constants.IntNullValue);
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
        DataTable dt = UserCtl.SelectDistributorUser(2, LocationID, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDxComboBoxList(ddlEmployee, dt, "USER_ID", "USER_NAME", false);
        
        if (dt.Rows.Count > 0)
        {
            ddlEmployee.SelectedIndex = 0;
        }
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillrptEmployee(Convert.ToInt32(ddlLocation.SelectedItem.Value));
    }
    protected string SetClass(bool IsLate)
    {
        if (IsLate)
        {
            return "RedRow";
        }
        else
        {
            return "WhiteRow";
        }
    }
    protected string GetAction(bool IsLate)
    {
        if (IsLate)
        {
            return "Remove Late";
        }
        else
        {
            return "";
        }
    }
    protected void rptEmployee_ItemCommand(object source, RepeaterCommandEventArgs e)
    {        
        if (e.CommandName == "edit")
        {            
            DataTable dt = Employee.GetAttendance(Convert.ToInt64(e.CommandArgument), Convert.ToInt64(ddlEmployee.SelectedItem.Value), Constants.IntNullValue, Convert.ToInt32(ddlLocation.SelectedItem.Value), 0, Constants.DateNullValue);
            if (dt.Rows.Count > 0)
            {
                rblTime.SelectedValue = dt.Rows[0]["AttendanceType"].ToString();
                txtDate.Text = Convert.ToDateTime(dt.Rows[0]["DayofMonth"]).ToString("dd-MMM-yyyy");
                DateTime dtTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + dt.Rows[0]["TimeOfDay"]);
                tsTime.Date = new DateTime(dtTime.Year, dtTime.Month, dtTime.Day, dtTime.Hour, dtTime.Minute, dtTime.Second);
                hfAttendanceID.Value = dt.Rows[0]["AttendanceID"].ToString();
            }
        }
        else if (e.CommandName == "del")
        {
            if(Employee.UpdateAttendeance(Convert.ToInt64(e.CommandArgument),Constants.DateNullValue,Constants.IntNullValue,null,null,Constants.IntNullValue,Constants.IntNullValue,false,Convert.ToInt32(Session["UserID"]),true,true))
            {
                fillrptEmployee(Convert.ToInt32(ddlLocation.SelectedItem.Value));
            }
        }
    }
    private bool IsFirstTimeIn()
    {
        bool flag = true;
        foreach (RepeaterItem ri in rptEmployee.Items)
        {
            HiddenField hfDayofMonth = (HiddenField)ri.FindControl("hfDayofMonth");
            if (Convert.ToDateTime(hfDayofMonth.Value) == Convert.ToDateTime(txtDate.Text))
            {
                flag = false;
                break;
            }
        }

        return flag;
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        fillrptEmployee(Convert.ToInt32(ddlLocation.SelectedItem.Value));
    }
}