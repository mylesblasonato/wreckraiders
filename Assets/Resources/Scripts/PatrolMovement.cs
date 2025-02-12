using UnityEngine;

public class PatrolMovement : MonoBehaviour
{
    public Vector3 offset;          // Offset from the starting position
    public float speed = 2f;        // Movement speed
    public int damage = 1;

    private Vector3 pointA;         // Starting point
    private Vector3 pointB;         // Ending point
    private Vector3 target;         // Current target position

    void Start()
    {
        pointA = transform.localPosition;           // Set point A to the initial local position
        pointB = pointA + offset;                   // Set point B based on the offset
        target = pointB;                           // Start moving towards point B
    }

    void Update()
    {
        // Move the object towards the current target using local position
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, speed * Time.deltaTime);

        // Check if the object has reached the target
        if (Vector3.Distance(transform.localPosition, target) < 0.1f)
        {
            // Switch the target
            target = target == pointA ? pointB : pointA;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            GameManager.Instance.UpdateLivesUI(-damage);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Draw a line between point A and point B
        Vector3 startPos = Application.isPlaying ? pointA : transform.localPosition;
        Vector3 endPos = startPos + offset;
        Gizmos.DrawLine(startPos, endPos);

        // Draw spheres at point A and point B
        Gizmos.DrawSphere(startPos, 0.1f);
        Gizmos.DrawSphere(endPos, 0.1f);
    }
}