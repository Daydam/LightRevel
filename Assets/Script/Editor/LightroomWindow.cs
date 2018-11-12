using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class LightroomWindow : EditorWindow
{
    bool hideBaseTutorial;
    LightSource[] allLights;
    LightReceiver[] allReceivers;
    PlayerCommands player;

    [MenuItem("Assistants/Light Room")]
    static void CreateWindow()
    {
        GetWindow(typeof(LightroomWindow), false, "Light Room").Show();
    }

    void OnGUI()
    {
        if (player == null) player = FindObjectOfType<PlayerCommands>();

        if (hideBaseTutorial)
        {
            if (GUILayout.Button("Show Tutorial")) hideBaseTutorial = false;
        }
        else
        {
            GUIStyle tutorialStyle = GUI.skin.GetStyle("HelpBox");
            tutorialStyle.richText = true;

            GUILayout.Box("<b>Welcome to the Light Room!</b> From here you can configurate the different lights and receivers in the game." +
                "\n" + "\n" +
                "Each <b><color=red>Light Source</color></b> will be shown in order, according to its distance to the player. In this window, you will be able to rename it and set its color." +
                "\n" + "\n" +
                "<b><color=red>Light Receivers</color></b> will also be shown following the same criteria. From here you will be able to link them to a nearby light.", tutorialStyle);
            
            if (GUILayout.Button("Hide Tutorial")) hideBaseTutorial = true;
        }
        EditorGUILayout.Space();

        GUIStyle titleStyle = new GUIStyle();
        titleStyle.fontSize = 12;
        titleStyle.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField("Light Sources: ", titleStyle);

        if (GUILayout.Button("Refresh List") || allLights == null)
        {
            allLights = FindObjectsOfType<LightSource>().OrderBy(a => Vector3.Distance(a.transform.position, player.transform.position)).ToArray();
        }
        for (int i = 0; i < allLights.Length; i++)
        {
            SerializedObject light = new SerializedObject(allLights[i]);
            light.Update();
            EditorGUILayout.BeginHorizontal();
            allLights[i].name = EditorGUILayout.DelayedTextField(allLights[i].name);
            EditorGUILayout.PropertyField(light.FindProperty("lightColor"), new GUIContent(""));
            EditorGUILayout.EndHorizontal();
            light.ApplyModifiedProperties();
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Light Receivers: ", titleStyle);

        if (GUILayout.Button("Refresh List") || allReceivers == null)
        {
            allReceivers = FindObjectsOfType<LightReceiver>().OrderBy(a => Vector3.Distance(a.transform.position, player.transform.position)).ToArray();
        }
        for (int i = 0; i < allReceivers.Length; i++)
        {
            SerializedObject receiver = new SerializedObject(allReceivers[i]);
            receiver.Update();
            EditorGUILayout.BeginHorizontal();
            allReceivers[i].name = EditorGUILayout.DelayedTextField(allReceivers[i].name);
            EditorGUILayout.PropertyField(receiver.FindProperty("linkedSource"), new GUIContent(""));
            EditorGUILayout.EndHorizontal();
            receiver.ApplyModifiedProperties();
        }
    }
}