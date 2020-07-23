using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    public List<AxleInfo> AxleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    private Vector2 axisInput;

    private void FixedUpdate()
    {
        float motor = maxMotorTorque * axisInput.y;
        float steering = maxSteeringAngle * axisInput.x;

        foreach (AxleInfo axleInfo in AxleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }

    public void Input(Vector2 direction)
    {
        axisInput = direction;
    }
}

[Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}
