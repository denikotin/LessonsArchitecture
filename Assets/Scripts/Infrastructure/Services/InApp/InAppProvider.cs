using System;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Data;
using UnityEngine.Purchasing;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Services.InApp;

public class InAppProvider : IStoreListener
{
    private const string IAP_CONFIG_PATH = "IAP/products";
    private IStoreController _controller;
    private IExtensionProvider _extensions;
    private IAPService _IAPService;

    public bool IsInitialized => _controller !=null && _extensions != null;
    public event Action OnInitializedEvent;
    public Dictionary<string,ProductConfig> configs { get; private set; }
    public Dictionary<string,Product> products { get; private set; }

    public void Initialize(IAPService iAPService)
    {
        _IAPService = iAPService;
        configs = new Dictionary<string,ProductConfig>();
        products = new Dictionary<string,Product>();

        Load();

        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        foreach (ProductConfig config in configs.Values)
        {
            Debug.Log(config.Id + " " + config.ProductType);
            builder.AddProduct(config.Id, config.ProductType);
            
        }
        Debug.Log(builder.products);
        UnityPurchasing.Initialize(this, builder);
    }

    public void StartPurchase(string productId)
    {
        _controller.InitiatePurchase(productId);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        _controller = controller;
        _extensions = extensions;

        foreach(Product product in _controller.products.all)
        {
            products.Add(product.definition.id, product);
        }

        OnInitializedEvent?.Invoke();

        Debug.Log("UnityPurchasing initialization success");
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"UnityPurchasing initialization failed:  {error}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogError($"Product {product.definition.id} purchase failed: {failureReason},  transaction ID {product.transactionID}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        Debug.Log($"UnityPurchasing process success {purchaseEvent.purchasedProduct.definition.id}");
        return _IAPService.ProcessPurchase(purchaseEvent.purchasedProduct);
    }

    private void Load()
    {
        
        configs = Resources.Load<TextAsset>(IAP_CONFIG_PATH).text
            .ToDeserialized<ProductConfigWrapper>().configs
            .ToDictionary(x=>x.Id, x=>x);
    }
}
