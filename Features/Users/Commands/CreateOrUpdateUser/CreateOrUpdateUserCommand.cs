using MediatR;
using QuizMuzycznyAPI.Features.Users.Models;

namespace QuizMuzycznyAPI.Features.Users.Commands.CreateOrUpdateUser;

public partial class CreateOrUpdateUserCommand : IRequest<User>
{
    public SpotifyUserDTO SpotifyUser { get; set; }
    public CreateOrUpdateUserCommand(SpotifyUserDTO spotifyUser) => SpotifyUser = spotifyUser;
}