using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.StateMachine;
using Assets.Scripts.UI;

namespace Assets.Scripts.Infrastructure
{
    public class Game 
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container);
        }
    }
}
