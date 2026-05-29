namespace TrelloBoard.API.Models
{
    public enum UserStoryState
    {
        Backlog =1,
        ToDo =2,
        InProgress =3,
        Done = 4
    }
    public class UserStory
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int AsignadoA { get; set; } // FK
        public UserStoryState Estado { get; set; }
        public int Estimacion { get; set; }

        //propiedad de navegación para la relación con Usuario
        //[ForeignKey("AsignadoA")]
        public Usuario Usuario { get; set; }
    }
}
