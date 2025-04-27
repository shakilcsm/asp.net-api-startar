using System;
using System.Data;
using System.Data.Common;
using API.Framework.Entity;
using API.Framework.Service;
using API.Framework.Shared;
using API.Framework.Shared.Error;
using Microsoft.Data.SqlClient;

namespace API.Implementation.Service;

public class InfluencerServiceImpl : InfluencerService
{
    Database _db;

    public InfluencerServiceImpl(Database db)
    {
        _db = db;
    }

    public Influencer ByUsername(string username)
    {
        try
        {
            string sqlQuery = "select * from Influencer where username = '{0}";
            sqlQuery = string.Format(sqlQuery, username);
            using (DataSet data = _db.GetDataSet(sqlQuery))
            {
                DataRow row = data.Tables[0].Rows[0];
                Influencer influencer = new Influencer
                {
                    Id = (int)row["id"],
                    UserId = (Guid)row["guid"],
                    UserName = (string)row["username"],
                    Name = (string)row["name"],
                    Phone = row["phone"] != DBNull.Value ? (string)row["phone"] : null,
                    Email = row["email"] != DBNull.Value ? (string)row["email"] : null,
                    Bio = row["bio"] != DBNull.Value ? (string)row["bio"] : null,
                    IsVerified = (bool)row["verified"],
                    City = row["cityId"] != DBNull.Value && row["cityName"] != null ? new Lookup
                    {
                        Id = (int)row["cityId"],
                        Text = (string)row["cityName"]
                    } : null,
                    Country = row["countryId"] != DBNull.Value && row["countryName"] != null ? new Lookup
                    {
                        Id = (int)row["countryId"],
                        Text = (string)row["countryName"]
                    } : null,
                    Gender = row["genderId"] != DBNull.Value && row["genderName"] != null ? new Lookup
                    {
                        Id = (int)row["genderId"],
                        Text = (string)row["genderName"]
                    } : null,
                    Dob = row["dob"] != DBNull.Value ? (DateTime?)row["dob"] : null,
                    Avatar = row["avatar"] != DBNull.Value ? (string)row["avatar"] : null,
                    AvatarBlurHash = row["avatarBlurHash"] != DBNull.Value ? (string)row["avatarBlurHash"] : null,
                    AcceptPR = (bool)row["acceptPR"],
                };
                return influencer;
            }
        }
        catch (DbException ex)
        {
            if (ex.InnerException is not SqlException)
            {
                throw new UnknowDatabaseException(ex.Message);
            }
            if (ex.Message.Contains(DBErrorCodes.INFLUENCER_USER_NOT_FOUND))
            {
                throw new UserNotExistsException("No account associated with following username");
            }
            else
            {
                throw new UnknowDatabaseException(ex.Message);
            }
        }
    }
}
