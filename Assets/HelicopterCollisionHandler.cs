using UnityEngine;

public class HelicopterCollisionHandler : MonoBehaviour
{
    [Header("Tags for collision detection")]
    [SerializeField, Tooltip("Tag used for pass zone.")]
    private string passZoneTag = "PassZone";

    public delegate void CollisionEvent();
    public static event CollisionEvent OnCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(passZoneTag))
        {
            Debug.Log("Collision detected!");
            OnCollision?.Invoke();
        }
    }
}
