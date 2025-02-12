using UnityEngine;

public class CoralBarricade : MonoBehaviour
{
    public int durability = 3; // Number of hits before breaking
    public GameObject debrisEffect;

    public void TakeDamage()
    {
        durability--;

        if (durability <= 0)
        {
            Break();
        }
    }

    void Break()
    {
        if (debrisEffect != null)
        {
            Instantiate(debrisEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrapplingHook"))
        {
            TakeDamage();
        }
    }
}