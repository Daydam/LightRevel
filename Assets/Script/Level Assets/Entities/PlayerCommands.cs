using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(ResetDetection))]
public class PlayerCommands : StoppableEntity
{

    public static event Action<float> OnWaveChargesModified = (left) => { };

    bool dead;
    public float movementSpeed;
    public float insanityDecrementSpeed;
    public float maxWaveCharges;
    Material insanity;
    float insanityLevel;
    float chargesLeft;
    float ChargesLeft { get { return chargesLeft; } set { chargesLeft = value; OnWaveChargesModified(chargesLeft); } }

    void Awake()
    {
        ResetDetection.OnResetToggled += OnResetToggled;
    }

    void Start()
    {
        insanity = Camera.main.GetComponent<PostProcess_Insanity>().insanity;
        insanity.SetFloat("_InsanityLevel", 0);
        insanityLevel = insanity.GetFloat("_InsanityLevel");
        ChargesLeft = maxWaveCharges;
        LightWaveManager.Instance.EmitWave(transform.position);
    }
    
    void Update()
    {
        if(!stopped && !dead)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) transform.position += transform.forward * movementSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) transform.position -= transform.right * movementSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) transform.position -= transform.forward * movementSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) transform.position += transform.right * movementSpeed * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space) && chargesLeft > 0)
            {
                ChargesLeft--;
                LightWaveManager.Instance.EmitWave(transform.position);
            }

            insanityLevel = Mathf.Min(1, insanityLevel + Time.deltaTime * insanityDecrementSpeed);
            insanity.SetFloat("_InsanityLevel", insanityLevel);
        }
    }

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.layer == 10)
        {
            insanityLevel = Mathf.Max(0, insanityLevel - Time.deltaTime * insanityDecrementSpeed * 5);
        }
        if (c.gameObject.layer == 11 || c.gameObject.layer == 12)
        {
            ChargesLeft = maxWaveCharges;
            insanityLevel = Mathf.Max(0, insanityLevel - Time.deltaTime * insanityDecrementSpeed * 5);
            LightWaveManager.Instance.WaveColor = c.GetComponent<LightSource>().LightColor;
        }
    }

    void OnResetToggled(bool state)
    {
        dead = state;
        if(dead == false)
        {
            LightWaveManager.Instance.EmitWave(transform.position);
        }
    }
}
