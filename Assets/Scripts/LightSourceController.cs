using System;
using UnityEngine;

public class LightSourceController : MonoBehaviour
{

    //AutoMode Var
    public bool autoMode = false;
    public float autoMoveSpeed = 1f;
    public float aMinX = -5f;
    public float aMaxX = 5f;
    private float autoDirection = 1f;


    //Manual Var
    public float moveSpeed = 10f;
    public float intensity = 1f;
    public float minX = -5f, maxX = 5f, minZ = -5f, maxZ = 5f;
    private Rigidbody rb;
    private Light pointLight;
    private SolarControls controls;
    private Vector2 moveInput;

   void Awake()
   {
       rb = GetComponent<Rigidbody>();
       pointLight = GetComponentInChildren<Light>();
       controls = new SolarControls();
       controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
       controls.Player.Move.canceled += _ => moveInput = Vector2.zero;
       controls.Player.IncreaseIntensity.performed += _ => AdjustIntensity(1f);
       controls.Player.DecreaseIntensity.performed += _ => AdjustIntensity(-1f);
       controls.Player.Mode.performed += _ => autoMode = !autoMode;
    }


    void OnEnable()
    {
        controls.Enable();
    }
    void Disable(){
        controls.Disable();
    }


    void Update()
    {




        if (autoMode)
        {
            Vector3 pos = transform.position;
            pos.x += autoMoveSpeed * autoDirection * Time.deltaTime;

            if (pos.x > aMaxX)
            {
                pos.x = aMaxX;
                autoDirection = -1f;
            }

            else if (pos.x < aMinX)
            {
                pos.x = aMinX;
                autoDirection = 1f;
            }

            transform.position = pos;
        }
        else
        {
            //float moveX = Input.GetAxis("Horizontal") * moveSpeed;
            //float moveZ = Input.GetAxis("Vertical") * moveSpeed;
            Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y)* moveSpeed;
            rb.velocity = movement;
            Vector3 newPosition = rb.position;
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
            newPosition.y = 2f;
            rb.position = newPosition;

            // Adjust intensity with + and -

           /* if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.Plus))
            {
                intensity += 1f;
                intensity = Mathf.Max(intensity, 0f);
            }
            if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.Underscore))
            {
                intensity -= 1f;
                intensity = Mathf.Min(intensity, 10f);
            }
            intensity = Mathf.Clamp(intensity, 0f, 10f);
*/
        }

        updateLightVisual();




    }

    public void updateLightVisual()
    {
        if (pointLight != null)
        {
            pointLight.intensity = intensity;
            pointLight.range = Mathf.Lerp(5f, 30f, intensity / 10f);
        }
    }

    void AdjustIntensity(float delta){
        intensity = Mathf.Clamp (intensity+delta, 0f , 10f);
    }


    
}