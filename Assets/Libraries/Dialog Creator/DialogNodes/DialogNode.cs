using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogNode
{
    public Rect window;
    public Rect[] connectionDots;
    public Dialog d;
    public DialogNode[] connections;

    public DialogNode(Dialog d, int posX = 50, int posY = 50, int width = 400, int height = 200)
    {
        this.d = d;
        connections = new DialogNode[3];
        window = new Rect(posX, posY, width, height);
        connectionDots = new Rect[]
        {
            new Rect(window.position.x, window.position.y + window.height/2, 20, 20),
            new Rect(window.position.x + window.width, window.position.y + (window.height/5)*2, 20, 20),
            new Rect(window.position.x + window.width, window.position.y + (window.height/5)*3, 20, 20),
            new Rect(window.position.x + window.width, window.position.y + (window.height/5)*4, 20, 20),
        };
    }

    public void UpdateConnectionDots()
    {
        connectionDots[0].position = new Vector2(window.position.x - 10, window.position.y + window.height / 2);
        connectionDots[1].position = new Vector2(window.position.x + window.width - 10, window.position.y + (window.height / 4) * 1);
        connectionDots[2].position = new Vector2(window.position.x + window.width - 10, window.position.y + (window.height / 4) * 2);
        connectionDots[3].position = new Vector2(window.position.x + window.width - 10, window.position.y + (window.height / 4) * 3);
    }
}
