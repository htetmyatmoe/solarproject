using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoModeUI : MonoBehaviour
{
   public LightSourceController sun;

   public GameObject batteryUI;
   public GameObject StatusUI;

   public void SetAutoMode(bool isOn){
    sun.autoMode = isOn;
    Debug.Log ("Toggle : " + isOn);

    if(batteryUI != null) batteryUI.SetActive(!isOn);
    if(StatusUI !=null) StatusUI.SetActive(!isOn);

   }
}
