using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class SolarVRInput : MonoBehaviour
{
   public Light sun;
   private float sunIntensity = 0f;
   private bool autoMode = false;
   void Update()
   {
       var gamepad = Gamepad.current;
       if (gamepad == null) return;
       // Manual Mode Controls
       if (!autoMode)
       {
           if (gamepad.buttonWest.wasPressedThisFrame) // X
           {
               sunIntensity = Mathf.Max(sunIntensity - 1f, 0f);
               sun.intensity = sunIntensity;
               Debug.Log("Decrease Intensity");
           }
           if (gamepad.buttonNorth.wasPressedThisFrame) // Y
           {
               sunIntensity = Mathf.Min(sunIntensity + 1f, 10f);
               sun.intensity = sunIntensity;
               Debug.Log("Increase Intensity");
           }
       }
       // Toggle Auto Mode
       if (gamepad.buttonSouth.wasPressedThisFrame) // A
       {
           autoMode = !autoMode;
           Debug.Log("Auto Mode: " + (autoMode ? "ON" : "OFF"));
       }
       // Auto Mode Behavior
       if (autoMode)
       {
           sunIntensity = Mathf.PingPong(Time.time * 2f, 10f);
           sun.intensity = sunIntensity;
       }
   }
}