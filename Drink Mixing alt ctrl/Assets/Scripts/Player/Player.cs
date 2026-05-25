using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List <Ingredients> m_currentIngredients = new List <Ingredients> ();
    [SerializeField] private int m_numOfIngredientsPerDrink;

    // 0 = red | 1 = green | 2 = blue | 3 = white
    [SerializeField] private List<string> BottleIDs = new List<string> ();

    private string previousBottleID;
    [SerializeField] private Bottles bottles;

    public void EmptyCup()
    {
        m_currentIngredients.Clear ();
    }

    //kinda goofy but I will fix that later
    /// <summary>
    /// This script gets the bottle RFID id and translates it into a bottle colour.
    /// </summary>
    /// <param name="ID"> this should be the RFID stringdata</param>
    /// <returns></returns>
    private Ingredients translateIDtoBottle(string ID)
    {
        if(ID == BottleIDs[0])
        {
            //add the red drink
            return Ingredients.Red;
        }
        else if (ID == BottleIDs[1])
        {
            //add the green drink
            return Ingredients.Green;
        }
        else if (ID == BottleIDs[2])
        {
            //add the blue drink
            return Ingredients.Blue;
        }
        else if (ID == BottleIDs[3])
        {
            //add the white drink
            return Ingredients.White;
        }
        else
        {
            return Ingredients.White;
        }

    }

    /// <summary>
    /// Adds ingridient to the cup based on the RFID tag ID string
    /// TODO check if the bottle is full enough first 
    /// </summary>
    public void addToCup(Enum ingridient)
    {
       //add to the cup only if the RFID has not been updated
        if (ArduinoDataReceiver.Instance.bottleData != previousBottleID)
        {
            //get bottle type
            bottles.isBeingUsed = true;
        }
        else
        {
            bottles.isBeingUsed = false;
        }
            previousBottleID = ArduinoDataReceiver.Instance.bottleData;
    }

    private void trashDrink()
    {
        //TODO check for long the player has pressed the button for

        //clear ingridients in the cup;
        m_currentIngredients.Clear();
    }




}

