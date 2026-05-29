namespace TrelloBoard.Strategies
{
    public class ApiEstimacionStrategy : IEstimacionStrategy
    {
        private readonly HttpClient _httpClient;
        public ApiEstimacionStrategy(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MinimalAPI");
        }

        public async Task<int> CalcularEstimacion()
        {
            return await _httpClient.GetFromJsonAsync<int>("estimate");
        }
    }
}
