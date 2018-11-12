using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class LightSource : MonoBehaviour
{
    public Color lightColor = Color.white;
    public Color LightColor { get { return lightColor; } }
    public LightSource[] neighbors;

    protected SphereCollider lightTrigger;

    protected void RegisterLightSource()
    {
        LightSourceManager.Instance.RegisterLight(this);
        LightSourceManager.OnLightSourceActivated += OnLightSourceActivated;
        lightTrigger = GetComponent<SphereCollider>();
        lightTrigger.isTrigger = true;
    }

    protected void OnLightSourceActivated(LightSource ls, Material mat, float height, float radius)
    {
        if (ls == this)
            StartCoroutine(TurnLightOn(mat, height, radius));
        else
        {
            StopAllCoroutines();
            lightTrigger.radius = 0;
        }
    }

    IEnumerator TurnLightOn(Material mat, float h, float r)
    {
        float currentRadius = mat.GetFloat("_SpotlightRadius");

        while (currentRadius > 5)
        {
            currentRadius = Mathf.Min(currentRadius - Time.deltaTime * 5, r);
            mat.SetFloat("_SpotlightRadius", currentRadius);
            yield return new WaitForEndOfFrame();
        }

        currentRadius = 5;
        mat.SetVector("_SpotlightPosition", new Vector4(transform.position.x, h, transform.position.z, 0));
        mat.SetFloat("_SpotlightRadius", currentRadius);
        mat.SetColor("_SpotlightColor", lightColor);
        while (currentRadius < r)
        {
            currentRadius = Mathf.Min(currentRadius + Time.deltaTime * 5, r);
            mat.SetFloat("_SpotlightRadius", currentRadius);
            //Hardcodeado hasta la concha de la lora. El radio de la luz es 5, pero por la altura el de la luz es 3.
            lightTrigger.radius = (currentRadius * 3) / 5;
            yield return new WaitForEndOfFrame();
        }
    }
}
