using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AutoScrollFocus : MonoBehaviour
{
    public ScrollRect ScrollViewRect;
    public RectTransform RectTransform;
    public RectTransform Content;
    public RectTransform SelectedContent;
    public float scrollSpeed = 1f;

    private void Awake()
    {
        ScrollViewRect = GetComponent<ScrollRect>();
        RectTransform = GetComponent<RectTransform>();
        Content = ScrollViewRect.content;
    }

    private void Update()
    {
        ScrollToSelected();
    }

    public void ScrollToSelected()
    {
        //Getting selected item
        GameObject selected = EventSystem.current.currentSelectedGameObject;

        if (selected == null)
            return;

        if (selected.transform.parent != Content.transform)
            return;

        SelectedContent = selected.GetComponent<RectTransform>();

        //Calculations
        Vector3 selectedDifference = RectTransform.localPosition - SelectedContent.localPosition;
        float contentHeightDifference = (Content.rect.height - RectTransform.rect.height);
        float contentWidthDifference = (Content.rect.width - RectTransform.rect.width);

        float selectedPosition = (Content.rect.height - selectedDifference.y);
        float currentScrollRectPosition = ScrollViewRect.normalizedPosition.y * contentHeightDifference;
        float above = currentScrollRectPosition - (SelectedContent.rect.height) + RectTransform.rect.height;
        float below = currentScrollRectPosition + (SelectedContent.rect.height);

        float selectedHorizontalPosition = (Content.rect.width - selectedDifference.x);
        float currentHorizontalScrollRectPosition = ScrollViewRect.normalizedPosition.x * contentWidthDifference;
        float left = currentHorizontalScrollRectPosition - (SelectedContent.rect.width) + RectTransform.rect.width;
        float right = currentHorizontalScrollRectPosition + (SelectedContent.rect.width);

        // check if selected is out of bounds
        float step = 0;
        float stepX = 0;

        if (selectedPosition > above)
        {
            step = selectedPosition - above;
        }
        else if (selectedPosition < below)
        {
            step = selectedPosition - below;
        }

        //if (selectedHorizontalPosition > left)
        //{
        //    stepX = selectedHorizontalPosition + left;
        //}
        //else if (selectedHorizontalPosition < right)
        //{
        //    stepX = selectedHorizontalPosition + right;
        //}

            float newY = currentScrollRectPosition + step;
            float newX = currentHorizontalScrollRectPosition + stepX;

            float newNormalizedY = newY / contentHeightDifference;
            //float newNormalizedX = newX / contentWidthDifference;
            ScrollViewRect.normalizedPosition =  new Vector2(0, newNormalizedY);

    }
}
