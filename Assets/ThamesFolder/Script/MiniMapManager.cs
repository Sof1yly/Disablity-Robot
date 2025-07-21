using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    public GameObject playerPrefab;  // Assign your player prefab in Inspector
    private GameObject playerInstance;

    void Start()
    {
        Vector3 spawnPos = new Vector3(0, 0, 0); // Choose spawn position
        playerInstance = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
    }
}
