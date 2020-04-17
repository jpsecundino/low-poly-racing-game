using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public static event Action<int, Collider> OnCheckpointEntered;

    [SerializeField]
    private int checkpointId = 0;

    private void OnTriggerEnter(Collider carCollider)
    {
        Debug.Log("collider ok");
        Debug.Log(carCollider.tag);
        //if a player entered checkpoint
        if (carCollider.CompareTag("Player"))
        {
            Debug.Log("tag ok");
            if (OnCheckpointEntered != null)
            {
                OnCheckpointEntered(checkpointId, carCollider);
            }
        }  

    }
}