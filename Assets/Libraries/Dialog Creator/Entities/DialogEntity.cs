using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEntity : StoppableEntity
{
    public SO_DialogContainer dialog;
    bool interactionReady;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 9) interactionReady = true;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == 9) interactionReady = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            StartDialog();
    }

    void StartDialog()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        DialogManager.Instance.LoadDialog(dialog.GetDialog);
    }
}
