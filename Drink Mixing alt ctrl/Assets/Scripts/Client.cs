using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] public List<Ingredients> order;

    [SerializeField] private int maxWaitTime;
    [SerializeField] private int maxTipWaiTime;
    [SerializeField] private bool isTipping = true;

    [SerializeField] private float currentWaitTime;
    [SerializeField] private bool isAngry = false;
    [SerializeField] public bool hasBeenServed = false;

    //from 1 to 3
    [SerializeField] public int coaster;

    [SerializeField] private int score;
    [SerializeField] private int tip;

    private bool triggerd = false;

    public Spawner mySpawn;
    public ClientManager clientManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //WHYYYYYYYYYYYYY
        //Debug.Log(Manager.Instance.NewMixture(3));

        currentWaitTime = maxWaitTime;
        isAngry = false;
        isTipping = true;
    }

    

    // Update is called once per frame
    void Update()
    {
        //start countdown
        clientWait();
        if(hasBeenServed && !triggerd)
        {
            triggerd = true;
            clearUpSpot();
            if(isTipping)
                ScoreManager.Instance.addScore(tip);
            ScoreManager.Instance.addScore(score);
        }
    }

    private void clientWait()
    {
        currentWaitTime -= Time.deltaTime;
        if (currentWaitTime < maxTipWaiTime)
        {
            isTipping = false;
        }
        if (currentWaitTime <= 0)
        {
            Debug.Log("angry guy");
            isAngry = true;
            clearUpSpot();
        }
    }

    private void clearUpSpot()
    {
        clientManager.FreeSpawn(mySpawn);
        Destroy(gameObject, 3f);
    }


    /*
    private void pickRandomIngridient()
    {
        int number = Random.Range(1, 4);
        Ingredients chosenIngridient;
        switch (number)
        {
            case 1:
                ingridient  = 
        }
    }

    private void createOrder(int size)
    {
        for (int i = 0; i < size; i++)
        {
            order.Add( Random.RandomRange()
        }
    }
    */
}
