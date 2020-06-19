using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDebug : MonoBehaviour
{
    public void PressAndRelease(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                Debug.Log("Performed");
                break;

            case InputActionPhase.Started:
                Debug.Log("Started");
                break;

            case InputActionPhase.Canceled:
                Debug.Log("Cancelled");
                break;

            default:
                print(context.phase);
                break;
        }
    }
}
