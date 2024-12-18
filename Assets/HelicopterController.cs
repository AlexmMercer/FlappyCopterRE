using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HelicopterController : MonoBehaviour
{
    [Header("Physics Settings")]
    [SerializeField, Tooltip("Force applied when the player taps or clicks.")]
    private float liftForce = 5f;

    [SerializeField, Tooltip("Custom gravity applied to the helicopter.")]
    private float gravity = 9.8f;

    [Header("Input Settings")]
    [SerializeField, Tooltip("Key or button for triggering the lift.")]
    private KeyCode liftKey = KeyCode.Mouse0;

    private Rigidbody rb;

    private void Awake()
    {
        InitializeComponents();
    }

    private void Start()
    {
        ConfigureRigidbody();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        ApplyCustomGravity();
    }

    /// <summary>
    /// Initializes required components.
    /// </summary>
    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is missing on the Helicopter object.");
        }
    }

    /// <summary>
    /// Configures Rigidbody settings.
    /// </summary>
    private void ConfigureRigidbody()
    {
        rb.useGravity = false; // Custom gravity will be applied manually
        rb.velocity = Vector3.zero; // Start with no velocity
    }

    /// <summary>
    /// Handles player input for lift.
    /// </summary>
    private void HandleInput()
    {
        // Handle mouse or keyboard input
        if (Input.GetKeyDown(liftKey))
        {
            ApplyLift();
        }

        // Handle touch input for mobile devices
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Trigger lift on touch began
            if (touch.phase == TouchPhase.Began)
            {
                ApplyLift();
            }
        }
    }

    /// <summary>
    /// Applies custom gravity to the helicopter.
    /// </summary>
    private void ApplyCustomGravity()
    {
        rb.velocity += Vector3.down * gravity * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Applies lift force to the helicopter, resetting vertical velocity.
    /// </summary>
    private void ApplyLift()
    {
        Vector3 newVelocity = rb.velocity;
        newVelocity.y = liftForce; // Reset vertical velocity with lift force
        rb.velocity = newVelocity;
    }

    /// <summary>
    /// Public method to dynamically adjust lift force, useful for power-ups or difficulty scaling.
    /// </summary>
    /// <param name="newLiftForce">New lift force value.</param>
    public void SetLiftForce(float newLiftForce)
    {
        liftForce = Mathf.Max(0, newLiftForce); // Prevent negative lift force
    }

    /// <summary>
    /// Public method to dynamically adjust gravity, useful for difficulty scaling.
    /// </summary>
    /// <param name="newGravity">New gravity value.</param>
    public void SetGravity(float newGravity)
    {
        gravity = Mathf.Max(0, newGravity); // Prevent negative gravity
    }
}
