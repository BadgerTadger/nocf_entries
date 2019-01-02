using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace nocf_entries.cls
{
    public class clsClassName
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

        private int _weighting;
        public int Weighting
        {
            get { return _weighting; }
            set { _weighting = value; }
        }

        private int _gender;
        public int Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public clsClassName()
        {

        }

        public clsClassName(int classNameID, string classNameDescription, int weighting, int gender)
        {
            _class_Name_ID = classNameID;
            _class_Name_Description = classNameDescription;
            _weighting = weighting;
            _gender = gender;
        }

        internal void Load(int classNameID)
        {
            _class_Name_ID = classNameID;

            try
            {
                string sqlCmd = @"SELECT Class_Name_ID,Class_Name_Description,Weighting,Gender
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
                        int wt = 0;
                        int gender = 0;
                        _class_Name_Description = ds.Tables[0].Rows[0]["Class_Name_Description"].ToString();
                        int.TryParse(ds.Tables[0].Rows[0]["Weighting"].ToString(), out wt);
                        _weighting = wt;
                        int.TryParse(ds.Tables[0].Rows[0]["Gender"].ToString(), out gender);
                        _gender = gender;
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
                    sqlCmd = @"INSERT INTO lkpClass_Names
                       (Class_Name_Description,Weighting,Gender)
                 VALUES
                       (@ClassNameDescription,@Weighting,@Gender)";
                }
                else
                {
                    sqlCmd = @"UPDATE lkpClass_Names
                   SET Class_Name_Description = @ClassNameDescription,
                    Weighting = @Weighting,
                    Gender = @Gender
                   WHERE Class_Name_ID = @ClassNameID";
                }

                cn = new SqlConnection(_connString);
                SqlCommand command = new SqlCommand(sqlCmd, cn);
                cn.Open();
                command.Parameters.AddWithValue("@ClassNameDescription", _class_Name_Description);
                command.Parameters.AddWithValue("@Weighting", _weighting);
                command.Parameters.AddWithValue("@Gender", _gender);
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
                ,Class_Name_Description, Weighting,
	            CASE Gender WHEN 1 THEN 'Dog' WHEN 2 THEN 'Bitch' WHEN 3 THEN 'Dog & Bitch' END as GenderDescr
                FROM lkpClass_Names
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

        public static DataTable GetClassesForSelection(clsShow show)
        {
            DataTable retVal = null;

            string sqlCmd = @"SELECT lcn.Class_Name_ID, Class_Name_Description, ShowClassID,  ClassNo, Gender, 
                    @DefaultClassCost as DefaultClassCost, @DefaultClassCap as DefaultClassCap, @DefaultDogsPerClassPart as DefaultDogsPerClassPart
                    FROM lkpClass_Names lcn LEFT JOIN tblShowClasses sc 
                    on lcn.Class_Name_ID = sc.ClassNameID AND sc.ShowID = @ShowID                
                    WHERE lcn.Class_Name_ID > 2";

            SqlConnection cn = new SqlConnection(_connString);
            cn.Open();
            SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
            adr.SelectCommand.CommandType = CommandType.Text;
            adr.SelectCommand.Parameters.AddWithValue("@ShowID", show.ShowID);
            adr.SelectCommand.Parameters.AddWithValue("@DefaultClassCost", show.DefaultClassCost);
            adr.SelectCommand.Parameters.AddWithValue("@DefaultClassCap", show.MaxDogsPerClass);
            adr.SelectCommand.Parameters.AddWithValue("@DefaultDogsPerClassPart", show.DefaultDogsPerClassPart);
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
