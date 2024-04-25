using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocalizationText))]
public class LocalizationTextEditor : Editor
{
    // 인스펙터 내용이 변경 될때 호출되는 메서드
    public override void OnInspectorGUI()
    {
        // 기본으로 출력
        //DrawDefaultInspector();
        //target 는 지금 인스펙터에서 그려주고 있는 LocalizationText 참조

        var text = target as LocalizationText;

        var currentId = EditorGUILayout.TextField("Text ID", text.textId);

        // EditorGUILayout 콤보박스
        var currentLang = (Languages)EditorGUILayout.EnumPopup("Language", text.language);

        if(currentLang != text.language || currentId != text.textId)
        {
            text.textId = currentId;
            text.language = currentLang;
            text.OnChangeLanguage(currentLang);
        }

        EditorUtility.SetDirty(text);
    }
}
