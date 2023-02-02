using Assets.Scripts.UI.Services.Factory;

namespace Assets.Scripts.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowID windowID)
        {
            switch (windowID)
            {
                case WindowID.Unknown:
                    break;
                case WindowID.Shop:
                    _uiFactory.CreateShop();
                    break;
            }
        }
    }
}
