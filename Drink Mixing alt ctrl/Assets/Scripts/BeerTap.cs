using DG.Tweening;
using FMODUnity;
using UnityEngine;
using UnityEngine.UIElements;

public class BeerTap : MonoBehaviour
{
    [Header("Type")]
    [SerializeField] private Ingredients m_tapIngridient;


    [Header("Pouring")]
    [SerializeField] public float timeToPour;
    [SerializeField] public float currentPourTime;

    [Header("Booleans")]
    [SerializeField] private bool isBeingUsed;

    [Header("References")]
    [SerializeField] private Player m_playerScript;

    [Header("Sounds")]
    [SerializeField] private StudioEventEmitter pourSound;
    [SerializeField] private bool isPlaying;

    [SerializeField] playerCupUIUpdater cupUI;
    [SerializeField] private Transform m_tapHandle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Setup();
    }

    private void Setup()
    {
        isBeingUsed = false;
        isPlaying = false;
    }


    private void ControlFill()
    {
        if (ArduinoDataReceiver.Instance.tapData == 1)
        {
            Debug.Log("Pouring");
            isBeingUsed = true;
        }
        else
        {
            isBeingUsed = false;
            Debug.Log(" Not Pouring");

        }
    }


    private void playAnimation()
    {
        if (currentPourTime >= .1f || isBeingUsed)
            m_tapHandle.DORotate(new Vector2 (- 42,0), .5f);
        else
            m_tapHandle.DORotate(new Vector2(0,0), .5f);
    }

    private void DebugPour()
    {
        if (Input.GetKey(KeyCode.T))
        {
            isBeingUsed = true;
        }
        else
        {
            isBeingUsed = false;
        }

    }

    public void PourTimer()
    {
        currentPourTime += Time.deltaTime;
        if (currentPourTime >= timeToPour)
        {
            m_playerScript.currentIngredients.Add(m_tapIngridient);

            currentPourTime = 0;
        }
    }



    // Update is called once per frame
    void Update()
    {
        playAnimation();
        ControlFill();
        DebugPour();

        if (isBeingUsed)
        {
            cupUI.UpdateBarColour(m_tapIngridient);
            cupUI.UpdateBarProgress(currentPourTime, timeToPour);
            PourTimer();
        }
        else
        {
            if (currentPourTime > 0f)
            {
                currentPourTime -= Time.deltaTime;
                cupUI.UpdateBarProgress(currentPourTime, timeToPour);
            }
            else
                currentPourTime = 0f;
        }
    }
}
