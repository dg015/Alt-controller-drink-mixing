using FMODUnity;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Bottles : MonoBehaviour
{
    [Header("Type")]
    [SerializeField] public Ingredients bottleIngredient;

    [Header("Pouring")]
    [SerializeField] public float timeToPour;
    [SerializeField] public float currentPourTime;

    [Header("Booleans")]
    [SerializeField] public bool isBeingUsed;

    [Header("References")]
    [SerializeField] private Player m_playerScript;

    [Header("RFID")]
    [SerializeField] public string FillingRFIDTag;
    [SerializeField] public string PouringRFIDTag;

    [Header("Sounds")]
    [SerializeField] private StudioEventEmitter pourSound;
    [SerializeField] private bool isPlaying;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //fillPercentage = maxPercentage;   
    }

    private void Update()
    {
        if (isBeingUsed)
        {
            PourTimer();
            if(!pourSound.IsPlaying())
            {
                pourSound.Play();
            }

        }
        //to make sure no over pour
        if (m_playerScript.currentIngredients.Count >= 4)
        {
            currentPourTime = 0;
        }
    }


    public void PourTimer()
    {
        currentPourTime += Time.deltaTime;

        if (currentPourTime >= timeToPour)
        {
            m_playerScript.currentIngredients.Add(bottleIngredient);

            currentPourTime = 0;
        }
    }

}
