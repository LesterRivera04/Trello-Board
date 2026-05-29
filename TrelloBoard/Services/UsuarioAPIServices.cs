using TrelloBoard.Models;

namespace TrelloBoard.Services
{
    public class UsuarioAPIServices : IUsuarioAPIServices
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _httpClientPokemon;
        public UsuarioAPIServices(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CrudAPI");
            _httpClientPokemon = httpClientFactory.CreateClient("APIPokemon");
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Usuario>>("usuarios");
        }

        public async Task AddUsuarioAsync(CreateUsuario createDto)
        {
            var random = await GetRandomPokemonAsync();
            createDto.IdentificadorPokemon = random;
            var response = await _httpClient.PostAsJsonAsync("usuarios", createDto);
            response.EnsureSuccessStatusCode();
        }

        public async Task<int> GetRandomPokemonAsync()
        {
            try
            {
                var numero = await _httpClientPokemon.GetAsync("pokemon");
                if (!numero.IsSuccessStatusCode)
                    return 1; //fallback si el API Pokemon responde mal
                var result = await numero.Content.ReadAsStringAsync();
                return int.TryParse(result, out var value) ? value : 1;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Pokemon API falló");
                return 1;
            }
            
            //return numero;
        }
    }
}
