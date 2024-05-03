using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocalizationText))]
public class LocalizationTextEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        var text = target as LocalizationText;
        var currentId = EditorGUILayout.TextField("Text ID", text.textId);
        var currentLang = (Languages)EditorGUILayout.EnumPopup("Language", text.language);

        if (currentId != text.textId || currentLang != text.language)
        {
            text.textId = currentId;
            text.language = currentLang;
            text.OnChangeLanguage(currentLang);
        }

        EditorUtility.SetDirty(text);
    }
}
