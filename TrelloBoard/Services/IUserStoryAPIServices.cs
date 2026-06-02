using TrelloBoard.Models;

namespace TrelloBoard.Services
{
    public interface IUserStoryAPIServices
    {
        Task<List<UserStoryViewModel>> GetAllAsync();
        Task<UserStoryViewModel> GetByIdAsync(int usuarioId);
        Task MoveAsync(int id, string nuevoEstado);
        Task<string> AddAsync(CreateUserStory createDto);
        Task<int> GetRandomEstimateAsync();
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<string> GetPokemonImageAsync(int id);
        Task EditAsync(int id, EditUserStoryViewModel updateDto);
        Task DeleteAsync(int id);
    }
}
