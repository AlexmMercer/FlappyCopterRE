using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField, Tooltip("Base speed of the obstacles.")]
    private float baseSpeed = 5f;

    [SerializeField, Tooltip("Speed increment over time.")]
    private float speedIncreasePerSecond = 0.1f;

    [SerializeField, Tooltip("Left boundary for obstacle deactivation.")]
    private float leftBoundary = -10f;

    private float currentSpeed;

    private void OnEnable()
    {
        // Reset speed to base speed when the obstacle is activated
        currentSpeed = baseSpeed;
    }

    private void Update()
    {
        MoveObstacle();
        CheckBounds();
        UpdateSpeed();
    }

    /// <summary>
    /// Moves the obstacle to the left based on the current speed.
    /// </summary>
    private void MoveObstacle()
    {
        transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Checks if the obstacle has moved out of the screen and deactivates it.
    /// </summary>
    private void CheckBounds()
    {
        if (transform.position.x <= leftBoundary)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Increases the movement speed over time to ramp up difficulty.
    /// </summary>
    private void UpdateSpeed()
    {
        currentSpeed += speedIncreasePerSecond * Time.deltaTime;
    }
}
