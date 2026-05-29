using TrelloBoard.API.Models;

namespace TrelloBoard.API.DTO
{
    public class UpdateUserStoryDto
    {
        //public string Titulo { get; set; } = string.Empty;
        //public string Descripcion { get; set; } = string.Empty;
        //public string AsignadoA { get; set; } = string.Empty;
        public UserStoryState Estado { get; set; } //= string.Empty;
    }
}
