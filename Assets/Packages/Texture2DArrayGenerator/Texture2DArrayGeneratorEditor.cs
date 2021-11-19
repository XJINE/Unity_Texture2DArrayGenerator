#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public class Texture2DArrayGeneratorEditor : EditorWindow
{
    #region Filed

    public string        fileName = "Tex2DArray.asset";
    public TextureFormat format   = TextureFormat.ARGB32;
    public bool          mipChain = true;

    public Texture2D[] textures;

    #endregion Field

    #region Method

    [MenuItem("Custom/Texture2DArrayGenerator")]
    private static void Init()
    {
        GetWindow<Texture2DArrayGeneratorEditor>(nameof(Texture2DArrayGenerator)); 
    }

    private void OnGUI()
    {
        var marginStyle = GUI.skin.label;
            marginStyle.wordWrap = true;
            marginStyle.margin   = new RectOffset(5, 5, 5, 5);

        if (GUILayout.Button("Generate"))
        {
            var path = AssetCreationHelper.CreateAssetInCurrentDirectory
                      (Texture2DArrayGenerator.Generate(textures, format, mipChain), fileName);

            ShowNotification(new GUIContent("SUCCESS : " + path));
        }

        fileName =                EditorGUILayout.TextField("File Name", fileName);
        format   = (TextureFormat)EditorGUILayout.EnumPopup("Format",    format  );
        mipChain =                EditorGUILayout.Toggle   ("MipChain",  mipChain);

        var serializedObject = new SerializedObject(this);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("textures"), true);
        serializedObject.ApplyModifiedProperties();
    }

    #endregion Method
}

#endif // UNITY_EDITOR