using System;
using LanguageExt.TypeClasses;

namespace API.Framework.Shared;


public static class AppConfig
{
    public static readonly JwtAppConfig Jwt = new JwtAppConfig();
    public static readonly ConnectionStringAppConfig ConnectionString = new ConnectionStringAppConfig();

    public sealed class JwtAppConfig
    {
        public string Secret => "Jwt:Secret";
        public string Audience => "Jwt:Audience";
        public string Issuer => "Jwt:Issuer";
    }

    public sealed class ConnectionStringAppConfig
    {
        public string Dev => "ConnectionStrings:Dev";
        public string Production => "ConnectionStrings:Production";
    }
}

public class DBErrorCodes
{
    public const string LOGIN_USER_NOT_FOUND = "LOGIN_USER_NOT_FOUND";
    public const string LOGIN_WRONG_PASSWORD = "LOGIN_WRONG_PASSWORD";
    public const string INFLUENCER_USER_NOT_FOUND = "INFLUENCER_USER_NOT_FOUND";
    public const string BRAND_USER_NOT_FOUND = "BRAND_USER_NOT_FOUND";
}

public class Role
{
    public const string Influencer = "Influencer";
    public const string Brand = "Brand";
}
