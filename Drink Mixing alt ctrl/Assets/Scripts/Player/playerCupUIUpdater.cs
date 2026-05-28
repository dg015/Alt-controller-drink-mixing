using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
public class playerCupUIUpdater : MonoBehaviour
{
    [SerializeField] private Player playerScript;
    [SerializeField] private List<Image> UIImages;


    [SerializeField] private Image fillBar;

    [SerializeField] private Color lightGray;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateUi();
    }

    public void updateBarColour(Ingredients currentIngridient)
    {
        if (currentIngridient == Ingredients.Red)
        {
            fillBar.color = Color.red;
        }
        else if (currentIngridient == Ingredients.Green)
        {
            fillBar.color = Color.green;
        }
        else if (currentIngridient == Ingredients.Blue)
        {
            fillBar.color = Color.blue;
        }
        else if (currentIngridient == Ingredients.White)
        {
            fillBar.color = Color.white;
        }
    }

    public void updateBarProgress(float currentPourTime, float timeToPour)
    {
        fillBar.fillAmount = currentPourTime / timeToPour;

    }

    private void updateUi()
    {
        if (playerScript.currentIngredients.Count == 0)
        {
            //Debug.Log("empty list");
            for (int i = 0; i < UIImages.Count; i++)
            {
                UIImages[i].color = lightGray;
            }
        }
        else
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
}
