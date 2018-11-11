using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace nocf_entries.App_Code
{
    public class Dog
    {
        private static string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private SqlConnection cn = null;

        private int _dogID;
        public int DogID
        {
            get { return _dogID; }
            set { _dogID = value; }
        }

        private string _ownerId;
        public string OwnerID
        {
            get { return _ownerId; }
            set { _ownerId = value; }
        }

        private string _kcName;
        public string KCName
        {
            get { return _kcName; }
            set { _kcName = value; }
        }

        private string _petName;
        public string PetName
        {
            get { return _petName; }
            set { _petName = value; }
        }

        private int _breedID;
        public int BreedID
        {
            get { return _breedID; }
            set { _breedID = value; }
        }

        private string _breedDescr;
        public string BreedDescr
        {
            get { return _breedDescr; }
            set { _breedDescr = value; }
        }

        private int _genderID;
        public int GenderID
        {
            get { return _genderID; }
            set { _genderID = value; }
        }

        private string _genderDescr;
        public string GenderDescr
        {
            get { return _genderDescr; }
            set { _genderDescr = value; }
        }

        private string _regNo;
        public string RegNo
        {
            get { return _regNo; }
            set { _regNo = value; }
        }

        private string _atcNo;
        public string ATCNo
        {
            get { return _atcNo; }
            set { _atcNo = value; }
        }

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }

        private string _descr;
        public string Descr
        {
            get { return _descr; }
            set { _descr = value; }
        }

        private bool _nlwu;
        public bool NLWU
        {
            get { return _nlwu; }
            set { _nlwu = value; }
        }

        public Dog(string ownerID)
        {
            _ownerId = ownerID;
        }

        internal void Load(int dogId)
        {
            _dogID = dogId;

            try
            {
                string sqlCmd = @"SELECT [KCName]
                ,[PetName]
                ,BreedID
                ,[Dog_Breed_Description] as Breed
                ,GenderID
                ,CASE [GenderID] WHEN 1 THEN 'Dog' ELSE 'Bitch' END as Gender
                ,[RegNo]
                ,[ATCNo]
                ,[DateOfBirth]
                ,[Descr]
                ,[NLWU]
                FROM tblDogs d
                inner join lkpDog_Breeds db on d.BreedID = db.Dog_Breed_ID 
                WHERE OwnerID = @OwnerID AND DogID = @DogID";

                cn = new SqlConnection(_connString);
                cn.Open();
                SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
                adr.SelectCommand.CommandType = CommandType.Text;
                adr.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@OwnerID",
                    Value = _ownerId,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 128
                });
                adr.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@DogID",
                    Value = _dogID,
                    SqlDbType = SqlDbType.Int
                });
                DataSet ds = new DataSet();
                adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    {
                        _kcName = ds.Tables[0].Rows[0]["KCName"].ToString();
                        _petName = ds.Tables[0].Rows[0]["PetName"].ToString();
                        _breedID = int.Parse(ds.Tables[0].Rows[0]["BreedID"].ToString());
                        _breedDescr = ds.Tables[0].Rows[0]["Breed"].ToString();
                        _genderID = int.Parse(ds.Tables[0].Rows[0]["GenderID"].ToString());
                        _genderDescr = ds.Tables[0].Rows[0]["Gender"].ToString();
                        _regNo = ds.Tables[0].Rows[0]["RegNo"].ToString();
                        _atcNo = ds.Tables[0].Rows[0]["ATCNo"].ToString();
                        _dateOfBirth = DateTime.Parse(ds.Tables[0].Rows[0]["DateOfBirth"].ToString());
                        _descr = ds.Tables[0].Rows[0]["Descr"].ToString();
                        _nlwu = ds.Tables[0].Rows[0]["NLWU"].ToString() == "1";
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
                if (_dogID == 0)
                {
                    sqlCmd = @"INSERT INTO [dbo].[tblDogs]
                       ([DogID]
                       ,[OwnerID]
                       ,[KCName]
                       ,[PetName]
                       ,[BreedID]
                       ,[GenderID]
                       ,[RegNo]
                       ,[ATCNo]
                       ,[DateOfBirth]
                       ,[Descr]
                       ,[NLWU])
                 VALUES
                       (@DogID
                       ,@OwnerID
                       ,@KCName
                       ,@PetName
                       ,@BreedID
                       ,@GenderID
                       ,@RegNo
                       ,@ATCNo
                       ,@DateOfBirth
                       ,@Descr
                       ,@NLWU)";
                }
                else
                {
                    sqlCmd = @"UPDATE [dbo].[tblDogs]
                   SET [KCName] = @KCName
                      ,[PetName] = @PetName
                      ,[BreedID] = @BreedID
                      ,[GenderID] = @GenderID
                      ,[RegNo] = @RegNo
                      ,[ATCNo] = @ATCNo
                      ,[DateOfBirth] = @DateOfBirth
                      ,[Descr] = @Descr
                      ,[NLWU] = @NLWU WHERE DogID = @DogID";
                }

                cn = new SqlConnection(_connString);
                SqlCommand command = new SqlCommand(sqlCmd, cn);
                cn.Open();
                if (_dogID == 0)
                {
                    _dogID = GetNextDogID();
                    command.Parameters.AddWithValue("@OwnerID", _ownerId);
                }
                command.Parameters.AddWithValue("@DogID", _dogID);
                command.Parameters.AddWithValue("@KCName", _kcName);
                command.Parameters.AddWithValue("@PetName", _petName);
                command.Parameters.AddWithValue("@BreedID", _breedID);
                command.Parameters.AddWithValue("@GenderID", _genderID);
                command.Parameters.AddWithValue("@RegNo", _regNo);
                command.Parameters.AddWithValue("@ATCNo", _atcNo);
                command.Parameters.AddWithValue("@DateOfBirth", _dateOfBirth);
                command.Parameters.AddWithValue("@Descr", _descr);
                command.Parameters.AddWithValue("@NLWU", _nlwu);
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

        public static int GetNextDogID()
        {
            int retVal = 0;
            SqlCommand cmd = null;
            SqlConnection cn = new SqlConnection(_connString);

            try
            {
                string sqlCmd = "SELECT MAX(DogID) From tblDogs";
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

        internal DataTable GetDogList()
        {
            DataTable retVal = null;

            string sqlCmd = @"SELECT [DogID]
          ,[OwnerID]
          ,[KCName]
          ,[PetName]
          ,[Dog_Breed_Description] as Breed
          ,CASE [GenderID] WHEN 1 THEN 'Dog' ELSE 'Bitch' END as Gender
          ,[RegNo]
          ,[ATCNo]
          ,[DateOfBirth]
          ,[Descr]
          ,[NLWU]
          FROM tblDogs d
          inner join lkpDog_Breeds db on d.BreedID = db.Dog_Breed_ID WHERE OwnerID = @OwnerID";

            cn = new SqlConnection(_connString);
            cn.Open();
            SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
            adr.SelectCommand.CommandType = CommandType.Text;
            adr.SelectCommand.Parameters.AddWithValue("@OwnerID", _ownerId);
            DataSet ds = new DataSet();
            adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = ds.Tables[0];
            }

            return retVal;
        }

        public static List<Breed> GetBreedList()
        {
            List<Breed> retVal = null;

            string sqlCmd = @"SELECT Dog_Breed_ID
              ,Dog_Breed_Description
          FROM lkpDog_Breeds";

            SqlConnection cn = new SqlConnection(_connString);
            cn.Open();
            SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
            adr.SelectCommand.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = new List<Breed>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    retVal.Add(new Breed(row["Dog_Breed_ID"].ToString(), row["Dog_Breed_Description"].ToString()));
                }
            }

            return retVal;
        }
    }

    public class Breed
    {
        public string BreedID { get; set; }
        public string Descr { get; set; }

        public Breed(string breedID, string descr)
        {
            BreedID = breedID;
            Descr = descr;
        }
    }
}