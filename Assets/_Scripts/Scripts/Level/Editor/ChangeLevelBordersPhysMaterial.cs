using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Level.Editor
{
    public static class ChangeLevelBordersPhysMaterial
    {
        [MenuItem("/LevelUtility/ChangeBordersMaterial")]
        private static void Change()
        {
            var material = Resources.Load("/Materials/Slide", typeof(PhysicMaterial)) as PhysicMaterial;

            var deepSelection = EditorUtility.CollectDeepHierarchy(Selection.gameObjects);
            var compCount = 0;
            var goCount = 0;

            foreach (var o in deepSelection)
            {
                if (o is not GameObject go)
                    continue;

                var count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);

                if (count <= 0)
                    continue;

                Undo.RegisterCompleteObjectUndo(go, "Remove missing scripts");

                BoxCollider leftBorder;
                BoxCollider rightBorder;

                if (o.name == "LeftBorder")
                {
                    leftBorder = o.GetComponent<BoxCollider>();
                    leftBorder.material = material;
                }

                if (o.name == "RightBorder")
                {
                    rightBorder = o.GetComponent<BoxCollider>();
                    rightBorder.material = material;
                }

                compCount += count;
                goCount++;
            }
        }
    }
}