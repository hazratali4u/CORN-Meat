using System;
using CrystalDecisions.CrystalReports.Engine;

using CORNCommon.Classes;
using CrystalDecisions.Shared;

public partial class Forms_Default : System.Web.UI.Page
{
    ReportDocument CrpReport = null;
    System.IO.Stream oStream = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        CrpReport = (ReportDocument)this.Session["CrpReport"];
        //System.IO.Stream oStream = null;
        byte[] byteArray = null;
        try
        {
            //ExportFormatType formatType = ExportFormatType.NoFormat;
            //switch (rbFormat.SelectedItem.Value)
            //{
            //    case "Word":
            //        formatType = ExportFormatType.WordForWindows;
            //        break;
            //    case "PDF":
            //        formatType = ExportFormatType.PortableDocFormat;
            //        break;
            //    case "Excel":
            //        formatType = ExportFormatType.Excel;
            //        break;
            //    case "CSV":
            //        formatType = ExportFormatType.CharacterSeparatedValues;
            //        break;
            //}
            //CrpReport.ExportToHttpResponse(formatType, Response, true, "Crystal");
            //Response.End();

            if (int.Parse(this.Session["ReportType"].ToString()) == 0)
            {
                oStream = (System.IO.Stream)CrpReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(byteArray);
                Response.End();
            }
            else
            {
                string path = Configuration.GetAppInstallationPath() + "\\Exported.xls";
                CrpReport.SetDatabaseLogon("sa", "123");
                CrpReport.ExportToDisk(ExportFormatType.Excel, path);
                System.IO.FileInfo file = new System.IO.FileInfo(path);

                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
                else
                {
                    Response.Write("This file does not exist.");
                }
            }
            CrpReport.Close();
            CrpReport.Dispose();
            this.Session.Remove("CrpReport");
            oStream.Close();
            oStream.Dispose();
            GC.Collect();
       
        }
        catch (Exception exp)
        {
            ExceptionPublisher.PublishException(exp);

        }
        finally
        {
            CrpReport.Close();
            CrpReport.Dispose();
            this.Session.Remove("CrpReport");
            oStream.Flush();
            oStream.Close();
            oStream.Dispose();
            GC.Collect();
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        try
        {
            if (this.CrpReport != null)
            {
                CrpReport.Close();
                CrpReport.Dispose();
                Session.Remove("CrpReport");
            }
            if (this.oStream != null)
            {
                oStream.Flush();
                oStream.Close();
                oStream.Dispose();
                GC.Collect();
            }
        }
        catch (Exception exp)
        {
            ExceptionPublisher.PublishException(exp);
        }
    }
}