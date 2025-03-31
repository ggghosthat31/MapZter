using MapZter.Proxy.Tasks;

namespace MapZter.Proxy.Interactions;

public interface IProxyResponse
{
    public TaskType Type { get; set; }
}