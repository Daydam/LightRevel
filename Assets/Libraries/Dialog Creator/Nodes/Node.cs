using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Rect window;
    public int posX;
    public int posY;
    public bool showText;
    public bool showConditionGroups;
    public bool[] showConditions;
    protected int valueSpace = 20;

    public virtual void ResetWindowSize() { }
}
