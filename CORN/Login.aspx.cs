using System;
using System.Data;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Collections;
using System.Web;
using System.Collections.Generic;
using CornPointHelper;

public partial class Login : Page
{
    readonly UserController _mController = new UserController();
    readonly DistributorController _mDist = new DistributorController();
    ConfigurationController _cController = new ConfigurationController();
    public static DataTable dtConfig;

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlHelper.GetAppPath();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            dtConfig = _cController.SelectAppConfiguration(1, (int)Enums.AppSettingMaster.WebSettings, null, Constants.IntNullValue);

            try
            {
                if (Convert.ToInt32(Session["UserID"]) > 0)
                {
                    Response.Redirect("Forms/Home.aspx");
                }
            }
            catch (Exception)
            {
                txtLogin.Focus();
            }
            DataRow[] dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.IsEncreptedCredentials + "'");
            bool IsEncrypted = Convert.ToBoolean(Convert.ToInt32(dr[0]["VALUE"]));

            if (IsEncrypted)
            {
                dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.Deployed + "'");
                if (dr[0]["VALUE"].ToString() == Cryptography.Encrypt("Deployed", "b0tin@74"))
                {
                    DataTable dtLicenseData = _mDist.GetLicenseData();
                    if (dtLicenseData.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtLicenseData.Rows[0]["DAYS"]) >= 70)
                        {
                            Response.Redirect("License.aspx");
                        }
                        if (Convert.ToInt32(dtLicenseData.Rows[0]["DAYS"]) >= 61)
                        {
                            lblLicenseMsg.Text = "Software License will expire after " + Convert.ToString(70 - Convert.ToInt32(dtLicenseData.Rows[0]["DAYS"])) + " days. Please Contact to Service Provider.";
                            dvLicense.Visible = true;
                        }
                        else
                        {
                            txtLogin.Focus();
                        }
                    }
                }
            }
            txtLogin.Focus();
        }
    }
    private void ValidateUser()
    {

        if (txtLogin.Text == "" && txtPassword.Text == "")
        {
            lblErrorMsg.Text = "Plz enter User name / Password";
            dvError.Visible = true;
            return;

        }
        else
        {
            DataTable dt = null;
            DataRow[] dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.IsEncreptedCredentials + "'");
            bool IsEncrypted = Convert.ToBoolean(Convert.ToInt32(dr[0]["VALUE"]));

            if (IsEncrypted)
            {
                dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.Deployed + "'");
                if (dr[0]["VALUE"].ToString() == Cryptography.Encrypt("Deployed", "b0tin@74"))
                {

                    dt = _mController.SelectSlashUserEncrypt(txtLogin.Text, Cryptography.Encrypt(txtPassword.Text, "b0tin@74"));
                }
            }
            else
            {
                dt = _mController.SelectSlashUser(txtLogin.Text, txtPassword.Text);
            }
            Session.Clear();

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToString(dt.Rows[0]["IS_ACTIVE"]) == "True" || Convert.ToString(dt.Rows[0]["IS_ACTIVE"]) == "Active")
                {
                    ArrayList list = new ArrayList();
                    {
                        list.Add(dt.Rows[0]["ROLE_ID"].ToString());
                        list.Add(dt.Rows[0]["USER_DETAIL"].ToString());
                    }
                    Session["EmployeeInfo"] = list;
                    DataTable dtOne = _cController.SelectAppConfiguration(1, (int)Enums.AppSettingMaster.InventorySetting, null, Constants.IntNullValue);

                    dr = dtOne.Select("CODE = '" + (int)Enums.AppSetting.ShowClosingStockStatus + "'");
                    bool ClosingStockStatus = Convert.ToBoolean(Convert.ToInt32(dr[0]["VALUE"]));


                    Session.Add("VALIDATE_STOCK", ClosingStockStatus);
                    Session.Add("UserID", Convert.ToInt32(dt.Rows[0]["USER_ID"].ToString()));
                    Session.Add("UserName", dt.Rows[0]["USER_DETAIL"].ToString());
                    Session.Add("DISTRIBUTOR_ID", Convert.ToInt32(dt.Rows[0]["DISTRIBUTOR_ID"]));
                    Session.Add("CompanyId", Convert.ToInt32(dt.Rows[0]["COMPANY_ID"].ToString()));
                    Session.Add("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());
                    Session.Add("ADDRESS1", dt.Rows[0]["ADDRESS1"].ToString());
                    Session.Add("PHONE", dt.Rows[0]["PHONE"].ToString());
                    Session.Add("RoleID", Convert.ToInt32(dt.Rows[0]["ROLE_ID"]));
                    Session.Add("IS_SystemAdmin", Convert.ToInt32(dt.Rows[0]["IS_SystemAdmin"]));
                    Session.Add("IS_CanReverseDayClose", Convert.ToBoolean(dt.Rows[0]["IS_CanReverseDayClose"]));
                    Session.Add("IS_CanGiveDiscount", Convert.ToBoolean(dt.Rows[0]["IS_CanGiveDiscount"]));
                    Session.Add("Division", Convert.ToInt32(dt.Rows[0]["ISCURRENT"]));
                    LastClosedDay(Convert.ToInt32(dt.Rows[0]["USER_ID"]), Convert.ToInt32(dt.Rows[0]["DISTRIBUTOR_ID"]));
                    long userLogId = _mController.InsertUserLoginTime(Convert.ToInt32(dt.Rows[0]["USER_ID"]));
                    Session.Add("User_Log_ID", userLogId);

                    if (dt.Rows[0]["ROLE_NAME"].ToString() == "Tab")
                    {
                        Response.Redirect("Forms/frmOrderTaking.aspx");
                    }
                    else
                    {
                        Response.Redirect("Forms/Home.aspx");
                    }
                }
                else
                {
                    lblErrorMsg.Text = "This User is In Active";
                    dvError.Visible = true;
                }
            }
            else
            {
                lblErrorMsg.Text = "Wrong User Id/Password";
                dvError.Visible = true;
            }
        }
    }
    private void LastClosedDay(int userId, int pDistributor)
    {
        DataTable dt = _mDist.SelectMaxDayClose(userId, pDistributor);
        Session.Add("CurrentWorkDate", dt.Rows.Count > 0 ? DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString()) : DateTime.Now);
    }

    private bool CheckDeplyed()
    {
        try
        {
            DataRow[] dr1 = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.IsDeployed + "'");
            bool deployed = Convert.ToBoolean(Convert.ToInt32(dr1[0]["VALUE"]));
            if (deployed)
            {
                return true;
            }
            if (dtConfig.Rows.Count > 0)
            {
                DataRow[] dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.ComputerInfo + "'");
                string CInfo = Cryptography.Encrypt(ComputerInfo.Value(), "b0tin@74");
                string[] ComInfo = (dr[0]["VALUE"].ToString()).Split(',');
                int length = ComInfo.Length;
                bool ComCheck = false;
                for (int i = 0; i < length; i++)
                {
                    if (ComInfo[i].ToString().Trim() == CInfo)
                    {
                        ComCheck = true;
                        break;
                    }
                }
                return ComCheck;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
            return false;
        }
    }
    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        bool Check = CheckDeplyed();
        if (Check)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["IsDeployment"] != null)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["IsDeployment"].ToString() == "1")
                {
                    int DistributorID = Convert.ToInt32(DataControl.chkNull_Zero(System.Configuration.ConfigurationManager.AppSettings["DistributorID"].ToString()));
                    DataTable dtServerData = new DataTable();
                    clsProcessServerData ProcessData = new clsProcessServerData();

                    try
                    {
                        //#region Distributor Assignment
                        //try
                        //{
                        //    dtServerData = ProcessData.GetDataFromServer2(2, DistributorID);
                        //    if (dtServerData.Rows.Count > 0)
                        //    {
                        //        ProcessData.InsertDistributorAssignment(DistributorID, dtServerData);
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
                        //    dtServerData = ProcessData.GetDataFromServer(3, DistributorID);
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
                        //    dtServerData = ProcessData.GetDataFromServer(4, DistributorID);
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
                        //    dtServerData = ProcessData.GetDataFromServer(5, DistributorID);
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

                        //#region Role Master
                        //try
                        //{
                        //    dtServerData = ProcessData.GetDataFromServer(9, DistributorID);
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
                        //    dtServerData = ProcessData.GetDataFromServer2(10, DistributorID);
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

                        ValidateUser();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    ValidateUser();
                }
            }
            else
            {
                ValidateUser();
            }
        }
        else
        {
            dvError.Visible = true;
            lblErrorMsg.Text = "Invalid Key. Contact to Administrator.";
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

    //public DataTable GetDataFromServer2(int intType, int intDistributorID)
    //{
    //    DataTable dtServerData = new DataTable();
    //    try
    //    {
    //        dtServerData = syncService.GetServerData(Constants.DateNullValue, intType, intDistributorID);
    //    }
    //    catch (Exception a)
    //    {
    //        //MessageBox.Show("Error While Sync Distributor :\n" + a.Message);
    //    }
    //    return dtServerData;
    //}

    //#endregion

    //#region Distributor
    //public void InsertDistributorAssignment(int p_DISTRIBUTOR_ID, DataTable dtServerData)
    //{
    //    try
    //    {
    //        if (mController.DeleteUserAssignmentClient(p_DISTRIBUTOR_ID))
    //        {
    //            mController.InsertUserAssignmentClient(p_DISTRIBUTOR_ID, dtServerData);
    //        }
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
    //        if (mController.DeleteRoleDetailClient())
    //        {
    //            mController.InsertRoleDetailClient(dtServerData);
    //        }

    //    }
    //    catch (Exception a)
    //    {
    //        //MessageBox.Show("Error While Insert Synced Data of Role Detail:\n" + a.Message);
    //    }
    //}

    //#endregion
}