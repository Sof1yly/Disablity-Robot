using System.Collections;
using UnityEngine;
public class PlayerJoinScript : MonoBehaviour
{
    public Transform SpawnPoint1, SpawnPoint2;
    public GameObject Player1, Player2;

    private void Awake()
    {

        StartCoroutine(SpawnDelay());
    }


    IEnumerator SpawnDelay()
    {
        Instantiate(Player1, SpawnPoint1.position, SpawnPoint1.rotation);

        yield return new WaitForSeconds(3);
        Instantiate(Player2, SpawnPoint2.position, SpawnPoint1.rotation);

    }
}
