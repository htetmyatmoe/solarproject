using UnityEngine;
using UnityEngine.UI;
public class SolarTrackerUI : MonoBehaviour
{
   public BaseTracker tracker;                 
   public LightSourceController sourceController; 

   
   public Text outputPowerText;
   public Text intensityText;
 
   void Update()
   {
       if (tracker != null)
       {
           
           float outputPercent = tracker.outputPower;
           if (sourceController != null)
           {
               float intensityValue = sourceController.intensity;
               outputPowerText.text = "Output Power: " + outputPercent.ToString("F0") + " %";
               intensityText.text = "Intensity: " + intensityValue.ToString("F0");
           }
       }

   }
}