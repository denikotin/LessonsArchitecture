﻿using System;

namespace Assets.Scripts.Data.PlayerProgressFolder
{
    [Serializable]
    public class State
    {
        public float currentHealth;
        public float maxHealth;
        public void ResetHealth() => currentHealth = maxHealth;
    }
}
