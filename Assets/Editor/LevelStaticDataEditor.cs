using Assets.Scripts.Enemy;
using Assets.Scripts.StaticData.LevelStaticDataFolder;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Logic;
using UnityEngine.SceneManagement;

namespace Assets.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
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
            }
            EditorUtility.SetDirty(target);
        }
    }
}
