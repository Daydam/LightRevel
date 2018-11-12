using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager: MonoBehaviour
{
	private static GameManager instance;
	public static GameManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType<GameManager>();
				if(instance == null)
				{
					instance = new GameObject("new GameManager Object").AddComponent<GameManager>().GetComponent<GameManager>();
				}
			}
			return instance;
		}
	}

    void Start()
    {
        EventManager.Instance.Subscribe(EventID.DIALOG_STARTED, OnDialogStarted);
        EventManager.Instance.Subscribe(EventID.DIALOG_ENDED, OnDialogEnded);
    }

    void OnDialogStarted(params object[] info)
    {
        OnStopEntities(true);
    }

    void OnDialogEnded(params object[] info)
    {
        OnStopEntities(false);
    }

    public Action<bool> OnStopEntities = (stopped) => { };
}
