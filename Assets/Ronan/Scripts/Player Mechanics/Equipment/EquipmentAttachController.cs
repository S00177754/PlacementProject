using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttachPoint { LeftHand, LeftHip, RightHand, RightHip, Back}

public class EquipmentAttachController : MonoBehaviour
{
    [Header("Attatchment Points")]
    public Transform LeftHandAttachPoint;
    public Transform RightHandAttachPoint;
    public Transform BackAttachPoint;
    public Transform LeftHipAttachPoint;
    public Transform RightHipAttachPoint;

    public void AttatchTo(AttachPoint attachPoint,Transform gameObj)
    {
        switch (attachPoint)
        {
            case AttachPoint.LeftHand:
                gameObj.transform.parent = LeftHandAttachPoint.transform;
                gameObj.transform.position = LeftHandAttachPoint.transform.position;
                break;

            case AttachPoint.RightHand:
                gameObj.transform.parent = RightHandAttachPoint.transform;
                gameObj.transform.position = RightHandAttachPoint.transform.position;
                break;

            case AttachPoint.RightHip:
                gameObj.transform.parent = RightHipAttachPoint.transform;
                gameObj.transform.position = RightHipAttachPoint.transform.position;
                gameObj.transform.localEulerAngles = new Vector3(190,-85,48);
                break;

            case AttachPoint.LeftHip:
                gameObj.transform.parent = LeftHipAttachPoint.transform;
                gameObj.transform.position = LeftHipAttachPoint.transform.position;
                break;

            case AttachPoint.Back:
                gameObj.transform.parent = BackAttachPoint.transform;
                gameObj.transform.position = BackAttachPoint.transform.position;
                break;
        }
    }
}
