using UnityEngine;
using System.Threading.Tasks;
using Assets.Scripts.UI.Windows;
using Assets.Scripts.Infrastructure.Services.Ads;
using Assets.Scripts.Infrastructure.Services.InApp;
using Assets.Scripts.StaticData.WindowsStaticDataFolder;
using Assets.Scripts.Infrastructure.Services.AssetManagement;
using Assets.Scripts.Infrastructure.Services.StaticDataService;

namespace Assets.Scripts.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private Transform _uiRoot;
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;
        private readonly IAdsService _adsService;
        private readonly IIAPService _iAPService;

        public UIFactory(IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressService, IAdsService adsService, IIAPService iAPService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
            _adsService = adsService;
            _iAPService = iAPService;
        }

        public void CreateShop()
        {
            WindowConfig windowConfig = _staticData.ForWindow(WindowID.Shop);
            ShopWindow window = Object.Instantiate(windowConfig.Prefab, _uiRoot) as ShopWindow;
            window.Construct(_adsService,_progressService,_iAPService,_assets);
        }

        public async Task CreateUIRoot()
        {
            GameObject root = await _assets.InstantiateAsync(AssetsAddress.UIROOT_PATH);
            _uiRoot =  root.transform ;
        }
        
    }
}
