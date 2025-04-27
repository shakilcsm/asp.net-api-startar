using API.Framework.Entity;

namespace API.Framework.Service;

public interface BrandService
{
    Brand ByUsername(string username);
}
