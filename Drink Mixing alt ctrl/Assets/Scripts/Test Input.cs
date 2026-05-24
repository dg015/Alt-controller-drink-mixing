using System;
using System.Net;
using UnityEngine;

public class TestInput : MonoBehaviour
{

    private void Awake()
    {
        InputEvents.OrderUp += OrderUp;
        InputEvents.BeginClearCup += BeginClearCup;
        InputEvents.CancelClearCup += CancelClearCup;
        InputEvents.BeginPullTap += BeginPullTap;
        InputEvents.CancelPullTap += CancelPullTap;

        InputEvents.BeginPour += BeginPour;
        
        InputEvents.CancelPour += CancelPour;

        InputEvents.BeginShake += BeginShake;
        InputEvents.CancelShake += CancelShake;
    }

    private void OrderUp() 
    {
        // No coaster options are true
        if (!InputEvents.Instance.CoasterOne && !InputEvents.Instance.CoasterTwo && !InputEvents.Instance.CoasterThree)
            Debug.Log("No coaster selected!");
        
        // Only coaster 1 is true
        else if (InputEvents.Instance.CoasterOne && !InputEvents.Instance.CoasterTwo && !InputEvents.Instance.CoasterThree)
            Debug.Log("OrderUp - Triggered! \n" + "Coaster 1 Order Up!");
        
        // Only coaster 2 is true
        else if (!InputEvents.Instance.CoasterOne && InputEvents.Instance.CoasterTwo && !InputEvents.Instance.CoasterThree)
            Debug.Log("OrderUp - Triggered! \n" + "Coaster 2 Order Up!");
        
        // Only coaster 3 is true
        else if (!InputEvents.Instance.CoasterOne && !InputEvents.Instance.CoasterTwo && InputEvents.Instance.CoasterThree)
            Debug.Log("OrderUp - Triggered! \n" + "Coaster 3 Order Up!");
        
        // More than a single coaster is true
        else
            Debug.Log("Too many coasters covered!");
    }
    private void BeginClearCup() 
    {
        Debug.Log("BeginClearCup - Triggered!");
    }
    private void CancelClearCup() 
    {
        Debug.Log("CancelClearCup - Triggered!");
    }
    private void BeginPullTap() 
    {
        Debug.Log("BeginPullTap - Triggered! \n" + InputEvents.Instance.RefillingBottle + " Bottle Refilling...");
    }
    private void CancelPullTap() 
    {
        Debug.Log("CancelPullTap - Triggered!");
    }
    private void BeginPour() 
    {
        Debug.Log("BeginPour - Triggered! \n"+InputEvents.Instance.PouringBottle+" Bottle Pouring...");
    }
    private void CancelPour() 
    {
        Debug.Log("CancelPour - Triggered!");
    }
    private void BeginShake() 
    {
        Debug.Log("BeginShake - Triggered!");
    }
    private void Shake() 
    {
        Debug.Log("Shake - Triggered!");
    }
    private void CancelShake() 
    {
        Debug.Log("CancelShake - Triggered!");
    }
}
