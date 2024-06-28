using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Scripting.APIUpdating;

public class NewBehaviourScript : MonoBehaviour
{
    public void OnMove(InputAction.CallbackContext context)
    {
       Debug.Log(context.ReadValue<Vector2>());
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }
    public void A(InputAction.CallbackContext context)
    {
        Debug.Log(context.started);
    }

    public void OnY(InputAction.CallbackContext context)
    {
        //Debug.Log("!");
        switch (context.phase)
        {
            case InputActionPhase.Started:

                break;
            case InputActionPhase.Performed:
                if(context.interaction is PressInteraction)
                {
                    Debug.Log(context.control.IsPressed());
                }
                break;
        }
    }

    public void OnX(InputAction.CallbackContext context)
    {
        Debug.Log("!");
    }
}
