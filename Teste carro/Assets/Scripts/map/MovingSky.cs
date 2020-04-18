using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSky : MonoBehaviour
{

    // Update is called once per frame
    public float step = 1;
    void Update()
    {
        transform.position = new Vector3(transform.position.x + step * Time.deltaTime, transform.position.y, transform.position.z);        
    }
}
