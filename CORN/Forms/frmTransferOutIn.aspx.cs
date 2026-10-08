using System;
using System.Data;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;


public partial class Forms_frmTransferOutIn : System.Web.UI.Page
{
    DataTable _PurchaseSKUS;
    private void CreateTable()
    {
        _PurchaseSKUS = new DataTable();
        _PurchaseSKUS.Columns.Add("SKU_ID", typeof(int));
        _PurchaseSKUS.Columns.Add("SKU_Code", typeof(string));
        _PurchaseSKUS.Columns.Add("SKU_Name", typeof(string));
        _PurchaseSKUS.Columns.Add("Quantity", typeof(decimal));
        _PurchaseSKUS.Columns.Add("UOM_ID", typeof(int));
        _PurchaseSKUS.Columns.Add("S_UOM_ID", typeof(int));
        _PurchaseSKUS.Columns.Add("S_Quantity", typeof(decimal));
        this.Session.Add("PurchaseSKUS", _PurchaseSKUS);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.GetDocumentNo();
            this.LoadToDistributor();

            this.LoadDocumentDetail();
            // CreateTable();
        }
    }
    private void GetDocumentNo()
    {
        try
        {
            
            PurchaseController mPurchase = new PurchaseController();

            DataTable dt = mPurchase.SelectPurchaseDocumentNo(Constants.Document_Transfer_Out, Constants.IntNullValue, Constants.LongNullValue, int.Parse(this.Session["UserId"].ToString()), 0, Convert.ToInt32(drpDistributor.Value));

            clsWebFormUtil.FillDxComboBoxList(drpDocumentNo, dt, 0, 0,true);

            if (dt.Rows.Count > 0)
            {
                drpDocumentNo.SelectedIndex = 0;
            }

        }
        catch (Exception EX)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' Error:   " + EX.Message.ToString() + " ');", true);
        }
    }
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadDocumentDetail();
    }
    private void LoadDocumentDetail()
    {
        if (drpDocumentNo.Items.Count > 0)
        {
            PurchaseController mPurchase = new PurchaseController();
            DataTable dt = mPurchase.SelectPurchaseDocumentNo(Constants.IntNullValue, Constants.IntNullValue, long.Parse(drpDocumentNo.Value.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            if (dt.Rows.Count > 0)
            {
                txtDocumentNo.Text = dt.Rows[0]["ORDER_NUMBER"].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
                //drpPrincipal.SelectedValue = dt.Rows[0]["PRINCIPAL_ID"].ToString();
                DrpTransferFor.Value = dt.Rows[0]["SOLD_FROM"].ToString();
                DataTable PurchaseSKUS = mPurchase.SelectPurchaseDetail(Constants.IntNullValue, long.Parse(dt.Rows[0][0].ToString()));
                GrdPurchase.DataSource = PurchaseSKUS;
                GrdPurchase.DataBind();
                this.Session.Add("PurchaseSKUS", PurchaseSKUS);
            }
        }
    }
    private void LoadDistributor()
    {
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.GetDistributorWithMaxDayClose(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()), 1);
            clsWebFormUtil.FillDxComboBoxList(drpDistributor, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");

            if (dt.Rows.Count > 0)
            {
                drpDistributor.SelectedIndex = 0;
            }
            Session.Add("dtLocationInfo", dt);
        }
        catch (Exception EX)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' Error:   " + EX.Message.ToString() + " ');", true);
        }
    }
    private void LoadToDistributor()
    {
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDxComboBoxList(DrpTransferFor, dt, 0, 2, true);
            if (dt.Rows.Count > 0)
            {
                DrpTransferFor.SelectedIndex = 0;
            }
        }
        catch (Exception EX)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' Error:   " + EX.Message.ToString() + " ');", true);
        }
    }
    public DataTable GetCOAConfiguration()
    {
        try
        {
            COAMappingController _cController = new COAMappingController();
            DataTable dt = _cController.SelectCOAConfiguration(5, Constants.ShortNullValue, Constants.LongNullValue, "Level 3");
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

    private bool GetFinanceConfig()
    {
        try
        {
            ConfigurationController _cController = new ConfigurationController();
            DataTable dt = _cController.SelectAppConfiguration(1, (int)Enums.AppSettingMaster.FinanceSetting, null, Constants.IntNullValue);

            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("CODE = '" + (int)Enums.AppSetting.IsFinanceIntegrate + "'");
                if (dr.Length > 0)
                {
                    return Convert.ToInt32(dr[0][2]) == 1 ? true : false;
                }
            }
            return false;
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Error in Financial Setting!');", true);
            throw;
        }
    }

    protected void btnTransferIn_Click(object sender, EventArgs e)
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.Value.ToString())
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }
        if (CurrentWorkDate != Constants.DateNullValue)
        {
            PurchaseController mController = new PurchaseController();
            DataTable dtPurchaseDetail = (DataTable)this.Session["PurchaseSKUS"];
            decimal mTotalAmount = 0;

            foreach (DataRow dr in dtPurchaseDetail.Rows)
            {
                mTotalAmount += decimal.Parse(dr["AMOUNT"].ToString());

            }
            DataTable dtConfig = GetCOAConfiguration();
            bool IsFinanceSetting = GetFinanceConfig();
            bool mResult = mController.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedItem.Value.ToString()), txtDocumentNo.Text, Constants.Document_Transfer_In,
                   CurrentWorkDate, int.Parse(drpDistributor.SelectedItem.Value.ToString()), int.Parse(DrpTransferFor.SelectedItem.Value.ToString())
                  , mTotalAmount, false, dtPurchaseDetail, 1, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpDocumentNo.SelectedItem.Value.ToString()), dtConfig, IsFinanceSetting);

            if (mResult == true)
            {
                mController.PostPendingDocument(int.Parse(drpDocumentNo.SelectedItem.Value.ToString()), Constants.Document_Transfer_Out, int.Parse(DrpTransferFor.SelectedItem.Value.ToString()), 1, 1);

                GrdPurchase.DataSource = null;
                GrdPurchase.DataBind();

                Session.Remove("PurchaseSKUS");
                GetDocumentNo();
                LoadDocumentDetail();
                txtBuiltyNo.Text = "";
                txtDocumentNo.Text = "";
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Dayclose not found for selected location!')", true);
        }
    }
    private void LoadGird()
    {
        try
        {
            _PurchaseSKUS = (DataTable)this.Session["PurchaseSKUS"];
            GrdPurchase.DataSource = _PurchaseSKUS;
            GrdPurchase.DataBind();
        }
        catch (Exception EX)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' Error:   " + EX.Message.ToString() + " ');", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //  Respo
    }
}