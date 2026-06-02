using TrelloBoard.Models;
using TrelloBoard.Strategies;
using System.Net.Http.Json;
using System.Text.Json;
using TrelloBoard.Observers;

namespace TrelloBoard.Services
{
    public class UserStoryAPIServices : IUserStoryAPIServices
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _httpClientMinimalAPI;
        private readonly IHttpClientFactory _httpClientFactory;
        //private readonly IEstimacionStrategy _estimacionStrategy;
        public UserStoryAPIServices(IHttpClientFactory httpClientFactory)
        {            
            _httpClient = httpClientFactory.CreateClient("CrudAPI");
            _httpClientMinimalAPI = httpClientFactory.CreateClient("MinimalAPI");
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<UserStoryViewModel>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<UserStoryViewModel>>("historiasdeusuario");
        }

        public async Task<UserStoryViewModel?> GetByIdAsync(int usuarioId)
        {
            return await _httpClient.GetFromJsonAsync<UserStoryViewModel>($"historiasdeusuario/{usuarioId}");
        }
                                
        public async Task<string> AddAsync(CreateUserStory createDto)
        {
            //var estimacion = await GetRandomEstimateAsync(); // aqui llamaba al MinimalAPI por medio del metodo GetRandomEstimateAsync,
            //pero ahora voy a probar Strategy que esta en la carpeta Strategies y puedo tener varias implementaciones distintas
            //de estimacion y elegir cual usar sin cambiar este servicio

            //IEstimacionStrategy strategy = new ApiEstimacionStrategy(_httpClientFactory); //puedo cambiar esta linea por RandomLocalEstimacionStrategy() y no tengo que tocar nada mas en este servicio, eso es lo bueno de usar Strategy
            IEstimacionStrategy strategy;

            if (DateTime.Now.Second % 2 == 0) // por ejemplo, elijo la estrategia segun si el segundo actual es par o impar, solo para probar
                strategy = new ApiEstimacionStrategy(_httpClientFactory);
            else
                strategy = new RandomLocalEstimacionStrategy();

            var estimacion = await strategy.CalcularEstimacion();
            createDto.Estimacion = estimacion;
            var response = await _httpClient.PostAsJsonAsync("historiasdeusuario", createDto);
            response.EnsureSuccessStatusCode();

            var subject = new UserStorySubject();
            subject.Suscribir(new NotificacionObserver());
            subject.Suscribir(new LogObserver());

            var mensajeCreado = $"User Story <b>'{createDto.Titulo}'</b> creado exitosamente, fecha y hora: " + DateTime.Now;
            subject.NotificarTodos(mensajeCreado);

            return mensajeCreado;
        }

        public async Task<int> GetRandomEstimateAsync()
        {
            var estimacion = await _httpClientMinimalAPI.GetFromJsonAsync<int>("estimate");
            return estimacion;
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Usuario>>("usuarios");
        }
                
        public async Task<string> GetPokemonImageAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}");
            if (!response.IsSuccessStatusCode)
                return "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/1.png"; //fallback a imagen de Bulbasaur
            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var image = doc.RootElement
                .GetProperty("sprites")
                .GetProperty("front_default")
                .GetString();

            return image ?? "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/1.png"; //fallback a imagen de Bulbasaur
        }

        public async Task MoveAsync(int id, string nuevoEstado)
        {
            if (!Enum.TryParse<UserStoryState>(nuevoEstado, out var estadoEnum))
                throw new Exception("Estado inválido");

            Console.WriteLine($"ID enviado desde MVC: {id}");
            Console.WriteLine($"Estado enviado desde MVC: {(int)estadoEnum}");

            var response = await _httpClient.PutAsJsonAsync(
                $"historiasdeusuario/{id}", new { Estado = (int)estadoEnum });
            response.EnsureSuccessStatusCode();
        }

        public async Task EditAsync(int id, EditUserStoryViewModel updateDto)
        {
            var userStory = await GetByIdAsync(id);

            var response = await _httpClient.PutAsJsonAsync($"historiasdeusuario/Edit/{id}", updateDto);
            response.EnsureSuccessStatusCode();

            var subject = new UserStorySubject();
            subject.Suscribir(new NotificacionObserver());
            subject.Suscribir(new LogObserver());
                        
            var cardEditado = $"User Story <b>{userStory?.Titulo}</b> editado exitosamente, fecha y hora: " + DateTime.Now;
            subject.NotificarTodos(cardEditado);
        }

        public async Task DeleteAsync(int id)
        {
            var userStory = await GetByIdAsync(id);

            var response = await _httpClient.DeleteAsync($"historiasdeusuario/{id}");
            response.EnsureSuccessStatusCode();

            var subject = new UserStorySubject();
            subject.Suscribir(new NotificacionObserver());
            subject.Suscribir(new LogObserver());

            var mensajeEliminado = $"User Story <b>{userStory?.Titulo}</b> eliminada exitosamente, fecha y hora: " + DateTime.Now;
            subject.NotificarTodos(mensajeEliminado);

        }
    }
}
