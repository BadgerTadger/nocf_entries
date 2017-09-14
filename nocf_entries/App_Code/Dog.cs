using System;
using System.Configuration;
using System.Data.SqlClient;

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

    public Dog(string ownerID)
    {
        _ownerId = ownerID;
    }

    public int GetNextDogID()
    {
        int retVal = 0;
        SqlCommand cmd = null;

        try
        {
            string sqlCmd = "SELECT MAX(DogID) From tblDogs";
            cn = new SqlConnection(_connString);
            cn.Open();
            cmd = new SqlCommand(sqlCmd, cn);

            retVal = Convert.ToInt32(cmd.ExecuteScalar());
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
