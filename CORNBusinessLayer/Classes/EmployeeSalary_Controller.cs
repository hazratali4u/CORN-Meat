using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CORNDataAccessLayer.Classes;
using CORNCommon.Classes;
using CORNDatabaseLayer.Classes;

namespace CORNBusinessLayer.Classes
{
    public class EmployeeSalary_Controller
    {
        public EmployeeSalary_Controller()
        {

        }

        #region Insert

        public bool Add_Salary(int p_Distributor_id, int p_Designation_id, int p_Employee_id, decimal p_Salary_Amount,
            DateTime p_Salary_Month, int p_UserId, DateTime p_Document_Date, DataTable dtAllowanceDetail, DataTable dtDeductionDetail)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
          try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertEMPLOYEE_SALARY mISom = new spInsertEMPLOYEE_SALARY();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Sale Order Master----------

               
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.DESIGNATION_ID = p_Designation_id;
                    mISom.EMPLOYEE_ID = p_Employee_id;
                    mISom.SALARY_AMOUNT = p_Salary_Amount;
                    mISom.SALARY_MONTH = p_Salary_Month;

                    mISom.DOCUMENT_DATE = p_Document_Date;
                    mISom.USER_ID = p_UserId;
                    mISom.TIME_STAMP = DateTime.Now;
                    mISom.LAST_UPDATE = System.DateTime.Now;
                    mISom.IS_ACTIVE =true;

                    mISom.ExecuteQuery();


                    //----------------Insert into sale order detail-------------
                    spInsertEMPLOYEE_SALARY_ALLOWANCE mSaleOrderDetail = new spInsertEMPLOYEE_SALARY_ALLOWANCE();
                    mSaleOrderDetail.Connection = mConnection;
                    mSaleOrderDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtAllowanceDetail.Rows)
                    {
                        
                        mSaleOrderDetail.SALARY_ID = mISom.SALARY_ID;
                        mSaleOrderDetail.EMPLOYEE_ID = p_Employee_id;
                        mSaleOrderDetail.ALLOWANCE_MASTER_ID = int.Parse(dr["ALLOWANCE_ID"].ToString());
                        mSaleOrderDetail.ALLOWANCE_DESCRIPTION = dr["ALLOWANCE_DESC"].ToString();
                        mSaleOrderDetail.ALLOWANCE_AMOUNT = decimal.Parse(dr["ALLOWANCE_AMOUNT"].ToString());
                      
                        mSaleOrderDetail.ExecuteQuery();

                    }
                    foreach (DataRow df in dtDeductionDetail.Rows)
                    {
                        //----------------Insert into sale order Promotion-------------
                        spInsertEMPLOYEE_SALARY_DEDUCTION mSaleOrderPromo = new spInsertEMPLOYEE_SALARY_DEDUCTION();
                        mSaleOrderPromo.Connection = mConnection;
                        mSaleOrderPromo.Transaction = mTransaction;

                        mSaleOrderPromo.SALARY_ID = mISom.SALARY_ID;
                        mSaleOrderPromo.EMPLOYEE_ID = p_Employee_id;
                        mSaleOrderPromo.DEDUCTION_MASTER_ID = int.Parse(df["DEDUCTION_ID"].ToString());
                        mSaleOrderPromo.DEDUCTION_DESCRIPTION = df["DEDUCTION_DESC"].ToString();
                        mSaleOrderPromo.DEDUCTION_AMOUNT = decimal.Parse(df["DEDUCTION_AMOUNT"].ToString());
                      
                        mSaleOrderPromo.ExecuteQuery();
                    }

                    mTransaction.Commit();
                    return true;
                
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public bool Update_Salary(int p_salaryID ,int p_Distributor_id, int p_Designation_id, int p_Employee_id, decimal p_Salary_Amount,
        DateTime p_Salary_Month, int p_UserId, DateTime p_Document_Date, DataTable dtAllowanceDetail, DataTable dtDeductionDetail)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdateEMPLOYEE_SALARY mISom = new spUpdateEMPLOYEE_SALARY();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Sale Order Master----------

                mISom.SALARY_ID = p_salaryID;
                mISom.DISTRIBUTOR_ID = p_Distributor_id;
                mISom.DESIGNATION_ID = p_Designation_id;
                mISom.EMPLOYEE_ID = p_Employee_id;
                mISom.SALARY_AMOUNT = p_Salary_Amount;
                mISom.SALARY_MONTH = p_Salary_Month;

                mISom.DOCUMENT_DATE = p_Document_Date;
                mISom.USER_ID = p_UserId;
                mISom.TIME_STAMP = DateTime.Now;
                mISom.LAST_UPDATE = System.DateTime.Now;
                mISom.IS_ACTIVE = true;

                mISom.ExecuteQuery();


                //----------------Insert into sale order detail-------------
                spInsertEMPLOYEE_SALARY_ALLOWANCE mSaleOrderDetail = new spInsertEMPLOYEE_SALARY_ALLOWANCE();
                mSaleOrderDetail.Connection = mConnection;
                mSaleOrderDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtAllowanceDetail.Rows)
                {

                    mSaleOrderDetail.SALARY_ID = mISom.SALARY_ID;
                    mSaleOrderDetail.EMPLOYEE_ID = p_Employee_id;
                    mSaleOrderDetail.ALLOWANCE_MASTER_ID = int.Parse(dr["ALLOWANCE_ID"].ToString());
                    mSaleOrderDetail.ALLOWANCE_DESCRIPTION = dr["ALLOWANCE_DESC"].ToString();
                    mSaleOrderDetail.ALLOWANCE_AMOUNT = decimal.Parse(dr["ALLOWANCE_AMOUNT"].ToString());

                    mSaleOrderDetail.ExecuteQuery();



                }
                foreach (DataRow df in dtDeductionDetail.Rows)
                {
                    //----------------Insert into sale order Promotion-------------
                    spInsertEMPLOYEE_SALARY_DEDUCTION mSaleOrderPromo = new spInsertEMPLOYEE_SALARY_DEDUCTION();
                    mSaleOrderPromo.Connection = mConnection;
                    mSaleOrderPromo.Transaction = mTransaction;

                    mSaleOrderPromo.SALARY_ID = mISom.SALARY_ID;
                    mSaleOrderPromo.EMPLOYEE_ID = p_Employee_id;
                    mSaleOrderPromo.DEDUCTION_MASTER_ID = int.Parse(df["DEDUCTION_ID"].ToString());
                    mSaleOrderPromo.DEDUCTION_DESCRIPTION = df["DEDUCTION_DESC"].ToString();
                    mSaleOrderPromo.DEDUCTION_AMOUNT = decimal.Parse(df["DEDUCTION_AMOUNT"].ToString());

                    mSaleOrderPromo.ExecuteQuery();
                }

                mTransaction.Commit();
                return true;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }
        

        #endregion

        #region Get
        public DataTable  Get_Employee_Salary(int p_Distributor_id, int p_Designation_id, DateTime  p_Salary_Amount)

        {

            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPROCESS_SALARY mdtCompany = new spSelectPROCESS_SALARY();
                mdtCompany.Connection = mConnection;
                mdtCompany.DISTRIBUTOR_ID = p_Distributor_id;
                mdtCompany.DESIGNATION_ID = p_Designation_id;
                mdtCompany.SALARY_MONTH = p_Salary_Amount;
                DataTable dt = mdtCompany.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public bool InsertSalary(DateTime p_SALARY_MONTH, int p_LOCATION_ID, int p_DEPARTMENT_ID, int p_TYPE_ID, int p_BONUS_TYPE_ID, int p_EmployeeID, decimal p_BASIC_SALARY, decimal p_ALLOWNCES, decimal p_DEDUCTIONS, decimal p_FINE, int p_USER_ID
                , int p_WORK_DATES, int p_OUTSIDE_HOURS, int p_VISITS)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSALARY_DETAIL salary = new spInsertSALARY_DETAIL();
                salary.Connection = mConnection;
                salary.SALARY_MONTH = p_SALARY_MONTH;
                salary.LOCATION_ID = p_LOCATION_ID;
                salary.DEPARTMENT_ID = p_DEPARTMENT_ID;
                salary.TYPE_ID = p_TYPE_ID;
                salary.BONUS_TYPE_ID = p_BONUS_TYPE_ID;
                salary.EmployeeID = p_EmployeeID;
                salary.BASIC_SALARY = p_BASIC_SALARY;
                salary.ALLOWNCES = p_ALLOWNCES;
                salary.DEDUCTIONS = p_DEDUCTIONS;
                salary.FINE = p_FINE;
                salary.USER_ID = p_USER_ID;
                salary.WORK_DATES = p_WORK_DATES;
                salary.OUTSIDE_HOURS = p_OUTSIDE_HOURS;
                salary.VISITS = p_VISITS;
                return salary.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        #endregion

        #region Salary Report

        public DataSet payslip(int p_Distributor_ID, int Designation_ID, DateTime p_SalaryMonth, int p_Empoyee_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                RptPay_SLIP ObjPrint = new RptPay_SLIP();
                CORNBusinessLayer.Reports.LatestDataSet ds = new CORNBusinessLayer.Reports.LatestDataSet();
                ObjPrint.Connection = mConnection;
                ObjPrint.LOCATION_ID = p_Distributor_ID;
                ObjPrint.DEPARTMENT_ID = Designation_ID;
                ObjPrint.EmployeeID = p_Empoyee_ID;
                ObjPrint.SALARY_MONTH = p_SalaryMonth;

                DataTable dt = ObjPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["RptPay_SLIP"].ImportRow(dr);
                }


                RptPay_SLIPAllowances ObjPrintAllowances = new RptPay_SLIPAllowances();

                ObjPrintAllowances.Connection = mConnection;
                ObjPrintAllowances.LOCATION_ID = p_Distributor_ID;
                ObjPrintAllowances.DEPARTMENT_ID = Designation_ID;
                ObjPrintAllowances.EmployeeID = p_Empoyee_ID;
                ObjPrintAllowances.SALARY_MONTH = p_SalaryMonth;

                DataTable dtAllowences= ObjPrintAllowances.ExecuteTable();
                foreach (DataRow dr in dtAllowences.Rows)
                {
                    ds.Tables["RptPay_SLIPAllowances"].ImportRow(dr);
                }

                RptPay_SLIPDeduction ObjPrintdeduction = new RptPay_SLIPDeduction();

                ObjPrintdeduction.Connection = mConnection;
                ObjPrintdeduction.LOCATION_ID = p_Distributor_ID;
                ObjPrintdeduction.DEPARTMENT_ID = Designation_ID;
                ObjPrintdeduction.EmployeeID = p_Empoyee_ID;
                ObjPrintdeduction.SALARY_MONTH = p_SalaryMonth;

                DataTable dtdeduction = ObjPrintdeduction.ExecuteTable();
                foreach (DataRow dr in dtdeduction.Rows)
                {
                    ds.Tables["RptPay_SLIPDeduction"].ImportRow(dr);
                }


                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion
    }
}
