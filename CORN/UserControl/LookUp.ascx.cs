using CORNBusinessLayer.Classes;
using DevExpress.Web;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Collections.Generic;
using DevExpress.XtraPrinting;
using System.Web.UI.WebControls;

public partial class UserControl_LookUp : System.Web.UI.UserControl, ILookUp
{
    public delegate void CompanyHander(object sender, EventArgs e);
    public event CompanyHander CompanyEvent;

    public DataRow[] drRowToEdit;
    public Dictionary<int, string> ListOfSelectedRows;
    Boolean bolIsWMSLookUp;
    LookupController objController;
    private DataRow selectedDataRow;
    public DataRow SelectedDataRow
    {
        get { return selectedDataRow; }
    }
    public void InitComponent(Int32 intMenuCode, String strConnectionString)
    {
        DataRow dr = null;
        bolIsWMSLookUp = false;
        objController = new LookupController(this, intMenuCode, Convert.ToInt32(Session["UserID"]), Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
        Session["objController"] = objController;
    }
    public void fillLookupGrid(Int32 intMenuCode, Int32 intCompanyGroupCode, Int32 intCompanyCode, Int32 intBasePKCode, Int32 intLocationPoolCode, String strWhereClause)
    {
        //DataRow dr = GeneralQuery.GetReportParamInfo("", intMenuCode);
        //dr["dtFrom"] = Session["dtpFrom"];
        //dr["dtTO"] = Session["dtpTo"];
        //DataSet dsReport = GetData(dr, intCompanyGroupCode, intCompanyCode, intBasePKCode, intLocationPoolCode, strWhereClause);
        //bolIsWMSLookUp = true;
        //grdLookup.DataSource = dsReport;
        //grdLookup.DataBind();
        //Session["dsGrdLookup"] = grdLookup.DataSource;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            InitComponent(Convert.ToInt32(Request.QueryString["LevelID"]), null);
            //ArrangeGrid("tblLocationPool.strName-Name", "~/Forms/frmLocationPool.aspx?intCode={0}");
            if (!bolIsWMSLookUp)
                ArrangeGrid();
        }
        if ((DataSet)Session["dsGrdLookup"] != null)
        {
            SetData();
            if (chkGroup.Checked == false)
            { UnGroupColumns(); }
        }
    }

    #region Properties
    public int DataRowIndex { get; set; }
    public object gridDataSource
    {
        get
        {
            return (DataSet)Session["dsGrdLookup"];
        }
        set
        {

            grdLookup.DataSource = (DataSet)value;
            grdLookup.DataBind();
            grdLookup.Columns.Remove(grdLookup.Columns["Select"]);
            Session["dsGrdLookup"] = grdLookup.DataSource;
        }
    }
    public string KeyFieldValue
    {
        get
        {
            return (string)Session["keyFieldValue"];
        }
        set
        {

            grdLookup.KeyFieldName = (string)value;
            Session["keyFieldValue"] = grdLookup.KeyFieldName;
        }
    }
    public string setRecordInfoStaus
    {
        set
        {
            Session["txtInfo"] = value;
            txtInfo.Text = value;
        }
        get
        {
            if (Session["txtInfo"] == null)
                return txtInfo.Text;
            return (string)Session["txtInfo"];
        }
    }
    public string GetFilterCriteria
    {
        get
        {
            return CheckCriteria();
        }
    }

    public int IntEmployeeCode
    {
        get
        {
            return 0;
        }

        set
        {

        }
    }

    public string StrCustomFilter
    {
        get
        {
            return "";
        }

        set
        {

        }
    }
    #endregion

    #region Checkbox Events
    protected void chkFilter_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkFilter.Checked == true)
            {
                SetFilter(true);
                if (chkFilter2.Checked == false)
                {
                    chkFilter2.Checked = true;
                }
            }
            else
            {
                SetFilter(false);
                if (chkFilter2.Checked == true)
                {
                    chkFilter2.Checked = false;
                }
            }
            //SetData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void chkGroup_CheckedChanged1(object sender, EventArgs e)
    {
        try
        {
            if (chkGroup.Checked == true)
            {
                SetGropBar(true);
                if (chkGroup2.Checked == false)
                {
                    chkGroup2.Checked = true;
                }

            }
            else
            {
                SetGropBar(false);
                if (chkGroup2.Checked == true)
                {
                    chkGroup2.Checked = false;
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void chkFilter2_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkFilter2.Checked == true)
            {
                SetFilter(true);
                if (chkFilter.Checked == false)
                {
                    chkFilter.Checked = true;
                }
            }
            else
            {
                SetFilter(false);
                if (chkFilter.Checked == true)
                {
                    chkFilter.Checked = false;
                }
            }
            //SetData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void chkGroup2_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkGroup2.Checked == true)
            {
                SetGropBar(true);
                if (chkGroup.Checked == false)
                {
                    chkGroup.Checked = true;
                }

            }
            else
            {
                SetGropBar(false);
                if (chkGroup.Checked == true)
                {
                    chkGroup.Checked = false;
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Button Events
    protected void btnNext_Click1(object sender, EventArgs e)
    {
        try
        {
            objController = (LookupController)Session["objController"];
            objController.GetNextRecords();
            SetData();
            enableDisableRecBtn();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnLast_Click1(object sender, EventArgs e)
    {
        try
        {
            objController = (LookupController)Session["objController"];
            objController.GetAllRemainingRecords();
            SetData();
            enableDisableRecBtn();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    #endregion

    #region Radio Buttons Events
    protected void rbtnPDF_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnPDF.Checked == true)
        {
            if (rbtnPDF2.Checked == false)
            {
                rbtnPDF2.Checked = true;
                rbtnExcel2.Checked = false;
            }
        }
        else
        {
            if (rbtnPDF2.Checked == true)
            {
                rbtnPDF2.Checked = false;
                rbtnExcel2.Checked = true;
            }
        }

    }
    protected void rbtnPDF2_CheckedChanged(object sender, EventArgs e)
    {

        if (rbtnPDF2.Checked == true)
        {
            if (rbtnPDF.Checked == false)
            {
                rbtnPDF.Checked = true;
                rbtnExcel.Checked = false;
            }
        }
        else
        {
            if (rbtnPDF.Checked == true)
            {
                rbtnPDF.Checked = false;
                rbtnExcel.Checked = true;
            }
        }

    }
    #endregion

    #region Grid Events & Methods
    public void HideColumn(String strColsToHide)
    {
        try
        {
            String[] strColumn = strColsToHide.Split(',');
            for (Int32 intCount = 0; intCount < strColumn.Length; intCount++)
                grdLookup.Columns[strColumn[intCount]].Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void UnGroupColumns()
    {
        try
        {
            for (Int32 intCount = 0; intCount < grdLookup.Columns.Count; intCount++)
            {
                grdLookup.UnGroup(grdLookup.Columns[intCount]);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdLookup_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
    {


        string WhereCondition = "";

        if (e.Criteria != null)
        {
            if (!String.IsNullOrEmpty(e.Value))
            {
                //WhereCondition = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetMsSqlWhere(e.Criteria);
                WhereCondition = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(e.Criteria);

                //Repeat:
                //if (!String.IsNullOrEmpty(WhereCondition))
                //    WhereCondition = WhereCondition.Replace(WhereCondition.Substring(WhereCondition.IndexOf('-'), WhereCondition.IndexOf('"', WhereCondition.IndexOf('-')) - WhereCondition.IndexOf('-')), "");

                //if (WhereCondition.IndexOf('-') > 0)
                //    goto Repeat;



                WhereCondition = WhereCondition.Replace("\"", "");
                WhereCondition = WhereCondition.Replace("(", "");
                WhereCondition = WhereCondition.Replace(")", "");
                Session["WhereCondition"] = WhereCondition;
            }
        }

        if (String.IsNullOrEmpty(grdLookup.FilterExpression))
        {
            Session["WhereCondition"] = WhereCondition;
        }



        // SetData();

    }
    protected void grdLookup_CustomColumnGroup(object sender, CustomColumnSortEventArgs e)
    {
        SetData();
    }
    protected void grdLookup_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (Session["WhereCondition"] != null)
        {
            if (Session["objController"] != null)
            {
                objController = (LookupController)Session["objController"];
                objController.GetFilteredData();
                SetData();
            }
        }


    }
    protected void grdLookup_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        try
        {
            e.Cell.Style.Clear();
            e.Cell.ForeColor = System.Drawing.Color.Black;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region ILookUp Members
    public String CheckCriteria()
    {
        String strCriteria;
        String strParameterCriteria;
        try
        {

            strCriteria = String.Empty;
            strParameterCriteria = String.Empty;

            if (Session["WhereCondition"] != null)
            {
                strCriteria = (string)Session["WhereCondition"];

            }
            return strCriteria;
        }
        catch (Exception ex)
        {
            //  MessageBox.Show("Error : " + ex.Message);
            return String.Empty;
        }
    }
    public void SetGridColomnCaption()
    {
        try
        {
            if (grdLookup.DataSource != null)
            {
                DataSet dsGrd = (DataSet)grdLookup.DataSource;

                for (Int32 intCount = 0; intCount < grdLookup.Columns.Count; intCount++)
                {
                    if (dsGrd.Tables[0].Columns[intCount].ColumnName != "RowIndex")
                    {
                        grdLookup.Columns[intCount].Caption = dsGrd.Tables[0].Columns[intCount].ColumnName.Substring(dsGrd.Tables[0].Columns[intCount].ColumnName.IndexOf("-") + 1);
                        grdLookup.Columns[intCount].Name = dsGrd.Tables[0].Columns[intCount].ColumnName.Remove(dsGrd.Tables[0].Columns[intCount].ColumnName.IndexOf("-"));
                        grdLookup.Columns[intCount].Width = 100;
                    }
                }
            }

            grdLookup.Columns["RowIndex"].Caption = "S.No";

            if (grdLookup.Columns.Count > 0)
            {
                foreach (GridViewColumn Col in grdLookup.Columns)
                {
                    if ((Col.ToString().Split(' ').Count() == 2 && Col.ToString().Contains("Code")) || (Col.ToString().Split(' ').Count() == 2 && Col.ToString().Contains("Record Status")))
                        grdLookup.Columns[Col.ToString()].Visible = false;
                }
            }

            if (chkGroup.Checked == false)
                UnGroupColumns();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Methods
    public void Export()
    {
        try
        {
            //grdLookup.DataSource = (DataSet)Session["dsGrdLookup"];

            if (rbtnPDF.Checked == true && rbtnPDF2.Checked == true)
            {
                //gridExport.GridView = gv;
                //gv.DataSource = dsGrdLookup;
                bool saveAs = true;
                GridExporter.WritePdfToResponse("abc", saveAs);
            }
            else
            {
                GridExporter.WriteCsvToResponse();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public void ExportData()
    {
        try
        {
            if (rbtnPDF.Checked == true && rbtnPDF2.Checked == true)
            {
                GridExporter.DataBind();
                GridExporter.WritePdfToResponse();
            }
            else
            {
                GridExporter.DataBind();
                GridExporter.WriteXlsToResponse();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    public Dictionary<int, string> GetSelectedRowData()
    {
        ListOfSelectedRows = new Dictionary<int, string>();
        string status;
        List<object> SelectedRows = grdLookup.GetSelectedFieldValues(grdLookup.KeyFieldName);
        foreach (object items in SelectedRows)
        {
            status = (string)grdLookup.GetRowValuesByKeyValue(items, "Status");

            ListOfSelectedRows.Add(Convert.ToInt32(items.ToString()), status);
        }
        return ListOfSelectedRows;
    }
    public void PrintPreview()
    {
        try
        {
            // this.grdLookup.PrintPreview(RowPropertyCategories.Hidden);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void SetData()
    {
        grdLookup.KeyFieldName = (string)Session["keyFieldValue"];
        grdLookup.DataSource = (DataSet)Session["dsGrdLookup"];
        grdLookup.DataBind();

        if (Session["txtInfo"] != null)
        {
            string infoText = (string)Session["txtInfo"];
            txtInfo.Text = infoText;
            txtInfo2.Text = infoText;
        }
    }
    public void SetFilter(bool Isfilter)
    {
        if (Isfilter)
        {
            grdLookup.Settings.ShowFilterRow = true;
            grdLookup.FilterEnabled = true;
        }
        else
        {
            grdLookup.Settings.ShowFilterRow = false;
            grdLookup.FilterEnabled = false;
            grdLookup.FilterExpression = "";
            SetData();
        }

    }
    public void SetGropBar(bool IsGroup)
    {
        if (IsGroup)
        {
            grdLookup.Settings.ShowGroupPanel = true;
        }
        else
        {
            UnGroupColumns();
            grdLookup.Settings.ShowGroupPanel = false;
        }

    }
    public void enableDisableRecBtn()
    {
        String infoText = (string)Session["txtInfo"];
        if (infoText != null)
        {
            infoText = infoText.Replace("Records ", "");

            string curRec = infoText.Substring(0, infoText.IndexOf(" "));

            infoText = infoText.Replace(curRec + " of ", "");
            string allRec = infoText;
            if (Convert.ToInt32(curRec) == Convert.ToInt32(allRec))
            {
                btnNext.Enabled = false;
                btnNext2.Enabled = false;
                btnLast.Enabled = false;
                btnLast2.Enabled = false;
            }
            else
            {
                btnNext.Enabled = true;
                btnNext2.Enabled = true;
                btnLast.Enabled = true;
                btnLast2.Enabled = true;
            }
        }
    }
    public void ArrangeGrid()
    {

        //GridViewDataHyperLinkColumn colLink = new GridViewDataHyperLinkColumn();
        //colLink.FieldName = "USER_ID";
        //colLink.Name = "USER_ID";
        //colLink.PropertiesHyperLinkEdit.TextField = "Text Field";
        //colLink.Caption = "Select";
        //colLink.Visible = true;
        //colLink.VisibleIndex = 0;
        //colLink.Index = 0;

        ////colLink.PropertiesHyperLinkEdit.NavigateUrlFormatString = navUrl;

        //if (objController.StrWebPageName.Contains("?"))
        //    colLink.PropertiesHyperLinkEdit.NavigateUrlFormatString = objController.StrURLPath + objController.StrWebPageName.Substring(0, objController.StrWebPageName.IndexOf("?")) + "?" + objController.StrWebPageParam + "={0}&intMenuCode=" + objController.IntMenuCode; // + "&strMenuName=" + objLookupInfo.MenuName;
        //else
        //    colLink.PropertiesHyperLinkEdit.NavigateUrlFormatString = objController.StrURLPath + objController.StrWebPageName + "?" + objController.StrPKColName.Split('-')[0].Split('.')[1].ToString() + "={0}&intMenuCode=" + objController.IntMenuCode; // +"&strMenuName=" + objLookupInfo.MenuName; 

        //colLink.CellStyle.HorizontalAlign = HorizontalAlign.Left;
        //colLink.CellStyle.ForeColor = System.Drawing.Color.Black;
        //colLink.Settings.FilterMode = ColumnFilterMode.DisplayText;
        //colLink.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
        //colLink.Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False;
        //colLink.Settings.ShowFilterRowMenu = DevExpress.Utils.DefaultBoolean.False;
        //colLink.Settings.ShowInFilterControl = DevExpress.Utils.DefaultBoolean.False;
        //colLink.Settings.AllowSort = DevExpress.Utils.DefaultBoolean.False;
        //colLink.Settings.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
        //grdLookup.Columns.Insert(0, colLink);

        ////grdLookup.Columns["Country Code"].Visible = false;
        ////grdLookup.Columns["Select"].CellStyle.ForeColor = System.Drawing.Color.Black;
    }

    #endregion

    #region Call Backs
    protected void ClBckPnlInfoText_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (Session["txtInfo"] != null)
        {
            txtInfo.Text = (string)Session["txtInfo"];
            txtInfo2.Text = (string)Session["txtInfo"];
        }
    }
    protected void clbkPnlbtnNext_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        enableDisableRecBtn();
    }
    protected void clbkPnlbtnLast_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        enableDisableRecBtn();
    }
    protected void grdLookup_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        try
        {
            object masterKeyValue = grdLookup.GetRowValues(Convert.ToInt32(e.Parameters), KeyFieldValue);
            DataTable dt = ((DataSet)Session["dsGrdLookup"]).Tables[0];
            drRowToEdit = dt.Select(KeyFieldValue + " = " + masterKeyValue.ToString());
            this.Page.GetType().InvokeMember("EditRequestCall", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }

    }
    #endregion

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //Export();
        if (rbtnPDF.Checked && rbtnPDF2.Checked)
        {
            GridExporter.WritePdfToResponse();
        }
        else
        {
            GridExporter.WriteXlsToResponse();
        }
    }

    #region Useless
    public void SetGridRowColor()
    {

    }

    public void SetGridColomnFormat(string strColsToDate, string strColsToTime, string strColsToDateTime)
    {

    }

    public void HideAndFixedColumn(string strColsToHide, string strColsToFixed)
    {

    }

    public void SetStatusForNevigationButtons(int intRecCount, int lngTotalRecord)
    {

    }
    #endregion
}