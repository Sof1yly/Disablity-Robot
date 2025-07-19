using UnityEngine;

public class MiniMapScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform minimapRect;   // UI minimap image
    [SerializeField] private RectTransform blip;          // Player icon
    [SerializeField] private Transform target;            // Player or tracked object

    [Header("Map Settings")]
    [SerializeField] private Vector2 worldSize = new Vector2(1919.13f, 2106.35f); // Full size of the world (X,Z)
    [SerializeField] private Vector2 mapCenterOffset = new Vector2(73.82489f, -519.6849f); // World center of the map image

    private void Update()
    {
        if (target == null || minimapRect == null || blip == null)
            return;

        Vector3 worldPos = target.position;

        // Normalize position based on map center and world size
        float relativeX = (worldPos.x - mapCenterOffset.x) / worldSize.x;
        float relativeY = (worldPos.z - mapCenterOffset.y) / worldSize.y;

        // Convert to local minimap coordinates (centered at 0,0)
        float posX = relativeX * minimapRect.sizeDelta.x;
        float posY = relativeY * minimapRect.sizeDelta.y;

        blip.anchoredPosition = new Vector2(posX, posY);
    }
}
