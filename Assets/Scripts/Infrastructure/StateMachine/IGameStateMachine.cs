using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.StateMachine.StateInterfaces;

namespace Assets.Scripts.Infrastructure.StateMachine
{
    public interface IGameStateMachine:IService
    {
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        void Enter<TState>() where TState : class, IState;
    }
}