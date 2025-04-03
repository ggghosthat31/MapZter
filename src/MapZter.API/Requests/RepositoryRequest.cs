using MapZter.Entities.RequestFeatures;
using MapZter.Entity.Models;
using MapZter.Proxy;
using MapZter.Proxy.Interactions;
using MediatR;

namespace MapZter.API.Requests;

public record RepositoryGetAllPlacesRequest(Guid RquestId) : IRequest<QueryResult<Place>?>;

public record RepositoryGetPlaceRequest(Guid RequestId, long PlaceId) : IRequest<QueryResult<Place>?>;

public record RepositoryGetPlacesRequest(Guid RequestId, IEnumerable<long> PlacesId) : IRequest<QueryResult<Place>?>;

public record RepositoryGetPlacesByParametersRequest(Guid RequestId, RequestParameters RequestParameters) : IRequest<QueryResult<Place>?>;

public record RepositoryCreatePlaceRequest(Guid RequestId, Place Place) : IRequest<CommandResult>;

public record RepositoryUpdatePlaceRequest(Guid RequestId, Place Place) : IRequest<CommandResult>;

public record RepositoryDeletePlaceRequest(Guid RequestId, Place PlaceId) : IRequest<CommandResult>;