using MapZter.Entity.Models;
using MapZter.Proxy;
using MapZter.API.Requests.Store;
using MediatR;
using MapZter.Proxy.Interactions;

namespace MapZter.API.Handlers;

public class PlaceTagHandler:  
    IRequestHandler<PlaceTagCreateRequest, CommandResult>,
    IRequestHandler<PlaceTagUpdateRequest, CommandResult>,
    IRequestHandler<PlaceTagDeleteRequest, CommandResult>,
    IRequestHandler<PlaceTagGetAllRequest, QueryResult<PlaceTag>?>,
    IRequestHandler<PlaceTagGetSingleRequest, QueryResult<PlaceTag>?>,
    IRequestHandler<PlaceTagGetMultipleRequest, QueryResult<PlaceTag>?>,
    IRequestHandler<PlaceTagGetByParametersRequest, QueryResult<PlaceTag>?>
{
    private readonly RepositoryProxy _repositoryProxy;

    public PlaceTagHandler(RepositoryProxy repositoryProxy)
    {
        _repositoryProxy = repositoryProxy;
    }

    public async Task<QueryResult<PlaceTag?>> Handle(PlaceTagGetAllRequest getAllPlaceRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryPlaceTagEntity(RepositoryMutePattern.GET_ALL);

    public async Task<QueryResult<PlaceTag?>> Handle(PlaceTagGetSingleRequest getPlaceRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryPlaceTagEntity(RepositoryMutePattern.GET_SINGLE, new RequestQueryParameters(getPlaceRequest.PlaceTagId));

    public async Task<QueryResult<PlaceTag?>> Handle(PlaceTagGetMultipleRequest getPlacesRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryPlaceTagEntity(RepositoryMutePattern.GET_MULTIPLE_BY_ID, new RequestQueryParameters(EntitiesId: getPlacesRequest.PlaceTagsId));

    public async Task<QueryResult<PlaceTag?>> Handle(PlaceTagGetByParametersRequest getPlacesByParametersRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.QueryPlaceTagEntity(RepositoryMutePattern.GET_MULTIPLE_BY_ID, new RequestQueryParameters(RequestParameters: getPlacesByParametersRequest.RequestParameters));

    public async Task<CommandResult> Handle(PlaceTagCreateRequest createRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.CommandAsync(RepositoryMutePattern.CREATE, createRequest.PlaceTag);

    public async Task<CommandResult> Handle(PlaceTagUpdateRequest createRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.CommandAsync(RepositoryMutePattern.UPDATE, createRequest.PlaceTag);

    public async Task<CommandResult> Handle(PlaceTagDeleteRequest createRequest, CancellationToken cancellationToken) =>
        await _repositoryProxy.CommandAsync(RepositoryMutePattern.DELETE, createRequest.PlaceTag);
}