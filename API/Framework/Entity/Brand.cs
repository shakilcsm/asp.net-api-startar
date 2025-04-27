namespace API.Framework.Entity;

public class Brand : User
{
    public required String BrandName { get; set; }
    public required string? BrandLogo { get; set; }
    public required string? BrandLogoBlurHash { get; set; }
    public required string? Website { get; set; }
    public required string? SocialLink { get; set; }
}
