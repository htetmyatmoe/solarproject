using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
public class UserMovement : MonoBehaviour
{
   public InputActionProperty rightStickAction;   // Right thumbstick input
   public InputActionProperty leftGripAction;     // Left grip button input
   public Transform headset;                      // Main camera (XR Rig head)
   public float moveSpeed = 2.0f;
   public float rotationSpeed = 60f;
   private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent <Rigidbody> ();
    }
    private void OnEnable()
   {
       rightStickAction.action.Enable();
       leftGripAction.action.Enable();
   }
   private void OnDisable()
   {
       rightStickAction.action.Disable();
       leftGripAction.action.Disable();
   }
   void Update()
   {
       Vector2 input = rightStickAction.action.ReadValue<Vector2>();
       float gripValue = leftGripAction.action.ReadValue<float>();
       // --- Calculate forward and right direction relative to headset
       Vector3 forward = new Vector3(headset.forward.x, 0f, headset.forward.z).normalized;
       Vector3 right = new Vector3(headset.right.x, 0f, headset.right.z).normalized;
       // --- Movement: combines forward/backward and left/right input
       Vector3 moveDirection = forward * input.y + right * input.x;
       transform.position += moveDirection * moveSpeed * Time.deltaTime;
       rb.MovePosition(rb.position+ moveDirection * Time.fixedDeltaTime);
       //Smooth Rotation if Left Grip is held 
       if (gripValue > 0.5f && Mathf.Abs(input.x) > 0.1f)
       {
           float yaw = input.x * rotationSpeed * Time.deltaTime;
           transform.Rotate(0f, yaw, 0f);
       }
   }
}