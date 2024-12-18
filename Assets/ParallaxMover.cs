using UnityEngine;

public class ParallaxMover : MonoBehaviour
{
    [SerializeField, Tooltip("Speed of the parallax layer.")]
    private float parallaxSpeed = 3f;

    [SerializeField, Tooltip("Width of the parallax segment for repositioning.")]
    private float segmentWidth = 20f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveParallax();
        RepositionParallaxIfNeeded();
    }

    private void MoveParallax()
    {
        transform.Translate(Vector3.left * parallaxSpeed * Time.deltaTime, Space.World);
    }

    private void RepositionParallaxIfNeeded()
    {
        if (transform.position.x <= -segmentWidth)
        {
            transform.position += Vector3.right * segmentWidth * 2f;
        }
    }
}
