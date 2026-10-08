using System;
using System.Data;
using System.IO;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;
using CORNDatabaseLayer.Classes;
using CORNBusinessLayer.Classes;

namespace CORNBusinessLayer.Classes
{
    /// <summary>
    /// Class For SKU Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert SKU
    /// </item>
    /// <term>
    /// Update SKU
    /// </term>
    /// <item>
    /// Get SKU
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
	public class SkuController
    {
        #region Constructors

        /// <summary>
        /// Constructor For SkuController
        /// </summary>
        public SkuController()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        #region public Methods

        #region Select
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_UOM_ID"></param>
        /// <param name="p_UOM_TYPE_ID"></param>
        /// <returns></returns>
        public DataTable SelectUOM(int p_UOM_ID, int p_UOM_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectUOM mspSelectSkuInfo = new spSelectUOM();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.UOM_ID = p_UOM_ID;
                mspSelectSkuInfo.UOM_TYPE_ID = p_UOM_TYPE_ID;

                DataTable dt = mspSelectSkuInfo.ExecuteTable();
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
        /// Gets SKUS Data
        /// </summary>
        /// <remarks>
        /// Returns SKUS Data as Datatable
        /// </remarks>
        /// <param name="p_company_id">Principal</param>
        /// <param name="p_division_id">Dicision</param>
        /// <param name="p_category_id">Category</param>
        /// <param name="p_brand_id">Brand</param>
        /// <param name="Companyid">Company</param>
        /// <returns>SKUS Data as Datatable</returns>
        public DataTable SelectSkuInfo(int p_company_id, int p_division_id, int p_category_id, int p_brand_id, int Companyid,string SectionID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSelectSkuInfo mspSelectSkuInfo = new uspSelectSkuInfo();
                mspSelectSkuInfo.Connection = mConnection;

                mspSelectSkuInfo.brand_id = p_brand_id;
                mspSelectSkuInfo.category_id = p_category_id;
                mspSelectSkuInfo.Principal_id = p_company_id;
                mspSelectSkuInfo.division_id = p_division_id;
                mspSelectSkuInfo.Company_id = Companyid;
                mspSelectSkuInfo.SECTION_ID = SectionID;
                DataTable dt = mspSelectSkuInfo.ExecuteTable();

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
        public DataTable SelectModifier(int p_Type_id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSKU_Modifier mSkuModifier = new spSelectSKU_Modifier();
                mSkuModifier.Connection = mConnection;

                mSkuModifier.Typeid =p_Type_id;

                DataTable dt = mSkuModifier.ExecuteTable();

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

        public DataSet SelectModifierRpt(int p_Type_id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSKU_ModifierReport mSkuModifier = new spSelectSKU_ModifierReport();
                CORNBusinessLayer.Reports.DsReport ds = new CORNBusinessLayer.Reports.DsReport();
                mSkuModifier.Connection = mConnection;

                mSkuModifier.SKU_ID = p_Type_id;

                DataTable dt = mSkuModifier.ExecuteTable();

                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["spSelectSKU_ModifierReport"].ImportRow(dr);
                }

                return ds;

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

        public DataSet SelectSKUConsumption(int p_DISTRIBUTOR_ID, int p_SKU_id, int pReportType, int p_Type_id, DateTime p_FromDate, DateTime p_ToDate)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                Reports.DsReport ds = new Reports.DsReport();
                spSelectSKU_Consumption mSkuModifier = new spSelectSKU_Consumption();

                mSkuModifier.Connection = mConnection;
                mSkuModifier.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mSkuModifier.SKU_ID = p_SKU_id;
                mSkuModifier.Type_ID = p_Type_id;
                mSkuModifier.REPORT_TYPE = pReportType;
                mSkuModifier.FROM_DATE = p_FromDate;
                mSkuModifier.TO_DATE = p_ToDate;

                DataTable dt = mSkuModifier.ExecuteTable();

                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["spSelectSKU_Consumption"].ImportRow(dr);
                }

                return ds;
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

        public DataSet SelectDiscountedInvoices(int p_distributer_id, int p_Cashier_id, DateTime p_FromDate, DateTime p_ToDate)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SpSelectDiscountedSaleInvoices mDiscountSale = new SpSelectDiscountedSaleInvoices();

                mDiscountSale.Connection = mConnection;

                mDiscountSale.DISTRIBUTOR_ID = p_distributer_id;
                mDiscountSale.CASHIER_ID = p_Cashier_id;
                mDiscountSale.FROMDATE = p_FromDate;
                mDiscountSale.TODATE = p_ToDate;

                DataTable dt = mDiscountSale.ExecuteTable();

                    CORNBusinessLayer.Reports.DSReportNew ds = new CORNBusinessLayer.Reports.DSReportNew();
                    foreach (DataRow dr in dt.Rows)
                    {
                        ds.Tables["SpSelectDiscountedSaleInvoices"].ImportRow(dr);
                    }
                    return ds;

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

        public DataSet SelectCustomerWiseSales(int p_Rpt_Type, int p_distributer_id, int p_Customer_id, int p_SortBy, DateTime p_FromDate, DateTime p_ToDate)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SpSelectCustomerWiseSaleReport mSkuCustomer = new SpSelectCustomerWiseSaleReport();

                mSkuCustomer.Connection = mConnection;

                mSkuCustomer.Rpt_Type = p_Rpt_Type;
                mSkuCustomer.DISTRIBUTOR_ID = p_distributer_id;
                mSkuCustomer.CUSTOMER_ID = p_Customer_id;
                mSkuCustomer.SORT_BY = p_SortBy;
                mSkuCustomer.FROMDATE = p_FromDate;
                mSkuCustomer.TODATE = p_ToDate;

                DataTable dt = mSkuCustomer.ExecuteTable();

                CORNBusinessLayer.Reports.DSReportNew ds = new CORNBusinessLayer.Reports.DSReportNew();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["SpSelectCustomerWiseSaleReport"].ImportRow(dr);
                }
                return ds;

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

        public DataSet SelectServiceWiseSales(int p_Rpt_Type, int p_distributer_id, int p_Service_Type_id, int p_SortBy, DateTime p_FromDate, DateTime p_ToDate)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SpSelectServiceWiseSaleReport mSkuCustomer = new SpSelectServiceWiseSaleReport();

                mSkuCustomer.Connection = mConnection;

                mSkuCustomer.Rpt_Type = p_Rpt_Type;
                mSkuCustomer.DISTRIBUTOR_ID = p_distributer_id;
                mSkuCustomer.SERVICE_ID = p_Service_Type_id;
                mSkuCustomer.SORT_BY = p_SortBy;
                mSkuCustomer.FROMDATE = p_FromDate;
                mSkuCustomer.TODATE = p_ToDate;

                DataTable dt = mSkuCustomer.ExecuteTable();

                Reports.DsReport2 ds = new Reports.DsReport2();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["SpSelectServiceWiseSaleReport"].ImportRow(dr);
                }
                return ds;

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

        public DataSet SelectServiceWiseSales(int p_Rpt_Type, int p_distributer_id, int p_Service_Type_id, int p_SortBy, DateTime p_FromDate, DateTime p_ToDate,string p_INVOICE_NO)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SpSelectServiceWiseSaleReport mSkuCustomer = new SpSelectServiceWiseSaleReport();

                mSkuCustomer.Connection = mConnection;

                mSkuCustomer.Rpt_Type = p_Rpt_Type;
                mSkuCustomer.DISTRIBUTOR_ID = p_distributer_id;
                mSkuCustomer.SERVICE_ID = p_Service_Type_id;
                mSkuCustomer.SORT_BY = p_SortBy;
                mSkuCustomer.FROMDATE = p_FromDate;
                mSkuCustomer.TODATE = p_ToDate;
                mSkuCustomer.INVOICE_NO = p_INVOICE_NO;
                DataTable dt = mSkuCustomer.ExecuteTable();

                Reports.DsReport2 ds = new Reports.DsReport2();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["SpSelectServiceWiseSaleReport"].ImportRow(dr);
                }
                return ds;

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
        public DataSet SelectMonthalyAttendance(int p_distributer_id, int p_dept_id, int p_Emp_id, DateTime p_FromDate)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                MONTHALY_ATTENDANCE mattendance = new MONTHALY_ATTENDANCE();

                mattendance.Connection = mConnection;

                mattendance.DISTRIBUTOR_ID = p_distributer_id;
                mattendance.DEPARTMENT_ID = p_dept_id;
                mattendance.Emp_ID = p_Emp_id;
                mattendance.DATE = p_FromDate;

                DataTable dt = mattendance.ExecuteTable();

                Reports.DsReport2 ds = new Reports.DsReport2();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["MONTHALY_ATTENDANCE"].ImportRow(dr);
                }
                return ds;

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

        public string InsertModifier(int p_Sku_Id, DataTable p_dt_Modifier)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSKU_Modifier mSkuModifier = new spInsertSKU_Modifier();
                mSkuModifier.Connection = mConnection;

                DataTable dt = p_dt_Modifier;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    mSkuModifier.SKU_ID = p_Sku_Id;
                    mSkuModifier.ModifierSKU_ID =Convert.ToInt32( dt.Rows[i]["IsModifierID"]);
                    mSkuModifier.intStockMUnitCode = Convert.ToInt32(dt.Rows[i]["StockUnitID"]); 
                    mSkuModifier.Default_Qty = Convert.ToDecimal(dt.Rows[i]["Default_Qty"]); 
                    mSkuModifier.IS_Manadatory = Convert.ToBoolean( dt.Rows[i]["IS_Manadatory"]);
                    mSkuModifier.ExecuteQuery();
                }
      
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

        public string UpdateModifier(int p_Sku_ModifierCode,int p_Sku_Id,int p_ModifierSKU_ID,int p_intStockMUnitCode,decimal p_Default_Qty, bool p_IS_Manadatory)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateSKU_Modifier mSkuModifier = new spUpdateSKU_Modifier();
                mSkuModifier.Connection = mConnection;

                    mSkuModifier.lngSKUModifierCode = p_Sku_ModifierCode;
                    mSkuModifier.SKU_ID = p_Sku_Id;
                    mSkuModifier.ModifierSKU_ID = p_ModifierSKU_ID;
                    mSkuModifier.intStockMUnitCode = p_intStockMUnitCode;
                    mSkuModifier.Default_Qty = p_Default_Qty;
                    mSkuModifier.IS_Manadatory = p_IS_Manadatory;
                    mSkuModifier.ExecuteQuery();

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

        public DataTable SelectSkuConsumption(int p_Typeid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectHasModifier mspSelectSkuInfo = new spSelectHasModifier();
                mspSelectSkuInfo.Connection = mConnection;

                mspSelectSkuInfo.Type_id = p_Typeid;

                DataTable dt = mspSelectSkuInfo.ExecuteTable();

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


        public DataTable SelectSkuHasModifier(int p_Typeid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectHasModifier mspSelectSkuInfo = new spSelectHasModifier();
                mspSelectSkuInfo.Connection = mConnection;

                mspSelectSkuInfo.Type_id = p_Typeid;

                DataTable dt = mspSelectSkuInfo.ExecuteTable();

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
        public DataTable SelectSkuInfo(int p_company_id, int p_division_id
            , int p_category_id, int p_brand_id, int Companyid, int p_Type_id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSelectSkuInfo mspSelectSkuInfo = new uspSelectSkuInfo();
                mspSelectSkuInfo.Connection = mConnection;

                mspSelectSkuInfo.brand_id = p_brand_id;
                mspSelectSkuInfo.category_id = p_category_id;
                mspSelectSkuInfo.Principal_id = p_company_id;
                mspSelectSkuInfo.division_id = p_division_id;
                mspSelectSkuInfo.Company_id = Companyid;
                mspSelectSkuInfo.Type_id = p_Type_id;

                DataTable dt = mspSelectSkuInfo.ExecuteTable();

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
        public DataTable SelectSkusforOrder(int pCompanyId, int pCategoryId, int pBrandId, int pSkuId, int pDistributorId, int pTypeId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSkusforOrder mspSelectSkuInfo = new spSelectSkusforOrder();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.SKU_ID = pSkuId;
                mspSelectSkuInfo.Category_Id = pCategoryId;
                mspSelectSkuInfo.DISTRIBUTOR_ID = pDistributorId;
                mspSelectSkuInfo.BRAND_ID = pBrandId;
                mspSelectSkuInfo.company_id = pCompanyId;
                mspSelectSkuInfo.TYPE_ID = pTypeId;

                DataTable dt = mspSelectSkuInfo.ExecuteTable();

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
        public DataTable SpGetPendingBill(long pSaleInvoiceId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetPendingBill mspSelectSkuInfo = new spGetPendingBill();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.SALE_INVOICE_ID = pSaleInvoiceId;
              
                DataTable dt = mspSelectSkuInfo.ExecuteTable();

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
        public DataTable SpGetPendingBill(long pSaleInvoiceId, int pTypeId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetPendingBill mspSelectSkuInfo = new spGetPendingBill();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.SALE_INVOICE_ID = pSaleInvoiceId;
                mspSelectSkuInfo.TYPE_ID = pTypeId;

                DataTable dt = mspSelectSkuInfo.ExecuteTable();

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
        public DataTable SelectSkuInfo2(int p_company_id, int p_division_id, int p_category_id, int p_brand_id, int Companyid, int p_TAGID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSelectSkuInfo2 mspSelectSkuInfo = new uspSelectSkuInfo2();
                mspSelectSkuInfo.brand_id = p_brand_id;
                mspSelectSkuInfo.category_id = p_category_id;
                mspSelectSkuInfo.Principal_id = p_company_id;
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.division_id = p_division_id;
                mspSelectSkuInfo.Company_id = Companyid;
                mspSelectSkuInfo.TAG_ID = p_TAGID;

                DataTable dt = mspSelectSkuInfo.ExecuteTable();

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
        /// Gets SKU Data
        /// </summary>
        /// <remarks>
        /// Returns SKUS Data as Datatable
        /// </remarks>
        /// <param name="p_SKU_Id">SKU</param>
        /// <param name="Companyid">Company</param>
        /// <returns>SKUS Data as Datatable</returns>
        public DataTable SelectSkuData(int p_SKU_Id, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSKUS mSkuInfo = new spSelectSKUS();
                mSkuInfo.Connection = mConnection;
                mSkuInfo.SKU_ID = p_SKU_Id;
                mSkuInfo.COMPANY_ID = Companyid;
                DataTable dt = mSkuInfo.ExecuteTable();
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
        /// Gets SKU UOM
        /// </summary>
        /// <param name="p_UOM_Id">UOM</param>
        /// <param name="p_UOM_Desc">Description</param>
        /// <returns>SKU UOM</returns>
        public DataTable SelectUOMs(int p_UOM_Id, string p_UOM_Desc)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectUOMS mUOMs = new spSelectUOMS();
                mUOMs.Connection = mConnection;

                mUOMs.UOM_ID = p_UOM_Id;
                mUOMs.UOM_DESC = p_UOM_Desc;
                mUOMs.TIME_STAMP = Constants.DateNullValue;
                mUOMs.STATUS = Constants.IntNullValue;

                DataTable dt = mUOMs.ExecuteTable();
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

        public DataTable SelectSkuCountry()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectCountrySku mUOMs = new spSelectCountrySku();
                mUOMs.Connection = mConnection;
                DataTable dt = mUOMs.ExecuteTable();
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
        public DataTable SearchProduct(string pSearchText)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSearchProduct mspSelectSkuInfo = new uspSearchProduct();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.SEARCH_TEXT = pSearchText;
                DataTable dt = mspSelectSkuInfo.ExecuteTable();

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
        #region Insert, Update

        /// <summary>
        /// Inserts Or Updates SKU Price From Excel File
        /// </summary>
        /// Returns True On Success And False On Failure
        /// <param name="p_DistributorId">Location</param>
        /// <param name="pFileName">ExcelFile</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool ImportSKUS(int p_DistributorId, string pFileName, int p_Principal_Id, int p_Company_ID, int p_UserId)
        {
            IDbConnection mConnection = null;
            FileStream Sourcefile = null;
            StreamReader ReadSourceFile = null;
            IDbTransaction mTransaction = null;
            DataControl DC = new DataControl();

            mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
            mConnection.Open();
            mTransaction = ProviderFactory.GetTransaction(mConnection);

            Sourcefile = new FileStream(pFileName, FileMode.Open);
            ReadSourceFile = new StreamReader(Sourcefile);
            string FileContents = "";
            try
            {
                while ((FileContents = ReadSourceFile.ReadLine()) != null)
                {

                    string[] ParametersArr = FileContents.Split(Constants.File_Delimiter);
                    uspImportSKUS mSKUS = new uspImportSKUS();
                    mSKUS.Connection = mConnection;
                    mSKUS.Transaction = mTransaction;
                    mSKUS.PRINCIPAL_ID = p_Principal_Id;
                    mSKUS.ISEXEMPTED = true;
                    mSKUS.ISACTIVE = true;
                    mSKUS.GST_ON = 'T';
                    mSKUS.COMPANY_ID = p_Company_ID;
                    mSKUS.SKU_CODE = ParametersArr[0].ToString();
                    mSKUS.SKU_NAME = ParametersArr[1].ToString();
                    mSKUS.BAR_CODE = ParametersArr[2].ToString();
                    mSKUS.DIVISION_ID = Convert.ToInt32(DC.chkNull_0(ParametersArr[3]));
                    mSKUS.CATEGORY_ID = Convert.ToInt32(DC.chkNull_0(ParametersArr[4]));
                    mSKUS.BRAND_ID = Convert.ToInt32(DC.chkNull_0(ParametersArr[5]));
                    mSKUS.SKU_TAG_ID = int.Parse(DC.chkNull_0(ParametersArr[6].ToString()));
                    mSKUS.COLOR = ParametersArr[7].ToString();
                    mSKUS.PACKSIZE = ParametersArr[8].ToString();
                    mSKUS.SKU_SEASON = ParametersArr[9].ToString();
                    mSKUS.SKU_COUNTRY = ParametersArr[10].ToString();
                    mSKUS.GST_RATE_REG = 0;
                    mSKUS.GST_RATE_UNREG = 0;
                    mSKUS.TIME_STAMP = System.DateTime.Now;
                    mSKUS.LASTUPDATE_DATE = System.DateTime.Now;
                    mSKUS.IP_ADDRESS = null;
                    mSKUS.USER_ID = p_UserId;

                    mSKUS.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;


            }

            catch (Exception excp)
            {
                mTransaction.Rollback();
                ReadSourceFile.Close();
                mConnection.Close();
                //ExceptionPublisher.PublishException(excp);
                // throw;
                return false;

            }
            finally
            {
                ReadSourceFile.Close();
                mConnection.Close();

            }
        }

        /// <summary>
        /// Insert SKU
        /// </summary>
        /// <remarks>
        /// Returns Inserted SKU ID as String
        /// </remarks>
        /// <param name="p_IsExempted">IsExempted</param>
        /// <param name="p_IsActive">IsActive</param>
        /// <param name="p_Gst_On">GSTOn</param>
        /// <param name="p_Company_Id">Principal</param>
        /// <param name="p_Division_Id">Division</param>
        /// <param name="p_Category_Id">Category</param>
        /// <param name="p_Brand_Id">Brand</param>
        /// <param name="p_Variant_Id">Variant</param>
        /// <param name="p_GST_Rate_Reg">GSTReg</param>
        /// <param name="p_GST_Rate_Unreg">GSTUnReg</param>
        /// <param name="p_Units_In_Case">Units</param>
        /// <param name="p_Sku_Code">Code</param>
        /// <param name="p_Sku_Name">Name</param>
        /// <param name="p_Ip_Address">Address</param>
        /// <param name="p_packSize">Packing</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="Companyid">Company</param>
        /// <returns>Inserted SKU ID as String</returns>
        public string InsertSKUS2(bool p_IsExempted, bool p_IsActive, char p_Gst_On, int p_Company_Id, int p_Division_Id, int p_Category_Id, int p_Brand_Id, int p_Variant_Id, decimal p_GST_Rate_Reg, decimal p_GST_Rate_Unreg, string p_Units_In_Case, string p_Sku_Code, string p_Sku_Name,
            string p_Ip_Address, string p_packSize, int p_UserId, int Companyid, string p_BarCode, string p_color, int p_skuTagId, string p_skuCountry, string p_skuSeason)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSKUS2 mSkus = new spInsertSKUS2();

                mSkus.Connection = mConnection;
                mSkus.PRINCIPAL_ID = p_Company_Id;
                mSkus.ISEXEMPTED = p_IsExempted;
                mSkus.ISACTIVE = p_IsActive;
                mSkus.GST_ON = p_Gst_On;
                mSkus.COMPANY_ID = Companyid;
                mSkus.DIVISION_ID = p_Division_Id;
                mSkus.BRAND_ID = p_Brand_Id;
                mSkus.CATEGORY_ID = p_Category_Id;
                mSkus.COLOR = p_Units_In_Case;
                mSkus.BAR_CODE = p_BarCode;
                mSkus.COLOR = p_color;
                mSkus.SKU_TAG_ID = p_skuTagId;
                mSkus.SKU_SEASON = p_skuSeason;
                mSkus.SKU_COUNTRY = p_skuCountry;

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
                //mSkus.UNITS_IN_CASE = p_Units_In_Case;
                mSkus.SKU_NAME = p_Sku_Name;
                mSkus.SKU_CODE = p_Sku_Code;
                mSkus.TIME_STAMP = System.DateTime.Now;
                mSkus.LASTUPDATE_DATE = System.DateTime.Now;
                mSkus.IP_ADDRESS = p_Ip_Address;
                mSkus.PACKSIZE = p_packSize;
                mSkus.USER_ID = p_UserId;

                mSkus.ExecuteQuery();

                return mSkus.SKU_ID.ToString();

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


        public string InsertSKUS(bool p_IsDesc, bool p_IsExempted, bool p_IsActive, char p_Gst_On, int p_Company_Id, int p_Division_Id, int p_Category_Id, int p_Brand_Id
            ,int p_Variant_Id, decimal p_GST_Rate_Reg, decimal p_GST_Rate_Unreg, short p_Units_In_Case, string p_Sku_Code, string p_Sku_Name, string p_Ip_Address
            ,string p_packSize, int p_UserId, int Companyid, string p_DESCRIPTION, int pSectionId, bool p_IsDeal, bool p_IsModifier
            ,decimal minlevel,decimal reorderlevel, bool p_IsRecipe, bool p_IS_HasMODIFIER,int p_intSaleMUnitCode,int p_intPurchaseMUnitCode,int p_intStockMUnitCode
            ,float p_fltFEDPercentage,float p_fltWHTPercentage,float p_fltAgeInDays,bool p_IsMarketItem
            ,bool p_IsReplaceable,bool p_IsFEDItem,bool p_IsWHTItem,bool p_IsSerialized
            ,bool p_IsHazardous,bool p_IsBatchItem,bool p_IsOverSaleAllowed,bool p_IsExpiryAllowed,bool p_IsWarehouseItem
            ,decimal p_MAX_LEVEL,decimal p_Sale_to_PurchaseFactor,decimal p_Purchase_to_SaleFactor,decimal p_Sale_to_StockFactor
            ,decimal p_Purchase_to_StockFactor,decimal p_Default_Qty,decimal p_Stock_to_SaleFactor,decimal p_Stock_to_PurchaseFactor
            ,string p_Sale_to_PurchaseOperator,string p_Purchase_to_SaleOperator,string p_Sale_to_StockOperator
            ,string p_Purchase_to_StockOperator,string p_Stock_to_SaleOperator
            ,string p_Stock_to_PurchaseOperator,string p_strDescription,string p_strSerialCode,string p_strStatus,string p_strERPCode
            ,float p_ShelfAgeInDays, int p_MUnitLifeCode,decimal p_Ratio_Factor,decimal p_Weight_Consumption,bool p_Is_WastageItem)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSKUS mSkus = new spInsertSKUS();

                mSkus.Connection = mConnection;
                mSkus.PRINCIPAL_ID = p_Company_Id;
                mSkus.ISEXEMPTED = p_IsExempted;
                mSkus.IS_DESC = p_IsDesc;
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
                mSkus.IS_DEAL = p_IsDeal;
                mSkus.IS_MODIFIER = p_IsModifier;
                mSkus.IS_Recipe = p_IsRecipe;
                mSkus.MIN_LEVEL = minlevel;
                mSkus.REORDER_LEVEL = reorderlevel;
                mSkus.IS_HasMODIFIER = p_IS_HasMODIFIER;
                mSkus.intSaleMUnitCode = p_intSaleMUnitCode;
                mSkus.intPurchaseMUnitCode = p_intPurchaseMUnitCode;
                mSkus.intStockMUnitCode = p_intStockMUnitCode;
                mSkus.fltFEDPercentage = p_fltFEDPercentage;
                mSkus.fltWHTPercentage = p_fltWHTPercentage;
                mSkus.fltAgeInDays = p_fltAgeInDays;
                mSkus.IsMarketItem = p_IsMarketItem;
                mSkus.IsReplaceable = p_IsReplaceable;
                mSkus.IsFEDItem = p_IsFEDItem;
                mSkus.IsWHTItem = p_IsWHTItem;
                mSkus.IsSerialized = p_IsSerialized;
                mSkus.IsHazardous = p_IsHazardous;
                mSkus.IsBatchItem = p_IsBatchItem;
                mSkus.IsOverSaleAllowed = p_IsOverSaleAllowed;
                mSkus.IsExpiryAllowed = p_IsExpiryAllowed;
                mSkus.IsWarehouseItem = p_IsWarehouseItem;
                mSkus.MAX_LEVEL = p_MAX_LEVEL;
                mSkus.Sale_to_PurchaseFactor = p_Sale_to_PurchaseFactor;
                mSkus.Purchase_to_SaleFactor = p_Purchase_to_SaleFactor;
                mSkus.Sale_to_StockFactor = p_Sale_to_StockFactor;
                mSkus.Purchase_to_StockFactor = p_Purchase_to_StockFactor;
                mSkus.Default_Qty = p_Default_Qty;
                mSkus.Stock_to_SaleFactor = p_Stock_to_SaleFactor;
                mSkus.Stock_to_PurchaseFactor = p_Stock_to_PurchaseFactor;
                mSkus.Sale_to_PurchaseOperator = p_Sale_to_PurchaseOperator;
                mSkus.Purchase_to_SaleOperator = p_Purchase_to_SaleOperator;
                mSkus.Sale_to_StockOperator = p_Sale_to_StockOperator;
                mSkus.Purchase_to_StockOperator = p_Purchase_to_StockOperator;
                mSkus.Stock_to_SaleOperator = p_Stock_to_SaleOperator;
                mSkus.Stock_to_PurchaseOperator = p_Stock_to_PurchaseOperator;
                mSkus.strDescription = p_strDescription;
                mSkus.strSerialCode = p_strSerialCode;
                mSkus.strStatus = p_strStatus;
                mSkus.strERPCode = p_strERPCode;
                mSkus.fltShelfAgeInDays = p_ShelfAgeInDays;
                mSkus.intMUnitLifeCode = p_MUnitLifeCode;
                mSkus.Ratio_Factor = p_Ratio_Factor;
                mSkus.Weight_Consumption = p_Weight_Consumption;
                mSkus.Is_WastageItem = p_Is_WastageItem;
                mSkus.ExecuteQuery();

                return mSkus.SKU_ID.ToString();

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
        /// Updates SKU
        /// </summary>
        /// <remarks>
        /// Returns "Record Updated" On Success And Null On Failure
        /// </remarks>
        /// <param name="p_IsExempted">IsExempted</param>
        /// <param name="p_IsActive">IsActive</param>
        /// <param name="p_Gst_On">GSTOn</param>
        /// <param name="p_Company_Id">Principal</param>
        /// <param name="p_Division_Id">Division</param>
        /// <param name="p_Category_Id">Category</param>
        /// <param name="p_Brand_Id">Brand</param>
        /// <param name="p_Variant_Id">Variant</param>
        /// <param name="p_GST_Rate_Reg">GSTReg</param>
        /// <param name="p_GST_Rate_Unreg">GSTUnReg</param>
        /// <param name="p_Units_In_Case">Units</param>
        /// <param name="p_Sku_Id">SKU</param>
        /// <param name="p_Sku_Code">Code</param>
        /// <param name="p_Sku_Name">Name</param>
        /// <param name="p_Ip_Address">Address</param>
        /// <param name="p_packSize">Packing</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="CompanyId">Company</param>
        /// <returns>"Record Updated" On Success And Null On Failure</returns>
        public string UpdateSKUS(bool p_IsDesc, bool p_IsExempted, bool p_IsActive, char p_Gst_On, int p_Company_Id, int p_Division_Id, int p_Category_Id
            , int p_Brand_Id, int p_Variant_Id, decimal p_GST_Rate_Reg, decimal p_GST_Rate_Unreg, short p_Units_In_Case, int p_Sku_Id, string p_Sku_Code
            , string p_Sku_Name, string p_Ip_Address, string p_packSize, int p_UserId, int CompanyId, string p_DESCRIPTION, int pSectionId, bool p_IsDeal, bool p_IsModifier
            , decimal minlevel, decimal reorderlevel, bool p_IsRecipe, bool p_IS_HasMODIFIER, int p_intSaleMUnitCode, int p_intPurchaseMUnitCode, int p_intStockMUnitCode,
         float p_fltFEDPercentage, float p_fltWHTPercentage, float p_fltAgeInDays, bool p_IsMarketItem,
         bool p_IsReplaceable, bool p_IsFEDItem, bool p_IsWHTItem, bool p_IsSerialized,
         bool p_IsHazardous, bool p_IsBatchItem, bool p_IsOverSaleAllowed, bool p_IsExpiryAllowed, bool p_IsWarehouseItem,
         decimal p_MAX_LEVEL, decimal p_Sale_to_PurchaseFactor, decimal p_Purchase_to_SaleFactor, decimal p_Sale_to_StockFactor,
         decimal p_Purchase_to_StockFactor, decimal p_Default_Qty, decimal p_Stock_to_SaleFactor, decimal p_Stock_to_PurchaseFactor,
         string p_Sale_to_PurchaseOperator, string p_Purchase_to_SaleOperator, string p_Sale_to_StockOperator,
         string p_Purchase_to_StockOperator, string p_Stock_to_SaleOperator,
         string p_Stock_to_PurchaseOperator, string p_strDescription, string p_strSerialCode, string p_strStatus, string p_strERPCode
         , float p_ShelfAgeInDays, int p_MUnitLifeCode,decimal p_Ratio_Factor,decimal p_Weight_Consumption,bool p_Is_WastageItem)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateSKUS mSkus = new spUpdateSKUS();

                mSkus.Connection = mConnection;
                mSkus.ISEXEMPTED = p_IsExempted;
                mSkus.IS_DESC = p_IsDesc;
                mSkus.ISACTIVE = p_IsActive;
                mSkus.GST_ON = p_Gst_On;
                mSkus.COMPANY_ID = CompanyId;
                mSkus.PRINCIPAL_ID = p_Company_Id;
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
                mSkus.SKU_ID = p_Sku_Id;
                mSkus.SKU_NAME = p_Sku_Name;
                mSkus.SKU_CODE = p_Sku_Code;
                mSkus.TIME_STAMP = DateTime.Now;
                mSkus.LASTUPDATE_DATE = DateTime.Now;
                mSkus.IP_ADDRESS = p_Ip_Address;
                mSkus.PACKSIZE = p_packSize;
                mSkus.USER_ID = p_UserId;
                mSkus.DESCRIPTION = p_DESCRIPTION;
                mSkus.SECTION_ID = pSectionId;
                mSkus.IS_DEAL = p_IsDeal;
                mSkus.IS_MODIFIER = p_IsModifier;
                mSkus.IS_Recipe = p_IsRecipe;
                mSkus.MIN_LEVEL = minlevel;
                mSkus.REORDER_LEVEL = reorderlevel;
                mSkus.IS_HasMODIFIER = p_IS_HasMODIFIER;
                mSkus.intSaleMUnitCode = p_intSaleMUnitCode;
                mSkus.intPurchaseMUnitCode = p_intPurchaseMUnitCode;
                mSkus.intStockMUnitCode = p_intStockMUnitCode;
                mSkus.fltFEDPercentage = p_fltFEDPercentage;
                mSkus.fltWHTPercentage = p_fltWHTPercentage;
                mSkus.fltAgeInDays = p_fltAgeInDays;
                mSkus.IsMarketItem = p_IsMarketItem;
                mSkus.IsReplaceable = p_IsReplaceable;
                mSkus.IsFEDItem = p_IsFEDItem;
                mSkus.IsWHTItem = p_IsWHTItem;
                mSkus.IsSerialized = p_IsSerialized;
                mSkus.IsHazardous = p_IsHazardous;
                mSkus.IsBatchItem = p_IsBatchItem;
                mSkus.IsOverSaleAllowed = p_IsOverSaleAllowed;
                mSkus.IsExpiryAllowed = p_IsExpiryAllowed;
                mSkus.IsWarehouseItem = p_IsWarehouseItem;
                mSkus.MAX_LEVEL = p_MAX_LEVEL;
                mSkus.Sale_to_PurchaseFactor = p_Sale_to_PurchaseFactor;
                mSkus.Purchase_to_SaleFactor = p_Purchase_to_SaleFactor;
                mSkus.Sale_to_StockFactor = p_Sale_to_StockFactor;
                mSkus.Purchase_to_StockFactor = p_Purchase_to_StockFactor;
                mSkus.Default_Qty = p_Default_Qty;
                mSkus.Stock_to_SaleFactor = p_Stock_to_SaleFactor;
                mSkus.Stock_to_PurchaseFactor = p_Stock_to_PurchaseFactor;
                mSkus.Sale_to_PurchaseOperator = p_Sale_to_PurchaseOperator;
                mSkus.Purchase_to_SaleOperator = p_Purchase_to_SaleOperator;
                mSkus.Sale_to_StockOperator = p_Sale_to_StockOperator;
                mSkus.Purchase_to_StockOperator = p_Purchase_to_StockOperator;
                mSkus.Stock_to_SaleOperator = p_Stock_to_SaleOperator;
                mSkus.Stock_to_PurchaseOperator = p_Stock_to_PurchaseOperator;
                mSkus.strDescription = p_strDescription;
                mSkus.strSerialCode = p_strSerialCode;
                mSkus.strStatus = p_strStatus;
                mSkus.strERPCode = p_strERPCode;
                mSkus.fltShelfAgeInDays = p_ShelfAgeInDays;
                mSkus.intMUnitLifeCode = p_MUnitLifeCode;
                mSkus.Ratio_Factor = p_Ratio_Factor;
                mSkus.Weight_Consumption = p_Weight_Consumption;
                mSkus.Is_WastageItem = p_Is_WastageItem;
                mSkus.ExecuteQuery();
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


        public string UpdateSKUS2(bool p_IsExempted, bool p_IsActive, char p_Gst_On, int p_Company_Id, int p_Division_Id,
            int p_Category_Id, int p_Brand_Id, int p_Variant_Id, decimal p_GST_Rate_Reg, decimal p_GST_Rate_Unreg, string p_Units_In_Case,
            int p_Sku_Id, string p_Sku_Code, string p_Sku_Name, string p_Ip_Address, string p_packSize, int p_UserId, int CompanyId, string p_BarCode, string p_color, int p_skuTagId, string p_skuCountry, string p_skuSeason)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateSKUS2 mSkus = new spUpdateSKUS2();

                mSkus.Connection = mConnection;
                mSkus.ISEXEMPTED = p_IsExempted;
                mSkus.ISACTIVE = p_IsActive;
                mSkus.GST_ON = p_Gst_On;
                mSkus.COMPANY_ID = CompanyId;
                mSkus.PRINCIPAL_ID = p_Company_Id;
                mSkus.DIVISION_ID = p_Division_Id;
                mSkus.BRAND_ID = p_Brand_Id;
                mSkus.CATEGORY_ID = p_Category_Id;
                mSkus.BAR_CODE = p_BarCode;
                mSkus.COLOR = p_color;
                mSkus.SKU_TAG_ID = p_skuTagId;
                mSkus.SKU_SEASON = p_skuSeason;
                mSkus.SKU_COUNTRY = p_skuCountry;

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
                mSkus.SKU_ID = p_Sku_Id;
                mSkus.SKU_NAME = p_Sku_Name;
                mSkus.SKU_CODE = p_Sku_Code;
                mSkus.TIME_STAMP = System.DateTime.Now;
                mSkus.LASTUPDATE_DATE = System.DateTime.Now;
                mSkus.IP_ADDRESS = p_Ip_Address;
                mSkus.PACKSIZE = p_packSize;
                mSkus.USER_ID = p_UserId;
                mSkus.ExecuteQuery();
                return "Record Updated";

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

        public bool InsertProductSection(string SECTION_CODE, string SECTION_NAME, string PRINTER_NAME, bool IS_PRINT)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertPRODUCT_SECTION mDepartment = new spInsertPRODUCT_SECTION();
                mDepartment.Connection = mConnection;
                mDepartment.SECTION_CODE = SECTION_CODE;
                mDepartment.SECTION_NAME = SECTION_NAME;
                mDepartment.PRINTER_NAME = PRINTER_NAME;
                mDepartment.IS_PRINT = IS_PRINT;
                bool a = mDepartment.ExecuteQuery();
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
        public bool UpdateProductSection(int SECTION_ID, string SECTION_CODE, string SECTION_NAME, string PRINTER_NAME, bool IS_ACTIVE, bool IS_PRINT)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdatePRODUCT_SECTION mSERVICE_TYPE = new spUpdatePRODUCT_SECTION();

                mSERVICE_TYPE.Connection = mConnection;
                mSERVICE_TYPE.IS_ACTIVE = IS_ACTIVE;
                mSERVICE_TYPE.PRINTER_NAME = PRINTER_NAME;
                mSERVICE_TYPE.SECTION_CODE = SECTION_CODE;
                mSERVICE_TYPE.SECTION_ID = SECTION_ID;
                mSERVICE_TYPE.SECTION_NAME = SECTION_NAME;
                mSERVICE_TYPE.IS_PRINT = IS_PRINT;
                bool a = mSERVICE_TYPE.ExecuteQuery();
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
        public DataTable SelectProductSection(int SECTION_ID, string SECTION_CODE, string SECTION_NAME)
        {

            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPRODUCT_SECTION mSERVICE_TYPE = new spSelectPRODUCT_SECTION();
                mSERVICE_TYPE.Connection = mConnection;

                mSERVICE_TYPE.SECTION_CODE = SECTION_CODE;
                mSERVICE_TYPE.SECTION_ID = SECTION_ID;
                mSERVICE_TYPE.SECTION_NAME = SECTION_NAME;

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

        public bool InsertBarcode(string p_Company_Name, string p_Product_Name, string p_Product_price, byte[] p_image)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertBARCODE mSkus = new spInsertBARCODE();

                mSkus.Connection = mConnection;
                mSkus.COMPANY_NAME = p_Company_Name;
                mSkus.PRODUCT_NAME = p_Product_Name;
                mSkus.PRODUCT_PRICE = p_Product_price;
                mSkus.BARCODE_IMAGE = p_image;

                mSkus.ExecuteQuery();
                return true;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                //return exp.Message;
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
        public DataTable SelectSkuBarcode()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectBARCODE mBarcode = new spSelectBARCODE();
                mBarcode.Connection = mConnection;
                //mBarcode.PRODUCT_NAME = p_ROWNO;
                DataTable dt = mBarcode.ExecuteTable();
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

        public DataTable GetMaxSku_ID()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectMaxSKU_ID mdpt_id = new spSelectMaxSKU_ID();
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

        public DataTable GetFinshedDetail(int p_FINISHED_SKU_ID, int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetFinishedDetail mSkuInfo = new uspGetFinishedDetail();
                mSkuInfo.Connection = mConnection;
                mSkuInfo.FINISHED_SKU_ID = p_FINISHED_SKU_ID;
                mSkuInfo.TYPE_ID = p_TYPE_ID;
                DataTable dt = mSkuInfo.ExecuteTable();
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

        public bool InsertFinishedSKU(int p_FINISHED_SKU_ID,decimal p_RecipeQty, int p_RecipeUnit, DateTime p_DOCUMENT_DATE
            , int p_USER_ID,bool p_Is_Production, DataTable dtFinishedDetail)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                uspInsertFinishedMaster mFinishedMaster = new uspInsertFinishedMaster();
                mFinishedMaster.Connection = mConnection;
                mFinishedMaster.Transaction = mTransaction;

                //------------Insert Finished Goods Master----------

                if (dtFinishedDetail.Rows.Count > 0)
                {
                    mFinishedMaster.FINISHED_SKU_ID = p_FINISHED_SKU_ID;
                    mFinishedMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                    mFinishedMaster.USER_ID = p_USER_ID;
                    mFinishedMaster.Recipe_Qty = p_RecipeQty;
                    mFinishedMaster.intRecipeMUnitCode = p_RecipeUnit;
                    mFinishedMaster.Is_Production = p_Is_Production;
                    mFinishedMaster.ExecuteQuery();

                    //----------------Insert Finished Goods Detail-------------
                    uspInsertFinishedDetail mFinishedDetail = new uspInsertFinishedDetail();
                    mFinishedDetail.Connection = mConnection;
                    mFinishedDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtFinishedDetail.Rows)
                    {
                        mFinishedDetail.FINISHED_GOOD_MASTER_ID = mFinishedMaster.FINISHED_GOOD_MASTER_ID;
                        mFinishedDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mFinishedDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                        mFinishedDetail.intStockMUnitCode = int.Parse(dr["UOM_ID"].ToString());
                        mFinishedDetail.ExecuteQuery();
                    }
                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }

        public DataSet GetSKUClosingStockLastPrice(int p_SKU_ID, int p_DISTRIBUTOR_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetClosingStockLastPrice mspSelectSkuInfo = new uspGetClosingStockLastPrice();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.SKU_ID = p_SKU_ID;
                mspSelectSkuInfo.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                return mspSelectSkuInfo.ExecuteTable();                
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

        public DataTable CheckSKU(int p_SKU_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spCheckSKU mSkuInfo = new spCheckSKU();
                mSkuInfo.Connection = mConnection;
                mSkuInfo.SKU_ID = p_SKU_ID;
                DataTable dt = mSkuInfo.ExecuteTable();
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

        public DataTable SelectCategoryType()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCategoryType mspSelectCategoryType = new spSelectCategoryType();
                mspSelectCategoryType.Connection = mConnection;


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

        public DataTable GetSKUByName(string p_SKU_NAME,int p_CATEGORY_ID,int p_BRAND_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetSKUSByName mSKU = new spGetSKUSByName();
                mSKU.Connection = mConnection;
                mSKU.SKU_NAME = p_SKU_NAME;
                mSKU.CATEGORY_ID = p_CATEGORY_ID;
                mSKU.BRAND_ID = p_BRAND_ID;
                DataTable ds = mSKU.ExecuteTable();

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

        #region Recipe Production

        public DataTable SelectRecipeInfo(int p_finish_id, long p_lngRecipeProductionCode, int DistributorID, DateTime Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelecttblRecipeProductionMaster mspSelectSkuInfo = new spSelecttblRecipeProductionMaster();
                mspSelectSkuInfo.Connection = mConnection;

                mspSelectSkuInfo.FINISHED_SKU_ID = p_finish_id;
                mspSelectSkuInfo.lngRecipeProductionCode = p_lngRecipeProductionCode;
                mspSelectSkuInfo.DISTRIBUTOR_ID = DistributorID;
                mspSelectSkuInfo.DATE = Date;

                DataTable ds = mspSelectSkuInfo.ExecuteTable();

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

        public DataTable SelectRecipeInfo(int p_finish_id, long p_lngRecipeProductionCode, int DistributorID, DateTime Date,int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelecttblRecipeProductionMaster mspSelectSkuInfo = new spSelecttblRecipeProductionMaster();
                mspSelectSkuInfo.Connection = mConnection;

                mspSelectSkuInfo.FINISHED_SKU_ID = p_finish_id;
                mspSelectSkuInfo.lngRecipeProductionCode = p_lngRecipeProductionCode;
                mspSelectSkuInfo.DISTRIBUTOR_ID = DistributorID;
                mspSelectSkuInfo.DATE = Date;
                mspSelectSkuInfo.TYPE_ID = p_TYPE_ID;
                DataTable ds = mspSelectSkuInfo.ExecuteTable();

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
        public bool InsertRecipeProduction(int p_FINISHED_GOOD_MASTER_ID, int p_DISTRIBUTOR_ID, int p_FINISHED_SKU_ID, decimal p_RecipeQty
            , decimal p_ActualQty, DateTime p_Production_DATE, int p_RecipeUnit, DateTime p_DOCUMENT_DATE, int p_USER_ID
            , DataTable dtFinishedDetail)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertblRecipeProductionMaster mRecipeMaster = new spInsertblRecipeProductionMaster();
                mRecipeMaster.Connection = mConnection;
                mRecipeMaster.Transaction = mTransaction;

                //------------Insert Recipe Master----------\\

                if (dtFinishedDetail.Rows.Count > 0)
                {
                    mRecipeMaster.FINISHED_SKU_ID = p_FINISHED_SKU_ID;
                    mRecipeMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mRecipeMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                    mRecipeMaster.USER_ID = p_USER_ID;
                    mRecipeMaster.ExpectedProduction_Qty = p_RecipeQty;
                    mRecipeMaster.ActualProduction_Qty = p_ActualQty;
                    mRecipeMaster.FINISHED_GOOD_MASTER_ID = p_FINISHED_GOOD_MASTER_ID;
                    mRecipeMaster.intProductionMUnitCode = p_RecipeUnit;
                    mRecipeMaster.Production_DATE = p_Production_DATE;
                    mRecipeMaster.TIME_STAMP = DateTime.Now;
                    mRecipeMaster.LASTUPDATE_DATE = DateTime.Now;
                    mRecipeMaster.IS_ACTIVE = true;


                    mRecipeMaster.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;

                    mStockUpdate.PRINCIPAL_ID = 0;
                    mStockUpdate.TYPE_ID = 17;
                    mStockUpdate.DISTRIBUTOR_ID = mRecipeMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mRecipeMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mRecipeMaster.FINISHED_SKU_ID;
                    mStockUpdate.STOCK_QTY = mRecipeMaster.ActualProduction_Qty;
                    mStockUpdate.PRICE = 0;
                    mStockUpdate.FREE_QTY = 0;
                    mStockUpdate.BATCHNO = "NA";
                    mStockUpdate.UOM_ID = mRecipeMaster.intProductionMUnitCode;
                    mStockUpdate.ExecuteQuery();


                    //----------------Insert Finished Goods Detail-------------
                    spInserttblRecipeProductionDetail mRecipeDetail = new spInserttblRecipeProductionDetail();
                    mRecipeDetail.Connection = mConnection;
                    mRecipeDetail.Transaction = mTransaction;

                    UspProcessStockRegister mStockDetail = new UspProcessStockRegister();
                    mStockDetail.Connection = mConnection;
                    mStockDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtFinishedDetail.Rows)
                    {
                        mRecipeDetail.lngRecipeProductionCode = mRecipeMaster.lngRecipeProductionCode;
                        mRecipeDetail.FINISHED_GOOD_DETAIL_ID = int.Parse(dr["FINISHED_GOOD_DETAIL_ID"].ToString());
                        mRecipeDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mRecipeDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                        mRecipeDetail.intStockMUnitCode = int.Parse(dr["UOM_ID"].ToString());
                        mRecipeDetail.sintRowNo = 0;
                        mRecipeDetail.ExecuteQuery();
                        
                        mStockDetail.PRINCIPAL_ID = 0;
                        mStockDetail.TYPE_ID = 18;
                        mStockDetail.DISTRIBUTOR_ID = mRecipeMaster.DISTRIBUTOR_ID;
                        mStockDetail.STOCK_DATE = mRecipeMaster.DOCUMENT_DATE;
                        mStockDetail.SKU_ID = mRecipeDetail.SKU_ID;
                        mStockDetail.STOCK_QTY = mRecipeDetail.QUANTITY;
                        mStockDetail.PRICE = 0;
                        mStockDetail.FREE_QTY = 0;
                        mStockDetail.BATCHNO = "NA";
                        mStockDetail.UOM_ID = mRecipeDetail.intStockMUnitCode;
                        mStockDetail.ExecuteQuery();

                    }
                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }

        public bool UpdateRecipeProduction(long MASTER_ID, int p_DISTRIBUTOR_ID, decimal p_RecipeQty, int p_FINISHED_SKU_ID
           , decimal p_ActualQty, DateTime p_Production_DATE, int p_RecipeUnit, DateTime p_DOCUMENT_DATE, int p_USER_ID
           , DataTable dtRecipeDetail)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatetblRecipeProductionMaster mRecipeMaster = new spUpdatetblRecipeProductionMaster();
                mRecipeMaster.Connection = mConnection;
                mRecipeMaster.Transaction = mTransaction;

                //------------Insert Recipe Master----------\\

                if (dtRecipeDetail.Rows.Count > 0)
                {
                    UspUpdatePurchaseDetailStock mRecipeStock = new UspUpdatePurchaseDetailStock();
                    mRecipeStock.Connection = mConnection;
                    mRecipeStock.Transaction = mTransaction;

                    foreach (DataRow dr in dtRecipeDetail.Rows)
                    {

                        mRecipeStock.TYPEID = 21;
                        mRecipeStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                        mRecipeStock.PURCHASE_DETAIL_ID = 0;
                        mRecipeStock.PURCHASE_MASTER_ID = MASTER_ID;
                        mRecipeStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mRecipeStock.DATE = p_DOCUMENT_DATE;
                        mRecipeStock.QTY= decimal.Parse(dr["ORG_QUANTITY"].ToString());
                        mRecipeStock.ExecuteQuery();
                    }


                    mRecipeMaster.USER_ID = p_USER_ID;
                    mRecipeMaster.ActualProduction_Qty = p_ActualQty;
                    mRecipeMaster.Production_DATE = p_Production_DATE;
                    mRecipeMaster.LASTUPDATE_DATE = DateTime.Now;
                    mRecipeMaster.IS_ACTIVE = true;
                    mRecipeMaster.lngRecipeProductionCode = MASTER_ID;

                    mRecipeMaster.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;

                    mStockUpdate.PRINCIPAL_ID = 0;
                    mStockUpdate.TYPE_ID = 17;
                    mStockUpdate.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = p_DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = p_FINISHED_SKU_ID;
                    mStockUpdate.STOCK_QTY = mRecipeMaster.ActualProduction_Qty;
                    mStockUpdate.PRICE = 0;
                    mStockUpdate.FREE_QTY = 0;
                    mStockUpdate.BATCHNO = "NA";
                    mStockUpdate.UOM_ID = p_RecipeUnit;
                    mStockUpdate.ExecuteQuery();


                    //----------------Insert Finished Goods Detail-------------
                    spInserttblRecipeProductionDetail mRecipeDetail = new spInserttblRecipeProductionDetail();
                    mRecipeDetail.Connection = mConnection;
                    mRecipeDetail.Transaction = mTransaction;

                    UspProcessStockRegister mStockDetail = new UspProcessStockRegister();
                    mStockDetail.Connection = mConnection;
                    mStockDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtRecipeDetail.Rows)
                    {
                        mRecipeDetail.lngRecipeProductionCode = mRecipeMaster.lngRecipeProductionCode;
                        mRecipeDetail.FINISHED_GOOD_DETAIL_ID = int.Parse(dr["FINISHED_GOOD_DETAIL_ID"].ToString());
                        mRecipeDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mRecipeDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                        mRecipeDetail.intStockMUnitCode = int.Parse(dr["UOM_ID"].ToString());
                        mRecipeDetail.sintRowNo = 0;
                        mRecipeDetail.ExecuteQuery();

                        mStockDetail.PRINCIPAL_ID = 0;
                        mStockDetail.TYPE_ID = 18;
                        mStockDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                        mStockDetail.STOCK_DATE = p_DOCUMENT_DATE;
                        mStockDetail.SKU_ID = mRecipeDetail.SKU_ID;
                        mStockDetail.STOCK_QTY = mRecipeDetail.QUANTITY;
                        mStockDetail.PRICE = 0;
                        mStockDetail.FREE_QTY = 0;
                        mStockDetail.BATCHNO = "NA";
                        mStockDetail.UOM_ID = mRecipeDetail.intStockMUnitCode;
                        mStockDetail.ExecuteQuery();

                    }
                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }

        public bool DeleteRecipeProduction(long p_MASTER_ID,int p_DISTRIBUTOR_ID,DateTime p_DOCUMENT_DATE,int p_FINISHED_SKU_ID, int p_USER_ID)
        {
            DataTable dtRecipeDetail = new DataTable();
            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatetblRecipeProductionMaster mRecipeMaster = new spUpdatetblRecipeProductionMaster();
                mRecipeMaster.Connection = mConnection;
                mRecipeMaster.Transaction = mTransaction;

                spSelecttblRecipeProductionMaster mSelectRecipe = new spSelecttblRecipeProductionMaster();
                mSelectRecipe.Connection = mConnection;
                mSelectRecipe.Transaction = mTransaction;

                mSelectRecipe.FINISHED_SKU_ID = p_FINISHED_SKU_ID;
                mSelectRecipe.lngRecipeProductionCode = p_MASTER_ID;
                mSelectRecipe.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mSelectRecipe.DATE = p_DOCUMENT_DATE;
                mSelectRecipe.TYPE_ID = 3;
                dtRecipeDetail = mSelectRecipe.ExecuteTable();

                if (dtRecipeDetail.Rows.Count > 0)
                {
                    UspUpdatePurchaseDetailStock mRecipeStock = new UspUpdatePurchaseDetailStock();
                    mRecipeStock.Connection = mConnection;
                    mRecipeStock.Transaction = mTransaction;

                    foreach (DataRow dr in dtRecipeDetail.Rows)
                    {

                        mRecipeStock.TYPEID = 21;
                        mRecipeStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                        mRecipeStock.PURCHASE_DETAIL_ID = 0;
                        mRecipeStock.PURCHASE_MASTER_ID = p_MASTER_ID;
                        mRecipeStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mRecipeStock.DATE = p_DOCUMENT_DATE;
                        mRecipeStock.QTY = decimal.Parse(dr["ORG_QUANTITY"].ToString());
                        mRecipeStock.ExecuteQuery();
                    }


                    mRecipeMaster.USER_ID = p_USER_ID;
                    mRecipeMaster.ActualProduction_Qty = Convert.ToDecimal(dtRecipeDetail.Rows[0]["ActualProduction_Qty"]);
                    mRecipeMaster.Production_DATE = p_DOCUMENT_DATE;
                    mRecipeMaster.LASTUPDATE_DATE = DateTime.Now;
                    mRecipeMaster.IS_ACTIVE = false;
                    mRecipeMaster.lngRecipeProductionCode = p_MASTER_ID;

                    mRecipeMaster.ExecuteQuery();

                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }
        #endregion

        #region Package Material

        public DataTable GetPackageDetail(int p_SKU_ID, int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetPackageDetail mSkuInfo = new uspGetPackageDetail();
                mSkuInfo.Connection = mConnection;
                mSkuInfo.SKU_ID = p_SKU_ID;
                mSkuInfo.TYPE_ID = p_TYPE_ID;
                DataTable dt = mSkuInfo.ExecuteTable();
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

        public bool InsertPackagedSKU(int p_SKU_ID, decimal p_PackageQty, int p_Unit, DateTime p_DOCUMENT_DATE
           , int p_USER_ID, DataTable dtPackageDetail)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                uspInsertPackageMaster mPackageMaster = new uspInsertPackageMaster();
                mPackageMaster.Connection = mConnection;
                mPackageMaster.Transaction = mTransaction;

                //------------Insert Finished Goods Master----------

                if (dtPackageDetail.Rows.Count > 0)
                {
                    mPackageMaster.SKU_ID = p_SKU_ID;
                    mPackageMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                    mPackageMaster.USER_ID = p_USER_ID;
                    mPackageMaster.Package_Qty = p_PackageQty;
                    mPackageMaster.intMUnitCode = p_Unit;

                    mPackageMaster.ExecuteQuery();

                    //----------------Insert Finished Goods Detail-------------
                    uspInsertPackageDetail mPackageDetail = new uspInsertPackageDetail();
                    mPackageDetail.Connection = mConnection;
                    mPackageDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtPackageDetail.Rows)
                    {
                        mPackageDetail.intPackageMaterialID = mPackageMaster.intPackageMaterialID;
                        mPackageDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mPackageDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                        mPackageDetail.intStockMUnitCode = int.Parse(dr["UOM_ID"].ToString());
                        mPackageDetail.DineIn_CUSTOMER_TYPE_ID = int.Parse(dr["DINE_IN"].ToString());
                        mPackageDetail.Delivery_CUSTOMER_TYPE_ID = int.Parse(dr["DELIVERY"].ToString());
                        mPackageDetail.TakeAway_CUSTOMER_TYPE_ID = int.Parse(dr["TAKEAWAY"].ToString());

                        mPackageDetail.ExecuteQuery();
                    }
                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }

        #endregion

        public DataTable GetItemList(int p_TYPE_ID, int p_CATEGORY_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetItemList mSKU = new uspGetItemList();
                mSKU.Connection = mConnection;
                mSKU.TYPE_ID = p_TYPE_ID;
                mSKU.CATEGORY_ID = p_CATEGORY_ID;
                DataTable ds = mSKU.ExecuteTable();

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

        #region By Products
        public bool InsertByProduct(int p_FINISHED_SKU_ID, DateTime p_DOCUMENT_DATE, int p_USER_ID, DataTable dtFinishedDetail)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                uspInsertByProductMaster mFinishedMaster = new uspInsertByProductMaster();
                mFinishedMaster.Connection = mConnection;
                mFinishedMaster.Transaction = mTransaction;

                //------------Insert By Product Master----------
                mFinishedMaster.FINISHED_SKU_ID = p_FINISHED_SKU_ID;
                mFinishedMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mFinishedMaster.USER_ID = p_USER_ID;
                mFinishedMaster.ExecuteQuery();

                //----------------Insert By Product Detail-------------
                uspInsertByPorductDetail mFinishedDetail = new uspInsertByPorductDetail();
                mFinishedDetail.Connection = mConnection;
                mFinishedDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtFinishedDetail.Rows)
                {
                    mFinishedDetail.BY_PRODUCT_MASTER_ID = mFinishedMaster.BY_PRODUCT_MASTER_ID;
                    mFinishedDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mFinishedDetail.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable GetByProduct(int p_FINISHED_SKU_ID, int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetByProductDetail mSkuInfo = new uspGetByProductDetail();
                mSkuInfo.Connection = mConnection;
                mSkuInfo.FINISHED_SKU_ID = p_FINISHED_SKU_ID;
                mSkuInfo.TYPE_ID = p_TYPE_ID;
                DataTable dt = mSkuInfo.ExecuteTable();
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

        #region Wastage Item
        public bool InsertWastageProduct(int p_FINISHED_SKU_ID, DateTime p_DOCUMENT_DATE, int p_USER_ID, DataTable dtFinishedDetail)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                uspInsertWastageProductMaster mFinishedMaster = new uspInsertWastageProductMaster();
                mFinishedMaster.Connection = mConnection;
                mFinishedMaster.Transaction = mTransaction;

                //------------Insert By Product Master----------
                mFinishedMaster.FINISHED_SKU_ID = p_FINISHED_SKU_ID;
                mFinishedMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mFinishedMaster.USER_ID = p_USER_ID;
                mFinishedMaster.ExecuteQuery();

                //----------------Insert By Product Detail-------------
                uspInsertWastagePorductDetail mFinishedDetail = new uspInsertWastagePorductDetail();
                mFinishedDetail.Connection = mConnection;
                mFinishedDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtFinishedDetail.Rows)
                {
                    mFinishedDetail.WASTAGE_PRODUCT_MASTER_ID = mFinishedMaster.WASTAGE_PRODUCT_MASTER_ID;
                    mFinishedDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mFinishedDetail.RATIO_FACTOR = decimal.Parse(dr["RATIO_FACTOR"].ToString());
                    mFinishedDetail.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable GetWastageProduct(int p_FINISHED_SKU_ID, int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetWastageProductDetail mSkuInfo = new uspGetWastageProductDetail();
                mSkuInfo.Connection = mConnection;
                mSkuInfo.FINISHED_SKU_ID = p_FINISHED_SKU_ID;
                mSkuInfo.TYPE_ID = p_TYPE_ID;
                DataTable dt = mSkuInfo.ExecuteTable();
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
    }
}