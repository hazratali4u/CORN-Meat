using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;
using CORNDatabaseLayer.Classes;
using System.IO;

namespace CORNBusinessLayer.Classes
{
    /// <summary>
    /// Class For Purchase, TranferOut, Purchase Return, TranferIn And Damage Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </item>
    /// <term>
    /// Update Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </term>
    /// <item>
    /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class PurchaseController
    {
        #region Constructor

        /// <summary>
        /// Constructor for PurchaseController
        /// </summary>
        public PurchaseController()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion

        #region Private Variables

        IDbTransaction mTransaction;
        IDbConnection mConnection;

        #endregion

        #region Public Methods

        #region Select

        /// <summary>
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="PConnection">Connection</param>
        /// <param name="PTransaction">Transaction</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPrivousePurchaseDetail(int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, IDbConnection PConnection, IDbTransaction PTransaction)
        {
            try
            {
                spSelectPURCHASE_DETAIL mPurchaseDetail = new spSelectPURCHASE_DETAIL();
                mPurchaseDetail.Connection = PConnection;
                mPurchaseDetail.Transaction = PTransaction;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
        }

        /// <summary>
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Posting">Posting</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage  Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, int p_User_Id, int p_Posting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.USER_ID = p_User_Id;
                mPurchaseMaster.POSTING = p_Posting;
                DataTable dt = mPurchaseMaster.ExecuteTable();
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


        /// <summary>
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="P_DocumentDate">Date</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, DateTime P_DocumentDate)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.DOCUMENT_DATE = P_DocumentDate;
                DataTable dt = mPurchaseMaster.ExecuteTable();
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

        /// <summary>
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, int p_User_Id, int p_Posting, int p_SOLD_TO)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.USER_ID = Constants.IntNullValue;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                DataTable dt = mPurchaseMaster.ExecuteTable();
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

        /// <summary>
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPurchaseDetail(int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_DETAIL mPurchaseDetail = new spSelectPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
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

        public DataTable SelectPrincipalOpening(int p_DISTRIBUTOR_ID, int pPrincipalId)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.ORDER_NUMBER = "opng";
                mPurchaseMaster.SOLD_FROM = pPrincipalId;
                mPurchaseMaster.TYPE_ID = 0;
                DataTable dt = mPurchaseMaster.ExecuteTable();
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

        #endregion

        #region Insert, Update


        /// <summary>
        /// Inserts Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_ORDER_NUMBER">DocumentNo</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <param name="p_SOLD_FROM">SoldFrom</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_IS_DELETE">IsDeleted</param>
        /// <param name="dtPurchaseDetail">PurchaseDetailDatatable</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_BuiltyNo">Builty</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_PrincipalId">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool InsertPurchaseDocument(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE
            , int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting
            , string p_BuiltyNo, int p_UserId, int p_PrincipalId, DataTable dtConfig, bool IsFinanceSetting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = "N/A";
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = 0;
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.UOM_ID = int.Parse(dr["UOM_ID"].ToString());
                    mPurchaseDetail.STOCK_UNIT_QTY = decimal.Parse(dr["S_QUANTITY"].ToString());

                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = decimal.Parse(dr["S_QUANTITY"].ToString());
                    mStockUpdate.PRICE = mPurchaseDetail.PRICE;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.UOM_ID = int.Parse(dr["S_UOM_ID"].ToString());
                    mStockUpdate.ExecuteQuery();
                }
                if (IsFinanceSetting)
                {
                    if (p_TYPE_ID != Constants.Document_Opening)
                    {
                        LedgerController LController = new LedgerController();

                        #region GL Master, Detail

                        string VoucherNo2 = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE);

                        DataRow[] drConfig = null;



                        if (p_TYPE_ID == Constants.Document_Damaged)
                        {
                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.Inventoryatstore + "'");
                            long Inventoryatstore = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.StockDamage + "'");
                            long StockDamage = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            if (LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_PrincipalId, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Damaged, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Damage Stock Voucher", p_UserId, "Damage", mTransaction, mConnection))
                            {

                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, Inventoryatstore, 0, p_TOTAL_AMOUNT, "Damage Stock Voucher", mTransaction, mConnection);
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, StockDamage, 0, p_TOTAL_AMOUNT, "Account(s) Payable " + "Damage Stock Voucher", mTransaction, mConnection);

                            }
                        }

                        else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                        {
                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.Inventoryatstore + "'");
                            long Inventoryatstore = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.StockInTransit + "'");
                            long StockInTransit = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            if (LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_PrincipalId, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Transfer_Out, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Transfer Out Voucher", p_UserId, "Transfer Out", mTransaction, mConnection))
                            {
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, Inventoryatstore, 0, p_TOTAL_AMOUNT, "Transfer Out Voucher", mTransaction, mConnection);
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, StockInTransit, p_TOTAL_AMOUNT, 0, "Account(s) Payable Transfer Out Voucher", mTransaction, mConnection);

                            }
                        }
                        else if (p_TYPE_ID == Constants.Document_Transfer_In)
                        {
                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.Inventoryatstore + "'");
                            long Inventoryatstore = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.StockInTransit + "'");
                            long StockInTransit = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            if (LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_PrincipalId, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Transfer_In, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Transfer In Voucher", p_UserId, "Transfer In", mTransaction, mConnection))
                            {
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, Inventoryatstore, p_TOTAL_AMOUNT, 0, "Transfer In Voucher", mTransaction, mConnection);
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, StockInTransit, 0, p_TOTAL_AMOUNT, "Account(s) Payable Transfer In Voucher", mTransaction, mConnection);

                            }
                        }

                        else if (p_TYPE_ID == Constants.Document_Short)
                        {
                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.StockinTrade + "'");
                            long StockinTrade = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.ShortExcessstock + "'");
                            long ShortExcessstock = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            if (LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_PrincipalId, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Short, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Short Voucher", p_UserId, "Short", mTransaction, mConnection))
                            {
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, StockinTrade, 0, p_TOTAL_AMOUNT, "Short Voucher", mTransaction, mConnection);
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, ShortExcessstock, p_TOTAL_AMOUNT, 0, "Account(s) Payable Short Voucher", mTransaction, mConnection);

                            }
                        }
                        else if (p_TYPE_ID == Constants.Document_Acess)
                        {
                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.StockinTrade + "'");
                            long StockinTrade = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.ShortExcessstock + "'");
                            long ShortExcessstock = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            if (LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_PrincipalId, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Acess, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Excess Voucher", p_UserId, "Excess", mTransaction, mConnection))
                            {
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, ShortExcessstock, 0, p_TOTAL_AMOUNT, "Excess Voucher", mTransaction, mConnection);
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, StockinTrade, p_TOTAL_AMOUNT, 0, "Account(s) Payable Excess Voucher", mTransaction, mConnection);

                            }
                        }
                        #endregion
                    }
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        //insert purchase and purchase return
        public long InsertPurchase(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM
            , decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId
            , decimal p_GST_AMOUNT, decimal p_DISCOUNT, decimal p_NET_AMOUNT, DataTable dtConfig, bool IsFinanceSetting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.GST_AMOUNT = p_GST_AMOUNT;
                mPurchaseMaster.DISCOUNT = p_DISCOUNT;
                mPurchaseMaster.NET_AMOUNT = p_NET_AMOUNT;
                mPurchaseMaster.DEBIT_AMOUNT = p_NET_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = "N/A";
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = 0;
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.UOM_ID = int.Parse(dr["UOM_ID"].ToString());
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.STOCK_UNIT_QTY = decimal.Parse(dr["S_Quantity"].ToString());

                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = decimal.Parse(dr["S_Quantity"].ToString());
                    mStockUpdate.PRICE = mPurchaseDetail.PRICE;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.UOM_ID = int.Parse(dr["S_UOM_ID"].ToString());
                    mStockUpdate.ExecuteQuery();
                }

                    #region Vendor Ledger

                    LedgerController LController = new LedgerController();
                    // Configuration.GetAccountHead();

                    string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, 1);

                    #endregion

                    #region GL Master, Detail

                    string VoucherNo2 = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE);

                    DataRow[] drConfig = null;

                    drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.Inventoryatstore + "'");
                    long PurchaseAccount = Convert.ToInt64(drConfig[0]["VALUE"].ToString());


                    drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.AccountPayable + "'");
                    long PayableAccount = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                    drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.PurchaseDiscount + "'");
                    long PurchaseDiscount = Convert.ToInt64(drConfig[0]["VALUE"].ToString());


                if (p_TYPE_ID == Constants.Document_Purchase)
                {
                    LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), PurchaseAccount, p_DISTRIBUTOR_ID, 0, p_NET_AMOUNT, mPurchaseMaster.DOCUMENT_DATE, "Purchase Default", DateTime.Now, p_PrincipalId, mPurchaseMaster.PURCHASE_MASTER_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), p_TYPE_ID, p_UserId, mTransaction, mConnection, Constants.CreditSale, "Purchase");
                    LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), PayableAccount, p_DISTRIBUTOR_ID, p_NET_AMOUNT, 0, mPurchaseMaster.DOCUMENT_DATE, "Purchase Default", DateTime.Now, p_PrincipalId, mPurchaseMaster.PURCHASE_MASTER_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), p_TYPE_ID, p_UserId, mTransaction, mConnection, Constants.CreditSale, "Purchase");
                    if (IsFinanceSetting)
                    {
                        if (LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_PrincipalId, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Purchase, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Purchase Voucher", p_UserId, "Purchase", mTransaction, mConnection))
                        {

                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, PurchaseAccount, p_TOTAL_AMOUNT, 0, "Purchase Voucher", mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, PayableAccount, 0, p_TOTAL_AMOUNT - p_DISCOUNT, "Account(s) Payable " + "Purchase Voucher", mTransaction, mConnection);
                            if (p_DISCOUNT > 0)
                            {
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, PurchaseDiscount, 0, p_DISCOUNT, "Account(s) Payable Discount" + "Purchase Voucher", mTransaction, mConnection);
                            }
                        }
                    }
                }

                else if (p_TYPE_ID == Constants.Document_Purchase_Return)
                {
                    LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), PurchaseAccount, p_DISTRIBUTOR_ID, p_NET_AMOUNT, 0, mPurchaseMaster.DOCUMENT_DATE, "Purchase Return Default", DateTime.Now, p_PrincipalId, mPurchaseMaster.PURCHASE_MASTER_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), p_TYPE_ID, p_UserId, mTransaction, mConnection, Constants.CreditSale, "Purchase Return");
                    LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), PayableAccount, p_DISTRIBUTOR_ID, 0, p_NET_AMOUNT, mPurchaseMaster.DOCUMENT_DATE, "Purchase Return Default", DateTime.Now, p_PrincipalId, mPurchaseMaster.PURCHASE_MASTER_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), p_TYPE_ID, p_UserId, mTransaction, mConnection, Constants.CreditSale, "Purchase Return");
                    if (IsFinanceSetting)
                    {
                        if (LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_PrincipalId, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Purchase_Return, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Purchase Return Voucher", p_UserId, "Purchase Return", mTransaction, mConnection))
                        {
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, PurchaseAccount, 0, p_TOTAL_AMOUNT, "Purchase Return Voucher", mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, PayableAccount, p_TOTAL_AMOUNT - p_DISCOUNT, 0, "Account(s) Payable Purchase Return Voucher", mTransaction, mConnection);

                            if (p_DISCOUNT > 0)
                            {
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, PurchaseDiscount, p_DISCOUNT, 0, "Account(s) Payable Discount" + "Purchase Return Voucher", mTransaction, mConnection);
                            }
                        }
                    }
                }

                    #endregion
                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_MASTER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return Constants.LongNullValue;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool InsertInvoiceBooking(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM
            , decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId
            , decimal p_GST_AMOUNT, decimal p_DISCOUNT, decimal p_NET_AMOUNT, string Remarks, DataTable dtConfig, bool IsFinanceIntegrate)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.GST_AMOUNT = p_GST_AMOUNT;
                mPurchaseMaster.DISCOUNT = p_DISCOUNT;
                mPurchaseMaster.NET_AMOUNT = p_NET_AMOUNT;
                mPurchaseMaster.DEBIT_AMOUNT = p_NET_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.REMARKS = Remarks;
                mPurchaseMaster.ExecuteQuery();

                if (IsFinanceIntegrate)
                {
                    LedgerController LController = new LedgerController();

                    DataRow[] drConfig = null;

                    drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.Inventoryatstore + "'");
                    long PurchaseAccount = Convert.ToInt64(drConfig[0]["VALUE"].ToString());


                    drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.AccountPayable + "'");
                    long PayableAccount = Convert.ToInt64(drConfig[0]["VALUE"].ToString());


                    string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, 1);

                    LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), PurchaseAccount, p_DISTRIBUTOR_ID, 0, p_NET_AMOUNT, mPurchaseMaster.DOCUMENT_DATE, "Purchase Default", DateTime.Now, p_PrincipalId, mPurchaseMaster.PURCHASE_MASTER_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), p_TYPE_ID, p_UserId, mTransaction, mConnection, Constants.CreditSale, "Booking Purchase");
                    LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), PayableAccount, p_DISTRIBUTOR_ID, p_NET_AMOUNT, 0, mPurchaseMaster.DOCUMENT_DATE, "Purchase Default", DateTime.Now, p_PrincipalId, mPurchaseMaster.PURCHASE_MASTER_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), p_TYPE_ID, p_UserId, mTransaction, mConnection, Constants.CreditSale, "Booking Purchase");

                    string VoucherNo2 = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE);


                    if (LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_PrincipalId, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Purchase, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Booking Purchase Voucher", p_UserId, "Booking Purchase", mTransaction, mConnection))
                    {
                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, PurchaseAccount, p_NET_AMOUNT, 0, "Booking Purchase Voucher", mTransaction, mConnection);
                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, PayableAccount, 0, p_NET_AMOUNT, "Account(s) Payable " + "Booking Purchase Voucher", mTransaction, mConnection);
                    }
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
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
        /// Updates Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_ORDER_NUMBER">DocumentNo</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <param name="p_SOLD_FROM">SoldFrom</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_IS_DELETE">IsDeleted</param>
        /// <param name="dtPurchaseDetail">PurchaseDetailDatatable</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_BuiltyNo">Builty</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_Principal">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool UpdatePurchaseDocument(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE
            , int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo
            , int p_UserId, int p_PrincipalId, DataTable dtConfig, bool IsFinanceSetting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = "N/A";
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = 0;
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.UOM_ID = int.Parse(dr["UOM_ID"].ToString());
                    mPurchaseDetail.STOCK_UNIT_QTY = decimal.Parse(dr["S_QUANTITY"].ToString());

                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = decimal.Parse(dr["QUANTITY"].ToString());
                    mStockUpdate.PRICE = mPurchaseDetail.PRICE;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.UOM_ID = int.Parse(dr["S_UOM_ID"].ToString());
                    mStockUpdate.ExecuteQuery();
                }
                if (IsFinanceSetting)
                {
                    if (p_TYPE_ID != Constants.Document_Opening)
                    {
                        #region GL Master, Detail

                        LedgerController LController = new LedgerController();

                        spDeleteGL_MASTER2 mDelete = new spDeleteGL_MASTER2();

                        mDelete.Connection = mConnection;
                        mDelete.Transaction = mTransaction;
                        mDelete.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                        mDelete.VOUCHER_TYPE_ID = Constants.Journal_Voucher;
                        mDelete.PAYEES_NAME = Convert.ToString(p_PURCHASE_MASTER_ID);
                        mDelete.TYPE_ID = p_TYPE_ID;

                        mDelete.ExecuteQuery();


                        UspSelectMaxVoucherNo mMaxDNo2 = new UspSelectMaxVoucherNo();
                        mMaxDNo2.Connection = mConnection;
                        mMaxDNo2.Transaction = mTransaction;

                        mMaxDNo2.Document_TypeId = Constants.Journal_Voucher;
                        mMaxDNo2.Distributor_id = p_DISTRIBUTOR_ID;
                        mMaxDNo2.Month = p_DOCUMENT_DATE;
                        DateTime mDate = p_DOCUMENT_DATE;
                        DataTable MaxId2 = mMaxDNo2.ExecuteTable();
                        string MaxVoucherId = MaxId2.Rows[0][0].ToString();

                        if (MaxVoucherId.Length == 1)
                        {
                            if (mDate.Month.ToString().Length == 1)
                            {
                                MaxVoucherId = "0" + mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-000" + MaxVoucherId;
                            }
                            else
                            {
                                MaxVoucherId = mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-000" + MaxVoucherId;
                            }

                        }
                        else if (MaxVoucherId.Length == 2)
                        {
                            if (mDate.Month.ToString().Length == 1)
                            {
                                MaxVoucherId = MaxVoucherId = "0" + mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-00" + MaxVoucherId;
                            }
                            else
                            {
                                MaxVoucherId = mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-00" + MaxVoucherId;
                            }

                        }
                        else if (MaxVoucherId.Length == 3)
                        {
                            if (mDate.Month.ToString().Length == 1)
                            {
                                MaxVoucherId = "0" + mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-0" + MaxVoucherId;
                            }
                            else
                            {
                                MaxVoucherId = mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-0" + MaxVoucherId;
                            }

                        }
                        else
                        {
                            if (mDate.Month.ToString().Length == 1)
                            {
                                MaxVoucherId = "0" + mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-" + MaxVoucherId;
                            }
                            else
                            {
                                MaxVoucherId = mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-" + MaxVoucherId;
                            }
                        }
                        string VoucherNo2 = MaxVoucherId;

                        DataRow[] drConfig = null;



                        if (p_TYPE_ID == Constants.Document_Damaged)
                        {
                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.Inventoryatstore + "'");
                            long Inventoryatstore = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.StockDamage + "'");
                            long StockDamage = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            if (LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_PrincipalId, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Damaged, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Damage Stock Voucher", p_UserId, "Damage", mTransaction, mConnection))
                            {

                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, Inventoryatstore, 0, p_TOTAL_AMOUNT, "Damage Stock Voucher", mTransaction, mConnection);
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, StockDamage, 0, p_TOTAL_AMOUNT, "Account(s) Payable " + "Damage Stock Voucher", mTransaction, mConnection);

                            }
                        }

                        else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                        {
                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.Inventoryatstore + "'");
                            long Inventoryatstore = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.StockInTransit + "'");
                            long StockInTransit = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            if (LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_PrincipalId, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Transfer_Out, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Transfer Out Voucher", p_UserId, "Transfer Out", mTransaction, mConnection))
                            {
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, Inventoryatstore, 0, p_TOTAL_AMOUNT, "Transfer Out Voucher", mTransaction, mConnection);
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, StockInTransit, p_TOTAL_AMOUNT, 0, "Account(s) Payable Transfer Out Voucher", mTransaction, mConnection);

                            }
                        }
                        else if (p_TYPE_ID == Constants.Document_Short)
                        {
                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.StockinTrade + "'");
                            long StockinTrade = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.ShortExcessstock + "'");
                            long ShortExcessstock = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            if (LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_PrincipalId, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Short, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Short Voucher", p_UserId, "Short", mTransaction, mConnection))
                            {
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, StockinTrade, 0, p_TOTAL_AMOUNT, "Short Voucher", mTransaction, mConnection);
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, ShortExcessstock, p_TOTAL_AMOUNT, 0, "Account(s) Payable Short Voucher", mTransaction, mConnection);

                            }
                        }
                        else if (p_TYPE_ID == Constants.Document_Acess)
                        {
                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.StockinTrade + "'");
                            long StockinTrade = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.ShortExcessstock + "'");
                            long ShortExcessstock = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                            if (LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_PrincipalId, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Acess, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Excess Voucher", p_UserId, "Excess", mTransaction, mConnection))
                            {
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, ShortExcessstock, 0, p_TOTAL_AMOUNT, "Excess Voucher", mTransaction, mConnection);
                                LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, StockinTrade, p_TOTAL_AMOUNT, 0, "Account(s) Payable Excess Voucher", mTransaction, mConnection);

                            }
                        }
                        #endregion
                    }
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        //update purchase and purchase return
        public bool UpdatePurchase(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO
            , int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal
            , decimal p_GST_AMOUNT, decimal p_DISCOUNT, decimal p_NET_AMOUNT, DataTable dtConfig, bool IsFinanceSetting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.GST_AMOUNT = p_GST_AMOUNT;
                mPurchaseMaster.DISCOUNT = p_DISCOUNT;
                mPurchaseMaster.NET_AMOUNT = p_NET_AMOUNT;
                mPurchaseMaster.DEBIT_AMOUNT = p_NET_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();


                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;

                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = "N/A";
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = 0;
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.UOM_ID = int.Parse(dr["UOM_ID"].ToString());
                    mPurchaseDetail.STOCK_UNIT_QTY = decimal.Parse(dr["S_Quantity"].ToString());

                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = decimal.Parse(dr["QUANTITY"].ToString());
                    mStockUpdate.PRICE = mPurchaseDetail.PRICE;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.UOM_ID = int.Parse(dr["S_UOM_ID"].ToString());
                    mStockUpdate.ExecuteQuery();
                }
                if (IsFinanceSetting)
                {
                    #region Vendor Ledger

                    LedgerController LController = new LedgerController();


                    UspSelectMaxDocumentNo mMaxLNo = new UspSelectMaxDocumentNo();
                    mMaxLNo.Connection = mConnection;
                    mMaxLNo.Transaction = mTransaction;
                    mMaxLNo.Document_TypeId = Constants.Journal_Voucher;
                    mMaxLNo.Distributor_id = mPurchaseMaster.DISTRIBUTOR_ID;
                    mMaxLNo.TypeId = 1;
                    DataTable MaxLedgerId = mMaxLNo.ExecuteTable();
                    string VoucherNo = MaxLedgerId.Rows[0][0].ToString();

                    DataRow[] drConfig = null;

                    drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.Inventoryatstore + "'");
                    long PurchaseAccount = Convert.ToInt64(drConfig[0]["VALUE"].ToString());


                    drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.AccountPayable + "'");
                    long PayableAccount = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                    drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.PurchaseDiscount + "'");
                    long PurchaseDiscount = Convert.ToInt64(drConfig[0]["VALUE"].ToString());


                    if (p_TYPE_ID == 2)
                    {
                        LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), PurchaseAccount, p_DISTRIBUTOR_ID, 0, p_NET_AMOUNT, mPurchaseMaster.DOCUMENT_DATE, "Purchase Default", DateTime.Now, p_Principal, mPurchaseMaster.PURCHASE_MASTER_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), p_TYPE_ID, p_UserId, mTransaction, mConnection, Constants.CreditSale, "Purchase");
                        LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), PayableAccount, p_DISTRIBUTOR_ID, p_NET_AMOUNT, 0, mPurchaseMaster.DOCUMENT_DATE, "Purchase Default", DateTime.Now, p_Principal, mPurchaseMaster.PURCHASE_MASTER_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), p_TYPE_ID, p_UserId, mTransaction, mConnection, Constants.CreditSale, "Purchase");
                    }
                    else if (p_TYPE_ID == 3)
                    {
                        LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), PurchaseAccount, p_DISTRIBUTOR_ID, p_NET_AMOUNT, 0, mPurchaseMaster.DOCUMENT_DATE, "Purchase Return Default", DateTime.Now, p_Principal, mPurchaseMaster.PURCHASE_MASTER_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), p_TYPE_ID, p_UserId, mTransaction, mConnection, Constants.CreditSale, "Purchase Return");
                        LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), PayableAccount, p_DISTRIBUTOR_ID, 0, p_NET_AMOUNT, mPurchaseMaster.DOCUMENT_DATE, "Purchase Return Default", DateTime.Now, p_Principal, mPurchaseMaster.PURCHASE_MASTER_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), p_TYPE_ID, p_UserId, mTransaction, mConnection, Constants.CreditSale, "Purchase Return");

                    }
                    #endregion

                    #region GLMaster, Detail

                    spDeleteGL_MASTER2 mDelete = new spDeleteGL_MASTER2();

                    mDelete.Connection = mConnection;
                    mDelete.Transaction = mTransaction;
                    mDelete.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mDelete.VOUCHER_TYPE_ID = Constants.Journal_Voucher;
                    mDelete.PAYEES_NAME = Convert.ToString(p_PURCHASE_MASTER_ID);
                    mDelete.TYPE_ID = p_TYPE_ID;

                    mDelete.ExecuteQuery();


                    UspSelectMaxVoucherNo mMaxDNo2 = new UspSelectMaxVoucherNo();
                    mMaxDNo2.Connection = mConnection;
                    mMaxDNo2.Transaction = mTransaction;

                    mMaxDNo2.Document_TypeId = Constants.Journal_Voucher;
                    mMaxDNo2.Distributor_id = p_DISTRIBUTOR_ID;
                    mMaxDNo2.Month = p_DOCUMENT_DATE;
                    DateTime mDate = p_DOCUMENT_DATE;
                    DataTable MaxId2 = mMaxDNo2.ExecuteTable();
                    string MaxVoucherId = MaxId2.Rows[0][0].ToString();

                    if (MaxVoucherId.Length == 1)
                    {
                        if (mDate.Month.ToString().Length == 1)
                        {
                            MaxVoucherId = "0" + mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-000" + MaxVoucherId;
                        }
                        else
                        {
                            MaxVoucherId = mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-000" + MaxVoucherId;
                        }

                    }
                    else if (MaxVoucherId.Length == 2)
                    {
                        if (mDate.Month.ToString().Length == 1)
                        {
                            MaxVoucherId = MaxVoucherId = "0" + mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-00" + MaxVoucherId;
                        }
                        else
                        {
                            MaxVoucherId = mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-00" + MaxVoucherId;
                        }

                    }
                    else if (MaxVoucherId.Length == 3)
                    {
                        if (mDate.Month.ToString().Length == 1)
                        {
                            MaxVoucherId = "0" + mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-0" + MaxVoucherId;
                        }
                        else
                        {
                            MaxVoucherId = mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-0" + MaxVoucherId;
                        }

                    }
                    else
                    {
                        if (mDate.Month.ToString().Length == 1)
                        {
                            MaxVoucherId = "0" + mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-" + MaxVoucherId;
                        }
                        else
                        {
                            MaxVoucherId = mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-" + MaxVoucherId;
                        }
                    }
                    string VoucherNo2 = MaxVoucherId;


                    if (p_TYPE_ID == Constants.Document_Purchase)
                    {
                        LController.PostingGLMaster(p_DISTRIBUTOR_ID, 0, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Purchase, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Purchase Voucher", p_UserId, "Purchase", mTransaction, mConnection);

                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo2, PurchaseAccount, p_TOTAL_AMOUNT, 0, "Purchase Voucher", mTransaction, mConnection);
                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo2, PayableAccount, 0, p_TOTAL_AMOUNT - p_DISCOUNT, "Vendors(s) Payable " + "Purchase Voucher", mTransaction, mConnection);

                        if (p_DISCOUNT > 0)
                        {
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo2, PurchaseDiscount, 0, p_DISCOUNT, "Vendors(s) Payable Discount" + "Purchase Voucher", mTransaction, mConnection);
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Purchase_Return)
                    {
                        LController.PostingGLMaster(p_DISTRIBUTOR_ID, 0, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Purchase_Return, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Purchase Return Voucher", p_UserId, "Purchase Return", mTransaction, mConnection);

                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo2, PurchaseAccount, 0, p_TOTAL_AMOUNT, "Purchase Return Voucher", mTransaction, mConnection);
                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo2, PayableAccount, p_TOTAL_AMOUNT - p_DISCOUNT, 0, "Vendors(s) Payable " + "Purchase Return Voucher", mTransaction, mConnection);
                        if (p_DISCOUNT > 0)
                        {
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo2, PurchaseDiscount, p_DISCOUNT, 0, "Vendors(s) Payable Discount" + "Purchase Return Voucher", mTransaction, mConnection);
                        }
                    }

                    #endregion
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateInvoiceBooking(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO
            , int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal
            , decimal p_GST_AMOUNT, decimal p_DISCOUNT, decimal p_NET_AMOUNT, string Remarks, DataTable dtConfig, bool IsFinanceSetting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.GST_AMOUNT = p_GST_AMOUNT;
                mPurchaseMaster.DISCOUNT = p_DISCOUNT;
                mPurchaseMaster.NET_AMOUNT = p_NET_AMOUNT;
                mPurchaseMaster.DEBIT_AMOUNT = p_NET_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.REMARKS = Remarks;
                mPurchaseMaster.ExecuteQuery();

                if (IsFinanceSetting)
                {
                    #region Vendor Ledger

                    LedgerController LController = new LedgerController();


                    UspSelectMaxDocumentNo mMaxLNo = new UspSelectMaxDocumentNo();
                    mMaxLNo.Connection = mConnection;
                    mMaxLNo.Transaction = mTransaction;
                    mMaxLNo.Document_TypeId = Constants.Journal_Voucher;
                    mMaxLNo.Distributor_id = mPurchaseMaster.DISTRIBUTOR_ID;
                    mMaxLNo.TypeId = 1;
                    DataTable MaxLedgerId = mMaxLNo.ExecuteTable();
                    string VoucherNo = MaxLedgerId.Rows[0][0].ToString();

                    DataRow[] drConfig = null;

                    drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.Inventoryatstore + "'");
                    long PurchaseAccount = Convert.ToInt64(drConfig[0]["VALUE"].ToString());


                    drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.AccountPayable + "'");
                    long PayableAccount = Convert.ToInt64(drConfig[0]["VALUE"].ToString());
                    
                    
                        LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), PurchaseAccount, p_DISTRIBUTOR_ID, 0, p_NET_AMOUNT, mPurchaseMaster.DOCUMENT_DATE, "Booking Purchase Default", DateTime.Now, p_Principal, mPurchaseMaster.PURCHASE_MASTER_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), p_TYPE_ID, p_UserId, mTransaction, mConnection, Constants.CreditSale, "Booking Purchase");
                        LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), PayableAccount, p_DISTRIBUTOR_ID, p_NET_AMOUNT, 0, mPurchaseMaster.DOCUMENT_DATE, "Booking Purchase Default", DateTime.Now, p_Principal, mPurchaseMaster.PURCHASE_MASTER_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), p_TYPE_ID, p_UserId, mTransaction, mConnection, Constants.CreditSale, "Booking Purchase");
                    
                   
                    #endregion

                    #region GLMaster, Detail

                    spDeleteGL_MASTER2 mDelete = new spDeleteGL_MASTER2();

                    mDelete.Connection = mConnection;
                    mDelete.Transaction = mTransaction;
                    mDelete.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mDelete.VOUCHER_TYPE_ID = Constants.Journal_Voucher;
                    mDelete.PAYEES_NAME = Convert.ToString(p_PURCHASE_MASTER_ID);
                    mDelete.TYPE_ID = p_TYPE_ID;

                    mDelete.ExecuteQuery();


                    UspSelectMaxVoucherNo mMaxDNo2 = new UspSelectMaxVoucherNo();
                    mMaxDNo2.Connection = mConnection;
                    mMaxDNo2.Transaction = mTransaction;

                    mMaxDNo2.Document_TypeId = Constants.Journal_Voucher;
                    mMaxDNo2.Distributor_id = p_DISTRIBUTOR_ID;
                    mMaxDNo2.Month = p_DOCUMENT_DATE;
                    DateTime mDate = p_DOCUMENT_DATE;
                    DataTable MaxId2 = mMaxDNo2.ExecuteTable();
                    string MaxVoucherId = MaxId2.Rows[0][0].ToString();

                    if (MaxVoucherId.Length == 1)
                    {
                        if (mDate.Month.ToString().Length == 1)
                        {
                            MaxVoucherId = "0" + mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-000" + MaxVoucherId;
                        }
                        else
                        {
                            MaxVoucherId = mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-000" + MaxVoucherId;
                        }

                    }
                    else if (MaxVoucherId.Length == 2)
                    {
                        if (mDate.Month.ToString().Length == 1)
                        {
                            MaxVoucherId = MaxVoucherId = "0" + mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-00" + MaxVoucherId;
                        }
                        else
                        {
                            MaxVoucherId = mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-00" + MaxVoucherId;
                        }

                    }
                    else if (MaxVoucherId.Length == 3)
                    {
                        if (mDate.Month.ToString().Length == 1)
                        {
                            MaxVoucherId = "0" + mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-0" + MaxVoucherId;
                        }
                        else
                        {
                            MaxVoucherId = mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-0" + MaxVoucherId;
                        }

                    }
                    else
                    {
                        if (mDate.Month.ToString().Length == 1)
                        {
                            MaxVoucherId = "0" + mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-" + MaxVoucherId;
                        }
                        else
                        {
                            MaxVoucherId = mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-" + MaxVoucherId;
                        }
                    }

                    string VoucherNo2 = MaxVoucherId;
                    
                        LController.PostingGLMaster(p_DISTRIBUTOR_ID, 0, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Purchase, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Booking Purchase Voucher", p_UserId, "Booking Purchase", mTransaction, mConnection);

                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo2, PurchaseAccount, p_TOTAL_AMOUNT, 0, "Booking Purchase Voucher", mTransaction, mConnection);
                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo2, PayableAccount, 0, p_TOTAL_AMOUNT - p_DISCOUNT, "Account(s) Payable " + "Booking  Purchase Voucher", mTransaction, mConnection);

                       
                    #endregion
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
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
        /// Posts Pending Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_Type_Id">Type</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Posting">Posting</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool PostPendingDocument(long p_PURCHASE_MASTER_ID, int p_Type_Id, int p_Distributor_Id, int p_Posting, int pUserId)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_Distributor_Id;
                mPurchaseMaster.TYPE_ID = p_Type_Id;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.USER_ID = pUserId;
                mPurchaseMaster.ExecuteQuery();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
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
        /// Inserts Or Updates SKU Price From Excel File
        /// </summary>
        /// Returns True On Success And False On Failure
        /// <param name="p_DistributorId">Location</param>
        /// <param name="pFileName">ExcelFile</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool ImportOpeningStock(int p_DISTRIBUTOR_ID, string pFileName, int p_PRINCIPAL_ID, DateTime p_DOCUMENT_DATE, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            FileStream Sourcefile = null;
            StreamReader ReadSourceFile = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = 7;//Opening Stock
                mPurchaseMaster.ORDER_NUMBER = "";
                mPurchaseMaster.SOLD_FROM = p_PRINCIPAL_ID;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TOTAL_AMOUNT = 0;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = 0;
                mPurchaseMaster.BUILTY_NO = "";
                mPurchaseMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mPurchaseMaster.ExecuteQuery();


                Sourcefile = new FileStream(pFileName, FileMode.Open);
                ReadSourceFile = new StreamReader(Sourcefile);
                string FileContents = "";
                while ((FileContents = ReadSourceFile.ReadLine()) != null)
                {

                    string[] ParametersArr = FileContents.Split(Constants.File_Delimiter);
                    spSelectSKUS mSKUS = new spSelectSKUS();
                    mSKUS.Connection = mConnection;
                    mSKUS.Transaction = mTransaction;
                    mSKUS.SKU_CODE = ParametersArr[0].ToString();
                    mSKUS.ISACTIVE = true;
                    DataTable dt = mSKUS.ExecuteTable();
                    if (dt.Rows.Count > 0)
                    {
                        spInsertPURCHASE_DETAIL mStockDetail = new spInsertPURCHASE_DETAIL();
                        mStockDetail.Connection = mConnection;
                        mStockDetail.Transaction = mTransaction;
                        mStockDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                        mStockDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                        mStockDetail.SKU_ID = int.Parse(dt.Rows[0]["SKU_ID"].ToString());
                        mStockDetail.BATCH_NO = "";
                        mStockDetail.PRICE = decimal.Parse(ParametersArr[2].ToString());
                        mStockDetail.QUANTITY = int.Parse(ParametersArr[1].ToString());
                        mStockDetail.FREE_SKU = 0;
                        mStockDetail.AMOUNT = decimal.Parse(ParametersArr[3].ToString());
                        mStockDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                        mStockDetail.TIME_STAMP = p_DOCUMENT_DATE;
                        mStockDetail.ExecuteQuery();

                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.PRINCIPAL_ID = p_PRINCIPAL_ID;
                        mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                        mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                        mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                        mStockUpdate.SKU_ID = mStockDetail.SKU_ID;
                        mStockUpdate.STOCK_QTY = mStockDetail.QUANTITY;
                        mStockUpdate.FREE_QTY = mStockDetail.FREE_SKU;
                        mStockUpdate.BATCHNO = mStockDetail.BATCH_NO;
                        // mStockUpdate.UOM_ID = int.Parse(dr["UOM_ID"].ToString());
                        mStockUpdate.ExecuteQuery();
                    }

                }
                mTransaction.Commit();
                return true;
            }

            catch (Exception excp)
            {
                mTransaction.Rollback();
                ReadSourceFile.Close();
                mConnection.Close();
                //    ExceptionPublisher.PublishException(excp);
                //    throw;
                return false;
            }
            finally
            {
                ReadSourceFile.Close();
                mConnection.Close();
            }
        }

        public bool ImportPurchaseStock(int p_DISTRIBUTOR_ID, string pFileName, int p_PRINCIPAL_ID, DateTime p_DOCUMENT_DATE, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            FileStream Sourcefile = null;
            StreamReader ReadSourceFile = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = 2;//Purchase Stock
                mPurchaseMaster.ORDER_NUMBER = "";
                mPurchaseMaster.SOLD_FROM = p_PRINCIPAL_ID;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TOTAL_AMOUNT = 0;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = 0;
                mPurchaseMaster.BUILTY_NO = "";
                mPurchaseMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mPurchaseMaster.ExecuteQuery();


                Sourcefile = new FileStream(pFileName, FileMode.Open);
                ReadSourceFile = new StreamReader(Sourcefile);
                string FileContents = "";
                while ((FileContents = ReadSourceFile.ReadLine()) != null)
                {

                    string[] ParametersArr = FileContents.Split(Constants.File_Delimiter);
                    spSelectSKUS mSKUS = new spSelectSKUS();
                    mSKUS.Connection = mConnection;
                    mSKUS.Transaction = mTransaction;
                    mSKUS.SKU_CODE = ParametersArr[0].ToString();
                    mSKUS.ISACTIVE = true;
                    DataTable dt = mSKUS.ExecuteTable();
                    if (dt.Rows.Count > 0)
                    {
                        spInsertPURCHASE_DETAIL mStockDetail = new spInsertPURCHASE_DETAIL();
                        mStockDetail.Connection = mConnection;
                        mStockDetail.Transaction = mTransaction;
                        mStockDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                        mStockDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                        mStockDetail.SKU_ID = int.Parse(dt.Rows[0]["SKU_ID"].ToString());
                        mStockDetail.BATCH_NO = "";
                        mStockDetail.PRICE = decimal.Parse(ParametersArr[2].ToString());
                        mStockDetail.QUANTITY = int.Parse(ParametersArr[1].ToString());
                        mStockDetail.FREE_SKU = 0;
                        mStockDetail.AMOUNT = decimal.Parse(ParametersArr[3].ToString());
                        mStockDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                        mStockDetail.TIME_STAMP = p_DOCUMENT_DATE;
                        mStockDetail.ExecuteQuery();

                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.PRINCIPAL_ID = p_PRINCIPAL_ID;
                        mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                        mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                        mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                        mStockUpdate.SKU_ID = mStockDetail.SKU_ID;
                        mStockUpdate.STOCK_QTY = mStockDetail.QUANTITY;
                        mStockUpdate.FREE_QTY = mStockDetail.FREE_SKU;
                        mStockUpdate.BATCHNO = mStockDetail.BATCH_NO;
                        mStockUpdate.ExecuteQuery();
                    }

                }
                mTransaction.Commit();
                return true;
            }

            catch (Exception excp)
            {
                mTransaction.Rollback();
                ReadSourceFile.Close();
                mConnection.Close();
                // ExceptionPublisher.PublishException(excp);
                // throw;
                return false;
            }
            finally
            {
                ReadSourceFile.Close();
                mConnection.Close();
            }
        }

        #endregion

        #region Issuance

        public long InsertIssuance(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM
            , decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId
            , DataTable dtConfig, bool IsFinanceSetting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;
                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = "N/A";
                    mPurchaseDetail.PRICE = 0;
                    mPurchaseDetail.QUANTITY = 0;
                    mPurchaseDetail.FREE_SKU = decimal.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = 0;
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.UOM_ID = int.Parse(dr["UOM_ID"].ToString());
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.PRICE = mPurchaseDetail.PRICE;
                    mStockUpdate.FREE_QTY = decimal.Parse(dr["PS_QUANTITY"].ToString());
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.UOM_ID = int.Parse(dr["S_UOM_ID"].ToString());
                    mStockUpdate.ExecuteQuery();
                }

                if (IsFinanceSetting)
                {


                    #region GL Master, Detail

                    LedgerController LController = new LedgerController();

                    string VoucherNo2 = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE);

                    DataRow[] drConfig = null;



                    if (p_TYPE_ID == Constants.Document_Issuance)
                    {
                        drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.Inventoryatstore + "'");
                        long Inventoryatstore = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                        drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.StockInTransit + "'");
                        long StockInTransit = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                        if (LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_PrincipalId, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Issuance, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Issuance Voucher", p_UserId, "Issuance", mTransaction, mConnection))
                        {
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, Inventoryatstore, 0, p_TOTAL_AMOUNT, "Issuance Voucher", mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, StockInTransit, p_TOTAL_AMOUNT, 0, "Account(s) Payable Issuance Voucher", mTransaction, mConnection);

                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Issue_Return)
                    {
                        drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.Inventoryatstore + "'");
                        long Inventoryatstore = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                        drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.StockInTransit + "'");
                        long StockInTransit = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                        if (LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_PrincipalId, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Issue_Return, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Return Voucher", p_UserId, "Return", mTransaction, mConnection))
                        {
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, Inventoryatstore, p_TOTAL_AMOUNT, 0, "Return Voucher", mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_PrincipalId, Constants.Journal_Voucher, VoucherNo2, StockInTransit, 0, p_TOTAL_AMOUNT, "Account(s) Payable Return Voucher", mTransaction, mConnection);

                        }
                    }


                    #endregion
                }
                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_MASTER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return 0;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool UpdateIssuance(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE
            , int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo
            , int p_UserId, int p_Principal, DataTable dtConfig, bool IsFinanceSetting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());

                    mPurchaseDetail.BATCH_NO = "N/A";
                    mPurchaseDetail.PRICE = 0;
                    mPurchaseDetail.QUANTITY = 0;
                    mPurchaseDetail.FREE_SKU = decimal.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = 0;
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.UOM_ID = int.Parse(dr["UOM_ID"].ToString());
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = p_DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.PRICE = mPurchaseDetail.PRICE;
                    mStockUpdate.FREE_QTY = decimal.Parse(dr["PS_QUANTITY"].ToString());
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.UOM_ID = int.Parse(dr["S_UOM_ID"].ToString());
                    mStockUpdate.ExecuteQuery();
                }

                if (IsFinanceSetting)
                {

                    #region GLMaster, Detail

                    spDeleteGL_MASTER2 mDelete = new spDeleteGL_MASTER2();

                    mDelete.Connection = mConnection;
                    mDelete.Transaction = mTransaction;
                    mDelete.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mDelete.VOUCHER_TYPE_ID = Constants.Journal_Voucher;
                    mDelete.PAYEES_NAME = Convert.ToString(p_PURCHASE_MASTER_ID);
                    mDelete.TYPE_ID = p_TYPE_ID;

                    mDelete.ExecuteQuery();


                    UspSelectMaxVoucherNo mMaxDNo2 = new UspSelectMaxVoucherNo();
                    mMaxDNo2.Connection = mConnection;
                    mMaxDNo2.Transaction = mTransaction;

                    mMaxDNo2.Document_TypeId = Constants.Journal_Voucher;
                    mMaxDNo2.Distributor_id = p_DISTRIBUTOR_ID;
                    mMaxDNo2.Month = p_DOCUMENT_DATE;
                    DateTime mDate = p_DOCUMENT_DATE;
                    DataTable MaxId2 = mMaxDNo2.ExecuteTable();
                    string MaxVoucherId = MaxId2.Rows[0][0].ToString();

                    if (MaxVoucherId.Length == 1)
                    {
                        if (mDate.Month.ToString().Length == 1)
                        {
                            MaxVoucherId = "0" + mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-000" + MaxVoucherId;
                        }
                        else
                        {
                            MaxVoucherId = mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-000" + MaxVoucherId;
                        }

                    }
                    else if (MaxVoucherId.Length == 2)
                    {
                        if (mDate.Month.ToString().Length == 1)
                        {
                            MaxVoucherId = MaxVoucherId = "0" + mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-00" + MaxVoucherId;
                        }
                        else
                        {
                            MaxVoucherId = mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-00" + MaxVoucherId;
                        }

                    }
                    else if (MaxVoucherId.Length == 3)
                    {
                        if (mDate.Month.ToString().Length == 1)
                        {
                            MaxVoucherId = "0" + mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-0" + MaxVoucherId;
                        }
                        else
                        {
                            MaxVoucherId = mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-0" + MaxVoucherId;
                        }

                    }
                    else
                    {
                        if (mDate.Month.ToString().Length == 1)
                        {
                            MaxVoucherId = "0" + mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-" + MaxVoucherId;
                        }
                        else
                        {
                            MaxVoucherId = mDate.Month.ToString() + mDate.Year.ToString().Substring(2, 2) + "-" + MaxVoucherId;
                        }
                    }
                    string VoucherNo2 = MaxVoucherId;




                    DataRow[] drConfig = null;


                    LedgerController LController = new LedgerController();

                    if (p_TYPE_ID == Constants.Document_Issuance)
                    {
                        drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.Inventoryatstore + "'");
                        long Inventoryatstore = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                        drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.StockInTransit + "'");
                        long StockInTransit = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                        LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_Principal, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Issuance, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Issuance Voucher", p_UserId, "Issuance", mTransaction, mConnection);

                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo2, Inventoryatstore, 0, p_TOTAL_AMOUNT, "Issuance Voucher", mTransaction, mConnection);
                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo2, StockInTransit, p_TOTAL_AMOUNT, 0, "Account(s) Payable Issuance Voucher", mTransaction, mConnection);
                    }
                    else if (p_TYPE_ID == Constants.Document_Issue_Return)
                    {
                        drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.Inventoryatstore + "'");
                        long Inventoryatstore = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                        drConfig = dtConfig.Select("CODE = '" + (int)Enums.COAMapping.StockInTransit + "'");
                        long StockInTransit = Convert.ToInt64(drConfig[0]["VALUE"].ToString());

                        LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_Principal, VoucherNo2, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.Document_Issue_Return, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), "Return Voucher", p_UserId, "Return", mTransaction, mConnection);

                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo2, Inventoryatstore, p_TOTAL_AMOUNT, 0, "Return Voucher", mTransaction, mConnection);
                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo2, StockInTransit, 0, p_TOTAL_AMOUNT, "Account(s) Payable Return Voucher", mTransaction, mConnection);
                    }


                    #endregion
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
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
    }
}