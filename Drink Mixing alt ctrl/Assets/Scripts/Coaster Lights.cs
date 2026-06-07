using System;
using UnityEngine;

public class CoasterLights : MonoBehaviour
{
    [Range(1,3)]
    public int coaster;
    public float luxValueTrigger;
    private MeshRenderer mr;
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
