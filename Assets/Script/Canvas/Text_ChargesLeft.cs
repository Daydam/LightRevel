using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_ChargesLeft : MonoBehaviour
{
    Text chargesLeft;

	void Awake ()
	{
        chargesLeft = GetComponent<Text>();
        PlayerCommands.OnWaveChargesModified += ModifyChargesText;
	}

    void ModifyChargesText(float charges)
    {
        chargesLeft.text = "Charges Left: " + charges;
    }
}
