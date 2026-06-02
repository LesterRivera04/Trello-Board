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
                Acceptance_Criteria = us.Acceptance_Criteria,
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
                Acceptance_Criteria = userStory.Acceptance_Criteria,
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
                Acceptance_Criteria = createDto.Acceptance_Criteria,
                AsignadoA = createDto.AsignadoA,
                Estado = UserStoryState.Backlog, // Estado inicial
                Estimacion = createDto.Estimacion // Estimación inicial, luego lo reemplazo con llamada a Minimal API
            };
            await _repository.AddAsync(userStory);
        }

        public async Task UpdateAsync(int id, UpdateUserStoryDto updateDto) // este método es para actualizar solo el estado, no el card completo, lo uso para mover la card entre columnas del tablero
        {
            var userStory = await _repository.GetByIdAsync(id);
            if (userStory == null) return;
            userStory.Estado = updateDto.Estado; //con el True ignora mayúsculas o minúsculas
            await _repository.UpdateAsync(userStory);
        }

        public async Task EditAsync(int id, EditUserStoryDto editDto) // este metodo es para editar el card completo
        {
            var userStory = await _repository.GetByIdAsync(id);
            if (userStory == null)
            {
                throw new Exception("User Story no encontrada");
            }
            if (!string.IsNullOrEmpty(editDto.Titulo))
            {
                userStory.Titulo = editDto.Titulo;
            }
            if (editDto.AsignadoA != 0)
            {
                userStory.AsignadoA = editDto.AsignadoA;
            }
            if (!string.IsNullOrEmpty(editDto.Descripcion))
            {
                userStory.Descripcion = editDto.Descripcion;
            }
            if (!string.IsNullOrEmpty(editDto.Acceptance_Criteria))
            {
                userStory.Acceptance_Criteria = editDto.Acceptance_Criteria;
            }
            if (editDto.Estimacion != 0)
            {
                userStory.Estimacion = editDto.Estimacion;
            }
            if (!string.IsNullOrEmpty(editDto.Estado))
            {
                if (Enum.TryParse<UserStoryState>(editDto.Estado, true, out var estadoEnum))
                {
                    userStory.Estado = estadoEnum;
                }
                else
                {
                    throw new Exception("Estado inválido");
                }
            }

            await _repository.UpdateAsync(userStory);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
