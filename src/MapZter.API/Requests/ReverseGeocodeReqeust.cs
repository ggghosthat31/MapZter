using MapZter.Entity.Models;
using MediatR;

namespace MapZter.API.Handlers;

public record struct ReverseGeocodeRequest(double lat, double lon) : IRequest<Place>;

public record struct ReverseGeocodeDetailedRequest(GeoPoint geoPoint) : IRequest<Place>;