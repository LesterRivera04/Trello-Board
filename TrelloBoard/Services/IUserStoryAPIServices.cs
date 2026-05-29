using TrelloBoard.Models;

namespace TrelloBoard.Services
{
    public interface IUserStoryAPIServices
    {
        Task<List<UserStoryViewModel>> GetAllAsync();
        Task MoveAsync(int id, string nuevoEstado);
        Task<string> AddAsync(CreateUserStory createDto);
        Task<int> GetRandomEstimateAsync();
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<string> GetPokemonImageAsync(int id);
    }
}
