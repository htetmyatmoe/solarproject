using System;
using UnityEngine;

public class LightSourceController : MonoBehaviour
{

    public SunOrbit sunOrbit;
    public TrailRenderer trail;

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
    void Disable()
    {
        controls.Disable();
    }


    void Update()
    {
        if (autoMode)
        {
            sunOrbit.SetAutoMode(autoMode);
            trail.Clear();
            trail.emitting = true;
        }

        else
        {
            ManualMode();
            trail.emitting = false;
        }



        updateLightVisual();
    }



    private void ManualMode()

    {

        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed;
        rb.velocity = movement;
        Vector3 newPosition = rb.position;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
        newPosition.y = 2f;
        rb.position = newPosition;

    }
    public void updateLightVisual()
    {
        if (pointLight != null)
        {
            pointLight.intensity = intensity;
            pointLight.range = Mathf.Lerp(5f, 30f, intensity / 10f);
        }
    }

    void AdjustIntensity(float delta)
    {
        intensity = Mathf.Clamp(intensity + delta, 0f, 10f);
    }



}