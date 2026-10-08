using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;
using System;
using System.Data;


namespace CORNDatabaseLayer.Classes
{
    public class spInsertCUSTOMER_FEEDBACK
    {
        #region Private Members
        private string sp_Name = "spInsertCUSTOMER_FEEDBACK";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private int m_LOCATION_ID;
        private int m_SERVICE_RATE;
        private int m_FOOD_RATE;
        private int m_ENVIRONMENT_RATE;
        private int m_OVERALL_RATE;
        private int m_HEAR_MEDIUM;
        private int m_FEEDBACK_ID;
        private DateTime m_TIME_STAMP;
        private string m_COMMENTS;
        private string m_OTHER_MEDIUM;
        private string m_NAME;
        private string m_CONTACT_NO;
        private string m_EMAIL;
        private string m_ADDRESS;
        #endregion
        #region Public Properties
        public int LOCATION_ID
        {
            set
            {
                m_LOCATION_ID = value;
            }
            get
            {
                return m_LOCATION_ID;
            }
        }
        public int SERVICE_RATE
        {
            set
            {
                m_SERVICE_RATE = value;
            }
            get
            {
                return m_SERVICE_RATE;
            }
        }
        public int FOOD_RATE
        {
            set
            {
                m_FOOD_RATE = value;
            }
            get
            {
                return m_FOOD_RATE;
            }
        }
        public int ENVIRONMENT_RATE
        {
            set
            {
                m_ENVIRONMENT_RATE = value;
            }
            get
            {
                return m_ENVIRONMENT_RATE;
            }
        }
        public int OVERALL_RATE
        {
            set
            {
                m_OVERALL_RATE = value;
            }
            get
            {
                return m_OVERALL_RATE;
            }
        }
        public int HEAR_MEDIUM
        {
            set
            {
                m_HEAR_MEDIUM = value;
            }
            get
            {
                return m_HEAR_MEDIUM;
            }
        }
        public int FEEDBACK_ID
        {
            get
            {
                return m_FEEDBACK_ID;
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
        public string COMMENTS
        {
            set
            {
                m_COMMENTS = value;
            }
            get
            {
                return m_COMMENTS;
            }
        }
        public string OTHER_MEDIUM
        {
            set
            {
                m_OTHER_MEDIUM = value;
            }
            get
            {
                return m_OTHER_MEDIUM;
            }
        }
        public string NAME
        {
            set
            {
                m_NAME = value;
            }
            get
            {
                return m_NAME;
            }
        }
        public string CONTACT_NO
        {
            set
            {
                m_CONTACT_NO = value;
            }
            get
            {
                return m_CONTACT_NO;
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
        public string ADDRESS
        {
            set
            {
                m_ADDRESS = value;
            }
            get
            {
                return m_ADDRESS;
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
        public spInsertCUSTOMER_FEEDBACK()
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
                m_FEEDBACK_ID = (int)((IDataParameter)(cmd.Parameters["@FEEDBACK_ID"])).Value;
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
                m_LOCATION_ID = Convert.ToInt32(dr["LOCATION_ID"]);
                m_SERVICE_RATE = Convert.ToInt32(dr["SERVICE_RATE"]);
                m_FOOD_RATE = Convert.ToInt32(dr["FOOD_RATE"]);
                m_ENVIRONMENT_RATE = Convert.ToInt32(dr["ENVIRONMENT_RATE"]);
                m_OVERALL_RATE = Convert.ToInt32(dr["OVERALL_RATE"]);
                m_HEAR_MEDIUM = Convert.ToInt32(dr["HEAR_MEDIUM"]);
                m_FEEDBACK_ID = Convert.ToInt32(dr["FEEDBACK_ID"]);
                m_TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                m_COMMENTS = Convert.ToString(dr["COMMENTS"]);
                m_OTHER_MEDIUM = Convert.ToString(dr["OTHER_MEDIUM"]);
                m_NAME = Convert.ToString(dr["NAME"]);
                m_CONTACT_NO = Convert.ToString(dr["CONTACT_NO"]);
                m_EMAIL = Convert.ToString(dr["EMAIL"]);
                m_ADDRESS = Convert.ToString(dr["ADDRESS"]);
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@LOCATION_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_LOCATION_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_LOCATION_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SERVICE_RATE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_SERVICE_RATE == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SERVICE_RATE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@FOOD_RATE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_FOOD_RATE == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_FOOD_RATE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@ENVIRONMENT_RATE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_ENVIRONMENT_RATE == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_ENVIRONMENT_RATE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@OVERALL_RATE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_OVERALL_RATE == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_OVERALL_RATE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@HEAR_MEDIUM";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_HEAR_MEDIUM == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_HEAR_MEDIUM;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@FEEDBACK_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            parameter.Direction = ParameterDirection.Output;
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
            parameter.ParameterName = "@COMMENTS";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.NVarChar);
            if (m_COMMENTS == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_COMMENTS;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@OTHER_MEDIUM";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.NVarChar);
            if (m_OTHER_MEDIUM == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_OTHER_MEDIUM;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@NAME";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.NVarChar);
            if (m_NAME == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_NAME;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CONTACT_NO";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.NVarChar);
            if (m_CONTACT_NO == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CONTACT_NO;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EMAIL";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.NVarChar);
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
            parameter.ParameterName = "@ADDRESS";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.NVarChar);
            if (m_ADDRESS == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_ADDRESS;
            }
            pparams.Add(parameter);
        }
        #endregion
    }
}
