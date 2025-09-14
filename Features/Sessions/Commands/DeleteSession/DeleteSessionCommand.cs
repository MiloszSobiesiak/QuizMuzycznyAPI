using MediatR;

namespace QuizMuzycznyAPI.Features.Sessions.Commands.DeleteSession;

public record DeleteSessionCommand(string SessionId) : IRequest<Unit>;