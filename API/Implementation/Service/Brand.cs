using System;
using System.Data;
using System.Data.Common;
using API.Framework.Entity;
using API.Framework.Service;
using API.Framework.Shared;
using API.Framework.Shared.Error;
using Microsoft.Data.SqlClient;

namespace API.Implementation.Service;

public class BrandServiceImpl : BrandService
{
    Database _db;

    public BrandServiceImpl(Database db)
    {
        _db = db;
    }

    public Brand ByUsername(string username)
    {
        try
        {
            string sqlQuery = "select * from Brand where username = '{0}";
            sqlQuery = string.Format(sqlQuery, username);
            using (DataSet data = _db.GetDataSet(sqlQuery))
            {
                DataRow row = data.Tables[0].Rows[0];
                Brand Brand = new Brand
                {
                    Id = (int)row["id"],
                    UserId = (Guid)row["guid"],
                    UserName = (string)row["username"],
                    Name = (string)row["name"],
                    BrandName = (string)row["brandName"],
                    Phone = row["phone"] != DBNull.Value ? (string)row["phone"] : null,
                    Email = row["email"] != DBNull.Value ? (string)row["email"] : null,
                    Website = row["website"] != DBNull.Value ? (string)row["website"] : null,
                    SocialLink = row["socialLink"] != DBNull.Value ? (string)row["socialLink"] : null,
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
                    BrandLogo = row["logo"] != DBNull.Value ? (string)row["logo"] : null,
                    BrandLogoBlurHash = row["logoBlurHash"] != DBNull.Value ? (string)row["logoBlurHash"] : null,
                };
                return Brand;
            }
        }
        catch (DbException ex)
        {
            if (ex.InnerException is not SqlException)
            {
                throw new UnknowDatabaseException(ex.Message);
            }
            if (ex.Message.Contains(DBErrorCodes.BRAND_USER_NOT_FOUND))
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
