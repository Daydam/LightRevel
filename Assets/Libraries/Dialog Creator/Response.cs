using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Response : DialogUnit
{
    [SerializeField]
    string responseText;
    public string ResponseText { get { return responseText; } set { responseText = value; } }
}
