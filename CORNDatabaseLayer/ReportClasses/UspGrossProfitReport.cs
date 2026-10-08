using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;


namespace CORNDatabaseLayer.Classes
{
	public class UspGrossProfitReport
	{
		#region Private Members
		private const string sp_Name = " UspGrossProfitReport" ;
		private IDbConnection _mConnection;
		private IDbTransaction _mTransaction;


		private int _mAccountCategoryId;
		private int _mDistributorId;
		private DateTime _mFromDate;
		private DateTime _mToDate;
		#endregion


		#region Public Properties
		public int AccountCategoryId
		{
			set
			{
				_mAccountCategoryId = value ;
			}
			get
			{
				return _mAccountCategoryId;
			}
		}


		public int DistributorId
		{
			set
			{
				_mDistributorId = value ;
			}
			get
			{
				return _mDistributorId;
			}
		}


		public DateTime FromDate
		{
			set
			{
				_mFromDate = value ;
			}
			get
			{
				return _mFromDate;
			}
		}


		public DateTime ToDate
		{
			set
			{
				_mToDate = value ;
			}
			get
			{
				return _mToDate;
			}
		}




		public IDbConnection  Connection
		{
			set
			{
				_mConnection = value;
			}
			get
			{
				return _mConnection;
			}
		}
		public IDbTransaction  Transaction
		{
			set
			{
				_mTransaction = value;
			}
			get
			{
				return _mTransaction;
			}
		}
		#endregion


		#region Constructor
		public UspGrossProfitReport()
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
				cmd.CommandText = "UspGrossProfitReport";
				cmd.Connection =   _mConnection;
				if(_mTransaction!=null)
				{
					cmd.Transaction = _mTransaction;
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
				command.CommandText = "UspGrossProfitReport";
				command.Connection = _mConnection;
				if(_mTransaction!=null)
				{
					command.Transaction = _mTransaction;
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


		public DataSet ExecuteTable()
		{
			try
			{
				IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = "UspGrossProfitReport";
				command.Connection = _mConnection;
				if(_mTransaction!=null)
				{
					command.Transaction = _mTransaction;
				}
				GetParameterCollection(ref command);
				IDbDataAdapter da = ProviderFactory.GetAdapter(EnumProviders.SQLClient);
				da.SelectCommand = command;
				DataSet ds = new DataSet();
				da.Fill(ds);
				return ds;
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
				command.CommandText = "UspGrossProfitReport";
				command.Connection = _mConnection;
				if(_mTransaction!=null)
				{
					command.Transaction = _mTransaction;
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
					_mAccountCategoryId= Convert.ToInt32(dr["ACCOUNT_CATEGORY_ID"]);
					_mDistributorId= Convert.ToInt32(dr["DISTRIBUTOR_ID"]);
					_mFromDate= Convert.ToDateTime(dr["FROM_DATE"]);
					_mToDate= Convert.ToDateTime(dr["TO_DATE"]);
				}
			}


		    public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@ACCOUNT_CATEGORY_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(_mAccountCategoryId==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = _mAccountCategoryId;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DISTRIBUTOR_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(_mDistributorId==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = _mDistributorId;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@FROM_DATE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(_mFromDate==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = _mFromDate;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@TO_DATE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(_mToDate==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = _mToDate;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
