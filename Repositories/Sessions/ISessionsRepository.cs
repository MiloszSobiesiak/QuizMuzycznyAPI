using QuizMuzycznyAPI.Features.Users.Models;

namespace QuizMuzycznyAPI.Repositories;

public interface ISessionsRepository
{
    Task CreateSessionAsync(string sessionId, User user);
    Task<User> GetUserBySessionIdAsync(string sessionId);
    Task DeleteSessionAsync(string sessionId);
}