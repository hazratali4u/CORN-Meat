using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;
using CORNDatabaseLayer.Classes;
using System;
using System.Data;

namespace CORNBusinessLayer.Classes
{
    public class CustomerFeedbackController
    {
        public string InsertFeedBack(int LocationId, int ServiceRate, int FoodRate, int EnvRate, int OverallRate, string Comments, int HearMedium, string OtherMeduim, string Name, string ContactNo, string Email, string Address)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCUSTOMER_FEEDBACK mFeedback = new spInsertCUSTOMER_FEEDBACK();
                mFeedback.Connection = mConnection;

                mFeedback.LOCATION_ID = LocationId;
                mFeedback.SERVICE_RATE = ServiceRate;
                mFeedback.FOOD_RATE = FoodRate;
                mFeedback.ENVIRONMENT_RATE = EnvRate;
                mFeedback.OVERALL_RATE = OverallRate;
                mFeedback.COMMENTS = Comments;
                mFeedback.HEAR_MEDIUM = HearMedium;
                mFeedback.OTHER_MEDIUM = OtherMeduim;
                mFeedback.NAME = Name;
                mFeedback.CONTACT_NO = ContactNo;
                mFeedback.EMAIL = Email;
                mFeedback.ADDRESS = Address;
                mFeedback.TIME_STAMP = System.DateTime.Now;

                mFeedback.ExecuteQuery();

                return mFeedback.FEEDBACK_ID.ToString();
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
    }
}
