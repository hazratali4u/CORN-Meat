using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CORNCommon.Classes;

namespace CornPointHelper
{
    public static class DashboardClass
    {
        public static string GetConnectionString()
        {
            try
            {
                string mConnectionString = string.Empty;
                string DatabseAuthentication = Cryptography.Decrypt(System.Configuration.ConfigurationManager.AppSettings["DBAuthentication"].ToString(), "b0tin@74");
                if (DatabseAuthentication.Equals("Windows")) // windows method
                {
                    mConnectionString = "Integrated Security=SSPI;Persist Security Info=False;";
                }
                else // MSDE mode
                {
                    mConnectionString = " Persist Security Info=False; ";
                    mConnectionString += " User ID=" + Cryptography.Decrypt(System.Configuration.ConfigurationManager.AppSettings["DBUser"].ToString(), "b0tin@74") + ";Password=" + Cryptography.Decrypt(System.Configuration.ConfigurationManager.AppSettings["DBPassword"].ToString(), "b0tin@74") + ";";
                }
                //common settings
                mConnectionString += " Initial Catalog = " + Cryptography.Decrypt(System.Configuration.ConfigurationManager.AppSettings["DBName"].ToString(), "b0tin@74") + "; ";
                mConnectionString += " Data Source= " + Cryptography.Decrypt(System.Configuration.ConfigurationManager.AppSettings["Server"].ToString(), "b0tin@74") + " ";

                return mConnectionString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int SaveDashboardinMenu(Int32 intMenuCode, byte[] xmlByteArray)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@intMenuCode", intMenuCode));
                parameters.Add(new SqlParameter("@xmlByteArray", xmlByteArray));
                return SqlHelper.ExecuteNonQuery(GetConnectionString(), "procSaveDashboardinMenu", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet GetDashboardMenuInfo(Int32 intMenuCode)
        {
            try
            {
                DataSet dsDashboard;
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@MenuCode", intMenuCode));
                dsDashboard = (DataSet)SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "procMenuDashboard", parameters.ToArray());

                return dsDashboard;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet GetDashboardDataSet(String strProcName, DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                DataSet dsDashboard;
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@FinancialYearCode", 1));
                parameters.Add(new SqlParameter("@dtFrom", dtFrom.Date));
                parameters.Add(new SqlParameter("@dtTo", dtTo.Date));
                parameters.Add(new SqlParameter("@EmployeeCode", 1));
                parameters.Add(new SqlParameter("@CompanyCode", 1));
                return dsDashboard = (DataSet)SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, strProcName, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}