using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHazard : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.UpdateLivesUI(-damage);
    }
}
