using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;
using CORNBusinessLayer.Reports;

/// <summary>
/// From to Add, Edit Employee
/// </summary>
public partial class Forms_frmShiftClose : System.Web.UI.Page
{
    Distributor_UserController UController = new Distributor_UserController();
    ShiftController _SController = new ShiftController();
    RptSaleController rptSaleCtl = new RptSaleController();
    int _diffenrence = 0;
    int Cur5000 = 0;
    int Cur1000 = 0;
    int Cur500 = 0;
    int Cur100 = 0;
    int Cur50 = 0;
    int Cur20 = 0;
    int Cur10 = 0;
    int Cur5 = 0;
    int Cur2 = 0;
    int Cur1 = 0;
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
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
            SetReadOnly();
            Session.Add("ShiftId", 0);
        }
        Response.Expires = 0;
        Response.Cache.SetNoStore();
    }
    public void SetReadOnly()
    {
        txtCal5000.Attributes.Add("readonly", "true");
        txtCal1000.Attributes.Add("readonly", "true");
        txtCal500.Attributes.Add("readonly", "true");
        txtCal100.Attributes.Add("readonly", "true");
        txtCal50.Attributes.Add("readonly", "true");
        txtCal20.Attributes.Add("readonly", "true");
        txtCal10.Attributes.Add("readonly", "true");
        txtCal5.Attributes.Add("readonly", "true");
        txtCal2.Attributes.Add("readonly", "true");
        txtCal1.Attributes.Add("readonly", "true");
        txtTotal.Attributes.Add("readonly", "true");

        txtOpeningCash.Attributes.Add("readonly", "true");
        txtOpeningRecieved.Attributes.Add("readonly", "true");
        txtGrossSale.Attributes.Add("readonly", "true");
        txtGST.Attributes.Add("readonly", "true");
        txtGST.Attributes.Add("readonly", "true");
        txtDiscount.Attributes.Add("readonly", "true");
        txtNetSale.Attributes.Add("readonly", "true");
        txtCreditCardSale.Attributes.Add("readonly", "true");
        txtCashInHand.Attributes.Add("readonly", "true");
        txtCashSubmitted.Attributes.Add("readonly", "true");
        txtDifference.Attributes.Add("readonly", "true");
        txtSkimming.Attributes.Add("readonly", "true");
    }
    private void LoadDISTRIBUTOR()
    {
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.GetDistributorWithMaxDayClose(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()), 1);
            clsWebFormUtil.FillDxComboBoxList(ddDistributorId, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");

            if (dt.Rows.Count > 0)
            {
                ddDistributorId.SelectedIndex = 0;
            }
            Session.Add("dtLocationInfo", dt);
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void LoadUser()
    {
        ddUser.Enabled = true;
        if (ddDistributorId.Items.Count > 0)
        {
            Distributor_UserController du = new Distributor_UserController();
            DataTable dt = du.SelectDistributorUser(7, int.Parse(ddDistributorId.Value.ToString()), int.Parse(Session["CompanyId"].ToString()));
            Session.Add("User", dt);
            clsWebFormUtil.FillDxComboBoxList(ddUser, dt, 0, 1, true);
            if (dt.Rows.Count > 0)
            {
                ddUser.SelectedIndex = 0;
            }

            Session.Add("ShiftTable", _SController.SelectShift(Convert.ToInt32(ddDistributorId.Value.ToString()), 12));
            DataRow[] dr = dt.Select("USER_ID = '" + Convert.ToInt32(Session["UserID"]) + "' ");
            if (dr.Length > 0)
            {
                ddUser.Enabled = false;
                ddUser.Value = Session["UserID"].ToString();
            }
            ddUser_SelectedIndexChanged(null, null);
        }
    }
    protected void ddDistributorId_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUser();
    }
    protected void ddUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtGrossSale.Text = "0.00";
        txtDiscount.Text = "0.00";
        txtNetSale.Text = "0.00";
        txtCreditCardSale.Text = "0.00";
        txtCashInHand.Text = "0.00";
        txtDifference.Text = "0";
        txtGST.Text = "0.00";
        txtSkimming.Text = "0.00";
        if (ddUser.Items.Count > 0)
        {
            Session["ShiftTable"] = _SController.SelectShift(Convert.ToInt32(ddDistributorId.Value.ToString()), 12);
            DataTable dtShift = (DataTable)Session["ShiftTable"];
            DataRow[] dr;
            dr = dtShift.Select("USER_ID = '" + Convert.ToInt32(ddUser.Value.ToString()) + "'");
            txtShift.Text = dr[0].ItemArray[3].ToString();
            Session["ShiftId"] = Convert.ToInt32(dr[0].ItemArray[2].ToString());
            LoadSales();
        }
    }
    public void LoadSales()
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow row in dtLocationInfo.Rows)
        {
            if (row["DISTRIBUTOR_ID"].ToString() == ddDistributorId.Value.ToString())
            {
                if (row["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(row["MaxDayClose"]);
                    break;
                }
            }
        }
        ClearFields();
        DataTable dt = _SController.SelectSales(Convert.ToInt32(ddDistributorId.Value.ToString()), Convert.ToInt32(ddUser.Value.ToString()), CurrentWorkDate, Constants.DateNullValue, 3, Convert.ToInt32(Session["ShiftId"]));
        DataTable dtsKIM = _SController.SelectSales(Convert.ToInt32(ddDistributorId.Value.ToString()), Convert.ToInt32(ddUser.Value.ToString()), Constants.DateNullValue, Constants.DateNullValue, 9, Constants.IntNullValue);
        GetOpeningCash();
        GetShiftOpeningAmount();
        try
        {
            if (dt.Rows[0].ItemArray[0] != DBNull.Value)
            {
                txtGrossSale.Text = Math.Round(Convert.ToDecimal(dt.Rows[0].ItemArray[0]), 2).ToString();
                txtDiscount.Text = Math.Round(Convert.ToDecimal(dt.Rows[0].ItemArray[3]), 2).ToString();
                txtNetSale.Text = Math.Round(Convert.ToDecimal(dt.Rows[0].ItemArray[4]), 2).ToString();
                txtCreditCardSale.Text = Math.Round(Convert.ToDecimal(dt.Rows[0].ItemArray[7]), 2).ToString();
                txtSkimming.Text = Math.Round(Convert.ToDecimal(dtsKIM.Rows[0].ItemArray[0]), 2).ToString();
                txtCashInHand.Text = (Math.Round(Convert.ToDecimal(dt.Rows[0].ItemArray[6]), 2) + DataControl.chkDecimalZero(txtOpeningRecieved.Text) + DataControl.chkDecimalZero(txtOpeningCash.Text) - DataControl.chkDecimalZero(txtSkimming.Text)).ToString();
                txtGST.Text = Math.Round(Convert.ToDecimal(dt.Rows[0].ItemArray[1]), 2).ToString();
            }
        }
        catch (Exception)
        {
            txtCashInHand.Text = (DataControl.chkDecimalZero(txtOpeningRecieved.Text) + DataControl.chkDecimalZero(txtOpeningCash.Text)).ToString();
        }
    }
    public void GetOpeningCash()
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow row in dtLocationInfo.Rows)
        {
            if (row["DISTRIBUTOR_ID"].ToString() == ddDistributorId.Value.ToString())
            {
                if (row["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(row["MaxDayClose"]);
                    break;
                }
            }
        }

        string fromDate = CurrentWorkDate.ToString("dd-MMM-yyyy");
        string ToDate = CurrentWorkDate.ToString("dd-MMM-yyyy");
        DateTime _startDate = DateTime.Parse(fromDate + " 00:00:00");
        DateTime _endDate = DateTime.Parse(ToDate + " 23:59:59");
        DataTable dt2 = _SController.SelectSales(Constants.IntNullValue, Convert.ToInt32(ddUser.Value.ToString()), _startDate.AddDays(-1), _endDate.AddDays(-1), 5, Constants.IntNullValue);
        if (dt2.Rows.Count > 0)
        {
            txtOpeningCash.Text = Math.Round(Convert.ToDecimal(dt2.Rows[0].ItemArray[0]), 2).ToString();
        }
        else
        {
            txtOpeningCash.Text = "0";
        }
    }
    public void GetShiftOpeningAmount()
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow row in dtLocationInfo.Rows)
        {
            if (row["DISTRIBUTOR_ID"].ToString() == ddDistributorId.Value.ToString())
            {
                if (row["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(row["MaxDayClose"]);
                    break;
                }
            }
        }

        string fromDate = CurrentWorkDate.ToString("dd-MMM-yyyy");
        string ToDate = CurrentWorkDate.ToString("dd-MMM-yyyy");
        DateTime _startDate = DateTime.Parse(fromDate + " 00:00:00");
        DateTime _endDate = DateTime.Parse(ToDate + " 23:59:59");
        DataTable dt2 = _SController.SelectSales(Constants.IntNullValue, Convert.ToInt32(ddUser.Value.ToString()), _startDate, _endDate, 51, Constants.IntNullValue);
        if (dt2.Rows.Count > 0)
        {
            txtOpeningRecieved.Text = Math.Round(Convert.ToDecimal(dt2.Rows[0].ItemArray[0]), 2).ToString();
        }
        else
        {
            txtOpeningCash.Text = "0";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblerror.Text = "";
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow row in dtLocationInfo.Rows)
        {
            if (row["DISTRIBUTOR_ID"].ToString() == ddDistributorId.Value.ToString())
            {
                if (row["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(row["MaxDayClose"]);
                    break;
                }
            }
        }
        if (CurrentWorkDate != Constants.DateNullValue)
        {
            try
            {
                if (txtTotal.Text == "0" || txtTotal.Text == "")
                {
                    lblerror.Text = "Please enter denomination";
                    return;
                }
                GetCurrencyQuantity();
                ShiftController mController = new ShiftController();
                DataTable dtClosedShifts = mController.SelectSales(Convert.ToInt32(ddDistributorId.Value.ToString()), Convert.ToInt32(ddUser.Value.ToString()), CurrentWorkDate, Constants.DateNullValue, 7, Convert.ToInt32(Session["ShiftId"]));
                if (dtClosedShifts.Rows.Count > 0)
                {
                    lblerror.Text = "Shift is already closed";
                    return;
                }
                bool _result = mController.InsertShiftClose(Convert.ToInt32(ddDistributorId.Value.ToString()), Convert.ToInt32(Session["ShiftId"].ToString()), Convert.ToInt32(ddUser.Value.ToString()), System.DateTime.Now, Cur5000, Cur1000, Cur500, Cur100, Cur50, Cur20, Cur10, Cur5, Cur2, Cur1, Convert.ToDecimal(txtTotal.Text), DataControl.chkDecimalNull(txtOpeningCash.Text), Convert.ToDecimal(txtDifference.Text), CurrentWorkDate);
                if (_result)
                {
                    ClearFields();
                    LoadSales();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "ErrorMessage", "ErrorMessages()", true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Dayclose not found for selected location!')", true);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow row in dtLocationInfo.Rows)
        {
            if (row["DISTRIBUTOR_ID"].ToString() == ddDistributorId.Value.ToString())
            {
                if (row["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(row["MaxDayClose"]);
                    break;
                }
            }
        }

        CrpShiftClose CrpReport = new CrpShiftClose();
        CrpReport.Refresh();
        CrpReport.SetParameterValue("LOCATION", ddDistributorId.SelectedItem.ToString());
        CrpReport.SetParameterValue("FROM_DATE", CurrentWorkDate.ToString());
        CrpReport.SetParameterValue("UserName", ddUser.SelectedItem.ToString());
        CrpReport.SetParameterValue("Shift", txtShift.Text.ToString());
        CrpReport.SetParameterValue("OpeningCashRecieved", txtOpeningRecieved.Text.ToString());
        CrpReport.SetParameterValue("OpeningBalance", txtOpeningCash.Text.ToString());
        CrpReport.SetParameterValue("GrossSale", txtGrossSale.Text.ToString());
        CrpReport.SetParameterValue("Discount", txtDiscount.Text.ToString());
        CrpReport.SetParameterValue("SalesRefund", txtGST.Text.ToString());
        CrpReport.SetParameterValue("NetSales", txtNetSale.Text.ToString());
        CrpReport.SetParameterValue("CreditCardSale", txtCreditCardSale.Text.ToString());
        CrpReport.SetParameterValue("CashInHand", txtCashInHand.Text.ToString());
        CrpReport.SetParameterValue("CashSubmitted", txtCashSubmitted.Text.ToString());
        CrpReport.SetParameterValue("Difference", txtDifference.Text.ToString());
        CrpReport.SetParameterValue("5000", Convert.ToInt32(DataControl.chkNull_Zero(txt5000.Text.ToString())));
        CrpReport.SetParameterValue("1000", Convert.ToInt32(DataControl.chkNull_Zero(txt1000.Text.ToString())));
        CrpReport.SetParameterValue("500", Convert.ToInt32(DataControl.chkNull_Zero(txt500.Text.ToString())));
        CrpReport.SetParameterValue("100", Convert.ToInt32(DataControl.chkNull_Zero(txt100.Text.ToString())));
        CrpReport.SetParameterValue("50", Convert.ToInt32(DataControl.chkNull_Zero(txt50.Text.ToString())));
        CrpReport.SetParameterValue("20", Convert.ToInt32(DataControl.chkNull_Zero(txt20.Text.ToString())));
        CrpReport.SetParameterValue("10", Convert.ToInt32(DataControl.chkNull_Zero(txt10.Text.ToString())));
        CrpReport.SetParameterValue("5", Convert.ToInt32(DataControl.chkNull_Zero(txt5.Text.ToString())));
        CrpReport.SetParameterValue("2", Convert.ToInt32(DataControl.chkNull_Zero(txt2.Text.ToString())));
        CrpReport.SetParameterValue("1", Convert.ToInt32(DataControl.chkNull_Zero(txt1.Text.ToString())));
        CrpReport.SetParameterValue("Denomination", txtTotal.Text.ToString());
        CrpReport.SetParameterValue("CashSkimming", txtSkimming.Text.ToString());

        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);


        //FormReport _formReport = new FormReport();
        //_formReport.crvReportViewer.ReportSource = CrpReport;
        //_formReport.ShowDialog();
    }
    public void ClearFields()
    {
        txtGrossSale.Text = "0";
        txtDiscount.Text = "0";
        txtNetSale.Text = "0";
        txtCreditCardSale.Text = "0";
        txtOpeningRecieved.Text = "0";
        txtDifference.Text = "0";
        txtGST.Text = "0.00";
        txtSkimming.Text = "0.00";
        Session["ShiftId"] = 0;
    }
    #region Calculations
    public void CalculateTotalDenomination()
    {
        try
        {
            txtTotal.Text = (Convert.ToInt32(txtCal5000.Text) + Convert.ToInt32(txtCal1000.Text) + Convert.ToInt32(txtCal500.Text) + Convert.ToInt32(txtCal100.Text) + Convert.ToInt32(txtCal50.Text) + Convert.ToInt32(txtCal20.Text) + Convert.ToInt32(txtCal10.Text) + Convert.ToInt32(txtCal5.Text) + Convert.ToInt32(txtCal2.Text) + Convert.ToInt32(txtCal1.Text)).ToString();
            txtCashSubmitted.Text = txtTotal.Text;
            CalculateDifference();
        }
        catch (Exception)
        {
        }
    }
    public void CalculateDifference()
    {

        if (txtCashSubmitted.Text != "")
        {
            //_diffenrence = Convert.ToDecimal(txtNetSale.Text) - Convert.ToDecimal(txtCashInHand.Text);
            //_diffenrence = _diffenrence + (Convert.ToDecimal(txtCashInHand.Text) - Convert.ToDecimal(txtCashSubmitted.Text));
            //txtDifference.Text = _diffenrence.ToString();
            txtDifference.Text = (Convert.ToDecimal(DataControl.chkDecimalNull(txtCashInHand.Text)) - Convert.ToDecimal(DataControl.chkDecimalNull(txtCashSubmitted.Text))).ToString();
        }
    }
    public void GetCurrencyQuantity()
    {
        if (txt5000.Text != "")
        {
            Cur5000 = Convert.ToInt32(txt5000.Text);
        }
        if (txt1000.Text != "")
        {
            Cur1000 = Convert.ToInt32(txt1000.Text);
        }
        if (txt500.Text != "")
        {
            Cur500 = Convert.ToInt32(txt500.Text);
        }
        if (txt100.Text != "")
        {
            Cur100 = Convert.ToInt32(txt100.Text);
        }
        if (txt50.Text != "")
        {
            Cur50 = Convert.ToInt32(txt50.Text);
        }
        if (txt20.Text != "")
        {
            Cur20 = Convert.ToInt32(txt20.Text);
        }
        if (txt10.Text != "")
        {
            Cur10 = Convert.ToInt32(txt10.Text);
        }
        if (txt5.Text != "")
        {
            Cur5 = Convert.ToInt32(txt5.Text);
        }
        if (txt2.Text != "")
        {
            Cur2 = Convert.ToInt32(txt2.Text);
        }
        if (txt1.Text != "")
        {
            Cur1 = Convert.ToInt32(txt1.Text);
        }
    }
    #endregion
}
