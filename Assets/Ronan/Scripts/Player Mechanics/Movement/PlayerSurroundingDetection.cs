using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSurroundingDetection : MonoBehaviour
{
    public Transform Front;
    public Transform Bottom;
    public LayerMask Layer;
    public float GroundRangeCheck = 0.4f;
    public float FallToLandDistance = 0.3f;

    public bool OnGround()
    {
        if (Physics.CheckSphere(Bottom.position, GroundRangeCheck, Layer))
            return true;

        return false;
    }

    public bool LandOnGroundCheck()
    {
        if (Physics.CheckSphere(Bottom.position, GroundRangeCheck, Layer))
            return true;

        return false;
    }
}
