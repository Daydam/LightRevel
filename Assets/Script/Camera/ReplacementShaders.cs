using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplacementShaders : MonoBehaviour
{
    public Shader replacementShader;
    public bool shaderEnabled;

    void Update()
    {
        if (shaderEnabled && replacementShader != null)
            GetComponent<Camera>().SetReplacementShader(replacementShader, null);
        else
            GetComponent<Camera>().ResetReplacementShader();
    }
}
