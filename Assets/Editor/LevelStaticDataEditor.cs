using Assets.Scripts.StaticData.LevelStaticDataFolder;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Logic;
using UnityEngine.SceneManagement;
using Assets.Scripts.Enemy.EnemySpawnScripts;

namespace Assets.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private const string INITIAL_POINT = "InitialPoint";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if(GUILayout.Button("Collect"))
            {
                levelData.EnemySpawners = FindObjectsOfType<SpawnMarker>()
                    .Select(x=> new EnemySpawnerData(x.GetComponent<UniqueID>().ID, x.monsterTypeID, x.transform.position))
                    .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;

                levelData.InitialPlayerPosition = GameObject.FindGameObjectWithTag(INITIAL_POINT).transform.position;
            }
            EditorUtility.SetDirty(target);
        }
    }
}
