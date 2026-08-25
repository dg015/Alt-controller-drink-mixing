using DG.Tweening;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] public List<Ingredients> order;


    [Header("wait time")]
    [SerializeField] public float maxWaitTime;

    [SerializeField] private float maxTipWaitTime;

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
    [SerializeField] private SpriteRenderer m_mainSprite;
    [SerializeField] private float m_animDuration;
    [SerializeField] private bool m_isReady = false;

    private bool triggerd = false;


    [Header("Refernces")]
    public Spawner mySpawn;
    public ClientManager clientManager;
    private Coroutine clearingSpot;

    [SerializeField] private GameObject m_badParticleEffect;
    [SerializeField] private GameObject m_goodParticleEffect;

    [Header("Sounds")]
    [SerializeField] private StudioEventEmitter m_happySound;
    [SerializeField] private StudioEventEmitter m_disapointedSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        order = Manager.Instance.NewMixture(4);

        currentWaitTime = maxWaitTime;
        isAngry = false;
        isTipping = true;

        Sprite randomSprite = clientSprites[Random.Range(0, clientSprites.Count)];
        //gameObject.GetComponent<SpriteRenderer>().sprite = randomSprite;
        m_mainSprite.sprite = randomSprite;

        SpawnAnimation(m_animDuration);
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
        if (currentWaitTime > 0 && (!hasBeenServed && !isAngry))
            currentWaitTime -= Time.deltaTime;

        if (currentWaitTime < maxTipWaitTime)
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

       //SpriteRenderer sr = GetComponent<SpriteRenderer>(); 

        Color originColor = m_mainSprite.color;

        while (timer < despawnTime)
        {
            if (isAngry)
                m_mainSprite.color = Color.Lerp(originColor, Color.red, timer / despawnTime);

            else
                m_mainSprite.color = Color.Lerp(originColor, Color.cyan, timer / despawnTime);
            //mr.material = newMat;

            timer += Time.deltaTime;
            yield return null; 
        }
        clientManager.FreeSpawn(mySpawn);

        InstatiateParticleEffect();

        DespawnAnimation(m_animDuration);
    }


    private void InstatiateParticleEffect()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, -3);

        if (isAngry)
        {
            m_disapointedSound.Play();
            Instantiate(m_badParticleEffect, pos, m_badParticleEffect.transform.rotation);
        }
        else
        {
            m_happySound.Play();
            Instantiate(m_goodParticleEffect, pos, m_goodParticleEffect.transform.rotation);
        }
    }

    private void DespawnAnimation(float animDuration)
    {
        m_mainSprite.DOColor(Color.black, animDuration).OnComplete(() =>
        {
            m_mainSprite.DOFade(0, animDuration / 3).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        });


    }

    private void SpawnAnimation(float animDuration)
    {
        m_mainSprite.DOColor(Color.black, 0f);
        m_mainSprite.DOFade(0f, 0f);

        m_mainSprite.DOFade(1, animDuration / 3).OnComplete(() =>
        {
            m_mainSprite.DOColor(Color.white, animDuration / 3).OnComplete(() =>
            {
                m_isReady = true;
            });
        });
    }

}
