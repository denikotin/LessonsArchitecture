using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.RandomService
{
    public class RandomService : IRandomService
    {
        public int Next(int min, int max)
        {
            return Random.Range(min, max);
        }
    }
}
