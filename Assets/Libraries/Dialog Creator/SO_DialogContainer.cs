using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_DialogContainer : ScriptableObject
{
    public List<DialogNode> dialogChain;
    public Dialog GetDialog { get { return dialogChain[0].d; } }
}
