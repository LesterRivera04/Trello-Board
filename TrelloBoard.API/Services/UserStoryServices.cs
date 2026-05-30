using TrelloBoard.API.DTO;
using TrelloBoard.API.Repository;
using TrelloBoard.API.Models;

namespace TrelloBoard.API.Services
{
    public class UserStoryServices : IUserStoryServices
    {
        private readonly IUserStoryRepository _repository;
        public UserStoryServices(IUserStoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserStoryDto>> GetAllAsync()
        {
            var userStories = await _repository.GetAllAsync();
            return userStories.Select(us => new UserStoryDto
            {
                Id = us.Id,
                Titulo = us.Titulo,
                Descripcion = us.Descripcion,
                AsignadoA = us.AsignadoA,
                Estado = us.Estado.ToString(),
                Estimacion = us.Estimacion
            });
        }

        public async Task<UserStoryDto?> GetByIdAsync(int id)
        {
            var userStory = await _repository.GetByIdAsync(id);
            if (userStory == null) return null;
            return new UserStoryDto
            {
                Id = userStory.Id,
                Titulo = userStory.Titulo,
                Descripcion = userStory.Descripcion,
                AsignadoA = userStory.AsignadoA,
                Estado = userStory.Estado.ToString(),
                Estimacion = userStory.Estimacion
            };
        }

        public async Task AddAsync(CreateUserStoryDto createDto)
        {
            var userStory = new Models.UserStory
            {
                Titulo = createDto.Titulo,
                Descripcion = createDto.Descripcion,
                AsignadoA = createDto.AsignadoA,
                Estado = UserStoryState.Backlog, // Estado inicial
                Estimacion = createDto.Estimacion // Estimación inicial, luego lo reemplazo con llamada a Minimal API
            };
            await _repository.AddAsync(userStory);
        }

        public async Task UpdateAsync(int id, UpdateUserStoryDto updateDto)
        {
            var userStory = await _repository.GetByIdAsync(id);
            if (userStory == null) return;
            //userStory.Titulo = updateDto.Titulo;
            //userStory.Descripcion = updateDto.Descripcion;
            //userStory.AsignadoA = updateDto.AsignadoA;
            userStory.Estado = updateDto.Estado; //con el True ignora mayúsculas o minúsculas
            await _repository.UpdateAsync(userStory);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
