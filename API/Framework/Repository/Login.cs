
using API.Framework.Shared;

namespace API.Framework.Repository;

public interface LoginRepository
{
    LoginPayload Password(string username, string password);
    LoginPayload Social(string username, string provider);
    void Forgot(string username);
}
