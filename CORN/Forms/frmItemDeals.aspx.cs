using System;
using System.Data;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;
using System.Web.UI.WebControls;

public partial class Forms_frmItemDeals : System.Web.UI.Page
{
    readonly DataControl dc = new DataControl();
    readonly SkuController mSKUController = new SkuController();
    readonly SkuHierarchyController sController = new SkuHierarchyController();

    DataTable _dealSkus;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            Response.AppendHeader("pragma", "no-cache");

            try
            {
                LoadDealItem();
                LoadCategory();
                CreateTable();
                LoadGrid();
                UnAssignItem();
                btnAssignItem.Attributes.Add("onclick", "return ValidateForm();");
                btnAssignAllItem.Attributes.Add("onclick", "return ValidateForm();");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    #region Load

    private void CreateTable()
    {
        _dealSkus = new DataTable();

        _dealSkus.Columns.Add("intDealID", typeof(int));
        _dealSkus.Columns.Add("lngDealDetailID", typeof(long));
        _dealSkus.Columns.Add("ITEM_DEAL_ID", typeof(int));
        _dealSkus.Columns.Add("ITEM_DEAL_NAME", typeof(string));
        _dealSkus.Columns.Add("CATEGORY_DEAL_ID", typeof(int));
        _dealSkus.Columns.Add("CATEGORY_ID", typeof(int));
        _dealSkus.Columns.Add("CATEGORY_NAME", typeof(string));
        _dealSkus.Columns.Add("SKU_ID", typeof(int));
        _dealSkus.Columns.Add("SKU_NAME", typeof(string));
        _dealSkus.Columns.Add("QUANTITY", typeof(decimal));
        _dealSkus.Columns.Add("SKU_QUANTITY", typeof(decimal));
        _dealSkus.Columns.Add("IS_CatChoiceBased", typeof(bool));
        _dealSkus.Columns.Add("IS_ChoiceBased", typeof(bool));
        _dealSkus.Columns.Add("IS_Optional", typeof(bool));
        _dealSkus.Columns.Add("IS_Active", typeof(bool));
        _dealSkus.Columns.Add("Status", typeof(string));
        Session.Add("dealSkus", _dealSkus);
    }

    private void LoadDealItem()
    {
        try
        {
            DataTable dt = mSKUController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue,
                Constants.IntNullValue, 4,
                int.Parse(Session["CompanyId"].ToString()), null);
            drpDealtem.Items.Add(new DevExpress.Web.ListEditItem("-------- Select --------", Constants.IntNullValue.ToString()));

            clsWebFormUtil.FillDxComboBoxList(drpDealtem, dt, "SKU_ID", "SKU_NAME", false);

            drpDealtem.SelectedIndex = 0;

            if (dt.Rows.Count > 0)
            {
                
                hfCategoryId.Value = dt.Rows[0]["CATEGORY_ID"].ToString();
            }
            Session.Add("dt", dt);
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void LoadCategory()
    {
        SkuHierarchyController _mHerController = new SkuHierarchyController();

        try
        {

            DataTable _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, 1, null, null, true, int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue);
            drpCateogry.Items.Add(new DevExpress.Web.ListEditItem("-------- Select --------", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDxComboBoxList(drpCateogry, _mDt, 0, 3, false);

            if (_mDt.Rows.Count > 0)
            {
                drpCateogry.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {
            throw;
        }

    }

    /// <summary>
    /// Load items on List Box category wise
    /// </summary>
    private void UnAssignItem()
    {

        SkuHierarchyController sController = new SkuHierarchyController();

        lstUnAssignItem.Items.Clear();

        try
        {
            if (drpDealtem.Items.Count > 0 && drpCateogry.Items.Count > 0)
            {
                DataTable dt = sController.SelectDealAssignment(int.Parse(drpDealtem.SelectedItem.Value.ToString()), int.Parse(drpCateogry.SelectedItem.Value.ToString()), int.Parse(Session["DISTRIBUTOR_ID"].ToString()), 2);

                clsWebFormUtil.FillListBox(lstUnAssignItem, dt, 0, 1, true);
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    /// <summary>
    /// Get Category base Information
    /// </summary>
    private void LoadMaster()
    {

        SkuHierarchyController sController = new SkuHierarchyController();

        try
        {
            if (drpDealtem.Items.Count > 0 && drpCateogry.Items.Count > 0)
            {
                DataTable _dealSkus = sController.SelectDealAssignment(int.Parse(drpDealtem.SelectedItem.Value.ToString()), int.Parse(drpCateogry.SelectedItem.Value.ToString()), int.Parse(Session["DISTRIBUTOR_ID"].ToString()), 4);

                if (_dealSkus.Rows.Count > 0)
                {
                    txtCategoryQty.Text = _dealSkus.Rows[0]["QUANTITY"].ToString();
                    chkIsChoice.Checked = bool.Parse(_dealSkus.Rows[0]["IS_CatChoiceBased"].ToString());
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void LoadGrid()
    {
        txtCategoryQty.Text = "1";
        chkIsChoice.Checked = false;
        chkIsActive.Checked = true;

        grdDeals.DataSource = null;
        grdDeals.DataBind();


        try
        {
            if (drpDealtem.Items.Count > 0 && drpCateogry.Items.Count > 0)
            {
                DataTable _dealSkus = sController.SelectDealAssignment(int.Parse(drpDealtem.SelectedItem.Value.ToString()), int.Parse(drpCateogry.SelectedItem.Value.ToString()), int.Parse(Session["DISTRIBUTOR_ID"].ToString()), 3);

                Session.Add("dealSkus", _dealSkus);

                grdDeals.DataSource = _dealSkus;
                grdDeals.DataBind();

                if (_dealSkus.Rows.Count > 0)
                {
                    chkIsActive.Checked = bool.Parse(_dealSkus.Rows[0]["Is_Active"].ToString());
                    if (!chkIsActive.Checked)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Deal is InActive');", true);
                    }

                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    #endregion

    #region Sel/Index Change 

    protected void drpDealtem_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dt"];

        DataRow[] foundRows = dt.Select("SKU_ID  = '" + drpDealtem.SelectedItem.Value + "'");
        if (foundRows.Length > 0)
        {
            hfCategoryId.Value = foundRows[0]["CATEGORY_ID"].ToString();
        }
        LoadGrid();
        LoadMaster();
    }
    protected void drpCateogry_SelectedIndexChanged(object sender, EventArgs e)
    {
        UnAssignItem();
        LoadMaster();
    }
    
    #endregion

    protected void grdDeals_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            _dealSkus = (DataTable)Session["dealSkus"];

            if (_dealSkus.Rows.Count > 0)
            {
                DataRow dr = _dealSkus.Rows[e.RowIndex];

                dr["Status"] = "Delete";
                
                Session.Add("dealSkus", _dealSkus);

                drpCateogry.Value = _dealSkus.Rows[e.RowIndex]["CATEGORY_ID"].ToString();
                txtCategoryQty.Text = _dealSkus.Rows[e.RowIndex]["QUANTITY"].ToString();

                chkIsChoice.Checked = bool.Parse(_dealSkus.Rows[e.RowIndex]["IS_CatChoiceBased"].ToString());

                //UnAssignItem();
                
                grdDeals.DataSource = _dealSkus;
                grdDeals.DataBind();
                
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }

    protected void grdDeals_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)

        {
            if (e.Row.Cells[19].Text == "Delete")
                e.Row.Visible = false;

            CheckBox chkIsItemChoice = e.Row.FindControl("chkIsItemChoice") as CheckBox;
            CheckBox chkIsOptional = e.Row.FindControl("chkIsOptional") as CheckBox;
            CheckBox chkIsItemActive = e.Row.FindControl("chkIsItemActive") as CheckBox;

            chkIsItemChoice.Checked = e.Row.Cells[15].Text.ToString() == "True" ? true : false;
            chkIsOptional.Checked = e.Row.Cells[16].Text.ToString() == "True" ? true : false;
            chkIsItemActive.Checked = e.Row.Cells[17].Text.ToString() == "True" ? true : false;
        }
    }

    private bool CheckDublicateSku(int sku_id)
    {
        try
        {
            _dealSkus = (DataTable)Session["dealSkus"];

            DataRow[] foundRows = _dealSkus.Select("ITEM_DEAL_ID  = '" + drpDealtem.SelectedItem.Value + "'and SKU_ID='" + sku_id + "'and Status='" + "New" + "'");

            if (foundRows.Length == 0)
            {
                return true;
            }
            return false;
        }
        catch (Exception)
        {

            throw;
        }
    }


    #region Click Operations

    protected void btnAssignItem_Click(object sender, EventArgs e)
    {
        try
        {
            CreateTable();
            DataRow drNew;

            foreach (GridViewRow item in grdDeals.Rows)
            {
                if (drpDealtem.Items.Count > 0)
                {
                    if (item.Cells[19].Text.ToString() != "Delete")
                    {
                        drNew = _dealSkus.NewRow();
                        CheckBox chkIsItemChoice = item.FindControl("chkIsItemChoice") as CheckBox;
                        CheckBox chkIsOptional = item.FindControl("chkIsOptional") as CheckBox;
                        CheckBox chkIsItemActive = item.FindControl("chkIsItemActive") as CheckBox;

                        TextBox txtQty = item.FindControl("txtQty") as TextBox;

                        drNew["intDEALID"] = int.Parse(dc.chkNull_0(item.Cells[0].Text.Replace("&nbsp;", "")));
                        drNew["lngDealDetailID"] = long.Parse(dc.chkNull_0(item.Cells[1].Text.Replace("&nbsp;", "")));
                        drNew["ITEM_DEAL_ID"] = int.Parse(item.Cells[2].Text.ToString());
                        drNew["ITEM_DEAL_NAME"] = item.Cells[3].Text.ToString();
                        drNew["CATEGORY_DEAL_ID"] = item.Cells[4].Text.ToString();
                        drNew["CATEGORY_ID"] = int.Parse(item.Cells[5].Text.ToString());
                        drNew["CATEGORY_NAME"] = item.Cells[6].Text.ToString();
                        drNew["SKU_ID"] = int.Parse(item.Cells[8].Text.ToString());
                        drNew["SKU_NAME"] = item.Cells[9].Text.ToString();
                        drNew["QUANTITY"] = decimal.Parse(dc.chkNull_0(item.Cells[7].Text.ToString()));
                        drNew["SKU_QUANTITY"] = decimal.Parse(dc.chkNull_0(txtQty.Text));
                        drNew["IS_CatChoiceBased"] = bool.Parse(item.Cells[10].Text.ToString());
                        drNew["IS_ChoiceBased"] = chkIsItemChoice.Checked;
                        drNew["IS_Optional"] = chkIsOptional.Checked;
                        drNew["IS_Active"] = chkIsItemActive.Checked;
                        drNew["Status"] = item.Cells[19].Text.ToString();

                        _dealSkus.Rows.Add(drNew);
                    }
                }
            }

            DataRow dr = _dealSkus.NewRow();

            for (int i = 0; i < lstUnAssignItem.Items.Count; i++)
            {
                if (lstUnAssignItem.Items[i].Selected == true && drpCateogry.Items.Count > 0)
                {
                    if (!CheckDublicateSku(int.Parse(lstUnAssignItem.Items[i].Value)))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + lstUnAssignItem.Items[i].Text + " Already Exists ');", true);

                        return;
                    }
                    else
                    {
                        dr["ITEM_DEAL_ID"] = int.Parse(drpDealtem.SelectedItem.Value.ToString());
                        dr["ITEM_DEAL_NAME"] = drpDealtem.SelectedItem.Text;
                        dr["CATEGORY_DEAL_ID"] = int.Parse(hfCategoryId.Value);
                        dr["CATEGORY_ID"] = int.Parse(drpCateogry.SelectedItem.Value.ToString());
                        dr["CATEGORY_NAME"] = drpCateogry.SelectedItem.Text;
                        dr["SKU_ID"] = int.Parse(lstUnAssignItem.Items[i].Value);
                        dr["SKU_NAME"] = lstUnAssignItem.Items[i].Text;
                        dr["QUANTITY"] = decimal.Parse(dc.chkNull_0(txtCategoryQty.Text));
                        dr["SKU_QUANTITY"] = 1;
                        dr["IS_CatChoiceBased"] = chkIsChoice.Checked;
                        dr["IS_ChoiceBased"] = true;
                        dr["IS_Optional"] = false;
                        dr["IS_Active"] = true;
                        dr["Status"] = "New";

                        _dealSkus.Rows.Add(dr);

                        break;
                    }
                }
            }

            Session.Add("dealSkus", _dealSkus);

            grdDeals.DataSource = _dealSkus;
            grdDeals.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
    protected void btnAssignAllItem_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpCateogry.Items.Count > 0)
            {
                _dealSkus = (DataTable)Session["dealSkus"];
                bool flag = false;

                for (int i = 0; i < lstUnAssignItem.Items.Count; i++)
                {
                    if (CheckDublicateSku(int.Parse(lstUnAssignItem.Items[i].Value)))
                    {

                        flag = true;
                        DataRow dr = _dealSkus.NewRow();


                        dr["ITEM_DEAL_ID"] = int.Parse(drpDealtem.SelectedItem.Value.ToString());
                        dr["ITEM_DEAL_NAME"] = drpDealtem.SelectedItem.Text;
                        dr["CATEGORY_DEAL_ID"] = int.Parse(hfCategoryId.Value);
                        dr["CATEGORY_ID"] = int.Parse(drpCateogry.SelectedItem.Value.ToString());
                        dr["CATEGORY_NAME"] = drpCateogry.SelectedItem.Text;
                        dr["SKU_ID"] = int.Parse(lstUnAssignItem.Items[i].Value);
                        dr["SKU_NAME"] = lstUnAssignItem.Items[i].Text;
                        dr["QUANTITY"] = decimal.Parse(dc.chkNull_0(txtCategoryQty.Text));
                        dr["SKU_QUANTITY"] = 1;
                        dr["IS_CatChoiceBased"] = chkIsChoice.Checked;
                        dr["IS_ChoiceBased"] = true;
                        dr["IS_Optional"] = false;
                        dr["IS_Active"] = true;
                        dr["Status"] = "New";

                        _dealSkus.Rows.Add(dr);
                    }
                }

                // lstUnAssignItem.Items.Clear();
                if (flag)
                {
                    Session.Add("dealSkus", _dealSkus);

                    grdDeals.DataSource = _dealSkus;
                    grdDeals.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + ex.Message.ToString() + "');", true);
        }

    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            sController.UpdateAssignItem(int.Parse(drpDealtem.SelectedItem.Value.ToString()), int.Parse(Session["DISTRIBUTOR_ID"].ToString())
                               , DateTime.Parse(Session["CurrentWorkDate"].ToString()), int.Parse(this.Session["UserId"].ToString()), null);

            LoadGrid();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mPopUpSection.Hide();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable orderSkus = new DataTable();

            orderSkus.Columns.Add("intDEALID", typeof(int));
            orderSkus.Columns.Add("lngDealDetailID", typeof(long));
            orderSkus.Columns.Add("ITEM_DEAL_ID", typeof(int));
            orderSkus.Columns.Add("ITEM_DEAL_NAME", typeof(string));
            orderSkus.Columns.Add("CATEGORY_DEAL_ID", typeof(int));
            orderSkus.Columns.Add("CATEGORY_ID", typeof(int));
            orderSkus.Columns.Add("CATEGORY_NAME", typeof(string));
            orderSkus.Columns.Add("SKU_ID", typeof(int));
            orderSkus.Columns.Add("SKU_NAME", typeof(string));
            orderSkus.Columns.Add("QUANTITY", typeof(decimal));
            orderSkus.Columns.Add("SKU_QUANTITY", typeof(decimal));
            orderSkus.Columns.Add("IS_CatChoiceBased", typeof(bool));
            orderSkus.Columns.Add("IS_ChoiceBased", typeof(bool));
            orderSkus.Columns.Add("IS_Optional", typeof(bool));
            orderSkus.Columns.Add("IS_Active", typeof(bool));
            orderSkus.Columns.Add("Status", typeof(string));

            foreach (GridViewRow item in grdDeals.Rows)
            {

                DataRow dr = orderSkus.NewRow();

                if (drpDealtem.Items.Count > 0)
                {

                    CheckBox chkIsItemChoice = item.FindControl("chkIsItemChoice") as CheckBox;
                    CheckBox chkIsOptional = item.FindControl("chkIsOptional") as CheckBox;
                    CheckBox chkIsItemActive = item.FindControl("chkIsItemActive") as CheckBox;

                    TextBox txtQty = item.FindControl("txtQty") as TextBox;

                    dr["intDEALID"] = int.Parse(dc.chkNull_0(item.Cells[0].Text.Replace("&nbsp;", "")));
                    dr["lngDealDetailID"] = long.Parse(dc.chkNull_0(item.Cells[1].Text.Replace("&nbsp;", "")));
                    dr["ITEM_DEAL_ID"] = int.Parse(item.Cells[2].Text.ToString());
                    dr["ITEM_DEAL_NAME"] = item.Cells[3].Text.ToString();
                    dr["CATEGORY_DEAL_ID"] = item.Cells[4].Text.ToString();
                    dr["CATEGORY_ID"] = int.Parse(item.Cells[5].Text.ToString());
                    dr["CATEGORY_NAME"] = item.Cells[6].Text.ToString();
                    dr["SKU_ID"] = int.Parse(item.Cells[8].Text.ToString());
                    dr["SKU_NAME"] = item.Cells[9].Text.ToString();
                    dr["QUANTITY"] = decimal.Parse(dc.chkNull_0(item.Cells[7].Text.ToString()));
                    dr["SKU_QUANTITY"] = decimal.Parse(dc.chkNull_0(txtQty.Text));
                    dr["IS_CatChoiceBased"] = bool.Parse(item.Cells[10].Text.ToString());
                    dr["IS_ChoiceBased"] = chkIsItemChoice.Checked;
                    dr["IS_Optional"] = chkIsOptional.Checked;
                    dr["IS_Active"] = chkIsItemActive.Checked;
                    dr["Status"] = item.Cells[19].Text.ToString();

                    orderSkus.Rows.Add(dr);
                }
            }

            DataView dv = orderSkus.DefaultView;
            dv.Sort = "CATEGORY_NAME ASC";
            orderSkus = dv.ToTable();

            if (orderSkus.Rows.Count > 0)
            {
                if (!chkIsActive.Checked)
                {
                    string status = sController.SelectDealAssignment(int.Parse(drpDealtem.SelectedItem.Value.ToString()), int.Parse(Session["DISTRIBUTOR_ID"].ToString()), 5);

                    if (status != null)
                    {
                        mPopUpSection.Show();
                        return;
                    }
                }

                  sController.InsertAssignItem(int.Parse(drpDealtem.SelectedItem.Value.ToString()), int.Parse(Session["DISTRIBUTOR_ID"].ToString())
                 , DateTime.Parse(Session["CurrentWorkDate"].ToString()), int.Parse(this.Session["UserId"].ToString()), null
                 , orderSkus);

                LoadGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record saved successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz make a Deal');", true);
            }

          
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
   
    #endregion


}