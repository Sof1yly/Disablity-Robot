using DavidJalbert;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Ability/BananaItem")]
public class BananaSpawn : ItemAbility
{
    public GameObject bananaPrefab;
    [Tooltip("Initial launch speed")]
    public float throwForce = 12f;
    [Tooltip("How far in front of the player to spawn")]
    public float spawnDistance = 4.5f;
    [Tooltip("How high above the ground to spawn")]
    public float spawnHeight = 1f;
    [Tooltip("Extra upward component to the throw")]
    public float arcFactor = 0.5f;
  

    public override void Activate(GameObject target)
    {
        Vector3 basePos = target.transform.position + target.transform.forward * spawnDistance;
        Vector3 spawnPos = new Vector3(basePos.x, target.transform.position.y + spawnHeight, basePos.z);


        GameObject banana = Instantiate(bananaPrefab, spawnPos, Quaternion.identity);


        var rb = banana.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 dir = (target.transform.forward + Vector3.up * arcFactor).normalized;
            rb.linearVelocity = dir * throwForce;
        }
    }

}
