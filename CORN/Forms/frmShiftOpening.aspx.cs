using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;

/// <summary>
/// From to Add, Edit Employee
/// </summary>
public partial class Forms_frmShiftOpening : System.Web.UI.Page
{
    readonly Distributor_UserController UController = new Distributor_UserController();
    readonly ShiftController _SController = new ShiftController();
    private static int _ID;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            LoadDISTRIBUTOR();
            ddDistributorId_SelectedIndexChanged(null, null);
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
            LoadGrid();
        }

    }

    private void LoadDISTRIBUTOR()
    {
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.GetDistributorWithMaxDayClose(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()), 1);
            clsWebFormUtil.FillDxComboBoxList(ddDistributorId, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");

            if (dt.Rows.Count > 0)
            {
                ddDistributorId.SelectedIndex = 0;
            }
            Session.Add("dtLocationInfo", dt);
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void LoadUser()
    {
        ddUser.Items.Clear();
        if (ddDistributorId.Items.Count > 0)
        {
            try
            {
                Distributor_UserController du = new Distributor_UserController();
                DataTable dt = du.SelectDistributorUser(7, int.Parse(ddDistributorId.Value.ToString()), int.Parse(Session["CompanyId"].ToString()));
                clsWebFormUtil.FillDxComboBoxList(ddUser, dt, 0, 1, true);

                Session.Add("User", dt);

                if (dt.Rows.Count > 0)
                {
                    ddUser.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    protected void ddDistributorId_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUser();
        LoadGrid();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime CurrentWorkDate = Constants.DateNullValue;
            DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
            foreach (DataRow row in dtLocationInfo.Rows)
            {
                if (row["DISTRIBUTOR_ID"].ToString() == ddDistributorId.Value.ToString())
                {
                    if (row["MaxDayClose"].ToString().Length > 0)
                    {
                        CurrentWorkDate = Convert.ToDateTime(row["MaxDayClose"]);
                        break;
                    }
                }
            }
            if (CurrentWorkDate != Constants.DateNullValue)
            {
                if (ddUser.Items.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Please select User');", true);
                    return;
                }

                int shiftID = 0;

                DataTable dtUser = (DataTable)Session["User"];
                DataRow[] dr = dtUser.Select("USER_ID = '" + DataControl.chkNull_Zero(ddUser.Value.ToString()) + "'");
                if (dr.Length != 0)
                {
                    shiftID = Convert.ToInt32(DataControl.chkNull_Zero(dr[0][2].ToString()));
                }
                ShiftController mController = new ShiftController();
                DataTable dtClosedShifts = mController.SelectSales(Convert.ToInt32(ddDistributorId.Value.ToString()), Convert.ToInt32(ddUser.Value.ToString()), CurrentWorkDate, Constants.DateNullValue, 7, shiftID);
                if (dtClosedShifts.Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "ErrorMessage", "ErrorMessages()", true);
                    return;
                }
                if (btnSave.Text == "Save")
                {
                    _SController.InsertShiftOpeningAmount(Convert.ToInt32(DataControl.chkNull_Zero(ddUser.SelectedItem.Value.ToString())), shiftID, Convert.ToDecimal(txtAmount.Text), CurrentWorkDate, txtRemarks.Text);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');", true);
                }
                else if (btnSave.Text == "Update")
                {
                    _SController.UpdateShiftOpeningAmount(_ID, Convert.ToInt32(DataControl.chkNull_Zero(ddUser.SelectedItem.Value.ToString())), shiftID, Convert.ToDecimal(txtAmount.Text), Constants.DateNullValue, txtRemarks.Text, 0);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully.');", true);
                }
                LoadGrid();
                Clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Dayclose not found for selected location!')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddDistributorId.SelectedIndex = 0;
        Clear();
    }
    private void Clear()
    {
        LoadUser();
        txtAmount.Text = "";
        txtRemarks.Text = "";
        btnSave.Text = "Save";
        _ID = 0;
    }
    protected void grdChannelData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdChannelData.PageIndex = e.NewPageIndex;
        LoadGrid();
    }
    protected void grdChannelData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            txtAmount.Text = "";
            txtRemarks.Text = "";
            _ID = Convert.ToInt32(grdChannelData.Rows[e.NewEditIndex].Cells[0].Text.ToString());
            txtAmount.Text = grdChannelData.Rows[e.NewEditIndex].Cells[3].Text.ToString();
            txtRemarks.Text = grdChannelData.Rows[e.NewEditIndex].Cells[4].Text.ToString();
            ddUser.Value = grdChannelData.Rows[e.NewEditIndex].Cells[1].Text.ToString();
            btnSave.Text = "Update";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
            throw;
        }
    }
    protected void grdChannelData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        _ID = Convert.ToInt32(grdChannelData.Rows[e.RowIndex].Cells[0].Text.ToString());
        _SController.UpdateShiftOpeningAmount(_ID, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.DateNullValue, null, 1);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record deleted successfully.');", true);
        LoadGrid();
        Clear();
    }
    public void LoadGrid()
    {
        if (ddUser.Items.Count > 0)
        {
            DataTable dt = _SController.SelectSales(Convert.ToInt32(ddDistributorId.Value.ToString()), Convert.ToInt32(ddUser.Value.ToString()), Constants.DateNullValue, Constants.DateNullValue, 10, Constants.IntNullValue);
            grdChannelData.DataSource = dt;
            grdChannelData.DataBind();
        }
    }

    protected void ddUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }
}
