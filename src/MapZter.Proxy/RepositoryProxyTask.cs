using MapZter.Entity.Models;

namespace MapZter.Proxy;

// 1. ProxyTask : IProxyTask || ProxyRequest
// 1.1 Type
// 1.2 Pattern
// 1.3 Details (Command | Query) 

// 1. ProxyReply : IProxyResponse || ProxyResponse
// 1.1 Type
// 1.2 Pattern
// 1.3 Details (CommandResult | QueryResult)  || where QueryResult : SingleEntity, CollectionEntity 


public interface IProxyTask
{
    public ProxyTaskType Type { get; set; }
}

public interface IProxyResponse
{
    public ProxyTaskType Type { get; set; }
}

public interface ITaskDetails;

public enum ProxyTaskType
{
    COMMAND_TYPE, 
    QUERY_TYPE
};

public record ProxyTask<T> : IProxyTask
{
    public ProxyTask(ProxyTaskType type, RepositoryMutePattern pattern, T detailObject)
    {
        Type = type;
        Pattern = pattern;
        DetailObject = detailObject;
    }

    public ProxyTaskType Type { get; set; }

    public RepositoryMutePattern Pattern { get; set; }

    public T DetailObject { get; set; }
}


public interface IResult;

public record CommandResult : IResult
{
    public CommandResult(bool isSuccess, string message = "")
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; set; }

    public string Message { get; set; }
}

public record QueryResult<T> : IResult
{

    public QueryResult(bool isSuccess, string message = "", int capacity = 0, T entity = default, IEnumerable<T> entityCollection = default)
    {
        IsSuccess = isSuccess;
        Message = message;
        Capacity = capacity;
        Entity = entity;
        EntityCollection = entityCollection;
    }
    
    public bool IsSuccess { get; set; }

    public string Message { get; set; }

    public int Capacity { get; set; }

    public T Entity { get; set; }

    public IEnumerable<T> EntityCollection { get; set; }
}

public record ProxyReply<T> : IProxyResponse where T : IResult
{
    public ProxyReply(ProxyTaskType type, RepositoryMutePattern pattern, T detailObject)
    {
        Type = type;
        Pattern = pattern;
        DetailObject = detailObject;
    }

    public ProxyTaskType Type { get; set; }

    public RepositoryMutePattern Pattern { get; set; }

    public T DetailObject { get; set; }
}