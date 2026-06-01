using System.ComponentModel.DataAnnotations;

namespace TrelloBoard.Models
{
    public class CreateUserStory
    {
        [Required(ErrorMessage = "El Título es requerido")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "La Descripción es requerida Lester")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El criterio de aceptación es requerido")]
        public string Acceptance_Criteria { get; set; }
        [Required(ErrorMessage = "Debe indicar a quien se asigna el US")]
        public int AsignadoA { get; set; }
        public int Estimacion { get; set; }
    }
}
