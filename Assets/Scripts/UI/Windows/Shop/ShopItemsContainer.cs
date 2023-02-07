using Assets.Scripts.Infrastructure.Services.AssetManagement;
using Assets.Scripts.Infrastructure.Services.InApp;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Windows
{
    public class ShopItemsContainer : MonoBehaviour
    {
        public const string ShopItemPath = "ShopItem";

        public GameObject[] ShopUnAvailableObjects;
        public Transform parent;
        private IIAPService _inAppService;
        private IPersistentProgressService _progressService;
        private IAssetProvider _assetProvider;
        private List<GameObject> _shopItems = new List<GameObject>();

        public void CleanUp()
        {
            _inAppService.OnInitializedEvent -= RefreshAvailableItems;
            _progressService.Progress.PurchaseData.OnChangedEvent -= RefreshAvailableItems;
        }

        public void Construct(IIAPService iAPService, IPersistentProgressService progressService, IAssetProvider assetProvider)
        {
            _inAppService = iAPService;
            _progressService = progressService;
            _assetProvider = assetProvider;
        }

        public void Initialize()
        {
            RefreshAvailableItems();
        }

        public void Subscribe()
        {
            _inAppService.OnInitializedEvent += RefreshAvailableItems;
            _progressService.Progress.PurchaseData.OnChangedEvent += RefreshAvailableItems;
        }
        private async void RefreshAvailableItems()
        {
            UpdateShopUnavailableObjects();

            if (!_inAppService.IsAppProviderInitialized)
            {
                return;
            }

            foreach(GameObject shopItem in _shopItems)
            {
                Destroy(shopItem.gameObject);
            }

            foreach(ProductDescription productDescription in _inAppService.Products())
            {
                GameObject shopItemObject =  await _assetProvider.InstantiateAsync(ShopItemPath, parent);
                ShopItem shopItem = shopItemObject.GetComponent<ShopItem>();
                shopItem.Construct(productDescription,_inAppService,_assetProvider);
                shopItem.Inititalize();

                _shopItems.Add(shopItemObject);
            }
        }

        private void UpdateShopUnavailableObjects()
        {
            foreach (GameObject shopUnavailableObject in ShopUnAvailableObjects)
            {
                shopUnavailableObject.SetActive(!_inAppService.IsAppProviderInitialized);

            }
        }
    }
}
