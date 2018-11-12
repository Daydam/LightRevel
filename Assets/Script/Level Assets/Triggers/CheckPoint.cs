using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LightSource))]
public class CheckPoint : MonoBehaviour
{
    public Vector3 respawnPoint;
    public Vector3 RespawnPoint { get { return respawnPoint; } }
    ResetDetection player;
    ParticleSystem checkpointParticles;

    void Start()
    {
        LightSourceManager.OnLightSourceActivated += LightSourceActivated;
        player = FindObjectOfType<ResetDetection>();
        checkpointParticles = GetComponentInChildren<ParticleSystem>();
        var partMain = checkpointParticles.main;
        partMain.startColor = GetComponent<LightSource>().LightColor;
    }

    void LightSourceActivated(LightSource ls, Material mat, float height, float radius)
    {
        if (ls == GetComponent<LightSource>())
            checkpointParticles.Play();
        else checkpointParticles.Stop();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(respawnPoint, 2f);
    }
}
