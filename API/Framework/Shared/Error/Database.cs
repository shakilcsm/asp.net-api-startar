using System;

namespace API.Framework.Shared.Error;

public class DatabaseConnectionException : Exception
{
    public DatabaseConnectionException(string message) : base(message) { }
}
public class UnknowDatabaseException : Exception
{
    public UnknowDatabaseException(string message) : base(message) { }
}