using UnityEngine;
using UnityEngine.UI;

public class BotttleUIUpdate : MonoBehaviour
{
    [SerializeField] private Bottles bottle;
    [SerializeField] private Image progressBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float progress = bottle.fillPercentage/ bottle.maxPercentage;
        progressBar.fillAmount = progress;
    }
}
