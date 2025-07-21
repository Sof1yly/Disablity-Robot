using UnityEngine;
using System.Linq;
using System.Collections.Generic; // Required for LINQ extensions like ToList()

public class RandomSpawnerOnMesh : MonoBehaviour
{
    public MeshCollider meshCollider;     // Assign in inspector
    public ObjectPool objectPool;         // Reference to ObjectPool script
    public float spawnInterval = 2f;      // Interval in seconds
    public int maxTries = 10;             // Max attempts to find a valid point

    private float[] triangleAreas;
    private float totalMeshArea;

    void Start()
    {
        PrecalculateTriangleAreas();
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    void PrecalculateTriangleAreas()
    {
        if (meshCollider == null || meshCollider.sharedMesh == null)
        {
            Debug.LogError("MeshCollider or Mesh not assigned!");
            return;
        }

        Mesh mesh = meshCollider.sharedMesh;
        int[] tris = mesh.triangles;
        Vector3[] verts = mesh.vertices;

        triangleAreas = new float[tris.Length / 3];
        totalMeshArea = 0f;

        for (int i = 0; i < tris.Length / 3; i++)
        {
            Vector3 v0 = verts[tris[i * 3]];
            Vector3 v1 = verts[tris[i * 3 + 1]];
            Vector3 v2 = verts[tris[i * 3 + 2]];

            // Calculate the area of the triangle using the cross product
            // Area = 0.5 * |(v1 - v0) x (v2 - v0)|
            float area = 0.5f * Vector3.Cross(v1 - v0, v2 - v0).magnitude;
            triangleAreas[i] = area;
            totalMeshArea += area;
        }
    }

    public float spawnOffsetHeight = 0.1f; // Adjust this value in the Inspector

    [SerializeField] List<Vector3> spawnPos = new List<Vector3>();
    void SpawnObject()
    {
        Vector3 spawnPoint;
        if (GetRandomPointOnMesh(meshCollider, out spawnPoint))
        {
            // Apply the offset to the Y-coordinate
            spawnPoint.y += spawnOffsetHeight;
            GameObject obj = objectPool.GetObject();
            if (obj == null) return;
            spawnPos.Add(spawnPoint);
            obj.transform.position = spawnPoint;  // Set the position with the offset
            obj.transform.rotation = Quaternion.identity; // Reset rotation (or you can set a specific rotation)

         
        }
    }

    bool GetRandomPointOnMesh(MeshCollider collider, out Vector3 point)
    {
        point = Vector3.zero;
        Mesh mesh = collider.sharedMesh;

        if (mesh == null || mesh.triangles.Length == 0 || totalMeshArea == 0)
            return false;

        // Choose a triangle based on its area probability
        float randomAreaValue = Random.Range(0f, totalMeshArea);
        float currentAreaSum = 0f;
        int selectedTriangleIndex = -1;

        for (int i = 0; i < triangleAreas.Length; i++)
        {
            currentAreaSum += triangleAreas[i];
            if (randomAreaValue <= currentAreaSum)
            {
                selectedTriangleIndex = i;
                break;
            }
        }

        if (selectedTriangleIndex == -1)
        {
            // Fallback: If for some reason a triangle wasn't selected (shouldn't happen with correct logic),
            // pick a random one directly.
            selectedTriangleIndex = Random.Range(0, triangleAreas.Length);
        }

        int[] tris = mesh.triangles;
        Vector3[] verts = mesh.vertices;

        // The * 3 is crucial here to get the start index of the triangle in the tris array
        Vector3 v0 = verts[tris[selectedTriangleIndex * 3]];
        Vector3 v1 = verts[tris[selectedTriangleIndex * 3 + 1]];
        Vector3 v2 = verts[tris[selectedTriangleIndex * 3 + 2]];

        // Get random point inside the selected triangle using barycentric coordinates
        Vector3 randomPoint = GetRandomPointInTriangle(v0, v1, v2);

        // Transform the point from local to world coordinates
        point = collider.transform.TransformPoint(randomPoint);
        return true;
    }

    Vector3 GetRandomPointInTriangle(Vector3 v0, Vector3 v1, Vector3 v2)
    {
        float r1 = Random.value;
        float r2 = Random.value;

        if (r1 + r2 >= 1f)
        {
            r1 = 1f - r1;
            r2 = 1f - r2;
        }

        return v0 + r1 * (v1 - v0) + r2 * (v2 - v0);
    }
}