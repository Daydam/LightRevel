using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Dialog
{
    public List<Condition> conditions;
    public List<string> textOptions;
    public List<Dialog> next;

    public Dialog ()
    {
        textOptions = new List<string>();
        next = new List<Dialog>();
        conditions = new List<Condition>();
    }

    public string GetDialogLine()
    {
        for (int i = 0; i < conditions.Count; i++)
        {
            if (!DialogConditions.Conditions.Contains(conditions[i])) return "";
        }
        
        return textOptions[Random.Range(0, textOptions.Count)];
    }
}