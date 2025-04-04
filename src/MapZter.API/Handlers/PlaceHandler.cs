using AutoMapper;
using MapZter.Contracts.Interfaces.Services;
using MapZter.Entity.Models;
using MapZter.Proxy;
using MapZter.API.Requests.Store;
using MediatR;
using MapZter.Proxy.Interactions;

namespace MapZter.API.Handlers;

public class PlaceHandler:  
    IRequestHandler<PlaceCreateRequest, CommandResult>,
    IRequestHandler<PlaceUpdateRequest, CommandResult>,
    IRequestHandler<PlaceDeleteRequest, CommandResult>,
    IRequestHandler<PlaceGetAllRequest, QueryResult<Place>?>,
    IRequestHandler<PlaceGetSingleRequest, QueryResult<Place>?>,
    IRequestHandler<PlaceGetMultipleRequest, QueryResult<Place>?>,
    IRequestHandler<PlaceGetByParametersRequest, QueryResult<Place>?>
{
    private readonly IMapService _mapService;
    private readonly IMapper _mapper;
    private readonly RepositoryProxy _repositoryProxy;

    public PlaceHandler(
        IMapService mapService,
        IMapper mapper,
        RepositoryProxy repositoryProxy)
    {
        _mapService = mapService;
        _mapper = mapper;
        _repositoryProxy = repositoryProxy;
    }

    public async Task<QueryResult<Place>?> Handle(PlaceGetAllRequest getAllPlaceRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryAsync(RepositoryMutePattern.GET_ALL);

    public async Task<QueryResult<Place>?> Handle(PlaceGetSingleRequest getPlaceRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryAsync(RepositoryMutePattern.GET_SINGLE, new PlaceQueryParameters(getPlaceRequest.PlaceId));

    public async Task<QueryResult<Place>?> Handle(PlaceGetMultipleRequest getPlacesRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryAsync(RepositoryMutePattern.GET_MULTIPLE_BY_ID, new PlaceQueryParameters(PlacesId: getPlacesRequest.PlacesId));

    public async Task<QueryResult<Place>?> Handle(PlaceGetByParametersRequest getPlacesByParametersRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryAsync(RepositoryMutePattern.GET_MULTIPLE_BY_ID, new PlaceQueryParameters(RequestParameters: getPlacesByParametersRequest.RequestParameters));

    public async Task<CommandResult> Handle(PlaceCreateRequest createRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.CommandAsync(RepositoryMutePattern.CREATE, createRequest.Place);

    public async Task<CommandResult> Handle(PlaceUpdateRequest createRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.CommandAsync(RepositoryMutePattern.UPDATE, createRequest.Place);

    public async Task<CommandResult> Handle(PlaceDeleteRequest createRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.CommandAsync(RepositoryMutePattern.DELETE, createRequest.PlaceId);
}