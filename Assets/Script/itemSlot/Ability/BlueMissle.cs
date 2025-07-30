using DavidJalbert;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Ability/BlueMissile")]
public class BlueMissle : ItemAbility
{
    [Tooltip("Prefab with the HomingMissile component")]
    public GameObject missilePrefab;

    [Tooltip("How many to fire when used")]
    public int missileCount = 1;

    [Tooltip("Local offset from user when spawning")]
    public Vector3 spawnOffset = new Vector3(0, 2, 0);


    public override void Activate(GameObject user)
    {

        Vector3 origin = user.transform.position + spawnOffset;
        Quaternion rot = user.transform.rotation;
        for (int i = 0; i < missileCount; i++)
        {
            GameObject missile = Instantiate(missilePrefab, origin, rot);
            BlueMissleScript missleScript = missile.GetComponent<BlueMissleScript>();
            if (missleScript != null)
            {
                missleScript.SetOwner(user);
            }
        }
        Debug.Log($"Spawned {missileCount} homing missiles");
    }
}
