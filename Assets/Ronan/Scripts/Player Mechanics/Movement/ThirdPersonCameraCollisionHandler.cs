using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraCollisionHandler : MonoBehaviour
{
    public Transform CameraParent;
    public float CollisonOffset = 0.2f;
    

    private Vector3 DefaultPosition;
    private Vector3 directionNormalized;
    private Transform parentTransform;
    private float DefaultDistance;

    void Start()
    {
        DefaultPosition = transform.localPosition;
        directionNormalized = DefaultPosition.normalized;
        parentTransform = transform.parent;
        DefaultDistance = Vector3.Distance(DefaultPosition, Vector3.zero);
    }

    // FixedUpdate for physics calculations
    void FixedUpdate()
    {
        Vector3 currentPos = DefaultPosition;

        RaycastHit hit;
        Vector3 directionTemp = parentTransform.TransformPoint(DefaultPosition) - CameraParent.position;

        if (Physics.SphereCast(CameraParent.position, CollisonOffset, directionTemp, out hit, DefaultDistance))
        {
            currentPos = (directionNormalized * (hit.distance - CollisonOffset));
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, currentPos, Time.deltaTime * 15f);
    }
}
