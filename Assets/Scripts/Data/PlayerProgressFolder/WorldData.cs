﻿using Assets.Scripts.Data.LootDataFolder;
using System;

namespace Assets.Scripts.Data.PlayerProgressFolder
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        public LootData LootData;

        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
            LootData = new LootData();
        }

    }
}
