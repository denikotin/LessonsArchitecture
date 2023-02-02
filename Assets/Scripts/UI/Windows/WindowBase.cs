using Assets.Scripts.Data.PlayerProgressFolder;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public Button CloseButton;
        protected IPersistentProgressService progressService;
        protected PlayerProgress playerProgress => progressService.Progress;

        public void Construct(IPersistentProgressService progressService)
        {
            this.progressService = progressService;
        }

        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy()
        {
            CleanUp();
        }

        protected virtual void OnAwake() => CloseButton.onClick.AddListener(() => Destroy(gameObject));

        protected virtual void Initialize() { }
        protected virtual void SubscribeUpdates() { }
        protected virtual void CleanUp() { }

    }
}
