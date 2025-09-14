using MediatR;
using QuizMuzycznyAPI.Features.Users.Models;
using QuizMuzycznyAPI.Repositories;

namespace QuizMuzycznyAPI.Features.Users.Commands.CreateOrUpdateUser;

public class CreateOrUpdateUserCommandHandler(IUsersRepository userRepository)
    : IRequestHandler<CreateOrUpdateUserCommand, User>
{
    public async Task<User> Handle(CreateOrUpdateUserCommand request, CancellationToken cancellationToken)
    {
        var spotifyUser = request.SpotifyUser;
        var user = await userRepository.GetBySpotifyIdAsync(spotifyUser.Id);

        if (user == null)
        {
            user = new User
            {
                SpotifyId = spotifyUser.Id,
                DisplayName = spotifyUser.DisplayName,
                Email = spotifyUser.Email,
                CreatedAt = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow,
                SpotifyAccessToken = spotifyUser.AccessToken,
                SpotifyRefreshToken = spotifyUser.RefreshToken,
                SpotifyTokenExpiry = spotifyUser.TokenExpiry
            };
            return await userRepository.AddAsync(user);
        }
        else
        {
            user.LastLogin = DateTime.UtcNow;
            user.DisplayName = spotifyUser.DisplayName;
            user.SpotifyAccessToken = spotifyUser.AccessToken;
            user.SpotifyRefreshToken = spotifyUser.RefreshToken;
            user.SpotifyTokenExpiry = spotifyUser.TokenExpiry;
            return await userRepository.UpdateAsync(user);
        }
    }
}