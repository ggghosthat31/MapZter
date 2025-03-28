using MapZter.Entity.Models;
using MediatR;

namespace MapZter.API.Requests;

public record struct GeocodeRequest(string address) : IRequest<Place[]>;