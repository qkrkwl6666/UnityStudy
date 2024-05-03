using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CumstomMenuItems
{
    private static void UpdateLanguage()
    {
        var texts = Component.FindObjectsOfType<LocalizationText>();
        foreach (var text in texts)
        {
            text.OnChangeLanguage(Vars.editorLanguage);
            EditorUtility.SetDirty(text);
        }
    }

    [MenuItem("Tools/Set Editor Language Korean", priority = 1)]

    private static void SetLangKr()
    {
        Vars.editorLanguage = Languages.KOREAN;
        UpdateLanguage();
    }

    [MenuItem("Tools/Set Editor Language English", priority = 2)]
    private static void SetLangEn()
    {
        Vars.editorLanguage = Languages.ENGLISH;
        UpdateLanguage();
    }

    [MenuItem("Tools/Set Editor Language Japanese", priority = 3)]
    private static void SetLangJp()
    {
        Vars.editorLanguage = Languages.JAPANESE;
        UpdateLanguage();
    }
}
