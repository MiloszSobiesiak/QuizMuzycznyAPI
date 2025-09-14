using MediatR;
using QuizMuzycznyAPI.Features.Users.Models;

namespace QuizMuzycznyAPI.Features.Sessions.Commands.CreateSession;

public class CreateSessionCommand : IRequest<Unit>
{
    public string SessionId { get; set; }
    public User User { get; set; }

    public CreateSessionCommand(string sessionId, User user)
    {
        SessionId = sessionId;
        User = user;
    }
}