using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Bottles : MonoBehaviour
{
    [Header("Type")]
    [SerializeField] public Ingredients bottleIngridient;

    [Header("Pouring")]
    [SerializeField] public float timeToPour;
    [SerializeField] public float currentPourTime;
    [SerializeField] private float fillReduceBonus;

    [Header("Bottle capacity")]
    [SerializeField] public float fillPercentage = 100;
    [SerializeField] public float maxPercentage = 100;
    [SerializeField] private int fillSpeed;

    [Header("Booleans")]
    [SerializeField] public bool isBeingFilled;
    [SerializeField] public bool isBeingUsed;

    [Header("References")]
    [SerializeField] private Player m_playerScript;

    [Header("RFID")]
    [SerializeField] public string FillingRFIDTag;
    [SerializeField] public string PouringRFIDTag;


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
        //to make sure no over pour
        if (m_playerScript.currentIngredients.Count >= 4)
        {
            currentPourTime = 0;
        }
        fillBottle();
    }


    public void pourTimer()
    {
        currentPourTime += Time.deltaTime;

        if( currentPourTime >= timeToPour)
        {
            decreasePercentage(currentPourTime + fillReduceBonus);
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
            fillPercentage = fillPercentage += Time.deltaTime * fillSpeed;
        }
    }


}
