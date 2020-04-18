using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheelCollider;
    public Transform leftWheelTransf;
    public WheelCollider rightWheelCollider;
    public Transform rightWheelTransf;
    public bool traction; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}


[RequireComponent(typeof(PlayerInput))] 
[RequireComponent(typeof(Rigidbody))]
public class SimpleCarController : MonoBehaviour
{
    public PlayerInput input;
    private float m_steeringAngle;
    public float maxSterrAngle = 30;
    public float maxSpeed = 200f;
    public float motorForce = 1000;
    public float breakStrength;
    public string traction = "4x2 Rear";

    public List<AxleInfo> axleInfos;

    public Rigidbody carRigidBody;

    public Transform carCenterOfMass;

    private void Start()
    { 
     
        UpdateCenterOfMass();
    }

    private void FixedUpdate()
    {
        Steer();
        Accelerate();
        UpdateWheelPoses();
        Debug.Log(carRigidBody.velocity.magnitude);
    }

    private void UpdateCenterOfMass()
    {
        carRigidBody = GetComponent<Rigidbody>();

        if (carCenterOfMass)
        {
            carRigidBody.centerOfMass = carCenterOfMass.InverseTransformPoint(transform.position);
        }
    }

    private void Steer()
    {
 
        m_steeringAngle = maxSterrAngle * input.steer;
        
        foreach(AxleInfo axle in axleInfos){
            
            if (axle.steering == true)
            {
                axle.leftWheelCollider.steerAngle = m_steeringAngle;
                axle.rightWheelCollider.steerAngle = m_steeringAngle;
            }
        
        }
   
    }

    private void Accelerate()
    {
   
        foreach (AxleInfo axle in axleInfos)
        {

            if (axle.traction == true)
            {

                if (input.brake)
                {
                    //axle.leftWheelCollider.motorTorque = 0;
                    axle.leftWheelCollider.brakeTorque = breakStrength * Time.deltaTime;
                    
                    //axle.rightWheelCollider.motorTorque = 0;
                    axle.rightWheelCollider.brakeTorque = breakStrength * Time.deltaTime;
                }
                else
                {
                    axle.leftWheelCollider.motorTorque = input.throttle * motorForce;
                    axle.leftWheelCollider.brakeTorque = 0f;
                    
                    axle.rightWheelCollider.motorTorque = input.throttle * motorForce;
                    axle.rightWheelCollider.brakeTorque = 0f;
                }
            }

        }

        if (carRigidBody.velocity.magnitude > maxSpeed)
        {
            carRigidBody.velocity = carRigidBody.velocity.normalized * maxSpeed;
        }
    }


    private void UpdateWheelPoses()
    {
        
        foreach(AxleInfo axle in axleInfos)
        {
            UpdateWheelPose(axle.leftWheelCollider, axle.leftWheelTransf);
            UpdateWheelPose(axle.rightWheelCollider, axle.rightWheelTransf);
        }

    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

}
