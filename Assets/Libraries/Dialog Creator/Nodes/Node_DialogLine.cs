using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node_DialogLine : Node
{
    public DialogLine dialog;

    public Node_DialogLine(DialogLine d, int posX = 20, int posY = 60, int width = 300, int height = 75)
    {
        dialog = d;
        window = new Rect(posX, posY, width, height);
        this.posX = posX;
        this.posY = posY;
    }

    public override void ResetWindowSize()
    {
        window.height = dialog.Id == 0 ? 75 : 110;
        if (showText) window.height += Mathf.Min(valueSpace + valueSpace * dialog.Text.Length, valueSpace * DialogEditorWindow.maxDialogLines);
        if (showConditionGroups)
        {
            window.height += Mathf.Min(valueSpace + valueSpace * dialog.Conditions.GetLength(0), valueSpace * DialogEditorWindow.maxConditionGroups);
            for (int i = 0; i < showConditions.Length; i++)
            {
                if (showConditions[i]) window.height += valueSpace * dialog.Conditions.GetLength(1);
            }
        }
    }
}
