namespace TrelloBoard.API.DTO
{
    public class CreateUserStoryDto
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Acceptance_Criteria { get; set; }
        public int AsignadoA { get; set; }
        //public int Estado { get; set; }
        public int Estimacion { get; set; }
    }
}
