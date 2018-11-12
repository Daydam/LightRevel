using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform_Interactible : MovingPlatform, iActivable
{
    void Start()
    {

    }

    void iActivable.Activate()
    {
        if (stopTime.Length == 0)
        {
            stopTime = new float[allPositions.Length];
            for (int i = 0; i < stopTime.Length; i++)
            {
                stopTime[i] = 0;
            }
        }
        StartCoroutine(Movement());
    }
}
