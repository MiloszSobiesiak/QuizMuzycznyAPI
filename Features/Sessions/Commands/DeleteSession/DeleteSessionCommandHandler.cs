using MediatR;
using QuizMuzycznyAPI.Repositories;

namespace QuizMuzycznyAPI.Features.Sessions.Commands.DeleteSession;

public class DeleteSessionCommandHandler(ISessionsRepository sessionsRepository) : IRequestHandler<DeleteSessionCommand,  Unit>
{
    public async Task<Unit> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
    {
        await sessionsRepository.DeleteSessionAsync(request.SessionId);
        return Unit.Value;
    }
}