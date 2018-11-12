using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(MovingPlatform), true)]
public class MovingPlatformEditor : Editor
{
    MovingPlatform Target { get { return (MovingPlatform)target; } }
    GUIStyle titleFormat;
    int startingPoint;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        if (titleFormat == null) titleFormat = new GUIStyle();
        titleFormat.fontStyle = FontStyle.Bold;

        if (Target.allPositions == null || Target.allPositions.Length < 1)
        {
            Target.allPositions = new Vector3[1] { Target.transform.position };
        }
        if (Target.stopTime == null || Target.stopTime.Length < 1) Target.stopTime = new float[1];

        startingPoint = Target.CurrentTarget - 1 < 0 ? Target.allPositions.Length - 1 : Target.currentTarget - 1;

        EditorGUI.BeginChangeCheck();
        Target.CurrentTarget = EditorGUILayout.IntSlider("Starting Point", startingPoint, 0, Target.allPositions.Length - 1) + 1;
        if(EditorGUI.EndChangeCheck())
        {
            startingPoint = Target.CurrentTarget - 1 < 0 ? Target.allPositions.Length - 1 : Target.currentTarget - 1;
            Target.transform.position = Target.allPositions[startingPoint];
        }

        EditorGUILayout.LabelField("Positions list:", titleFormat);
        for (int i = 0; i < Target.allPositions.Length; i++)
        {
            EditorGUI.BeginDisabledGroup(i == startingPoint);
            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            var newPosition = EditorGUILayout.Vector3Field("Point " + (i + 1) + ":", Target.allPositions[i]);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(Target, "Change position " + i);
                Target.allPositions[i] = newPosition;
            }
            EditorGUI.EndDisabledGroup();

            if (GUILayout.Button("Remove"))
            {
                var part1 = Target.allPositions.Take(i);
                var part2 = Target.allPositions.Skip(i + 1);
                Target.allPositions = part1.Concat(part2).ToArray();
            }

            EditorGUILayout.EndHorizontal();
        }
        if (GUILayout.Button("Add Point"))
        {
            Target.allPositions = Target.allPositions.Concat(new Vector3[] { new Vector3(0, -0.5f, 0) }).ToArray();
        }

        EditorGUILayout.Space();
        if (Target.stopTime.Length > 1)
        {
            EditorGUILayout.LabelField("Stop times:", titleFormat);
            for (int i = 0; i < Target.stopTime.Length; i++)
            {
                Target.stopTime[i] = EditorGUILayout.FloatField("Point " + (i + 1) + ":", Target.stopTime[i]);
            }
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Stop time:", titleFormat);
            Target.stopTime[0] = EditorGUILayout.FloatField(Target.stopTime[0]);
            EditorGUILayout.EndHorizontal();
        }
        if (Target.stopTime.Length > 1 && GUILayout.Button("Make single stop time"))
        {
            var targ = Target.stopTime[0];
            Target.stopTime = new float[1] { targ };
        }
        if (Target.stopTime.Length == 1 && GUILayout.Button("Make different stop times"))
        {
            float targ1 = Target.stopTime[0];
            Target.stopTime = new float[Target.allPositions.Length];
            for (int i = 0; i < Target.stopTime.Length; i++)
            {
                Target.stopTime[i] = targ1;
            }
        }
    }

    void OnSceneGUI()
    {
        if (!Application.isPlaying)
        {
            Target.allPositions[startingPoint] = Target.transform.position;

            for (int i = 0; i < Target.allPositions.Length; i++)
            {
                if (i != startingPoint)
                {
                    EditorGUI.BeginChangeCheck();
                    var newPosition = Handles.DoPositionHandle(Target.allPositions[i], Quaternion.identity);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(Target, "Change position " + i);
                        Target.allPositions[i] = newPosition;
                    }
                }
            }
        }
    }
}