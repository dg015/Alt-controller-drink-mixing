using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Bottles : MonoBehaviour
{
    [Header("Type")]
    [SerializeField] public Ingredients bottleIngridient;

    [Header("Pouring")]
    [SerializeField] private float timeToPour;
    [SerializeField] private float currentPourTime;

    [Header("Bottle capacity")]
    [SerializeField] public float fillPercentage = 100;
    [SerializeField] private float maxPercentage = 100;

    [Header("Booleans")]
    [SerializeField] public bool isBeingFilled;
    [SerializeField] public bool isBeingUsed;

    [Header("References")]
    [SerializeField] private Player m_playerScript;

    [Header("RFID")]
    [SerializeField] public string RFIDTag;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fillPercentage = maxPercentage;   
    }

    private void Update()
    {
        if (isBeingUsed)
        {
            pourTimer();
        }

        fillBottle();
    }



    public void pourTimer()
    {
        currentPourTime += Time.deltaTime;

        if( currentPourTime >= timeToPour)
        {
           
            decreasePercentage(currentPourTime);
            
            m_playerScript.currentIngredients.Add(bottleIngridient);
            currentPourTime = 0;
        }
    }

    private void decreasePercentage(float ammount)
    {
        fillPercentage -= ammount;
    }

    private void fillBottle()
    {
        if (isBeingFilled && fillPercentage < maxPercentage)
        {
            fillPercentage += Time.deltaTime;
        }
    }


}
