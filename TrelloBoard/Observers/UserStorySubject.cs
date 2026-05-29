namespace TrelloBoard.Observers
{
    public class UserStorySubject
    {
        private readonly List<IUserStoryObserver> _observers = new();
        public void Suscribir(IUserStoryObserver observer)
        {
            _observers.Add(observer);
        }

        public void Desuscribir(IUserStoryObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotificarTodos(string mensaje)
        {
            foreach (var observer in _observers)
            {
                observer.Notificar(mensaje);
            }
        }
    }
}
