using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomMenuItem : Editor
{
    private static void UpdateLanguage()
    {
        var texts = FindObjectsOfType<LocalizationText>();

        foreach (var text in texts)
        {
            text.OnChangeLanguage(Vars.editorLanguage);
            EditorUtility.SetDirty(text);
        }
    }

    // 메뉴 에 추가 하는 기능 아래 메서드를 호출 
    [MenuItem("Tools/Set Editor Language Korean")]
    private static void SetLangKR()
    {
        Vars.editorLanguage = Languages.KOREAN;
        UpdateLanguage();
    }

    [MenuItem("Tools/Set Editor Language English")]
    private static void SetLangEN()
    {
        Vars.editorLanguage = Languages.ENGLISH;
        UpdateLanguage();
    }

    [MenuItem("Tools/Set Editor Language Japanese")]
    private static void SetLangJP()
    {
        Vars.editorLanguage = Languages.JAPANESE;
        UpdateLanguage();
    }
}
