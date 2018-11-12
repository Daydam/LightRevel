using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoppableEntity : MonoBehaviour
{
    bool started;
    protected bool stopped;

    void OnEnable()
    {
        GameManager.Instance.OnStopEntities += StopEntity;
    }

    void OnDisable()
    {
        GameManager.Instance.OnStopEntities -= StopEntity;
    }

    void StopEntity(bool stop)
    {
        stopped = stop;
    }
}
