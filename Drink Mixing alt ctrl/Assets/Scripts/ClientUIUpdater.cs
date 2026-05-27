using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientUIUpdater : MonoBehaviour
{
    [SerializeField] private Client client;
    [SerializeField] private List<Image> UIImages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateUi();
    }

    private void updateUi()
    {
        for (int i  = 0; i < client.order.Count;i++)
        {
            if(client.order[i] == Ingredients.Red)
            {
                UIImages[i].color = Color.red;
            }
            else if (client.order[i] == Ingredients.Green)
            {
                UIImages[i].color = Color.green;
            }
            else if (client.order[i] == Ingredients.Blue)
            {
                UIImages[i].color = Color.blue;
            }
            else if (client.order[i] == Ingredients.White)
            {
                UIImages[i].color = Color.white;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
