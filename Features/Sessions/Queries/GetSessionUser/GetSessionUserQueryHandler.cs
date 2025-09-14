using MediatR;
using QuizMuzycznyAPI.Features.Users.Models;
using QuizMuzycznyAPI.Repositories;

namespace QuizMuzycznyAPI.Features.Sessions.Queries.GetSessionUser;


public class GetSessionUserQueryHandler(ISessionsRepository sessionsRepository)
    : IRequestHandler<GetSessionUserQuery, User>
{

    public async Task<User> Handle(GetSessionUserQuery request, CancellationToken cancellationToken)
    {
        var user = await sessionsRepository.GetUserBySessionIdAsync(request.SessionId);
        return user;
    }
}