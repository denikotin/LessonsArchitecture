using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string sceneName, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadSceneAsync(sceneName, onLoaded));

        private IEnumerator LoadSceneAsync(string sceneName, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoaded?.Invoke();
                yield break;
            }
            AsyncOperation loadNextScene = SceneManager.LoadSceneAsync(sceneName);

            while (!loadNextScene.isDone)
            {
                yield return null;
            }

            onLoaded?.Invoke();
        }
    }
}