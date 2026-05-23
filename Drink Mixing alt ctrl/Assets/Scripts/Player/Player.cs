using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    [SerializeField] private List <Ingredients> m_currentIngredients = new List <Ingredients> ();
    [SerializeField] private int m_numOfIngredientsPerDrink;




    public void EmptyCup()
    {
        m_currentIngredients.Clear ();
    }
}
