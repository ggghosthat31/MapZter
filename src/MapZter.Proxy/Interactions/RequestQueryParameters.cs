using MapZter.Entities.RequestFeatures;
using MapZter.Proxy.Tasks;

namespace MapZter.Proxy.Interactions;

public record RequestQueryParameters(
    long EntityId = default,
    IEnumerable<long> EntitiesId = default,
    RequestParameters RequestParameters = default) : ITaskDetails;
