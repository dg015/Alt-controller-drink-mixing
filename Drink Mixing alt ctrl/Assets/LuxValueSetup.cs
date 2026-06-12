using TMPro;
using UnityEngine;

public class LuxValueSetup : MonoBehaviour
{
    public static LuxValueSetup Instance { get; private set; }

    [SerializeField] private ArduinoDataReceiver arduinoData;
    [SerializeField] public float luxFactor;

    [SerializeField] private TMP_InputField textbox;

    [SerializeField] private TextMeshProUGUI luxText1;
    [SerializeField] private TextMeshProUGUI luxText2;
    [SerializeField] private TextMeshProUGUI luxText3;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Update()
    {
        if (luxText1 != null || luxText2 != null || luxText3 != null)
        luxText1.text = arduinoData.coaster1Data.ToString();
        luxText2.text = arduinoData.coaster2Data.ToString();
        luxText3.text = arduinoData.coaster3Data.ToString();

    }
    public void applyLuxValue()
    {
        if (float.TryParse(textbox.text, out float value))
        {
            luxFactor = value;
            
        }
    }
}
