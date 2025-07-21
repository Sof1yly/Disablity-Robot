using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;           // Prefab to spawn
    public int poolSize = 10;           // Size of the pool

    private List<GameObject> pool = new List<GameObject>();

    void Start()
    {
        // Preload objects into the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false); // Initially disable all objects
            pool.Add(obj);
        }
    }

    // Get an object from the pool
    public GameObject GetObject()
    {
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true); // Activate the object

                // *** NEW LINE HERE ***
                // If the object has a PooledObject component, initialize it
                PooledObject pooledComp = obj.GetComponent<PooledObject>();
                if (pooledComp != null)
                {
                    pooledComp.Initialize(this); // Pass a reference to THIS ObjectPool
                }
                // *********************

                return obj;
            }
        }
       float highest = pool.Max(i => i.GetComponent<PooledObject>().LifeSpan);
        GameObject pooledObject = pool.FirstOrDefault(i => i.GetComponent<PooledObject>().LifeSpan == highest);
        pooledObject.SetActive(false);
        pooledObject.SetActive(true);
        return pooledObject;
    }

    // Return an object to the pool
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // Deactivate the object and return to the pool
    }
}