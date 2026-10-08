using System;
using System.Data;
using CORNDataAccessLayer.Classes;
using CORNCommon.Classes;
using CORNDatabaseLayer.Classes;

namespace CORNBusinessLayer.Classes
{
    public class BankController
    {
        //============================================================
        public DataTable SelectCheckBook(int checkbookid, int bankAccountid, string checkfrom, string checkto, string remarks, bool isActive, DateTime timeStamp, int Typeid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCHECK_BOOK mdtCheckBook = new spSelectCHECK_BOOK();
                mdtCheckBook.Connection = mConnection;
                mdtCheckBook.CHECK_BOOK_ID = checkbookid;
                mdtCheckBook.BANK_ACCOUNT_ID = bankAccountid;
                mdtCheckBook.CHECK_FROM = checkfrom;
                mdtCheckBook.CHECK_TO = checkto;
                mdtCheckBook.REMARKS = remarks;
                mdtCheckBook.IS_ACTIVE = isActive;
                mdtCheckBook.TIME_STAMP = timeStamp;
                mdtCheckBook.TYPEID = Typeid;
                DataTable dt = mdtCheckBook.ExecuteTable();
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

        public DataSet SelectRPTCheckBook(int checkbookid, int bankAccountid, string checkfrom, string checkto, string remarks, bool isActive, DateTime timeStamp, int Typeid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                Reports.LatestDataSet ds = new Reports.LatestDataSet();
                spSelectCHECK_BOOK mdtCheckBook = new spSelectCHECK_BOOK();
                mdtCheckBook.Connection = mConnection;
                mdtCheckBook.CHECK_BOOK_ID = checkbookid;
                mdtCheckBook.BANK_ACCOUNT_ID = bankAccountid;
                mdtCheckBook.CHECK_FROM = checkfrom;
                mdtCheckBook.CHECK_TO = checkto;
                mdtCheckBook.REMARKS = remarks;
                mdtCheckBook.IS_ACTIVE = isActive;
                mdtCheckBook.TIME_STAMP = timeStamp;
                mdtCheckBook.TYPEID = Typeid;
                DataTable dt = mdtCheckBook.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["spSelectCHECK_BOOK"].ImportRow(dr);
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

        public int InsertCheckBook(int bankAccountid, long checkfrom, long checkto, string remarks, bool isActive, DateTime timeStamp, string BranchCode, string BranchNo, string Iban, string AccountTitle, string CpNo, string ChequeAlphabets)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCHECK_BOOK mdtCheckBook = new spInsertCHECK_BOOK();
                mdtCheckBook.Connection = mConnection;
                mdtCheckBook.BANK_ACCOUNT_ID = bankAccountid;
                mdtCheckBook.REMARKS = remarks;
                mdtCheckBook.IS_ACTIVE = isActive;
                mdtCheckBook.TIME_STAMP = timeStamp;

                mdtCheckBook.BRANCH_CODE = BranchCode;
                mdtCheckBook.BRANCH_NO = BranchNo;
                mdtCheckBook.IBAN = Iban;
                mdtCheckBook.ACCOUNT_TITLE = AccountTitle;
                mdtCheckBook.CP_NUMBER = CpNo;
                mdtCheckBook.CHEQUE_ALFABETS = ChequeAlphabets;
                mdtCheckBook.CHECK_FROM = checkfrom;
                mdtCheckBook.CHECK_TO = checkto;

                mdtCheckBook.ExecuteQuery();
                return mdtCheckBook.CHECK_BOOK_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw;
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

        public string UpdateCheckBook(int checkbookid, int bankAccountid, long checkfrom, long checkto, string remarks, bool isActive, DateTime timeStamp
            , int typeid, int status, string Alphabets, string branchCode, string branchNo, string iban, string AccountTitle, string cpNumber)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCHECK_BOOK mdtCheckBook = new spUpdateCHECK_BOOK();
                mdtCheckBook.Connection = mConnection;
                mdtCheckBook.CHECK_BOOK_ID = checkbookid;
                mdtCheckBook.BANK_ACCOUNT_ID = bankAccountid;
                mdtCheckBook.CHECK_FROM = checkfrom;
                mdtCheckBook.CHECK_TO = checkto;
                mdtCheckBook.REMARKS = remarks;
                mdtCheckBook.IS_ACTIVE = isActive;
                mdtCheckBook.TIME_STAMP = timeStamp;

                mdtCheckBook.TYPEID = typeid;
                mdtCheckBook.STATUS = status;

                mdtCheckBook.CHEQUE_ALFABETS = Alphabets;
                mdtCheckBook.BRANCH_CODE = branchCode;
                mdtCheckBook.BRANCH_NO = branchNo;
                mdtCheckBook.IBAN = iban;
                mdtCheckBook.ACCOUNT_TITLE = AccountTitle;
                mdtCheckBook.CP_NUMBER = cpNumber;

                mdtCheckBook.ExecuteQuery();
                return "Record Updated.";
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

        //============================================================

        public string UpdateChequeStatus(int Status, string chequeNo, String Remarks, int typeid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateChequeStatus mdtCheck = new spUpdateChequeStatus();
                mdtCheck.Connection = mConnection;
                mdtCheck.STATUS = Status;
                mdtCheck.CHEQUE_NO = chequeNo;
                mdtCheck.TYPEID = typeid;
                mdtCheck.REMARKS = Remarks;
                mdtCheck.ExecuteQuery();
                return "Record Updated.";
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
