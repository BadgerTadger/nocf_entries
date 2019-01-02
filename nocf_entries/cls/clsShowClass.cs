using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace nocf_entries.cls
{
    public class clsShowClass
    {
        private static string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private int _showClassID;
        public int ShowClassID
        {
            get { return _showClassID; }
            set { _showClassID = value; }
        }

        private int _showID;
        public int ShowID
        {
            get { return _showID; }
            set { _showID = value; }
        }

        private int _classNameID;
        public int ClassNameID
        {
            get { return _classNameID; }
            set { _classNameID = value; }
        }

        private int _classNo;
        public int ClassNo
        {
            get { return _classNo; }
            set { _classNo = value; }
        }

        private int _classCap;
        public int ClassCap
        {
            get { return _classCap; }
            set { _classCap = value; }
        }

        private int _dogsPerClassPart;
        public int DogsPerClassPart
        {
            get { return _dogsPerClassPart; }
            set { _dogsPerClassPart = value; }
        }

        private string _judges;
        public string Judges
        {
            get { return _judges; }
            set { _judges = value; }
        }

        private decimal _classCost;
        public decimal ClassCost
        {
            get { return _classCost; }
            set { _classCost = value; }
        }

        public clsShowClass()
        {

        }

        public clsShowClass(int showClassID)
        {
            _showClassID = showClassID;
        }

        public static DataTable GetShowClassByID(int showClassID)
        {
            DataTable retVal = null;

            string sqlCmd = @"SELECT lcn.Class_Name_ID, Class_Name_Description, ShowClassID,  ClassNo, Gender, ClassCost,
                	Case WHEN Judges IS NULL THEN 'Not Set' ELSE Judges END AS Judges,
	                CASE Gender WHEN 1 THEN 'Dog' WHEN 2 THEN 'Bitch' WHEN 3 THEN 'Dog & Bitch' END as GenderDescr,
	                Case WHEN ClassCap IS NULL THEN 'No Limit' ELSE CONVERT(varchar(10), ClassCap) END AS ClassCap,
                    CASE WHEN DogsPerClassPart IS NULL THEN 0 ELSE DogsPerClassPart END AS DogsPerClassPart
                    FROM lkpClass_Names lcn INNER JOIN tblShowClasses sc 
                    on lcn.Class_Name_ID = sc.ClassNameID AND ShowClassID = @ShowClassID
                    WHERE lcn.Class_Name_ID > 1
	                ORDER BY ClassNo";

            SqlConnection cn = new SqlConnection(_connString);
            cn.Open();
            SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
            adr.SelectCommand.CommandType = CommandType.Text;
            adr.SelectCommand.Parameters.AddWithValue("@ShowClassID", showClassID);
            DataSet ds = new DataSet();
            adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = ds.Tables[0];
            }

            return retVal;
        }

        public static void DeleteClassesForShow(int showID)
        {
            SqlConnection cn = null;
            try
            {
                string sqlCmd = "";
                sqlCmd = @"DELETE tblShowClasses
                    WHERE ShowID = @ShowID";

                cn = new SqlConnection(_connString);
                SqlCommand command = new SqlCommand(sqlCmd, cn);
                cn.Open();
                command.Parameters.AddWithValue("@ShowID", showID);
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

        public static DataTable GetSelectedClassesForShow(int showID, bool showNFC = true)
        {
            DataTable retVal = null;

            string sqlCmd = @"SELECT s.EventID, sc.ShowID, lcn.Class_Name_ID, Class_Name_Description, ShowClassID,ClassNo,ClassCost,
                	Case WHEN Judges IS NULL THEN 'Not Set' ELSE Judges END AS Judges,
	                CASE Gender WHEN 1 THEN 'Dog' WHEN 2 THEN 'Bitch' WHEN 3 THEN 'Dog & Bitch' END as GenderDescr,
	                Case WHEN ClassCap IS NULL THEN 'No Limit' ELSE CONVERT(varchar(10), ClassCap) END AS ClassCap, 
                    CASE WHEN DogsPerClassPart IS NULL THEN 0 ELSE DogsPerClassPart END AS DogsPerClassPart
                    FROM lkpClass_Names lcn INNER JOIN tblShowClasses sc 
                    on lcn.Class_Name_ID = sc.ClassNameID AND sc.ShowID = @ShowID
                    INNER JOIN tblShows s ON s.ShowID = sc.ShowID
                    WHERE lcn.Class_Name_ID > @MinClass
	                ORDER BY ClassNo";

            SqlConnection cn = new SqlConnection(_connString);
            cn.Open();
            SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
            adr.SelectCommand.CommandType = CommandType.Text;
            adr.SelectCommand.Parameters.AddWithValue("@ShowID", showID);
            adr.SelectCommand.Parameters.AddWithValue("@MinClass", showNFC ? 1 : 2);
            DataSet ds = new DataSet();
            adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = ds.Tables[0];
            }

            return retVal;
        }

        public void SaveSelected(int showClassID, int showID, int classNameID, int classNo)
        {
            SqlConnection cn = null;

            try
            {
                string sqlCmd = "";
                if (showClassID == 0)
                {
                    sqlCmd = @"INSERT INTO tblShowClasses
                        (ShowClassID
                        ,ShowID
                        ,ClassNameID
                        ,ClassNo
                        ,ClassCap
                        ,DogsPerClassPart
                        ,Judges
                        ,ClassCost)
                 VALUES
                        (@ShowClassID
                        ,@ShowID
                        ,@ClassNameID
                        ,@ClassNo
                        ,@ClassCap
                        ,@DogsPerClassPart
                        ,@Judges
                        ,@ClassCost)";
                }
                else
                {
                    sqlCmd = @"UPDATE tblShowClasses
                        SET ShowID = @ShowID
                        ,Class_Name_ID = @ClassNameID
                        ,ClassNo = @ClassNo
                        ,ClassCap = @ClassCap
                        ,DogsPerClassPart = @DogsPerClassPart
                        ,Judges = @Judges
                        ,ClassCost = @ClassCost
                    WHERE ShowClassID = @ShowClassID";
                }

                cn = new SqlConnection(_connString);
                SqlCommand command = new SqlCommand(sqlCmd, cn);
                cn.Open();
                if (showClassID == 0)
                {
                    showClassID = GetNextShowClassID();
                }
                command.Parameters.AddWithValue("@ShowClassID", showClassID);
                command.Parameters.AddWithValue("@ShowID", showID);
                command.Parameters.AddWithValue("@ClassNameID", classNameID);
                command.Parameters.AddWithValue("@ClassNo", classNo);
                command.Parameters.AddWithValue("@ClassCap", _classCap);
                command.Parameters.AddWithValue("@DogsPerClassPart", _dogsPerClassPart);
                command.Parameters.AddWithValue("@Judges", _judges);
                command.Parameters.AddWithValue("@ClassCost", _classCost);
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

        internal int GetNextShowClassID()
        {
            int retVal = 0;
            SqlCommand cmd = null;
            SqlConnection cn = new SqlConnection(_connString);

            try
            {
                string sqlCmd = "SELECT MAX(ShowClassID) From tblShowClasses";
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

        internal void AddNFC(int showID)
        {
            SaveSelected(0, showID, 2, 0);
        }

        internal void Save()
        {
            SqlConnection cn = null;

            try
            {
                string sqlCmd = "";
                sqlCmd = @"UPDATE tblShowClasses
                        SET ShowID = @ShowID
                        ,ClassNameID = @ClassNameID
                        ,ClassNo = @ClassNo
                        ,ClassCap = @ClassCap
                        ,DogsPerClassPart = @DogsPerClassPart
                        ,Judges = @Judges
                        ,ClassCost = @ClassCost
                    WHERE ShowClassID = @ShowClassID";

                cn = new SqlConnection(_connString);
                SqlCommand command = new SqlCommand(sqlCmd, cn);
                cn.Open();
                command.Parameters.AddWithValue("@ShowClassID", _showClassID);
                command.Parameters.AddWithValue("@ShowID", _showID);
                command.Parameters.AddWithValue("@ClassNameID", _classNameID);
                command.Parameters.AddWithValue("@ClassNo", _classNo);
                command.Parameters.AddWithValue("@ClassCap", _classCap);
                command.Parameters.AddWithValue("@DogsPerClassPart", _dogsPerClassPart);
                command.Parameters.AddWithValue("@Judges", _judges);
                command.Parameters.AddWithValue("@ClassCost", _classCost);
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