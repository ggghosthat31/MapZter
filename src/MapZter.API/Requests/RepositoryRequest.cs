using MapZter.Entity.Models;

namespace MapZter.API.Requests;

public record RepositoryGetAllPlaces(Guid RquestId);

public record RepositoryGetPlaceRequest(Guid RequestId, long PlaceId);

public record RepositoryGetPlacesRequest(Guid RequestId, IEnumerable<long> PlacesId);

public record RepositoryCreatePlaceRequest(Guid RequestId, Place Place);

public record RepositoryUpdatePlaceRequest(Guid RequestId, Place Place);

public record RepositoryDeletePlaceRequest(Guid RequestId, Guid PlaceId);   