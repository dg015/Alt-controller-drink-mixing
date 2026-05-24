using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputEvents : MonoBehaviour
{
    [SerializeField] public float clearCupDuration;
    [SerializeField] public float refillDuration;
    [SerializeField] public float pourDuration;
    [SerializeField] public float shakeDuration;

    #region Events
    // Button/Lever Actions
    public static event Action OrderUp;
    public static event Action BeginClearCup;
    public static event Action ClearCup;
    public static event Action CancelClearCup;
    public static event Action BeginPullTap;
    public static event Action PullTap;
    public static event Action CancelPullTap;

    // Coaster Actions displaying a bottle is placed there
    public static event Action BlockCoasterOne;
    public static event Action ClearCoasterOne;
    public static event Action BlockCoasterTwo;
    public static event Action ClearCoasterTwo;
    public static event Action BlockCoasterThree;
    public static event Action ClearCoasterThree;

    // RFID Actions displaying which bottle is under tap
    public static event Action RefillRed;
    public static event Action RefillGreen;
    public static event Action RefillBlue;
    public static event Action RefillWhite;

    // RFID Actions displaying which bottle is being poured into cup
    public static event Action BeginPour;
    public static event Action PourRed;
    public static event Action PourGreen;
    public static event Action PourBlue;
    public static event Action PourWhite;
    public static event Action CancelPour;

    // Accelerometer Action returning movement of the cup
    public static event Action BeginShake;
    public static event Action Shake;
    public static event Action CancelShake;

    // Input Action variables to subscribe 
    private InputAction orderUp;
    private InputAction clearCup;
    private InputAction pullTap;
    private InputAction coasterOne;
    private InputAction coasterTwo;
    private InputAction coasterThree;
    private InputAction refillRed;
    private InputAction refillGreen;
    private InputAction refillBlue;
    private InputAction refillWhite;
    private InputAction pourRed;
    private InputAction pourGreen;
    private InputAction pourBlue;
    private InputAction pourWhite;
    private InputAction shake;
    #endregion
    private void Awake()
    {
        orderUp = InputSystem.actions.FindAction("Order Up");
        clearCup = InputSystem.actions.FindAction("Clear Cup");
        pullTap = InputSystem.actions.FindAction("Pull Tap");
        coasterOne = InputSystem.actions.FindAction("Coaster One");
        coasterTwo = InputSystem.actions.FindAction("Coaster Two");
        coasterThree = InputSystem.actions.FindAction("Coaster Three");
        refillRed = InputSystem.actions.FindAction("Refill Red");
        refillGreen = InputSystem.actions.FindAction("Refill Green");
        refillBlue = InputSystem.actions.FindAction("Refill Blue");
        refillWhite = InputSystem.actions.FindAction("Refill White");
        pourRed = InputSystem.actions.FindAction("Pour Red");
        pourGreen = InputSystem.actions.FindAction("Pour Green");
        pourBlue = InputSystem.actions.FindAction("Pour Blue");
        pourWhite = InputSystem.actions.FindAction("Pour White");
        shake = InputSystem.actions.FindAction("Shake");

        orderUp.performed += OnOrderUp;

        clearCup.started += OnClearCup;
        clearCup.performed += OnClearCup;
        clearCup.canceled += OnClearCup;

        pullTap.started += OnPullTap;
        pullTap.performed += OnPullTap;

        coasterOne.performed += OnCoasterOne;
        coasterTwo.performed += OnCoasterTwo;
        coasterThree.performed += OnCoasterThree;

        refillRed.performed += OnRefillRed;
        refillGreen.performed += OnRefillGreen;
        refillBlue.performed += OnRefillBlue;
        refillWhite.performed += OnRefillWhite;

        pourRed.started += OnPourRed;
        pourRed.performed += OnPourRed;
        pourRed.canceled += OnPourRed;

        pourGreen.started += OnPourGreen;
        pourGreen.performed += OnPourGreen;
        pourGreen.canceled += OnPourGreen;

        pourBlue.started += OnPourBlue;
        pourBlue.performed += OnPourBlue;
        pourBlue.canceled += OnPourBlue;

        pourWhite.started += OnPourWhite;
        pourWhite.performed += OnPourWhite;
        pourWhite.canceled += OnPourWhite;

        shake.started += OnShake;
        shake.performed += OnShake;
        shake.canceled += OnShake;
    }
    #region Event Invoking
    void OnOrderUp(InputAction.CallbackContext ctx) => OrderUp?.Invoke();
    void OnClearCup(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            BeginClearCup?.Invoke();
        }
        else if (ctx.duration >= clearCupDuration)
        {
            ClearCup?.Invoke();
        }
        else if (ctx.canceled)
        {
            CancelClearCup?.Invoke();
        }
    }

    void OnPullTap(InputAction.CallbackContext ctx) 
    {
        if (ctx.started)
        {
            BeginPullTap?.Invoke();
        }
        else if (ctx.duration >= refillDuration)
        {
            PullTap?.Invoke();
        }
        else if (ctx.canceled)
        {
            CancelPullTap?.Invoke();
        }
    }

    void OnCoasterOne(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            BlockCoasterOne?.Invoke();
        if (ctx.canceled)
            ClearCoasterOne?.Invoke();
    }
    void OnCoasterTwo(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            BlockCoasterTwo?.Invoke();
        if (ctx.canceled)
            ClearCoasterTwo?.Invoke();
    }
    void OnCoasterThree(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            BlockCoasterThree?.Invoke();
        if (ctx.canceled)
            ClearCoasterThree?.Invoke();
    }
    void OnRefillRed(InputAction.CallbackContext ctx) => RefillRed?.Invoke();
    void OnRefillGreen(InputAction.CallbackContext ctx) => RefillGreen?.Invoke();
    void OnRefillBlue(InputAction.CallbackContext ctx) => RefillBlue?.Invoke();
    void OnRefillWhite(InputAction.CallbackContext ctx) => RefillWhite?.Invoke();
    void OnPourRed(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            BeginPour?.Invoke();
        }
        else if (ctx.duration >= pourDuration)
        {
            PourRed?.Invoke();
        }
        else if (ctx.canceled)
        {
            CancelPour?.Invoke();
        }
    }
    void OnPourGreen(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            BeginPour?.Invoke();
        }
        else if (ctx.duration >= pourDuration)
        {
            PourGreen?.Invoke();
        }
        else if (ctx.canceled)
        {
            CancelPour?.Invoke();
        }
    }
    void OnPourBlue(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            BeginPour?.Invoke();
        }
        else if (ctx.duration >= pourDuration)
        {
            PourBlue?.Invoke();
        }
        else if (ctx.canceled)
        {
            CancelPour?.Invoke();
        }
    }
    void OnPourWhite(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            BeginPour?.Invoke();
        }
        else if (ctx.duration >= pourDuration)
        {
            PourWhite?.Invoke();
        }
        else if (ctx.canceled)
        {
            CancelPour?.Invoke();
        }
    }
    void OnShake(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            BeginShake?.Invoke();
        }
        else if (ctx.duration >= pourDuration)
        {
            Shake?.Invoke();
        }
        else if (ctx.canceled)
        {
            CancelShake?.Invoke();
        }
    }
    #endregion
}
