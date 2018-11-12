using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogConditions : MonoBehaviour
{
    public static HashSet<Condition> Conditions;
}

public enum Condition
{
    FIRST_TALK,
}
