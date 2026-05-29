namespace TrelloBoard.Observers
{
    public class LogObserver : IUserStoryObserver
    {
        public void Notificar(string mensaje)
        {
            File.AppendAllText("log.txt", mensaje + "\n");
        }
    }
}
