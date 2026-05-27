using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public enum Ingredients { Red, Green, Blue, White } //, Shake }
public class Manager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float minTimeToNewOrder;
    [SerializeField] private float maxTimeToNewOrder;
    [SerializeField] private float decreaseToOrderTime;
    public static Manager Instance { get; private set; }
    public static List<Ingredients> request1 { get; private set; }
    public static List<Ingredients> request2 { get; private set; }
    public static List<Ingredients> request3 { get; private set; }


    private float timer;
    private Coroutine orderRoutine;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        orderRoutine = StartCoroutine(NextOrder(maxTimeToNewOrder));
    }
    private IEnumerator NextOrder(float time)
    {
        timer = 0f;
        if (time < minTimeToNewOrder) time = UnityEngine.Random.Range(minTimeToNewOrder, maxTimeToNewOrder);

        while (timer < time)
        {

            yield return null;
        }

        List<int> availableSlots = new List<int>();
        if (request1 != null) availableSlots.Add(1);
        if (request2 != null) availableSlots.Add(2);
        if (request3 != null) availableSlots.Add(3);

        if (availableSlots.Count > 0)
        {
            int randSlot = UnityEngine.Random.Range(0, availableSlots.Count);
            int randSteps = UnityEngine.Random.Range(2, 6);
            newOrder(availableSlots[randSlot], randSteps);
        }

        // Decreases the time window before next order by diminishing amount each time.
        minTimeToNewOrder -= minTimeToNewOrder * decreaseToOrderTime;
        maxTimeToNewOrder -= maxTimeToNewOrder * decreaseToOrderTime;
        float randTime = UnityEngine.Random.Range(minTimeToNewOrder, maxTimeToNewOrder);

        orderRoutine = StartCoroutine(NextOrder(randTime));
    }
    /// <summary>
    /// Begins the process of filling a new order by determining which spot gets a new mixture
    /// </summary>
    /// <param name="slot">The slot to fill with a new order</param>
    /// <param name="steps">The number of ingredients the mixture should require</param>
    private void newOrder(int slot, int steps)
    {
        switch (slot)
        {
            case 1:
                request1 = NewMixture(steps); 
                break;
            case 2:
                request2 = NewMixture(steps);
                break;
            case 3:
                request3 = NewMixture(steps);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Creates a random list of ingreddients to make a mixture out of
    /// </summary>
    /// <param name="steps">The amount of ingredients to include in the mixture</param>
    /// <returns>The list of ingredients needed for the recipe</returns>
    public List<Ingredients> NewMixture(int steps)
    {
        List<Ingredients> mixture = new List<Ingredients>();

        for (int i = 0; i < steps; i++)
        {
            // Builds a temporary list of every Ingredient option
            List<Ingredients> options = new List<Ingredients>();
            foreach (Ingredients ing in Enum.GetValues(typeof(Ingredients))) options.Add(ing);


            //disablign this for now since we might not have shake
            // Cannot get Shake as one of first 2 options
          // if (i <= 1) options.Remove(Ingredients.Shake);




            // Cannot get same option twice in a row
            if (i > 0)
            {
                foreach (Ingredients ing in Enum.GetValues(typeof(Ingredients)))
                    if (mixture[i - 1] == ing)
                        options.Remove(ing);
            }
            // Determine a random Ingredient from the remaining list of options and add to mixture
            mixture.Add(options[UnityEngine.Random.Range(0, options.Count)]);
        }

        return mixture;
    }
    /// <summary>
    /// Call this when the player puts a cup onto a coaster.
    /// It will return true if it is the correct mixture; false if not.
    /// When returning true, empties the player's cup.
    /// </summary>
    /// <param name="slot">The coaster being delivered to. 1, 2, or 3</param>
    /// <param name="order">The Ingredient List being compared to the request</param>
    /// <returns>True if the mixture is a match to the slot; false otherwise</returns>
    public static bool OrderUp(int slot,  List<Ingredients> order)
    {
        // If the order has more or less ingredients than the request, it must be wrong, so return false
        // If any ingredient does not match the request, it must be wrong, so return false
        // If it doesn't return false, it must be correct, so return true
        switch (slot)
        {
            case 1:
                if (order.Count != request1.Count)
                {
                    return false;
                }
                for (int i = 0; i < order.Count; i++)
                {
                    if (request1[i] != order[i])
                    {
                        return false;
                    }
                }
                break;
            case 2:
                if (order.Count != request2.Count)
                {
                    return false;
                }
                for (int i = 0; i < order.Count; i++)
                {
                    if (request2[i] != order[i])
                    {
                        return false;
                    }
                }
                break;
            case 3:
                if (order.Count != request3.Count) 
                { 
                    return false; 
                }
                for (int i = 0; i < order.Count; i++)
                {
                    if (request3[i] != order[i])
                    {
                        return false;
                    }
                }
                break;
            default:
                return false;
        }

        Instance.ClearRequest(slot);

        return true;
    }
    /// <summary>
    /// Clears the specified request from the game.
    /// </summary>
    /// <param name="slot">The coaster/request slot to clear</param>
    private void ClearRequest(int slot)
    {
        switch (slot)
        {
            case 1:
                request1 = null;
                break;
            case 2: 
                request2 = null;
                break;
            case 3: 
                request3 = null;
                break;
        }
    }
}
