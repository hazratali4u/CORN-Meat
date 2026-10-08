using System;
using System.Data;
using System.Web;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web.UI.WebControls;

public partial class Forms_frmCustomerFeedback : System.Web.UI.Page
{
    readonly CustomerFeedbackController cCustomerFeedback = new CustomerFeedbackController();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");
        if (!Page.IsPostBack)
        {
            txtOther.Attributes.Add("readonly", "readonly");
        }
    }
    //=======Rates========
    // 1 = POOR
    // 2 = FAIR
    // 3 = GOOD
    // 4 = VERY GOOD
    // 5 = EXCELLENT 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            cCustomerFeedback.InsertFeedBack(Convert.ToInt32(Session["DISTRIBUTOR_ID"]), FindRate("Service"), FindRate("Food"), FindRate("Environment"), FindRate("Overall"), txtComments.Text, FindRate("Hear"), txtOther.Text, txtName.Text, txtContact.Text, txtEmail.Text, null);
            CustomerDataController.InsertCustomer(Convert.ToInt32(Session["DISTRIBUTOR_ID"]), "", "", "", txtContact.Text, txtEmail.Text, txtName.Text, "", null, 0, "", "", 0,0,0,0,0);
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }
        clearAll();
        ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "ErrorMessage", "ShowPopup()", true);
    }
    public void clearAll()
    {
        Service1.Checked = false;
        Service2.Checked = false;
        Service3.Checked = false;
        Service4.Checked = false;
        Service5.Checked = false;

        Food1.Checked = false;
        Food2.Checked = false;
        Food3.Checked = false;
        Food4.Checked = false;
        Food5.Checked = false;

        Environment1.Checked = false;
        Environment2.Checked = false;
        Environment3.Checked = false;
        Environment4.Checked = false;
        Environment5.Checked = false;

        Overall1.Checked = false;
        Overall2.Checked = false;
        Overall3.Checked = false;
        Overall4.Checked = false;
        Overall5.Checked = false;

        Hear1.Checked = false;
        Hear2.Checked = false;
        Hear3.Checked = false;
        Hear4.Checked = false;
        Hear5.Checked = false;
        Hear6.Checked = false;

        txtComments.Text = "";
        txtContact.Text = "";
        txtEmail.Text = "";
        txtName.Text = "";
        txtOther.Text = "";
    }

    public int FindRate(String ListName)
    {
        if (ListName == "Service")
        {
            if (Service1.Checked)
            {
                return 1;
            }
            if (Service2.Checked)
            {
                return 2;
            }
            if (Service3.Checked)
            {
                return 3;
            }
            if (Service4.Checked)
            {
                return 4;
            }
            if (Service5.Checked)
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }
        if (ListName == "Food")
        {
            if (Food1.Checked)
            {
                return 1;
            }
            if (Food2.Checked)
            {
                return 2;
            }
            if (Food3.Checked)
            {
                return 3;
            }
            if (Food4.Checked)
            {
                return 4;
            }
            if (Food5.Checked)
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }
        if (ListName == "Environment")
        {
            if (Environment1.Checked)
            {
                return 1;
            }
            if (Environment2.Checked)
            {
                return 2;
            }
            if (Environment3.Checked)
            {
                return 3;
            }
            if (Environment4.Checked)
            {
                return 4;
            }
            if (Environment5.Checked)
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }
        if (ListName == "Overall")
        {
            if (Overall1.Checked)
            {
                return 1;
            }
            if (Overall2.Checked)
            {
                return 2;
            }
            if (Overall3.Checked)
            {
                return 3;
            }
            if (Overall4.Checked)
            {
                return 4;
            }
            if (Overall5.Checked)
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }
        if (ListName == "Hear")
        {
            if (Hear1.Checked)
            {
                return 1;
            }
            if (Hear2.Checked)
            {
                return 2;
            }
            if (Hear3.Checked)
            {
                return 3;
            }
            if (Hear4.Checked)
            {
                return 4;
            }
            if (Hear5.Checked)
            {
                return 5;
            }
            if (Hear6.Checked)
            {
                return 6;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
    }
}
