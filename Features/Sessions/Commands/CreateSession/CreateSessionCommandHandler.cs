using MediatR;
using QuizMuzycznyAPI.Repositories;

namespace QuizMuzycznyAPI.Features.Sessions.Commands.CreateSession;

public class CreateSessionCommandHandler(ISessionsRepository sessionsRepository) : IRequestHandler<CreateSessionCommand, Unit>
{
    public async Task<Unit> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        await sessionsRepository.CreateSessionAsync(request.SessionId, request.User);
        return Unit.Value;
    }
}