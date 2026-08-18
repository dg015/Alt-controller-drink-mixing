using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] public List<Ingredients> order;


    [Header("wait time")]
    [SerializeField] public float maxWaitTime;
    [SerializeField] private float maxTipWaiTime;
    [SerializeField] public float currentWaitTime;

    [SerializeField] private float despawnTime;


    [Header ("States")]
    [SerializeField] private bool isAngry = false;
    [SerializeField] public bool hasBeenServed = false;

    //from 1 to 3
    [SerializeField][Range(1,3)] public int coaster;

    [SerializeField] private int score;

    [Header("Tipping")]
    [SerializeField] private bool isTipping = true;
    [SerializeField] private int tip;


    [Header("Sprite")]
    [SerializeField] private List <Sprite> clientSprites;

    [Header("Animation")]
    [SerializeField] private SpriteRenderer m_shadowSprite;
    [SerializeField] private float m_animDuration;
    [SerializeField] private bool m_isReady = false;

    private bool triggerd = false;


    [Header("Refernces")]
    public Spawner mySpawn;
    public ClientManager clientManager;
    private Coroutine clearingSpot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnAnimation(m_animDuration);

        order = Manager.Instance.NewMixture(4);

        currentWaitTime = maxWaitTime;
        isAngry = false;
        isTipping = true;

        Sprite randomSprite = clientSprites[Random.Range(0, clientSprites.Count)];
        gameObject.GetComponent<SpriteRenderer>().sprite = randomSprite;
        m_shadowSprite.sprite = randomSprite;
    }

    

    // Update is called once per frame
    void Update()
    {
        //start countdown
        if (!m_isReady)
            return;

        ClientWait();
        if(hasBeenServed && !triggerd)
        {
            triggerd = true;
            ClearUpSpot();
            if(isTipping)
                ScoreManager.Instance.AddScore(tip);
            ScoreManager.Instance.AddScore(score);
        }
    }

    private void ClientWait()
    {
        currentWaitTime -= Time.deltaTime;
        if (currentWaitTime < maxTipWaiTime)
        {
            isTipping = false;
        }
        if (currentWaitTime <= 0)
        {
            Debug.Log("angry guy");
            isAngry = true;
            ClearUpSpot();
        }
    }

    private void ClearUpSpot()
    {
        clearingSpot ??= StartCoroutine(ClearingSpot());
    }
    private IEnumerator ClearingSpot()
    {
        float timer = 0f;

        SpriteRenderer sr = GetComponent<SpriteRenderer>(); 

        Color originColor = sr.color;

        while (timer < despawnTime)
        {
            if (isAngry)
                sr.color = Color.Lerp(originColor, Color.red, timer / despawnTime);
            else
                sr.color = Color.Lerp(originColor, Color.cyan, timer / despawnTime);
            //mr.material = newMat;

            timer += Time.deltaTime;
            yield return null; 
        }
        clientManager.FreeSpawn(mySpawn);
        Destroy(gameObject);
    }

    private void SpawnAnimation(float animDuration)
    {
        SpriteRenderer renderer = transform.GetComponent<SpriteRenderer>();

        renderer.DOFade(1, animDuration / 3).OnComplete(() =>
        {
            m_shadowSprite.DOFade(0, animDuration).OnComplete(() =>
            {
                m_isReady = true;
            });
        });
    }

}
