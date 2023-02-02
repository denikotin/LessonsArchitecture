using System;

namespace Assets.Scripts.Data.LootDataFolder
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

        public void Collect(int value)
        {
            Collected += value;
            ChangedLootData?.Invoke();
        }


    }
}