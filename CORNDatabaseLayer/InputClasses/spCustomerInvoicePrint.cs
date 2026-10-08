using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;

namespace CORNDatabaseLayer.Classes
{
    public class spCustomerInvoicePrint
    {
        #region Private Members
		private string sp_Name = "spCustomerInvoicePrint" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;

		private int m_Distributor_id;
		private int m_Principal_id;
        private int m_Type;
        private int m_Sale_id;
		private DateTime m_From_date;
		private DateTime m_To_Date;
     
		#endregion


		#region Public Properties
		public int Distributor_id
		{
			set
			{
				m_Distributor_id = value ;
			}
			get
			{
				return m_Distributor_id;
			}
		}
		public int Principal_id
		{
			set
			{
				m_Principal_id = value ;
			}
			get
			{
				return m_Principal_id;
			}
		}
        public int Type
        {
            set
            {
                m_Type = value;
            }
            get
            {
                return m_Type;
            }
        }
        public int Sale_id
        {
            set
            {
                m_Sale_id = value;
            }
            get
            {
                return m_Sale_id;
            }
        }
        public DateTime From_date
		{
			set
			{
				m_From_date = value ;
			}
			get
			{
				return m_From_date;
			}
		}
		public DateTime To_Date
		{
			set
			{
				m_To_Date = value ;
			}
			get
			{
				return m_To_Date;
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
        public spCustomerInvoicePrint()
        {
            m_Distributor_id = Constants.IntNullValue;
            m_Principal_id = Constants.IntNullValue;
            m_Type = Constants.IntNullValue;
            m_Sale_id = Constants.IntNullValue;
            m_From_date = Constants.DateNullValue;
            m_To_Date = Constants.DateNullValue;
       
        }
		#endregion

		#region public Methods
		public bool  ExecuteQuery()
		{
			try
			{
			    IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				cmd.CommandType =  CommandType.StoredProcedure;
                cmd.CommandText = "spCustomerInvoicePrint";
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
                command.CommandText = "spCustomerInvoicePrint";
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
                command.CommandText = "spCustomerInvoicePrint";
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
                command.CommandText = "spCustomerInvoicePrint";
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
					m_Distributor_id= Convert.ToInt32(dr["Distributor_id"]);
					m_Principal_id= Convert.ToInt32(dr["Principal_id"]);
                    m_Type = Convert.ToInt32(dr["type"]);
                    m_Sale_id = Convert.ToInt32(dr["Sale_INV_ID"]);
					m_From_date= Convert.ToDateTime(dr["From_date"]);
					m_To_Date= Convert.ToDateTime(dr["To_Date"]);
                 
				}
			}


		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@Distributor_id" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_Distributor_id==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Distributor_id;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@Principal_id" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_Principal_id==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Principal_id;
			}
			pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@type";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_Type == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Type;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Sale_INV_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_Sale_id == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Sale_id;
            }
            pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@From_date" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_From_date==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_From_date;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@To_Date" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_To_Date==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_To_Date;
			}
			pparams.Add(parameter);


		}
		#endregion
    }
}
