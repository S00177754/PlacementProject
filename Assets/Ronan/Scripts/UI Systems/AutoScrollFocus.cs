﻿using System.Collections;
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

        float selectedPosition = (Content.rect.height - selectedDifference.y);
        float currentScrollRectPosition = ScrollViewRect.normalizedPosition.y * contentHeightDifference;
        float above = currentScrollRectPosition - (SelectedContent.rect.height) + RectTransform.rect.height;
        float below = currentScrollRectPosition + (SelectedContent.rect.height);

        // check if selected is out of bounds
        if (selectedPosition > above)
        {
            float step = selectedPosition - above;
            float newY = currentScrollRectPosition + step;
            float newNormalizedY = newY / contentHeightDifference;
            ScrollViewRect.normalizedPosition = new Vector2(0, newNormalizedY);
        }
        else if (selectedPosition < below)
        {
            float step = selectedPosition - below;
            float newY = currentScrollRectPosition + step;
            float newNormalizedY = newY / contentHeightDifference;
            ScrollViewRect.normalizedPosition =  new Vector2(0, newNormalizedY);
        }

    }
}
