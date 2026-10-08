using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;


public partial class Forms_SkuModifier : System.Web.UI.Page
{
    readonly SkuController SKUCtl = new SkuController();
    DataRow dr;
    DataTable dt = new DataTable();
    DataTable dtsku = new DataTable();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            try
            {
            LoadHasModifier();
            LoadIsModifier();
            ddlIsModifier_SelectedIndexChanged(null, null);
            LoadGrid();           
        }
            catch (Exception)
            {
        
                throw;
    }
        }
        
    }
    protected void CreateTable()
    {
        try
        { 
        dt.Columns.Add(new DataColumn("ModifierSKU_ID", typeof(string)));
        dt.Columns.Add("IsModifierID");
        dt.Columns.Add(new DataColumn("intStockMUnitCode", typeof(string)));
        dt.Columns.Add("StockUnitID");
        dt.Columns.Add("Default_Qty");
        dt.Columns.Add("IS_Manadatory");
        Session.Add("GrdTable", dt);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }

    }
    protected void btnAddGrid_Click(object sender, EventArgs e)
    {
        try
        { 
        ddlHasModifier_SelectedIndexChanged(null, null);

        if (ChkExistValue() == true)
        {
            if (ChkGridRepeat())
            {
                if(txtDefaultQty.Text=="")
                {
                    txtDefaultQty.Text = "1";
                }
                dt = (DataTable)Session["GrdTable"];
                dr = dt.NewRow();
                dr["ModifierSKU_ID"] = ddlIsModifier.SelectedItem;
                dr["IsModifierID"] = ddlIsModifier.SelectedItem.Value;
                dr["intStockMUnitCode"] = ddlStockUnit.SelectedItem.Text;
                dr["StockUnitID"] = ddlStockUnit.SelectedItem.Value;
                dr["Default_Qty"] = txtDefaultQty.Text;
                if (cbIsManadatory.Checked)
                {
                    dr["ddlIsModifier"] = true;
                }
                else
                {
                    dr["IS_Manadatory"] = false;
                }
                dt.Rows.Add(dr);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                mPopUpLocation.Show();

                Session.Add("GrdDataTable", dt);

                ClearForGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('This SKU Modifier already exist');", true);
                mPopUpLocation.Show();
                //ddlHasModifier.Enabled = true;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('This SKU Modifier already exist');", true);
            mPopUpLocation.Show();
            ddlHasModifier.Enabled = true;
        }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
            mPopUpLocation.Show();
        }
    }
    protected bool ChkGridRepeat()
    {
            DataTable dtgrd = new DataTable();
            dtgrd = (DataTable)Session["GrdTable"];
            if (dtgrd != null)
            {
                if (dtgrd.Rows.Count > 0)
                {
                    for (int i = 0; i < dtgrd.Rows.Count; i++)
                    {
                        if (Convert.ToString(dtgrd.Rows[i]["IsModifierID"]) == ddlIsModifier.SelectedItem.Value.ToString())
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
    }
    private void ClearForGrid()
    {
        try
        { 
        ddlHasModifier.Enabled = false;
        ddlIsModifier.SelectedIndex = 0;
        ddlStockUnit.SelectedIndex = 0;
        txtDefaultQty.Text = "";
        ddlIsModifier_SelectedIndexChanged(null, null);
        cbIsManadatory.Checked = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
            mPopUpLocation.Show();
        }
    }
    private void LoadGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SKUCtl.SelectModifier(0);
            if (txtSearch.Text != "" || txtSearch.Text != string.Empty) 
            {
                dt.DefaultView.RowFilter = "Has_Modifier LIKE '%" + txtSearch.Text + "%'  OR Is_Modifier LIKE '%" + txtSearch.Text + "%' OR Stock_Unit LIKE '%" + txtSearch.Text + "%'   OR Default_Qty LIKE '%" + txtSearch.Text + "%' ";
                GrdModifier.DataSource = dt;
                GrdModifier.DataBind();
            }
            else
            {
                GrdModifier.DataSource = dt;
                GrdModifier.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }

    }
    private void LoadHasModifier()
    {
        try
        {
        DataTable Dtsku_HasModifier = SKUCtl.SelectSkuHasModifier(0);
        clsWebFormUtil.FillDxComboBoxList(this.ddlHasModifier, Dtsku_HasModifier, 0, 2, true);

            if (Dtsku_HasModifier.Rows.Count > 0)
            {
                ddlHasModifier.SelectedIndex = 0;
            }

        }
        catch (Exception)
        {

            throw;
        }     
    }
    private void LoadIsModifier()
    {

        try
        {
        DataTable Dtsku_HasModifier = SKUCtl.SelectSkuHasModifier(1);
        clsWebFormUtil.FillDxComboBoxList(this.ddlIsModifier, Dtsku_HasModifier, 0, 2, true);
            if (Dtsku_HasModifier.Rows.Count > 0)
            {
                ddlIsModifier.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlIsModifier_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlIsModifier.Items.Count > 0)
            {
                if (IsPostBack)
                {
                    mPopUpLocation.Show();
                }
                DataTable dt = SKUCtl.SelectSkuInfo(int.Parse(ddlIsModifier.SelectedItem.Value.ToString()), Constants.IntNullValue, Constants.IntNullValue, 8, int.Parse(Session["CompanyId"].ToString()), null);
                clsWebFormUtil.FillDxComboBoxList(ddlStockUnit, dt, 1, 0, true);

                if (dt.Rows.Count > 0)
                {

                    ddlStockUnit.SelectedIndex = 0;
                }
                GetItemUnitAndQty();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Plz Add IsModifier Items.');", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
            mPopUpLocation.Show();
        }
    }
    public void GetItemUnitAndQty()
    {
        try
        {
            DataTable dt = new DataTable();

            dt = SKUCtl.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, DataControl.chkIntNull(ddlIsModifier.SelectedItem.Value.ToString()), 10, int.Parse(Session["CompanyId"].ToString()), null);
            if (dt.Rows.Count > 0)
            {
                txtDefaultQty.Text = dt.Rows[0]["QUANTITY"].ToString();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid();

    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
       
    }
    

    protected void cbIsManadatory_CheckedChanged(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

             dt = (DataTable)Session["GrdDataTable"];
            if (dt!=null || btnSave.Text== "Update")
            {
                if (btnSave.Text == "Save")
                {
                    SKUCtl.InsertModifier(Convert.ToInt32(ddlHasModifier.SelectedItem.Value), dt);

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');", true);

                }
                else if (btnSave.Text == "Update")
                {
                    SKUCtl.UpdateModifier(int.Parse(hfModifierCode.Value), int.Parse(ddlHasModifier.SelectedItem.Value.ToString()), int.Parse(ddlIsModifier.SelectedItem.Value.ToString()), int.Parse(ddlStockUnit.SelectedItem.Value.ToString()), Convert.ToDecimal(txtDefaultQty.Text), cbIsManadatory.Checked);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully.');", true);
                    mPopUpLocation.Hide();
                    btnAddGrid.Text = "Add Grid";
                    btnSave.Text = "Save";
                }
                LoadGrid();
                ClearAll();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('At least one sku modifier item should be added.');", true);
                mPopUpLocation.Show();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
            mPopUpLocation.Show();
        }
    }

    protected bool ChkExistValue()
    {
        bool chkValue = true;
        if (dtsku != null)
        {
            if (dtsku.Rows.Count > 0)
            {
                for (int i = 0; i < dtsku.Rows.Count; i++)
                {
                    if (Convert.ToString(dtsku.Rows[i]["ModifierSKU_ID"]) == ddlIsModifier.SelectedItem.Value.ToString())
                    {
                        chkValue = false;
                        return chkValue;
                    }
                }
            }
        }       
        return chkValue;

    }

    protected void GrdModifier_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GrdModifier.PageIndex = e.NewPageIndex;
        LoadGrid();
    }

    protected void GrdModifier_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
        hfModifierCode.Value = GrdModifier.Rows[e.NewEditIndex].Cells[1].Text;
        ddlHasModifier.Value = GrdModifier.Rows[e.NewEditIndex].Cells[2].Text;
        ddlIsModifier.Value = GrdModifier.Rows[e.NewEditIndex].Cells[4].Text;
        ddlStockUnit.Value = GrdModifier.Rows[e.NewEditIndex].Cells[6].Text;
        txtDefaultQty.Text = GrdModifier.Rows[e.NewEditIndex].Cells[8].Text;
        cbIsManadatory.Checked = Convert.ToBoolean(GrdModifier.Rows[e.NewEditIndex].Cells[9].Text);

        btnSave.Text = "Update";
        ddlHasModifier.Enabled = false;
        ddlIsModifier.Enabled = false;
        ddlStockUnit.Enabled = false;
        myModalLabel.InnerText = "Edit Sku Modifier";
        mPopUpLocation.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
            mPopUpLocation.Show();
            ddlIsModifier_SelectedIndexChanged(null, null);
            //dt = null;
            //Session.Add("GrdTable", dt);
            CreateTable();
            // ddlIsModifier_SelectedIndexChanged(null, null);
            dt = null;
            Session.Add("GrdDataTable", dt);
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
            mPopUpLocation.Show();
        }
    }
    private void ClearAll()
    {
        try
        { 
        ddlHasModifier.Enabled = true;
        ddlIsModifier.Enabled = true;
        ddlStockUnit.Enabled = true;
        ddlHasModifier.SelectedIndex = 0;
        ddlIsModifier.SelectedIndex = 0;
        ddlStockUnit.SelectedIndex = 0;
        txtDefaultQty.Text = "";
        GridView1.DataSource = null;
        GridView1.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
            mPopUpLocation.Show();
        }
    }

    protected void ddlHasModifier_SelectedIndexChanged(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
        dtsku = SKUCtl.SelectModifier(int.Parse(ddlHasModifier.SelectedItem.Value.ToString()));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            CreateTable();
            ClearAll();
            btnAddGrid.Text = "Add Grid";
            btnSave.Text = "Save";
            myModalLabel.InnerText = "Sku Modifier";
            dt = null;
            Session.Add("GrdDataTable", dt);
            GetItemUnitAndQty();
            mPopUpLocation.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }

    protected void ddlHasModifier_SelectedIndexChanged1(object sender, EventArgs e)
    {
        mPopUpLocation.Show();
    }

}