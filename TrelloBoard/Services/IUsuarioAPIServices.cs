using TrelloBoard.Models;

namespace TrelloBoard.Services
{
    public interface IUsuarioAPIServices
    {
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task AddUsuarioAsync(CreateUsuario createDto);
        Task<int> GetRandomPokemonAsync();
    }
}
