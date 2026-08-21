using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CoasterLights : MonoBehaviour
{
    [Range(0,3)]
    public int coaster;
    public float luxValueTrigger;
    public float trashTimer;
    public Material glowMat1;
    public Material glowMat2;

    private MeshRenderer mr;
    private float timer;

    private int currentSelectCoaster;

    [Header("cup")]
    [SerializeField] private Transform m_cupTransform;
    private Vector3 m_startingPosition;
    [SerializeField] private float m_animDuration;


    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        mr.enabled = false;
        m_startingPosition = m_cupTransform.position;
    }

    private void ResetCup()
    {
        bool shouldReset = false;

        switch (coaster)
        {
            case 1:
                shouldReset = ArduinoDataReceiver.Instance.coaster1Data > luxValueTrigger;
                break;

            case 2:
                shouldReset = ArduinoDataReceiver.Instance.coaster2Data > luxValueTrigger;
                break;

            case 3:
                shouldReset = ArduinoDataReceiver.Instance.coaster3Data > luxValueTrigger;
                break;
        }

        if (shouldReset)
        {
            m_cupTransform.DOKill();
            m_cupTransform.DOMove(m_startingPosition, m_animDuration);
        }

    }

    private void ResetCupDebug()
    {
        bool shouldReset = false;

        switch (coaster)
        {
            case 1:
                shouldReset = Input.GetKeyUp(KeyCode.Alpha1);
                break;

            case 2:
                shouldReset = Input.GetKeyUp(KeyCode.Alpha2);
                break;

            case 3:
                shouldReset = Input.GetKeyUp(KeyCode.Alpha3);
                break;
        }

        if (shouldReset)
        {
            m_cupTransform.DOKill();
            m_cupTransform.DOMove(m_startingPosition, m_animDuration);
        }
    }

    private void Update()
    {
        ResetCupDebug();
        if (ArduinoDataReceiver.Instance.foundPort)
        {
            ResetCup();
            switch (coaster)
            {
                // Player is pressing the send/trash drink button
                case 0:
                    if (ArduinoDataReceiver.Instance.buttonData == 0)
                    {
                        mr.enabled = true;
                        timer += Time.deltaTime;
                        if (timer >= trashTimer)
                            mr.material = glowMat2;
                        else
                            mr.material = glowMat1;
                    }
                    else
                    {
                        timer = 0f;
                        mr.material = glowMat1;
                        mr.enabled = false;
                    }
                    break;
                case 1:
                    //coaster 1
                    if (ArduinoDataReceiver.Instance.coaster1Data <= luxValueTrigger)
                    {
                        m_cupTransform.DOKill();
                        mr.enabled = true;
                        m_cupTransform.DOMove(mr.transform.position, m_animDuration);
                    }

                    else
                        mr.enabled = false;
                    break;
                case 2:
                    //coaster 2
                    if (ArduinoDataReceiver.Instance.coaster2Data <= luxValueTrigger)
                    {
                        m_cupTransform.DOKill();
                        mr.enabled = true;
                        m_cupTransform.DOMove(mr.transform.position, m_animDuration);
                    }

                    else
                        mr.enabled = false;
                    break;
                case 3:
                    //coaster 3
                    if (ArduinoDataReceiver.Instance.coaster3Data <= luxValueTrigger)
                    {
                        m_cupTransform.DOKill();
                        mr.enabled = true;
                        m_cupTransform.DOMove(mr.transform.position, m_animDuration);
                    }
                    else
                        mr.enabled = false;
                    break;
                default:
                    mr.enabled = false;
                    break;
            }
        }
        // For debugging or when playing without arduino
        else
        {
            switch (coaster)
            {
                // Player is pressing the send/trash drink button
                case 0:
                    if (Input.GetKey(KeyCode.Space))
                    {
                        mr.enabled = true;
                        timer += Time.deltaTime;
                        if (timer >= trashTimer)
                        {
                            m_cupTransform.DOKill();
                            m_cupTransform.DOMove(m_startingPosition, m_animDuration);
                            mr.material = glowMat2;
                        }

                        else
                            mr.material = glowMat1;
                    }
                    else
                    {
                        timer = 0f;
                        mr.material = glowMat1;
                        mr.enabled = false;
                    }
                    break;
                case 1:
                    //coaster 1
                    if (Input.GetKey(KeyCode.Alpha1))
                    {
                        m_cupTransform.DOKill();
                        mr.enabled = true;
                        m_cupTransform.DOMove(mr.transform.position, m_animDuration);
                    }

                    else
                        mr.enabled = false;
                    break;
                case 2:
                    //coaster 2
                    if (Input.GetKey(KeyCode.Alpha2))
                    {
                        m_cupTransform.DOKill();
                        mr.enabled = true;
                        m_cupTransform.DOMove(mr.transform.position, m_animDuration);
                    }

                    else
                        mr.enabled = false;
                    break;
                case 3:
                    //coaster 3
                    if (Input.GetKey(KeyCode.Alpha3))
                    {
                        m_cupTransform.DOKill();
                        mr.enabled = true;
                        m_cupTransform.DOMove(mr.transform.position, m_animDuration);
                    }

                    else 
                        mr.enabled = false;
                        break;
                default:
                    mr.enabled = false;
                    break;
            }
        }
    }
}
