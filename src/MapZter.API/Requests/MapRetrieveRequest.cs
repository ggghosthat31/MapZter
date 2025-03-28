using MediatR;

namespace MapZter.API.Requests;

public record struct MapRetrieveRequest() : IRequest<string>;