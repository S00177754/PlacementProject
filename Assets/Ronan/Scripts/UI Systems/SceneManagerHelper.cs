using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerHelper
{
    public static void TransitionToScene(int scene)
    {
        TravelPoint.FastTravelPoints.Clear();
        SceneManager.LoadScene(scene);
    }
}
