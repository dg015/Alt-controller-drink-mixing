using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;


public class ClientManager : MonoBehaviour
{
    [SerializeField] private List<Client> currentClients;

    [SerializeField] private float clientSpawnTime;

    [SerializeField] private GameObject clientPrefab;

    [SerializeField] private List<Spawner> spawnPoints;

    [SerializeField] private float cooldown;
    [SerializeField] private float runTime;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    private Spawner GetFreeSpawn()
    {
        foreach (var sp in spawnPoints)
        {
            if (!sp.isOccupied)
                return sp;
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        runTime += Time.deltaTime;
        if (runTime >= cooldown)
        {
            spawnClient();
        }
    }

    public void FreeSpawn(Spawner spawn)
    {
        spawn.isOccupied = false;
    }


    private void spawnClient()
    {
        if(currentClients.Count < 3)
        {
            Spawner spawn = GetFreeSpawn();

            runTime = 0;
            //rn using 2 justfot the sake of something
            GameObject newCLient = Instantiate(clientPrefab, spawn.point.position, quaternion.identity);

          

            Client clientScript = newCLient.GetComponent<Client>();

            //clientScript.mySpawn = spawn;
            //clientScript = this;

            currentClients.Add(newCLient.GetComponent<Client>());
            spawn.isOccupied = true;
        }


    }

}
