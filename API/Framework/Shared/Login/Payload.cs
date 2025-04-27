using System;
using API.Framework.Entity;
using LanguageExt;

namespace API.Framework.Shared;

public class LoginPayload
{
    public required string Username { get; set; }
    public required string Token { get; set; }
    public required DateTime ExpiresAt { get; set; }
    public required Either<Influencer, Brand> Profile { get; set; }

}
