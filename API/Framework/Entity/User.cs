using API.Framework.Shared;

namespace API.Framework.Entity;

public class User
{
    public required int Id { get; set; }
    public required Guid UserId { get; set; }
    public required string Name { get; set; }
    public required string UserName { get; set; }
    public required Lookup? City { get; set; }
    public required Lookup? Country { get; set; }
    public required string? Phone { get; set; }
    public required string? Email { get; set; }
    public required string? Bio { get; set; }
    public required bool IsVerified { get; set; }

}
