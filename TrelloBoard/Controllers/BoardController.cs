using TrelloBoard.Models;
using TrelloBoard.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TrelloBoard.Controllers
{
    public class BoardController : Controller
    {
        private readonly IUserStoryAPIServices _userStoryAPIServices;
        public BoardController(IUserStoryAPIServices userStoryAPIServices)
        {
            _userStoryAPIServices = userStoryAPIServices;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["DoneColor"] = "bg-success-Lester";
            var userStories = await _userStoryAPIServices.GetAllAsync();

            // mostrar usuarios por nombre en lugar de solo el id
            var usuarios = await _userStoryAPIServices.GetUsuariosAsync();
            ViewBag.Usuarios = usuarios;

            var board = new BoardViewModel();
            foreach (var us in userStories)
            {
                var usuario = usuarios.FirstOrDefault(u => u.Id == us.AsignadoA);
                us.NombreUsuario = usuario != null ? usuario.Nombre + " " + usuario.Apellidos : "Sin asignar, contactar a Lester";

                if (usuario != null && usuario.IdentificadorPokemon != null)
                {
                    us.PokemonImageUrl = await _userStoryAPIServices.GetPokemonImageAsync(usuario.IdentificadorPokemon);
                }
                else
                {
                    us.PokemonImageUrl = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/0.png"; // imagen por defecto si no hay identificador
                }
                switch (us.Estado)
                {
                    case "Backlog":
                        board.Backlog.Add(us);
                        break;
                    case "ToDo":
                        board.ToDo.Add(us);
                        break;
                    case "InProgress":
                        board.InProgress.Add(us);
                        break;
                    case "Done":
                        board.Done.Add(us);
                        break;
                }
            }
            return View(board);
        }

        [HttpPost]
        public async Task<IActionResult> Move(int id, string nuevoEstado)
        {
            await _userStoryAPIServices.MoveAsync(id, nuevoEstado);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateUserStory createDto)
        {
            if (!ModelState.IsValid)
            {
                var usuarios = await _userStoryAPIServices.GetUsuariosAsync();
                ViewBag.Usuarios = usuarios;
                // Aquí podría yo manejar el error, por ejemplo, mostrando un mensaje al usuario como "está mamando papi, asi no"
                return RedirectToAction("Index");
            }

            var mensajeCreado = await _userStoryAPIServices.AddAsync(createDto);
            TempData["UserStory_Creado"] = mensajeCreado;
            TempData.Keep("UserStroy_Creado");
            return RedirectToAction("Index");
        }
                
        [HttpGet]
        public async Task<IActionResult> CreateUserStory()
        {            
            return View();
        }
    }
}
