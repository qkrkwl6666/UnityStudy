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

    // �޴� �� �߰� �ϴ� ��� �Ʒ� �޼��带 ȣ�� 
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
