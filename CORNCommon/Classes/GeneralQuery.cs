
using System;
using System.Data;
using System.Data.SqlClient;

namespace CORNCommon.Classes
{
    public class GeneralQuery
    {
        private Int32? intUserCode;
        private Int64? intBasePKCode;

        public GeneralQuery(Int32? intUserCode, Int64? intBasePKCode)
        {
            this.intUserCode = intUserCode;
            this.intBasePKCode = intBasePKCode;
        }
        
        public Boolean IsGetStatusBarMessage()
        {
            //try
            //{
            //    return (Boolean) DAL.ExecuteScalar(CommandType.Text, "SELECT dbo.fnIsGetStatusBarMessage()");
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return false;
        }

        public Int32 CheckUserRight(Int32 intDivisionCode)
        {
            try
            {
                //SqlParamCollection objCollection = new SqlParamCollection();
                //objCollection.Add(new SqlParameter("@intCompanySetupCode", intDivisionCode));
                //objCollection.Add(new SqlParameter("@intUserCode", Global.Common.GlobalInfo.UserCode));
                //return (Int32)SH.Global.Common.DAL.ExecuteScalar("procCheckUserRights", objCollection);
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }     
}
