using System;
using System.IO;
using System.Data;
using DevExpress.DashboardCommon;

public partial class Forms_frmDashBoard2 : System.Web.UI.Page
{

    DataSet objDsMenu = null, objDsDashboard = null;
    DataSource objDataSource = null;
    DashboardSqlDataSource objSQLDataSource = null;
    Int32 intMenuCode=0;

    String[] strDashboardProcName;
    String[] strDashboardNameInfo;

    protected void Page_Load(object sender, EventArgs e)
    {
        intMenuCode =Convert.ToInt32(Request.QueryString["LevelID"].ToString());

        GetDashboard();
    }

    protected void dshViewer_DashboardLoading(object sender, DevExpress.DashboardWeb.DashboardLoadingEventArgs e)
    {
        try
        {
            // Checks the identifier of the required dashboard.
            //if (e.DashboardId == "FinalSaleReview")
            //{
            //    // Writes the dashboard XML definition from a file to a string.
            //    string definitionPath = Server.MapPath("App_Data/Sale.xml");
            //    string dashboardDefinition = File.ReadAllText(definitionPath);

            //    // Specifies the dashboard XML definition.
            //    e.DashboardXml = dashboardDefinition;
            //}

            if (objDsMenu.Tables[0].Rows[0]["vbrFile"] != null && !String.IsNullOrEmpty(Convert.ToString(objDsMenu.Tables[0].Rows[0]["vbrFile"])))
            {
                using (Stream ms = new MemoryStream((byte[])objDsMenu.Tables[0].Rows[0]["vbrFile"]))
                {
                    ms.Seek(0L, SeekOrigin.Begin);
                    using (StreamReader streamReader = new StreamReader(ms))
                        e.DashboardXml = streamReader.ReadToEnd();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void dshViewer_DataLoading(object sender, DevExpress.DashboardWeb.DataLoadingWebEventArgs e)
    {
        try
        {
            e.Data = objDsDashboard.Tables[e.DataSourceName];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetDashboard()
    {
        try
        {
            Session.Add("dtFrom", DateTime.Now.Date);
            Session.Add("dtTo", DateTime.Now.Date);

            objDsMenu = CornPointHelper.DashboardClass.GetDashboardMenuInfo(intMenuCode);

            objDsDashboard = CornPointHelper.DashboardClass.GetDashboardDataSet(objDsMenu.Tables[0].Rows[0]["strDashboardMainProcName"].ToString(), Convert.ToDateTime(Session["dtFrom"]), Convert.ToDateTime(Session["dtTo"]));

            strDashboardProcName = objDsMenu.Tables[0].Rows[0]["strDashboardProcName"].ToString().Split(',');
            strDashboardNameInfo = objDsMenu.Tables[0].Rows[0]["strDashboardNameInfo"].ToString().Split(',');

            //objDsDashboard = CornPointHelper.DashboardClass.GetDashboardDataSet(strDashboardProcName[0]);
            dshViewer.DashboardId = strDashboardNameInfo[0];

            for (Int32 intCount = 0; intCount < objDsDashboard.Tables.Count; intCount++)
            {
                objDsDashboard.Tables[intCount].TableName = strDashboardNameInfo[intCount];
                //objDataSource = new DataSource(strDashboardNameInfo[intCount], objDsDashboard.Tables[intCount]);
                //objSQLDataSource = new DashboardSqlDataSource(strDashboardNameInfo[intCount], objDsDashboard.Tables[intCount]);
            }

            dshViewer.AllowExportDashboard = true;
            dshViewer.AllowExportDashboardItems = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}