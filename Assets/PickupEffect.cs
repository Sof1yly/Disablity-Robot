using UnityEngine;
using DavidJalbert; // We still need this to access TinyCarController

// This script should be attached to the "showcase item" or "pickup object"
[RequireComponent(typeof(PooledObject))] // Ensure this component is present
public class PickupEffect : MonoBehaviour
{
    

    [Header("Effect Settings")]
    [Tooltip("The duration for which the surface effect on the car lasts.")]
    public float effectDuration = 3f;

    [Tooltip("The surface parameters that will be applied to the car on collision.")]
    public TinyCarSurfaceParameters effectParameters = new TinyCarSurfaceParameters();

    // No need for myTinyCarSurface, etc.

    private void OnTriggerEnter(Collider other)
    {
        ApplySurfaceEffectToPlayer(other.gameObject); // Apply the surface effect
        ReturnPickupObjectToPool();
    }

    void ApplySurfaceEffectToPlayer(GameObject playerCar)
    {
        Debug.Log("Player car collided with " + gameObject.name + "! Applying surface effect.");

        // Get the TinyCarController component from the player car
        DavidJalbert.TinyCarController carController = playerCar.GetComponent<DavidJalbert.TinyCarController>();

        if (carController != null)
        {
            // Pass the directly defined effectParameters to the car controller
            carController.ApplyTemporarySurfaceEffect(effectParameters, effectDuration);
        }
        else
        {
            Debug.LogWarning("Player car does not have a TinyCarController component. Cannot apply surface effect.");
        }
    }

    // *** NEW METHOD TO RETURN TO POOL ***
    void ReturnPickupObjectToPool()
    {
        Debug.Log(gameObject.name + " returning to pool.");
        PooledObject pooledComp = GetComponent<PooledObject>();
        if (pooledComp != null)
        {
            pooledComp.ReturnToPool();
        }
        else
        {
            // Fallback if somehow PooledObject is missing (shouldn't happen with RequireComponent)
            Debug.LogError("PickupEffect: PooledObject component missing on pickup item " + gameObject.name + ". Destroying directly.", this);
            Destroy(gameObject);
        }
    }
}