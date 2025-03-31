using MapZter.Entities.RequestFeatures;
using MapZter.Proxy.Tasks;

namespace MapZter.Proxy.Interactions;

public record PlaceQueryParameters(long PlaceId, IEnumerable<long> PlaceIds, RequestParameters RequestParameters) : ITaskDetails;
