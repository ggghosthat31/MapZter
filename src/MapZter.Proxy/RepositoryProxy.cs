using MapZter.Contracts.Interfaces.Logger;
using MapZter.Contracts.Interfaces.Repository;
using MapZter.Contracts.Interfaces.RepositoryProxy;
using MapZter.Entities.RequestFeatures;
using MapZter.Entity.Models;
using MapZter.Repository;

namespace MapZter.Proxy;

public class RepositoryProxy : IRepositoryProxy
{
    private readonly ILoggerManager _loggerManager;
    private readonly IRepositoryManager _repositoryManager;

    public RepositoryProxy(
        ILoggerManager loggerManager,
        IRepositoryManager repositoryManager)
    {
        _loggerManager = loggerManager;
        _repositoryManager = repositoryManager;
    }

    private async Task<CommandResult> Manage(RepositoryMutePattern pattern, IEntity inputEntity)
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

                var commandResult = performedTask.IsCompletedSuccessfully ? true : throw performedTask.Exception;

                return new CommandResult(commandResult);
            }
            else throw new ArgumentException($"Input arguments does not match the desired signature. Repostitory type: {repositoryInstance.GetType().FullName}. Input entity type: {inputEntity.GetType().FullName}");
        }
        catch (Exception ex)
        {
            _loggerManager.LogError($"Error trace: {ex.Message}");
            return new CommandResult(false, ex.Message);
        }
    }

    private async Task<QueryResult<Place?>> RecievePlace(long placeId)
    {
        try
        {
            Place? place;
            var repositoryInstance = _repositoryManager.PlaceRepository;
            if (repositoryInstance is PlaceRepository repository)
                place =  await repository.GetPlaceAsync(placeId);
            else throw new ArgumentException($"Input arguments does not match the desired signature. Repostitory type: {repositoryInstance.GetType().FullName}.");
        
            var succeded = place is null;
            return new QueryResult<Place?>(succeded, message: String.Empty, capacity: 1, entity: place);
        }
        catch (Exception ex)
        {
            _loggerManager.LogError($"Error trace: {ex.Message}");
            return new QueryResult<Place?>(false, message: ex.Message, capacity: 0);
        }
    }

    private async Task<QueryResult<Place>?> RecievePlaces(RepositoryMutePattern pattern, PlaceQueryParameters queryParameters)
    {
        try
        {
            var repositoryInstance = _repositoryManager.PlaceRepository;
            var requestParameters = queryParameters.RequestParameters;

            if (repositoryInstance is PlaceRepository repository && requestParameters is PlaceParameters parameters)
            {
                var retrievedCollection = pattern switch
                {
                    RepositoryMutePattern.GET_MULTIPLE_BY_ID => await repository.GetPlacesAsync(queryParameters.PlaceIds),
                    RepositoryMutePattern.GET_MULTIPLE_BY_CONDITION => await repository.GetPlacesAsync(parameters),
                    RepositoryMutePattern.GET_ALL => await repository.GetAllPlacesAsync(),
                } ?? throw new NullReferenceException("Could not detect prefered repository pattern.");
                
                bool succeded = retrievedCollection.Count() > 0;

                if (succeded)
                    return new QueryResult<Place>(succeded, message: String.Empty, capacity: retrievedCollection.Count(), entityCollection: retrievedCollection);
                else 
                    throw ThrowQueryException(pattern, queryParameters);
            }
            else throw new ArgumentException($"Input arguments does not match the desired signature. Repostitory type: {repositoryInstance.GetType().FullName}. Input entity type: {requestParameters.GetType().FullName}");
        }
        catch (Exception ex)
        {
            _loggerManager.LogError(ex);
            return new QueryResult<Place>(false, message: String.Empty, capacity: 0);
        }
    }

    private Exception ThrowQueryException(RepositoryMutePattern pattern, PlaceQueryParameters queryParameters)
    {
        var exception = new QueryException("Could find entities within input arguments.");
        exception.Data["Pattern"] = pattern;
        exception.Data["Query parameters"] = queryParameters;
        return exception;
    }

    private async Task<object?> Process(RepositoryMutePattern repositoryPattern, PlaceQueryParameters proxyInputParameters)
    {
        if (repositoryPattern is RepositoryMutePattern.GET_SINGLE)
            return await RecievePlace(proxyInputParameters.PlaceId);
        else if (repositoryPattern is (RepositoryMutePattern.GET_ALL | RepositoryMutePattern.GET_MULTIPLE_BY_ID | RepositoryMutePattern.GET_MULTIPLE_BY_CONDITION))
            return await RecievePlaces(repositoryPattern, proxyInputParameters);
        else return default;
    }

    public CommandResult Command(RepositoryMutePattern repositoryPattern, IEntity proxyInputEntity) =>
        Manage(repositoryPattern, proxyInputEntity).Result;

    public CommandResult CommandAsync(RepositoryMutePattern repositoryPattern, IEntity proxyInputEntity) =>
        Manage(repositoryPattern, proxyInputEntity).Result;

    public object? Query(RepositoryMutePattern repositoryPattern, PlaceQueryParameters proxyObserveParameters) =>
        Process(repositoryPattern, proxyObserveParameters).Result;

    public async Task<object?> QueryAsync(RepositoryMutePattern repositoryPattern, PlaceQueryParameters proxyObserveParameters) =>
        await Process(repositoryPattern, proxyObserveParameters);
}