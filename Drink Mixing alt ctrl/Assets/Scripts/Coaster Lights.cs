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
    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        mr.enabled = false;
    }
    private void Update()
    {
        if (ArduinoDataReceiver.Instance.foundPort)
        {
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
                        mr.enabled = true;
                    else
                        mr.enabled = false;
                    break;
                case 2:
                    //coaster 2
                    if (ArduinoDataReceiver.Instance.coaster2Data <= luxValueTrigger)
                        mr.enabled = true;
                    else
                        mr.enabled = false;
                    break;
                case 3:
                    //coaster 3
                    if (ArduinoDataReceiver.Instance.coaster3Data <= luxValueTrigger)
                        mr.enabled = true;
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
                    if (Input.GetKey(KeyCode.Alpha1))
                        mr.enabled = true;
                    else
                        mr.enabled = false;
                    break;
                case 2:
                    //coaster 2
                    if (Input.GetKey(KeyCode.Alpha2))
                        mr.enabled = true;
                    else
                        mr.enabled = false;
                    break;
                case 3:
                    //coaster 3
                    if (Input.GetKey(KeyCode.Alpha3))
                        mr.enabled = true;
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
