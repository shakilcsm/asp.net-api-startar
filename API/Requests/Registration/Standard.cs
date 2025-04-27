using System;

namespace API.Requests.Registration;

public class StandardRegistrationRequest : BasicRegistrationRequest
{
    public required string Password { get; set; }
}
