using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace nocf_entries.App_Code
{
    public class clsShowType
    {
        private static string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

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

        public clsShowType(int showTypeID, string showTypeDescription)
        {
            _showTypeID = showTypeID;
            _showTypeDescription = showTypeDescription;
        }

        public static List<clsShowType> GetShowTypeList()
        {
            List<clsShowType> retVal = null;

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
                retVal = new List<clsShowType>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    retVal.Add(new clsShowType(int.Parse(row["ShowTypeID"].ToString()), row["ShowTypeDescription"].ToString()));
                }
            }

            return retVal;
        }
    }
}