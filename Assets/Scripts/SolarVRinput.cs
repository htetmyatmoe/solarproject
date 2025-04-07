using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class SolarVRInput : MonoBehaviour
{
   public Light sun;
   public AutoModeUI autoModeUI;
   private SolarControls controls;
   private float sunIntensity = 0f;
   private bool autoMode = false;
   private void Awake()
   {
       controls = new SolarControls();
       controls.Player.Mode.performed += ctx =>
       {
           autoMode = !autoMode;
           autoModeUI.SetAutoMode(autoMode);
           Debug.Log("Toggled Auto Mode: " + autoMode);
       };
       controls.Player.IncreaseIntensity.performed += ctx =>
       {
           if (autoMode) return;
           sunIntensity = Mathf.Min(sunIntensity + 1f, 10f);
           sun.intensity = sunIntensity;
           Debug.Log("Increased: " + sunIntensity);
       };
       controls.Player.DecreaseIntensity.performed += ctx =>
       {
           if (autoMode) return;
           sunIntensity = Mathf.Max(sunIntensity - 1f, 0f);
           sun.intensity = sunIntensity;
           Debug.Log("Decreased: " + sunIntensity);
       };
       controls.Player.Move.performed += ctx =>
       {
           Vector2 move = ctx.ReadValue<Vector2>();
           Debug.Log("Move input: " + move);
       };
   }
   private void OnEnable() => controls.Enable();
   private void OnDisable() => controls.Disable();
}