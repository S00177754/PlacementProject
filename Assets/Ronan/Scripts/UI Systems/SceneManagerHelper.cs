using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerHelper
{
    public static void TransitionToScene(int scene)
    {
        //if(SceneTransitionController.Instance != null)
        //{
        //    SceneTransitionController.Instance.FadeOut();
        //}

        TravelPoint.FastTravelPoints.Clear();
        DialogueManager.ChattyNPCs.Clear();
        SceneManager.LoadScene(scene);
    }
}
