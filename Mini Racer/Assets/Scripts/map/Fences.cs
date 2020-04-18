using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fences : MonoBehaviour
{

    private Rigidbody fenceRB;
    private Vector3 initialTransfPosition;
    private Quaternion initialTransfRotation;
    private bool collided = false;
    public float mass = 2;
    
    private void Awake()
    {
        fenceRB = GetComponent<Rigidbody>();
        fenceRB.constraints = RigidbodyConstraints.FreezeAll;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            fenceRB.constraints = RigidbodyConstraints.None;
    }

}
