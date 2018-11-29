using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace nocf_entries.App_Code
{
    public class clsEntry
    {
        private static string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private SqlConnection cn = null;

        private int _entryID;
        public int EntryID
        {
            get { return _entryID; }
            set { _entryID = value; }
        }

        private string _ownerID;
        public string OwnerID
        {
            get { return _ownerID; }
            set { _ownerID = value; }
        }

        private int _showID;
        public int ShowID
        {
            get { return _showID; }
            set { _showID = value; }
        }

        private bool _catalogue;
        public bool Catalogue
        {
            get { return _catalogue; }
            set { _catalogue = value; }
        }

        private bool _overnightCamping;
        public bool OvernightCamping
        {
            get { return _overnightCamping; }
            set { _overnightCamping = value; }
        }

        private decimal _overpayment;
        public decimal Overpayment
        {
            get { return _overpayment; }
            set { _overpayment = value; }
        }

        private decimal _underpayment;
        public decimal Underpayment
        {
            get { return _underpayment; }
            set { _underpayment = value; }
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

        private bool _witholdAddress;
        public bool WitholdAddress
        {
            get { return _witholdAddress; }
            set { _witholdAddress = value; }
        }

        private bool _sendRunningOrder;
        public bool SendRunningOrder
        {
            get { return _sendRunningOrder; }
            set { _sendRunningOrder = value; }
        }

        private DateTime _entryDate;
        public DateTime EntryDate
        {
            get { return _entryDate; }
            set { _entryDate = value; }
        }

        public clsEntry(string ownerID)
        {
            _ownerID = ownerID;
        }

        internal void Load(int entryID)
        {
            _entryID = entryID;

            try
            {
                string sqlCmd = @"SELECT ShowID
                              ,Catalogue
                              ,OvernightCamping
                              ,Overpayment
                              ,Underpayment
                              ,OfferOfHelp
                              ,HelpDetails
                              ,WitholdAddress
                              ,SendRunningOrder
                              ,EntryDate
                          FROM tblEntries
                          WHERE OwnerID = @OwnerID AND EntryID = @EntryID";

                cn = new SqlConnection(_connString);
                cn.Open();
                SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
                adr.SelectCommand.CommandType = CommandType.Text;
                adr.SelectCommand.Parameters.AddWithValue("@OwnerID", _ownerID);
                adr.SelectCommand.Parameters.AddWithValue("@EntryID", _entryID);
                DataSet ds = new DataSet();
                adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    _showID = int.Parse(ds.Tables[0].Rows[0]["ShowID"].ToString());
                    _catalogue = ds.Tables[0].Rows[0]["Catalogue"].ToString() == "True";
                    _overnightCamping = ds.Tables[0].Rows[0]["OvernightCamping"].ToString() == "True";
                    _overpayment = decimal.Parse(ds.Tables[0].Rows[0]["Overpayment"].ToString());
                    _underpayment = decimal.Parse(ds.Tables[0].Rows[0]["Underpayment"].ToString());
                    _offerOfHelp = ds.Tables[0].Rows[0]["OfferOfHelp"].ToString() == "True";
                    _helpDetails = ds.Tables[0].Rows[0]["HelpDetails"].ToString();
                    _witholdAddress = ds.Tables[0].Rows[0]["WitholdAddress"].ToString() == "True";
                    _sendRunningOrder = ds.Tables[0].Rows[0]["SendRunningOrder"].ToString() == "True";
                    _entryDate = DateTime.Parse(ds.Tables[0].Rows[0]["EntryDate"].ToString());
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
                string sqlCmd = @"SELECT EntryID
                              ,Catalogue
                              ,OvernightCamping
                              ,Overpayment
                              ,Underpayment
                              ,OfferOfHelp
                              ,HelpDetails
                              ,WitholdAddress
                              ,SendRunningOrder
                              ,EntryDate
                          FROM tblEntries
                          WHERE OwnerID = @OwnerID AND ShowID = @ShowID";

                cn = new SqlConnection(_connString);
                cn.Open();
                SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
                adr.SelectCommand.CommandType = CommandType.Text;
                adr.SelectCommand.Parameters.AddWithValue("@OwnerID", _ownerID);
                adr.SelectCommand.Parameters.AddWithValue("@ShowID", _showID);
                DataSet ds = new DataSet();
                adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    retVal = true;
                    _entryID = int.Parse(ds.Tables[0].Rows[0]["EntryID"].ToString());
                    _catalogue = ds.Tables[0].Rows[0]["Catalogue"].ToString() == "True";
                    _overnightCamping = ds.Tables[0].Rows[0]["OvernightCamping"].ToString() == "True";
                    _overpayment = decimal.Parse(ds.Tables[0].Rows[0]["Overpayment"].ToString());
                    _underpayment = decimal.Parse(ds.Tables[0].Rows[0]["Underpayment"].ToString());
                    _offerOfHelp = ds.Tables[0].Rows[0]["OfferOfHelp"].ToString() == "True";
                    _helpDetails = ds.Tables[0].Rows[0]["HelpDetails"].ToString();
                    _witholdAddress = ds.Tables[0].Rows[0]["WitholdAddress"].ToString() == "True";
                    _sendRunningOrder = ds.Tables[0].Rows[0]["SendRunningOrder"].ToString() == "True";
                    _entryDate = DateTime.Parse(ds.Tables[0].Rows[0]["EntryDate"].ToString());

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
                if (_entryID == 0)
                {
                    sqlCmd = @"INSERT INTO tblEntries
                           (EntryID
                           ,OwnerID
                           ,ShowID
                           ,Catalogue
                           ,OvernightCamping
                           ,Overpayment
                           ,Underpayment
                           ,OfferOfHelp
                           ,HelpDetails
                           ,WitholdAddress
                           ,SendRunningOrder
                           ,EntryDate)
                         VALUES
                           (@EntryID
                           ,@OwnerID
                           ,@ShowID
                           ,@Catalogue
                           ,@OvernightCamping
                           ,@Overpayment
                           ,@Underpayment
                           ,@OfferOfHelp
                           ,@HelpDetails
                           ,@WitholdAddress
                           ,@SendRunningOrder
                           ,@EntryDate)";
                }
                else
                {
                    sqlCmd = @"UPDATE tblEntries
                           SET ShowID = @ShowID
                           ,Catalogue = @Catalogue
                           ,OvernightCamping = @OvernightCamping
                           ,Overpayment = @Overpayment
                           ,Underpayment = @Underpayment
                           ,OfferOfHelp = @OfferOfHelp
                           ,HelpDetails = @HelpDetails
                           ,WitholdAddress = @WitholdAddress
                           ,SendRunningOrder = @SendRunningOrder
                           ,EntryDate = @EntryDate
                        WHERE OwnerID = @OwnerID AND EntryID = @EntryID";
                }

                cn = new SqlConnection(_connString);
                SqlCommand command = new SqlCommand(sqlCmd, cn);
                cn.Open();
                if (_entryID == 0)
                {
                    _entryID = GetNextEntryID();
                }
                command.Parameters.AddWithValue("@OwnerID", _ownerID);
                command.Parameters.AddWithValue("@EntryID", _entryID);
                command.Parameters.AddWithValue("@ShowID", _showID);
                command.Parameters.AddWithValue("@Catalogue", _catalogue);
                command.Parameters.AddWithValue("@OvernightCamping", _overnightCamping);
                command.Parameters.AddWithValue("@Overpayment", _overpayment);
                command.Parameters.AddWithValue("@Underpayment", _underpayment);
                command.Parameters.AddWithValue("@OfferOfHelp", _offerOfHelp);
                command.Parameters.AddWithValue("@HelpDetails", _helpDetails);
                command.Parameters.AddWithValue("@WitholdAddress", _witholdAddress);
                command.Parameters.AddWithValue("@SendRunningOrder", _sendRunningOrder);
                command.Parameters.AddWithValue("@EntryDate", _entryDate);
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

            return _entryID;
        }

        private int GetNextEntryID()
        {
            int retVal = 0;
            SqlCommand cmd = null;
            SqlConnection cn = new SqlConnection(_connString);

            try
            {
                string sqlCmd = "SELECT MAX(EntryID) From tblEntries";
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