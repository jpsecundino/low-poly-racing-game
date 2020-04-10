using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    public int id;
    public GameObject car;
    [HideInInspector] public int pos;
    public int actualLap;
    public int nextCheckpoint;
    [HideInInspector] public string bestTimeLap;

    // Start is called before the first frame update
    void Start()
    {
        nextCheckpoint = 0;
        actualLap = 0;
        bestTimeLap = "00:00:00";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
