using UnityEngine;

public class PartSpawner : MonoBehaviour
{        
    [SerializeField] private Transform playerTransform; 

    public void SpawnPart(Body bodySO)
    {
        if(bodySO == null)
        {
            Debug.LogError("no bodyPart assign");
            return;
        }

        GameObject prefab = bodySO.PartPrefabs;

        if(prefab == null)
        {
            Debug.LogError("Prefab is not assigned in BodySo");
            return;
        }

        Instantiate(prefab, playerTransform.position, playerTransform.rotation);

    }

}
