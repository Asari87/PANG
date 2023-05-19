using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantObjectsManager : MonoBehaviour
{
    private static PersistantObjectsManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        EnableChildObjects();
    }

    private void EnableChildObjects()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
