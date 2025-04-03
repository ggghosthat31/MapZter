using MapZter.Entities.RequestFeatures;
using MapZter.Proxy.Tasks;

namespace MapZter.Proxy.Interactions;

public record PlaceQueryParameters(long PlaceId = default, IEnumerable<long> PlacesId = default, RequestParameters RequestParameters = default) : ITaskDetails;
