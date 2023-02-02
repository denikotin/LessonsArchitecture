using Assets.Scripts.Infrastructure.Services.Ads;
using Assets.Scripts.UI.Windows.Shop;
using TMPro;

namespace Assets.Scripts.UI.Windows
{
    public class ShopWindow : WindowBase
    {
        public TextMeshProUGUI skullText;
        public RewardedAdItem rewardedAdItem;


        public void Construct(IAdsService adsService, IPersistentProgressService progressService)
        {
            base.Construct(progressService);
            rewardedAdItem.Construst(adsService, progressService);
        }

        protected override void Initialize()
        {
            rewardedAdItem.Initialize();
;           RefreshSkullText();
        }

        protected override void SubscribeUpdates()
        {
            rewardedAdItem.Subscribe();
            playerProgress.WorldData.LootData.ChangedLootData += RefreshSkullText;
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            rewardedAdItem.CleanUp();
            playerProgress.WorldData.LootData.ChangedLootData -= RefreshSkullText;
        }

        private void RefreshSkullText()
        {
            skullText.text = playerProgress.WorldData.LootData.Collected.ToString();
        }
    }
}
