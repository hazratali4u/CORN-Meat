using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CORNDataAccessLayer.Classes;
using CORNCommon.Classes;

namespace CORNDatabaseLayer.Classes
{
   public class RptSelectProductSale_Detail
    {
        #region Private Members
        private string sp_Name = "RptSelectProductSale_Detail";
        private IDbConnection _mConnection;
        private IDbTransaction _mTransaction;
        private int _mType;
        private int _mDistributorId;
        private int _mPrincipalId;
        private DateTime _mFromDate;
        private DateTime _mToDate;
        #endregion
        #region Public Properties
        public int TYPE
        {
            set
            {
                _mType = value;
            }
            get
            { 
                return _mType;
            }
        }
        public int DISTRIBUTOR_ID
        {
            set
            {
                _mDistributorId = value;
            }
            get
            {
                return _mDistributorId;
            }
        }
        public int PRINCIPAL_ID
        {
            set
            {
                _mPrincipalId = value;
            }
            get
            {
                return _mPrincipalId;
            }
        }
        public DateTime FROM_DATE
        {
            set
            {
                _mFromDate = value;
            }
            get
            {
                return _mFromDate;
            }
        }
        public DateTime TO_DATE
        {
            set
            {
                _mToDate = value;
            }
            get
            {
                return _mToDate;
            }
        }


        public IDbConnection Connection
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
        public IDbTransaction Transaction
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
        public RptSelectProductSale_Detail()
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
                cmd.Connection = _mConnection;
                if (_mTransaction != null)
                {
                    cmd.Transaction = _mTransaction;
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
                command.Connection = _mConnection;
                if (_mTransaction != null)
                {
                    command.Transaction = _mTransaction;
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
                command.Connection = _mConnection;
                if (_mTransaction != null)
                {
                    command.Transaction = _mTransaction;
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
                command.Connection = _mConnection;
                if (_mTransaction != null)
                {
                    command.Transaction = _mTransaction;
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
                _mType = Convert.ToInt32(dr["TYPE"]);
                _mDistributorId = Convert.ToInt32(dr["DISTRIBUTOR_ID"]);
                _mPrincipalId = Convert.ToInt32(dr["PRINCIPAL_ID"]);
                _mFromDate = Convert.ToDateTime(dr["FROM_DATE"]);
                _mToDate = Convert.ToDateTime(dr["TO_DATE"]);
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TYPE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (_mType == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = _mType;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DISTRIBUTOR_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (_mDistributorId == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = _mDistributorId;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PRINCIPAL_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (_mPrincipalId == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = _mPrincipalId;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@FROM_DATE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (_mFromDate == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = _mFromDate;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TO_DATE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (_mToDate == Constants.DateNullValue)
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
