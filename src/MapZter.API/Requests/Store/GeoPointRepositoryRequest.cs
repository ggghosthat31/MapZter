using MapZter.Entities.RequestFeatures;
using MapZter.Entity.Models;
using MapZter.Proxy;
using MapZter.Proxy.Interactions;
using MediatR;

namespace MapZter.API.Requests.Store;

public record GeoPointGetAllRequest(Guid RquestId) : IRequest<QueryResult<GeoPoint>?>;

public record GeoPointGetSingleRequest(Guid RequestId, long PlaceId) : IRequest<QueryResult<GeoPoint>?>;

public record GeoPointGetMultipleRequest(Guid RequestId, IEnumerable<long> PlacesId) : IRequest<QueryResult<GeoPoint>?>;

public record GeoPointGetByParametersRequest(Guid RequestId, RequestParameters RequestParameters) : IRequest<QueryResult<GeoPoint>?>;

public record GeoPointCreateRequest(Guid RequestId, GeoPoint GeoPoint) : IRequest<CommandResult>;

public record GeoPointUpdateRequest(Guid RequestId, GeoPoint GeoPoint) : IRequest<CommandResult>;

public record GeoPointDeleteRequest(Guid RequestId, GeoPoint GeoPoint) : IRequest<CommandResult>;