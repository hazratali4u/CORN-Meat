using System;
using System.Collections.Generic;
using System.Text;
using CORNDatabaseLayer.Classes;
using System.Data;
using CORNDataAccessLayer.Classes;
using CORNCommon.Classes;

namespace CORNBusinessLayer.Classes
{
    public class ShiftController
    {
        public bool InsertShiftClose(int p_LOCATION_ID, int p_SHIFT_ID, int p_USER_ID, DateTime p_TIME_STAMP, int p_CURRENCY_5000, int p_CURRENCY_1000, int p_CURRENCY_500, int p_CURRENCY_100, int p_CURRENCY_50, int p_CURRENCY_20, int p_CURRENCY_10, int p_CURRENCY_5, int p_CURRENCY_2, int p_CURRENCY_1, decimal p_CASH_TOTAL, decimal p_OPENING_CASH, decimal p_DIFFERENCE, DateTime p_Close_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                InsertShiftClose mShift = new InsertShiftClose();
                mShift.Connection = mConnection;
                mShift.LOCATION_ID = p_LOCATION_ID;
                mShift.SHIFT_ID = p_SHIFT_ID;
                mShift.USER_ID = p_USER_ID;
                mShift.TIME_STAMP = p_TIME_STAMP;
                mShift.CURRENCY_5000 = p_CURRENCY_5000;
                mShift.CURRENCY_1000 = p_CURRENCY_1000;
                mShift.CURRENCY_500 = p_CURRENCY_500;
                mShift.CURRENCY_100 = p_CURRENCY_100;
                mShift.CURRENCY_50 = p_CURRENCY_50;
                mShift.CURRENCY_20 = p_CURRENCY_20;
                mShift.CURRENCY_10 = p_CURRENCY_10;
                mShift.CURRENCY_5 = p_CURRENCY_5;
                mShift.CURRENCY_2 = p_CURRENCY_2;
                mShift.CURRENCY_1 = p_CURRENCY_1;
                mShift.CASH_TOTAL = p_CASH_TOTAL;
                mShift.OPENING_CASH = p_OPENING_CASH;
                mShift.DIFFERENCE = p_DIFFERENCE;
                mShift.CLOSE_DATE = p_Close_Date;
                mShift.ExecuteQuery();

                return true;

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

        public bool InsertShiftOpeningAmount(int p_UserID, int p_ShiftID, decimal p_Amount, DateTime p_TimeStamp, string remarks)
        {
            try
            {
                IDbConnection mConnection = null;
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSHIFT_OPENING_AMOUNT mShift = new spInsertSHIFT_OPENING_AMOUNT();
                mShift.Connection = mConnection;
                mShift.SHIFT_ID = p_ShiftID;
                mShift.USER_ID = p_UserID;
                mShift.AMOUNT = p_Amount;
                mShift.TIME_STAMP = p_TimeStamp;
                mShift.REMARKS = remarks;
                mShift.ExecuteQuery();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw;
            }
        }
        public DataTable SelectShift(int p_location_ID, int p_TYPEID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectShift m_Shift = new spSelectShift();
                m_Shift.Connection = mConnection;
                m_Shift.LOCATION_ID = p_location_ID;
                m_Shift.TYPEID = p_TYPEID;

                DataTable dt = m_Shift.ExecuteTable();
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
        public DataTable SelectSales(int p_Distributor_Id, int p_UserId, DateTime p_StartDate, DateTime p_EndDate, int pType, int p_ShiftID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                sp_SelectSaleReport1 mOrder = new sp_SelectSaleReport1();
                mOrder.Connection = mConnection;
                mOrder.DISTRIBUTOR_ID = p_Distributor_Id;
                mOrder.USER_ID = p_UserId;
                mOrder.STARTDATE = p_StartDate;
                mOrder.ENDDATE = p_EndDate;
                mOrder.TYPE = pType;
                mOrder.SHIFT_ID = p_ShiftID;
                DataTable dt = mOrder.ExecuteTable();
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
        public void UpdateShiftOpeningAmount(int OpeningId, int p_UserID, int p_ShiftID, decimal p_Amount, DateTime p_TimeStamp, string remarks, int TYPEID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateSHIFT_OPENING_AMOUNT mShiftOpening = new spUpdateSHIFT_OPENING_AMOUNT();
                mShiftOpening.Connection = mConnection;
                mShiftOpening.OPENIIN_ID = OpeningId;
                mShiftOpening.SHIFT_ID = p_ShiftID;
                mShiftOpening.USER_ID = p_UserID;
                mShiftOpening.AMOUNT = p_Amount;
                mShiftOpening.REMARKS = remarks;
                mShiftOpening.TIME_STAMP = Constants.DateNullValue;
                mShiftOpening.IS_ACTIVE = true;
                mShiftOpening.TYPEID = TYPEID;

                mShiftOpening.ExecuteQuery();
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
    }
}
