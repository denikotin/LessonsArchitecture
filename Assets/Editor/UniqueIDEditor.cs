using Assets.Scripts.Logic;
using System;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof(UniqueID))]
    public class UniqueIDEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = (UniqueID)target;

            if (string.IsNullOrEmpty(uniqueId.ID))
            {
                Generate(uniqueId);
            }
            else
            {
                UniqueID[] uniqueIDs = FindObjectsOfType<UniqueID>();
                if(uniqueIDs.Any(other => other != uniqueId && other.ID == uniqueId.ID ))
                {
                    Generate(uniqueId);
                }

            }
        }

        private void Generate(UniqueID uniqueId)
        {
            uniqueId.ID = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";

            if(!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
            }
        }
    }
}