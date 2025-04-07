using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoModeUI : MonoBehaviour
{
   public LightSourceController sun;

   public GameObject batteryUI;
   public GameObject StatusUI;
   public GameObject AutoModeLabel;

   public void SetAutoMode(bool isOn){

    Debug.Log ("SetAutoMode called: " + isOn);
    if(batteryUI != null)
    {batteryUI.SetActive(!isOn);}
    if(StatusUI != null){
      StatusUI.SetActive(!isOn);
    }

    if (AutoModeLabel != null){
      AutoModeLabel.SetActive(isOn);
    }
    
   }
}
