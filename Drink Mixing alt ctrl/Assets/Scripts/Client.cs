using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] private List<Ingredients> desiredIngridients = null;
    
    [SerializeField] private int maxWaitTime;
    [SerializeField] private int maxTipWaiTime;
    [SerializeField] private bool isTipping = true;

    [SerializeField] private float currentWaitTime;
    [SerializeField] private bool isAngry = false;
    [SerializeField] private bool hasBeenServed = false;

    //from 1 to 3
    [SerializeField] private int coaster;


    //public Spawner mySpawn;
   // public ClientManager clientManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set wait time   
        currentWaitTime = maxWaitTime;
        isAngry = false;
        isTipping = true;
    }

    // Update is called once per frame
    void Update()
    {
        //start countdown
        clientWait();
    }

    private void clientWait()
    {
        currentWaitTime -= Time.deltaTime;
        if( currentWaitTime > maxTipWaiTime)
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
        //clientManager.FreeSpawn(mySpawn);
        Destroy(gameObject, 3f);
    }

    private void serveClient()
    {
        clearUpSpot();
    }


}
