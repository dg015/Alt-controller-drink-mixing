using DG.Tweening;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float gameTimerLimit;
    [SerializeField] public bool isGameover;
    private float currentGameTimer;

    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private GameObject endGameUI;

    [Header("Sounds")]
    [SerializeField] private StudioEventEmitter EndGameSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endGameUI.SetActive(false);
        isGameover = false;
         currentGameTimer = gameTimerLimit;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isGameover)
        {
            countDownTimer();
        }
        else if( isGameover == true )
        {
            enableEndGameUI();
        }
    }

    private void countDownTimer()
    {
        //currentGameTimer = Mathf.Clamp(currentGameTimer,0,gameTimerLimit);
        currentGameTimer -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(currentGameTimer / 60);
        int seconds = Mathf.FloorToInt(currentGameTimer % 60);

        timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);

        if (currentGameTimer <= 0)
        {
            isGameover = true;
            EndGameSound.Play();
        }

    }

    private void enableEndGameUI()
    {
        endGameUI.SetActive(true);
        RectTransform recTransform = endGameUI.GetComponent<RectTransform>();
        recTransform.DOAnchorPos(Vector2.zero, 1.15f);
        //recTransform.DOMove(Vector2.zero,1.5f);


    }

}
