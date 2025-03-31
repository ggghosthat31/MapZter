using MapZter.Proxy.Tasks;

namespace MapZter.Proxy.Interactions;

public record ProxyReply<T> : IProxyResponse where T : IResult
{
    public ProxyReply(TaskType type, RepositoryMutePattern pattern, T detailObject)
    {
        Type = type;
        Pattern = pattern;
        DetailObject = detailObject;
    }

    public TaskType Type { get; set; }

    public RepositoryMutePattern Pattern { get; set; }

    public T DetailObject { get; set; }
}