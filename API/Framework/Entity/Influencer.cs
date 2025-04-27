using API.Framework.Shared;

namespace API.Framework.Entity;

public class Influencer : User
{
    public required Lookup? Gender { get; set; }
    public required DateTime? Dob { get; set; }
    public required string? Avatar { get; set; }
    public required string? AvatarBlurHash { get; set; }
    public required bool AcceptPR { get; set; }
}
