using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public float steer;
    [HideInInspector] public float throttle;
    public bool brake;

    // Update is called once per frame
    void Update()
    {
        steer = Input.GetAxis("Horizontal");
        throttle = Input.GetAxis("Vertical");

        brake = Input.GetKey(KeyCode.Space);
    }
    
}
