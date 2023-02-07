using Assets.Scripts.Infrastructure.Services.Ads;
using Assets.Scripts.Infrastructure.Services.AssetManagement;
using Assets.Scripts.Infrastructure.Services.InApp;
using Assets.Scripts.UI.Windows.Shop;
using TMPro;

namespace Assets.Scripts.UI.Windows
{
    public class ShopWindow : WindowBase
    {
        public TextMeshProUGUI skullText;
        public RewardedAdItem rewardedAdItem;
        public ShopItemsContainer ShopItemsContainer; 


        public void Construct(IAdsService adsService, IPersistentProgressService progressService, IIAPService iAPService, IAssetProvider assetProvider)
        {
            base.Construct(progressService);
            rewardedAdItem.Construst(adsService, progressService);
            ShopItemsContainer.Construct(iAPService, progressService, assetProvider);
        }

        protected override void Initialize()
        {
            rewardedAdItem.Initialize();
            ShopItemsContainer.Initialize();
;           RefreshSkullText();
        }

        protected override void SubscribeUpdates()
        {
            rewardedAdItem.Subscribe();
            ShopItemsContainer.Subscribe();
            playerProgress.WorldData.LootData.ChangedLootData += RefreshSkullText;
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            rewardedAdItem.CleanUp();
            ShopItemsContainer.CleanUp();
            playerProgress.WorldData.LootData.ChangedLootData -= RefreshSkullText;
        }

        private void RefreshSkullText()
        {
            skullText.text = playerProgress.WorldData.LootData.Collected.ToString();
        }
    }
}
