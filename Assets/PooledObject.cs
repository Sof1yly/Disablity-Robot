using UnityEngine;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;
public class PooledObject : MonoBehaviour
{
    private ObjectPool parentPool; // Renamed for clarity, matches previous explanation

    // This method is called by the ObjectPool when the object is retrieved
    public void Initialize(ObjectPool pool)
    {
        parentPool = pool;
        // No timer reset here for pickups
    }

    // Call this method when you want to "destroy" the object and return it to its pool
    public void ReturnToPool()
    {
        if (parentPool != null)
        {
            parentPool.ReturnObject(this.gameObject);
        }
        else
        {
            // Fallback: If for some reason it wasn't initialized by a pool, just destroy it.
            // This shouldn't happen if you always get objects via GetObject().
            Debug.LogWarning("PooledObject tried to return to pool but has no parent pool reference. Destroying directly.", this);
            Destroy(this.gameObject);
        }
    }


    public float LifeSpan ;
    private void Update()
    {
        LifeSpan += Time.deltaTime;
    }

    private void OnDisable()
    {
        LifeSpan = 0;
    }
    // No Update() method for timer for pickups
}