using MapZter.Contracts.Interfaces.Repository;
using MapZter.Contracts.Interfaces.RepositoryProxy;
using MapZter.Entity.Models;
using MapZter.Repository;

namespace MapZter.Proxy;

public class RepositoryProxy : IRepositoryProxy
{
    public readonly IRepositoryManager _repositoryManager;

    public RepositoryProxy(IRepositoryManager repositoryManager) =>
        _repositoryManager = repositoryManager;
    
    private async Task<bool> Process(RepositoryMutePattern pattern, IEntity inputPlace)
    {
        try
        {
            var place = inputPlace as Place ?? throw new NullReferenceException("Input parameters is not <Place> type.");;
            var repository = _repositoryManager.PlaceRepository as PlaceRepository;

            var performedTask = pattern switch
            {
                RepositoryMutePattern.CREATE => repository.Create(place),
                RepositoryMutePattern.UPDATE => repository.Update(place),
                RepositoryMutePattern.DELETE => repository.Delete(place),
            } ?? throw new NullReferenceException("Could not detect prefered repository pattern.");

            await performedTask;

            return performedTask.IsCompletedSuccessfully ? true : throw performedTask.Exception;
        }
        catch (Exception ex)
        {
            //TODO: add logger
            return false;
        }
    }

    public bool Execute(RepositoryMutePattern repositoryPattern, IEntity proxyInputEntity) =>
        Process(repositoryPattern, proxyInputEntity).Result;

    public async Task<bool> ExecuteAsync(RepositoryMutePattern repositoryPattern, IEntity proxyInputEntity) =>
        await Process(repositoryPattern, proxyInputEntity);
}