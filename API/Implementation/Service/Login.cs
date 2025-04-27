using System.Data;
using System.Data.Common;
using API.Framework.Entity;
using API.Framework.Service;
using API.Framework.Shared;
using API.Framework.Shared.Error;
using LanguageExt;
using Microsoft.Data.SqlClient;

namespace API.Implementation.Service;

public class LoginServiceImpl : LoginService
{
    Database _db;
    InfluencerService _influencer;
    BrandService _brand;

    public LoginServiceImpl(Database db, InfluencerService influencer, BrandService brand)
    {
        _db = db;
        _influencer = influencer;
        _brand = brand;
    }
    public Either<Influencer, Brand> Password(string username, string password)
    {
        try
        {
            string sqlQuery = @"
DECLARE @UserName NVARCHAR(50) = '{0}';
DECLARE @Password NVARCHAR(50) = '{1}';

-- Check if username exists
IF NOT EXISTS (SELECT 1 FROM Login WHERE UserName = @UserName)
BEGIN
    RAISERROR('LOGIN_USER_NOT_FOUND', 16, 1);
    RETURN;
END

-- Check if password matches
IF NOT EXISTS (SELECT 1 FROM Login WHERE UserName = @UserName AND Password = @Password)
BEGIN
    RAISERROR('LOGIN_WRONG_PASSWORD', 16, 1);
    RETURN;
END

-- Success
SELECT UserName
FROM Login
WHERE UserName = @UserName AND Password = @Password;";
            sqlQuery = string.Format(sqlQuery, username, password);
            using (DataSet data = _db.GetDataSet(sqlQuery))
            {
                DataRow row = data.Tables[0].Rows[0];
                string realUsername = (string)row["username"];
                string role = (string)row["role"];
                if (role.ToLower() == Role.Influencer.ToLower())
                {
                    Influencer influencer = _influencer.ByUsername(realUsername);
                    return influencer;
                }
                else
                {
                    Brand brand = _brand.ByUsername(realUsername);
                    return brand;
                }
            }
        }
        catch (DbException ex)
        {
            if (ex is not SqlException)
            {
                throw new UnknowDatabaseException(ex.Message);
            }
            if (ex.Message.Contains(DBErrorCodes.LOGIN_USER_NOT_FOUND))
            {
                throw new UserNotExistsException("No account associated with following username");
            }
            else if (ex.Message.Contains(DBErrorCodes.LOGIN_WRONG_PASSWORD))
            {
                throw new UserNotExistsException("Wrong password. Please try again.");
            }
            else
            {
                throw new UnknowDatabaseException(ex.Message);
            }
        }

    }

    public string? Recovery(string username)
    {
        throw new NotImplementedException();
    }

    public Either<Influencer, Brand> Social(string username, string provider)
    {
        throw new NotImplementedException();
    }
}
