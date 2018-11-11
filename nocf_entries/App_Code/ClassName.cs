using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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
                //retVal = new List<ClassName>();
                //foreach (DataRow row in ds.Tables[0].Rows)
                //{
                //    retVal.Add(new ClassName(int.Parse(row["Class_Name_ID"].ToString()), row["Class_Name_Description"].ToString()));
                //}
            }

            return retVal;
        }
    }
}
