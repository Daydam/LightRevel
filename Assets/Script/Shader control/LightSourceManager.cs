using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightSourceManager: MonoBehaviour
{
	private static LightSourceManager instance;
	public static LightSourceManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType<LightSourceManager>();
				if(instance == null)
				{
					instance = new GameObject("new LightSourceManager Object").AddComponent<LightSourceManager>().GetComponent<LightSourceManager>();
				}
			}
			return instance;
		}
	}

    public static event Action<LightSource, Material, float, float> OnLightSourceActivated = (light, mat, height, radius) => { };

    PlayerCommands player;
    List<LightSource> allLights;
    LightSource closestLight;
    public float lightHeight = 5;
    public float radius = 10;
    Material lightSourceMat;

    void Start()
    {
        player = FindObjectOfType<PlayerCommands>();
        lightSourceMat = player.GetComponent<Renderer>().sharedMaterials[0];
        lightSourceMat.SetFloat("_SpotlightRadius", 0);

        if (allLights != default(List<LightSource>))
        {
            closestLight = allLights[0];
            //CheckProximity, adapted to force to turn on the closest light at the beginning of the game.
            for (int i = 0; i < allLights.Count; i++)
            {
                if (Vector3.Distance(allLights[i].transform.position, player.transform.position) < Vector3.Distance(closestLight.transform.position, player.transform.position))
                {
                    closestLight = allLights[i];
                }
            }
            OnLightSourceActivated(closestLight, lightSourceMat, lightHeight, radius);
        }
    }

    void Update()
    {
        CheckLightProximity();
    }

    void CheckLightProximity()
    {
        LightSource newClosestLight = closestLight;

        for (int i = 0; i < allLights.Count; i++)
        {
            if (Vector3.Distance(allLights[i].transform.position, player.transform.position) < Vector3.Distance(newClosestLight.transform.position, player.transform.position))
            {
                newClosestLight = allLights[i];
            }
        }
        if (newClosestLight != closestLight)
        {
            closestLight = newClosestLight;
            OnLightSourceActivated(closestLight, lightSourceMat, lightHeight, radius);
        }
    }

    public void RegisterLight(LightSource lightSource)
    {
        if (allLights == default(List<LightSource>)) allLights = new List<LightSource>();
        allLights.Add(lightSource);
    }
}
