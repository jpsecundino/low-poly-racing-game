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
        //if a player entered checkpoint
        if (carCollider.CompareTag("Player"))
        {
            {
                OnCheckpointEntered(checkpointId, carCollider);
            }
        }  

    }
}