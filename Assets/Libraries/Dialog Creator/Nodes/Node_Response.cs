using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node_Response : Node
{
    public Response response;

    public Node_Response(Response d, int posX = 20, int posY = 60, int width = 300, int height = 125)
    {
        response = d;
        window = new Rect(posX, posY, width, height);
        this.posX = posX;
        this.posY = posY;
    }

    public override void ResetWindowSize()
    {
        window.height = 125;
        if (showConditionGroups)
        {
            window.height += Mathf.Min(valueSpace + valueSpace * response.Conditions.GetLength(0), valueSpace * DialogEditorWindow.maxConditionGroups);
            for (int i = 0; i < showConditions.Length; i++)
            {
                if(showConditions[i])window.height += valueSpace * response.Conditions.GetLength(1);
            }
        }
    }
}
