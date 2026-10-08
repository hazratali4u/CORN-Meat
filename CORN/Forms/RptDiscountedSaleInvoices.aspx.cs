using System;
using System.Web;
using System.Web.UI;
using System.Data;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;


public partial class Forms_Default2 : System.Web.UI.Page
{
    readonly SkuController SKUCtl = new SkuController();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");
        if (!Page.IsPostBack)
        {
            LoadDISTRIBUTOR();
            ddDistributorId_SelectedIndexChanged(null, null);
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }
    }
    private void LoadDISTRIBUTOR()
    {
        DistributorController mController = new DistributorController();
        try
        {
            DataTable dt = mController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
           
            clsWebFormUtil.FillDxComboBoxList(ddDistributorId, dt, 0, 2);

            if (dt.Rows.Count > 0)
            {
                ddDistributorId.SelectedIndex = 0;
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
    private void LoadUser()
    {
        if (ddDistributorId.Items.Count > 0)
        {
            try
            {
                Distributor_UserController du = new Distributor_UserController();
                DataTable dt = du.SelectDistributorUser(3, int.Parse(ddDistributorId.SelectedItem.Value.ToString()), int.Parse(Session["CompanyId"].ToString()));
               
                clsWebFormUtil.FillDxComboBoxList(ddUser, dt, 0,1, true);

                if (dt.Rows.Count > 0)
                {
                    ddUser.SelectedIndex = 0;
                }


                Session.Add("User", dt);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    protected void ddDistributorId_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUser();
    }


    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
            CORNBusinessLayer.Reports.CrpDiscountedSaleInvoices CrpReport = new CORNBusinessLayer.Reports.CrpDiscountedSaleInvoices();
            DataSet ds = new DataSet();
            ds = SKUCtl.SelectDiscountedInvoices(Convert.ToInt32(ddDistributorId.SelectedItem.Value), Convert.ToInt32(ddUser.SelectedItem.Value), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text));

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("Location", ddDistributorId.SelectedItem.Text);
            CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
            CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
            CrpReport.SetParameterValue("ReportType", "Discounted Invoices Sales");
            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
            RptSaleController RptSaleCtl = new RptSaleController();
            CORNBusinessLayer.Reports.CrpDiscountedSaleInvoices CrpReport = new CORNBusinessLayer.Reports.CrpDiscountedSaleInvoices();
            DataSet ds = new DataSet();
            ds = SKUCtl.SelectDiscountedInvoices(Convert.ToInt32(ddDistributorId.SelectedItem.Value), Convert.ToInt32(ddUser.SelectedItem.Value), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text));

        CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

        CrpReport.SetParameterValue("Location", ddDistributorId.SelectedItem.Text);
        CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
        CrpReport.SetParameterValue("ToDate", txtEndDate.Text);

        Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", 1);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }

    }