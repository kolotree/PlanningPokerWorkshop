namespace Ports
{
    public interface IClientNotifier
    {
        void NotifyAllClients(string message);
    }
}