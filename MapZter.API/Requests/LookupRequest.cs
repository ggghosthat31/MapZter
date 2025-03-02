using MapZter.Entity.Models;
using MediatR;

namespace MapZter.API.Handlers;

public record LookupRequest(char type, long placeId) : IRequest<Place[]>;