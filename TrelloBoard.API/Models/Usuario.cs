namespace TrelloBoard.API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public int IdentificadorPokemon { get; set; } = 1;

        //propiedad de navegación inversa (Usuario -> muchas UserStories)
        public List<UserStory> UserStories { get; set; }
    }
}
