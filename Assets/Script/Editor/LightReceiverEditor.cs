using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LightReceiver))]
public class LightReceiverEditor : Editor
{
    LightReceiver tg { get { return (LightReceiver)target; } }
    bool linkedSourceHelp;

    public override void OnInspectorGUI()
    {
        SerializedObject receiver = new SerializedObject(tg);
        receiver.Update();

        GUIStyle tutorialStyle = GUI.skin.GetStyle("HelpBox");
        tutorialStyle.richText = true;


        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(receiver.FindProperty("linkedSource"), new GUIContent("Linked Source"));
        if (GUILayout.Button("?")) linkedSourceHelp = !linkedSourceHelp;
        EditorGUILayout.EndHorizontal();

        if (linkedSourceHelp)
        {
            GUILayout.Box("Use a <b><color=red>Light Source</color></b> as a reference to set the <b><color=red>Light Receiver</color></b>'s activation color! " +
                "The <b><color=red>Light Receiver</color></b> will only become activated after being lit with that color.", tutorialStyle);
        }

        tg.activationSpeed = EditorGUILayout.FloatField("Activation Speed", tg.activationSpeed);

        EditorGUILayout.PropertyField(receiver.FindProperty("interactObj"), new GUIContent("Activable Object"));
        if (tg.interactObj != null && tg.interactObj.GetComponent<iActivable>() == null)
            EditorGUILayout.HelpBox("You need to put an iActivable object into this field!", MessageType.Error);
        receiver.ApplyModifiedProperties();
    }
}
