using UnityEngine;

public class DriftingFish : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float size = 1f;
    private Vector3 target;
    private bool isStunned = false;
    private float stunDuration = 2f;

    void Start()
    {
        target = pointB.position;
        float sizeChange = Random.Range(0.5f, size);
        transform.localScale = new Vector3(sizeChange, sizeChange, sizeChange);
    }

    void Update()
    {
        if (!isStunned)
        {
            MoveBetweenPoints(Random.Range(0.1f, speed));
        }
    }

    void MoveBetweenPoints(float tmpSpeed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, tmpSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
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
