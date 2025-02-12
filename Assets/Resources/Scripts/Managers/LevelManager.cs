using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform listOfTreasure;

    public List<GameObject> treasureList;
    public static LevelManager Instance { get; private set; } // Singleton Instance

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        SetupTreasure();
    }

    private void SetupTreasure()
    {
        treasureList = new List<GameObject>();

        foreach (Transform go in listOfTreasure)
        {
            treasureList.Add(go.gameObject);
        }
    }

    public void AllTreasureFound(GameObject level)
    {
        if (treasureList.Count == 0)
            Destroy(level);

        GameObject.FindGameObjectWithTag("X").SetActive(false);
    }

    public void TakeTreasure(GameObject treasure, GameObject level)
    {
        Destroy(treasure);
        treasureList.Remove(treasure);
        AllTreasureFound(level);
    }
}
