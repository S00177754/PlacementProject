using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttachPoint { LeftHand, LeftHip, RightHand, RightHip, Back}

[Serializable]
public class EquipmentAttachController
{
    [Header("Attatchment Points")]
    public Transform LeftHandAttachPoint;
    public Transform RightHandAttachPoint;
    public Transform BackAttachPoint;
    public Transform LeftHipAttachPoint;
    public Transform RightHipAttachPoint;

    public void AttatchTo(AttachPoint attachPoint, WeaponInfo info)
    {
        if (info == null)
            return;

        Quaternion rotation;
        switch (attachPoint)
        {
            case AttachPoint.LeftHand:
                info.gameObject.transform.SetParent(LeftHandAttachPoint);
                info.gameObject.transform.localPosition = info.PositioningDetails.UnsheathedPosition;
                rotation = Quaternion.Euler(info.PositioningDetails.UnsheathedRotation);
                info.gameObject.transform.localRotation = rotation;
                //animator set weapon to true
                break;

            case AttachPoint.RightHand:
                info.gameObject.transform.SetParent(RightHandAttachPoint);
                info.gameObject.transform.localPosition = info.PositioningDetails.UnsheathedPosition;
                rotation = Quaternion.Euler(info.PositioningDetails.UnsheathedRotation);
                info.gameObject.transform.localRotation = rotation;
                //animator set weapon to true
                break;

            case AttachPoint.RightHip:
                info.gameObject.transform.SetParent(RightHipAttachPoint);
                info.gameObject.transform.localPosition = info.PositioningDetails.SheathedPosition;
                rotation = Quaternion.Euler(info.PositioningDetails.SheathedRotation);
                info.gameObject.transform.localRotation = rotation;
                //animator set weapon to false
                break;

            case AttachPoint.LeftHip:
                info.gameObject.transform.SetParent(LeftHipAttachPoint);
                info.gameObject.transform.localPosition = info.PositioningDetails.SheathedPosition;
                rotation = Quaternion.Euler(info.PositioningDetails.SheathedRotation);
                info.gameObject.transform.localRotation = rotation;
                //animator set weapon to false
                break;

            case AttachPoint.Back:
                info.gameObject.transform.SetParent(BackAttachPoint);
                info.gameObject.transform.localPosition = info.PositioningDetails.SheathedPosition;
                rotation = Quaternion.Euler(info.PositioningDetails.SheathedRotation);
                info.gameObject.transform.localRotation = rotation;
                //animator set weapon to false
                break;
        }
    }
}
