using System;
using System.Data;
using System.Web;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web.UI.WebControls;

public partial class Forms_frmConfiguration : System.Web.UI.Page
{
    UserController UserInfo = new UserController();
    DataTable dtLogin_ID = new DataTable();
    ConfigurationController _cController = new ConfigurationController();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadConfiguration();
            // btnSave.Attributes.Add("onclick", "return ValidatePassword();");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtMaster = _cController.ChekAppConfigurationExistance(2, DataControl.chkIntNull(drpLocation.SelectedItem.Value.ToString()));
            if (Convert.ToInt32(dtMaster.Rows[0]["IS_EXIST"]) == 0)
            {
                int MasterLength = dtMaster.Rows.Count;
                for (int i = 0; i < MasterLength; i++)
                {
                    _cController.InsertAppConfiguration(Convert.ToInt32(dtMaster.Rows[i]["CODE_MASTER"]), Convert.ToInt32(drpLocation.SelectedItem.Value));
                }
            }
            DataTable dtUpdateData = new DataTable();
            dtUpdateData.Columns.Add("Code", typeof(int));
            dtUpdateData.Columns.Add("ColumnValue", typeof(string));
            //Page Setting
            dtUpdateData.Rows.Add((int)Enums.AppSetting.MarginLeft, txtMarginLeft.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.MarginRight, txtMarginRight.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.MarginTop, txtMarginTop.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.MarginBottom, txtMarginBottom.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.Orientation, drpOrientation.SelectedItem.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.PaperFormat, drpPaperFormat.SelectedItem.Text);
            //Printer Setting
            dtUpdateData.Rows.Add((int)Enums.AppSetting.PrinterName, txtDefaultPrinter.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.PrinterAddress, txtPrinterAddress.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.PrinterPort, txtPrinterPort.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.NoOfCopies, txtNoofCopies.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.IsDirectPrinting, chkDirectPrinting.Checked ? "1" : "0");
            //Email Setting
            dtUpdateData.Rows.Add((int)Enums.AppSetting.SMTPServer, txtSMTPServer.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.EMAILUserName, txtEmailUserName.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.EMAILPassword, txtEmailPassword.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.EMIALPort, txtEmailPort.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.IsEnabledSSL, chkIsEnabledSSL.Checked ? "1" : "0");
            dtUpdateData.Rows.Add((int)Enums.AppSetting.IsUsedDefaultCredentials, chkUseDefaulCredentials.Checked ? "1" : "0");
            //SMS Setting
            dtUpdateData.Rows.Add((int)Enums.AppSetting.SMSSendingType, drpSMSSendingType.SelectedItem.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.SMSComPort, txtSMSCOMPort.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.SMSbaudrate, txtSMSBaudRate.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.SMStimeout, txtSMSTimeout.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.SMSUserID, txtSMSUserID.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.SMSPassword, txtSMSPassword.Text);
            dtUpdateData.Rows.Add((int)Enums.AppSetting.SMSMask, txtSMSMask.Text);
            //Web Config
            dtUpdateData.Rows.Add((int)Enums.AppSetting.IsEncreptedCredentials, chkIsEncripted.Checked ? "1" : "0");
            // Inventory Setting
            dtUpdateData.Rows.Add((int)Enums.AppSetting.ShowClosingStockStatus, chkClosingStockStatus.Checked ? "1" : "0");
            dtUpdateData.Rows.Add((int)Enums.AppSetting.NegativeAllowed, chkOversaleAllowed.Checked ? "1" : "0");
            dtUpdateData.Rows.Add((int)Enums.AppSetting.ExpiryAllowed, chkExpiryAllowed.Checked ? "1" : "0");
            dtUpdateData.Rows.Add((int)Enums.AppSetting.RawMaterialDeductionOnProduction, chkRawMaterialOnProduction.Checked ? "1" : "0");
            dtUpdateData.Rows.Add((int)Enums.AppSetting.PackageMaterialDeductionOnSale, chkPackageMaterialDeductionOnSale.Checked ? "1" : "0");


            bool flag = _cController.UpdateAppConfiguration(Constants.IntNullValue, Constants.IntNullValue, DataControl.chkIntNull(drpLocation.SelectedItem.Value.ToString()), dtUpdateData);
            if (flag)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg1", "Message('Configured Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "Message", "Message('Cannnot Configure')", true);
            }
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
        }
    }
    public void LoadColumnValueList(DevExpress.Web.ASPxComboBox drp, DataTable dt, int Code, DataRow[] dr)
    {
        try
        {
            drp.Items.Clear();
            dr = dt.Select("CODE = '" + Code + "' ");
            string Value = dr[0][3].ToString();
            string[] valueList = Value.Split(',');
            int Length = valueList.Length;
            for (int i = 0; i < Length; i++)
            {
                drp.Items.Add(valueList[i].ToString());
            }
            Value = dr[0][2].ToString();
            drp.Items.FindByText(Value).Selected = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
        }
    }
    protected void drpLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadConfiguration();
    }
    public void LoadConfiguration()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow[] dr = null;
            dt = _cController.SelectAppConfiguration(1, Constants.IntNullValue, null, Convert.ToInt32(drpLocation.SelectedItem.Value));
            if (dt.Rows.Count == 0)
            {
                dt = _cController.SelectAppConfiguration(1, Constants.IntNullValue, null, Constants.IntNullValue);
            }
            //Page Settings
            txtMarginLeft.Text = GetConfigValue((int)Enums.AppSetting.MarginLeft, dt, dr);
            txtMarginRight.Text = GetConfigValue((int)Enums.AppSetting.MarginRight, dt, dr);
            txtMarginTop.Text = GetConfigValue((int)Enums.AppSetting.MarginTop, dt, dr);
            txtMarginBottom.Text = GetConfigValue((int)Enums.AppSetting.MarginBottom, dt, dr);
            LoadColumnValueList(drpOrientation, dt, (int)Enums.AppSetting.Orientation, dr);
            LoadColumnValueList(drpPaperFormat, dt, (int)Enums.AppSetting.PaperFormat, dr);
            //Printer Settings
            txtDefaultPrinter.Text = GetConfigValue((int)Enums.AppSetting.PrinterName, dt, dr);
            txtPrinterAddress.Text = GetConfigValue((int)Enums.AppSetting.PrinterAddress, dt, dr);
            txtNoofCopies.Text = GetConfigValue((int)Enums.AppSetting.NoOfCopies, dt, dr);
            txtPrinterPort.Text = GetConfigValue((int)Enums.AppSetting.PrinterPort, dt, dr);
            chkDirectPrinting.Checked = (GetConfigValue((int)Enums.AppSetting.IsDirectPrinting, dt, dr) == "1") ? true : false;
            //Email Setting
            txtSMTPServer.Text = GetConfigValue((int)Enums.AppSetting.SMTPServer, dt, dr);
            txtEmailUserName.Text = GetConfigValue((int)Enums.AppSetting.EMAILUserName, dt, dr);
            txtEmailPassword.Text = GetConfigValue((int)Enums.AppSetting.EMAILPassword, dt, dr);
            txtEmailPort.Text = GetConfigValue((int)Enums.AppSetting.EMIALPort, dt, dr);
            chkIsEnabledSSL.Checked = (GetConfigValue((int)Enums.AppSetting.IsEnabledSSL, dt, dr) == "1") ? true : false;
            chkUseDefaulCredentials.Checked = (GetConfigValue((int)Enums.AppSetting.IsUsedDefaultCredentials, dt, dr) == "1") ? true : false;
            //SMS Setting
            LoadColumnValueList(drpSMSSendingType, dt, (int)Enums.AppSetting.SMSSendingType, dr);
            ScriptManager.RegisterStartupScript(this, GetType(), "asd", "SMSTypeChange()", true);
            txtSMSCOMPort.Text = GetConfigValue((int)Enums.AppSetting.SMSComPort, dt, dr);
            txtSMSBaudRate.Text = GetConfigValue((int)Enums.AppSetting.SMSbaudrate, dt, dr);
            txtSMSTimeout.Text = GetConfigValue((int)Enums.AppSetting.SMStimeout, dt, dr);
            txtSMSUserID.Text = GetConfigValue((int)Enums.AppSetting.SMSUserID, dt, dr);
            txtSMSPassword.Text = GetConfigValue((int)Enums.AppSetting.SMSPassword, dt, dr);
            txtSMSMask.Text = GetConfigValue((int)Enums.AppSetting.SMSMask, dt, dr);
            //Web Setting
            chkIsEncripted.Checked = (GetConfigValue((int)Enums.AppSetting.IsEncreptedCredentials, dt, dr) == "1") ? true : false;
            //Inventory Setting
            chkClosingStockStatus.Checked = (GetConfigValue((int)Enums.AppSetting.ShowClosingStockStatus, dt, dr) == "1") ? true : false;
            chkOversaleAllowed.Checked = (GetConfigValue((int)Enums.AppSetting.NegativeAllowed, dt, dr) == "1") ? true : false;
            chkExpiryAllowed.Checked = (GetConfigValue((int)Enums.AppSetting.ExpiryAllowed, dt, dr) == "1") ? true : false;
            chkRawMaterialOnProduction.Checked = (GetConfigValue((int)Enums.AppSetting.RawMaterialDeductionOnProduction, dt, dr) == "1") ? true : false;
            chkPackageMaterialDeductionOnSale.Checked = (GetConfigValue((int)Enums.AppSetting.PackageMaterialDeductionOnSale, dt, dr) == "1") ? true : false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
        }
    }
    private void LoadDistributor()
    {
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dtDistributor = DController.SelectDistributor(Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));

            drpLocation.Items.Add(new DevExpress.Web.ListEditItem("Default", Constants.IntNullValue.ToString()));

            clsWebFormUtil.FillDxComboBoxList(drpLocation, dtDistributor, 1, 3);

            if (dtDistributor.Rows.Count > 0)
            {
                drpLocation.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
        }
    }
    public string GetConfigValue(int Code, DataTable dt, DataRow[] dr)
    {
        try
        {
            dr = dt.Select("CODE = '" + Code + "' ");
            return dr[0][2].ToString();
        }
        catch (Exception EX)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + EX + "');", true);
            return null;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
}
