namespace TrelloBoard.API.DTO
{
    public class EditUserStoryDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Acceptance_Criteria { get; set; }
        public int AsignadoA { get; set; }
        public int Estimacion { get; set; }
        public string Estado { get; set; }
    }
}
