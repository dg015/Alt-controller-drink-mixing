using UnityEngine;
using System.IO.Ports;

public class ArduinoDataReceiver : MonoBehaviour
{
    private SerialPort serial = new SerialPort("COM4", 9600);

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
    public string bottleData;

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
        readArduinoData();
    }

    //MISSING COLOUR SENSOR
    /// <summary>
    /// ORDER of data retrieved COASTER1 -> COASTER2 -> COASTER3 -> BUTTON -> SWITCH -> RFID
    /// </summary>
    private void readArduinoData()
    {
        string data = serial.ReadExisting();

        string[] values = data.Split(",");

        if (values.Length < 3)
        {
            return;
        }
        //Debug.Log("arduino data: " + data + " - values[0]: " + values[0]);
        //Debug.Log(values[2]);
        coaster1Data = int.Parse(values[0]);
        coaster2Data = int.Parse(values[1]);
        coaster3Data = int.Parse(values[2]);

        buttonData = int.Parse(values[3]);
        tapData = int.Parse(values[4]);
        bottleData = values[5];

    }

}
