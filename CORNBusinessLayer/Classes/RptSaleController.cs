using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;
using CORNDatabaseLayer.Classes;

namespace CORNBusinessLayer.Classes
{
    /// <summary>
    /// Class For Fetching Data Of Sales Reports
    /// </summary>
    public class RptSaleController
    {
        #region Constructor

        /// <summary>
        /// Constructor for RptSaleController
        /// </summary>
        public RptSaleController()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

        #region Public Methods

        #region Print Sale Document Report

        /// <summary>
        /// Gets Data For Print Sale Document Report(Orders, Invoices, Sale Returns And Delivery Chalans)
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Areaid">Route</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="FromDocNo">FromDate</param>
        /// <param name="ToDocNo">ToDate</param>
        /// <param name="DocumentTypeId">Type</param>
        /// <param name="p_DOCUMENT_ID">Document</param>
        /// <param name="p_IS_REGISTERED">IsRegistered</param>
        /// <param name="p_CUSTOMER_ID">Customer</param>
        /// <param name="p_Route_ID">Route</param>
        /// <returns>DataSet</returns>
        public DataSet SelectDocumentforPrint(int p_Distributor_ID, int p_Areaid, int p_Principal_Id, DateTime FromDocNo, DateTime ToDocNo, int DocumentTypeId, long p_DOCUMENT_ID, int p_IS_REGISTERED, int p_CUSTOMER_ID, int p_Route_ID, int p_PRINTTYPE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspDocumentPrinting ObjPrint = new UspDocumentPrinting();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBTOR_ID = p_Distributor_ID;
                ObjPrint.AREA_ID = p_Areaid;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.FROM_DATE = FromDocNo;
                ObjPrint.TO_DATE = ToDocNo;
                ObjPrint.TYPE_ID = DocumentTypeId;
                ObjPrint.DOCUMENT_ID = p_DOCUMENT_ID;
                ObjPrint.IS_REGISTERED = p_IS_REGISTERED;
                ObjPrint.CUSTOMER_ID = p_CUSTOMER_ID;
                ObjPrint.ROUTE_ID = p_Route_ID;
                ObjPrint.PRINTTYPE = p_PRINTTYPE;

                DataTable dt = ObjPrint.ExecuteTable();

                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["SALE_DOCUMENTPRINT"].ImportRow(dr);
                }

                uspPrintSALE_ORDER_PROMOTION Promotion = new uspPrintSALE_ORDER_PROMOTION();

                Promotion.Connection = mConnection;
                Promotion.DISTRIBTOR_ID = p_Distributor_ID;
                Promotion.AREA_ID = p_Areaid;
                Promotion.PRINCIPAL_ID = p_Principal_Id;
                Promotion.FROM_DATE = FromDocNo;
                Promotion.TO_DATE = ToDocNo;
                Promotion.TYPE_ID = DocumentTypeId;
                DataTable dtPro = Promotion.ExecuteTable();

                foreach (DataRow dr in dtPro.Rows)
                {
                    ds.Tables["SALE_PROMOTIONPRINT"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #region Added By Hazrat Ali

        /// <summary>
        /// Gets Data For Print Sale Document Report(USD Invoices)
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Areaid">Route</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="FromDocNo">DateFrom</param>
        /// <param name="ToDocNo">DateTo</param>
        /// <param name="DocumentTypeId">Type</param>
        /// <param name="p_DOCUMENT_ID">Document</param>
        /// <param name="p_IS_REGISTERED">IsRegistered</param>
        /// <param name="p_CUSTOMER_ID">Customer</param>
        /// <param name="p_ROUTE_ID">Route</param>
        /// <returns>DataSet</returns>
        public DataSet SelectCSDUSCDocumentforPrint(int p_Distributor_ID, int p_Areaid, int p_Principal_Id, DateTime FromDocNo, DateTime ToDocNo, int DocumentTypeId, long p_DOCUMENT_ID, int p_IS_REGISTERED, int p_CUSTOMER_ID, int p_ROUTE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspPrintCSDUSCInvoice ObjPrint = new UspPrintCSDUSCInvoice();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBTOR_ID = p_Distributor_ID;
                ObjPrint.AREA_ID = p_Areaid;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.FROM_DATE = FromDocNo;
                ObjPrint.TO_DATE = ToDocNo;
                ObjPrint.TYPE_ID = DocumentTypeId;
                ObjPrint.DOCUMENT_ID = p_DOCUMENT_ID;
                ObjPrint.IS_REGISTERED = p_IS_REGISTERED;
                ObjPrint.CUSTOMER_ID = p_CUSTOMER_ID;
                ObjPrint.ROUTE_ID = p_ROUTE_ID;
                DataTable dt = ObjPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["SALE_DOCUMENTPRINT"].ImportRow(dr);
                }

                uspPrintSALE_ORDER_PROMOTION Promotion = new uspPrintSALE_ORDER_PROMOTION();

                Promotion.Connection = mConnection;
                Promotion.DISTRIBTOR_ID = p_Distributor_ID;
                Promotion.AREA_ID = p_Areaid;
                Promotion.PRINCIPAL_ID = p_Principal_Id;
                Promotion.FROM_DATE = FromDocNo;
                Promotion.TO_DATE = ToDocNo;
                Promotion.TYPE_ID = DocumentTypeId;
                DataTable dtPro = Promotion.ExecuteTable();

                foreach (DataRow dr in dtPro.Rows)
                {
                    ds.Tables["SALE_PROMOTIONPRINT"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// Gets Data For Route Wise Customer List Report
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Areaid">Route</param>
        /// <param name="p_ChannelType_Id">Type</param>
        /// <param name="p_TownId">Town</param>
        /// <param name="IsRegister">IsRegistered</param>
        /// <param name="p_Principal">Principal</param>
        /// <returns>DataSet</returns>
        public DataSet SelectPrincipalWiseCustomer(int p_Distributor_ID, int p_Type_ID, int p_User_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectPrincipalWiseCustomer ObjPrint = new UspSelectPrincipalWiseCustomer();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_ID;
                ObjPrint.TYPE_ID = p_Type_ID;
                ObjPrint.USER_ID = p_User_ID;

                DataTable dt = ObjPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["UspSelectPrincipalWiseCustomer"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #region Sale Person DSR Report

        /// <summary>
        /// Gets Data For Sale Person DSR Report(Order Booker,Product Wise)
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_FromDate">DateFrom</param>
        /// <param name="p_To_Date">DateTo</param>
        /// <param name="User_Id">User</param>
        /// <returns>DataSet</returns>
        public DataSet SelectOrderBookerDSRProDuctWise(int p_Distributor_ID, int p_Principal_Id, DateTime p_FromDate, DateTime p_To_Date, int User_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                RptOrderBookerDSRProductWise ObjPrint = new RptOrderBookerDSRProductWise();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_ID;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.FROM_DATE = p_FromDate;
                ObjPrint.TO_DATE = p_To_Date;
                ObjPrint.USER_ID = User_Id;
                DataTable dt = ObjPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["RptSaleReportProductWise"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Sale Person DSR Report(Saleman, Product Wise)
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_FromDate">DateFrom</param>
        /// <param name="p_To_Date">DateTo</param>
        /// <param name="User_Id">User</param>
        /// <returns>DataSet</returns>
        public DataSet SelectSalePersonDSRProDuctWise(int p_Distributor_ID, int p_Principal_Id, DateTime p_FromDate, DateTime p_To_Date, int User_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                RptSalePersonDSRProductWise ObjPrint = new RptSalePersonDSRProductWise();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_ID;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.FROM_DATE = p_FromDate;
                ObjPrint.TO_DATE = p_To_Date;
                ObjPrint.USER_ID = User_Id;
                DataTable dt = ObjPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["RptSaleReportProductWise"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Sale Person DSR Report(Order Booker,Value Wise)
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_FromDate">DateFrom</param>
        /// <param name="p_To_Date">DateTo</param>
        /// <param name="p_UserId">User</param>
        /// <returns>DataSet</returns>
        public DataSet SelectOrderBookerDSR(int p_Distributor_ID, int p_Principal_Id, DateTime p_FromDate, DateTime p_To_Date, int p_UserId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspPrintOrderBookerDSR ObjPrint = new UspPrintOrderBookerDSR();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_ID;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.FROM_DATE = p_FromDate;
                ObjPrint.TO_DATE = p_To_Date;
                ObjPrint.USER_ID = p_UserId;
                DataTable dt = ObjPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["SALEPERSON_TRANSCTIONDETAIL"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

      
        public DataSet SelectSalePersonDSR(int pDistributorId, DateTime pFromDate, DateTime pToDate, int pUserId,int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspPrintSaleForceDSR objPrint = new UspPrintSaleForceDSR();
                Reports.LatestDataSet ds = new Reports.LatestDataSet();
                objPrint.Connection = mConnection;
                objPrint.DISTRIBUTOR_ID = pDistributorId;
                objPrint.FROM_DATE = pFromDate;
                objPrint.TO_DATE = pToDate;
                objPrint.USER_ID = pUserId;
                objPrint.TYPE_ID = p_TYPE_ID;

                DataTable dt = objPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["UspPrintSaleForceDSR"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }



        public DataSet SelectBookerDSR(int pDistributorId, DateTime pFromDate, DateTime pToDate, int pUserId, int p_TYPE_ID, int p_Booker_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspPrintBookerDSR objPrint = new UspPrintBookerDSR();
                Reports.LatestDataSet ds = new Reports.LatestDataSet();
                objPrint.Connection = mConnection;
                objPrint.DISTRIBUTOR_ID = pDistributorId;
                objPrint.FROM_DATE = pFromDate;
                objPrint.TO_DATE = pToDate;
                objPrint.USER_ID = pUserId;
                //objPrint.TYPE_ID = p_TYPE_ID;
                objPrint.BOOKER_ID = p_Booker_ID;
                DataTable dt = objPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["UspPrintSaleForceDSR"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public DataSet SelectBookerInvoiceWiseDSR(int pDistributorId, DateTime pFromDate, DateTime pToDate, int pUserId, int p_TYPE_ID, int p_Booker_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspPrintBookerWiseInvoiceDetail objPrint = new UspPrintBookerWiseInvoiceDetail();
                Reports.LatestDataSet ds = new Reports.LatestDataSet();
                objPrint.Connection = mConnection;
                objPrint.DISTRIBUTOR_ID = pDistributorId;
                objPrint.FROM_DATE = pFromDate;
                objPrint.TO_DATE = pToDate;
                objPrint.USER_ID = pUserId;
                //objPrint.TYPE_ID = p_TYPE_ID;
                objPrint.BOOKER_ID = p_Booker_ID;
                DataTable dt = objPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["UspPrintSaleForceDSR"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataSet SelectBookerSkuWiseDSR(int pDistributorId, DateTime pFromDate, DateTime pToDate, int pUserId, int p_TYPE_ID, int p_Booker_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspPrintBookerSkuDetail objPrint = new UspPrintBookerSkuDetail();
                Reports.LatestDataSet ds = new Reports.LatestDataSet();
                objPrint.Connection = mConnection;
                objPrint.DISTRIBUTOR_ID = pDistributorId;
                objPrint.FROM_DATE = pFromDate;
                objPrint.TO_DATE = pToDate;
                objPrint.USER_ID = pUserId;
                //objPrint.TYPE_ID = p_TYPE_ID;
                objPrint.BOOKER_ID = p_Booker_ID;
                DataTable dt = objPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["UspPrintBookerSkuDetail"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }


        public DataSet PrintDailySalesReportDateWise(int pDistributorId, DateTime pFromDate, DateTime pToDate, int pUserId, int pTypeId)
        {  
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spPrintDailySalesReportDateWise objPrint = new spPrintDailySalesReportDateWise();
                Reports.LatestDataSet ds = new Reports.LatestDataSet();
                objPrint.Connection = mConnection;
                objPrint.DISTRIBUTOR_ID = pDistributorId;
                objPrint.FROM_DATE = pFromDate;
                objPrint.TO_DATE = pToDate;
                objPrint.USER_ID = pUserId;
                objPrint.TYPE_ID = pTypeId;
                DataTable dt = objPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["spPrintDailySalesReportDateWise"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public DataSet PrintVoidOrderSaleReport(int pDistributorId, DateTime pFromDate, DateTime pToDate, int pUserId, int pTypeId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spVoidOrderSaleReport objPrint = new spVoidOrderSaleReport();
                Reports.DsReport ds = new Reports.DsReport();
                objPrint.Connection = mConnection;
                objPrint.DISTRIBUTOR_ID = pDistributorId;
                objPrint.FROM_DATE = pFromDate;
                objPrint.TO_DATE = pToDate;
                objPrint.USER_ID = pUserId;
                objPrint.TYPE_ID = pTypeId;
                DataTable dt = objPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["spVoidOrderSaleReport"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        /// <summary>
        /// Gets Data For Value Reconciliation Report
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_Type">Type</param>
        /// <param name="p_FromDate">DateFrom</param>
        /// <param name="p_To_Date">DateTo</param>
        /// <param name="p_UserId">User</param>
        /// <returns>DataSet</returns>
        public DataSet SelectValueReconcilation(int p_Distributor_ID, int p_Principal_Id, string p_Type, DateTime p_FromDate, DateTime p_To_Date, int p_UserId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspRptValueReconcilation ObjPrint = new UspRptValueReconcilation();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_ID;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.TYPE = p_Type;
                ObjPrint.FROM_DATE = p_FromDate;
                ObjPrint.TO_DATE = p_To_Date;
                ObjPrint.USER_ID = p_UserId;
                DataTable dt = ObjPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["RptRegionWise_Reconciliation"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public DataTable SelectSaleReport(int p_Distributor_Id, int p_UserId, DateTime p_StartDate, int pTypeId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                sp_SelectSaleReport mOrder = new sp_SelectSaleReport
                {
                    Connection = mConnection,
                    DISTRIBUTOR_ID = p_Distributor_Id,
                    typeId = pTypeId,
                    USER_ID = p_UserId,
                    Date = p_StartDate
                };
                return mOrder.ExecuteTable();
               
               

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Gets Data For NCS vs Bank Deposit Report
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_FromDate">DateFrom</param>
        /// <param name="p_To_Date">DateTo</param>
        /// <param name="p_UserId">User</param>
        /// <returns>DataSet</returns>
        public DataSet SelectDailyBankDeposit(int p_Distributor_ID, int p_Principal_Id, DateTime p_FromDate, DateTime p_To_Date, int p_UserId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspDailyBankDeposit ObjPrint = new UspDailyBankDeposit();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_ID;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.FROM_DATE = p_FromDate;
                ObjPrint.TO_DATE = p_To_Date;
                ObjPrint.USER_ID = p_UserId;
                DataTable dt = ObjPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["RptDailyBankDeposit"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Sales & Closing Stock Report(Sales, Sales Return, Damage, Opening Stock And Closing Stock)
        /// </summary>
        /// <param name="P_DistributorType">LocationType</param>
        /// <param name="P_PrincipalId">Principal</param>
        /// <param name="P_ZoneId">Zone</param>
        /// <param name="P_DistributorId">Location</param>
        /// <param name="P_frmDate">DateFrom</param>
        /// <param name="P_toDate">DateTo</param>
        /// <param name="P_Category">Cateogry</param>
        /// <param name="P_Type">Type</param>
        /// <param name="P_ReportType">ReportType</param>
        /// <param name="p_UserId">User</param>
        /// <returns>DataSet</returns>
        public DataSet GetRegionSaleDetail(int P_DistributorType, int P_PrincipalId, int P_ZoneId, int P_DistributorId, DateTime P_frmDate, DateTime P_toDate, int P_Category, int P_Type, int P_ReportType, int p_UserId)
        {

            IDbConnection mConnection = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                Reports.DsReport ds = new Reports.DsReport();
                var objRegionwise = new uspRegionWiseSales();
                objRegionwise.Connection = mConnection;
                objRegionwise.CATEGORY_ID = P_Category;
                objRegionwise.distributor_id = P_DistributorId;
                objRegionwise.Distributor_type = P_DistributorType;
                objRegionwise.Zone_id = P_ZoneId;
                objRegionwise.FROM_DATE = P_frmDate;
                objRegionwise.TO_DATE = P_toDate;
                objRegionwise.Type = P_Type;
                objRegionwise.ReportType = P_ReportType;
                objRegionwise.PrincipalId = P_PrincipalId;
                objRegionwise.user_id = p_UserId;
                DataTable DT = objRegionwise.ExecuteTable();

                foreach (DataRow dr in DT.Rows)
                {
                    ds.Tables["RegionWiseSales"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception excp)
            {
                ExceptionPublisher.PublishException(excp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataSet GetRegionItemSaleDetail(int pDistributorId, int pCategory, int pUserId, DateTime pFrmDate, DateTime pToDate, int p_RptType)
        {

            IDbConnection mConnection = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var ds = new Reports.dsSalesPurchaseRegister();
                var objRegionwise = new uspRegionWiseItemSales
                {
                    Connection = mConnection,
                    CATEGORY_ID = pCategory,
                    distributor_id = pDistributorId,
                    FROM_DATE = pFrmDate,
                    TO_DATE = pToDate,
                    RptType = p_RptType,
                    user_id = pUserId
                    
                };

                DataTable dt = objRegionwise.ExecuteTable();

                foreach (DataRow dr in dt.Rows)
                {
                    if (p_RptType == 1)
                    {
                        ds.Tables["uspRegionWiseItemSales1"].ImportRow(dr);
                    }
                    else
                    {
                        ds.Tables["uspRegionWiseItemSales"].ImportRow(dr);
                    }
                }
                return ds;
            }
            catch (Exception excp)
            {
                ExceptionPublisher.PublishException(excp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public DataSet GethourlySaleDetail(int pDistributorId, int pCategory, int pUserId, DateTime pFrmDate, DateTime pToDate, int pType)
        {

            IDbConnection mConnection = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var ds = new Reports.dsSalesPurchaseRegister();
                var objRegionwise = new uspHourlyItemSales
                {
                    Connection = mConnection,
                    CATEGORY_ID = pCategory,
                    distributor_id = pDistributorId,
                    FROM_DATE = pFrmDate,
                    TO_DATE = pToDate,
                    Type = pType,
                    user_id = pUserId
                };

                DataTable dt = objRegionwise.ExecuteTable();

                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["uspHourlyItemSales"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception excp)
            {
                ExceptionPublisher.PublishException(excp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }



        public DataSet GetPRASynchSaleDetail(int pDistributorId, int pCategory, int pUserId, DateTime pFrmDate, DateTime pToDate, int pType)
        {

            IDbConnection mConnection = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var ds = new Reports.dsSalesPurchaseRegister();
                var objRegionwise = new uspPraSyncReport
                {
                    Connection = mConnection,
                    CATEGORY_ID = pCategory,
                    distributor_id = pDistributorId,
                    FROM_DATE = pFrmDate,
                    TO_DATE = pToDate,
                    Type = pType,
                    user_id = pUserId
                };

                DataTable dt = objRegionwise.ExecuteTable();

                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["UspSelectRollBackDocument"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception excp)
            {
                ExceptionPublisher.PublishException(excp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For SKU Wise Branch Sales Report
        /// </summary>
        /// <param name="P_DistributorType">LocationType</param>
        /// <param name="P_PrincipalId">Principal</param>
        /// <param name="P_DistributorId">Location</param>
        /// <param name="P_frmDate">DateFrom</param>
        /// <param name="P_toDate">DateTo</param>
        /// <param name="p_UserId">User</param>
        /// <returns>DataSet</returns>
        public DataSet GetDistributorReconcilation(int P_DistributorType, int P_PrincipalId, int P_DistributorId, DateTime P_frmDate, DateTime P_toDate, int p_UserId)
        {

            IDbConnection mConnection = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();
                UspSelectDistributorWiseSale obj_regionwise = new UspSelectDistributorWiseSale();
                obj_regionwise.Connection = mConnection;
                obj_regionwise.LocationType = P_DistributorType;
                obj_regionwise.DistributorId = P_DistributorId;
                obj_regionwise.PrincipalId = P_PrincipalId;
                obj_regionwise.FROM_DATE = P_frmDate;
                obj_regionwise.TO_DATE = P_toDate;
                obj_regionwise.USER_ID = p_UserId;
                DataTable DT = obj_regionwise.ExecuteTable();

                foreach (DataRow dr in DT.Rows)
                {
                    ds.Tables["DistributorReconcilation"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception excp)
            {
                ExceptionPublisher.PublishException(excp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Target Vs Achievement Report
        /// </summary>
        /// <param name="p_Principal_ID">Principal</param>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_dateTime">Month</param>
        /// <param name="p_Option_ID">Option</param>
        /// <param name="p_User_id">User</param>
        /// <param name="p_RegionId">Region</param>
        /// <returns></returns>
        public DataSet SelectVolumeSale(int p_Principal_ID, int p_Distributor_ID, DateTime p_dateTime, int p_Option_ID, int p_User_id, int p_RegionId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();

                uspSaleVolmeRpt mSaleVolume = new uspSaleVolmeRpt();
                mSaleVolume.Connection = mConnection;

                mSaleVolume.PRINCIPAL_ID = p_Principal_ID;
                mSaleVolume.DISTRIBUTOR_ID = p_Distributor_ID;
                mSaleVolume.TODAY_DATE = p_dateTime;
                mSaleVolume.OPTION = p_Option_ID;
                mSaleVolume.USER_ID = p_User_id;
                mSaleVolume.REGION_ID = p_RegionId;
                DataTable DT = mSaleVolume.ExecuteTable();
                foreach (DataRow dr in DT.Rows)
                {
                    ds.Tables["VolumeSale"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Export Data Excel Report
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PRINCIPAL_ID">Principal</param>
        /// <param name="P_FromDate">DateFrom</param>
        /// <param name="p_ToDate">DateTo</param>
        /// <param name="p_UserId">User</param>
        /// <returns>DataSet</returns>
        public DataSet SelectPivateTableExcelFile(int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, DateTime P_FromDate, DateTime p_ToDate, int p_UserId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspfinalsaleDetailView mCustData = new UspfinalsaleDetailView();
                mCustData.Connection = mConnection;

                mCustData.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustData.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mCustData.FROM_DATE = P_FromDate;
                mCustData.TO_DATE = p_ToDate;
                mCustData.TOWN_ID = p_UserId;
                DataSet dt = mCustData.ExecuteTable();
                return dt;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #region Order Booker Reports

        /// <summary>
        /// Gets Data For Order Booker Reports(Load Pass Summary)
        /// </summary>
        /// <param name="p_Principal_ID">Principal</param>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Fromdate">DateFrom</param>
        /// <param name="p_ToDate">DateTo</param>
        /// <param name="p_OrderBookerId">OrderBookerType</param>
        /// <param name="p_TypeId">Type</param>
        /// <returns>DataSet</returns>
        public DataSet LoadPass(int p_Principal_ID, int p_Distributor_ID, DateTime p_Fromdate, DateTime p_ToDate, int p_OrderBookerId, int p_TypeId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();

                RptLoadPass mLoadPass = new RptLoadPass();
                mLoadPass.Connection = mConnection;
                mLoadPass.PRINCIPAL_ID = p_Principal_ID;
                mLoadPass.DISTRIBUTOR_ID = p_Distributor_ID;
                mLoadPass.FROM_DATE = p_Fromdate;
                mLoadPass.TO_DATE = p_ToDate;
                mLoadPass.ORDERBOOKER_ID = p_OrderBookerId;
                mLoadPass.USER_ID = p_TypeId;
                DataTable DT = mLoadPass.ExecuteTable();
                foreach (DataRow dr in DT.Rows)
                {
                    ds.Tables["LoadPass"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Order Booker Reports(Order Booker Sheet)
        /// </summary>
        /// <param name="p_Principal_ID">Principal</param>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Fromdate">DateFrom</param>
        /// <param name="p_ToDate">DateTo</param>
        /// <param name="p_OrderBookerId">OrderBooker</param>
        /// <param name="p_User_Id">User</param>
        /// <returns>DataSet</returns>
        public DataSet OrderBookerSheet(int p_Principal_ID, int p_Distributor_ID, DateTime p_Fromdate, DateTime p_ToDate, int p_OrderBookerId, int p_User_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();

                UspOrderBookerSheet mLoadPass = new UspOrderBookerSheet();
                mLoadPass.Connection = mConnection;
                mLoadPass.PRINCIPAL_ID = p_Principal_ID;
                mLoadPass.DISTRIBUTOR_ID = p_Distributor_ID;
                mLoadPass.FROM_DATE = p_Fromdate;
                mLoadPass.TO_DATE = p_ToDate;
                mLoadPass.ORDERBOOKER_ID = p_OrderBookerId;
                mLoadPass.USER_ID = p_User_Id;
                DataTable DT = mLoadPass.ExecuteTable();
                foreach (DataRow dr in DT.Rows)
                {
                    ds.Tables["RptOrderBookerSheet"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        /// <summary>
        /// Gets Data For SKU Price List Report
        /// </summary>
        /// <param name="p_Principal_ID">Principal</param>
        /// <param name="p_Catagory_ID">Category</param>
        /// <param name="p_DistributorId">Location</param>
        /// <returns>DataSet</returns>
        public DataSet PriceList(int p_Principal_ID, int p_Catagory_ID, int p_DistributorId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                Reports.DsReport ds = new Reports.DsReport();

                rptPriceList mPriceList = new rptPriceList();

                mPriceList.Connection = mConnection;
                mPriceList.PRINCIPAL_ID = p_Principal_ID;
                mPriceList.CATEGORY_ID = p_Catagory_ID;
                mPriceList.DISTRIBUTOR_ID = p_DistributorId;


                DataTable DT = mPriceList.ExecuteTable();
                foreach (DataRow dr in DT.Rows)
                {
                    ds.Tables["PriceList"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Promotion Report(Active, InActive)
        /// </summary>
        /// <param name="p_Principal_ID">Principal</param>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_StartDate">DateStart</param>
        /// <param name="p_EndDate">DateEnd</param>
        /// <param name="p_PromotionType">Type</param>
        /// <returns>DataSet</returns>
        public DataSet PromotionDetail(int p_Principal_ID, int p_Distributor_ID, DateTime p_StartDate, DateTime p_EndDate, int p_PromotionType)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();

                RptPromotionReport mPromotionReport = new RptPromotionReport();

                mPromotionReport.Connection = mConnection;
                mPromotionReport.Principal_ID = p_Principal_ID;
                mPromotionReport.Distributor_ID = p_Distributor_ID;
                mPromotionReport.START_DATE = p_StartDate;
                mPromotionReport.END_DATE = p_EndDate;
                mPromotionReport.ISACTIVE = Convert.ToBoolean(p_PromotionType);
                DataTable DT = mPromotionReport.ExecuteTable();
                foreach (DataRow dr in DT.Rows)
                {
                    ds.Tables["PromotionReport"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Daily Sale Update Report
        /// </summary>
        /// <param name="p_Principal_ID">Principal</param>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_dateTime">Date</param>
        /// <param name="p_Region">Region</param>
        /// <param name="p_Zone">Zone</param>
        /// <param name="p_Territory_Id">Territory</param>
        /// <param name="p_Type">Type</param>
        /// <returns>DataSet</returns>
        public DataSet SelectDailyUpdateSales(int p_Principal_ID, int p_Distributor_ID, DateTime p_dateTime, int p_Region, int p_Zone, int p_Territory_Id, int p_Type)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();
                UspDailySaleUpdate mSaleVolume = new UspDailySaleUpdate();
                mSaleVolume.Connection = mConnection;
                mSaleVolume.PRINCIPAL_ID = p_Principal_ID;
                mSaleVolume.DISTRIBUTOR_ID = p_Distributor_ID;
                mSaleVolume.TARGET_DATE = p_dateTime;
                mSaleVolume.Region_Id = p_Region;
                mSaleVolume.Zone_Id = p_Zone;
                mSaleVolume.Territory_Id = p_Territory_Id;
                mSaleVolume.TYPE_ID = p_Type;
                DataTable DT = mSaleVolume.ExecuteTable();
                foreach (DataRow dr in DT.Rows)
                {
                    ds.Tables["RptDailySalesUpdated"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if ((mConnection != null) && (mConnection.State == ConnectionState.Open))
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Business Analysis Report
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_FromDate">DateFrom</param>
        /// <param name="p_ToDate">DateTo</param>
        /// <param name="p_UserId">User</param>
        /// <param name="p_PrincipalId">Principal</param>
        /// <returns>DataSet</returns>
        public DataSet SelectBusinessAnalysis(int p_Distributor_ID, DateTime p_FromDate, DateTime p_ToDate, int p_UserId, int p_PrincipalId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspBusinessAnalysisReport ObjPrint = new UspBusinessAnalysisReport();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_ID;
                ObjPrint.FROM_DATE = p_FromDate;
                ObjPrint.TO_DATE = p_ToDate;
                ObjPrint.UserId = p_UserId;
                ObjPrint.PRINCIPAL_ID = p_PrincipalId;
                DataTable dt = ObjPrint.ExecuteTable();

                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["RptBusiness_Anaysis Report"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Trade Channel Sale Report (SKU Wise)
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_FromDate">DateFrom</param>
        /// <param name="p_To_Date">DateTo</param>
        /// <param name="User_Id">User</param>
        /// <param name="p_AreaId">Route</param>
        /// <param name="p_SalesForce_Id">Deliverman</param>
        /// <returns>DataSet</returns>
        public DataSet SelectTradeChannelSale(int p_Distributor_ID, int p_Principal_Id, DateTime p_FromDate, DateTime p_To_Date, int User_Id, int p_AreaId, int p_SalesForce_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspTradeChannelSale ObjPrint = new UspTradeChannelSale();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_ID;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.FROM_DATE = p_FromDate;
                ObjPrint.TO_DATE = p_To_Date;
                ObjPrint.UserId = User_Id;
                ObjPrint.AreaId = p_AreaId;
                ObjPrint.DeliveryManId = p_SalesForce_Id;
                ObjPrint.OrderBookerId = Constants.IntNullValue;

                DataTable dt = ObjPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["RptTradeChannelSaleDetail"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Trade Channel Sale Report (Branch Wise)
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_FromDate">DateFrom</param>
        /// <param name="p_To_Date">DateTo</param>
        /// <param name="User_Id">User</param>
        /// <returns>DataSet</returns>
        public DataSet SelectTradeChannelSale(int p_Distributor_ID, int p_Principal_Id, DateTime p_FromDate, DateTime p_To_Date, int User_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspTradeChannelSaleBRANCH ObjPrint = new UspTradeChannelSaleBRANCH();
                CORNBusinessLayer.Reports.DsReport2 ds = new CORNBusinessLayer.Reports.DsReport2();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_ID;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.FROM_DATE = p_FromDate;
                ObjPrint.TO_DATE = p_To_Date;
                ObjPrint.UserId = User_Id;
                DataTable dt = ObjPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["TradeChannelSale"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Gross Profit Report
        /// </summary>
        /// <param name="pDistributorId">Location</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="pFromDate">DateFrom</param>
        /// <param name="pToDate">DateTo</param>
        /// <returns>DataSet</returns>
        //public DataSet SelectRptGrossProfit(int p_Distributor_ID, int p_Principal_Id, DateTime p_FromDate, DateTime p_To_Date)
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        UspGrossProfitReport ObjPrint = new UspGrossProfitReport();
        //        Reports.DsReport2 ds = new Reports.DsReport2();
        //        ObjPrint.Connection = mConnection;
        //        ObjPrint.DISTRIBUTOR_ID = p_Distributor_ID;
        //        ObjPrint.PRINCIPAL_ID = p_Principal_Id;
        //        ObjPrint.FROM_DATE = p_FromDate;
        //        ObjPrint.TO_DATE = p_To_Date;
        //        DataTable dt = ObjPrint.ExecuteTable();
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            ds.Tables["GrossPrifitReport"].ImportRow(dr);
        //        }

        //        return ds;
        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        return null;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }
        //}
        /// <summary>
        /// Gets Data For Gross Profit Report
        /// </summary>
        /// <param name="pDistributorId">Location</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="pFromDate">DateFrom</param>
        /// <param name="pToDate">DateTo</param>
        /// <returns>DataSet</returns>
        public DataSet SelectRptGrossProfit(int pAccountCategoryId, int pDistributorId, DateTime pFromDate, DateTime pToDate)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspGrossProfitReport objPrint = new UspGrossProfitReport();
                Reports.DsReport2 ds = new Reports.DsReport2();
                objPrint.Connection = mConnection;
                objPrint.AccountCategoryId = pAccountCategoryId;
                objPrint.DistributorId = pDistributorId;
                objPrint.FromDate = pFromDate;

                objPrint.ToDate = pToDate;
                DataSet ds2 = objPrint.ExecuteTable();

                foreach (DataRow dr in ds2.Tables[0].Rows)
                {
                    ds.Tables["IncomeStatement"].ImportRow(dr);
                }

                foreach (DataRow dr in ds2.Tables[1].Rows)
                {
                    ds.Tables["IncomeStatement1"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        /// <summary>
        /// Gets Data For Monthly Sale Report (Trade Price And Purchase Price)
        /// </summary>
        /// <param name="P_DateType">DateType</param>
        /// <param name="P_PrincipalId">Principal</param>
        /// <param name="P_DistributorId">Location</param>
        /// <param name="P_frmDate">DateFrom</param>
        /// <param name="P_toDate">DateTo</param>
        /// <param name="p_UserId">User</param>
        /// <param name="p_Column">ReprotFor</param>
        /// <param name="p_PriceType">PriceType</param>
        /// <returns>DataSet</returns>
        public DataSet GetDistributorReconcilation(byte P_DateType, int P_PrincipalId, int P_DistributorId, DateTime P_frmDate, DateTime P_toDate, int p_UserId, byte p_Column, byte p_PriceType)
        {

            IDbConnection mConnection = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                CORNBusinessLayer.Reports.DsReport2 ds = new CORNBusinessLayer.Reports.DsReport2();
                DataTable DT;
                if (p_PriceType == 0)
                {
                    UspPrintSaleAnalysisSummary obj_regionwise = new UspPrintSaleAnalysisSummary();
                    obj_regionwise.Connection = mConnection;
                    obj_regionwise.DATETYPE = P_DateType;
                    obj_regionwise.DISTRIBUTOR_ID = P_DistributorId;
                    obj_regionwise.PRINCIPAL_ID = p_UserId;
                    obj_regionwise.FROM_DATE = P_frmDate;
                    obj_regionwise.TO_DATE = P_toDate;
                    obj_regionwise.COLUMN = p_Column;
                    DT = obj_regionwise.ExecuteTable();
                }
                else
                {
                    UspPrintSaleAnalysisSummaryPR obj_regionwise = new UspPrintSaleAnalysisSummaryPR();
                    obj_regionwise.Connection = mConnection;
                    obj_regionwise.DATETYPE = P_DateType;
                    obj_regionwise.DISTRIBUTOR_ID = P_DistributorId;
                    obj_regionwise.PRINCIPAL_ID = P_PrincipalId;
                    obj_regionwise.FROM_DATE = P_frmDate;
                    obj_regionwise.TO_DATE = P_toDate;
                    obj_regionwise.COLUMN = p_Column;
                    DT = obj_regionwise.ExecuteTable();
                }
                foreach (DataRow dr in DT.Rows)
                {
                    ds.Tables["RptMonthSaleValues"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception excp)
            {
                ExceptionPublisher.PublishException(excp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Monthly Sale Report (Trade Price And Purchase Price)
        /// </summary>
        /// <param name="P_DateType">DateType</param>
        /// <param name="P_PrincipalId">Principal</param>
        /// <param name="P_DistributorId">Location</param>
        /// <param name="P_frmDate">DateFrom</param>
        /// <param name="P_toDate">DateTo</param>
        /// <param name="p_UserId">User</param>
        /// <param name="p_Column">ReprotFor</param>
        /// <param name="p_PriceType">PriceType</param>
        /// <returns>DataSet</returns>
        public DataSet GetDistributorReconcilation2(byte P_DateType, int P_PrincipalId, int P_DistributorId, DateTime P_frmDate, DateTime P_toDate, int p_UserId, byte p_Column, byte p_PriceType,int p_Month)
        {

            IDbConnection mConnection = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                CORNBusinessLayer.Reports.DsReport2 ds = new CORNBusinessLayer.Reports.DsReport2();
                DataTable DT;

                UspPrintSaleAnalysisSummary obj_regionwise = new UspPrintSaleAnalysisSummary();
                obj_regionwise.Connection = mConnection;
                obj_regionwise.DATETYPE = P_DateType;
                obj_regionwise.DISTRIBUTOR_ID = P_DistributorId;
                obj_regionwise.PRINCIPAL_ID = p_UserId;
                obj_regionwise.FROM_DATE = P_frmDate;
                obj_regionwise.TO_DATE = P_toDate;
                obj_regionwise.COLUMN = p_Column;
                obj_regionwise.MONTH = p_Month;
                DT = obj_regionwise.ExecuteTable();


                foreach (DataRow dr in DT.Rows)
                {
                    ds.Tables["RptMonthSaleValues"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception excp)
            {
                ExceptionPublisher.PublishException(excp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public DataTable GetWeakofYear(int p_TYPE, string p_YEAR)
        {

            IDbConnection mConnection = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();



                spSelectWEAK_OF_YEAR obj_regionwise = new spSelectWEAK_OF_YEAR();
                obj_regionwise.Connection = mConnection;
                obj_regionwise.TYPE = p_TYPE;
                obj_regionwise.YEAR = p_YEAR;
               
                DataTable dt = obj_regionwise.ExecuteTable();
                return dt;

               
            }
            catch (Exception excp)
            {
                ExceptionPublisher.PublishException(excp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For  Principal Wise Reconciliation Report
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_FromDate">DateFrom</param>
        /// <param name="p_To_Date">DateTo</param>
        /// <param name="p_UserId">User</param>
        /// <returns>DataSet</returns>
        public DataSet SelectPrincipalReconcilation(int p_Distributor_ID, int p_Principal_Id, DateTime p_FromDate, DateTime p_To_Date, int p_UserId, string p_TYPE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspRptValueReconcilation ObjPrint = new UspRptValueReconcilation();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_ID;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.FROM_DATE = p_FromDate;
                ObjPrint.TO_DATE = p_To_Date;
                ObjPrint.USER_ID = p_UserId;
                ObjPrint.TYPE = p_TYPE;
                DataTable dt = ObjPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["RptRegionWise_Reconciliation"].ImportRow(dr);
                }

                UspCreditAgintReportPrincipalWise obj_regionwise = new UspCreditAgintReportPrincipalWise();
                obj_regionwise.Connection = mConnection;
                obj_regionwise.DISTRIBUTOR_ID = p_Distributor_ID;
                obj_regionwise.DISTRIBUTOR_TYPE_ID = Constants.IntNullValue;
                obj_regionwise.TYPE_ID = 0;
                obj_regionwise.PRINCIPAL_ID = p_Principal_Id;
                obj_regionwise.USER_ID = p_UserId;
                obj_regionwise.DOCUMENT_DATEFROM = p_FromDate;
                obj_regionwise.DOCUMENT_DATE = p_To_Date;
                obj_regionwise.CHANNEL_TYPE_ID = Constants.IntNullValue;
                DataTable DT = obj_regionwise.ExecuteTable();

                foreach (DataRow dr in DT.Rows)
                {
                    ds.Tables["RptCreditAgingReport"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Credit Tagging Report
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_FromDate">DateFrom</param>
        /// <param name="p_To_Date">DateTo</param>
        /// <param name="p_CreditTypeId">CreditType</param>
        /// <returns>DataSet</returns>
        public DataSet SelectCreditTagging(int p_Distributor_ID, int p_Principal_Id, DateTime p_FromDate, DateTime p_To_Date, int p_CreditTypeId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspCreditTaggingDetail ObjPrint = new UspCreditTaggingDetail();
                CORNBusinessLayer.Reports.DsReport2 ds = new CORNBusinessLayer.Reports.DsReport2();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_ID;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.FROM_DATE = p_FromDate;
                ObjPrint.TO_DATE = p_To_Date;
                ObjPrint.CREDIT_TYPE = p_CreditTypeId;
                DataTable dt = ObjPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["RptCustomerTagging"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        
        #region Added by Hazrat Ali

        /// <summary>
        /// Gets Data For Date Wise Discount Report (Date Wise, Branch Wise)
        /// </summary>
        /// <param name="p_FromDate">DateFrom</param>
        /// <param name="p_To_Date">DateTo</param>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="P_PrincipalID">Principal</param>
        /// <param name="p_TypeId">Type</param>
        /// <returns>DataSet</returns>
        public DataSet SelectDateWiseDiscount(DateTime p_FromDate, DateTime p_To_Date, int p_Distributor_ID, int P_PrincipalID, int p_TypeId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();

                uspRptDateWiseDiscount ObjDiscount = new uspRptDateWiseDiscount();
                ObjDiscount.Connection = mConnection;
                ObjDiscount.DateFrom = p_FromDate;
                ObjDiscount.DateTo = p_To_Date;
                ObjDiscount.DistributorID = p_Distributor_ID;
                ObjDiscount.PrincipalID = P_PrincipalID;
                ObjDiscount.TYPE_ID = p_TypeId;

                DataTable dt = ObjDiscount.ExecuteTable();

                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["RptDateWiseDiscount"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For User Login History Report
        /// </summary>
        /// <param name="p_FromDate">DateFrom</param>
        /// <param name="p_To_Date">DateTo</param>
        /// <param name="p_User_ID">User</param>
        /// <param name="p_User_Log_ID">UserLog</param>
        /// <returns>DataSet</returns>
        public DataSet GetUserLoginDetail(DateTime p_FromDate, DateTime p_To_Date, int p_User_ID, long p_User_Log_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();

                uspGetUserLoginDetail ObjLogin = new uspGetUserLoginDetail();
                ObjLogin.Connection = mConnection;
                ObjLogin.DateFrom = p_FromDate;
                ObjLogin.DateTo = p_To_Date;
                ObjLogin.USER_ID = p_User_ID;
                ObjLogin.User_Log_ID = p_User_Log_ID;

                DataTable dt = ObjLogin.ExecuteTable();

                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["uspGetUserLoginDetail"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For SKU Price History Report
        /// </summary>
        /// <param name="p_SKU_ID">SKU</param>
        /// <param name="p_Principal_ID">Principal</param>
        /// <param name="p_Category_ID">Category</param>
        /// <param name="p_DistributorId">Location</param>
        /// <param name="p_From_Date">DateFrom</param>
        /// <param name="p_ToDate">DateTo</param>
        /// <param name="p_TYPE">Type</param>
        /// <returns>DataSet</returns>
        public DataSet GetSKUPriceHistory(int p_SKU_ID, int p_Principal_ID, int p_Category_ID, int p_DistributorId, DateTime p_From_Date, DateTime p_ToDate, int p_TYPE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();

                uspGetSKUPriceHistory mSKUPriceHistory = new uspGetSKUPriceHistory();

                mSKUPriceHistory.Connection = mConnection;
                mSKUPriceHistory.SKU_ID = p_SKU_ID;
                mSKUPriceHistory.PRINCIPAL_ID = p_Principal_ID;
                mSKUPriceHistory.DISTRIBUTOR_ID = p_DistributorId;
                mSKUPriceHistory.CATEGORY_ID = p_Category_ID;
                mSKUPriceHistory.DATEFROM = p_From_Date;
                mSKUPriceHistory.DATETO = p_ToDate;
                mSKUPriceHistory.TYPE = p_TYPE;

                DataTable DT = mSKUPriceHistory.ExecuteTable();
                foreach (DataRow dr in DT.Rows)
                {
                    ds.Tables["SKUPriceHistory"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Branch Position Report
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_FromDate">DateFrom</param>
        /// <param name="p_To_Date">DateTo</param>
        /// <param name="p_UserId">User</param>
        /// <returns>DataSet</returns>
        public DataSet GetBranchPosition(int p_Distributor_ID, int p_Principal_Id, DateTime p_FromDate, DateTime p_To_Date, int p_UserId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetBranchPositionData ObjPrint = new uspGetBranchPositionData();
                CORNBusinessLayer.Reports.DsReport2 ds = new CORNBusinessLayer.Reports.DsReport2();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_ID;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.FROM_DATE = p_FromDate;
                ObjPrint.TO_DATE = p_To_Date;
                ObjPrint.USER_ID = p_UserId;
                DataTable dt = ObjPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["uspGetBranchPositionData"].ImportRow(dr);
                }

                uspGetSummarizedBranchPositionData ObjPrintSummary = new uspGetSummarizedBranchPositionData();

                ObjPrintSummary.Connection = mConnection;
                ObjPrintSummary.DISTRIBUTOR_ID = p_Distributor_ID;
                ObjPrintSummary.PRINCIPAL_ID = p_Principal_Id;
                ObjPrintSummary.FROM_DATE = p_FromDate;
                ObjPrintSummary.TO_DATE = p_To_Date;
                ObjPrintSummary.USER_ID = p_UserId;
                DataTable dtSummary = ObjPrintSummary.ExecuteTable();
                foreach (DataRow dr in dtSummary.Rows)
                {
                    ds.Tables["uspGetSummarizedBranchPositionData"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Daily Business Update Report
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_FromDate">DateFrom</param>
        /// <param name="p_To_Date">DateTo</param>
        /// <returns>DataSet</returns>
        public DataSet GetDailyBusinessUpdate(int p_Distributor_ID, int p_Principal_Id, DateTime p_FromDate, DateTime p_To_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspGetDailyBusinessUpdate ObjPrint = new UspGetDailyBusinessUpdate();
                CORNBusinessLayer.Reports.DsReport2 dsReport = new CORNBusinessLayer.Reports.DsReport2();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_ID;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.FROM_DATE = p_FromDate;
                ObjPrint.TO_DATE = p_To_Date;
                DataSet ds = ObjPrint.ExecuteDataSet();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dsReport.Tables["UspGetDailyBusinessUpdate"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        public DataSet GetDashboardFile(int p_intMODULE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetDashboardFile mDashboardFile = new uspGetDashboardFile();
                mDashboardFile.Connection = mConnection;
                mDashboardFile.intMODULE_ID = p_intMODULE_ID;
                return mDashboardFile.ExecuteDataSet();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataSet GetDashboardData(DateTime p_DATE_TO, string p_DISTRIBUTOR_IDs)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetDashboardData mDashboard = new uspGetDashboardData();
                mDashboard.Connection = mConnection;
                mDashboard.DATE_TO = p_DATE_TO;
                mDashboard.DISTRIBUTOR_IDs = p_DISTRIBUTOR_IDs;

                return mDashboard.ExecuteDataSet();


            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        #region Added by Hasan

        public DataSet SelectSKUSaleReport(int p_Distributor_ID, int p_Principal_Id, DateTime p_FromDate, DateTime p_To_Date, int p_Type)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();


                RptSelectProductSale_Detail ObjPrint = new RptSelectProductSale_Detail();
                CORNBusinessLayer.Reports.DsReport2 ds = new CORNBusinessLayer.Reports.DsReport2();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_ID;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.FROM_DATE = p_FromDate;
                ObjPrint.TO_DATE = p_To_Date;
                ObjPrint.TYPE = p_Type;
               
                DataTable dt = ObjPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["RptProductSale_Detail"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets Data For Gross Profit Report
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_FromDate">DateFrom</param>
        /// <param name="p_To_Date">DateTo</param>
        /// <returns>DataSet</returns>
        public DataSet GetBalanceSheet(int p_ACCOUNT_CATEGORY_ID, int p_DISTRIBUTOR_ID, DateTime p_FromDate, DateTime p_To_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetBalanceSheet ObjPrint = new uspGetBalanceSheet();

                Reports.DsReport2 ds = new Reports.DsReport2();

                ObjPrint.Connection = mConnection;
                ObjPrint.ACCOUNT_CATEGORY_ID = p_ACCOUNT_CATEGORY_ID;
                ObjPrint.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                ObjPrint.FROM_DATE = p_FromDate;
                ObjPrint.TO_DATE = p_To_Date;
                DataSet ds2 = ObjPrint.ExecuteTable();

                foreach (DataRow dr in ds2.Tables[0].Rows)
                {
                    ds.Tables["IncomeStatement"].ImportRow(dr);
                }

                foreach (DataRow dr in ds2.Tables[1].Rows)
                {
                    ds.Tables["IncomeStatement1"].ImportRow(dr);
                }

                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        #endregion

        public DataSet SelectRptGrossProfit2(int p_Distributor_ID, int p_Principal_Id, DateTime p_FromDate, DateTime p_To_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspGrossProfitReport2 ObjPrint = new UspGrossProfitReport2();
                CORNBusinessLayer.Reports.DsReport2 ds = new CORNBusinessLayer.Reports.DsReport2();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_ID;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.FROM_DATE = p_FromDate;
                ObjPrint.TO_DATE = p_To_Date;
                DataTable dt = ObjPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["UspGrossProfitReport2"].ImportRow(dr);
                }

                uspGetExpenseDetail Expense = new uspGetExpenseDetail();
                Expense.Connection = mConnection;
                Expense.DISTRIBUTOR_ID = p_Distributor_ID;
                Expense.PRINCIPAL_ID = p_Principal_Id;
                Expense.FROM_DATE = p_FromDate;
                Expense.TO_DATE = p_To_Date;
                DataTable dtPro = Expense.ExecuteTable();

                foreach (DataRow dr in dtPro.Rows)
                {
                    ds.Tables["uspGetExpenseDetail"].ImportRow(dr);
                }
                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        #endregion
    }
}
