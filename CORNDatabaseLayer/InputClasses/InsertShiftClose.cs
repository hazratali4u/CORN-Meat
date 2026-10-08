using System;
using System.Data;
using CORNDataAccessLayer.Classes;
using CORNCommon.Classes;


namespace CORNDatabaseLayer.Classes
{
	public class InsertShiftClose
	{
		#region Private Members
		private string sp_Name = "InsertShiftClose" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_LOCATION_ID;
		private int m_SHIFT_ID;
		private int m_USER_ID;
		private int m_CURRENCY_5000;
		private int m_CURRENCY_1000;
		private int m_CURRENCY_500;
		private int m_CURRENCY_1;
		private int m_CURRENCY_100;
		private int m_CURRENCY_50;
		private int m_CURRENCY_20;
		private int m_CURRENCY_10;
		private int m_CURRENCY_5;
		private int m_CURRENCY_2;
		private decimal m_CASH_TOTAL;
		private decimal m_OPENING_CASH;
		private decimal m_DIFFERENCE;
		private DateTime m_TIME_STAMP;
        private DateTime m_CLOSE_DATE;
		#endregion
		#region Public Properties
		public int LOCATION_ID
		{
			set
			{
				m_LOCATION_ID = value ;
			}
			get
			{
				return m_LOCATION_ID;
			}
		}
		public int SHIFT_ID
		{
			set
			{
				m_SHIFT_ID = value ;
			}
			get
			{
				return m_SHIFT_ID;
			}
		}
		public int USER_ID
		{
			set
			{
				m_USER_ID = value ;
			}
			get
			{
				return m_USER_ID;
			}
		}
		public int CURRENCY_5000
		{
			set
			{
				m_CURRENCY_5000 = value ;
			}
			get
			{
				return m_CURRENCY_5000;
			}
		}
		public int CURRENCY_1000
		{
			set
			{
				m_CURRENCY_1000 = value ;
			}
			get
			{
				return m_CURRENCY_1000;
			}
		}
		public int CURRENCY_500
		{
			set
			{
				m_CURRENCY_500 = value ;
			}
			get
			{
				return m_CURRENCY_500;
			}
		}
		public int CURRENCY_1
		{
			set
			{
				m_CURRENCY_1 = value ;
			}
			get
			{
				return m_CURRENCY_1;
			}
		}
		public int CURRENCY_100
		{
			set
			{
				m_CURRENCY_100 = value ;
			}
			get
			{
				return m_CURRENCY_100;
			}
		}
		public int CURRENCY_50
		{
			set
			{
				m_CURRENCY_50 = value ;
			}
			get
			{
				return m_CURRENCY_50;
			}
		}
		public int CURRENCY_20
		{
			set
			{
				m_CURRENCY_20 = value ;
			}
			get
			{
				return m_CURRENCY_20;
			}
		}
		public int CURRENCY_10
		{
			set
			{
				m_CURRENCY_10 = value ;
			}
			get
			{
				return m_CURRENCY_10;
			}
		}
		public int CURRENCY_5
		{
			set
			{
				m_CURRENCY_5 = value ;
			}
			get
			{
				return m_CURRENCY_5;
			}
		}
		public int CURRENCY_2
		{
			set
			{
				m_CURRENCY_2 = value ;
			}
			get
			{
				return m_CURRENCY_2;
			}
		}
		public decimal  CASH_TOTAL
		{
			set
			{
				m_CASH_TOTAL = value ;
			}
			get
			{
				return m_CASH_TOTAL;
			}
		}
		public decimal  OPENING_CASH
		{
			set
			{
				m_OPENING_CASH = value ;
			}
			get
			{
				return m_OPENING_CASH;
			}
		}
		public decimal  DIFFERENCE
		{
			set
			{
				m_DIFFERENCE = value ;
			}
			get
			{
				return m_DIFFERENCE;
			}
		}
		public DateTime TIME_STAMP
		{
			set
			{
				m_TIME_STAMP = value ;
			}
			get
			{
				return m_TIME_STAMP;
			}
		}
        public DateTime CLOSE_DATE
        {
            set
            {
                m_CLOSE_DATE = value;
            }
            get
            {
                return m_CLOSE_DATE;
            }
        }


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
		public InsertShiftClose()
		{
		}
		#endregion
		#region public Methods
		public bool  ExecuteQuery()
		{
			try
			{
			    IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				cmd.CommandType =  CommandType.StoredProcedure;
				cmd.CommandText = sp_Name;
				cmd.Connection =   m_connection;
				if(m_transaction!=null)
				{
					cmd.Transaction = m_transaction;
				}
				GetParameterCollection(ref cmd);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch(Exception e)
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
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				IDataReader dr = command.ExecuteReader();
				return dr;
			}
			catch(Exception exp)
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
				if(m_transaction!=null)
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
				command.CommandText = sp_Name;
				command.Connection = m_connection;
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
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
			if(dr.Read())
			{
				m_LOCATION_ID= Convert.ToInt32(dr["LOCATION_ID"]);
				m_SHIFT_ID= Convert.ToInt32(dr["SHIFT_ID"]);
				m_USER_ID= Convert.ToInt32(dr["USER_ID"]);
				m_CURRENCY_5000= Convert.ToInt32(dr["CURRENCY_5000"]);
				m_CURRENCY_1000= Convert.ToInt32(dr["CURRENCY_1000"]);
				m_CURRENCY_500= Convert.ToInt32(dr["CURRENCY_500"]);
				m_CURRENCY_1= Convert.ToInt32(dr["CURRENCY_1"]);
				m_CURRENCY_100= Convert.ToInt32(dr["CURRENCY_100"]);
				m_CURRENCY_50= Convert.ToInt32(dr["CURRENCY_50"]);
				m_CURRENCY_20= Convert.ToInt32(dr["CURRENCY_20"]);
				m_CURRENCY_10= Convert.ToInt32(dr["CURRENCY_10"]);
				m_CURRENCY_5= Convert.ToInt32(dr["CURRENCY_5"]);
				m_CURRENCY_2= Convert.ToInt32(dr["CURRENCY_2"]);
				m_CASH_TOTAL= Convert.ToDecimal(dr["CASH_TOTAL"]);
				m_OPENING_CASH= Convert.ToDecimal(dr["OPENING_CASH"]);
				m_DIFFERENCE= Convert.ToDecimal(dr["DIFFERENCE"]);
				m_TIME_STAMP= Convert.ToDateTime(dr["TIME_STAMP"]);
                m_CLOSE_DATE = Convert.ToDateTime(dr["CLOSE_DATE"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@LOCATION_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_LOCATION_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_LOCATION_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SHIFT_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_SHIFT_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SHIFT_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@USER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_USER_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_USER_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CURRENCY_5000" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_CURRENCY_5000==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CURRENCY_5000;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CURRENCY_1000" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_CURRENCY_1000==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CURRENCY_1000;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CURRENCY_500" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_CURRENCY_500==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CURRENCY_500;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CURRENCY_1" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_CURRENCY_1==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CURRENCY_1;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CURRENCY_100" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_CURRENCY_100==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CURRENCY_100;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CURRENCY_50" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_CURRENCY_50==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CURRENCY_50;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CURRENCY_20" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_CURRENCY_20==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CURRENCY_20;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CURRENCY_10" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_CURRENCY_10==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CURRENCY_10;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CURRENCY_5" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_CURRENCY_5==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CURRENCY_5;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CURRENCY_2" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_CURRENCY_2==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CURRENCY_2;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CASH_TOTAL" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
			if(m_CASH_TOTAL==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CASH_TOTAL;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@OPENING_CASH" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
			if(m_OPENING_CASH==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_OPENING_CASH;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DIFFERENCE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
			if(m_DIFFERENCE==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DIFFERENCE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@TIME_STAMP" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_TIME_STAMP==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_TIME_STAMP;
			}
			pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CLOSE_DATE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_CLOSE_DATE == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CLOSE_DATE;
            }
            pparams.Add(parameter);


		}
		#endregion
	}
}
