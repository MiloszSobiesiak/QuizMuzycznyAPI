using MediatR;
using MediatR.Pipeline;
using QuizMuzycznyAPI.Features.Sessions.Queries.GetSessionUser;

namespace QuizMuzycznyAPI.Features._Base.RequireSpotifyAuth;


public class SpotifyAuthPreProcessor<TRequest>(IHttpContextAccessor httpContextAccessor, IMediator mediator)
    : IRequestPreProcessor<TRequest>
    where TRequest : IRequireSpotifyAuth
{
    private readonly IMediator _mediator = mediator;

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var context = httpContextAccessor.HttpContext!;
        if (!context.Request.Cookies.TryGetValue("session_id", out var sessionId))
            throw new UnauthorizedAccessException();

        var user = await _mediator.Send(new GetSessionUserQuery(sessionId), cancellationToken);
        if (user == null)
            throw new UnauthorizedAccessException();
        
        context.Items["SpotifyUser"] = user;
    }
}