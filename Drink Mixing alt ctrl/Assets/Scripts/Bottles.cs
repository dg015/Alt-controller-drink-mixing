using UnityEngine;

public class Bottles : MonoBehaviour
{
    [SerializeField] public float fillPercentage = 100;
    [SerializeField] private float maxPercentage = 100;
    [SerializeField] private Ingredients bottleIngridient;
    [SerializeField] public bool isBeingFilled;
    [SerializeField] public bool isBeingUsed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fillPercentage = maxPercentage;   
    }

    private void Update()
    {
        decreasePercentage();
        fillBottle();
    }

    private void decreasePercentage()
    {
        if(isBeingUsed && fillPercentage > 0)
        {
            fillPercentage -= Time.deltaTime;
        }
    }

    private void fillBottle()
    {
        if (isBeingFilled && fillPercentage < maxPercentage)
        {
            fillPercentage += Time.deltaTime;
        }
    }


}
