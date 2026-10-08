using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;


namespace CORNDatabaseLayer.Classes
{
	public class spInsertTableDefination
	{
		#region Private Members
		private string sp_Name = "spInsertTableDefination" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_p_USER_ID;
		private DateTime m_P_TIME_STAMP;
		private string m_p_TableDefination_No;
		private string m_p_TableDefination_Description;
		private string m_p_TableDefination_Capacity;
		private string m_p_TableDefination_Abbrivation;
        private int m_distributorId;
		#endregion
		#region Public Properties
        public int distributorId
        {
            set
            {
                m_distributorId = value;
            }
            get
            {
                return m_distributorId;
            }
        }
		public int p_USER_ID
		{
			set
			{
				m_p_USER_ID = value ;
			}
			get
			{
				return m_p_USER_ID;
			}
		}
		public DateTime P_TIME_STAMP
		{
			set
			{
				m_P_TIME_STAMP = value ;
			}
			get
			{
				return m_P_TIME_STAMP;
			}
		}
		public  string p_TableDefination_No
		{
			set
			{
				m_p_TableDefination_No = value ;
			}
			get
			{
				return m_p_TableDefination_No;
			}
		}
		public  string p_TableDefination_Description
		{
			set
			{
				m_p_TableDefination_Description = value ;
			}
			get
			{
				return m_p_TableDefination_Description;
			}
		}
		public  string p_TableDefination_Capacity
		{
			set
			{
				m_p_TableDefination_Capacity = value ;
			}
			get
			{
				return m_p_TableDefination_Capacity;
			}
		}
		public  string p_TableDefination_Abbrivation
		{
			set
			{
				m_p_TableDefination_Abbrivation = value ;
			}
			get
			{
				return m_p_TableDefination_Abbrivation;
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
		public spInsertTableDefination()
		{
            m_distributorId = Constants.IntNullValue;

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
				m_p_USER_ID= Convert.ToInt32(dr["p_USER_ID"]);
				m_P_TIME_STAMP= Convert.ToDateTime(dr["P_TIME_STAMP"]);
				m_p_TableDefination_No= Convert.ToString(dr["p_TableDefination_No"]);
				m_p_TableDefination_Description= Convert.ToString(dr["p_TableDefination_Description"]);
				m_p_TableDefination_Capacity= Convert.ToString(dr["p_TableDefination_Capacity"]);
				m_p_TableDefination_Abbrivation= Convert.ToString(dr["p_TableDefination_Abbrivation"]);
                m_distributorId = Convert.ToInt32(dr["distributorId"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@distributorId";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_distributorId == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_distributorId;
            }
            pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@p_USER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_p_USER_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_p_USER_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@P_TIME_STAMP" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_P_TIME_STAMP==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_P_TIME_STAMP;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@p_TableDefination_No" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_p_TableDefination_No== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_p_TableDefination_No;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@p_TableDefination_Description" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_p_TableDefination_Description== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_p_TableDefination_Description;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@p_TableDefination_Capacity" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_p_TableDefination_Capacity== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_p_TableDefination_Capacity;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@p_TableDefination_Abbrivation" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_p_TableDefination_Abbrivation== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_p_TableDefination_Abbrivation;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
