using TrelloBoard.API.Data;
using TrelloBoard.API.Models;
using Microsoft.EntityFrameworkCore;

namespace TrelloBoard.API.Repository
{
    public class UserStoryRepository : IUserStoryRepository
    {
        private readonly AppDbContext _context;
        public UserStoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserStory>> GetAllAsync()
        {
            return await _context.UserStories.ToListAsync();
        }

        public async Task<UserStory?> GetByIdAsync(int id)
        {
            return await _context.UserStories.FindAsync(id); //o FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(UserStory userStory)
        {
            await _context.UserStories.AddAsync(userStory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserStory userStory)
        {
            _context.UserStories.Update(userStory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userStory = await _context.UserStories.FindAsync(id);
            if (userStory != null)
            {
                _context.UserStories.Remove(userStory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
