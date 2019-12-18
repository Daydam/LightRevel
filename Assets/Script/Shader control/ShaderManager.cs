using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    private static ShaderManager instance;
    public static ShaderManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ShaderManager>();
                if (instance == null)
                {
                    instance = new GameObject("new ShaderManager Object").AddComponent<ShaderManager>().GetComponent<ShaderManager>();
                }
            }
            return instance;
        }
    }

    PlayerCommands player;

    void OnEnable()
    {
        player = FindObjectOfType<PlayerCommands>();
    }

    void Update()
    {
        Shader.SetGlobalVector("PlayerPos", player.transform.position);
    }
}
