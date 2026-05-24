using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputEvents : MonoBehaviour
{
    public static InputEvents Instance;
    public bool CoasterOne { get; private set; }
    public bool CoasterTwo { get; private set; }
    public bool CoasterThree { get; private set; }
    public Ingredients PouringBottle { get; private set; }
    public Ingredients RefillingBottle { get; private set; }

    #region Events
    // Button/Lever Actions
    public static event Action OrderUp;
    public static event Action BeginClearCup;
    public static event Action CancelClearCup;
    public static event Action BeginPullTap;
    public static event Action CancelPullTap;

    // RFID Actions displaying which bottle is being poured into cup
    public static event Action BeginPour;
    public static event Action CancelPour;

    // Accelerometer Action returning movement of the cup
    public static event Action BeginShake;
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
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);


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
        clearCup.canceled += OnClearCup;

        pullTap.started += OnPullTap;
        pullTap.canceled += OnPullTap;

        coasterOne.started += OnCoasterOne;
        coasterTwo.started += OnCoasterTwo;
        coasterThree.started += OnCoasterThree;
        coasterOne.canceled += OnCoasterOne;
        coasterTwo.canceled += OnCoasterTwo;
        coasterThree.canceled += OnCoasterThree;

        refillRed.started += OnRefillRed;
        refillGreen.started += OnRefillGreen;
        refillBlue.started += OnRefillBlue;
        refillWhite.started += OnRefillWhite;

        pourRed.started += OnPourRed;
        pourRed.canceled += OnPourRed;

        pourGreen.started += OnPourGreen;
        pourGreen.canceled += OnPourGreen;

        pourBlue.started += OnPourBlue;
        pourBlue.canceled += OnPourBlue;

        pourWhite.started += OnPourWhite;
        pourWhite.canceled += OnPourWhite;

        shake.started += OnShake;
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
        else if (ctx.canceled)
        {
            CancelPullTap?.Invoke();
        }
    }
    void OnCoasterOne(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            CoasterOne = true;
        if (ctx.canceled)
            CoasterOne = false;
    }
    void OnCoasterTwo(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            CoasterTwo = true;
        if (ctx.canceled)
            CoasterTwo = false;
    }
    void OnCoasterThree(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            CoasterThree = true;
        if (ctx.canceled)
            CoasterThree = false;
    }
    
    void OnRefillRed(InputAction.CallbackContext ctx)
    {
        RefillingBottle = Ingredients.Red;
    }
    void OnRefillGreen(InputAction.CallbackContext ctx)
    {
        RefillingBottle = Ingredients.Green;
    }
    void OnRefillBlue(InputAction.CallbackContext ctx)
    {
        RefillingBottle = Ingredients.Blue;
    }
    void OnRefillWhite(InputAction.CallbackContext ctx)
    {
        RefillingBottle = Ingredients.White;
    }
    void OnPourRed(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            PouringBottle = Ingredients.Red;
            BeginPour?.Invoke();
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
            PouringBottle = Ingredients.Green;
            BeginPour?.Invoke();
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
            PouringBottle = Ingredients.Blue;
            BeginPour?.Invoke();
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
            PouringBottle = Ingredients.White;
            BeginPour?.Invoke();
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
        else if (ctx.canceled)
        {
            CancelShake?.Invoke();
        }
    }
    #endregion
}
