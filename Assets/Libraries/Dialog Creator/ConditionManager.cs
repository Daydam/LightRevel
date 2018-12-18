using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConditionManager : MonoBehaviour
{
    static ConditionManager instance;
    public static ConditionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ConditionManager>();
                if (instance == null) instance = new GameObject("New ConditionManager").AddComponent<ConditionManager>();
            }
            return instance;
        }
    }

    string[] conditions;

    public bool CheckCondition(string[,] cond)
    {
        for (int i = 0; i < cond.GetLength(0); i++)
        {
            bool valid = true;
            for (int j = 0; i < cond.GetLength(1); i++)
            {
                if (System.Array.IndexOf(conditions, cond[i, j]) == -1) valid = false;
            }
            if(valid) return true;
        }
        return false;
    }

    public void AddCondition(string cond)
    {
        if (System.Array.IndexOf(conditions, cond) == -1) conditions.Concat(new string[] { cond });
    }

    public void RemoveCondition(string cond)
    {
        if (System.Array.IndexOf(conditions, cond) != -1) conditions = conditions.Where(a => a != cond).ToArray();
    }

    void SaveConditions()
    {

    }

    void LoadConditions()
    {
        
    }
}