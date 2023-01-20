﻿using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Logic;
using Assets.Scripts.Logic.CameraLogic;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.StateMachine
{
    public class LoadLevelState: IPayloadedState<string>
    {
        private const string INITIAL_POINT = "InitialPoint";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            InitializeGameWorld();
            InformProgressReader();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReader()
        {
            foreach(ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private void InitializeGameWorld()
        {
            GameObject player = _gameFactory.CreatePlayer(GameObject.FindGameObjectWithTag(INITIAL_POINT));
            _gameFactory.CreateHud();
            CameraFollow(player);
        }

        private void CameraFollow(GameObject player)
        {
             Camera.main
                .GetComponent<CameraFollow>()
                .SetFollowing(player);
        }
    }
}