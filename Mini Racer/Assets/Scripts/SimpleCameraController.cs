using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SimpleCameraController : MonoBehaviour
{
    public Transform objectiveTranf;
    
    public float smoothedSpeed = 0.125f;
    public float xOffset;
    public float zOffset;
    public float yOffset;

    public bool changeCameraX;
    public bool changeCameraY;
    public bool changeCameraZ;


    private void MoveCameraTo(Transform _objectiveTransform)
    {

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, _objectiveTransform.position, smoothedSpeed);

        smoothedPosition.x = setValueAxis(xOffset, changeCameraX, transform.position.x, smoothedPosition.x);
        smoothedPosition.y = setValueAxis(yOffset, changeCameraY, transform.position.y, smoothedPosition.y);
        smoothedPosition.z = setValueAxis(zOffset, changeCameraZ, transform.position.z, smoothedPosition.z);

        transform.position = smoothedPosition;

    }

    private float setValueAxis(float _offset, bool _change, float _actualCameraAxisValue, float _actualAxisValue)
    {
        if (_change)
        {
            return _actualAxisValue + _offset;
        }
        else
        {
            return _actualCameraAxisValue;
        }

    }

    public void Update()
    {
        MoveCameraTo(objectiveTranf.transform);
    }

}
