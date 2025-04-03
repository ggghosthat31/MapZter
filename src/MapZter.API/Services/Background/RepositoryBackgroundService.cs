using System.Threading.Channels;
using MapZter.Entity.Models;
using MapZter.Proxy;
using MapZter.Proxy.Tasks;
using MapZter.Proxy.Interactions;

namespace MapZter.API.Services.Background;

public class RepositoryBackgroundService : BackgroundService
{
    private readonly RepositoryProxy _repositoryProxy;

    private readonly Queue<IProxyTask> repositoryTasks = [];

    private readonly Channel<IProxyTask> _channel;

    public RepositoryBackgroundService(
        RepositoryProxy repositoryProxy,
        Channel<IProxyTask> channel)
    {
        _repositoryProxy = repositoryProxy;
        _channel = channel;
    }

    public void SetTask(
        TaskType proxyTaskType,
        RepositoryMutePattern repositoryMutePattern,
        IEntity inputEntity)
    {
        var repositoryProxyTask = new ProxyTask<IEntity>(proxyTaskType, repositoryMutePattern, inputEntity);
        repositoryTasks.Enqueue(repositoryProxyTask);
    }

    public void SetTask(
        TaskType proxyTaskType,
        RepositoryMutePattern repositoryMutePattern,
        PlaceQueryParameters inputEntity)
    {
        var repositoryProxyTask = new ProxyTask<ITaskDetails>(proxyTaskType, repositoryMutePattern, inputEntity);
        repositoryTasks.Enqueue(repositoryProxyTask);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            var task = await _channel.Reader.ReadAsync();
            
            if (task.Type == TaskType.COMMAND_TYPE && task is ProxyTask<IEntity> currentTask)
                ProcessCommand(currentTask);
            else if(task.Type == TaskType.QUERY_TYPE && task is ProxyTask<PlaceQueryParameters> currentQueryParameter)
                ProcessQuery(currentQueryParameter);
        }
    }

    private CommandResult? ProcessCommand(IProxyTask entireTask)
    {
        if (entireTask is not ProxyTask<IEntity> task)
            return null;

        var type = task.Type;
        var pattern = task.Pattern;
        var detailObject = task.DetailObject;

        if (type == TaskType.COMMAND_TYPE && detailObject is IEntity)
            return _repositoryProxy.CommandAsync(pattern, detailObject).Result;

        return null;
    }

    public object? ProcessQuery(IProxyTask entireTask)
    {
        if (entireTask is not ProxyTask<ITaskDetails> task)
            return null;

        var type = task.Type;
        var pattern = task.Pattern;
        var detailObject = task.DetailObject;

        if (type is TaskType.QUERY_TYPE && detailObject is PlaceQueryParameters requestParameters)
            return _repositoryProxy.QueryAsync(pattern, requestParameters).Result;
        else 
            return null;
    }
}