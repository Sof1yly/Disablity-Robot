using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastReflection : MonoBehaviour
{
    public int reflections;
    public float maxLength;

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }

    private void Update()
    {
        ray = new Ray(transform.position, transform.forward);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++) {
            if(Physics.Raycast(ray, out hit, remainingLength))
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                // Calculate the reflection direction
                direction = Vector3.Reflect(ray.direction, hit.normal);

                // Update the ray for the next iteration
                ray = new Ray(hit.point, direction);

                // Reduce the remaining length
                remainingLength -= Vector3.Distance(ray.origin, hit.point);

                if(hit.collider.tag != "Mirror")
                {
                    break;
                }
            }
            else
            {
                // If no hit, just extend the line to the max length
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
                break;
            }
        }
    }
}
