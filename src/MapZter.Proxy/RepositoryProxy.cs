using MapZter.Contracts.Interfaces.Logger;
using MapZter.Contracts.Interfaces.Repository;
using MapZter.Contracts.Interfaces.RepositoryProxy;
using MapZter.Entities.RequestFeatures;
using MapZter.Entity.Models;
using MapZter.Repository;

namespace MapZter.Proxy;

public record ProxyObserveParameters(long PlaceId, IEnumerable<long> PlaceIds, RequestParameters RequestParameters);

public class RepositoryProxy : IRepositoryProxy
{
    private readonly ILoggerManager _loggerManager;
    private readonly IRepositoryManager _repositoryManager;

    public RepositoryProxy(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
    }

    private async Task<bool> Manage(RepositoryMutePattern pattern, IEntity inputEntity)
    {
        try
        {
            var repositoryInstance = _repositoryManager.PlaceRepository;
            if (repositoryInstance is PlaceRepository repository && inputEntity is Place place)
            {
                var performedTask = pattern switch
                {
                    RepositoryMutePattern.CREATE => repository.Create(place),
                    RepositoryMutePattern.UPDATE => repository.Update(place),
                    RepositoryMutePattern.DELETE => repository.Delete(place.PlaceId),
                } ?? throw new NullReferenceException("Could not detect prefered repository pattern.");

                await performedTask;

                return performedTask.IsCompletedSuccessfully ? true : throw performedTask.Exception;
            }
            else
                throw new ArgumentException($"Input arguments does not match the desired signature. Repostitory type: {repositoryInstance.GetType().FullName}. Input entity type: {inputEntity.GetType().FullName}");
        }
        catch (Exception ex)
        {
            _loggerManager.LogError($"Error trace: {ex.Message}");
            return false;
        }
    }

    private async Task<Place?> RecievePlace(long placeId)
    {
        try
        {
            var repositoryInstance = _repositoryManager.PlaceRepository;
            if (repositoryInstance is PlaceRepository repository)
                return await repository.GetPlaceAsync(placeId);
            else 
                throw new ArgumentException($"Input arguments does not match the desired signature. Repostitory type: {repositoryInstance.GetType().FullName}.");
        }
        catch (Exception ex)
        {
            _loggerManager.LogError($"Error trace: {ex.Message}");
            return default;
        }
    }

    private async Task<IEnumerable<Place>> RecievePlaces(RepositoryMutePattern pattern, ProxyObserveParameters proxyObserveParameters)
    {
        try
        {
            var repositoryInstance = _repositoryManager.PlaceRepository;
            var requestParameters = proxyObserveParameters.RequestParameters;
            if (repositoryInstance is PlaceRepository repository && requestParameters is PlaceParameters parameters)
            {
                var performedTask = pattern switch
                {
                    RepositoryMutePattern.GET_MULTIPLE_BY_ID => repository.GetPlacesAsync(proxyObserveParameters.PlaceIds),
                    RepositoryMutePattern.GET_MULTIPLE_BY_CONDITION => repository.GetPlacesAsync(parameters),
                    RepositoryMutePattern.GET_ALL => repository.GetAllPlacesAsync(),
                } ?? throw new NullReferenceException("Could not detect prefered repository pattern.");
                
                return await performedTask;
            }
            else 
                throw new ArgumentException($"Input arguments does not match the desired signature. Repostitory type: {repositoryInstance.GetType().FullName}. Input entity type: {requestParameters.GetType().FullName}");
        }
        catch (Exception ex)
        {
            _loggerManager.LogError($"Error trace: {ex.Message}");
            return Enumerable.Empty<Place>();
        }
    }

    private async Task<object?> Process(RepositoryMutePattern repositoryPattern, ProxyObserveParameters proxyInputParameters)
    {
        if (repositoryPattern is RepositoryMutePattern.GET_SINGLE)
            return await RecievePlace(proxyInputParameters.PlaceId);
        else if (repositoryPattern is (RepositoryMutePattern.GET_ALL | RepositoryMutePattern.GET_MULTIPLE_BY_ID | RepositoryMutePattern.GET_MULTIPLE_BY_CONDITION))
            return await RecievePlaces(repositoryPattern, proxyInputParameters);
        else return default;
    }

    public bool Execute(RepositoryMutePattern repositoryPattern, IEntity proxyInputEntity) =>
        (repositoryPattern is (RepositoryMutePattern.CREATE | RepositoryMutePattern.UPDATE | RepositoryMutePattern.DELETE)) ?
            Manage(repositoryPattern, proxyInputEntity).Result :  false;

    public object? Execute(RepositoryMutePattern repositoryPattern, ProxyObserveParameters proxyObserveParameters) =>
        Process(repositoryPattern, proxyObserveParameters).Result;

    public async Task<object?> ExecuteAsync(RepositoryMutePattern repositoryPattern, ProxyObserveParameters proxyObserveParameters) =>
        await Process(repositoryPattern, proxyObserveParameters);
}