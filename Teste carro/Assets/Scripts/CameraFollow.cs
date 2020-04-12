using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour
{
    private Func<Vector3> GetCameraFollowPositionFunc;

    public void Setup(Func<Vector3> _GetCameraFollowPositionFunc)
    {
        GetCameraFollowPositionFunc = _GetCameraFollowPositionFunc;
    }
    
    
    void Update()
    {
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        transform.position = cameraFollowPosition ;

    }
}
