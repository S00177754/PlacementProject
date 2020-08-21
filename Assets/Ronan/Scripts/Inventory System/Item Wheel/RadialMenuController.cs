using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RadialMenuController : MonoBehaviour
{
    //*************** Public Variables ********************
    public bool IsActive = false; //If IsActive is set to false then the segment checking logic will not run
    [Range(0.1f, 0.9f)]
    public float InputResistance = 0.1f;

    [Header("Highlight Colors")]
    public Color UnrestrictedHighlight;
    public Color RestrictedHighlight;

    [Tooltip("Start from top segment and add in order of clockwise.")]
    public List<RadialMenuSection> Sections;

    //*************** Private Variables ******************
    private Vector2 input_axis;
    protected int segmentNum = -1;
    private Vector2 MousePosition;

    //**************** Monobehaviour Methods ******************
    public virtual void Update()
    {
        if (IsActive)
        {
            if (input_axis != Vector2.zero)
            {
                segmentNum = CheckActiveSegment(input_axis);
                if (segmentNum != 0)
                {
                    Sections[segmentNum - 1].HighlightSection(UnrestrictedHighlight,RestrictedHighlight);
                    ResetAllHighlightsExcept(segmentNum - 1);
                }
            }
            else
            {
                ResetAllHighlightsExcept(-1);
            }
        }

        
    }

    //**************** Radial Input & Calculation ********************
    public virtual void Input(Vector2 input)
    {
        //if (PlayerController.Instance.GetComponent<PlayerInput>().currentControlScheme == "Keyboard&Mouse")
        //{
            
        //}
        //else
        if (IsActive)
        {
            input_axis = input;
        }
    }

    protected double DistanceFromCircleOrigin(Vector2 origin, Vector2 point)
    {
        return Math.Sqrt(Math.Pow((point.x - origin.x), 2) + Math.Pow((point.y - origin.y), 2));
    }

    protected bool IsAxisInCircle(Vector2 axis, Vector2 origin, float radius)
    {
        return DistanceFromCircleOrigin(origin, axis) < radius;
    }

    protected float AngleCheck(Vector2 boundaryOne)
    {
        return Vector2.Angle(boundaryOne, input_axis);
    }

    protected bool SegmentCheck(Vector2 startPoint, Vector2 endPoint)
    {
        if (AngleCheck(startPoint) < 45 && AngleCheck(endPoint) < 45)
        {
            return true;
        }

        return false;
    }

    protected bool IsAxisInSegment(int segment)
    {
        switch (segment)
        {
            case 1:
                return SegmentCheck(new Vector2(-0.5f, 1), new Vector2(0.5f, 1));

            case 2:
                return SegmentCheck(new Vector2(0.5f, 1), new Vector2(1, 0.5f));

            case 3:
                return SegmentCheck(new Vector2(1, 0.5f), new Vector2(1, -0.5f));

            case 4:
                return SegmentCheck(new Vector2(1, -0.5f), new Vector2(0.5f, -1));

            case 5:
                return SegmentCheck(new Vector2(0.5f, -1), new Vector2(-0.5f, -1));

            case 6:
                return SegmentCheck(new Vector2(-0.5f, -1), new Vector2(-1, -0.5f));

            case 7:
                return SegmentCheck(new Vector2(-1, -0.5f), new Vector2(-1, 0.5f));

            case 8:
                return SegmentCheck(new Vector2(-1, 0.5f), new Vector2(-0.5f, 1));

            default:
                return false;
        }
    }

    protected int CheckActiveSegment(Vector2 input)
    {
        for (int i = 1; i <= 8; i++)
        {
            if (IsAxisInSegment(i) && !IsAxisInCircle(input, Vector2.zero, InputResistance))
                return i;
        }

        return 0;
    }



    //**************** Graphical Methods *********************
    public void ResetAllHighlightsExcept(int segment)
    {
        for (int i = 0; i < Sections.Count; i++)
        {
            if (i != segment)
            {
                Sections[i].ResetHighlight();
            }
        }
    }



    //**************** Functionality Methods ****************
    public virtual void UseSectionAction()
    {

    }

    //**************** Initialisation & Close Methods *********************
    public virtual void Startup() //Need to add cooldown to activating menu
    {
        segmentNum = -1;
        IsActive = true;
        if (PlayerController.Instance.GetComponent<PlayerInput>().currentControlScheme == "Keyboard&Mouse")
        {
            MousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Debug.Log("MOUSE POSITION TRACKED - " + MousePosition.ToString());
        }

        Input(Vector2.zero);
    }

    public virtual void CloseMenu()
    {
        Input(Vector2.zero);
        IsActive = false;
        if(segmentNum >= 1)
        {
            UseSectionAction();
        }
        segmentNum = -1;
        gameObject.SetActive(false);
        //StartCoroutine(DisableAfter(0.2f));
    }

    public IEnumerator DisableAfter(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
       
    }
}
