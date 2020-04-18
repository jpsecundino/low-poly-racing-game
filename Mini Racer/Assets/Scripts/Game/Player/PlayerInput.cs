using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour, InputManager
{
    [HideInInspector] public float steer { get; set; }
    [HideInInspector] public float throttle { get; set; }

    [SerializeField]
    public bool brake { get; set; }

    void Update()
    {
        steer = Input.GetAxis("Horizontal");
        throttle = Input.GetAxis("Vertical");
       
        brake = Input.GetKey(KeyCode.Space);
    }

}
