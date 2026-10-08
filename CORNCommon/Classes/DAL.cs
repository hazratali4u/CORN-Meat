using System;
using System.Data;
using System.Data.SqlClient;

namespace CORNCommon.Classes
{
    public static class DAL
    {
        public static DataRow GetParamInfo(String strConnectionstring, Int32 intMenuCode)
        {
            SqlConnection objSqlConn = null;
            SqlCommand objSqlCmd = null;
            SqlDataAdapter objSqlDA = null;
            DataTable dtblParamInfo = null;
            try
            {
                objSqlConn = new SqlConnection(strConnectionstring);


                objSqlCmd = new SqlCommand(@"  SELECT 
                                                tblModuleParamInfo.sintFinancialYearCode ,
                                                tblModuleParamInfo.dtFrom ,
                                                tblModuleParamInfo.dtTo ,
                                                tblModuleParamInfo.strCode ,
                                                tblModuleParamInfo.bolIsProcOrVw ,
                                                tblModuleParamInfo.strEntityName ,
                                                tblModuleParamInfo.strProcName ,
                                                tblModuleParamInfo.strVwName ,
                                                tblModuleParamInfo.strInSelection ,
                                                tblModuleParamInfo.strTopRecords ,
                                                tblModuleParamInfo.strColNameWithAlias ,
                                                tblModuleParamInfo.strColNameWithoutAlias ,
                                                tblModuleParamInfo.strInnerColBaseOrderBy ,
                                                tblModuleParamInfo.strOuterAliasBaseOrderBy ,
                                                tblModuleParamInfo.strInnerColBaseGroupBy ,
                                                tblModuleParamInfo.strDefaultInnerColBaseWhereClause,
                                                tblModuleParamInfo.strOuterColBaseGroupBy ,
                                                tblModuleParamInfo.strInnerColBaseWhereClause ,
                                                tblModuleParamInfo.strOuterAliasBaseWhereClause ,
                                                tblModuleParamInfo.strInnerColBaseHavingClause ,
                                                tblModuleParamInfo.strOuterAliasBaseHavingClause ,
                                                tblModuleParamInfo.intPageIndex ,
                                                tblModuleParamInfo.intPageSize ,
                                                MODULE.WhereClause,
                                                MODULE.ColsToHide, 
                                                MODULE.ColsToFixed,
                                                MODULE.ColsToDate,
                                                MODULE.ColsToTime,
                                                MODULE.ColsToDateTime,
                                                MODULE.PKArraryNameCol,  
                                                CAST (NULL AS INT) AS intCompanyGroupCode, -- as distributor Id
                                                CAST(NULL AS INT) AS intCompanyCode ,   -- as user id
                                                CAST(NULL AS INT) AS intBasePKCode 
                                                FROM MODULE
                                                INNER JOIN tblModuleParamInfo ON MODULE.MODULE_ID = tblModuleParamInfo.MODULE_ID
                                                WHERE MODULE.MODULE_ID =" + intMenuCode);


                objSqlCmd.Connection = objSqlConn;
                objSqlDA = new SqlDataAdapter(objSqlCmd);
                dtblParamInfo = new DataTable();
                objSqlDA.Fill(dtblParamInfo);
                objSqlConn.Close();

                return dtblParamInfo.Rows[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSqlConn != null)
                    objSqlConn.Dispose();
                if (objSqlCmd != null)
                    objSqlCmd.Dispose();
                if (objSqlDA != null)
                    objSqlDA.Dispose();
            }
        }

        public static DataSet GetData(String strConnectionString, DataRow drParamInfo)
        {
            SqlConnection objSqlConn = null;
            SqlCommand objSqlCmd = null;
            SqlDataAdapter objSqlDA = null;
            DataSet dsLookUp = null;
            try
            {
                objSqlConn = new SqlConnection(strConnectionString);

                objSqlCmd = new SqlCommand();
                objSqlCmd.CommandType = CommandType.StoredProcedure;
                objSqlCmd.Connection = objSqlConn;
                objSqlCmd.CommandTimeout = 0;
                objSqlCmd.CommandText = "procForLookup";

                objSqlCmd.Parameters.AddWithValue("@sintFinancialYearCode", drParamInfo["sintFinancialYearCode"]);
                objSqlCmd.Parameters.AddWithValue("@dtFrom", drParamInfo["dtFrom"]);
                objSqlCmd.Parameters.AddWithValue("@dtTo", drParamInfo["dtTo"]);
                objSqlCmd.Parameters.AddWithValue("@intCompanyGroupCode", drParamInfo["intCompanyGroupCode"]);
                objSqlCmd.Parameters.AddWithValue("@intCompanyCode", drParamInfo["intCompanyCode"]);
                objSqlCmd.Parameters.AddWithValue("@strCode", drParamInfo["strCode"]);
                objSqlCmd.Parameters.AddWithValue("@strInSelection", drParamInfo["strInSelection"]);
                objSqlCmd.Parameters.AddWithValue("@strTopRecords", drParamInfo["strTopRecords"]);
                objSqlCmd.Parameters.AddWithValue("@strColNameWithAlias", drParamInfo["strColNameWithAlias"]);
                objSqlCmd.Parameters.AddWithValue("@strColNameWithoutAlias", drParamInfo["strColNameWithoutAlias"]);
                objSqlCmd.Parameters.AddWithValue("@strInnerColBaseOrderBy", drParamInfo["strInnerColBaseOrderBy"]);
                objSqlCmd.Parameters.AddWithValue("@strOuterAliasBaseOrderBy", drParamInfo["strOuterAliasBaseOrderBy"]);
                objSqlCmd.Parameters.AddWithValue("@strInnerColBaseGroupBy", drParamInfo["strInnerColBaseGroupBy"]);
                objSqlCmd.Parameters.AddWithValue("@strOuterColBaseGroupBy", drParamInfo["strOuterColBaseGroupBy"]);

                if (drParamInfo["strInnerColBaseWhereClause"].ToString().Length > 0 && drParamInfo["strDefaultInnerColBaseWhereClause"].ToString().Length > 0)
                    objSqlCmd.Parameters.AddWithValue("@strInnerColBaseWhereClause", drParamInfo["strInnerColBaseWhereClause"] + " AND " + drParamInfo["strDefaultInnerColBaseWhereClause"]);
                else if (drParamInfo["strInnerColBaseWhereClause"].ToString().Length > 0 && drParamInfo["strDefaultInnerColBaseWhereClause"].ToString().Length == 0)
                    objSqlCmd.Parameters.AddWithValue("@strInnerColBaseWhereClause", drParamInfo["strInnerColBaseWhereClause"]);
                if (drParamInfo["strInnerColBaseWhereClause"].ToString().Length == 0 && drParamInfo["strDefaultInnerColBaseWhereClause"].ToString().Length > 0)
                    objSqlCmd.Parameters.AddWithValue("@strInnerColBaseWhereClause", " AND " + drParamInfo["strDefaultInnerColBaseWhereClause"]);

                //objSqlCmd.Parameters.AddWithValue("@strInnerColBaseWhereClause", drParamInfo["strInnerColBaseWhereClause"]);
                objSqlCmd.Parameters.AddWithValue("@strOuterAliasBaseWhereClause", drParamInfo["strOuterAliasBaseWhereClause"]);
                objSqlCmd.Parameters.AddWithValue("@strInnerColBaseHavingClause", drParamInfo["strInnerColBaseHavingClause"]);
                objSqlCmd.Parameters.AddWithValue("@strOuterAliasBaseHavingClause", drParamInfo["strOuterAliasBaseHavingClause"]);
                objSqlCmd.Parameters.AddWithValue("@intPageIndex", drParamInfo["intPageIndex"]);
                objSqlCmd.Parameters.AddWithValue("@intPageSize", drParamInfo["intPageSize"]);
                objSqlCmd.Parameters.AddWithValue("@strProcName", drParamInfo["strProcName"]);

                dsLookUp = new DataSet();
                objSqlDA = new SqlDataAdapter(objSqlCmd);
                objSqlDA.Fill(dsLookUp);
                objSqlConn.Close();
                //drParamInfo["intPageIndex"] = Int32.Parse(drParamInfo["intPageIndex"].ToString()) + 1;
                return dsLookUp;
                //grdLookup.DataSource = dsLookUp;
                //grdLookup.DisplayLayout.Bands[0].Columns["RowIndex"].Hidden = true;
                //intTotalRecords = Int32.Parse(dsLookUp.Tables[1].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSqlConn != null)
                    objSqlConn.Dispose();
                if (objSqlCmd != null)
                    objSqlCmd.Dispose();
                if (objSqlDA != null)
                    objSqlDA.Dispose();
            }
        }

        public static DataSet GetData(String strConnectionString, DataSet dsLookUp, DataRow drParamInfo)
        {
            SqlConnection objSqlConn = null;
            SqlCommand objSqlCmd = null;
            SqlDataAdapter objSqlDA = null;
            try
            {
                objSqlConn = new SqlConnection(strConnectionString);

                objSqlCmd = new SqlCommand();
                objSqlCmd.CommandType = CommandType.StoredProcedure;
                objSqlCmd.Connection = objSqlConn;
                objSqlCmd.CommandTimeout = 0;
                objSqlCmd.CommandText = "procForLookup";

                objSqlCmd.Parameters.AddWithValue("@sintFinancialYearCode", drParamInfo["sintFinancialYearCode"]);
                objSqlCmd.Parameters.AddWithValue("@dtFrom", drParamInfo["dtFrom"]);
                objSqlCmd.Parameters.AddWithValue("@dtTo", drParamInfo["dtTo"]);
                objSqlCmd.Parameters.AddWithValue("@intCompanyGroupCode", drParamInfo["intCompanyGroupCode"]);
                objSqlCmd.Parameters.AddWithValue("@intCompanyCode", drParamInfo["intCompanyCode"]);
                objSqlCmd.Parameters.AddWithValue("@strCode", drParamInfo["strCode"]);
                objSqlCmd.Parameters.AddWithValue("@strInSelection", drParamInfo["strInSelection"]);
                objSqlCmd.Parameters.AddWithValue("@strTopRecords", drParamInfo["strTopRecords"]);
                objSqlCmd.Parameters.AddWithValue("@strColNameWithAlias", drParamInfo["strColNameWithAlias"]);
                objSqlCmd.Parameters.AddWithValue("@strColNameWithoutAlias", drParamInfo["strColNameWithoutAlias"]);
                objSqlCmd.Parameters.AddWithValue("@strInnerColBaseOrderBy", drParamInfo["strInnerColBaseOrderBy"]);
                objSqlCmd.Parameters.AddWithValue("@strOuterAliasBaseOrderBy", drParamInfo["strOuterAliasBaseOrderBy"]);
                objSqlCmd.Parameters.AddWithValue("@strInnerColBaseGroupBy", drParamInfo["strInnerColBaseGroupBy"]);
                objSqlCmd.Parameters.AddWithValue("@strOuterColBaseGroupBy", drParamInfo["strOuterColBaseGroupBy"]);

                if (drParamInfo["strInnerColBaseWhereClause"].ToString().Length > 0 && drParamInfo["strDefaultInnerColBaseWhereClause"].ToString().Length > 0)
                    objSqlCmd.Parameters.AddWithValue("@strInnerColBaseWhereClause", drParamInfo["strInnerColBaseWhereClause"] + " AND " + drParamInfo["strDefaultInnerColBaseWhereClause"]);
                else if (drParamInfo["strInnerColBaseWhereClause"].ToString().Length > 0 && drParamInfo["strDefaultInnerColBaseWhereClause"].ToString().Length == 0)
                    objSqlCmd.Parameters.AddWithValue("@strInnerColBaseWhereClause", drParamInfo["strInnerColBaseWhereClause"]);
                if (drParamInfo["strInnerColBaseWhereClause"].ToString().Length == 0 && drParamInfo["strDefaultInnerColBaseWhereClause"].ToString().Length > 0)
                    objSqlCmd.Parameters.AddWithValue("@strInnerColBaseWhereClause", " AND " + drParamInfo["strDefaultInnerColBaseWhereClause"]);

                //objSqlCmd.Parameters.AddWithValue("@strInnerColBaseWhereClause", drParamInfo["strInnerColBaseWhereClause"]);
                objSqlCmd.Parameters.AddWithValue("@strOuterAliasBaseWhereClause", drParamInfo["strOuterAliasBaseWhereClause"]);
                objSqlCmd.Parameters.AddWithValue("@strInnerColBaseHavingClause", drParamInfo["strInnerColBaseHavingClause"]);
                objSqlCmd.Parameters.AddWithValue("@strOuterAliasBaseHavingClause", drParamInfo["strOuterAliasBaseHavingClause"]);
                objSqlCmd.Parameters.AddWithValue("@intPageIndex", drParamInfo["intPageIndex"]);
                objSqlCmd.Parameters.AddWithValue("@intPageSize", drParamInfo["intPageSize"]);
                objSqlCmd.Parameters.AddWithValue("@strProcName", drParamInfo["strProcName"]);

                objSqlDA = new SqlDataAdapter(objSqlCmd);
                objSqlDA.Fill(dsLookUp);
                objSqlConn.Close();
                //drParamInfo["intPageIndex"] = Int32.Parse(drParamInfo["intPageIndex"].ToString()) + 1;
                return dsLookUp;
                //grdLookup.DataSource = dsLookUp;
                //grdLookup.DisplayLayout.Bands[0].Columns["RowIndex"].Hidden = true;
                //
                //intTotalRecords = Int32.Parse(dsLookUp.Tables[1].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSqlConn != null)
                    objSqlConn.Dispose();
                if (objSqlCmd != null)
                    objSqlCmd.Dispose();
                if (objSqlDA != null)
                    objSqlDA.Dispose();
            }
        }

        public static DataTable GetFilterFiels(Int32 intMenuCode, String strConnectionString)
        {
            SqlConnection objSqlConn = null;
            SqlCommand objSqlCmd = null;
            SqlDataAdapter objSqlDA = null;
            String strCommand = String.Empty;
            DataTable dsFields = null;
            try
            {

                objSqlConn = new SqlConnection(strConnectionString);

                objSqlCmd = new SqlCommand();
                objSqlCmd.CommandType = CommandType.Text;
                objSqlCmd.Connection = objSqlConn;
                objSqlCmd.CommandTimeout = 0;

                strCommand = @"         SELECT DISTINCT tblMenuColInfo.strTableOrFunctionName,tblMenuColInfo.strColName,tblMenuColInfo.strColProcOrVwName,tblMenuColInfo.strColumnValue,
                                        tblMenuColInfo.strColAliasName,bolIsDefaultParam,
                                        tblDataType.strCSharp,tblDataType.strSQL

                                        FROM tblMenuColInfo

                                        INNER JOIN tblDataType ON tblMenuColInfo.intDataTypeCode = tblDataType.intCode
                                        AND tblDataType.tintRecordStatusCode NOT IN (0, 11, 12)

                                        INNER JOIN tblMenu ON tblMenuColInfo.intMenuCode = tblMenu.intCode
                                        AND tblMenuColInfo.tintRecordStatusCode NOT IN (0, 11, 12)

                                        WHERE tblMenu.intCode = " + intMenuCode.ToString() + " AND ISNULL(tblMenuColInfo.bolIsDefault,0) = 0 AND tblMenu.tintRecordStatusCode NOT IN (0, 11, 12) ";

                objSqlCmd.CommandText = strCommand;
                objSqlDA = new SqlDataAdapter(objSqlCmd);
                dsFields = new DataTable();
                objSqlDA.Fill(dsFields);
                objSqlConn.Close();
                return dsFields;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSqlConn != null)
                    objSqlConn.Dispose();
                if (objSqlCmd != null)
                    objSqlCmd.Dispose();
                if (objSqlDA != null)
                    objSqlDA.Dispose();
            }
        }

        public static DataTable FiscalYearList(Int32 intFiscalYearCode, Int32 intBasePKCode, String strConnectionString)
        {
            SqlConnection objSqlConn = null;
            SqlCommand objSqlCmd = null;
            SqlDataAdapter objSqlDA = null;
            String strCommand = String.Empty;
            DataTable dsFields = null;
            try
            {

                objSqlConn = new SqlConnection(strConnectionString);

                objSqlCmd = new SqlCommand();
                objSqlCmd.CommandType = CommandType.Text;
                objSqlCmd.Connection = objSqlConn;
                objSqlCmd.CommandTimeout = 0;

                strCommand = @"         SELECT tblFinancialYear.intCode [Code],
                                        tblFinancialYear.strYearName [Name],
                                        CAST(tblFinancialYear.dtStart AS DATE) [Start],
                                        CAST(tblFinancialYear.dtEnd AS DATE) [End],
                                        tblFinancialYear.bolIsOpen [Is Open],
                                        tblFinancialYear.bolIsActive [Is Active] 
                                        FROM tblFinancialYear
                                        WHERE tblFinancialYear.tintRecordStatusCode <> dbo.fnDeleteMode()
                                        AND tblFinancialYear.intCode = " + intFiscalYearCode +
                                        " AND tblFinancialYear.intBasePKCode = dbo.fnGetCompanyCode(" + intBasePKCode.ToString() + ")";

                objSqlCmd.CommandText = strCommand;
                objSqlDA = new SqlDataAdapter(objSqlCmd);
                dsFields = new DataTable();
                objSqlDA.Fill(dsFields);
                objSqlConn.Close();
                return dsFields;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSqlConn != null)
                    objSqlConn.Dispose();
                if (objSqlCmd != null)
                    objSqlCmd.Dispose();
                if (objSqlDA != null)
                    objSqlDA.Dispose();
            }
        }

        public static DataTable FiscalYearList(Int32 intBasePKCode, String strConnectionString)
        {
            SqlConnection objSqlConn = null;
            SqlCommand objSqlCmd = null;
            SqlDataAdapter objSqlDA = null;
            String strCommand = String.Empty;
            DataTable dsFields = null;
            try
            {

                objSqlConn = new SqlConnection(strConnectionString);

                objSqlCmd = new SqlCommand();
                objSqlCmd.CommandType = CommandType.Text;
                objSqlCmd.Connection = objSqlConn;
                objSqlCmd.CommandTimeout = 0;

                strCommand = @"         SELECT tblFinancialYear.intCode [Code],
                                        tblFinancialYear.strYearName [Name],
                                        CAST(tblFinancialYear.dtStart AS DATE) [Start],
                                        CAST(tblFinancialYear.dtEnd AS DATE) [End],
                                        tblFinancialYear.bolIsOpen [Is Open],
                                        tblFinancialYear.bolIsActive [Is Active] 
                                        FROM tblFinancialYear
                                        WHERE tblFinancialYear.tintRecordStatusCode <> dbo.fnDeleteMode()
                                        AND tblFinancialYear.intBasePKCode = dbo.fnGetCompanyCode(" + intBasePKCode.ToString() + ")";

                objSqlCmd.CommandText = strCommand;
                objSqlDA = new SqlDataAdapter(objSqlCmd);
                dsFields = new DataTable();
                objSqlDA.Fill(dsFields);
                objSqlConn.Close();
                return dsFields;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSqlConn != null)
                    objSqlConn.Dispose();
                if (objSqlCmd != null)
                    objSqlCmd.Dispose();
                if (objSqlDA != null)
                    objSqlDA.Dispose();
            }
        }

        public static DataTable GetReportParamInfo(Int32 intMenuCode, String strConnectionString)
        {
            SqlConnection objSqlConn = null;
            SqlCommand objSqlCmd = null;
            SqlDataAdapter objSqlDA = null;
            String strCommand = String.Empty;
            DataTable dsFields = null;
            try
            {

                objSqlConn = new SqlConnection(strConnectionString);

                objSqlCmd = new SqlCommand();
                objSqlCmd.CommandType = CommandType.Text;
                objSqlCmd.Connection = objSqlConn;
                objSqlCmd.CommandTimeout = 0;

                strCommand = @"         SELECT  tblReportInfo.intNoOfPrint ,
                                        tblReportInfo.bolIsAutoPrint ,
                                        tblReportInfo.bolIsCrReport ,
                                        tblReportInfo.strCrReportName ,
                                        tblReportInfo.strCrSubReportNameInfo ,
                                        tblReportInfo.strCrReportCaption ,
                                        tblReportInfo.strCrReportPath ,
                                        tblReportInfo.strSQLReportName ,
                                        tblReportInfo.strSQLSubReportNameInfo ,
                                        tblReportInfo.strSQLReportCaption ,
                                        tblReportInfo.strSQLReportPath ,
                                        tblReportInfo.strReportParam ,
                                        tblReportInfo.strDefaultWhereClause ,
                                        tblReportInfo.strDefaultSubWhereClause ,
                                        tblReportInfo.strReportSpecialText ,
                                        tblMenuParamInfo.intFinancialYearCode ,
                                        tblMenuParamInfo.dtFrom ,
                                        tblMenuParamInfo.dtTo ,
                                        CAST(NULL AS INT) AS intCompanyGroupCode,
                                        CAST(NULL AS INT) AS intCompanyCode ,
                                        CAST(NULL AS INT) AS intBasePKCode ,
                                        tblMenuParamInfo.intLocationSetupCode ,
                                        tblMenuParamInfo.strCode ,
                                        tblMenuParamInfo.bolIsProcOrVw ,
                                        tblMenuParamInfo.strEntityName ,
                                        tblMenuParamInfo.strProcName ,
                                        tblMenuParamInfo.strVwName ,
                                        tblMenuParamInfo.strInSelection ,
                                        tblMenuParamInfo.strTopRecords ,
                                        tblMenuParamInfo.strColNameWithAlias ,
                                        tblMenuParamInfo.strColNameWithoutAlias ,
                                        tblMenuParamInfo.strInnerColBaseOrderBy ,
                                        tblMenuParamInfo.strOuterAliasBaseOrderBy ,
                                        tblMenuParamInfo.strInnerColBaseGroupBy ,
                                        tblMenuParamInfo.strOuterColBaseGroupBy ,
                                        tblMenuParamInfo.strDefaultInnerColBaseWhereClause ,
                                        tblMenuParamInfo.strInnerColBaseWhereClause ,
                                        tblMenuParamInfo.strOuterAliasBaseWhereClause ,
                                        tblMenuParamInfo.strInnerColBaseHavingClause ,
                                        tblMenuParamInfo.strOuterAliasBaseHavingClause ,
                                        tblMenuParamInfo.intPageIndex ,
                                        tblMenuParamInfo.intPageSize 

                                        FROM tblMenu

                                        INNER JOIN tblMenuParamInfo ON tblMenu.intReportLookupParamInfoCode = tblMenuParamInfo.intCode
                                        AND tblMenuParamInfo.tintRecordStatusCode NOT IN (0, 11, 12)

                                        INNER JOIN tblReportInfo ON tblMenu.intReportInfoCode = tblReportInfo.intCode
                                        AND tblReportInfo.tintRecordStatusCode NOT IN (0, 11, 12)
                                    
                                        WHERE tblMenu.intCode = " + intMenuCode.ToString() +
                                        " AND tblMenu.tintRecordStatusCode NOT IN (0, 11, 12) ";


                objSqlCmd.CommandText = strCommand;
                objSqlDA = new SqlDataAdapter(objSqlCmd);
                dsFields = new DataTable();
                objSqlDA.Fill(dsFields);
                objSqlConn.Close();
                return dsFields;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSqlConn != null)
                    objSqlConn.Dispose();
                if (objSqlCmd != null)
                    objSqlCmd.Dispose();
                if (objSqlDA != null)
                    objSqlDA.Dispose();
            }
        }

        public static DataTable GetColumnData(String strVwProcName, String strColName, Int32 IntEmployeeCode, Int32 intCompanyGroupCode, Int32 intCompanyCode, Int32 intLocationSetupCode, Int32 intBasePKCode, Int32 intFinancialYearCode, String strWhereClause, String strConnectionString, DateTime? dtFrom, DateTime? dtTo)
        {
            SqlConnection objSqlConnTemp = null;
            DataSet objDtbl = null;
            SqlDataAdapter objSqlDataAdapter = null;
            SqlCommand objSqlCmd = null;
            String strDefaultClause = String.Empty;

            try
            {
                objSqlConnTemp = new SqlConnection(strConnectionString);
                objSqlConnTemp.Open();


                objSqlCmd = new SqlCommand();
                objSqlCmd.CommandType = CommandType.StoredProcedure;
                objSqlCmd.CommandText = strVwProcName;

                //objSqlCmd.CommandTimeout = 0;

                objSqlCmd.Connection = objSqlConnTemp;
                objSqlCmd.CommandTimeout = 0;

                objSqlCmd.Parameters.AddWithValue("@strTopRecords", "50");
                objSqlCmd.Parameters.AddWithValue("@strColNameWithAlias", strColName);
                objSqlCmd.Parameters.AddWithValue("@intCompanyCode", intCompanyCode);
                objSqlCmd.Parameters.AddWithValue("@intCompanyGroupCode", IntEmployeeCode);
                objSqlCmd.Parameters.AddWithValue("@intLocationSetupCode", intLocationSetupCode);
                objSqlCmd.Parameters.AddWithValue("@intBasePKCode", intBasePKCode);
                objSqlCmd.Parameters.AddWithValue("@intFinancialYearCode", intFinancialYearCode);
                if (dtFrom != null)
                    objSqlCmd.Parameters.AddWithValue("@dtFrom", dtFrom);
                if (dtTo != null)
                    objSqlCmd.Parameters.AddWithValue("@dtTo", dtTo);


                //strDefaultClause = " AND " + trReportsMenu.SelectedNodes[0].Cells["TableName"].Value.ToString() + ".intProjectCode =" + GlobalDefinition.G_intProjectCode.ToString() + " AND " + trReportsMenu.SelectedNodes[0].Cells["TableName"].Value.ToString() + ".intCompanyCode = " + GlobalDefinition.G_intCompanyCode.ToString();
                //already set from calling function
                //strDefaultClause = " AND " + strTableName + ".intProjectCode =" + GlobalDefinition.G_intProjectCode.ToString() + " AND " + strTableName + ".intCompanyCode = " + GlobalDefinition.G_intCompanyCode.ToString();


                objSqlCmd.Parameters.AddWithValue("@strInnerColBaseWhereClause ", strWhereClause);

                objDtbl = new DataSet();
                objSqlDataAdapter = new SqlDataAdapter(objSqlCmd);// new SqlDataAdapter(strQuery + " " + strWhereClause + " " + strOrderBy + " ", objSqlConnTemp);
                objSqlDataAdapter.Fill(objDtbl);

                objSqlConnTemp.Close();
                objSqlConnTemp = null;

                return objDtbl.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSqlCmd != null)
                {
                    objSqlCmd.Dispose();
                    objSqlCmd = null;
                }
                if (objSqlDataAdapter != null)
                {
                    objSqlDataAdapter.Dispose();
                    objSqlDataAdapter = null;
                }
                if (objSqlConnTemp != null)
                {
                    objSqlConnTemp.Dispose();
                    objSqlConnTemp = null;
                }
            }

            return objDtbl.Tables[0];
        }

        public static DataSet GetDefaultFilterFiels(Int32 intMenuCode, String strConnectionString)
        {
            SqlConnection objSqlConn = null;
            SqlCommand objSqlCmd = null;
            SqlDataAdapter objSqlDA = null;
            String strCommand = String.Empty;
            DataSet dsFields = null;
            try
            {

                objSqlConn = new SqlConnection(strConnectionString);

                objSqlCmd = new SqlCommand();
                objSqlCmd.CommandType = CommandType.Text;
                objSqlCmd.Connection = objSqlConn;
                objSqlCmd.CommandTimeout = 0;

                strCommand = @"         SELECT tblMenuColInfo.strTableOrFunctionName AS [Table Name],tblMenuColInfo.strColName AS [Column Name], 
                                        tblMenuColInfo.strColAliasName AS [Display Name],
                                        tblMenuColInfo.strColProcOrVwName AS [Report Param],tblMenuColInfo.strColumnValue AS [Default Value]

                                        FROM tblMenuColInfo

                                        INNER JOIN tblMenu ON tblMenuColInfo.intMenuCode = tblMenu.intCode
                                        AND tblMenuColInfo.tintRecordStatusCode NOT IN (0, 11, 12)

                                        WHERE tblMenu.intCode = " + intMenuCode.ToString() + " AND ISNULL(tblMenuColInfo.bolIsDefault,0) = 1 AND tblMenu.tintRecordStatusCode NOT IN (0, 11, 12) ";

                objSqlCmd.CommandText = strCommand;
                objSqlDA = new SqlDataAdapter(objSqlCmd);
                dsFields = new DataSet();
                objSqlDA.Fill(dsFields);
                objSqlConn.Close();
                return dsFields;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSqlConn != null)
                    objSqlConn.Dispose();
                if (objSqlCmd != null)
                    objSqlCmd.Dispose();
                if (objSqlDA != null)
                    objSqlDA.Dispose();
            }
        }
    }
}

































