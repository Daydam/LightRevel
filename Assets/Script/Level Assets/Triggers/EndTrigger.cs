using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    PlayerCommands player;
    ParticleSystem endParticles;

    void Start()
    {
        player = FindObjectOfType<PlayerCommands>();
        endParticles = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (Mathf.Abs(Vector3.Distance(player.transform.position, transform.position)) < 40)
        {
            if (!endParticles.isPlaying) endParticles.Play();
        }
        else endParticles.Stop();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 9) SceneManager.LoadScene("You Won");
    }
}
