using System;

namespace Assets.Scripts.Infrastructure.Services.Ads
{
    public interface IAdsService:IService
    {
        bool IsReady { get; }
        int Reward { get; }

        event Action OnRewardedVideoReady;

        void Initialize();
        void LoadRewardedVideo();
        void ShowRewardedVideo(Action OnVideoFinish);
    }
}