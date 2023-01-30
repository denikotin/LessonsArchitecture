using UnityEngine;

namespace Assets.Scripts.StaticData.EnemyStaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeID monsterID;

        [Range(1f, 100f)]
        public int health;

        [Range(1f, 30f)]
        public float damage;

        public int MaxLoot;
        public int MinLoot;

        [Range(0, 50f)]
        public float moveSpeed;

        [Range(0.5f, 1f)]
        public float effectiveDistance;

        [Range(0.5f, 1f)]
        public float cleavage;

        public GameObject monsterPrefab;
    }
}
