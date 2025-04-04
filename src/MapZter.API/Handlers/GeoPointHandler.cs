using AutoMapper;
using MapZter.Contracts.Interfaces.Services;
using MapZter.Entity.Models;
using MapZter.Proxy;
using MapZter.API.Requests.Store;
using MediatR;
using MapZter.Proxy.Interactions;

namespace MapZter.API.Handlers;

public class GeoPointHandler:  
    IRequestHandler<GeoPointCreateRequest, CommandResult>,
    IRequestHandler<GeoPointUpdateRequest, CommandResult>,
    IRequestHandler<GeoPointDeleteRequest, CommandResult>,
    IRequestHandler<GeoPointGetAllRequest, QueryResult<GeoPoint>?>,
    IRequestHandler<GeoPointGetSingleRequest, QueryResult<GeoPoint>?>,
    IRequestHandler<GeoPointGetMultipleRequest, QueryResult<GeoPoint>?>,
    IRequestHandler<GeoPointGetByParametersRequest, QueryResult<GeoPoint>?>
{
    private readonly IMapService _mapService;
    private readonly IMapper _mapper;
    private readonly RepositoryProxy _repositoryProxy;

    public GeoPointHandler(
        IMapService mapService,
        IMapper mapper,
        RepositoryProxy repositoryProxy)
    {
        _mapService = mapService;
        _mapper = mapper;
        _repositoryProxy = repositoryProxy;
    }

    public async Task<QueryResult<GeoPoint>?> Handle(GeoPointGetAllRequest getAllPlaceRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryAsync(RepositoryMutePattern.GET_ALL);

    public async Task<QueryResult<GeoPoint>?> Handle(GeoPointGetSingleRequest getPlaceRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryAsync(RepositoryMutePattern.GET_SINGLE, new PlaceQueryParameters(getPlaceRequest.PlaceId));

    public async Task<QueryResult<GeoPoint>?> Handle(GeoPointGetMultipleRequest getPlacesRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryAsync(RepositoryMutePattern.GET_MULTIPLE_BY_ID, new PlaceQueryParameters(PlacesId: getPlacesRequest.PlacesId));

    public async Task<QueryResult<GeoPoint>?> Handle(GeoPointGetByParametersRequest getPlacesByParametersRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryAsync(RepositoryMutePattern.GET_MULTIPLE_BY_ID, new PlaceQueryParameters(RequestParameters: getPlacesByParametersRequest.RequestParameters));

    public async Task<CommandResult> Handle(GeoPointCreateRequest createRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.CommandAsync(RepositoryMutePattern.CREATE, createRequest.Place);

    public async Task<CommandResult> Handle(GeoPointUpdateRequest createRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.CommandAsync(RepositoryMutePattern.UPDATE, createRequest.Place);

    public async Task<CommandResult> Handle(GeoPointDeleteRequest createRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.CommandAsync(RepositoryMutePattern.DELETE, createRequest.PlaceId);
}