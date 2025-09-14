using QuizMuzycznyAPI.Features.Users.Models;

namespace QuizMuzycznyAPI.Repositories;


public interface IUsersRepository
{
    Task<User> GetBySpotifyIdAsync(string spotifyId);
    Task<User> AddAsync(User user);
    Task<User> UpdateAsync(User user);
}