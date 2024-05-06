using UnityEditor;

[CustomEditor(typeof(TextLocalization))]
public class LocalizationTest : Editor
{
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        //base.OnInspectorGUI();
        var text = target as TextLocalization;
        
        var currentLang = (Languages)EditorGUILayout.EnumPopup("Lable", text.language);
        var currentId = EditorGUILayout.TextField("ID", text.id);
        if (currentLang != text.language || currentId != text.id)
        {
            text.language = currentLang;
            text.id = currentId;
            text.SetText();
        }
        EditorUtility.SetDirty(target);
    }
}
