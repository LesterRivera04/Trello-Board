namespace TrelloBoard.API.DTO
{
    public class UserStoryDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int AsignadoA { get; set; }
        public string Estado { get; set; }
        public int Estimacion { get; set; }

        //
    }
}
