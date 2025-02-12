
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton Instance

    [Header("Map Settings")]
    public GameObject redXMarker; // Assign your red X GameObject in Unity
    public TextMeshProUGUI lifeLabel; // Show the lives
    public TextMeshProUGUI goldLabel; // Show the gold
    public Transform waterPlane; // Assign your water plane (ocean surface)
    public Vector2 mapBoundsMin = new Vector2(-50, -50); // Adjust bounds
    public Vector2 mapBoundsMax = new Vector2(50, 50);
    public int playerGold = 100; // Example currency system
    public int mapCost = 100;
    public int lives = 3;
    public ParticleSystem hit;
    public GameObject[] maps; // Array of map objects
    private GameObject previousMap;

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
        redXMarker.transform.GetChild(0).gameObject.SetActive(false);
        UpdateGoldUI();
        UpdateLivesUI(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            PurchaseMap();
        }
    }

    public void PurchaseMap()
    {
        if (playerGold >= mapCost)
        {
            redXMarker.transform.GetChild(0).gameObject.SetActive(true);
            playerGold -= mapCost;
            MoveRedX();
            Debug.Log($"Map Purchased! Gold Remaining: {playerGold}");
        }
        else
        {
            Debug.Log("Not enough gold to buy a map!");
        }
        UpdateGoldUI();
    }

    private void MoveRedX()
    {
        if (previousMap != null)
        {
            Destroy(previousMap);
        }

        float randomX = Random.Range(mapBoundsMin.x, mapBoundsMax.x);
        float randomZ = Random.Range(mapBoundsMin.y, mapBoundsMax.y);
        Vector3 newPosition = new Vector3(randomX, waterPlane.position.y + 0.1f, randomZ);

        redXMarker.transform.position = newPosition;
        Debug.Log($"New Treasure Location: {newPosition}");

        // Spawn a random map at the Red X marker
        GameObject newMap = Instantiate(maps[Random.Range(0, maps.Length)]);
        newMap.transform.SetParent(redXMarker.transform.GetChild(1)); // Attach to Red X marker
        newMap.transform.localPosition = new Vector3(0, newMap.transform.localPosition.y, 0);

        previousMap = newMap;
    }

    private void UpdateGoldUI()
    {
        goldLabel.text = $"GOLD: {playerGold}G";
    }

    public void UpdateLivesUI(int addRemoveThisNumberOfLives)
    {
        lives += addRemoveThisNumberOfLives;
        hit.Play();
        lifeLabel.text = $"LIVES: {lives}";
    }

    public void AddGold(int amount)
    {
        playerGold += amount;
        UpdateGoldUI();
        Debug.Log($"Gold Added: {amount}. Total Gold: {playerGold}");
    }
}
