using Assets.Scripts.Infrastructure.StateMachine;
using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain loadingCurtainPrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(loadingCurtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(gameObject);
        }
    }

}
