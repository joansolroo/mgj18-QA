#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

public class MailCreator
{
    [MenuItem("Assets/Create/Mail")]
    public static void CreateMyAsset()
    {
        Mail asset = ScriptableObject.CreateInstance<Mail>();

        AssetDatabase.CreateAsset(asset, "Assets/mails/new_email.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
#endif
