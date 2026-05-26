using UnityEngine;
using System.IO.Ports;

public class ArduinoDataReceiver : MonoBehaviour
{
    private SerialPort serial = new SerialPort("COM5", 115200);

    public static ArduinoDataReceiver Instance;

    [Header("Coasters")]
    public float coaster1Data;
    public float coaster2Data;
    public float coaster3Data;
    [Header("Tap")]
    public int tapData;
    [Header("Button")]
    public int buttonData;
    [Header("RFID bottle")]
    public string pouringRFIDData;
    public string refilRFIDData;



    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        serial.Open();
        serial.ReadTimeout = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("running");
        readArduinoData();
    }

    //MISSING COLOUR SENSOR
    /// <summary>
    /// ORDER of data retrieved COASTER1 -> COASTER2 -> COASTER3 -> BUTTON -> SWITCH -> RFID
    /// </summary>
    private void readArduinoData()
    {

        try
        {
            if (!serial.IsOpen) return;
            if (serial.BytesToRead == 0) return;

            string data = serial.ReadLine();

            string[] values = data.Split(",");

            if (values.Length < 6) return;

            float.TryParse(values[0], out coaster1Data);
            float.TryParse(values[1], out coaster2Data);
            float.TryParse(values[2], out coaster3Data);

            int.TryParse(values[3], out buttonData);
            int.TryParse(values[4], out tapData);

            refilRFIDData = values[5];


           //Debug.Log(coaster1Data);

        }
        catch (System.TimeoutException)
        {
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("serial error :" + e.Message);
        }


    }
    private void OnApplicationQuit()
    {
        serial.Close();
    }
}
