using UnityEngine;

public class PatrollingShark : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 3f;
    private int currentPointIndex = 0;
    private bool isStunned = false;
    private float stunDuration = 3f;

    void Update()
    {
        if (!isStunned)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, patrolPoints[currentPointIndex].position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    public void Stun()
    {
        if (!isStunned)
        {
            isStunned = true;
            Invoke(nameof(Recover), stunDuration);
        }
    }

    void Recover()
    {
        isStunned = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrapplingHook"))
        {
            Stun();
        }
    }
}
