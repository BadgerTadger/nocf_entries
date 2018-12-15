using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace nocf_entries.App_Code
{
    public class clsShowEntry
    {
        private static string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private SqlConnection cn = null;

        private int _showEntryID;
        public int ShowEntryID
        {
            get { return _showEntryID; }
            set { _showEntryID = value; }
        }

        private int _eventEntryID;
        public int EventEntryID
        {
            get { return _eventEntryID; }
            set { _eventEntryID = value; }
        }

        private int _showID;
        public int ShowID
        {
            get { return _showID; }
            set { _showID = value; }
        }

        private bool _offerOfHelp;
        public bool OfferOfHelp
        {
            get { return _offerOfHelp; }
            set { _offerOfHelp = value; }
        }

        private string _helpDetails;
        public string HelpDetails
        {
            get { return _helpDetails; }
            set { _helpDetails = value; }
        }

        private DateTime _entryDate;
        public DateTime EntryDate
        {
            get { return _entryDate; }
            set { _entryDate = value; }
        }

        private string _showSpecialRequest;

        public string ShowSpecialRequest
        {
            get { return _showSpecialRequest; }
            set { _showSpecialRequest = value; }
        }


        public clsShowEntry(int eventEntryID)
        {
            _eventEntryID = eventEntryID;
        }

        internal void Load(int showEntryID)
        {
            _showEntryID = showEntryID;

            try
            {
                string sqlCmd = @"SELECT ShowID
                              ,OfferOfHelp
                              ,HelpDetails
                              ,EntryDate
                              ,ShowSpecialRequest
                          FROM tblShowEntries
                          WHERE ShowEntryID = @ShowEntryID";

                cn = new SqlConnection(_connString);
                cn.Open();
                SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
                adr.SelectCommand.CommandType = CommandType.Text;
                adr.SelectCommand.Parameters.AddWithValue("@EventEntryID", _eventEntryID);
                adr.SelectCommand.Parameters.AddWithValue("@ShowEntryID", _showEntryID);
                DataSet ds = new DataSet();
                adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    _showID = int.Parse(ds.Tables[0].Rows[0]["ShowID"].ToString());
                    _offerOfHelp = ds.Tables[0].Rows[0]["OfferOfHelp"].ToString() == "True";
                    _helpDetails = ds.Tables[0].Rows[0]["HelpDetails"].ToString();
                    _entryDate = DateTime.Parse(ds.Tables[0].Rows[0]["EntryDate"].ToString());
                    _showSpecialRequest = ds.Tables[0].Rows[0]["ShowSpecialRequest"].ToString();
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

        internal bool LoadByShowID(int showID)
        {
            bool retVal = false;
            _showID = showID;

            try
            {
                string sqlCmd = @"SELECT ShowEntryID
                              ,OfferOfHelp
                              ,HelpDetails
                              ,EntryDate
                              ,ShowSpecialRequest
                          FROM tblShowEntries
                          WHERE EventEntryID = @EventEntryID AND ShowID = @ShowID";

                cn = new SqlConnection(_connString);
                cn.Open();
                SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
                adr.SelectCommand.CommandType = CommandType.Text;
                adr.SelectCommand.Parameters.AddWithValue("@EventEntryID", _eventEntryID);
                adr.SelectCommand.Parameters.AddWithValue("@ShowID", _showID);
                DataSet ds = new DataSet();
                adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    retVal = true;
                    _showEntryID = int.Parse(ds.Tables[0].Rows[0]["ShowEntryID"].ToString());
                    _offerOfHelp = ds.Tables[0].Rows[0]["OfferOfHelp"].ToString() == "True";
                    _helpDetails = ds.Tables[0].Rows[0]["HelpDetails"].ToString();
                    _entryDate = DateTime.Parse(ds.Tables[0].Rows[0]["EntryDate"].ToString());
                    _showSpecialRequest = ds.Tables[0].Rows[0]["ShowSpecialRequest"].ToString();
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

            return retVal;
        }

        internal List<clsShowEntry> GetShowEntriesForEventEntry()
        {
            List<clsShowEntry> retVal = null;

            try
            {
                string sqlCmd = @"SELECT ShowEntryID
                              ,OfferOfHelp
                              ,HelpDetails
                              ,EntryDate
                              ,ShowSpecialRequest
                          FROM tblShowEntries
                          WHERE EventEntryID = @EventEntryID";

                cn = new SqlConnection(_connString);
                cn.Open();
                SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
                adr.SelectCommand.CommandType = CommandType.Text;
                adr.SelectCommand.Parameters.AddWithValue("@EventEntryID", _eventEntryID);
                DataSet ds = new DataSet();
                adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    retVal = new List<clsShowEntry>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        clsShowEntry showEntry = new clsShowEntry(_eventEntryID);
                        showEntry.ShowEntryID = int.Parse(ds.Tables[0].Rows[0]["ShowEntryID"].ToString());
                        showEntry.OfferOfHelp = ds.Tables[0].Rows[0]["OfferOfHelp"].ToString() == "True";
                        showEntry.HelpDetails = ds.Tables[0].Rows[0]["HelpDetails"].ToString();
                        showEntry.EntryDate = DateTime.Parse(ds.Tables[0].Rows[0]["EntryDate"].ToString());
                        showEntry.ShowSpecialRequest = ds.Tables[0].Rows[0]["ShowSpecialRequest"].ToString();

                        retVal.Add(showEntry);
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

            return retVal;
        }

        internal int Save()
        {
            try
            {
                string sqlCmd = "";
                if (_showEntryID == 0)
                {
                    sqlCmd = @"INSERT INTO tblShowEntries
                           (ShowEntryID
                          ,EventEntryID
                          ,ShowID
                          ,OfferOfHelp
                          ,HelpDetails
                          ,EntryDate
                          ,ShowSpecialRequest)
                         VALUES
                           (@ShowEntryID
                          ,@EventEntryID
                          ,@ShowID
                          ,@OfferOfHelp
                          ,@HelpDetails
                          ,@EntryDate
                          ,@ShowSpecialRequest)";
                }
                else
                {
                    sqlCmd = @"UPDATE tblShowEntries
                           SET OfferOfHelp = @OfferOfHelp
                          ,HelpDetails = @HelpDetails
                          ,EntryDate = @EntryDate
                          ,ShowSpecialRequest = @ShowSpecialRequest
                        WHERE ShowEntryID = @ShowEntryID";
                }

                cn = new SqlConnection(_connString);
                SqlCommand command = new SqlCommand(sqlCmd, cn);
                cn.Open();
                if (_showEntryID == 0)
                {
                    _showEntryID = GetNextShowEntryID();
                }
                command.Parameters.AddWithValue("@ShowEntryID", _showEntryID);
                command.Parameters.AddWithValue("@EventEntryID", _eventEntryID);
                command.Parameters.AddWithValue("@ShowID", _showID);
                command.Parameters.AddWithValue("@OfferOfHelp", _offerOfHelp);
                command.Parameters.AddWithValue("@HelpDetails", _helpDetails);
                command.Parameters.AddWithValue("@EntryDate", _entryDate);
                command.Parameters.AddWithValue("@ShowSpecialRequest", _showSpecialRequest);
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

            return _showEntryID;
        }

        private int GetNextShowEntryID()
        {
            int retVal = 0;
            SqlCommand cmd = null;
            SqlConnection cn = new SqlConnection(_connString);

            try
            {
                string sqlCmd = "SELECT MAX(ShowEntryID) From tblShowEntries";
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
    }
}