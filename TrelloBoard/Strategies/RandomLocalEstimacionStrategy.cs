namespace TrelloBoard.Strategies
{
    public class RandomLocalEstimacionStrategy : IEstimacionStrategy
    {
        public Task<int> CalcularEstimacion()
        {
            //var valores = new[] { 1, 2, 3, 5, 8 }; // digamos que quiero usar Fibonacci pero local en lugar de API
            //var random = new Random();
            //return Task.FromResult(valores[random.Next(valores.Length)]);

            var random = new Random();
            return Task.FromResult(random.Next(1, 10)); // Estimación aleatoria entre 1 y 10
        }
    }
}
