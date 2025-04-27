using System;

namespace API.Requests.Registration;


public class BrandStandardRegistrationRequest : StandardRegistrationRequest
{
    public required string BrandName { get; set; }
}

public class BrandSocialRegistrationRequest : SocialRegistrationRequest
{
    public required string BrandName { get; set; }
}