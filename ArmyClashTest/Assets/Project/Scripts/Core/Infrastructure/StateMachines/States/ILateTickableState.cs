namespace Project.Scripts.Core.Infrastructure.StateMachines.States
{
    public interface ILateTickableState
    {
        void LateTick();
    }
}