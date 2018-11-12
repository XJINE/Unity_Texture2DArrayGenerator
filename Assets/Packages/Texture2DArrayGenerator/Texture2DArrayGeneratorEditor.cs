#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public class Texture2DArrayGeneratorEditor : EditorWindow
{
    #region Filed

    public string fileName = "tex2darray.asset";

    public Texture2D[] textures;

    #endregion Field

    #region Method

    [MenuItem("Custom/Texture2DArrayGenerator")]
    static void Init()
    {
        EditorWindow.GetWindow<Texture2DArrayGeneratorEditor>(typeof(Texture2DArrayGenerator).Name); 
    }

    protected void OnGUI()
    {
        GUIStyle marginStyle = GUI.skin.label;
                 marginStyle.wordWrap = true;
                 marginStyle.margin   = new RectOffset(5, 5, 5, 5);

        if (GUILayout.Button("Generate"))
        {
            string path = AssetCreationHelper.CreateAssetInCurrentDirectory
                          (Texture2DArrayGenerator.Generate(this.textures), this.fileName);

            ShowNotification(new GUIContent("SUCCESS : " + path));
        }

        this.fileName = EditorGUILayout.TextField("File Name", this.fileName);

        SerializedObject scriptableObject = new SerializedObject(this);
        EditorGUILayout.PropertyField(scriptableObject.FindProperty("textures"), true);
        scriptableObject.ApplyModifiedProperties();
    }

    #endregion Method
}

#endif // UNITY_EDITOR