using UnityEngine;

using UnityEngine.UI;

public class BatteryManager : MonoBehaviour

{

    public Slider batterySlider;

    public Text batteryText;
    public Text batteryStatus;
    public Image batteryFillImage;

    public BaseTracker tracker;

    public float batteryLevel = 0f;

    public float maxChargeRate = 10f;
    public float dischargeRate = 2f;

    void Update()

    {

        if (batterySlider == null || tracker == null || batteryText == null) return;

        float output = tracker.outputPower;


        string statusMessage = " ";
        Color barColor = Color.white;
        Color stsColor = Color.black;


        

        if (output > 0f){
            float actualChargeRate = (output /100f )* maxChargeRate;
            batteryLevel += actualChargeRate * Time.deltaTime;
            statusMessage = "Charging Rate: + " + actualChargeRate.ToString("F0");
            barColor = Color.yellow;
        }
        else {
            batteryLevel -= dischargeRate * Time.deltaTime;
            statusMessage = "Discharging Rate: -"+ dischargeRate.ToString("F0");
            barColor = Color.red;
        }

        if(batteryLevel <= 0f) {
            statusMessage = "No Battery";
            stsColor = Color.red;
            
        }
        else if (batteryLevel >= 100f){
            statusMessage = "Fully Charged";
            barColor = Color.green;
            stsColor = Color.green;
            
        }

        

        batteryLevel = Mathf.Clamp(batteryLevel, 0f, 100f);
        batterySlider.value = batteryLevel;
        batteryText.text = "Battery: " + batteryLevel.ToString("F0") + "%";
        batteryStatus.text = statusMessage;
        batteryStatus.color = stsColor;
        batteryFillImage.color = barColor;

    }

} 