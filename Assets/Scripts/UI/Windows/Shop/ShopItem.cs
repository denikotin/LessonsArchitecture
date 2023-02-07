using Assets.Scripts.Infrastructure.Services.AssetManagement;
using Assets.Scripts.Infrastructure.Services.InApp;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Windows
{
    public class ShopItem:MonoBehaviour
    {
        public Button buyItemButton;
        public TextMeshProUGUI priceText;
        public TextMeshProUGUI quantityText;
        public TextMeshProUGUI availableItemsLeftText;
        public Image icon;
        private ProductDescription _productDescription;
        private IIAPService _iapService;
        private IAssetProvider _assets;

        public void Construct(ProductDescription productDescription, IIAPService iAPService, IAssetProvider assetProvider)
        {
            _productDescription = productDescription;
            _iapService = iAPService;
            _assets = assetProvider;
        }

        public async void Inititalize()
        {
            buyItemButton.onClick.AddListener(OnBuyItemClick);

            priceText.text = _productDescription.ProductConfig.Price;
            quantityText.text = _productDescription.ProductConfig.Quantity.ToString();
            availableItemsLeftText.text = _productDescription.AvailablePurchasesLeft.ToString();
            icon.sprite =  await _assets.Load<Sprite>(_productDescription.ProductConfig.icon);
        }

        private void OnBuyItemClick()
        {
            _iapService.StartPurchase(_productDescription.Id);
        }
    }
}
