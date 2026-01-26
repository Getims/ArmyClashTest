namespace Project.Scripts.Core.Infrastructure.Events
{
    public interface IEvent
    {
        int ListenersCount();
    }
}