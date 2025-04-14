using MapZter.Contracts.Interfaces.Logger;
using MapZter.Contracts.Interfaces.Repository;
using MapZter.Contracts.Interfaces.RepositoryProxy;
using MapZter.Entities.RequestFeatures;
using MapZter.Entity.Models;
using MapZter.Repository;
using MapZter.Proxy.Interactions;
using System.Formats.Asn1;

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
            if (repositoryInstance is PlaceRepository placeRepository && inputEntity is Place place)
            {
                var performedTask = pattern switch
                {
                    RepositoryMutePattern.CREATE => placeRepository.Create(place),
                    RepositoryMutePattern.UPDATE => placeRepository.Update(place),
                    RepositoryMutePattern.DELETE => placeRepository.Delete(place.PlaceId),
                } ?? throw new NullReferenceException("Could not detect prefered repository pattern.");

                await performedTask;

                var commandResult = performedTask.IsCompletedSuccessfully ? true : throw performedTask.Exception;

                return new CommandResult(commandResult);
            }
            else if (repositoryInstance is GeoPointRepository geoPointRepository && inputEntity is GeoPoint geoPoint)
            {
                var performedTask = pattern switch
                {
                    RepositoryMutePattern.CREATE => geoPointRepository.Create(geoPoint),
                    RepositoryMutePattern.UPDATE => geoPointRepository.Update(geoPoint),
                    RepositoryMutePattern.DELETE => geoPointRepository.Delete(geoPoint.Id),
                } ?? throw new NullReferenceException("Could not detect prefered repository pattern.");

                await performedTask;

                var commandResult = performedTask.IsCompletedSuccessfully ? true : throw performedTask.Exception;

                return new CommandResult(commandResult);
            }
            else if (repositoryInstance is PlaceTagRepository placeTagRepository && inputEntity is PlaceTag placeTag)
            {
                var performedTask = pattern switch
                {
                    RepositoryMutePattern.CREATE => placeTagRepository.Create(placeTag),
                    RepositoryMutePattern.UPDATE => placeTagRepository.Update(placeTag),
                    RepositoryMutePattern.DELETE => placeTagRepository.Delete(placeTag.PlaceTagId),
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

    private async Task<QueryResult<GeoPoint?>> RecieveGeoPoint(long geoPointId)
    {
        try
        {
            GeoPoint? place;
            var repositoryInstance = _repositoryManager.GeoPointRepository;
            if (repositoryInstance is GeoPointRepository repository)
                place =  await repository.GetGeoPointAsync(geoPointId);
            else throw new ArgumentException($"Input arguments does not match the desired signature. Repostitory type: {repositoryInstance.GetType().FullName}.");
        
            var succeded = place is null;
            return new QueryResult<GeoPoint?>(succeded, message: String.Empty, capacity: 1, entity: place);
        }
        catch (Exception ex)
        {
            _loggerManager.LogError($"Error trace: {ex.Message}");
            return new QueryResult<GeoPoint?>(false, message: ex.Message, capacity: 0);
        }
    }

    private async Task<QueryResult<PlaceTag?>> RecievePlaceTag(long placeTagId)
    {
        try
        {
            PlaceTag? placeTag;
            var repositoryInstance = _repositoryManager.GeoPointRepository;
            if (repositoryInstance is PlaceTagRepository repository)
                placeTag =  await repository.GetPlaceTagAsync(placeTagId);
            else throw new ArgumentException($"Input arguments does not match the desired signature. Repostitory type: {repositoryInstance.GetType().FullName}.");
        
            var succeded = placeTag is null;
            return new QueryResult<PlaceTag?>(succeded, message: String.Empty, capacity: 1, entity: placeTag);
        }
        catch (Exception ex)
        {
            _loggerManager.LogError($"Error trace: {ex.Message}");
            return new QueryResult<PlaceTag?>(false, message: ex.Message, capacity: 0);
        }
    }

    private async Task<QueryResult<Place?>> RecievePlaces(RepositoryMutePattern pattern, RequestQueryParameters queryParameters)
    {
        try
        {
            var repositoryInstance = _repositoryManager.PlaceRepository;
            var requestParameters = queryParameters.RequestParameters;

            if (repositoryInstance is PlaceRepository repository && requestParameters is PlaceParameters parameters)
            {
                var retrievedCollection = pattern switch
                {
                    RepositoryMutePattern.GET_MULTIPLE_BY_ID => await repository.GetPlacesAsync(queryParameters.EntitiesId),
                    RepositoryMutePattern.GET_MULTIPLE_BY_CONDITION => await repository.GetPlacesAsync(parameters),
                    RepositoryMutePattern.GET_ALL => await repository.GetAllPlacesAsync(),
                } ?? throw new NullReferenceException("Could not detect prefered repository pattern.");
                
                bool succeded = retrievedCollection.Count() > 0;

                if (succeded)
                    return new QueryResult<Place?>(succeded, message: String.Empty, capacity: retrievedCollection.Count(), entityCollection: retrievedCollection);
                else 
                    throw ThrowQueryException(pattern, queryParameters);
            }
            else throw new ArgumentException($"Input arguments does not match the desired signature. Repostitory type: {repositoryInstance.GetType().FullName}. Input entity type: {requestParameters.GetType().FullName}");
        }
        catch (Exception ex)
        {
            _loggerManager.LogError(ex);
            return new QueryResult<Place?>(false, message: String.Empty, capacity: 0);
        }
    }

    private async Task<QueryResult<GeoPoint?>> RecieveGeoPoints(RepositoryMutePattern pattern, RequestQueryParameters queryParameters)
    {
        try
        {
            var repositoryInstance = _repositoryManager.GeoPointRepository;
            var requestParameters = queryParameters.RequestParameters;

            if (repositoryInstance is GeoPointRepository repository && requestParameters is GeoPointParameters parameters)
            {
                var retrievedCollection = pattern switch
                {
                    RepositoryMutePattern.GET_MULTIPLE_BY_ID => await repository.GetGeoPointsAsync(queryParameters.EntitiesId),
                    RepositoryMutePattern.GET_MULTIPLE_BY_CONDITION => await repository.GetGeoPointsAsync(parameters),
                    RepositoryMutePattern.GET_ALL => await repository.GetAllGeoPointsAsync(),
                } ?? throw new NullReferenceException("Could not detect prefered repository pattern.");
                
                bool succeded = retrievedCollection.Count() > 0;

                if (succeded)
                    return new QueryResult<GeoPoint>(succeded, message: String.Empty, capacity: retrievedCollection.Count(), entityCollection: retrievedCollection);
                else 
                    throw ThrowQueryException(pattern, queryParameters);
            }
            else throw new ArgumentException($"Input arguments does not match the desired signature. Repostitory type: {repositoryInstance.GetType().FullName}. Input entity type: {requestParameters.GetType().FullName}");
        }
        catch (Exception ex)
        {
            _loggerManager.LogError(ex);
            return new QueryResult<GeoPoint>(false, message: String.Empty, capacity: 0);
        }
    }

    private async Task<QueryResult<PlaceTag?>> RecievePlaceTags(RepositoryMutePattern pattern, RequestQueryParameters queryParameters)
    {
        try
        {
            var repositoryInstance = _repositoryManager.GeoPointRepository;
            var requestParameters = queryParameters.RequestParameters;

            if (repositoryInstance is PlaceTagRepository repository && requestParameters is PlaceTagParameters parameters)
            {
                var retrievedCollection = pattern switch
                {
                    RepositoryMutePattern.GET_MULTIPLE_BY_ID => await repository.GetPlaceTagsAsync(queryParameters.EntitiesId),
                    RepositoryMutePattern.GET_ALL => await repository.GetAllPlaceTagsAsync(),
                } ?? throw new NullReferenceException("Could not detect prefered repository pattern.");
                
                bool succeded = retrievedCollection.Count() > 0;

                if (succeded)
                    return new QueryResult<PlaceTag>(succeded, message: String.Empty, capacity: retrievedCollection.Count(), entityCollection: retrievedCollection);
                else 
                    throw ThrowQueryException(pattern, queryParameters);
            }
            else throw new ArgumentException($"Input arguments does not match the desired signature. Repostitory type: {repositoryInstance.GetType().FullName}. Input entity type: {requestParameters.GetType().FullName}");
        }
        catch (Exception ex)
        {
            _loggerManager.LogError(ex);
            return new QueryResult<PlaceTag>(false, message: String.Empty, capacity: 0);
        }
    }

    private Exception ThrowQueryException(RepositoryMutePattern pattern, RequestQueryParameters queryParameters)
    {
        var exception = new QueryException("Could find entities within input arguments.");
        exception.Data["Pattern"] = pattern;
        exception.Data["Query parameters"] = queryParameters;
        return exception;
    }


    public Task<CommandResult> CommandAsync(RepositoryMutePattern repositoryPattern, IEntity proxyInputEntity) =>
        Manage(repositoryPattern, proxyInputEntity);

    public async Task<QueryResult<Place?>> QueryPlaceEntity(RepositoryMutePattern repositoryPattern, RequestQueryParameters proxyInputParameters = default)
    {
        if (repositoryPattern is RepositoryMutePattern.GET_SINGLE)
            return await RecievePlace(proxyInputParameters.EntityId);
        else if (repositoryPattern is (RepositoryMutePattern.GET_ALL | RepositoryMutePattern.GET_MULTIPLE_BY_ID | RepositoryMutePattern.GET_MULTIPLE_BY_CONDITION))
            return await RecievePlaces(repositoryPattern, proxyInputParameters);
        else return null;
    }

    public async Task<QueryResult<GeoPoint?>> QueryGeoPointEntity(RepositoryMutePattern repositoryPattern, RequestQueryParameters proxyInputParameters = default) 
    {
        if (repositoryPattern is RepositoryMutePattern.GET_SINGLE)
            return await RecieveGeoPoint(proxyInputParameters.EntityId);
        else if (repositoryPattern is (RepositoryMutePattern.GET_ALL | RepositoryMutePattern.GET_MULTIPLE_BY_ID | RepositoryMutePattern.GET_MULTIPLE_BY_CONDITION))
            return await RecieveGeoPoints(repositoryPattern, proxyInputParameters);
        else return null;
    }

    public async Task<QueryResult<PlaceTag?>> QueryPlaceTagEntity(RepositoryMutePattern repositoryPattern, RequestQueryParameters proxyInputParameters = default) 
    {
        if (repositoryPattern is RepositoryMutePattern.GET_SINGLE)
            return await RecievePlaceTag(proxyInputParameters.EntityId);
        else if (repositoryPattern is (RepositoryMutePattern.GET_ALL | RepositoryMutePattern.GET_MULTIPLE_BY_ID | RepositoryMutePattern.GET_MULTIPLE_BY_CONDITION))
            return await RecievePlaceTags(repositoryPattern, proxyInputParameters);
        else return null;
    }
}