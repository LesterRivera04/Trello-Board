using TrelloBoard.API.DTO;
using TrelloBoard.API.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static TrelloBoard.API.Services.UsuarioServices;

namespace TrelloBoard.API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServices _services;
        public UsuarioController(IUsuarioServices services)
        {
            _services = services;
        }

        //GET: api/Usuarios
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _services.GetAllUsuariosAsync();
            return Ok(usuarios); //uso return OK en lugar de return View() xq es una API, y lo que manda a MVC es un JSON, no una vista
        }

        //GET: api/Usuarios/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _services.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        //POST: api/Usuarios
        [HttpPost]
        public async Task<IActionResult> Create(CreateUsuarioDto usuario)
        {
            await _services.AddUsuarioAsync(usuario);
            return Ok();
        }

        //PUT: api/Usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateUsuarioDto usuario)
        {
            var existingUsuario = await _services.GetUsuarioByIdAsync(id);
            if (existingUsuario == null)
                return NotFound();
            await _services.UpdateUsuarioAsync(id, usuario);
            return NoContent();
        }

        //DELETE: api/Usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingUsuario = await _services.GetUsuarioByIdAsync(id);
            if (existingUsuario == null)
                return NotFound();
            await _services.DeleteUsuarioAsync(id);
            return NoContent();
        }

        [HttpGet("test-persona")]
        public IActionResult TestPersona()
        {
            Persona p1 = new Persona
            {
                Name = "Lester",
                Age = 30
            };

            p1.Saludar(); // Esto va al console log del servidor

            return Ok(p1);
        }

        public static List<Persona> personas = new List<Persona>();

        [HttpPost("crear-persona")]
        public IActionResult CrearPersona(Persona persona)
        {
            personas.Add(persona);
            return Ok(personas);
        }
    }
}
