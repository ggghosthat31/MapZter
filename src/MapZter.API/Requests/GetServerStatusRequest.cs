using MediatR;

namespace MapZter.API.Requests;

public record GetServerStatusRequest() : IRequest<string>;