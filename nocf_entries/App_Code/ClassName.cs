using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace nocf_entries.App_Code
{
    public class ClassName
    {
        private static string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private SqlConnection cn = null;

        private int _class_Name_ID;
        public int Class_Name_ID
        {
            get { return _class_Name_ID; }
            set { _class_Name_ID = value; }
        }

        private string _class_Name_Description;
        public string Class_Name_Description
        {
            get { return _class_Name_Description; }
            set { _class_Name_Description = value; }
        }

        public ClassName()
        {

        }

        public ClassName(int classNameID, string classNameDescription)
        {
            _class_Name_ID = classNameID;
            _class_Name_Description = classNameDescription;
        }

        internal void Load(int classNameID)
        {
            _class_Name_ID = classNameID;

            try
            {
                string sqlCmd = @"SELECT [Class_Name_ID],[Class_Name_Description]
                FROM lkpClass_Names WHERE Class_Name_ID = @ClassNameID";

                cn = new SqlConnection(_connString);
                cn.Open();
                SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
                adr.SelectCommand.CommandType = CommandType.Text;
                adr.SelectCommand.Parameters.AddWithValue("@ClassNameID", classNameID);
                DataSet ds = new DataSet();
                adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    {
                        _class_Name_Description = ds.Tables[0].Rows[0]["Class_Name_Description"].ToString();
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
                if (_class_Name_ID == 0)
                {
                    sqlCmd = @"INSERT INTO [dbo].[lkpClass_Names]
                       ([Class_Name_Description])
                 VALUES
                       (@ClassNameDescription)";
                }
                else
                {
                    sqlCmd = @"UPDATE [dbo].[lkpClass_Names]
                   SET [Class_Name_Description] = @ClassNameDescription
                   WHERE Class_Name_ID = @ClassNameID";
                }

                cn = new SqlConnection(_connString);
                SqlCommand command = new SqlCommand(sqlCmd, cn);
                cn.Open();
                command.Parameters.AddWithValue("@ClassNameDescription", _class_Name_Description);
                if (_class_Name_ID != 0)
                {
                    command.Parameters.AddWithValue("@ClassNameID", _class_Name_ID);
                }
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

        public static DataTable GetClassNameList()
        {
            DataTable retVal = null;

            string sqlCmd = @"SELECT Class_Name_ID
                ,Class_Name_Description FROM lkpClass_Names
                WHERE Class_Name_ID > 1";

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

        public static DataTable GetSelectedClassesForShow(int showID)
        {
            DataTable retVal = null;

            string sqlCmd = @"SELECT lcn.Class_Name_ID, Class_Name_Description, ShowClassID,  ClassNo, 
                	Case WHEN Judges IS NULL THEN 'Not Set' ELSE Judges END AS Judges,
	                CASE Gender WHEN 1 THEN 'Dog' WHEN 2 THEN 'Bitch' WHEN 3 THEN 'Dog & Bitch' END as GenderDescr,
	                Case WHEN ClassCap IS NULL THEN 'No Limit' ELSE CONVERT(varchar(10), ClassCap) END AS ClassCap 
                    FROM lkpClass_Names lcn INNER JOIN tblShowClasses sc 
                    on lcn.Class_Name_ID = sc.Class_Name_ID AND ShowID = @ShowID
                    WHERE lcn.Class_Name_ID > 1
	                ORDER BY ClassNo";

            SqlConnection cn = new SqlConnection(_connString);
            cn.Open();
            SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
            adr.SelectCommand.CommandType = CommandType.Text;
            adr.SelectCommand.Parameters.AddWithValue("@ShowID", showID);
            DataSet ds = new DataSet();
            adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = ds.Tables[0];
            }

            return retVal;
        }

        public static DataTable GetClassesForSelection(int showID)
        {
            DataTable retVal = null;

            string sqlCmd = @"SELECT lcn.Class_Name_ID, Class_Name_Description, ShowClassID,  ClassNo, Gender 
                    FROM lkpClass_Names lcn LEFT JOIN tblShowClasses sc 
                    on lcn.Class_Name_ID = sc.Class_Name_ID AND ShowID = @ShowID
                    WHERE lcn.Class_Name_ID > 1";

            SqlConnection cn = new SqlConnection(_connString);
            cn.Open();
            SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
            adr.SelectCommand.CommandType = CommandType.Text;
            adr.SelectCommand.Parameters.AddWithValue("@ShowID", showID);
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

        public static void SaveSelected(int showClassID, int showID, int classNameID, int classNo, int gender)
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
                       ,Class_Name_ID
                       ,ClassNo
                       ,Gender)
                 VALUES
                       (@ShowClassID
                       ,@ShowID
                       ,@ClassNameID
                       ,@ClassNo
                       ,@Gender)";
                }
                else
                {
                    sqlCmd = @"UPDATE tblShowClasses
                   SET ShowID = @ShowID
                      ,Class_Name_ID = @ClassNameID
                      ,ClassNo = @ClassNo
                      ,Gender = @Gender
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
                command.Parameters.AddWithValue("@Gender", gender);
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

        private static int GetNextShowClassID()
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

        public static DataTable GetShowClassByID(int showClassID)
        {
            DataTable retVal = null;

            string sqlCmd = @"SELECT lcn.Class_Name_ID, Class_Name_Description, ShowClassID,  ClassNo, Gender,
                	Case WHEN Judges IS NULL THEN 'Not Set' ELSE Judges END AS Judges,
	                CASE Gender WHEN 1 THEN 'Dog' WHEN 2 THEN 'Bitch' WHEN 3 THEN 'Dog & Bitch' END as GenderDescr,
	                Case WHEN ClassCap IS NULL THEN 'No Limit' ELSE CONVERT(varchar(10), ClassCap) END AS ClassCap 
                    FROM lkpClass_Names lcn INNER JOIN tblShowClasses sc 
                    on lcn.Class_Name_ID = sc.Class_Name_ID AND ShowClassID = @ShowClassID
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
    }

    public class ShowClass
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

        private int _genderID;

        public int GenderID
        {
            get { return _genderID; }
            set { _genderID = value; }
        }

        private int _classCap;

        public int ClassCap
        {
            get { return _classCap; }
            set { _classCap = value; }
        }

        private string _judges;

        public string Judges
        {
            get { return _judges; }
            set { _judges = value; }
        }


        public ShowClass(int showClassID)
        {
            _showClassID = showClassID;
        }

        internal void Save()
        {
            SqlConnection cn = null;

            try
            {
                string sqlCmd = "";
                    sqlCmd = @"UPDATE tblShowClasses
                   SET ShowID = @ShowID
                      ,Class_Name_ID = @ClassNameID
                      ,ClassNo = @ClassNo
                      ,Gender = @Gender
                      ,ClassCap = @ClassCap
                      ,Judges = @Judges
                    WHERE ShowClassID = @ShowClassID";

                cn = new SqlConnection(_connString);
                SqlCommand command = new SqlCommand(sqlCmd, cn);
                cn.Open();
                command.Parameters.AddWithValue("@ShowClassID", _showClassID);
                command.Parameters.AddWithValue("@ShowID", _showID);
                command.Parameters.AddWithValue("@ClassNameID", _classNameID);
                command.Parameters.AddWithValue("@ClassNo", _classNo);
                command.Parameters.AddWithValue("@Gender", _genderID);
                command.Parameters.AddWithValue("@ClassCap", _classCap);
                command.Parameters.AddWithValue("@Judges", _judges);
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
