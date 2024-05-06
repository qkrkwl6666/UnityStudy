using UnityEditor;
using UnityEngine;

public class CustomMenuItems
{
    [MenuItem("Tools/Set Editor Language/Korean %#&J")]
    private static void SetLangKR()
    {
        foreach (var text in Component.FindObjectsOfType<TextLocalization>())
        {
            text.language = Languages.Korean;
            text.SetText();
            EditorUtility.SetDirty(text);
        }
    }

    [MenuItem("Tools/Set Editor Language/English")]
    private static void SetLangEN()
    {
        foreach (var text in Component.FindObjectsOfType<TextLocalization>())
        {
            text.language = Languages.English;
            text.SetText();
            EditorUtility.SetDirty(text);
        }
    }

    [MenuItem("Tools/Set Editor Language/Japanese")]
    private static void SetLangJP()
    {
        foreach (var text in Component.FindObjectsOfType<TextLocalization>())
        {
            text.language = Languages.Japanese;
            text.SetText();
            EditorUtility.SetDirty(text);
        }
    }


}
