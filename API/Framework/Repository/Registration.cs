using API.Framework.Shared;
using API.Requests.Registration;
namespace API.Framework.Repository;

public interface RegistrationRepository
{
    LoginPayload Standard(StandardRegistrationRequest request);
    LoginPayload Social(SocialRegistrationRequest request);

}
