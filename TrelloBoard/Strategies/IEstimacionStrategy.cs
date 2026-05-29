namespace TrelloBoard.Strategies
{
    public interface IEstimacionStrategy
    {
        Task<int> CalcularEstimacion();
    }
}
