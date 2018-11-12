using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResetDetection : MonoBehaviour
{
    public static Action<bool> OnResetToggled = (state) => { };

    public ParticleSystem deathParticles;
    Rigidbody rb;
    bool dead;
    Vector3 startingPoint;
    public Vector3 StartingPoint { get { return startingPoint; } set { startingPoint = value; } }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPoint = transform.position;
    }

    void Update ()
	{
        //Reset a lo re head
        if (transform.position.y < -20 && dead == false)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            dead = true;
            deathParticles.transform.position = transform.position;
            deathParticles.Play();
            OnResetToggled(true);
        }

        if(dead == true && !deathParticles.isPlaying)
        {
            rb.useGravity = true;
            transform.position = startingPoint;
            dead = false;
            OnResetToggled(false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 12) startingPoint = col.GetComponent<CheckPoint>().RespawnPoint;
    }
}
