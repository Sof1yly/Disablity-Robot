using UnityEngine;

public class DrawLineDebugger : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField] Transform Tracker;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, Tracker.transform.position);
    }
}
