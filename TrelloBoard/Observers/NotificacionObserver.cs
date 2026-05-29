namespace TrelloBoard.Observers
{
    public class NotificacionObserver : IUserStoryObserver
    {
        public void Notificar(string mensaje)
        {
            Console.WriteLine($"[Notificación]: {mensaje}");
        }
    }
}
