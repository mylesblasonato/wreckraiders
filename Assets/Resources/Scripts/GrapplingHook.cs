using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public float speed = 20f;
    public float maxDistance = 10f;
    public LineRenderer lineRenderer;
    public Transform origin;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isFired = false;
    private bool isRetracting = false;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isFired)
        {
            FireGrapple();
        }

        if (isFired && !isRetracting)
        {
            MoveGrapple();
        }

        if (isRetracting)
        {
            RetractGrapple();
        }

        DrawLine();
    }

    void FireGrapple()
    {
        startPosition = origin.position;
        targetPosition = startPosition + transform.forward * maxDistance;
        isFired = true;
    }

    void MoveGrapple()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isRetracting = true;
        }

        startPosition = origin.position;
    }

    void RetractGrapple()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, startPosition) < 0.1f)
        {
            ResetGrapple();
        }
    }

    void ResetGrapple()
    {
        isFired = false;
        isRetracting = false;
        transform.position = startPosition;
    }

    void DrawLine()
    {
        if (lineRenderer != null && isFired)
        {
            // Ensure the line starts from the grappling hook's firing point
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, origin.position); // Attach to the submarine or origin point
            lineRenderer.SetPosition(1, transform.position);        // The current position of the hook
        }
        else if (lineRenderer != null && !isFired)
        {
            // Hide the line when not in use
            lineRenderer.positionCount = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFired && !isRetracting)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Obstacle"))
            {
                other.SendMessage("TakeDamage", SendMessageOptions.DontRequireReceiver);
                isRetracting = true;
            }
        }
    }
}