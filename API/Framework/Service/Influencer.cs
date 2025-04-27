using System;
using API.Framework.Entity;
using LanguageExt;

namespace API.Framework.Service;

public interface InfluencerService
{
    Influencer ByUsername(string username);
}
