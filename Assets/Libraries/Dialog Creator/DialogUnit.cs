using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogUnit
{
    [SerializeField]
    int id = 5000;
    public int Id { get { return id; } set { id = value; } }
    [SerializeField]
    int comesFrom;
    public int ComesFrom { get { return comesFrom; } set { comesFrom = value; } }
    //Conditions will work with Or,And groups. This means the first index will indicate a group of And conditions, and each group will be connected by an OR.
    [SerializeField]
    string[,] conditions;
    public string[,] Conditions { get { return conditions; } set { conditions = value; } }

    //We need a constructor to make sure saving actually clones the dialog/response, instead of just creating a new pointer towards it.
}
