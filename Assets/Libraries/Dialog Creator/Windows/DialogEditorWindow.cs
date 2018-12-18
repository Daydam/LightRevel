using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class DialogEditorWindow : EditorWindow
{
    SO_Dialog dialogContainer;
    string newDialogName;

    public static int maxDialogLines = 5;
    public int maxNextOptions = 3;
    public static int maxConditionGroups = 3;
    int maxTexts = 500;
    //public bool allowIdEditing = false;
    public bool allowComesFromEditing = false;

    int nodeFieldUpperBorder = 40;
    int nodeFieldLowerBorder = 10;
    int nodeFieldSideBorder = 10;

    Rect nodeField;
    Texture2D nodeFieldBGColor;
    Vector2 scroll;
    
    Texture2D dragScreenColor;
    GUIStyle dragScreenStyle;

    Texture2D nodeColor;
    GUIStyle nodeStyle;

    GUIStyle uiStyle;
    Texture2D uiColor;


    public static void CreateWindow(SO_Dialog dialog)
    {
        DialogEditorWindow dew = GetWindow(typeof(DialogEditorWindow), false, "Dialog Editor") as DialogEditorWindow;
        dew.Show();

        dew.dialogContainer = dialog;

        if (dew.dialogContainer != null && dew.dialogContainer.DialogNodes == null)
        {
            dew.dialogContainer.DialogNodes = new Node_DialogLine[0];
        }
        if (dew.dialogContainer != null && dew.dialogContainer.ResponseNodes == null)
        {
            dew.dialogContainer.ResponseNodes = new Node_Response[0];
        }
    }

    void OnEnable()
    {
        dragScreenColor = new Texture2D(1, 1);
        dragScreenColor.SetPixel(0, 0, new Color(0.5f, 0.5f, 0.5f));
        dragScreenColor.Apply();
        dragScreenStyle = new GUIStyle();
        dragScreenStyle.normal.background = dragScreenColor;

        nodeFieldBGColor = new Texture2D(1, 1);
        nodeFieldBGColor.SetPixel(0, 0, new Color(0.4f, 0.4f, 0.4f));
        nodeFieldBGColor.Apply();

        nodeColor = new Texture2D(1, 1);
        nodeColor.SetPixel(0, 0, new Color(0.7f, 0.7f, 0.7f));
        nodeColor.Apply();

        nodeStyle = new GUIStyle();
        nodeStyle.fontStyle = FontStyle.Bold;
        nodeStyle.alignment = TextAnchor.UpperCenter;
        nodeStyle.normal.background = nodeColor;
        nodeStyle.border = new RectOffset(12, 12, 12, 12);

        uiColor = new Texture2D(1, 1);
        uiColor.SetPixel(0, 0, new Color(0.78f, 0.78f, 0.78f));
        uiColor.Apply();

        uiStyle = new GUIStyle();
        uiStyle.normal.background = uiColor;
        uiStyle.border = new RectOffset(12, 12, 12, 12);
    }

    void OnGUI()
    {
        /*EditorGUILayout.BeginHorizontal();
        allowComesFromEditing = EditorGUILayout.Toggle("Allow ID editing", allowComesFromEditing);
        if(allowComesFromEditing) EditorGUILayout.HelpBox("Please note that this tool should be used with extreme precaution. Multiple objects sharing the same ID could cause some horrible issues.", MessageType.Warning);
        EditorGUILayout.EndHorizontal();*/
        DrawNodeField();
        EditorUtility.SetDirty(dialogContainer);
    }

    void DrawNodeField()
    {
        nodeField = new Rect(nodeFieldSideBorder, nodeFieldUpperBorder, position.width - nodeFieldSideBorder*2, position.height - nodeFieldUpperBorder - 10);
        GUI.DrawTexture(nodeField, nodeFieldBGColor);
        
        BeginWindows();
        /*if (dialogContainer.dragScreen == default(Rect)) dialogContainer.dragScreen = new Rect(0, 0, float.MaxValue, float.MaxValue);
        dialogContainer.dragScreen = GUI.Window(int.MaxValue, dialogContainer.dragScreen, DragScreen, "", dragScreenStyle);*/
        if (dialogContainer != null)
        {
            for (int i = 0; i < dialogContainer.DialogNodes.Length; i++)
            {
                dialogContainer.DialogNodes[i].window = GUI.Window(i, dialogContainer.DialogNodes[i].window, DrawDialogNode, "Id: " + dialogContainer.DialogNodes[i].dialog.Id.ToString(), nodeStyle);
                var comeFrom = dialogContainer.ResponseNodes.Where(a => a.response.Id == dialogContainer.DialogNodes[i].dialog.ComesFrom).ToArray();
                for (int j = 0; j < comeFrom.Length; j++)
                {
                    Handles.DrawBezier(
                        comeFrom[j].window.center + new Vector2(comeFrom[j].window.width / 2, 0),
                        dialogContainer.DialogNodes[i].window.center - new Vector2(dialogContainer.DialogNodes[i].window.width / 2, 0),
                        comeFrom[j].window.center + new Vector2(comeFrom[j].window.width / 2, 0) - Vector2.left * 50,
                        dialogContainer.DialogNodes[i].window.center - new Vector2(dialogContainer.DialogNodes[i].window.width / 2, 0) + Vector2.left * 50,
                        Color.black,
                        null,
                        3f
                        );
                }
            }
            for (int i = 0; i < dialogContainer.ResponseNodes.Length; i++)
            {
                dialogContainer.ResponseNodes[i].window = GUI.Window(i + maxTexts, dialogContainer.ResponseNodes[i].window, DrawResponseNode, "Id: " + dialogContainer.ResponseNodes[i].response.Id.ToString(), nodeStyle);
                var comeFrom = dialogContainer.DialogNodes.Where(a => a.dialog.Id == dialogContainer.ResponseNodes[i].response.ComesFrom).ToArray();
                for (int j = 0; j < comeFrom.Length; j++)
                {
                    Handles.DrawBezier(
                        comeFrom[j].window.center + new Vector2(comeFrom[j].window.width / 2, 0),
                        dialogContainer.ResponseNodes[i].window.center - new Vector2(dialogContainer.ResponseNodes[i].window.width / 2, 0),
                        comeFrom[j].window.center + new Vector2(comeFrom[j].window.width / 2, 0) - Vector2.left * 50,
                        dialogContainer.ResponseNodes[i].window.center - new Vector2(dialogContainer.ResponseNodes[i].window.width / 2, 0) + Vector2.left * 50,
                        Color.black,
                        null,
                        3f
                        );
                }
            }
        }
        DrawBorders();
        EndWindows();
        if (GUI.Button(new Rect(20, 10, 60, 20), "Setup")) DialogEditorSetupWindow.CreateWindow(this);
        if (GUI.Button(new Rect(90, 10, 60, 20), "Save"))
        {
            Save();
            Debug.Log("Progress saved! To be honest, unless you have added new nodes, this button doesn't do much after the first time you press it.");
        }
        if (GUI.Button(new Rect(position.width - 40, 10, 20, 20), "?")) DialogEditorHelpWindow.CreateWindow(this);
    }

    void DrawBorders()
    {
        GUI.Box(new Rect(0, 0, position.width, nodeFieldUpperBorder),"", uiStyle);
        GUI.Box(new Rect(0, 0, nodeFieldSideBorder, position.height),"", uiStyle);
        GUI.Box(new Rect(position.width - nodeFieldSideBorder, 0, nodeFieldSideBorder, position.height),"", uiStyle);
        GUI.Box(new Rect(0, position.height - nodeFieldLowerBorder, position.width, nodeFieldLowerBorder),"", uiStyle);
    }

    void DragScreen(int id)
    {
        GUI.DragWindow();
        dialogContainer.dragScreen.x = Mathf.Min(0, Mathf.Max(float.MinValue, dialogContainer.dragScreen.x));
        dialogContainer.dragScreen.y = Mathf.Min(0, Mathf.Max(float.MinValue, dialogContainer.dragScreen.y));
        for (int i = 0; i < dialogContainer.DialogNodes.Length; i++)
        {
            dialogContainer.DialogNodes[i].window.x = dialogContainer.DialogNodes[i].posX + dialogContainer.dragScreen.x;
            dialogContainer.DialogNodes[i].window.y = dialogContainer.DialogNodes[i].posY + dialogContainer.dragScreen.y;
        }
        for (int i = 0; i < dialogContainer.ResponseNodes.Length; i++)
        {
            dialogContainer.ResponseNodes[i].window.x = dialogContainer.ResponseNodes[i].posX + dialogContainer.dragScreen.x;
            dialogContainer.ResponseNodes[i].window.y = dialogContainer.ResponseNodes[i].posY + dialogContainer.dragScreen.y;
        }
    }

    void DrawDialogNode(int id)
    {
        GUILayout.Space(15);
        var line = dialogContainer.DialogNodes[id].dialog;
        EditorGUI.BeginDisabledGroup(!allowComesFromEditing);
        if(line.Id != 0) line.ComesFrom = EditorGUILayout.IntSlider("Comes from:", line.ComesFrom, maxTexts, maxTexts * 3 - 1);
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        bool textUpdate = false;
        EditorGUI.BeginChangeCheck();
        dialogContainer.DialogNodes[id].showText = EditorGUILayout.Foldout(dialogContainer.DialogNodes[id].showText, "Dialog lines:");
        if (EditorGUI.EndChangeCheck()) textUpdate = true;
        if(dialogContainer.DialogNodes[id].showText)
        {
            if (line.Text == null) line.Text = new string[1];

            for (int i = 0; i < line.Text.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                line.Text[i] = EditorGUILayout.TextField(line.Text[i]);
                if (line.Text.Length > 1)
                {
                    if (GUILayout.Button("Remove"))
                    {
                        line.Text = line.Text.Take(i).Concat(line.Text.Skip(i + 1)).ToArray();
                        dialogContainer.DialogNodes[id].ResetWindowSize();
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            if (line.Text.Length < maxDialogLines)
            {
                if (GUILayout.Button("Add"))
                {
                    line.Text = line.Text.Concat(new string[] { "" }).ToArray();
                    dialogContainer.DialogNodes[id].ResetWindowSize();
                }
            }
        }
        if(textUpdate) dialogContainer.DialogNodes[id].ResetWindowSize();

        if(line.Id != 0)
        {
            bool conditionsUpdate = false;
            EditorGUI.BeginChangeCheck();
            dialogContainer.DialogNodes[id].showConditionGroups = EditorGUILayout.Foldout(dialogContainer.DialogNodes[id].showConditionGroups, "Conditions:");
            if (EditorGUI.EndChangeCheck()) conditionsUpdate = true;
            if(dialogContainer.DialogNodes[id].showConditionGroups)
            {
                if (line.Conditions == null)
                {
                    line.Conditions = new string[1, 5];
                    dialogContainer.DialogNodes[id].showConditions = new bool[5];
                }
                for (int i = 0; i < line.Conditions.GetLength(0); i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    dialogContainer.DialogNodes[id].showConditions[i] = EditorGUILayout.Foldout(dialogContainer.DialogNodes[id].showConditions[i], "Condition group " + (i + 1) + ":");
                    if (EditorGUI.EndChangeCheck()) conditionsUpdate = true;
                    if(line.Conditions.GetLength(0) > 1)
                    {
                        if(GUILayout.Button("Remove Group"))
                        {
                            string[,] newConds = new string[dialogContainer.DialogNodes[id].dialog.Conditions.GetLength(0) -1, 5];
                            for (int k = 0; k < dialogContainer.DialogNodes[id].dialog.Conditions.GetLength(0); k++)
                            {
                                if (k != i)
                                {
                                    if (k > i)
                                    {
                                        for (int l = 0; l < dialogContainer.DialogNodes[id].dialog.Conditions.GetLength(1); l++)
                                        {
                                            newConds[k - 1, l] = dialogContainer.DialogNodes[id].dialog.Conditions[k, l];
                                        }
                                    }
                                    else
                                    {
                                        for (int l = 0; l < dialogContainer.DialogNodes[id].dialog.Conditions.GetLength(1); l++)
                                        {
                                            newConds[k, l] = dialogContainer.DialogNodes[id].dialog.Conditions[k, l];
                                        }
                                    }
                                }
                            }
                            line.Conditions = newConds;
                            dialogContainer.DialogNodes[id].ResetWindowSize();
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    if (dialogContainer.DialogNodes[id].showConditions[i])
                    {
                        for (int j = 0; j < line.Conditions.GetLength(1); j++)
                        {
                            line.Conditions[i, j] = EditorGUILayout.TextField(line.Conditions[i,j]);
                        }
                    }
                }
                if (line.Conditions.GetLength(0) < maxConditionGroups)
                {
                    if(GUILayout.Button("Add Condition Group"))
                    {
                        string[,] newConds = new string[dialogContainer.DialogNodes[id].dialog.Conditions.GetLength(0) + 1, 5];
                        for (int k = 0; k < line.Conditions.GetLength(0); k++)
                        {
                            for (int l = 0; l < line.Conditions.GetLength(1); l++)
                            {
                                newConds[k, l] = line.Conditions[k, l];
                            }
                        }
                        line.Conditions = newConds;
                        dialogContainer.DialogNodes[id].ResetWindowSize();
                    }
                }
            }
            if(conditionsUpdate)dialogContainer.DialogNodes[id].ResetWindowSize();
        }

        EditorGUILayout.BeginHorizontal();
        if(line.Id != 0)
        {
            if (GUILayout.Button("Destroy"))
            {
                dialogContainer.DialogNodes = dialogContainer.DialogNodes.Take(id).Concat(dialogContainer.DialogNodes.Skip(id + 1)).ToArray();
            }
        }
        if (dialogContainer.ResponseNodes.Where(a => a.response.ComesFrom == id).Count() < maxNextOptions)
        {
            if (GUILayout.Button("Add Response"))
            {
                CreateResponse(id,
                    (int)(dialogContainer.DialogNodes[id].window.x + dialogContainer.DialogNodes[id].window.width + 50),
                    (int)(dialogContainer.DialogNodes[id].window.y + dialogContainer.DialogNodes[id].window.height/2 + Random.Range(-50, 50)));
            }
        }
        EditorGUILayout.EndHorizontal();
        GUI.DragWindow();
        //dialogContainer.DialogNodes[id].posX = (int)(dialogContainer.DialogNodes[id].window.x - dialogContainer.dragScreen.x);
        //dialogContainer.DialogNodes[id].posY = (int)(dialogContainer.DialogNodes[id].window.y - dialogContainer.dragScreen.y);
    }

    void DrawResponseNode(int id)
    {
        GUILayout.Space(15);
        int index = id - maxTexts;
        var response = dialogContainer.ResponseNodes[index].response;
        EditorGUI.BeginDisabledGroup(!allowComesFromEditing);
        response.ComesFrom = EditorGUILayout.IntSlider("Comes from:", response.ComesFrom, 0, maxTexts - 1);
        EditorGUI.EndDisabledGroup();



        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Response:");
        response.ResponseText = EditorGUILayout.TextField(response.ResponseText);

        bool conditionsUpdate = false;
        EditorGUI.BeginChangeCheck();
        dialogContainer.ResponseNodes[index].showConditionGroups = EditorGUILayout.Foldout(dialogContainer.ResponseNodes[index].showConditionGroups, "Conditions:");
        if (EditorGUI.EndChangeCheck()) conditionsUpdate = true;
        if (dialogContainer.ResponseNodes[index].showConditionGroups)
        {
            if (response.Conditions == null)
            {
                response.Conditions = new string[1, 5];
                dialogContainer.ResponseNodes[index].showConditions = new bool[5];
            }
            for (int i = 0; i < response.Conditions.GetLength(0); i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUI.BeginChangeCheck();
                dialogContainer.ResponseNodes[index].showConditions[i] = EditorGUILayout.Foldout(dialogContainer.ResponseNodes[index].showConditions[i], "Condition group " + (i + 1) + ":");
                if (EditorGUI.EndChangeCheck()) conditionsUpdate = true;
                if (response.Conditions.GetLength(0) > 1)
                {
                    if (GUILayout.Button("Remove Group"))
                    {
                        string[,] newConds = new string[response.Conditions.GetLength(0) - 1, 5];
                        for (int k = 0; k < response.Conditions.GetLength(0); k++)
                        {
                            if (k != i)
                            {
                                if (k > i)
                                {
                                    for (int l = 0; l < response.Conditions.GetLength(1); l++)
                                    {
                                        newConds[k - 1, l] = response.Conditions[k, l];
                                    }
                                }
                                else
                                {
                                    for (int l = 0; l < response.Conditions.GetLength(1); l++)
                                    {
                                        newConds[k, l] = response.Conditions[k, l];
                                    }
                                }
                            }
                        }
                        response.Conditions = newConds;
                        dialogContainer.ResponseNodes[index].ResetWindowSize();
                    }
                }
                EditorGUILayout.EndHorizontal();
                if (dialogContainer.ResponseNodes[index].showConditions[i])
                {
                    for (int j = 0; j < response.Conditions.GetLength(1); j++)
                    {
                        response.Conditions[i, j] = EditorGUILayout.TextField(response.Conditions[i, j]);
                    }
                }
            }
            if (response.Conditions.GetLength(0) < maxConditionGroups)
            {
                if (GUILayout.Button("Add Condition Group"))
                {
                    string[,] newConds = new string[response.Conditions.GetLength(0) + 1, 5];
                    for (int k = 0; k < response.Conditions.GetLength(0); k++)
                    {
                        for (int l = 0; l < response.Conditions.GetLength(1); l++)
                        {
                            newConds[k, l] = response.Conditions[k, l];
                        }
                    }
                    response.Conditions = newConds;
                    dialogContainer.ResponseNodes[index].ResetWindowSize();
                }
            }
        }
        if (conditionsUpdate) dialogContainer.ResponseNodes[index].ResetWindowSize();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Destroy"))
        {
            dialogContainer.ResponseNodes = dialogContainer.ResponseNodes.Take(index).Concat(dialogContainer.ResponseNodes.Skip(index + 1)).ToArray();
        }
        if (dialogContainer.DialogNodes.Where(a => a.dialog.ComesFrom == id).Count() < 3)
        {
            if (GUILayout.Button("Add Dialog"))
            {
                CreateDialog(id,
                    (int)(dialogContainer.ResponseNodes[index].window.x + dialogContainer.ResponseNodes[index].window.width + 50),
                    (int)(dialogContainer.ResponseNodes[index].window.y));
            }
        }

        EditorGUILayout.EndHorizontal();
        GUI.DragWindow();
        //dialogContainer.ResponseNodes[index].posX = (int)(dialogContainer.ResponseNodes[index].window.x - dialogContainer.dragScreen.x);
        //dialogContainer.ResponseNodes[index].posY = (int)(dialogContainer.ResponseNodes[index].window.y - dialogContainer.dragScreen.y);
    }

    void CreateDialog(int comesFrom, int posX, int posY)
    {
        dialogContainer.DialogNodes = dialogContainer.DialogNodes.Concat(new Node_DialogLine[] { new Node_DialogLine(new DialogLine(), posX, posY) }).ToArray();
        dialogContainer.DialogNodes[dialogContainer.DialogNodes.Length - 1].dialog.ComesFrom = comesFrom;
        dialogContainer.DialogNodes[dialogContainer.DialogNodes.Length - 1].dialog.Id = dialogContainer.DialogNodes.Length - 1;
        dialogContainer.DialogNodes[dialogContainer.DialogNodes.Length - 1].ResetWindowSize();
    }

    void CreateResponse(int comesFrom, int posX, int posY)
    {
        dialogContainer.ResponseNodes = dialogContainer.ResponseNodes.Concat(new Node_Response[] { new Node_Response(new Response(), posX, posY) }).ToArray();
        dialogContainer.ResponseNodes[dialogContainer.ResponseNodes.Length - 1].response.ComesFrom = comesFrom;
        dialogContainer.ResponseNodes[dialogContainer.ResponseNodes.Length - 1].response.Id = dialogContainer.ResponseNodes.Length + maxTexts - 1;
        dialogContainer.ResponseNodes[dialogContainer.ResponseNodes.Length - 1].ResetWindowSize();

    }

    void Save()
    {
        dialogContainer.Responses = dialogContainer.ResponseNodes.Select(a => a.response).ToArray();
        dialogContainer.Dialogs = dialogContainer.DialogNodes.Select(a => a.dialog).ToArray();
    }

    public void OnDestroy()
    {
        Save();
        GetWindow(typeof(DialogEditorHelpWindow)).Close();
        GetWindow(typeof(DialogEditorSetupWindow)).Close();
    }

    public void OnDisable()
    {
        Save();
        GetWindow(typeof(DialogEditorHelpWindow)).Close();
        GetWindow(typeof(DialogEditorSetupWindow)).Close();
    }
}