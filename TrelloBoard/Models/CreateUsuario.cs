using System.ComponentModel.DataAnnotations;

namespace TrelloBoard.Models
{
    public class CreateUsuario
    {
        [Required (ErrorMessage = "El Nombre es requerido")]
        public string Nombre { get; set; }
        [Required (ErrorMessage = "Los Apellidos son requeridos")]
        public string Apellidos { get; set; }
        [Required (ErrorMessage = "El email es requerido")]
        [EmailAddress (ErrorMessage = "Formato de email inválido. Ejemplo de formato válido: usuario@email.com")]
        public string Email { get; set; }
        public int IdentificadorPokemon { get; set; }
    }
}
