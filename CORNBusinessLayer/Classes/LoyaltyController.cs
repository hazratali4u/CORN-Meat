using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;
using CORNDatabaseLayer.Classes;
using System;
using System.Data;

namespace CORNBusinessLayer.Classes
{
    public class LoyaltyController
    {
        public string InsertLoyaltyCard(int DistributorId, int CardTypeId, decimal Discont, decimal Purchasing, decimal Points, decimal AmountLimit
            , DateTime TimeStamp, DateTime LastUpdate, int UserId, DateTime DateCreated, string p_CardNo)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertLOYALTY_CARD mLoyaltyCard = new spInsertLOYALTY_CARD();
                mLoyaltyCard.Connection = mConnection;
                mLoyaltyCard.DISTIBUTOR_ID = DistributorId;
                mLoyaltyCard.CARD_TYPE_ID = CardTypeId;
                mLoyaltyCard.DISCOUNT = Discont;
                mLoyaltyCard.PURCHASING = Purchasing;
                mLoyaltyCard.POINTS = Points;
                mLoyaltyCard.AMOUNT_LIMIT = AmountLimit;
                mLoyaltyCard.IS_ACTIVE = true;
                mLoyaltyCard.TIME_STAMP = TimeStamp;
                mLoyaltyCard.LAST_UPDATE = LastUpdate;
                mLoyaltyCard.USER_ID = UserId;
                mLoyaltyCard.DATE_CREATED = DateCreated;
                mLoyaltyCard.strCardNo = p_CardNo;

                mLoyaltyCard.ExecuteQuery();
                return "";
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public DataTable SelectCardType(int p_DistributorID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCardType mspSelectCategoryType = new spSelectCardType();
                mspSelectCategoryType.Connection = mConnection;

                mspSelectCategoryType.DISTRIBUTOR_ID = p_DistributorID;

                DataTable ds = mspSelectCategoryType.ExecuteTable();

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

        public DataTable SelectLoyaltyCard(int ID, int DistributorId, int CartTypeId, int TypeID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectLoyalty_Card mLoyaltyCard = new spSelectLoyalty_Card();
                mLoyaltyCard.Connection = mConnection;
                mLoyaltyCard.ID = ID;
                mLoyaltyCard.DISTRIBUTOR_ID = DistributorId;
                mLoyaltyCard.CARD_TYPE_ID = CartTypeId;
                mLoyaltyCard.DISCOUNT = Constants.DecimalNullValue;
                mLoyaltyCard.PURCHASING = Constants.DecimalNullValue;
                mLoyaltyCard.POINTS = Constants.DecimalNullValue;
                mLoyaltyCard.AMOUNT_LIMIT = Constants.DecimalNullValue;
                mLoyaltyCard.IS_ACTIVE = true;
                mLoyaltyCard.TYPEID = TypeID;
                mLoyaltyCard.TIME_STAMP = Constants.DateNullValue;
                mLoyaltyCard.LAST_UPDATE = Constants.DateNullValue;
                mLoyaltyCard.USER_ID = Constants.IntNullValue;
                mLoyaltyCard.DATE_CREATED = Constants.DateNullValue;

                DataTable dt = mLoyaltyCard.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public int UpdatedLoyaltyCard(int ID, int distributorId, int cardTypeId, decimal discount, decimal purchase, decimal points, bool isActive, DateTime lastUpdate, int UserId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateLOYALTY_CARD mLoyaltyCard = new spUpdateLOYALTY_CARD();
                mLoyaltyCard.Connection = mConnection;
                mLoyaltyCard.ID = ID;
                mLoyaltyCard.DISTIBUTOR_ID = distributorId;
                mLoyaltyCard.CARD_TYPE_ID = cardTypeId;
                mLoyaltyCard.DISCOUNT = discount;
                mLoyaltyCard.PURCHASING = purchase;
                mLoyaltyCard.POINTS = points;
                mLoyaltyCard.IS_ACTIVE = isActive;
                mLoyaltyCard.TIME_STAMP = Constants.DateNullValue;
                mLoyaltyCard.LAST_UPDATE = lastUpdate;
                mLoyaltyCard.USER_ID = UserId;
                mLoyaltyCard.DATE_CREATED = Constants.DateNullValue;

                mLoyaltyCard.ExecuteQuery();
                return 1;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public DataTable GetLoyaltyCardDetail(string p_strCardNo,DateTime p_DOCUMENT_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetLoyaltyCardDetail mLoyaltyCard = new uspGetLoyaltyCardDetail();
                mLoyaltyCard.Connection = mConnection;

                mLoyaltyCard.strCardNo = p_strCardNo;
                mLoyaltyCard.DOCUMENT_DATE = p_DOCUMENT_DATE;

                DataTable ds = mLoyaltyCard.ExecuteTable();

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

        #region Reward Slab

        public bool InsertLoyaltyCardSalab(int intLoyaltyCardTypeID, int intLoyaltyCardSalabTypeID, int intItemID, decimal decPointsFrom, decimal decPointsTo, decimal decCash, decimal decDiscount,decimal decFreeUnit,decimal decPointsDeduction,DateTime dtmDocumentDate, int intUserID,DataTable dtLocation)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                uspInsertLoyaltyCardSalab mLoyaltyCard = new uspInsertLoyaltyCardSalab();
                mLoyaltyCard.Connection = mConnection;
                mLoyaltyCard.Transaction = mTransaction;

                mLoyaltyCard.intLoyaltyCardTypeID = intLoyaltyCardTypeID;
                mLoyaltyCard.intLoyaltyCardSalabTypeID = intLoyaltyCardSalabTypeID;
                mLoyaltyCard.intItemID = intItemID;
                mLoyaltyCard.decPointsFrom = decPointsFrom;
                mLoyaltyCard.decPointsTo = decPointsTo;
                mLoyaltyCard.decCash = decCash;
                mLoyaltyCard.decDiscount = decDiscount;
                mLoyaltyCard.decFreeUnit = decFreeUnit;
                mLoyaltyCard.decPointsDeduction = decPointsDeduction;
                mLoyaltyCard.dtmDocumentDate = dtmDocumentDate;
                mLoyaltyCard.intUserID = intUserID;
                mLoyaltyCard.ExecuteQuery();

                uspInsertLoyaltyCardSalabLocation mLoyaltyCardLocation = new uspInsertLoyaltyCardSalabLocation();
                mLoyaltyCardLocation.Connection = mConnection;
                mLoyaltyCardLocation.Transaction = mTransaction;

                foreach(DataRow dr in dtLocation.Rows)
                {
                    mLoyaltyCardLocation.intLocationID = Convert.ToInt32(dr["intLocationID"]);
                    mLoyaltyCardLocation.lngLoyaltyCardSalabID = mLoyaltyCard.lngLoyaltyCardSalabID;
                    mLoyaltyCardLocation.ExecuteQuery();
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

        public bool UpdateLoyaltyCardSalab(long lngLoyaltyCardSalabID, int intLoyaltyCardTypeID, int intLoyaltyCardSalabTypeID, int intItemID, decimal decPointsFrom, decimal decPointsTo, decimal decCash, decimal decDiscount, decimal decFreeUnit, decimal decPointsDeduction,int intUserID,bool IsActive,bool IsDeleted, DataTable dtLocation)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                uspUpdateLoyaltyCardSalab mLoyaltyCard = new uspUpdateLoyaltyCardSalab();
                mLoyaltyCard.Connection = mConnection;
                mLoyaltyCard.Transaction = mTransaction;

                mLoyaltyCard.lngLoyaltyCardSalabID = lngLoyaltyCardSalabID;
                mLoyaltyCard.intLoyaltyCardTypeID = intLoyaltyCardTypeID;
                mLoyaltyCard.intLoyaltyCardSalabTypeID = intLoyaltyCardSalabTypeID;
                mLoyaltyCard.intItemID = intItemID;
                mLoyaltyCard.decPointsFrom = decPointsFrom;
                mLoyaltyCard.decPointsTo = decPointsTo;
                mLoyaltyCard.decCash = decCash;
                mLoyaltyCard.decDiscount = decDiscount;
                mLoyaltyCard.decFreeUnit = decFreeUnit;
                mLoyaltyCard.decPointsDeduction = decPointsDeduction;
                mLoyaltyCard.intUserID = intUserID;
                mLoyaltyCard.IsActive = IsActive;
                mLoyaltyCard.IsDeleted = IsDeleted;
                mLoyaltyCard.ExecuteQuery();

                uspInsertLoyaltyCardSalabLocation mLoyaltyCardLocation = new uspInsertLoyaltyCardSalabLocation();
                mLoyaltyCardLocation.Connection = mConnection;
                mLoyaltyCardLocation.Transaction = mTransaction;

                foreach (DataRow dr in dtLocation.Rows)
                {
                    mLoyaltyCardLocation.intLocationID = Convert.ToInt32(dr["intLocationID"]);
                    mLoyaltyCardLocation.lngLoyaltyCardSalabID = mLoyaltyCard.lngLoyaltyCardSalabID;
                    mLoyaltyCardLocation.ExecuteQuery();
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

        public DataTable GetLoyaltyCardSlab(long lngLoyaltyCardSalabID, int intLoyaltyCardSalabTypeID,int TypeID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetLoyaltyCardSalab mLoyaltyCard = new uspGetLoyaltyCardSalab();
                mLoyaltyCard.Connection = mConnection;

                mLoyaltyCard.lngLoyaltyCardSalabID = lngLoyaltyCardSalabID;
                mLoyaltyCard.intLoyaltyCardSalabTypeID = intLoyaltyCardSalabTypeID;
                mLoyaltyCard.TypeID = TypeID;
                DataTable ds = mLoyaltyCard.ExecuteTable();

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
