using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;

namespace CORNDatabaseLayer.Classes
{
	public class spInsertDirectSALE_INVOICE_MASTER
	{
		#region Private Members
		private string sp_Name = "spInsertDirectSALE_INVOICE_MASTER" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private byte m_POSTING;
		private short m_EmpDiscountType;
		private int m_PAYMENT_MODE_ID;
		private int m_DISCOUNT_TYPE;
		private int m_CUSTOMER_TYPE_ID;
		private int m_TABLE_ID;
		private int m_USER_ID;
		private int m_DISTRIBUTOR_ID;
		private int m_orderBookerId;
		private int m_EMC_UserID;
		private int m_Manager_UserID;
		private DateTime m_DOCUMENT_DATE;
		private bool m_IS_HOLD;
		private bool m_IS_GST;
		private decimal m_AMOUNTDUE;
		private decimal m_DISCOUNT;
		private decimal m_GST;
		private decimal m_PAIDIN;
		private decimal m_BALANCE;
		private decimal m_SERVICE_CHARGES;
		private decimal m_POINTS;
		private decimal m_PURCHASING;
		private long m_CUSTOMER_ID;
        private long m_SALE_INVOICE_ID;
        private string m_APPROVED_BY;
		private string m_coverTable;
		private string m_TAKEAWAY_CUSTOMER;
		private string m_ORDER_NO;
		private string m_ManagerPWD;
		private string m_strCardNo;
        private string m_REMARKS;
        #endregion
        #region Public Properties
        public byte POSTING
		{
			set
			{
				m_POSTING = value ;
			}
			get
			{
				return m_POSTING;
			}
		}
		public short EmpDiscountType
		{
			set
			{
				m_EmpDiscountType = value ;
			}
			get
			{
				return m_EmpDiscountType;
			}
		}
		public int PAYMENT_MODE_ID
		{
			set
			{
				m_PAYMENT_MODE_ID = value ;
			}
			get
			{
				return m_PAYMENT_MODE_ID;
			}
		}
		public int DISCOUNT_TYPE
		{
			set
			{
				m_DISCOUNT_TYPE = value ;
			}
			get
			{
				return m_DISCOUNT_TYPE;
			}
		}
		public int CUSTOMER_TYPE_ID
		{
			set
			{
				m_CUSTOMER_TYPE_ID = value ;
			}
			get
			{
				return m_CUSTOMER_TYPE_ID;
			}
		}
		public int TABLE_ID
		{
			set
			{
				m_TABLE_ID = value ;
			}
			get
			{
				return m_TABLE_ID;
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
		public int DISTRIBUTOR_ID
		{
			set
			{
				m_DISTRIBUTOR_ID = value ;
			}
			get
			{
				return m_DISTRIBUTOR_ID;
			}
		}
		public int orderBookerId
		{
			set
			{
				m_orderBookerId = value ;
			}
			get
			{
				return m_orderBookerId;
			}
		}
		public int EMC_UserID
		{
			set
			{
				m_EMC_UserID = value ;
			}
			get
			{
				return m_EMC_UserID;
			}
		}
		public int Manager_UserID
		{
			set
			{
				m_Manager_UserID = value ;
			}
			get
			{
				return m_Manager_UserID;
			}
		}
		public DateTime DOCUMENT_DATE
		{
			set
			{
				m_DOCUMENT_DATE = value ;
			}
			get
			{
				return m_DOCUMENT_DATE;
			}
		}
		public bool IS_HOLD
		{
			set
			{
				m_IS_HOLD = value ;
			}
			get
			{
				return m_IS_HOLD;
			}
		}
		public bool IS_GST
		{
			set
			{
				m_IS_GST = value ;
			}
			get
			{
				return m_IS_GST;
			}
		}
		public decimal AMOUNTDUE
		{
			set
			{
				m_AMOUNTDUE = value ;
			}
			get
			{
				return m_AMOUNTDUE;
			}
		}
		public decimal DISCOUNT
		{
			set
			{
				m_DISCOUNT = value ;
			}
			get
			{
				return m_DISCOUNT;
			}
		}
		public decimal GST
		{
			set
			{
				m_GST = value ;
			}
			get
			{
				return m_GST;
			}
		}
		public decimal PAIDIN
		{
			set
			{
				m_PAIDIN = value ;
			}
			get
			{
				return m_PAIDIN;
			}
		}
		public decimal BALANCE
		{
			set
			{
				m_BALANCE = value ;
			}
			get
			{
				return m_BALANCE;
			}
		}
		public decimal SERVICE_CHARGES
		{
			set
			{
				m_SERVICE_CHARGES = value ;
			}
			get
			{
				return m_SERVICE_CHARGES;
			}
		}
		public decimal POINTS
		{
			set
			{
				m_POINTS = value ;
			}
			get
			{
				return m_POINTS;
			}
		}
		public decimal PURCHASING
		{
			set
			{
				m_PURCHASING = value ;
			}
			get
			{
				return m_PURCHASING;
			}
		}
        public long SALE_INVOICE_ID
        {
            get
            {
                return m_SALE_INVOICE_ID;
            }
        }
        public long CUSTOMER_ID
		{
			set
			{
				m_CUSTOMER_ID = value ;
			}
			get
			{
				return m_CUSTOMER_ID;
			}
		}
		public  string APPROVED_BY
		{
			set
			{
				m_APPROVED_BY = value ;
			}
			get
			{
				return m_APPROVED_BY;
			}
		}
		public  string coverTable
		{
			set
			{
				m_coverTable = value ;
			}
			get
			{
				return m_coverTable;
			}
		}
		public  string TAKEAWAY_CUSTOMER
		{
			set
			{
				m_TAKEAWAY_CUSTOMER = value ;
			}
			get
			{
				return m_TAKEAWAY_CUSTOMER;
			}
		}
		public  string ORDER_NO
		{
			set
			{
				m_ORDER_NO = value ;
			}
			get
			{
				return m_ORDER_NO;
			}
		}
		public string ManagerPWD
		{
			set
			{
				m_ManagerPWD = value ;
			}
			get
			{
				return m_ManagerPWD;
			}
		}
		public string strCardNo
		{
			set
			{
				m_strCardNo = value ;
			}
			get
			{
				return m_strCardNo;
			}
		}
        public string REMARKS
        {
            set { m_REMARKS = value; }
            get { return m_REMARKS; }
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
		public spInsertDirectSALE_INVOICE_MASTER()
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
                m_SALE_INVOICE_ID = (long)((IDataParameter)(cmd.Parameters["@SALE_INVOICE_ID"])).Value;
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
				m_POSTING= Convert.ToByte(dr["POSTING"]);
				m_EmpDiscountType= Convert.ToInt16(dr["EmpDiscountType"]);
				m_PAYMENT_MODE_ID= Convert.ToInt32(dr["PAYMENT_MODE_ID"]);
				m_DISCOUNT_TYPE= Convert.ToInt32(dr["DISCOUNT_TYPE"]);
				m_CUSTOMER_TYPE_ID= Convert.ToInt32(dr["CUSTOMER_TYPE_ID"]);
				m_TABLE_ID= Convert.ToInt32(dr["TABLE_ID"]);
				m_USER_ID= Convert.ToInt32(dr["USER_ID"]);
				m_DISTRIBUTOR_ID= Convert.ToInt32(dr["DISTRIBUTOR_ID"]);
				m_orderBookerId= Convert.ToInt32(dr["orderBookerId"]);
				m_EMC_UserID= Convert.ToInt32(dr["EMC_UserID"]);
				m_Manager_UserID= Convert.ToInt32(dr["Manager_UserID"]);
				m_DOCUMENT_DATE= Convert.ToDateTime(dr["DOCUMENT_DATE"]);
				m_IS_HOLD=Convert.ToBoolean(dr["IS_HOLD"]);
				m_IS_GST=Convert.ToBoolean(dr["IS_GST"]);
				m_AMOUNTDUE= Convert.ToDecimal(dr["AMOUNTDUE"]);
				m_DISCOUNT= Convert.ToDecimal(dr["DISCOUNT"]);
				m_GST= Convert.ToDecimal(dr["GST"]);
				m_PAIDIN= Convert.ToDecimal(dr["PAIDIN"]);
				m_BALANCE= Convert.ToDecimal(dr["BALANCE"]);
				m_SERVICE_CHARGES= Convert.ToDecimal(dr["SERVICE_CHARGES"]);
				m_POINTS= Convert.ToDecimal(dr["POINTS"]);
				m_PURCHASING= Convert.ToDecimal(dr["PURCHASING"]);
				m_CUSTOMER_ID=Convert.ToInt64(dr["CUSTOMER_ID"]);
				m_APPROVED_BY= Convert.ToString(dr["APPROVED_BY"]);
				m_coverTable= Convert.ToString(dr["coverTable"]);
				m_TAKEAWAY_CUSTOMER= Convert.ToString(dr["TAKEAWAY_CUSTOMER"]);
				m_ORDER_NO= Convert.ToString(dr["ORDER_NO"]);
				m_ManagerPWD= Convert.ToString(dr["ManagerPWD"]);
				m_strCardNo= Convert.ToString(dr["strCardNo"]);
			}
		}
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@POSTING";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.TinyInt);
            if (m_POSTING == Constants.ByteNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_POSTING;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SALE_INVOICE_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            parameter.Direction = ParameterDirection.Output;
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmpDiscountType";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.SmallInt);
            if (m_EmpDiscountType == Constants.ShortNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmpDiscountType;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PAYMENT_MODE_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_PAYMENT_MODE_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PAYMENT_MODE_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DISCOUNT_TYPE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_DISCOUNT_TYPE == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DISCOUNT_TYPE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CUSTOMER_TYPE_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_CUSTOMER_TYPE_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CUSTOMER_TYPE_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TABLE_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_TABLE_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TABLE_ID;
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
            parameter.ParameterName = "@orderBookerId";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_orderBookerId == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_orderBookerId;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EMC_UserID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_EMC_UserID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EMC_UserID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Manager_UserID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_Manager_UserID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Manager_UserID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DOCUMENT_DATE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_DOCUMENT_DATE == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DOCUMENT_DATE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IS_HOLD";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IS_HOLD;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IS_GST";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IS_GST;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@AMOUNTDUE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_AMOUNTDUE == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_AMOUNTDUE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DISCOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_DISCOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DISCOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@GST";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_GST == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_GST;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PAIDIN";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_PAIDIN == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PAIDIN;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@BALANCE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_BALANCE == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_BALANCE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SERVICE_CHARGES";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_SERVICE_CHARGES == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SERVICE_CHARGES;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@POINTS";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_POINTS == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_POINTS;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PURCHASING";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_PURCHASING == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PURCHASING;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CUSTOMER_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_CUSTOMER_ID == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CUSTOMER_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@APPROVED_BY";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_APPROVED_BY == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_APPROVED_BY;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@coverTable";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_coverTable == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_coverTable;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TAKEAWAY_CUSTOMER";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_TAKEAWAY_CUSTOMER == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TAKEAWAY_CUSTOMER;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@ORDER_NO";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_ORDER_NO == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_ORDER_NO;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@ManagerPWD";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.NVarChar);
            if (m_ManagerPWD == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_ManagerPWD;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@strCardNo";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.NVarChar);
            if (m_strCardNo == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_strCardNo;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@REMARKS";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.NVarChar);
            if (m_REMARKS == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_REMARKS;
            }
            pparams.Add(parameter);
        }
        #endregion
    }
}
