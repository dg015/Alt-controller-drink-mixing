using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public List <Ingredients> currentIngredients = new List <Ingredients> ();
    [SerializeField] private int m_numOfIngredientsPerDrink;

    [Header("Bottle managing")]
    // 0 = red | 1 = green | 2 = blue | 3 = white
    [SerializeField] private List <Bottles> bottles;

    [SerializeField] private Bottles currentPourBottle;
    [SerializeField] private Bottles currentRefilBottle;
    private string previousBottleID;

    [Header("Coaster Managing")]
    [SerializeField] private float luxValueTrigger;

    [Header("Button")]
    [SerializeField] private float currentButtonHoldTime;
    [SerializeField] private float trashButtonHoldTime;
    [SerializeField] private bool isHoldingButton;
    [SerializeField] private int previousButtonState;

    private void Update()
    {
        //POURING
        //get the new RFID FOR POURING
        string currentPourBottleRFID = ArduinoDataReceiver.Instance.pouringRFIDData;
        //compare to previous RFID so it updates only if its a different bottle
        if (currentPourBottleRFID != previousBottleID)
        {
            //select the new bottle
            selectBottleByRFID(currentPourBottleRFID);
            previousBottleID = currentPourBottleRFID;
        }
        refilBottle();
        buttonManager();
    }

    //this is to select the bottle for pouring
    private void selectBottleByRFID(string RFID)
    {
        foreach (var bottle in bottles)
        {
            if (bottle.RFIDTag == RFID)
            {
                currentPourBottle = bottle;
            }
        }
    }

    /// <summary>
    /// Adds ingridient to the cup based on the RFID tag ID string
    /// TODO check if the bottle is full enough first 
    /// </summary>
    public void addToCup(Enum ingridient)
    {
        for (int i = 0; i < bottles.Count; i++)
        {
            //loop through all bottles and check if the RFID tag matches the bottle
            if (currentPourBottle.RFIDTag == bottles[i].RFIDTag)
            {
                //enable pouring of the current bottle
                bottles[i].isBeingUsed = true;
                Debug.Log("Pouring bottle: " + bottles[i].name);
            }
            else
            {
                bottles[i].isBeingUsed = false;
            }
        }
    }


    //checks the LUX value from each coaster and compares it to see which is below the treshhold and returns the number of the coaster as an int
    private int returnSelectedCoaster()
    {            
        //return coaster 1
        if (ArduinoDataReceiver.Instance.coaster1Data <= luxValueTrigger)
        {
            return 1;
        }
        //return coaster 2
        else if (ArduinoDataReceiver.Instance.coaster2Data <= luxValueTrigger)
        {
            return 2;
        }
        //return coaster 3
        else if (ArduinoDataReceiver.Instance.coaster3Data <= luxValueTrigger)
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Checks for long the button was pressed for and then decides what to do afterwards
    /// 1 - the button is held and timer starts rolling
    /// transition from 0 to 1 executes task based on how long it was held
    ///     Long hold -> trash drink
    ///     Short hold -> send drink
    /// if the previous state was 0 then do nothing so it doesnt trigger every frame
    /// </summary>
    private void buttonManager()
    {
        int currentButtonState = ArduinoDataReceiver.Instance.buttonData;
        if (currentButtonState == 1)
        {
            //check how long the button has been held for
            currentButtonHoldTime += Time.deltaTime;
        }
        //trigger   O N L Y   if it was previously held otherwise it will trigger everytime
        else if(currentButtonState == 0 && previousButtonState == 1)
        {
            //trash the drink if its been held for a while
            if (currentButtonHoldTime >= trashButtonHoldTime)
            {
                trashDrink();
                currentButtonHoldTime = 0;
            }
            //otherwise send drink
            else if (currentButtonHoldTime <= trashButtonHoldTime)
            {
                //call order up using the manager and passing the currentIngredients and returnSelectedCoaster()
                Manager.OrderUp(returnSelectedCoaster(), currentIngredients);
                currentButtonHoldTime = 0;
            }
           
        }
        previousButtonState = currentButtonState;
    }

    /// <summary>
    /// Grab the refil bottle RFID data
    /// compare the RFID tag with all the bottles and if it matches then set that bottle as refilling otherwise set as not being refilled
    /// </summary>
    private void refilBottle()
    {
        string currentRefilBottle = ArduinoDataReceiver.Instance.refilRFIDData;
        if (ArduinoDataReceiver.Instance.tapData == 1)
        {
            for (int i = 0; i < bottles.Count; i++)
            {
                //loop through all bottles and check if the RFID tag matches the bottle
                if (currentRefilBottle == bottles[i].RFIDTag)
                {
                    //enable pouring of the current bottle
                    bottles[i].isBeingFilled = true;
                }
                else
                {
                    bottles[i].isBeingFilled = false;
                }
            }
            Debug.Log("tap active");
        }
        else
        {
            Debug.Log("tap unactive");
        }
    }

    public void EmptyCup()
    {
        currentIngredients.Clear();
    }

    private void trashDrink()
    {
        //TODO check for long the player has pressed the button for

        //clear ingridients in the cup;
        currentIngredients.Clear();
    }




}

