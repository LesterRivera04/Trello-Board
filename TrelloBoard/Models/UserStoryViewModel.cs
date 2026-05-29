namespace TrelloBoard.Models
{
    public class UserStoryViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int AsignadoA { get; set; } 
        public string Estado { get; set; } = string.Empty;
        public int Estimacion { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string PokemonImageUrl { get; set; }
    }
}
