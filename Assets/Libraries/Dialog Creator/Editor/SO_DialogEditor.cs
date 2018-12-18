using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SO_Dialog))]
public class SO_DialogEditor : Editor
{
    SO_Dialog Target { get { return (SO_Dialog)target; } }

    public override void OnInspectorGUI()
    {
        if (Target.Dialogs != null && Target.Dialogs.Length > 0) EditorGUILayout.LabelField("'" + Target.Dialogs[0].Text[0] + "'");

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Open in Dialog Editor")) DialogEditorWindow.CreateWindow(Target);
        if(GUILayout.Button("Reset"))
        {
            Target.Dialogs = new DialogLine[0];
            Target.Responses = new Response[0];
            Target.DialogNodes = new Node_DialogLine[] { new Node_DialogLine(new DialogLine()) };
            Target.ResponseNodes = new Node_Response[0];
            Target.DialogNodes[0].dialog.Id = 0;
            Target.DialogNodes[0].dialog.ComesFrom = 0;
            Target.dragScreen = new Rect(0, 0, float.MaxValue, float.MaxValue);
        }
        EditorGUILayout.EndHorizontal();
        
        EditorUtility.SetDirty(Target);
    }
}
