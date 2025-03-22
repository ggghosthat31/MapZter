using MapZter.Proxy;
using MapZter.Repository;

namespace MapZter.API.Services.Background;

public class RepositoryBackgroundService : BackgroundService
{
    public readonly RepositoryProxy _repositoryProxy;

    public RepositoryBackgroundService(RepositoryProxy repositoryProxy) =>
        _repositoryProxy = repositoryProxy;


    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}