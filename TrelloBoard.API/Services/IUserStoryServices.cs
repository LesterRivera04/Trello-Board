using TrelloBoard.API.DTO;

namespace TrelloBoard.API.Services
{
    public interface IUserStoryServices
    {
        Task<IEnumerable<UserStoryDto>> GetAllAsync();
        Task<UserStoryDto> GetByIdAsync(int id);
        Task AddAsync(CreateUserStoryDto createDto);
        Task UpdateAsync(int id, UpdateUserStoryDto updateDto);
        Task EditAsync(int id, EditUserStoryDto editDto);
        Task DeleteAsync(int id);
    }
}
