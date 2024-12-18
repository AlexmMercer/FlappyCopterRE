using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GroundMover : MonoBehaviour
{
    [SerializeField, Tooltip("Speed at which the ground moves.")]
    private float speed = 5f;

    private float segmentWidth;
    private Vector3 startPosition;

    private void Start()
    {
        // ?????????? ?????? ???????? ?? BoxCollider
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        segmentWidth = boxCollider.size.x * transform.localScale.x;

        startPosition = transform.position;
    }

    private void Update()
    {
        MoveGround();
        RepositionGroundIfNeeded();
    }

    private void MoveGround()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }

    private void RepositionGroundIfNeeded()
    {
        if (transform.position.x <= -segmentWidth)
        {
            transform.position += Vector3.right * segmentWidth * 2f;
        }
    }
}
