using MediatR;

namespace MapZter.API.Handlers;

public record struct MapRetrieveRequest() : IRequest<string>;