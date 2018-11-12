using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGhost : MonoBehaviour
{
    Vector3 target;

	void Update ()
	{
		if(target != null)
        {
            //ACÁ SE HACE LA STEERING BEHAVIOUR
        }
	}

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer == 10) target = c.transform.position;
    }
}
