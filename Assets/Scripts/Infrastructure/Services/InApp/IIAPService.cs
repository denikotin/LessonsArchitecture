using System;
using System.Collections.Generic;

namespace Assets.Scripts.Infrastructure.Services.InApp
{
    public interface IIAPService:IService
    {
        bool IsAppProviderInitialized { get; }

        event Action OnInitializedEvent;

        void Initialize();
        List<ProductDescription> Products();
        void StartPurchase(string productId);
    }
}