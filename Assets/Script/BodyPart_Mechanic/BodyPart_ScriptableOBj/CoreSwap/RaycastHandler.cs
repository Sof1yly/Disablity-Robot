using UnityEngine;

public class RaycastHandler : MonoBehaviour
{
    private Camera mainCamera;

    public RaycastHandler(Camera camera)
    {
        mainCamera = camera;            
    }


    public RaycastHit? CastRay(Vector3 origin, Vector3 direction, float maxDistance, LayerMask layerMask)
    {
        RaycastHit hit;
        Debug.DrawRay(origin, direction * maxDistance, Color.green); 
        if (Physics.Raycast(origin, direction, out hit, maxDistance, layerMask))
        {
            return hit; 
        }
        return null; 
    }

    public void HandleReflection(RaycastHit hit, float maxDistance, LayerMask layerMask, System.Action<Transform> swapAction)
    {
        // Reflect the ray on the Mirror
        Vector3 reflectionDirection = Vector3.Reflect(hit.point - mainCamera.transform.position, hit.normal);

        // Debugging the reflected raycast line
        Debug.DrawRay(hit.point, reflectionDirection * maxDistance, Color.red);

        RaycastHit? reflectedHit = CastRay(hit.point, reflectionDirection, maxDistance, layerMask);

        if (reflectedHit.HasValue)
        {
            Debug.Log($"[Reflection] Raycast reflected: {reflectedHit.Value.transform.name}");

            // If the reflected ray hits a Core, swap it
            if (reflectedHit.Value.transform.CompareTag("Core"))
            {
                swapAction(reflectedHit.Value.transform);
            }
        }
        else
        {
            Debug.Log("[Reflection] Reflection missed.");
        }
    }
}
