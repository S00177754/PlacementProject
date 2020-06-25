using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

static public class UIHelper
{
    static public void SelectedObjectSet(GameObject defaultSelectedUI)
    {
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(defaultSelectedUI);
        Debug.Log(defaultSelectedUI);
    }
}
