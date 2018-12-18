using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class DialogLine : DialogUnit
{
    //As long as there's still more in the array, we'll only see a Continue button. Once it gets to the last index, the options show up.
    [SerializeField]
    string[] text;
    public string[] Text { get { return text; } set { text = value; } }
}
