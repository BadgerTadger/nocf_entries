using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace nocf_entries.App_Code
{
    public class Show
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

        private int _maxClassesPerDog;
        public int MaxClassesPerDog
        {
            get { return _maxClassesPerDog; }
            set { _maxClassesPerDog = value; }
        }

        public Show()
        {

        }

        public Show(int eventID)
        {
            _eventID = eventID;
        }

        internal void Load(int showID)
        {
            _showID = showID;

            try
            {
                string sqlCmd = @"SELECT [ShowTypeID]
                        ,[ShowTypeDescription]
                        ,[ShowName]
                        ,[ShowOpens]
                        ,[JudgingCommences]
                        ,[ClosingDate]
                        ,[MaxClassesPerDog]
                        FROM [dbo].[tblShows] s
                        inner join lkpShowTypes st on s.ShowTypeID = st.ShowTypeID 
                        WHERE ShowID = @ShowID AND EventID = @EventID";

                cn = new SqlConnection(_connString);
                cn.Open();
                SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
                adr.SelectCommand.CommandType = CommandType.Text;
                adr.SelectCommand.Parameters.AddWithValue("@ShowID", _showID);
                adr.SelectCommand.Parameters.AddWithValue("@EventID", _eventID);
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
                        _maxClassesPerDog = int.Parse(ds.Tables[0].Rows[0]["MaxClassesPerDog"].ToString());
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
                    sqlCmd = @"INSERT INTO [dbo].[tblShows]
                       ([ShowID]
                       ,[EventID]
                       ,[ShowTypeID]
                       ,[ShowName]
                       ,[ShowOpens]
                       ,[JudgingCommences]
                       ,[ClosingDate]
                       ,[MaxClassesPerDog])
                 VALUES
                       (@ShowID
                       ,@EventID
                       ,@ShowTypeID
                       ,@ShowName
                       ,@ShowOpens
                       ,@JudgingCommences
                       ,@ClosingDate
                       ,@MaxClassesPerDog)";
                }
                else
                {
                    sqlCmd = @"UPDATE [dbo].[tblShows]
                   SET [EventID] = @EventID
                      ,[ShowTypeID] = @ShowTypeID
                      ,[ShowName] = @ShowName
                      ,[ShowOpens] = @ShowOpens
                      ,[JudgingCommences] = @JudgingCommences
                      ,[ClosingDate] = @ClosingDate
                      ,[MaxClassesPerDog] = @MaxClassesPerDog
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
                command.Parameters.AddWithValue("@MaxClassesPerDog", _maxClassesPerDog);
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

            string sqlCmd = @"SELECT [ShowID]
                    ,[EventID]
                    ,[ShowTypeID]
                    ,[ShowTypeDescription
                    ,[ShowName]
                    ,[ShowOpens]
                    ,[JudgingCommences]
                    ,[ClosingDate]
                    ,[MaxClassesPerDog]
                    FROM [dbo].[tblShows] s
                    inner join lkpShowTypes st on s.ShowTypeID = st.ShowTypeID WHERE EventID = @EventID";

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

        public static List<ShowType> GetShowTypeList()
        {
            List<ShowType> retVal = null;

            string sqlCmd = @"SELECT ShowTypeID
                      ,ShowTypeDescription
                      FROM lkpShowTypes";

            SqlConnection cn = new SqlConnection(_connString);
            cn.Open();
            SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
            adr.SelectCommand.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = new List<ShowType>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    retVal.Add(new ShowType(int.Parse(row["ShowTypeID"].ToString()), row["ShowTypeDescription"].ToString()));
                }
            }

            return retVal;
        }
    }


    public class ShowType
    {
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

        public ShowType(int showTypeID, string showTypeDescription)
        {
            _showTypeID = showTypeID;
            _showTypeDescription = showTypeDescription;
        }
    }
}