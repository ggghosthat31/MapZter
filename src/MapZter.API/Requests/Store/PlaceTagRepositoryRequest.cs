using MapZter.Entities.RequestFeatures;
using MapZter.Entity.Models;
using MapZter.Proxy;
using MapZter.Proxy.Interactions;
using MediatR;

namespace MapZter.API.Requests.Store;

public record PlaceTagGetAllRequest(Guid RquestId) : IRequest<QueryResult<PlaceTag>?>;

public record PlaceTagGetSingleRequest(Guid RequestId, long PlaceTagId) : IRequest<QueryResult<PlaceTag>?>;

public record PlaceTagGetMultipleRequest(Guid RequestId, IEnumerable<long> PlaceTagsId) : IRequest<QueryResult<PlaceTag>?>;

public record PlaceTagGetByParametersRequest(Guid RequestId, RequestParameters RequestParameters) : IRequest<QueryResult<PlaceTag>?>;

public record PlaceTagCreateRequest(Guid RequestId, PlaceTag PlaceTag) : IRequest<CommandResult>;

public record PlaceTagUpdateRequest(Guid RequestId, PlaceTag PlaceTag) : IRequest<CommandResult>;

public record PlaceTagDeleteRequest(Guid RequestId, PlaceTag PlaceTag) : IRequest<CommandResult>;