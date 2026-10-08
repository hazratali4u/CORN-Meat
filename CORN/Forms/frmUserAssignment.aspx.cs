using System;
using System.Data;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;

public partial class Forms_frmUserAssignment : System.Web.UI.Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            LoadVoucherType();
            LoadUser();
            DistributorType();
            LoadUnAssingned();
            LoadAssingned();
            AssignBrand();
            UnAssignBrand();
            AssignVoucherType();

        }
    }

    /// <summary>
    /// Loads Users To User Combo
    /// </summary>
    private void LoadUser()
    {
        UserController mUserController = new UserController();
        DataTable dt = mUserController.SelectSlashUser(null, null);
        clsWebFormUtil.FillDxComboBoxList(ddUser, dt, 0, 4, true);
        if (dt.Rows.Count > 0)
        {
            ddUser.SelectedIndex = 0;
        }
    }

    protected void ddUser_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadUnAssingned();
        LoadAssingned();

        UnAssignBrand();
        AssignBrand();
    }
    #region Location Tab

    /// <summary>
    /// Loads LocationTypes To LocationType Combo
    /// </summary>
    private void DistributorType()
    {
        DistributorController dController = new DistributorController();
        DataTable dt = dController.SelectDistributorTypeInfo(Constants.IntNullValue);
        clsWebFormUtil.FillDxComboBoxList(ddDistributorType, dt, 0, 2);
        if (dt.Rows.Count > 0)
        {
            ddDistributorType.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Loads UnAssigned Locations To UnAssigned ListBox on Location Tab
    /// </summary>
    private void LoadUnAssingned()
    {
        if (ddUser.Items.Count > 0 && ddDistributorType.Items.Count > 0)
        {
            UserController mUserController = new UserController();
            DataTable dt = mUserController.SelectUserAssignment(int.Parse(ddUser.SelectedItem.Value.ToString()), int.Parse(ddDistributorType.SelectedItem.Value.ToString()), 0, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillListBox(lstUnAssignDistributor, dt, 0, 1, true);
        }
    }

    /// <summary>
    /// Loads Assigned Locations To Assigned ListBox on Location Tab
    /// </summary>
    private void LoadAssingned()
    {
        if (ddUser.Items.Count > 0 && ddDistributorType.Items.Count > 0)
        {
            UserController mUserController = new UserController();
            DataTable dt = mUserController.SelectUserAssignment(int.Parse(ddUser.SelectedItem.Value.ToString()), int.Parse(ddDistributorType.SelectedItem.Value.ToString()), 1, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillListBox(lstAssignDistributor, dt, 0, 1, true);
        }
    }

    /// <summary>
    /// Loads Assigned/UnAssigned Locations To Assigned/UnAssigned ListBox on Location Tab
    /// </summary>
    protected void ddDistributorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadUnAssingned();
        this.LoadAssingned();
    }

    /// <summary>
    /// Inserts Location Assignment To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssignLocation_Click(object sender, EventArgs e)
    {
        UserController mUserController = new UserController();
        for (int i = 0; i < lstUnAssignDistributor.Items.Count; i++)
        {
            if (lstUnAssignDistributor.Items[i].Selected == true && ddUser.Items.Count > 0)
            {
                mUserController.InsertUserAssignment(int.Parse(ddUser.SelectedItem.Value.ToString()), int.Parse(ddDistributorType.SelectedItem.Value.ToString()), int.Parse(lstUnAssignDistributor.Items[i].Value.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            }
        }
        this.LoadUnAssingned();
        this.LoadAssingned();
    }

    /// <summary>
    /// Deletes Location Assignment To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUnAssignLocation_Click(object sender, EventArgs e)
    {
        UserController mUserController = new UserController();
        for (int i = 0; i < lstAssignDistributor.Items.Count; i++)
        {
            if (lstAssignDistributor.Items[i].Selected == true && ddUser.Items.Count > 0)
            {
                mUserController.DeleteUserAssignment(int.Parse(ddUser.SelectedItem.Value.ToString()), int.Parse(ddDistributorType.SelectedItem.Value.ToString()), int.Parse(lstAssignDistributor.SelectedItem.Value.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            }
        }
        this.LoadUnAssingned();
        this.LoadAssingned();
    }

    #endregion

    #region Principal Tab

    /// <summary>
    /// Loads UnAssigned Principals To User To UnAssigned Principal ListBox On Principal Combo
    /// </summary>
    private void UnAssignBrand()
    {

        SkuHierarchyController sController = new SkuHierarchyController();
        DataTable dt = sController.SelectBrandAssignment(0, int.Parse(ddUser.SelectedItem.Value.ToString()));
        clsWebFormUtil.FillListBox(lstUnAssignBrand, dt, 0, 1, true);

    }

    /// <summary>
    /// Loads nAssigned Principals To User To nAssigned Principal ListBox On Principal Combo
    /// </summary>
    private void AssignBrand()
    {

        SkuHierarchyController sController = new SkuHierarchyController();
        DataTable dt = sController.SelectBrandAssignment(1, int.Parse(ddUser.SelectedItem.Value.ToString()));
        clsWebFormUtil.FillListBox(lstAssignBrand, dt, 0, 1, true);

        //DataTable dtAssignBrand = sController.GetBrandAssignment(Convert.ToInt32(ddUser.SelectedItem.Value));
        //if (dtAssignBrand.Rows.Count > 0)
        //{
        //    for (int i = 0; i < lstAssignBrand.Items.Count; i++)
        //    {
        //        for (int j = 0; j < dtAssignBrand.Rows.Count; j++)
        //        {
        //            if (lstAssignBrand.Items[i].Value == dtAssignBrand.Rows[j]["PRINCIPAL_ID"].ToString() && dtAssignBrand.Rows[j]["Is_ManualDiscount"].ToString() == "True")
        //            {
        //                lstAssignBrand.Items[i].Selected = true;
        //                break;
        //            }
        //        }
        //    }
        //}
    }

    /// <summary>
    /// Inserts One Principal Assignment To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssignPrincipal_Click(object sender, EventArgs e)
    {
        SkuHierarchyController sController = new SkuHierarchyController();
        for (int i = 0; i < lstUnAssignBrand.Items.Count; i++)
        {
            if (lstUnAssignBrand.Items[i].Selected == true && ddUser.Items.Count > 0)
            {
                sController.InsertAssignBrand(int.Parse(lstUnAssignBrand.SelectedItem.Value.ToString()), int.Parse(ddUser.SelectedItem.Value.ToString()));
            }
        }
        this.UnAssignBrand();
        this.AssignBrand();
    }

    /// <summary>
    /// Inserts All Principal Assignment To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssignAllPrincipal_Click(object sender, EventArgs e)
    {
        SkuHierarchyController sController = new SkuHierarchyController();
        for (int i = 0; i < lstUnAssignBrand.Items.Count; i++)
        {
            if (ddUser.Items.Count > 0)
            {
                sController.InsertAssignBrand(int.Parse(lstUnAssignBrand.Items[i].Value.ToString()), int.Parse(ddUser.SelectedItem.Value.ToString()));

            }
        }
        this.UnAssignBrand();
        this.AssignBrand();
    }

    /// <summary>
    /// Deletes One Principal Assignment To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUnAssignPrincipal_Click(object sender, EventArgs e)
    {
        SkuHierarchyController sController = new SkuHierarchyController();
        for (int i = 0; i < lstAssignBrand.Items.Count; i++)
        {
            if (lstAssignBrand.Items[i].Selected == true && ddUser.Items.Count > 0)
            {
                sController.DeleteAssignBrand(int.Parse(lstAssignBrand.SelectedItem.Value.ToString()), int.Parse(ddUser.SelectedItem.Value.ToString()));

            }
        }
        this.UnAssignBrand();
        this.AssignBrand();
    }

    /// <summary>
    /// Deletes All Principal Assignment To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUnAssignAllPrincipal_Click(object sender, EventArgs e)
    {
        SkuHierarchyController sController = new SkuHierarchyController();
        for (int i = 0; i < lstAssignBrand.Items.Count; i++)
        {
            if (ddUser.Items.Count > 0)
            {
                sController.DeleteAssignBrand(int.Parse(lstAssignBrand.Items[i].Value.ToString()), int.Parse(ddUser.SelectedItem.Value.ToString()));
            }
        }
        UnAssignBrand();
        AssignBrand();
    }

    /// <summary>
    /// Updates Promotion For Principal To Manual Or Auto
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SkuHierarchyController sController = new SkuHierarchyController();
        for (int i = 0; i < lstAssignBrand.Items.Count; i++)
        {
            if (ddUser.Items.Count > 0)
            {
                sController.UpdateBrandAssignment(int.Parse(ddUser.SelectedItem.Value.ToString()), int.Parse(lstAssignBrand.Items[i].Value), lstAssignBrand.Items[i].Selected);
            }
        }
    }

    #endregion    

    #region VoucherType Tab

    /// <summary>
    /// Loads VoucherTypes To VoucherType ListBox on VoucherType Tab
    /// </summary>
    private void LoadVoucherType()
    {
        //LedgerController LLedger = new LedgerController();
        //DataTable dt = LLedger.SelectVoucherType(int.Parse(this.Session["UserId"].ToString()));
        //clsWebFormUtil.FillListBox(this.cblVoucherType, dt, 0, 1);                            
    }

    /// <summary>
    /// Checks/UnChecks VoucherTypes Of VoucherType ListBox VoucherType Tab
    /// </summary>
    private void AssignVoucherType()
    {
        //for (int cnt = 0; cnt < cblVoucherType.Items.Count; cnt++)                      
        //{
        //    cblVoucherType.Items[cnt].Selected = false;
        //}

        //LedgerController LController = new LedgerController();
        //if (ddUser.Items.Count > 0)
        //{
        //    DataTable dtAssignVoucher = LController.GetAssignVoucherType(Convert.ToInt32(ddUser.SelectedItem.Value));
        //    if (dtAssignVoucher.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < cblVoucherType.Items.Count; i++)
        //        {
        //            for (int j = 0; j < dtAssignVoucher.Rows.Count; j++)
        //            {
        //                if (cblVoucherType.Items[i].Value == dtAssignVoucher.Rows[j]["VOUCHER_TYPE_ID"].ToString())
        //                {
        //                    cblVoucherType.Items[i].Selected = true;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}
    }

    /// <summary>
    /// Assigns/UnAssigns VoucherTypes To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        //LedgerController LController = new LedgerController();
        //for (int i = 0; i < cblVoucherType.Items.Count; i++)
        //{
        //    if (ddUser.Items.Count > 0)
        //    {
        //        LController.Assign_UnAssign_VoucherType(int.Parse(cblVoucherType.Items[i].Value), Convert.ToInt32(ddUser.SelectedItem.Value), !cblVoucherType.Items[i].Selected);
        //    }
        //}
    }

    #endregion
}
