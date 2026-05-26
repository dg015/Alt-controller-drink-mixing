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
        if(ArduinoDataReceiver.Instance.coaster1Data <= luxValueTrigger)
        {
            return 1;
            //return coaster 1
        }
        else if (ArduinoDataReceiver.Instance.coaster2Data <= luxValueTrigger)
        {
            return 2;
            //return coaster 2
        }
        else if(ArduinoDataReceiver.Instance.coaster3Data <= luxValueTrigger)
        {
            return 3;
            //return coaster 3
        }
        else
        {
            return 0;
        }
    }

    //Call manager for send order but check if it was not a long press (long press will trash the drink)
    private void sendOrder()
    {
        //TODO add logic to check if it was not a long press
        if(ArduinoDataReceiver.Instance.buttonData == 1)
        {
            //call order up using the manager and passing the currentIngredients and returnSelectedCoaster()
            Manager.OrderUp(returnSelectedCoaster(), currentIngredients);
        }


    }

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
                Debug.Log("refilling bottle: " + bottles[i].name);
            }
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

