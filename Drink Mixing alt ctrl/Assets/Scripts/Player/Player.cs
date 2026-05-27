using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public List <Ingredients> currentIngredients = new List <Ingredients> ();
    [SerializeField] private int m_numOfIngredientsPerDrink;

    [Header("Bottle managing")]
    // 0 = red | 1 = green | 2 = blue | 3 = white
    [SerializeField] private List <Bottles> bottles;

    //[SerializeField] private Bottles currentPourBottle;
    //[SerializeField] private Bottles currentRefilBottle;
    private string previousBottleID;

    [Header("Coaster Managing")]
    [SerializeField] private float luxValueTrigger;

    [Header("Button")]
    [SerializeField] private float currentButtonHoldTime;
    [SerializeField] private float trashButtonHoldTime;
    [SerializeField] private bool isHoldingButton;
    [SerializeField] private int previousButtonState;

    [SerializeField] private Manager manager;
    [SerializeField] private ClientManager clientManager;

    private void Start()
    {
        
    }

    private void Update()
    {
        //POURING
        //get the new RFID FOR POURING
        string currentPourBottleRFID = ArduinoDataReceiver.Instance.pouringRFIDData;
        //compare to previous RFID so it updates only if its a different bottle
        /*
        if (currentPourBottleRFID != previousBottleID)
        {
            //select the new bottle
            selectBottleByRFID(currentPourBottleRFID);
            previousBottleID = currentPourBottleRFID;
        }
        */
        pourBottle();
        refilBottle();
        buttonManager();


        //debug methods
        debugAddToCup();
    }

    /*
    //this is to select the bottle for pouring
    private void selectBottleByRFID(string RFID)
    {
        foreach (var bottle in bottles)
        {
            if (bottle.PouringRFIDTag == RFID)
            {
                currentPourBottle = bottle;
            }
        }
    }

    */

    /*
    /// <summary>
    /// Adds ingridient to the cup based on the RFID tag ID string
    /// TODO check if the bottle is full enough first 
    /// </summary>
    public void addToCup(Enum ingridient)
    {
        for (int i = 0; i < bottles.Count; i++)
        {
            //loop through all bottles and check if the RFID tag matches the bottle
            if (currentPourBottle.PouringRFIDTag == bottles[i].PouringRFIDTag)
            {
                //enable pouring of the current bottle
                bottles[i].isBeingUsed = true;
                Debug.Log("Pouring bottle: " + bottles[i].name);
            }
            else
            {
                bottles[i].isBeingUsed = false;
            }
        }
    }
    */



    //checks the LUX value from each coaster and compares it to see which is below the treshhold and returns the number of the coaster as an int
    private int returnSelectedCoaster()
    {            
        //return coaster 1
        if (ArduinoDataReceiver.Instance.coaster1Data <= luxValueTrigger)
        {
            return 1;
        }
        //return coaster 2
        else if (ArduinoDataReceiver.Instance.coaster2Data <= luxValueTrigger)
        {
            return 2;
        }
        //return coaster 3
        else if (ArduinoDataReceiver.Instance.coaster3Data <= luxValueTrigger)
        {
            return 3;
        }
        else // return 0 since there are no coaster with the id 0
        {
            return 0;
        }
    }

    /// <summary>
    /// Checks for long the button was pressed for and then decides what to do afterwards
    /// 1 - the button is held and timer starts rolling
    /// transition from 0 to 1 executes task based on how long it was held
    ///     Long hold -> trash drink
    ///     Short hold -> send drink
    /// if the previous state was 0 then do nothing so it doesnt trigger every frame
    /// </summary>
    private void buttonManager()
    {
        int currentButtonState = ArduinoDataReceiver.Instance.buttonData;
        if (currentButtonState == 0)
        {
            //check how long the button has been held for
            currentButtonHoldTime += Time.deltaTime;
        }
        //trigger   O N L Y   if it was previously held otherwise it will trigger everytime
        else if(currentButtonState == 1 && previousButtonState == 0)
        {
            //trash the drink if its been held for a while
            if (currentButtonHoldTime >= trashButtonHoldTime)
            {
                Debug.Log("trash drink");
                trashDrink();
                currentButtonHoldTime = 0;
            }
            //otherwise send drink
            else if (currentButtonHoldTime <= trashButtonHoldTime)
            {
                Debug.Log("send drink");
                //call order up using the manager and passing the currentIngredients and returnSelectedCoaster()
                //Manager.OrderUp(returnSelectedCoaster(), currentIngredients);
                checkClientRecipe();
                currentButtonHoldTime = 0;
            }
        }
        //reset stat
        previousButtonState = currentButtonState;
    }


    //check clients recipe
    private void checkClientRecipe()
    {
        //get the current client list
        List <Client> clientList = clientManager.currentClients;

        //get which coaster is selected
        returnSelectedCoaster();

        //run through the list
        for (int i  = 0; i < clientList.Count; i++)
        {
            //Debug.Log(clientList[i].coaster);
            //check which client contains the matchin coaster
            if(returnSelectedCoaster() == clientList[i].coaster)
            {
                //Debug.Log(clientList[i].order);
                //compare the list of the client with the matching coaster
                if(compareLists(currentIngredients, clientList[i].order) == true)
                {
                    //if it matches set client as served
                    clientList[i].hasBeenServed = true;
                }

            }
        }
    }

    private bool compareLists(List<Ingredients> playerOrder, List<Ingredients> clientOrder)
    {
        //check if same size
        if (playerOrder.Count != clientOrder.Count)
        {
            return false;
        }

        //check items by items
        for (int i = 0; i < playerOrder.Count; i++)
        {
            if (playerOrder[i] != clientOrder[i])
            {
                return false;
            }   
        }
        return true;
    }

    /// <summary>
    /// Grab the refil bottle RFID data
    /// compare the RFID tag with all the bottles and if it matches then set that bottle as refilling otherwise set as not being refilled
    /// </summary>
    private void refilBottle()
    {
        string currentRefilBottle = ArduinoDataReceiver.Instance.refilRFIDData;
        if (ArduinoDataReceiver.Instance.tapData == 1)
        {
            for (int i = 0; i < bottles.Count; i++)
            {
                //loop through all bottles and check if the RFID tag matches the bottle
                if (currentRefilBottle == bottles[i].FillingRFIDTag)
                {
                    //enable pouring of the current bottle
                    bottles[i].isBeingFilled = true;
                }
                else
                {
                    bottles[i].isBeingFilled = false;
                }
            }
        }
    }

    private void pourBottle()
    {
        string currentPourBottle = ArduinoDataReceiver.Instance.refilRFIDData;
        for (int i = 0; i < bottles.Count; i++)
        {
            //loop through all bottles and check if the RFID tag matches the bottle
            if (currentPourBottle == bottles[i].PouringRFIDTag)
            {
                //enable pouring of the current bottle
                bottles[i].isBeingUsed = true;
            }
            else
            {
                bottles[i].isBeingUsed = false;
            }
        }
    }

    private void trashDrink()
    {
        currentIngredients.Clear();
    }


    private void debugAddToCup()
    {
        if (Input.GetKey(KeyCode.R))
        {
            //currentIngredients.Add(Ingredients.Red);
            bottles[0].isBeingUsed = true;
        }
        else if (Input.GetKey(KeyCode.G))
        {
            //currentIngredients.Add(Ingredients.Green);
            bottles[1].isBeingUsed = true;
        }
        else if (Input.GetKey(KeyCode.B))
        {
            //currentIngredients.Add(Ingredients.Blue);
            bottles[2].isBeingUsed = true;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            //currentIngredients.Add(Ingredients.White);
            bottles[3].isBeingUsed = true;
        }
        else
        {
            for (int i = 0; i < bottles.Count; i++)
            {
                bottles[i].isBeingUsed = false;
            }

        }
    }


}

