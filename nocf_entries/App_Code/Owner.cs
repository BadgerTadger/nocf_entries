using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class Owner
{
    private static string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    private SqlConnection cn = null;

    private string _id;
    public string ID
    {
        get { return _id; }
        set { _id = value; }
    }

    private string _title;
    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    private string _firstName;
    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    private string _lastName;
    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    private string _kcName;
    public string KCName
    {
        get { return _kcName; }
        set { _kcName = value; }
    }

    private string _userName;
    public string UserName
    {
        get { return _userName; }
        set { _userName = value; }
    }

    private string _address1;
    public string Address1
    {
        get { return _address1; }
        set { _address1 = value; }
    }

    private string _address2;
    public string Address2
    {
        get { return _address2; }
        set { _address2 = value; }
    }

    private string _town;
    public string Town
    {
        get { return _town; }
        set { _town = value; }
    }

    private string _county;
    public string County
    {
        get { return _county; }
        set { _county = value; }
    }

    private string _postcode;
    public string Postcode
    {
        get { return _postcode; }
        set { _postcode = value; }
    }

    private string _country;
    public string Country
    {
        get { return _country; }
        set { _country = value; }
    }

    private string _email;
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    private string _phone;
    public string Phone
    {
        get { return _phone; }
        set { _phone = value; }
    }

    private string _mobile;
    public string Mobile
    {
        get { return _mobile; }
        set { _mobile = value; }
    }

    public Owner(string id, string username, string email)
    {
        _id = id;
        _userName = username;
        _email = email;
    }

    internal void Load()
    {
        try
        {
            string sqlCmd = @"SELECT Title, FirstName, LastName, KCName, Address_1, Address_2, Town, County, Postcode,
                Country, Phone, Mobile FROM tblOwners WHERE ID = @ID";

            cn = new SqlConnection(_connString);
            cn.Open();
            SqlDataAdapter adr = new SqlDataAdapter(sqlCmd, cn);
            adr.SelectCommand.CommandType = CommandType.Text;
            adr.SelectCommand.Parameters.Add(new SqlParameter
            {
                ParameterName = "@ID",
                Value = _id,
                SqlDbType = SqlDbType.NVarChar,
                Size = 128
            });
            DataSet ds = new DataSet();
            adr.Fill(ds); //opens and closes the DB connection automatically !! (fetches from pool)
            if (ds.Tables[0].Rows.Count > 0)
            {
                {
                    _title = ds.Tables[0].Rows[0]["Title"].ToString();
                    _firstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    _lastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    _kcName = ds.Tables[0].Rows[0]["KCName"].ToString();
                    _address1 = ds.Tables[0].Rows[0]["Address_1"].ToString();
                    _address2 = ds.Tables[0].Rows[0]["Address_2"].ToString();
                    _town = ds.Tables[0].Rows[0]["Town"].ToString();
                    _county = ds.Tables[0].Rows[0]["County"].ToString();
                    _postcode = ds.Tables[0].Rows[0]["Postcode"].ToString();
                    _country = ds.Tables[0].Rows[0]["Country"].ToString();
                    _phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                    _mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
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
            string sqlCmd = @"UPDATE  tblOwners SET Title = @Title, FirstName = @FirstName, LastName = @LastName, 
                            KCName = @KCName, Address_1 = @Address_1, Address_2 = @Address_2, Town = @Town, 
                            County = @County, Postcode = @Postcode, Country = @Country, Phone = @Phone, 
                            Mobile = @Mobile WHERE ID = @ID";

            cn = new SqlConnection(_connString);
            SqlCommand command = new SqlCommand(sqlCmd, cn);
            cn.Open();
            command.Parameters.AddWithValue("@Title", _title);
            command.Parameters.AddWithValue("@FirstName", _firstName);
            command.Parameters.AddWithValue("@LastName", _lastName);
            command.Parameters.AddWithValue("@KCName", _kcName);
            command.Parameters.AddWithValue("@Address_1", _address1);
            command.Parameters.AddWithValue("@Address_2", _address2);
            command.Parameters.AddWithValue("@Town", _town);
            command.Parameters.AddWithValue("@County", _county);
            command.Parameters.AddWithValue("@Postcode", _postcode);
            command.Parameters.AddWithValue("@Country", _country);
            command.Parameters.AddWithValue("@Phone", _phone);
            command.Parameters.AddWithValue("@Mobile", _mobile);
            command.Parameters.AddWithValue("@ID", _id);
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