using System;
using CORNDatabaseLayer.Classes;
using CORNDataAccessLayer.Classes;
using CORNCommon.Classes;
using System.Data;
using CORNBusinessLayer.Classes;    

namespace CORNBusinessLayer.Classes
{
    /// <summary>
    /// Class For Geo Hierarchy Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Geo Hierarchy
    /// </item>
    /// <term>
    /// Update Geo Hierarchy
    /// </term>
    /// <item>
    /// Get Geo Hierarchy
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class GeoHierarchyController
    {

        # region Payment Type

        public bool InsertPAYMENT_TYPE(string p_PT_CODE, string p_PT_DESCRIPTION, string p_PT_REMARKS, DateTime p_TIME_STAMP, int p_USER_ID, bool p_IS_ACTIVE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();


                spInsertPAYMENT_TYPE mPAYMENT_TYPE = new spInsertPAYMENT_TYPE();

                mPAYMENT_TYPE.Connection = mConnection;


                mPAYMENT_TYPE.PT_CODE = p_PT_CODE;
                mPAYMENT_TYPE.PT_DESCRIPTION = p_PT_DESCRIPTION;
                mPAYMENT_TYPE.PT_REMARKS = p_PT_REMARKS;
                mPAYMENT_TYPE.TIME_STAMP = p_TIME_STAMP;
                mPAYMENT_TYPE.USER_ID = p_USER_ID;
                mPAYMENT_TYPE.IS_ACTIVE = p_IS_ACTIVE;
                bool a = mPAYMENT_TYPE.ExecuteQuery();
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
        public DataTable GetMaxPAYMENT_TYPE_ID()
        {

            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectMaxPAYMENT_TYPE_ID mPAYMENT_TYPE = new spSelectMaxPAYMENT_TYPE_ID();
                mPAYMENT_TYPE.Connection = mConnection;

                return mPAYMENT_TYPE.ExecuteTable();
                ;

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
        public DataTable GetPAYMENT_TYPE(int p_PT_ID)
        {

            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPAYMENT_TYPE mPAYMENT_TYPE = new spSelectPAYMENT_TYPE();
                mPAYMENT_TYPE.PT_ID = p_PT_ID;
                mPAYMENT_TYPE.Connection = mConnection;

                
                //   mPAYMENT_TYPE.IS_ACTIVE = p_IS_ACTIVE;


                return mPAYMENT_TYPE.ExecuteTable();

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
        public bool UpdatePAYMENT_TYPE(int p_PT_ID, string p_PT_CODE, string p_PT_DESCRIPTION, string p_PT_REMARKS, DateTime p_TIME_STAMP, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdatePAYMENT_TYPE mPAYMENT_TYPE = new spUpdatePAYMENT_TYPE();
                mPAYMENT_TYPE.Connection = mConnection;

                mPAYMENT_TYPE.PT_ID = p_PT_ID;
                mPAYMENT_TYPE.PT_CODE = p_PT_CODE;
                mPAYMENT_TYPE.PT_DESCRIPTION = p_PT_DESCRIPTION;
                mPAYMENT_TYPE.PT_REMARKS = p_PT_REMARKS;
                mPAYMENT_TYPE.TIME_STAMP = p_TIME_STAMP;
                mPAYMENT_TYPE.USER_ID = p_USER_ID;
                //  mPAYMENT_TYPE.IS_ACTIVE = p_IS_ACTIVE;

                bool a = mPAYMENT_TYPE.ExecuteQuery();
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
        public bool DeletePAYMENT_TYPE(int p_PT_ID, bool p_IS_ACTIVE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeletePAYMENT_TYPE mPAYMENT_TYPE = new spDeletePAYMENT_TYPE();
                mPAYMENT_TYPE.Connection = mConnection;

                mPAYMENT_TYPE.PT_ID = p_PT_ID;
                mPAYMENT_TYPE.IS_ACTIVE = p_IS_ACTIVE;

                //  mGeoHierarchy.USER_ID = p_USER_ID;
                bool a = mPAYMENT_TYPE.ExecuteQuery();
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

        #region Table Defination

        public bool InsertTableDefination(int pDistributorId,string p_TableDefination_No, string p_TableDefination_Description,
            string p_TableDefination_Capacity, string p_TableDefination_Abbrivation, DateTime p_TIME_STAMP,
            int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();


                spInsertTableDefination mTableDefination = new spInsertTableDefination();

                mTableDefination.Connection = mConnection;

                mTableDefination.distributorId = pDistributorId;
                mTableDefination.p_TableDefination_No = p_TableDefination_No;
                mTableDefination.p_TableDefination_Description = p_TableDefination_Description;
                mTableDefination.p_TableDefination_Capacity = p_TableDefination_Capacity;
                mTableDefination.p_TableDefination_Abbrivation = p_TableDefination_Abbrivation;
                mTableDefination.P_TIME_STAMP = p_TIME_STAMP;
                mTableDefination.p_USER_ID = p_USER_ID;
                //  mTableDefination.TableDefination_Is_Active = p_Is_Active;
                bool a = mTableDefination.ExecuteQuery();
                return a;
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
        public DataTable GetTableDefination(int p_TableDefination_ID, int pDistributorId, bool Is_Active, int pUserId)
        {

            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectTableDefination mtblDefination = new spSelectTableDefination();
                mtblDefination.Connection = mConnection;
                mtblDefination.Active = Is_Active;
                mtblDefination.TableDefination_ID = p_TableDefination_ID;
                mtblDefination.distributorId = pDistributorId;
                mtblDefination.USER_ID = pUserId;
                return mtblDefination.ExecuteTable();

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

        public DataSet SelectDataForPosLoad(int pUserId, int pDistributerId, DateTime pDocumentDate, int pRoleId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SpSelectDataForPOSLoad mtblDefination = new SpSelectDataForPOSLoad
                {
                    Connection = mConnection,
                    USER_ID= pUserId,
                    Distributer_ID = pDistributerId,
                    DOCUMENT_DATE = pDocumentDate,
                    ROLE_ID = pRoleId
                };
                return mtblDefination.ExecuteDataSet();
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


        public string SelectMaxOrderNo(int pDistributerId, DateTime pDocumentDate)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SpSelectORDER_NO mOrderNo = new SpSelectORDER_NO
                {
                    Connection = mConnection,
                    
                    Distributer_ID = pDistributerId,
                    DOCUMENT_DATE = pDocumentDate
                };
                return mOrderNo.ExecuteScalar();
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

        public bool UpdateTableDefination(int p_TableDefination_ID, string p_TableDefination_No, string p_TableDefination_Description, string p_TableDefination_Capacity, string p_TableDefination_Abbrivation, DateTime p_TIME_STAMP, int p_USER_ID, bool p_Is_Active,int location)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateTableDefination mTableDefination = new spUpdateTableDefination();
                mTableDefination.Connection = mConnection;

                mTableDefination.p_TableDefination_ID = p_TableDefination_ID;
                mTableDefination.p_TableDefination_No = p_TableDefination_No;
                mTableDefination.p_TableDefination_Description = p_TableDefination_Description;
                mTableDefination.p_TableDefination_Capacity = p_TableDefination_Capacity;
                mTableDefination.p_TableDefination_Abbrivation = p_TableDefination_Abbrivation;
                mTableDefination.p_Is_Active = p_Is_Active;
                mTableDefination.p_distributorId = location;

                mTableDefination.p_USER_ID = p_USER_ID;
                mTableDefination.P_TIME_STAMP = p_TIME_STAMP;
                bool a = mTableDefination.ExecuteQuery();
                return a;
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
        public bool DeleteTableDefination(int p_TableDefination_ID, bool p_Is_Active)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeleteTableDefination mTableDefination = new spDeleteTableDefination();
                mTableDefination.Connection = mConnection;

                mTableDefination.p_TableDefination_ID = p_TableDefination_ID;
                mTableDefination.p_Is_Active = p_Is_Active;
              
                bool a = mTableDefination.ExecuteQuery();
                return a;
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

        # endregion
        public bool InsertSERVICE_TYPE(string p_ST_CODE, string p_ST_DESCRIPTION, int p_ST_SERVICE, DateTime p_TIME_STAMP, int p_USER_ID, bool p_IS_ACTIVE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();


                spInsertSERVICE_TYPE mSERVICE_TYPE = new spInsertSERVICE_TYPE();

                mSERVICE_TYPE.Connection = mConnection;


                mSERVICE_TYPE.ST_CODE = p_ST_CODE;
                mSERVICE_TYPE.ST_DESCRIPTION = p_ST_DESCRIPTION;
                mSERVICE_TYPE.ST_SERVICE = p_ST_SERVICE;
                mSERVICE_TYPE.TIME_STAMP = p_TIME_STAMP;
                mSERVICE_TYPE.USER_ID = p_USER_ID;
                mSERVICE_TYPE.IS_ACTIVE = p_IS_ACTIVE;
                bool a = mSERVICE_TYPE.ExecuteQuery();
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
        public bool UpdateSERVICE_TYPE(int p_ST_ID, string p_ST_CODE, string p_ST_DESCRIPTION, int p_ST_SERVICE, DateTime p_TIME_STAMP, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateSERVICE_TYPE mSERVICE_TYPE = new spUpdateSERVICE_TYPE();

                mSERVICE_TYPE.Connection = mConnection;

                mSERVICE_TYPE.ST_ID = p_ST_ID;
                mSERVICE_TYPE.ST_CODE = p_ST_CODE;
                mSERVICE_TYPE.ST_DESCRIPTION = p_ST_DESCRIPTION;
                mSERVICE_TYPE.ST_SERVICE = p_ST_SERVICE;
                mSERVICE_TYPE.TIME_STAMP = p_TIME_STAMP;
                mSERVICE_TYPE.USER_ID = p_USER_ID;

                bool a = mSERVICE_TYPE.ExecuteQuery();
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetMaxUOM_ID()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectMaxUOM_ID mdpt_id = new spSelectMaxUOM_ID();
                mdpt_id.Connection = mConnection;
                return mdpt_id.ExecuteTable();
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
        /// Gets Different Type Of Geo Hierarical Data Like
        /// <remarks>
        /// <list type="bullet">
        /// <item>Regions</item>
        /// <item>Zones</item>
        /// <item>Territories</item>
        /// <item>Towns</item>
        /// </list>
        /// </remarks>
        /// </summary>
        /// <param name="p_geo_id">GeoHierarchy</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PARENT_GEO_ID">ParentGeoHierarchy</param>
        /// <param name="p_GEO_CODE">Code</param>
        /// <param name="p_GEO_NAME">Name</param>
        /// <param name="p_Is_Deleted">IsDeleted</param>
        /// <param name="p_STATUS">Status</param>
        /// <param name="p_GEO_TYPE_ID">Type</param>
        /// <param name="p_USER_ID">InsertedBy</param>
        /// <param name="p_TIME_STAMP">CreatedOn</param>
        /// <param name="p_LASTUPDATE_DATE">LastUpdateDate</param>
        /// <param name="Companyid">Company</param>
        /// <returns>Geo Hierarical Data As Datatable</returns>
        /// 
        public DataTable SelectGeoHierarchy(int p_geo_id, int p_DISTRIBUTOR_ID, int p_PARENT_GEO_ID, string p_GEO_CODE, string p_GEO_NAME, bool p_Is_Deleted, int p_STATUS, int p_GEO_TYPE_ID, int p_USER_ID, DateTime p_TIME_STAMP, DateTime p_LASTUPDATE_DATE, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectGEO_HIERARCHY mGeoHierarchy = new spSelectGEO_HIERARCHY();
                mGeoHierarchy.Connection = mConnection;
                mGeoHierarchy.GEO_ID = p_geo_id;
                mGeoHierarchy.PARENT_GEO_ID = p_PARENT_GEO_ID;
                mGeoHierarchy.GEO_CODE = p_GEO_CODE;
                mGeoHierarchy.GEO_NAME = p_GEO_NAME;
                mGeoHierarchy.COMPANY_ID = Companyid;   
                mGeoHierarchy.ISCURRENT = Constants.IntNullValue; 
                if (p_Is_Deleted == true)
                {
                    mGeoHierarchy.ISDELETED = 1;

                }
                else
                {
                  mGeoHierarchy.ISDELETED = 0;

                }
                mGeoHierarchy.STATUS = p_STATUS;
                mGeoHierarchy.COMPANY_ID = Companyid; 
                mGeoHierarchy.GEO_TYPE_ID = p_GEO_TYPE_ID;
                mGeoHierarchy.USER_ID = Constants.IntNullValue; 
                mGeoHierarchy.TIME_STAMP = Constants.DateNullValue;
                mGeoHierarchy.LASTUPDATE_DATE = Constants.DateNullValue;
                mGeoHierarchy.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mGeoHierarchy.IP_ADDRESS = null;

                DataTable dt = mGeoHierarchy.ExecuteTable();
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
        public bool InsertUOM(string p_UOM_CODE, string p_UOM_DESCRIPTION, string p_UOM_REMARKS, int p_UOM_TYPE_ID, DateTime p_TIME_STAMP, int p_USER_ID, bool p_IS_ACTIVE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertUOM mDEPARTMENT = new spInsertUOM();
                mDEPARTMENT.Connection = mConnection;

                mDEPARTMENT.UOM_CODE = p_UOM_CODE;
                mDEPARTMENT.UOM_DESC = p_UOM_DESCRIPTION;
                mDEPARTMENT.UOM_REMARKS = p_UOM_REMARKS;
                mDEPARTMENT.UOM_TYPE_ID = p_UOM_TYPE_ID;
                mDEPARTMENT.TIME_STAMP = p_TIME_STAMP;
                mDEPARTMENT.USER_ID = p_USER_ID;
                mDEPARTMENT.IS_ACTIVE = p_IS_ACTIVE;

                bool a = mDEPARTMENT.ExecuteQuery();
                return a;
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
        public bool UpdateUOM(int p_UOM_ID, string p_UOM_DESCRIPTION, string p_UOM_REMARKS, int p_UOM_TYPE_ID
            , DateTime p_TIME_STAMP, int p_USER_ID, bool p_IS_ACTIVE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spUpdateUOM mDEPARTMENT = new spUpdateUOM();
                mDEPARTMENT.Connection = mConnection;

                mDEPARTMENT.UOM_ID = p_UOM_ID;
                mDEPARTMENT.UOM_DESC = p_UOM_DESCRIPTION;
                mDEPARTMENT.UOM_REMARKS = p_UOM_REMARKS;
                mDEPARTMENT.UOM_TYPE_ID = p_UOM_TYPE_ID;
                mDEPARTMENT.TIME_STAMP = p_TIME_STAMP;
                mDEPARTMENT.USER_ID = p_USER_ID;
                mDEPARTMENT.IS_ACTIVE = p_IS_ACTIVE;
                //mDEPARTMENT.TYPE_ID = p_TYPE_ID;

                bool a = mDEPARTMENT.ExecuteQuery();
                return a;
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
       
        public DataTable GetUOM(int p_UOM_ID, int UomTypeId,int TypeID)
        {

            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectUOM mGetDpt = new spSelectUOM();
                mGetDpt.Connection = mConnection;
                mGetDpt.UOM_TYPE_ID = UomTypeId;
                mGetDpt.UOM_ID = p_UOM_ID;
                mGetDpt.TYPEID = TypeID;
                return mGetDpt.ExecuteTable();

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
      /// 
      /// </summary>
      /// <param name="p_DPT_TYPE_ID"></param>
      /// <returns></returns>
        public DataTable GetDptType(int p_DPT_TYPE_ID)
        {

            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectDEPARTMENT_TYPE mGetDpt = new spSelectDEPARTMENT_TYPE();
                mGetDpt.Connection = mConnection;

                mGetDpt.DPT_TYPE_ID = p_DPT_TYPE_ID;

                return mGetDpt.ExecuteTable();

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
        /// Gets Different Type Of Geo Hierarical Data Like
        /// <remarks>
        /// <list type="bullet">
        /// <item>Regions</item>
        /// <item>Zones</item>
        /// <item>Territories</item>
        /// <item>Towns</item>
        /// </list>
        /// </remarks>
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <returns>Geo Hierarical Data As Datatable</returns>
        public DataTable SelectGeoHierarchy(int p_DISTRIBUTOR_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectDistributor_TOWN mGeoHierarchy = new spSelectDistributor_TOWN();
                mGeoHierarchy.Connection = mConnection;
                mGeoHierarchy.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;  
                DataTable dt = mGeoHierarchy.ExecuteTable();
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
        /// Inserts Geo Hierarchy
        /// </summary>
        /// <param name="p_Parent_Geo_Id">GeoHierarchy</param>
        /// <param name="p_Geo_Code">Code</param>
        /// <param name="p_Geo_Name">Name</param>
        /// <param name="p_Is_Current">IsCurretn</param>
        /// <param name="p_Is_Deleted">IsDeleted</param>
        /// <param name="p_Status">Status</param>
        /// <param name="p_Company_Id">Company</param>
        /// <param name="p_Geo_Type_Id">Type</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Ip_Address">IP</param>
        /// <returns>On Success "Record Inserted" And Exception.Message On Failure.</returns>
        public string InsertHierarchy(int p_Parent_Geo_Id, string p_Geo_Code, string p_Geo_Name, bool p_Is_Current, bool p_Is_Deleted, int p_Status, int p_Company_Id, int p_Geo_Type_Id, int p_User_Id, int p_Distributor_Id, string p_Ip_Address)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertGEO_HIERARCHY mGeoHierarchy = new spInsertGEO_HIERARCHY();
                mGeoHierarchy.Connection = mConnection;

                mGeoHierarchy.PARENT_GEO_ID = p_Parent_Geo_Id;
                mGeoHierarchy.GEO_CODE = p_Geo_Code;
                mGeoHierarchy.GEO_NAME = p_Geo_Name;
                mGeoHierarchy.ISCURRENT = p_Is_Current;
                mGeoHierarchy.ISDELETED = p_Is_Deleted;
                mGeoHierarchy.STATUS = p_Status;
                mGeoHierarchy.COMPANY_ID = p_Company_Id;
                mGeoHierarchy.GEO_TYPE_ID = p_Geo_Type_Id;
                mGeoHierarchy.USER_ID = p_User_Id;
                mGeoHierarchy.TIME_STAMP = System.DateTime.Now;
                mGeoHierarchy.LASTUPDATE_DATE = System.DateTime.Now;
                mGeoHierarchy.DISTRIBUTOR_ID = p_Distributor_Id;
                mGeoHierarchy.IP_ADDRESS = p_Ip_Address;

                bool a = mGeoHierarchy.ExecuteQuery();
                if (a)
                {
                    return "Record Inserted";
                }
                else
                {
                    return "Record Inserted";
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return exp.Message;
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
        /// Gets Geo Hierarical Data
        /// </summary>
        /// <param name="p_Brand_id">Brand</param>
        /// <param name="p_sku_type_id">Type</param>
        /// <returns>Geo Hierarical Data AS Datatable</returns>
        public DataTable SelectGeoHierarchyData(int p_Brand_id , int p_sku_type_id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                UspGetDataGeohierarchy ObjSelect = new UspGetDataGeohierarchy();
                ObjSelect.Connection = mConnection;  
                ObjSelect.Brand_id = p_Brand_id;
                ObjSelect.SKU_Hie_type_id = p_sku_type_id;

                DataTable dt = ObjSelect.ExecuteTable();
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
        /// Updates Geo Hierarchy
        /// </summary>
        /// <param name="p_Geo_Id">GeoHierarchy</param>
        /// <param name="p_Parent_Geo_Id">ParentGeoHierarchy</param>
        /// <param name="p_Geo_Code">Code</param>
        /// <param name="p_Geo_Name">Name</param>
        /// <param name="p_Is_Current">IsCurrent</param>
        /// <param name="p_Is_Deleted">IsDeleted</param>
        /// <param name="p_Status">Status</param>
        /// <param name="p_Company_Id">Company</param>
        /// <param name="p_Geo_Type_Id">Type</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Ip_Address">IP</param>
        /// <returns>On Success "Record Updated" And Exception.Message On Failure.</returns>
        public string UpdateHierarchy(int p_Geo_Id, int p_Parent_Geo_Id, string p_Geo_Code, string p_Geo_Name, bool p_Is_Current, bool p_Is_Deleted, int p_Status, int p_Company_Id, int p_Geo_Type_Id, int p_User_Id, int p_Distributor_Id, string p_Ip_Address)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateGEO_HIERARCHY mGeoHierarchy = new spUpdateGEO_HIERARCHY();
                mGeoHierarchy.Connection = mConnection;

                mGeoHierarchy.GEO_ID = p_Geo_Id;
                mGeoHierarchy.PARENT_GEO_ID = p_Parent_Geo_Id;
                mGeoHierarchy.GEO_CODE = p_Geo_Code;
                mGeoHierarchy.GEO_NAME = p_Geo_Name;
                mGeoHierarchy.ISCURRENT = p_Is_Current;
                mGeoHierarchy.ISDELETED = p_Is_Deleted;
                mGeoHierarchy.STATUS = p_Status;
                mGeoHierarchy.COMPANY_ID = p_Company_Id;
                mGeoHierarchy.GEO_TYPE_ID = p_Geo_Type_Id;
                mGeoHierarchy.USER_ID = p_User_Id;
                mGeoHierarchy.TIME_STAMP = System.DateTime.Now;
                mGeoHierarchy.LASTUPDATE_DATE = System.DateTime.Now;
                mGeoHierarchy.DISTRIBUTOR_ID = p_Distributor_Id;
                mGeoHierarchy.IP_ADDRESS = p_Ip_Address;

                mGeoHierarchy.ExecuteQuery();
                return "Record Updated";
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return "Record Updated"; 
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
        /// Gets Assigned Regions, Zones, Territories And Towns  
        /// </summary>
        /// <param name="typeid">Type</param>
        /// <param name="Locationtype">Location</param>
        /// <param name="TownId">Town</param>
        /// <returns>Assigned Regions, Zones, Territories And Towns As Datatable</returns>
        public DataTable SelectDistributorHierachyWithType(int typeid, int Locationtype, string TownId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectView_GeoHIERARCHY Selectdata = new UspSelectView_GeoHIERARCHY();
                Selectdata.Connection = mConnection;
                Selectdata.type = typeid;
                Selectdata.Paraid = Locationtype;
                Selectdata.ZoneId = Constants.IntNullValue;
                Selectdata.TerritoryId = Constants.IntNullValue;
                Selectdata.TownId = TownId;
                DataTable dt = Selectdata.ExecuteTable();
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
        /// Gets Assigned Regions, Zones, Territories And Towns  
        /// </summary>
        /// <param name="typeid">Type</param>
        /// <param name="Locationtype">Location</param>
        /// <param name="TownId">Town</param>
        /// <returns>Assigned Regions, Zones, Territories And Towns As Datatable</returns>
        public DataTable SelectDistributorHierachyWithType(int typeid, int RegionId,int ZoneId,int TerritoryId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectView_GeoHIERARCHY Selectdata = new UspSelectView_GeoHIERARCHY();
                Selectdata.Connection = mConnection;
                Selectdata.type = typeid;
                Selectdata.Paraid = RegionId;
                Selectdata.ZoneId = ZoneId;
                Selectdata.TerritoryId = TerritoryId;
                Selectdata.TownId = null;
                DataTable dt = Selectdata.ExecuteTable();
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
        
        
        public DataTable GetSERVICE_TYPE(int p_ST_ID)
        {

            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSERVICE_TYPE mSERVICE_TYPE = new spSelectSERVICE_TYPE();
                mSERVICE_TYPE.Connection = mConnection;

                mSERVICE_TYPE.ST_ID = p_ST_ID;
                //   mPAYMENT_TYPE.IS_ACTIVE = p_IS_ACTIVE;


                return mSERVICE_TYPE.ExecuteTable();

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
        public bool DeleteSERVICE_TYPE(int p_ST_ID, bool p_IS_ACTIVE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeleteSERVICE_TYPE mSERVICE_TYPE = new spDeleteSERVICE_TYPE();
                mSERVICE_TYPE.Connection = mConnection;

                mSERVICE_TYPE.ST_ID = p_ST_ID;
                mSERVICE_TYPE.IS_ACTIVE = p_IS_ACTIVE;

                //  mGeoHierarchy.USER_ID = p_USER_ID;
                bool a = mSERVICE_TYPE.ExecuteQuery();
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
    }

}
