using MapZter.Entities.RequestFeatures;

namespace MapZter.Proxy;

public record PlaceQueryParameters(long PlaceId, IEnumerable<long> PlaceIds, RequestParameters RequestParameters) : ITaskDetails;
