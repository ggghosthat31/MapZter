using AutoMapper;
using MapZter.Contracts.Interfaces.Services;
using MapZter.Entity.Models;
using MapZter.Repository;
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
    private readonly IMapService _mapService;
    private readonly IMapper _mapper;
    private readonly RepositoryManager _repositoryManager;

    public MapHandler(
        IMapService mapService,
        IMapper mapper,
        RepositoryManager repositoryManager)
    {
        _mapService = mapService;
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }

    public async Task<string> Handle(MapRetrieveRequest mapRetrieveRequest, CancellationToken cancellationToken)
    {
        return await _mapService.GetMap();
    }

    public async Task<string> Handle(GetServerStatusRequest getServerStatusRequest, CancellationToken cancellationToken)
    {
        return await _mapService.GetServerStatus();
    }
    
    public async Task<Place[]> Handle(GeocodeRequest geocodeRequest, CancellationToken cancellationToken) =>
        await _mapService.Geocode(geocodeRequest.address);

    public async Task<Place> Handle(ReverseGeocodeRequest reverseGeocodeRequest, CancellationToken cancellationToken) =>
        await _mapService.ReverseGeocode(reverseGeocodeRequest.lat, reverseGeocodeRequest.lon);

    public async Task<Place> Handle(ReverseGeocodeDetailedRequest reverseGeocodeDetailedRequest, CancellationToken cancellationToken) =>
        await _mapService.ReverseGeocode(reverseGeocodeDetailedRequest.geoPoint);

    public async Task<Place[]> Handle(LookupRequest lookupRequest, CancellationToken cancellationToken) =>
        await _mapService.Lookup(lookupRequest.type, lookupRequest.placeId);
}