using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterParticles : MonoBehaviour
{
    public Material emitterMaterial;
    Material particleMaterial;

    void Start()
    {
        particleMaterial = GetComponent<Renderer>().sharedMaterials[0];
    }

    void Update()
    {
        particleMaterial.SetFloat("Radius", emitterMaterial.GetFloat("Radius"));
        particleMaterial.SetVector("Position", emitterMaterial.GetVector("Position"));
        particleMaterial.SetColor("_WaveColor", emitterMaterial.GetColor("_WaveColor"));

        particleMaterial.SetVector("_SpotlightPosition", emitterMaterial.GetVector("_SpotlightPosition"));
        particleMaterial.SetFloat("_SpotlightRadius", emitterMaterial.GetFloat("_SpotlightRadius"));
        particleMaterial.SetColor("_SpotlightColor", emitterMaterial.GetColor("_SpotlightColor"));
    }
}
