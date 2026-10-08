using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CORNDataAccessLayer.Classes;
using CORNCommon.Classes;


namespace CORNDatabaseLayer.Classes
{
    public class spInsertEMPLOYEE_SALARY_DEDUCTION
    {
        #region Private Members
        private string sp_Name = "spInsertEMPLOYEE_SALARY_DEDUCTION";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private int m_DEDUCTION_DETAIL_ID;
        private long m_SALARY_ID;
        private int m_EMPLOYEE_ID;
        private int m_DEDUCTION_MASTER_ID;
        private decimal m_DEDUCTION_AMOUNT;
        private string m_DEDUCTION_DESCRIPTION;
        #endregion
        #region Public Properties
        public int DEDUCTION_DETAIL_ID
        {
            set
            {
                m_DEDUCTION_DETAIL_ID = value;
            }
            get
            {
                return m_DEDUCTION_DETAIL_ID;
            }
        }
        public long SALARY_ID
        {
            set
            {
                m_SALARY_ID = value;
            }
            get
            {
                return m_SALARY_ID;
            }
        }
        public int EMPLOYEE_ID
        {
            set
            {
                m_EMPLOYEE_ID = value;
            }
            get
            {
                return m_EMPLOYEE_ID;
            }
        }
        public int DEDUCTION_MASTER_ID
        {
            set
            {
                m_DEDUCTION_MASTER_ID = value;
            }
            get
            {
                return m_DEDUCTION_MASTER_ID;
            }
        }
        public decimal DEDUCTION_AMOUNT
        {
            set
            {
                m_DEDUCTION_AMOUNT = value;
            }
            get
            {
                return m_DEDUCTION_AMOUNT;
            }
        }
        public string DEDUCTION_DESCRIPTION
        {
            set
            {
                m_DEDUCTION_DESCRIPTION = value;
            }
            get
            {
                return m_DEDUCTION_DESCRIPTION;
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
        public spInsertEMPLOYEE_SALARY_DEDUCTION()
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
                m_DEDUCTION_DETAIL_ID = Convert.ToInt32(dr["DEDUCTION_DETAIL_ID"]);
                m_SALARY_ID = Convert.ToInt64(dr["SALARY_ID"]);
                m_EMPLOYEE_ID = Convert.ToInt32(dr["EMPLOYEE_ID"]);
                m_DEDUCTION_MASTER_ID = Convert.ToInt32(dr["DEDUCTION_MASTER_ID"]);
                m_DEDUCTION_AMOUNT = Convert.ToDecimal(dr["DEDUCTION_AMOUNT"]);
                m_DEDUCTION_DESCRIPTION = Convert.ToString(dr["DEDUCTION_DESCRIPTION"]);
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DEDUCTION_DETAIL_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_DEDUCTION_DETAIL_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DEDUCTION_DETAIL_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SALARY_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_SALARY_ID == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SALARY_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EMPLOYEE_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_EMPLOYEE_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EMPLOYEE_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DEDUCTION_MASTER_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_DEDUCTION_MASTER_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DEDUCTION_MASTER_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DEDUCTION_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_DEDUCTION_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DEDUCTION_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DEDUCTION_DESCRIPTION";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_DEDUCTION_DESCRIPTION == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DEDUCTION_DESCRIPTION;
            }
            pparams.Add(parameter);


        }
        #endregion
    }
}
