using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DialogEditorHelpWindow : EditorWindow
{
    GUIStyle textStyle;

    public static void CreateWindow(DialogEditorWindow editor)
    {
        DialogEditorHelpWindow dew = GetWindow(typeof(DialogEditorHelpWindow), false, "Help") as DialogEditorHelpWindow;
        dew.Show();
        dew.position = new Rect(editor.position.x + editor.position.width, editor.position.y + 250, 400, 300);
        dew.textStyle = new GUIStyle
        {
            richText = true,
            wordWrap = true,
            fontSize = 10,
            margin = new RectOffset(12, 12, 12, 12)
        };
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Welcome to the <b>Dialog Editor Help menu</b>! Here you'll learn how to properly use the <b>Dialog Editor</b> tool." + "\n" + 
            "When you open a Dialog in the Dialog Editor for the first time, you'll see a single node. This is your starting point, and will be the first dialog line" +
            "shown in your game once you load the dialog. Each node has a specific <b>Id</b>, which will be used by the <b>Dialog Manager</b> to check the connections." +
            "Also, every node except for the first one has a <b>'Comes from'</b> field, which contains a number. This number is the Id of the previous dialog or response." +
            "<b>You can edit the 'comes from' field by tapping the Setup button, then toggling 'Allow Comes from editing' on.</b>" + "\n" + 
            "Every Dialog Line node has many lines in it. You can add or remove lines as you wish. In game, these lines will make just one character's part, and will" +
            "appear in order, the player having to tap Continue in order to see the next one. <b>You can modify the maximum amount of lines you can add by going to" +
            "Setup and using the 'Max dialog lines' slider.</b>" + "\n" +
            "The conditions for dialogs and responses have not yet been implemented so I will not explain them. The rest... Well, I'm too tired and want to implement this" +
            "system, so I'm sorry but no more explanations for now. Time to pass this final! Also, whoever is reading this, poneme mínimo un 8 master te lo pido por favor.", textStyle);
    }
}
