using Assets.Scripts.Data.PlayerProgressFolder;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class LootCounter : MonoBehaviour
    {
        public TextMeshProUGUI counter;

        private WorldData _worldData;
        public void Constructor(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.ChangedLootData += UpdateCounter;
        }

        private void Start()
        {
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            counter.text = $"{_worldData.LootData.Collected}";
        }
    }
}