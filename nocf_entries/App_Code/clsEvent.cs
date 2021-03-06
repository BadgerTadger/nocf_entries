﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace nocf_entries.App_Code
{
    public class clsEvent
    {
        private static string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private SqlConnection cn = null;

        private int _eventID;
        public int EventID
        {
            get { return _eventID; }
            set { _eventID = value; }
        }

        private string _eventName;
        public string EventName
        {
            get { return _eventName; }
            set { _eventName = value; }
        }

        private bool _eventActive;
        public bool EventActive
        {
            get { return _eventActive; }
            set { _eventActive = value; }
        }

        public clsEvent()
        {

        }

        internal void Load(int eventID)
        {
            _eventID = eventID;

            try
            {
                string sqlCmd = @"SELECT [EventID],[EventName],[EventActive]
                FROM tblEvents WHERE EventID = @EventID";

                cn = new SqlConnection(_connString);
                cn.Open();
                SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
                adr.SelectCommand.CommandType = CommandType.Text;
                adr.SelectCommand.Parameters.AddWithValue("@EventID", eventID);
                DataSet ds = new DataSet();
                adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    {
                        _eventName = ds.Tables[0].Rows[0]["EventName"].ToString();
                        _eventActive = ds.Tables[0].Rows[0]["EventActive"].ToString() == "True";
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }
        }

        public static int GetNextEventID()
        {
            int retVal = 0;
            SqlCommand cmd = null;
            SqlConnection cn = new SqlConnection(_connString);

            try
            {
                string sqlCmd = "SELECT MAX(EventID) From tblEvents";
                cn.Open();
                cmd = new SqlCommand(sqlCmd, cn);

                var id = cmd.ExecuteScalar();
                if(!id.Equals(DBNull.Value)) retVal = Convert.ToInt32(id);
                retVal += 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }

            return retVal;
        }

        internal void Save()
        {
            try
            {
                string sqlCmd = "";
                if (_eventID == 0)
                {
                    _eventID = GetNextEventID();

                    sqlCmd = @"INSERT INTO [dbo].[tblEvents]
                        ([EventID],[EventName],[EventActive])
                        VALUES
                        (@EventID, @EventName,@EventActive)";
                }
                else
                {
                    sqlCmd = @"UPDATE [dbo].[tblEvents]
                        SET [EventName] = @EventName,
                        EventActive = @EventActive
                        WHERE EventID = @EventID";
                }

                cn = new SqlConnection(_connString);
                SqlCommand command = new SqlCommand(sqlCmd, cn);
                cn.Open();
                command.Parameters.AddWithValue("@EventID", _eventID);
                command.Parameters.AddWithValue("@EventName", _eventName);
                command.Parameters.AddWithValue("@EventActive", _eventActive);
                command.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Dispose(); // return connection to pool
            }
        }

        internal static DataTable GetMyEventList(string ownerID, bool getActiveOnly = false)
        {
            DataTable retVal = null;

            string sqlCmd = @"SELECT DISTINCT e.EventID
                    ,EventName,EventActive
                    FROM tblEvents e
                    INNER JOIN tblShows s ON e.EventID = s.EventID
                    INNER JOIN tblEntries en ON s.ShowID = en.ShowID
                    AND OwnerID = @OwnerID";

            if (getActiveOnly)
            {
                sqlCmd += @" WHERE EventActive = 1 ";
            }

            SqlConnection cn = new SqlConnection(_connString);
            cn.Open();
            SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
            adr.SelectCommand.CommandType = CommandType.Text;
            adr.SelectCommand.Parameters.AddWithValue("@OwnerID", ownerID);
            DataSet ds = new DataSet();
            adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = ds.Tables[0];
            }

            return retVal;
        }

        internal static DataTable GetUpcomingEventList(bool getActiveOnly = false)
        {
            DataTable retVal = null;

            string sqlCmd = @"SELECT [EventID]
                      ,[EventName],[EventActive]
                      FROM tblEvents";

            if (getActiveOnly)
            {
                sqlCmd += @" WHERE EventActive = 1 ";
            }

            SqlConnection cn = new SqlConnection(_connString);
            cn.Open();
            SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
            adr.SelectCommand.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = ds.Tables[0];
            }

            return retVal;
        }
    }
}