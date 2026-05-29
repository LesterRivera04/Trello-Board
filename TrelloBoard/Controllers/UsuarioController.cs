using TrelloBoard.Models;
using TrelloBoard.Services;
using Microsoft.AspNetCore.Mvc;

namespace TrelloBoard.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioAPIServices _usuarioAPIServices;
        public UsuarioController(IUsuarioAPIServices usuarioAPIServices)
        {
            _usuarioAPIServices = usuarioAPIServices;
        }
        public IActionResult CrearUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUsuario(CreateUsuario createDto)
        {
            var pokemonId = await _usuarioAPIServices.GetRandomPokemonAsync();
            createDto.IdentificadorPokemon = pokemonId;
            await _usuarioAPIServices.AddUsuarioAsync(createDto);
            return RedirectToAction("Index", "Board");
        }
    }
}
