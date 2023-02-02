using Assets.Scripts.Infrastructure.Services.Ads;
using Assets.Scripts.Infrastructure.Services.AssetManagement;
using Assets.Scripts.Infrastructure.Services.StaticDataService;
using Assets.Scripts.StaticData.WindowsStaticDataFolder;
using Assets.Scripts.UI.Windows;
using UnityEngine;

namespace Assets.Scripts.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private Transform _uiRoot;
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;
        private readonly IAdsService _adsService;

        public UIFactory(IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressService, IAdsService adsService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
            _adsService = adsService;
        }

        public void CreateShop()
        {
            WindowConfig windowConfig = _staticData.ForWindow(WindowID.Shop);
            ShopWindow window = Object.Instantiate(windowConfig.Prefab, _uiRoot) as ShopWindow;
            window.Construct(_adsService,_progressService);
        }

        public void CreateUIRoot() => _uiRoot = _assets.Instantiate(AssetsPath.UIROOT_PATH).transform;
    }
}
