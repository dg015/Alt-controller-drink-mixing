using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
public class playerCupUIUpdater : MonoBehaviour
{
    [SerializeField] private Player playerScript;
    [SerializeField] private List<Image> UIImages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateUi();
    }
    private void updateUi()
    {
        for (int i = 0; i < playerScript.currentIngredients.Count; i++)
        {
            if (playerScript.currentIngredients[i] == Ingredients.Red)
            {
                UIImages[i].color = Color.red;
            }
            else if (playerScript.currentIngredients[i] == Ingredients.Green)
            {
                UIImages[i].color = Color.green;
            }
            else if (playerScript.currentIngredients[i] == Ingredients.Blue)
            {
                UIImages[i].color = Color.blue;
            }
            else if (playerScript.currentIngredients[i] == Ingredients.White)
            {
                UIImages[i].color = Color.white;
            }
        }
    }
}
