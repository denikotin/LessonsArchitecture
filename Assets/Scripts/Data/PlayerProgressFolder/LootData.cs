using Assets.Scripts.Data.LootData;
using System;

namespace Assets.Scripts.Data.PlayerProgressFolder
{
    [Serializable]
    public class LootData
    {
        public int Collected;
        public Action ChangedLootData;

        public void Collect(Loot loot)
        {
            Collected += loot.value;
            ChangedLootData?.Invoke();
        }


    }
}