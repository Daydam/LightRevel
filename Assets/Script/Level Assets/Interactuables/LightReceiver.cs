using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReceiver : Interactible
{
    //This focken shet is here to avoid Unity from freaking destroying my OnEnable code at the beginning of the game.
    bool started;

    public LightSource linkedSource;
    public float activationSpeed;

    //This fucking shit sucks! Part 1
    public GameObject interactObj;

    Material mat;

	void Start ()
	{
        //This fucking shit sucks! Part 2. Thx, Unity.
        activationTarget = interactObj.GetComponent<iActivable>();

        mat = GetComponent<Renderer>().sharedMaterial;
        mat.SetColor("_RequiredColor", linkedSource.LightColor);
        mat.SetFloat("_Activated", 0f);
        started = true;
        LightWaveManager.OnWaveEmitted += CheckLightWaveCollision;
    }

    void OnEnable()
    {
        if (started && mat.GetFloat("_Activated") <= 0f) LightWaveManager.OnWaveEmitted += CheckLightWaveCollision;
    }

    void OnDisable()
    {
        LightWaveManager.OnWaveEmitted -= CheckLightWaveCollision;
    }

    void OnTriggerStay(Collider c)
    {
        if(mat.GetFloat("_Activated") <= 0f)
        {
            if (c.gameObject.layer == 10)
            {
                if (LightWaveManager.Instance.WaveMat.GetColor("_WaveColor") == mat.GetColor("_RequiredColor")) Interact();
            }
            if (c.gameObject.layer == 11 || c.gameObject.layer == 12)
            {
                Color lightColor = c.GetComponent<LightSource>().LightColor;
                if (lightColor == mat.GetColor("_RequiredColor")) Interact();
            }
        }
    }

    void CheckLightWaveCollision(Vector3 position, float radius, Color color)
    {
        if (Vector3.Distance(position, transform.position) <= radius && color == mat.GetColor("_RequiredColor"))
        {
            Interact();
            LightWaveManager.OnWaveEmitted -= CheckLightWaveCollision;
        }
    }

    protected override void Interact()
    {
        StartCoroutine(ActivateReceiver());
    }

    IEnumerator ActivateReceiver()
    {
        float act = 0;

        while(act < 1)
        {
            act += Time.deltaTime * activationSpeed;
            mat.SetFloat("_Activated", act);
            yield return new WaitForEndOfFrame();
        }
        mat.SetFloat("_Activated", 1f);
        activationTarget.Activate();
    }
}
