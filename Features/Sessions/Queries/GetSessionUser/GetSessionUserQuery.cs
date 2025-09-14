using MediatR;
using QuizMuzycznyAPI.Features.Users.Models;

namespace QuizMuzycznyAPI.Features.Sessions.Queries.GetSessionUser;

public class GetSessionUserQuery : IRequest<User>
{
    public string SessionId { get; set; }

    public GetSessionUserQuery(string sessionId)
    {
        SessionId = sessionId;
    }
}