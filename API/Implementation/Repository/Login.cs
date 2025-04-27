
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;
using API.Framework.Entity;
using API.Framework.Repository;
using API.Framework.Service;
using API.Framework.Shared;
using API.Framework.Shared.Error;
using Microsoft.IdentityModel.Tokens;
using LoginResult = LanguageExt.Either<API.Framework.Entity.Influencer, API.Framework.Entity.Brand>;
namespace API.Implementation.Repository;

public class LoginRepositoryImpl : LoginRepository
{
    private readonly LoginService _service;
    private readonly IConfiguration _config;

    public LoginRepositoryImpl(LoginService service, IConfiguration configuration)
    {
        _config = configuration;
        _service = service;
    }

    public void Forgot(string username)
    {
        String? recoveryContact = _service.Recovery(username);
        if (recoveryContact == null)
        {
            throw new UserNotExistsException("User does not exist in the system");
        }
        //TODO: send email/sms
    }

    public LoginPayload Password(string username, string password)
    {
        LoginResult result = _service.Password(username, password);
        return AssignTokenAndReturn(result);
    }

    public LoginPayload Social(string username, string provider)
    {
        LoginResult result = _service.Social(username, provider);
        return AssignTokenAndReturn(result);
    }

    private LoginPayload AssignTokenAndReturn(LoginResult profile)
    {
        User user = profile.Match(
            influencer => (User)influencer,
            brand => brand
        );
        string username = user.UserName;
        string identifier = user.UserId.ToString();
        string role = profile.IsLeft ? Role.Influencer : Role.Brand;

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Jti, identifier),
            new Claim(ClaimTypes.NameIdentifier, identifier),
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.UserData, identifier)
        };

        DateTime tokenexpirydate = DateTime.UtcNow.AddMonths(1);
        SymmetricSecurityKey authSiginKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config[AppConfig.Jwt.Secret]!));
        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: _config[AppConfig.Jwt.Issuer],
            audience: _config[AppConfig.Jwt.Audience],
            expires: tokenexpirydate,
            claims: claims,
            signingCredentials: new SigningCredentials(authSiginKey, SecurityAlgorithms.HmacSha256Signature)
        );
        string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        LoginPayload payload = new LoginPayload()
        {
            Username = username,
            ExpiresAt = tokenexpirydate,
            Token = token,
            Profile = profile.IsLeft ? (Influencer)profile : (Brand)profile,
        };
        return payload;
    }
}
