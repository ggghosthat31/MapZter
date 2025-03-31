using MapZter.Proxy.Tasks;

namespace MapZter.Proxy.Interactions;

public class QueryException : Exception
{
    public QueryException(string message) : base(message)
    {}
}