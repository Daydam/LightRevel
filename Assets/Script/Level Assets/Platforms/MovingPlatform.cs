using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3[] allPositions;
    public float[] stopTime;
    public float movementSpeed;
    public int currentTarget = 1;
    public int CurrentTarget { get { return currentTarget; } set { currentTarget = value >= allPositions.Length ? 0 : value < 0 ? allPositions.Length -1 : value; } }
    float count;

    void Start()
    {
        if (stopTime.Length == 0)
        {
            stopTime = new float[allPositions.Length];
            for (int i = 0; i < stopTime.Length; i++)
            {
                stopTime[i] = 0;
            }
        }
        StartCoroutine(Movement());
    }

    protected IEnumerator Movement()
    {
        while (true)
        {
            while (transform.position != allPositions[currentTarget])
            {
                transform.position += (allPositions[currentTarget] - transform.position).normalized * movementSpeed * Time.deltaTime;
                if (Vector3.Distance(allPositions[currentTarget], transform.position) <= movementSpeed * Time.deltaTime) transform.position = allPositions[currentTarget];
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(stopTime[Mathf.Max(0, Mathf.Min(currentTarget, stopTime.Length - 1))]);
            CurrentTarget++;
            count = 0;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 9) col.transform.parent = transform;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == 9 && col.transform.parent == transform) col.transform.parent = null;
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < allPositions.Length; i++)
        {
            Gizmos.DrawWireSphere(allPositions[i], 0.2f);
            if (allPositions.Length > 1)
            {
                if (i + 1 >= allPositions.Length)
                    Gizmos.DrawLine(allPositions[i], allPositions[0]);
                else
                    Gizmos.DrawLine(allPositions[i], allPositions[i + 1]);
            }
        }
    }
}
