using MediatR;

namespace MapZter.API.Handlers;

public record GetServerStatusRequest() : IRequest<string>;