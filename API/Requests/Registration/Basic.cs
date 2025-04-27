using System;

namespace API.Requests.Registration;

public class BasicRegistrationRequest
{
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string? Email { get; set; }
    public required string? Phone { get; set; }

}
