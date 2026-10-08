using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Collections.Generic;

public partial class Forms_frmSlipNotes : System.Web.UI.Page
{
  readonly  EmployeController _empCtrl = new EmployeController();
    readonly SlipNoteController _SlipController = new SlipNoteController();

    DataTable dt;
    DataTable dt_Notes;
    DataTable dTable;
    int _noteID;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            CreateTable();
            LoadDistributor();
            LoadSlipNotes();
          
            Session.Add("NoteID", 0);
            foreach (ListItem item in ChbDistributorList.Items)
            {
                item.Selected = true;
            }
        }
    }

    #region Slip Notes Tab

    public void LoadSlipNotes()
    {
        dt = _SlipController.GetSlipNotes(1, Constants.IntNullValue);
        grdChannelData.DataSource = dt;
        grdChannelData.DataBind();
        GetLocations(0);
    }
    public void CreateTable()
    {
        dt_Notes = new DataTable();
        dt_Notes.Columns.Add("SLIP_NOTE", typeof(string));
        dt_Notes.Columns.Add("IS_ACTIVE", typeof(bool));
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(-1, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(ChbDistributorList, dt, 0, 2, true);
    }

    public void GetLocations(int _rowIndex)
    {
        int _rows = ChbDistributorList.Items.Count;
        foreach (ListItem item in ChbDistributorList.Items)
        {
            item.Selected = false;
        }
        if (grdChannelData.Rows.Count > 0)
        {
            int value;
            _noteID = Convert.ToInt32(grdChannelData.Rows[_rowIndex].Cells[0].Text.ToString());
            DataTable dt = _SlipController.GetSlipNotes(2, _noteID);
            List<int> ids = new List<int>(dt.Rows.Count);
            foreach (DataRow row in dt.Rows)
            {
                ids.Add((int)row["DISTRIBUTOR_ID"]);
            }
            foreach (ListItem item in ChbDistributorList.Items)
            {
                value = Convert.ToInt32(item.Value);
                if (ids.Contains(value))
                {
                    item.Selected = true;
                }
            }
           
        }
    }

    protected void grdChannelData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ChbSelectAll.Checked = false;
        _noteID = Convert.ToInt32(grdChannelData.Rows[e.NewEditIndex].Cells[0].Text.ToString());
        Session["NoteID"] = _noteID;
        GetLocations(e.NewEditIndex);
        txtChannelName.Text = grdChannelData.Rows[e.NewEditIndex].Cells[1].Text.ToString();
        btnSaveChannelType.Text = "Update";
    }

    protected void grdChannelData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        _noteID = Convert.ToInt32(grdChannelData.Rows[e.RowIndex].Cells[0].Text.ToString());
        string result = _SlipController.UpdateSlipNote_Master(_noteID, "", false, 1);
        if (result != null)
        {
            LoadSlipNotes();
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Removed successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some Error Occurred.');", true);
        }
    }

    protected void btnSaveChannelType_Click(object sender, EventArgs e)
    {
        lblErrorMsg.Visible = false;
        lblErrorMsg.Text = "";
        int _items = ChbDistributorList.Items.Count;
        int count = 0;
        foreach (ListItem item in ChbDistributorList.Items)
        {
            if (item.Selected)
            {
                count++;
            }
        }
        if (count == 0)
        {
            lblErrorMsg.Visible = true;
            lblErrorMsg.Text = Utility.ShowAlert(false, "Select Location/s");
            return;
        }
        if (txtChannelName.Text.Length == 0)
        {
            lblErrorMsg.Visible = true;
            lblErrorMsg.Text = Utility.ShowAlert(false, "Note is required");
            return;
        }
        if (btnSaveChannelType.Text == "Save")
        {

            int _noteID = _SlipController.InsertSlipNote_Master(txtChannelName.Text, true);
            if (_noteID != 0)
            {
                foreach (ListItem item in ChbDistributorList.Items)
                {
                    if (item.Selected)
                    {
                        int _Distributor_id = Convert.ToInt32(item.Value);
                        _SlipController.InsertSlipNote_Detail(_noteID, _Distributor_id);
                    }
                }
            }
            LoadSlipNotes();
            foreach (ListItem item in ChbDistributorList.Items)
            {
                item.Selected = true;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Added successfully.');", true);
            txtChannelName.Text = "";
        }
        else if (btnSaveChannelType.Text == "Update")
        {
            string result = _SlipController.UpdateSlipNote_Master(Convert.ToInt32(Session["NoteID"]), txtChannelName.Text, true, 2);
            if (result != null)
            {
                foreach (ListItem item in ChbDistributorList.Items)
                {
                    if (item.Selected)
                    {
                        int _Distributor_id = Convert.ToInt32(item.Value);
                        _SlipController.InsertSlipNote_Detail(Convert.ToInt32(Session["NoteID"]), _Distributor_id);
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Updated successfully.');", true);
                LoadSlipNotes();
                btnSaveChannelType.Text = "Save";
                txtChannelName.Text = "";
                foreach (ListItem item in ChbDistributorList.Items)
                {
                    item.Selected = true;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some Error Occurred.');", true);
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ChbSelectAll.Checked = false;
        GetLocations(0);
        txtChannelName.Text = "";
        btnSaveChannelType.Text = "Save";
        lblErrorMsg.Text = "";
        lblErrorMsg.Visible = false;
    }

    #endregion

    private string GetAutoCode(string PreeFix, int CodeType, long CValue)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        return AutoCode.GetAutoCode(PreeFix, CodeType, CValue);
    }
    protected void grdChannelData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdChannelData.PageIndex = e.NewPageIndex;
        LoadSlipNotes();
    }


}
