using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoControl : MonoBehaviour, InputManager
{
    [HideInInspector] public float steer { get; set; }
    [HideInInspector] public float throttle { get; set; }

    public Transform[] waypoints;
    int current = 0;
    float WPradius = 1;
    public bool brake { get; set; }

    void Start()
    {
        waypoints = GameObject.Find("Waypoints").GetComponentsInChildren<Transform>();
        Debug.Log(waypoints.Length);
        brake = false;
    }
    // Update is called once per frame
    void Update()
    {
        //problema ao coletar as posições dos waypoints: todas são 0,0,0
        if(Vector3.Distance(waypoints[current].position, transform.position) < WPradius)
        {
            current = (current + 1) % waypoints.Length;
        }

        Debug.Log(waypoints[current].position);

        Vector3 _whereToGo = (transform.position - waypoints[current].transform.position).normalized;  

        steer = Vector3.Project(_whereToGo, transform.right).x;
        throttle = Vector3.Project(_whereToGo, transform.forward).z;
        

    }



}
