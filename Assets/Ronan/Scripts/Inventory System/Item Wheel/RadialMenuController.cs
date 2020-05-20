using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuController : MonoBehaviour
{
    [Header("Radial Elements")]
    public RadialInfoPanel InfoPanel;

    [Tooltip("Start from top segment and add in order of clockwise.")]
    public List<RadialItemSection> Sections;

    private Vector2 input_axis;

    private void Update()
    {
        if(gameObject.activeSelf)
        {
            if (input_axis != Vector2.zero)
            {
                int segmentNum = CheckActiveSegment();
                if (segmentNum != 0)
                {
                    //Debug.Log(string.Concat("Segment: ", segmentNum));
                    Sections[segmentNum - 1].HighlightSection(Color.blue);
                    ResetAllHighlightsExcept(segmentNum - 1);
                }
            }
            else
            {
                ResetAllHighlightsExcept(-1);
            }
        }
    }

    public void SetInputAxis(Vector2 input)
    {
        input_axis = input;
    }

    public void ResetAllHighlightsExcept(int segment)
    {
        for (int i = 0; i < Sections.Count; i++)
        {
            if(i != segment)
            {
                //Debug.Log(string.Concat("Unhighlight: ", segment));
                Sections[i].ResetHighlight();
            }
        }
    }

    //Math bois
    private double DistanceFromCircleOrigin(Vector2 origin, Vector2 point)
    {
        return Math.Sqrt(Math.Pow((point.x - origin.x), 2) + Math.Pow((point.y - origin.y), 2));
    }

    private bool IsAxisInCircle(Vector2 axis, Vector2 origin, float radius)
    {
        return DistanceFromCircleOrigin(origin, axis) < radius;
    }

    private float AngleCheck(Vector2 boundaryOne)
    {
        return Vector2.Angle(boundaryOne, input_axis);
    }

    private bool SegmentCheck(Vector2 startPoint, Vector2 endPoint)
    {
        if (AngleCheck(startPoint) < 45 && AngleCheck(endPoint) < 45)
        {
            return true;
        }

        return false;
    }

    private bool IsAxisInSegment(int segment)
    {
        switch (segment)
        {
            case 1:
                return SegmentCheck(new Vector2(-0.5f, 1), new Vector2(0.5f, 1));

            case 2:
                return SegmentCheck(new Vector2(0.5f, 1), new Vector2(1,0.5f));

            case 3:
                return SegmentCheck(new Vector2(1,0.5f), new Vector2(1,-0.5f));

            case 4:
                return SegmentCheck(new Vector2(1, -0.5f), new Vector2(0.5f, -1));

            case 5:
                return SegmentCheck(new Vector2(0.5f, -1), new Vector2(-0.5f, -1));

            case 6:
                return SegmentCheck(new Vector2(-0.5f, -1), new Vector2(-1,-0.5f));

            case 7:
                return SegmentCheck(new Vector2(-1, -0.5f), new Vector2(-1, 0.5f));

            case 8:
                return SegmentCheck(new Vector2(-1, 0.5f), new Vector2(-0.5f, 1));

            default:
                return false;
        }
    }

    private int CheckActiveSegment()
    {
        for (int i = 1; i <= 8; i++)
        {
            if (IsAxisInSegment(i) && !IsAxisInCircle(input_axis,Vector2.zero,1))
                return i;
        }

        return 0;
    }
}
