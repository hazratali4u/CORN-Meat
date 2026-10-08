using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;
using CORNDatabaseLayer.Classes;

namespace CORNBusinessLayer.Classes
{
    /// <summary>
    /// Insert Product Price
    /// </summary>
    public class ProductPrice_Controller
    {
        #region Product Price
        
        public bool InsertPPrice(decimal p_RETAIL_PRICE, decimal p_CURRENT_PRICE, decimal p_NEW_PRICE, int p_SKU_ID, DateTime p_DOCUMENT_DATE, DateTime p_TIME_STAMP, int p_USER_ID, bool p_IS_ACTIVE)
        {  
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertPRODUCT_PRICE mProductPrice = new spInsertPRODUCT_PRICE();
                mProductPrice.Connection = mConnection;
                mProductPrice.RETAIL_PRICE = p_RETAIL_PRICE;
                mProductPrice.CURRENT_PRICE = p_CURRENT_PRICE;
                mProductPrice.NEW_PRICE = p_NEW_PRICE;
                mProductPrice.SKU_ID = p_SKU_ID;
                mProductPrice.DOCUMENT_DATE=p_DOCUMENT_DATE;
                mProductPrice.USER_ID = p_USER_ID;
                mProductPrice.IS_ACTIVE = p_IS_ACTIVE;
                mProductPrice.TIME_STAMP = p_TIME_STAMP;

               bool a= mProductPrice.ExecuteQuery();
               return a;

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
        public DataTable SelectProductPrice(int p_PP_ID, int p_CATG_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectProductPriceInfo mProductPrice = new spSelectProductPriceInfo();
                mProductPrice.Connection = mConnection;

                mProductPrice.PP_ID = p_PP_ID;
                mProductPrice.CATG_ID = p_CATG_ID;
                
           //     mProductPrice.IS_ACTIVE = p_IS_ACTIVE;
                

                    return  mProductPrice.ExecuteTable();
                

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
        public DataTable SelectProductPrice2(int p_PP_ID, int p_CATG_ID, int p_PP_ID2)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectProductPriceInfo mProductPrice = new spSelectProductPriceInfo();
                mProductPrice.Connection = mConnection;

                mProductPrice.PP_ID = p_PP_ID;
                mProductPrice.CATG_ID = p_CATG_ID;
                mProductPrice.PP_ID2 = p_PP_ID2;
                //     mProductPrice.IS_ACTIVE = p_IS_ACTIVE;


                return mProductPrice.ExecuteTable();


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
        public bool UpdatePPrice(int p_PP_ID,decimal p_RETAIL_PRICE, decimal p_CURRENT_PRICE, decimal p_NEW_PRICE, int p_SKU_ID, DateTime p_TIME_STAMP, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdatePRODUCT_PRICE mProductPrice = new spUpdatePRODUCT_PRICE();
                mProductPrice.Connection = mConnection;
                mProductPrice.PP_ID = p_PP_ID;
                mProductPrice.RETAIL_PRICE = p_RETAIL_PRICE;
                mProductPrice.CURRENT_PRICE = p_CURRENT_PRICE;
                mProductPrice.NEW_PRICE = p_NEW_PRICE;
                mProductPrice.SKU_ID = p_SKU_ID;
                mProductPrice.USER_ID = p_USER_ID;
                mProductPrice.TIME_STAMP = p_TIME_STAMP;

                bool a = mProductPrice.ExecuteQuery();
                return a;

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
        public bool DeletePPrice(int p_PP_ID, bool p_IS_ACTIVE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeletePRODUCT_PRICE mPRODUCT_PRICE = new spDeletePRODUCT_PRICE();
                mPRODUCT_PRICE.Connection = mConnection;

                mPRODUCT_PRICE.PP_ID = p_PP_ID;
                mPRODUCT_PRICE.IS_ACTIVE = p_IS_ACTIVE;

                //  mGeoHierarchy.USER_ID = p_USER_ID;
                bool a = mPRODUCT_PRICE.ExecuteQuery();
                return a;
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
        #endregion
    }
}
