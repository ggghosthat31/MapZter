using MapZter.Entity.Models;
using MediatR;

namespace MapZter.API.Handlers;

public record struct GeocodeRequest(string address) : IRequest<Place[]>;

