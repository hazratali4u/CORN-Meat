using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;


namespace CORNDatabaseLayer.Classes
{
	public class UspUpdatePurchaseDetailStock
	{
		#region Private Members
        private string sp_Name = " UspUpdatePurchaseDetailStock";
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;


		private int m_DISTRIBUTOR_ID;
		private int m_TYPEID;
		private int m_SKU_ID;
		private long m_PURCHASE_MASTER_ID;
		private long m_PURCHASE_DETAIL_ID;
        private DateTime m_DATE;
        private Decimal m_QTY;
		#endregion


		#region Public Properties
		public int DISTRIBUTOR_ID
		{
			set
			{
                m_DISTRIBUTOR_ID = value;
			}
			get
			{
				return m_DISTRIBUTOR_ID;
			}
		}


		public int TYPEID
		{
			set
			{
                m_TYPEID = value;
			}
			get
			{
				return m_TYPEID;
			}
		}


		public int SKU_ID
		{
			set
			{
                m_SKU_ID = value;
			}
			get
			{
				return m_SKU_ID;
			}
		}


		public long PURCHASE_MASTER_ID
		{
			set
			{
                m_PURCHASE_MASTER_ID = value;
			}
			get
			{
				return m_PURCHASE_MASTER_ID;
			}
		}


		public long PURCHASE_DETAIL_ID
		{
			set
			{
                m_PURCHASE_DETAIL_ID = value;
			}
			get
			{
				return m_PURCHASE_DETAIL_ID;
			}
		}


        public DateTime DATE
		{
			set
			{
                m_DATE = value;
			}
			get
			{
                return m_DATE;
			}
		}
        public decimal QTY
        {
            set
            {
                m_QTY = value;
            }
            get
            {
                return m_QTY;
            }
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
		public UspUpdatePurchaseDetailStock()
		{
		m_DISTRIBUTOR_ID = Constants.IntNullValue;
		m_TYPEID = Constants.IntNullValue;
		m_SKU_ID = Constants.IntNullValue;
            m_PURCHASE_MASTER_ID = Constants.LongNullValue;
            m_PURCHASE_DETAIL_ID = Constants.LongNullValue;
            m_DATE = Constants.DateNullValue;
            m_QTY = Constants.DecimalNullValue;
		}
		#endregion

		#region public Methods
        public bool ExecuteQuery()
		{
			try
			{
			    IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "UspUpdatePurchaseDetailStock";
                cmd.Connection = m_connection;
                if (m_transaction != null)
				{
					cmd.Transaction = m_transaction;
				}
				GetParameterCollection(ref cmd);
				cmd.ExecuteNonQuery();
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
				command.CommandText = "UspUpdatePurchaseDetailStock";
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
				command.CommandText = "UspUpdatePurchaseDetailStock";
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
				command.CommandText = "UspUpdatePurchaseDetailStock";
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
                m_DISTRIBUTOR_ID = Convert.ToInt32(dr["DISTRIBUTOR_ID"]);
                m_TYPEID = Convert.ToInt32(dr["TYPEID"]);
                m_SKU_ID = Convert.ToInt32(dr["SKU_ID"]);
                m_PURCHASE_MASTER_ID = Convert.ToInt64(dr["PURCHASE_MASTER_ID"]);
                m_PURCHASE_DETAIL_ID = Convert.ToInt64(dr["PURCHASE_DETAIL_ID"]);
				}
			}


		    public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DISTRIBUTOR_ID";
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_DISTRIBUTOR_ID == Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DISTRIBUTOR_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TYPEID";
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_TYPEID == Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_TYPEID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SKU_ID";
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_SKU_ID == Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SKU_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PURCHASE_MASTER_ID";
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_PURCHASE_MASTER_ID == Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PURCHASE_MASTER_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PURCHASE_DETAIL_ID";
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_PURCHASE_DETAIL_ID == Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PURCHASE_DETAIL_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DATE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_DATE == Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
                parameter.Value = m_DATE;
			}
			pparams.Add(parameter);



            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@QTY";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_QTY == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_QTY;
            }
            pparams.Add(parameter);
		}
		#endregion
	}
}
