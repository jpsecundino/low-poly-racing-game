using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TrafficLight : MonoBehaviour
{
    public Light red, 
                yellow,
                green;
    public Rigidbody poleRB;

    private void Awake()
    {
        poleRB = GetComponent<Rigidbody>();
        poleRB.constraints = RigidbodyConstraints.FreezeAll;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            poleRB.constraints = RigidbodyConstraints.None;
    }

    public void OnRed()
    {
        red.enabled = true;
        yellow.enabled = false;
        green.enabled = false;
    }

    public void OnYellow()
    {
        red.enabled = false;
        yellow.enabled = true;
        green.enabled = false;
    }

    public void OnGreen()
    {
        red.enabled = false;
        yellow.enabled = false;
        green.enabled = true;
    }
}
