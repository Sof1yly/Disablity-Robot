using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Ability/BananaItem")]
public class BananaSpawn : ItemAbility
{
    public GameObject bananaPrefab;  
    public float throwForce = 12f;   
    public float spawnDistance = 4.5f; 

    public override void Activate(GameObject target)
    {
        Vector3 spawnPos = target.transform.position + target.transform.forward * spawnDistance;
        spawnPos.y = target.transform.position.y; 

 
        GameObject banana = Instantiate(bananaPrefab, spawnPos, Quaternion.identity);

        Rigidbody bananaRb = banana.GetComponent<Rigidbody>();
        if (bananaRb != null)
        {
            Vector3 throwDirection = target.transform.forward + Vector3.up * 0.5f; 
            bananaRb.linearVelocity = throwDirection.normalized * throwForce;
        }
    }
}
