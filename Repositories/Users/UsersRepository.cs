using Microsoft.EntityFrameworkCore;
using QuizMuzycznyAPI.Config;
using QuizMuzycznyAPI.Features.Users.Models;

namespace QuizMuzycznyAPI.Repositories;

public class UsersRepository(AppDbContext db) : IUsersRepository
{
    public async Task<User> GetBySpotifyIdAsync(string spotifyId)
    {
        return await db.Users.FirstOrDefaultAsync(u => u.SpotifyId == spotifyId);
    }
    
    public async Task<User> AddAsync(User user)
    {
        db.Users.Add(user);
        await db.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        db.Users.Update(user);
        await db.SaveChangesAsync();
        return user;
    }
}