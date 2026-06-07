using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] public List<Ingredients> order;

    [SerializeField] public float maxWaitTime;
    [SerializeField] private float maxTipWaiTime;
    [SerializeField] private bool isTipping = true;
    [SerializeField] private float despawnTime;
    [SerializeField] public float currentWaitTime;
    [SerializeField] private bool isAngry = false;
    [SerializeField] public bool hasBeenServed = false;

    //from 1 to 3
    [SerializeField][Range(1,3)] public int coaster;

    [SerializeField] private int score;
    [SerializeField] private int tip;

    private bool triggerd = false;

    public Spawner mySpawn;
    public ClientManager clientManager;
    private Coroutine clearingSpot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        order = Manager.Instance.NewMixture(4);

        currentWaitTime = maxWaitTime;
        isAngry = false;
        isTipping = true;
    }

    

    // Update is called once per frame
    void Update()
    {
        //start countdown
        ClientWait();
        if(hasBeenServed && !triggerd)
        {
            triggerd = true;
            ClearUpSpot();
            if(isTipping)
                ScoreManager.Instance.AddScore(tip);
            ScoreManager.Instance.AddScore(score);
        }
    }

    private void ClientWait()
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
            ClearUpSpot();
        }
    }

    private void ClearUpSpot()
    {
        clearingSpot ??= StartCoroutine(ClearingSpot());
    }
    private IEnumerator ClearingSpot()
    {
        float timer = 0f;
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material newMat = mr.material;
        Color originColor = mr.material.color;

        while (timer < despawnTime)
        {
            if (isAngry)
                newMat.color = Color.Lerp(originColor, Color.red, timer / despawnTime);
            else
                newMat.color = Color.Lerp(originColor, Color.cyan, timer / despawnTime);
            mr.material = newMat;

            timer += Time.deltaTime;
            yield return null; 
        }
        clientManager.FreeSpawn(mySpawn);
        Destroy(gameObject);
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
