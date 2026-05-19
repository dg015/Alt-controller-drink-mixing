using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{


    public enum Ingridients
    {
        Red,
        Green,
        Blue,
        White
    }

    [SerializeField] private List <Ingridients> m_currentIngridients = new List <Ingridients> ();
    [SerializeField] private int m_numOfIngridientsPerDrink;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
