#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

public class DownloadCReator
{
    [MenuItem("Assets/Create/Download")]
    public static void CreateMyAsset()
    {
        Download asset = ScriptableObject.CreateInstance<Download>();

        AssetDatabase.CreateAsset(asset, "Assets/downloads/new_download.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
#endif
