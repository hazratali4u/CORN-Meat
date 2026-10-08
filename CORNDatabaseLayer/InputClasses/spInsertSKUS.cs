using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;

namespace CORNDatabaseLayer.Classes
{
    public class spInsertSKUS
    {
        #region Private Members
        private string sp_Name = "spInsertSKUS";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private short m_UNITS_IN_CASE;
        private int m_PRINCIPAL_ID;
        private int m_DIVISION_ID;
        private int m_BRAND_ID;
        private int m_CATEGORY_ID;
        private int m_COMPANY_ID;
        private int m_USER_ID;
        private int m_SECTION_ID;
        private int m_SKU_ID;
        private decimal m_GST_RATE_REG;
        private decimal m_GST_RATE_UNREG;
        private DateTime m_TIME_STAMP;
        private DateTime m_LASTUPDATE_DATE;
        private bool m_ISEXEMPTED;
        private bool m_ISACTIVE;
        private bool m_IS_DESC;
        private bool m_IS_DEAL;
        private bool m_IS_MODIFIER;
        private bool m_Is_WastageItem;
        private bool m_IS_Recipe;
        private string m_SKU_CODE;
        private string m_SKU_NAME;
        private string m_PACKSIZE;
        private string m_IP_ADDRESS;
        private string m_DESCRIPTION;
        private char m_GST_ON;
        private decimal m_MIN_LEVEL;
        private decimal m_REORDER_LEVEL;

        private bool m_IS_HasMODIFIER;
        private int m_intSaleMUnitCode;
        private int m_intPurchaseMUnitCode;
        private int m_intStockMUnitCode;
        private float m_fltFEDPercentage;
        private float m_fltWHTPercentage;
        private float m_fltAgeInDays;
        private bool m_IsMarketItem;
        private bool m_IsReplaceable;
        private bool m_IsFEDItem;
        private bool m_IsWHTItem;
        private bool m_IsSerialized;
        private bool m_IsHazardous;
        private bool m_IsBatchItem;
        private bool m_IsOverSaleAllowed;
        private bool m_IsExpiryAllowed;
        private bool m_IsWarehouseItem;
        private decimal m_MAX_LEVEL;
        private decimal m_Sale_to_PurchaseFactor;
        private decimal m_Purchase_to_SaleFactor;
        private decimal m_Sale_to_StockFactor;
        private decimal m_Purchase_to_StockFactor;
        private decimal m_Default_Qty;
        private decimal m_Stock_to_SaleFactor;
        private decimal m_Stock_to_PurchaseFactor;
        private string m_Sale_to_PurchaseOperator;
        private string m_Purchase_to_SaleOperator;
        private string m_Sale_to_StockOperator;
        private string m_Purchase_to_StockOperator;
        private string m_Stock_to_SaleOperator;
        private string m_Stock_to_PurchaseOperator;
        private string m_strDescription;
        private string m_strSerialCode;
        private string m_strStatus;
        private string m_strERPCode;
        private float m_fltShelfAgeInDays;
        private int m_intMUnitLifeCode;
        private decimal m_Ratio_Factor;
        private decimal m_Weight_Consumption;

        #endregion
        #region Public Properties
        public short UNITS_IN_CASE
        {
            set
            {
                m_UNITS_IN_CASE = value;
            }
            get
            {
                return m_UNITS_IN_CASE;
            }
        }
        public int PRINCIPAL_ID
        {
            set
            {
                m_PRINCIPAL_ID = value;
            }
            get
            {
                return m_PRINCIPAL_ID;
            }
        }
        public int DIVISION_ID
        {
            set
            {
                m_DIVISION_ID = value;
            }
            get
            {
                return m_DIVISION_ID;
            }
        }
        public int BRAND_ID
        {
            set
            {
                m_BRAND_ID = value;
            }
            get
            {
                return m_BRAND_ID;
            }
        }
        public int CATEGORY_ID
        {
            set
            {
                m_CATEGORY_ID = value;
            }
            get
            {
                return m_CATEGORY_ID;
            }
        }
        public int COMPANY_ID
        {
            set
            {
                m_COMPANY_ID = value;
            }
            get
            {
                return m_COMPANY_ID;
            }
        }
        public int USER_ID
        {
            set
            {
                m_USER_ID = value;
            }
            get
            {
                return m_USER_ID;
            }
        }
        public int SECTION_ID
        {
            set
            {
                m_SECTION_ID = value;
            }
            get
            {
                return m_SECTION_ID;
            }
        }
        public int SKU_ID
        {
            get
            {
                return m_SKU_ID;
            }
        }
        public decimal GST_RATE_REG
        {
            set
            {
                m_GST_RATE_REG = value;
            }
            get
            {
                return m_GST_RATE_REG;
            }
        }
        public decimal GST_RATE_UNREG
        {
            set
            {
                m_GST_RATE_UNREG = value;
            }
            get
            {
                return m_GST_RATE_UNREG;
            }
        }
        public DateTime TIME_STAMP
        {
            set
            {
                m_TIME_STAMP = value;
            }
            get
            {
                return m_TIME_STAMP;
            }
        }
        public DateTime LASTUPDATE_DATE
        {
            set
            {
                m_LASTUPDATE_DATE = value;
            }
            get
            {
                return m_LASTUPDATE_DATE;
            }
        }
        public bool ISEXEMPTED
        {
            set
            {
                m_ISEXEMPTED = value;
            }
            get
            {
                return m_ISEXEMPTED;
            }
        }
        public bool IS_DESC
        {
            set
            {
                m_IS_DESC = value;
            }
            get
            {
                return m_IS_DESC;
            }
        }
        public bool IS_DEAL
        {
            set
            {
                m_IS_DEAL = value;
            }
            get
            {
                return m_IS_DEAL;
            }
        }
        public bool IS_MODIFIER
        {
            set
            {
                m_IS_MODIFIER = value;
            }
            get
            {
                return m_IS_MODIFIER;
            }
        }
        public bool Is_WastageItem
        {
            set { m_Is_WastageItem = value; }
            get { return m_Is_WastageItem; }
        }
        public bool ISACTIVE
        {
            set
            {
                m_ISACTIVE = value;
            }
            get
            {
                return m_ISACTIVE;
            }
        }
        public bool IS_Recipe
        {
            set
            {
                m_IS_Recipe = value;
            }
            get
            {
                return m_IS_Recipe;
            }
        }
        public string SKU_CODE
        {
            set
            {
                m_SKU_CODE = value;
            }
            get
            {
                return m_SKU_CODE;
            }
        }
        public string SKU_NAME
        {
            set
            {
                m_SKU_NAME = value;
            }
            get
            {
                return m_SKU_NAME;
            }
        }
        public string PACKSIZE
        {
            set
            {
                m_PACKSIZE = value;
            }
            get
            {
                return m_PACKSIZE;
            }
        }
        public string IP_ADDRESS
        {
            set
            {
                m_IP_ADDRESS = value;
            }
            get
            {
                return m_IP_ADDRESS;
            }
        }
        public string DESCRIPTION
        {
            set
            {
                m_DESCRIPTION = value;
            }
            get
            {
                return m_DESCRIPTION;
            }
        }
        public char GST_ON
        {
            set
            {
                m_GST_ON = value;
            }
            get
            {
                return m_GST_ON;
            }
        }
        public decimal MIN_LEVEL
        {
            set
            {
                m_MIN_LEVEL = value;
            }
            get
            {
                return m_MIN_LEVEL;
            }
        }
        public decimal REORDER_LEVEL
        {
            set
            {
                m_REORDER_LEVEL = value;
            }
            get
            {
                return m_REORDER_LEVEL;
            }
        }
       
        public int intSaleMUnitCode
        {
            set
            {
                m_intSaleMUnitCode = value;
            }
            get
            {
                return m_intSaleMUnitCode;
            }
        }
        public int intPurchaseMUnitCode
        {
            set
            {
                m_intPurchaseMUnitCode = value;
            }
            get
            {
                return m_intPurchaseMUnitCode;
            }
        }
        public int intStockMUnitCode
        {
            set
            {
                m_intStockMUnitCode = value;
            }
            get
            {
                return m_intStockMUnitCode;
            }
        }
      
        public float fltFEDPercentage
        {
            set
            {
                m_fltFEDPercentage = value;
            }
            get
            {
                return m_fltFEDPercentage;
            }
        }
        public float fltWHTPercentage
        {
            set
            {
                m_fltWHTPercentage = value;
            }
            get
            {
                return m_fltWHTPercentage;
            }
        }
        public float fltAgeInDays
        {
            set
            {
                m_fltAgeInDays = value;
            }
            get
            {
                return m_fltAgeInDays;
            }
        }
       
        public bool IsMarketItem
        {
            set
            {
                m_IsMarketItem = value;
            }
            get
            {
                return m_IsMarketItem;
            }
        }
        public bool IsReplaceable
        {
            set
            {
                m_IsReplaceable = value;
            }
            get
            {
                return m_IsReplaceable;
            }
        }
        public bool IsFEDItem
        {
            set
            {
                m_IsFEDItem = value;
            }
            get
            {
                return m_IsFEDItem;
            }
        }
        public bool IsWHTItem
        {
            set
            {
                m_IsWHTItem = value;
            }
            get
            {
                return m_IsWHTItem;
            }
        }
        public bool IsSerialized
        {
            set
            {
                m_IsSerialized = value;
            }
            get
            {
                return m_IsSerialized;
            }
        }
        public bool IsHazardous
        {
            set
            {
                m_IsHazardous = value;
            }
            get
            {
                return m_IsHazardous;
            }
        }
        public bool IsBatchItem
        {
            set
            {
                m_IsBatchItem = value;
            }
            get
            {
                return m_IsBatchItem;
            }
        }
        public bool IsOverSaleAllowed
        {
            set
            {
                m_IsOverSaleAllowed = value;
            }
            get
            {
                return m_IsOverSaleAllowed;
            }
        }
        public bool IsExpiryAllowed
        {
            set
            {
                m_IsExpiryAllowed = value;
            }
            get
            {
                return m_IsExpiryAllowed;
            }
        }
        public bool IsWarehouseItem
        {
            set
            {
                m_IsWarehouseItem = value;
            }
            get
            {
                return m_IsWarehouseItem;
            }
        }
        public bool IS_HasMODIFIER
        {
            set
            {
                m_IS_HasMODIFIER = value;
            }
            get
            {
                return m_IS_HasMODIFIER;
            }
        }
        
        public decimal MAX_LEVEL
        {
            set
            {
                m_MAX_LEVEL = value;
            }
            get
            {
                return m_MAX_LEVEL;
            }
        }
        public decimal Sale_to_PurchaseFactor
        {
            set
            {
                m_Sale_to_PurchaseFactor = value;
            }
            get
            {
                return m_Sale_to_PurchaseFactor;
            }
        }
        public decimal Purchase_to_SaleFactor
        {
            set
            {
                m_Purchase_to_SaleFactor = value;
            }
            get
            {
                return m_Purchase_to_SaleFactor;
            }
        }
        public decimal Sale_to_StockFactor
        {
            set
            {
                m_Sale_to_StockFactor = value;
            }
            get
            {
                return m_Sale_to_StockFactor;
            }
        }
        public decimal Purchase_to_StockFactor
        {
            set
            {
                m_Purchase_to_StockFactor = value;
            }
            get
            {
                return m_Purchase_to_StockFactor;
            }
        }
        public decimal Default_Qty
        {
            set
            {
                m_Default_Qty = value;
            }
            get
            {
                return m_Default_Qty;
            }
        }
        public decimal Stock_to_SaleFactor
        {
            set
            {
                m_Stock_to_SaleFactor = value;
            }
            get
            {
                return m_Stock_to_SaleFactor;
            }
        }
        public decimal Stock_to_PurchaseFactor
        {
            set
            {
                m_Stock_to_PurchaseFactor = value;
            }
            get
            {
                return m_Stock_to_PurchaseFactor;
            }
        }
       
        public string Sale_to_PurchaseOperator
        {
            set
            {
                m_Sale_to_PurchaseOperator = value;
            }
            get
            {
                return m_Sale_to_PurchaseOperator;
            }
        }
        public string Purchase_to_SaleOperator
        {
            set
            {
                m_Purchase_to_SaleOperator = value;
            }
            get
            {
                return m_Purchase_to_SaleOperator;
            }
        }
        public string Sale_to_StockOperator
        {
            set
            {
                m_Sale_to_StockOperator = value;
            }
            get
            {
                return m_Sale_to_StockOperator;
            }
        }
        public string Purchase_to_StockOperator
        {
            set
            {
                m_Purchase_to_StockOperator = value;
            }
            get
            {
                return m_Purchase_to_StockOperator;
            }
        }
        public string Stock_to_SaleOperator
        {
            set
            {
                m_Stock_to_SaleOperator = value;
            }
            get
            {
                return m_Stock_to_SaleOperator;
            }
        }
        public string Stock_to_PurchaseOperator
        {
            set
            {
                m_Stock_to_PurchaseOperator = value;
            }
            get
            {
                return m_Stock_to_PurchaseOperator;
            }
        }
       
        public string strDescription
        {
            set
            {
                m_strDescription = value;
            }
            get
            {
                return m_strDescription;
            }
        }
        public string strSerialCode
        {
            set
            {
                m_strSerialCode = value;
            }
            get
            {
                return m_strSerialCode;
            }
        }
        public string strStatus
        {
            set
            {
                m_strStatus = value;
            }
            get
            {
                return m_strStatus;
            }
        }
        public string strERPCode
        {
            set
            {
                m_strERPCode = value;
            }
            get
            {
                return m_strERPCode;
            }
        }
        public float fltShelfAgeInDays
        {
            set
            {
                m_fltShelfAgeInDays = value;
            }
            get
            {
                return m_fltShelfAgeInDays;
            }
        }
        public int intMUnitLifeCode
        {
            set
            {
                m_intMUnitLifeCode = value;
            }
            get
            {
                return m_intMUnitLifeCode;
            }
        }
        public decimal Ratio_Factor
        {
            set { m_Ratio_Factor = value; }
            get { return m_Ratio_Factor; }
        }
        public decimal Weight_Consumption
        {
            set { m_Weight_Consumption = value; }
            get { return m_Weight_Consumption; }
        }
        public IDbConnection Connection
        {
            set
            {
                m_connection = value;
            }
            get
            {
                return m_connection;
            }
        }
        public IDbTransaction Transaction
        {
            set
            {
                m_transaction = value;
            }
            get
            {
                return m_transaction;
            }
        }
        #endregion
        #region Constructor
        public spInsertSKUS()
        {
        }
        #endregion
        #region public Methods
        public bool ExecuteQuery()
        {
            try
            {
                IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp_Name;
                cmd.Connection = m_connection;
                if (m_transaction != null)
                {
                    cmd.Transaction = m_transaction;
                }
                GetParameterCollection(ref cmd);
                cmd.ExecuteNonQuery();
                m_SKU_ID = (int)((IDataParameter)(cmd.Parameters["@SKU_ID"])).Value;
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
            }
        }
        public IDataReader ExecuteReader()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                IDataReader dr = command.ExecuteReader();
                return dr;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }
        public DataTable ExecuteTable()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                IDbDataAdapter da = ProviderFactory.GetAdapter(EnumProviders.SQLClient);
                da.SelectCommand = command;
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }
        public string ExecuteScalar()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                object o;
                o = command.ExecuteScalar();


                return o.ToString();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }
        public void FirstReader(IDataReader dr)
        {
            if (dr.Read())
            {
                m_UNITS_IN_CASE = Convert.ToInt16(dr["UNITS_IN_CASE"]);
                m_PRINCIPAL_ID = Convert.ToInt32(dr["PRINCIPAL_ID"]);
                m_DIVISION_ID = Convert.ToInt32(dr["DIVISION_ID"]);
                m_BRAND_ID = Convert.ToInt32(dr["BRAND_ID"]);
                m_CATEGORY_ID = Convert.ToInt32(dr["CATEGORY_ID"]);
                m_COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                m_USER_ID = Convert.ToInt32(dr["USER_ID"]);
                m_SECTION_ID = Convert.ToInt32(dr["SECTION_ID"]);
                m_SKU_ID = Convert.ToInt32(dr["SKU_ID"]);
                m_GST_RATE_REG = Convert.ToDecimal(dr["GST_RATE_REG"]);
                m_GST_RATE_UNREG = Convert.ToDecimal(dr["GST_RATE_UNREG"]);
                m_TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                m_LASTUPDATE_DATE = Convert.ToDateTime(dr["LASTUPDATE_DATE"]);
                m_ISEXEMPTED = Convert.ToBoolean(dr["ISEXEMPTED"]);
                m_ISACTIVE = Convert.ToBoolean(dr["ISACTIVE"]);
                m_SKU_CODE = Convert.ToString(dr["SKU_CODE"]);
                m_SKU_NAME = Convert.ToString(dr["SKU_NAME"]);
                m_PACKSIZE = Convert.ToString(dr["PACKSIZE"]);
                m_IP_ADDRESS = Convert.ToString(dr["IP_ADDRESS"]);
                m_DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]);
                m_GST_ON = Convert.ToChar(dr["GST_ON"]);
                m_IS_DESC = Convert.ToBoolean(dr["IS_DESC"]);

                m_MIN_LEVEL = Convert.ToDecimal(dr["MIN_LEVEL"]);
                m_REORDER_LEVEL = Convert.ToDecimal(dr["REORDER_LEVEL"]);
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@UNITS_IN_CASE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.SmallInt);
            if (m_UNITS_IN_CASE == Constants.ShortNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_UNITS_IN_CASE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PRINCIPAL_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_PRINCIPAL_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PRINCIPAL_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DIVISION_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_DIVISION_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DIVISION_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@BRAND_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_BRAND_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_BRAND_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CATEGORY_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_CATEGORY_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CATEGORY_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@COMPANY_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_COMPANY_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_COMPANY_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@USER_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_USER_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_USER_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SECTION_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_SECTION_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SECTION_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SKU_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            parameter.Direction = ParameterDirection.Output;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@GST_RATE_REG";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_GST_RATE_REG == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_GST_RATE_REG;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@GST_RATE_UNREG";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_GST_RATE_UNREG == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_GST_RATE_UNREG;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TIME_STAMP";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_TIME_STAMP == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TIME_STAMP;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@LASTUPDATE_DATE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_LASTUPDATE_DATE == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_LASTUPDATE_DATE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@ISEXEMPTED";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_ISEXEMPTED;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@ISACTIVE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_ISACTIVE;
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IS_DESC";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IS_DESC;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IS_DEAL";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IS_DEAL;
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IS_MODIFIER";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IS_MODIFIER;
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Is_WastageItem";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_Is_WastageItem;
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IS_HasMODIFIER";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IS_HasMODIFIER;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IS_Recipe";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IS_Recipe;
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SKU_CODE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_SKU_CODE == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SKU_CODE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SKU_NAME";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_SKU_NAME == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SKU_NAME;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PACKSIZE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_PACKSIZE == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PACKSIZE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IP_ADDRESS";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_IP_ADDRESS == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_IP_ADDRESS;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DESCRIPTION";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_DESCRIPTION == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DESCRIPTION;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@GST_ON";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Char);
            if (m_GST_ON == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_GST_ON;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@MIN_LEVEL";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_MIN_LEVEL == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_MIN_LEVEL;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@REORDER_LEVEL";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_REORDER_LEVEL == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_REORDER_LEVEL;
            }
            pparams.Add(parameter);

            //
            

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@intSaleMUnitCode";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_intSaleMUnitCode == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_intSaleMUnitCode;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@intPurchaseMUnitCode";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_intPurchaseMUnitCode == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_intPurchaseMUnitCode;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@intStockMUnitCode";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_intStockMUnitCode == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_intStockMUnitCode;
            }
            pparams.Add(parameter);

            


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@fltFEDPercentage";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Float);
            if (m_fltFEDPercentage == Constants.FloatNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_fltFEDPercentage;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@fltWHTPercentage";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Float);
            if (m_fltWHTPercentage == Constants.FloatNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_fltWHTPercentage;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@fltAgeInDays";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Float);
            if (m_fltAgeInDays == Constants.FloatNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_fltAgeInDays;
            }
            pparams.Add(parameter);
            

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IsMarketItem";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IsMarketItem;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IsReplaceable";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IsReplaceable;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IsFEDItem";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IsFEDItem;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IsWHTItem";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IsWHTItem;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IsSerialized";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IsSerialized;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IsHazardous";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IsHazardous;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IsBatchItem";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IsBatchItem;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IsOverSaleAllowed";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IsOverSaleAllowed;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IsExpiryAllowed";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IsExpiryAllowed;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IsWarehouseItem";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IsWarehouseItem;
            pparams.Add(parameter);
            


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@MAX_LEVEL";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_MAX_LEVEL == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_MAX_LEVEL;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Sale_to_PurchaseFactor";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_Sale_to_PurchaseFactor == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Sale_to_PurchaseFactor;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Purchase_to_SaleFactor";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_Purchase_to_SaleFactor == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Purchase_to_SaleFactor;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Sale_to_StockFactor";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_Sale_to_StockFactor == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Sale_to_StockFactor;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Purchase_to_StockFactor";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_Purchase_to_StockFactor == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Purchase_to_StockFactor;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Default_Qty";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_Default_Qty == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Default_Qty;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Stock_to_SaleFactor";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_Stock_to_SaleFactor == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Stock_to_SaleFactor;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Stock_to_PurchaseFactor";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_Stock_to_PurchaseFactor == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Stock_to_PurchaseFactor;
            }
            pparams.Add(parameter);

            

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Sale_to_PurchaseOperator";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_Sale_to_PurchaseOperator == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Sale_to_PurchaseOperator;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Purchase_to_SaleOperator";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_Purchase_to_SaleOperator == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Purchase_to_SaleOperator;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Sale_to_StockOperator";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_Sale_to_StockOperator == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Sale_to_StockOperator;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Purchase_to_StockOperator";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_Purchase_to_StockOperator == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Purchase_to_StockOperator;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Stock_to_SaleOperator";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_Stock_to_SaleOperator == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Stock_to_SaleOperator;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Stock_to_PurchaseOperator";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_Stock_to_PurchaseOperator == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Stock_to_PurchaseOperator;
            }
            pparams.Add(parameter);

            


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@strDescription";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.NVarChar);
            if (m_strDescription == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_strDescription;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@strSerialCode";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.NVarChar);
            if (m_strSerialCode == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_strSerialCode;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@strStatus";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.NVarChar);
            if (m_strStatus == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_strStatus;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@strERPCode";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.NVarChar);
            if (m_strERPCode == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_strERPCode;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@fltShelfAgeInDays";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Float);
            if (m_fltShelfAgeInDays == Constants.FloatNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_fltShelfAgeInDays;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@intMUnitLifeCode";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_intMUnitLifeCode == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_intMUnitLifeCode;
            }

            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Ratio_Factor";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_Ratio_Factor == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Ratio_Factor;
            }

            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Weight_Consumption";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_Weight_Consumption == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Weight_Consumption;
            }

            pparams.Add(parameter);
        }
        #endregion
    }
}
