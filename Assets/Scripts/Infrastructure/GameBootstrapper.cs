using Assets.Scripts.Infrastructure.StateMachine;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain LoadingCurtainPrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(LoadingCurtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(gameObject);
        }
    }

}
