using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public int goldAmount = 100; // Amount of gold this treasure gives

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("GrapplingHook")) // Ensure the player has the correct tag
        {
            if(GameManager.Instance != null)
                GameManager.Instance.AddGold(goldAmount);
            Debug.Log($"Treasure Collected! +{goldAmount} Gold");
            Destroy(gameObject);
            LevelManager.Instance.TakeTreasure(gameObject, gameObject.transform.parent.parent.gameObject); // Remove treasure after collection
        }
    }
}
