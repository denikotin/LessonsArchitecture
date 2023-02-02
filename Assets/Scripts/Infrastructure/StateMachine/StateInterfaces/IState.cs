namespace Assets.Scripts.Infrastructure.StateMachine.StateInterfaces
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}