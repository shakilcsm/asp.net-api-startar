using System;

namespace API.Requests.Registration;

public class SocialRegistrationRequest : BasicRegistrationRequest
{
    public required string Id { get; set; }
    public required string Provider { get; set; }
    public required string? Avatar { get; set; }
    public required string? AvatarBlurHash { get; set; }
}
