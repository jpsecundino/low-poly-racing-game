using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    public int id;
    public GameObject car;
    public int actualLap;
    public int actualCheckpoint;
    [HideInInspector] public MyTime bestTimeLap;

    // Start is called before the first frame update
    void Start()
    {
        actualCheckpoint = -1;
        actualLap = 0;
        bestTimeLap = new MyTime(10,10,0);
    }

}
