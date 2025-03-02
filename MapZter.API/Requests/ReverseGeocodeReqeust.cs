using MapZter.Entity.Models;
using MediatR;

namespace MapZter.API.Handlers;

public record struct ReverseGeocodeRequest(double lat, double lon) : IRequest<Place>;