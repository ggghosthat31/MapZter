namespace MapZter.Proxy.Tasks;

public record ProxyTask<T> : IProxyTask
{
    public ProxyTask(TaskType type, RepositoryMutePattern pattern, T detailObject)
    {
        Type = type;
        Pattern = pattern;
        DetailObject = detailObject;
    }

    public TaskType Type { get; set; }

    public RepositoryMutePattern Pattern { get; set; }

    public T DetailObject { get; set; }
}