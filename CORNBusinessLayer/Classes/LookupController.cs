using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;
using System;
using System.Data;

namespace CORNBusinessLayer.Classes
{
    public class LookupController
    {
        DataRow dtrParamInfo;
        DataRow dtrParamInfoInitil;
        private int intTotalRecords;
        private int intPageSize;
        private int intMenuCode;
        private int intFinancialYearCode;
        private int intBasePkCode;
        ILookUp objLookUp;
        private string StrConnectionString;

        public LookupController()
        {
        }

        public LookupController(ILookUp objCtrlLookUp, int IntMenuCode,int UserId, int DistributorId)
        {
            try
            {
                objLookUp = objCtrlLookUp;
                intMenuCode = IntMenuCode;
                StrConnectionString = Configuration.ConnectionString;
                dtrParamInfo = DAL.GetParamInfo(StrConnectionString, IntMenuCode);
                objLookUp.KeyFieldValue = dtrParamInfo["PKArraryNameCol"].ToString();
                dtrParamInfo["intCompanyGroupCode"] = DistributorId;
                dtrParamInfo["intCompanyCode"] = UserId;
                objLookUp.gridDataSource = DAL.GetData(StrConnectionString, dtrParamInfo);
                objLookUp.HideColumn(dtrParamInfo["ColsToHide"].ToString());
                objLookUp.setRecordInfoStaus = "Records " + ((DataSet)objLookUp.gridDataSource).Tables[0].Rows.Count + " of " + ((DataSet)objLookUp.gridDataSource).Tables[1].Rows[0][0].ToString();
                objLookUp.SetStatusForNevigationButtons(Convert.ToInt32(((DataSet)objLookUp.gridDataSource).Tables[0].Rows.Count), Convert.ToInt32(((DataSet)objLookUp.gridDataSource).Tables[1].Rows[0][0]));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetNextRecords()
        {
            DataSet dsLookUp = null;
            try
            {
                intPageSize = Convert.ToInt32(dtrParamInfo["intPageSize"]);
                dsLookUp = (DataSet)objLookUp.gridDataSource;

                if (dsLookUp.Tables.Count == 2)
                    if (Int32.Parse(dsLookUp.Tables[1].Rows[0][0].ToString()) == dsLookUp.Tables[0].Rows.Count)
                        return;
                intTotalRecords = Int32.Parse(dsLookUp.Tables[1].Rows[0][0].ToString());
                dtrParamInfo["intPageIndex"] = dsLookUp.Tables[0].Rows.Count;

                if ((dsLookUp.Tables[0].Rows.Count + intPageSize) <= intTotalRecords)
                    dtrParamInfo["intPageSize"] = dsLookUp.Tables[0].Rows.Count + intPageSize;
                else
                    dtrParamInfo["intPageSize"] = intTotalRecords;

                objLookUp.gridDataSource = DAL.GetData(StrConnectionString, dsLookUp, dtrParamInfo);
                //objLookUp.SetGridColomnCaption();
                //objLookUp.SetGridRowColor();

                //objLookUp.SetGridColomnFormat(dtrParamInfo["strColsToDate"].ToString(), dtrParamInfo["strColsToTime"].ToString(), dtrParamInfo["strColsToDateTime"].ToString());
                //objLookUp.HideAndFixedColumn(dtrParamInfo["strColsToHide"].ToString(), dtrParamInfo["strColsToFixed"].ToString());

                objLookUp.setRecordInfoStaus = "Records " + dsLookUp.Tables[0].Rows.Count + " of " + dsLookUp.Tables[1].Rows[0][0].ToString();
                objLookUp.SetStatusForNevigationButtons(Convert.ToInt32(((DataSet)objLookUp.gridDataSource).Tables[0].Rows.Count), Convert.ToInt32(((DataSet)objLookUp.gridDataSource).Tables[1].Rows[0][0]));
            }
            catch (Exception ex)
            {

                //ZEASuite.LogManager.ErrorLogging.objErrorLogging.LogError("LookupController of " + this.intMenuCode, "GetNextRecords", ex, false, false);
                throw ex;
            }
        }

        public void GetAllRemainingRecords()
        {
            DataSet dsLookUp = null;
            try
            {
                dsLookUp = (DataSet)objLookUp.gridDataSource;

                if (dsLookUp.Tables.Count == 2)
                    if (dsLookUp.Tables.Count == 2)
                        if (Int32.Parse(dsLookUp.Tables[1].Rows[0][0].ToString()) == dsLookUp.Tables[0].Rows.Count)
                            return;

                intTotalRecords = Int32.Parse(dsLookUp.Tables[1].Rows[0][0].ToString());
                dtrParamInfo["intPageIndex"] = dsLookUp.Tables[0].Rows.Count;
                dtrParamInfo["intPageSize"] = intTotalRecords;

                objLookUp.gridDataSource = DAL.GetData(StrConnectionString, (DataSet)objLookUp.gridDataSource, dtrParamInfo);
                //objLookUp.SetGridColomnCaption();
                //objLookUp.SetGridRowColor();

                //objLookUp.SetGridColomnFormat(dtrParamInfo["strColsToDate"].ToString(), dtrParamInfo["strColsToTime"].ToString(), dtrParamInfo["strColsToDateTime"].ToString());
                //objLookUp.HideAndFixedColumn(dtrParamInfo["strColsToHide"].ToString(), dtrParamInfo["strColsToFixed"].ToString());

                objLookUp.setRecordInfoStaus = "Records " + dsLookUp.Tables[0].Rows.Count + " of " + ((DataSet)objLookUp.gridDataSource).Tables[1].Rows[0][0].ToString();
                objLookUp.SetStatusForNevigationButtons(Convert.ToInt32(((DataSet)objLookUp.gridDataSource).Tables[0].Rows.Count), Convert.ToInt32(((DataSet)objLookUp.gridDataSource).Tables[1].Rows[0][0]));
            }
            catch (Exception ex)
            {
                // ZEASuite.LogManager.ErrorLogging.objErrorLogging.LogError("LookupController of " + this.intMenuCode, "GetAllRemainingRecords", ex, false, false);
                throw ex;
            }

        }

        public void GetFilteredData()
        {
            try
                {
                //dtrParamInfo = CloneDataRow(dtrParamInfoInitil);
                String strCriteria = objLookUp.GetFilterCriteria;

                if (strCriteria.Length > 0)
                {
                    if (dtrParamInfo["strOuterAliasBaseWhereClause"].ToString().Length > 0)
                    {
                        dtrParamInfo["strOuterAliasBaseWhereClause"] = dtrParamInfo["strOuterAliasBaseWhereClause"] + " AND " + strCriteria;
                    }
                    else
                    {
                        //dtrParamInfo["strOuterAliasBaseWhereClause"] = " AND " + strCriteria.Replace("[", "").Replace("]", "");
                        dtrParamInfo["strOuterAliasBaseWhereClause"] = " AND " + strCriteria;
                    }
                }
                objLookUp.gridDataSource = DAL.GetData(StrConnectionString, dtrParamInfo);
                objLookUp.setRecordInfoStaus = "Records " + ((DataSet)objLookUp.gridDataSource).Tables[0].Rows.Count + " of " + ((DataSet)objLookUp.gridDataSource).Tables[1].Rows[0][0].ToString();
                //objLookUp.SetGridColomnCaption();
                //objLookUp.SetGridRowColor();

                //objLookUp.SetGridColomnFormat(dtrParamInfo["strColsToDate"].ToString(), dtrParamInfo["strColsToTime"].ToString(), dtrParamInfo["strColsToDateTime"].ToString());
                //objLookUp.HideAndFixedColumn(dtrParamInfo["strColsToHide"].ToString(), dtrParamInfo["strColsToFixed"].ToString());
                objLookUp.HideColumn(dtrParamInfo["ColsToHide"].ToString());
                objLookUp.setRecordInfoStaus = "Records " + ((DataSet)objLookUp.gridDataSource).Tables[0].Rows.Count + " of " + ((DataSet)objLookUp.gridDataSource).Tables[1].Rows[0][0].ToString();
                objLookUp.SetStatusForNevigationButtons(Convert.ToInt32(((DataSet)objLookUp.gridDataSource).Tables[0].Rows.Count), Convert.ToInt32(((DataSet)objLookUp.gridDataSource).Tables[1].Rows[0][0]));
            }
            catch (Exception ex)
            {
                // ZEASuite.LogManager.ErrorLogging.objErrorLogging.LogError("LookupController of " + this.intMenuCode, "GetFilteredData", ex, false, false);
                throw ex;
            }
        }

        private DataRow CloneDataRow(DataRow dtrOld)
        {
            try
            {
                DataTable dtNew = dtrOld.Table.Clone();
                dtNew.ImportRow(dtrOld);
                return dtNew.Rows[0];
            }
            catch (Exception ex)
            {
                // ZEASuite.LogManager.ErrorLogging.objErrorLogging.LogError("LookupController of " + this.intMenuCode, "CloneDataRow", ex, false, false);
                throw ex;
            }
        }
    }
}