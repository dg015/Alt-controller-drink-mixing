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
    [Header("RFID")]
    //top of the bottle
    public string pouringRFIDData;
    //bottom of the bottle
    public string refilRFIDData;

    //to ignore the first few junk frames
    private bool isInitialized = false;
    private float initTimer = 0f;
    [SerializeField] private float initTime = 2f;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        serial.Open();
        serial.ReadTimeout = 100;
        buttonData = 3;
    }

    // Update is called once per frame
    void Update()
    {
        //little warmup for the arduino so it ignores the junk content
        initTimer += Time.deltaTime;

        //little timer
        if (!isInitialized && initTimer < initTime)
        {
            return; 
        }

        if (!isInitialized)
        {
            isInitialized = true;
            Debug.Log("Arduino warmup complete");
        }
        readArduinoData();
    }

    /// <summary>
    /// ORDER of data retrieved COASTER1 -> COASTER2 -> COASTER3 -> BUTTON -> SWITCH -> RFID
    /// </summary>
    private void readArduinoData()
    {
        try
        {
            //chek if serial is open
            if (!serial.IsOpen) return;

            //check if theres content to read
            if (serial.BytesToRead == 0) return;

            //read content
            string data = serial.ReadLine();

            //separate it based on ","
            string[] values = data.Split(",");

            //check if its receiving all the data
            if (values.Length < 6) return;

            //parse variables into desired format
            float.TryParse(values[0], out coaster1Data);
            float.TryParse(values[1], out coaster2Data);
            float.TryParse(values[2], out coaster3Data);

            int.TryParse(values[3], out buttonData);
            int.TryParse(values[4], out tapData);

            refilRFIDData = values[5];

        }
        //if timeout just timeout
        catch (System.TimeoutException)
        {
        }
        //if it throws in an exception write it to log
        catch (System.Exception e)
        {
            Debug.LogWarning("serial error :" + e.Message);
        }
    }
    //close serial door on close to stop it from future errors
    private void OnApplicationQuit()
    {
        serial.Close();
    }
}
