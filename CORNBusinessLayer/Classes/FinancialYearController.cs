using System;
using System.Data;
using CORNDataAccessLayer.Classes;
using CORNCommon.Classes;
using CORNDatabaseLayer.Classes;

namespace CORNBusinessLayer.Classes
{
    public class FinancialYearController
    {
        //============================================================
        public DataTable SelectFinancialYear(string yearName, string ShortName, string DisplayName, string Description, Int16 code, bool isOpen, bool isActive, int TYPE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectFinancialYear mdtFinancialYear = new spSelectFinancialYear();
                mdtFinancialYear.Connection = mConnection;
                mdtFinancialYear.IsActive = isActive;
                mdtFinancialYear.IsOpen = isOpen;
                mdtFinancialYear.sintCode = code;
                mdtFinancialYear.strDescription = Description;
                mdtFinancialYear.strDisplayName = DisplayName;
                mdtFinancialYear.strShortName = ShortName;
                mdtFinancialYear.strYearName = yearName;
                mdtFinancialYear.TYPEID = TYPE;
                DataTable dt = mdtFinancialYear.ExecuteTable();
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
        public bool InsertFinancialYear(string yearName, string ShortName, string Description, bool isOpen, bool isActive, DateTime dtStart, DateTime dtEnd)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInserttblFinancialYear mdtFinancialYear = new spInserttblFinancialYear();
                mdtFinancialYear.Connection = mConnection;
                mdtFinancialYear.IsActive = isActive;
                mdtFinancialYear.IsOpen = isOpen;
                mdtFinancialYear.strDescription = Description;
                mdtFinancialYear.strDisplayName = yearName;
                mdtFinancialYear.strShortName = ShortName;
                mdtFinancialYear.strYearName = yearName;
                mdtFinancialYear.dtStart = dtStart;
                mdtFinancialYear.dtEnd = dtEnd;
                mdtFinancialYear.ExecuteQuery();
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
        public bool UpdateFinancialYear(string yearName, string ShortName, string Description, Int16 code, bool isOpen, bool isActive, DateTime dtStart, DateTime dtEnd)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spUpdatetblFinancialYear mdtFinancialYear = new spUpdatetblFinancialYear();
                mdtFinancialYear.Connection = mConnection;
                mdtFinancialYear.IsActive = isActive;
                mdtFinancialYear.IsOpen = isOpen;
                mdtFinancialYear.sintCode = code;
                mdtFinancialYear.strDescription = Description;
                mdtFinancialYear.strDisplayName = yearName;
                mdtFinancialYear.strShortName = ShortName;
                mdtFinancialYear.strYearName = yearName;
                mdtFinancialYear.dtStart = dtStart;
                mdtFinancialYear.dtEnd = dtEnd;
                mdtFinancialYear.ExecuteQuery();
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
    }
}
