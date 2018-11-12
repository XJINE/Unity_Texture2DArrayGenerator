#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using System.Reflection;

public static class AssetCreationHelper
{
    #region Field

    private static Assembly     UnityEditor;
    private static System.Type  ProjectBrowser;
    private static System.Type  ProjectWindowUtil;
    private static EditorWindow ProjectWindow;
    private static MethodInfo   FrameObjectInProjectWindow;

    #endregion Field

    #region Constructor

    static AssetCreationHelper() 
    {
        AssetCreationHelper.UnityEditor       = Assembly.Load("UnityEditor");
        AssetCreationHelper.ProjectBrowser    = AssetCreationHelper.UnityEditor.GetType("UnityEditor.ProjectBrowser");
        AssetCreationHelper.ProjectWindow     = EditorWindow.GetWindow(AssetCreationHelper.ProjectBrowser);
        AssetCreationHelper.ProjectWindowUtil = AssetCreationHelper.UnityEditor.GetType("UnityEditor.ProjectWindowUtil");
        AssetCreationHelper.FrameObjectInProjectWindow =
        AssetCreationHelper.ProjectWindowUtil.GetMethod("FrameObjectInProjectWindow",
                                                      BindingFlags.NonPublic |
                                                      BindingFlags.Instance  |
                                                      BindingFlags.Static);
    }

    #endregion Constructor

    #region Method

    public static string CreateAssetInCurrentDirectory(Object asset, string nameWithExtension)
    {
        return CreateAssetInCurrentDirectory<Object>(asset, nameWithExtension);
    }

    public static string CreateAssetInCurrentDirectory(string asset, string nameWithExtension)
    {
        return CreateAssetInCurrentDirectory<string>(asset, nameWithExtension);
    }

    private static string CreateAssetInCurrentDirectory<T>(object asset, string nameWithExtension)
    {
        string path = GetCurrentAssetDirectoryPathRelative() + "/" + nameWithExtension;

        if (typeof(T) == typeof(Object))
        {
            AssetDatabase.CreateAsset((Object)asset, path);
        }
        else
        {
            System.IO.File.WriteAllText(path, (string)asset);
        }

        AssetDatabase.Refresh();

        StartToRenameAsset(path);

        return path;
    }

    public static string GetCurrentAssetDirectoryPathAbsolute()
    {
        string path = Application.dataPath;

        foreach (Object asset in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
        {
            string tempPath = AssetDatabase.GetAssetPath(asset);

            if (System.IO.File.Exists(path))
            {
                path += "/" + System.IO.Path.GetDirectoryName(tempPath);
                break;
            }
        }

        return path;
    }

    public static string GetCurrentAssetDirectoryPathRelative()
    {
        // WARNING:
        // "Application.dataPath" returns absolute path
        // and the following code makes a relative path.
        // If any assets are not selected in UnityEditor like a just after started,
        // this function returns absolute path.
        // "AssetDatabase.CreateAsset()" will be failed if the path is absolute path.

        string path = "Assets";

        foreach (Object asset in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(asset);

            if (System.IO.File.Exists(path))
            {
                path = System.IO.Path.GetDirectoryName(path);
                break;
            }
        }

        return path;
    }

    public static void StartToRenameAsset(string assetPath) 
    {
        StartToRenameAsset(AssetDatabase.LoadAssetAtPath<Object>(assetPath));
    }

    public static void StartToRenameAsset(Object asset)
    {
        // NOTE:
        // PingObject function is quite simple & work completely to rename.
        // However the animation is not so good.
        // EditorGUIUtility.PingObject(asset);

        // NOTE:
        // FocusAsset function is important to rename asset in following case.
        // - "Project/Assets/~Any Directory" is focused.
        // - Any other UnityEditor window is focused.

        Selection.activeObject = asset;
        EditorUtility.FocusProjectWindow();
        FocusAsset(asset);
        AssetCreationHelper.ProjectWindow.SendEvent(Event.KeyboardEvent(KeyCode.F2.ToString()));
    }

    public static void FocusAsset(Object asset)
    {
        // NOTE:
        // https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/ProjectWindow/ProjectWindowUtil.cs

        AssetCreationHelper.FrameObjectInProjectWindow.Invoke
        (AssetCreationHelper.ProjectWindow, new object[] { asset.GetInstanceID() });
    }

    #endregion Method
}

#endif // UNITY_EDITOR