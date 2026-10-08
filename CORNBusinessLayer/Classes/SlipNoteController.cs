using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;
using CORNDatabaseLayer.Classes;

namespace CORNBusinessLayer.Classes
{
    public class SlipNoteController
    {
        public SlipNoteController()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetSlipNotes(int p_TypeId, int p_NoteID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSlipNotes mSlipNoteInfo = new spSelectSlipNotes();
                mSlipNoteInfo.Connection = mConnection;
                mSlipNoteInfo.TYPEID = p_TypeId;
                mSlipNoteInfo.NOTE_ID = p_NoteID;
                mSlipNoteInfo.IS_ACTIVE = true;

                DataTable dt = mSlipNoteInfo.ExecuteTable();
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

        public int InsertSlipNote_Master(string p_note, bool p_isActive)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertSlipNote_MASTER mSlipNoteInfo = new spInsertSlipNote_MASTER();
                mSlipNoteInfo.Connection = mConnection;
                mSlipNoteInfo.SLIP_NOTE = p_note;
                mSlipNoteInfo.IS_ACTIVE = p_isActive;
                mSlipNoteInfo.ExecuteQuery();
                return mSlipNoteInfo.NOTE_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return 0;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool InsertSlipNote_Detail(int p_noteID, int p_DistributorID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertSLIP_NOTE_DETAIL mSlipNote_detail = new spInsertSLIP_NOTE_DETAIL();
                mSlipNote_detail.Connection = mConnection;
                mSlipNote_detail.DISTRIBUTOR_ID = p_DistributorID;
                mSlipNote_detail.NOTE_ID = p_noteID;
                mSlipNote_detail.ExecuteQuery();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public string UpdateSlipNote_Master(int Note_id, string Slip_Note, bool IS_ACTIVE, int TYPEID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateSlip_Note_MASTER mSlipNoteInfo = new spUpdateSlip_Note_MASTER();
                mSlipNoteInfo.Connection = mConnection;
                mSlipNoteInfo.NOTE_ID = Note_id;
                mSlipNoteInfo.IS_ACTIVE = IS_ACTIVE;
                mSlipNoteInfo.SLIIP_NOTE = Slip_Note;
                mSlipNoteInfo.TYPEID = TYPEID;
                mSlipNoteInfo.ExecuteQuery();
                return "Record Updated.";

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
