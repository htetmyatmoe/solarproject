using System.Timers;
using UnityEngine;
public class BaseTracker : MonoBehaviour
{
   public Transform sun;
   public float rotationSpeed = 1f;
   public float yOffset = 0f;
   public float outputPower;

   public Transform panelForward;
   void Update()
   {
      if (sun == null || panelForward == null) return;
      Vector3 direction = sun.position - transform.position;
      direction.y = 0;
      if (direction.sqrMagnitude > 0.001f)
      {
         Quaternion targetRotation = Quaternion.LookRotation(direction);
         transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

         LightSourceController lightSource = sun.GetComponent<LightSourceController>();
         if (lightSource != null)
         {
            if (lightSource.autoMode)
            {
               float  sunX = sun.position.x;
               float disFromCenter = Mathf.Abs(sunX);
               float mappedInternsity = Mathf.Clamp01(1f - (disFromCenter / 5f));

               lightSource.intensity = mappedInternsity * 10f;
               lightSource.updateLightVisual();

               outputPower = mappedInternsity * 100f;

               Light sunlight = sun.GetComponentInChildren<Light> ();
               if (sunlight != null) {
                  float blend = 1f - (Mathf.Abs(sun.position.x)/5f);
                  Color morning = new Color (1f, 0.5f ,0.2f); ///orange
                  Color afternoon = new Color (0.8f, 0.9f, 1f); ////BlueTinty

                  sunlight.color = Color.Lerp(morning,afternoon,blend);

               }


            }
            else
            {
               float intensity = lightSource.intensity;
               outputPower = Mathf.Clamp01(intensity / 10f) * 100f;
            }

         }

      }
   }


 
}