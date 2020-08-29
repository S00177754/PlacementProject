using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionController : MonoBehaviour
{
    public static SceneTransitionController Instance;

    public Animator TransitionAnim;

    private void Awake()
    {
        Instance = this;
    }

    public void FadeOut()
    {
        TransitionAnim.SetTrigger("Fade");
    }

    private void OnDestroy()
    {
        Instance = null;
    }

}
