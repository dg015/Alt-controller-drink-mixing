using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CupInputManager : MonoBehaviour
{
    public UnityEvent onRedBottlePour, onBlueBottlePour, onGreenBottlePour, onWhiteBottlePour, onStartShake, onStopShake;

    public void OnRedBottlePour(InputAction.CallbackContext ct)
    {
        if (ct.phase == InputActionPhase.Started)
        {
            print("red bottle pour");
            onRedBottlePour?.Invoke();
        }
    }
    public void OnBlueBottlePour(InputAction.CallbackContext ct)
    {
        if (ct.phase == InputActionPhase.Started)
        {
            print("blue bottle pour");
            onBlueBottlePour?.Invoke();
        }
    }
    public void OnGreenBottlePour(InputAction.CallbackContext ct)
    {
        if (ct.phase == InputActionPhase.Started)
        {
            print("green bottle pour");

            onGreenBottlePour?.Invoke();
        }
    }
    public void OnWhiteBottlePour(InputAction.CallbackContext ct)
    {
        if (ct.phase == InputActionPhase.Started)
        {
            print("white bottle pour");

            onWhiteBottlePour?.Invoke();
        }
    }

    public void OnShake(InputAction.CallbackContext ct)
    {
        if (ct.phase == InputActionPhase.Started)
        {
            print("start shake");
            onStartShake?.Invoke();
        }
        else if (ct.phase == InputActionPhase.Canceled)
        {
            print("stop shake");
            onStopShake?.Invoke();
        }
    }

}
