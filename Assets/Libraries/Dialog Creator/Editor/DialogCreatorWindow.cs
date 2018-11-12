using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DialogCreatorWindow : EditorWindow
{
    SO_DialogContainer dialogContainer;
    string newDialogName;

    int maxTextOptions = 3;
    int maxNextOptions = 3;
    int maxConditions = 5;

    Rect nodeField;
    Texture2D nodeFieldBGColor;
    GUIStyle nodeStyle;
    Texture2D nodeColor;
    GUIStyle titleStyle;

    [MenuItem("Assistants/Dialog Creator")]
    static void CreateWindow()
    {
        GetWindow(typeof(DialogCreatorWindow), false, "Light Room").Show();
    }

    void OnEnable()
    {
        nodeFieldBGColor = new Texture2D(1, 1);
        nodeFieldBGColor.SetPixel(0, 0, new Color(0.5f, 0.5f, 0.5f));
        nodeFieldBGColor.Apply();

        nodeColor = new Texture2D(1, 1);
        nodeColor.SetPixel(0, 0, new Color(0.89f, 0.89f, 0.89f));
        nodeColor.Apply();

        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = nodeColor;
        nodeStyle.border = new RectOffset(12,12,12,12);

        titleStyle = new GUIStyle();
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.fontSize = 12;
    }

    void OnGUI()
    {
        dialogContainer = EditorGUILayout.ObjectField("Dialog:", dialogContainer, typeof(SO_DialogContainer), false) as SO_DialogContainer;
        EditorGUILayout.BeginHorizontal();
        newDialogName = EditorGUILayout.TextField("New Dialog: ", newDialogName);
        EditorGUI.BeginDisabledGroup(newDialogName == string.Empty);
        if (GUILayout.Button("Create new Dialog"))
        {
            AssetDatabase.CreateAsset(CreateInstance<SO_DialogContainer>(), "Assets/Resources/Dialog/" + newDialogName + ".asset");
            dialogContainer = AssetDatabase.LoadAssetAtPath<SO_DialogContainer>("Assets/Resources/Dialog/" + newDialogName + ".asset");
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();


        if (dialogContainer != null && dialogContainer.dialogChain == null)
        {
            dialogContainer.dialogChain = new List<DialogNode>();
            dialogContainer.dialogChain.Add(new DialogNode(new Dialog()));
        }

        DrawNodeField();
    }

    void DrawNodeField()
    {
        nodeField = new Rect(10, 50, position.width - 20, position.height - 60);
        GUI.DrawTexture(nodeField, nodeFieldBGColor);

        BeginWindows();
        if (dialogContainer != null)
        {
            for (int i = 0; i < dialogContainer.dialogChain.Count; i++)
            {
                dialogContainer.dialogChain[i].window = GUI.Window(i, dialogContainer.dialogChain[i].window, DrawNodeWindow, "", nodeStyle);
            }
        }
        EndWindows();
    }

    void DrawNodeWindow(int id)
    {
        EditorGUILayout.LabelField("Possible lines:", titleStyle);
        for (int i = 0; i < dialogContainer.dialogChain[id].d.textOptions.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            dialogContainer.dialogChain[id].d.textOptions[i] = EditorGUILayout.TextField(dialogContainer.dialogChain[id].d.textOptions[i]);
            if (GUILayout.Button("X")) dialogContainer.dialogChain[id].d.textOptions.RemoveAt(i);
            EditorGUILayout.EndHorizontal();
        }
        if (dialogContainer.dialogChain[id].d.textOptions.Count < maxTextOptions)
            if (GUILayout.Button("Add new text option")) dialogContainer.dialogChain[id].d.textOptions.Add("");

        EditorGUILayout.LabelField("Conditions:", titleStyle);
        for (int i = 0; i < dialogContainer.dialogChain[id].d.conditions.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            dialogContainer.dialogChain[id].d.conditions[i] = (Condition)EditorGUILayout.EnumPopup(dialogContainer.dialogChain[id].d.conditions[i]);
            if (GUILayout.Button("X")) dialogContainer.dialogChain[id].d.conditions.RemoveAt(i);
            EditorGUILayout.EndHorizontal();
        }
        if (dialogContainer.dialogChain[id].d.conditions.Count < maxConditions)
            if (GUILayout.Button("Add new condition")) dialogContainer.dialogChain[id].d.conditions.Add(Condition.FIRST_TALK);

        for (int i = 0; i < dialogContainer.dialogChain[id].connections.Length; i++)
        {

        }

        dialogContainer.dialogChain[id].UpdateConnectionDots();

        GUI.DragWindow();
    }
}
