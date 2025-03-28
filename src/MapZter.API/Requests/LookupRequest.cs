using MapZter.Entity.Models;
using MediatR;

namespace MapZter.API.Requests;

public record LookupRequest(char type, long placeId) : IRequest<Place[]>;