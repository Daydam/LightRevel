﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactible : MonoBehaviour
{
    public iActivable activationTarget;

    protected abstract void Interact();
}
