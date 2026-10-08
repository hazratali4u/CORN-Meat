using System;
using CORNDatabaseLayer.Classes;
using CORNDataAccessLayer.Classes;
using CORNCommon.Classes;
using System.Data;

namespace CORNBusinessLayer.Classes
{
    public class SyncServerController
    {
        #region Private Variables

        IDbTransaction mTransaction;
        IDbConnection mConnection;

        #endregion
        public object GetClientLastUpdateDateTime(int p_TYPE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetClientLastUpdateDateTimeForSync ObjSelect = new uspGetClientLastUpdateDateTimeForSync();
                ObjSelect.Connection = mConnection;
                ObjSelect.TYPE = p_TYPE;

                object obj = ObjSelect.ExecuteScalar();
                return obj;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishExceptionforSync(exp, "Error in GetLastUpdateTime sync.");
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

        #region Distributor

        public bool UpdateDistributorClient(int p_CompanyId, int pCoverTable, bool p_IsDeleted, int p_Division_Id
            , int p_User_Id, int p_Region_Id, int p_Zone_Id, int p_SubZone_Id, string p_Contact_Person, string p_Contact_Number, string p_Gst_Number, string p_Password
            , string p_Address1, string p_Address2, int p_Distributor_Id, string p_Distributor_Code, string p_Distributor_Name, string p_Ip_Address, bool p_Is_Registered
            , int p_CREDIT_LEVEL, decimal pGst, bool p_ServiceCharges)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateDISTRIBUTOR mDistributorInfo = new spUpdateDISTRIBUTOR();
                mDistributorInfo.Connection = mConnection;
                mDistributorInfo.COMPANY_ID = p_CompanyId;
                mDistributorInfo.DIST_CLASS_ID = pCoverTable;
                mDistributorInfo.DIVISION_ID = p_Division_Id;
                mDistributorInfo.USER_ID = p_User_Id;
                mDistributorInfo.REGION_ID = p_Region_Id;
                mDistributorInfo.ZONE_ID = p_Zone_Id;
                mDistributorInfo.SUBZONE_ID = p_SubZone_Id;
                mDistributorInfo.CREDIT_LEVEL = p_CREDIT_LEVEL;
                mDistributorInfo.CONTACT_PERSON = p_Contact_Person;
                mDistributorInfo.CONTACT_NUMBER = p_Contact_Number;
                mDistributorInfo.GST_NUMBER = p_Gst_Number;
                mDistributorInfo.PASSWORD = p_Password;
                mDistributorInfo.ADDRESS1 = p_Address1;
                mDistributorInfo.ADDRESS2 = p_Address2;
                mDistributorInfo.DISTRIBUTOR_ID = p_Distributor_Id;
                mDistributorInfo.DISTRIBUTOR_CODE = p_Distributor_Code;
                mDistributorInfo.DISTRIBUTOR_NAME = p_Distributor_Name;
                mDistributorInfo.IP_ADDRESS = p_Ip_Address;
                mDistributorInfo.IS_REGISTERED = p_Is_Registered;
                mDistributorInfo.ISDELETED = p_IsDeleted;
                mDistributorInfo.GST = pGst;
                mDistributorInfo.SERVICE_CHARGES = p_ServiceCharges;
                mDistributorInfo.ExecuteQuery();
                return true;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishExceptionforSync(exp, "Error in UpdateDistributorClient sync.");
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

        public bool DeleteUserAssignmentClient(int p_DISTRIBUTOR_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spDeleteDISTRIBUTOR_ASSIGNMENTClient mDel = new spDeleteDISTRIBUTOR_ASSIGNMENTClient();
                mDel.Connection = mConnection;
                mDel.Transaction = mTransaction;
                mDel.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mDel.ExecuteQuery();
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishExceptionforSync(exp, "Error in DeleteUserAssignmentClient sync.");
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

        public bool InsertUserAssignmentClient(int p_DISTRIBUTOR_ID, DataTable dtDistributorAssignment)
        {
            bool flag = false;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertDISTRIBUTOR_ASSIGNMENTClient mUserAssing = new spInsertDISTRIBUTOR_ASSIGNMENTClient();
                mUserAssing.Connection = mConnection;
                mUserAssing.Transaction = mTransaction;

                foreach (DataRow dr in dtDistributorAssignment.Rows)
                {
                    mUserAssing.USER_ID = Convert.ToInt32(dr["USER_ID"]);
                    mUserAssing.DISTRIBUTOR_TYPE = Convert.ToInt32(dr["DISTRIBUTOR_TYPE"]);
                    mUserAssing.DISTRIBUTOR_ID = Convert.ToInt32(dr["DISTRIBUTOR_ID"]);
                    mUserAssing.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    mUserAssing.LASTUPDATE_DATE = Convert.ToDateTime(dr["LASTUPDATE_DATE"]);
                    flag = mUserAssing.ExecuteQuery();
                }
                mTransaction.Commit();
                return flag;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishExceptionforSync(exp, "Error in InsertUserAssignmentClient sync.");
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

        #region Role

        public bool InsertRoleMasterClient(int p_ROLE_ID, string p_ROLE_NAME, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertROLE_MASTERClient ObjInsert = new spInsertROLE_MASTERClient();
                ObjInsert.Connection = mConnection;
                ObjInsert.ROLE_ID = p_ROLE_ID;
                ObjInsert.ROLE_NAME = p_ROLE_NAME;
                ObjInsert.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                bool Bvalue = ObjInsert.ExecuteQuery();
                return Bvalue;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishExceptionforSync(exp, "Error in InsertRoleMasterClient sync.");
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

        public bool DeleteRoleDetailClient()
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spDeleteROLE_DETAILClient mDel = new spDeleteROLE_DETAILClient();
                mDel.Connection = mConnection;
                mDel.Transaction = mTransaction;
                mDel.ExecuteQuery();
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishExceptionforSync(exp, "Error in InsertRoleDetailClient sync.");
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

        public bool InsertRoleDetailClient(DataTable dtRoleDetail)
        {
            bool flag = false;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertROLE_DETAILClient mRoleDetail = new spInsertROLE_DETAILClient();
                mRoleDetail.Connection = mConnection;
                mRoleDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtRoleDetail.Rows)
                {
                    mRoleDetail.ROLE_ID = Convert.ToInt32(dr["ROLE_ID"]);
                    mRoleDetail.MODULE_ID = Convert.ToInt32(dr["MODULE_ID"]);
                    mRoleDetail.LASTUPDATE_DATE = Convert.ToDateTime(dr["LASTUPDATE_DATE"]);
                    flag = mRoleDetail.ExecuteQuery();
                }
                mTransaction.Commit();
                return flag;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishExceptionforSync(exp, "Error in InsertRoleDetailClient sync.");
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

        #region SKUS
        public string InsertSKUSClient(int p_SKU_ID, bool p_IsExempted, bool p_IsActive, char p_Gst_On, int p_Company_Id, int p_Division_Id, int p_Category_Id, int p_Brand_Id, decimal p_GST_Rate_Reg, decimal p_GST_Rate_Unreg, short p_Units_In_Case, string p_Sku_Code, string p_Sku_Name, string p_Ip_Address, string p_packSize, int p_UserId, int Companyid, string p_DESCRIPTION, int pSectionId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSKUSClient mSkus = new spInsertSKUSClient();

                mSkus.Connection = mConnection;
                mSkus.SKU_ID = p_SKU_ID;
                mSkus.PRINCIPAL_ID = p_Company_Id;
                mSkus.ISEXEMPTED = p_IsExempted;
                mSkus.ISACTIVE = p_IsActive;
                mSkus.GST_ON = p_Gst_On;
                mSkus.COMPANY_ID = Companyid;
                mSkus.DIVISION_ID = p_Division_Id;
                mSkus.BRAND_ID = p_Brand_Id;
                mSkus.CATEGORY_ID = p_Category_Id;
                if (!p_IsExempted)
                {
                    mSkus.GST_RATE_REG = p_GST_Rate_Reg;
                    mSkus.GST_RATE_UNREG = p_GST_Rate_Unreg;
                }
                else
                {
                    mSkus.GST_RATE_REG = 0;
                    mSkus.GST_RATE_UNREG = 0;
                }
                mSkus.UNITS_IN_CASE = p_Units_In_Case;
                mSkus.SKU_NAME = p_Sku_Name;
                mSkus.SKU_CODE = p_Sku_Code;
                mSkus.TIME_STAMP = System.DateTime.Now;
                mSkus.LASTUPDATE_DATE = System.DateTime.Now;
                mSkus.IP_ADDRESS = p_Ip_Address;
                mSkus.PACKSIZE = p_packSize;
                mSkus.USER_ID = p_UserId;
                mSkus.DESCRIPTION = p_DESCRIPTION;
                mSkus.SECTION_ID = pSectionId;

                mSkus.ExecuteQuery();

                return mSkus.SKU_ID.ToString();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishExceptionforSync(exp, "Error in InsertSKUClient sync.");
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

        public string InsertSKUHierarchyClient(int p_SKU_HIE_ID, int p_Sku_Type_Id, int p_Parent_Sku_Hie_Id, string p_Sku_Hie_Code, string p_Sku_Hie_Name, string p_Ip_Address, bool p_Is_Active, int Companyid, bool p_IS_MANUALDISCOUNT, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSKU_HIERARCHYClient mSkuHierarchy = new spInsertSKU_HIERARCHYClient();
                mSkuHierarchy.Connection = mConnection;
                mSkuHierarchy.SKU_HIE_ID = p_SKU_HIE_ID;
                mSkuHierarchy.SKU_HIE_TYPE_ID = p_Sku_Type_Id;
                mSkuHierarchy.PARENT_SKU_HIE_ID = p_Parent_Sku_Hie_Id;
                mSkuHierarchy.SKU_HIE_CODE = p_Sku_Hie_Code;
                mSkuHierarchy.SKU_HIE_NAME = p_Sku_Hie_Name;
                mSkuHierarchy.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                mSkuHierarchy.IP_ADDRESS = p_Ip_Address;
                mSkuHierarchy.IS_ACTIVE = p_Is_Active;
                mSkuHierarchy.COMPANY_ID = Companyid;
                mSkuHierarchy.IS_MANUALDISCOUNT = p_IS_MANUALDISCOUNT;
                mSkuHierarchy.ExecuteQuery();
                return "Record Inserted";

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishExceptionforSync(exp, "Error in InsertSKUHierarchyClient sync.");
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

        public string InsertSKU_PRICESClient(int p_Distributor_Id, int p_Sku_Id, decimal p_Distributor_Discount, decimal p_Tax_Price, decimal p_Distributor_Price, decimal p_Trade_Price, decimal p_Retail_Price, DateTime p_date_effected, decimal p_SED_Tax, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspProcessSKU_PricesClient mSkuPrices = new UspProcessSKU_PricesClient();
                mSkuPrices.Connection = mConnection;
                mSkuPrices.DISTRIBUTOR_ID = p_Distributor_Id;
                mSkuPrices.DISTRIBUTOR_PRICE = p_Distributor_Price;
                mSkuPrices.SKU_ID = p_Sku_Id;
                mSkuPrices.RETAIL_PRICE = p_Retail_Price;
                mSkuPrices.TAX_PRICE = p_Tax_Price;
                mSkuPrices.TRADE_PRICE = p_Trade_Price;
                mSkuPrices.DISTRIBUTOR_DISCOUNT = p_Distributor_Discount;
                mSkuPrices.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                mSkuPrices.DATE_EFFECTED = p_date_effected;
                mSkuPrices.SED_TAX = p_SED_Tax;
                mSkuPrices.ExecuteQuery();
                return "Record Inserted";

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishExceptionforSync(exp, "Error in InsertSKU_PRICESClient sync.");
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

        #region Module

        public bool Insert_ModuleClient(int p_MODULE_ID, string p_MODULE_DESCRIPTION, int p_MODULE_PARENT_ID, int p_MODULE_TYPE_ID, DateTime p_LASTUPDATE_DATE, bool p_IS_ACTIVE, string p_MODULE_KEY)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertMODULEClient ObjInsert = new spInsertMODULEClient();
                ObjInsert.Connection = mConnection;
                ObjInsert.MODULE_ID = p_MODULE_ID;
                ObjInsert.MODULE_DESCRIPTION = p_MODULE_DESCRIPTION;
                ObjInsert.MODULE_PARENT_ID = p_MODULE_PARENT_ID;
                ObjInsert.MODULE_TYPE_ID = p_MODULE_TYPE_ID;
                ObjInsert.IS_ACTIVE = p_IS_ACTIVE;
                ObjInsert.MODULE_KEY = p_MODULE_KEY;
                ObjInsert.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                ObjInsert.ExecuteQuery();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishExceptionforSync(exp, "Error in Insert_ModuleClient sync.");
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

        #region Employee

        public string InsertDistributor_UserClient(int p_USER_ID, int p_CompanyId, string p_NIC, bool p_Is_Active, DateTime p_Lastupdate_Date, int p_User_Type_Id, int p_Distributor_Id,
            int p_Role_Id, string p_Email_Address, string p_Address1, string p_Address2, string p_Login_Id, string p_Password, string p_Mobile, string p_User_Code, string p_User_Name, string p_Phone, int p_DEPT_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertDISTRIBUTOR_USERClient mDistributorUser = new spInsertDISTRIBUTOR_USERClient();
                mDistributorUser.Connection = mConnection;
                mDistributorUser.USER_ID = p_USER_ID;
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
                mDistributorUser.LASTUPDATE_DATE = p_Lastupdate_Date;
                mDistributorUser.USER_CODE = p_User_Code;
                mDistributorUser.USER_NAME = p_User_Name;
                mDistributorUser.USER_TYPE_ID = p_User_Type_Id;
                mDistributorUser.IS_ACTIVE = p_Is_Active;
                mDistributorUser.DEPT_ID = p_DEPT_ID;
                mDistributorUser.ExecuteQuery();
                return mDistributorUser.USER_ID.ToString();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishExceptionforSync(exp, "Error in InsertDistributor_UserClient sync.");
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

        #region User

        public string InserUserClient(int User_Id, int p_CompanyId, int p_DistributorId, string p_LoginId, string p_Password, int p_RoleId, bool p_IsLessRight, bool p_IsDelRight, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsert_USERClient mSlashUser = new spInsert_USERClient();
                mSlashUser.Connection = mConnection;
                mSlashUser.USER_ID = User_Id;
                mSlashUser.DISTRIBUTOR_ID = p_DistributorId;
                mSlashUser.COMPANY_ID = p_CompanyId;
                mSlashUser.LOGIN_ID = p_LoginId;
                mSlashUser.PASSWORD = p_Password;
                mSlashUser.ROLE_ID = p_RoleId;
                mSlashUser.IsDelRight = p_IsDelRight;
                mSlashUser.IsLessRight = p_IsLessRight;
                mSlashUser.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                mSlashUser.ExecuteQuery();
                return mSlashUser.USER_ID.ToString();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishExceptionforSync(exp, "Error in InsertUserClient sync.");
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

        #endregion

        #region Tables

        public bool InsertTableDefinationClient(int p_TableDefination_ID, int pDistributorId, string p_TableDefination_No, string p_TableDefination_Description,
            string p_TableDefination_Capacity, string p_TableDefination_Abbrivation, bool p_Is_Active, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertTableDefinationClient mTableDefination = new spInsertTableDefinationClient();
                mTableDefination.Connection = mConnection;
                mTableDefination.TableDefination_ID = p_TableDefination_ID;
                mTableDefination.distributorId = pDistributorId;
                mTableDefination.p_TableDefination_No = p_TableDefination_No;
                mTableDefination.p_TableDefination_Description = p_TableDefination_Description;
                mTableDefination.p_TableDefination_Capacity = p_TableDefination_Capacity;
                mTableDefination.p_TableDefination_Abbrivation = p_TableDefination_Abbrivation;
                mTableDefination.Is_Active = p_Is_Active;
                mTableDefination.p_USER_ID = p_USER_ID;
                bool a = mTableDefination.ExecuteQuery();
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

        #region Finished Goods

        public bool InsertFinishedGoodMasterClient(int p_FINISHED_GOOD_MASTER_ID, int p_FINISHED_SKU_ID, DateTime p_DOCUMENT_DATE, int p_USER_ID, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspInsertFinishedMasterClient mFinishedMaster = new uspInsertFinishedMasterClient();
                mFinishedMaster.Connection = mConnection;
                mFinishedMaster.FINISHED_GOOD_MASTER_ID = p_FINISHED_GOOD_MASTER_ID;
                mFinishedMaster.FINISHED_SKU_ID = p_FINISHED_SKU_ID;
                mFinishedMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mFinishedMaster.USER_ID = p_USER_ID;
                mFinishedMaster.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                bool a = mFinishedMaster.ExecuteQuery();
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

        public bool InsertFinishedGoodDetailClient(int p_FINISHED_GOOD_MASTER_ID, int p_SKU_ID, decimal p_QUANTITY, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspInsertFinishedDetailClient mFinishedMaster = new uspInsertFinishedDetailClient();
                mFinishedMaster.Connection = mConnection;
                mFinishedMaster.FINISHED_GOOD_MASTER_ID = p_FINISHED_GOOD_MASTER_ID;
                mFinishedMaster.SKU_ID = p_SKU_ID;
                mFinishedMaster.QUANTITY = p_QUANTITY;
                mFinishedMaster.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                bool a = mFinishedMaster.ExecuteQuery();
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

        #region Product Section

        public bool InsertProductSectionClient(int p_SECTION_ID, string SECTION_CODE, string SECTION_NAME, string PRINTER_NAME, bool p_IS_ACTIVE, DateTime p_LAST_UPDATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertPRODUCT_SECTIONClient mDepartment = new spInsertPRODUCT_SECTIONClient();
                mDepartment.Connection = mConnection;
                mDepartment.SECTION_ID = p_SECTION_ID;
                mDepartment.SECTION_CODE = SECTION_CODE;
                mDepartment.SECTION_NAME = SECTION_NAME;
                mDepartment.PRINTER_NAME = PRINTER_NAME;
                mDepartment.IS_ACTIVE = p_IS_ACTIVE;
                mDepartment.LAST_UPDATE = p_LAST_UPDATE;
                bool a = mDepartment.ExecuteQuery();
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
