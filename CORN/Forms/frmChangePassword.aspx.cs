using System;
using System.Data;
using System.Web;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// Form To Change User Password
/// </summary>
public partial class Forms_fmChangePassword : System.Web.UI.Page
{
    UserController UserInfo = new UserController();
    DataTable dtLogin_ID = new DataTable();
    static DataTable dtConfig;

    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        txtCurrentPassword.Attributes["value"] = txtCurrentPassword.Text;
        txtNewPassword.Attributes["value"] = txtNewPassword.Text;
        txtConfirmNewPassword.Attributes["value"] = txtConfirmNewPassword.Text;

        if (!Page.IsPostBack)
        {
            dtLogin_ID = UserInfo.SelectSlashUser(Convert.ToInt32(Session["UserID"].ToString()));
            this.txtCurrentPassword.Text = dtLogin_ID.Rows[0]["PASSWORD"].ToString();

            btnSave.Attributes.Add("onclick", "return ValidatePassword();");
            dtConfig = GetAppConfiguration();
        }
    }
    public DataTable GetAppConfiguration()
    {
        try
        {
            ConfigurationController _cController = new ConfigurationController();
            DataTable dt = _cController.SelectAppConfiguration(1, (int)Enums.AppSettingMaster.WebSettings, null, Constants.IntNullValue);
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
            return null;
        }
    }
    /// <summary>
    /// Updates User Password
    /// </summary>
    /// <remarks>
    /// Returns True on Success And False on Failure
    /// </remarks>
    /// <returns>True on Success And False on Failure</returns>
    protected bool UpdatePassword()
    {
        try
        {
            dtLogin_ID = UserInfo.SelectSlashUser(Convert.ToInt32(Session["UserID"].ToString()));
            if (txtCurrentPassword.Text == dtLogin_ID.Rows[0]["PASSWORD"].ToString())
            {
                if (txtCurrentPassword.Text == txtNewPassword.Text)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Current password is same as new password ');", true);
                    return false;
                }
                txtConfirmNewPassword.Text = CheckDeplyed();
                try
                {
                    UserInfo.UpdatePassword(Convert.ToInt32(Session["UserID"].ToString())
                         , dtLogin_ID.Rows[0]["LOGIN_ID"].ToString(), txtConfirmNewPassword.Text);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Catchmsg", "alert('" + ex.Message.ToString() + "');", true);
                    return false;
                }

                return true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Current Password is wrong ');", true);
                return false;
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Catchmsg2", "alert('" + ex.Message.ToString() + "');", true);
            return false;
        }
    }
    private string CheckDeplyed()
    {
        DataRow[] dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.IsEncreptedCredentials + "'");
        bool IsEncrypted = Convert.ToBoolean(Convert.ToInt32(dr[0]["VALUE"]));

        if (IsEncrypted)
        {
            dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.Deployed + "'");
            if (dr[0]["VALUE"].ToString() == Cryptography.Encrypt("Deployed", "b0tin@74"))
            {
                txtConfirmNewPassword.Attributes["type"] = "text";
                return Cryptography.Encrypt(txtConfirmNewPassword.Text, "b0tin@74");
            }
            else
            {
                return txtConfirmNewPassword.Text;
            }
        }
        else
        {
            return txtConfirmNewPassword.Text;
        }
    }

    /// <summary>
    /// Updates User Password Through UpdatePassword() Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool status = UpdatePassword();

        if (status == true)
        {
            txtCurrentPassword.Attributes["value"] = "";
            txtNewPassword.Attributes["value"] = "";
            txtConfirmNewPassword.Attributes["value"] = "";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Password has changed successfully ');", true);
        }
    }

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtNewPassword.Attributes.Add("value", "");
        txtCurrentPassword.Attributes.Add("value", "");
        txtConfirmNewPassword.Attributes.Add("value", "");
        //Response.Redirect("~/Forms/Home.aspx");
    }
}
