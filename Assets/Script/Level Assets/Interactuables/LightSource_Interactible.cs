using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource_Interactible : LightSource, iActivable
{
    void Awake()
    {
        GetComponent<SphereCollider>().radius = 0;
    }

    void iActivable.Activate()
    {
        RegisterLightSource();
    }
}
