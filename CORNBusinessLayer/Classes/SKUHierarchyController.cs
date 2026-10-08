using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;
using CORNDatabaseLayer.Classes;
using System.Collections.Generic;

namespace CORNBusinessLayer.Classes
{
    /// <summary>
    /// Class For SKU Heirarchy Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert SKU Heirarchy
    /// </item>
    /// <term>
    /// Update SKU Heirarchy
    /// </term>
    /// <item>
    /// Get SKU Heirarchy
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
	public class SkuHierarchyController
	{
		#region Contructors

        /// <summary>
        /// Constructor for SkuHierarchyController
        /// </summary>
		public SkuHierarchyController()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#endregion
        
		#region Public Method

        #region Select

        /// <summary>
        /// Gets SKU Hierarchy
        /// </summary>
        /// Returns SKU Hierarchy Data as Datatable
        /// <param name="p_Sku_Type_Id">Type</param>
        /// <param name="p_Sku_Hie_Id">Hierarchy</param>
        /// <param name="p_Parent_Sku_Hie_Id">Parent</param>
        /// <param name="p_Sku_Hie_Code">Code</param>
        /// <param name="p_Sku_Hie_Name">Name</param>
        /// <param name="p_Is_Active">IsActive</param>
        /// <param name="Companyid">Company</param>
        /// <returns>SKU Hierarchy Data as Datatable</returns>
        public DataTable SelectSkuHierarchy(int p_Sku_Type_Id, int p_Sku_Hie_Id, int p_Parent_Sku_Hie_Id, string p_Sku_Hie_Code, string p_Sku_Hie_Name, bool p_Is_Active, int Companyid, int p_Parent_sku_hie_TypeId)
		{
			IDbConnection mConnection = null;
			try
			{
				mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
				mConnection.Open();
				spSelectSKU_HIERARCHY mSkuHierarchy = new spSelectSKU_HIERARCHY();
				mSkuHierarchy.Connection = mConnection;
                
				mSkuHierarchy.SKU_HIE_TYPE_ID = p_Sku_Type_Id;
				mSkuHierarchy.SKU_HIE_ID = p_Sku_Hie_Id;
				mSkuHierarchy.PARENT_SKU_HIE_ID = p_Parent_Sku_Hie_Id;
				mSkuHierarchy.SKU_HIE_CODE = p_Sku_Hie_Code;
				mSkuHierarchy.SKU_HIE_NAME = p_Sku_Hie_Name;
				mSkuHierarchy.TIME_STAMP = Constants.DateNullValue ;
				mSkuHierarchy.LASTUPDATE_DATE = Constants .DateNullValue ;
				mSkuHierarchy.IP_ADDRESS = null;
				mSkuHierarchy.IS_ACTIVE = p_Is_Active;
                mSkuHierarchy.COMPANY_ID = Companyid;
                mSkuHierarchy.PARENT_SKU_HIE_TYPE_ID = p_Parent_sku_hie_TypeId;

                DataTable dt = mSkuHierarchy.ExecuteTable();
				return dt;
				
			}
			catch(Exception exp)
			{
				ExceptionPublisher.PublishException(exp);				
				return null;
			}
			finally
			{
				if(mConnection != null && mConnection.State == ConnectionState.Open)
				{
					mConnection.Close();
				}
			}
		}

        public DataTable SelectSkuHierarchy(int p_Sku_Type_Id, int p_Sku_Hie_Id, int p_Parent_Sku_Hie_Id, string p_Sku_Hie_Code, string p_Sku_Hie_Name, bool p_Is_Active, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSKU_HIERARCHY mSkuHierarchy = new spSelectSKU_HIERARCHY();
                mSkuHierarchy.Connection = mConnection;

                mSkuHierarchy.SKU_HIE_TYPE_ID = p_Sku_Type_Id;
                mSkuHierarchy.SKU_HIE_ID = p_Sku_Hie_Id;
                mSkuHierarchy.PARENT_SKU_HIE_ID = p_Parent_Sku_Hie_Id;
                mSkuHierarchy.SKU_HIE_CODE = p_Sku_Hie_Code;
                mSkuHierarchy.SKU_HIE_NAME = p_Sku_Hie_Name;
                mSkuHierarchy.TIME_STAMP = Constants.DateNullValue;
                mSkuHierarchy.LASTUPDATE_DATE = Constants.DateNullValue;
                mSkuHierarchy.IP_ADDRESS = null;
                mSkuHierarchy.IS_ACTIVE = p_Is_Active;
                mSkuHierarchy.COMPANY_ID = Companyid;
                

                DataTable dt = mSkuHierarchy.ExecuteTable();
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
        /// Gets SKU Hierarchy
        /// </summary>
        /// <remarks>
        /// Returns SKU Hierarchy Data as Datatable
        /// </remarks>
        /// <param name="p_Sku_Type_Hie_Id">Hierarchy</param>
        /// <param name="Companyid">Company</param>
        /// <returns>SKU Hierarchy Data as Datatable</returns>
        public DataTable SelectSkuHierarchy(int p_Sku_Type_Hie_Id, int Companyid)
		{
			IDbConnection mConnection = null;
			try
			{
				mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
				mConnection.Open();
				spSelectSKU_HIERARCHY mSkuHierarchy = new spSelectSKU_HIERARCHY();
				mSkuHierarchy.Connection = mConnection;
                mSkuHierarchy.SKU_HIE_TYPE_ID = p_Sku_Type_Hie_Id;
                mSkuHierarchy.COMPANY_ID  = Companyid;   
				DataTable dt = mSkuHierarchy.ExecuteTable();
				return dt;
				
			}
			catch(Exception exp)
			{
				ExceptionPublisher.PublishException(exp);				
				return null;
			}
			finally
			{
				if(mConnection != null && mConnection.State == ConnectionState.Open)
				{
					mConnection.Close();
				}
			}
		}
        
        /// <summary>
        /// Gets SKU Hierarchy
        /// </summary>
        /// <remarks>
        /// Returns SKU Hierarchy Data as Datatable
        /// </remarks>
        /// <param name="p_Sku_Type_Id"></param>
        /// <param name="p_Parent_Sku_Hie_Id"></param>
        /// <param name="Companyid"></param>
        /// <returns>SKU Hierarchy Data as Datatable</returns>
        public DataTable SelectSkuHierarchy(int p_Sku_Type_Id, int p_Parent_Sku_Hie_Id, int Companyid)
		{
			IDbConnection mConnection = null;
			try
			{
				mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
				mConnection.Open();
				spSelectSKU_HIERARCHY mSkuHierarchy = new spSelectSKU_HIERARCHY();
				mSkuHierarchy.Connection = mConnection;
                
				mSkuHierarchy.SKU_HIE_TYPE_ID = p_Sku_Type_Id;
				mSkuHierarchy.PARENT_SKU_HIE_ID=p_Parent_Sku_Hie_Id;
                mSkuHierarchy.COMPANY_ID = Companyid;    
				
				

				DataTable dt = mSkuHierarchy.ExecuteTable();
				return dt;
				
			}
			catch(Exception exp)
			{
				ExceptionPublisher.PublishException(exp);				
				return null;
			}
			finally
			{
				if(mConnection != null && mConnection.State == ConnectionState.Open)
				{
					mConnection.Close();
				}
			}
		}

        /// <summary>
        /// Gets SKU Hierarchy
        /// </summary>
        /// <remarks>
        /// Returns SKU Hierarchy Data as Datatable
        /// </remarks>
        /// <param name="p_Sku_Type_Id">Type</param>
        /// <param name="p_Parent_Sku_Hie_Id">Parent</param>
        /// <returns>SKU Hierarchy Data as Datatable</returns>
        public DataTable SelectDTSkuHierarchy(int p_Sku_Type_Id,int p_Parent_Sku_Hie_Id)
		{
			IDbConnection mConnection = null;
			try
			{
				mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
				mConnection.Open();
				spSelectSKU_HIERARCHY mSkuHierarchy = new spSelectSKU_HIERARCHY();
				mSkuHierarchy.Connection = mConnection;
                
				mSkuHierarchy.SKU_HIE_TYPE_ID = p_Sku_Type_Id;
				mSkuHierarchy.PARENT_SKU_HIE_ID=p_Parent_Sku_Hie_Id;
				
				

				DataTable dt = mSkuHierarchy.ExecuteTable();
				return dt;
				
			}
			catch(Exception exp)
			{
				ExceptionPublisher.PublishException(exp);				
				return null;
			}
			finally
			{
				if(mConnection != null && mConnection.State == ConnectionState.Open)
				{
					mConnection.Close();
				}
			}
		}
        		
        /// <summary>
        /// Gets SKU Hierarchy
        /// </summary>
        /// <remarks>
        /// Returns SKU Hierarchy Data as Datatable
        /// </remarks>
        /// <param name="p_type">Type</param>
        /// <param name="CompanyId">Company</param>
        /// <returns>SKU Hierarchy Data as Datatable</returns>
        public DataTable SelectSkuHierarchyView(int p_type,int CompanyId)
		{
			IDbConnection mConnection = null;
			try
			{
				mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
				mConnection.Open();
				UspSelectView_SKUHIERARCHY mSkuHierarchyInfo = new UspSelectView_SKUHIERARCHY();
				mSkuHierarchyInfo.Connection = mConnection;
                mSkuHierarchyInfo.MCompany_id = CompanyId;   
				mSkuHierarchyInfo.type = p_type;
				
				DataTable dt = mSkuHierarchyInfo.ExecuteTable();
				return dt;
				
			}
			catch(Exception exp)
			{
				ExceptionPublisher.PublishException(exp);				
				return null;
			}
			finally
			{
				if(mConnection != null && mConnection.State == ConnectionState.Open)
				{
					mConnection.Close();
				}
			}
        }

        /// <summary>
        /// Gets SKU Hierarchy
        /// </summary>
        /// <remarks>
        /// Returns SKU Hierarchy Data as Datatable
        /// </remarks>
        /// <param name="p_type">Type</param>
        /// <param name="p_company">Company</param>
        /// <param name="p_division">Division</param>
        /// <param name="p_category">Category</param>
        /// <param name="p_brand">Brand</param>
        /// <param name="Companyid">Company</param>
        /// <returns>SKU Hierarchy Data as Datatable</returns>
        public DataTable SelectDTSkuHierarchy01(int p_type, int p_company, int p_division, int p_category, int p_brand, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectView_SKUHIERARCHY mSkuHierarchy = new UspSelectView_SKUHIERARCHY();
                mSkuHierarchy.Connection = mConnection;

                mSkuHierarchy.type = p_type;
                mSkuHierarchy.Company_id = p_company;
                mSkuHierarchy.Division_id = p_division;
                mSkuHierarchy.Category_Id = p_category;
                mSkuHierarchy.Brand_Id = p_brand;
                mSkuHierarchy.MCompany_id = Companyid;
                DataTable dt = new DataTable();
                dt = mSkuHierarchy.ExecuteTable();
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
        /// Gets SKU Hierarchy
        /// </summary>
        /// <remarks>
        /// Returns SKU Hierarchy Data as Datatable
        /// </remarks>
        /// <param name="p_type">Type</param>
        /// <param name="p_UserId">User</param>
        /// <returns>SKU Hierarchy Data as Datatable</returns>
        public DataTable SelectBrandAssignment(int p_type, int p_UserId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspGetBrandAssignment mSkuHierarchy = new UspGetBrandAssignment();
                mSkuHierarchy.Connection = mConnection;

                mSkuHierarchy.TYPE_ID = p_type;
                mSkuHierarchy.USER_ID = p_UserId;
                DataTable dt = new DataTable();
                dt = mSkuHierarchy.ExecuteTable();
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

        public DataTable SelectDealAssignment(int p_ItemDealId, int p_CategoryId, int p_distributorId, int p_TypeId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spITEM_DEAL_ASSIGNMENT mInsertBrand = new spITEM_DEAL_ASSIGNMENT();
                mInsertBrand.Connection = mConnection;

                mInsertBrand.ITEM_DEAL_ID = p_ItemDealId;
                mInsertBrand.CATEGORY_ID = p_CategoryId;
                mInsertBrand.DISTRIBUTOR_ID = p_distributorId;
                mInsertBrand.TYPE_ID = p_TypeId;

                DataTable dt = mInsertBrand.ExecuteTable();
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

        public string SelectDealAssignment(int p_ItemDealId, int p_distributorId, int p_TypeId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spITEM_DEAL_ASSIGNMENT mInsertBrand = new spITEM_DEAL_ASSIGNMENT();
                mInsertBrand.Connection = mConnection;

                mInsertBrand.ITEM_DEAL_ID = p_ItemDealId;
                mInsertBrand.CATEGORY_ID = 0;
                mInsertBrand.DISTRIBUTOR_ID = p_distributorId;
                mInsertBrand.TYPE_ID = p_TypeId;

                string dt = mInsertBrand.ExecuteScalar();
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


        #region Added By Hazrat Ali

        /// <summary>
        /// Gets SKU Hierarchy
        /// </summary>
        /// <remarks>
        /// Returns SKU Hierarchy Data as Datatable
        /// </remarks>
        /// <param name="p_Principal_ID">Principal</param>
        /// <param name="p_Is_Active">IsActive</param>
        /// <returns>SKU Hierarchy Data as Datatable</returns>
        public DataTable SelectSKUCategories(int p_Principal_ID, bool p_Is_Active)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetSKUCategories mSKUCategory = new uspGetSKUCategories();
                mSKUCategory.Connection = mConnection;
                mSKUCategory.PRINCIPAL_ID = p_Principal_ID;
                mSKUCategory.IS_ACTIVE = p_Is_Active;
                DataTable dt = mSKUCategory.ExecuteTable();
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
        /// Gets Principals
        /// </summary>
        /// <remarks>
        /// Returns Principal Data as Datatable
        /// </remarks>
        /// <param name="p_Sku_Type_Hie_Id">Type</param>
        /// <param name="Companyid">Company</param>
        /// <returns>Principal Data as Datatable</returns>
        public DataTable SelectPrincipal(int p_Sku_Type_Hie_Id, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPRINCIPAL mPRINCIPAL = new spSelectPRINCIPAL();
                mPRINCIPAL.Connection = mConnection;
                mPRINCIPAL.SKU_HIE_TYPE_ID = p_Sku_Type_Hie_Id;
                mPRINCIPAL.COMPANY_ID = Companyid;
                DataTable dt = mPRINCIPAL.ExecuteTable();
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
        /// Gets PromotionType For A User
        /// </summary>
        /// <remarks>
        /// Returns PromotionType IsManual or Auto For A User as Datatable
        /// </remarks>
        /// <param name="p_USER_ID">User</param>
        /// <returns>PromotionType IsManual or Auto For A User as Datatable</returns>
        public DataTable GetBrandAssignment(int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetBRAND_ASSIGNMENT mBrand = new spGetBRAND_ASSIGNMENT();
                mBrand.Connection = mConnection;
                mBrand.USER_ID = p_USER_ID;

                DataTable dt = mBrand.ExecuteTable();
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

        #endregion

        #region Insert, Update, Delete

        /// <summary>
        /// Insert SKU Hierarchy
        /// </summary>
        /// <remarks>
        /// Returns "Record Inserted" on Success And Exception.Message on Failure
        /// </remarks>
        /// <param name="p_Sku_Type_Id">Type</param>
        /// <param name="p_Parent_Sku_Hie_Id">Hierarchy</param>
        /// <param name="p_Sku_Hie_Code">Code</param>
        /// <param name="p_Sku_Hie_Name">Name</param>
        /// <param name="p_Ip_Address">Address</param>
        /// <param name="p_Is_Active">IsActive</param>
        /// <param name="Companyid">Company</param>
        /// <returns>"Record Inserted" on Success And Exception.Message on Failure</returns>
        public string InsertHierarchy(int p_Sku_Type_Id, int p_Parent_Sku_Hie_Id, string p_Sku_Hie_Code, string p_Sku_Hie_Name, string p_Ip_Address, bool p_Is_Active, int Companyid, int p_Parent_sku_hie_TypeId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSKU_HIERARCHY mSkuHierarchy = new spInsertSKU_HIERARCHY();
                mSkuHierarchy.Connection = mConnection;

                mSkuHierarchy.SKU_HIE_TYPE_ID = p_Sku_Type_Id;

                mSkuHierarchy.PARENT_SKU_HIE_ID = p_Parent_Sku_Hie_Id;
                mSkuHierarchy.SKU_HIE_CODE = p_Sku_Hie_Code;
                mSkuHierarchy.SKU_HIE_NAME = p_Sku_Hie_Name;
                mSkuHierarchy.TIME_STAMP = System.DateTime.Now; ;
                mSkuHierarchy.LASTUPDATE_DATE = System.DateTime.Now; ;
                mSkuHierarchy.IP_ADDRESS = p_Ip_Address;
                mSkuHierarchy.IS_ACTIVE = p_Is_Active;
                mSkuHierarchy.COMPANY_ID = Companyid;
                mSkuHierarchy.PARENT_SKU_HIE_TYPE_ID = p_Parent_sku_hie_TypeId;
                mSkuHierarchy.IS_MANUALDISCOUNT = false;
                mSkuHierarchy.ExecuteQuery();
                return "Record Inserted";

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
        /// Updates SKU Hierarchy
        /// </summary>
        /// <remarks>
        /// Returns "Record Updated" On Success And Exception.Message On Failure
        /// </remarks>
        /// <param name="p_Sku_Type_Id">Type</param>
        /// <param name="p_Sku_Hie_Id">Hierarchy</param>
        /// <param name="p_Parent_Sku_Hie_Id">Parent</param>
        /// <param name="p_Sku_Hie_Code">Code</param>
        /// <param name="p_Sku_Hie_Name">Name</param>
        /// <param name="p_Ip_Address">IP</param>
        /// <param name="p_Is_Active">IsActive</param>
        /// <param name="Companyid">Compay</param>
        /// <param name="p_IsManualDiscount">PromotionType</param>
        /// <returns>"Record Updated" On Success And Exception.Message On Failure</returns>
        public string UpdateHierarchy(int p_Sku_Type_Id, int p_Sku_Hie_Id, int p_Parent_Sku_Hie_Id, string p_Sku_Hie_Code, string p_Sku_Hie_Name, string p_Ip_Address, bool p_Is_Active, int Companyid,bool p_IsManualDiscount)
		{
			IDbConnection mConnection = null;
			try
			{
				mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
				mConnection.Open();
				spUpdateSKU_HIERARCHY  mSkuHierarchy = new spUpdateSKU_HIERARCHY ();
				mSkuHierarchy.Connection = mConnection;
				
				mSkuHierarchy.SKU_HIE_TYPE_ID = p_Sku_Type_Id;
				mSkuHierarchy.SKU_HIE_ID = p_Sku_Hie_Id;
				mSkuHierarchy.PARENT_SKU_HIE_ID = p_Parent_Sku_Hie_Id;
				mSkuHierarchy.SKU_HIE_CODE = p_Sku_Hie_Code;
				mSkuHierarchy.SKU_HIE_NAME = p_Sku_Hie_Name;	
				mSkuHierarchy.TIME_STAMP = System.DateTime.Now; ;
				mSkuHierarchy.LASTUPDATE_DATE = System.DateTime.Now;;
				mSkuHierarchy.IP_ADDRESS = p_Ip_Address  ;
				mSkuHierarchy.IS_ACTIVE = p_Is_Active;
                mSkuHierarchy.COMPANY_ID = Companyid;
                mSkuHierarchy.IS_MANUALDISCOUNT = p_IsManualDiscount;  
                mSkuHierarchy.ExecuteQuery();
				return "Record Updated";

			}			
			catch(Exception exp)
			{
				ExceptionPublisher.PublishException(exp);				
				return exp.Message;
			}
			finally
			{
				if(mConnection != null && mConnection.State == ConnectionState.Open)
				{
					mConnection.Close();
				}
			}
		}
        
        /// <summary>
        /// Updates SKU Hierarchy
        /// </summary>
        /// <remarks>
        /// Returns "Record Updated" On Success And Exception.Message On Failure
        /// </remarks>
        /// <param name="p_Sku_Type_Id">Type</param>
        /// <param name="p_Sku_Hie_Id">Hierarchy</param>
        /// <param name="p_Parent_Sku_Hie_Id">Parent</param>
        /// <param name="p_Sku_Hie_Code">Code</param>
        /// <param name="p_Sku_Hie_Name">Name</param>
        /// <param name="p_Ip_Address">IP</param>
        /// <param name="p_Is_Active">IsActive</param>
        /// <param name="Companyid">Company</param>
        /// <returns>"Record Updated" On Success And Exception.Message On Failure</returns>
        public string UpdateHierarchy(int p_Sku_Type_Id, int p_Sku_Hie_Id, int p_Parent_Sku_Hie_Id, string p_Sku_Hie_Code, string p_Sku_Hie_Name, string p_Ip_Address, bool p_Is_Active, int Companyid, int p_Parent_sku_hie_TypeId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateSKU_HIERARCHY mSkuHierarchy = new spUpdateSKU_HIERARCHY();
                mSkuHierarchy.Connection = mConnection;

                mSkuHierarchy.SKU_HIE_TYPE_ID = p_Sku_Type_Id;
                mSkuHierarchy.SKU_HIE_ID = p_Sku_Hie_Id;
                mSkuHierarchy.PARENT_SKU_HIE_ID = p_Parent_Sku_Hie_Id;
                mSkuHierarchy.SKU_HIE_CODE = p_Sku_Hie_Code;
                mSkuHierarchy.SKU_HIE_NAME = p_Sku_Hie_Name;
                mSkuHierarchy.TIME_STAMP = System.DateTime.Now; ;
                mSkuHierarchy.LASTUPDATE_DATE = System.DateTime.Now; ;
                mSkuHierarchy.IP_ADDRESS = p_Ip_Address;
                mSkuHierarchy.IS_ACTIVE = p_Is_Active;
                mSkuHierarchy.COMPANY_ID = Companyid;
                mSkuHierarchy.PARENT_SKU_HIE_TYPE_ID = p_Parent_sku_hie_TypeId;
                mSkuHierarchy.IS_MANUALDISCOUNT = false;
                mSkuHierarchy.ExecuteQuery();
                return "Record Updated";

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
        /// Deletes PromotionType For a User
        /// </summary>
        /// <param name="p_companyId">Company</param>
        /// <param name="p_UserId">User</param>
        public void DeleteAssignBrand(int p_companyId,int p_UserId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spDeleteBRAND_ASSIGNMENT mDeleteBrand = new spDeleteBRAND_ASSIGNMENT();
                mDeleteBrand.Connection = mConnection;
                mDeleteBrand.PRINCIPAL_ID = p_companyId;
                mDeleteBrand.USER_ID = p_UserId;
                mDeleteBrand.ExecuteQuery();
                
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                
            }
            finally
            {
            }
        }
        
        /// <summary>
        /// Inserts PromotionType Manual Or Auto For A User
        /// </summary>
        /// <param name="p_companyId">Company</param>
        /// <param name="p_UserId">User</param>
        public void InsertAssignBrand(int p_companyId,int p_UserId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertBRAND_ASSIGNMENT mInsertBrand = new spInsertBRAND_ASSIGNMENT();
                mInsertBrand.Connection = mConnection;
                mInsertBrand.PRINCIPAL_ID = p_companyId;
                mInsertBrand.USER_ID = p_UserId;
                mInsertBrand.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);

            }
            finally
            {
            }
        }

        /// <summary>
        /// Inserts Items in Item_deal_Assignment table TYPE_ID=1
        /// SELECT Unassign Items in Item_deal_Assignment table TYPE_ID=2
        /// </summary>
        /// <param name="p_skuId"></param>
        /// <param name="p_CategoryId"></param>
        /// <param name="p_distributorId"></param>
        public void InsertAssignItem(int p_ItemDealId, int p_distributorId,DateTime p_Document_date, int p_UserId, string pRemarks
            , DataTable Skus)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTransaction=null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                //spDELETE_DEAL_ASSIGNMENT spDEAL_ASSIGNMENT = new spDELETE_DEAL_ASSIGNMENT();
                //spDEAL_ASSIGNMENT.Connection = mConnection;
                //spDEAL_ASSIGNMENT.Transaction = mTransaction;
                //foreach (DataRow dr in Skus.Rows)
                //{
                //    if (dr["Status"].ToString() == "Delete")
                //    {
                //        spDEAL_ASSIGNMENT.ITEM_DEAL_ID = p_ItemDealId;
                //        spDEAL_ASSIGNMENT.DISTRIBUTOR_ID = int.Parse(dr["CATEGORY_ID"].ToString());
                //        spDEAL_ASSIGNMENT.TYPE_ID = 2;
                //        spDEAL_ASSIGNMENT.IS_Active = false;

                //        spDEAL_ASSIGNMENT.ExecuteQuery();
                //    }
                //}

                List<int> Category = new List<int>();
                int mid = 0;

                spITEM_DEAL_ASSIGNMENT spITEM_DEAL_ASSIGNMENT = new spITEM_DEAL_ASSIGNMENT();
                spITEM_DEAL_ASSIGNMENT.Connection = mConnection;
                spITEM_DEAL_ASSIGNMENT.Transaction = mTransaction;

                spITEM_DEAL_ASSIGNMENT_DETAIL mInsertDetail = new spITEM_DEAL_ASSIGNMENT_DETAIL();
                mInsertDetail.Connection = mConnection;
                mInsertDetail.Transaction = mTransaction;

                foreach (DataRow dr in Skus.Rows)
                {
                    if (!Category.Contains(int.Parse(dr["CATEGORY_ID"].ToString())))
                    {
                        if (dr["Status"].ToString() == "New")
                        {
                            Category.Add(int.Parse(dr["CATEGORY_ID"].ToString()));
                        }
                        spITEM_DEAL_ASSIGNMENT.CATEGORY_DEAL_ID = int.Parse(dr["CATEGORY_DEAL_ID"].ToString());
                        spITEM_DEAL_ASSIGNMENT.ITEM_DEAL_ID = int.Parse(dr["ITEM_DEAL_ID"].ToString());
                        spITEM_DEAL_ASSIGNMENT.CATEGORY_ID = int.Parse(dr["CATEGORY_ID"].ToString());
                        spITEM_DEAL_ASSIGNMENT.DISTRIBUTOR_ID = p_distributorId;
                        spITEM_DEAL_ASSIGNMENT.IS_ChoiceBased = bool.Parse(dr["IS_CatChoiceBased"].ToString());
                        spITEM_DEAL_ASSIGNMENT.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                        spITEM_DEAL_ASSIGNMENT.TYPE_ID = 1;
                        spITEM_DEAL_ASSIGNMENT.TIME_STAMP = p_Document_date;
                        spITEM_DEAL_ASSIGNMENT.USER_ID = p_UserId;
                        spITEM_DEAL_ASSIGNMENT.Status = dr["Status"].ToString();
                        mid = spITEM_DEAL_ASSIGNMENT.ExecuteQuery();

                    }

                    mInsertDetail.ITEM_DEAL_ID = int.Parse(dr["ITEM_DEAL_ID"].ToString());
                    mInsertDetail.intDealID = mid;
                    mInsertDetail.IS_ChoiceBased = bool.Parse(dr["IS_ChoiceBased"].ToString());
                    mInsertDetail.IS_Optional = bool.Parse(dr["IS_Optional"].ToString());
                    mInsertDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mInsertDetail.QUANTITY = decimal.Parse(dr["SKU_QUANTITY"].ToString());
                    mInsertDetail.TIME_STAMP = p_Document_date;
                    mInsertDetail.USER_ID = p_UserId;
                    mInsertDetail.TYPE_ID = 1;
                    mInsertDetail.IS_Active = bool.Parse(dr["IS_Active"].ToString());
                    mInsertDetail.Status = dr["Status"].ToString();


                    mInsertDetail.ExecuteQuery();
                }
                mTransaction.Commit();
            }
            catch (Exception exp)
            {
                mTransaction.Rollback();
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

        public void UpdateAssignItem(int p_ItemDealId, int p_distributorId, DateTime p_Document_date, int p_UserId, string pRemarks)
        {
            IDbConnection mConnection = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spDELETE_DEAL_ASSIGNMENT spDEAL_ASSIGNMENT = new spDELETE_DEAL_ASSIGNMENT();
                spDEAL_ASSIGNMENT.Connection = mConnection;


                spDEAL_ASSIGNMENT.ITEM_DEAL_ID = p_ItemDealId;
                spDEAL_ASSIGNMENT.DISTRIBUTOR_ID = p_distributorId;
                spDEAL_ASSIGNMENT.TYPE_ID = 1;

                spDEAL_ASSIGNMENT.ExecuteQuery();

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


        public void UpdateAssignItem(int p_CatDealId, int p_ItemDealId, int p_skuId, int p_CategoryId, int p_distributorId
            , bool p_IsDefault, int p_Quantity)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spITEM_DEAL_ASSIGNMENT mInsertBrand = new spITEM_DEAL_ASSIGNMENT();
                mInsertBrand.Connection = mConnection;

                mInsertBrand.CATEGORY_DEAL_ID = p_CatDealId;
                mInsertBrand.ITEM_DEAL_ID = p_ItemDealId;
                mInsertBrand.SKU_ID = p_skuId;
                mInsertBrand.CATEGORY_ID = p_CategoryId;
                mInsertBrand.DISTRIBUTOR_ID = p_distributorId;
                mInsertBrand.TYPE_ID = 5;
                mInsertBrand.IS_ChoiceBased = p_IsDefault;
                mInsertBrand.QUANTITY = p_Quantity;

                mInsertBrand.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public void DeleteAssignItem(int p_ItemDealId, int p_skuId, int p_CategoryId, int p_distributorId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spITEM_DEAL_ASSIGNMENT mInsertBrand = new spITEM_DEAL_ASSIGNMENT();
                mInsertBrand.Connection = mConnection;

                mInsertBrand.ITEM_DEAL_ID = p_ItemDealId;
                mInsertBrand.SKU_ID = p_skuId;
                mInsertBrand.CATEGORY_ID = p_CategoryId;
                mInsertBrand.DISTRIBUTOR_ID = p_distributorId;
                mInsertBrand.TYPE_ID = 2;
                mInsertBrand.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);

            }
            finally
            {
            }
        }

        #region Added By Hazrat Ali

        /// <summary>
        /// Inserts Principal
        /// </summary>
        /// <remarks>
        /// Returns "Record Inserted" On Success And Exception.Message On Failure
        /// </remarks>
        /// <param name="p_Sku_Type_Id">Type</param>
        /// <param name="p_Parent_Sku_Hie_Id">Hierarchy</param>
        /// <param name="p_Sku_Hie_Code">Code</param>
        /// <param name="p_Sku_Hie_Name">Name</param>
        /// <param name="p_Ip_Address">IP</param>
        /// <param name="p_Is_Active">IsActive</param>
        /// <param name="Companyid">Compnay</param>
        /// <param name="p_IsManualDiscount">PromotionType</param>
        /// <param name="p_Address">Address</param>
        /// <param name="p_NTN">NTN</param>
        /// <param name="p_STRN">STRN</param>
        /// <returns>"Record Inserted" On Success And Exception.Message On Failure</returns>
        public string InsertPrincipal(int p_Sku_Type_Id, int p_Parent_Sku_Hie_Id, string p_Sku_Hie_Code, string p_Sku_Hie_Name, string p_Ip_Address, bool p_Is_Active
            , int Companyid, bool p_IsManualDiscount, string p_Address, string P_ContactPerson, string p_EMAIL, string p_PHONE, string p_FAX)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertPRINCIPAL mPrincipal = new spInsertPRINCIPAL();
                mPrincipal.Connection = mConnection;

                mPrincipal.SKU_HIE_TYPE_ID = p_Sku_Type_Id;

                mPrincipal.PARENT_SKU_HIE_ID = p_Parent_Sku_Hie_Id;
                mPrincipal.SKU_HIE_CODE = p_Sku_Hie_Code;
                mPrincipal.SKU_HIE_NAME = p_Sku_Hie_Name;
                mPrincipal.TIME_STAMP = System.DateTime.Now; ;
                mPrincipal.LASTUPDATE_DATE = System.DateTime.Now; ;
                mPrincipal.IP_ADDRESS = p_Ip_Address;
                mPrincipal.IS_ACTIVE = p_Is_Active;
                mPrincipal.COMPANY_ID = Companyid;
                mPrincipal.IS_MANUALDISCOUNT = p_IsManualDiscount;
                mPrincipal.ADDRESS = p_Address;
                mPrincipal.CONTACT_PERSON = P_ContactPerson;
                mPrincipal.EMAIL = p_EMAIL;
                mPrincipal.FAX = p_FAX;
                mPrincipal.PHONE = p_PHONE;
               // mPrincipal.CONTACT_PERSON = P_ContactPerson;
                mPrincipal.ExecuteQuery();
                return "Record Inserted";

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
        /// Updates Principal
        /// </summary>
        /// <remarks>
        /// Returns "Record Updated" On Success And Exception.Message On Failure
        /// </remarks>
        /// <param name="p_Sku_Type_Id">Type</param>
        /// <param name="p_Sku_Hie_Id">Hierarchy</param>
        /// <param name="p_Parent_Sku_Hie_Id">Parent</param>
        /// <param name="p_Sku_Hie_Code">Code</param>
        /// <param name="p_Sku_Hie_Name">Name</param>
        /// <param name="p_Ip_Address">IP</param>
        /// <param name="p_Is_Active">IsActive</param>
        /// <param name="Companyid">Company</param>
        /// <param name="p_IsManualDiscount">PromotionType</param>
        /// <param name="p_Address">Address</param>
        /// <param name="p_NTN">NTN</param>
        /// <param name="p_STRN">STRN</param>
        /// <returns>"Record Updated" On Success And Exception.Message On Failure</returns>
        public string UpdatePrincipal(int p_Sku_Type_Id, int p_Sku_Hie_Id, int p_Parent_Sku_Hie_Id, string p_Sku_Hie_Code, string p_Sku_Hie_Name, string p_Ip_Address
            , bool p_Is_Active, int Companyid, bool p_IsManualDiscount, string p_Address, string P_ContactPerson, string p_EMAIL, string p_PHONE, string p_FAX)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdatePRINCIPAL mPRINCIPAL = new spUpdatePRINCIPAL();
                mPRINCIPAL.Connection = mConnection;

                mPRINCIPAL.SKU_HIE_TYPE_ID = p_Sku_Type_Id;
                mPRINCIPAL.SKU_HIE_ID = p_Sku_Hie_Id;
                mPRINCIPAL.PARENT_SKU_HIE_ID = p_Parent_Sku_Hie_Id;
                mPRINCIPAL.SKU_HIE_CODE = p_Sku_Hie_Code;
                mPRINCIPAL.SKU_HIE_NAME = p_Sku_Hie_Name;
                mPRINCIPAL.TIME_STAMP = System.DateTime.Now; ;
                mPRINCIPAL.LASTUPDATE_DATE = System.DateTime.Now; ;
                mPRINCIPAL.IP_ADDRESS = p_Ip_Address;
                mPRINCIPAL.IS_ACTIVE = p_Is_Active;
                mPRINCIPAL.COMPANY_ID = Companyid;
                mPRINCIPAL.IS_MANUALDISCOUNT = p_IsManualDiscount;
                mPRINCIPAL.ADDRESS = p_Address;
                mPRINCIPAL.CONTACT_PERSON = P_ContactPerson;
                mPRINCIPAL.EMAIL = p_EMAIL;
                mPRINCIPAL.FAX = p_FAX;
                mPRINCIPAL.PHONE = p_PHONE;

                mPRINCIPAL.ExecuteQuery();
                return "Record Updated";

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
        /// Updates PromotionType For User
        /// </summary>
        /// <remarks>
        /// Returns "Record Updated" On Success And Exception.Message On Failure
        /// </remarks>
        /// <param name="p_USER_ID">User</param>
        /// <param name="p_PRINCIPAL_ID">Principal</param>
        /// <param name="p_Is_ManualDiscount">PromotionType</param>
        /// <returns>"Record Updated" On Success And Exception.Message On Failure</returns>
        public string UpdateBrandAssignment(int p_USER_ID, int p_PRINCIPAL_ID, bool p_Is_ManualDiscount)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateBRAND_ASSIGNMENT mBrand = new spUpdateBRAND_ASSIGNMENT();
                mBrand.Connection = mConnection;
                mBrand.USER_ID = p_USER_ID;
                mBrand.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mBrand.Is_ManualDiscount = p_Is_ManualDiscount;
                mBrand.ExecuteQuery();
                return "Record Updated";

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
        
        #endregion

        #endregion

        #endregion
    }
}
