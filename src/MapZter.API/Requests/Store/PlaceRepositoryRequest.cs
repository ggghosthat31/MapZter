using MapZter.Entities.RequestFeatures;
using MapZter.Entity.Models;
using MapZter.Proxy;
using MapZter.Proxy.Interactions;
using MediatR;

namespace MapZter.API.Requests.Store;

public record PlaceGetAllRequest(Guid RquestId) : IRequest<QueryResult<Place>?>;

public record PlaceGetSingleRequest(Guid RequestId, long PlaceId) : IRequest<QueryResult<Place>?>;

public record PlaceGetMultipleRequest(Guid RequestId, IEnumerable<long> PlacesId) : IRequest<QueryResult<Place>?>;

public record PlaceGetByParametersRequest(Guid RequestId, RequestParameters RequestParameters) : IRequest<QueryResult<Place>?>;

public record PlaceCreateRequest(Guid RequestId, Place Place) : IRequest<CommandResult>;

public record PlaceUpdateRequest(Guid RequestId, Place Place) : IRequest<CommandResult>;

public record PlaceDeleteRequest(Guid RequestId, Place PlaceId) : IRequest<CommandResult>;