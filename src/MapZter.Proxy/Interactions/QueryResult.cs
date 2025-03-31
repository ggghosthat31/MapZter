namespace MapZter.Proxy;

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