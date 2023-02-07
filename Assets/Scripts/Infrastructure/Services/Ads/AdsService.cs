using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Assets.Scripts.Infrastructure.Services.Ads
{
    public class AdsService : IUnityAdsShowListener, IUnityAdsLoadListener, IUnityAdsInitializationListener, IAdsService
    {
        private const string ANDROID_GAME_ID = "5145175";
        private const string IOS_GAME_ID = "5145174";
        private const string IOS_REWARDED_VIDEO_PLACEMENT_ID = "rewardedVideoIOS";
        private const string ANDROID_REWARDED_VIDEO_PLACEMENT_ID = "rewardedVideoAndroid";

        private string _gameId;
        private Action _onVideoFinish;
        public event Action OnRewardedVideoReady;

        public bool IsReady { get; private set; } = false;

        public int Reward => 10;


        public void Initialize()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    _gameId = ANDROID_GAME_ID;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    _gameId = IOS_GAME_ID;
                    break;
                case RuntimePlatform.WindowsEditor:
                    _gameId = ANDROID_GAME_ID;
                    break;
                default:
                    Debug.Log("Unsupported platfrom");
                    break;
            }
            Advertisement.Initialize(_gameId, true, this);
        }

        public void LoadRewardedVideo()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    Advertisement.Load(ANDROID_REWARDED_VIDEO_PLACEMENT_ID,this);
                    break;
                case RuntimePlatform.IPhonePlayer:
                    Advertisement.Load(IOS_REWARDED_VIDEO_PLACEMENT_ID, this);
                    break;
                case RuntimePlatform.WindowsEditor:
                    Advertisement.Load(ANDROID_REWARDED_VIDEO_PLACEMENT_ID, this);
                    break;
                default:
                    Debug.Log("Unsupported platfrom");
                    break;
            }
        }


        public void ShowRewardedVideo(Action OnVideoFinish)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    Advertisement.Show(ANDROID_REWARDED_VIDEO_PLACEMENT_ID);
                    break;
                case RuntimePlatform.IPhonePlayer:
                    Advertisement.Show(IOS_REWARDED_VIDEO_PLACEMENT_ID);
                    break;
                case RuntimePlatform.WindowsEditor:
                    Advertisement.Show(ANDROID_REWARDED_VIDEO_PLACEMENT_ID);
                    break;
                default:
                    Debug.Log("Unsupported platfrom");
                    break;
            }
            _onVideoFinish = OnVideoFinish;
        }


        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log($"OnUnityAdsAdLoaded {placementId}");

            if (placementId == IOS_REWARDED_VIDEO_PLACEMENT_ID)
            {
                OnRewardedVideoReady?.Invoke();
                IsReady = true;
            }
            else if (placementId == ANDROID_REWARDED_VIDEO_PLACEMENT_ID)
            {
                OnRewardedVideoReady?.Invoke();
                IsReady = true;
            }
            else
            {
                IsReady = false;
            }
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) => Debug.Log($"OnUnityAdsShowFailure: {placementId}. With message {message}");

        public void OnUnityAdsShowStart(string placementId) => Debug.Log($"OnUnityAdsShowStart {placementId}");

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.SKIPPED:
                    Debug.LogError($"OnUnityAdsShowComplete {showCompletionState}");
                    break;
                case UnityAdsShowCompletionState.COMPLETED:
                    _onVideoFinish?.Invoke();
                    break;
                case UnityAdsShowCompletionState.UNKNOWN:
                    Debug.LogError($"OnUnityAdsShowComplete {showCompletionState}");
                    break;
                default:
                    Debug.LogError($"OnUnityAdsShowComplete {showCompletionState}");
                    break;
            }
            _onVideoFinish = null;
        }

        public void OnInitializationComplete() => LoadRewardedVideo();

        public void OnInitializationFailed(UnityAdsInitializationError error, string message) { }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }

        public void OnUnityAdsShowClick(string placementId) { }

    }
}
