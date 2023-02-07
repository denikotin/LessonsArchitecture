using System;
using System.Linq;
using UnityEngine.Purchasing;
using System.Collections.Generic;
using Assets.Scripts.Data.PlayerProgressFolder;

namespace Assets.Scripts.Infrastructure.Services.InApp
{
    public class IAPService : IIAPService
    {
        private readonly InAppProvider _inAppProvider;
        private readonly IPersistentProgressService _progressService;

        public bool IsAppProviderInitialized => _inAppProvider.IsInitialized;
        public event Action OnInitializedEvent;

        public IAPService(InAppProvider inAppProvider, IPersistentProgressService progressService)
        {
            _inAppProvider = inAppProvider;
            _progressService = progressService;
        }

        public void Initialize()
        {
            _inAppProvider.Initialize(this);
            _inAppProvider.OnInitializedEvent += () => OnInitializedEvent?.Invoke();
        }

        public List<ProductDescription> Products() => ProductDescriptions().ToList();

        public void StartPurchase(string productId) => _inAppProvider.StartPurchase(productId);

        public PurchaseProcessingResult ProcessPurchase(Product purchasedProduct)
        {
            ProductConfig config = _inAppProvider.configs[purchasedProduct.definition.id];

            switch (config.ItemType)
            {
                case ItemType.Skulls:
                    _progressService.Progress.WorldData.LootData.Collect(config.Quantity);
                    _progressService.Progress.PurchaseData.AddPurchase(purchasedProduct.definition.id);
                    break;
            }
            return PurchaseProcessingResult.Complete;
        }

        private IEnumerable<ProductDescription> ProductDescriptions()
        {
            PurchaseData purchaseData = _progressService.Progress.PurchaseData;

            foreach (string productId in _inAppProvider.products.Keys)
            {
                ProductConfig config = _inAppProvider.configs[productId];
                Product product = _inAppProvider.products[productId];

                BoughtIAP boughtIAP = purchaseData.boughtIAPs.Find(x => x.id == productId);
                if (boughtIAP != null && boughtIAP.count >= config.MaxPurchaseCount)
                {
                    continue;
                }

                yield return new ProductDescription
                {
                    Id = productId,
                    ProductConfig = config,
                    Product = product,
                    AvailablePurchasesLeft = boughtIAP != null ? config.MaxPurchaseCount - boughtIAP.count : config.MaxPurchaseCount,
                };
            }

        }
    }
}
