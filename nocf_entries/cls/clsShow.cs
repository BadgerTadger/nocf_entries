﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace nocf_entries.cls
{
    public class clsShow
    {
        private static string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private SqlConnection cn = null;

        private int _showID;
        public int ShowID
        {
            get { return _showID; }
            set { _showID = value; }
        }

        private int _eventID;
        public int EventID
        {
            get { return _eventID; }
            set { _eventID = value; }
        }

        private int _showTypeID;
        public int ShowTypeID
        {
            get { return _showTypeID; }
            set { _showTypeID = value; }
        }

        private string _showTypeDescription;
        public string ShowTypeDescription
        {
            get { return _showTypeDescription; }
            set { _showTypeDescription = value; }
        }

        private string _showName;
        public string ShowName
        {
            get { return _showName; }
            set { _showName = value; }
        }

        private DateTime _showOpens;
        public DateTime ShowOpens
        {
            get { return _showOpens; }
            set { _showOpens = value; }
        }

        private DateTime _judgingCommences;
        public DateTime JudgingCommences
        {
            get { return _judgingCommences; }
            set { _judgingCommences = value; }
        }

        private DateTime _closingDate;
        public DateTime ClosingDate
        {
            get { return _closingDate; }
            set { _closingDate = value; }
        }

        private decimal _defaultClassCost;
        public decimal DefaultClassCost
        {
            get { return _defaultClassCost; }
            set { _defaultClassCost = value; }
        }

        private int _maxClassesPerDog;
        public int MaxClassesPerDog
        {
            get { return _maxClassesPerDog; }
            set { _maxClassesPerDog = value; }
        }

        private int _maxDogsPerClass;
        public int MaxDogsPerClass
        {
            get { return _maxDogsPerClass; }
            set { _maxDogsPerClass = value; }
        }

        private int _defaultDogsPerClassPart;
        public int DefaultDogsPerClassPart
        {
            get { return _defaultDogsPerClassPart; }
            set { _defaultDogsPerClassPart = value; }
        }

        public clsShow()
        {

        }

        public clsShow(int eventID)
        {
            _eventID = eventID;
        }

        internal void Load(int showID)
        {
            _showID = showID;

            try
            {
                string sqlCmd = @"SELECT s.ShowTypeID
                        ,ShowTypeDescription
                        ,ShowName
                        ,ShowOpens
                        ,JudgingCommences
                        ,ClosingDate
                        ,DefaultClassCost
                        ,MaxClassesPerDog
                        ,MaxDogsPerClass
                        ,DefaultDogsPerClassPart
                        FROM tblShows s
                        inner join lkpShowTypes st on s.ShowTypeID = st.ShowTypeID 
                        WHERE ShowID = @ShowID";

                cn = new SqlConnection(_connString);
                cn.Open();
                SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
                adr.SelectCommand.CommandType = CommandType.Text;
                adr.SelectCommand.Parameters.AddWithValue("@ShowID", _showID);
                DataSet ds = new DataSet();
                adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    {
                        _showTypeID = int.Parse(ds.Tables[0].Rows[0]["ShowTypeID"].ToString());
                        _showTypeDescription = ds.Tables[0].Rows[0]["ShowTypeDescription"].ToString();
                        _showName = ds.Tables[0].Rows[0]["ShowName"].ToString();
                        _showOpens = DateTime.Parse(ds.Tables[0].Rows[0]["ShowOpens"].ToString());
                        _judgingCommences = DateTime.Parse(ds.Tables[0].Rows[0]["JudgingCommences"].ToString());
                        _closingDate = DateTime.Parse(ds.Tables[0].Rows[0]["ClosingDate"].ToString());
                        _defaultClassCost = decimal.Parse(ds.Tables[0].Rows[0]["DefaultClassCost"].ToString());
                        _maxClassesPerDog = int.Parse(ds.Tables[0].Rows[0]["MaxClassesPerDog"].ToString());
                        _maxDogsPerClass = int.Parse(ds.Tables[0].Rows[0]["MaxDogsPerClass"].ToString());
                        _defaultDogsPerClassPart = int.Parse(ds.Tables[0].Rows[0]["DefaultDogsPerClassPart"].ToString());
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
        internal void Save()
        {
            try
            {
                string sqlCmd = "";
                if (_showID == 0)
                {
                    sqlCmd = @"INSERT INTO tblShows
                       (ShowID
                       ,EventID
                       ,ShowTypeID
                       ,ShowName
                       ,ShowOpens
                       ,JudgingCommences
                       ,ClosingDate
                       ,DefaultClassCost
                       ,MaxClassesPerDog
                       ,MaxDogsPerClass
                       ,DefaultDogsPerClassPart)
                 VALUES
                       (@ShowID
                       ,@EventID
                       ,@ShowTypeID
                       ,@ShowName
                       ,@ShowOpens
                       ,@JudgingCommences
                       ,@ClosingDate
                       ,@DefaultClassCost
                       ,@MaxClassesPerDog
                       ,@MaxDogsPerClass
                       ,@DefaultDogsPerClassPart)";
                }
                else
                {
                    sqlCmd = @"UPDATE tblShows
                   SET EventID = @EventID
                      ,ShowTypeID = @ShowTypeID
                      ,ShowName = @ShowName
                      ,ShowOpens = @ShowOpens
                      ,JudgingCommences = @JudgingCommences
                      ,ClosingDate = @ClosingDate
                      ,DefaultClassCost = @DefaultClassCost
                      ,MaxClassesPerDog = @MaxClassesPerDog
                      ,MaxDogsPerClass = @MaxDogsPerClass
                      ,DefaultDogsPerClassPart = @DefaultDogsPerClassPart
                    WHERE ShowID = @ShowID";
                }

                cn = new SqlConnection(_connString);
                SqlCommand command = new SqlCommand(sqlCmd, cn);
                cn.Open();
                if (_showID == 0)
                {
                    _showID = GetNextShowID();
                }
                command.Parameters.AddWithValue("@ShowID", _showID);
                command.Parameters.AddWithValue("@EventID", _eventID);
                command.Parameters.AddWithValue("@ShowTypeID", _showTypeID);
                command.Parameters.AddWithValue("@ShowName", _showName);
                command.Parameters.AddWithValue("@ShowOpens", _showOpens);
                command.Parameters.AddWithValue("@JudgingCommences", _judgingCommences);
                command.Parameters.AddWithValue("@ClosingDate", _closingDate);
                command.Parameters.AddWithValue("@DefaultClassCost", _defaultClassCost);
                command.Parameters.AddWithValue("@MaxClassesPerDog", _maxClassesPerDog);
                command.Parameters.AddWithValue("@MaxDogsPerClass", _maxDogsPerClass);
                command.Parameters.AddWithValue("@DefaultDogsPerClassPart", _defaultDogsPerClassPart);
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

        private int GetNextShowID()
        {
            int retVal = 0;
            SqlCommand cmd = null;
            SqlConnection cn = new SqlConnection(_connString);

            try
            {
                string sqlCmd = "SELECT MAX(ShowID) From tblShows";
                cn.Open();
                cmd = new SqlCommand(sqlCmd, cn);

                var id = cmd.ExecuteScalar();
                if (!id.Equals(DBNull.Value)) retVal = Convert.ToInt32(id);
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

        internal DataTable GetShowList()
        {
            DataTable retVal = null;

            string sqlCmd = @"SELECT ShowID
                    ,EventID
                    ,s.ShowTypeID
                    ,ShowTypeDescription
                    ,ShowName
                    ,ShowOpens
                    ,JudgingCommences
                    ,ClosingDate
                    ,DefaultClassCost
                    ,MaxClassesPerDog
                    ,MaxDogsPerClass
                    ,DefaultDogsPerClassPart
                    FROM tblShows s
                    inner join lkpShowTypes st on s.ShowTypeID = st.ShowTypeID 
                    WHERE EventID = @EventID";

            cn = new SqlConnection(_connString);
            cn.Open();
            SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
            adr.SelectCommand.CommandType = CommandType.Text;
            adr.SelectCommand.Parameters.AddWithValue("@EventID", _eventID);
            DataSet ds = new DataSet();
            adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = ds.Tables[0];
            }

            return retVal;
        }

        internal DataTable GetMyShowList(string ownerID)
        {
            DataTable retVal = null;

            string sqlCmd = @"SELECT s.ShowID
                        ,EventID
                        ,s.ShowTypeID
                        ,ShowTypeDescription
                        ,ShowName
                        ,ShowOpens
                        ,JudgingCommences
                        ,ClosingDate
                        ,DefaultClassCost
                        ,MaxClassesPerDog
                        ,MaxDogsPerClass
                        ,DefaultDogsPerClassPart
                        FROM tblShows s
                        inner join lkpShowTypes st on s.ShowTypeID = st.ShowTypeID 
                        INNER JOIN tblShowEntries se ON s.ShowID = se.ShowID
                        INNER JOIN tblEventEntries ee ON se.EventEntryID = ee.EventEntryID
                        AND OwnerID = @OwnerID
                        WHERE EventID = @EventID
                        ORDER BY ShowID";
                
            cn = new SqlConnection(_connString);
            cn.Open();
            SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
            adr.SelectCommand.CommandType = CommandType.Text;
            adr.SelectCommand.Parameters.AddWithValue("@EventID", _eventID);
            adr.SelectCommand.Parameters.AddWithValue("@OwnerID", ownerID);
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