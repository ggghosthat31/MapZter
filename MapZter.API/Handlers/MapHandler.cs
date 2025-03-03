using MapZter.Entity.Models;
using MediatR;

namespace MapZter.API.Handlers;

public class MapHandler :  
    IRequestHandler<MapRetrieveRequest, string>,
    IRequestHandler<GeocodeRequest, Place[]>,
    IRequestHandler<ReverseGeocodeRequest, Place>,
    IRequestHandler<ReverseGeocodeDetailedRequest, Place>,
    IRequestHandler<LookupRequest, Place[]>,
    IRequestHandler<GetServerStatusRequest, string>
{
    public async Task<string> Handle(MapRetrieveRequest mapRetrieveRequest, CancellationToken cancellationToken)
    {
        return null;
    }

    public async Task<Place[]> Handle(GeocodeRequest geocodeRequest, CancellationToken cancellationToken)
    {
        return null;
    }

    public async Task<Place> Handle(ReverseGeocodeRequest reverseGeocodeRequest, CancellationToken cancellationToken)
    {
        return null;
    }

    public async Task<Place> Handle(ReverseGeocodeDetailedRequest reverseGeocodeDetailedRequest, CancellationToken cancellationToken)
    {
        return null;
    }

    public async Task<Place[]> Handle(LookupRequest lookupRequest, CancellationToken cancellationToken)
    {
        return null;
    }

    public async Task<string> Handle(GetServerStatusRequest getServerStatusRequest, CancellationToken cancellationToken)
    {
        return null;
    }
}