using Assets.Scripts.Data.PlayerProgressFolder;
using Assets.Scripts.Infrastructure.Services.SaveLoadService;

namespace Assets.Scripts.Infrastructure.StateMachine
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressServices, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressServices;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {

        }

        private void LoadProgressOrInitNew() => _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            var progress =  new PlayerProgress(initialLevel: "Main");
            
            progress.HeroState.maxHealth = 50f;
            progress.HeroState.ResetHealth();
            progress.Stats.damage = 1f;
            progress.Stats.damageRadius = 0.5f;
            return progress;
        }
    }
}
