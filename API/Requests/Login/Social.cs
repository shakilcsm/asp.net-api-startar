using System;

namespace API.Requests.Login;

public class SocialLoginRequest
{
    public string Id { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;

}
