using UnityEngine;
using UnityEditor; // This 'using' directive is correctly placed

namespace FastMesh_Example
{
    // [ExecuteInEditMode] is fine as it tells Unity to run this script in edit mode
    [ExecuteInEditMode]
    public class SceneViewText : MonoBehaviour
    {
        public bool isShow = true;
        string text2 = "These 3D models, all created with \"Fast Mesh - 3D Asset Creation Tool\" (click)";
        Color backgroundColor = Color.white;
        Color textColor = Color.black;

        // All the code that uses UnityEditor classes must be inside this block
#if UNITY_EDITOR
        private void OnEnable()
        {
            // SceneView.duringSceneGui is an Editor-only event
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDisable()
        {
            // SceneView.duringSceneGui is an Editor-only event
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            if (isShow == false) return;

            Handles.BeginGUI(); // Handles is an Editor-only class
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 20;
            style.normal.textColor = textColor;
            style.wordWrap = true;

            float width = 420f;
            float height = 50f;
            float x = (sceneView.position.width - width) / 2f;
            float y = 10f;

            GUI.color = backgroundColor;
            GUI.DrawTexture(new Rect(x - 10, y - 10, width + 20, height + 20), Texture2D.whiteTexture);
            GUI.color = Color.white;

            if (GUI.Button(new Rect(x, y, width, height), text2, style))
            {
                Application.OpenURL("https://assetstore.unity.com/packages/slug/288711");
            }
            Handles.EndGUI(); // Handles is an Editor-only class
        }
#endif // End of UNITY_EDITOR block
    }
}