using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerCupUIUpdater : MonoBehaviour
{
    [SerializeField] private Player playerScript;
    [SerializeField] private List<Image> UIImages;


    [SerializeField] private Image fillBar;

    [SerializeField] private Color lightGray;

    // Update is called once per frame
    void Update()
    {
        UpdateUi();
    }

    public void UpdateBarColour(Ingredients currentIngridient)
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
        else if (currentIngridient == Ingredients.Tap)
        {
            //brown
            fillBar.color = new Color(0.59f, 0.29f, 0.0f);
        }
    }

    public void UpdateBarProgress(float currentPourTime, float timeToPour)
    {
        fillBar.fillAmount = currentPourTime / timeToPour;

    }

    private void UpdateUi()
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
                else if (playerScript.currentIngredients[i] == Ingredients.Tap)
                {
                    UIImages[i].color = new Color(0.59f, 0.29f, 0.0f);
                }
            }
        }
    }
}
