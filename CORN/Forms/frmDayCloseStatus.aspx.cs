using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;

/// <summary>
/// Form For Day Close
/// </summary>
public partial class Forms_frmDayCloseStatus : System.Web.UI.Page
{
    DistributorController mController = new DistributorController();
    DataControl DC = new DataControl();
    DataSet ds = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            LastClosedDay(int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["Distributor_Id"].ToString()));
            if (Grid_Hierarchy.Rows.Count > 1)
            {
                Grid_Hierarchy.UseAccessibleHeader = true;
                Grid_Hierarchy.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            bool chk = bool.Parse(Session["IS_CanReverseDayClose"].ToString());
            if (chk)
            {
                btnReverseDayClose.Visible = true;
            }
        }
    }

    
    private DataTable SelectPendingBills()
    {

        OrderEntryController mSkuController = new OrderEntryController();
        DataTable dtSkus = mSkuController.SelectPendingBills(Constants.LongNullValue,
            DateTime.Parse(Session["CurrentWorkDate"].ToString()), int.Parse(Session["DISTRIBUTOR_ID"].ToString()), int.Parse(HttpContext.Current.Session["UserID"].ToString()), Constants.IntNullValue,2);

        return dtSkus;
    }
    /// <summary>
    /// Gets Location(s) Last Day Close(s)
    /// </summary>
    /// <param name="UserId">User</param>
    /// <param name="p_Distributor">Location</param>
    private void LastClosedDay(int UserId, int p_Distributor)
    {
        btnDayClose.Visible = true;
        if (System.Configuration.ConfigurationManager.AppSettings["IsSync"] != null)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["IsSync"].ToString() == "1")
            {
                btnSync.Visible = true;
            }
        }
        
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(UserId, p_Distributor);
        if (dt.Rows.Count > 0)
        {
            this.Session.Add("CurrentWorkDate", DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString()));
        }
        else
        {
            this.Session.Add("CurrentWorkDate", DateTime.Now);
            rblDistributorTypes.Visible = true;
        }
        GetLastClosedDay(UserId, p_Distributor, 4);
    }

    /// <summary>
    /// Loads Location(s) Last Day Close(s) To Grid
    /// </summary>
    /// <param name="UserId">User</param>
    /// <param name="p_Distributor">Location</param>
    /// <param name="p_Status">Status</param>
    private void GetLastClosedDay(int UserId, int p_Distributor, int p_Status)
    {
        DataTable dtable = mController.MaxDayClose(int.Parse(this.Session["UserId"].ToString()), p_Status);
        Grid_Hierarchy.DataSource = dtable;
        Grid_Hierarchy.DataBind();
    }

    /// <summary>
    /// Loads Locations(Active/InActive/All) Last Day Close(s) To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void rblDistributorTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLastClosedDay(int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["Distributor_Id"].ToString()), Convert.ToInt32(rblDistributorTypes.SelectedValue));
        if (Grid_Hierarchy.Rows.Count > 1)
        {
            Grid_Hierarchy.UseAccessibleHeader = true;
            Grid_Hierarchy.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    /// <summary>
    /// Performs Following Tasks And LogOuts
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// Closes Day Transactions
    /// </item>
    /// <item>
    /// Inserts LogOut Time
    /// </item>
    /// </list>
    /// </remarks>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnDayClose_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnDayClose.Visible == true)
            {
                DataTable dtOrders = SelectPendingBills();

                if (dtOrders.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Plz Close your Orders first');", true);

                }
                else
                {
                    DistributorController mDayClose = new DistributorController();

                    bool dt = mDayClose.UspDayClose(DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), int.Parse(this.Session["DISTRIBUTOR_ID"].ToString()), int.Parse(this.Session["UserID"].ToString()));

                    if (dt == false)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Some error in day Close Contact System Administrator');", true);
                    }

                    else
                    {
                        UserController mController = new UserController();
                        if (mController.InsertUserLogoutTime(Convert.ToInt64(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"])) == "Logout Time Inserted")
                        {
                            this.Session.Clear();
                            Response.Redirect("~/Login.aspx");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Some error in day Close Contact System Administrator');", true);
                        }
                    }
                }
            }
        }
        catch (Exception)
        {

            throw;
        }

    }
    protected void btnReverseDayClose_Click(object sender, EventArgs e)
    {
        try
        {

            if (btnReverseDayClose.Visible == true)
            {
                DistributorController mDayClose = new DistributorController();

                bool dt = mDayClose.UspDayReverse(DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), int.Parse(this.Session["DISTRIBUTOR_ID"].ToString()), int.Parse(this.Session["UserID"].ToString()));

                if (dt == false)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Some error in Reverse Day Close Contact System Administrator');", true);
                }

                else
                {

                    Session.Clear();
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        catch (Exception)
        {

            throw;
        }

    }
    protected void btnSync_Click(object sender, EventArgs e)
    {
        
        DataTable dtServerData = new DataTable();
        clsProcessServerData ProcessData = new clsProcessServerData();

        try
        {

            //#region Distributor
            //try
            //{
            //    dtServerData = ProcessData.GetDataFromServer(1, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
            //    if (dtServerData.Rows.Count > 0)
            //    {
            //        ProcessData.UpdateDistributor(dtServerData);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionPublisher.PublishExceptionforSync(ex, "Error in Distributor sync.");
            //}
            //finally
            //{
            //    dtServerData = null;
            //}
            //#endregion

            //#region Distributor Assignment
            //try
            //{
            //    dtServerData = ProcessData.GetDataFromServer(2, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
            //    if (dtServerData.Rows.Count > 0)
            //    {
            //        ProcessData.InsertDistributorAssignment(Convert.ToInt32(Session["DISTRIBUTOR_ID"]), dtServerData);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionPublisher.PublishExceptionforSync(ex, "Error in Distributor Assignment sync.");
            //}
            //finally
            //{
            //    dtServerData = null;
            //}
            //#endregion

            //#region Distributor User
            //try
            //{
            //    dtServerData = ProcessData.GetDataFromServer(3, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
            //    if (dtServerData.Rows.Count > 0)
            //    {
            //        ProcessData.InsertDistributorUser(dtServerData);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionPublisher.PublishExceptionforSync(ex, "Error in User sync.");
            //}
            //finally
            //{
            //    dtServerData = null;
            //}
            //#endregion

            //#region User
            //try
            //{
            //    dtServerData = ProcessData.GetDataFromServer(4, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
            //    if (dtServerData.Rows.Count > 0)
            //    {
            //        ProcessData.InsertUser(dtServerData);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionPublisher.PublishExceptionforSync(ex, "Error in User sync.");
            //}
            //finally
            //{
            //    dtServerData = null;
            //}
            //#endregion

            //#region Module
            //try
            //{
            //    dtServerData = ProcessData.GetDataFromServer(5, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
            //    if (dtServerData.Rows.Count > 0)
            //    {
            //        ProcessData.InsertModule(dtServerData);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionPublisher.PublishExceptionforSync(ex, "Error in Module sync.");
            //}
            //finally
            //{
            //    dtServerData = null;
            //}
            //#endregion

            //#region SKUS
            //try
            //{
            //    dtServerData = ProcessData.GetDataFromServer(6, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
            //    if (dtServerData.Rows.Count > 0)
            //    {
            //        ProcessData.InsertSKUS(dtServerData);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionPublisher.PublishExceptionforSync(ex, "Error in SKUS sync.");
            //}
            //finally
            //{
            //    dtServerData = null;
            //}
            //#endregion

            //#region SKU Heirarchy
            //try
            //{
            //    dtServerData = ProcessData.GetDataFromServer(7, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
            //    if (dtServerData.Rows.Count > 0)
            //    {
            //        ProcessData.InsertSKUHeirarchy(dtServerData);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionPublisher.PublishExceptionforSync(ex, "Error in SKU Heirarchy sync.");
            //}
            //finally
            //{
            //    dtServerData = null;
            //}
            //#endregion

            //#region SKU Prices
            //try
            //{
            //    dtServerData = ProcessData.GetDataFromServer(8, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
            //    if (dtServerData.Rows.Count > 0)
            //    {
            //        ProcessData.InsertSKUPrice(dtServerData);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionPublisher.PublishExceptionforSync(ex, "Error in SKU Prices sync.");
            //}
            //finally
            //{
            //    dtServerData = null;
            //}
            //#endregion

            //#region Role Master
            //try
            //{
            //    dtServerData = ProcessData.GetDataFromServer(9, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
            //    if (dtServerData.Rows.Count > 0)
            //    {
            //        ProcessData.InsertRoleMaster(dtServerData);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionPublisher.PublishExceptionforSync(ex, "Error in SKU Prices sync.");
            //}
            //finally
            //{
            //    dtServerData = null;
            //}
            //#endregion

            //#region Role Detail
            //try
            //{
            //    dtServerData = ProcessData.GetDataFromServer(10, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
            //    if (dtServerData.Rows.Count > 0)
            //    {
            //        ProcessData.InsertRoleDetail(dtServerData);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionPublisher.PublishExceptionforSync(ex, "Error in Role Detail sync.");
            //}
            //finally
            //{
            //    dtServerData = null;
            //}
            //#endregion

            //#region Tables
            //try
            //{
            //    dtServerData = ProcessData.GetDataFromServer(11, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
            //    if (dtServerData.Rows.Count > 0)
            //    {
            //        ProcessData.InsertTable(dtServerData);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionPublisher.PublishExceptionforSync(ex, "Error in Role Detail sync.");
            //}
            //finally
            //{
            //    dtServerData = null;
            //}
            //#endregion            

            //#region Finished Goods Master
            //try
            //{
            //    dtServerData = ProcessData.GetDataFromServer(12, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
            //    if (dtServerData.Rows.Count > 0)
            //    {
            //        ProcessData.InsertFinishedGoodMaster(dtServerData);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionPublisher.PublishExceptionforSync(ex, "Error in Finished Good Master sync.");
            //}
            //finally
            //{
            //    dtServerData = null;
            //}
            //#endregion            

            //#region Finished Goods Detail
            //try
            //{
            //    dtServerData = ProcessData.GetDataFromServer(13, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
            //    if (dtServerData.Rows.Count > 0)
            //    {
            //        ProcessData.InsertFinishedGoodDetail(dtServerData);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionPublisher.PublishExceptionforSync(ex, "Error in Finished Good Detail sync.");
            //}
            //finally
            //{
            //    dtServerData = null;
            //}
            //#endregion            

            //#region Product Section
            //try
            //{
            //    dtServerData = ProcessData.GetDataFromServer(14, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
            //    if (dtServerData.Rows.Count > 0)
            //    {
            //        ProcessData.InsertProductSection(dtServerData);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionPublisher.PublishExceptionforSync(ex, "Error in Role Detail sync.");
            //}
            //finally
            //{
            //    dtServerData = null;
            //}
            //#endregion            

        }
        catch (Exception ex)
        {

        }
    }
    
}

public class clsProcessServerData
{
    //SyncServerController mController = new SyncServerController();
    //public static CafeSyncService.SAMSSyncServiceSoap syncService = new CafeSyncService.SAMSSyncServiceSoapClient();

    //DataControl dc = new DataControl();

    //#region Get Data from Server

    //public DataTable GetDataFromServer(int intType, int intDistributorID)
    //{
    //    DataTable dtServerData = new DataTable();
    //    try
    //    {
    //        object p_LastUpdateDateTime = mController.GetClientLastUpdateDateTime(intType);
    //        if (p_LastUpdateDateTime == DBNull.Value)
    //        {
    //            p_LastUpdateDateTime = CORNCommon.Classes.Constants.DateNullValue;
    //        }
    //        dtServerData = syncService.GetServerData(Convert.ToDateTime(p_LastUpdateDateTime), intType, intDistributorID);
    //    }
    //    catch (Exception a)
    //    {
    //        //MessageBox.Show("Error While Sync Distributor :\n" + a.Message);
    //    }
    //    return dtServerData;
    //}

    //#endregion

    //#region Distributor

    //public void UpdateDistributor(DataTable dtServerData)
    //{
    //    try
    //    {
    //        int RegionID = Constants.IntNullValue;
    //        int ZoneID = Constants.IntNullValue;
    //        int DivisionID = Constants.IntNullValue;

    //        foreach (DataRow dr in dtServerData.Rows)
    //        {
    //            if (dr["REGION_ID"].ToString() != "")
    //            {
    //                RegionID = Convert.ToInt32(dr["REGION_ID"]);
    //            }
    //            if (dr["ZONE_ID"].ToString() != "")
    //            {
    //                ZoneID = Convert.ToInt32(dr["ZONE_ID"]);
    //            }
    //            if (dr["DIVISION_ID"].ToString() != "")
    //            {
    //                DivisionID = Convert.ToInt32(dr["DIVISION_ID"]);
    //            }
    //            mController.UpdateDistributorClient(Convert.ToInt32(dr["COMPANY_ID"]), Convert.ToInt32(dr["DIST_CLASS_ID"]), Convert.ToBoolean(dr["ISDELETED"]), DivisionID, Convert.ToInt32(dr["USER_ID"])
    //                , RegionID, ZoneID, Convert.ToInt32(dr["SUBZONE_ID"]), dr["CONTACT_PERSON"].ToString(), dr["CONTACT_NUMBER"].ToString(), dr["GST_NUMBER"].ToString()
    //                , dr["PASSWORD"].ToString(), dr["ADDRESS1"].ToString(), dr["ADDRESS2"].ToString(), Convert.ToInt32(dr["DISTRIBUTOR_ID"]), dr["DISTRIBUTOR_CODE"].ToString(), dr["DISTRIBUTOR_NAME"].ToString()
    //                , dr["IP_ADDRESS"].ToString(), Convert.ToBoolean(dr["IS_REGISTERED"]), Convert.ToInt32(dr["CREDIT_LEVEL"]), Convert.ToDecimal(dr["GST"]), Convert.ToBoolean(dr["SERVICE_CHARGES"]));
    //        }
    //    }
    //    catch (Exception a)
    //    {
    //        //MessageBox.Show("Error While Insert Synced Data of Distributor:\n" + a.Message);
    //    }
    //}

    //public void InsertDistributorAssignment(int p_DISTRIBUTOR_ID, DataTable dtServerData)
    //{
    //    try
    //    {
    //        mController.InsertUserAssignmentClient(p_DISTRIBUTOR_ID, dtServerData);
    //    }
    //    catch (Exception a)
    //    {
    //        //MessageBox.Show("Error While Insert Synced Data of Distributor Assignment:\n" + a.Message);
    //    }
    //}

    //#endregion

    //#region Distributor User

    //public void InsertDistributorUser(DataTable dtServerData)
    //{
    //    try
    //    {
    //        int RoleID = Constants.IntNullValue;
    //        int DeptID = Constants.IntNullValue;
    //        DateTime LastupdateDate = Constants.DateNullValue;

    //        foreach (DataRow dr in dtServerData.Rows)
    //        {
    //            if (dr["ROLE_ID"].ToString() != "")
    //            {
    //                RoleID = Convert.ToInt32(dr["ROLE_ID"]);
    //            }
    //            if (dr["DEPT_ID"].ToString() != "")
    //            {
    //                DeptID = Convert.ToInt32(dr["DEPT_ID"]);
    //            }
    //            if (dr["LASTUPDATE_DATE"].ToString() != "")
    //            {
    //                LastupdateDate = Convert.ToDateTime(dr["LASTUPDATE_DATE"]);
    //            }
    //            mController.InsertDistributor_UserClient(Convert.ToInt32(dr["USER_ID"]), Convert.ToInt32(dr["COMPANY_ID"]), dr["NIC_NO"].ToString(), Convert.ToBoolean(dr["IS_ACTIVE"]), LastupdateDate,
    //               Convert.ToInt32(dc.chkNull_0(dr["USER_TYPE_ID"].ToString())), Convert.ToInt32(dr["DISTRIBUTOR_ID"]), RoleID, dr["EMAIL"].ToString(), dr["ADDRESS1"].ToString(), dr["ADDRESS2"].ToString(), dr["LOGIN_ID"].ToString(), dr["PASSWORD"].ToString(), dr["MOBILE"].ToString(), dr["USER_CODE"].ToString(), dr["USER_NAME"].ToString(), dr["PHONE"].ToString(), DeptID);
    //        }
    //    }
    //    catch (Exception a)
    //    {
    //        //MessageBox.Show("Error While Insert Synced Data of SKU Hierarchy:\n" + a.Message);
    //    }
    //}

    //public void InsertUser(DataTable dtServerData)
    //{
    //    try
    //    {
    //        foreach (DataRow dr in dtServerData.Rows)
    //        {
    //            mController.InserUserClient(Convert.ToInt32(dr["USER_ID"]), Convert.ToInt32(dr["COMPANY_ID"]), Convert.ToInt32(dr["DISTRIBUTOR_ID"]), dr["LOGIN_ID"].ToString(), dr["PASSWORD"].ToString(),
    //               Convert.ToInt32(dc.chkNull_0(dr["ROLE_ID"].ToString())), Convert.ToBoolean(dr["IsLessRight"]), Convert.ToBoolean(dr["IsDelRight"]), Convert.ToDateTime(dr["LASTUPDATE_DATE"]));
    //        }
    //    }
    //    catch (Exception a)
    //    {
    //        //MessageBox.Show("Error While Insert Synced Data of SKU Hierarchy:\n" + a.Message);
    //    }
    //}
    //#endregion

    //#region Module
    //public void InsertModule(DataTable dtServerData)
    //{
    //    try
    //    {
    //        foreach (DataRow dr in dtServerData.Rows)
    //        {
    //            int intModuleID = Constants.IntNullValue;
    //            int intModulePrentID = Constants.IntNullValue;
    //            int intModuleTypeID = Constants.IntNullValue;
    //            DateTime dtLastUpdate = Constants.DateNullValue;
    //            bool bIsActive = false;
    //            if (dr["MODULE_ID"].ToString() != "")
    //            {
    //                intModuleID = Convert.ToInt32(dr["MODULE_ID"]);
    //            }
    //            if (dr["MODULE_PARENT_ID"].ToString() != "")
    //            {
    //                intModulePrentID = Convert.ToInt32(dr["MODULE_PARENT_ID"]);
    //            }
    //            if (dr["MODULE_TYPE_ID"].ToString() != "")
    //            {
    //                intModuleTypeID = Convert.ToInt32(dr["MODULE_TYPE_ID"]);
    //            }
    //            if (dr["LASTUPDATE_DATE"].ToString() != "")
    //            {
    //                dtLastUpdate = Convert.ToDateTime(dr["LASTUPDATE_DATE"]);
    //            }
    //            if (dr["IS_ACTIVE"].ToString() != "")
    //            {
    //                bIsActive = Convert.ToBoolean(dr["IS_ACTIVE"]);
    //            }
    //            mController.Insert_ModuleClient(intModuleID, dr["MODULE_DESCRIPTION"].ToString(), intModulePrentID, intModuleTypeID, dtLastUpdate, bIsActive, dr["MODULE_KEY"].ToString());
    //        }
    //    }
    //    catch (Exception a)
    //    {
    //        //MessageBox.Show("Error While Insert Synced Data of Module:\n" + a.Message);
    //    }
    //}

    //#endregion

    //#region SKUS

    //public void InsertSKUS(DataTable dtServerData)
    //{
    //    try
    //    {
    //        foreach (DataRow dr in dtServerData.Rows)
    //        {
    //            decimal GSTRateReg = Constants.DecimalNullValue;
    //            int UserID = Constants.IntNullValue;

    //            if (dr["GST_RATE_REG"].ToString() != "")
    //            {
    //                GSTRateReg = Convert.ToDecimal(dr["GST_RATE_REG"]);
    //            }
    //            if (dr["USER_ID"].ToString() != "")
    //            {
    //                UserID = Convert.ToInt32(dr["USER_ID"]);
    //            }

    //            mController.InsertSKUSClient(Convert.ToInt32(dr["SKU_ID"]), Convert.ToBoolean(dr["ISEXEMPTED"]), Convert.ToBoolean(dr["ISACTIVE"]), Convert.ToChar(dr["GST_ON"].ToString().Trim()), Convert.ToInt32(dr["PRINCIPAL_ID"]), Convert.ToInt32(dr["DIVISION_ID"]), Convert.ToInt32(dr["CATEGORY_ID"]), Convert.ToInt32(dr["BRAND_ID"]), GSTRateReg, Convert.ToDecimal(dr["GST_RATE_UNREG"]), short.Parse(dr["UNITS_IN_CASE"].ToString()), dr["SKU_CODE"].ToString(), dr["SKU_NAME"].ToString()
    //            , dr["IP_ADDRESS"].ToString(), dr["PACKSIZE"].ToString(), UserID, Convert.ToInt32(dr["COMPANY_ID"]), dr["DESCRIPTION"].ToString(), Convert.ToInt32(dr["SECTION_ID"]));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionPublisher.PublishExceptionforSync(ex, "Error in SKUS sync.");
    //    }
    //}

    //public void InsertSKUHeirarchy(DataTable dtServerData)
    //{
    //    try
    //    {
    //        foreach (DataRow dr in dtServerData.Rows)
    //        {
    //            mController.InsertSKUHierarchyClient(Convert.ToInt32(dr["SKU_HIE_ID"]), Convert.ToInt32(dc.chkNull_0(dr["SKU_HIE_TYPE_ID"].ToString())), Convert.ToInt32(dc.chkNull_0(dr["PARENT_SKU_HIE_ID"].ToString())), dr["SKU_HIE_CODE"].ToString(), dr["SKU_HIE_NAME"].ToString(),
    //                dr["IP_ADDRESS"].ToString(), Convert.ToBoolean(dr["IS_ACTIVE"]), Convert.ToInt32(dr["COMPANY_ID"]), Convert.ToBoolean(dr["IS_MANUALDISCOUNT"]), Convert.ToDateTime(dr["LASTUPDATE_DATE"]));
    //        }
    //    }
    //    catch (Exception a)
    //    {
    //        //MessageBox.Show("Error While Insert Synced Data of SKU Hierarchy:\n" + a.Message);
    //    }
    //}

    //public void InsertSKUPrice(DataTable dtServerData)
    //{
    //    try
    //    {
    //        foreach (DataRow dr in dtServerData.Rows)
    //        {
    //            decimal DistributorDiscount = Constants.DecimalNullValue;
    //            decimal TaxPrice = Constants.DecimalNullValue;
    //            decimal DistributorPrice = Constants.DecimalNullValue;

    //            if (dr["DISTRIBUTOR_DISCOUNT"].ToString() != "")
    //            {
    //                DistributorDiscount = Convert.ToDecimal(dr["DISTRIBUTOR_DISCOUNT"]);
    //            }
    //            if (dr["TAX_PRICE"].ToString() != "")
    //            {
    //                TaxPrice = Convert.ToDecimal(dr["TAX_PRICE"]);
    //            }
    //            if (dr["DISTRIBUTOR_PRICE"].ToString() != "")
    //            {
    //                DistributorPrice = Convert.ToDecimal(dr["DISTRIBUTOR_PRICE"]);
    //            }
    //            mController.InsertSKU_PRICESClient(Convert.ToInt32(dr["DISTRIBUTOR_ID"]), Convert.ToInt32(dr["SKU_ID"]), DistributorDiscount, TaxPrice, DistributorPrice, Convert.ToDecimal(dc.chkNull_0(dr["TRADE_PRICE"].ToString())), Convert.ToDecimal(dc.chkNull_0(dr["RETAIL_PRICE"].ToString())), Convert.ToDateTime(dr["DATE_EFFECTED"]),
    //                Convert.ToDecimal(dc.chkNull_0(dr["SED_TAX"].ToString())), Convert.ToDateTime(dr["LASTUPDATE_DATE"]));
    //        }
    //    }
    //    catch (Exception a)
    //    {
    //        //MessageBox.Show("Error While Insert Synced Data of SKU Price:\n" + a.Message);
    //    }
    //}


    //#endregion

    //#region Role
    //public void InsertRoleMaster(DataTable dtServerData)
    //{
    //    try
    //    {
    //        foreach (DataRow dr in dtServerData.Rows)
    //        {
    //            mController.InsertRoleMasterClient(Convert.ToInt32(dr["ROLE_ID"]), dr["ROLE_NAME"].ToString(), Convert.ToDateTime(dr["LASTUPDATE_DATE"]));
    //        }
    //    }
    //    catch (Exception a)
    //    {
    //        //MessageBox.Show("Error While Insert Synced Data of Role Master:\n" + a.Message);
    //    }
    //}

    //public void InsertRoleDetail(DataTable dtServerData)
    //{
    //    try
    //    {
    //        mController.InsertRoleDetailClient(dtServerData);

    //    }
    //    catch (Exception a)
    //    {
    //        //MessageBox.Show("Error While Insert Synced Data of Role Detail:\n" + a.Message);
    //    }
    //}

    //#endregion

    //#region Tables

    //public void InsertTable(DataTable dtServerData)
    //{
    //    try
    //    {
    //        foreach (DataRow dr in dtServerData.Rows)
    //        {
    //            mController.InsertTableDefinationClient(Convert.ToInt32(dr["TableDefination_ID"]), Convert.ToInt32(dr["distributorId"]), dr["TableDefination_No"].ToString(), dr["TableDefination_Description"].ToString(), dr["TableDefination_Capacity"].ToString(), dr["TableDefination_Abbrivation"].ToString(), Convert.ToBoolean(dr["Is_Active"]), Convert.ToInt32(dr["USER_ID"]));
    //        }
    //    }
    //    catch (Exception a)
    //    {
    //        //MessageBox.Show("Error While Insert Synced Data of Role Master:\n" + a.Message);
    //    }
    //}

    //#endregion

    //#region Finished Goods

    //public void InsertFinishedGoodMaster(DataTable dtServerData)
    //{
    //    try
    //    {
    //        foreach (DataRow dr in dtServerData.Rows)
    //        {
    //            mController.InsertFinishedGoodMasterClient(Convert.ToInt32(dr["FINISHED_GOOD_MASTER_ID"]), Convert.ToInt32(dr["FINISHED_SKU_ID"]), Convert.ToDateTime(dr["DOCUMENT_DATE"]), Convert.ToInt32(dr["USER_ID"]), Convert.ToDateTime(dr["LASTUPDATE_DATE"]));
    //        }
    //    }
    //    catch (Exception a)
    //    {
    //        //MessageBox.Show("Error While Insert Synced Data of Role Master:\n" + a.Message);
    //    }
    //}
    //public void InsertFinishedGoodDetail(DataTable dtServerData)
    //{
    //    try
    //    {
    //        foreach (DataRow dr in dtServerData.Rows)
    //        {
    //            mController.InsertFinishedGoodDetailClient(Convert.ToInt32(dr["FINISHED_GOOD_MASTER_ID"]), Convert.ToInt32(dr["SKU_ID"]), Convert.ToDecimal(dr["QUANTITY"]), Convert.ToDateTime(dr["LASTUPDATE_DATE"]));
    //        }
    //    }
    //    catch (Exception a)
    //    {
    //        //MessageBox.Show("Error While Insert Synced Data of Role Master:\n" + a.Message);
    //    }
    //}

    //#endregion

    //#region Tables

    //public void InsertProductSection(DataTable dtServerData)
    //{
    //    try
    //    {
    //        foreach (DataRow dr in dtServerData.Rows)
    //        {
    //            mController.InsertProductSectionClient(Convert.ToInt32(dr["SECTION_ID"]), dr["SECTION_CODE"].ToString(), dr["SECTION_NAME"].ToString(), dr["PRINTER_NAME"].ToString(), Convert.ToBoolean(dr["IS_ACTIVE"]),Convert.ToDateTime(dr["LAST_UPDATE"]));
    //        }
    //    }
    //    catch (Exception a)
    //    {
    //        //MessageBox.Show("Error While Insert Synced Data of Role Master:\n" + a.Message);
    //    }
    //}

    //#endregion
}