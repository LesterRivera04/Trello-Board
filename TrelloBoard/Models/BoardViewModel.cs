namespace TrelloBoard.Models
{
    public class BoardViewModel
    {
        public List<UserStoryViewModel> Backlog { get; set; } = new();
        public List<UserStoryViewModel> ToDo { get; set; } = new();
        public List<UserStoryViewModel> InProgress { get; set; } = new();
        public List<UserStoryViewModel> Done { get; set; } = new();

        //esto lo pongo para llamarlo desde el Modal
        public CreateUserStory NuevaHistoria { get; set; } = new();
        public CreateUsuario NuevoUsuario { get; set; }

        //aqui para mostrar la imagen pokemon en cada User Story card
        public string PokemonImageUrl { get; set; }
    }
}
