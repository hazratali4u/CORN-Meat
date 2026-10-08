using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;


namespace CORNDatabaseLayer.Classes
{
    public class spSelectProductPriceInfo
    {
         #region Private Members
        private string sp_Name = "spSelectProductPriceInfo";
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;

        private int m_PP_ID;
        private int m_PP_ID2;
        private int m_CATG_ID;
        //private decimal m_RETAIL_PRICE;
        //private decimal m_CURRENT_PRICE;
        //private decimal m_NEW_PRICE;
        //private DateTime m_DOCUMENT_DATE;
        //private DateTime m_TIME_STAMP;
        //private int m_USER_ID;
        //private bool m_IS_ACTIVE;
        
        //private int m_SKU_ID;
        ////gET cOLUMNS FROM SKU TABLE
        //private string m_SKU_CODE;
        //private string m_SKU_NAME;
        //private string m_SKU_DESCRIPTION;

       
		#endregion
		#region Public Properties

        public int PP_ID
        {
            get 
            {
                return m_PP_ID;
            }
            set
            {
                m_PP_ID = value;
            }
        }
        public int PP_ID2
        {
            get
            {
                return m_PP_ID2;
            }
            set
            {
                m_PP_ID2 = value;
            }
        }
        public int CATG_ID
        {
            get
            {
                return m_CATG_ID;
            }
            set
            {
                m_CATG_ID = value;
            }
        }
        //public decimal RETAIL_PRICE
        //{
        //    set
        //    {
        //        m_RETAIL_PRICE = value;
        //    }
        //    get
        //    {
        //        return m_RETAIL_PRICE;
        //    }
        //}   
        //public decimal CURRENT_PRICE
        //{
        //    set
        //    {
        //        m_CURRENT_PRICE = value;
        //    }
        //    get
        //    {
        //        return m_CURRENT_PRICE;
        //    }
        //}
        //public decimal NEW_PRICE
        //{
        //    set
        //    {
        //        m_NEW_PRICE = value;
        //    }
        //    get
        //    {
        //        return m_NEW_PRICE;
        //    }
        //}
        //public DateTime DOCUMENT_DATE
        //{
        //    set
        //    {
        //        m_DOCUMENT_DATE = value;
        //    }
        //    get
        //    {
        //        return m_DOCUMENT_DATE;
        //    }
        //}
        //public bool IS_ACTIVE
        //{
        //    set
        //    {
        //        m_IS_ACTIVE = value;
        //    }
        //    get
        //    {
        //        return m_IS_ACTIVE;
        //    }
        //}
        //public DateTime TIME_STAMP
        //{
        //    set
        //    {
        //        m_TIME_STAMP = value;
        //    }
        //    get
        //    {
        //        return m_TIME_STAMP;
        //    }
        //}
        //public int USER_ID
        //{
        //    set
        //    {
        //        m_USER_ID = value;
        //    }
        //    get
        //    {
        //        return m_USER_ID;
        //    }
        //}

        ////SKU TABLE
        //public int SKU_ID
        //{
        //    get
        //    {
        //        return m_SKU_ID;
        //    }
        //    set
        //    {
        //        m_SKU_ID=value;
        //    }
        //}
        //public string SKU_CODE
        //{
        //    get
        //    {
        //        return m_SKU_CODE;
        //    }
        //}
        //public string SKU_NAME
        //{
        //    get
        //    {
        //        return m_SKU_NAME;
        //    }
        //}
        //public string SKU_DESCRIPTION
        //{
        //    get
        //    {
        //        return m_SKU_DESCRIPTION;
        //    }
        //}
       

        public IDbConnection  Connection
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
		public IDbTransaction  Transaction
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
        public spSelectProductPriceInfo()
		{


		}
		#endregion

		#region public Methods
		public bool  ExecuteQuery()
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
                //m_PT_ID = (Int32)((IDataParameter)(cmd.Parameters["@PT_ID"])).Value;
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
				GetParameterCollection(ref command);
				IDataReader dr = command.ExecuteReader();
				return dr;
			}
			catch(Exception exp)
			{
				return null;
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
				GetParameterCollection(ref command);
				IDbDataAdapter da = ProviderFactory.GetAdapter(EnumProviders.SQLClient);
				da.SelectCommand = command;
				DataSet ds = new DataSet();
				da.Fill(ds);
				return ds.Tables[0];
			}
			catch(Exception exp)
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
                command.CommandText = sp_Name; ;
				command.Connection = m_connection;
				GetParameterCollection(ref command);
				object o;
				o = command.ExecuteScalar();


				return o.ToString();
			}
			catch(Exception exp)
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
                m_PP_ID = Convert.ToInt32(dr["PP_ID"]);
                m_CATG_ID = Convert.ToInt32(dr["CATG_ID"]);
                //m_IS_ACTIVE = Convert.ToBoolean(dr["ISACTIVE"]);
                //m_CURRENT_PRICE = Convert.ToDecimal(dr["CURRENT_PRICE"]);
                //m_RETAIL_PRICE = Convert.ToDecimal(dr["RETAIL_PRICE"]);
                //m_NEW_PRICE = Convert.ToDecimal(dr["NEW_PRICE"]);
                //m_TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                //m_DOCUMENT_DATE = Convert.ToDateTime(dr["DOCUMENT_DATE"]);
                //m_USER_ID = Convert.ToInt32(dr["USER_ID"]);
                //m_SKU_ID = Convert.ToInt32(dr["SKU_ID"]);
                //m_SKU_CODE = Convert.ToString(dr["SKU_CODE"]);
                //m_SKU_NAME = Convert.ToString(dr["SKU_NAME"]);
                //m_SKU_DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]);
            }
        }

	     public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;

			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PP_ID"; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_PP_ID == Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
                parameter.Value = m_PP_ID;
			}
			pparams.Add(parameter);
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PP_ID2";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_PP_ID2 == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PP_ID2;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CATG_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_CATG_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CATG_ID;
            }
            pparams.Add(parameter);

        

		}
		#endregion
    }
}
