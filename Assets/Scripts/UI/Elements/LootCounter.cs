﻿using Assets.Scripts.Data.PlayerProgressFolder;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Elements
{
    public class LootCounter : MonoBehaviour
    {
        public TextMeshProUGUI counter;

        private WorldData _worldData;
        public void Constructor(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.ChangedLootData += UpdateCounter;
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            counter.text = $"{_worldData.LootData.Collected}";
        }
    }
}