using UnityEngine;

public class AutoAddMeshCollider : MonoBehaviour
{
    [ContextMenu("Add Mesh")]
    void AddMeshColliders()
    {
        // Get all MeshRenderer components in children
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in meshRenderers)
        {
            GameObject child = renderer.gameObject;

            // Check if it already has a MeshCollider
            if (child.GetComponent<MeshCollider>() == null)
            {
                MeshFilter filter = child.GetComponent<MeshFilter>();
                if (filter != null && filter.sharedMesh != null)
                {
                    MeshCollider collider = child.AddComponent<MeshCollider>();
                    collider.sharedMesh = filter.sharedMesh;
                }
                else
                {
                    Debug.LogWarning($"Missing MeshFilter or mesh on {child.name}, cannot add MeshCollider.");
                }
            }
        }
    }

    [SerializeField] string tagName;

    [ContextMenu("Set Tag")]
    void SetTag()
    {
        MeshCollider[] meshRenderers = GetComponentsInChildren<MeshCollider>();
        foreach (MeshCollider renderer in meshRenderers)
        {
            GameObject child = renderer.gameObject;

            renderer.tag = tagName;
            // Check if it already has a MeshCollider

        }

    }


}
