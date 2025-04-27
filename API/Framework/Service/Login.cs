using LoginResult = LanguageExt.Either<API.Framework.Entity.Influencer, API.Framework.Entity.Brand>;

namespace API.Framework.Service;

public interface LoginService
{

    LoginResult Password(string username, string password);
    LoginResult Social(string username, string provider);
    String? Recovery(string username);
}
