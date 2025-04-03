using AutoMapper;
using MapZter.Contracts.Interfaces.Services;
using MapZter.Entity.Models;
using MapZter.Proxy;
using MapZter.API.Requests;
using MediatR;
using MapZter.Proxy.Interactions;

namespace MapZter.API.Handlers;

public class PlaceHandler:  
    IRequestHandler<RepositoryCreatePlaceRequest, CommandResult>,
    IRequestHandler<RepositoryUpdatePlaceRequest, CommandResult>,
    IRequestHandler<RepositoryDeletePlaceRequest, CommandResult>,
    IRequestHandler<RepositoryGetAllPlacesRequest, QueryResult<Place>?>,
    IRequestHandler<RepositoryGetPlaceRequest, QueryResult<Place>?>,
    IRequestHandler<RepositoryGetPlacesRequest, QueryResult<Place>?>,
    IRequestHandler<RepositoryGetPlacesByParametersRequest, QueryResult<Place>?>
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

    public async Task<QueryResult<Place>?> Handle(RepositoryGetAllPlacesRequest getAllPlaceRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryAsync(RepositoryMutePattern.GET_ALL);

    public async Task<QueryResult<Place>?> Handle(RepositoryGetPlaceRequest getPlaceRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryAsync(RepositoryMutePattern.GET_SINGLE, new PlaceQueryParameters(getPlaceRequest.PlaceId));

    public async Task<QueryResult<Place>?> Handle(RepositoryGetPlacesRequest getPlacesRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryAsync(RepositoryMutePattern.GET_MULTIPLE_BY_ID, new PlaceQueryParameters(PlacesId: getPlacesRequest.PlacesId));

    public async Task<QueryResult<Place>?> Handle(RepositoryGetPlacesByParametersRequest getPlacesByParametersRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryAsync(RepositoryMutePattern.GET_MULTIPLE_BY_ID, new PlaceQueryParameters(RequestParameters: getPlacesByParametersRequest.RequestParameters));

    public async Task<CommandResult> Handle(RepositoryCreatePlaceRequest createRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.CommandAsync(RepositoryMutePattern.CREATE, createRequest.Place);

    public async Task<CommandResult> Handle(RepositoryUpdatePlaceRequest createRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.CommandAsync(RepositoryMutePattern.UPDATE, createRequest.Place);

    public async Task<CommandResult> Handle(RepositoryDeletePlaceRequest createRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.CommandAsync(RepositoryMutePattern.DELETE, createRequest.PlaceId);
}