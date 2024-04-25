using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocalizationText))]
public class LocalizationTextEditor : Editor
{
    // �ν����� ������ ���� �ɶ� ȣ��Ǵ� �޼���
    public override void OnInspectorGUI()
    {
        // �⺻���� ���
        //DrawDefaultInspector();
        //target �� ���� �ν����Ϳ��� �׷��ְ� �ִ� LocalizationText ����

        var text = target as LocalizationText;

        var currentId = EditorGUILayout.TextField("Text ID", text.textId);

        // EditorGUILayout �޺��ڽ�
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
