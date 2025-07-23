using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Ability/BananaItem")]
public class BananaSpawn: ItemAbility
{
    public GameObject trapPrefab;
    public float spawnDistance = 5f;

    public override void Activate(GameObject target)
    {
        Debug.Log("Spawn banana");
        Vector3 spawnPos = target.transform.position - target.transform.forward * spawnDistance;
        Quaternion spawnRot = Quaternion.identity;
        Instantiate(trapPrefab, spawnPos, spawnRot);
    }
}
