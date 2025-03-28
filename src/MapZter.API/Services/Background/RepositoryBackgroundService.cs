using System.Threading.Channels;
using MapZter.Entities.RequestFeatures;
using MapZter.Entity.Models;
using MapZter.Proxy;
using MapZter.Repository;
using NLog.LayoutRenderers;

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
        ProxyTaskType proxyTaskType,
        RepositoryMutePattern repositoryMutePattern,
        IEntity inputEntity)
    {
        var repositoryProxyTask = new ProxyTask<IEntity>(proxyTaskType, repositoryMutePattern, inputEntity);
        repositoryTasks.Enqueue(repositoryProxyTask);
    }

    public void SetTask(
        ProxyTaskType proxyTaskType,
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
            
            if (task.Type == ProxyTaskType.COMMAND_TYPE && task is ProxyTask<IEntity> currentTask)
                ProcessCommand(currentTask);
            else if(task.Type == ProxyTaskType.QUERY_TYPE && task is ProxyTask<PlaceQueryParameters> currentQueryParameter)
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

        if (type == ProxyTaskType.COMMAND_TYPE && detailObject is IEntity)
            return _repositoryProxy.CommandAsync(pattern, detailObject);

        return null;
    }

    public object? ProcessQuery(IProxyTask entireTask)
    {
        if (entireTask is not ProxyTask<ITaskDetails> task)
            return null;

        var type = task.Type;
        var pattern = task.Pattern;
        var detailObject = task.DetailObject;

        if (type is ProxyTaskType.QUERY_TYPE && detailObject is PlaceQueryParameters requestParameters)
            return _repositoryProxy.QueryAsync(pattern, requestParameters).Result;
        else 
            return null;
    }
}