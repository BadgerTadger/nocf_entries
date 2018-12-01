using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace nocf_entries.App_Code
{
    public class clsDogClass
    {
        private static string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private SqlConnection cn = null;

        private int _dogClassID;
        public int DogClassID
        {
            get { return _dogClassID; }
            set { _dogClassID = value; }
        }

        private int _entryID;
        public int EntryID
        {
            get { return _entryID; }
            set { _entryID = value; }
        }

        private int _dogID;
        public int DogID
        {
            get { return _dogID; }
            set { _dogID = value; }
        }

        private int _showClassID;                
        public int ShowClassID
        {
            get { return _showClassID; }
            set { _showClassID = value; }
        }

        private int _preferredPart;
        public int PreferredPart
        {
            get { return _preferredPart; }
            set { _preferredPart = value; }
        }

        private int _showFinalClassID;
        public int ShowFinalClassID
        {
            get { return _showFinalClassID; }
            set { _showFinalClassID = value; }
        }

        private int _ringNo;
        public int RingNo
        {
            get { return _ringNo; }
            set { _ringNo = value; }
        }

        private int _runningOrder;
        public int RunningOrder
        {
            get { return _runningOrder; }
            set { _runningOrder = value; }
        }

        private string _specialRequest;
        public string SpecialRequest
        {
            get { return _specialRequest; }
            set { _specialRequest = value; }
        }

        public clsDogClass(int entryID)
        {
            _entryID = entryID;
        }

        internal void Load()
        {
            _dogClassID = GetDogClassID();
            Load(_dogClassID);
        }

        internal void Load(int dogClassID)
        {
            _dogClassID = dogClassID;

            try
            {
                string sqlCmd = @"SELECT EntryID
                              ,DogID
                              ,ShowClassID
                              ,PreferredPart
                              ,ShowFinalClassID
                              ,RingNo
                              ,RunningOrder
                              ,SpecialRequest
                          FROM tblDogClasses
                          WHERE DogClassID = @DogClassID AND EntryID = @EntryID";

                cn = new SqlConnection(_connString);
                cn.Open();
                SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
                adr.SelectCommand.CommandType = CommandType.Text;
                adr.SelectCommand.Parameters.AddWithValue("@DogClassID", _dogClassID);
                adr.SelectCommand.Parameters.AddWithValue("@EntryID", _entryID);
                DataSet ds = new DataSet();
                adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    {
                        _dogID = int.Parse(ds.Tables[0].Rows[0]["DogID"].ToString());
                        _showClassID = int.Parse(ds.Tables[0].Rows[0]["ShowClassID"].ToString());
                        _preferredPart = int.Parse(ds.Tables[0].Rows[0]["PreferredPart"].ToString());
                        _showFinalClassID = int.Parse(ds.Tables[0].Rows[0]["ShowFinalClassID"].ToString());
                        _ringNo = int.Parse(ds.Tables[0].Rows[0]["RingNo"].ToString());
                        _runningOrder = int.Parse(ds.Tables[0].Rows[0]["RunningOrder"].ToString());
                        _specialRequest = ds.Tables[0].Rows[0]["SpecialRequest"].ToString();
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
                _dogClassID = GetDogClassID();

                string sqlCmd = "";
                if (_dogClassID == 0)
                {
                    sqlCmd = @"INSERT INTO tblDogClasses
                            (DogClassID
                            ,EntryID
                            ,DogID
                            ,ShowClassID
                            ,PreferredPart
                            ,ShowFinalClassID
                            ,RingNo
                            ,RunningOrder
                            ,SpecialRequest)
                         VALUES
                           (@DogClassID
                            ,@EntryID
                            ,@DogID
                            ,@ShowClassID
                            ,@PreferredPart
                            ,@ShowFinalClassID
                            ,@RingNo
                            ,@RunningOrder
                            ,@SpecialRequest)";
                }
                else
                {
                    sqlCmd = @"UPDATE tblDogClasses
                           SET DogID = @DogID
                           ,ShowClassID = @ShowClassID
                           ,PreferredPart = @PreferredPart
                           ,ShowFinalClassID = @ShowFinalClassID
                           ,RingNo = @RingNo
                           ,RunningOrder = @RunningOrder
                           ,SpecialRequest = @SpecialRequest
                        WHERE DoogClassID = @DogClassID AND EntryID = @EntryID";
                }

                cn = new SqlConnection(_connString);
                SqlCommand command = new SqlCommand(sqlCmd, cn);
                cn.Open();
                if (_dogClassID == 0)
                {
                    _dogClassID = GetNextDogClassID();
                }
                command.Parameters.AddWithValue("@DogClassID", _dogClassID);
                command.Parameters.AddWithValue("@EntryID", _entryID);
                command.Parameters.AddWithValue("@DogID", _dogID);
                command.Parameters.AddWithValue("@ShowClassID", _showClassID);
                command.Parameters.AddWithValue("@PreferredPart", _preferredPart);
                command.Parameters.AddWithValue("@ShowFinalClassID", _showFinalClassID);
                command.Parameters.AddWithValue("@RingNo", _ringNo);
                command.Parameters.AddWithValue("@RunningOrder", _runningOrder);
                command.Parameters.AddWithValue("@SpecialRequest", _specialRequest);
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

        private int GetDogClassID()
        {
            int retVal = 0;
            SqlCommand cmd = null;
            SqlConnection cn = new SqlConnection(_connString);

            try
            {
                string sqlCmd = @"SELECT MAX(DogClassID) From tblDogClasses
                        WHERE EntryID = @EntryID AND DogID = @DogID AND ShowClassID = @ShowClassID";
                cn.Open();
                cmd = new SqlCommand(sqlCmd, cn);
                cmd.Parameters.AddWithValue("@EntryID", _entryID);
                cmd.Parameters.AddWithValue("@DogID", _dogID);
                cmd.Parameters.AddWithValue("@ShowClassID", _showClassID);

                var id = cmd.ExecuteScalar();
                if (!id.Equals(DBNull.Value)) retVal = Convert.ToInt32(id);
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

        private int GetNextDogClassID()
        {
            int retVal = 0;
            SqlCommand cmd = null;
            SqlConnection cn = new SqlConnection(_connString);

            try
            {
                string sqlCmd = "SELECT MAX(DogClassID) From tblDogClasses";
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

        internal DataTable GetEnteredClasses()
        {
            DataTable retVal = null;

            string sqlCmd = @"SELECT e.EntryID
                              ,e.OwnerID
                              ,e.ShowID
                              ,Catalogue
                              ,OvernightCamping
                              ,Overpayment
                              ,Underpayment
                              ,OfferOfHelp
                              ,HelpDetails
                              ,WitholdAddress
                              ,SendRunningOrder
                              ,EntryDate
	                          ,DogClassID
                              ,dc.DogID
                              ,dc.ShowClassID
                              ,PreferredPart
                              ,ShowFinalClassID
                              ,RingNo
                              ,RunningOrder
                              ,SpecialRequest
                              ,KCName
                              ,PetName
                              ,BreedID
                              ,GenderID
                              ,RegNo
                              ,ATCNo
                              ,DateOfBirth
                              ,Descr
                              ,NLWU
                              ,LowestClass
	                          ,cn.Class_Name_ID
                              ,Class_Name_Description
                              ,Weighting
                              ,Gender
                              ,ClassNo
                              ,ClassCap
                              ,Judges
                          FROM tblEntries e 
                          INNER JOIN tblDogClasses dc ON e.EntryID = dc.EntryID
                          INNER JOIN tblDogs d ON dc.DogID = d.DogID
                          INNER JOIN tblShowClasses sc ON e.ShowID = sc.ShowID
                          INNER JOIN lkpClass_Names cn ON sc.Class_Name_ID = cn.Class_Name_ID
                          AND sc.ShowClassID = dc.ShowClassID
                          WHERE e.EntryID = @EntryID
                          ORDER BY Weighting, ClassNo, KCName";

            cn = new SqlConnection(_connString);
            cn.Open();
            SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
            adr.SelectCommand.CommandType = CommandType.Text;
            adr.SelectCommand.Parameters.AddWithValue("@EntryID", _entryID);
            DataSet ds = new DataSet();
            adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = ds.Tables[0];
            }

            return retVal;
        }

        public static void DeleteDogEntriesForClass(int entryID, int classID)
        {
            SqlConnection cn = null;
            try
            {
                string sqlCmd = "";
                sqlCmd = @"DELETE tblDogClasses
                    WHERE EntryID = @EntryID AND ShowClassID = @ClassID";

                cn = new SqlConnection(_connString);
                SqlCommand command = new SqlCommand(sqlCmd, cn);
                cn.Open();
                command.Parameters.AddWithValue("@EntryID", entryID);
                command.Parameters.AddWithValue("@ClassID", classID);
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
    }
}