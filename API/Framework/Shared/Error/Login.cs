namespace API.Framework.Shared.Error;

public class UserNotExistsException : Exception
{
    public UserNotExistsException(string message) : base(message) { }
}

public class UnexpectedAmbigeuousUserIdentityException : Exception
{
    public UnexpectedAmbigeuousUserIdentityException(string message) : base(message) { }
}