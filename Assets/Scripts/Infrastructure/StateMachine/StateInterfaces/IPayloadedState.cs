namespace Assets.Scripts.Infrastructure.StateMachine.StateInterfaces
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }
}