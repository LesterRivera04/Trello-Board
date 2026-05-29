using TrelloBoard.API.DTO;
using TrelloBoard.API.Models;

namespace TrelloBoard.API.Services
{
    public interface IUsuarioServices
    {
        Task<IEnumerable<UsuarioDto>> GetAllUsuariosAsync();
        Task<UsuarioDto?> GetUsuarioByIdAsync(int id);
        Task AddUsuarioAsync(CreateUsuarioDto usuario);
        Task UpdateUsuarioAsync(int id, CreateUsuarioDto usuario);
        Task DeleteUsuarioAsync(int id);
    }
}
