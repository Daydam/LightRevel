using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DialogEditorSetupWindow : EditorWindow
{
    DialogEditorWindow dew;

    public static void CreateWindow(DialogEditorWindow editor)
    {
        DialogEditorSetupWindow desw = GetWindow(typeof(DialogEditorSetupWindow), false, "Dialog Options") as DialogEditorSetupWindow;
        desw.Show();
        desw.position = new Rect(editor.position.x + editor.position.width, editor.position.y, 400, 200);
        desw.dew = editor;
    }

    void OnGUI()
    {
        if (dew == null) EditorGUILayout.HelpBox("There is no Dialog Editor associated with this window. Please go to your Dialog Editor and tap the Setup Button to reload.", MessageType.Error);
        else
        {
            //dew.allowIdEditing = EditorGUILayout.Toggle("Allow 'ID' editing", dew.allowIdEditing);
            dew.allowComesFromEditing = EditorGUILayout.Toggle("Allow 'Comes From' editing", dew.allowComesFromEditing);
            DialogEditorWindow.maxDialogLines = EditorGUILayout.IntSlider("Max dialog lines", DialogEditorWindow.maxDialogLines, 1, 10);
            dew.maxNextOptions = EditorGUILayout.IntSlider("Max 'Response' Options", dew.maxNextOptions, 1, 10);

            dew.Repaint();
        }
    }
}
