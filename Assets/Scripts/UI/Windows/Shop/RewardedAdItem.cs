using Assets.Scripts.Infrastructure.Services.Ads;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Windows.Shop
{
    public class RewardedAdItem: MonoBehaviour
    {
        public Button showAdButton;

        public GameObject[] adActiveObjects;
        public GameObject[] adInactiveObjects;

        private IAdsService _adsService;
        private IPersistentProgressService _progressService;

        public void Construst(IAdsService adsService, IPersistentProgressService progressService)
        {
            _adsService = adsService;
            _progressService = progressService;
        }

        public void Initialize()
        {
            showAdButton.onClick.AddListener(OnShowAdClick);
            RefreshAvailableAd();
        }

        public void Subscribe() => _adsService.OnRewardedVideoReady += RefreshAvailableAd;


        public void CleanUp() => _adsService.OnRewardedVideoReady -= RefreshAvailableAd;

        private void OnShowAdClick()
        {
            _adsService.ShowRewardedVideo(OnVideoFinished);
        }

        private void OnVideoFinished()
        {
            _progressService.Progress.WorldData.LootData.Collect(_adsService.Reward);
        }

        private void RefreshAvailableAd()
        {
            bool videoReady = _adsService.IsReady;

            foreach (GameObject ad in adActiveObjects)
            {
                ad.SetActive(videoReady);
            }

            foreach (GameObject ad in adInactiveObjects)
            {
                ad.SetActive(!videoReady);
            }
        }

    }
}
