using Assets.Scripts.Infrastructure.SceneLoaderFolder;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.StateMachine;
using Assets.Scripts.UI.Elements;

namespace Assets.Scripts.Infrastructure
{
    public class Game 
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, ServiceLocator.Container);
        }
    }
}
