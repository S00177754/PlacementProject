using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargettingController : MonoBehaviour
{
    public TargetableObject LockOnObject;
    public bool IsOnScreen = false; 
    public bool IsLockedOn = false; 
    public Camera playerCamera;

    [Header("Target Sprites")]
    public Sprite PossibleTarget;
    public Sprite LockedOntoTarget;
    public Vector2 SizeCloseUp;
    public Vector2 SizeFarAway;

    private Image TargetIcon;
    private RectTransform rect;
    private Vector3 targetPos;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        TargetIcon = GetComponent<Image>();

        SetLockedTarget(LockOnObject);
    }

    private void Update()
    {
        if(LockOnObject != null)
        {
            targetPos = playerCamera.WorldToScreenPoint(LockOnObject.gameObject.transform.position + LockOnObject.TargetIconLocation);

            if(targetPos.z < 0)
            {
                TargetIcon.color = SetTargetAlpha(0);
                IsOnScreen = false;
            }
            else
            {
                TargetIcon.color = SetTargetAlpha(1);
                IsOnScreen = true;
            }

            rect.position = targetPos;
        }
        else
        {
            SetTargetNull();
        }
    }

    public void SetPossibleTarget(TargetableObject objectLockOn)
    {
        if (objectLockOn != null)
        {
            LockOnObject = objectLockOn;
            TargetIcon.sprite = PossibleTarget;
            TargetIcon.color = SetTargetAlpha(1);
            return;
        }

        SetTargetNull();
    }

    public void SetLockedTarget(TargetableObject objectLockOn)
    {
        LockOnObject = objectLockOn;
        IsLockedOn = true;
        TargetIcon.sprite = LockedOntoTarget;
        TargetIcon.color = SetTargetAlpha(1);
    }

    public void SetTargetNull()
    {
        LockOnObject = null;
        IsLockedOn = false;
        TargetIcon.color = SetTargetAlpha(0);
    }

    private Color SetTargetAlpha(float alpha)
    {
        Color color = TargetIcon.color;
        color.a = alpha;
        return color;
    }
}
