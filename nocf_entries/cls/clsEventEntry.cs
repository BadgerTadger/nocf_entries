using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace nocf_entries.cls
{
    public class clsEventEntry
    {
        private static string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private SqlConnection cn = null;

        private int _eventEntryID;
        public int EventEntryID
        {
            get { return _eventEntryID; }
            set { _eventEntryID = value; }
        }

        private string _ownerID;
        public string OwnerID
        {
            get { return _ownerID; }
            set { _ownerID = value; }
        }

        private int _eventID;
        public int EventID
        {
            get { return _eventID; }
            set { _eventID = value; }
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

        private bool _sendCatalogue;
        public bool SendCatalogue
        {
            get { return _sendCatalogue; }
            set { _sendCatalogue = value; }
        }

        private decimal _catalogueCost;
        public decimal CatalogueCost
        {
            get { return _catalogueCost; }
            set { _catalogueCost = value; }
        }

        private int _campingOptionID;
        public int CampingOptionID
        {
            get { return _campingOptionID; }
            set { _campingOptionID = value; }
        }

        private string _vehicleReg;
        public string VehicleReg
        {
            get { return _vehicleReg; }
            set { _vehicleReg = value; }
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

        private string _eventSpecialRequest;
        public string EventSpecialRequest
        {
            get { return _eventSpecialRequest; }
            set { _eventSpecialRequest = value; }
        }

        private List<clsShowEntry> _showEntryList;
        public List<clsShowEntry> ShowEntryList
        {
            get { return _showEntryList; }
            set { _showEntryList = value; }
        }

        public clsEventEntry(string ownerID)
        {
            _ownerID = ownerID;
        }

        internal void Load(int eventEntryID)
        {
            _eventEntryID = eventEntryID;

            try
            {
                string sqlCmd = @"SELECT EventID
                              ,WitholdAddress
                              ,SendRunningOrder
                              ,SendCatalogue
                              ,CatalogueCost
                              ,CampingOptionID
                              ,VehicleReg
                              ,Overpayment
                              ,Underpayment
                              ,EventSpecialRequest
                          FROM tblEventEntries
                          WHERE OwnerID = @OwnerID AND EventEntryID = @EventEntryID";

                cn = new SqlConnection(_connString);
                cn.Open();
                SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
                adr.SelectCommand.CommandType = CommandType.Text;
                adr.SelectCommand.Parameters.AddWithValue("@OwnerID", _ownerID);
                adr.SelectCommand.Parameters.AddWithValue("@EventEntryID", _eventEntryID);
                DataSet ds = new DataSet();
                adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    _eventID = int.Parse(ds.Tables[0].Rows[0]["EventID"].ToString());
                    _witholdAddress = ds.Tables[0].Rows[0]["WitholdAddress"].ToString() == "True";
                    _sendRunningOrder = ds.Tables[0].Rows[0]["SendRunningOrder"].ToString() == "True";
                    _sendCatalogue = ds.Tables[0].Rows[0]["Catalogue"].ToString() == "True";
                    _catalogueCost = decimal.Parse(ds.Tables[0].Rows[0]["CatalogueCost"].ToString());
                    _campingOptionID = int.Parse(ds.Tables[0].Rows[0]["CampingOptionID"].ToString());
                    _vehicleReg = ds.Tables[0].Rows[0]["VehicleReg"].ToString();
                    _overpayment = decimal.Parse(ds.Tables[0].Rows[0]["Overpayment"].ToString());
                    _underpayment = decimal.Parse(ds.Tables[0].Rows[0]["Underpayment"].ToString());
                    _eventSpecialRequest = ds.Tables[0].Rows[0]["EventSpecialRequest"].ToString();
                    clsShowEntry showEntry = new clsShowEntry(_eventEntryID);
                    _showEntryList = showEntry.GetShowEntriesForEventEntry();
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

        internal bool LoadByEventID(int eventID)
        {
            bool retVal = false;
            _eventID = eventID;

            try
            {
                string sqlCmd = @"SELECT EventEntryID
                              ,WitholdAddress
                              ,SendRunningOrder
                              ,SendCatalogue
                              ,CatalogueCost
                              ,CampingOptionID
                              ,VehicleReg
                              ,Overpayment
                              ,Underpayment
                              ,EventSpecialRequest
                          FROM tblEventEntries
                          WHERE OwnerID = @OwnerID AND EventID = @EventID";

                cn = new SqlConnection(_connString);
                cn.Open();
                SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
                adr.SelectCommand.CommandType = CommandType.Text;
                adr.SelectCommand.Parameters.AddWithValue("@OwnerID", _ownerID);
                adr.SelectCommand.Parameters.AddWithValue("@EventID", _eventID);
                DataSet ds = new DataSet();
                adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    retVal = true;
                    _eventEntryID = int.Parse(ds.Tables[0].Rows[0]["EventEntryID"].ToString());
                    _witholdAddress = ds.Tables[0].Rows[0]["WitholdAddress"].ToString() == "True";
                    _sendRunningOrder = ds.Tables[0].Rows[0]["SendRunningOrder"].ToString() == "True";
                    _sendCatalogue = ds.Tables[0].Rows[0]["Catalogue"].ToString() == "True";
                    _catalogueCost = decimal.Parse(ds.Tables[0].Rows[0]["CatalogueCost"].ToString());
                    _campingOptionID = int.Parse(ds.Tables[0].Rows[0]["CampingOptionID"].ToString());
                    _vehicleReg = ds.Tables[0].Rows[0]["VehicleReg"].ToString();
                    _overpayment = decimal.Parse(ds.Tables[0].Rows[0]["Overpayment"].ToString());
                    _underpayment = decimal.Parse(ds.Tables[0].Rows[0]["Underpayment"].ToString());
                    _eventSpecialRequest = ds.Tables[0].Rows[0]["EventSpecialRequest"].ToString();
                    clsShowEntry showEntry = new clsShowEntry(_eventEntryID);
                    _showEntryList = showEntry.GetShowEntriesForEventEntry();
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

        internal bool LoadByShowID(int showID)
        {
            bool retVal = false;

            try
            {
                string sqlCmd = @"SELECT ShowEntryID
                                ,ee.EventEntryID
                                ,ShowID
                                ,OfferOfHelp
                                ,HelpDetails
                                ,EntryDate
                                ,ShowSpecialRequest
                                ,OwnerID
                                ,EventID
                                ,WitholdAddress
                                ,SendRunningOrder
                                ,SendCatalogue
                                ,CatalogueCost
                                ,CampingOptionID
                                ,VehicleReg
                                ,Overpayment
                                ,Underpayment
                                ,EventSpecialRequest
                            FROM tblEventEntries ee INNER JOIN tblShowEntries se
                            ON ee.EventEntryID = se.EventEntryID
                            WHERE OwnerID = @OwnerID AND ShowID = @ShowID";

                cn = new SqlConnection(_connString);
                cn.Open();
                SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
                adr.SelectCommand.CommandType = CommandType.Text;
                adr.SelectCommand.Parameters.AddWithValue("@OwnerID", _ownerID);
                adr.SelectCommand.Parameters.AddWithValue("@ShowID", showID);
                DataSet ds = new DataSet();
                adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    retVal = true;
                    _eventEntryID = int.Parse(ds.Tables[0].Rows[0]["EventEntryID"].ToString());
                    _witholdAddress = ds.Tables[0].Rows[0]["WitholdAddress"].ToString() == "True";
                    _sendRunningOrder = ds.Tables[0].Rows[0]["SendRunningOrder"].ToString() == "True";
                    _sendCatalogue = ds.Tables[0].Rows[0]["Catalogue"].ToString() == "True";
                    _catalogueCost = decimal.Parse(ds.Tables[0].Rows[0]["CatalogueCost"].ToString());
                    _campingOptionID = int.Parse(ds.Tables[0].Rows[0]["CampingOptionID"].ToString());
                    _vehicleReg = ds.Tables[0].Rows[0]["VehicleReg"].ToString();
                    _overpayment = decimal.Parse(ds.Tables[0].Rows[0]["Overpayment"].ToString());
                    _underpayment = decimal.Parse(ds.Tables[0].Rows[0]["Underpayment"].ToString());
                    _eventSpecialRequest = ds.Tables[0].Rows[0]["EventSpecialRequest"].ToString();
                    int showEntryID = int.Parse(ds.Tables[0].Rows[0]["EventEntryID"].ToString());
                    clsShowEntry showEntry = new clsShowEntry(_eventEntryID);
                    showEntry.Load(showEntryID);
                    _showEntryList.Add(showEntry);
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
                if (_eventEntryID == 0)
                {
                    sqlCmd = @"INSERT INTO tblEventEntries
                           (EventEntryID
                           ,OwnerID
                           ,EventID
                           ,WitholdAddress
                           ,SendRunningOrder
                           ,SendCatalogue
                           ,CatalogueCost
                           ,CampingOptionID
                           ,VehicleReg
                           ,Overpayment
                           ,Underpayment
                           ,EventSpecialRequest)
                         VALUES
                           (@EventEntryID
                           ,@OwnerID
                           ,@EventID
                           ,@WitholdAddress
                           ,@SendRunningOrder
                           ,@SendCatalogue
                           ,@CatalogueCost
                           ,@CampingOptionID
                           ,@VehicleReg
                           ,@Overpayment
                           ,@Underpayment
                           ,@EventSpecialRequest)";
                }
                else
                {
                    sqlCmd = @"UPDATE tblEventEntries
                           SET EventID = @EventID
                           ,WitholdAddress = @WitholdAddress
                           ,SendRunningOrder = @SendRunningOrder
                           ,SendCatalogue = @SendCatalogue
                           ,CatalogueCost = @CatalogueCost
                           ,CampingOptionID = @CampingOptionID
                           ,VehicleReg = @VehicleReg
                           ,Overpayment = @Overpayment
                           ,Underpayment = @Underpayment
                           ,EventSpecialRequest = @EventSpecialRequest
                        WHERE OwnerID = @OwnerID AND EventEntryID = @EventEntryID";
                }

                cn = new SqlConnection(_connString);
                SqlCommand command = new SqlCommand(sqlCmd, cn);
                cn.Open();
                if (_eventEntryID == 0)
                {
                    _eventEntryID = GetNextEventEntryID();
                }
                command.Parameters.AddWithValue("@OwnerID", _ownerID);
                command.Parameters.AddWithValue("@EventEntryID", _eventEntryID);
                command.Parameters.AddWithValue("@EventID", _eventID);
                command.Parameters.AddWithValue("@WitholdAddress", _witholdAddress);
                command.Parameters.AddWithValue("@SendRunningOrder", _sendRunningOrder);
                command.Parameters.AddWithValue("@SendCatalogue", _sendCatalogue);
                command.Parameters.AddWithValue("@CampingOptionID", _campingOptionID);
                command.Parameters.AddWithValue("@VehicleReg", _vehicleReg);
                command.Parameters.AddWithValue("@Overpayment", _overpayment);
                command.Parameters.AddWithValue("@Underpayment", _underpayment);
                command.Parameters.AddWithValue("@EventSpecialRequest", _eventSpecialRequest);
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

            return _eventEntryID;
        }

        private int GetNextEventEntryID()
        {
            int retVal = 0;
            SqlCommand cmd = null;
            SqlConnection cn = new SqlConnection(_connString);

            try
            {
                string sqlCmd = "SELECT MAX(EventEntryID) From tblEventEntries";
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