using MapZter.API.Requests.Store;
using MapZter.Entity.Models;
using MapZter.Proxy;
using MapZter.Proxy.Interactions;
using MediatR;

namespace MapZter.API.Handlers;

public class GeoPointHandler:  
    IRequestHandler<GeoPointCreateRequest, CommandResult>,
    IRequestHandler<GeoPointUpdateRequest, CommandResult>,
    IRequestHandler<GeoPointDeleteRequest, CommandResult>,
    IRequestHandler<GeoPointGetAllRequest, QueryResult<GeoPoint?>>,
    IRequestHandler<GeoPointGetSingleRequest, QueryResult<GeoPoint?>>,
    IRequestHandler<GeoPointGetMultipleRequest, QueryResult<GeoPoint?>>,
    IRequestHandler<GeoPointGetByParametersRequest, QueryResult<GeoPoint?>>
{
    private readonly RepositoryProxy _repositoryProxy;

    public GeoPointHandler(RepositoryProxy repositoryProxy)
    {
        _repositoryProxy = repositoryProxy;
    }

    public async Task<QueryResult<GeoPoint?>> Handle(GeoPointGetAllRequest getAllPlaceRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryGeoPointEntity(RepositoryMutePattern.GET_ALL);

    public async Task<QueryResult<GeoPoint?>> Handle(GeoPointGetSingleRequest getPlaceRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryGeoPointEntity(RepositoryMutePattern.GET_SINGLE, new RequestQueryParameters(getPlaceRequest.PlaceId));

    public async Task<QueryResult<GeoPoint?>> Handle(GeoPointGetMultipleRequest getPlacesRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryGeoPointEntity(RepositoryMutePattern.GET_MULTIPLE_BY_ID, new RequestQueryParameters(EntitiesId: getPlacesRequest.PlacesId));

    public async Task<QueryResult<GeoPoint?>> Handle(GeoPointGetByParametersRequest getPlacesByParametersRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryGeoPointEntity(RepositoryMutePattern.GET_MULTIPLE_BY_ID, new RequestQueryParameters(RequestParameters: getPlacesByParametersRequest.RequestParameters));

    public async Task<CommandResult> Handle(GeoPointCreateRequest createRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.CommandAsync(RepositoryMutePattern.CREATE, createRequest.GeoPoint);

    public async Task<CommandResult> Handle(GeoPointUpdateRequest createRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.CommandAsync(RepositoryMutePattern.UPDATE, createRequest.GeoPoint);

    public async Task<CommandResult> Handle(GeoPointDeleteRequest createRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.CommandAsync(RepositoryMutePattern.DELETE, createRequest.GeoPoint);
}