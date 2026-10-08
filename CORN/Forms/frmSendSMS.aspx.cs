using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using System.Data;
using CORNCommon.Classes;


/// <summary>
/// Form To Change User Password
/// </summary>
public partial class Forms_frmSendSMS : System.Web.UI.Page
{

    GsmCommMain comm;
    readonly  ConfigurationController _cController = new ConfigurationController();

    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadCustomers();
        }
    }
    private void LoadCustomers()
    {
        LstCustomer.Items.Clear();

        var mController = new CustomerDataController();
        DataTable dtCustomers = mController.UspSelectCustomer(Constants.IntNullValue, "SMS", "", DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillListBox(LstCustomer, dtCustomers, 0, 3, false);
    }
    public DataTable GetSMSConfiguration()
    {
        try
        {
            DataTable dt = _cController.SelectAppConfiguration(1, (int)Enums.AppSettingMaster.SMS, null, Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg3", "alert('Error Occured: \n" + ex + "');", true);
            return null;
        }
    }
    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        DataTable dtConfig = GetSMSConfiguration();
        if (dtConfig == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Configure SMS setting for this location');", true);
            return;
        }
        hfNumber.Value = "";

        for (int i = 0; i < LstCustomer.Items.Count; i++)
        {
            if (LstCustomer.Items[i].Selected == true)
            {
                hfNumber.Value += LstCustomer.Items[i].Value.ToString() + ",";
            }
        }
        if (hfNumber.Value == "" && txtPhoneNo.Text.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please select customer or enter number.');", true);
            return;
        }


        lblError.Text = string.Empty;
        //string SMSSendingType = System.Configuration.ConfigurationManager.AppSettings["SMSSendingType"].ToString();
        DataRow[] dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.SMSSendingType + "'");
        string SMSSendingType = dr[0]["VALUE"].ToString();

        if (SMSSendingType == "Web Service")
        {
            lblError.Text = readHtmlPage("http://www.sms4connect.com/api/sendsms.php/sendsms/url", dtConfig);
            
        }
        else
        {
            Int32 comport, baudrate, timeout;
            dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.SMSComPort + "'");
            comport = Convert.ToInt32(dr[0]["VALUE"].ToString());
            dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.SMSbaudrate + "'");
            baudrate = Convert.ToInt32(dr[0]["VALUE"].ToString());
            dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.SMStimeout + "'");
            timeout = Convert.ToInt32(dr[0]["VALUE"].ToString());

            CommSetting cs = new CommSetting();
            SmsSubmitPdu pdu;
            cs.comm = new GsmCommMain(comport, baudrate, timeout);
            comm = cs.comm;

            try
            {
                if (!comm.IsOpen())
                {
                    comm.Open();
                }
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishExceptionforSync(ex, "Exception from SMS");
                lblError.Text = ex.Message;
            }

            try
            {

                if (hfNumber.Value != "")
                {
                    for (int i = 0; i < LstCustomer.Items.Count; i++)
                    {
                        if (LstCustomer.Items[i].Selected == true)
                        {

                            pdu = new SmsSubmitPdu(txtSMS.Text, LstCustomer.Items[i].Value);
                            cs.comm.SendMessage(pdu);
                        }
                    }
                    lblError.Text = "Message Sent";
                }

                if (txtPhoneNo.Text != "")
                {

                    pdu = new SmsSubmitPdu(txtSMS.Text, txtPhoneNo.Text);
                    cs.comm.SendMessage(pdu);

                    lblError.Text = "Message Sent";
                }
                comm.Close();
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }

        }
    }

    //private String readHtmlPage(string url)
    //{
    //    String result = "";
    //    String message = HttpUtility.UrlEncode("Hello this is a test with a 5 note and an ampersand (&) symbol");
    //    String strPost = "id=rchbaitalmandi&pass=mandi456&msg=" + message + "&to=923426141628" + "&mask=BAITALMANDI&type=xml&lang=English";

    //    StreamWriter myWriter = null;
    //    HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
    //    objRequest.Method = "POST";
    //    objRequest.ContentLength = Encoding.UTF8.GetByteCount(strPost);
    //    objRequest.ContentType = "application/x-www-form-urlencoded";
    //    try {
    //        myWriter = new StreamWriter(objRequest.GetRequestStream());
    //        myWriter.Write(strPost);
    //    }
    //    catch (Exception e) {
    //        return e.Message;
    //    } finally {
    //        myWriter.Close();
    //    }
    //    HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
    //    using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
    //    {
    //        result = sr.ReadToEnd(); // Close and clean up the StreamReader 
    //        sr.Close(); 
    //    }
    //    return result;
    //}


    private String readHtmlPage(string url, DataTable dtConfig)
    {

        string receipients = "";

        if (hfNumber.Value != "" && txtPhoneNo.Text.ToString() != "")
        {
            receipients = hfNumber.Value + txtPhoneNo.Text.ToString();
        }
        if (hfNumber.Value != "" && txtPhoneNo.Text.ToString() == "")
        {
            receipients = hfNumber.Value;
        }
        if (hfNumber.Value == "" && txtPhoneNo.Text.ToString() != "")
        {
            receipients = txtPhoneNo.Text.ToString();
        }
        DataRow[] dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.SMSUserID + "'");
        string ID = dr[0]["VALUE"].ToString();
        dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.SMSPassword + "'");
        string PWD = dr[0]["VALUE"].ToString();
        dr = dtConfig.Select("CODE = '" + (int)Enums.AppSetting.SMSMask + "'");
        string MASK = dr[0]["VALUE"].ToString();

        String result = "";
        String message = HttpUtility.UrlEncode(txtSMS.Text);

        String strPost = "id=" + ID + "&pass=" + PWD + "&msg=" + message + "&to=" + receipients + "&mask=" + MASK + "&type=xml&lang=English";
        // String strPost = "id=rchbaitalmandi&pass=mandi456&msg=" + "AutoPostBackControl" +"&to=923426141628" + "&mask=BAITALMANDI&type=xml&lang=English";
        StreamWriter myWriter = null;
        HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
        objRequest.Method = "POST";
        objRequest.ContentLength = Encoding.UTF8.GetByteCount(strPost);
        objRequest.ContentType = "application/x-www-form-urlencoded";
        try
        {
            myWriter = new StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(strPost);
        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
            myWriter.Close();
        }
        HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
        using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
        {
            result = sr.ReadToEnd();   // Close and clean up the StreamReader   
            sr.Close();
        }

        return result;
    }

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.txtPhoneNo.Text = null;
        this.txtSMS.Text = null;
    }
}
public class CommSetting : IDisposable
{
    private bool disposed = false;
    public static int Comm_Port = 2;
    public static int Comm_BaudRate = 9600;
    public static int Comm_TimeOut = 300;
    public static GsmCommMain comm2;
    public GsmCommMain comm;
    public static int count = 1;

    public CommSetting()
    {

    }

    public static Int32 Add()
    {
        return count++;
    }

    ~CommSetting()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this.comm);
    }

    private void Dispose(bool disposing)
    {
        if (this.comm.IsOpen())
        {
            this.comm.Close();
        }

        disposed = true;

    }
}