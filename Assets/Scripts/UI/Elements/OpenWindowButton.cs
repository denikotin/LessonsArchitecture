using Assets.Scripts.UI.Services;
using Assets.Scripts.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Elements
{
    public class OpenWindowButton:MonoBehaviour
    {
        public Button button;

        public WindowID windowID;
        private IWindowService _windowService;

        public void Construct(IWindowService windowService) => _windowService = windowService;

        private void Awake() => button.onClick.AddListener(Open);

        private void Open() => _windowService.Open(windowID);
    }
}
