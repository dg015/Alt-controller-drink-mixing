using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List <Ingredients> m_currentIngredients = new List <Ingredients> ();
    [SerializeField] private int m_numOfIngredientsPerDrink;


    private string previousBottleID;

    public void EmptyCup()
    {
        m_currentIngredients.Clear ();
    }

    private void addToCup()
    {
       //check if the data has been updated
        if (ArduinoDataReceiver.Instance.bottleData != previousBottleID)
        {
            //call here function to add to cup based on the ID of the tag
            //Manager.Instance.
        }

        previousBottleID = ArduinoDataReceiver.Instance.bottleData;
    }
}

