using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;
using CORNDatabaseLayer.Classes;

namespace CORNBusinessLayer.Classes
{


    /// <summary>
    /// Class For Employee Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Employee
    /// </item>
    /// <term>
    /// Update Employee
    /// </term>
    /// <item>
    /// Get Employee
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class Distributor_UserController
    {
        #region Constructor

        /// <summary>
        /// Constructor for Distributor_UserController
        /// </summary>
        public Distributor_UserController()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        /// <summary>
        /// Inserts Employee
        /// </summary>
        /// <remarks>
        /// Returns Inserted Employee ID
        /// </remarks>
        /// <param name="p_CompanyId">Company</param>
        /// <param name="p_NIC">NIC</param>
        /// <param name="p_Is_Active">IsActive</param>
        /// <param name="p_Time_Stamp">CreatedOn</param>
        /// <param name="p_Lastupdate_Date">LastUpdateDate</param>
        /// <param name="p_User_Type_Id">Type</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Role_Id">Role</param>
        /// <param name="p_Email_Address">Email</param>
        /// <param name="p_Address1">Address1</param>
        /// <param name="p_Address2">Address2</param>
        /// <param name="p_Login_Id">Login</param>
        /// <param name="p_Password">Password</param>
        /// <param name="p_Mobile">Mobile</param>
        /// <param name="p_User_Code">Code</param>
        /// <param name="p_User_Name">Name</param>
        /// <param name="p_Phone">Phone</param>
        /// <returns>Inserted Employee ID</returns>
        public string InsertDistributor_User(int p_CompanyId, string p_NIC, bool p_Is_Active, DateTime p_Time_Stamp,
              DateTime p_Lastupdate_Date, int p_User_Type_Id, int p_Distributor_Id,
              int p_Role_Id, string p_Email_Address, string p_Address1, string p_Address2, string p_Login_Id, string p_Password, string p_Mobile,
              string p_User_Code, string p_User_Name, string p_Phone, int p_Dept_ID, int shiftId
            , decimal DirectorCardValue, decimal EMCDiscount, int ApprovalBy, string p_CardNo, int p_CARD_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertDISTRIBUTOR_USER mDistributorUser = new spInsertDISTRIBUTOR_USER();
                mDistributorUser.Connection = mConnection;
                mDistributorUser.COMPANY_ID = p_CompanyId;
                mDistributorUser.USER_CODE = p_User_Code;
                mDistributorUser.USER_NAME = p_User_Name;
                mDistributorUser.NIC_NO = p_NIC;
                mDistributorUser.ADDRESS1 = p_Address1;
                mDistributorUser.ADDRESS2 = p_Address2;
                mDistributorUser.DISTRIBUTOR_ID = p_Distributor_Id;
                mDistributorUser.EMAIL = p_Email_Address;
                mDistributorUser.LASTUPDATE_DATE = p_Lastupdate_Date;
                mDistributorUser.LOGIN_ID = p_Login_Id;
                mDistributorUser.MOBILE = p_Mobile;
                mDistributorUser.PASSWORD = p_Password;
                mDistributorUser.PHONE = p_Phone;
                mDistributorUser.ROLE_ID = p_Role_Id;
                mDistributorUser.TIME_STAMP = p_Time_Stamp;
                mDistributorUser.DATE_JOIN = p_Time_Stamp;
                mDistributorUser.LASTUPDATE_DATE = p_Lastupdate_Date;
                mDistributorUser.USER_CODE = p_User_Code;
                mDistributorUser.USER_NAME = p_User_Name;
                mDistributorUser.USER_TYPE_ID = p_User_Type_Id;
                mDistributorUser.IS_ACTIVE = p_Is_Active;
                mDistributorUser.DEPARTMENT_ID = p_Dept_ID;
                mDistributorUser.SHIFT_ID = shiftId;
                mDistributorUser.strCardNo = p_CardNo;
                mDistributorUser.AMOUNT_LIMIT = DirectorCardValue;
                mDistributorUser.EMC_LimitPerDay = EMCDiscount;
                mDistributorUser.Manager_UserID = ApprovalBy;
                mDistributorUser.CARD_TYPE_ID = p_CARD_TYPE_ID;

                mDistributorUser.ExecuteQuery();

                return mDistributorUser.USER_ID.ToString();

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

        /// <summary>
        /// Updates Employee
        /// </summary>
        /// <remarks>
        /// Returns Updated Employee ID
        /// </remarks>
        /// <param name="UserId">Employee</param>
        /// <param name="p_CompanyId">Company</param>
        /// <param name="p_NIC">NIC</param>
        /// <param name="p_Is_Active">IsActive</param>
        /// <param name="p_Time_Stamp">CreatedOn</param>
        /// <param name="p_Lastupdate_Date">LastUpdateDate</param>
        /// <param name="p_User_Type_Id">Type</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Role_Id">Role</param>
        /// <param name="p_Email_Address">Email</param>
        /// <param name="p_Address1">Address1</param>
        /// <param name="p_Address2">Address2</param>
        /// <param name="p_Login_Id">Login</param>
        /// <param name="p_Password">Password</param>
        /// <param name="p_Mobile">Mobile</param>
        /// <param name="p_User_Code">Code</param>
        /// <param name="p_User_Name">Name</param>
        /// <param name="p_Phone">Phone</param>
        /// <returns>Updated Employee ID</returns>
        public string UpdateDistributor_User(int UserId, int p_CompanyId, string p_NIC, bool p_Is_Active, DateTime p_Time_Stamp,
             DateTime p_Lastupdate_Date, int p_User_Type_Id, int p_Distributor_Id,
               int p_Role_Id, string p_Email_Address, string p_Address1, string p_Address2, string p_Login_Id, string p_Password, string p_Mobile,
             string p_User_Code, string p_User_Name, string p_Phone, int p_Dept_ID, int shiftId
            , decimal DirectorCardValue
            , decimal EMCDiscount, int ApprovalBy, string p_CardNo, int P_CARD_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateDISTRIBUTOR_USER mDistributorUser = new spUpdateDISTRIBUTOR_USER();
                mDistributorUser.USER_ID = UserId;
                mDistributorUser.Connection = mConnection;
                mDistributorUser.COMPANY_ID = p_CompanyId;
                mDistributorUser.USER_CODE = p_User_Code;
                mDistributorUser.USER_NAME = p_User_Name;
                mDistributorUser.NIC_NO = p_NIC;
                mDistributorUser.ADDRESS1 = p_Address1;
                mDistributorUser.ADDRESS2 = p_Address2;
                mDistributorUser.DISTRIBUTOR_ID = p_Distributor_Id;
                mDistributorUser.EMAIL = p_Email_Address;
                mDistributorUser.LASTUPDATE_DATE = p_Lastupdate_Date;
                mDistributorUser.LOGIN_ID = p_Login_Id;
                mDistributorUser.MOBILE = p_Mobile;
                mDistributorUser.PASSWORD = p_Password;
                mDistributorUser.PHONE = p_Phone;
                mDistributorUser.ROLE_ID = p_Role_Id;
                mDistributorUser.DATE_JOIN = p_Time_Stamp;
                mDistributorUser.TIME_STAMP = p_Time_Stamp;
                mDistributorUser.LASTUPDATE_DATE = p_Lastupdate_Date;
                mDistributorUser.USER_CODE = p_User_Code;
                mDistributorUser.USER_NAME = p_User_Name;
                mDistributorUser.USER_TYPE_ID = p_User_Type_Id;
                mDistributorUser.IS_ACTIVE = p_Is_Active;
                mDistributorUser.DEPARTMENT_ID = p_Dept_ID;
                mDistributorUser.SHIFT_ID = shiftId;
                mDistributorUser.strCardNo = p_CardNo;
                mDistributorUser.AMOUNT_LIMIT = DirectorCardValue;
                mDistributorUser.EMC_LimitPerDay = EMCDiscount;
                mDistributorUser.Manager_UserID = ApprovalBy;
                mDistributorUser.CARD_TYPE_ID = P_CARD_TYPE_ID;
                mDistributorUser.ExecuteQuery();


                return mDistributorUser.USER_ID.ToString();

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

        /// <summary>
        /// Gets Employee Data
        /// </summary>
        /// <remarks>
        /// Returns Employee Data as Datatable
        /// </remarks>
        /// <param name="p_Type">Type</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="Companyid">Company</param>
        /// <returns>Employee Data as Datatable</returns>
        public DataTable SelectDistributorUser(int p_Type, int p_Distributor_Id, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSelectDISTRIBUTOR_USERInfo mDistUser = new uspSelectDISTRIBUTOR_USERInfo();
                mDistUser.Connection = mConnection;

                mDistUser.TYPE = p_Type;
                mDistUser.DISTRIBUTOR_ID = p_Distributor_Id;
                mDistUser.COMPANY_ID = Companyid;

                DataTable dt = mDistUser.ExecuteTable();
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
        public DataSet SelectDistributorUserInfo(int pUserId, bool pStatus, int pDistributorId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectDISTRIBUTOR_USER mDistUser = new spSelectDISTRIBUTOR_USER();
                CORNBusinessLayer.Reports.LatestDataSet ds = new CORNBusinessLayer.Reports.LatestDataSet();
                mDistUser.Connection = mConnection;

                mDistUser.USER_ID = pUserId;
                mDistUser.IS_ACTIVE = pStatus;
                mDistUser.DISTRIBUTOR_ID = pDistributorId;

                DataTable dt = mDistUser.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["spSelectDISTRIBUTOR_USER"].ImportRow(dr);
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
        /// Get All Employees
        /// </summary>
        /// <returns>All Employees Data as Datatable</returns>
        public DataTable SelectGLUser()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectGLUserDetail mDistUser = new UspSelectGLUserDetail();
                mDistUser.Connection = mConnection;
                DataTable dt = mDistUser.ExecuteTable();
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
        #region Shift
        public bool InsertShift(int p_distributorID, DateTime p_from, DateTime p_to)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertShift mShift = new spInsertShift();
                mShift.Connection = mConnection;
                mShift.LOCATION_ID = p_distributorID;
                mShift.SHIFT_FROM = p_from;
                mShift.SHIFT_TO = p_to;
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

        public bool UpdateShift(int p_ShiftID, int p_LocationID, DateTime p_ShiftFrom, DateTime p_ShiftTo, bool p_IsActive, int p_TypeID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateSHIFT mShiftUpdate = new spUpdateSHIFT();
                mShiftUpdate.Connection = mConnection;
                mShiftUpdate.LOCATION_ID = p_LocationID;
                mShiftUpdate.SHIFT_FROM = p_ShiftFrom;
                mShiftUpdate.SHIFT_TO = p_ShiftTo;
                mShiftUpdate.IS_ACTIVE = p_IsActive;
                mShiftUpdate.SHIFT_ID = p_ShiftID;
                mShiftUpdate.TYPEID = p_TypeID;
                mShiftUpdate.ExecuteQuery();
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
        #endregion        

    }
}
