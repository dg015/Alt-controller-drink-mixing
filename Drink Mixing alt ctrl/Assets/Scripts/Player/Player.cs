using FMODUnity;
using System.Collections.Generic;
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


    [SerializeField] playerCupUIUpdater cupUI;

    [Header("beer tap")]
    private bool isFilling;

    [Header("Sounds")]
    [SerializeField] private StudioEventEmitter bellSound;
    [SerializeField] private StudioEventEmitter trashSound;
    [SerializeField] private StudioEventEmitter fillSound;
    private void Update()
    {
        PourBottle();


        //refilBottle();
        RefillForVideo();

        ButtonManager();


        //debug methods
        DebugAddToCup();
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
    private int ReturnSelectedCoaster()
    {
        if (ArduinoDataReceiver.Instance.foundPort)
        {
            // Returns -1 if no coasters are pressed
            if (!(ArduinoDataReceiver.Instance.coaster1Data <= luxValueTrigger) && !(ArduinoDataReceiver.Instance.coaster2Data <= luxValueTrigger) && !(ArduinoDataReceiver.Instance.coaster3Data <= luxValueTrigger))
            {
                return - 1;
            }
            //return coaster 1
            else if ((ArduinoDataReceiver.Instance.coaster1Data <= luxValueTrigger) && !(ArduinoDataReceiver.Instance.coaster2Data <= luxValueTrigger) && !(ArduinoDataReceiver.Instance.coaster3Data <= luxValueTrigger))
            {
                return 1;
            }
            //return coaster 2
            else if ((ArduinoDataReceiver.Instance.coaster2Data <= luxValueTrigger) && !(ArduinoDataReceiver.Instance.coaster1Data <= luxValueTrigger) && !(ArduinoDataReceiver.Instance.coaster3Data <= luxValueTrigger))
            {
                return 2;
            }
            //return coaster 3
            else if ((ArduinoDataReceiver.Instance.coaster3Data <= luxValueTrigger) && !(ArduinoDataReceiver.Instance.coaster1Data <= luxValueTrigger) && !(ArduinoDataReceiver.Instance.coaster2Data <= luxValueTrigger))
            {
                return 3;
            }
            // return 0 if multiple coasters are pressed since there are no coaster with the id 0
            else
            {
                return 0;
            } 
        }
        // For debugging or when playing without arduino
        else
        {
            if (!Input.GetKey(KeyCode.Alpha1) && !Input.GetKey(KeyCode.Alpha2) && !Input.GetKey(KeyCode.Alpha3)) return -1;
            else if (Input.GetKey(KeyCode.Alpha1) && !Input.GetKey(KeyCode.Alpha2) && !Input.GetKey(KeyCode.Alpha3)) return 1;
            else if (Input.GetKey(KeyCode.Alpha2) && !Input.GetKey(KeyCode.Alpha1) && !Input.GetKey(KeyCode.Alpha3)) return 2;
            else if (Input.GetKey(KeyCode.Alpha3) && !Input.GetKey(KeyCode.Alpha1) && !Input.GetKey(KeyCode.Alpha2)) return 3;
            else return 0;
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
    private void ButtonManager()
    {
        int currentButtonState = ArduinoDataReceiver.Instance.buttonData;

        // For debugging purposes, use spacebar to simulate bell button
        if (!ArduinoDataReceiver.Instance.foundPort)
        {
            if (Input.GetKey(KeyCode.Space))
                currentButtonState = 0;
            else
                currentButtonState = 1;
        }

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
                trashSound.Play();
                currentIngredients.Clear();
            }
            //otherwise send drink
            else if (currentButtonHoldTime <= trashButtonHoldTime)
            {
                if (ReturnSelectedCoaster() > 0)
                {
                    bellSound.Play();
                    Debug.Log("send drink");
                    //call order up using the manager and passing the currentIngredients and returnSelectedCoaster()
                    //Manager.OrderUp(returnSelectedCoaster(), currentIngredients);
                    CheckClientRecipe();
                    currentIngredients.Clear();
                }
                else if (ReturnSelectedCoaster() == -1)
                {
                    Debug.Log("No Coaster Selected!");
                }
                else
                {
                    Debug.Log("Too Many Coasters Selected!");
                }
            }
            currentButtonHoldTime = 0;
        }
        //reset stat
        previousButtonState = currentButtonState;
    }


    //check clients recipe
    private void CheckClientRecipe()
    {
        //get the current client list
        List <Client> clientList = clientManager.currentClients;

        //get which coaster is selected
        if (ReturnSelectedCoaster() > 0)
        {
            //run through the list
            for (int i = 0; i < clientList.Count; i++)
            {
                //Debug.Log(clientList[i].coaster);
                //check which client contains the matchin coaster
                if (ReturnSelectedCoaster() == clientList[i].coaster)
                {
                    //Debug.Log(clientList[i].order);
                    //compare the list of the client with the matching coaster
                    if (CompareLists(currentIngredients, clientList[i].order) == true)
                    {
                        //if it matches set client as served
                        clientList[i].hasBeenServed = true;
                    }

                }
            }
        }
    }

    private bool CompareLists(List<Ingredients> playerOrder, List<Ingredients> clientOrder)
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
    private void RefillBottle()
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

    private void RefillForVideo()
    {
        bool tapData = ArduinoDataReceiver.Instance.tapData == 1;
        if (!tapData && !isFilling)
        {
            isFilling = true;
            fillSound.Play();
        }
        else if(tapData && isFilling)
        {
            fillSound.Stop();
            isFilling =false;
        }
        for (int i = 0; i < bottles.Count; i++)
        {
            bottles[i].isBeingFilled = tapData;
        }
    }

    private void PourBottle()
    {
        string currentPourBottle = ArduinoDataReceiver.Instance.pouringRFIDData;
        //Debug.Log(currentPourBottle);

        for (int i = 0; i < bottles.Count; i++)
        {
            //loop through all bottles and check if the RFID tag matches the bottle
            if (currentPourBottle == bottles[i].PouringRFIDTag )
            {
                Debug.Log(currentPourBottle);
                //enable pouring of the current bottle
                bottles[i].isBeingUsed = true;
                cupUI.UpdateBarColour(bottles[i].bottleIngredient);
                cupUI.UpdateBarProgress(bottles[i].currentPourTime, bottles[i].timeToPour);

            }
            else if (currentPourBottle == "NONE")
            {
                bottles[i].isBeingUsed = false;

                if (bottles[i].currentPourTime > 0f)
                {
                    bottles[i].currentPourTime -= 0.5f * Time.deltaTime;
                    cupUI.UpdateBarProgress(bottles[i].currentPourTime, bottles[i].timeToPour);
                }
                else
                    bottles[i].currentPourTime = 0f;
            }
        }

    }

    private void DebugAddToCup()
    {
        if (Input.GetKey(KeyCode.R))
        {
            //currentIngredients.Add(Ingredients.Red);
            bottles[0].isBeingUsed = true;
            cupUI.UpdateBarColour(bottles[0].bottleIngredient);
            cupUI.UpdateBarProgress(bottles[0].currentPourTime, bottles[0].timeToPour);
        }
        else if (Input.GetKey(KeyCode.G))
        {
            //currentIngredients.Add(Ingredients.Green);
            bottles[1].isBeingUsed = true;
            cupUI.UpdateBarColour(bottles[1].bottleIngredient);
            cupUI.UpdateBarProgress(bottles[1].currentPourTime, bottles[1].timeToPour);
        }
        else if (Input.GetKey(KeyCode.B))
        {
            //currentIngredients.Add(Ingredients.Blue);
            bottles[2].isBeingUsed = true;
            cupUI.UpdateBarColour(bottles[2].bottleIngredient);
            cupUI.UpdateBarProgress(bottles[2].currentPourTime, bottles[2].timeToPour);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            //currentIngredients.Add(Ingredients.White);
            bottles[3].isBeingUsed = true;
            cupUI.UpdateBarColour(bottles[3].bottleIngredient);
            cupUI.UpdateBarProgress(bottles[3].currentPourTime, bottles[3].timeToPour);
        }
        else
        {
            if (!ArduinoDataReceiver.Instance.foundPort)
            {
                foreach (Bottles bot in bottles)
                {
                    bot.isBeingUsed = false;
                    if (bot.currentPourTime > 0f)
                    {
                        bot.currentPourTime -= Time.deltaTime;
                        cupUI.UpdateBarProgress(bot.currentPourTime, bot.timeToPour);
                    }
                    else
                        bot.currentPourTime = 0f;
                }
            }
        }
    }


}

