using TrelloBoard.API.DTO;
using TrelloBoard.API.Models;
using TrelloBoard.API.Repository;

namespace TrelloBoard.API.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioServices(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<IEnumerable<UsuarioDto>> GetAllUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellidos = u.Apellidos,
                Email = u.Email,
                IdentificadorPokemon = u.IdentificadorPokemon
            });
        }

        public async Task<UsuarioDto?> GetUsuarioByIdAsync(int id)
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                if (usuario == null)
                    return null;

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellidos = usuario.Apellidos,
                Email = usuario.Email,
                IdentificadorPokemon = usuario.IdentificadorPokemon
                };
        }

        public async Task AddUsuarioAsync(CreateUsuarioDto usuario)
        {
            var nuevoUsuario = new Usuario
            {
                Nombre = usuario.Nombre,
                Apellidos = usuario.Apellidos,
                Email = usuario.Email,
                IdentificadorPokemon = usuario.IdentificadorPokemon
            };
            await _usuarioRepository.AddAsync(nuevoUsuario);
        }

        public async Task UpdateUsuarioAsync(int id, CreateUsuarioDto usuario)
        {
            var existingUsuario = await _usuarioRepository.GetByIdAsync(id);
            if (existingUsuario == null)
                throw new Exception("Usuario no encontrado");

            existingUsuario.Nombre = usuario.Nombre;
            existingUsuario.Apellidos = usuario.Apellidos;
            existingUsuario.Email = usuario.Email;
            existingUsuario.IdentificadorPokemon = usuario.IdentificadorPokemon;

            await _usuarioRepository.UpdateAsync(existingUsuario);
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }

        public class Persona
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public void Saludar()
            {
                Console.WriteLine("Hola, soy " + Name);
            }
        }
    }
}
