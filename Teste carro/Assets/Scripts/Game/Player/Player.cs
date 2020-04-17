using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    public int id;
    public GameObject car;
    public int actualLap;
    public int nextCheckpoint;
    [HideInInspector] public MyTime bestTimeLap;

    // Start is called before the first frame update
    void Start()
    {
        nextCheckpoint = 0;
        actualLap = 0;
        bestTimeLap = new MyTime(10,10,0);
    }

}
