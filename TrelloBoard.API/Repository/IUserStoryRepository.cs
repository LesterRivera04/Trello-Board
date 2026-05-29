using TrelloBoard.API.Models;

namespace TrelloBoard.API.Repository
{
    public interface IUserStoryRepository
    {
        Task<IEnumerable<UserStory>> GetAllAsync();
        Task<UserStory> GetByIdAsync(int id);
        Task AddAsync(UserStory userStory);
        Task UpdateAsync(UserStory userStory);
        Task DeleteAsync(int id);
    }
}
