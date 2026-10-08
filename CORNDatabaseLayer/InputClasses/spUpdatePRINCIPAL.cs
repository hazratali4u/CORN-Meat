using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;

namespace CORNDatabaseLayer.Classes
{
	public class spUpdatePRINCIPAL
	{
		#region Private Members
		private string sp_Name = " spUpdatePRINCIPAL" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;


		private int m_SKU_HIE_ID;
		private int m_COMPANY_ID;
		private int m_PARENT_SKU_HIE_ID;
		private int m_SKU_HIE_TYPE_ID;
		private DateTime m_TIME_STAMP;
		private DateTime m_LASTUPDATE_DATE;
		private bool m_IS_ACTIVE;
		private bool m_IS_MANUALDISCOUNT;
		private string m_SKU_HIE_CODE;
		private string m_SKU_HIE_NAME;
		private string m_IP_ADDRESS;
		private string m_ADDRESS;
        private string m_CONTACT_PERSON;
        private string m_EMAIL;
        private string m_FAX;
        private string m_PHONE;
		#endregion


		#region Public Properties
		public int SKU_HIE_ID
		{
			set
			{
				m_SKU_HIE_ID = value ;
			}
			get
			{
				return m_SKU_HIE_ID;
			}
		}


		public int COMPANY_ID
		{
			set
			{
				m_COMPANY_ID = value ;
			}
			get
			{
				return m_COMPANY_ID;
			}
		}


		public int PARENT_SKU_HIE_ID
		{
			set
			{
				m_PARENT_SKU_HIE_ID = value ;
			}
			get
			{
				return m_PARENT_SKU_HIE_ID;
			}
		}


		public int SKU_HIE_TYPE_ID
		{
			set
			{
				m_SKU_HIE_TYPE_ID = value ;
			}
			get
			{
				return m_SKU_HIE_TYPE_ID;
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


		public DateTime LASTUPDATE_DATE
		{
			set
			{
				m_LASTUPDATE_DATE = value ;
			}
			get
			{
				return m_LASTUPDATE_DATE;
			}
		}


		public bool IS_ACTIVE
		{
			set
			{
				m_IS_ACTIVE = value ;
			}
			get
			{
				return m_IS_ACTIVE;
			}
		}


		public bool IS_MANUALDISCOUNT
		{
			set
			{
				m_IS_MANUALDISCOUNT = value ;
			}
			get
			{
				return m_IS_MANUALDISCOUNT;
			}
		}


		public  string SKU_HIE_CODE
		{
			set
			{
				m_SKU_HIE_CODE = value ;
			}
			get
			{
				return m_SKU_HIE_CODE;
			}
		}


		public  string SKU_HIE_NAME
		{
			set
			{
				m_SKU_HIE_NAME = value ;
			}
			get
			{
				return m_SKU_HIE_NAME;
			}
		}


		public  string IP_ADDRESS
		{
			set
			{
				m_IP_ADDRESS = value ;
			}
			get
			{
				return m_IP_ADDRESS;
			}
		}


		public  string ADDRESS
		{
			set
			{
				m_ADDRESS = value ;
			}
			get
			{
				return m_ADDRESS;
			}
		}

        public string EMAIL
        {
            set
            {
                m_EMAIL = value;
            }
            get
            {
                return m_EMAIL;
            }
        }


        public string FAX
        {
            set
            {
                m_FAX = value;
            }
            get
            {
                return m_FAX;
            }
        }
        public string CONTACT_PERSON
        {
            set
            {
                m_CONTACT_PERSON = value;
            }
            get
            {
                return m_CONTACT_PERSON;
            }
        }
        public string PHONE
        {
            set
            {
                m_PHONE = value;
            }
            get
            {
                return m_PHONE;
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
		public spUpdatePRINCIPAL()
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
				cmd.CommandText = "spUpdatePRINCIPAL";
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
				command.CommandText = "spUpdatePRINCIPAL";
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
				command.CommandText = "spUpdatePRINCIPAL";
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
				command.CommandText = "spUpdatePRINCIPAL";
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
					m_SKU_HIE_ID= Convert.ToInt32(dr["SKU_HIE_ID"]);
					m_COMPANY_ID= Convert.ToInt32(dr["COMPANY_ID"]);
					m_PARENT_SKU_HIE_ID= Convert.ToInt32(dr["PARENT_SKU_HIE_ID"]);
					m_SKU_HIE_TYPE_ID= Convert.ToInt32(dr["SKU_HIE_TYPE_ID"]);
					m_TIME_STAMP= Convert.ToDateTime(dr["TIME_STAMP"]);
					m_LASTUPDATE_DATE= Convert.ToDateTime(dr["LASTUPDATE_DATE"]);
					m_IS_ACTIVE=Convert.ToBoolean(dr["IS_ACTIVE"]);
					m_IS_MANUALDISCOUNT=Convert.ToBoolean(dr["IS_MANUALDISCOUNT"]);
					m_SKU_HIE_CODE= Convert.ToString(dr["SKU_HIE_CODE"]);
					m_SKU_HIE_NAME= Convert.ToString(dr["SKU_HIE_NAME"]);
					m_IP_ADDRESS= Convert.ToString(dr["IP_ADDRESS"]);
					m_ADDRESS= Convert.ToString(dr["ADDRESS"]);
                    m_EMAIL = Convert.ToString(dr["EMAIL"]);
                    m_FAX = Convert.ToString(dr["FAX"]);
                    m_CONTACT_PERSON = Convert.ToString(dr["CONTACT_PERSON"]);
                    m_PHONE = Convert.ToString(dr["PHONE"]);
				}
			}


		    public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SKU_HIE_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_SKU_HIE_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SKU_HIE_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@COMPANY_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_COMPANY_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_COMPANY_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PARENT_SKU_HIE_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_PARENT_SKU_HIE_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PARENT_SKU_HIE_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SKU_HIE_TYPE_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_SKU_HIE_TYPE_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SKU_HIE_TYPE_ID;
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
			parameter.ParameterName = "@LASTUPDATE_DATE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_LASTUPDATE_DATE==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_LASTUPDATE_DATE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@IS_ACTIVE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_IS_ACTIVE;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@IS_MANUALDISCOUNT" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_IS_MANUALDISCOUNT;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SKU_HIE_CODE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_SKU_HIE_CODE== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SKU_HIE_CODE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SKU_HIE_NAME" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_SKU_HIE_NAME== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SKU_HIE_NAME;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@IP_ADDRESS" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_IP_ADDRESS== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_IP_ADDRESS;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@ADDRESS" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_ADDRESS== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_ADDRESS;
			}
			pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EMAIL";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EMAIL == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EMAIL;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@FAX";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_FAX == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_FAX;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CONTACT_PERSON";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_CONTACT_PERSON == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CONTACT_PERSON;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PHONE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_PHONE == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PHONE;
            }
            pparams.Add(parameter);


		}
		#endregion
	}
}
