using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

[CustomEditor(typeof(WindowManager))]
public class WindowManagerEditer : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate Window enums"))
        {
            var mgr = target as WindowManager;

            var sb = new StringBuilder();
            sb.AppendLine("public enum Windows\r\n{");

            foreach(var window in mgr.windows)
            {
                sb.AppendLine($"\t{window.name},");
            }

            sb.AppendLine("}");

            var path = EditorUtility.SaveFilePanel("Save The Windows Enums", "", 
                "Windows.cs", "cs");

            Debug.Log(path);
            Debug.Log(sb.ToString());

            System.IO.File.WriteAllText(path, sb.ToString());

            AssetDatabase.Refresh();
        }
    }
}
