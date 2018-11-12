using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightWaveManager: MonoBehaviour
{
	private static LightWaveManager instance;
	public static LightWaveManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType<LightWaveManager>();
				if(instance == null)
				{
					instance = new GameObject("new LightWaveManager Object").AddComponent<LightWaveManager>().GetComponent<LightWaveManager>();
				}
			}
			return instance;
		}
	}

    public static Action<Vector3, float, Color> OnWaveEmitted = (position, radius, color) => { };

    public float growthSpeed;
    public SphereCollider lightTrigger;
    Material waveMat;
    public Material WaveMat { get { return waveMat; } }
    ParticleSystem partEffect;
    Color waveColor = Color.white;
    public Color WaveColor { get { return waveColor; } set { waveColor = value; } }

    void Start()
    {
        var player = FindObjectOfType<PlayerCommands>();
        waveMat = player.GetComponent<Renderer>().sharedMaterials[0];
        partEffect = GetComponentInChildren<ParticleSystem>();
    }

    public void EmitWave(Vector3 position)
    {
        StopAllCoroutines();
        StartCoroutine(EmitWaveCoroutine(position));
        if (partEffect != null)
        {
            var partMain = partEffect.main;
            partMain.startColor = waveColor;
            partEffect.transform.position = position;
            partEffect.Play();
        }
    }

    IEnumerator EmitWaveCoroutine(Vector3 position)
    {
        Color currentColor = new Color(waveColor.r, waveColor.g, waveColor.b, waveColor.a);
        float radius = 0;
        waveMat.SetFloat("Radius", radius);
        waveMat.SetVector("Position", position);
        waveMat.SetColor("_WaveColor", waveColor);
        lightTrigger.gameObject.SetActive(true);
        lightTrigger.transform.position = position;
        while (radius < 80)
        {
            radius += Time.deltaTime * growthSpeed;
            waveMat.SetFloat("Radius", radius);
            lightTrigger.radius = Mathf.Min(radius / 2, waveMat.GetFloat("_MaxRadius"));
            OnWaveEmitted(lightTrigger.transform.position, lightTrigger.radius, currentColor);
            yield return new WaitForEndOfFrame();
        }
        lightTrigger.radius = 0;
        lightTrigger.gameObject.SetActive(false);
    }
}
